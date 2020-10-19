Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Math
'12/01/2015 PUCCIA Nuova connessione  tls ssl
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Partial Class RATEIZZAZIONE_ProspettoRateiz2
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim pp As New MavOnline.MAVOnlineBeanService
    Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
    Dim Esito As New MavOnline.rispostaMAVOnlineWS
    Dim binaryData() As Byte
    Dim outFile As System.IO.FileStream
    Dim outputFileName As String = ""
    Dim myExcelFile As New CM.ExcelFile
    Dim sNomeFile As String
    Dim dt As Data.DataTable
    Dim ContaRate As Integer = 0

    Public Property vIdBolletta() As String
        Get
            If Not (ViewState("par_IdBolletta") Is Nothing) Then
                Return CStr(ViewState("par_IdBolletta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdBolletta") = value
        End Set

    End Property
    Public Property vIdBolletta1() As String
        Get
            If Not (ViewState("par_IdBolletta1") Is Nothing) Then
                Return CStr(ViewState("par_IdBolletta1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdBolletta1") = value
        End Set

    End Property

    Public Property vIdTipoBol() As String
        Get
            If Not (ViewState("par_IdTipoBol") Is Nothing) Then
                Return CStr(ViewState("par_IdTipoBol"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdTipoBol") = value
        End Set

    End Property
    Public Property vNbolletta() As String
        Get
            If Not (ViewState("par_Nbolletta") Is Nothing) Then
                Return CStr(ViewState("par_Nbolletta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Nbolletta") = value
        End Set

    End Property

    Private Sub AggiungiDt(ByVal Descrizione As String, ByVal Link As String)
        Try
            Dim dtResult As New Data.DataTable
            dtResult = CType(HttpContext.Current.Session.Item("dtResult"), Data.DataTable)
            If IsNothing(dtResult) Then
                dtResult = New Data.DataTable
                dtResult.Columns.Add("DESCRIZIONE")
                dtResult.Columns.Add("LINK")
            End If
            If Descrizione <> "" And Link <> "" Then
                Dim r As System.Data.DataRow
                r = dtResult.NewRow
                r.Item("DESCRIZIONE") = Descrizione
                r.Item("LINK") = Link
                dtResult.Rows.Add(r)

            End If
            Session.Add("dtResult", dtResult)
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "AggiungiDt - " & ex.Message

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        Try

            If Not IsPostBack Then
                vIdBolletta = Session.Item("IDBOLLETTE")
                vIdBolletta1 = Session.Item("IDBOLLESCLUSE")
                dataSaldo.Value = Request.QueryString("DATAS")
                par.caricaComboBox("select id,descrizione from siscom_mi.tipo_stato_rateizz order by id asc", cmbSTRateizz, "ID", "DESCRIZIONE", True)

                Me.cmbSTRateizz.SelectedValue = 0

                If Not String.IsNullOrEmpty(vIdBolletta) Then
                    CaricaDettaglio()
                    'SololetturaAccesso()
                    Me.btnPrint.Visible = True
                Else
                    Response.Write("<script>alert('Impossibile trovare una bolletta!')</script>")
                End If
                par.caricaComboBox("select id,VALORE_PERC from SISCOM_MI.PERCENTUALI_ACCONTO_RATEIZZ where id in (select id_percentuale from SISCOM_MI.acconto_rateizzazione,SISCOM_MI.DETT_TIPI_ACCONTO_RATEIZZ where id_dett_tipo_acconto=id and (dettaglio='" & classe.Value & "' or dettaglio='" & codTipoRU.Value & "')) order by id asc", cmbPercentuale, "ID", "VALORE_PERC", True)

                If cmbPercentuale.Items.Count > 1 Then
                    cmbPercentuale.Visible = True
                    lblPerc.Visible = True
                Else
                    cmbPercentuale.Visible = False
                    lblPerc.Visible = False
                End If


                txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtData.Attributes.Add("onBlur", "javascript:ControlDate(this);")

                txtscadBollettino.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtscadBollettino.Attributes.Add("onBlur", "javascript:ControlDate(this);")

                txtVersAnticipo.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);Sottrai();")
                txtVersAnticipo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                txtCapitale.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                txtCapitale.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")


                txtNRate.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');")

            End If

            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Page_Load - " & ex.Message

        End Try

    End Sub
    Private Sub Sololettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                End If
                'Me.btnPrint.Visible = False
                Me.cmbSTRateizz.Enabled = False
                Me.btnSa.Visible = False
                Me.BtnSimula.Visible = False
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Sololettura - " & ex.Message

        End Try
    End Sub
    Private Sub SololetturaAccesso()
        Try
            Dim CTRL As Control = Nothing
            'Me.chkPagato.Enabled = False
            Me.btnSa.Visible = False

            'Me.BtnSimula.Visible = False
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Sololettura - " & ex.Message

        End Try
    End Sub

    Private Sub StatoRateizzo()
        If cmbSTRateizz.SelectedValue >= 1 Then
            Sololettura()

        End If
    End Sub
    Private Sub CaricaDettaglio()
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            idRateizz.Value = Request.QueryString("IDRAT")

            Dim testoTabella As String = ""
            Dim testoTabellaVoci As String = ""
            Dim Contatore As Integer = 0
            Dim COLORE As String = "#E6E6E6"
            Dim bTrovato As Boolean = False
            Dim TotDaPagare As Double = 0
            Dim TotFinalePagato As Double = 0
            Dim TotFinaleDaPagare As Double = 0
            Dim TotRateizzazione As Decimal = 0
            testoTabella = "<div style='overflow: auto; width: 100%; height: 150px'><table cellpadding='0' cellspacing='0' width='98%'>" & vbCrLf _
                & "<tr>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial;'><strong></strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUM. BOLLETTA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial;'><strong>TIPO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUM. RATA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISS.</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                & "<td style='height: 19px; text-align:center'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI DELLA BOLLETTA</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO TOT.</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO PAG.</strong></span></td>" _
                & "</tr>"
            testoTabellaVoci = "<div style='overflow: auto; width: 100%; height: 100px'><table cellpadding='0' cellspacing='0' width='92%'>" _
                & "<tr>" _
                & "<td style='height: 15'>" _
                & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                & "<td style='height: 15px;text-align:right'>" _
                & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMP.</strong></span></td>" _
                & "<td style='height: 15px;text-align:right'>" _
                & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMP.PAGATO</strong></span></td>" _
                & "</tr>"


            Dim s As String = "SELECT BOL_BOLLETTE.*, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO , " _
                    & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA " _
                    & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE, SISCOM_MI.ANAGRAFICA " _
                    & "WHERE  BOL_BOLLETTE.ID in (#SOST#) AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO  " _
                    & "AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID "

            par.cmd.CommandText = QueryIN(vIdBolletta, s, "#SOST#")


            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Do While reader.Read
                TotDaPagare = 0

                If par.IfNull(reader("FL_ANNULLATA"), 0) <> 0 Then
                    Sololettura()
                    Response.Write("<script>alert('Impossibile rateizzare.La bolletta risulta essere annullata!');</script>")
                End If

                idContratto.Value = par.IfNull(reader("ID_CONTRATTO"), 0)
                vNbolletta = ""
                If par.IfNull(reader("N_RATA"), "") = "99" Then
                    vNbolletta = "MA/" & par.IfNull(reader("ANNO"), "")
                ElseIf par.IfNull(reader("N_RATA"), "") = "999" Then
                    vNbolletta = "AU/" & par.IfNull(reader("ANNO"), "")
                ElseIf par.IfNull(reader("N_RATA"), "") = "99999" Then
                    vNbolletta = "CO/" & par.IfNull(reader("ANNO"), "")
                Else
                    vNbolletta = par.IfNull(reader("N_RATA"), "") & "/" & par.IfNull(reader("ANNO"), "")
                End If


                Contatore = Contatore + 1
                If par.IfNull(reader("FL_ANNULLATA"), "0") = "0" Then

                    testoTabella = testoTabella _
                    & "<tr>" _
                    & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial;vertical-align :top '>" & Contatore & ")</span></td>" _
                    & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.IfNull(reader("NUM_BOLLETTA"), "") & "</span></td>"


                    If par.IfNull(reader("TIPO"), "n.d.") = "MOR" Or par.IfNull(reader("TIPO"), "n.d.") = "FIN" Then
                        testoTabella = testoTabella _
                        & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;vertical-align :top '>" & par.IfNull(reader("TIPO"), "n.d.") & "</span></td>"
                    Else
                        testoTabella = testoTabella _
                        & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;vertical-align :top '>" & par.IfNull(reader("TIPO"), "n.d.") & "</span></td>"
                    End If
                    testoTabella = testoTabella _
                    & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & vNbolletta & "</span></td>" _
                    & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(reader("DATA_EMISSIONE"), "")) & "</span></td>" _
                    & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(reader("DATA_SCADENZA"), "")) & "</span></td>" _
                    & "<td style='height: 19px;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"

                    par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(reader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                    Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                    Do While lettore2.Read
                        testoTabella = testoTabella _
                        & "<tr bgcolor = '" & COLORE & "'>" _
                        & "<td style='height: 15px'>" _
                        & "<span style='font-size: 6pt; font-family: Courier New'>" & par.IfNull(lettore2("DESCRIZIONE"), "") & "</span></td>" _
                        & "<td style='height: 15px;text-align:right'>" _
                        & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(lettore2("IMPORTO"), 0)), "##,##0.00") & "</span></td>" _
                        & "<td style='height: 15px;text-align:right'>" _
                        & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(lettore2("IMP_PAGATO"), 0)), "##,##0.00") & "</span></td>"

                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If
                    Loop
                    TotDaPagare = par.IfNull(reader("IMPORTO_TOTALE"), 0) - par.IfNull(reader("QUOTA_SIND_B"), 0)
                    lettore2.Close()
                    testoTabella = testoTabella & "</table></div></td>" _
                    & "<td style='height: 19px;text-align:right;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                    & "<td style='height: 19px;text-align:right;border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF'>" _
                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format((par.IfNull(reader("IMPORTO_PAGATO"), 0) - par.IfNull(reader("QUOTA_SIND_PAGATA_B"), 0)), "##,##0.00") & "</span></td></tr>"

                    TotFinalePagato = TotFinalePagato + par.IfNull(reader("IMPORTO_PAGATO"), 0) - par.IfNull(reader("QUOTA_SIND_PAGATA_B"), 0)

                    TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare

                End If


                Me.lblIntestatario.Text = par.IfNull(reader("INTECONTRATTO"), "n.d")
                Me.lblCFIVA.Text = par.IfNull(reader("CFIVA"), "n.d")
                vIdTipoBol = par.IfNull(reader("ID_TIPO"), 0)

                TotRateizzazione = TotRateizzazione + ((par.IfNull(reader("IMPORTO_TOTALE"), 0) - par.IfNull(reader("QUOTA_SIND_B"), 0)) - (par.IfNull(reader("IMPORTO_PAGATO"), 0) - par.IfNull(reader("QUOTA_SIND_PAGATA_B"), 0)))
            Loop
            reader.Close()



            par.cmd.CommandText = "SELECT cod_contratto,cod_tipologia_contr_loc,cod_unita_immobiliare FROM SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.UNITA_CONTRATTUALE WHERE RAPPORTI_UTENZA.ID = " & idContratto.Value _
            & " AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND ID_UNITA_PRINCIPALE IS NULL "
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                codTipoRU.Value = par.IfNull(MyReader("cod_tipologia_contr_loc"), "ERP")
                lblCodiceRu.Text = par.IfNull(MyReader("cod_contratto"), "COD_RU")
                codUI.value = par.IfNull(MyReader("cod_unita_immobiliare"), "")
            End If
            MyReader.Close()

            testoTabella = testoTabella _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                        & "</tr></table></div>"
            Me.TBL_RIEPILOGO.Text = testoTabella
            If par.IfEmpty(idRateizz.Value, 0) <> 0 Then
                CaricaDatiRateizz()
            End If
            Me.totDebito.Text = Format(Math.Round(CDec(saldo.Value), 2), "##,##0.00")
            Me.txtImporto.Text = Format(Math.Round(TotRateizzazione, 2), "##,##0.00")
            Me.txtCapitale.Text = Format(TotRateizzazione - Me.txtVersAnticipo.Text, "##,##0.00")
            Me.CapitaleRateiz.Value = txtCapitale.Text
            Me.txtscadBollettino.Text = Date.Parse(Format(Now, "dd/MM/yyyy")).AddDays(15).ToString("dd/MM/yyyy")
            Me.txtData.Text = Date.Parse(Format(Now, "dd/MM/yyyy")).AddMonths(1).ToString("dd/MM/yyyy")
            Me.txtData.Text = "01" & txtData.Text.Substring(2)


            'Segn.2217/2018 sostituisco la query errata con quella corretta a seguito di richiesta della Marconi
            'par.cmd.CommandText = "select max_num_rate from siscom_mi.PARAM_RATEIZZ_MOROSITA where ID_AREA_ECONOMICA = " & fascia.Value & " and importo_da<=" & par.VirgoleInPunti(Me.CapitaleRateiz.Value.Replace(".", "")) & " and importo_a>=" & par.VirgoleInPunti(Me.CapitaleRateiz.Value.Replace(".", "")) & " "

            par.cmd.CommandText = "select max_num_rate from siscom_mi.PARAM_RATEIZZ_MOROSITA where ID_AREA_ECONOMICA = " & fascia.Value & " and importo_da<=" & par.VirgoleInPunti(saldo.Value) & " and importo_a>=" & par.VirgoleInPunti(saldo.Value) & " "
            MaxNumRate.Value = par.cmd.ExecuteScalar
            Me.lblN.Text = "Num. rate (MAX " & MaxNumRate.Value & ")"
            If par.IfEmpty(Me.txtNRate.Text, 0) = 0 Then
                Me.txtNRate.Text = MaxNumRate.Value
            End If

            Dim importoMaxRateizzabile As Decimal = 0
            Dim percentuale As Integer = 0
            Select Case fascia.Value
                Case 1, 2, 3
                    importoMaxRateizzabile = (Math.Round(CDec(reddMensile.Value) / 8)) * MaxNumRate.Value

                    If TotRateizzazione > importoMaxRateizzabile Then
                        txtImpRateizzabile.Text = Format(importoMaxRateizzabile, "##,##0.00")
                    Else
                        txtImpRateizzabile.Text = Format(TotRateizzazione, "##,##0.00")
                    End If

                Case Else
                    percentuale = CInt(cmbPercentuale.SelectedItem.Text) 'RicavaPercAnticipo()
                    txtVersAnticipo.Text = Format(Math.Round(TotRateizzazione * (percentuale / 100), 2), "##,##0.00")

                    importoMaxRateizzabile = (Math.Round(CDec(reddMensile.Value) / 8)) * MaxNumRate.Value

                    If TotRateizzazione > importoMaxRateizzabile Then
                        txtImpRateizzabile.Text = Format(importoMaxRateizzabile, "##,##0.00")
                    Else
                        txtImpRateizzabile.Text = Format(TotRateizzazione, "##,##0.00")
                    End If
                    Me.txtCapitale.Text = Format(TotRateizzazione - CDec(Me.txtVersAnticipo.Text), "##,##0.00")
                    Me.CapitaleRateiz.Value = txtCapitale.Text
            End Select


            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaDettagli - " & ex.Message

        End Try

    End Sub

    Private Function RicavaPercAnticipo() As Integer

        Dim acconto As Decimal = 0

        par.cmd.CommandText = "select acconto_rateizzazione.* from siscom_mi.tipi_acconto_rateizz,siscom_mi.acconto_rateizzazione where tipi_acconto_rateizz.id=acconto_rateizzazione.id_tipo_acconto " _
            & " and acconto_rateizzazione.classe= '" & classe.Value & "' and tipi_acconto_rateizz.id_area_economica=" & fascia.Value & "" _
            & " and isee_erp_da<=" & par.VirgoleInPunti(isee.Value) & " and isee_erp_a>=" & par.VirgoleInPunti(isee.Value) & " "
        Dim lettoreAcc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreAcc.Read Then
            acconto = par.IfNull(lettoreAcc("percentuale"), 0)
        End If
        lettoreAcc.Close()



        Return acconto

    End Function

    Private Function ControlliEmissRateizz(ByVal idAreaEconomica As Integer, ByRef importoRata As Decimal) As Boolean

        Dim Controlli As Boolean = True
        Dim msgAnomalia As String = ""
        Dim redditoMensileRival As Decimal = 0

        If idAreaEconomica = 1 Or idAreaEconomica = 2 Then
            If par.IfEmpty(Me.txtVersAnticipo.Text, 0) <> 0 Then
                msgAnomalia = "Valore diverso da 0 per ACCONTO non previsto."
                Controlli = False
            End If
        End If
        Dim importoMaxRateizzabile As Decimal = 0
        If Controlli = True Then
            importoRata = Math.Round(CDec(txtCapitale.Text.Replace(".", "")) / txtNRate.Text, 2)
            redditoMensileRival = Math.Round(CDec(reddMensile.Value) / 8, 2)

            If par.IfEmpty(Me.txtNRate.Text, 0) <= CDec(MaxNumRate.Value) Then
                If importoRata <= redditoMensileRival Then
                    Controlli = True
                Else
                    If par.IfEmpty(Me.txtNRate.Text, 0) = CDec(MaxNumRate.Value) Then
                        importoMaxRateizzabile = Math.Round(redditoMensileRival * MaxNumRate.Value, 2)
                        importoRata = importoMaxRateizzabile / MaxNumRate.Value
                        txtImpRate.Text = importoRata
                        Controlli = True
                    Else
                        importoRata = 0
                        msgAnomalia = "Numero rate non sufficiente per emettere la rateizzazione"
                        Controlli = False
                    End If
                End If
            Else
                importoRata = 0
                msgAnomalia = "Numero rate maggiore del limite massimo"
                Controlli = False
            End If

        End If

        If Not String.IsNullOrEmpty(msgAnomalia) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE! " & msgAnomalia & "\nOPERAZIONE ANNULLATA!');", True)
            Exit Function
        End If

        Return Controlli
    End Function

    Private Sub CaricaDatiRateizz()

        par.cmd.CommandText = "SELECT BOL_RATEIZZAZIONI.*,AREA_ECONOMICA.descrizione ||' '|| classe as fascia FROM siscom_mi.BOL_RATEIZZAZIONI,siscom_mi.AREA_ECONOMICA WHERE BOL_RATEIZZAZIONI.id_AREA_ECONOMICA=area_Economica.id and BOL_RATEIZZAZIONI.ID = " & idRateizz.Value
        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If MyReader.Read Then
            Me.cmbSTRateizz.SelectedValue = par.IfNull(MyReader("ID_STATO"), "-1")

            Me.lblFascia.Text = par.IfNull(MyReader("FASCIA"), "")
            classe.Value = par.IfNull(MyReader("CLASSE"), "")
            fascia.Value = par.IfNull(MyReader("ID_AREA_ECONOMICA"), 1)
            reddMensile.Value = Format(Math.Round((par.IfNull(MyReader("REDDITO_COMPLESSIVO"), 0) * 2 / 3) / 12, 2), "##,##0.00")
            saldo.Value = par.IfNull(MyReader("SALDO"), 0)
            isee.Value = par.IfNull(MyReader("ISEE"), 0)
            Me.lblTassoInt.Text = par.IfNull(MyReader("TASSO_INTERESSE"), "0") & "%"
            Me.txtNRate.Text = par.IfNull(MyReader("NUM_RATE"), "0")
            Me.txtNote.Text = par.IfNull(MyReader("NOTE"), "")
            idDich.Value = par.IfNull(MyReader("ID_DIC_REDDITI"), 0)
            If par.IfNull(MyReader("IMP_ANTICIPO"), 0) <> 0 Then
                Me.txtVersAnticipo.Text = Format(Math.Round(CDec(par.IfNull(MyReader("IMP_ANTICIPO"), 0)), 2), "##,##0.00")
                Me.impAnticipo.Value = par.IfNull(MyReader("IMP_ANTICIPO"), 0)
            End If
        End If
        MyReader.Close()

        txtReddMensile.Text = reddMensile.Value
        totDebito.Text = 1
    End Sub

    Protected Sub BtnSimula_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSimula.Click
        Dim importoRata As Decimal = 0
        If ControlliEmissRateizz(fascia.Value, importoRata) = True Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "funzSim", "Simulazione();", True)
        End If

        'If Not String.IsNullOrEmpty(Me.txtNRate.Text) Then
        '    Me.txtNRate.Text = NumRate.Value
        'Else
        '    Exit Sub
        'End If

    End Sub
    Private Function EmettiMavRata(ByVal idRata As String) As Boolean
        EmettiMavRata = False

        Dim ConOpenNow As Boolean = False
        Try

            Dim dtRata As New Data.DataTable


            Dim APPLICABOLLO As Double = 0
            Dim SPESEmav As Double = 0
            Dim BOLLO As Double = 0
            Dim Tot_Bolletta As Double = 0
            Dim TipoIngiunzione As String = ""
            Dim CodiceContratto As String = ""
            'Dim VOCE As String = ""
            Dim IdContratto As String = ""
            Dim IdAnagrafica As String = "-1"
            Dim ImpCapitale As Decimal = 0
            Dim ImpInteressi As Decimal = 0
            Dim presso_cor As String = ""
            Dim civico_cor As String = ""
            Dim luogo_cor As String = ""
            Dim cap_cor As String = ""
            Dim indirizzo_cor As String = ""
            Dim tipo_cor As String = ""
            Dim sigla_cor As String = ""
            Dim idUnita As String = ""
            Dim idEdificio As String = ""
            Dim idComplesso As String = ""
            Dim ScadenzaBollettino As String = ""
            Dim periodo As String
            Dim causalepagamento As String = ""
            Dim num_bollettino As String = ""


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID =" & idRata
            Dim daRata As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daRata.Fill(dtRata)

            If par.IfNull(dtRata.Rows(0).Item("QUOTA_CAPITALI"), 0) <= 0 And par.IfNull(dtRata.Rows(0).Item("QUOTA_INTERESSI"), 0) <= 0 Then
                EmettiMavRata = True
                Exit Function
            End If

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            Dim s As String = "SELECT BOL_BOLLETTE.*, ANAGRAFICA.ID AS ID_ANA , anagrafica.sesso, " _
            & "ragione_sociale, COGNOME ,NOME, " _
            & "PARTITA_IVA, COD_FISCALE , RAPPORTI_UTENZA.COD_CONTRATTO" _
            & " FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE, SISCOM_MI.ANAGRAFICA, SISCOM_MI.RAPPORTI_UTENZA " _
            & "WHERE  BOL_BOLLETTE.ID in (#SOST#) AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO " _
            & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO "

            par.cmd.CommandText = QueryIN(vIdBolletta, s, "#SOST#")



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                If Len(par.IfNull(dt.Rows(0).Item("PARTITA_IVA"), 0)) = 11 Or par.ControllaCF(par.IfNull(dt.Rows(0).Item("COD_FISCALE"), 0)) = True Then
                    Tot_Bolletta = 0

                    TipoIngiunzione = "BOLLETTA RATEIZZAZIONE NUM " & dtRata.Rows(0).Item("NUM_RATA")
                    CodiceContratto = par.IfNull(dt.Rows(0).Item("COD_CONTRATTO"), "")

                    IdAnagrafica = par.IfNull(dt.Rows(0).Item("id_ana"), "")

                    par.cmd.CommandText = "select complessi_immobiliari.id as idcomplesso,edifici.id as idedificio,siscom_mi.rapporti_utenza.*,unita_contrattuale.id_unita from SISCOM_MI.EDIFICI,siscom_mi.complessi_immobiliari,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza where complessi_immobiliari.id=edifici.id_complesso and unita_immobiliari.id=unita_contrattualE.id_unita and edifici.id=unita_immobiliari.id_edificio and unita_contrattuale.id_contratto=rapporti_utenza.id and unita_contrattuale.id_unita_principale is null and cod_contratto='" & CodiceContratto & "'"
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123.Read Then
                        IdContratto = par.IfNull(myReader123("id"), "-1")
                        idUnita = par.IfNull(myReader123("id_unita"), "-1")
                        presso_cor = par.IfNull(myReader123("presso_cor"), "")
                        luogo_cor = par.IfNull(myReader123("luogo_cor"), "")
                        civico_cor = par.IfNull(myReader123("civico_cor"), "")
                        cap_cor = par.IfNull(myReader123("cap_cor"), "")
                        indirizzo_cor = par.IfNull(myReader123("VIA_cor"), "")
                        tipo_cor = par.IfNull(myReader123("tipo_cor"), "")
                        sigla_cor = par.IfNull(myReader123("sigla_cor"), "")
                        idEdificio = par.IfNull(myReader123("idedificio"), "0")
                        idComplesso = par.IfNull(myReader123("idcomplesso"), "0")
                    End If
                    myReader123.Close()

                    Dim Titolo As String = ""
                    Dim Nome As String = ""
                    Dim Cognome As String = ""
                    Dim CF As String = ""

                    Dim ID_BOLLETTA As Long = 0

                    If par.IfNull(dt.Rows(0).Item("ragione_sociale"), "") <> "" Then
                        Titolo = ""
                        Cognome = par.IfNull(dt.Rows(0).Item("ragione_sociale"), "")
                        Nome = ""
                        CF = par.IfNull(dt.Rows(0).Item("partita_iva"), "")
                    Else
                        If par.IfNull(dt.Rows(0).Item("sesso"), "") = "M" Then
                            Titolo = "Sign."
                        Else
                            Titolo = "Sign.ra"
                        End If
                        Cognome = par.IfNull(dt.Rows(0).Item("cognome"), "")
                        Nome = par.IfNull(dt.Rows(0).Item("nome"), "")
                        CF = par.IfNull(dt.Rows(0).Item("cod_fiscale"), "")
                    End If


                    ScadenzaBollettino = ""
                    periodo = Format(Now, "yyyyMMdd") & " - " & Format(Now, "yyyyMMdd")

                    Dim Nome1 As String = ""
                    Dim Nome2 As String = ""

                    If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
                        Nome1 = Cognome & " " & Nome
                        Nome2 = presso_cor
                    Else
                        Nome1 = presso_cor
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE " _
                                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                        & "Values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999 , '" & Format(Now, "yyyyMMdd") _
                                        & "', '" & par.IfNull(dtRata.Rows(0).Item("DATA_SCADENZA"), Format(Now, "yyyyMMdd")) & "', NULL,NULL,NULL,'BOLLETTA RATEIZZAZIONE'," _
                                        & "" & IdContratto _
                                        & " ," & par.RicavaEsercizioCorrente & ", " _
                                        & par.IfNull(dt.Rows(0).Item("ID_UNITA"), 0) _
                                        & ", '0', ''," & par.IfNull(dt.Rows(0).Item("ID_ANA"), 0) _
                                        & ", '" & par.PulisciStrSql(Nome1) & "', " _
                                        & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) _
                                        & "', '" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") _
                                        & "', '" & par.PulisciStrSql(Nome2) & "', '" & Format(Now, "yyyyMMdd") _
                                        & "', '" & Format(Now, "yyyyMMdd") & "', " _
                                        & "'0', " & idComplesso & ", '', NULL, '', " _
                                        & Year(Now) & ", '', " & idEdificio & ", NULL, NULL,'MOD',5)"

                    par.cmd.ExecuteNonQuery()




                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        ID_BOLLETTA = myReaderB(0)
                    Else
                        ID_BOLLETTA = -1
                    End If
                    myReaderB.Close()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_RATEIZZAZIONI_DETT SET ID_BOLLETTA = " & ID_BOLLETTA & " WHERE ID = " & dtRata.Rows(0).Item("ID")
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",677" _
                    & "," & par.VirgoleInPunti(dtRata.Rows(0).Item("QUOTA_CAPITALI")) & ")"
                    par.cmd.ExecuteNonQuery()

                    Tot_Bolletta = Tot_Bolletta + dtRata.Rows(0).Item("QUOTA_CAPITALI")
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",678" _
                                        & "," & par.VirgoleInPunti(dtRata.Rows(0).Item("QUOTA_INTERESSI")) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + dtRata.Rows(0).Item("QUOTA_INTERESSI")


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                            & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()

                    Tot_Bolletta = Tot_Bolletta + SPESEmav

                    If Tot_Bolletta >= APPLICABOLLO Then

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                    & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                        Tot_Bolletta = Tot_Bolletta + BOLLO
                    End If


                    par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=32"
                    Dim letty As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If letty.Read Then
                        causalepagamento = par.IfNull(letty("valore"), "")
                    End If
                    letty.Close()

                    If Session.Item("AmbienteDiTest") = "1" Then
                        causalepagamento = "COMMITEST01"
                        'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    Else
                        'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    End If

                    RichiestaEmissioneMAV.codiceEnte = "commi"
                    RichiestaEmissioneMAV.tipoPagamento = causalepagamento
                    RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
                    RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

                    RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA, par.IfNull(dt.Rows(0).Item("COD_CONTRATTO"), ""))

                    RichiestaEmissioneMAV.scadenzaPagamento = Mid(dtRata.Rows(0).Item("DATA_SCADENZA"), 1, 4) & "-" & Mid(dtRata.Rows(0).Item("DATA_SCADENZA"), 5, 2) & "-" & Mid(dtRata.Rows(0).Item("DATA_SCADENZA"), 7, 2)

                    RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                    RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                    RichiestaEmissioneMAV.codiceFiscaleDebitore = CF


                    RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                    If Nome <> "" Then
                        RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                    End If


                    If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
                        RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                    Else
                        RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                        RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                    End If

                    RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                    RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                    RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                    RichiestaEmissioneMAV.nazioneDebitore = "IT"

                    '/*/*/*/*/*tls v1
                    Dim v As String = ""
                    par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                    v = par.cmd.ExecuteScalar
                    System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                    '/*/*/*/*/*tls v1

                    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler
                    Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                    If Esito.codiceRisultato = "0" Then


                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                        binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length - 1)
                        outFile.Close()


                        num_bollettino = Esito.numeroMAV
                        par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & num_bollettino & "' where  id=" & ID_BOLLETTA
                        par.cmd.ExecuteNonQuery()

                        outputFileName = ("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                        AggiungiDt("MAV RATA" & dtRata.Rows(0).Item("NUM_RATA"), outputFileName.Replace("\", "/"))
                        WriteEvent(IdContratto, "F174", "GENERATA BOLLETTA PER LA RATA N." & dtRata.Rows(0).Item("NUM_RATA"))
                        EmettiMavRata = True
                        ContaRate = ContaRate + 1
                    Else

                        lblErrore.Visible = True
                        lblErrore.Text = "Ci sono stati degli errori durante la fase di creazione.Il MAV on line non è stato creato!!( " & Format(ID_BOLLETTA, "0000000000") & ".xml)"
                        par.cmd.CommandText = "update siscom_mi.bol_bollette set fl_annullata = 1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "' where id=" & ID_BOLLETTA & ""
                        '############ATTENZIONE ALLA DELETE!###############
                        par.cmd.ExecuteNonQuery()

                        Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                        If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml") = True Then
                            FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                        End If

                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml"
                        binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length)
                        outFile.Close()
                    End If

                Else

                    lblErrore.Visible = True
                    lblErrore.Text = "Il bollettino di " & par.IfNull(dt.Rows(0).Item("COGNOME"), "") & " " & par.IfNull(dt.Rows(0).Item("COGNOME"), "") & " non è stati stampati perchè il codice fiscale o la partita iva non hanno un formato corretto!"

                End If
            End If





        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "EmettiMavRata - " & ex.Message
        End Try

    End Function
    '12/01/2015 PUCCIA Nuova connessione  tls ssl
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Function GenerateMav() As Boolean

        GenerateMav = False
        Try
            Dim idRataAnticipo As String = ""
            Dim IdAnagrafica As String = "-1"
            Dim CodiceContratto As String = ""
            Dim ScadenzaPagamento As String = ""
            Dim presso_cor As String = ""
            Dim civico_cor As String = ""
            Dim luogo_cor As String = ""
            Dim cap_cor As String = ""
            Dim indirizzo_cor As String = ""
            Dim tipo_cor As String = ""
            Dim sigla_cor As String = ""
            Dim TipoIngiunzione As String = ""
            Dim Importo As String = "0"
            Dim Tot_Bolletta As Double = 0
            Dim VOCE As String = ""

            Dim IdContratto As String = ""
            Dim idUnita As String = ""
            Dim idEdificio As String = ""
            Dim idComplesso As String = ""
            Dim ScadenzaBollettino As String = ""
            Dim periodo As String
            Dim APPLICABOLLO As Double = 0
            Dim SPESEmav As Double = 0
            Dim BOLLO As Double = 0
            Dim causalepagamento As String = ""
            Dim num_bollettino As String = ""
            Dim contenutoRiassunto As String = ""

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            Dim s As String = "SELECT BOL_BOLLETTE.*, ANAGRAFICA.ID AS ID_ANA , anagrafica.sesso, " _
                        & "ragione_sociale, COGNOME ,NOME, " _
                        & "PARTITA_IVA, COD_FISCALE , RAPPORTI_UTENZA.COD_CONTRATTO" _
                        & " FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE, SISCOM_MI.ANAGRAFICA, SISCOM_MI.RAPPORTI_UTENZA " _
                        & " WHERE  BOL_BOLLETTE.ID IN (#SOST#) AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO " _
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO "

            par.cmd.CommandText = QueryIN(vIdBolletta, s, "#SOST#")


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then

                If Len(par.IfNull(dt.Rows(0).Item("PARTITA_IVA"), 0)) = 11 Or par.ControllaCF(par.IfNull(dt.Rows(0).Item("COD_FISCALE"), 0)) = True Then
                    'Contenuto = contenutoOriginale
                    Tot_Bolletta = 0

                    TipoIngiunzione = "ANTICIPO RATEIZZAZIONE"
                    CodiceContratto = par.IfNull(dt.Rows(0).Item("COD_CONTRATTO"), "")
                    VOCE = "676"

                    Importo = Me.txtVersAnticipo.Text.Replace(".", "")
                    IdAnagrafica = par.IfNull(dt.Rows(0).Item("id_ana"), "")

                    par.cmd.CommandText = "select complessi_immobiliari.id as idcomplesso,edifici.id as idedificio,siscom_mi.rapporti_utenza.*,unita_contrattuale.id_unita from SISCOM_MI.EDIFICI,siscom_mi.complessi_immobiliari,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza where complessi_immobiliari.id=edifici.id_complesso and unita_immobiliari.id=unita_contrattualE.id_unita and edifici.id=unita_immobiliari.id_edificio and unita_contrattuale.id_contratto=rapporti_utenza.id and unita_contrattuale.id_unita_principale is null and cod_contratto='" & CodiceContratto & "'"
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123.Read Then
                        IdContratto = par.IfNull(myReader123("id"), "-1")
                        idUnita = par.IfNull(myReader123("id_unita"), "-1")
                        presso_cor = par.IfNull(myReader123("presso_cor"), "")
                        luogo_cor = par.IfNull(myReader123("luogo_cor"), "")
                        civico_cor = par.IfNull(myReader123("civico_cor"), "")
                        cap_cor = par.IfNull(myReader123("cap_cor"), "")
                        indirizzo_cor = par.IfNull(myReader123("VIA_cor"), "")
                        tipo_cor = par.IfNull(myReader123("tipo_cor"), "")
                        sigla_cor = par.IfNull(myReader123("sigla_cor"), "")
                        idEdificio = par.IfNull(myReader123("idedificio"), "0")
                        idComplesso = par.IfNull(myReader123("idcomplesso"), "0")
                    End If
                    myReader123.Close()

                    Dim Titolo As String = ""
                    Dim Nome As String = ""
                    Dim Cognome As String = ""
                    Dim CF As String = ""

                    Dim ID_BOLLETTA As Long = 0

                    If par.IfNull(dt.Rows(0).Item("ragione_sociale"), "") <> "" Then
                        Titolo = ""
                        Cognome = par.IfNull(dt.Rows(0).Item("ragione_sociale"), "")
                        Nome = ""
                        CF = par.IfNull(dt.Rows(0).Item("partita_iva"), "")
                    Else
                        If par.IfNull(dt.Rows(0).Item("sesso"), "") = "M" Then
                            Titolo = "Sign."
                        Else
                            Titolo = "Sign.ra"
                        End If
                        Cognome = par.IfNull(dt.Rows(0).Item("cognome"), "")
                        Nome = par.IfNull(dt.Rows(0).Item("nome"), "")
                        CF = par.IfNull(dt.Rows(0).Item("cod_fiscale"), "")
                    End If


                    ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, Now))
                    periodo = Format(Now, "yyyyMMdd") & " - " & Format(Now, "yyyyMMdd")

                    Dim Nome1 As String = ""
                    Dim Nome2 As String = ""

                    If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
                        Nome1 = Cognome & " " & Nome
                        Nome2 = presso_cor
                    Else
                        Nome1 = presso_cor
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE " _
                                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                        & "Values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999 , '" & Format(Now, "yyyyMMdd") _
                                        & "', '" & par.AggiustaData(Me.txtscadBollettino.Text) & "', NULL,NULL,NULL,'BOLLETTA ANTICIPO RATEIZZAZIONE'," _
                                        & "" & IdContratto _
                                        & " ," & par.RicavaEsercizioCorrente & ", " _
                                        & par.IfNull(dt.Rows(0).Item("ID_UNITA"), 0) _
                                        & ", '0', ''," & par.IfNull(dt.Rows(0).Item("ID_ANA"), 0) _
                                        & ", '" & par.PulisciStrSql(Nome1) & "', " _
                                        & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) _
                                        & "', '" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") _
                                        & "', '" & par.PulisciStrSql(Nome2) & "', '" & Format(Now, "yyyyMMdd") _
                                        & "', '" & Format(Now, "yyyyMMdd") & "', " _
                                        & "'0', " & idComplesso & ", '', NULL, '', " _
                                        & Year(Now) & ", '', " & idEdificio & ", NULL, NULL,'MOD',5)"

                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        ID_BOLLETTA = myReaderB(0)
                    Else
                        ID_BOLLETTA = -1
                    End If
                    myReaderB.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",676" _
                    & "," & par.VirgoleInPunti(Importo) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + Importo


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                            & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()

                    Tot_Bolletta = Tot_Bolletta + SPESEmav

                    If Tot_Bolletta >= APPLICABOLLO Then

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                    & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                        Tot_Bolletta = Tot_Bolletta + BOLLO
                    End If


                    par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=32"
                    Dim letty As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If letty.Read Then
                        causalepagamento = par.IfNull(letty("valore"), "")
                    End If
                    letty.Close()



                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_RATEIZZAZIONI_DETT.NEXTVAL FROM DUAL "
                    myReaderB = par.cmd.ExecuteReader
                    If myReaderB.Read Then
                        idRataAnticipo = par.IfNull(myReaderB(0), 0)
                    End If
                    myReaderB.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_RATEIZZAZIONI_DETT (ID,ID_RATEIZZAZIONE,NUM_RATA,DATA_EMISSIONE,DATA_SCADENZA,IMPORTO_RATA,QUOTA_CAPITALI,QUOTA_INTERESSI,RESIDUO) VALUES " _
                                        & "(" & idRataAnticipo & ", " & idRateizz.Value & ",0," & Format(Now, "yyyyMMdd") & "," & par.AggiustaData(Me.txtscadBollettino.Text) _
                                        & "," & par.VirgoleInPunti(Me.txtVersAnticipo.Text.Replace(".", "")) & "," & par.VirgoleInPunti(Me.txtVersAnticipo.Text.Replace(".", "")) & ",null" _
                                        & ",null )"

                    par.cmd.ExecuteNonQuery()
                    '*****************27/10/2011 ANCHE PER LA RATA DI ANTICIPO SCRIVO L'ID_BOLLETTA
                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_RATEIZZAZIONI_DETT SET ID_BOLLETTA = " & ID_BOLLETTA & " WHERE ID = " & idRataAnticipo
                    par.cmd.ExecuteNonQuery()


                    If Session.Item("AmbienteDiTest") = "1" Then
                        causalepagamento = "COMMITEST01"
                        ' pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    Else
                        'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    End If

                    RichiestaEmissioneMAV.codiceEnte = "commi"
                    RichiestaEmissioneMAV.tipoPagamento = causalepagamento
                    RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
                    RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

                    RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA, par.IfNull(dt.Rows(0).Item("COD_CONTRATTO"), ""))

                    RichiestaEmissioneMAV.scadenzaPagamento = Mid(par.AggiustaData(Me.txtscadBollettino.Text), 1, 4) & "-" & Mid(par.AggiustaData(Me.txtscadBollettino.Text), 5, 2) & "-" & Mid(par.AggiustaData(Me.txtscadBollettino.Text), 7, 2)

                    RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                    RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                    RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                    RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                    If Nome <> "" Then
                        RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                    End If


                    If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
                        RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                    Else
                        RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                        RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                    End If

                    RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                    RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                    RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                    RichiestaEmissioneMAV.nazioneDebitore = "IT"

                    '/*/*/*/*/*tls v1
                    Dim v As String = ""
                    par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                    v = par.cmd.ExecuteScalar
                    System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                    '/*/*/*/*/*tls v1


                    '12/01/2015 PUCCIA Nuova connessione  tls ssl


                    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler

                    Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                    If Esito.codiceRisultato = "0" Then


                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                        binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length - 1)
                        outFile.Close()


                        num_bollettino = Esito.numeroMAV
                        par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & num_bollettino & "' where  id=" & ID_BOLLETTA
                        par.cmd.ExecuteNonQuery()

                        'Response.Write("<script>window.open(" & outputFileName.Replace("\", "/") & ");<script>")
                        outputFileName = ("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"

                        AggiungiDt("MAV ANTICIPO", outputFileName.Replace("\", "/"))
                        'WriteEvent(IdContratto, "F173", "GENERATO BOLLETTA MAV PER ANTICIPO RATEIZZAZIONE BOLLETTA " & vIdBolletta)
                        'commento vidBolletta perchè potrebbe superare lo spazio massimo ammesso nel campo motivazione di eventi contratti 
                        WriteEvent(IdContratto, "F173", "GENERATO BOLLETTA MAV PER ANTICIPO RATEIZZAZIONE ")

                        GenerateMav = True

                    Else
                        lblErrore.Visible = True

                        Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                        If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml") = True Then
                            FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                        End If
                        lblErrore.Text = "Ci sono stati degli errori durante la fase di creazione.Il MAV on line non è stato creato!!( " & FileDaCreare & ".xml)"
                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml"
                        binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length)
                        outFile.Close()
                    End If

                Else

                    lblErrore.Visible = True
                    lblErrore.Text = "Il bollettino di anticipo di " & par.IfNull(dt.Rows(0).Item("COGNOME"), "") & " " & par.IfNull(dt.Rows(0).Item("COGNOME"), "") & " non è stato stampato perchè il codice fiscale o la partita iva non hanno un formato corretto o non sono presenti!"

                End If



            End If



        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "GenerateMav - " & ex.Message

        End Try

    End Function
    Private Function ControlMavRate() As Boolean
        ControlMavRate = False
        Try
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID_RATEIZZAZIONE = " & idRateizz.Value & " ORDER BY DATA_EMISSIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠

            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim fine As Integer = (Now.Date.Month + 1) Mod 3
            If fine > 0 Then
                fine = fine - 1
            End If

            For i As Integer = 0 To fine
                If ValidMonth4Mav(par.IfNull(Format(Now, "yyyyMMdd"), "00")) = True Then
                    If EmettiMavRata(par.IfNull(dt.Rows(i).Item("ID"), 0)) = False Then
                        ControlMavRate = False
                        Exit Function
                    End If
                End If
                'i = i + 1
            Next
            '2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠2♠
            ControlMavRate = True
            Session.Add("titolo", "Sono stati emessi " & ContaRate & "  M.A.V. I restanti " & dt.Rows.Count - ContaRate & "  saranno spediti automaticamente dal sistema.")
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "ControlMavRate - " & ex.Message

        End Try
    End Function
    Private Function ValidMonth4Mav(ByVal dataEmissione As String) As Boolean
        Dim lstValidMonth As New ArrayList

        lstValidMonth.Add("01")
        lstValidMonth.Add("02")

        lstValidMonth.Add("04")
        lstValidMonth.Add("05")

        lstValidMonth.Add("07")
        lstValidMonth.Add("08")

        lstValidMonth.Add("10")
        lstValidMonth.Add("11")

        ValidMonth4Mav = False
        Try
            If lstValidMonth.Contains(dataEmissione.Substring(4, 2)) = True Then

                ValidMonth4Mav = True
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "ValidMonth4Mav - " & ex.Message

        End Try
        Return ValidMonth4Mav
    End Function
    Private Function PrenotaRateizzazione() As Boolean
        PrenotaRateizzazione = False
        Try
            Dim codContratto As String = ""

            'If Not IsPostBack Then
            Dim SottoTitolo As String = ""
            Dim giorniScad As Integer = 0
            Dim tasso As Decimal = 0

            par.cmd.CommandText = "SELECT TASSO FROM SISCOM_MI.TAB_INTERESSI_LEGALI WHERE ANNO = (SELECT MAX(ANNO) FROM SISCOM_MI.TAB_INTERESSI_LEGALI) "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If lettore.Read Then
                tasso = par.IfNull(lettore("TASSO"), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE DESCRIZIONE = 'GIORNO DEL MESE SCADENZA RATEIZZAZIONE'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                giorniScad = lettore(0)
            End If
            lettore.Close()


            'Dim msgAnomalia As String = ""
            'par.VerificaImportoMinimo(Me.txtNRate.Text, 0, Me.CapitaleRateiz.Value.Replace(".", ""), Me.txtData.Text, tasso, giorniScad, msgAnomalia)
            'If Not String.IsNullOrEmpty(msgAnomalia) Then
            '    Response.Write("<script>alert('Operazione interrotta!\n" & msgAnomalia & "');</script>")
            '    Exit Function
            'End If


            par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO, " _
                                & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA ,"


            If Not String.IsNullOrEmpty(par.IfEmpty(Me.txtCapitale.Text.Replace(".", ""), 0)) And Not String.IsNullOrEmpty(par.IfEmpty(Me.txtData.Text, "")) Then

                If Not String.IsNullOrEmpty(Me.txtNRate.Text) Then
                    dt = par.Ammortamento(Me.CapitaleRateiz.Value.Replace(".", ""), Me.txtNRate.Text, tasso, 12, Me.txtData.Text, giorniScad)

                End If
                Me.DataGridRate.Visible = True
                DataGridRate.DataSource = dt
                DataGridRate.DataBind()

                Dim s As String = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno " _
                                    & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO " _
                                    & "AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE'" _
                                    & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  AND scale_edifici.ID = unita_immobiliari.id_scala AND BOL_BOLLETTE.ID in (#SOST#)"
                par.cmd.CommandText = QueryIN(vIdBolletta, s, "#SOST#")


                lettore = par.cmd.ExecuteReader



                If lettore.Read Then
                    SottoTitolo = lettore("INTECONTRATTO") & " , " & lettore("DESCRIZIONE") & " civ." & lettore("CIVICO") & " sc." & lettore("SCALA") & " int." & lettore("INTERNO")
                End If
                lettore.Close()

                Dim comando As String = "SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.BOL_BOLLETTE WHERE RAPPORTI_UTENZA.ID = BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID in (#SOST#)"
                par.cmd.CommandText = QueryIN(vIdBolletta, comando, "#SOST#")


                lettore = par.cmd.ExecuteReader

                If lettore.Read Then
                    codContratto = par.IfNull(lettore(0), 0)
                End If
                lettore.Close()



                If idRateizz.Value > 0 Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID_RATEIZZAZIONE = " & idRateizz.Value
                    par.cmd.ExecuteNonQuery()
                End If

                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_RATEIZZAZIONI_DETT (ID,ID_RATEIZZAZIONE,NUM_RATA,DATA_EMISSIONE,DATA_SCADENZA,IMPORTO_RATA,QUOTA_CAPITALI,QUOTA_INTERESSI,RESIDUO,FL_ANNULLATA) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_RATEIZZAZIONI_DETT.NEXTVAL, " & idRateizz.Value & "," & row.Item("NUMRATA") & "," & par.AggiustaData(row.Item("EMISSIONE")) & "," & par.AggiustaData(row.Item("SCADENZA")) _
                                        & "," & par.VirgoleInPunti(row.Item("IMPORTORATA")) & "," & par.VirgoleInPunti(row.Item("QUOTACAPITALI")) & "," & par.VirgoleInPunti(row.Item("QUOTAINTERESSI")) _
                                        & "," & par.VirgoleInPunti(row.Item("IMPORTORESIDUO")) & ",0 )"

                    par.cmd.ExecuteNonQuery()
                Next
                '**05/08/2011 NON ANNULLO PIU' LE BOLLETTE MA AGGIORNO ID_RATEIZZAZIONE
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET ID_RATEIZZAZIONE = " & idRateizz.Value & " WHERE ID IN (" & vIdBolletta & ")"
                'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA = '1', NOTE = NOTE ||' RATEIZZAZ. IN CORSO' WHERE ID IN (" & vIdBolletta & ")"
                par.cmd.ExecuteNonQuery()


            End If


            PrintPdf(SottoTitolo, tasso, dt.Rows.Count, dt.Rows(0).Item("IMPORTORATA"), codContratto)

            Me.DataGridRate.Visible = False
            PrenotaRateizzazione = True
            WriteEvent(idContratto.Value, "F172", "INSERIMENTO DI UN NUOVO PIANO DI RATEIZZAZIONE PER LE BOLLETTE")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "PrenotaRateizzazione - " & ex.Message

        End Try
    End Function
    Private Sub PrintPdf(ByVal Subtitle As String, ByVal interesse As Decimal, ByVal numrate As Integer, ByVal impSingola As Decimal, ByVal codContratto As String)
        Try

            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)

            Me.DataGridRate.RenderControl(sourcecode)
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
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = "PIANO DI RATEIZZAZIONE"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = Subtitle
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            pdfConverter1.PdfHeaderOptions.TextArea = New TextArea(10, 50, "CAPITALE €. " & Me.CapitaleRateiz.Value & " - INTERESSE " & interesse & "% - N° Rate " & numrate & " - Importo Singola Rata €. " & impSingola, New System.Drawing.Font(New System.Drawing.FontFamily("Arial"), 8, System.Drawing.GraphicsUnit.Point))
            pdfConverter1.PdfHeaderOptions.TextArea.TextAlign = HorizontalTextAlign.Left

            Dim nomefile As String = "Rateizzaz" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            '


            '***********************************************************************************************

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            Dim NomeZipfile As String = Format(Now, "yyyyMMddHHmmss") & "_007_" & codContratto
            zipfic = Server.MapPath("..\FileTemp\" & NomeZipfile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile)
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            AggiungiDt("PIANO DI RATEIZZAZIONE", "../FileTemp/" & nomefile)

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "PrintPdf - " & ex.Message

        End Try

    End Sub

    Private Function CreaCausale(ByVal Tipo As String, ByVal idb As Long, Optional ByVal codContratto As String = "") As String
        CreaCausale = ""
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            sCausale = ""
            If Not String.IsNullOrEmpty(codContratto) Then
                sCausale = ("COD.RAPPORTO: " & codContratto).ToString.PadRight(55)
            End If
            par.cmd.CommandText = "select t_voci_bolletta.descrizione,bol_bollette_voci.importo from siscom_mi.bol_bollette,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where bol_bollette_voci.id_bolletta=bol_bollette.id and t_voci_bolletta.id=bol_bollette_voci.id_voce and bol_bollette.id=" & idb & " order by t_voci_bolletta.descrizione asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                sImporto = Format(par.IfNull(myReader("importo"), "0"), "##,##0.00")

                If sImporto < 1 And sImporto > 0 Then
                    sImporto = "0" & sImporto
                End If

                If sImporto > -1 And sImporto < 0 Then
                    sImporto = "-0" & Replace(sImporto, "-", "")
                End If

                iDifferenza = 55 - Len(sImporto)
                sDescrizione = par.IfNull(myReader("descrizione"), "")
                sCausale = sCausale & Mid(sDescrizione.PadRight(iDifferenza), 1, iDifferenza) & sImporto
            Loop
            CreaCausale = sCausale
            myReader.Close()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CreaCausale - " & ex.Message

        End Try

    End Function


    Private Function SalvaDati() As Boolean
        SalvaDati = False
        Try
            Dim Numero As Integer = 0
            Dim tasso As Decimal = 0

            par.cmd.CommandText = "UPDATE siscom_mi.BOL_RATEIZZAZIONI SET " _
                                & " IMP_ANTICIPO = " & par.VirgoleInPunti(Me.txtVersAnticipo.Text.Replace(".", "")) _
                                & ",IMP_RESIDUO = " & par.VirgoleInPunti(Me.CapitaleRateiz.Value.Replace(".", "")) _
                                & ",SCADENZA_ANTICIPO = '" & par.AggiustaData(Me.txtscadBollettino.Text) & "'" _
                                & ",FL_SOSPESO = 0 " _
                                & ",TOT_RATEIZZATO = " & par.VirgoleInPunti(txtImpRateizzabile.Text.Replace(".", "")) _
                                & ",ID_STATO = " & Me.cmbSTRateizz.SelectedValue _
                                & ",DATA_EMISSIONE = '" & par.AggiustaData(Me.txtData.Text) & "'" _
                                & ",TIPO_RATEIZZAZIONE=0 " _
                                & ",NUM_RATE = " & par.IfEmpty(Me.txtNRate.Text, "null") _
                                & ",IMPORTO_RATA = " & par.IfEmpty(par.VirgoleInPunti(Me.txtImpRate.Text.Replace(".", "")), "NULL") _
                                & ",DATA_STIPULA = '" & Format(Now, "yyyyMMdd") _
                                & "',NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "'" _
                                & ",DATA_SALDO_MOROSITA='" & par.AggiustaData(Me.dataSaldo.Value) & "'" _
                                & " WHERE ID = " & idRateizz.Value
            par.cmd.ExecuteNonQuery()

            If Me.cmbSTRateizz.SelectedValue = 1 Then

                par.cmd.CommandText = "select id from siscom_mi.bol_Bollette where id_rateizzazione=" & idRateizz.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.HasRows = False Then
                    par.cmd.CommandText = "UPDATE siscom_mi.BOL_BOLLETTE SET ID_RATEIZZAZIONE = " & idRateizz.Value & " WHERE ID IN (" & vIdBolletta & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE siscom_mi.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO = (nvl(importo_riclassificato,0) + (NVL(IMPORTO,0) - NVL(IMP_PAGATO,0))) WHERE ID_BOLLETTA IN (" & vIdBolletta & ") AND ID_VOCE NOT IN (SELECT ID FROM siscom_mi.T_VOCI_BOLLETTA WHERE GRUPPO = 5)"
                    par.cmd.ExecuteNonQuery()

                    If Not IsNothing(Session.Item("lIdConnessDOMANDA")) Then
                        Dim par1 As New CM.Global
                        par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessDOMANDA")), Oracle.DataAccess.Client.OracleConnection)
                        par1.cmd = par1.OracleConn.CreateCommand()
                        par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessDOMANDA")), Oracle.DataAccess.Client.OracleTransaction)

                        par1.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_AUTORIZZAZIONE='1',DATA_AUTORIZZAZIONE='" & Format(Now, "yyyyMMdd") & "' WHERE ID in (select id from domande_bando_vsa where id_dichiarazione=" & idDich.Value & ")"
                        par1.cmd.ExecuteNonQuery()

                        par1.myTrans.Commit()
                        par1.myTrans = par1.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessDOMANDA"), par1.myTrans)
                        par1.Dispose()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Else
                        par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_AUTORIZZAZIONE='1',DATA_AUTORIZZAZIONE='" & Format(Now, "yyyyMMdd") & "' WHERE ID in (select id from domande_bando_vsa where id_dichiarazione=" & idDich.Value & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                End If
                lettore.Close()
            End If
            SalvaDati = True
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "SalvaDati - " & ex.Message

        End Try
    End Function
    Function ZeroUnoChecked(ByVal B As Boolean) As Integer
        ZeroUnoChecked = 0
        If B = True Then
            ZeroUnoChecked = 1
        End If
        Return ZeroUnoChecked
    End Function
    Private Sub UpdateDati()
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            If idRateizz.Value > 0 Then
                '**********apertura transazione

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_RATEIZZAZIONI SET " _
                                    & "IMP_ANTICIPO = " & par.VirgoleInPunti(Me.txtVersAnticipo.Text.Replace(".", "")) & "," _
                                    & "IMP_RESIDUO = " & par.VirgoleInPunti(Me.txtCapitale.Text.Replace(".", "")) & "," _
                                    & "SCADENZA_ANTICIPO = '" & par.AggiustaData(Me.txtscadBollettino.Text) & "'," _
                                    & "FL_SOSPESO = 0," _
                                    & "DATA_EMISSIONE= '" & par.AggiustaData(Me.txtData.Text) & "'," _
                                    & "TIPO_RATEIZZAZIONE = 0," _
                                    & "NUM_RATE = " & par.IfEmpty(Me.txtNRate.Text, "null") & " " _
                                    & "WHERE ID = " & idRateizz.Value
                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            End If
            par.myTrans.Commit()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "UpdateDati - " & ex.Message
            par.myTrans.Rollback()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub


    Protected Sub btnSa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSa.Click
        Try


            If ConfermaSalva.Value = 1 Then
                Dim importoRata As Decimal = 0
                If Not String.IsNullOrEmpty(Me.txtNRate.Text) Then

                    AggiungiDt("", "")

                    Dim esito As Boolean = False

                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    par.myTrans = par.OracleConn.BeginTransaction()

                    If ControlliEmissRateizz(fascia.Value, importoRata) = True Then

                        If SalvaDati() = True Then
                            If Me.cmbSTRateizz.SelectedValue = 1 Then
                                If PrenotaRateizzazione() = True Then
                                    If ControlMavRate() = True Then
                                        If par.IfEmpty(Me.txtVersAnticipo.Text, 0) > 0 Then
                                            If GenerateMav() = True Then

                                            Else
                                                par.myTrans.Rollback()
                                                '*********************CHIUSURA CONNESSIONE**********************
                                                par.OracleConn.Close()
                                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                                Response.Write("<script>alert('ERRORE nella generazione del mav di anticipo! Operazione Annullata!!');</script>")
                                                Response.Write("<script>alert('" & lblErrore.Text.Replace("'", "\'") & "');</script>")

                                                Session.Remove("dtResult")
                                                Session.Remove("titolo")
                                                'Session.Add("ERRORE", "ERRORE Rateizzazione:" & Page.Title & "<br/>" & lblErrore.Text)
                                                Response.Write("<script>window.close();</script>")
                                                Exit Sub
                                            End If
                                        End If
                                    Else
                                        par.myTrans.Rollback()
                                        '*********************CHIUSURA CONNESSIONE**********************
                                        par.OracleConn.Close()
                                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                        Response.Write("<script>alert('ERRORE nella generazione del mav della rata! Operazione Annullata!!');</script>")
                                        Session.Remove("dtResult")
                                        Session.Remove("titolo")

                                        Session.Add("ERRORE", "ERRORE Rateizzazione:" & Page.Title)
                                        Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")

                                        Exit Sub
                                    End If
                                Else
                                    par.myTrans.Rollback()
                                    '*********************CHIUSURA CONNESSIONE**********************
                                    par.OracleConn.Close()
                                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                    Response.Write("<script>alert('Operazione Annullata!');</script>")
                                    Session.Remove("dtResult")
                                    Session.Remove("titolo")

                                    Session.Add("ERRORE", "ERRORE Rateizzazione:" & Page.Title)
                                    Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")

                                    Exit Sub
                                End If


                                '********************allinea la somma dei capitali riclassificati, a quelli delle bollette di rateizzazione
                                par.cmd.CommandText = "select sum(nvl(quota_capitali,0)) from siscom_mi.bol_rateizzazioni_dett where id_rateizzazione = " & idRateizz.Value
                                Dim SumCapitaliRat As Decimal = par.cmd.ExecuteScalar

                                par.cmd.CommandText = "select nvl(nvl(imp_anticipo,0) + nvl(imp_residuo,0),0) from siscom_mi.bol_rateizzazioni where id = " & idRateizz.Value
                                Dim SumRiclass As Decimal = par.cmd.ExecuteScalar

                                If SumCapitaliRat <> SumRiclass Then
                                    Dim centUpd As Decimal = SumRiclass - SumCapitaliRat

                                    par.cmd.CommandText = "update siscom_mi.bol_rateizzazioni_dett set quota_capitali = quota_capitali + " & par.VirgoleInPunti(centUpd) & " where id_rateizzazione = " & idRateizz.Value & " and num_rata = (select max(r2.num_rata) from siscom_mi.bol_rateizzazioni_dett r2 where r2.id_rateizzazione = " & idRateizz.Value & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If

                                If idDich.Value <> 0 Then
                                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & " VALUES ((select id from domande_bando_vsa where id_dichiarazione=" & idDich.Value & ")," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8'," _
                                    & "'F214','','')"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                            par.myTrans.Commit()
                            '*********************CHIUSURA CONNESSIONE**********************
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Response.Write("<script>alert('Operazioni completate correttamente!');</script>")
                            StatoRateizzo()
                            'Response.Redirect("RisultatiRate.aspx", False)
                        Else
                            par.myTrans.Rollback()
                            '*********************CHIUSURA CONNESSIONE**********************
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Response.Write("<script>alert('ERRORE nel salvataggio! Operazione Annullata!!');</script>")
                            Session.Remove("dtResult")
                            Session.Remove("titolo")

                            Session.Add("ERRORE", "ERRORE Rateizzazione:" & Page.Title)
                            Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")

                            Exit Sub
                        End If

                    Else
                        par.myTrans.Rollback()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        Exit Sub


                    End If
                Else
                    If String.IsNullOrEmpty(Me.txtNRate.Text) Then
                        Response.Write("<script>alert('Definire il numero di rate!');</script>")

                    End If
                End If
            Else
                Response.Write("<script>alert('Operazione Annullata! Nessun dato salvato!');</script>")
            End If
        Catch ex As Exception

            par.myTrans.Rollback()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('ERRORE durante il collegamento! Operazione annullata!');</script>")
            Session.Remove("dtResult")
            Session.Remove("titolo")

            Session.Add("ERRORE", "ERRORE Rateizzazione:" & Page.Title)
            Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")

            Exit Sub

        End Try
    End Sub

    'Protected Sub btnPrintAccDebito_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPrintAccDebito.Click
    '    Me.btnSa.Visible = True
    '    'Me.chkPagato.Visible = True
    '    Me.BtnSimula.Visible = True
    'End Sub
    Public Sub WriteEvent(ByVal ID_CONTRATTO As String, ByVal CodEvento As String, Optional ByVal Motivazione As String = "")

        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES ( " & ID_CONTRATTO & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "')"
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

    Private Function QueryIN(ByVal InCondition As String, ByVal q As String, ByVal sostituzione As String) As String
        QueryIN = ""

        Dim s As String = ""
        Dim query As String = ""
        Dim listaquery As New System.Collections.Generic.List(Of String)
        Dim i As Integer = 0
        Dim fine As Boolean = False
        Dim listBollette As String = ""
        While fine = False
            listBollette = ""
            s = ""
            Dim primo As Boolean = True
            For j As Integer = 0 To 999
                If 999 * i + j < InCondition.Split(",").Length Then
                    If primo = True Then
                        listBollette = listBollette & InCondition.Split(",")(999 * i + j)
                        primo = False
                    Else
                        listBollette = listBollette & "," & InCondition.Split(",")(999 * i + j)
                    End If
                Else
                    fine = True
                    Exit For
                End If
            Next
            s = q.Replace(sostituzione, listBollette)
            listaquery.Add(s)

            i += 1
        End While
        For Each Items As String In listaquery
            query &= Items & " UNION "
        Next
        query = Left(query, Len(query) - 6)


        QueryIN = query

    End Function

    Protected Sub cmbPercentuale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPercentuale.SelectedIndexChanged
        txtVersAnticipo.Text = Format(Math.Round(CDec(totDebito.Text) * (CInt(cmbPercentuale.SelectedItem.Text) / 100), 2), "##,##0.00")
    End Sub

    Protected Sub btnPrint_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles btnPrint.MenuItemClick
        If Me.btnSa.Visible = True Then
            Me.btnSa.Visible = True
        End If
        Me.BtnSimula.Visible = True
        If txtModificato.Value = "0" Then
            Select Case btnPrint.SelectedValue
                Case 1
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2A", "PrintDoc();", True)
                Case 2
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2A", "EsitoPosRAT();", True)
            End Select
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE! Prima di procedere salvare le modifiche.');", True)
        End If
    End Sub
End Class
