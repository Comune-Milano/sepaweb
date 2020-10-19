Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Pagamenti_RisultatiPagManuale
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Str As String
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Str = "<div align='center' id='divPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Contabilita/IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then

            Response.Flush()
            If Not IsNothing(Request.QueryString("T")) Then
                tipoPagamanto.Value = Request.QueryString("T")
            End If
            Cerca()

        End If
        Beep()
    End Sub
    Public Property Query() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property

    Public Property strChiamata() As String
        Get
            If Not (ViewState("par_strChiamata") Is Nothing) Then
                Return CStr(ViewState("par_strChiamata"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strChiamata") = value
        End Set

    End Property

    Private Sub Cerca()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Indietro.Value = "COGNOME=" & Request.QueryString("COGNOME") _
                          & "☺NOME=" & Request.QueryString("NOME") _
                          & "☺CF=" & Request.QueryString("CF") _
                          & "☺RS=" & Request.QueryString("RS") _
                          & "☺PIVA=" & Request.QueryString("PIVA") _
                          & "☺CODCONT=" & Request.QueryString("CODCONT") _
                          & "☺CODUI=" & Request.QueryString("CODUI")


            '*******Elenco variabili criteri di ricerca
            Dim Cognome As String = Request.QueryString("COGNOME")
            Dim Nome As String = Request.QueryString("NOME")
            Dim CF As String = Request.QueryString("CF")
            Dim RagSoc As String = Request.QueryString("RS")
            Dim IVA As String = Request.QueryString("PIVA")
            Dim CodContr As String = Request.QueryString("CODCONT")
            Dim CodUI As String = Request.QueryString("CODUI")

            Dim sValore As String
            Dim sCompara As String

            sStringaSql = ""

            'If IdAnagrafica = "0" Then

            If par.IfEmpty(Cognome, "Null") <> "Null" Then
                sValore = Cognome.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.COGNOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(Nome, "Null") <> "Null" Then
                sValore = Nome.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.NOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            'Else
            'sStringaSql = sStringaSql & " AND ANAGRAFICA.id = " & IdAnagrafica

            'End If

            If par.IfEmpty(CF, "Null") <> "Null" Then
                sValore = CF.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.COD_FISCALE) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            '*********RAGIONE SOCIALE E PARTITA IVA***

            If par.IfEmpty(RagSoc, "Null") <> "Null" Then
                sValore = RagSoc.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.RAGIONE_SOCIALE) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If


            If par.IfEmpty(IVA, "Null") <> "Null" Then
                sValore = IVA.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(ANAGRAFICA.PARTITA_IVA) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            '*****************FINE********************

            If par.IfEmpty(CodContr, "Null") <> "Null" Then
                sValore = CodContr.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(RAPPORTI_UTENZA.COD_CONTRATTO)" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If par.IfEmpty(CodUI, "Null") <> "Null" Then
                sValore = CodUI.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " AND UPPER(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE)" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            '                        & "AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (SELECT ID_CONTRATTO FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND BOL_BOLLETTE.FL_ANNULLATA = '0' AND BOL_BOLLETTE.IMPORTO_TOTALE <> nvl(BOL_BOLLETTE.IMPORTO_PAGATO,0)) " _

            sStringaSql = "SELECT DISTINCT ANAGRAFICA.ID AS ID_ANAGRAFICA,SOGGETTI_CONTRATTUALI.ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,siscom_mi.getstatocontratto(rapporti_utenza.id) as stato_contr, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"", " _
                        & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"",TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA , ANAGRAFICA.SESSO, ANAGRAFICA.TELEFONO," _
                        & "RTRIM(LTRIM(ANAGRAFICA.INDIRIZZO_RESIDENZA||','|| ANAGRAFICA.CIVICO_RESIDENZA)) AS RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA, ANAGRAFICA.PROVINCIA_RESIDENZA " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
                        & "SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, " _
                        & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
                        & "RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
                        & "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) " _
                        & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' " & sStringaSql
            '                        & "AND nvl(SOGGETTI_CONTRATTUALI.DATA_FINE,TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd'))>= TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') " _

            sStringaSql = sStringaSql & " ORDER BY INTESTATARIO ASC"
            Query = sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

            Dim dt As New Data.DataTable()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                dgvResult.DataSource = dt
                dgvResult.DataBind()
                Session.Add("dtExport", dt)

                If Not String.IsNullOrEmpty(CodContr) Then
                    IdAnagrafica.Value = dt.Rows(0).Item("ID_ANAGRAFICA")
                    IdContratto.Value = dt.Rows(0).Item("ID_CONTRATTO")
                    Indietro.Value = "-1"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Visual", "Visualizza();", True)
                End If
            Else
                TrovaIntestaNucleo()
            End If

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

    'End Function
    Private Sub TrovaIntestaNucleo()
        Try

            If tipoPagamanto.Value = "R" Then
                par.cmd.CommandText = sStringaSql.Replace("AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'", "").Replace("AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (SELECT ID_CONTRATTO FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.FL_ANNULLATA = '0' AND BOL_BOLLETTE.IMPORTO_RUOLO <> BOL_BOLLETTE.IMP_RUOLO_PAGATO)", "")
            ElseIf tipoPagamanto.Value = "I" Then
                par.cmd.CommandText = sStringaSql.Replace("AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'", "").Replace("AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (SELECT ID_CONTRATTO FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.FL_ANNULLATA = '0' AND BOL_BOLLETTE.IMPORTO_INGIUNZIONE <> BOL_BOLLETTE.IMP_INGIUNZIONE_PAG)", "")
            Else
                par.cmd.CommandText = sStringaSql.Replace("AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'", "").Replace("AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (SELECT ID_CONTRATTO FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.FL_ANNULLATA = '0' AND BOL_BOLLETTE.IMPORTO_TOTALE <> BOL_BOLLETTE.IMPORTO_PAGATO)", "")
            End If
            Dim queryStr As String = ""
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim contratti As String = ""
            Dim Primo As Boolean = True
            Dim inte As String = ""
            While Lettore.Read
                inte = par.IfNull(Lettore("INTESTATARIO"), "")
                If Primo = True Then
                    contratti = Lettore("ID_CONTRATTO")
                    Primo = False
                Else
                    contratti = contratti & "," & Lettore("ID_CONTRATTO")
                End If
            End While
            If Not String.IsNullOrEmpty(contratti) Then
                sStringaSql = "SELECT DISTINCT ANAGRAFICA.ID AS ID_ANAGRAFICA,SOGGETTI_CONTRATTUALI.ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"", " _
                & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"",TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA , ANAGRAFICA.SESSO, ANAGRAFICA.TELEFONO," _
                & "RTRIM(LTRIM(ANAGRAFICA.INDIRIZZO_RESIDENZA||','|| ANAGRAFICA.CIVICO_RESIDENZA)) AS RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA, ANAGRAFICA.PROVINCIA_RESIDENZA " _
                & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
                & "SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, " _
                & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
                & "RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
                & "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) " _
                & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (" & contratti & ")  ORDER BY INTESTATARIO ASC"
                '                & "AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (SELECT ID_CONTRATTO FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND BOL_BOLLETTE.FL_ANNULLATA = '0' AND BOL_BOLLETTE.IMPORTO_TOTALE <> BOL_BOLLETTE.IMPORTO_PAGATO) " _


                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                dgvResult.DataSource = dt
                dgvResult.DataBind()

                If dt.Rows.Count > 0 Then
                    Response.Write("<script>alert('Il soggetto ricercato non risulta essere intestatario di un Rapporto!\nVerranno caricati i contratti su cui " & inte & " è componente del nucleo!')</script>")
                    Session.Add("dtExport", dt)
                Else
                    Response.Write("<script>alert('Il soggetto ricercato non risulta essere intestatario di un Rapporto!\nVerranno caricati i contratti su cui " & inte & " è componente del nucleo!')</script>")
                    Response.Write("<script>alert('La ricerca non ha prodotto risultati!')</script>")
                    Response.Write("<script>parent.main.location.replace('RicercaPagManuale.aspx');</script>")
                End If

            Else
                Response.Write("<script>alert('La ricerca non ha prodotto risultati!')</script>")
                'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)
                'Dim dt As New Data.DataTable()
                'da.Fill(dt)
                'dgvResult.DataSource = dt
                'dgvResult.DataBind()
                If tipoPagamanto.Value <> "" Then
                    queryStr = "?T=" & tipoPagamanto.Value
                End If
                Response.Write("<script>parent.main.location.replace('RicercaPagManuale.aspx" & queryStr & "');</script>")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgvResult_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvResult.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il soggetto: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('IdAnagrafica').value='" & e.Item.Cells(0).Text & "';document.getElementById('CFIVA').value='" & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(3).Text & "';")
            e.Item.Attributes.Add("onDblclick", "Visualizza();")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il soggetto: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('IdAnagrafica').value='" & e.Item.Cells(0).Text & "';document.getElementById('CFIVA').value='" & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(3).Text & "';")
            e.Item.Attributes.Add("onDblclick", "Visualizza();")

        End If
    End Sub

    Protected Sub dgvResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvResult.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            dgvResult.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim dt As New Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("dtExport"), Data.DataTable)
            sNomeFile = "ExportPagManuale_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls"))
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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "C.F./P.IVA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA NASCITA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "SESSO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TELEFONO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "RESIDENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "COMUNE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "PR.", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("COD_CONTRATTO"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("INTESTATARIO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("CFIVA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("DATA_NASCITA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("SESSO"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("TELEFONO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("RESIDENZA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("COMUNE_RESIDENZA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("PROVINCIA_RESIDENZA"), ""))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls")
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
            Response.Redirect("..\..\FileTemp\" & sNomeFile & ".zip")


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub
End Class
