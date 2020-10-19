Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_Report_TotPatrimTipoUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:350px; left:450px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            DatiPatrimTipoUI()
        End If

    End Sub


    Private Sub DatiPatrimTipoUI()

        Dim s As String
        Dim n_totale As Integer
        Dim mq_totale As Double
        If Request.QueryString("C") = "-1" Then
            s = " ORDER BY SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE,INDIRIZZO ASC"
        Else
            s = " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID =" & Request.QueryString("C") & " ORDER BY SISCOM_MI.EDIFICI.COD_EDIFICIO ASC"
        End If
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim RIGA As System.Data.DataRow
            Dim RIGA2 As System.Data.DataRow
            
            'variabili per i totali dei civici
            Dim contAllCIV As Integer = 0
            Dim contAll2CIV As Double = 0
            Dim contPortCIV As Integer = 0
            Dim contPort2CIV As Double = 0
            Dim contDepCIV As Integer = 0
            Dim contDep2CIV As Double = 0
            Dim contNegCIV As Integer = 0
            Dim contNeg2CIV As Double = 0
            Dim contLabCIV As Integer = 0
            Dim contLab2CIV As Double = 0
            Dim contTotCIV As Integer = 0
            Dim contTot2CIV As Double = 0
            Dim contAltreCIV As Integer = 0
            Dim contAltre2CIV As Double = 0

            'variabili per i totali dei complessi
            Dim contAllCOMPL As Integer = 0
            Dim contAll2COMPL As Double = 0
            Dim contPortCOMPL As Integer = 0
            Dim contPort2COMPL As Double = 0
            Dim contDepCOMPL As Integer = 0
            Dim contDep2COMPL As Double = 0
            Dim contNegCOMPL As Integer = 0
            Dim contNeg2COMPL As Double = 0
            Dim contLabCOMPL As Integer = 0
            Dim contLab2COMPL As Double = 0
            Dim contTotCOMPL As Integer = 0
            Dim contTot2COMPL As Double = 0
            Dim contAltreCOMPL As Integer = 0
            Dim contAltre2COMPL As Double = 0

            Dim contAllGENER As Integer = 0
            Dim contAll2GENER As Double = 0
            Dim contPortGENER As Integer = 0
            Dim contPort2GENER As Double = 0
            Dim contDepGENER As Integer = 0
            Dim contDep2GENER As Double = 0
            Dim contNegGENER As Integer = 0
            Dim contNeg2GENER As Double = 0
            Dim contLabGENER As Integer = 0
            Dim contLab2GENER As Double = 0
            Dim contTotGENER As Integer = 0
            Dim contTot2GENER As Double = 0
            Dim contAltreGENER As Integer = 0
            Dim contAltre2GENER As Double = 0

            dt.Columns.Add("ID_COMPLESSO", Type.GetType("System.String"))
            dt.Columns.Add("DENOMINAZIONE", Type.GetType("System.String"))
            dt.Columns.Add("COD_EDIFICIO", Type.GetType("System.String"))
            dt.Columns.Add("INDIRIZZO", Type.GetType("System.String"))
            dt.Columns.Add("N_ALLOGGI")
            dt.Columns.Add("MQ_ALLOGGI")
            dt.Columns.Add("N_PORTINERIE")
            dt.Columns.Add("MQ_PORTINERIE")
            dt.Columns.Add("N_DEPOSITI")
            dt.Columns.Add("MQ_DEPOSITI")
            dt.Columns.Add("N_NEGOZI")
            dt.Columns.Add("MQ_NEGOZI")
            dt.Columns.Add("N_LABORATORI")
            dt.Columns.Add("MQ_LABORATORI")
            dt.Columns.Add("N_ALTRE")
            dt.Columns.Add("MQ_ALTRE")
            dt.Columns.Add("N_TOTALE")
            dt.Columns.Add("MQ_TOTALE")

            Dim dt2 As New Data.DataTable
            par.cmd.CommandText = "SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE,SISCOM_MI.EDIFICI.COD_EDIFICIO,SISCOM_MI.INDIRIZZI.DESCRIZIONE||', '||INDIRIZZI.CIVICO AS INDIRIZZO,SISCOM_MI.COMPLESSI_IMMOBILIARI.ID AS ID_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " & s & ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt2)
            DataGrid1.DataSource = dt2

            For i As Integer = 0 To dt2.Rows.Count - 1

                RIGA = dt.NewRow()
                RIGA.Item("DENOMINAZIONE") = dt2.Rows(i).Item(0)
                RIGA.Item("COD_EDIFICIO") = dt2.Rows(i).Item(1)
                RIGA.Item("INDIRIZZO") = dt2.Rows(i).Item(2)
                RIGA.Item("ID_COMPLESSO") = dt2.Rows(i).Item(3)

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL' AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_ALLOGGI") = par.IfNull(myReader2("N_ALLOGGI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='AL' AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_ALLOGGI") = par.IfNull(myReader2("MQ_ALLOGGI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_PORTINERIE FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'P' AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_PORTINERIE") = par.IfNull(myReader2("N_PORTINERIE"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_PORTINERIE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='P' AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_PORTINERIE") = par.IfNull(myReader2("MQ_PORTINERIE"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_DEPOSITI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'S' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_DEPOSITI") = par.IfNull(myReader2("N_DEPOSITI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_DEPOSITI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='S' AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_DEPOSITI") = par.IfNull(myReader2("MQ_DEPOSITI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_NEGOZI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_NEGOZI") = par.IfNull(myReader2("N_NEGOZI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_NEGOZI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='N' AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_NEGOZI") = par.IfNull(myReader2("MQ_NEGOZI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_LABORATORI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'L' AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_LABORATORI") = par.IfNull(myReader2("N_LABORATORI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_LABORATORI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='L' AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_LABORATORI") = par.IfNull(myReader2("MQ_LABORATORI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_ALTRE FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'S' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'N' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'P' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'L' AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_ALTRE") = par.IfNull(myReader2("N_ALTRE"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_ALTRE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'S' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'N' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'P' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <>'L' AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_ALTRE") = par.IfNull(myReader2("MQ_ALTRE"), "0")
                End If
                myReader2.Close()

                n_totale = CInt(RIGA.Item("N_ALLOGGI")) + CInt(RIGA.Item("N_PORTINERIE")) + CInt(RIGA.Item("N_DEPOSITI")) + CInt(RIGA.Item("N_NEGOZI")) + CInt(RIGA.Item("N_LABORATORI")) + CInt(RIGA.Item("N_ALTRE"))
                mq_totale = CDbl(RIGA.Item("MQ_ALLOGGI")) + CDbl(RIGA.Item("MQ_PORTINERIE")) + CDbl(RIGA.Item("MQ_DEPOSITI")) + CDbl(RIGA.Item("MQ_NEGOZI")) + CDbl(RIGA.Item("MQ_LABORATORI")) + CDbl(RIGA.Item("MQ_ALTRE"))
                RIGA.Item("N_TOTALE") = Format(n_totale, "##,##0")
                RIGA.Item("MQ_TOTALE") = Format(mq_totale, "##,##0.00")

                If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                    dt.Rows.Add(RIGA)
                End If

                contAllCIV = contAllCIV + RIGA.Item("N_ALLOGGI")
                contAll2CIV = contAll2CIV + RIGA.Item("MQ_ALLOGGI")
                contPortCIV = contPortCIV + RIGA.Item("N_PORTINERIE")
                contPort2CIV = contPort2CIV + RIGA.Item("MQ_PORTINERIE")
                contDepCIV = contDepCIV + RIGA.Item("N_DEPOSITI")
                contDep2CIV = contDep2CIV + RIGA.Item("MQ_DEPOSITI")
                contNegCIV = contNegCIV + RIGA.Item("N_NEGOZI")
                contNeg2CIV = contNeg2CIV + RIGA.Item("MQ_NEGOZI")
                contLabCIV = contLabCIV + RIGA.Item("N_LABORATORI")
                contLab2CIV = contLab2CIV + RIGA.Item("MQ_LABORATORI")
                contAltreCIV = contAltreCIV + RIGA.Item("N_ALTRE")
                contAltre2CIV = contAltre2CIV + RIGA.Item("MQ_ALTRE")
                contTotCIV = contTotCIV + RIGA.Item("N_TOTALE")
                contTot2CIV = contTot2CIV + RIGA.Item("MQ_TOTALE")

                contAllCOMPL = contAllCOMPL + RIGA.Item("N_ALLOGGI")
                contAll2COMPL = contAll2COMPL + RIGA.Item("MQ_ALLOGGI")
                contPortCOMPL = contPortCOMPL + RIGA.Item("N_PORTINERIE")
                contPort2COMPL = contPort2COMPL + RIGA.Item("MQ_PORTINERIE")
                contDepCOMPL = contDepCOMPL + RIGA.Item("N_DEPOSITI")
                contDep2COMPL = contDep2COMPL + RIGA.Item("MQ_DEPOSITI")
                contNegCOMPL = contNegCOMPL + RIGA.Item("N_NEGOZI")
                contNeg2COMPL = contNeg2COMPL + RIGA.Item("MQ_NEGOZI")
                contLabCOMPL = contLabCOMPL + RIGA.Item("N_LABORATORI")
                contLab2COMPL = contLab2COMPL + RIGA.Item("MQ_LABORATORI")
                contAltreCOMPL = contAltreCOMPL + RIGA.Item("N_ALTRE")
                contAltre2COMPL = contAltre2COMPL + RIGA.Item("MQ_ALTRE")
                contTotCOMPL = contTotCOMPL + RIGA.Item("N_TOTALE")
                contTot2COMPL = contTot2COMPL + RIGA.Item("MQ_TOTALE")

                contAllGENER = contAllGENER + RIGA.Item("N_ALLOGGI")
                contAll2GENER = contAll2GENER + RIGA.Item("MQ_ALLOGGI")
                contPortGENER = contPortGENER + RIGA.Item("N_PORTINERIE")
                contPort2GENER = contPort2GENER + RIGA.Item("MQ_PORTINERIE")
                contDepGENER = contDepGENER + RIGA.Item("N_DEPOSITI")
                contDep2GENER = contDep2GENER + RIGA.Item("MQ_DEPOSITI")
                contNegGENER = contNegGENER + RIGA.Item("N_NEGOZI")
                contNeg2GENER = contNeg2GENER + RIGA.Item("MQ_NEGOZI")
                contLabGENER = contLabGENER + RIGA.Item("N_LABORATORI")
                contLab2GENER = contLab2GENER + RIGA.Item("MQ_LABORATORI")
                contAltreGENER = contAltreGENER + RIGA.Item("N_ALTRE")
                contAltre2GENER = contAltre2GENER + RIGA.Item("MQ_ALTRE")
                contTotGENER = contTotGENER + RIGA.Item("N_TOTALE")
                contTot2GENER = contTot2GENER + RIGA.Item("MQ_TOTALE")

                If i < dt2.Rows.Count - 1 Then
                    If dt2.Rows(i).Item(2) <> dt2.Rows(i + 1).Item(2) Then
                        RIGA2 = dt.NewRow()
                        If Request.QueryString("TOT") <> "0" Then
                            RIGA2.Item("DENOMINAZIONE") = RIGA.Item("DENOMINAZIONE")
                            RIGA2.Item("INDIRIZZO") = RIGA.Item("INDIRIZZO")
                        End If
                        RIGA2.Item("COD_EDIFICIO") = "TOTALE CIVICO"
                        RIGA2.Item("N_ALLOGGI") = Format(contAllCIV, "##,##0")
                        RIGA2.Item("MQ_ALLOGGI") = Format(contAll2CIV, "##,##0.00")
                        RIGA2.Item("N_PORTINERIE") = Format(contPortCIV, "##,##0")
                        RIGA2.Item("MQ_PORTINERIE") = Format(contPort2CIV, "##,##0.00")
                        RIGA2.Item("N_DEPOSITI") = Format(contDepCIV, "##,##0")
                        RIGA2.Item("MQ_DEPOSITI") = Format(contDep2CIV, "##,##0.00")
                        RIGA2.Item("N_NEGOZI") = Format(contNegCIV, "##,##0")
                        RIGA2.Item("MQ_NEGOZI") = Format(contNeg2CIV, "##,##0.00")
                        RIGA2.Item("N_LABORATORI") = Format(contLabCIV, "##,##0")
                        RIGA2.Item("MQ_LABORATORI") = Format(contLab2CIV, "##,##0.00")
                        RIGA2.Item("N_ALTRE") = Format(contAltreCIV, "##,##0")
                        RIGA2.Item("MQ_ALTRE") = Format(contAltre2CIV, "##,##0.00")
                        RIGA2.Item("N_TOTALE") = Format(contTotCIV, "##,##0")
                        RIGA2.Item("MQ_TOTALE") = Format(contTot2CIV, "##,##0.00")
                        If Request.QueryString("TOT") = "1" Or Request.QueryString("TOT") = "0" Then
                            dt.Rows.Add(RIGA2)
                        End If
                        contAllCIV = 0
                        contAll2CIV = 0
                        contPortCIV = 0
                        contPort2CIV = 0
                        contDepCIV = 0
                        contDep2CIV = 0
                        contNegCIV = 0
                        contNeg2CIV = 0
                        contLabCIV = 0
                        contLab2CIV = 0
                        contAltreCIV = 0
                        contAltre2CIV = 0
                        contTotCIV = 0
                        contTot2CIV = 0
                    End If
                    If dt2.Rows(i).Item(3) <> dt2.Rows(i + 1).Item(3) Then
                        RIGA2 = dt.NewRow()
                        RIGA2.Item("DENOMINAZIONE") = RIGA.Item("DENOMINAZIONE")
                        RIGA2.Item("INDIRIZZO") = "TOTALE COMPLESSO"
                        RIGA2.Item("N_ALLOGGI") = Format(contAllCOMPL, "##,##0")
                        RIGA2.Item("MQ_ALLOGGI") = Format(contAll2COMPL, "##,##0.00")
                        RIGA2.Item("N_PORTINERIE") = Format(contPortCOMPL, "##,##0")
                        RIGA2.Item("MQ_PORTINERIE") = Format(contPort2COMPL, "##,##0.00")
                        RIGA2.Item("N_DEPOSITI") = Format(contDepCOMPL, "##,##0")
                        RIGA2.Item("MQ_DEPOSITI") = Format(contDep2COMPL, "##,##0.00")
                        RIGA2.Item("N_NEGOZI") = Format(contNegCOMPL, "##,##0")
                        RIGA2.Item("MQ_NEGOZI") = Format(contNeg2COMPL, "##,##0.00")
                        RIGA2.Item("N_LABORATORI") = Format(contLabCOMPL, "##,##0")
                        RIGA2.Item("MQ_LABORATORI") = Format(contLab2COMPL, "##,##0.00")
                        RIGA2.Item("N_ALTRE") = Format(contAltreCOMPL, "##,##0")
                        RIGA2.Item("MQ_ALTRE") = Format(contAltre2COMPL, "##,##0.00")
                        RIGA2.Item("N_TOTALE") = Format(contTotCOMPL, "##,##0")
                        RIGA2.Item("MQ_TOTALE") = Format(contTot2COMPL, "##,##0.00")
                        If Request.QueryString("TOT") = "2" Or Request.QueryString("TOT") = "0" Then
                            dt.Rows.Add(RIGA2)
                        End If
                        contAllCOMPL = 0
                        contAll2COMPL = 0
                        contPortCOMPL = 0
                        contPort2COMPL = 0
                        contDepCOMPL = 0
                        contDep2COMPL = 0
                        contNegCOMPL = 0
                        contNeg2COMPL = 0
                        contLabCOMPL = 0
                        contLab2COMPL = 0
                        contAltreCOMPL = 0
                        contAltre2COMPL = 0
                        contTotCOMPL = 0
                        contTot2COMPL = 0
                    End If
                Else
                    If dt2.Rows(i).Item(2) = dt2.Rows(i).Item(2) Then
                        RIGA2 = dt.NewRow()
                        If Request.QueryString("TOT") <> "0" Then
                            RIGA2.Item("DENOMINAZIONE") = RIGA.Item("DENOMINAZIONE")
                            RIGA2.Item("INDIRIZZO") = RIGA.Item("INDIRIZZO")
                        End If
                        RIGA2.Item("COD_EDIFICIO") = "TOTALE CIVICO"
                        RIGA2.Item("N_ALLOGGI") = Format(contAllCIV, "##,##0")
                        RIGA2.Item("MQ_ALLOGGI") = Format(contAll2CIV, "##,##0.00")
                        RIGA2.Item("N_PORTINERIE") = Format(contPortCIV, "##,##0")
                        RIGA2.Item("MQ_PORTINERIE") = Format(contPort2CIV, "##,##0.00")
                        RIGA2.Item("N_DEPOSITI") = Format(contDepCIV, "##,##0")
                        RIGA2.Item("MQ_DEPOSITI") = Format(contDep2CIV, "##,##0.00")
                        RIGA2.Item("N_NEGOZI") = Format(contNegCIV, "##,##0")
                        RIGA2.Item("MQ_NEGOZI") = Format(contNeg2CIV, "##,##0.00")
                        RIGA2.Item("N_LABORATORI") = Format(contLabCIV, "##,##0")
                        RIGA2.Item("MQ_LABORATORI") = Format(contLab2CIV, "##,##0.00")
                        RIGA2.Item("N_ALTRE") = Format(contAltreCIV, "##,##0")
                        RIGA2.Item("MQ_ALTRE") = Format(contAltre2CIV, "##,##0.00")
                        RIGA2.Item("N_TOTALE") = Format(contTotCIV, "##,##0")
                        RIGA2.Item("MQ_TOTALE") = Format(contTot2CIV, "##,##0.00")
                        If Request.QueryString("TOT") = "1" Or Request.QueryString("TOT") = "0" Then
                            dt.Rows.Add(RIGA2)
                        End If
                        contAllCIV = 0
                        contAll2CIV = 0
                        contPortCIV = 0
                        contPort2CIV = 0
                        contDepCIV = 0
                        contDep2CIV = 0
                        contNegCIV = 0
                        contNeg2CIV = 0
                        contLabCIV = 0
                        contLab2CIV = 0
                        contAltreCIV = 0
                        contAltre2CIV = 0
                        contTotCIV = 0
                        contTot2CIV = 0
                    End If
                    RIGA2 = dt.NewRow()
                    RIGA2.Item("DENOMINAZIONE") = RIGA.Item("DENOMINAZIONE")
                    RIGA2.Item("INDIRIZZO") = "TOTALE COMPLESSO"
                    RIGA2.Item("N_ALLOGGI") = Format(contAllCOMPL, "##,##0")
                    RIGA2.Item("MQ_ALLOGGI") = Format(contAll2COMPL, "##,##0.00")
                    RIGA2.Item("N_PORTINERIE") = Format(contPortCOMPL, "##,##0")
                    RIGA2.Item("MQ_PORTINERIE") = Format(contPort2COMPL, "##,##0.00")
                    RIGA2.Item("N_DEPOSITI") = Format(contDepCOMPL, "##,##0")
                    RIGA2.Item("MQ_DEPOSITI") = Format(contDep2COMPL, "##,##0.00")
                    RIGA2.Item("N_NEGOZI") = Format(contNegCOMPL, "##,##0")
                    RIGA2.Item("MQ_NEGOZI") = Format(contNeg2COMPL, "##,##0.00")
                    RIGA2.Item("N_LABORATORI") = Format(contLabCOMPL, "##,##0")
                    RIGA2.Item("MQ_LABORATORI") = Format(contLab2COMPL, "##,##0.00")
                    RIGA2.Item("N_ALTRE") = Format(contAltreCOMPL, "##,##0")
                    RIGA2.Item("MQ_ALTRE") = Format(contAltre2COMPL, "##,##0.00")
                    RIGA2.Item("N_TOTALE") = Format(contTotCOMPL, "##,##0")
                    RIGA2.Item("MQ_TOTALE") = Format(contTot2COMPL, "##,##0.00")
                    If Request.QueryString("TOT") = "2" Or Request.QueryString("TOT") = "0" Then
                        dt.Rows.Add(RIGA2)
                    End If
                    contAllCOMPL = 0
                    contAll2COMPL = 0
                    contPortCOMPL = 0
                    contPort2COMPL = 0
                    contDepCOMPL = 0
                    contDep2COMPL = 0
                    contNegCOMPL = 0
                    contNeg2COMPL = 0
                    contLabCOMPL = 0
                    contLab2COMPL = 0
                    contAltreCOMPL = 0
                    contAltre2COMPL = 0
                    contTotCOMPL = 0
                    contTot2COMPL = 0
                End If

                If Request.QueryString("C") = "-1" And i = dt2.Rows.Count - 1 Then
                    RIGA = dt.NewRow()
                    dt.Rows.Add(RIGA)
                    RIGA = dt.NewRow()
                    RIGA.Item("DENOMINAZIONE") = "TOTALE GENERALE"
                    RIGA.Item("N_ALLOGGI") = Format(contAllGENER, "##,##0")
                    RIGA.Item("MQ_ALLOGGI") = Format(contAll2GENER, "##,##0.00")
                    RIGA.Item("N_PORTINERIE") = Format(contPortGENER, "##,##0")
                    RIGA.Item("MQ_PORTINERIE") = Format(contPort2GENER, "##,##0.00")
                    RIGA.Item("N_DEPOSITI") = Format(contDepGENER, "##,##0")
                    RIGA.Item("MQ_DEPOSITI") = Format(contDep2GENER, "##,##0.00")
                    RIGA.Item("N_NEGOZI") = Format(contNegGENER, "##,##0")
                    RIGA.Item("MQ_NEGOZI") = Format(contNeg2GENER, "##,##0.00")
                    RIGA.Item("N_LABORATORI") = Format(contLabGENER, "##,##0")
                    RIGA.Item("MQ_LABORATORI") = Format(contLab2GENER, "##,##0.00")
                    RIGA.Item("N_ALTRE") = Format(contAltreGENER, "##,##0")
                    RIGA.Item("MQ_ALTRE") = Format(contAltre2GENER, "##,##0.00")
                    RIGA.Item("N_TOTALE") = Format(contTotGENER, "##,##0")
                    RIGA.Item("MQ_TOTALE") = Format(contTot2GENER, "##,##0.00")
                    dt.Rows.Add(RIGA)
                End If
            Next

            'RIGA = dt.NewRow()
            'RIGA.Item("DENOMINAZIONE") = "TOTALE GENERALE"
            'RIGA.Item("N_ALLOGGI") = dt.Compute("SUM(N_ALLOGGI)", String.Empty)
            'RIGA.Item("MQ_ALLOGGI") = dt.Compute("SUM(MQ_ALLOGGI)", String.Empty)
            'RIGA.Item("N_PORTINERIE") = dt.Compute("SUM(N_PORTINERIE)", String.Empty)
            'RIGA.Item("MQ_PORTINERIE") = dt.Compute("SUM(MQ_PORTINERIE)", String.Empty)
            'RIGA.Item("N_DEPOSITI") = dt.Compute("SUM(N_DEPOSITI)", String.Empty)
            'RIGA.Item("MQ_DEPOSITI") = dt.Compute("SUM(MQ_DEPOSITI)", String.Empty)
            'RIGA.Item("N_NEGOZI") = dt.Compute("SUM(N_NEGOZI)", String.Empty)
            'RIGA.Item("MQ_NEGOZI") = dt.Compute("SUM(MQ_NEGOZI)", String.Empty)
            'RIGA.Item("N_LABORATORI") = dt.Compute("SUM(N_LABORATORI)", String.Empty)
            'RIGA.Item("MQ_LABORATORI") = dt.Compute("SUM(MQ_LABORATORI)", String.Empty)
            'RIGA.Item("N_TOTALE") = dt.Compute("SUM(N_TOTALE)", String.Empty)
            'RIGA.Item("MQ_TOTALE") = dt.Compute("SUM(MQ_TOTALE)", String.Empty)
            'dt.Rows.Add(RIGA)

            Session.Add("MIADT", dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            If Request.QueryString("TOT") = "1" Or Request.QueryString("TOT") = "2" Or Request.QueryString("TOT") = "3" Then
                DataGrid1.AlternatingItemStyle.BackColor = Drawing.Color.Lavender
            End If

            If Request.QueryString("TOT") = "0" Then
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(3).Text.Contains("TOTALE") Then
                        di.ForeColor = Drawing.Color.Red
                        di.Cells(3).Font.Bold = True
                    End If
                    If di.Cells(2).Text.Contains("TOTALE") Then
                        di.BackColor = Drawing.Color.Gainsboro
                        di.ForeColor = Drawing.Color.Black
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                    If di.Cells(1).Text.Contains("TOTALE") Then
                        di.BackColor = Drawing.Color.RoyalBlue
                        di.ForeColor = Drawing.Color.White
                        di.Cells(1).Font.Underline = True
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(1).Text.Contains("TOTALE") Then
                        di.BackColor = Drawing.Color.RoyalBlue
                        di.ForeColor = Drawing.Color.White
                        di.Cells(1).Font.Underline = True
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Tot_dati_patrim_tipoUI" & Format(Now, "yyyyMMddHHmm")
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
                '.SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                .SetColumnWidth(1, 1, 30)
                .SetColumnWidth(2, 2, 40)
                .SetColumnWidth(3, 3, 15)
                .SetColumnWidth(4, 4, 15)
                .SetColumnWidth(5, 5, 15)
                .SetColumnWidth(6, 6, 17)
                .SetColumnWidth(7, 7, 18)
                .SetColumnWidth(8, 8, 16)
                .SetColumnWidth(9, 9, 15)
                .SetColumnWidth(10, 10, 15)
                .SetColumnWidth(11, 11, 15)
                .SetColumnWidth(12, 12, 18)
                .SetColumnWidth(13, 13, 18)
                .SetColumnWidth(14, 14, 15)
                .SetColumnWidth(15, 15, 15)
                .SetColumnWidth(16, 16, 15)
                .SetColumnWidth(17, 17, 15)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "Totalizzazioni dati patrimoniali per tipo UI")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 1, "COMPLESSO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 2, "INDIRIZZO E NUM. CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 3, "EDIFICIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 4, "NUM. ALLOGGI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 5, "MQ ALLOGGI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 6, "NUM. PORTINERIE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 7, "MQ PORTINERIE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 8, "NUM. DEPOSITI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 9, "MQ DEPOSITI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 10, "NUM. NEGOZI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 11, "MQ NEGOZI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 12, "NUM. LABORATORI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 13, "MQ LABORATORI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 14, "NUM. ALTRE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 15, "MQ ALTRE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 16, "NUM. TOTALI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 17, "MQ TOTALI", 0)

                K = 4
                For Each row In dt.Rows
                    If dt.Rows(i).Item("DENOMINAZIONE").ToString.Contains("GENERALE") Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont2, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("DENOMINAZIONE"), ""))
                    Else
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("DENOMINAZIONE"), ""))
                    End If
                    If dt.Rows(i).Item("INDIRIZZO").ToString.Contains("COMPLESSO") And Request.QueryString("TOT") = "0" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("DENOMINAZIONE"), ""))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("INDIRIZZO"), ""))
                    End If
                    If dt.Rows(i).Item("INDIRIZZO").ToString.Contains("COMPLESSO") = False Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("INDIRIZZO"), ""))
                    End If
                    If dt.Rows(i).Item("INDIRIZZO").ToString.Contains("COMPLESSO") And Request.QueryString("TOT") <> "0" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("INDIRIZZO"), ""))
                    End If
                    If dt.Rows(i).Item("COD_EDIFICIO").ToString.Contains("CIVICO") And Request.QueryString("TOT") = "0" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("COD_EDIFICIO"), ""))
                    End If
                    If dt.Rows(i).Item("COD_EDIFICIO").ToString.Contains("CIVICO") And Request.QueryString("TOT") <> "0" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("COD_EDIFICIO"), ""))
                    End If
                    If dt.Rows(i).Item("COD_EDIFICIO").ToString.Contains("CIVICO") = False Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("COD_EDIFICIO"), ""))
                    End If
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("N_ALLOGGI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("MQ_ALLOGGI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("N_PORTINERIE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("MQ_PORTINERIE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("N_DEPOSITI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("MQ_DEPOSITI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("N_NEGOZI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("MQ_NEGOZI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("N_LABORATORI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("MQ_LABORATORI"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(dt.Rows(i).Item("N_ALTRE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(dt.Rows(i).Item("MQ_ALTRE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(dt.Rows(i).Item("N_TOTALE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(dt.Rows(i).Item("MQ_TOTALE"), ""))
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
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
