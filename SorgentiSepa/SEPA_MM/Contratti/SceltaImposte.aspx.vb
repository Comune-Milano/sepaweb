Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_SceltaIstat
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim aggiornamento_istat As Double = 0
    Dim AltriAdeguamenti As Double = 0
    Dim contaprog As Integer = 0

    Dim accatastato As String = ""
    Dim accatastato2 As Boolean
    Dim datainizioproroga As String = ""
    Dim datafineproroga As String = ""
    Dim tipopagamentoproroga As String = ""
    Dim importo112 As Double = 0
    Dim errcf As String = ""
    Dim errcfconta As Integer = 0
    Dim outerrcf As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        txtValuta.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -32, Now))) & " " & Year(DateAdd("M", -32, Now)), CStr(Year(DateAdd("M", -32, Now)) & Format(Month(DateAdd("M", -32, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -31, Now))) & " " & Year(DateAdd("M", -31, Now)), CStr(Year(DateAdd("M", -31, Now)) & Format(Month(DateAdd("M", -31, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -30, Now))) & " " & Year(DateAdd("M", -30, Now)), CStr(Year(DateAdd("M", -30, Now)) & Format(Month(DateAdd("M", -30, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -29, Now))) & " " & Year(DateAdd("M", -29, Now)), CStr(Year(DateAdd("M", -29, Now)) & Format(Month(DateAdd("M", -29, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -28, Now))) & " " & Year(DateAdd("M", -28, Now)), CStr(Year(DateAdd("M", -28, Now)) & Format(Month(DateAdd("M", -28, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -27, Now))) & " " & Year(DateAdd("M", -27, Now)), CStr(Year(DateAdd("M", -27, Now)) & Format(Month(DateAdd("M", -27, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -26, Now))) & " " & Year(DateAdd("M", -26, Now)), CStr(Year(DateAdd("M", -26, Now)) & Format(Month(DateAdd("M", -26, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -25, Now))) & " " & Year(DateAdd("M", -25, Now)), CStr(Year(DateAdd("M", -25, Now)) & Format(Month(DateAdd("M", -25, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -24, Now))) & " " & Year(DateAdd("M", -24, Now)), CStr(Year(DateAdd("M", -24, Now)) & Format(Month(DateAdd("M", -24, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -23, Now))) & " " & Year(DateAdd("M", -23, Now)), CStr(Year(DateAdd("M", -23, Now)) & Format(Month(DateAdd("M", -23, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -22, Now))) & " " & Year(DateAdd("M", -22, Now)), CStr(Year(DateAdd("M", -22, Now)) & Format(Month(DateAdd("M", -22, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -21, Now))) & " " & Year(DateAdd("M", -21, Now)), CStr(Year(DateAdd("M", -21, Now)) & Format(Month(DateAdd("M", -21, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -20, Now))) & " " & Year(DateAdd("M", -20, Now)), CStr(Year(DateAdd("M", -20, Now)) & Format(Month(DateAdd("M", -20, Now)), "00"))))

                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -19, Now))) & " " & Year(DateAdd("M", -19, Now)), CStr(Year(DateAdd("M", -19, Now)) & Format(Month(DateAdd("M", -19, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -18, Now))) & " " & Year(DateAdd("M", -18, Now)), CStr(Year(DateAdd("M", -18, Now)) & Format(Month(DateAdd("M", -18, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -17, Now))) & " " & Year(DateAdd("M", -17, Now)), CStr(Year(DateAdd("M", -17, Now)) & Format(Month(DateAdd("M", -17, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -16, Now))) & " " & Year(DateAdd("M", -16, Now)), CStr(Year(DateAdd("M", -16, Now)) & Format(Month(DateAdd("M", -16, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -15, Now))) & " " & Year(DateAdd("M", -15, Now)), CStr(Year(DateAdd("M", -15, Now)) & Format(Month(DateAdd("M", -15, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -14, Now))) & " " & Year(DateAdd("M", -14, Now)), CStr(Year(DateAdd("M", -14, Now)) & Format(Month(DateAdd("M", -14, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -13, Now))) & " " & Year(DateAdd("M", -13, Now)), CStr(Year(DateAdd("M", -13, Now)) & Format(Month(DateAdd("M", -13, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -12, Now))) & " " & Year(DateAdd("M", -12, Now)), CStr(Year(DateAdd("M", -12, Now)) & Format(Month(DateAdd("M", -12, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -11, Now))) & " " & Year(DateAdd("M", -11, Now)), CStr(Year(DateAdd("M", -11, Now)) & Format(Month(DateAdd("M", -11, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -10, Now))) & " " & Year(DateAdd("M", -10, Now)), CStr(Year(DateAdd("M", -10, Now)) & Format(Month(DateAdd("M", -10, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -9, Now))) & " " & Year(DateAdd("M", -9, Now)), CStr(Year(DateAdd("M", -9, Now)) & Format(Month(DateAdd("M", -9, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -8, Now))) & " " & Year(DateAdd("M", -8, Now)), CStr(Year(DateAdd("M", -8, Now)) & Format(Month(DateAdd("M", -8, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -7, Now))) & " " & Year(DateAdd("M", -7, Now)), CStr(Year(DateAdd("M", -7, Now)) & Format(Month(DateAdd("M", -7, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -6, Now))) & " " & Year(DateAdd("M", -6, Now)), CStr(Year(DateAdd("M", -6, Now)) & Format(Month(DateAdd("M", -6, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -5, Now))) & " " & Year(DateAdd("M", -5, Now)), CStr(Year(DateAdd("M", -5, Now)) & Format(Month(DateAdd("M", -5, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -4, Now))) & " " & Year(DateAdd("M", -4, Now)), CStr(Year(DateAdd("M", -4, Now)) & Format(Month(DateAdd("M", -4, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -3, Now))) & " " & Year(DateAdd("M", -3, Now)), CStr(Year(DateAdd("M", -3, Now)) & Format(Month(DateAdd("M", -3, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -2, Now))) & " " & Year(DateAdd("M", -2, Now)), CStr(Year(DateAdd("M", -2, Now)) & Format(Month(DateAdd("M", -2, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -1, Now))) & " " & Year(DateAdd("M", -1, Now)), CStr(Year(DateAdd("M", -1, Now)) & Format(Month(DateAdd("M", -1, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(Now)) & " " & Year(Now), CStr(Year(Now) & Format(Month(Now), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 1, Now))) & " " & Year(DateAdd("M", 1, Now)), CStr(Year(DateAdd("M", 1, Now)) & Format(Month(DateAdd("M", 1, Now)), "00"))))

                cmbMese.Items.FindByValue(CStr(Year(Now) & Format(Month(Now), "00"))).Selected = True
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Label3.Visible = True
                Label3.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Dim ElencoFile() as string
        Dim myExcelFile As New CM.ExcelFile

        If txtValuta.Text <> "" And par.AggiustaData(txtValuta.Text) >= Format(Now, "yyyyMMdd") Then
            Try
                Dim ImportoRecesso As Double = 0
                Dim ImportoRegistrazione As Double = 0
                Dim ImportoTassa As Double = 0
                Dim cflocatore As String = ""
                Dim filler As String = " "
                Dim importoSanzioni As Double = 0
                Dim importoInteressi As Double = 0
                Dim SUBENTRI As String = ""
                Dim UfficioRegistro As String = ""
                Dim i As Integer = 0
                Dim Nominativo As String = ""
                Dim CodiceUtente As String = ""

                Dim kk As Integer = 2

                Dim NomeFile As String = "IMPOSTE_" & Format(Now, "yyyyMMddHHmmss")
                Dim NomeFilexls As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFile & ".xls"

                With myExcelFile

                    .CreateFile(NomeFilexls)
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

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD.CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA DECORRENZA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA SCADENZA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA SLOGGIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD.UTENTE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NOMINATIVO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "UFF.REGISTRO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ANNO REGISTRAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "SERIE REGISTRAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NUM. REGISTRAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "COD.TRIBUTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "IMPORTO CANONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "IMPORTO TRIBUTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "GIORNI SANZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "IMPORTO SANZIONI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "IMPORTO INTERESSI", 12)






                    Dim NomeFileIMP As String = Format(Now, "yyMMddmmss")

                    Dim Str As String

                    Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                    Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
                    Str = Str & "<" & "/div>"

                    Response.Write(Str)
                    Response.Flush()

                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=1"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        ImportoRecesso = CDbl(par.PuntiInVirgole(par.IfNull(myReader("valore"), "")))
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=6"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        ImportoRegistrazione = CDbl(par.PuntiInVirgole(par.IfNull(myReader("valore"), "")))
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=10"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        cflocatore = par.IfNull(myReader("valore"), "")
                    End If
                    myReader.Close()

                    Dim Interessi As New SortedDictionary(Of Integer, Double)
                    Interessi.Clear()

                    par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legaLI order by anno desc"
                    Dim myReaderAB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReaderAB.Read
                        Interessi.Add(myReaderAB("anno"), myReaderAB("tasso"))
                    Loop
                    myReaderAB.Close()

                    'Dim sSql As String
                    'sSql = "CREATE TABLE DETTAGLI ([COD.CONTRATTO] TEXT(50),[DATA DECORRENZA] TEXT(10),[DATA SCADENZA] TEXT(10),[DATA SLOGGIO] TEXT(10),[COD.UTENTE] TEXT(20),[NOMINATIVO] TEXT(100),[UFF.REGISTRO] TEXT(50),[ANNO REGISTRAZIONE] TEXT(50),[SERIE REGISTRAZIONE] TEXT(50), [NUM. REGISTRAZIONE] TEXT(50), [COD.TRIBUTO] TEXT(50), [IMPORTO CANONE] TEXT(50), [IMPORTO TRIBUTO] TEXT(50), [GIORNI SANZIONE] TEXT(50),[IMPORTO SANZIONI] TEXT(50), [IMPORTO INTERESSI] TEXT(50))"

                    'Dim cnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                    '       "Data Source=" & Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFile & ".xls;") & _
                    '       "Extended Properties=""Excel 8.0;HDR=YES"""
                    'Dim cn As New OleDbConnection(cnString)
                    'cn.Open()
                    'Dim cmd As New OleDbCommand(sSql, cn)
                    'cmd.ExecuteNonQuery()

                    'Dim cmd1 As New OleDbCommand
                    'cmd1.Connection = cn

                    Dim RECORD_TROVATI As Integer = 0
                    Dim record_trovati2 As Integer = 0
                    Dim ContoCorrente As String = "01349670156"

                    Dim DataValuta As Date
                    Dim GiorniMese As Integer = 30
                    Dim gioniDiff As Integer = 0
                    Dim sanzione As String = "0"

                    Dim Giorni As Integer = 0
                    Dim GiorniAnno As Integer = 0
                    Dim dataPartenza As String = ""

                    Dim Totale As Double = 0
                    Dim TotalePeriodo As Double = 0
                    Dim indice As Long = 0
                    Dim tasso As Double = 0
                    Dim DataFine As String = ""
                    Dim baseCalcolo As Double = 0
                    Dim DataCalcolo As String = ""
                    Dim DataInizio As String = ""
                    Dim KK1 As Integer = 0
                    Dim Agevolato As String = ""
                    Dim CanoneDaScrivere As String = ""

                    Dim ContatoreFile As Integer = 1

                    Dim TipologiaAbitativa As String = ""

                    Dim IndiceGiorni As Integer = 1
                    par.cmd.CommandText = "SELECT distinct cod_ufficio_reg  from siscom_mi.rapporti_utenza WHERE data_stipula>'20091001' and bozza=0 and COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    ContatoreFile = 1
                    Do While myReader1.Read

                        UfficioRegistro = par.IfNull(myReader1(0), "")

                        Select Case Mid(cmbMese.SelectedItem.Value, 5, 2)
                            Case "01", "03", "05", "07", "08", "10", "12"
                                GiorniMese = 31
                            Case "02"
                                GiorniMese = 28
                            Case "04", "06", "09", "11"
                                GiorniMese = 30
                            Case Else
                                GiorniMese = 30
                        End Select



                        For IndiceGiorni = 1 To GiorniMese  'fa un file per ogni giorno del mese
                            Agevolato = "N"
                            RECORD_TROVATI = 0

                            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "R" & "" & NomeFileIMP & "" & ContatoreFile & ".con"), False, System.Text.Encoding.Default)

                            DataValuta = DateAdd("d", 29, CDate(IndiceGiorni & "/" & Mid(cmbMese.SelectedItem.Value, 5, 2) & "/" & Mid(cmbMese.SelectedItem.Value, 1, 4)))

                            'scrivo i lrecord di testa
                            If Format(DataValuta, "yyyyMMdd") > par.AggiustaData(txtValuta.Text) Then
                                sr.WriteLine("A" & filler.PadLeft(14) & "LOCA0" & filler.PadLeft(2) & ContoCorrente.PadRight(16) & cflocatore.PadRight(16) & cflocatore.PadRight(16) & Format(CDate(DataValuta), "ddMMyyyy") & filler.PadLeft(419) & "A")
                            Else
                                sr.WriteLine("A" & filler.PadLeft(14) & "LOCA0" & filler.PadLeft(2) & ContoCorrente.PadRight(16) & cflocatore.PadRight(16) & cflocatore.PadRight(16) & Format(CDate(txtValuta.Text), "ddMMyyyy") & filler.PadLeft(419) & "A")
                            End If

                            'ANNUALITA SUCCESSIVE
                            par.cmd.CommandText = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                                                & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                                                & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA  from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI where  " _
                                                & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND  data_stipula>'20091001' and VERSAMENTO_TR='A' AND cod_ufficio_reg='" & UfficioRegistro _
                                                & "' and SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND SUBSTR(DATA_DECORRENZA,7,2)='" & Format(IndiceGiorni, "00") _
                                                & "' and SUBSTR(DATA_DECORRENZA,1,4)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                                                & "' AND SUBSTR(DATA_DECORRENZA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                                                & "' AND SUBSTR(DATA_SCADENZA_RINNOVO,1,6)>'" & cmbMese.SelectedItem.Value _
                                                & "' AND (DURATA_ANNI+SUBSTR(DATA_DECORRENZA,1,4))<>'" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "' AND BOZZA=0" _
                                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='112T')" _
                                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='112T')" _
                                                & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"

                            '
                            myReader = par.cmd.ExecuteReader()
                            Do While myReader.Read
                                Agevolato = "N"
                                If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And (par.IfNull(myReader("DEST_USO"), "") = "0" Or par.IfNull(myReader("DEST_USO"), "") = "P") Then
                                    Agevolato = "S"
                                End If

                                If Agevolato = "N" Then
                                    CanoneDaScrivere = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0))
                                    ImportoTassa = Format(((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)) / 100) * 2, "0")
                                Else
                                    CanoneDaScrivere = ((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)) - ((30 / 100) * (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0))))
                                    ImportoTassa = Format((((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)) - ((30 / 100) * (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)))) / 100) * 2, "0")
                                End If

                                importoSanzioni = 0
                                importoInteressi = 0
                                gioniDiff = 0

                                If Format(DataValuta, "yyyyMMdd") < par.AggiustaData(txtValuta.Text) Then

                                    gioniDiff = DateDiff(DateInterval.Day, DataValuta, CDate(txtValuta.Text))

                                    If gioniDiff <= 90 Then
                                        importoSanzioni = Format((ImportoTassa * 3) / 100, "0.00")
                                    End If

                                    If gioniDiff >= 91 And gioniDiff <= 365 Then
                                        importoSanzioni = Format((ImportoTassa * 3.75) / 100, "0.00")
                                    End If

                                    If gioniDiff >= 366 Then
                                        importoSanzioni = Format((ImportoTassa * 30) / 100, "0.00")
                                    End If


                                    importoInteressi = 0
                                    If importoSanzioni > 0 Then
                                        DataCalcolo = par.AggiustaData(txtValuta.Text)
                                        DataInizio = Format(DateAdd(DateInterval.Day, 1, DataValuta), "yyyyMMdd")

                                        Giorni = 0
                                        GiorniAnno = 0
                                        dataPartenza = DataInizio
                                        importoInteressi = 0
                                        TotalePeriodo = 0

                                        For KK1 = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                                            If KK1 = CInt(Mid(DataCalcolo, 1, 4)) Then
                                                DataFine = par.FormattaData(DataCalcolo)
                                            Else
                                                DataFine = "31/12/" & KK1

                                            End If

                                            GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & KK1), CDate("31/12/" & KK1)) + 1

                                            Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                                            If KK1 < 1990 Then
                                                tasso = 5
                                            Else
                                                If Interessi.ContainsKey(KK1) = True Then
                                                    tasso = Interessi(KK1)
                                                End If
                                            End If

                                            TotalePeriodo = Format((((ImportoTassa * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                                            importoInteressi = importoInteressi + TotalePeriodo

                                            dataPartenza = KK1 + 1 & "0101"

                                        Next
                                    Else
                                        importoInteressi = 0
                                        TotalePeriodo = 0
                                    End If

                                End If

                                RECORD_TROVATI = RECORD_TROVATI + 1
                                par.cmd.CommandText = "SELECT * from siscom_mi.volture where data_decorrenza<='" & Format(Now, "yyyyMMdd") & "' and id_contratto=" & myReader("id")
                                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader2.HasRows = True Then
                                    SUBENTRI = "S"
                                Else
                                    SUBENTRI = "N"
                                End If
                                myReader2.Close()

                                Select Case par.IfNull(myReader("cod_tipologia_contr_loc"), "ERP")
                                    Case "ERP"
                                        TipologiaAbitativa = "02"
                                    Case "USD"
                                        TipologiaAbitativa = "02"
                                    Case Else
                                        If par.IfNull(myReader("cod_tipologia"), "AL") = "AL" Then
                                            TipologiaAbitativa = "02"
                                        Else
                                            TipologiaAbitativa = "02"
                                        End If
                                End Select

                                sr.WriteLine("BS" & myReader("ID").ToString.PadRight(14) & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX").ToString.PadLeft(3) & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4).ToString.PadLeft(4) & Mid(par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 1, 2).ToString.PadLeft(2) & Format(Val(par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000")), "000000").ToString.PadRight(6) & "000" & Mid(cmbMese.SelectedItem.Value, 1, 4) & Format((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, "000000000000000") & "F" & Agevolato & Format((ImportoTassa) * 100, "000000000000000") & Format((importoSanzioni) * 100, "000000000000000") & Format((importoInteressi) * 100, "000000000000000") & "0000000000000000 000                  0000000000000000000000000000000000000000000 " & SUBENTRI & TipologiaAbitativa & filler.PadLeft(312) & "A")
                                '#registrazione rapporto nel database#
                                Dim datatxtvaluta As String = Mid(txtValuta.Text, 7, 4) & Mid(txtValuta.Text, 4, 2) & Mid(txtValuta.Text, 1, 2)
                                Dim canone As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100
                                Dim canone2 As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0))

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                                                    & "(" & myReader("ID").ToString & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'112T','" & Mid(NomeFile, 9, 8) & "','" & datatxtvaluta & "'," & Replace(canone2, ",", ".") & "," & Replace(ImportoTassa, ",", ".") & "," & gioniDiff & "," & Replace(importoSanzioni, ",", ".") & "," & Replace(importoInteressi, ",", ".") & ",'" & NomeFile & ".zip')"
                                par.cmd.ExecuteNonQuery()
                                '##
                                Nominativo = ""
                                CodiceUtente = ""

                                par.cmd.CommandText = " select id, (CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS ""INTESTATARIO"" from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where soggetti_contrattuali.id_contratto=" & myReader("id") & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderX.Read Then
                                    Nominativo = par.IfNull(myReaderX("intestatario"), "")
                                    CodiceUtente = Format(par.IfNull(myReaderX("id"), "0"), "0000000000")
                                End If
                                myReaderX.Close()


                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 1, par.IfNull(myReader("COD_CONTRATTO"), ""), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 2, par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 3, par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 4, "", 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 5, CodiceUtente, 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 6, par.PulisciStrSql(Nominativo), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 7, par.IfNull(myReader("COD_UFFICIO_REG"), "XXX"), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 8, Left(cmbMese.SelectedItem.Value, 4), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 9, par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 10, par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000"), 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 11, "112T", 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 12, (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 13, (ImportoTassa) * 100, 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 14, gioniDiff, 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 15, importoSanzioni * 100, 12)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 16, importoInteressi * 100, 12)
                                KK = KK + 1
                            Loop
                            myReader.Close()

                            'scrivo il record di coda
                            sr.WriteLine("Z" & filler.PadLeft(14) & Format(RECORD_TROVATI, "000000000") & "000000000000000000" & "         " & filler.PadLeft(446) & "A")
                            sr.Close()

                            If RECORD_TROVATI = 0 Then
                                System.IO.File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "R" & "" & NomeFileIMP & "" & ContatoreFile & ".con"))

                            Else
                                ReDim Preserve ElencoFile(i)
                                ElencoFile(i) = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "R" & "" & NomeFileIMP & "" & ContatoreFile & ".con")
                                i = i + 1
                                ContatoreFile = ContatoreFile + 1
                            End If
                            RECORD_TROVATI = 0

                        Next

                        'RISOLUZIONI
                        Dim DataScadenzaOriginale As String = ""
                        Dim DataRisoluzione As String = ""
                        par.cmd.CommandText = "SELECT (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT,(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO,RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI  WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND data_stipula>'20091001' AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  AND SUBSTR(DATA_RICONSEGNA,1,4)='" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "' AND SUBSTR(DATA_RICONSEGNA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) & "' and cod_ufficio_reg='" & UfficioRegistro & "'" _
                            & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='113T')" _
                            & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='113T')" _
                            & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"

                        'par.cmd.CommandText = "SELECT (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT,(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO,RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI  WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND data_stipula>'20091001' AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  AND COD_CONTRATTO='0510110010200H02401' and cod_ufficio_reg='" & UfficioRegistro & "'" _
                        '   & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='113T')" _
                        '   & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='113T')" _
                        '   & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"
                        ''

                        myReader = par.cmd.ExecuteReader
                        RECORD_TROVATI = 0

                        Dim sr1 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "D" & "" & NomeFileIMP & ContatoreFile & ".con"), False, System.Text.Encoding.Default)


                        'scrivo il record di testa
                        sr1.WriteLine("A" & filler.PadLeft(14) & "LOCA0" & filler.PadLeft(2) & ContoCorrente.PadRight(16) & cflocatore.PadRight(16) & cflocatore.PadRight(16) & Format(CDate(txtValuta.Text), "ddMMyyyy") & filler.PadLeft(419) & "A")


                        ImportoTassa = ImportoRecesso

                        Do While myReader.Read
                            Agevolato = "N"
                            Agevolato = "N"
                            If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And (par.IfNull(myReader("DEST_USO"), "") = "0" Or par.IfNull(myReader("DEST_USO"), "") = "P") Then
                                Agevolato = "S"
                            End If
                            DataValuta = DateAdd("d", 29, CDate(par.FormattaData(myReader("data_riconsegna"))))
                            importoSanzioni = 0
                            importoInteressi = 0
                            gioniDiff = 0

                            If Format(DataValuta, "yyyyMMdd") < par.AggiustaData(txtValuta.Text) Then

                                gioniDiff = DateDiff(DateInterval.Day, DataValuta, CDate(txtValuta.Text))

                                If gioniDiff <= 90 Then
                                    importoSanzioni = Format((ImportoTassa * 3) / 100, "0.00")
                                End If

                                If gioniDiff >= 91 And gioniDiff <= 365 Then
                                    importoSanzioni = Format((ImportoTassa * 3.75) / 100, "0.00")
                                End If

                                If gioniDiff >= 366 Then
                                    importoSanzioni = Format((ImportoTassa * 30) / 100, "0.00")
                                End If

                                importoInteressi = 0
                                If importoSanzioni > 0 Then
                                    DataCalcolo = par.AggiustaData(txtValuta.Text)
                                    DataInizio = Format(DateAdd(DateInterval.Day, 1, DataValuta), "yyyyMMdd")

                                    Giorni = 0
                                    GiorniAnno = 0
                                    dataPartenza = DataInizio
                                    importoInteressi = 0
                                    TotalePeriodo = 0

                                    For KK1 = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                                        If KK1 = CInt(Mid(DataCalcolo, 1, 4)) Then
                                            DataFine = par.FormattaData(DataCalcolo)
                                        Else
                                            DataFine = "31/12/" & KK1

                                        End If

                                        GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & KK1), CDate("31/12/" & KK1)) + 1

                                        Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                                        If KK1 < 1990 Then
                                            tasso = 5
                                        Else
                                            If Interessi.ContainsKey(KK1) = True Then
                                                tasso = Interessi(KK1)
                                            End If
                                        End If

                                        TotalePeriodo = Format((((ImportoTassa * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                                        importoInteressi = importoInteressi + TotalePeriodo

                                        dataPartenza = KK1 + 1 & "0101"

                                    Next
                                Else
                                    importoInteressi = 0
                                    TotalePeriodo = 0
                                End If

                            End If

                            RECORD_TROVATI = RECORD_TROVATI + 1
                            par.cmd.CommandText = "SELECT * from siscom_mi.volture where data_decorrenza<='" & Format(Now, "yyyyMMdd") & "' and id_contratto=" & myReader("id")
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.HasRows = True Then
                                SUBENTRI = "S"
                            Else
                                SUBENTRI = "N"
                            End If


                            Select Case par.IfNull(myReader("cod_tipologia_contr_loc"), "ERP")
                                Case "ERP"
                                    TipologiaAbitativa = "02"
                                Case "USD"
                                    TipologiaAbitativa = "02"
                                Case Else
                                    If par.IfNull(myReader("cod_tipologia"), "AL") = "AL" Then
                                        TipologiaAbitativa = "02"
                                    Else
                                        TipologiaAbitativa = "02"
                                    End If
                            End Select

                            DataRisoluzione = Format(CDate(par.FormattaData(myReader("data_riconsegna"))), "ddMMyyyy")
                            sr1.WriteLine("BR" & myReader("ID").ToString.PadRight(14) & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX").ToString.PadLeft(3) & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4).ToString.PadLeft(4) & Mid(par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 1, 2).ToString.PadLeft(2) & Format(Val(par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000")), "000000").ToString.PadRight(6) & "000" & "0000" & Format((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("ADEGUAMENTO"), 0)) * 100, "000000000000000") & "F" & Agevolato & Format((ImportoTassa) * 100, "000000000000000") & Format((importoSanzioni) * 100, "000000000000000") & Format((importoInteressi) * 100, "000000000000000") & "0000000000000000 000                  " & DataRisoluzione & "00000000000000000000000000000000000 " & SUBENTRI & TipologiaAbitativa & filler.PadLeft(312) & "A")
                            Dim accatastato As String = ""
                            '#registrazione rapporto nel database#
                            Dim datatxtvaluta As String = Mid(txtValuta.Text, 7, 4) & Mid(txtValuta.Text, 4, 2) & Mid(txtValuta.Text, 1, 2)
                            Dim canone As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100
                            Dim canone2 As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0))
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,FILE_SCARICATO) VALUES " _
                                                & "(" & myReader("ID").ToString & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'113T','" & Mid(NomeFile, 9, 8) & "','" & datatxtvaluta & "'," & Replace(canone2, ",", ".") & "," & Replace(ImportoTassa, ",", ".") & "," & gioniDiff & "," & Replace(importoSanzioni, ",", ".") & "," & Replace(importoInteressi, ",", ".") & ",'" & NomeFile & ".zip')"
                            par.cmd.ExecuteNonQuery()
                            '##
                            'verifico se l'impianto è accatastato
                            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.*, siscom_mi.identificativi_catastali.* " _
                            & "FROM siscom_mi.UNITA_IMMOBILIARI, siscom_mi.UNITA_CONTRATTUALE, siscom_mi.RAPPORTI_UTENZA, siscom_mi.identificativi_catastali  " _
                            & "WHERE(UNITA_CONTRATTUALE.id_contratto = RAPPORTI_UTENZA.ID And UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita And UNITA_CONTRATTUALE.id_unita_principale Is NULL And RAPPORTI_UTENZA.ID =" & myReader("ID") & " And siscom_mi.identificativi_catastali.id = siscom_mi.unita_immobiliari.id_catastale)"
                            Dim myreader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            While myreader3.Read
                                accatastato = par.IfNull(myreader3("sub"), "")
                                If accatastato = "" Or accatastato = "-" Or accatastato = "*" Or accatastato = "00" Or accatastato = "***" Or accatastato = "ND" Or accatastato = "??" Or accatastato = "?" Or accatastato = "**" Or accatastato = "--" Or accatastato = "nd" Or accatastato = "0" Or accatastato = "00" Then
                                    'non accatastato
                                    sr1.WriteLine("IR" & myReader("ID").ToString.PadRight(17) & "S".PadRight(6) & "NN".PadRight(472) & "A")
                                Else
                                    'accatastato
                                    sr1.WriteLine("IR" & myReader("ID").ToString.PadRight(17) & "N" & par.IfNull(myReader("COD_COMUNE"), "XXX").ToString.PadRight(5) & "UI" & myreader3("FOGLIO").ToString.PadLeft(6) & myreader3("SUB").ToString.PadLeft(3) & myreader3("SUB").ToString.PadLeft(8) & filler.PadRight(453) & "A")
                                End If
                                record_trovati2 = record_trovati2 + 1
                            End While
                            'fine verifica

                            Nominativo = ""
                            CodiceUtente = ""

                            par.cmd.CommandText = " select id, (CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS ""INTESTATARIO"" from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where soggetti_contrattuali.id_contratto=" & myReader("id") & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderX.Read Then
                                Nominativo = par.IfNull(myReaderX("intestatario"), "")
                                CodiceUtente = Format(par.IfNull(myReaderX("id"), "0"), "0000000000")
                            End If
                            myReaderX.Close()


                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 1, par.IfNull(myReader("COD_CONTRATTO"), ""), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 2, par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 3, par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 4, par.FormattaData(par.IfNull(myReader("DATA_RICONSEGNA"), "")), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 5, CodiceUtente, 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 6, par.PulisciStrSql(Nominativo), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 7, par.IfNull(myReader("COD_UFFICIO_REG"), "XXX"), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 8, Left(cmbMese.SelectedItem.Value, 4), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 9, par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 10, par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000"), 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 11, "113T", 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 12, (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 13, (ImportoTassa) * 100, 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 14, gioniDiff, 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 15, importoSanzioni * 100, 12)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 16, importoInteressi * 100, 12)
                            KK = KK + 1

                        Loop
                        myReader.Close()
                        'scrivo il record di coda
                        sr1.WriteLine("Z" & filler.PadLeft(14) & Format(RECORD_TROVATI, "000000000") & Format(record_trovati2, "000000000000000000") & filler.PadLeft(455) & "A")
                        sr1.Close()

                        If RECORD_TROVATI = 0 Then
                            System.IO.File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "D" & "" & NomeFileIMP & ContatoreFile & ".con"))
                        Else
                            ReDim Preserve ElencoFile(i)
                            ElencoFile(i) = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "D" & "" & NomeFileIMP & ContatoreFile & ".con")
                            i = i + 1
                            ContatoreFile = ContatoreFile + 1
                        End If
                        RECORD_TROVATI = 0



                        ''CESSIONI

                        'Dim DataCessione As String = ""
                        ''par.cmd.CommandText = "SELECT RAPPORTI_UTENZA_CESSIONI.ID_INT,RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                        ''    & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                        ''    & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune " _
                        ''    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA_CESSIONI  " _
                        ''    & "WHERE RAPPORTI_UTENZA_CESSIONI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND data_stipula>'20091001' AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  AND SUBSTR(RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,1,4)='" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "' AND SUBSTR(RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) & "' and cod_ufficio_reg='" & UfficioRegistro & "'" _
                        ''    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='110T')" _
                        ''    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='110T')" _
                        ''    & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"

                        'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA_CESSIONI.ID_INT,RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                        '   & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                        '   & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune " _
                        '   & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA_CESSIONI  " _
                        '   & "WHERE RAPPORTI_UTENZA_CESSIONI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND data_stipula>'20091001' AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  AND cod_ufficio_reg='" & UfficioRegistro & "'" _
                        '   & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE COD_TRIBUTO='110T')" _
                        '   & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE COD_TRIBUTO='110T')" _
                        '   & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"


                        'myReader = par.cmd.ExecuteReader
                        'RECORD_TROVATI = 0

                        'sr1 = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "C" & "" & NomeFileIMP & ContatoreFile & ".con"), False, System.Text.Encoding.Default)


                        ''scrivo il record di testa
                        'sr1.WriteLine("A" & filler.PadLeft(14) & "LOCA0" & filler.PadLeft(2) & ContoCorrente.PadRight(16) & cflocatore.PadRight(16) & cflocatore.PadRight(16) & Format(CDate(txtValuta.Text), "ddMMyyyy") & filler.PadLeft(419) & "A")


                        'ImportoTassa = ImportoRecesso

                        'Do While myReader.Read
                        '    Agevolato = "N"
                        '    If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And (par.IfNull(myReader("DEST_USO"), "") = "0" Or par.IfNull(myReader("DEST_USO"), "") = "P") Then
                        '        Agevolato = "S"
                        '    End If
                        '    DataValuta = DateAdd("d", 29, CDate(par.FormattaData(myReader("DATA_CESSIONE"))))
                        '    importoSanzioni = 0
                        '    importoInteressi = 0
                        '    gioniDiff = 0

                        '    If Format(DataValuta, "yyyyMMdd") < par.AggiustaData(txtValuta.Text) Then

                        '        gioniDiff = DateDiff(DateInterval.Day, DataValuta, CDate(txtValuta.Text))

                        '        If gioniDiff <= 90 Then
                        '            importoSanzioni = Format((ImportoTassa * 3) / 100, "0.00")
                        '        End If

                        '        If gioniDiff >= 91 And gioniDiff <= 365 Then
                        '            importoSanzioni = Format((ImportoTassa * 3.75) / 100, "0.00")
                        '        End If

                        '        If gioniDiff >= 366 Then
                        '            importoSanzioni = Format((ImportoTassa * 30) / 100, "0.00")
                        '        End If

                        '        importoInteressi = 0
                        '        If importoSanzioni > 0 Then
                        '            DataCalcolo = par.AggiustaData(txtValuta.Text)
                        '            DataInizio = Format(DateAdd(DateInterval.Day, 1, DataValuta), "yyyyMMdd")

                        '            Giorni = 0
                        '            GiorniAnno = 0
                        '            dataPartenza = DataInizio
                        '            importoInteressi = 0
                        '            TotalePeriodo = 0

                        '            For KK = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                        '                If KK = CInt(Mid(DataCalcolo, 1, 4)) Then
                        '                    DataFine = par.FormattaData(DataCalcolo)
                        '                Else
                        '                    DataFine = "31/12/" & KK

                        '                End If

                        '                GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & KK), CDate("31/12/" & KK)) + 1

                        '                Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                        '                If KK < 1990 Then
                        '                    tasso = 5
                        '                Else
                        '                    If Interessi.ContainsKey(KK) = True Then
                        '                        tasso = Interessi(KK)
                        '                    End If
                        '                End If

                        '                TotalePeriodo = Format((((ImportoTassa * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                        '                importoInteressi = importoInteressi + TotalePeriodo

                        '                dataPartenza = KK + 1 & "0101"

                        '            Next
                        '        Else
                        '            importoInteressi = 0
                        '            TotalePeriodo = 0
                        '        End If

                        '    End If

                        '    RECORD_TROVATI = RECORD_TROVATI + 1
                        '    par.cmd.CommandText = "SELECT * from siscom_mi.volture where data_decorrenza<='" & Format(Now, "yyyyMMdd") & "' and id_contratto=" & myReader("id")
                        '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    If myReader2.HasRows = True Then
                        '        SUBENTRI = "S"
                        '    Else
                        '        SUBENTRI = "N"
                        '    End If


                        '    Select Case par.IfNull(myReader("cod_tipologia_contr_loc"), "ERP")
                        '        Case "ERP"
                        '            TipologiaAbitativa = "02"
                        '        Case "USD"
                        '            TipologiaAbitativa = "02"
                        '        Case Else
                        '            If par.IfNull(myReader("cod_tipologia"), "AL") = "AL" Then
                        '                TipologiaAbitativa = "02"
                        '            Else
                        '                TipologiaAbitativa = "02"
                        '            End If
                        '    End Select
                        '    DataScadenzaOriginale = "00000000"
                        '    DataRisoluzione = Format(CDate(par.FormattaData(myReader("DATA_CESSIONE"))), "ddMMyyyy")
                        '    DataScadenzaOriginale = Format(CDate(par.FormattaData(myReader("DATA_SCADENZA"))), "ddMMyyyy")
                        '    sr1.WriteLine("BC" & myReader("ID").ToString.PadRight(14) & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX").ToString.PadLeft(3) & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4).ToString.PadLeft(4) & Mid(par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 1, 2).ToString.PadLeft(2) & Format(Val(par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000")), "000000").ToString.PadRight(6) & "000" & "0000" & Format((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("ADEGUAMENTO"), 0)) * 100, "000000000000000") & "F" & Agevolato & Format((ImportoTassa) * 100, "000000000000000") & Format((importoSanzioni) * 100, "000000000000000") & Format((importoInteressi) * 100, "000000000000000") & "0000000000000000 000                  " & "00000000" & DataRisoluzione & DataScadenzaOriginale & "1000000000000000002SN" & TipologiaAbitativa & filler.PadLeft(312) & "A")

                        '    Dim NUOVOINT As String = "????????????????"

                        '    par.cmd.CommandText = "select * from siscom_mi.anagrafica WHERE id=" & myReader("ID_INT")
                        '    Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    If myReaderX1.Read Then
                        '        If par.IfNull(myReaderX1("COD_FISCALE"), "") <> "" Then
                        '            NUOVOINT = myReaderX1("COD_FISCALE")
                        '        End If
                        '        If par.IfNull(myReaderX1("PARTITA_IVA"), "") <> "" Then
                        '            NUOVOINT = myReaderX1("PARTITA_IVA") & "     "
                        '        End If
                        '    End If
                        '    myReaderX1.Close()

                        '    sr1.WriteLine("CC" & myReader("ID").ToString.PadRight(14) & NUOVOINT & filler.PadLeft(304) & filler.PadLeft(161) & "A")

                        '    Nominativo = ""
                        '    CodiceUtente = ""

                        '    par.cmd.CommandText = " select id, (CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS ""INTESTATARIO"" from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where soggetti_contrattuali.id_contratto=" & myReader("id") & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                        '    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    If myReaderX.Read Then
                        '        Nominativo = par.IfNull(myReaderX("intestatario"), "")
                        '        CodiceUtente = Format(par.IfNull(myReaderX("id"), "0"), "0000000000")
                        '    End If
                        '    myReaderX.Close()



                        '    Dim accatastato As String = ""
                        '    '#registrazione rapporto nel database#
                        '    Dim datatxtvaluta As String = Mid(txtValuta.Text, 7, 4) & Mid(txtValuta.Text, 4, 2) & Mid(txtValuta.Text, 1, 2)
                        '    Dim canone As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100
                        '    Dim canone2 As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0))
                        '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,FILE_SCARICATO) VALUES " _
                        '                        & "(" & myReader("ID").ToString & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'110T','" & Mid(NomeFile, 9, 8) & "','" & datatxtvaluta & "'," & Replace(canone2, ",", ".") & "," & Replace(ImportoTassa, ",", ".") & "," & gioniDiff & "," & Replace(importoSanzioni, ",", ".") & "," & Replace(importoInteressi, ",", ".") & ",'" & NomeFile & ".zip')"
                        '    par.cmd.ExecuteNonQuery()
                        '    '##
                        '    'verifico se l'impianto è accatastato
                        '    par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.*, siscom_mi.identificativi_catastali.* " _
                        '    & "FROM siscom_mi.UNITA_IMMOBILIARI, siscom_mi.UNITA_CONTRATTUALE, siscom_mi.RAPPORTI_UTENZA, siscom_mi.identificativi_catastali  " _
                        '    & "WHERE(UNITA_CONTRATTUALE.id_contratto = RAPPORTI_UTENZA.ID And UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita And UNITA_CONTRATTUALE.id_unita_principale Is NULL And RAPPORTI_UTENZA.ID =" & myReader("ID") & " And siscom_mi.identificativi_catastali.id = siscom_mi.unita_immobiliari.id_catastale)"
                        '    Dim myreader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    While myreader3.Read
                        '        accatastato = par.IfNull(myreader3("sub"), "")
                        '        If accatastato = "" Or accatastato = "-" Or accatastato = "*" Or accatastato = "00" Or accatastato = "***" Or accatastato = "ND" Or accatastato = "??" Or accatastato = "?" Or accatastato = "**" Or accatastato = "--" Or accatastato = "nd" Or accatastato = "0" Or accatastato = "00" Then
                        '            'non accatastato
                        '            sr1.WriteLine("IC" & myReader("ID").ToString.PadRight(17) & "S".PadRight(6) & "NN".PadRight(472) & "A")
                        '        Else
                        '            'accatastato

                        '            sr1.WriteLine("IC" & myReader("ID").ToString.PadRight(17) & "N" & par.IfNull(myReader("COD_COMUNE"), "XXX").ToString.PadRight(5) & "UI" & myreader3("FOGLIO").ToString.PadLeft(6) & myreader3("SUB").ToString.PadLeft(3) & myreader3("SUB").ToString.PadLeft(8) & filler.PadRight(453) & "A")
                        '        End If
                        '        record_trovati2 = record_trovati2 + 1
                        '    End While
                        '    'fine verifica



                        '    sSql = "INSERT INTO DETTAGLI  VALUES ('" & par.IfNull(myReader("COD_CONTRATTO"), "") & "','" & par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")) & "','" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "','" & par.FormattaData(par.IfNull(myReader("DATA_RICONSEGNA"), "")) & "','" & CodiceUtente & "','" & par.PulisciStrSql(Nominativo) & "','" & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX") & "','" & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4) & "','" & par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX") & "','" & par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000") & "','113T','" & (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("ADEGUAMENTO"), 0)) * 100 & "','" & (ImportoTassa) * 100 & "','" & gioniDiff & "','" & importoSanzioni * 100 & "','" & importoInteressi * 100 & "')"
                        '    cmd1.CommandText = sSql
                        '    cmd1.ExecuteNonQuery()
                        'Loop
                        'myReader.Close()
                        ''scrivo il record di coda
                        'sr1.WriteLine("Z" & filler.PadLeft(14) & Format(RECORD_TROVATI, "000000000") & Format(RECORD_TROVATI, "000000000") & Format(record_trovati2, "000000000") & filler.PadLeft(455) & "A")
                        'sr1.Close()

                        'If RECORD_TROVATI = 0 Then
                        '    System.IO.File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "C" & "" & NomeFileIMP & ContatoreFile & ".con"))
                        'Else
                        '    ReDim Preserve ElencoFile(i)
                        '    ElencoFile(i) = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "C" & "" & NomeFileIMP & ContatoreFile & ".con")
                        '    i = i + 1
                        '    ContatoreFile = ContatoreFile + 1
                        'End If
                        'RECORD_TROVATI = 0



                        'PROROGHE
                        Dim SU As Boolean = False
                        RECORD_TROVATI = 0
                        record_trovati2 = 0
                        errcfconta = 0

                        IndiceGiorni = 1
                        Select Case Mid(cmbMese.SelectedItem.Value, 5, 2)
                            Case "01", "03", "05", "07", "08", "10", "12"
                                GiorniMese = 31
                            Case "02"
                                GiorniMese = 28
                            Case "04", "06", "09", "11"
                                GiorniMese = 30
                            Case Else
                                GiorniMese = 30
                        End Select

                        For IndiceGiorni = 1 To GiorniMese  'fa un file per ogni giorno del mese

                            Dim sr2 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "P" & "" & NomeFileIMP & "" & ContatoreFile & ".con"), False, System.Text.Encoding.Default)

                            DataValuta = DateAdd("d", 29, CDate(IndiceGiorni & "/" & Mid(cmbMese.SelectedItem.Value, 5, 2) & "/" & Mid(cmbMese.SelectedItem.Value, 1, 4)))

                            'scrivo il record di testa
                            sr2.WriteLine("A" & filler.PadLeft(14) & "LOCA0" & filler.PadLeft(2) & ContoCorrente.PadRight(16) & cflocatore.PadRight(16) & cflocatore.PadRight(16) & Format(CDate(txtValuta.Text), "ddMMyyyy") & filler.PadLeft(419) & "A")

                            Dim StringaSql As String = ""
                            StringaSql = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                            & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                            & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune, (select distinct cf_piva  from siscom_mi.rapporti_utenza_imposte where (cod_tributo='107T' or cod_tributo='115T') AND siscom_mi.rapporti_utenza_imposte.id_contratto=siscom_mi.rapporti_utenza.ID) AS CF_PIVA from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI where  " _
                            & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND  data_stipula>'20091001' AND cod_ufficio_reg='" & UfficioRegistro _
                            & "' and SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND SUBSTR(DATA_DECORRENZA,7,2)='" & Format(IndiceGiorni, "00") _
                            & "' and SUBSTR(DATA_DECORRENZA,1,4)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                            & "' AND SUBSTR(DATA_DECORRENZA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                            & "' AND SUBSTR(DATA_SCADENZA_RINNOVO,1,6)>'" & cmbMese.SelectedItem.Value _
                            & "' AND (DURATA_ANNI+SUBSTR(DATA_DECORRENZA,1,4))='" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "' AND BOZZA=0" _
                            & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='114T')" _
                            & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='114T')" _
                            & " AND SISCOM_MI.RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"


                            par.cmd.CommandText = StringaSql
                            myReader = par.cmd.ExecuteReader()
                            Dim PROROGA As Boolean = False


                            While myReader.Read

                                Agevolato = "N"
                                If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And (par.IfNull(myReader("DEST_USO"), "") = "0" Or par.IfNull(myReader("DEST_USO"), "") = "P") Then
                                    Agevolato = "S"
                                End If

                                PROROGA = False
                                If par.IfNull(myReader("PROVENIENZA_ASS"), "") <> "7" Then
                                    'verifico se deve pagare la PROROGA del contratto


                                    If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "ERP" And par.IfNull(myReader("DATA_DECORRENZA"), "19990101") >= 20040101 Then
                                        If (CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)) - CInt(Mid(par.IfNull(myReader("DATA_DECORRENZA"), "2010"), 1, 4))) Mod CInt(myReader("DURATA_RINNOVO")) = 0 Then

                                            datainizioproroga = CInt(myReader("DATA_DECORRENZA")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000"))
                                            datafineproroga = par.AggiustaData(DateAdd("d", -1, CDate(par.FormattaData(CInt(myReader("DATA_DECORRENZA")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000")))))) ' CInt(myReader("DATA_DECORRENZA")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000"))

                                            PROROGA = True
                                            datainizioproroga = Mid(datainizioproroga, 7, 2) & Mid(datainizioproroga, 5, 2) & Mid(datainizioproroga, 1, 4)
                                            datafineproroga = Mid(datafineproroga, 7, 2) & Mid(datafineproroga, 5, 2) & Mid(datafineproroga, 1, 4)
                                        End If
                                    End If

                                    If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") <> "ERP" And par.IfNull(myReader("DURATA_RINNOVO"), 0) <> 0 Then
                                        If (CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)) - CInt(Mid(par.IfNull(myReader("DATA_DECORRENZA"), "2010"), 1, 4))) Mod CInt(myReader("DURATA_RINNOVO")) = 0 Then

                                            datainizioproroga = CInt(myReader("DATA_DECORRENZA")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000"))
                                            datafineproroga = par.AggiustaData(DateAdd("d", -1, CDate(par.FormattaData(CInt(myReader("DATA_DECORRENZA")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000")))))) ' CInt(myReader("DATA_DECORRENZA")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000")) + (CInt(Format(myReader("DURATA_RINNOVO"), "0000") & "0000"))

                                            PROROGA = True
                                            datainizioproroga = Mid(datainizioproroga, 7, 2) & Mid(datainizioproroga, 5, 2) & Mid(datainizioproroga, 1, 4)
                                            datafineproroga = Mid(datafineproroga, 7, 2) & Mid(datafineproroga, 5, 2) & Mid(datafineproroga, 1, 4)
                                        End If
                                    End If



                                    par.cmd.CommandText = "select tipologia_contratto_locazione.* from siscom_mi.tipologia_contratto_locazione where cod='" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & "'"
                                    Dim myReaderFF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderFF.Read Then
                                        If PROROGA = True Then
                                            'If par.SoluzioneUnica(CDbl(par.PuntiInVirgole((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)))), CInt(Format(myReader("DURATA_RINNOVO"), "0"))) <= 67 Then
                                                'SU = True
                                            'Else
                                                'SU = False
                                            'End If

                                            If SU = False Then
                                                If Agevolato = "N" Then
                                                    CanoneDaScrivere = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0))
                                                    importo112 = Format(((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)) / 100) * 2, "0")
                                                Else
                                                    CanoneDaScrivere = ((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)) - ((30 / 100) * (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0))))
                                                    importo112 = Format((((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)) - ((30 / 100) * (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0) + par.IfNull(myReader("adeguamento"), 0)))) / 100) * 2, "0")
                                                End If

                                                If importo112 < 67 Then
                                                    importo112 = 67
                                                End If
                                                tipopagamentoproroga = "P"
                                            Else
                                                'importo112 = par.SoluzioneUnica(CDbl(par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti) - ((30 / 100) * CDbl(par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)), CInt(myReader("durata_anni")))

                                                'If importo112 < 67 Then
                                                '    importo112 = 67
                                                'End If
                                                'tipopagamentoproroga = "T"
                                            End If
                                        End If
                                    End If
                                    myReaderFF.Close()

                                End If
                                If PROROGA = True Then
                                    ImportoTassa = importo112
                                    importoSanzioni = 0
                                    importoInteressi = 0
                                    gioniDiff = 0

                                    If Format(DataValuta, "yyyyMMdd") < par.AggiustaData(txtValuta.Text) Then

                                        gioniDiff = DateDiff(DateInterval.Day, DataValuta, CDate(txtValuta.Text))

                                        If gioniDiff <= 90 Then
                                            importoSanzioni = Format((ImportoTassa * 3) / 100, "0.00")
                                        End If

                                        If gioniDiff >= 91 And gioniDiff <= 365 Then
                                            importoSanzioni = Format((ImportoTassa * 3.75) / 100, "0.00")
                                        End If

                                        If gioniDiff >= 366 Then
                                            importoSanzioni = Format((ImportoTassa * 30) / 100, "0.00")
                                        End If


                                        importoInteressi = 0
                                        If importoSanzioni > 0 Then
                                            DataCalcolo = par.AggiustaData(txtValuta.Text)
                                            DataInizio = Format(DateAdd(DateInterval.Day, 1, DataValuta), "yyyyMMdd")

                                            Giorni = 0
                                            GiorniAnno = 0
                                            dataPartenza = DataInizio
                                            importoInteressi = 0
                                            TotalePeriodo = 0

                                            For KK1 = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                                                If KK1 = CInt(Mid(DataCalcolo, 1, 4)) Then
                                                    DataFine = par.FormattaData(DataCalcolo)
                                                Else
                                                    DataFine = "31/12/" & KK1

                                                End If

                                                GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & KK1), CDate("31/12/" & KK1)) + 1

                                                Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                                                If KK1 < 1990 Then
                                                    tasso = 5
                                                Else
                                                    If Interessi.ContainsKey(KK1) = True Then
                                                        tasso = Interessi(KK1)
                                                    End If
                                                End If

                                                TotalePeriodo = Format((((ImportoTassa * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                                                importoInteressi = importoInteressi + TotalePeriodo

                                                dataPartenza = KK1 + 1 & "0101"

                                            Next
                                        Else
                                            importoInteressi = 0
                                            TotalePeriodo = 0
                                        End If

                                    End If

                                    RECORD_TROVATI = RECORD_TROVATI + 1

                                    par.cmd.CommandText = "SELECT * from siscom_mi.volture where data_decorrenza<='" & Format(Now, "yyyyMMdd") & "' and id_contratto=" & myReader("id")
                                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReader2.HasRows = True Then
                                        SUBENTRI = "S"
                                    Else
                                        SUBENTRI = "N"
                                    End If

                                    Select Case par.IfNull(myReader("cod_tipologia_contr_loc"), "ERP")
                                        Case "ERP"
                                            TipologiaAbitativa = "02"
                                        Case "USD"
                                            TipologiaAbitativa = "02"
                                        Case Else
                                            If par.IfNull(myReader("cod_tipologia"), "AL") = "AL" Then
                                                TipologiaAbitativa = "02"
                                            Else
                                                TipologiaAbitativa = "02"
                                            End If
                                    End Select
                                    myReader2.Close()
                                    'verifico se l'impianto è accatastato
                                    accatastato2 = False
                                    par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.*, siscom_mi.identificativi_catastali.* " _
                                    & "FROM siscom_mi.UNITA_IMMOBILIARI, siscom_mi.UNITA_CONTRATTUALE, siscom_mi.RAPPORTI_UTENZA, siscom_mi.identificativi_catastali  " _
                                    & "WHERE (UNITA_CONTRATTUALE.id_contratto = RAPPORTI_UTENZA.ID And UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita And UNITA_CONTRATTUALE.id_unita_principale Is NULL And RAPPORTI_UTENZA.ID =" & myReader("ID") & " And siscom_mi.identificativi_catastali.id = siscom_mi.unita_immobiliari.id_catastale)"
                                    Dim myreader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myreader4.Read Then
                                        accatastato = par.IfNull(myreader4("sub"), "")
                                        If accatastato = "" Or accatastato = "-" Or accatastato = "*" Or accatastato = "00" Or accatastato = "***" Or accatastato = "ND" Or accatastato = "??" Or accatastato = "?" Or accatastato = "**" Or accatastato = "--" Or accatastato = "nd" Or accatastato = "0" Or accatastato = "00" Then
                                            'non accatastato
                                            accatastato2 = False
                                        Else
                                            'accatastato
                                            accatastato2 = True
                                        End If
                                    End If

                                    'fine verifica
                                    Dim serie_registrazione As String = par.IfNull(myReader("SERIE_REGISTRAZIONE"), "")
                                    Dim cf_piva As String = par.IfNull(myReader("CF_PIVA"), "")
                                    If serie_registrazione = "3T" And accatastato2 = True Then
                                        'GESTIONE CASO 1: contratti con serie 3T ed immobile accatastato
                                        sr2.WriteLine("BP" & myReader("ID").ToString.PadRight(14) & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX").ToString.PadRight(3) & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4).ToString.PadRight(4) & Mid(par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 1, 2).ToString.PadRight(2) & Format(Val(par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000")), "000000").ToString.PadRight(6) & "000" & "0000" & Format((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, "000000000000000") & "F" & Agevolato & Format((importo112) * 100, "000000000000000") & Format((importoSanzioni) * 100, "000000000000000") & Format((importoInteressi) * 100, "000000000000000") & datainizioproroga & datafineproroga & tipopagamentoproroga & "0" & TipologiaAbitativa.PadRight(19) & "N" & "0000000000000000000000000000000000000000000 N".PadRight(359) & "A")
                                        sr2.WriteLine("IP" & myReader("ID").ToString.PadRight(14) & filler.PadRight(3) & "N" & par.IfNull(myReader("COD_COMUNE"), "XXX").ToString.PadRight(5) & "UI".PadRight(4) & myreader4("FOGLIO").ToString.PadRight(4) & myreader4("numero").ToString.PadRight(9) & myreader4("SUB").ToString.PadRight(4) & filler.PadRight(451) & "A")
                                        'FINE GESTIONE CASO 1
                                    ElseIf (serie_registrazione = "3" Or serie_registrazione = "3A") And accatastato2 = True Then
                                        'GESTIONE CASO 2: contratti con serie 3 e 3A e immobile accatastato
                                        sr2.WriteLine("BP" & myReader("ID").ToString.PadRight(14) & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX").ToString.PadRight(3) & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4).ToString.PadRight(4) & Mid(par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 1, 2).ToString.PadRight(2) & Format(Val(par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000")), "000000").ToString.PadRight(6) & "000" & "0000" & Format((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, "000000000000000") & "F" & Agevolato & Format((importo112) * 100, "000000000000000") & Format((importoSanzioni) * 100, "000000000000000") & Format((importoInteressi) * 100, "000000000000000") & datainizioproroga & datafineproroga & tipopagamentoproroga & "0" & TipologiaAbitativa & par.IfNull(myReader("CF_PIVA"), "????????????????").ToString.PadRight(16) & "B" & "N" & "0000000000000000000000000000000000000000000 N".PadRight(359) & "A")
                                        sr2.WriteLine("IP" & myReader("ID").ToString.PadRight(14) & filler.PadRight(3) & "N" & par.IfNull(myReader("COD_COMUNE"), "XXX").ToString.PadRight(5) & "UI".PadRight(4) & myreader4("FOGLIO").ToString.PadRight(4) & myreader4("numero").ToString.PadRight(9) & myreader4("SUB").ToString.PadRight(4) & filler.PadRight(451) & "A")
                                        'FINE GESTIONE CASO 2
                                    ElseIf accatastato2 = False Then
                                        'GESTIONE CASO 3: immobile non accatastato
                                        sr2.WriteLine("BP" & myReader("ID").ToString.PadRight(14) & par.IfNull(myReader("COD_UFFICIO_REG"), "XXX").ToString.PadRight(3) & Mid(par.IfNull(myReader("DATA_REG"), "1111"), 1, 4).ToString.PadRight(4) & Mid(par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 1, 2).ToString.PadRight(2) & Format(Val(par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000")), "000000").ToString.PadRight(6) & "000" & "0000" & Format((par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, "000000000000000") & "F" & Agevolato & Format((importo112) * 100, "000000000000000") & Format((importoSanzioni) * 100, "000000000000000") & Format((importoInteressi) * 100, "000000000000000") & datainizioproroga & datafineproroga & tipopagamentoproroga & "0" & TipologiaAbitativa.PadRight(19) & "N" & "0000000000000000000000000000000000000000000 N".PadRight(359) & "A")
                                        sr2.WriteLine("IP" & myReader("ID").ToString.PadRight(14) & filler.PadRight(3) & "S".PadRight(6) & "NN".PadRight(7) & filler.PadRight(472) & "A")
                                        'FINE GESTIONE CASO 3
                                    End If
                                    myreader4.Close()
                                    '#registrazione rapporto nel database#
                                    Dim datatxtvaluta As String = Mid(txtValuta.Text, 7, 4) & Mid(txtValuta.Text, 4, 2) & Mid(txtValuta.Text, 1, 2)
                                    Dim canone As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100
                                    Dim canone2 As String = (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0))
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                                                        & "(" & myReader("ID").ToString & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'114T','" & Mid(NomeFile, 9, 8) & "','" & datatxtvaluta & "'," & Replace(canone2, ",", ".") & "," & Replace(ImportoTassa, ",", ".") & "," & gioniDiff & "," & Replace(importoSanzioni, ",", ".") & "," & Replace(importoInteressi, ",", ".") & ",'" & NomeFile & ".zip')"
                                    par.cmd.ExecuteNonQuery()
                                    '##

                                    If cf_piva = "" And errcfconta = 0 And RECORD_TROVATI > 0 Then
                                        'errcf = errcf & UfficioRegistro & "_" & NomeFile & "_PROROGHE_" & IndiceGiorni & ".con "
                                        errcf = errcf & "P" & "" & NomeFile & "" & ContatoreFile & ".con "
                                        errcfconta = 1
                                        outerrcf = 1
                                    End If

                                    Nominativo = ""
                                    CodiceUtente = ""

                                    par.cmd.CommandText = " select id, (CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS ""INTESTATARIO"" from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where soggetti_contrattuali.id_contratto=" & myReader("id") & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderX.Read Then
                                        Nominativo = par.IfNull(myReaderX("intestatario"), "")
                                        CodiceUtente = Format(par.IfNull(myReaderX("id"), "0"), "0000000000")
                                    End If
                                    myReaderX.Close()


                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 1, par.IfNull(myReader("COD_CONTRATTO"), ""), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 2, par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 3, par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 4, par.FormattaData(par.IfNull(myReader("DATA_RICONSEGNA"), "")), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 5, CodiceUtente, 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 6, par.PulisciStrSql(Nominativo), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 7, par.IfNull(myReader("COD_UFFICIO_REG"), "XXX"), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 8, Left(cmbMese.SelectedItem.Value, 4), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 9, par.IfNull(myReader("SERIE_REGISTRAZIONE"), "XX"), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 10, par.IfNull(myReader("NUM_REGISTRAZIONE"), "000000"), 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 11, "114T", 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 12, (par.IfNull(myReader("imp_canone_iniziale"), 0) + par.IfNull(myReader("istat"), 0)) * 100, 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 13, (ImportoTassa) * 100, 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 14, gioniDiff, 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 15, importoSanzioni * 100, 12)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, KK, 16, importoInteressi * 100, 12)
                                    KK = KK + 1


                                    If SU = True Then
                                        par.cmd.CommandText = "update siscom_mi.rapporti_utenza set versamento_tr='U' where cod_contratto='" & par.IfNull(myReader("COD_CONTRATTO"), "") & "'"
                                    Else
                                        par.cmd.CommandText = "update siscom_mi.rapporti_utenza set versamento_tr='A' where cod_contratto='" & par.IfNull(myReader("COD_CONTRATTO"), "") & "'"
                                    End If

                                    par.cmd.ExecuteNonQuery()

                                End If

                            End While
                            myReader.Close()

                            'scrivo il record di coda
                            sr2.WriteLine("Z" & filler.PadLeft(14) & Format(RECORD_TROVATI, "000000000") & Format(RECORD_TROVATI, "000000000000000000") & filler.PadRight(455) & "A")
                            sr2.Close()

                            If RECORD_TROVATI = 0 Then
                                System.IO.File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "P" & "" & NomeFileIMP & "" & ContatoreFile & ".con"))
                            Else
                                ReDim Preserve ElencoFile(i)
                                ElencoFile(i) = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & "P" & "" & NomeFileIMP & "" & ContatoreFile & ".con")
                                i = i + 1
                                ContatoreFile = ContatoreFile + 1
                            End If
                            RECORD_TROVATI = 0
                        Next

                    Loop
                    myReader1.Close()

                    .CloseFile()
                End With

                If i > 0 Then

                    KK = 0
                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String

                    zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFile & ".zip"

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    '
                    Dim strFile As String

                    For KK = 0 To i - 1
                        strFile = ElencoFile(KK)
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
                        File.Delete(strFile)
                    Next
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                    Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Response.Write("<script>location.href='ElencoImposte.aspx';</script>")
                Else

                    System.IO.File.Delete(NomeFilexls)
                    Response.Write("<script>alert('Nessun file elaborato!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                End If

            Catch ex1 As Oracle.DataAccess.Client.OracleException
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                par.myTrans.Rollback()
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:Versamento Imposte - " & ex1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Catch ex As Exception
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                par.myTrans.Rollback()
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:Versamento Imposte - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        Else
            Response.Write("<script>alert('Specificare la valuta del pagamento, che deve essere successiva o uguale alla data odierna!');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
