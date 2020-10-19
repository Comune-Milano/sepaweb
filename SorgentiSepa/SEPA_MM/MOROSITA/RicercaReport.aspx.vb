'*** RICERCA REPORT MOROSITA'

Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class MOROSITA_RicercaReport
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If


        If Not IsPostBack Then

            Me.txtFlag.Value = 0

            SettaggioCampi()

            Me.txtDataDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtDataRIF_DAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRIF_AL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub



    'CARICO Campi e/o ComboBox
    Private Sub SettaggioCampi()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            'NOTA GIUSEPPE: selezionare solo le date per ID_MOROSITA not null 
            par.cmd.CommandText = "select MIN(RIF_DA) from SISCOM_MI.MOROSITA "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtDataDAL.Text = par.FormattaData(par.IfNull(myReader1(0), ""))
                Me.txtDataAL.Text = par.FormattaData(Format(Now, "yyyyMMdd"))
            End If
            myReader1.Close()

            '**************************

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


    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca2.Click
    '    Dim FlagConnessione As Boolean

    '    Me.txtNomeFile.Value = "Export_20110712125749.xls"
    '    Response.Write("<script>window.open('F:/" & Me.txtNomeFile.Value & "','','');self.close();</script>")

    '    Exit Sub

    '    Me.txtFlag.Value = 1
    '    If Strings.Trim(Me.txtProtocollo.Text) <> Strings.Trim(Me.txtProtocollo_TMP.Value) Or Strings.Trim(Me.txtDataDAL.Text) <> Strings.Trim(Me.txtDataDAL_TMP.Value) Or Strings.Trim(Me.txtDataAL.Text) <> Strings.Trim(Me.txtDataAL_TMP.Value) Or Me.txtNomeFile.Value = "" Then
    '        If Elabora() = True Then
    '            Me.txtFlag.Value = 0
    '            'Response.Redirect("..\FileTemp\Export_" & Me.txtNomeFile.Value & ".zip")

    '            'Response.Write("<script>window.open('../FileTemp/Export_" & Me.txtNomeFile.Value & ".xls','','');self.close();</script>")
    '            Response.Write("<script>window.open('" & Server.MapPath("..\FileTemp\Export_" & Me.txtNomeFile.Value & ".xls") & "','','');</script>")

    '            Me.txtProtocollo_TMP.Value = Me.txtProtocollo.Text
    '            Me.txtDataDAL_TMP.Value = Me.txtDataDAL.Text
    '            Me.txtDataAL_TMP.Value = Me.txtDataAL.Text
    '        Else
    '            Me.txtFlag.Value = 0
    '            Response.Write("<script>alert('Nessuna Morosità trovata!');</script>")
    '        End If
    '    Else

    '        ' APRO CONNESSIONE
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Response.Write("IMPOSSIBILE VISUALIZZARE")
    '            Exit Sub
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '            FlagConnessione = True
    '        End If

    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Me.txtFlag.Value = 0
    '        Me.txtProtocollo_TMP.Value = Me.txtProtocollo.Text
    '        Me.txtDataDAL_TMP.Value = Me.txtDataDAL.Text
    '        Me.txtDataAL_TMP.Value = Me.txtDataAL.Text


    '        'Response.Redirect("..\FileTemp\Export_" & Me.txtNomeFile.Value & ".zip")



    '        Response.Write("<script>window.open('" & Server.MapPath("..\FileTemp\Export_" & Me.txtNomeFile.Value & ".xls") & "','','');self.close();</script>")





    '    End If

    'End Sub

    'Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
    '    Dim FlagConnessione As Boolean

    '    Me.txtFlag.Value = 1
    '    If Strings.Trim(Me.txtProtocollo.Text) <> Strings.Trim(Me.txtProtocollo_TMP.Value) Or Strings.Trim(Me.txtDataDAL.Text) <> Strings.Trim(Me.txtDataDAL_TMP.Value) Or Strings.Trim(Me.txtDataAL.Text) <> Strings.Trim(Me.txtDataAL_TMP.Value) Or Me.txtNomeFile.Value = "" Then
    '        If Elabora() = True Then
    '            Me.txtFlag.Value = 0
    '            Response.Write("<script>window.open('../FileTemp/ReportMorosita_" & Me.txtNomeFile.Value & ".pdf','','');self.close();</script>")

    '            Me.txtProtocollo_TMP.Value = Me.txtProtocollo.Text
    '            Me.txtDataDAL_TMP.Value = Me.txtDataDAL.Text
    '            Me.txtDataAL_TMP.Value = Me.txtDataAL.Text
    '        Else
    '            Me.txtFlag.Value = 0
    '            Response.Write("<script>alert('Nessuna Morosità trovata!');</script>")

    '        End If
    '    Else

    '        ' APRO CONNESSIONE
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Response.Write("IMPOSSIBILE VISUALIZZARE")
    '            Exit Sub
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '            FlagConnessione = True
    '        End If

    '        Response.Write("<script>window.open('../FileTemp/ReportMorosita_" & Me.txtNomeFile.Value & ".pdf','','');self.close();</script>")

    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Me.txtFlag.Value = 0
    '        Me.txtProtocollo_TMP.Value = Me.txtProtocollo.Text
    '        Me.txtDataDAL_TMP.Value = Me.txtDataDAL.Text
    '        Me.txtDataAL_TMP.Value = Me.txtDataAL.Text

    '    End If
    'End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub



    'Function Elabora() As Boolean
    '    Dim FlagConnessione As Boolean

    '    Try

    '        Dim TestoPagina As String = ""
    '        Dim myExcelFile As New CM.ExcelFile
    '        Dim i, K As Long

    '        Dim sNomeFile As String
    '        Dim NomeFilePDF As String

    '        Dim sStringaSQL As String = ""
    '        Dim sStringaSQL1 As String = ""
    '        Dim sValore As String = ""
    '        Dim sCompara As String = ""

    '        Dim sValoreProtocollo As String = par.IfEmpty(Me.txtProtocollo.Text, "")
    '        Dim sValoreData_Dal As String = par.IfEmpty(par.AggiustaData(Me.txtDataDAL.Text), "")
    '        Dim sValoreData_Al As String = par.IfEmpty(par.AggiustaData(Me.txtDataAL.Text), "")

    '        Dim sID_Morosita As String = ""

    '        '*** TOTALI *************************************
    '        Dim Tot_Inquilini As Long = 0
    '        Dim Tot_MG As Long = 0
    '        Dim Tot_MA As Long = 0

    '        Dim Tot_MG_EURO As Decimal = 0
    '        Dim Tot_MA_EURO As Decimal = 0

    '        Dim Tot_MG_Pagati As Long = 0
    '        Dim Tot_MA_Pagati As Long = 0

    '        Dim Tot_MG_NO_Pagati As Long = 0
    '        Dim Tot_MA_NO_Pagati As Long = 0

    '        Dim Tot_MG_EURO_Riga As Decimal = 0
    '        Dim Tot_MA_EURO_Riga As Decimal = 0

    '        Dim Tot_MG_Pagati_Riga As Long = 0
    '        Dim Tot_MA_Pagati_Riga As Long = 0

    '        Dim Tot_MG_NO_Pagati_Riga As Long = 0
    '        Dim Tot_MA_NO_Pagati_riga As Long = 0
    '        '***********************

    '        Dim dVal As Decimal = 0
    '        Dim lVal As Long = 0

    '        Dim Str As String = ""

    '        Elabora = False


    '        Me.txtNomeFile.Value = Format(Now, "yyyyMMddHHmmss")
    '        sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

    '        With myExcelFile

    '            .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

    '            .PrintGridLines = False
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
    '            .SetDefaultRowHeight(14)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
    '            .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NUM. PROTOCOLLO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA PROTOCOLLO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "NUM. INQUILINI", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "MG EMESSI", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "MA EMESSI", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "IMPORTO MG", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "IMPORTO MA", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MAV MG Pagati", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "MAV MA Pagati", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "MAV MG Non Pagati", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "MAV MA Non Pagati", 12)

    '            K = 2

    '            'PDF ***********************
    '            NomeFilePDF = "ReportMorosita_" & Format(Now, "yyyyMMddHHmmss")
    '            TestoPagina = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>SITUAZIONE MOROSITA: </p></br>"

    '            TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
    '            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
    '                                      & "<td align='left'  style='border-bottom-style: dashed; width:14%;  border-bottom-width: 1px; border-bottom-color: #000000'>NUM. PROTOCOLLO</td>" _
    '                                      & "<td align='left'  style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>DATA PROTOCOLLO</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>NUM. INQUILINI</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>MG EMESSI</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>MA EMESSI</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:12%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO MG</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:12%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO MA</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MG Pagati</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MA Pagati</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MG Non Pagati</td>" _
    '                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>MAV MA Non Pagati</td>" _
    '                                      & "</tr>"
    '            '*************************************************

    '            ' APRO CONNESSIONE
    '            If par.OracleConn.State = Data.ConnectionState.Open Then
    '                Response.Write("IMPOSSIBILE VISUALIZZARE")
    '                Exit Function
    '            Else
    '                par.OracleConn.Open()
    '                par.SettaCommand(par)
    '                FlagConnessione = True
    '            End If


    '            '1) select MOROSITA per PROTOCOLLO_ALER e/o DATA_PROTOCOLLO o niente
    '            sStringaSQL = "select * from  SISCOM_MI.MOROSITA "

    '            '*** PROTOCOLLO 
    '            If sValoreProtocollo <> "" Then
    '                sValore = Strings.UCase(sValoreProtocollo)
    '                If InStr(sValore, "*") Then
    '                    sCompara = " LIKE "
    '                    Call par.ConvertiJolly(sValore)
    '                Else
    '                    sCompara = " = "
    '                End If

    '                If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
    '                sStringaSQL1 = sStringaSQL1 & "  PROTOCOLLO_ALER " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
    '            End If
    '            '********************************

    '            '*** DATA_PROTOCOLLO 
    '            If sValoreData_Dal <> "" Then
    '                If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "

    '                sStringaSQL1 = sStringaSQL1 & " MOROSITA.DATA_PROTOCOLLO>='" & sValoreData_Dal & "' "
    '            End If

    '            If sValoreData_Al <> "" Then
    '                If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "

    '                sStringaSQL1 = sStringaSQL1 & " MOROSITA.DATA_PROTOCOLLO<='" & sValoreData_Al & "' "
    '            End If
    '            '********************************

    '            If sStringaSQL1 <> "" Then sStringaSQL1 = " where " & sStringaSQL1
    '            sStringaSQL = sStringaSQL & sStringaSQL1

    '            par.cmd.CommandText = sStringaSQL
    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            Do While myReader1.Read()

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(myReader1("PROTOCOLLO_ALER"), ""))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.FormattaData(par.IfNull(myReader1("DATA_PROTOCOLLO"), "")))


    '                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
    '                      & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("PROTOCOLLO_ALER"), "") & "</td>" _
    '                      & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReader1("DATA_PROTOCOLLO"), "")) & "</td>"



    '                '*** conoscere il numero di inquilini morosi
    '                '2) Loop per Select Case DISTINCT(ID_ANAGRAFICA) from MOROSITA_LETTERE WHERE ID_MOROSITA=XXX
    '                i = 0
    '                par.cmd.CommandText = "select DISTINCT(ID_ANAGRAFICA) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1)
    '                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                Do While myReader2.Read()
    '                    i = i + 1
    '                Loop
    '                myReader2.Close()

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, i)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & i & "</td>"

    '                Tot_Inquilini = Tot_Inquilini + i
    '                '*******************************************************


    '                'MOROSITA_LETTERE ****************************************************************************
    '                '*** Numero di MAV MG emessi 
    '                '3) select count(*) from MOROSITA_LETTERE where ID_MOROSITA=XXX and INIZIO_PERIODO<>20091001
    '                lVal = 0
    '                par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and INIZIO_PERIODO<>20091001 "
    '                myReader2 = par.cmd.ExecuteReader()
    '                If myReader2.Read Then
    '                    lVal = par.IfNull(myReader2(0), 0)
    '                    Tot_MG = Tot_MG + par.IfNull(myReader2(0), 0)
    '                End If
    '                myReader2.Close()

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, lVal)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"
    '                '*******************************************************


    '                '*** Numero di MAV MA emessi 
    '                '4) select count(*) from MOROSITA_LETTERE where ID_MOROSITA=XXX and INIZIO_PERIODO=20091001
    '                lVal = 0
    '                i = 0
    '                par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and INIZIO_PERIODO=20091001 "
    '                myReader2 = par.cmd.ExecuteReader()
    '                If myReader2.Read Then
    '                    lVal = par.IfNull(myReader2(0), 0)
    '                    Tot_MA = Tot_MA + par.IfNull(myReader2(0), 0)
    '                End If
    '                myReader2.Close()

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, lVal)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"
    '                'FINE MOROSITA_LETTERE  *******************************************************


    '                'BOL_BOLLETTE *************************************************************
    '                '*** corrispondente valore della morosità richiesta MG 
    '                '5)  select SUM(NVL(IMPORTO_TOTALE,0)) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MG'
    '                dVal = 0

    '                Tot_MG_EURO_Riga = 0
    '                Tot_MA_EURO_Riga = 0

    '                Tot_MG_Pagati_Riga = 0
    '                Tot_MA_Pagati_Riga = 0

    '                Tot_MG_NO_Pagati_Riga = 0
    '                Tot_MA_NO_Pagati_riga = 0

    '                'par.cmd.CommandText = "select SUM(NVL(IMPORTO_TOTALE,0)) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MG' "
    '                par.cmd.CommandText = "select NVL(IMPORTO_TOTALE,0) as IMPORTO_TOTALE,NVL(IMPORTO_PAGATO,0) as IMPORTO_PAGATO,TRIM(NOTE) as NOTE " _
    '                                   & " from SISCOM_MI.BOL_BOLLETTE " _
    '                                   & " where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) _
    '                                   & " and (NOTE='MOROSITA'' MG' or NOTE='MOROSITA'' MA') "

    '                myReader2 = par.cmd.ExecuteReader()
    '                Do While myReader2.Read()

    '                    If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MG" Then
    '                        Tot_MG_EURO_Riga = Tot_MG_EURO_Riga + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
    '                        Tot_MG_EURO = Tot_MG_EURO + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
    '                    End If
    '                    If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MA" Then
    '                        Tot_MA_EURO_Riga = Tot_MA_EURO_Riga + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
    '                        Tot_MA_EURO = Tot_MA_EURO + par.IfNull(myReader2("IMPORTO_TOTALE"), 0)
    '                    End If


    '                    If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MG" And (par.IfNull(myReader2("IMPORTO_TOTALE"), 0) - par.IfNull(myReader2("IMPORTO_PAGATO"), 0)) = 0 Then
    '                        Tot_MG_Pagati_Riga = Tot_MG_Pagati_Riga + 1
    '                        Tot_MG_Pagati = Tot_MG_Pagati + 1
    '                    End If
    '                    If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MA" And (par.IfNull(myReader2("IMPORTO_TOTALE"), 0) - par.IfNull(myReader2("IMPORTO_PAGATO"), 0)) = 0 Then
    '                        Tot_MA_Pagati_Riga = Tot_MA_Pagati_Riga + 1
    '                        Tot_MA_Pagati = Tot_MA_Pagati + 1
    '                    End If


    '                    If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MG" And par.IfNull(myReader2("IMPORTO_PAGATO"), 0) = 0 Then
    '                        Tot_MG_NO_Pagati_Riga = Tot_MG_NO_Pagati_Riga + 1
    '                        Tot_MG_NO_Pagati = Tot_MG_NO_Pagati + 1
    '                    End If
    '                    If Strings.Right(par.IfNull(myReader2("NOTE"), ""), 2) = "MA" And par.IfNull(myReader2("IMPORTO_PAGATO"), 0) = 0 Then
    '                        Tot_MA_NO_Pagati_riga = Tot_MA_NO_Pagati_riga + 1
    '                        Tot_MA_NO_Pagati = Tot_MA_NO_Pagati + 1
    '                    End If
    '                Loop
    '                myReader2.Close()


    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, IsNumFormat(Tot_MG_EURO_Riga, 0, "##,##0.00"))
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(Tot_MG_EURO_Riga, "##,##0.00") & "</td>"

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, IsNumFormat(Tot_MA_EURO_Riga, 0, "##,##0.00"))
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(Tot_MA_EURO_Riga, "##,##0.00") & "</td>"

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, Tot_MG_Pagati_Riga)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MG_Pagati_Riga & "</td>"

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, Tot_MA_Pagati_Riga)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MA_Pagati_Riga & "</td>"

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, Tot_MG_NO_Pagati_Riga)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MG_NO_Pagati_Riga & "</td>"

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, Tot_MA_NO_Pagati_riga)
    '                TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Tot_MA_NO_Pagati_riga & "</td>"
    '                ''*******************************************************


    '                ''*** corrispondente valore della morosità richiesta MA 
    '                ''6)  select SUM(NVL(IMPORTO_TOTALE,0)) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA'
    '                'dVal = 0
    '                'par.cmd.CommandText = "select SUM(NVL(IMPORTO_TOTALE,0)) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' "
    '                ''myReader2 = par.cmd.ExecuteReader()
    '                ''If myReader2.Read Then
    '                ''    dVal = par.IfNull(myReader2(0), 0)
    '                ''    Tot_MA_EURO = Tot_MA_EURO + par.IfNull(myReader2(0), 0)
    '                ''End If
    '                ''myReader2.Close()

    '                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, IsNumFormat(dVal, "", "##,##0.00"))
    '                'TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(dVal, "##,##0.00") & "</td>"
    '                '*******************************************************


    '                '*** quanti MAV MG sono stati pagati
    '                '6) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0
    '                'lVal = 0
    '                'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MG' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0 "
    '                ''myReader2 = par.cmd.ExecuteReader()
    '                ''If myReader2.Read Then
    '                ''    lVal = par.IfNull(myReader2(0), 0)
    '                ''    Tot_MG_Pagati = Tot_MG_Pagati + par.IfNull(myReader2(0), 0)
    '                ''End If
    '                ''myReader2.Close()

    '                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, lVal)
    '                'TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"

    '                '*******************************************************


    '                '*** quanti MAV MG sono stati pagati
    '                '7) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0
    '                'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' and (NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0))=0 "
    '                'myReader2 = par.cmd.ExecuteReader()
    '                'If myReader2.Read Then
    '                '    lVal = par.IfNull(myReader2(0), 0)
    '                '    Tot_MA_Pagati = Tot_MA_Pagati + par.IfNull(myReader2(0), 0)
    '                'End If
    '                'myReader2.Close()

    '                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, lVal)
    '                'TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"

    '                '*******************************************************



    '                '*** quanti MAV MG non sono stati pagati
    '                '6) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA' and NVL(IMPORTO_PAGATO,0)=0
    '                'lVal = 0
    '                'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MG' and NVL(IMPORTO_PAGATO,0)=0 "
    '                'myReader2 = par.cmd.ExecuteReader()
    '                'If myReader2.Read Then
    '                '    lVal = par.IfNull(myReader2(0), 0)
    '                '    Tot_MG_NO_Pagati = Tot_MG_NO_Pagati + par.IfNull(myReader2(0), 0)
    '                'End If
    '                'myReader2.Close()

    '                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, lVal)
    '                'TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"

    '                '*******************************************************


    '                '*** quanti MAV MA non sono stati pagati
    '                '7) select count(*) from BOL_BOLLETTE where ID_MOROSITA=XXX and NOTE='MOROSITA'' MA'  and NVL(IMPORTO_PAGATO,0)=0
    '                'lVal = 0
    '                'par.cmd.CommandText = "select count(*) from SISCOM_MI.BOL_BOLLETTE where ID_MOROSITA=" & par.IfNull(myReader1("ID"), -1) & " and NOTE='MOROSITA'' MA' and NVL(IMPORTO_PAGATO,0)=0 "
    '                'myReader2 = par.cmd.ExecuteReader()
    '                'If myReader2.Read Then
    '                '    lVal = par.IfNull(myReader2(0), 0)
    '                '    Tot_MA_NO_Pagati = Tot_MA_NO_Pagati + par.IfNull(myReader2(0), 0)
    '                'End If
    '                'myReader2.Close()

    '                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, lVal)
    '                'TestoPagina = TestoPagina & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & lVal & "</td>"

    '                '*******************************************************

    '                TestoPagina = TestoPagina & "</tr>"

    '                K = K + 1

    '            Loop
    '            myReader1.Close()

    '            If K = 2 Then
    '                .CloseFile()

    '                File.Delete(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))

    '                If FlagConnessione = True Then
    '                    par.cmd.Dispose()
    '                    par.OracleConn.Close()
    '                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '                End If

    '                Me.txtNomeFile.Value = ""
    '                Elabora = False
    '                Exit Function

    '            End If

    '            'TOTALI 
    '            K = K + 1

    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "TOTALE: ")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ")

    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, IsNumFormat(Tot_Inquilini, "", "##,##"))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, IsNumFormat(Tot_MG, "", "##,##"))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, IsNumFormat(Tot_MA, "", "##,##"))

    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, IsNumFormat(Tot_MG_EURO, "", "##,##0.00"))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, IsNumFormat(Tot_MA_EURO, "", "##,##0.00"))


    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, IsNumFormat(Tot_MG_Pagati, "", "##,##"))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, IsNumFormat(Tot_MA_Pagati, "", "##,##"))


    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, IsNumFormat(Tot_MG_NO_Pagati, "", "##,##0"))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, IsNumFormat(Tot_MA_NO_Pagati, "", "##,##0"))



    '            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
    '                                                 & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE: </td>" _
    '                                                 & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_Inquilini, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MG, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MA, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MG_EURO, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MA_EURO, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MG_Pagati, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MA_Pagati, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MG_NO_Pagati, 0), "##,##") & "</td>" _
    '                                                 & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(par.IfEmpty(Tot_MA_NO_Pagati, 0), "##,##") & "</td>" _
    '                                                 & "</tr>"

    '            '******************************

    '            .CloseFile()
    '        End With




    '        'Dim objCrc32 As New Crc32()
    '        'Dim strmZipOutputStream As ZipOutputStream
    '        'Dim zipfic As String

    '        'zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

    '        'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '        'strmZipOutputStream.SetLevel(6)
    '        '
    '        'Dim strFile As String
    '        'strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")

    '        'Dim strmFile As FileStream = File.OpenRead(strFile)
    '        'Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '        ''
    '        'strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '        'Dim sFile As String = Path.GetFileName(strFile)
    '        'Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '        'Dim fi As New FileInfo(strFile)
    '        'theEntry.DateTime = fi.LastWriteTime
    '        'theEntry.Size = strmFile.Length
    '        'strmFile.Close()

    '        'objCrc32.Reset()
    '        'objCrc32.Update(abyBuffer)
    '        'theEntry.Crc = objCrc32.Value
    '        'strmZipOutputStream.PutNextEntry(theEntry)
    '        'strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '        'strmZipOutputStream.Finish()
    '        'strmZipOutputStream.Close()

    '        'File.Delete(strFile)

    '        '*** PDF

    '        Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm", False, System.Text.Encoding.Default)
    '        sr.WriteLine(TestoPagina)
    '        sr.Close()

    '        Dim url As String = NomeFilePDF
    '        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

    '        Dim pdfConverter As PdfConverter = New PdfConverter

    '        If Licenza <> "" Then
    '            pdfConverter.LicenseKey = Licenza
    '        End If

    '        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
    '        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
    '        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
    '        pdfConverter.PdfDocumentOptions.ShowHeader = False
    '        pdfConverter.PdfDocumentOptions.ShowFooter = False
    '        pdfConverter.PdfDocumentOptions.LeftMargin = 20
    '        pdfConverter.PdfDocumentOptions.RightMargin = 20
    '        pdfConverter.PdfDocumentOptions.TopMargin = 5
    '        pdfConverter.PdfDocumentOptions.BottomMargin = 5
    '        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

    '        pdfConverter.PdfDocumentOptions.ShowHeader = False
    '        pdfConverter.PdfFooterOptions.FooterText = ("")
    '        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
    '        pdfConverter.PdfFooterOptions.DrawFooterLine = False
    '        pdfConverter.PdfFooterOptions.PageNumberText = ""
    '        pdfConverter.PdfFooterOptions.ShowPageNumber = False


    '        pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm", Server.MapPath("..\FileTemp\") & NomeFilePDF & ".pdf")
    '        IO.File.Delete(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm")
    '        'Response.Redirect("..\FileTemp\" & NomeFilePDF & ".pdf")

    '        For i = 0 To 10000
    '        Next

    '        'Response.Write("<script>window.open('../FileTemp/" & NomeFilePDF & ".pdf','','');self.close();</script>")




    '        'Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

    '        'Response.Write("<script>var cazzo=window.open('../FileTemp/" & sNomeFile & ".zip','','');cazzo.focus();</script>") 'nella stessa pagina chiede dove salvare



    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Elabora = True

    '    Catch ex As Exception
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")

    '    End Try

    'End Function


    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    'If Strings.Len(sID_Morosita) = 0 Then
    '    sID_Morosita = " ID_MOROSITA in (" & par.IfNull(myReader1("ID"), -1)

    'ElseIf i = 1000 Then
    '    sID_Morosita = sID_Morosita & ") or ID_MOROSITA in (-1"
    '    i = 0
    'Else
    '    sID_Morosita = sID_Morosita & "," & par.IfNull(myReader1("ID"), -1)
    'End If

    'i = i + 1

    'If Strings.Len(sID_Morosita) = 0 Then
    '    sID_Morosita = " ID_MOROSITA in (-1) "
    'Else
    '    sID_Morosita = sID_Morosita & ")"
    'End If
    '****************************

   
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Try

            If Strings.Len(Me.txtDataAL.Text) > 0 Then
                If par.AggiustaData(Me.txtDataAL.Text) < par.AggiustaData(Me.txtDataDAL.Text) Then
                    Response.Write("<script>alert('Attenzione...Controllare il range delle date!');</script>")
                    Exit Sub
                End If
            End If

            Response.Write("<script>location.replace('RisultatiReport.aspx?PRO=" & par.VaroleDaPassare(Me.txtProtocollo.Text) _
                                                                        & "&DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataDAL.Text), "") _
                                                                        & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataAL.Text), "") _
                                                                        & "');</script>")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click

        ''height=580,top=0,left=0,width=780'
        Response.Write("<script>window.open('Flussi_Anno.aspx?DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataRIF_DAL.Text), "") _
                                                           & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataRIF_AL.Text), "") _
                                            & "','STAMPA" & Format(Now, "hhss") & "');</script>")

    End Sub
End Class
