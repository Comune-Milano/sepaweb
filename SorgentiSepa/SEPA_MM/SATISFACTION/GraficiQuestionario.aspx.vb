Imports System.Data.OleDb
Imports System.Math

Partial Class SATISFACTION_GraficiQuestionario
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            CaricaDomande("0")
        End If
        Chart1.Visible = False
        Chart2.Visible = False
        Label5.Text = ""
        'disegnaGrafici()

    End Sub

    Protected Sub apriConnessione()
        Try
            If par.OracleConn.State = 0 Then
                par.OracleConn.Open()
            End If
            par.cmd = par.OracleConn.CreateCommand
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('GraficiQuestionario.aspx');</script>")
        End Try

    End Sub

    Protected Sub chiudiConnessione()
        Try
            If par.OracleConn.State = 1 Then

                par.cmd.Dispose()
                par.OracleConn.Close()

            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('GraficiQuestionario.aspx');</script>")
        End Try

    End Sub

    Protected Sub creaGraficoNessunaDomanda(ByVal risposta As String, ByVal valore As String)
        apriConnessione()
        Dim query As String = ""
        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
            Else
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND ("
            End If

            query = query & "PU_REGOLARITA='" & risposta & "' OR "
            query = query & "PU_QUALITA='" & risposta & "' OR "
            query = query & "PU_CORTESIA='" & risposta & "' OR "
            query = query & "PU_IGIENE='" & risposta & "' OR "
            query = query & "PU_PARTI_COMUNI='" & risposta & "' OR "
            query = query & "PU_RIF_INGOMBRANTI='" & risposta & "' OR "
            query = query & "PO_REGOLARITA='" & risposta & "' OR "
            query = query & "PO_QUALITA='" & risposta & "' OR "
            query = query & "PO_CORTESIA='" & risposta & "' OR "
            query = query & "PO_POSTA='" & risposta & "' OR "
            query = query & "PO_INF_COMPLETE='" & risposta & "' OR "
            query = query & "RI_REGOLARITA='" & risposta & "' OR "
            query = query & "RI_QUALITA='" & risposta & "' OR "
            query = query & "RI_CORTESIA='" & risposta & "' OR "
            query = query & "RI_TEMPERATURA='" & risposta & "' OR "
            query = query & "RI_GUASTI='" & risposta & "' OR "
            query = query & "RI_RIS_GUASTI='" & risposta & "' OR "
            query = query & "VE_REGOLARITA='" & risposta & "' OR "
            query = query & "VE_QUALITA='" & risposta & "' OR "
            query = query & "VE_CORTESIA='" & risposta & "' OR "
            query = query & "VE_TEMPESTIVITA='" & risposta & "' OR "
            query = query & "VE_SMALTIMENTO_RIF='" & risposta & "' OR "

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = query & "VE_RUMORE='" & risposta & "' "
            Else
                query = query & "VE_RUMORE='" & risposta & "') "
            End If
            par.cmd.CommandText = query

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
            Else
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND ("
            End If

            query = query & "PU_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "PU_QUALITA_VAL='" & valore & "' OR "
            query = query & "PU_CORTESIA_VAL='" & valore & "' OR "
            query = query & "PU_IGIENE_VAL='" & valore & "' OR "
            query = query & "PU_PARTI_COMUNI_VAL='" & valore & "' OR "
            query = query & "PU_RIF_INGOMBRANTI_VAL='" & valore & "' OR "
            query = query & "PO_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "PO_QUALITA_VAL='" & valore & "' OR "
            query = query & "PO_CORTESIA_VAL='" & valore & "' OR "
            query = query & "PO_POSTA_VAL='" & valore & "' OR "
            query = query & "PO_INF_COMPLETE_VAL='" & valore & "' OR "
            query = query & "RI_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "RI_QUALITA_VAL='" & valore & "' OR "
            query = query & "RI_CORTESIA_VAL='" & valore & "' OR "
            query = query & "RI_TEMPERATURA_VAL='" & valore & "' OR "
            query = query & "RI_GUASTI_VAL='" & valore & "' OR "
            query = query & "RI_RIS_GUASTI_VAL='" & valore & "' OR "
            query = query & "VE_REGOLARITA_VAL='" & valore & "' OR "
            query = query & "VE_QUALITA_VAL='" & valore & "' OR "
            query = query & "VE_CORTESIA_VAL='" & valore & "' OR "
            query = query & "VE_TEMPESTIVITA_VAL='" & valore & "' OR "
            query = query & "VE_SMALTIMENTO_RIF_VAL='" & valore & "' OR "

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = query & "VE_RUMORE_VAL='" & valore & "' "
            Else
                query = query & "VE_RUMORE_VAL='" & valore & "') "
            End If
            par.cmd.CommandText = query

        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
            Else
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND ("
            End If

            query = query & "(PU_REGOLARITA='" & risposta & "' AND PU_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(PU_QUALITA='" & risposta & "' AND PU_QUALITA_VAL='" & valore & "') OR "
            query = query & "(PU_CORTESIA='" & risposta & "' AND PU_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PU_IGIENE='" & risposta & "' AND PU_IGIENE_VAL='" & valore & "') OR "
            query = query & "(PU_PARTI_COMUNI='" & risposta & "' AND PU_PARTI_COMUNI_VAL='" & valore & "') OR "
            query = query & "(PU_RIF_INGOMBRANTI='" & risposta & "' AND PU_RIF_INGOMBRANTI_VAL='" & valore & "') OR "
            query = query & "(PO_REGOLARITA='" & risposta & "' AND PO_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(PO_QUALITA='" & risposta & "' AND PO_QUALITA_VAL='" & valore & "') OR "
            query = query & "(PO_CORTESIA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PO_POSTA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(PO_INF_COMPLETE='" & risposta & "' AND PO_INF_COMPLETE_VAL='" & valore & "') OR "
            query = query & "(RI_REGOLARITA='" & risposta & "' AND RI_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(RI_QUALITA='" & risposta & "' AND RI_QUALITA_VAL='" & valore & "') OR "
            query = query & "(RI_CORTESIA='" & risposta & "' AND RI_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(RI_TEMPERATURA='" & risposta & "' AND RI_TEMPERATURA_VAL='" & valore & "') OR "
            query = query & "(RI_GUASTI='" & risposta & "' AND RI_GUASTI_VAL='" & valore & "') OR "
            query = query & "(RI_RIS_GUASTI='" & risposta & "' AND RI_RIS_GUASTI_VAL='" & valore & "') OR "
            query = query & "(VE_REGOLARITA='" & risposta & "' AND VE_REGOLARITA_VAL='" & valore & "') OR "
            query = query & "(VE_QUALITA='" & risposta & "' AND VE_QUALITA_VAL='" & valore & "') OR "
            query = query & "(VE_CORTESIA='" & risposta & "' AND VE_CORTESIA_VAL='" & valore & "') OR "
            query = query & "(VE_TEMPESTIVITA='" & risposta & "' AND VE_TEMPESTIVITA_VAL='" & valore & "') OR "
            query = query & "(VE_SMALTIMENTO_RIF='" & risposta & "' AND VE_SMALTIMENTO_RIF_VAL='" & valore & "') OR "

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = query & "(VE_RUMORE='" & risposta & "' AND VE_RUMORE_VAL='" & valore & "') "
            Else
                query = query & "(VE_RUMORE='" & risposta & "' AND VE_RUMORE_VAL='" & valore & "')) "
            End If

            par.cmd.CommandText = query

        End If
        Dim areaGrafico As String
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("PU_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_IGIENE")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_PARTI_COMUNI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_RIF_INGOMBRANTI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_TEMPERATURA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_GUASTI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_RIS_GUASTI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_INF_COMPLETE")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_POSTA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_RUMORE")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_TEMPESTIVITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_SMALTIMENTO_RIF")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("PU_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_IGIENE_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_PARTI_COMUNI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_RIF_INGOMBRANTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_TEMPERATURA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_GUASTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_RIS_GUASTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_INF_COMPLETE_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_POSTA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_RUMORE_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_TEMPESTIVITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_SMALTIMENTO_RIF_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim numero As String
        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True
        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 23))

    End Sub

    Protected Sub creaGraficoRegolarita(ByVal risposta As String, ByVal valore As String)

        apriConnessione()
        Dim query As String = ""
        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "PU_REGOLARITA='" & risposta & "' OR "
                query = query & "PO_REGOLARITA='" & risposta & "' OR "
                query = query & "RI_REGOLARITA='" & risposta & "' OR "
                query = query & "VE_REGOLARITA='" & risposta & "'"
            Else
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND("
                query = query & "PU_REGOLARITA='" & risposta & "' OR "
                query = query & "PO_REGOLARITA='" & risposta & "' OR "
                query = query & "RI_REGOLARITA='" & risposta & "' OR "
                query = query & "VE_REGOLARITA='" & risposta & "')"
            End If

            par.cmd.CommandText = query

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "PU_REGOLARITA_VAL='" & valore & "' OR "
                query = query & "PO_REGOLARITA_VAL='" & valore & "' OR "
                query = query & "RI_REGOLARITA_VAL='" & valore & "' OR "
                query = query & "VE_REGOLARITA_VAL='" & valore & "'"
            Else
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND("
                query = query & "PU_REGOLARITA_VAL='" & valore & "' OR "
                query = query & "PO_REGOLARITA_VAL='" & valore & "' OR "
                query = query & "RI_REGOLARITA_VAL='" & valore & "' OR "
                query = query & "VE_REGOLARITA_VAL='" & valore & "')"
            End If

            par.cmd.CommandText = query
        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "(PU_REGOLARITA='" & risposta & "' AND PU_REGOLARITA_VAL='" & valore & "') OR "
                query = query & "(PO_REGOLARITA='" & risposta & "' AND PO_REGOLARITA_VAL='" & valore & "') OR "
                query = query & "(RI_REGOLARITA='" & risposta & "' AND RI_REGOLARITA_VAL='" & valore & "') OR "
                query = query & "(VE_REGOLARITA='" & risposta & "' AND VE_REGOLARITA_VAL='" & valore & "') "
            Else
                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND("
                query = query & "(PU_REGOLARITA='" & risposta & "' AND PU_REGOLARITA_VAL='" & valore & "') OR "
                query = query & "(PO_REGOLARITA='" & risposta & "' AND PO_REGOLARITA_VAL='" & valore & "') OR "
                query = query & "(RI_REGOLARITA='" & risposta & "' AND RI_REGOLARITA_VAL='" & valore & "') OR "
                query = query & "(VE_REGOLARITA='" & risposta & "' AND VE_REGOLARITA_VAL='" & valore & "')) "
            End If

            par.cmd.CommandText = query

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("PU_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("PU_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case myReader("PO_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case myReader("VE_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While

        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"
        Dim numero As String
        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 4))

    End Sub

    Protected Sub creaGraficoQualita(ByVal risposta As String, ByVal valore As String)

        apriConnessione()

        Dim query As String = ""

        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "PU_QUALITA='" & risposta & "' OR "
                query = query & "PO_QUALITA='" & risposta & "' OR "
                query = query & "RI_QUALITA='" & risposta & "' OR "
                query = query & "VE_QUALITA='" & risposta & "'"

            Else

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
                query = query & "PU_QUALITA='" & risposta & "' OR "
                query = query & "PO_QUALITA='" & risposta & "' OR "
                query = query & "RI_QUALITA='" & risposta & "' OR "
                query = query & "VE_QUALITA='" & risposta & "')"

            End If

            par.cmd.CommandText = query

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "PU_QUALITA_VAL='" & valore & "' OR "
                query = query & "PO_QUALITA_VAL='" & valore & "' OR "
                query = query & "RI_QUALITA_VAL='" & valore & "' OR "
                query = query & "VE_QUALITA_VAL='" & valore & "'"

            Else

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
                query = query & "PU_QUALITA_VAL='" & valore & "' OR "
                query = query & "PO_QUALITA_VAL='" & valore & "' OR "
                query = query & "RI_QUALITA_VAL='" & valore & "' OR "
                query = query & "VE_QUALITA_VAL='" & valore & "')"

            End If

            par.cmd.CommandText = query

        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "(PU_QUALITA='" & risposta & "' AND PU_QUALITA_VAL='" & valore & "') OR "
                query = query & "(PO_QUALITA='" & risposta & "' AND PO_QUALITA_VAL='" & valore & "') OR "
                query = query & "(RI_QUALITA='" & risposta & "' AND RI_QUALITA_VAL='" & valore & "') OR "
                query = query & "(VE_QUALITA='" & risposta & "' AND VE_QUALITA_VAL='" & valore & "') "

            Else

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
                query = query & "(PU_QUALITA='" & risposta & "' AND PU_QUALITA_VAL='" & valore & "') OR "
                query = query & "(PO_QUALITA='" & risposta & "' AND PO_QUALITA_VAL='" & valore & "') OR "
                query = query & "(RI_QUALITA='" & risposta & "' AND RI_QUALITA_VAL='" & valore & "') OR "
                query = query & "(VE_QUALITA='" & risposta & "' AND VE_QUALITA_VAL='" & valore & "')) "

            End If

            par.cmd.CommandText = query

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("PU_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("PU_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case myReader("PO_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case myReader("VE_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

        End While

        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim numero As String
        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 4))

    End Sub

    Protected Sub creaGraficoCortesia(ByVal risposta As String, ByVal valore As String)

        apriConnessione()

        Dim query As String = ""
        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "PU_CORTESIA='" & risposta & "' OR "
                query = query & "PO_CORTESIA='" & risposta & "' OR "
                query = query & "RI_CORTESIA='" & risposta & "' OR "
                query = query & "VE_CORTESIA='" & risposta & "'"

            Else

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
                query = query & "PU_CORTESIA='" & risposta & "' OR "
                query = query & "PO_CORTESIA='" & risposta & "' OR "
                query = query & "RI_CORTESIA='" & risposta & "' OR "
                query = query & "VE_CORTESIA='" & risposta & "')"

            End If

            par.cmd.CommandText = query
        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "PU_CORTESIA_VAL='" & valore & "' OR "
                query = query & "PO_CORTESIA_VAL='" & valore & "' OR "
                query = query & "RI_CORTESIA_VAL='" & valore & "' OR "
                query = query & "VE_CORTESIA_VAL='" & valore & "'"

            Else

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
                query = query & "PU_CORTESIA_VAL='" & valore & "' OR "
                query = query & "PO_CORTESIA_VAL='" & valore & "' OR "
                query = query & "RI_CORTESIA_VAL='" & valore & "' OR "
                query = query & "VE_CORTESIA_VAL='" & valore & "')"

            End If

            par.cmd.CommandText = query
        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE "
                query = query & "(PU_CORTESIA='" & risposta & "' AND PU_CORTESIA_VAL='" & valore & "') OR "
                query = query & "(PO_CORTESIA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
                query = query & "(RI_CORTESIA='" & risposta & "' AND RI_CORTESIA_VAL='" & valore & "') OR "
                query = query & "(VE_CORTESIA='" & risposta & "' AND VE_CORTESIA_VAL='" & valore & "') "

            Else

                query = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
                query = query & "(PU_CORTESIA='" & risposta & "' AND PU_CORTESIA_VAL='" & valore & "') OR "
                query = query & "(PO_CORTESIA='" & risposta & "' AND PO_CORTESIA_VAL='" & valore & "') OR "
                query = query & "(RI_CORTESIA='" & risposta & "' AND RI_CORTESIA_VAL='" & valore & "') OR "
                query = query & "(VE_CORTESIA='" & risposta & "' AND VE_CORTESIA_VAL='" & valore & "')) "

            End If

            par.cmd.CommandText = query
        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("PU_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("PU_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


            Select Case myReader("PO_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

        End While

        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"
        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"
        Dim numero As String
        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 4))

    End Sub

    Protected Sub creaGraficoConRisposta(ByVal campoRic As String, ByVal num As Integer)
        Dim areaGrafico As String

        Dim campo As String = campoRic

        Dim campoQuery As String
        If Right(campo, 4) = "_VAL" Then
            campoQuery = Mid(campo, 1, Len(campo) - 4)
        Else
            campoQuery = campo
        End If
        apriConnessione()

        If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE " & campoQuery & "='" & ddlRisposta.SelectedValue & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND " & campoQuery & "='" & ddlRisposta.SelectedValue & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        End If

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0
        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))

            somma = somma + myReader(1)
            i = i + 1
        End While

        chiudiConnessione()

        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100
                Chart1.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.Transparent
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True

        Else
            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100

                Chart2.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.Transparent
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)

    End Sub

    Protected Sub creaGraficoConValore(ByVal campoRic As String, ByVal num As Integer)

        Dim areaGrafico As String
        Dim campo As String = campoRic
        Dim campoQuery As String
        If Not Right(campo, 4) = "_VAL" Then
            campoQuery = campo & "_VAL"
        Else
            campoQuery = campo
        End If
        apriConnessione()

        If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE " & campoQuery & "='" & ddlValore.SelectedValue & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND " & campoQuery & "='" & ddlValore.SelectedValue & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        End If

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0

        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))
            somma = somma + myReader(1)
            i = i + 1
        End While

        chiudiConnessione()

        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100
                Chart1.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.Transparent
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True

        Else
            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100
                Chart2.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.Transparent
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)

    End Sub

    Protected Sub creaGraficoConValoreRisposta(ByVal campoRic As String, ByVal num As Integer)
        Dim areaGrafico As String
        Dim campo As String = campoRic
        Dim campoQueryValore As String
        Dim campoQueryRisposta As String
        If Not Right(campo, 4) = "_VAL" Then
            campoQueryValore = campo & "_VAL"
        Else
            campoQueryValore = campo
        End If

        If Right(campo, 4) = "_VAL" Then
            campoQueryRisposta = Mid(campo, 1, Len(campo) - 4)
        Else
            campoQueryRisposta = campo
        End If

        apriConnessione()
        If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE " & campoQueryValore & "='" & ddlValore.SelectedValue & "' AND " & campoQueryRisposta & "='" & ddlRisposta.SelectedValue & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND " & campoQueryValore & "='" & ddlValore.SelectedValue & "' AND " & campoQueryRisposta & "='" & ddlRisposta.SelectedValue & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        End If

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0
        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))

            somma = somma + myReader(1)
            i = i + 1
        End While
        chiudiConnessione()

        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100

                Chart1.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.Transparent
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True

        Else

            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100

                Chart2.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.Transparent
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)

    End Sub

    Protected Sub creaGrafico(ByVal campoRic As String, ByVal num As Integer)
        Dim areaGrafico As String
        Dim campo As String = campoRic

        apriConnessione()
        If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        Else
            par.cmd.CommandText = "SELECT (" & campo & "),COUNT(" & campo & ") FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' GROUP BY " & campo & " ORDER BY " & campo & " ASC"
        End If

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        Dim i As Integer = 1
        Dim vettoreNum As New System.Collections.ArrayList
        Dim vettoreLbl As New System.Collections.ArrayList
        vettoreNum.Clear()
        vettoreLbl.Clear()
        Dim somma As Double = 0
        While myReader.Read
            vettoreLbl.Add(myReader(0))
            vettoreNum.Add(myReader(1))

            somma = somma + myReader(1)
            i = i + 1
        End While
        chiudiConnessione()

        If num = 1 Then
            areaGrafico = "ChartArea1"
            Chart1.Series.Add(campo)
            Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart1.Series(campo).LabelForeColor = Drawing.Color.White
            Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart1.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100
                Chart1.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart1.Titles.Add("TITOLO")
            Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart1.BackColor = Drawing.Color.Transparent
            Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart1.Series(campo)("DrawingStyle") = "Cylinder"
            Chart1.Series(campo).ChartArea = areaGrafico
            Chart1.Legends.Add("Legenda")
            Chart1.Visible = True
        Else
            areaGrafico = "ChartArea2"
            Chart2.Series.Add(campo)
            Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Chart2.Series(campo).LabelForeColor = Drawing.Color.White
            Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
            Chart2.Series(campo)("BarLabelStyle") = "Center"
            Dim numero As String
            For j As Integer = 0 To vettoreNum.Count - 1
                numero = vettoreNum(j) / somma * 100
                Chart2.Series(campo).Points.Add(numero).AxisLabel = vettoreLbl(j)
            Next
            'Chart2.Titles.Add("TITOLO")
            Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
            Chart2.BackColor = Drawing.Color.Transparent
            Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
            Chart2.Series(campo)("DrawingStyle") = "Cylinder"
            Chart2.Series(campo).ChartArea = areaGrafico
            Chart2.Legends.Add("Legenda")
            Chart2.Visible = True

        End If

        Label5.Text = "Numero di questionari coinvolti: " & CStr(somma)


    End Sub

    Protected Sub creaGraficoServiziPulizia(ByVal risposta As String, ByVal valore As String)
        apriConnessione()

        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE PU_REGOLARITA='" & risposta & "' OR PU_QUALITA='" & risposta & "' OR PU_CORTESIA='" & risposta & "' OR PU_PARTI_COMUNI='" & risposta & "' OR PU_IGIENE='" & risposta & "' OR PU_RIF_INGOMBRANTI='" & risposta & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND (PU_REGOLARITA='" & risposta & "' OR PU_QUALITA='" & risposta & "' OR PU_CORTESIA='" & risposta & "' OR PU_PARTI_COMUNI='" & risposta & "' OR PU_IGIENE='" & risposta & "' OR PU_RIF_INGOMBRANTI='" & risposta & "')"
            End If

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE PU_REGOLARITA_VAL='" & valore & "' OR PU_QUALITA_VAL='" & valore & "' OR PU_CORTESIA_VAL='" & valore & "' OR PU_PARTI_COMUNI_VAL='" & valore & "' OR PU_IGIENE_VAL='" & valore & "' OR PU_RIF_INGOMBRANTI_VAL='" & valore & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND (PU_REGOLARITA_VAL='" & valore & "' OR PU_QUALITA_VAL='" & valore & "' OR PU_CORTESIA_VAL='" & valore & "' OR PU_PARTI_COMUNI_VAL='" & valore & "' OR PU_IGIENE_VAL='" & valore & "' OR PU_RIF_INGOMBRANTI_VAL='" & valore & "')"
            End If

        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE (PU_REGOLARITA_VAL='" & valore & "' AND PU_REGOLARITA='" & risposta & "') OR (PU_QUALITA_VAL='" & valore & "' AND PU_QUALITA='" & risposta & "') OR (PU_CORTESIA_VAL='" & valore & "' AND PU_CORTESIA='" & risposta & "') OR (PU_IGIENE_VAL='" & valore & "' AND PU_IGIENE='" & risposta & "') OR (PU_PARTI_COMUNI_VAL='" & valore & "' AND PU_PARTI_COMUNI='" & risposta & "') OR (PU_RIF_INGOMBRANTI_VAL='" & valore & "' AND PU_RIF_INGOMBRANTI='" & risposta & "')"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND ((PU_REGOLARITA_VAL='" & valore & "' AND PU_REGOLARITA='" & risposta & "') OR (PU_QUALITA_VAL='" & valore & "' AND PU_QUALITA='" & risposta & "') OR (PU_CORTESIA_VAL='" & valore & "' AND PU_CORTESIA='" & risposta & "') OR (PU_IGIENE_VAL='" & valore & "' AND PU_IGIENE='" & risposta & "') OR (PU_PARTI_COMUNI_VAL='" & valore & "' AND PU_PARTI_COMUNI='" & risposta & "') OR (PU_RIF_INGOMBRANTI_VAL='" & valore & "' AND PU_RIF_INGOMBRANTI='" & risposta & "'))"
            End If

        End If

        'Label6.Text = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE (PU_REGOLARITA_VAL='" & valore & "' AND PU_REGOLARITA='" & risposta & "') OR (PU_QUALITA_VAL='" & valore & "' AND PU_QUALITA='" & risposta & "') OR (PU_CORTESIA_VAL='" & valore & "' AND PU_CORTESIA='" & risposta & "') OR (PU_IGIENE_VAL='" & valore & "' AND PU_IGIENE='" & risposta & "') OR (PU_PARTI_COMUNI_VAL='" & valore & "' AND PU_PARTI_COMUNI='" & risposta & "') OR (PU_RIF_INGOMBRANTI_VAL='" & valore & "' AND PU_RIF_INGOMBRANTI='" & risposta & "')"
        Dim areaGrafico As String
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("PU_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_CORTESIA")

                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_IGIENE")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_PARTI_COMUNI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PU_RIF_INGOMBRANTI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select


            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("PU_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_IGIENE_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_PARTI_COMUNI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PU_RIF_INGOMBRANTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di pulizia"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim numero As String
        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True
        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 6))
    End Sub

    Protected Sub creaGraficoServiziRiscaldamento(ByVal risposta As String, ByVal valore As String)
        apriConnessione()

        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE RI_REGOLARITA='" & risposta & "' OR RI_QUALITA='" & risposta & "' OR RI_CORTESIA='" & risposta & "' OR RI_TEMPERATURA='" & risposta & "' OR RI_GUASTI='" & risposta & "' OR RI_RIS_GUASTI='" & risposta & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND (RI_REGOLARITA='" & risposta & "' OR RI_QUALITA='" & risposta & "' OR RI_CORTESIA='" & risposta & "' OR RI_TEMPERATURA='" & risposta & "' OR RI_GUASTI='" & risposta & "' OR RI_RIS_GUASTI='" & risposta & "')"
            End If

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE RI_REGOLARITA_VAL='" & valore & "' OR RI_QUALITA_VAL='" & valore & "' OR RI_CORTESIA_VAL='" & valore & "' OR RI_TEMPERATURA_VAL='" & valore & "' OR RI_GUASTI_VAL='" & valore & "' OR RI_RIS_GUASTI_VAL='" & valore & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND (RI_REGOLARITA_VAL='" & valore & "' OR RI_QUALITA_VAL='" & valore & "' OR RI_CORTESIA_VAL='" & valore & "' OR RI_TEMPERATURA_VAL='" & valore & "' OR RI_GUASTI_VAL='" & valore & "' OR RI_RIS_GUASTI_VAL='" & valore & "')"
            End If

        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE (RI_REGOLARITA_VAL='" & valore & "' AND RI_REGOLARITA='" & risposta & "') OR (RI_QUALITA_VAL='" & valore & "' AND RI_QUALITA='" & risposta & "') OR (RI_CORTESIA_VAL='" & valore & "' AND RI_CORTESIA='" & risposta & "') OR (RI_TEMPERATURA_VAL='" & valore & "' AND RI_TEMPERATURA='" & risposta & "') OR (RI_GUASTI_VAL='" & valore & "' AND RI_GUASTI='" & risposta & "') OR (RI_RIS_GUASTI_VAL='" & valore & "' AND RI_RIS_GUASTI='" & risposta & "')"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND ((RI_REGOLARITA_VAL='" & valore & "' AND RI_REGOLARITA='" & risposta & "') OR (RI_QUALITA_VAL='" & valore & "' AND RI_QUALITA='" & risposta & "') OR (RI_CORTESIA_VAL='" & valore & "' AND RI_CORTESIA='" & risposta & "') OR (RI_TEMPERATURA_VAL='" & valore & "' AND RI_TEMPERATURA='" & risposta & "') OR (RI_GUASTI_VAL='" & valore & "' AND RI_GUASTI='" & risposta & "') OR (RI_RIS_GUASTI_VAL='" & valore & "' AND RI_RIS_GUASTI='" & risposta & "'))"
            End If

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("RI_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_TEMPERATURA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_GUASTI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("RI_RIS_GUASTI")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select


            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("RI_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_TEMPERATURA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_GUASTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_RIS_GUASTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di riscaldamento"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim numero As String
        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True

        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 6))

    End Sub

    Protected Sub creaGraficoServiziPortierato(ByVal risposta As String, ByVal valore As String)
        apriConnessione()

        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE PO_REGOLARITA='" & risposta & "' OR PO_QUALITA='" & risposta & "' OR PO_CORTESIA='" & risposta & "' OR PO_INF_COMPLETE='" & risposta & "' OR PO_POSTA='" & risposta & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND (PO_REGOLARITA='" & risposta & "' OR PO_QUALITA='" & risposta & "' OR PO_CORTESIA='" & risposta & "' OR PO_INF_COMPLETE='" & risposta & "' OR PO_POSTA='" & risposta & "')"
            End If

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE PO_REGOLARITA_VAL='" & valore & "' OR PO_QUALITA_VAL='" & valore & "' OR PO_CORTESIA_VAL='" & valore & "' OR PO_INF_COMPLETE_VAL='" & valore & "' OR PO_POSTA_VAL='" & valore & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND (PO_REGOLARITA_VAL='" & valore & "' OR PO_QUALITA_VAL='" & valore & "' OR PO_CORTESIA_VAL='" & valore & "' OR PO_INF_COMPLETE_VAL='" & valore & "' OR PO_POSTA_VAL='" & valore & "')"
            End If

        Else

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE (PO_REGOLARITA_VAL='" & valore & "' AND PO_REGOLARITA='" & risposta & "') OR (PO_QUALITA_VAL='" & valore & "' AND PO_QUALITA='" & risposta & "') OR (PO_CORTESIA_VAL='" & valore & "' AND PO_CORTESIA='" & risposta & "') OR (PO_INF_COMPLETE_VAL='" & valore & "' AND PO_INF_COMPLETE='" & risposta & "') OR (PO_POSTA_VAL='" & valore & "' AND PO_POSTA='" & risposta & "') "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND ((PO_REGOLARITA_VAL='" & valore & "' AND PO_REGOLARITA='" & risposta & "') OR (PO_QUALITA_VAL='" & valore & "' AND PO_QUALITA='" & risposta & "') OR (PO_CORTESIA_VAL='" & valore & "' AND PO_CORTESIA='" & risposta & "') OR (PO_INF_COMPLETE_VAL='" & valore & "' AND PO_INF_COMPLETE='" & risposta & "') OR (PO_POSTA_VAL='" & valore & "' AND PO_POSTA='" & risposta & "'))"
            End If

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("PO_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_INF_COMPLETE")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("PO_POSTA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("PO_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_INF_COMPLETE_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("PO_POSTA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("RI_RIS_GUASTI_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di portierato"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim numero As String
        Dim somma As Integer

        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True


        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 5))
    End Sub

    Protected Sub creaGraficoServiziManutenzione(ByVal risposta As String, ByVal valore As String)
        apriConnessione()

        If risposta = "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION "
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' "
            End If

        ElseIf risposta <> "---" And valore = "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE VE_REGOLARITA='" & risposta & "' OR VE_QUALITA='" & risposta & "' OR VE_CORTESIA='" & risposta & "' OR VE_SMALTIMENTO_RIF='" & risposta & "' OR VE_RUMORE='" & risposta & "' OR VE_TEMPESTIVITA='" & risposta & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND(VE_REGOLARITA='" & risposta & "' OR VE_QUALITA='" & risposta & "' OR VE_CORTESIA='" & risposta & "' OR VE_SMALTIMENTO_RIF='" & risposta & "' OR VE_RUMORE='" & risposta & "' OR VE_TEMPESTIVITA='" & risposta & "')"
            End If

        ElseIf risposta = "---" And valore <> "---" Then

            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE VE_REGOLARITA_VAL='" & valore & "' OR VE_QUALITA_VAL='" & valore & "' OR VE_CORTESIA_VAL='" & valore & "' OR VE_SMALTIMENTO_RIF_VAL='" & valore & "' OR VE_RUMORE_VAL='" & valore & "' OR VE_TEMPESTIVITA_VAL='" & valore & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND(VE_REGOLARITA_VAL='" & valore & "' OR VE_QUALITA_VAL='" & valore & "' OR VE_CORTESIA_VAL='" & valore & "' OR VE_SMALTIMENTO_RIF_VAL='" & valore & "' OR VE_RUMORE_VAL='" & valore & "' OR VE_TEMPESTIVITA_VAL='" & valore & "')"
            End If

        Else
            If Session.Item("ID_UNITA_IMMOBILIARE") = -1 Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE (VE_REGOLARITA_VAL='" & valore & "' AND VE_REGOLARITA='" & risposta & "') OR (VE_QUALITA_VAL='" & valore & "' AND VE_QUALITA='" & risposta & "') OR (VE_CORTESIA_VAL='" & valore & "' AND VE_CORTESIA='" & risposta & "') OR (VE_SMALTIMENTO_RIF_VAL='" & valore & "' AND VE_SMALTIMENTO_RIF='" & risposta & "') OR (VE_RUMORE_VAL='" & valore & "' AND VE_RUMORE='" & risposta & "') OR (VE_TEMPESTIVITA_VAL='" & valore & "' AND VE_TEMPESTIVITA='" & risposta & "')"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID_UNITA='" & Session.Item("ID_UNITA_IMMOBILIARE") & "' AND((VE_REGOLARITA_VAL='" & valore & "' AND VE_REGOLARITA='" & risposta & "') OR (VE_QUALITA_VAL='" & valore & "' AND VE_QUALITA='" & risposta & "') OR (VE_CORTESIA_VAL='" & valore & "' AND VE_CORTESIA='" & risposta & "') OR (VE_SMALTIMENTO_RIF_VAL='" & valore & "' AND VE_SMALTIMENTO_RIF='" & risposta & "') OR (VE_RUMORE_VAL='" & valore & "' AND VE_RUMORE='" & risposta & "') OR (VE_TEMPESTIVITA_VAL='" & valore & "' AND VE_TEMPESTIVITA='" & risposta & "'))"
            End If

        End If

        Dim areaGrafico As String

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()

        Dim arraySI As Integer = 0
        Dim arrayAB As Integer = 0
        Dim arrayPC As Integer = 0
        Dim arrayNO As Integer = 0
        Dim array1 As Integer = 0
        Dim array2 As Integer = 0
        Dim array3 As Integer = 0
        Dim array4 As Integer = 0
        Dim i As Integer = 0

        While myReader.Read

            Select Case myReader("VE_REGOLARITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_QUALITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_CORTESIA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_RUMORE")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_TEMPESTIVITA")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            Select Case myReader("VE_SMALTIMENTO_RIF")
                Case "SI"
                    arraySI = arraySI + 1
                Case "AB"
                    arrayAB = arrayAB + 1
                Case "PC"
                    arrayPC = arrayPC + 1
                Case "NO"
                    arrayNO = arrayNO + 1
            End Select

            '---------------------------
            'VALORI
            '---------------------------

            Select Case myReader("VE_REGOLARITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_QUALITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_CORTESIA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_SMALTIMENTO_RIF_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_RUMORE_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select

            Select Case myReader("VE_TEMPESTIVITA_VAL")
                Case "1"
                    array1 = array1 + 1
                Case "2"
                    array2 = array2 + 1
                Case "3"
                    array3 = array3 + 1
                Case "4"
                    array4 = array4 + 1
            End Select


        End While
        chiudiConnessione()

        Dim campo As String = "Servizi di manutenzione del verde"


        areaGrafico = "ChartArea1"
        Chart1.Series.Add(campo)
        Chart1.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart1.Series(campo).LabelForeColor = Drawing.Color.White
        Chart1.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart1.Series(campo)("BarLabelStyle") = "Center"

        Dim numero As String
        Dim somma As Integer
        somma = arrayAB + arrayNO + arrayPC + arraySI

        If arraySI <> 0 Then
            numero = arraySI / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "SI"
        End If
        If arrayAB <> 0 Then
            numero = arrayAB / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "AB"
        End If
        If arrayPC <> 0 Then
            numero = arrayPC / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "PC"
        End If
        If arrayNO <> 0 Then
            numero = arrayNO / somma * 100
            Chart1.Series(campo).Points.Add(numero).AxisLabel = "NO"
        End If

        'Chart1.Titles.Add("TITOLO")
        Chart1.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart1.BackColor = Drawing.Color.Transparent
        Chart1.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart1.Series(campo)("DrawingStyle") = "Cylinder"
        Chart1.Series(campo).ChartArea = areaGrafico
        Chart1.Legends.Add("Legenda")
        Chart1.Visible = True


        areaGrafico = "ChartArea2"
        Chart2.Series.Add(campo)
        Chart2.Series(campo).ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Chart2.Series(campo).LabelForeColor = Drawing.Color.White
        Chart2.Series(campo).BackImageTransparentColor = Drawing.Color.Transparent
        Chart2.Series(campo)("BarLabelStyle") = "Center"

        somma = array1 + array2 + array3 + array4

        If array1 <> 0 Then
            numero = array1 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "1"
        End If
        If array2 <> 0 Then
            numero = array2 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "2"
        End If
        If array3 <> 0 Then
            numero = array3 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "3"
        End If
        If array4 <> 0 Then
            numero = array4 / somma * 100
            Chart2.Series(campo).Points.Add(numero).AxisLabel = "4"
        End If

        'Chart2.Titles.Add("TITOLO")
        Chart2.ChartAreas(areaGrafico).Area3DStyle.Enable3D = True
        Chart2.BackColor = Drawing.Color.Transparent
        Chart2.ChartAreas(areaGrafico).BackColor = Drawing.Color.Transparent
        Chart2.Series(campo)("DrawingStyle") = "Cylinder"
        Chart2.Series(campo).ChartArea = areaGrafico
        Chart2.Legends.Add("Legenda")
        Chart2.Visible = True

        Label5.Text = "Numero di questionari coinvolti: " & CStr(Round(somma / 6))
    End Sub

    Protected Sub CaricaDomande(ByVal Tipo As String)

        'Carica la ddl delle domande
        Select Case Tipo
            Case "0"

                'Caso generico
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "25"))
                AggiungiServiziGenerici(0)
                AggiungiServiziPulizia()
                AggiungiServiziPortierato()
                AggiungiServiziRiscaldamento()
                AggiungiServiziManutenzione()

            Case "1"

                'Servizi di pulizia
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "26"))
                AggiungiServiziGenerici(1)
                AggiungiServiziPulizia()

            Case "2"

                'Servizi di portierato
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "27"))
                AggiungiServiziGenerici(2)
                AggiungiServiziPortierato()

            Case "3"

                'Servizi di riscaldamento
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "28"))
                AggiungiServiziGenerici(3)
                AggiungiServiziRiscaldamento()

            Case "4"

                'Servizi di manutenzione del verde
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "29"))
                AggiungiServiziGenerici(4)
                AggiungiServiziManutenzione()

            Case Else

                'Caso generico
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "25"))
                AggiungiServiziGenerici(0)
                AggiungiServiziPulizia()
                AggiungiServiziPortierato()
                AggiungiServiziRiscaldamento()
                AggiungiServiziManutenzione()

        End Select
    End Sub

    Protected Sub AggiungiServiziGenerici(ByVal indice As Integer)

        ddlDomande.Items.Add(New ListItem("Ritiene che il servizio sia svolto con REGOLARITà?", CStr(indice * 3)))
        ddlDomande.Items.Add(New ListItem("Ritiene che il servizio sia svolto con QUALITà?", CStr(indice * 3 + 1)))
        ddlDomande.Items.Add(New ListItem("Ritiene che il personale sia CORTESE?", CStr(indice * 3 + 2)))

    End Sub

    Protected Sub AggiungiServiziPulizia()

        ddlDomande.Items.Add(New ListItem("Ritiene che la condizione dei punti di raccolta rifiuti sia IGIENICAMENTE soddisfacente?", "32"))
        ddlDomande.Items.Add(New ListItem("Ritiene che la PULIZIA dei PIAZZALI E DELLE PARTI COMUNI sia adeguata?", "33"))
        ddlDomande.Items.Add(New ListItem("Ritiene tempestiva la rimozione di masserizie e RIFIUTI INGOMBRANTI?", "34"))

    End Sub

    Protected Sub AggiungiServiziPortierato()

        ddlDomande.Items.Add(New ListItem("Ritiene che il personale offra INFORMAZIONI COMPLETE?", "17"))
        ddlDomande.Items.Add(New ListItem("Ritiene che la GESTIONE DELLA POSTA sia soddisfacente?", "18"))

    End Sub

    Protected Sub AggiungiServiziRiscaldamento()

        ddlDomande.Items.Add(New ListItem("Ritiene che la TEMPERATURA sia adeguata nei mesi invernali?", "19"))
        ddlDomande.Items.Add(New ListItem("Ritiene che sia facile contattare il pronto intervento in caso di GUASTI?", "20"))
        ddlDomande.Items.Add(New ListItem("Ritiene che i GUASTI siano risolti con tempestività?", "21"))

    End Sub

    Protected Sub AggiungiServiziManutenzione()

        ddlDomande.Items.Add(New ListItem("Ritiene che l’intervento per risolvere i potenziali rischi (es: rami pendenti), sia tempestivo?", "22"))
        ddlDomande.Items.Add(New ListItem("Ritiene che i macchinari utilizzati siano troppo rumorosi?", "23"))
        ddlDomande.Items.Add(New ListItem("Ritiene che i rifiuti prodotti (es. taglio erba...) vengano smaltiti in modo soddisfacente?", "24"))

    End Sub

    Protected Sub disegnaGrafici()
        'MsgBox(Session.Item("ID_UNITA_IMMOBILIARE"))
        Chart1.Series.Clear()
        Chart2.Series.Clear()
        Chart1.Legends.Clear()
        Chart2.Legends.Clear()

        Select Case ddlDomande.SelectedValue
            Case "3"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PU_REGOLARITA", 1)
                    creaGrafico("PU_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PU_REGOLARITA", 1)
                    creaGraficoConValore("PU_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PU_REGOLARITA", 1)
                    creaGraficoConRisposta("PU_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("PU_REGOLARITA_VAL", 2)
                End If

            Case "4"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PU_QUALITA", 1)
                    creaGrafico("PU_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PU_QUALITA", 1)
                    creaGraficoConValore("PU_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PU_QUALITA", 1)
                    creaGraficoConRisposta("PU_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_QUALITA", 1)
                    creaGraficoConValoreRisposta("PU_QUALITA_VAL", 2)
                End If

            Case "5"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PU_CORTESIA", 1)
                    creaGrafico("PU_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PU_CORTESIA", 1)
                    creaGraficoConValore("PU_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PU_CORTESIA", 1)
                    creaGraficoConRisposta("PU_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_CORTESIA", 1)
                    creaGraficoConValoreRisposta("PU_CORTESIA_VAL", 2)
                End If

            Case "6"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PO_REGOLARITA", 1)
                    creaGrafico("PO_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PO_REGOLARITA", 1)
                    creaGraficoConValore("PO_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PO_REGOLARITA", 1)
                    creaGraficoConRisposta("PO_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("PO_REGOLARITA_VAL", 2)
                End If

            Case "7"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PO_QUALITA", 1)
                    creaGrafico("PO_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PO_QUALITA", 1)
                    creaGraficoConValore("PO_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PO_QUALITA", 1)
                    creaGraficoConRisposta("PO_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_QUALITA", 1)
                    creaGraficoConValoreRisposta("PO_QUALITA_VAL", 2)
                End If

            Case "8"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PO_CORTESIA", 1)
                    creaGrafico("PO_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PO_CORTESIA", 1)
                    creaGraficoConValore("PO_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PO_CORTESIA", 1)
                    creaGraficoConRisposta("PO_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_CORTESIA", 1)
                    creaGraficoConValoreRisposta("PO_CORTESIA_VAL", 2)
                End If

            Case "9"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("RI_REGOLARITA", 1)
                    creaGrafico("RI_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("RI_REGOLARITA", 1)
                    creaGraficoConValore("RI_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("RI_REGOLARITA", 1)
                    creaGraficoConRisposta("RI_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("RI_REGOLARITA_VAL", 2)
                End If

            Case "10"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("RI_QUALITA", 1)
                    creaGrafico("RI_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("RI_QUALITA", 1)
                    creaGraficoConValore("RI_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("RI_QUALITA", 1)
                    creaGraficoConRisposta("RI_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_QUALITA", 1)
                    creaGraficoConValoreRisposta("RI_QUALITA_VAL", 2)
                End If

            Case "11"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("RI_CORTESIA", 1)
                    creaGrafico("RI_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("RI_CORTESIA", 1)
                    creaGraficoConValore("RI_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("RI_CORTESIA", 1)
                    creaGraficoConRisposta("RI_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_CORTESIA", 1)
                    creaGraficoConValoreRisposta("RI_CORTESIA_VAL", 2)
                End If

            Case "12"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("VE_REGOLARITA", 1)
                    creaGrafico("VE_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("VE_REGOLARITA", 1)
                    creaGraficoConValore("VE_REGOLARITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("VE_REGOLARITA", 1)
                    creaGraficoConRisposta("VE_REGOLARITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_REGOLARITA", 1)
                    creaGraficoConValoreRisposta("VE_REGOLARITA_VAL", 2)
                End If

            Case "13"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("VE_QUALITA", 1)
                    creaGrafico("VE_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("VE_QUALITA", 1)
                    creaGraficoConValore("VE_QUALITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("VE_QUALITA", 1)
                    creaGraficoConRisposta("VE_QUALITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_QUALITA", 1)
                    creaGraficoConValoreRisposta("VE_QUALITA_VAL", 2)
                End If

            Case "14"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("VE_CORTESIA", 1)
                    creaGrafico("VE_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("VE_CORTESIA", 1)
                    creaGraficoConValore("VE_CORTESIA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("VE_CORTESIA", 1)
                    creaGraficoConRisposta("VE_CORTESIA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_CORTESIA", 1)
                    creaGraficoConValoreRisposta("VE_CORTESIA_VAL", 2)
                End If

            Case "17"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PO_INF_COMPLETE", 1)
                    creaGrafico("PO_INF_COMPLETE_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PO_INF_COMPLETE", 1)
                    creaGraficoConValore("PO_INF_COMPLETE_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PO_INF_COMPLETE", 1)
                    creaGraficoConRisposta("PO_INF_COMPLETE_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_INF_COMPLETE", 1)
                    creaGraficoConValoreRisposta("PO_INF_COMPLETE_VAL", 2)
                End If

            Case "18"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PO_POSTA", 1)
                    creaGrafico("PO_POSTA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PO_POSTA", 1)
                    creaGraficoConValore("PO_POSTA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PO_POSTA", 1)
                    creaGraficoConRisposta("PO_POSTA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PO_POSTA", 1)
                    creaGraficoConValoreRisposta("PO_POSTA_VAL", 2)
                End If

            Case "19"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("RI_TEMPERATURA", 1)
                    creaGrafico("RI_TEMPERATURA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("RI_TEMPERATURA", 1)
                    creaGraficoConValore("RI_TEMPERATURA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("RI_TEMPERATURA", 1)
                    creaGraficoConRisposta("RI_TEMPERATURA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_TEMPERATURA", 1)
                    creaGraficoConValoreRisposta("RI_TEMPERATURA_VAL", 2)
                End If

            Case "20"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("RI_GUASTI", 1)
                    creaGrafico("RI_GUASTI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("RI_GUASTI", 1)
                    creaGraficoConValore("RI_GUASTI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("RI_GUASTI", 1)
                    creaGraficoConRisposta("RI_GUASTI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_GUASTI", 1)
                    creaGraficoConValoreRisposta("RI_GUASTI_VAL", 2)
                End If

            Case "21"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("RI_RIS_GUASTI", 1)
                    creaGrafico("RI_RIS_GUASTI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("RI_RIS_GUASTI", 1)
                    creaGraficoConValore("RI_RIS_GUASTI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("RI_RIS_GUASTI", 1)
                    creaGraficoConRisposta("RI_RIS_GUASTI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("RI_RIS_GUASTI", 1)
                    creaGraficoConValoreRisposta("RI_RIS_GUASTI_VAL", 2)
                End If

            Case "22"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("VE_TEMPESTIVITA", 1)
                    creaGrafico("VE_TEMPESTIVITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("VE_TEMPESTIVITA", 1)
                    creaGraficoConValore("VE_TEMPESTIVITA_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("VE_TEMPESTIVITA", 1)
                    creaGraficoConRisposta("VE_TEMPESTIVITA_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_TEMPESTIVITA", 1)
                    creaGraficoConValoreRisposta("VE_TEMPESTIVITA_VAL", 2)
                End If

            Case "23"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("VE_RUMORE", 1)
                    creaGrafico("VE_RUMORE_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("VE_RUMORE", 1)
                    creaGraficoConValore("VE_RUMORE_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("VE_RUMORE", 1)
                    creaGraficoConRisposta("VE_RUMORE_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_RUMORE", 1)
                    creaGraficoConValoreRisposta("VE_RUMORE_VAL", 2)
                End If

            Case "24"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("VE_SMALTIMENTO_RIF", 1)
                    creaGrafico("VE_SMALTIMENTO_RIF_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("VE_SMALTIMENTO_RIF", 1)
                    creaGraficoConValore("VE_SMALTIMENTO_RIF_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("VE_SMALTIMENTO_RIF", 1)
                    creaGraficoConRisposta("VE_SMALTIMENTO_RIF_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("VE_SMALTIMENTO_RIF", 1)
                    creaGraficoConValoreRisposta("VE_SMALTIMENTO_RIF_VAL", 2)
                End If

            Case "32"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PU_IGIENE", 1)
                    creaGrafico("PU_IGIENE_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PU_IGIENE", 1)
                    creaGraficoConValore("PU_IGIENE_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PU_IGIENE", 1)
                    creaGraficoConRisposta("PU_IGIENE_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_IGIENE", 1)
                    creaGraficoConValoreRisposta("PU_IGIENE_VAL", 2)
                End If

            Case "33"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PU_PARTI_COMUNI", 1)
                    creaGrafico("PU_PARTI_COMUNI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PU_PARTI_COMUNI", 1)
                    creaGraficoConValore("PU_PARTI_COMUNI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PU_PARTI_COMUNI", 1)
                    creaGraficoConRisposta("PU_PARTI_COMUNI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_PARTI_COMUNI", 1)
                    creaGraficoConValoreRisposta("PU_PARTI_COMUNI_VAL", 2)
                End If

            Case "34"

                If ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue = "---" Then
                    creaGrafico("PU_RIF_INGOMBRANTI", 1)
                    creaGrafico("PU_RIF_INGOMBRANTI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue = "---" And ddlValore.SelectedValue <> "---" Then
                    creaGraficoConValore("PU_RIF_INGOMBRANTI", 1)
                    creaGraficoConValore("PU_RIF_INGOMBRANTI_VAL", 2)
                ElseIf ddlRisposta.SelectedValue <> "---" And ddlValore.SelectedValue = "---" Then
                    creaGraficoConRisposta("PU_RIF_INGOMBRANTI", 1)
                    creaGraficoConRisposta("PU_RIF_INGOMBRANTI_VAL", 2)
                Else
                    creaGraficoConValoreRisposta("PU_RIF_INGOMBRANTI", 1)
                    creaGraficoConValoreRisposta("PU_RIF_INGOMBRANTI_VAL", 2)
                End If

            Case "26"

                'servizi di pulizia generici
                creaGraficoServiziPulizia(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "27"

                'servizi di portierato generici
                creaGraficoServiziPortierato(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "28"

                'servizi di riscaldamento generici
                creaGraficoServiziRiscaldamento(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "29"

                'servizi di manutenzione generici
                creaGraficoServiziManutenzione(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "0"

                'regolarità generica
                creaGraficoRegolarita(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "1"

                'qualità generica
                creaGraficoQualita(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "2"

                'cortesia generica
                creaGraficoCortesia(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case "25"

                'Nessuna domanda selezionata
                creaGraficoNessunaDomanda(ddlRisposta.SelectedValue, ddlValore.SelectedValue)

            Case Else
        End Select
    End Sub

    Protected Sub ddlDomande_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDomande.SelectedIndexChanged
        'disegnaGrafici()
    End Sub

    Protected Sub ddlServizi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlServizi.SelectedIndexChanged
        CaricaDomande(ddlServizi.SelectedIndex)
        'disegnaGrafici()
    End Sub

    Protected Sub ddlValore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlValore.SelectedIndexChanged
        'disegnaGrafici()
    End Sub

    Protected Sub ddlRisposta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRisposta.SelectedIndexChanged
        'disegnaGrafici()
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Dim codiceUnitaImmobiliare As String = Trim(cod.Text)
        Session.Item("ID_UNITA_IMMOBILIARE") = -1

        If Not codiceUnitaImmobiliare = "" Then

            'Controllo se esiste il codice dell'unità immobiliare inserito
            Try
                apriConnessione()
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE ='" & codiceUnitaImmobiliare & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Session.Item("ID_UNITA_IMMOBILIARE") = myReader("ID")
                    disegnaGrafici()
                Else
                    Chart1.Visible = False
                    Chart2.Visible = False
                    Label5.Text = ""
                    Response.Write("<script>alert('Codice unità immobiliare non corretto!');</script>")
                End If
                chiudiConnessione()
            Catch ex As Exception
                chiudiConnessione()
            End Try
        Else
            disegnaGrafici()
        End If
    End Sub

End Class