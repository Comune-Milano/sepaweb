Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RptMorCondomini
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim impMezzoMav As Boolean = False
    Dim impExtraMav As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaTabella()
        End If
    End Sub

    Private Sub CaricaTabella()
        Try

            Dim idCondominio As Integer = par.IfEmpty(Request.QueryString("IDCOND"), 0)
            Dim idMorosita As Integer = 0
            Dim rifDa As String = par.IfEmpty(Request.QueryString("DAL"), "")
            Dim rifA As String = par.IfEmpty(Request.QueryString("AL"), "")
            Dim isInquilino As String = par.IfEmpty(Request.QueryString("INQ"), "")

            rifDa = par.FormatoDataDB(rifDa)
            rifA = par.FormatoDataDB(rifA)

            Dim Tabelle As String = ""
            Dim Join As String = ""
            Dim Condizioni As String = ""

            Dim dt As New Data.DataTable

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT condomini.denominazione AS DENOMINAZIONE, condomini.gestione_inizio, condomini.gestione_fine, cond_amministratori.cognome || ' ' || cond_amministratori.nome as amministratore " _
                & "from siscom_mi.condomini, siscom_mi.cond_amministratori,siscom_mi.cond_amministrazione where condomini.id= " & idCondominio & " AND condomini.id = cond_amministrazione.id_condominio AND cond_amministrazione.id_amministratore = cond_amministratori.id"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lblCondominio.Text = par.IfNull(myReader1("DENOMINAZIONE"), "")
                lblPeriodoGestione.Text = "DAL " & Replace(par.FormattaData("2000" & myReader1("GESTIONE_INIZIO").ToString), "/2000", "") & " AL " & Replace(par.FormattaData("2000" & myReader1("GESTIONE_FINE").ToString), "/2000", "")
                lblAmministratore.Text = par.IfNull(myReader1("amministratore"), "")
            End If

            myReader1.Close()


            Tabelle = "siscom_mi.cond_morosita, siscom_mi.prenotazioni, siscom_mi.pagamenti, siscom_mi.pagamenti_liquidati, siscom_mi.condomini"
            Join = "condomini.id = cond_morosita.id_condominio AND cond_morosita.id_prenotazione = prenotazioni.id AND prenotazioni.id_pagamento = pagamenti.id (+) AND pagamenti.id = pagamenti_liquidati.id_pagamento (+)"
            If rifDa <> "" And rifA <> "" Then
                Condizioni = "condomini.id= " & idCondominio & " AND (cond_morosita.rif_da >= '" & rifDa & "' AND cond_morosita.rif_a <= '" & rifA & "')"
            Else
                Condizioni = "condomini.id= " & idCondominio
            End If


            par.cmd.CommandText = "SELECT TO_CHAR(TO_DATE(COND_MOROSITA.DATA_RICHIESTA_RIMBORSO,'YYYYmmdd'),'DD/MM/YYYY') AS data_richiesta_rimborso, " _
                & "cond_morosita.num_mandato_com, TO_CHAR(TO_DATE(cond_morosita.data_mandato_com,'YYYYmmdd'),'DD/MM/YYYY') AS data_mandato_com, trim(TO_CHAR(cond_morosita.importo,'9G999G990D99')) AS IMPORTO_MANDATO_COM, cond_morosita.id AS ID_MOROSITA, " _
                & "pagamenti.progr || '/' || pagamenti.anno AS NUMERO_ADP, " _
                & "TO_CHAR(TO_DATE(pagamenti.data_emissione,'YYYYmmdd'),'DD/MM/YYYY') AS data_emissione, pagamenti_liquidati.num_mandato AS NUM_MANDATO_ALER, TO_CHAR(TO_DATE(pagamenti_liquidati.data_mandato,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_MANDATO_ALER, trim(TO_CHAR(pagamenti_liquidati.importo,'9G999G990D99')) AS IMPORTO_MANDATO_ALER " _
                & "from " & Tabelle & " where " & Join & " AND " & Condizioni & ""

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim stringaHtml As String = ""

            da.Fill(dt)
            Dim row As Data.DataRow

            Session.Add("DTMOROSITA", dt)

            If dt.Rows.Count <> 0 Then
                For Each row In dt.Rows


                    stringaHtml += "<table width='100%'><tr valign='top'><td><table width='95%'>"
                    stringaHtml += "<tr class='style_intestazione'><td colspan='2'> DATA RICHIESTA RIMBORSO</td> <td> NUMERO MANDATO PAGAMENTO COM. </td> <td> DATA MANDATO PAGAMENTO COM. </td><td> IMPORTO MANDATO PAGAMENTO COM. </td><td> NUMERO A.D.P. </td><td> DATA A.D.P. </td><td> NUMERO MANDATO GESTORE </td><td> DATA MANDATO GESTORE </td><td> IMPORTO MANDATO GESTORE </td></tr>"
                    stringaHtml += "<tr class='style_risultati'><td colspan='2'>" & par.IfNull(row("DATA_RICHIESTA_RIMBORSO"), "") & "</td><td>" & par.IfNull(row("NUM_MANDATO_COM"), "NON DISPONIBILE") & "</td><td>" & par.IfNull(row("DATA_MANDATO_COM"), "") & "</td><td>" & par.IfNull(row("IMPORTO_MANDATO_COM"), "NON DISPONIBILE") & "</td>"
                    stringaHtml += "<td>" & par.IfNull(row("NUMERO_ADP"), "NON DISPONIBILE") & "</td><td>" & par.IfNull(row("DATA_EMISSIONE"), "") & "</td><td>" & par.IfNull(row("NUM_MANDATO_ALER"), "NON DISPONIBILE") & "</td><td>" & par.IfNull(row("DATA_MANDATO_ALER"), "") & "</td><td>" & par.IfNull(row("IMPORTO_MANDATO_ALER"), "NON DISPONIBILE") & "</td></tr>"
                    stringaHtml += "<tr style='height:6px'><td></td></tr>"

                    If isInquilino = "True" Then
                        stringaHtml += CreaInfoInquilini(idCondominio, row("ID_MOROSITA"), rifDa, rifA)
                    End If


                    stringaHtml += "</table></td>"
                    stringaHtml += "<td><table width='90%' border='0px solid black'><tr class='style_intestazione'><td>DATE DOCUMENTAZIONI</td></tr>"
                    stringaHtml += CreaListBoxDataDoc(row("ID_MOROSITA"))
                    stringaHtml += "</td></tr></table><div>&nbsp;</div><div>&nbsp;</div>"
                    rigaMorosita.Text += stringaHtml

                    stringaHtml = ""

                Next
            Else
                Response.Write("<script>alert('Il condominio selezionato non possiede morosità');</script>")
            End If


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub



    Private Function CreaInfoInquilini(ByVal idCondominio As Integer, ByVal idMorosita As Integer, ByVal rifDa As String, ByVal rifA As String) As String

        Dim Tabelle As String = "siscom_mi.cond_morosita_inquilini, siscom_mi.anagrafica, sepa.comuni_nazioni, siscom_mi.cond_morosita"
        Dim Join As String = "cond_morosita_inquilini.id_intestatario = anagrafica.id AND anagrafica.cod_comune_nascita = comuni_nazioni.cod(+) AND cond_morosita_inquilini.id_morosita = cond_morosita.id"
        Dim Condizioni As String = ""
        If rifDa <> "" And rifA <> "" Then
            Condizioni = "cond_morosita.id_condominio= " & idCondominio & " AND cond_morosita_inquilini.id_morosita= " & idMorosita & " AND (cond_morosita.rif_da >= '" & rifDa & "' AND cond_morosita.rif_a <= '" & rifA & "')"
        Else
            Condizioni = "cond_morosita.id_condominio= " & idCondominio & " AND cond_morosita_inquilini.id_morosita= " & idMorosita
        End If

        Dim SelectMav As String = ""

        Dim htmlIntestMav As String = ""
        Dim htmlCampiMav As String = ""

        Dim isContSol As String = par.IfEmpty(Request.QueryString("CSOL"), "")
        Dim isMav As String = par.IfEmpty(Request.QueryString("MAV"), "")
        Dim isFonSoc As String = par.IfEmpty(Request.QueryString("FSOL"), "")

        Dim dt As New Data.DataTable

        If isMav = "True" Then

            Tabelle += ", siscom_mi.cond_morosita_lettere, siscom_mi.bol_bollette"
            Join += " AND cond_morosita_inquilini.id_intestatario = cond_morosita_lettere.id_anagrafica (+) AND cond_morosita_lettere.bollettino = bol_bollette.rif_bollettino (+) "
            SelectMav = ", trim(TO_CHAR(cond_morosita_lettere.importo,'9G999G990D99')) AS IMPORTO_MAV, TO_CHAR(TO_DATE(cond_morosita_lettere.emissione,'YYYYmmdd'),'DD/MM/YYYY') AS emissione, cond_morosita_lettere.data_notifica_comune, " _
                & "TO_CHAR(TO_DATE(bol_bollette.data_pagamento,'YYYYmmdd'),'DD/MM/YYYY') AS data_pagamento, trim(TO_CHAR(bol_bollette.importo_pagato,'9G999G990D99')) AS importo_pagato, bol_bollette.id_tipo_pagamento"

            htmlIntestMav = "<td class='style_intestazione'> IMPORTO MAV </td><td class='style_intestazione'> DATA ELAB. MAV </td><td class='style_intestazione'> DATA NOTIF. MAV </td> <td class='style_intestazione'> DATA PAGAM. MAV </td> <td class='style_intestazione'> IMPORTO PAGATO MEZZO MAV </td><td class='style_intestazione'> IMPORTO PAGATO EXTRA MAV </td>"

        End If


        par.cmd.CommandText = "SELECT DISTINCT (anagrafica.cognome || ' ' || anagrafica.nome) as inquilino, anagrafica.id, anagrafica.cod_fiscale, TO_CHAR(TO_DATE(anagrafica.data_nascita,'YYYYmmdd'),'DD/MM/YYYY') AS data_nascita, " _
            & "comuni_nazioni.nome AS COMUNE, trim(TO_CHAR(cond_morosita_inquilini.importo,'9G999G990D99')) AS importo" & SelectMav & "" _
            & " from " & Tabelle & " where " & Join & " AND " & Condizioni & " ORDER BY INQUILINO ASC"

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim stringaHtml As String = ""

        da.Fill(dt)

        Session.Add("DTINQUILINI", dt)

        If isMav = "True" Then
            Session.Add("DTMAV", dt)
        End If

        Dim row As Data.DataRow


        stringaHtml = "<tr><td width='10px'></td><td class='style_intestazione'> INQUILINO </td> <td class='style_intestazione'> CODICE FISCALE </td> <td class='style_intestazione'> DATA NASCITA </td><td class='style_intestazione'> LUOGO NASCITA </td> <td class='style_intestazione'> IMPORTO </td>" & htmlIntestMav & "</tr>"

        For Each row In dt.Rows


            If htmlIntestMav <> "" Then
                htmlCampiMav = "<td class='style_risultati'>" & par.IfNull(row("IMPORTO_MAV"), "") & "</td><td class='style_risultati'>" & par.IfNull(row("EMISSIONE"), "") & "</td><td class='style_risultati'>" & par.IfNull(row("DATA_NOTIFICA_COMUNE"), "") & "</td><td class='style_risultati'>" & par.IfNull(row("DATA_PAGAMENTO"), "") & "</td>"


                If IsDBNull(row("ID_TIPO_PAGAMENTO")) Then
                Else
                    If row("ID_TIPO_PAGAMENTO") = "1" Then
                        impMezzoMav = True
                        htmlCampiMav += "<td class='style_risultati'>" & par.IfNull(row("IMPORTO_PAGATO"), "") & "</td><td class='style_risultati'></td>"
                    ElseIf row("ID_TIPO_PAGAMENTO") = "2" Then
                        impExtraMav = True
                        htmlCampiMav += "<td class='style_risultati'></td><td class='style_risultati'>" & par.IfNull(row("IMPORTO_PAGATO"), "") & "</td>"
                    End If
                End If
            End If

            impMezzoMav = False
            impExtraMav = False

            stringaHtml += "<tr><td width='10px'></td><td class='style_risultati'>" & par.IfNull(row("INQUILINO"), "") & "</td><td class='style_risultati'>" & par.IfNull(row("COD_FISCALE"), "") & "</td><td class='style_risultati'>" & par.IfEmpty(par.FormattaData(row("DATA_NASCITA").ToString), "NON DISPONIBILE") & "</td><td class='style_risultati'>" & par.IfNull(row("COMUNE"), "NON DISPONIBILE") & "</td><td class='style_risultati'>" & par.IfNull(row("IMPORTO"), "") & "</td>" & htmlCampiMav & "</tr>"

        Next

        Return stringaHtml

    End Function

    Private Function CreaListBoxDataDoc(ByVal idMorosita As String) As String

        Dim dt As New Data.DataTable

        par.cmd.CommandText = "SELECT DISTINCT (TO_CHAR(TO_DATE(cond_morosita_inquilini_det.data,'YYYYmmdd'),'DD/MM/YYYY')) AS data,id_morosita from siscom_mi.cond_morosita_inquilini_det where id_morosita= " & idMorosita

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim stringaHtml As String = "<tr valign='top'><td><div style='width:90%; height:80px; overflow:auto'>"

        da.Fill(dt)
        Session.Add("DTDATEDOC", dt)
        Dim row As Data.DataRow
        Dim i As Integer = 0

        For Each row In dt.Rows

            stringaHtml += "<div style='width:100%;' class='style_risultati'>" & par.IfNull(row("DATA"), "") & "</div>"
            i += 1

        Next

        stringaHtml += "</div></td></tr></table>"

        Return stringaHtml

    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim j As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim row2 As System.Data.DataRow
            Dim dataTabMorosita As New Data.DataTable
            Dim dataTabDateDoc As New Data.DataTable
            Dim dataTabInquilini As New Data.DataTable
            Dim dataTabMav As New Data.DataTable

            dataTabMorosita = CType(HttpContext.Current.Session.Item("DTMOROSITA"), Data.DataTable)
            dataTabDateDoc = CType(HttpContext.Current.Session.Item("DTDATEDOC"), Data.DataTable)

            If Not IsNothing(CType(HttpContext.Current.Session.Item("DTINQUILINI"), Data.DataTable)) Then
                dataTabInquilini = CType(HttpContext.Current.Session.Item("DTINQUILINI"), Data.DataTable)
            End If

            If Not IsNothing(CType(HttpContext.Current.Session.Item("DTMAV"), Data.DataTable)) Then
                dataTabMav = CType(HttpContext.Current.Session.Item("DTMAV"), Data.DataTable)
            End If


            sNomeFile = "MorExp_" & Format(Now, "yyyyMMddHHmmss")

            i = 0
            j = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "DATA RICHIESTA RIMBORSO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NUMERO MANDATO PAGAMENTO COM.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA MANDATO PAGAMENTO COM.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "IMPORTO MANDATO PAGAMENTO COM.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "NUMERO A.D.P.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DATA A.D.P.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NUMERO MANDATO GESTORE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "DATA MANDATO GESTORE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "IMPORTO MANDATO GESTORE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "DATE DOCUMENTAZIONI", 12)
                If Not IsNothing(CType(HttpContext.Current.Session.Item("DTINQUILINI"), Data.DataTable)) Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "INQUILINO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "CODICE FISCALE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "DATA DI NASCITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "LUOGO DI NASCITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "IMPORTO", 12)
                End If
                If Not IsNothing(CType(HttpContext.Current.Session.Item("DTMAV"), Data.DataTable)) Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "IMPORTO MAV", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "DATA ELABORAZIONE MAV", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "DATA NOTIFICA MAV", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "DATA PAGAMENTO MAV", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "IMPORTO PAGATO MEZZO MAV", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "IMPORTO PAGATO EXTRA MAV", 12)
                End If
                

                K = 2

                For Each row In dataTabMorosita.Rows
                    For Each row2 In dataTabInquilini.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("DATA_RICHIESTA_RIMBORSO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("NUM_MANDATO_COM"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("DATA_MANDATO_COM"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("IMPORTO_MANDATO_COM"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("NUMERO_ADP"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("DATA_EMISSIONE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("NUM_MANDATO_ALER"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("DATA_MANDATO_ALER"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dataTabMorosita.Rows(j).Item("IMPORTO_MANDATO_ALER"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dataTabDateDoc.Rows(j).Item("DATA"), "")))
                        If Not IsNothing(CType(HttpContext.Current.Session.Item("DTINQUILINI"), Data.DataTable)) Then
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dataTabInquilini.Rows(i).Item("INQUILINO"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dataTabInquilini.Rows(i).Item("COD_FISCALE"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dataTabInquilini.Rows(i).Item("DATA_NASCITA"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dataTabInquilini.Rows(i).Item("COMUNE"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dataTabInquilini.Rows(i).Item("IMPORTO"), "")))
                        End If
                        If Not IsNothing(CType(HttpContext.Current.Session.Item("DTMAV"), Data.DataTable)) Then
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dataTabMav.Rows(i).Item("IMPORTO_MAV"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dataTabMav.Rows(i).Item("EMISSIONE"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dataTabMav.Rows(i).Item("DATA_NOTIFICA_COMUNE"), "")))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dataTabMav.Rows(i).Item("DATA_PAGAMENTO"), "")))


                            If impMezzoMav = True Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dataTabMav.Rows(i).Item("IMPORTO_PAGATO"), "")))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, "")
                            ElseIf impExtraMav = True Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, "")
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dataTabMav.Rows(i).Item("IMPORTO_PAGATO"), "")))
                            End If
                        End If
                        K = K + 1
                        i = i + 1
                    Next
                    j = j + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
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

            File.Delete(strFile)
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            Session.Remove("DTMOROSITA")
            Session.Remove("DTDATEDOC")

            If Not IsNothing(CType(HttpContext.Current.Session.Item("DTINQUILINI"), Data.DataTable)) Then
                Session.Remove("DTINQUILINI")
            End If

            If Not IsNothing(CType(HttpContext.Current.Session.Item("DTMAV"), Data.DataTable)) Then
                Session.Remove("DTMAV")
            End If

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class
