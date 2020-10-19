'*** LISTA RISULTATO MOROSITA REPORT Proviene da : RicercaReport.aspx

Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class MOROSITA_RisultatiReport
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If


        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then

            Response.Flush()

            Cerca()
            Elabora()

        End If

    End Sub


    Public Property sStringaSQL() As String
        Get
            If Not (ViewState("par_sStringaSQL") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL") = value
        End Set

    End Property

    Private Sub Cerca()
        Dim sStringaSQL1 As String = ""
        Dim sValore As String = ""
        Dim sCompara As String = ""


        Dim sValoreProtocollo As String = ""

        Dim sValoreData_Dal As String = ""
        Dim sValoreData_Al As String = ""


        sValoreProtocollo = Request.QueryString("PRO")
        sValoreData_Dal = Request.QueryString("DAL")
        sValoreData_Al = Request.QueryString("AL")



        '1) select MOROSITA per PROTOCOLLO_ALER e/o DATA_PROTOCOLLO o niente
        sStringaSQL = "select * from  SISCOM_MI.MOROSITA "

        '*** PROTOCOLLO 
        If sValoreProtocollo <> "" Then
            sValore = Strings.UCase(sValoreProtocollo)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  UPPER(PROTOCOLLO_ALER) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************

        '*** DATA_PROTOCOLLO 
        If sValoreData_Dal <> "" Then
            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "

            sStringaSQL1 = sStringaSQL1 & " MOROSITA.DATA_PROTOCOLLO>='" & sValoreData_Dal & "' "
        End If

        If sValoreData_Al <> "" Then
            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "

            sStringaSQL1 = sStringaSQL1 & " MOROSITA.DATA_PROTOCOLLO<='" & sValoreData_Al & "' "
        End If
        '********************************

        If sStringaSQL1 <> "" Then sStringaSQL1 = " where " & sStringaSQL1
        sStringaSQL = sStringaSQL & sStringaSQL1


    End Sub


    Sub Elabora()
        Dim FlagConnessione As Boolean

        Dim TestoPagina As String = ""
        Dim myExcelFile As New CM.ExcelFile

        Dim sNomeFile As String
        Dim NomeFilePDF As String

        Dim sValore As String = ""
        Dim sCompara As String = ""

        Dim sID_Morosita As String = ""

        '*** TOTALI *************************************
        Dim Tot_Inquilini As Long = 0
        Dim Tot_MG As Long = 0
        Dim Tot_MA As Long = 0

        Dim Tot_MG_EURO As Decimal = 0
        Dim Tot_MA_EURO As Decimal = 0

        Dim Tot_MG_Pagati As Long = 0
        Dim Tot_MA_Pagati As Long = 0

        Dim Tot_MG_NO_Pagati As Long = 0
        Dim Tot_MA_NO_Pagati As Long = 0

        Dim Tot_MG_EURO_Riga As Decimal = 0
        Dim Tot_MA_EURO_Riga As Decimal = 0

        Dim Tot_MG_Pagati_Riga As Long = 0
        Dim Tot_MA_Pagati_Riga As Long = 0

        Dim Tot_MG_NO_Pagati_Riga As Long = 0
        Dim Tot_MA_NO_Pagati_riga As Long = 0
        '***********************

        Dim dVal As Decimal = 0
        Dim lVal As Long = 0
        Dim i, K As Long


        Dim dt As New Data.DataTable

        Try


            Me.txtNomeFile.Value = Format(Now, "yyyyMMddHHmmss")
            sNomeFile = "Export_" & Me.txtNomeFile.Value


            dt.Columns.Add("ID")
            dt.Columns.Add("NUM_PROTOCOLLO")
            dt.Columns.Add("DATA_PROTOCOLLO")
            dt.Columns.Add("NUM_INQUILINI")
            dt.Columns.Add("MG_EMESSI")
            dt.Columns.Add("MA_EMESSI")
            dt.Columns.Add("IMPORTO_MG")
            dt.Columns.Add("IMPORTO_MA")
            dt.Columns.Add("MAV_MG_Pagati")
            dt.Columns.Add("MAV_MA_Pagati")
            dt.Columns.Add("MAV_MG_Non_Pagati")
            dt.Columns.Add("MAV_MA_Non_Pagati")

            Dim row As System.Data.DataRow


            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NUM. PROTOCOLLO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA PROTOCOLLO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "NUM. INQUILINI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "MG EMESSI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "MA EMESSI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "IMPORTO MG", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "IMPORTO MA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MAV MG Pagati", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "MAV MA Pagati", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "MAV MG Non Pagati", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "MAV MA Non Pagati", 12)

                .SetColumnWidth(1, 2, 20)
                .SetColumnWidth(3, 11, 15)

                K = 2

                'PDF ***********************
                NomeFilePDF = "ReportMorosita_" & Me.txtNomeFile.Value
                TestoPagina = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>SITUAZIONE MOROSITA: </p></br>"

                TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                          & "<td align='left'  style='border-bottom-style: dashed; width:14%;  border-bottom-width: 1px; border-bottom-color: #000000'>NUM. PROTOCOLLO</td>" _
                                          & "<td align='left'  style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>DATA PROTOCOLLO</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>NUM. INQUILINI</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>MG EMESSI</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>MA EMESSI</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:12%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO MG</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:12%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO MA</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MG Pagati</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MA Pagati</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MG Non Pagati</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MA Non Pagati</td>" _
                                          & "</tr>"
                '*************************************************

                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    FlagConnessione = True
                End If


                par.cmd.CommandText = sStringaSQL
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read()

                    row = dt.NewRow()
                    row.Item("ID") = par.IfNull(myReader1("ID"), -1)
                    row.Item("NUM_PROTOCOLLO") = par.IfNull(myReader1("PROTOCOLLO_ALER"), "")
                    'row.Item("NUM_PROTOCOLLO") = "<a href=£javascript:void(0)£ onclick=£window.open('Morosita.aspx?ID='" & par.IfNull(myReader1("ID"), -1) & "','Dettagli','height=580,top=0,left=0,width=780');£>'" & par.IfNull(myReader1("PROTOCOLLO_ALER"), "") & "'</a>"
                    'row.Item("NUM_PROTOCOLLO") = Replace(Replace("<a href=£javascript:void(0)£ onclick=£location.replace('Morosita.aspx?ID=" & par.IfNull(myReader1("ID"), -1) & "';£>Dettagli</a>", "$", "&"), "£", Chr(34))

                    row.Item("DATA_PROTOCOLLO") = par.FormattaData(par.IfNull(myReader1("DATA_PROTOCOLLO"), ""))


                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(myReader1("PROTOCOLLO_ALER"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.FormattaData(par.IfNull(myReader1("DATA_PROTOCOLLO"), "")))


                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("PROTOCOLLO_ALER"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReader1("DATA_PROTOCOLLO"), "")) & "</td>"



                    '*** conoscere il numero di inquilini morosi
                    '2) Loop per Select Case DISTINCT(ID_ANAGRAFICA) from MOROSITA_LETTERE WHERE ID_MOROSITA=XXX
                    i = 0
                    par.cmd.CommandText = "select DISTINCT(ID_ANAGRAFICA) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1)
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader2.Read()
                        i = i + 1
                    Loop
                    myReader2.Close()

                    row.Item("NUM_INQUILINI") = i
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, i)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & i & "</td>"


                    Tot_Inquilini = Tot_Inquilini + i
                    '*******************************************************


                    'MOROSITA_LETTERE ****************************************************************************
                    '*** Numero di MAV MG emessi 
                    '3) select count(*) from MOROSITA_LETTERE where ID_MOROSITA=XXX and INIZIO_PERIODO<>20091001
                    lVal = 0
                    par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and INIZIO_PERIODO<>20091001 and INIZIO_PERIODO<20091001 "
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        lVal = par.IfNull(myReader2(0), 0)
                        Tot_MG = Tot_MG + par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()

                    row.Item("MG_EMESSI") = lVal
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, lVal)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"
                    '*******************************************************


                    '*** Numero di MAV MA emessi 
                    '4) select count(*) from MOROSITA_LETTERE where ID_MOROSITA=XXX and INIZIO_PERIODO=20091001
                    lVal = 0
                    i = 0
                    par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and INIZIO_PERIODO>=20091001 "
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        lVal = par.IfNull(myReader2(0), 0)
                        Tot_MA = Tot_MA + par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()

                    row.Item("MA_EMESSI") = lVal
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, lVal)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"
                    'FINE MOROSITA_LETTERE  *******************************************************


                    'BOL_BOLLETTE *************************************************************
                    '*** corrispondente valore della morosità richiesta MG 
                    '5)  select SUM(NVL(IMPORTO_TOTALE,0)) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MG'
                    dVal = 0

                    Tot_MG_EURO_Riga = 0
                    Tot_MA_EURO_Riga = 0

                    Tot_MG_Pagati_Riga = 0
                    Tot_MA_Pagati_Riga = 0

                    Tot_MG_NO_Pagati_Riga = 0
                    Tot_MA_NO_Pagati_riga = 0

                    par.cmd.CommandText = "select NVL(IMPORTO_TOTALE,0) as IMPORTO_TOTALE,NVL(IMPORTO_PAGATO,0) as IMPORTO_PAGATO,TRIM(NOTE) as NOTE " _
                                       & " from SISCOM_MI.BOL_BOLLETTE " _
                                       & " where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) _
                                       & " and (NOTE='MOROSITA'' MG' or NOTE='MOROSITA'' MA') " _
                                       & " and FL_ANNULLATA=0"

                    myReader2 = par.cmd.ExecuteReader()
                    Do While myReader2.Read()

                        ''*** corrispondente valore della morosità richiesta MA 
                        ''6)  select SUM(NVL(IMPORTO_TOTALE,0)) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA'
                        'par.cmd.CommandText = "select SUM(NVL(IMPORTO_TOTALE,0)) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' "
                        If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MG" Then
                            Tot_MG_EURO_Riga = Tot_MG_EURO_Riga + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
                            Tot_MG_EURO = Tot_MG_EURO + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
                        End If

                        ''*** corrispondente valore della morosità richiesta MA 
                        ''6)  select SUM(NVL(IMPORTO_TOTALE,0)) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA'
                        'par.cmd.CommandText = "select SUM(NVL(IMPORTO_TOTALE,0)) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' "
                        If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MA" Then
                            Tot_MA_EURO_Riga = Tot_MA_EURO_Riga + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
                            Tot_MA_EURO = Tot_MA_EURO + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
                        End If


                        '*** quanti MAV MG sono stati pagati
                        '7) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0
                        'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MG' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0 "
                        If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MG" And (par.IfNull(myReader2("IMPORTO_TOTALE"), 0) - par.IfNull(myReader2("IMPORTO_PAGATO"), 0)) = 0 Then
                            Tot_MG_Pagati_Riga = Tot_MG_Pagati_Riga + 1
                            Tot_MG_Pagati = Tot_MG_Pagati + 1
                        End If

                        '*** quanti MAV MG sono stati pagati
                        '7) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0
                        'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0 "
                        If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MA" And (par.IfNull(myReader2("IMPORTO_TOTALE"), 0) - par.IfNull(myReader2("IMPORTO_PAGATO"), 0)) = 0 Then
                            Tot_MA_Pagati_Riga = Tot_MA_Pagati_Riga + 1
                            Tot_MA_Pagati = Tot_MA_Pagati + 1
                        End If

                        '*** quanti MAV MG non sono stati pagati
                        '8) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA' and NVL(IMPORTO_PAGATO,0)=0
                        'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MG' and NVL(IMPORTO_PAGATO,0)=0 "
                        If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MG" And par.IfNull(myReader2("IMPORTO_PAGATO"), 0) = 0 Then
                            Tot_MG_NO_Pagati_Riga = Tot_MG_NO_Pagati_Riga + 1
                            Tot_MG_NO_Pagati = Tot_MG_NO_Pagati + 1
                        End If

                        '*** quanti MAV MA non sono stati pagati
                        '8) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA'  and NVL(IMPORTO_PAGATO,0)=0
                        'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' and NVL(IMPORTO_PAGATO,0)=0 "
                        If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MA" And par.IfNull(myReader2("IMPORTO_PAGATO"), 0) = 0 Then
                            Tot_MA_NO_Pagati_riga = Tot_MA_NO_Pagati_riga + 1
                            Tot_MA_NO_Pagati = Tot_MA_NO_Pagati + 1
                        End If
                    Loop
                    myReader2.Close()


                    row.Item("IMPORTO_MG") = par.IfEmpty(Format(Tot_MG_EURO_Riga, "##,##0.00"), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfEmpty(Format(Tot_MG_EURO_Riga, "##,##0.00"), 0))
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfEmpty(Format(Tot_MG_EURO_Riga, "##,##0.00"), 0) & "</td>"

                    row.Item("IMPORTO_MA") = par.IfEmpty(Format(Tot_MA_EURO_Riga, "##,##0.00"), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfEmpty(Format(Tot_MA_EURO_Riga, "##,##0.00"), 0))
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfEmpty(Format(Tot_MA_EURO_Riga, "##,##0.00"), 0) & "</td>"

                    row.Item("MAV_MG_Pagati") = Tot_MG_Pagati_Riga
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, Tot_MG_Pagati_Riga)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MG_Pagati_Riga & "</td>"

                    row.Item("MAV_MA_Pagati") = Tot_MA_Pagati_Riga
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, Tot_MA_Pagati_Riga)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MA_Pagati_Riga & "</td>"

                    row.Item("MAV_MG_Non_Pagati") = Tot_MG_NO_Pagati_Riga
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, Tot_MG_NO_Pagati_Riga)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MG_NO_Pagati_Riga & "</td>"

                    row.Item("MAV_MA_Non_Pagati") = Tot_MA_NO_Pagati_riga
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, Tot_MA_NO_Pagati_riga)
                    TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MA_NO_Pagati_riga & "</td>"
                    ''*******************************************************


                    TestoPagina = TestoPagina & "</tr>"

                    K = K + 1
                    dt.Rows.Add(row)

                Loop
                myReader1.Close()


                If K = 2 Then
                    .CloseFile()

                    File.Delete(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))



                    row = dt.NewRow()
                    row.Item("ID") = "-1"
                    row.Item("NUM_PROTOCOLLO") = "TOTALE: "
                    row.Item("DATA_PROTOCOLLO") = " "

                    row.Item("NUM_INQUILINI") = "0"
                    row.Item("MG_EMESSI") = "0"
                    row.Item("MA_EMESSI") = "0"

                    row.Item("IMPORTO_MG") = "0"
                    row.Item("IMPORTO_MA") = "0"

                    row.Item("MAV_MG_Pagati") = "0"
                    row.Item("MAV_MA_Pagati") = "0"

                    row.Item("MAV_MG_Non_Pagati") = "0"
                    row.Item("MAV_MA_Non_Pagati") = "0"
                    dt.Rows.Add(row)

                    DataGrid1.DataSource = dt
                    DataGrid1.DataBind()

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                    Me.txtNomeFile.Value = ""
                    Exit Sub
                End If

                'TOTALI 
                K = K + 1

                row = dt.NewRow()
                row.Item("ID") = "-1"
                row.Item("NUM_PROTOCOLLO") = "TOTALE: "
                row.Item("DATA_PROTOCOLLO") = " "


                row.Item("NUM_INQUILINI") = par.IfEmpty(Format(Tot_Inquilini, "##,##"), 0)
                row.Item("MG_EMESSI") = par.IfEmpty(Format(Tot_MG, "##,##"), 0)
                row.Item("MA_EMESSI") = par.IfEmpty(Format(Tot_MA, "##,##"), 0)

                row.Item("IMPORTO_MG") = par.IfEmpty(Format(Tot_MG_EURO, "##,##0.00"), 0)
                row.Item("IMPORTO_MA") = par.IfEmpty(Format(Tot_MA_EURO, "##,##0.00"), 0)

                row.Item("MAV_MG_Pagati") = par.IfEmpty(Format(Tot_MG_Pagati, "##,##"), 0)
                row.Item("MAV_MA_Pagati") = par.IfEmpty(Format(Tot_MA_Pagati, "##,##"), 0)

                row.Item("MAV_MG_Non_Pagati") = par.IfEmpty(Format(Tot_MG_NO_Pagati, "##,##"), 0)
                row.Item("MAV_MA_Non_Pagati") = par.IfEmpty(Format(Tot_MA_NO_Pagati, "##,##"), 0)
                dt.Rows.Add(row)

                DataGrid1.DataSource = dt
                DataGrid1.DataBind()


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "TOTALE: ")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ")

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfEmpty(Format(Tot_Inquilini, "##,##"), 0))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfEmpty(Format(Tot_MG, "##,##"), 0))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfEmpty(Format(Tot_MA, "##,##"), 0))

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfEmpty(Format(Tot_MG_EURO, "##,##0.00"), 0))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfEmpty(Format(Tot_MA_EURO, "##,##0.00"), 0))

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfEmpty(Format(Tot_MG_Pagati, "##,##"), 0))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfEmpty(Format(Tot_MA_Pagati, "##,##"), 0))

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfEmpty(Format(Tot_MG_NO_Pagati, "##,##"), 0))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfEmpty(Format(Tot_MA_NO_Pagati, "##,##"), 0))


                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                     & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE: </td>" _
                                                     & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_Inquilini, "##,##"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MG, "##,##"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MA, "##,##"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MG_EURO, "##,##0.00"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MA_EURO, "##,##0.00"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MG_Pagati, "##,##"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MA_Pagati, "##,##"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MG_NO_Pagati, "##,##"), 0) & "</td>" _
                                                     & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfEmpty(Format(Tot_MA_NO_Pagati, "##,##"), 0) & "</td>" _
                                                     & "</tr>"

                '******************************

                .CloseFile()
            End With


            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

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

            '*** PDF

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(TestoPagina)
            sr.Close()

            Dim url As String = NomeFilePDF
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

            Dim pdfConverter As PdfConverter = New PdfConverter

            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfDocumentOptions.ShowFooter = False
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False


            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm", Server.MapPath("..\FileTemp\") & NomeFilePDF & ".pdf")
            IO.File.Delete(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm")
            'Response.Redirect("..\FileTemp\" & NomeFilePDF & ".pdf")

            For i = 0 To 10000
            Next

            'Response.Write("<script>window.open('../FileTemp/" & NomeFilePDF & ".pdf','','');self.close();</script>")
            'Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")
            'Response.Write("<script>var cazzo=window.open('../FileTemp/" & NomeFilePDF & ".pdf','','');cazzo.focus();</script>") 'nella stessa pagina chiede dove salvare
            'Response.Write("<script>window.open('" & Server.MapPath("..\FileTemp\") & "ReportMorosita_" & Me.txtNomeFile.Value & ".pdf','','');</script>")


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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la morosità con protocollo numero: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la morosità con protocollo numero: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtID.Value = "" Or txtID.Value = "-1" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            'Session.Add("ID", txtid.Text)


            Response.Write("<script>window.open('Morosita.aspx?ID=" & Me.txtID.Value _
                                                    & "&CHIAMANTE=REPORT" _
                                             & "','Morosita','height=530,width=800');</script>")

            'Response.Write("<script>location.replace('Morosita.aspx?ID=" & Me.txtID.Value _
            '                                                   & "&PRO=" & sValoreProtocollo _
            '                                                   & "&DAL=" & sValoreData_Dal _
            '                                                   & "&AL=" & sValoreData_Al _
            '                                                   & "&CHIAMANTE=REPORT" _
            '                                        & "');</script>")

        End If

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click

        Response.Write("<script>document.location.href=""RicercaReport.aspx""</script>")

    End Sub



    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If Trim(Me.txtNomeFile.Value) <> "" Then

            Response.Redirect("..\FileTemp\Export_" & Me.txtNomeFile.Value & ".zip")
        Else
            Response.Write("<script>alert('Nessuna morosità trovata!')</script>")
        End If
    End Sub

    Protected Sub btnStampa_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        If Trim(Me.txtNomeFile.Value) <> "" Then

            Response.Write("<script>window.open('../FileTemp/ReportMorosita_" & Me.txtNomeFile.Value & ".pdf','','');</script>")
        Else
            Response.Write("<script>alert('Nessuna morosità trovata!')</script>")
        End If
    End Sub
End Class
