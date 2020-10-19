Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoProposteDec
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Carica()
            CaricaDati()
        End If
    End Sub

    Public Property lIdAU() As Long
        Get
            If Not (ViewState("par_lIdAU") Is Nothing) Then
                Return CLng(ViewState("par_lIdAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdAU") = value
        End Set
    End Property

    Public Property Provenienza() As Long
        Get
            If Not (ViewState("par_Provenienza") Is Nothing) Then
                Return CLng(ViewState("par_Provenienza"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Provenienza") = value
        End Set
    End Property

    Public Property AnnoAu() As Long
        Get
            If Not (ViewState("par_AnnoAu") Is Nothing) Then
                Return CLng(ViewState("par_AnnoAu"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_AnnoAu") = value
        End Set
    End Property

    Public Property AnnoInizio() As Long
        Get
            If Not (ViewState("par_AnnoInizio") Is Nothing) Then
                Return CLng(ViewState("par_AnnoInizio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_AnnoInizio") = value
        End Set
    End Property

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI ORDER BY ID DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lIdAU = myReader("id")
                Provenienza = myReader("id_tipo_provenienza")
                AnnoAu = myReader("ANNO_AU")
                Label1.Text = "Anno " & AnnoAu & " Redditi " & myReader("ANNO_isee")
                AnnoInizio = CLng(Mid(myReader("inizio_canone"), 1, 4))
            End If
            myReader.Close()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Function


    Private Function CaricaDati()
        Try
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow
            Dim DECADENZA As Boolean = False
            Dim TIPOLOGIA As String = ""
            Dim LIMITE_PATRIMONIO As Double = 0
            Dim ISEE_DEC As String = ""
            Dim PATRIMONIO_DEC As String = ""

            PAR.OracleConn.Open()
            par.SettaCommand(par)


            dt.Columns.Add("TIPO_DECADENZA")
            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("SOTTO_AREA")
            dt.Columns.Add("ANNOTAZIONI")
            dt.Columns.Add("ISEE")
            dt.Columns.Add("ISE")
            dt.Columns.Add("ISP")
            dt.Columns.Add("ISR")
            dt.Columns.Add("PSE")
            dt.Columns.Add("VSE")
            dt.Columns.Add("ISEE_27")

            dt.Columns.Add("REDD_MOBILIARI")
            dt.Columns.Add("REDD_IMMOBILIARI")
            dt.Columns.Add("REDD_MOBILIARI_FR")
            dt.Columns.Add("REDD_IMMOBILIARI_FR")
            dt.Columns.Add("LIMITE_PATRIMONIO")
            dt.Columns.Add("PATRIMONIO_DEC")
            dt.Columns.Add("ISEE_DECADENZA")

            dt.Columns.Add("COGNOME")
            dt.Columns.Add("NOME")
            dt.Columns.Add("INDIRIZZO")
            dt.Columns.Add("CIVICO")
            dt.Columns.Add("CAP")
            dt.Columns.Add("LOCALITA")

            PAR.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.PATRIMONIO_DEC,UTENZA_DICHIARAZIONI.ISEE_DEC,INDIRIZZI.DESCRIZIONE AS INDIRIZZO,INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA, ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,CANONI_EC.* FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.CANONI_EC,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID (+)=CANONI_EC.ID_DICHIARAZIONE AND  INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=CANONI_EC.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=CANONI_EC.ID_CONTRATTO AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND TIPO_PROVENIENZA=" & Provenienza & " AND nvl(competenza,2014)=" & AnnoInizio & " AND (sotto_area='D5' OR SOTTO_AREA='D6' OR ANNOTAZIONI LIKE '%ISEE>35.000/%' OR PATRIMONIO_SUP='1' OR DECADENZA_ALL_ADEGUATO='1' OR DECADENZA_VAL_ICI='1') order by anagrafica.cognome asc,anagrafica.nome asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                TIPOLOGIA = ""
                DECADENZA = False
                LIMITE_PATRIMONIO = 0
                ISEE_DEC = ""
                PATRIMONIO_DEC = ""

                If myReader("SOTTO_AREA") = "D5" Or myReader("SOTTO_AREA") = "D6" Then
                    ROW = dt.NewRow()
                    If myReader("SOTTO_AREA") = "D5" Then
                        ROW.Item("TIPO_DECADENZA") = "DICHIARAZIONE INCOMPLETA"
                    Else
                        ROW.Item("TIPO_DECADENZA") = "DICHIARAZIONE AU NON PRESENTATA"
                    End If
                    ROW.Item("COD_CONTRATTO") = myReader("COD_CONTRATTO")
                    ROW.Item("SOTTO_AREA") = myReader("SOTTO_AREA")
                    ROW.Item("ANNOTAZIONI") = myReader("ANNOTAZIONI")
                    ROW.Item("ISEE") = ""
                    ROW.Item("ISE") = ""
                    ROW.Item("ISP") = ""
                    ROW.Item("ISR") = ""
                    ROW.Item("PSE") = ""
                    ROW.Item("VSE") = ""
                    ROW.Item("ISEE_27") = myReader("ISEE_27")

                    ROW.Item("REDD_MOBILIARI") = ""
                    ROW.Item("REDD_IMMOBILIARI") = ""
                    ROW.Item("REDD_MOBILIARI_FR") = ""
                    ROW.Item("REDD_IMMOBILIARI_FR") = ""
                    ROW.Item("LIMITE_PATRIMONIO") = ""
                    ROW.Item("PATRIMONIO_DEC") = ""
                    ROW.Item("ISEE_DECADENZA") = ""
                    ROW.Item("COGNOME") = myReader("COGNOME")
                    ROW.Item("NOME") = myReader("NOME")
                    ROW.Item("INDIRIZZO") = myReader("INDIRIZZO")
                    ROW.Item("CIVICO") = myReader("CIVICO")
                    ROW.Item("CAP") = myReader("CAP")
                    ROW.Item("LOCALITA") = myReader("LOCALITA")
                    dt.Rows.Add(ROW)
                Else
                    If PAR.IfNull(myReader("id_dichiarazione"), "-1") <> "-1" Then
                        PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & myReader("ID_CONTRATTO") & " AND TIPO_PROVENIENZA<>" & Provenienza & " order by data_calcolo desc"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReader1.Read Then
                            'ISEE>35000
                            If InStr(PAR.IfNull(myReader("ANNOTAZIONI"), "  "), "ISEE>35.000/") > 0 And myReader("ID_AREA_ECONOMICA") = "4" Then
                                If InStr(PAR.IfNull(myReader1("ANNOTAZIONI"), "  "), "ISEE>35.000/") > 0 And myReader1("ID_AREA_ECONOMICA") = "4" Then
                                    TIPOLOGIA = "ISEE>35.000/"
                                    DECADENZA = True
                                End If
                            End If

                            'PATRIMONIO SUPERATO
                            If PAR.IfNull(myReader("PATRIMONIO_SUP"), "") = "1" Then
                                If PAR.IfNull(myReader1("PATRIMONIO_SUP"), "") = "1" Then
                                    TIPOLOGIA = TIPOLOGIA & "LIMITE PATRIMONIALE SUP./"
                                    DECADENZA = True
                                End If
                            End If
                            'ALLOGGIO ADEGUATO
                            If PAR.IfNull(myReader("decadenza_all_adeguato"), "") = "1" Then
                                If PAR.IfNull(myReader1("decadenza_all_adeguato"), "") = "1" Then
                                    TIPOLOGIA = TIPOLOGIA & "ALLOGGIO ADEG./"
                                    DECADENZA = True
                                End If
                            End If

                            'ALLOGGIO ADEGUATO ICI
                            If PAR.IfNull(myReader("decadenza_val_ici"), "") = "1" Then
                                If PAR.IfNull(myReader1("decadenza_val_ici"), "") = "1" Then
                                    TIPOLOGIA = TIPOLOGIA & "VALORE ICI/IMU ALLOGGIO/"
                                    DECADENZA = True
                                End If
                            End If
                            If DECADENZA = True Then
                                ROW = dt.NewRow()
                                ROW.Item("TIPO_DECADENZA") = TIPOLOGIA
                                ROW.Item("COD_CONTRATTO") = myReader("COD_CONTRATTO")
                                ROW.Item("SOTTO_AREA") = myReader("SOTTO_AREA")
                                ROW.Item("ANNOTAZIONI") = myReader("ANNOTAZIONI")
                                ROW.Item("ISEE") = myReader("ISEE")
                                ROW.Item("ISE") = myReader("ISE")
                                ROW.Item("ISP") = myReader("ISP")
                                ROW.Item("ISR") = myReader("ISR")
                                ROW.Item("PSE") = myReader("PSE")
                                ROW.Item("VSE") = myReader("VSE")
                                ROW.Item("ISEE_27") = myReader("ISEE_27")
                                ROW.Item("REDD_MOBILIARI") = myReader("REDD_MOBILIARI")
                                ROW.Item("REDD_IMMOBILIARI") = myReader("REDD_IMMOBILIARI")

                                If myReader("REDD_MOBILIARI") - 25000 > 0 Then
                                    ROW.Item("REDD_MOBILIARI_FR") = myReader("REDD_MOBILIARI") - 25000
                                Else
                                    ROW.Item("REDD_MOBILIARI_FR") = "0"
                                End If
                                If myReader("REDD_IMMOBILIARI") - 25000 > 0 Then
                                    ROW.Item("REDD_IMMOBILIARI_FR") = myReader("REDD_IMMOBILIARI") - 25000
                                Else
                                    ROW.Item("REDD_IMMOBILIARI_FR") = "0"
                                End If

                                LIMITE_PATRIMONIO = (16000 + (6000 * myReader("VSE"))) * 3
                                ROW.Item("LIMITE_PATRIMONIO") = LIMITE_PATRIMONIO

                                ROW.Item("ISEE_DECADENZA") = PAR.IfNull(myReader("ISEE_DEC"), "")
                                ROW.Item("PATRIMONIO_DEC") = PAR.IfNull(myReader("PATRIMONIO_DEC"), "")
                                ROW.Item("COGNOME") = myReader("COGNOME")
                                ROW.Item("NOME") = myReader("NOME")
                                ROW.Item("INDIRIZZO") = myReader("INDIRIZZO")
                                ROW.Item("CIVICO") = myReader("CIVICO")
                                ROW.Item("CAP") = myReader("CAP")
                                ROW.Item("LOCALITA") = myReader("LOCALITA")
                                dt.Rows.Add(ROW)
                            End If
                        End If
                        myReader1.Close()
                    End If
                End If
            Loop
            myReader.Close()

            HttpContext.Current.Session.Add("ElencoRisultati", dt)
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            BindGrid()

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
        'SELECT * FROM CANONI_EC WHERE TIPO_PROVENIENZA=7 AND competenza=" & annoinizio & " AND (sotto_area='D5' OR SOTTO_AREA='D6' OR ANNOTAZIONI='ISEE>35.000/' OR PATRIMONIO_SUP='1' OR DECADENZA_ALL_ADEGUATO='1' OR DECADENZA_VAL_ICI='1')
    End Function

    Private Sub BindGrid()

        Dim dt As New System.Data.DataTable
        dt = CType(HttpContext.Current.Session.Item("ElencoRisultati"), Data.DataTable)
        DataGridRateEmesse.DataSource = dt
        DataGridRateEmesse.DataBind()



    End Sub

    Protected Sub DataGridRateEmesse_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridRateEmesse.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridRateEmesse.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Export()
    End Sub

    Function Export()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try

            Dim dt As New System.Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("ElencoRisultati"), Data.DataTable)
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            If Not IsNothing(dt) Then
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




                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "TIPO_DECADENZA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD_CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "SOTTO_AREA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ANNOTAZIONI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "ISEE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "ISE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "ISP")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ISR")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "PSE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "VSE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "ISEE_27")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "REDD_MOBILIARI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "REDD_IMMOBILIARI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "REDD_MOBILIARI_FR")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "REDD_IMMOBILIARI_FR")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "LIMITE_PATRIMONIO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "PATRIMONIO_DEC")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "ISEE_DECADENZA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "COGNOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "NOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "INDIRIZZO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "CIVICO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "CAP")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "LOCALITA")
                        K = 2
                        For Each row In dt.Rows

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("TIPO_DECADENZA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("SOTTO_AREA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("ANNOTAZIONI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("ISEE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("ISE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("ISP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("ISR"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("PSE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("VSE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("ISEE_27"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("REDD_MOBILIARI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("REDD_IMMOBILIARI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(dt.Rows(i).Item("REDD_MOBILIARI_FR"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(dt.Rows(i).Item("REDD_IMMOBILIARI_FR"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(dt.Rows(i).Item("LIMITE_PATRIMONIO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(dt.Rows(i).Item("PATRIMONIO_DEC"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.IfNull(dt.Rows(i).Item("ISEE_DECADENZA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.IfNull(dt.Rows(i).Item("COGNOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.IfNull(dt.Rows(i).Item("NOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.IfNull(dt.Rows(i).Item("INDIRIZZO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.IfNull(dt.Rows(i).Item("CIVICO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.IfNull(dt.Rows(i).Item("CAP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.IfNull(dt.Rows(i).Item("LOCALITA"), ""))
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


                If File.Exists(Server.MapPath("~\FileTemp\") & FileCSV & ".ZIP") Then
                    Response.Write("<script>window.open('DownLoad.aspx?C=1&F=" & par.Cripta(FileCSV) & "','Export','');</script>")

                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else

            End If

        Catch ex As Exception
            HttpContext.Current.Session.Remove("ElencoOriginaleDT")
            HttpContext.Current.Session.Remove("ElencoDT")
            HttpContext.Current.Session.Remove("ElencoRegistroDT")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function


    Private Function CalcolaISEEDecadenza(ByVal lIdDichiarazione As Long, ByRef ISEE_DECADENZA As String, ByRef PATRIMONIO_DEC As String)
        Dim DETRAZIONI As Long


        Dim INV_100_CON As Integer
        Dim INV_100_NO As Integer
        Dim INV_66_99 As Integer
        Dim TOT_COMPONENTI As Integer
        Dim REDDITO_COMPLESSIVO As Double
        Dim TOT_SPESE As Long
        Dim DETRAZIONI_FRAGILE As Long
        Dim DETRAZIONI_FR As Long

        Dim MOBILI As Double
        Dim TASSO_RENDIMENTO As Double
        Dim FIGURATIVO_MOBILI As Double
        Dim TOTALE_ISEE_ERP As Double
        Dim IMMOBILI As Long
        Dim MUTUI As Long
        Dim IMMOBILI_RESIDENZA As Long
        Dim MUTUI_RESIDENZA As Long
        Dim TOTALE_PATRIMONIO_ISEE_ERP As Double
        Dim TOTALE_IMMOBILI As Long
        Dim LIMITE_PATRIMONIO As Double

        Dim ISR_ERP As Double
        Dim ISP_ERP As Double
        Dim ISE_ERP As Double
        Dim VSE As Double
        Dim ISEE_ERP As Double
        Dim ESCLUSIONE As String


        Dim PARAMETRO_MINORI As Double

        Dim MINORI As Integer
        Dim adulti As Integer


        Dim limite_isee As Long


        MINORI = 0
        adulti = 0
        ISR_ERP = 0
        ISP_ERP = 0
        ISE_ERP = 0
        VSE = 0
        TOT_COMPONENTI = 0
        DETRAZIONI = 0
        REDDITO_COMPLESSIVO = 0
        TOT_SPESE = 0
        DETRAZIONI_FRAGILE = 0
        DETRAZIONI_FR = 0
        ISEE_ERP = 0
        MOBILI = 0
        FIGURATIVO_MOBILI = 0
        IMMOBILI = 0
        MUTUI = 0
        IMMOBILI_RESIDENZA = 0
        MUTUI_RESIDENZA = 0
        TOTALE_IMMOBILI = 0
        TOTALE_ISEE_ERP = 0
        TOTALE_PATRIMONIO_ISEE_ERP = 0
        LIMITE_PATRIMONIO = 0
        ESCLUSIONE = ""
        Dim data_pg As String = ""

        PAR.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        If myReader.Read() Then
            INV_100_CON = PAR.IfNull(myReader("N_INV_100_CON"), 0)
            INV_100_NO = PAR.IfNull(myReader("N_INV_100_SENZA"), 0)
            INV_66_99 = PAR.IfNull(myReader("N_INV_100_66"), 0)
            DATA_PG = myReader("DATA_PG")
        End If
        myReader.Close()

        PAR.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
        myReader = PAR.cmd.ExecuteReader()
        TOT_COMPONENTI = 0

        Do While myReader.Read()

            If PAR.RicavaEtaChiusura(myReader("DATA_NASCITA"), data_pg) >= 15 Then
                If PAR.RicavaEtaChiusura(myReader("DATA_NASCITA"), data_pg) >= 18 Then
                    adulti = adulti + 1
                End If
            Else
                MINORI = MINORI + 1
            End If

            DETRAZIONI = 0

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + PAR.IfNull(myReader1("REDDITO_IRPEF"), 0) + PAR.IfNull(myReader1("PROV_AGRARI"), 0)
            End While
            myReader1.Close()

            DETRAZIONI_FRAGILE = 0
            PAR.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
            myReader1 = PAR.cmd.ExecuteReader()
            If myReader1.HasRows Then
                While myReader1.Read
                    DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + PAR.IfNull(myReader1("IMPORTO"), 0)
                    TOT_SPESE = TOT_SPESE + PAR.IfNull(myReader1("IMPORTO"), 0)
                    If DETRAZIONI_FRAGILE > 10000 Then
                        DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                    Else
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    End If

                End While
                myReader1.Close()
            Else
                If PAR.IfNull(myReader("indennita_acc"), 0) = "1" Then
                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    TOT_SPESE = TOT_SPESE + 10000
                End If
                myReader1.Close()
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
            myReader1 = PAR.cmd.ExecuteReader()
            While myReader1.Read
                MOBILI = MOBILI + PAR.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
            myReader1 = PAR.cmd.ExecuteReader()
            While myReader1.Read
                If PAR.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                    IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + PAR.IfNull(myReader1("VALORE"), 0)
                    MUTUI_RESIDENZA = MUTUI_RESIDENZA + PAR.IfNull(myReader1("MUTUO"), 0)
                Else
                    IMMOBILI = IMMOBILI + PAR.IfNull(myReader1("VALORE"), 0)
                    MUTUI = MUTUI + PAR.IfNull(myReader1("MUTUO"), 0)
                End If
            End While
            myReader1.Close()
            TOT_COMPONENTI = TOT_COMPONENTI + 1
        Loop
        myReader.Close()

        MOBILI = MOBILI - 25000
        If MOBILI < 0 Then MOBILI = 0



        DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

        FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165
        ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
        If ISEE_ERP < 0 Then
            ISEE_ERP = 0
        End If
        ISR_ERP = ISEE_ERP
        ISEE_ERP = 0

        TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA) - 25000
        If TOTALE_IMMOBILI < 0 Then TOTALE_IMMOBILI = 0

        TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

        ISP_ERP = TOTALE_ISEE_ERP

        TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)

        Dim PARAMETRO As Double
        Select Case TOT_COMPONENTI
            Case 1
                PARAMETRO = 1
            Case 2
                PARAMETRO = (138 / 100)
            Case 3
                PARAMETRO = (167 / 100)
            Case 4
                PARAMETRO = (190 / 100)
            Case 5
                PARAMETRO = (211 / 100)
            Case Else
                PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
        End Select

        PARAMETRO_MINORI = 0
        VSE = PARAMETRO
        If adulti >= 2 Then
            VSE = VSE - (MINORI * (1 / 10))
            PARAMETRO_MINORI = (MINORI * (1 / 10))
        End If

        LIMITE_PATRIMONIO = 16000 + (6000 * VSE)

        ISE_ERP = ISR_ERP + ISP_ERP

        ISEE_ERP = ISE_ERP / VSE

        ISEE_DECADENZA = ISE_ERP
        PATRIMONIO_DEC = TOTALE_PATRIMONIO_ISEE_ERP

        

    End Function

End Class
