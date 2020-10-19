Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_RisultatoContratti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValoreRS As String
    Dim sValoreCO As String
    Dim sValoreTC As String
    Dim sValoreTI As String
    Dim sValoreUN As String
    Dim sValoreST As String

    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sValoreRDAL As String
    Dim sValoreRAL As String

    Dim sValoreDDAL As String
    Dim sValoreDAL As String

    Dim sValoreFDAL As String
    Dim sValoreFAL As String
    Dim sValorePIVA As String
    Dim sValoreGIMI As String

    Dim sValoreRECDA As String
    Dim sValoreRECA As String
    Dim sValoreSLODA As String
    Dim sValoreSLOA As String
    Dim sValoreINSDA As String
    Dim sValoreINSA As String

    Dim sValoreSLOV As String


    Dim sValoreVIRT As String
    Dim sValoreTIPO As String
    Dim sValorePROVEN As String
    Dim sValoreORIGINE As String
    Dim sValoreANNI As String
    Dim sValoreRINN As String

    Dim sValoreDECAD As String
    Dim sValoreCTRANAG As String

    Dim sValoreINTEST As String
    Dim sValoreEXG As String
    Dim sStringaSql As String
    Dim scriptblock As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()



            sValoreCG = UCase(Request.QueryString("CG"))
            sValoreNM = UCase(Request.QueryString("NM"))
            sValoreCF = UCase(Request.QueryString("CF"))
            sValoreRS = UCase(Request.QueryString("RS"))
            sValoreCO = UCase(Request.QueryString("CO"))
            sValoreTI = UCase(Request.QueryString("TI"))
            sValoreTC = UCase(Request.QueryString("TC"))
            sValoreUN = UCase(Request.QueryString("UN"))
            sValoreST = UCase(Request.QueryString("ST"))

            sValoreSDAL = Request.QueryString("SDAL")
            sValoreSAL = Request.QueryString("SAL")

            sValoreRDAL = Request.QueryString("RDAL")
            sValoreRAL = Request.QueryString("RAL")

            sValoreDDAL = Request.QueryString("DDAL")
            sValoreDAL = Request.QueryString("DAL")

            sValoreFDAL = Request.QueryString("FDAL")
            sValoreFAL = Request.QueryString("FAL")
            sValorePIVA = Request.QueryString("PIVA")
            sValoreGIMI = Request.QueryString("GIMI")

            sValoreRECDA = Request.QueryString("RECDA")
            sValoreRECA = Request.QueryString("RECA")
            sValoreSLODA = Request.QueryString("SLODA")
            sValoreSLOA = Request.QueryString("SLOA")

            sValoreINSDA = Request.QueryString("INSDA")
            sValoreINSA = Request.QueryString("INSA")

            sValoreSLOV = Request.QueryString("SLOV")

            '****** 03/11/2011 Variabili per le 3 nuove condizioni (virtuali, tipo specifico, durata) ******
            sValoreVIRT = Request.QueryString("VIRT")
            sValorePROVEN = Request.QueryString("PROV")
            sValoreORIGINE = Request.QueryString("ORIG")
            sValoreTIPO = Request.QueryString("TIPO")
            sValoreANNI = Request.QueryString("DUR")
            sValoreRINN = Request.QueryString("RINN")
            '****** Fine 08/11/2011 Variabili ecc. ******


            sValoreDECAD = Request.QueryString("DECAD")
            sValoreCTRANAG = Request.QueryString("CTRANAG")


            sValoreINTEST = Request.QueryString("INTEST")
            'btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")

            sValoreEXG = Request.QueryString("EXG")
            LBLID.Value = "-1"
            Cerca()
            If Session.Item("LIVELLO") = "1" Or Session.Item("FL_RUEXP") = "1" Then
                btnExport.Visible = True
            End If

        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + Datagrid2.ClientID + "', 350, 776 , 25 ,true); </script>", False)
    End Sub

    Private Sub BindGrid()
        Try





            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA")
            Label4.Text = Datagrid2.Items.Count
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()

            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = sStringaSQL2
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Label4.Text = "(" & Datagrid2.Items.Count & " nella pagina - Totale :" & ds.Tables(0).Rows.Count & ") in " & myReader(0) & " Rapporti"
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message

        End Try
    End Sub


    Private Function ExportXLS_Chiama100()

        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            par.OracleConn.Open()
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile

                    .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
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
                    .SetColumnWidth(1, 1, 30)
                    .SetColumnWidth(2, 2, 20)
                    .SetColumnWidth(3, 3, 30)
                    .SetColumnWidth(4, 4, 30)
                    .SetColumnWidth(5, 5, 15)
                    .SetColumnWidth(6, 6, 45)
                    .SetColumnWidth(7, 7, 20)
                    .SetColumnWidth(8, 8, 45)
                    .SetColumnWidth(9, 9, 20)
                    .SetColumnWidth(10, 10, 25)
                    .SetColumnWidth(11, 11, 20)
                    .SetColumnWidth(12, 12, 25)
                    .SetColumnWidth(13, 13, 20)
                    .SetColumnWidth(14, 14, 20)
                    .SetColumnWidth(15, 15, 20)
                    .SetColumnWidth(16, 16, 55)
                    .SetColumnWidth(17, 17, 60)
                    .SetColumnWidth(18, 18, 30)
                    .SetColumnWidth(19, 19, 20)
                    .SetColumnWidth(20, 20, 35)
                    .SetColumnWidth(21, 21, 20)
                    .SetColumnWidth(22, 22, 25)
                    .SetColumnWidth(23, 23, 20)
                    .SetColumnWidth(24, 24, 20)
                    .SetColumnWidth(25, 25, 20)
                    .SetColumnWidth(26, 26, 20)
                    .SetColumnWidth(27, 27, 20)
                    .SetColumnWidth(28, 28, 20)
                    .SetColumnWidth(29, 29, 20)
                    .SetColumnWidth(30, 30, 20)
                    .SetColumnWidth(31, 31, 20)
                    .SetColumnWidth(32, 32, 20)
                    .SetColumnWidth(33, 33, 20)
                    .SetColumnWidth(34, 34, 20)
                    .SetColumnWidth(35, 35, 20)
                    .SetColumnWidth(36, 36, 20)




                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ID", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD. CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "TIPO CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO CONTR. SPECIF.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "ORIGINE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "STATO CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DURATA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "NOMINATIVO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TIPO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "INTESTATARIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "DATA NASCITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "COD.FISCALE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "PARTITA IVA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "POSIZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "CITTADINANZA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "DATA STIPULA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "DATA DECORRENZA", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "DATA DECORRENZA_AE", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "DATA SCADENZA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "DATA DISDETTA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "DATA SLOGGIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "COD. UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "TIPO UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "INDIRIZZO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 25, "CIVICO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 26, "INTERNO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 27, "PIANO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 28, "SCALA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 29, "COMUNE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 30, "CAP", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 31, "FILIALE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 32, "RECAPITO BOLLETTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 33, "INDIRIZZO BOLLETTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 34, "CIVICO BOLLETTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 35, "CAP BOLLETTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 36, "COMUNE BOLLETTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 37, "PROVINCIA BOLLETTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 38, "PIANO RECAPITO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 39, "INTERNO RECAPITO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 40, "SCALA RECAPITO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 41, "DECRETO DECAD.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 42, "ESITO CONTROLLO ANAG.", 12)

                    'max 23/01/2018
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 43, "E-MAIL", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 44, "E-MAIL PEC", 12)

                    If Session.Item("ID_OPERATORE") = "1" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 45, "VIRTUALE MANUALE", 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 46, "ID", 12)
                    End If

                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ID"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_TIPOLOGIA_CONTR_LOC"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_SPECIFICO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ORIGINE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STATO_DEL_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DURATA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_OCCUPANTE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME_INTEST"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_NASCITA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PARTITA_IVA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CITTADINANZA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_STIPULA"), ""))))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_DECORRENZA"), ""))))

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_DECORRENZA_AE"), ""))))

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_SCADENZA"), ""))))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_DISDETTA_LOCATARIO"), ""))))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.FormattaData(par.IfNull(dt.Rows(i).Item("DATA_RICONSEGNA"), ""))))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CUI"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_TIPOLOGIA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COMUNE_UNITA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 31, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILIALE_ALER"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 32, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PRESSO_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 33, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_COR"), "")) & " " & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("VIA_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 34, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 35, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 36, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("LUOGO_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 37, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SIGLA_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 38, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO_RECAPITO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 39, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO_RECAPITO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 40, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA_RECAPITO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 41, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DECRETO_DECADENZA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 42, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ESITO_CTRL_ANAGRAF"), "")))
                        'max 23/01/2018
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 43, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MAIL_COR"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 44, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MAIL_PEC_COR"), "")))

                        If Session.Item("ID_OPERATORE") = "1" Then
                            If Mid(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), ""), 1, 6) = "000000" And par.IfNull(dt.Rows(i).Item("durata_mesi"), "0") <> "0" Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 45, "X")
                            Else
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 45, "")
                            End If
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 46, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ID"), "")))

                        End If


                        i = i + 1
                        K = K + 1
                    Next

                    .CloseFile()
                End With

            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

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

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            ' Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message
        End Try




    End Function

Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePIVA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePIVA
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.PARTITA_IVA " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreUN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreUN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
        End If

        If sValoreST <> "TUTTI" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreRS <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRS
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.RAGIONE_SOCIALE" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If



        If sValoreSDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreSAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreDDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DECORRENZA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DECORRENZA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA<='" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreRDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRDAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA_RINNOVO>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreRAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRAL
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA_RINNOVO<='" & par.PulisciStrSql(sValore) & "' "
        End If





        If sValoreCO <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreCO
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTC <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreTC
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC ='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTI <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreTI
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_CONTRATTUALE.TIPOLOGIA ='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreGIMI <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreGIMI

            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO_GIMI ='" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreRECDA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRECDA
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreRECA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRECA
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO<='" & par.PulisciStrSql(sValore) & "' "
        End If


        'INSERIMENTO

        If sValoreINSDA <> "" Or sValoreINSA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql = sStringaSql & "  RAPPORTI_UTENZA.ID IN (SELECT ID_CONTRATTO FROM SISCOM_MI.EVENTI_CONTRATTI WHERE EVENTI_CONTRATTI.COD_EVENTO='F01' "
            bTrovato = True

            If sValoreINSDA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreINSDA
                bTrovato = True
                sStringaSql = sStringaSql & " SUBSTR(EVENTI_CONTRATTI.DATA_ORA,1,8)>='" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreINSA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreINSA
                bTrovato = True
                sStringaSql = sStringaSql & " SUBSTR(EVENTI_CONTRATTI.DATA_ORA,1,8)<='" & par.PulisciStrSql(sValore) & "' "
            End If
            sStringaSql = sStringaSql & ")"
        End If


        If sValoreSLOV <> "1" Then
            If sValoreSLODA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreSLODA
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA>='" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreSLOA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValoreSLOA
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA<='" & par.PulisciStrSql(sValore) & "' "
            End If
        Else
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            bTrovato = True
            sStringaSql = sStringaSql & " (RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL OR RAPPORTI_UTENZA.DATA_RICONSEGNA='') "

        End If

        '******* CheckBox (virtuale) - MTeresa 02/11/2011 *******
        If sValoreVIRT = "1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            bTrovato = True
            sStringaSql = sStringaSql & " (SUBSTR((RAPPORTI_UTENZA.COD_CONTRATTO),0,6) = '000000' OR SUBSTR((RAPPORTI_UTENZA.COD_CONTRATTO),0,2) = '41' OR SUBSTR((RAPPORTI_UTENZA.COD_CONTRATTO),0,2) = '42' OR SUBSTR((RAPPORTI_UTENZA.COD_CONTRATTO),0,2) = '43')"
        End If
        '******* fine CheckBox (virtuale) - MTeresa 02/11/2011 *******


        '******* ComboBox (TipoSpecifico) - MTeresa 03/11/2011 *******
        If sValoreTIPO = "ERP" Then
            If sValorePROVEN <> "0" And sValorePROVEN <> "2" Then 'Canone convenz., Art.22, Forze dell'ordine
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValorePROVEN
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.PROVENIENZA_ASS = " & sValorePROVEN & " "
            End If

            If sValorePROVEN = "2" Then 'Erp Moderato
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValorePROVEN
                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = " & sValorePROVEN & " "
            End If


        End If
        If sValoreORIGINE <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreORIGINE
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.ID_ORIGINE_CONTRATTO = " & sValoreORIGINE & " "
        End If
        If sValoreTIPO = "L43198" Then
            If sValorePROVEN <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValorePROVEN
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DEST_USO = '" & sValorePROVEN & "' "
            End If
        End If

        '******* fine ComboBox (TipoSpecifico) - MTeresa 08/11/2011 *******



        '******* TextBox (Durata) - MTeresa 07/11/2011 *******
        If sValoreANNI <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreANNI
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.RAPPORTI_UTENZA.DURATA_ANNI='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreRINN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRINN
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.RAPPORTI_UTENZA.DURATA_RINNOVO='" & par.PulisciStrSql(sValore) & "' "
        End If
        '******* fine TextBox (Durata) - MTeresa 07/11/2011 *******


        If sValoreDECAD <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDECAD
            bTrovato = True
            sStringaSql = sStringaSql & " rapporti_utenza.id in (select id_contratto from siscom_mi.rapporti_utenza_controllo where morosita_pregr=" & sValore & ") "
        End If
        If sValoreCTRANAG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCTRANAG
            bTrovato = True
            sStringaSql = sStringaSql & " rapporti_utenza.id in (select id_contratto from siscom_mi.rapporti_utenza_controllo where controllo_anagra_esito=" & sValore & ") "
        End If



        'If sValoreCG = "" And sValoreNM = "" And sValoreRS = "" Then
        '    sStringaSql = sStringaSql & " AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') "
        'End If

        If sValoreINTEST = 1 Then
            sStringaSql = sStringaSql & " AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') "
        End If

        If sValoreEXG = "1" Then
            sStringaSql = sStringaSql & " AND rapporti_utenza.id in (select distinct id_contratto from siscom_mi.documenti_ex_gestori) "
        End If



       

        'window.open('ElencoAllegati.aspx?COD=<%=CodContratto1 %>', 'Allegati', '');

        sStringaSQL1 = "SELECT DISTINCT(RAPPORTI_UTENZA.COD_CONTRATTO) AS CODCONTR,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS COD_TIPOLOGIA_CONTR_LOC,(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' " _
                     & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='V' THEN '431/98 ART.15 C.2 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
                     & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO,(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_ORIGINE_CONTRATTO WHERE ID=ID_ORIGINE_CONTRATTO) AS ORIGINE, (CASE WHEN RAPPORTI_UTENZA.DURATA_ANNI IS NULL AND RAPPORTI_UTENZA.DURATA_RINNOVO IS NULL THEN RAPPORTI_UTENZA.DURATA_ANNI||''||RAPPORTI_UTENZA.DURATA_RINNOVO ELSE " _
                     & "RAPPORTI_UTENZA.DURATA_ANNI||'+'||RAPPORTI_UTENZA.DURATA_RINNOVO END) AS DURATA,TAB_FILIALI.NOME AS FILIALE_ALER,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,INDIRIZZI.CAP,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as CUI," _
                     & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE, " _
                     & " (select (case when morosita_pregr = 1 then 'Eseguito' when morosita_pregr= 0 then 'Non Eseguito' when morosita_pregr is null then '' end) from siscom_mi.rapporti_utenza_controllo where id_contratto(+)=rapporti_utenza.id) as decreto_decadenza,  " _
                     & " (select (case when controllo_anagra_esito = 1 then 'Titolare mononucleo deced.' when controllo_anagra_esito= 0 then 'Vuoto' when controllo_anagra_esito is null then '' end)  from siscom_mi.rapporti_utenza_controllo where id_contratto(+)=rapporti_utenza.id) as esito_ctrl_anagraf,  " _
                     & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''ElencoAllegati.aspx?COD='||rapporti_utenza.cod_contratto||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') as ALLEGATI_CONTRATTO, " _
                     & "RAPPORTI_UTENZA.*,nvl(ANAGRAFICA.CITTADINANZA,'') as cittadinanza,siscom_mi.getstatocontratto(rapporti_utenza.id) as STATO_DEL_CONTRATTO, SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS NOME_INTEST," _
                     & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"" ," _
                     & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ,TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NASCITA,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA," _
                     & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) AS ""POSIZIONE_CONTRATTO"",TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCCUPANTE,unita_immobiliari.interno,scale_edifici.descrizione AS scala,tipo_livello_piano.descrizione AS piano " _
                     & " ,RAPPORTI_UTENZA.SCALA_COR AS SCALA_RECAPITO,RAPPORTI_UTENZA.PIANO_COR AS PIANO_RECAPITO,INTERNO_COR AS INTERNO_RECAPITO " _
                     & "FROM " _
                     & "siscom_mi.scale_edifici,siscom_mi.tipo_livello_piano,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
                     & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                     & "WHERE " _
                     & "TIPOLOGIA_CONTRATTO_LOCAZIONE.COD(+)=RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AND scale_edifici.ID(+)=unita_immobiliari.id_scala AND tipo_livello_piano.cod(+)=unita_immobiliari.cod_tipo_livello_piano AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+) AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
                     & " AND EDIFICI.ID (+) =UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID (+) =UNITA_CONTRATTUALE.ID_UNITA  AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                     & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND TIPOLOGIA_OCCUPANTE.COD = SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
        '& "AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') AND RAPPORTI_UTENZA.ID IN " _
        '& "(SELECT id_contratto FROM siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica WHERE soggetti_contrattuali.id_anagrafica=siscom_mi.anagrafica.ID "
        '02/11/2011 Condizione (eliminata) per estrarre solo gli intestatari: (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT')


        If sStringaSql <> "" Then
            If Left(sStringaSql, 4) = " AND" Then
                sStringaSql = Replace(sStringaSql, "AND", " ")
            End If
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        If Session.Item("INDIRIZZI") <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND (" & Session.Item("INDIRIZZI") & ") "
            'Session.Item("INDIRIZZI") = ""
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY ""INTESTATARIO"" ASC"


        sStringaSQL2 = "SELECT COUNT(distinct rapporti_utenza.cod_contratto) FROM SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA," _
             & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
             & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
             & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "

        If sStringaSql <> "" Then
            sStringaSQL2 = sStringaSQL2 & " AND " & Replace(sStringaSql, " AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') ", "")
        End If

        If Session.Item("INDIRIZZI") <> "" Then
            sStringaSQL2 = sStringaSQL2 & " AND (" & Session.Item("INDIRIZZI") & ") "
            Session.Item("INDIRIZZI") = ""
        End If



        BindGrid()

    End Function

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set

    End Property

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

    'Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
    '    If LBLID.Value <> "-1" And LBLID.Value <> "" Then
    '        Response.Write("<script>popupWindow=window.open('Contratto.aspx?ID=" & LBLID.Value & "&COD=" & Label3.Value & "','Contratto" & Format(Now, "hhss") & "','height=700,width=900');popupWindow.focus();</script>")
    '    Else
    '        Response.Write("<script>alert('Selezionare un contratto dalla lista!');</script>")
    '    End If
    'End Sub

    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "'")
            e.Item.Attributes.Add("onDblclick", "ApriContratto();")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If

    End Sub

    Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If H1.Value = "1" Then
            ExportXLS_Chiama100()
            H1.Value = "0"
        End If
    End Sub

End Class
