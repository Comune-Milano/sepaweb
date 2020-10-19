'*** LISTA RISULTATO PAGAMENTI RRS da STAMPARE
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Math
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data


Partial Class RRS_RisultatiPagamentiRRS
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreStruttura As String
    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreServizio As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreStato As String

    Public sOrdinamento As String

    Public importo, penale, importoT, penaleT, oneriT, astaT, ivaT, ritenutaT, ritenutaNoIvaT, rimborsoT, risultato1T, risultato2T, risultato3T, risultato4T, risultatoImponibileT As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            TipoAllegato.Value = par.getIdOggettoAllegatiWs("SAL")
            HFGriglia.Value = DataGrid1.ClientID
            '   BindGrid()
            'Panel1.Visible = False

            Session.Add("NOME_FILE", "")

        End If

    End Sub



    Private Sub BindGrid()
        Dim FlagConnessione As Boolean
        Dim sOrder As String
        Dim sStringaSql As String

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreStato = Request.QueryString("ST")

            sOrder = " order by SISCOM_MI.PAGAMENTI.DATA_EMISSIONE desc,PROG_ANNO desc"

            'STATO PAGAMENTO    0=PRENOTATO 1=EMESSO 5=PAGATO

            '& " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE"","
            '& " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_PRENOTATO,'9G999G990D99')) AS ""IMPORTO_PRENOTATO"", " 

            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",(SISCOM_MI.PAGAMENTI.PROGR_APPALTO||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""SAL_ANNO"",'' as ""DATA_PRENOTAZIONE""," _
                                 & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_EMISSIONE""," _
                                 & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                 & "     then  COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
                                 & "     else  COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||LTRIM(FORNITORI.NOME))) end  AS ""BENEFICIARIO""," _
                                 & " trim(SISCOM_MI.PAGAMENTI.DESCRIZIONE) as DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                                 & " trim(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                                 & " trim(SISCOM_MI.APPALTI.NUM_REPERTORIO) as ""NUM_REPERTORIO""," _
                                 & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO""" _
                         & " from SISCOM_MI.PAGAMENTI," _
                              & " SISCOM_MI.FORNITORI," _
                              & " SISCOM_MI.TAB_STATI_PAGAMENTI," _
                              & " SISCOM_MI.APPALTI" _
                         & " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=7 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) "


            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO " _
                                                                         & " from SISCOM_MI.PRENOTAZIONI " _
                                                                         & " where ID in (select ID_PRENOTAZIONE_PAGAMENTO " _
                                                                                      & " from SISCOM_MI.MANUTENZIONI " _
                                                                                      & " where ID_PF_VOCE_IMPORTO is null " _
                                                                                      & " AND MANUTENZIONI.STATO<>5 " _
                                                                                      & "   and ID_PIANO_FINANZIARIO = " & sValoreEsercizioFinanziarioR & ")" _
                                                                         & " and TIPO_PAGAMENTO=7 " _
                                                                         & " and ID_STATO>=2 " _
                                                                         & " and ID_PAGAMENTO is not null ) "

            End If

            If par.IfEmpty(sValoreStato, -1) <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.STATO_FIRMA=" & sValoreStato
            End If

            If sValoreFornitore <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            End If


            If sValoreAppalto <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_APPALTO=" & sValoreAppalto
            End If


            If sValoreServizio <> "-1" Then
                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                              & " where ID_VOCE_PF=" & sValoreServizio _
                                                                              & "   and ID_STRUTTURA=" & sValoreStruttura & ")"
                Else
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI where ID_VOCE_PF=" & sValoreServizio & ")"
                End If
            Else
                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                              & " where TIPO_PAGAMENTO=7 and ID_STRUTTURA=" & sValoreStruttura & ")"
                End If
            End If


            sStringaSql = sStringaSql & sOrder


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            'Label1.Text = " " & ds.Tables(0).Rows.Count

            da.Dispose()
            ds.Dispose()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    'Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
    '    Session.Remove("NOME_FILE")

    '    Page.Dispose()

    '    Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")
    'End Sub


    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If

    'End Sub


    'Protected Sub btnStampaPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampaPagamento.Click


    'End Sub


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Sub CalcolaImporti(ByVal importo As Decimal, ByVal IVA_CONSUMO As Decimal, ByVal rimborso As Decimal, ByVal penale As Decimal, ByVal Id_Voce As Integer, ByVal Id_Appalto As Integer)
        Dim sStr1 As String
        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenuta, ritenutaIVATA, risultato5 As Decimal
        Dim FlagConnessione As Boolean


        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            sStr1 = "select APPALTI_VOCI_PF.*,APPALTI.FL_RIT_LEGGE " _
                & "  from   SISCOM_MI.APPALTI_VOCI_PF,SISCOM_MI.APPALTI " _
                & "  where ID_PF_VOCE=" & Id_Voce _
                & "  and   ID_APPALTO=" & Id_Appalto _
                & "  and   APPALTI.ID=" & Id_Appalto

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader2 = par.cmd.ExecuteReader

            If myReader2.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                perc_oneri = par.IfNull(myReader2("PERC_ONERI_SIC_CON"), 0)

                perc_sconto = par.IfNull(myReader2("SCONTO_CONSUMO"), 0)
                perc_iva = IVA_CONSUMO 'par.IfNull(myReader2("IVA_CONSUMO"), 0) '************


                importoT = importoT + importo
                penaleT = penaleT + Round(penale, 2)


                'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                oneri = importo - ((importo * 100) / (100 + perc_oneri))
                oneri = Round(oneri, 2)
                oneriT = oneriT + oneri

                'LORDO senza ONERI= IMPORTO-oneri
                risultato1 = importo - oneri
                risultato1T = risultato1T + risultato1

                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                asta = (risultato1 * perc_sconto) / 100
                asta = Round(asta, 2)
                astaT = astaT + asta

                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                risultato2 = risultato1 - asta '- penale
                risultato2T = risultato2T + risultato2

                'AGGIUNTO
                'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato3 = risultato2 + oneri

                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                    ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenutaIVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                    ritenutaIVATA = Round(ritenutaIVATA, 2)
                Else
                    ritenuta = 0
                    ritenutaIVATA = 0
                End If
                ritenutaT = ritenutaT + ritenutaIVATA
                ritenutaNoIvaT = ritenutaNoIvaT + ritenuta

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri
                risultato3T = risultato3T + risultato3

                risultatoImponibileT = risultatoImponibileT + risultato3 - ritenuta

                'IVA= (NETTO con oneri*perc_iva)/100
                iva = Math.Round((risultato5 * perc_iva) / 100, 2)
                ivaT = ivaT + iva

                'NETTO+ONERI+IVA
                risultato4 = risultato5 + iva + Round(rimborso, 2)
                risultato4T = risultato4T + risultato4

                rimborsoT = rimborsoT + Round(rimborso, 2)


            End If
            myReader2.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    'Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
    '    Session.Remove("NOME_FILE")

    '    Response.Write("<script>document.location.href=""RicercaPagamentiRRS.aspx""</script>")
    'End Sub

    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

    '    Dim oDataGridItem As DataGridItem
    '    Dim myExcelFile As New CM.ExcelFile
    '    Dim K As Long

    '    Dim NomeFile As String = CType(HttpContext.Current.Session.Item("NOME_FILE"), String)


    '    If Strings.Trim(NomeFile) = "" Then

    '        If Me.DataGrid1.Items.Count > 0 Then

    '            NomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")
    '            Session.Add("NOME_FILE", NomeFile)

    '            With myExcelFile

    '                .CreateFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".xls")

    '                .PrintGridLines = False
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
    '                .SetDefaultRowHeight(14)
    '                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
    '                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
    '                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
    '                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "PROG/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "SAL/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "EMISSIONE", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "BENEFICIARIO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "IMP. CONSUNTIVATO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NUM. REPERTORIO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DESCRIZIONE", 0)


    '                K = 2

    '                .SetColumnWidth(1, 3, 12)
    '                .SetColumnWidth(4, 4, 45)
    '                .SetColumnWidth(5, 6, 20)
    '                .SetColumnWidth(7, 7, 45)


    '                For Each oDataGridItem In Me.DataGrid1.Items

    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, oDataGridItem.Cells(1).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, oDataGridItem.Cells(2).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, oDataGridItem.Cells(4).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, oDataGridItem.Cells(5).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, oDataGridItem.Cells(6).Text, 4)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, oDataGridItem.Cells(8).Text, 0)
    '                    If Trim(oDataGridItem.Cells(9).Text) = "&nbsp;" Then
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)
    '                    Else
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, oDataGridItem.Cells(9).Text, 0)
    '                    End If


    '                    K = K + 1
    '                Next

    '                .CloseFile()
    '            End With

    '            Dim objCrc32 As New Crc32()
    '            Dim strmZipOutputStream As ZipOutputStream
    '            Dim zipfic As String

    '            zipfic = Server.MapPath("..\..\..\FileTemp\" & NomeFile & ".zip")

    '            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '            strmZipOutputStream.SetLevel(6)
    '            '
    '            Dim strFile As String
    '            strFile = Server.MapPath("..\..\..\FileTemp\" & NomeFile & ".xls")

    '            Dim strmFile As FileStream = File.OpenRead(strFile)
    '            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '            '
    '            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '            Dim sFile As String = Path.GetFileName(strFile)
    '            Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '            Dim fi As New FileInfo(strFile)
    '            theEntry.DateTime = fi.LastWriteTime
    '            theEntry.Size = strmFile.Length
    '            strmFile.Close()
    '            objCrc32.Reset()
    '            objCrc32.Update(abyBuffer)
    '            theEntry.Crc = objCrc32.Value
    '            strmZipOutputStream.PutNextEntry(theEntry)
    '            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '            strmZipOutputStream.Finish()
    '            strmZipOutputStream.Close()

    '            File.Delete(strFile)
    '            Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".zip")
    '            'Response.Write("<script>window.open('../../../FileTemp/" & Me.FileNameXLS.Value & ".xls','','');</script>")

    '        Else
    '            Response.Write("<script>alert('Nessun Pagamento Trovato!');</script>")
    '        End If

    '    Else
    '        Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".zip")
    '    End If

    'End Sub


    '11/05/2015 MODIFICA PER RECUPERARE DATA SCADENZA DALLE TABELLE DI SUPPORTO
    Private Function CalcolaDataScadenza(ByVal TipoModalita As String, ByVal tipoPagamento As String, ByVal DataScadPagamento As String) As String
        CalcolaDataScadenza = ""
        TipoModalita = TipoModalita.ToUpper.Replace("NULL", "")
        tipoPagamento = tipoPagamento.ToUpper.Replace("NULL", "")

        If String.IsNullOrEmpty(DataScadPagamento) Then
            If Not String.IsNullOrEmpty(TipoModalita) Then
                Dim Table As String = ""
                Dim Column As String = ""
                Dim FlSomma As Integer = 0
                Dim DaySum As Integer = 0
                par.cmd.CommandText = "SELECT tab_rif,fld_rif,fl_somma_giorni FROM siscom_mi.TAB_DATE_MODALITA_PAG WHERE ID = (SELECT id_data_riferimento FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = " & idModalita.Value & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    Table = par.IfNull(lettore("tab_rif"), "")
                    Column = par.IfNull(lettore("fld_rif"), "")
                    FlSomma = par.IfNull(lettore("fl_somma_giorni"), "")
                End If
                lettore.Close()

                If Not String.IsNullOrEmpty(Table) And Not String.IsNullOrEmpty(Column) Then
                    par.cmd.CommandText = "select " & Column & " from siscom_Mi." & Table & " where id = " & Me.txtid.Text
                    CalcolaDataScadenza = par.IfNull(par.cmd.ExecuteScalar, "")
                End If

                If Not String.IsNullOrEmpty(CalcolaDataScadenza) Then
                    If FlSomma = 1 Then
                        par.cmd.CommandText = "select nvl(num_giorni,0) from siscom_mi.tipo_pagamento where id = " & tipoPagamento
                        DaySum = par.IfNull(par.cmd.ExecuteScalar, 0)

                        If DaySum > 0 Then
                            CalcolaDataScadenza = Date.Parse(par.FormattaData(CalcolaDataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(DaySum).ToString("dd/MM/yyyy")
                            CalcolaDataScadenza = par.AggiustaData(CalcolaDataScadenza)
                        End If
                    End If
                End If
            End If
        End If

        If String.IsNullOrEmpty(CalcolaDataScadenza) Then
            CalcolaDataScadenza = DataScadPagamento
        End If

    End Function
    Protected Sub btnStampaPagamento_Click(sender As Object, e As System.EventArgs) Handles btnStampaPagamento.Click
        DataEmissione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
        Panel1.Visible = True
        Dim flagconnessione As Boolean
        Try
            '*******************APERURA CONNESSIONE*********************
            If String.IsNullOrEmpty(txtid.Text) Then
                RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")

            Else
                flagconnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    flagconnessione = True
                End If
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN ( " & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & Me.txtid.Text & "Order BY ID DESC"
                Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                If String.IsNullOrEmpty(nome) Then
                    Panel1.Visible = True

                    par.cmd.CommandText = "SELECT NVL(PAGAMENTI.DATA_EMISSIONE_PAGAMENTO,PAGAMENTI.DATA_EMISSIONE) AS DATA_EMISSIONE,DATA_SCADENZA,DESCRIZIONE_BREVE,PAGAMENTI.DESCRIZIONE,PAGAMENTI.PROGR,PAGAMENTI.ANNO, " _
                          & "(select descrizione from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as modalita," _
                          & "(select descrizione from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as condizione, " _
                          & "(select id from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_modalita," _
                          & "(select id from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_condizione " _
                          & ",'SAL n. '||pagamenti.progr_appalto||'/'||pagamenti.anno||' del '||siscom_mi.getdata (pagamenti.data_sal) as sal " _
                          & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                          & " WHERE   PAGAMENTI.ID=" & Me.txtid.Text _
                          & " AND PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                          & " AND PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "

                    Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim sal As String = ""
                    If Lettore.Read Then
                        DataEmissione.Text = par.FormattaData(par.IfNull(Lettore("DATA_EMISSIONE"), ""))
                        ADP.Text = "Attestato di pagamento N." & par.IfNull(Lettore("PROGR"), "") & "/" & par.IfNull(Lettore("ANNO"), "")
                        txtModalitaPagamento.Text = par.IfNull(Lettore("modalita"), "")
                        txtCondizionePagamento.Text = par.IfNull(Lettore("condizione"), "")
                        idCondizione.Value = par.IfNull(Lettore("id_Condizione"), "NULL")
                        idModalita.Value = par.IfNull(Lettore("id_Modalita"), "NULL")
                        Me.txtDataScadenza.Text = par.FormattaData(CalcolaDataScadenza(idModalita.Value, idCondizione.Value, par.IfNull(Lettore("DATA_SCADENZA"), "")))
                        'Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione_breve"), "")
                        Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione"), "")
                        sal = par.IfNull(Lettore("sal"), "")

                    End If
                    Lettore.Close()
                    If txtDescrizioneBreve.Text = "" Then
                        If Len(sal) > 7 Then
                            'txtDescrizioneBreve.Text &= " (" & sal & ")"
                            txtDescrizioneBreve.Text = sal
                        Else
                            txtDescrizioneBreve.Text = txtDescrizioneBreve.Text
                        End If
                    End If
                    RadWindowVociServizi.Title = "Stampa Pagamento"
                    Dim Script As String = "function f(){$find(""" + RadWindowVociServizi.ClientID + """).show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    If Not String.IsNullOrWhiteSpace(Script) Then
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", Script, True)
                    End If
                Else
                    Response.Write("<script>window.open('../../../ALLEGATI/SAL/" & nome & "','AttPagamento','');self.close();</script>")
                End If
                '*********************CHIUSURA CONNESSIONE**********************
                If flagconnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If

            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If flagconnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Panel1.Visible = False
        End Try
    End Sub




    Protected Sub ImgConferma_Click(sender As Object, e As System.EventArgs) Handles ImgConferma.Click
        If IsDate(DataEmissione.Text) Then
            Dim dataemissioneSal As String = ""
            Dim flagconnessione As Boolean
            Try
                '*******************APERURA CONNESSIONE*********************
                flagconnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    flagconnessione = True
                End If

                par.cmd.CommandText = "SELECT DATA_EMISSIONE from siscom_mi.PAGAMENTI WHERE PAGAMENTI.ID=" & Me.txtid.Text
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lettore.Read Then
                    dataemissioneSal = par.IfNull(Lettore("DATA_EMISSIONE"), "")
                End If
                Lettore.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                If flagconnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                If dataemissioneSal > par.AggiustaData(DataEmissione.Text) Then
                    Response.Write("<script>alert('La data di emissione dell\'attestato di pagamento \ndeve essere successiva alla data di emissione del SAL!');</script>")
                Else
                    Panel1.Visible = False
                    stampaP(DataEmissione.Text)
                End If

            Catch ex As Exception
                If flagconnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Panel1.Visible = False
            End Try
        Else
            Response.Write("<script>alert('Inserire correttamente data emissione e data scadanza!');</script>")
        End If
    End Sub

    Private Sub stampaP(ByVal dataEmiss As String)
        Dim sStr1 As String

        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, ritenutaIVATA As Decimal
        Dim risultato4Tot As Decimal
        Dim FlagConnessione As Boolean

        Try

            If Me.txtid.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            Else

                'RIEPILOGO SAL
                importoT = 0
                penaleT = 0
                oneriT = 0
                astaT = 0
                ivaT = 0
                ritenutaT = 0
                rimborsoT = 0
                risultato1T = 0
                risultato2T = 0
                risultato3T = 0
                risultato4T = 0
                risultatoImponibileT = 0

                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoMANU2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                '*******************APERURA CONNESSIONE*********************
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If


                '****Scrittura evento EMISSIONE DEL PAGAMENTO
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Me.txtid.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','Stampa Attestato di Pagamento')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""


                Dim dataScadenza As String = "NULL"
                If IsDate(txtDataScadenza.Text) Then
                    dataScadenza = "'" & par.AggiustaData(txtDataScadenza.Text) & "'"
                End If
                contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(Me.txtid.Text))

                par.cmd.CommandText = "UPDATE SISCOM_MI.PAGAMENTI " _
                    & " SET DATA_EMISSIONE_PAGAMENTO='" & par.AggiustaData(dataEmiss) & "', " _
                    & " DATA_SCADENZA=" & dataScadenza & ", " _
                    & " DESCRIZIONE_BREVE='" & par.PulisciStrSql(txtDescrizioneBreve.Text) & "', " _
                    & " ID_TIPO_MODALITA_PAG=" & idModalita.Value & ", " _
                    & " ID_TIPO_PAGAMENTO=" & idCondizione.Value & " " _
                    & " WHERE ID=" & Me.txtid.Text
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = ""


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                par.cmd.CommandText = "select MANUTENZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                   & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                   & " where MANUTENZIONI.ID_PAGAMENTO=" & Me.txtid.Text _
                                   & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "

                myReader1 = par.cmd.ExecuteReader()
                Dim imponibile_rimborso As Decimal = 0
                Dim iva_rimborso As Decimal = 0

                While myReader1.Read
                    ''***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                    'If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then

                    par.cmd.CommandText = "SELECT IMPONIBILE_RIMBORSO, PERC_IVA_RIMBORSO " _
                              & "FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                              & "WHERE     ID_MANUTENZIONI_INTERVENTI IN (SELECT ID " _
                              & "FROM SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                              & "WHERE ID_MANUTENZIONE = " & par.IfNull(myReader1("id"), "-1") & ") " _
                              & "AND COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE' "
                    Dim myReaderRimb As Oracle.DataAccess.Client.OracleDataReader
                    myReaderRimb = par.cmd.ExecuteReader
                    While myReaderRimb.Read
                        imponibile_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0"))
                        iva_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0")) * CDec(par.IfNull(myReaderRimb("PERC_IVA_RIMBORSO"), "0")) / 100
                    End While
                    myReaderRimb.Close()

                    CalcolaImporti(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), par.IfNull(myReader1("IVA_CONSUMO"), 0), par.IfNull(myReader1("RIMBORSI"), 0), par.IfNull(myReader1("PENALE2"), 0), par.IfNull(myReader1("ID_PF_VOCE"), 0), par.IfNull(myReader1("ID_APPALTO"), 0))
                    'Else
                    'Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento!');</script>")
                    'myReader1.Close()

                    ''*********************CHIUSURA CONNESSIONE**********************
                    'If FlagConnessione = True Then
                    '    par.cmd.Dispose()
                    '    par.OracleConn.Close()
                    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    'End If

                    'Exit Sub
                    'End If
                End While
                myReader1.Close()

                '$anno=                 PAGAMENTI.ANNO
                '$progr=                PAGAMENTI.PROGR
                '$data_emissione=       PAGAMENTI.DATA_EMISSIONE
                '$dettagli_chiamante=   N. APPALTI.NUM_REPERTORIO DEL APPALTI.DATA_REPERTORIO APPALTI.DESCRIZIONE

                '$dettagli= 
                '$imp_letterale=        contenuto = Replace(contenuto, "$imp_letterale$", NumeroInLettere(par.IfNull(myReader1("PAGAMENTI.IMPORTO_PRENOTATO"), 0)))

                '$data_stampa=          contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("PAGAMENTI.DATA_STAMPA")))
                '$chiamante=            contenuto = Replace(contenuto, "$chiamante$", "CONTRATTO")


                '$cod_capitolo=         PF_VOCI.CODICE
                '$voce_pf=              PF_VOCI.DESCRIZIONE
                '$finanziamento=        contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")
                '$dettaglio=            ???
                '$TOT=                  contenuto = Replace(contenuto, "$TOT$", par.IfNull(myReader1("PAGAMENTI.IMPORTO_PRENOTATO"), 0))


                sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.PROGR_APPALTO,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_STAMPA,PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.DESCRIZIONE,PAGAMENTI.CONTO_CORRENTE," _
                            & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                            & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE, FORNITORI.ID as ID_FORNITORE," _
                            & " PF_VOCI.ID as ""ID_VOCI"",PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"", " _
                            & "(select descrizione from siscom_mi.tipo_modalita_pag where id=pagamenti.id_tipo_modalita_pag) as modalita," _
                            & " (select descrizione from siscom_mi.tipo_pagamento where id=pagamenti.id_tipo_pagamento) as condizione,to_char(to_date(pagamenti.data_scadenza,'yyyyMMdd'),'dd/MM/yyyy') as data_scadenza,pagamenti.descrizione_breve " _
                     & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.PRENOTAZIONI " _
                     & " where   PAGAMENTI.ID=" & Me.txtid.Text _
                         & " and PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                         & " and PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                         & " and PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO (+) " _
                         & " and PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) "

                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader

                If myReader1.Read Then


                    contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReader1("modalita"), "-"))
                    contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReader1("condizione"), "-"))
                    contenuto = Replace(contenuto, "$datascadenza$", par.IfNull(myReader1("data_scadenza"), "-"))
                    contenuto = Replace(contenuto, "$descrpag$", par.IfNull(myReader1("descrizione_breve"), "-"))


                    'contenuto = Replace(contenuto, "$chiamante$", "CONTRATTO:")

                    contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progr$", myReader1("PROGR"))
                    contenuto = Replace(contenuto, "$annoSAL$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progrSAL$", myReader1("PROGR_APPALTO"))

                    contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "")))
                    contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))

                    'contenuto = Replace(contenuto, "$dettagli_chiamante$", "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(myReader1("DATA_REPERTORIO")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$contratto$", "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(myReader1("DATA_REPERTORIO")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$CIG$", par.IfNull(myReader1("CIG"), ""))
                    contenuto = Replace(contenuto, "$conto_corrente$", par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01"))

                    'FORNITORI
                    Dim sFORNITORI As String = ""
                    If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            sFORNITORI = par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                        Else
                            sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                        End If

                    Else
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            sFORNITORI = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        Else
                            sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        End If
                    End If
                    contenuto = Replace(contenuto, "$fornitoreIntestazione$", sFORNITORI)
                    'INDIRIZZO FORNITORE
                    Dim sIndirizzoFornitore1 As String = ""
                    Dim sComuneFornitore1 As String = ""
                    par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReader1("ID_FORNITORE"), 0)

                    Dim myReaderTT As Oracle.DataAccess.Client.OracleDataReader
                    myReaderTT = par.cmd.ExecuteReader
                    While myReaderTT.Read

                        sIndirizzoFornitore1 = par.IfNull(myReaderTT("TIPO"), "") _
                                        & " " & par.IfNull(myReaderTT("INDIRIZZO"), "") _
                                        & " " & par.IfNull(myReaderTT("CIVICO"), "")

                        sComuneFornitore1 = par.IfNull(myReaderTT("CAP"), "") _
                                        & " " & par.IfNull(myReaderTT("COMUNE"), "")

                    End While
                    myReaderTT.Close()
                    contenuto = Replace(contenuto, "$fornitore_indirizzo$", sIndirizzoFornitore1)
                    contenuto = Replace(contenuto, "$comuneIntestazione$", sComuneFornitore1)


                    'IBAN **************************************************
                    par.cmd.CommandText = "select IBAN||' - '||(SELECT DISTINCT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI_IBAN.ID_FORNITORE=FORNITORI.ID) AS IBAN from SISCOM_MI.FORNITORI_IBAN " _
                                       & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"

                    Dim myReaderBP As Oracle.DataAccess.Client.OracleDataReader
                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        sFORNITORI = sFORNITORI & "<br/>" & par.IfNull(myReaderBP("IBAN"), "")
                    End While
                    myReaderBP.Close()
                    contenuto = Replace(contenuto, "$fornitori$", sFORNITORI)
                    '******************************************************


                    '*****************Modifica Peppe 06/08/2010, $TOT$ deve essere uguale a riultato4, lo sposto dopo il calcolo dello stesso*******
                    'contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "", "##,##0.00"))

                    '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO

                    'sStr1 = "select * from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '    & " where ID_PF_VOCE_IMPORTO=" & par.IfNull(myReader1("ID_VOCI"), "Null") _
                    '    & " and   ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), "Null")

                    'Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                    'par.cmd.CommandText = sStr1
                    'myReaderT = par.cmd.ExecuteReader

                    'If myReaderT.Read Then
                    '    Dim importo, perc_oneri, perc_sconto, perc_iva As Double
                    '    Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4 As Double

                    'importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                    'perc_oneri = par.IfNull(myReader1("PERC_ONERI_SIC_CON"), 0)

                    'perc_sconto = par.IfNull(myReaderT("SCONTO_CONSUMO"), 0)
                    'perc_iva = par.IfNull(myReaderT("IVA_CONSUMO"), 0)


                    ''ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                    'oneri = importo - ((importo * 100) / (100 + perc_oneri))

                    ''LORDO senza ONERI= IMPORTO-oneri
                    'risultato1 = importo - oneri

                    ''RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                    'asta = (risultato1 * perc_sconto) / 100

                    ''NETTO senza ONERI= (LORDO senza oneri-asta)
                    'risultato2 = risultato1 - asta

                    ''NETTO con ONERI= (IMPORTO-asta)
                    'risultato3 = importo - asta

                    ''IVA= (NETTO con oneri*perc_iva)/100
                    'iva = (risultato3 * perc_iva) / 100

                    ''NETTO+ONERI+IVA
                    'risultato4 = risultato3 + iva

                    contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(risultato4T, 0), "", "##,##0.00"))


                    ' sDescrizione = "Pagamento ODL (Vedi Allegato)" '& vbCrLf & vbCrLf

                    Dim S2 As String = "<table style='width:100%;'>"
                    S2 = S2 & "<tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(importoT, 0), "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(oneriT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato1T, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(astaT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2T, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3T, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenutaT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Imponibile (al netto trattenute) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultatoImponibileT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "</tr><tr>"
                    Dim percIva As String = "0"
                    par.cmd.CommandText = "select sum(anticipo_contrattuale) as anticipo_contrattuale from siscom_mi.prenotazioni where id_pagamento = " & txtid.Text
                    Dim anticipo As Decimal = CDec(par.cmd.ExecuteScalar.ToString)
                    If par.IfEmpty(anticipo, 0) > 0 Then

                        Dim totaleIVA As String = ""
                        par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) as PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"
                        Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore1.Read Then
                            percIva = par.IfNull(lettore1("PERC_IVA"), "")
                        End If
                        lettore1.Close()
                        totaleIVA = (anticipo * CDec(percIva) / 100)
                        S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(anticipo, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Importo IVA recupero (" & IsNumFormat(percIva, "", "##,##0") & "%) €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(totaleIVA, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                    End If

                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(iva_rimborso, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Totale Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(rimborsoT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T, "", "##,##0.00") & "</td>"

                    If penaleT > 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left;  width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align:  right; width:40%; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                        S2 = S2 & "<td style='text-align:  right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(-penaleT, "", "##,##0.00") & "</td>"
                        'S2 = S2 & "</tr><tr>"
                    End If


                    If par.IfEmpty(anticipo, 0) > 0 Then

                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left;  width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T - (anticipo + (anticipo * CDec(percIva) / 100)), "", "##,##0.00") & "</td>"
                    ElseIf penaleT <> 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T - penaleT, "", "##,##0.00") & "</td>"
                    End If

                    S2 = S2 & "</tr></table>"


                    Dim T As String = "<table style='width:100%;'>"
                    T = T & "<tr>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                    T = T & "</tr></table>"

                    contenuto = Replace(contenuto, "$dettagli$", T)

                    '*********** DETTAGLIO GRIGLIA MANUTENZIONI
                    Dim TestoGrigliaINTESTAZIONE As String = "" ' "<p style='page-break-after: always'>&nbsp;</p>"

                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<table cellspacing='0' style='width:50%; border: 1px solid black;border-collapse: collapse;' >"
                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >A netto compresi oneri</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultato3T, "", "##,##0.00") & "</td>" _
                                              & "</tr>" _
                                              & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Ritenuta di legge 0,5 % (senza IVA)</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(ritenutaNoIvaT, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    If par.IfEmpty(anticipo, 0) > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Recupero anticipazione contrattuale (SENZA IVA)</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(anticipo, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If
                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile (al netto delle trattenute) </td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultatoImponibileT, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    If rimborsoT > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile rimborsi</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If
                    If penaleT > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Penale</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(-penaleT, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If

                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "</table>"
                    contenuto = Replace(contenuto, "$grigliaIntestazione$", TestoGrigliaINTESTAZIONE)
                    'TestoPagina = TestoPagina & "</table>"
                    Dim TestoGrigliaM As String = "<p style='page-break-before: always'>&nbsp;</p>"

                    TestoGrigliaM = TestoGrigliaM & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
                    TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ODL</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SAL</td>" _
                                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA ORDINE</td>" _
                                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA CONSUNTIVO</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI)</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IVA</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Tot. RIMBORSI</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI e IVA)</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. PENALE</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                              & "</tr>"


                    risultato4Tot = 0

                    par.cmd.CommandText = "select MANUTENZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                       & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                       & " where ID_PAGAMENTO=" & Me.txtid.Text _
                                       & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                                       & " order by MANUTENZIONI.PROGR "


                    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                    myReaderT = par.cmd.ExecuteReader

                    While myReaderT.Read
                        '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                        If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then

                            sStr1 = "select APPALTI_VOCI_PF.*,APPALTI.FL_RIT_LEGGE " _
                                & "  from   SISCOM_MI.APPALTI_VOCI_PF,SISCOM_MI.APPALTI " _
                                & "  where APPALTI_VOCI_PF.ID_PF_VOCE=" & par.IfNull(myReaderT("ID_PF_VOCE"), 0) _
                                & "  and   APPALTI_VOCI_PF.ID_APPALTO=" & par.IfNull(myReaderT("ID_APPALTO"), 0) _
                                & "  and   APPALTI.ID=" & par.IfNull(myReaderT("ID_APPALTO"), 0)

                            Dim myReaderT2 As Oracle.DataAccess.Client.OracleDataReader
                            par.cmd.CommandText = sStr1
                            myReaderT2 = par.cmd.ExecuteReader

                            If myReaderT2.Read Then

                                perc_oneri = par.IfNull(myReaderT2("PERC_ONERI_SIC_CON"), 0)

                                perc_sconto = par.IfNull(myReaderT2("SCONTO_CONSUMO"), 0)
                                perc_iva = par.IfNull(myReaderT("IVA_CONSUMO"), 0) 'par.IfNull(myReaderT2("IVA_CONSUMO"), 0)


                                'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                                oneri = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) - ((par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) * 100) / (100 + perc_oneri))
                                oneri = Round(oneri, 2)

                                'LORDO senza ONERI= IMPORTO-oneri
                                risultato1 = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) - oneri

                                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                                asta = (risultato1 * perc_sconto) / 100
                                asta = Round(asta, 2)

                                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                                risultato2 = risultato1 - asta '- penale

                                'AGGIUNTO
                                'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                                risultato3 = risultato2 + oneri

                                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                                If par.IfNull(myReaderT2("FL_RIT_LEGGE"), 0) = 1 Then
                                    ritenuta = (risultato3 * 0.5) / 100
                                    ritenuta = Round(ritenuta, 2)
                                    ritenutaIVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                                    ritenutaIVATA = Round(ritenutaIVATA, 2)

                                Else
                                    ritenuta = 0
                                    ritenutaIVATA = 0
                                End If

                                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                                risultato3 = risultato3 - ritenuta

                                'IVA= (NETTO con oneri*perc_iva)/100
                                iva = Math.Round((risultato3 * perc_iva) / 100, 2)

                                'NETTO+ONERI+IVA
                                risultato4 = risultato3 + iva + Round(par.IfNull(myReaderT("RIMBORSI"), 0), 2)
                                risultato4Tot = risultato4Tot + risultato4

                                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PROGR"), "") & "</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("ANNO"), "") & "</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("PROGR"), "") & "</td>" _
                                                      & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_INIZIO_ORDINE"), "")) & "</td>" _
                                                      & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_FINE_ORDINE"), "")) & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato3, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(iva, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("RIMBORSI"), 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato4, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("PENALE2"), 0), "##,##0.00") & "</td>" _
                                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                                      & "</tr>"


                            End If
                            myReaderT2.Close()
                        End If
                    End While
                    myReaderT.Close()


                    TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & IsNumFormat(risultato4Tot, "", "##,##0.00") & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                              & "</tr>"

                    contenuto = Replace(contenuto, "$grigliaM$", TestoGrigliaM)

                    '********************************



                    Dim TestoGrigliaBP As String = "" '"<p style='page-break-before: always'>&nbsp;</p>"

                    TestoGrigliaBP = TestoGrigliaBP & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 10pt; font-weight: bold'>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>CAPITOLO</td>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO BP</td>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 60%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width: 20%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                              & "</tr>"

                    sStr1 = "select PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"",prenotazioni.importo_approvato - nvl(rit_legge_ivata,0) as importo_voce " _
                            & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.PRENOTAZIONI " _
                     & " where   PAGAMENTI.ID=" & Me.txtid.Text _
                         & " and PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                         & " and PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                         & " and PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO (+) " _
                         & " and PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) "

                    par.cmd.CommandText = sStr1
                    Dim myReaderCapitolo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    While myReaderCapitolo.Read
                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderCapitolo("COD_VOCE"), "") & "</td>" _
                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.AnnoBPPag(Me.txtid.Text) & "</td>" _
                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderCapitolo("DESC_VOCE"), "") & "</td>" _
                                                    & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderCapitolo("IMPORTO_VOCE"), "0"), "", "##,##0.00") & "</td>" _
                                                    & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                                                    & "</tr>"
                    End While
                    myReaderCapitolo.Close()


                    par.cmd.CommandText = "select sum(anticipo_contrattuale) " _
                          & " from   SISCOM_MI.PRENOTAZIONI " _
                          & " where ID_PAGAMENTO=" & Me.txtid.Text

                    Dim anticipoContrattuale As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If anticipoContrattuale <> 0 Then


                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " RECUPERO ANTICIPAZIONE CONTRATTUALE   : " & IsNumFormat(anticipoContrattuale + (anticipoContrattuale * percIva / 100), "", "##,##0.00") & "</td>" _
                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                              & "</tr>"
                    End If

                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                        & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " TOTALE   : " & IsNumFormat(risultato4Tot - (anticipoContrattuale + (anticipoContrattuale * percIva / 100)), "", "##,##0.00") & "</td>" _
                        & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                        & "</tr>"
                    'If anticipo <> 0 Then
                    '    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                    '          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                    '          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                    '          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                    '          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " IMPORTO TRATTENUTO   : " & IsNumFormat(anticipo, "", "##,##0.00") & "</td>" _
                    '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                    '          & "</tr>"
                    'End If


                    'TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                    '      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                    '      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                    '      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                    '      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " TOTALE   : " & IsNumFormat(risultato4Tot - anticipo, "", "##,##0.00") & "</td>" _
                    '      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                    '      & "</tr>"

                    contenuto = Replace(contenuto, "$grigliaBP$", TestoGrigliaBP)
                    'End If
                    'myReaderT.Close()

                    '*****************FINE SCRITTURA DETTAGLI
                    contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))

                    'contenuto = Replace(contenuto, "$dettaglio$", "MANUTENZIONE")

                    'contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("COD_VOCE"), ""))
                    'contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESC_VOCE"), ""))
                    'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")

                    par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim chiamante2 As String = ""
                    If lettore.Read Then
                        chiamante2 = par.IfNull(lettore(0), "")
                    End If
                    lettore.Close()
                    contenuto = Replace(contenuto, "$chiamante2$", chiamante2)
                    par.cmd.CommandText = "SELECT INITCAP(GESTORI_ORDINI.DESCRIZIONE) FROM SISCOM_MI.GESTORI_ORDINI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI " _
                        & " WHERE APPALTI.ID_gESTORE_ORDINI=GESTORI_ORDINI.ID AND PAGAMENTI.ID_APPALTO=APPALTI.ID AND PAGAMENTI.ID=" & Me.txtid.Text
                    lettore = par.cmd.ExecuteReader
                    Dim gest As String = ""
                    If lettore.Read Then
                        gest = par.IfNull(lettore(0), "")
                    End If
                    lettore.Close()
                    contenuto = Replace(contenuto, "$proponente$", gest)



                End If
                myReader1.Close()


                Dim url As String = Server.MapPath("..\..\..\FileTemp\")
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
                pdfConverter1.PdfDocumentOptions.TopMargin = 30
                pdfConverter1.PdfDocumentOptions.BottomMargin = 10
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfFooterOptions.FooterText = ("")
                pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = ""

                pdfConverter1.PdfDocumentOptions.ShowFooter = True
                'pdfConverter1.PdfFooterOptions.ShowPageNumber = True
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False

                Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

                Dim i As Integer = 0
                For i = 0 To 10000
                Next

                Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")
                'GIANCARLO 16-02-2017
                'inserimento della stampa cdp negli allegati
                Dim descrizione As String = "Stampa pagamento"
                Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
                If HiddenFieldRielabPagam.Value = "1" Then
                    descrizione = "Stampa rielaborazione pagamento"
                    idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")

                End If


                par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
                Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
                par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, txtid.Text, "../../../ALLEGATI/SAL/")
                '*********************CHIUSURA CONNESSIONE**********************
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
                e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "'")
                e.Item.Attributes.Add("onDblclick", "document.getElementById('btnStampaPagamento').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Dim FlagConnessione As Boolean
        Dim sOrder As String
        Dim sStringaSql As String

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreStato = Request.QueryString("ST")

            sOrder = " order by SISCOM_MI.PAGAMENTI.DATA_EMISSIONE desc,PROG_ANNO desc"

            'STATO PAGAMENTO    0=PRENOTATO 1=EMESSO 5=PAGATO

            '& " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE"","
            '& " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_PRENOTATO,'9G999G990D99')) AS ""IMPORTO_PRENOTATO"", " 

            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",(SISCOM_MI.PAGAMENTI.PROGR_APPALTO||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""SAL_ANNO"",'' as ""DATA_PRENOTAZIONE""," _
                                 & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_EMISSIONE""," _
                                 & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                 & "     then  COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
                                 & "     else  COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||LTRIM(FORNITORI.NOME))) end  AS ""BENEFICIARIO""," _
                                 & " trim(SISCOM_MI.PAGAMENTI.DESCRIZIONE) as DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                                 & " trim(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                                 & " trim(SISCOM_MI.APPALTI.NUM_REPERTORIO) as ""NUM_REPERTORIO""," _
                                 & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO""" _
                         & " from SISCOM_MI.PAGAMENTI," _
                              & " SISCOM_MI.FORNITORI," _
                              & " SISCOM_MI.TAB_STATI_PAGAMENTI," _
                              & " SISCOM_MI.APPALTI" _
                         & " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=7 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) "


            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO " _
                                                                         & " from SISCOM_MI.PRENOTAZIONI " _
                                                                         & " where ID in (select ID_PRENOTAZIONE_PAGAMENTO " _
                                                                                      & " from SISCOM_MI.MANUTENZIONI " _
                                                                                      & " where ID_PF_VOCE_IMPORTO is null " _
                                                                                      & " AND MANUTENZIONI.STATO<>5 " _
                                                                                      & "   and ID_PIANO_FINANZIARIO = " & sValoreEsercizioFinanziarioR & ")" _
                                                                         & " and TIPO_PAGAMENTO=7 " _
                                                                         & " and ID_STATO>=2 " _
                                                                         & " and ID_PAGAMENTO is not null ) "

            End If

            If par.IfEmpty(sValoreStato, -1) <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.STATO_FIRMA=" & sValoreStato
            End If

            If sValoreFornitore <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            End If


            If sValoreAppalto <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_APPALTO=" & sValoreAppalto
            End If


            If sValoreServizio <> "-1" Then
                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                              & " where ID_VOCE_PF=" & sValoreServizio _
                                                                              & "   and ID_STRUTTURA=" & sValoreStruttura & ")"
                Else
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI where ID_VOCE_PF=" & sValoreServizio & ")"
                End If
            Else
                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                              & " where TIPO_PAGAMENTO=7 and ID_STRUTTURA=" & sValoreStruttura & ")"
                End If
            End If


            sStringaSql = sStringaSql & sOrder





            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("NOME_FILE")
        Page.Dispose()
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid1.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "PAGAMENTI", "PAGAMENTI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub ImgAnnulla_Click(sender As Object, e As System.EventArgs) Handles ImgAnnulla.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Private Sub btnNuovaRicerca_Click(sender As Object, e As EventArgs) Handles btnNuovaRicerca.Click
        Session.Remove("NOME_FILE")

        Response.Write("<script>document.location.href=""RicercaPagamentiRRS.aspx""</script>")
    End Sub
End Class
