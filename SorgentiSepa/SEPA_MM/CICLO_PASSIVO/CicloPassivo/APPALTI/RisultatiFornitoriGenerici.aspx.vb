Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class MANUTENZIONI_RisultatiFornitoriGenerici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Public nome As String
    Public cognome As String
    Public cf As String
    Public ragsociale As String
    Public piva As String
    Public f As String
    Public g As String
    Dim dt As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then

            nome = UCase(Request.QueryString("NO"))
            cognome = UCase(Request.QueryString("CO"))
            cf = UCase(Request.QueryString("CF"))
            ragsociale = UCase(Request.QueryString("RA"))
            piva = UCase(Request.QueryString("PI"))
            f = Request.QueryString("F")
            g = Request.QueryString("G")

            Cerca()

            Response.Flush()

        End If
    End Sub
    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""


        If cognome <> "" And cognome <> "*" Then
            sValore = cognome
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.FORNITORI.COGNOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If nome <> "" And nome <> "*" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = nome
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.FORNITORI.NOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If cf <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = cf
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.FORNITORI.COD_FISCALE" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If ragsociale <> "" And ragsociale <> "*" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = ragsociale
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.FORNITORI.RAGIONE_SOCIALE" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If piva <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = piva
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.FORNITORI.PARTITA_IVA" & sCompara & " '" & sValore & "' "
        End If



        sStringaSQL1 = "select SISCOM_MI.FORNITORI.ID, SISCOM_MI.FORNITORI.TIPO, SISCOM_MI.FORNITORI.RAGIONE_SOCIALE, SISCOM_MI.FORNITORI.COGNOME, SISCOM_MI.FORNITORI.NOME, SISCOM_MI.FORNITORI.COD_FISCALE_R, SISCOM_MI.FORNITORI.COD_FISCALE, " _
    & "SISCOM_MI.FORNITORI.PARTITA_IVA, SISCOM_MI.FORNITORI.COMUNE_RESIDENZA, SISCOM_MI.FORNITORI.PR_RESIDENZA, SISCOM_MI.FORNITORI.TIPO_INDIRIZZO_RESIDENZA, SISCOM_MI.FORNITORI.INDIRIZZO_RESIDENZA, " _
    & "SISCOM_MI.FORNITORI.CIVICO_RESIDENZA, SISCOM_MI.FORNITORI.CAP_RESIDENZA, SISCOM_MI.FORNITORI.NUM_TELEFONO, SISCOM_MI.FORNITORI.NUM_FAX, SISCOM_MI.FORNITORI.COMUNE_SEDE_A, SISCOM_MI.FORNITORI.PR_SEDE_A, " _
    & "SISCOM_MI.FORNITORI.TIPO_INDIRIZZO_SEDE_A, SISCOM_MI.FORNITORI.INDIRIZZO_SEDE_A, SISCOM_MI.FORNITORI.CIVICO_SEDE_A, SISCOM_MI.FORNITORI.CAP_SEDE_A, SISCOM_MI.FORNITORI.NUM_TELEFONO_SEDE_A, " _
    & "SISCOM_MI.FORNITORI.NUM_FAX_SEDE_A, SISCOM_MI.FORNITORI.TIPO_R, SISCOM_MI.FORNITORI.NUM_PROCURA, TO_CHAR(TO_DATE(SISCOM_MI.FORNITORI.DATA_PROCURA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA PROCURA"", SISCOM_MI.FORNITORI.COD_FISCALE_R, " _
    & "SISCOM_MI.FORNITORI.IBAN, SISCOM_MI.FORNITORI.RIFERIMENTI FROM SISCOM_MI.FORNITORI"


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY SISCOM_MI.FORNITORI.RAGIONE_SOCIALE ASC, SISCOM_MI.FORNITORI.COGNOME ASC, SISCOM_MI.FORNITORI.NOME ASC "

        dt.Columns.Add("ID")
        dt.Columns.Add("TIPO")
        dt.Columns.Add("RAGIONE_SOCIALE")
        dt.Columns.Add("PARTITA_IVA")
        dt.Columns.Add("COD_FISCALE")
        dt.Columns.Add("IBAN")
        dt.Columns.Add("TIPO_INDIRIZZO_RESIDENZA")
        dt.Columns.Add("INDIRIZZO_RESIDENZA")
        dt.Columns.Add("CIVICO_RESIDENZA")
        dt.Columns.Add("CAP_RESIDENZA")
        dt.Columns.Add("COMUNE_RESIDENZA")
        dt.Columns.Add("PR_RESIDENZA")
        dt.Columns.Add("NUM_TELEFONO")
        dt.Columns.Add("NUM_FAX")
        dt.Columns.Add("TIPO_INDIRIZZO_SEDE_A")
        dt.Columns.Add("INDIRIZZO_SEDE_A")
        dt.Columns.Add("CIVICO_SEDE_A")
        dt.Columns.Add("CAP_SEDE_A")
        dt.Columns.Add("COMUNE_SEDE_A")
        dt.Columns.Add("PR_SEDE_A")
        dt.Columns.Add("NUM_TELEFONO_SEDE_A")
        dt.Columns.Add("NUM_FAX_SEDE_A")
        dt.Columns.Add("TIPO_R")
        dt.Columns.Add("COGNOME")
        dt.Columns.Add("NOME")
        dt.Columns.Add("COD_FISCALE_R")
        dt.Columns.Add("NUM_PROCURA")
        dt.Columns.Add("DATA PROCURA")
        dt.Columns.Add("RIFERIMENTI")

        Dim RIGA As System.Data.DataRow

        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        While myReader.Read()
            RIGA = dt.NewRow()
            RIGA.Item("ID") = par.IfNull(myReader("ID"), "")
            RIGA.Item("TIPO") = par.IfNull(myReader("TIPO"), "")
            RIGA.Item("COGNOME") = par.IfNull(myReader("COGNOME"), "")
            RIGA.Item("NOME") = par.IfNull(myReader("NOME"), "")
            RIGA.Item("COD_FISCALE_R") = par.IfNull(myReader("COD_FISCALE_R"), "")
            RIGA.Item("COD_FISCALE") = par.IfNull(myReader("COD_FISCALE"), "")
            RIGA.Item("COMUNE_RESIDENZA") = par.IfNull(myReader("COMUNE_RESIDENZA"), "")
            RIGA.Item("COMUNE_SEDE_A") = par.IfNull(myReader("COMUNE_SEDE_A"), "")
            'RIGA.Item("COMUNE_RESIDENZA_R") = par.IfNull(myReader("COMUNE_RESIDENZA_R"), "")
            RIGA.Item("PR_RESIDENZA") = par.IfNull(myReader("PR_RESIDENZA"), "")
            RIGA.Item("PR_SEDE_A") = par.IfNull(myReader("PR_SEDE_A"), "")
            'RIGA.Item("PR_RESIDENZA_R") = par.IfNull(myReader("PR_RESIDENZA_R"), "")
            RIGA.Item("INDIRIZZO_RESIDENZA") = par.IfNull(myReader("TIPO_INDIRIZZO_RESIDENZA"), "") & " " & par.IfNull(myReader("INDIRIZZO_RESIDENZA"), "") & " " & par.IfNull(myReader("CIVICO_RESIDENZA"), "")
            RIGA.Item("INDIRIZZO_SEDE_A") = par.IfNull(myReader("TIPO_INDIRIZZO_SEDE_A"), "") & " " & par.IfNull(myReader("INDIRIZZO_SEDE_A"), "") & " " & par.IfNull(myReader("CIVICO_SEDE_A"), "")
            'RIGA.Item("INDIRIZZO_RESIDENZA_R") = par.IfNull(myReader("INDIRIZZO_RESIDENZA_R"), "")
            RIGA.Item("CIVICO_RESIDENZA") = par.IfNull(myReader("CIVICO_RESIDENZA"), "")
            RIGA.Item("CIVICO_SEDE_A") = par.IfNull(myReader("CIVICO_SEDE_A"), "")
            'RIGA.Item("CIVICO_RESIDENZA_R") = par.IfNull(myReader("CIVICO_RESIDENZA_R"), "")
            RIGA.Item("NUM_FAX") = par.IfNull(myReader("NUM_FAX"), "")
            RIGA.Item("NUM_FAX_SEDE_A") = par.IfNull(myReader("NUM_FAX_SEDE_A"), "")
            RIGA.Item("CAP_RESIDENZA") = par.IfNull(myReader("CAP_RESIDENZA"), "")
            RIGA.Item("CAP_SEDE_A") = par.IfNull(myReader("CAP_SEDE_A"), "")
            'RIGA.Item("CAP_RESIDENZA_R") = par.IfNull(myReader("CAP_RESIDENZA_R"), "")
            RIGA.Item("IBAN") = par.IfNull(myReader("IBAN"), "")
            RIGA.Item("NUM_TELEFONO") = par.IfNull(myReader("NUM_TELEFONO"), "")
            RIGA.Item("NUM_TELEFONO_SEDE_A") = par.IfNull(myReader("NUM_TELEFONO_SEDE_A"), "")
            'RIGA.Item("TELEFONO_R") = par.IfNull(myReader("TELEFONO_R"), "")
            RIGA.Item("NUM_PROCURA") = par.IfNull(myReader("NUM_PROCURA"), "")
            RIGA.Item("DATA PROCURA") = (par.IfNull(myReader("DATA PROCURA"), ""))
            RIGA.Item("RAGIONE_SOCIALE") = par.IfNull(myReader("RAGIONE_SOCIALE"), "")
            RIGA.Item("PARTITA_IVA") = par.IfNull(myReader("PARTITA_IVA"), "")
            RIGA.Item("RIFERIMENTI") = par.IfNull(myReader("RIFERIMENTI"), "")
            RIGA.Item("TIPO_INDIRIZZO_RESIDENZA") = par.IfNull(myReader("TIPO_INDIRIZZO_RESIDENZA"), "")
            RIGA.Item("TIPO_INDIRIZZO_SEDE_A") = par.IfNull(myReader("TIPO_INDIRIZZO_SEDE_A"), "")
            'RIGA.Item("TIPO_INDIRIZZO_RESIDENZA_R") = par.IfNull(myReader("TIPO_INDIRIZZO_RESIDENZA_R"), "")
            RIGA.Item("TIPO_R") = par.IfNull(myReader("TIPO_R"), "")
            dt.Rows.Add(RIGA)
        End While
        cmd.Dispose()
        myReader.Close()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Session.Add("MIADT", dt)

        BindGrid()
    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()

        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        'Dim ds As New Data.DataSet()

        'da.Fill(ds, "SISCOM_MI.FORNITORI")
        dt = Session.Item("MIADT")
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()

        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click

        f = Request.QueryString("F")
        g = Request.QueryString("G")

        If txtid.Text = "" Then
            Response.Write("<script>alert('Non hai selezionato alcuna riga!')</script>")
            Exit Sub
        Else
            Session.Add("ID", txtid.Text)

            If Me.txtragsociale.Text <> "" Then
                ragsociale = UCase(Request.QueryString("RA"))
                piva = UCase(Request.QueryString("PI"))
                Response.Redirect("FornitoreG.aspx?F=" & f & "&G=" & g & "&RA=" & par.VaroleDaPassare(ragsociale) & "&PI=" & par.VaroleDaPassare(piva))
            Else
                nome = UCase(Request.QueryString("NO"))
                cognome = UCase(Request.QueryString("CO"))
                cf = UCase(Request.QueryString("CF"))
                Response.Redirect("FornitoreF.aspx?F=" & f & "&G=" & g & "&CF=" & cf & "&CO=" & par.VaroleDaPassare(cognome) & "&NO=" & par.VaroleDaPassare(nome))
            End If
        End If
    End Sub


    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            If e.Item.Cells(2).Text <> "&nbsp;" Then
                e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il fornitore: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtragsociale').value='" & e.Item.Cells(2).Text & "'")
            Else
                e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il fornitore: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " " & Replace(e.Item.Cells(4).Text, "", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtragsociale').value=''")
            End If
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            If e.Item.Cells(2).Text <> "&nbsp;" Then
                e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il fornitore: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtragsociale').value='" & e.Item.Cells(2).Text & "'")
            Else
                e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il fornitore: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " " & Replace(e.Item.Cells(4).Text, "", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtragsociale').value=''")
            End If
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("Export\" & sNomeFile & ".xls"))
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "RAGIONE SOCIALE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PARTITA IVA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "CODICE FISCALE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "IBAN", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "TIPO INDIRIZZO RESIDENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO RESIDENZA", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "CIVICO RESIDENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CAP RESIDENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE RESIDENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "PROVINCIA RESIDENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "NUM. TELEFONO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "NUM. FAX", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "TIPO INDIRIZZO SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "INDIRIZZO SEDE AMM.VA", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "CIVICO SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "CAP SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "COMUNE SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "PROVINCIA SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "NUM. TEL. SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "NUM FAX SEDE AMM.VA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "TIPO RAPP.TE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COGNOME", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "NOME", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "CODICE FISCALE RAPP.TE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "NUM. PROCURA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "DATA PROCURA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "RIFERIMENTI", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RAGIONE_SOCIALE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PARTITA_IVA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_FISCALE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IBAN"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_INDIRIZZO_RESIDENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO_RESIDENZA"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_RESIDENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP_RESIDENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COMUNE_RESIDENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PR_RESIDENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_TELEFONO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_FAX"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_INDIRIZZO_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO_SEDE_A"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COMUNE_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PR_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_TELEFONO_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_FAX_SEDE_A"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_R"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COGNOME"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_FISCALE_R"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_PROCURA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA PROCURA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RIFERIMENTI"), 0)))


                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("Export\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("Export\" & sNomeFile & ".xls")
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
            Response.Redirect("Export\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaFornitori.aspx""</script>")
    End Sub
End Class
