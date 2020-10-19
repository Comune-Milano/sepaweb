Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_Report_TotPatrimGruppiUI
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
            DatiPatrimGruppiUI()
        End If
    End Sub

    Private Sub DatiPatrimGruppiUI()

        Dim s As String
        Dim n_totale As Integer
        Dim mq_totale As Double
        Dim RIGA As System.Data.DataRow
        Dim RIGA2 As System.Data.DataRow

        'variabili per i totali dei civici
        Dim contAllCIV As Integer = 0
        Dim contAll2CIV As Double = 0
        Dim contCommercCIV As Integer = 0
        Dim contCommerc2CIV As Double = 0
        Dim contBoxCIV As Integer = 0
        Dim contBox2CIV As Double = 0
        Dim contVarieCIV As Integer = 0
        Dim contVarie2CIV As Double = 0
        Dim contPertCIV As Integer = 0
        Dim contPert2CIV As Double = 0
        Dim contTotCIV As Integer = 0
        Dim contTot2CIV As Double = 0
        Dim contComCIV As Integer = 0
        Dim contCom2CIV As Double = 0

        'variabili per i totali dei complessi
        Dim contAllCOMPL As Integer = 0
        Dim contAll2COMPL As Double = 0
        Dim contCommercCOMPL As Integer = 0
        Dim contCommerc2COMPL As Double = 0
        Dim contBoxCOMPL As Integer = 0
        Dim contBox2COMPL As Double = 0
        Dim contVarieCOMPL As Integer = 0
        Dim contVarie2COMPL As Double = 0
        Dim contPertCOMPL As Integer = 0
        Dim contPert2COMPL As Double = 0
        Dim contTotCOMPL As Integer = 0
        Dim contTot2COMPL As Double = 0
        Dim contComCOMPL As Integer = 0
        Dim contCom2COMPL As Double = 0

        Dim contAllGENER As Integer = 0
        Dim contAll2GENER As Double = 0
        Dim contCommercGENER As Integer = 0
        Dim contCommerc2GENER As Double = 0
        Dim contBoxGENER As Integer = 0
        Dim contBox2GENER As Double = 0
        Dim contVarieGENER As Integer = 0
        Dim contVarie2GENER As Double = 0
        Dim contPertGENER As Integer = 0
        Dim contPert2GENER As Double = 0
        Dim contTotGENER As Integer = 0
        Dim contTot2GENER As Double = 0
        Dim contComGENER As Integer = 0
        Dim contCom2GENER As Double = 0

        If Request.QueryString("C") = "-1" Then
            s = " ORDER BY SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE,INDIRIZZO ASC"
        ElseIf Request.QueryString("C") = "-10" Then
            s = " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE =" & Request.QueryString("F") & " ORDER BY SISCOM_MI.EDIFICI.COD_EDIFICIO ASC"
        Else
            s = " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID =" & Request.QueryString("C") & " ORDER BY SISCOM_MI.EDIFICI.COD_EDIFICIO ASC"
        End If

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            dt.Columns.Add("ID_COMPLESSO", Type.GetType("System.String"))
            dt.Columns.Add("DENOMINAZIONE", Type.GetType("System.String"))
            dt.Columns.Add("COD_EDIFICIO", Type.GetType("System.String"))
            dt.Columns.Add("INDIRIZZO", Type.GetType("System.String"))
            dt.Columns.Add("N_ALLOGGI")
            dt.Columns.Add("MQ_ALLOGGI")
            dt.Columns.Add("N_COMMERC")
            dt.Columns.Add("MQ_COMMERC")
            dt.Columns.Add("N_BOX")
            dt.Columns.Add("MQ_BOX")
            dt.Columns.Add("N_VARIE")
            dt.Columns.Add("MQ_VARIE")
            dt.Columns.Add("N_PERTINENZE")
            dt.Columns.Add("MQ_PERTINENZE")
            dt.Columns.Add("N_COMUNI")
            dt.Columns.Add("MQ_COMUNI")
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

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_COMMERC FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AU' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'D' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'E' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'F' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'L' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'O' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'R' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'RIST' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'S' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SC' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SEAS' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'U') AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_COMMERC") = par.IfNull(myReader2("N_COMMERC"), "0")
                End If
                myReader2.Close()

                'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_COMMERC FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AU' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'D' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'E' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'F' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'L' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'O' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'R' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'RIST' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'S' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SC' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SEAS' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'U') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_COMMERC FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AU' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'D' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'E' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'F' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'L' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'O' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'R' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'RIST' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'S' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SC' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SEAS' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'U') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_COMMERC") = par.IfNull(myReader2("MQ_COMMERC"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_BOX FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'M') AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_BOX") = par.IfNull(myReader2("N_BOX"), "0")
                End If
                myReader2.Close()

                'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_BOX FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'M') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_BOX FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'B' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'H' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'I' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'M') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_BOX") = par.IfNull(myReader2("MQ_BOX"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_VARIE FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'K' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'MF' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'T' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'ST') AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_VARIE") = par.IfNull(myReader2("N_VARIE"), "0")
                End If
                myReader2.Close()

                'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_VARIE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'K' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'MF' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'T' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'ST') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_VARIE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'K' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'MF' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'T' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'ST') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_VARIE") = par.IfNull(myReader2("MQ_VARIE"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_COMUNI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'P' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'V') AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_COMUNI") = par.IfNull(myReader2("N_COMUNI"), "0")
                End If
                myReader2.Close()

                'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_COMUNI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'P' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'V') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_COMUNI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'P' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'V') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_COMUNI") = par.IfNull(myReader2("MQ_COMUNI"), "0")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS N_PERTINENZE FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'C' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'G' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SO') AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("N_PERTINENZE") = par.IfNull(myReader2("N_PERTINENZE"), "0")
                End If
                myReader2.Close()

                'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_PERTINENZE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'C' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'G' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SO') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ_PERTINENZE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'C' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'G' OR SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'SO') AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & dt2.Rows(i).Item(1) & ""
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("MQ_PERTINENZE") = par.IfNull(myReader2("MQ_PERTINENZE"), "0")
                End If
                myReader2.Close()

                n_totale = CInt(RIGA.Item("N_ALLOGGI")) + CInt(RIGA.Item("N_COMMERC")) + CInt(RIGA.Item("N_BOX")) + CInt(RIGA.Item("N_VARIE")) + CInt(RIGA.Item("N_PERTINENZE")) + CInt(RIGA.Item("N_COMUNI"))
                mq_totale = CDbl(RIGA.Item("MQ_ALLOGGI")) + CDbl(RIGA.Item("MQ_COMMERC")) + CDbl(RIGA.Item("MQ_BOX")) + CDbl(RIGA.Item("MQ_VARIE")) + CDbl(RIGA.Item("MQ_PERTINENZE")) + CDbl(RIGA.Item("MQ_COMUNI"))
                RIGA.Item("N_TOTALE") = Format(n_totale, "##,##0")
                RIGA.Item("MQ_TOTALE") = Format(mq_totale, "##,##0.00")

                If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                    dt.Rows.Add(RIGA)
                End If

                contAllCIV = contAllCIV + RIGA.Item("N_ALLOGGI")
                contAll2CIV = contAll2CIV + RIGA.Item("MQ_ALLOGGI")
                contCommercCIV = contCommercCIV + RIGA.Item("N_COMMERC")
                contCommerc2CIV = contCommerc2CIV + RIGA.Item("MQ_COMMERC")
                contBoxCIV = contBoxCIV + RIGA.Item("N_BOX")
                contBox2CIV = contBox2CIV + RIGA.Item("MQ_BOX")
                contVarieCIV = contVarieCIV + RIGA.Item("N_VARIE")
                contVarie2CIV = contVarie2CIV + RIGA.Item("MQ_VARIE")
                contPertCIV = contPertCIV + RIGA.Item("N_PERTINENZE")
                contPert2CIV = contPert2CIV + RIGA.Item("MQ_PERTINENZE")
                contComCIV = contComCIV + RIGA.Item("N_COMUNI")
                contCom2CIV = contCom2CIV + RIGA.Item("MQ_COMUNI")
                contTotCIV = contTotCIV + RIGA.Item("N_TOTALE")
                contTot2CIV = contTot2CIV + RIGA.Item("MQ_TOTALE")

                contAllCOMPL = contAllCOMPL + RIGA.Item("N_ALLOGGI")
                contAll2COMPL = contAll2COMPL + RIGA.Item("MQ_ALLOGGI")
                contCommercCOMPL = contCommercCOMPL + RIGA.Item("N_COMMERC")
                contCommerc2COMPL = contCommerc2COMPL + RIGA.Item("MQ_COMMERC")
                contBoxCOMPL = contBoxCOMPL + RIGA.Item("N_BOX")
                contBox2COMPL = contBox2COMPL + RIGA.Item("MQ_BOX")
                contVarieCOMPL = contVarieCOMPL + RIGA.Item("N_VARIE")
                contVarie2COMPL = contVarie2COMPL + RIGA.Item("MQ_VARIE")
                contPertCOMPL = contPertCOMPL + RIGA.Item("N_PERTINENZE")
                contPert2COMPL = contPert2COMPL + RIGA.Item("MQ_PERTINENZE")
                contComCOMPL = contComCOMPL + RIGA.Item("N_COMUNI")
                contCom2COMPL = contCom2COMPL + RIGA.Item("MQ_COMUNI")
                contTotCOMPL = contTotCOMPL + RIGA.Item("N_TOTALE")
                contTot2COMPL = contTot2COMPL + RIGA.Item("MQ_TOTALE")

                contAllGENER = contAllGENER + RIGA.Item("N_ALLOGGI")
                contAll2GENER = contAll2GENER + RIGA.Item("MQ_ALLOGGI")
                contCommercGENER = contCommercGENER + RIGA.Item("N_COMMERC")
                contCommerc2GENER = contCommerc2GENER + RIGA.Item("MQ_COMMERC")
                contBoxGENER = contBoxGENER + RIGA.Item("N_BOX")
                contBox2GENER = contBox2GENER + RIGA.Item("MQ_BOX")
                contVarieGENER = contVarieGENER + RIGA.Item("N_VARIE")
                contVarie2GENER = contVarie2GENER + RIGA.Item("MQ_VARIE")
                contPertGENER = contPertGENER + RIGA.Item("N_PERTINENZE")
                contPert2GENER = contPert2GENER + RIGA.Item("MQ_PERTINENZE")
                contComGENER = contComGENER + RIGA.Item("N_COMUNI")
                contCom2GENER = contCom2GENER + RIGA.Item("MQ_COMUNI")
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
                        RIGA2.Item("N_COMMERC") = Format(contCommercCIV, "##,##0")
                        RIGA2.Item("MQ_COMMERC") = Format(contCommerc2CIV, "##,##0.00")
                        RIGA2.Item("N_BOX") = Format(contBoxCIV, "##,##0")
                        RIGA2.Item("MQ_BOX") = Format(contBox2CIV, "##,##0.00")
                        RIGA2.Item("N_VARIE") = Format(contVarieCIV, "##,##0")
                        RIGA2.Item("MQ_VARIE") = Format(contVarie2CIV, "##,##0.00")
                        RIGA2.Item("N_PERTINENZE") = Format(contPertCIV, "##,##0")
                        RIGA2.Item("MQ_PERTINENZE") = Format(contPert2CIV, "##,##0.00")
                        RIGA2.Item("N_COMUNI") = Format(contComCIV, "##,##0")
                        RIGA2.Item("MQ_COMUNI") = Format(contCom2CIV, "##,##0.00")
                        RIGA2.Item("N_TOTALE") = Format(contTotCIV, "##,##0")
                        RIGA2.Item("MQ_TOTALE") = Format(contTot2CIV, "##,##0.00")
                        If Request.QueryString("TOT") = "1" Or Request.QueryString("TOT") = "0" Then
                            dt.Rows.Add(RIGA2)
                        End If
                        contAllCIV = 0
                        contAll2CIV = 0
                        contCommercCIV = 0
                        contCommerc2CIV = 0
                        contBoxCIV = 0
                        contBox2CIV = 0
                        contVarieCIV = 0
                        contVarie2CIV = 0
                        contPertCIV = 0
                        contPert2CIV = 0
                        contComCIV = 0
                        contCom2CIV = 0
                        contTotCIV = 0
                        contTot2CIV = 0
                    End If
                    If dt2.Rows(i).Item(3) <> dt2.Rows(i + 1).Item(3) Then
                        RIGA2 = dt.NewRow()
                        RIGA2.Item("DENOMINAZIONE") = RIGA.Item("DENOMINAZIONE")
                        RIGA2.Item("INDIRIZZO") = "TOTALE COMPLESSO"
                        RIGA2.Item("N_ALLOGGI") = Format(contAllCOMPL, "##,##0")
                        RIGA2.Item("MQ_ALLOGGI") = Format(contAll2COMPL, "##,##0.00")
                        RIGA2.Item("N_COMMERC") = Format(contCommercCOMPL, "##,##0")
                        RIGA2.Item("MQ_COMMERC") = Format(contCommerc2COMPL, "##,##0.00")
                        RIGA2.Item("N_BOX") = Format(contBoxCOMPL, "##,##0")
                        RIGA2.Item("MQ_BOX") = Format(contBox2COMPL, "##,##0.00")
                        RIGA2.Item("N_VARIE") = Format(contVarieCOMPL, "##,##0")
                        RIGA2.Item("MQ_VARIE") = Format(contVarie2COMPL, "##,##0.00")
                        RIGA2.Item("N_PERTINENZE") = Format(contPertCOMPL, "##,##0")
                        RIGA2.Item("MQ_PERTINENZE") = Format(contPert2COMPL, "##,##0.00")
                        RIGA2.Item("N_COMUNI") = Format(contComCOMPL, "##,##0")
                        RIGA2.Item("MQ_COMUNI") = Format(contCom2COMPL, "##,##0.00")
                        RIGA2.Item("N_TOTALE") = Format(contTotCOMPL, "##,##0")
                        RIGA2.Item("MQ_TOTALE") = Format(contTot2COMPL, "##,##0.00")
                        If Request.QueryString("TOT") = "2" Or Request.QueryString("TOT") = "0" Then
                            dt.Rows.Add(RIGA2)
                        End If
                        contAllCOMPL = 0
                        contAll2COMPL = 0
                        contCommercCOMPL = 0
                        contCommerc2COMPL = 0
                        contBoxCOMPL = 0
                        contBox2COMPL = 0
                        contVarieCOMPL = 0
                        contVarie2COMPL = 0
                        contPertCOMPL = 0
                        contPert2COMPL = 0
                        contComCOMPL = 0
                        contCom2COMPL = 0
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
                        RIGA2.Item("N_COMMERC") = Format(contCommercCIV, "##,##0")
                        RIGA2.Item("MQ_COMMERC") = Format(contCommerc2CIV, "##,##0.00")
                        RIGA2.Item("N_BOX") = Format(contBoxCIV, "##,##0")
                        RIGA2.Item("MQ_BOX") = Format(contBox2CIV, "##,##0.00")
                        RIGA2.Item("N_VARIE") = Format(contVarieCIV, "##,##0")
                        RIGA2.Item("MQ_VARIE") = Format(contVarie2CIV, "##,##0.00")
                        RIGA2.Item("N_PERTINENZE") = Format(contPertCIV, "##,##0")
                        RIGA2.Item("MQ_PERTINENZE") = Format(contPert2CIV, "##,##0.00")
                        RIGA2.Item("N_COMUNI") = Format(contComCIV, "##,##0")
                        RIGA2.Item("MQ_COMUNI") = Format(contCom2CIV, "##,##0.00")
                        RIGA2.Item("N_TOTALE") = Format(contTotCIV, "##,##0")
                        RIGA2.Item("MQ_TOTALE") = Format(contTot2CIV, "##,##0.00")
                        If Request.QueryString("TOT") = "1" Or Request.QueryString("TOT") = "0" Then
                            dt.Rows.Add(RIGA2)
                        End If
                        contAllCIV = 0
                        contAll2CIV = 0
                        contCommercCIV = 0
                        contCommerc2CIV = 0
                        contBoxCIV = 0
                        contBox2CIV = 0
                        contVarieCIV = 0
                        contVarie2CIV = 0
                        contPertCIV = 0
                        contPert2CIV = 0
                        contComCIV = 0
                        contCom2CIV = 0
                        contTotCIV = 0
                        contTot2CIV = 0
                    End If
                    RIGA2 = dt.NewRow()
                    RIGA2.Item("DENOMINAZIONE") = RIGA.Item("DENOMINAZIONE")
                    RIGA2.Item("INDIRIZZO") = "TOTALE COMPLESSO"
                    RIGA2.Item("N_ALLOGGI") = Format(contAllCOMPL, "##,##0")
                    RIGA2.Item("MQ_ALLOGGI") = Format(contAll2COMPL, "##,##0.00")
                    RIGA2.Item("N_COMMERC") = Format(contCommercCOMPL, "##,##0")
                    RIGA2.Item("MQ_COMMERC") = Format(contCommerc2COMPL, "##,##0.00")
                    RIGA2.Item("N_BOX") = Format(contBoxCOMPL, "##,##0")
                    RIGA2.Item("MQ_BOX") = Format(contBox2COMPL, "##,##0.00")
                    RIGA2.Item("N_VARIE") = Format(contVarieCOMPL, "##,##0")
                    RIGA2.Item("MQ_VARIE") = Format(contVarie2COMPL, "##,##0.00")
                    RIGA2.Item("N_PERTINENZE") = Format(contPertCOMPL, "##,##0")
                    RIGA2.Item("MQ_PERTINENZE") = Format(contPert2COMPL, "##,##0.00")
                    RIGA2.Item("N_COMUNI") = Format(contComCOMPL, "##,##0")
                    RIGA2.Item("MQ_COMUNI") = Format(contCom2COMPL, "##,##0.00")
                    RIGA2.Item("N_TOTALE") = Format(contTotCOMPL, "##,##0")
                    RIGA2.Item("MQ_TOTALE") = Format(contTot2COMPL, "##,##0.00")
                    If Request.QueryString("TOT") = "2" Or Request.QueryString("TOT") = "0" Then
                        dt.Rows.Add(RIGA2)
                    End If
                    contAllCOMPL = 0
                    contAll2COMPL = 0
                    contCommercCOMPL = 0
                    contCommerc2COMPL = 0
                    contBoxCOMPL = 0
                    contBox2COMPL = 0
                    contVarieCOMPL = 0
                    contVarie2COMPL = 0
                    contPertCOMPL = 0
                    contPert2COMPL = 0
                    contComCOMPL = 0
                    contCom2COMPL = 0
                    contTotCOMPL = 0
                    contTot2COMPL = 0
                End If

                If Request.QueryString("C") = "-1" And i = dt2.Rows.Count - 1 Then
                    'RIGA = dt.NewRow()
                    'dt.Rows.Add(RIGA)
                    RIGA = dt.NewRow()
                    RIGA.Item("DENOMINAZIONE") = "TOTALE GENERALE"
                    RIGA.Item("N_ALLOGGI") = Format(contAllGENER, "##,##0")
                    RIGA.Item("MQ_ALLOGGI") = Format(contAll2GENER, "##,##0.00")
                    RIGA.Item("N_COMMERC") = Format(contCommercGENER, "##,##0")
                    RIGA.Item("MQ_COMMERC") = Format(contCommerc2GENER, "##,##0.00")
                    RIGA.Item("N_BOX") = Format(contBoxGENER, "##,##0")
                    RIGA.Item("MQ_BOX") = Format(contBox2GENER, "##,##0.00")
                    RIGA.Item("N_VARIE") = Format(contVarieGENER, "##,##0")
                    RIGA.Item("MQ_VARIE") = Format(contVarie2GENER, "##,##0.00")
                    RIGA.Item("N_PERTINENZE") = Format(contPertGENER, "##,##0")
                    RIGA.Item("MQ_PERTINENZE") = Format(contPert2GENER, "##,##0.00")
                    RIGA.Item("N_COMUNI") = Format(contComGENER, "##,##0")
                    RIGA.Item("MQ_COMUNI") = Format(contCom2GENER, "##,##0.00")
                    RIGA.Item("N_TOTALE") = Format(contTotGENER, "##,##0")
                    RIGA.Item("MQ_TOTALE") = Format(contTot2GENER, "##,##0.00")
                    dt.Rows.Add(RIGA)
                End If
            Next

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
            sNomeFile = "Tot_dati_patrim_gruppoUI" & Format(Now, "yyyyMMddHHmm")
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
                .SetColumnWidth(6, 6, 27)
                .SetColumnWidth(7, 7, 24)
                .SetColumnWidth(8, 8, 12)
                .SetColumnWidth(9, 9, 10)
                .SetColumnWidth(10, 10, 13)
                .SetColumnWidth(11, 11, 11)
                .SetColumnWidth(12, 12, 18)
                .SetColumnWidth(13, 13, 18)
                .SetColumnWidth(14, 14, 20)
                .SetColumnWidth(15, 15, 20)
                .SetColumnWidth(16, 16, 15)
                .SetColumnWidth(17, 17, 15)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "Totalizzazioni dati patrimoniali per gruppo UI")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 1, "COMPLESSO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 2, "INDIRIZZO E NUM. CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 3, "EDIFICIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 4, "NUM. ALLOGGI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 5, "MQ ALLOGGI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 6, "NUM. UNITA' COMMERCIALI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 7, "MQ UNITA' COMMERCIALI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 8, "NUM. BOX", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 9, "MQ BOX", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 10, "NUM. VARIE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 11, "MQ VARIE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 12, "NUM. PERTINENZE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 13, "MQ PERTINENZE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 14, "NUM. PARTI COMUNI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 15, "MQ PARTI COMUNI", 0)
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
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("COD_EDIFICIO"), ""))
                    End If
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("N_ALLOGGI"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("MQ_ALLOGGI"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("N_COMMERC"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("MQ_COMMERC"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("N_BOX"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("MQ_BOX"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("N_VARIE"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("MQ_VARIE"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("N_PERTINENZE"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("MQ_PERTINENZE"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(dt.Rows(i).Item("N_COMUNI"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(dt.Rows(i).Item("MQ_COMUNI"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(dt.Rows(i).Item("N_TOTALE"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(dt.Rows(i).Item("MQ_TOTALE"), "0"))
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
