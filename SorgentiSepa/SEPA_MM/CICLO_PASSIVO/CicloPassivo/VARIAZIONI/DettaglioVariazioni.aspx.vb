Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Math
Imports System.Drawing

Partial Class DettaglioVariazioni

    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TabellaRiepilogo As String = ""
    Public TabellaIntestazione As String = ""
    Public Titolo As String = ""
    Public tabellaVociPositive As String = ""
    Dim TabellaIntestazioneGen As String = ""
    Dim dt2 As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_VARIAZIONI_SL") <> 1 And Session.Item("BP_VARIAZIONI") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblErrore.Visible = False
        TabellaRiepilogo = ""
        TabellaIntestazione = ""
        TabellaIntestazioneGen = ""
        CaricaVoci()

    End Sub

    Protected Sub CaricaVoci()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'ESERCIZIO FINANZIARIO
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                & "WHERE T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                & "AND PF_MAIN.ID='" & Request.QueryString("AN") & "' "


            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                Dim ANNO As String = par.IfNull(myReader("INIZIO"), "")
                If Len(ANNO) = 8 Then
                    Titolo = Left(ANNO, 4)
                End If
            End If
            myReader.Close()

            '##### QUERY VOCI #####
            'SELECT * FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID = PF_VOCI.ID_CAPITOLO AND id_piano_finanziario = 1 AND id_voce_madre 
            'IS NOT NULL AND INSTR(CODICE,'.',1,3)=0 
            '######################

            'INTESTAZIONE TABELLA
            TabellaIntestazione = "<table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color: #333333;" _
                & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;"">" _
                & "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                & "<td align=""center"" style=""width:7%; font-weight: bold; font-style: normal; text-decoration: none; "">CAP. BIL. COM.</td>" _
                & "<td align=""center"" style=""width:7%;font-weight: bold; font-style: normal; text-decoration: none; "">COD SOTTOVOCE</td>" _
                & "<td align=""center"" style=""width:35%;font-weight: bold; font-style: normal; text-decoration: none; "">DESCRIZIONE VOCI E SOTTOVOCI</td>" _
                & "<td align=""center"" style=""width:27%;font-weight: bold; font-style: normal; text-decoration: none; "">UFFICI</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">PREVISIONE INIZIALE</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">VARIAZIONI</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">PREVISIONE VARIATA</td><tr></table>"

            TabellaIntestazioneGen = "<table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color: #333333;" _
                & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;"">" _
                & "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                & "<td align=""center"" style=""width:7%; font-weight: bold; font-style: normal; text-decoration: none; "">CAP. BIL. COM.</td>" _
                & "<td align=""center"" style=""width:7%;font-weight: bold; font-style: normal; text-decoration: none; "">COD</td>" _
                & "<td align=""center"" style=""width:62%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">FINANZIAMENTI ATTRIBUITI</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">VARIAZIONI</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">FINANZIAMENTI VARIATI</td><tr>"

            If Not IsNothing(Session.Item("DT2DV")) Then
                Session.Remove("DT2DV")
            End If
            '§§§§§§§§§§§§§§§§§§§§§§§§
            '#### dt per export pdf e excel####
            dt2.Clear()
            dt2.Columns.Clear()
            dt2.Columns.Add("COD")
            dt2.Columns.Add("CODICE")
            dt2.Columns.Add("VOCE")
            dt2.Columns.Add("UFFICI")
            dt2.Columns.Add("PREVISIONE")
            dt2.Columns.Add("VARIAZIONI")
            dt2.Columns.Add("VARIATA")
            '§§§§§§§§§§§§§§§§§§§§§§§§
            Dim RIGA As Data.DataRow

            'TITOLI PRIMO BLOCCO
            RIGA = dt2.NewRow
            RIGA.Item("COD") = "CAP. BIL. COM."
            RIGA.Item("CODICE") = "COD"
            RIGA.Item("VOCE") = "VOCE"
            RIGA.Item("UFFICI") = ""
            RIGA.Item("PREVISIONE") = "FINANZIAMENTI ATTRIBUITI"
            RIGA.Item("VARIAZIONI") = "VARIAZIONI"
            RIGA.Item("VARIATA") = "FINANZIAMENTI VARIATI"
            dt2.Rows.Add(RIGA)


            '#################### SEZIONE DEL RIEPILOGO INIZIALE #####################

            '1) RAGGRUPPAMENTO PER CAPITOLI PER LE VOCI DI DESTINAZIONE
            par.cmd.CommandText = "SELECT DISTINCT COD FROM SISCOM_MI.EVENTI_VARIAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI " _
                    & "WHERE PF_VOCI.ID = EVENTI_VARIAZIONI.ID_VOCE_A " _
                    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    & "ORDER BY COD "
            Dim lettoreVociDestinazione As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While lettoreVociDestinazione.Read

                '2) PER OGNI CAPITOLO RAGGRUPPARE LE VOCI DI DESTINAZIONE UGUALI
                '****************************************************************************************
                par.cmd.CommandText = "SELECT MADRE.ID AS ID,MADRE.CODICE,PF_CAPITOLI.COD,MADRE.DESCRIZIONE,SUM(VARIAZIONI) AS VARIAZIONI,'0' AS F " _
                    & "FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI MADRE,SISCOM_MI.PF_CAPITOLI " _
                    & "WHERE VARIAZIONI <> 0 " _
                    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    & "AND INSTR(MADRE.CODICE,'.',1,2)<>0 " _
                    & "AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE AND MADRE.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "AND COD='" & par.IfNull(lettoreVociDestinazione("COD"), "") & "' " _
                    & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "' " _
                    & "GROUP BY MADRE.ID,PF_CAPITOLI.COD,MADRE.DESCRIZIONE,MADRE.CODICE " _
                    & "UNION " _
                    & "SELECT PF_VOCI.ID AS ID,PF_VOCI.CODICE,PF_CAPITOLI.COD,PF_VOCI.DESCRIZIONE,SUM(VARIAZIONI) AS VARIAZIONI,'1' AS F " _
                    & "FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI MADRE,SISCOM_MI.PF_CAPITOLI " _
                    & "WHERE VARIAZIONI<>0 " _
                    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    & "AND INSTR(MADRE.CODICE,'.',1,2)=0 " _
                    & "AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE AND MADRE.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "AND COD='" & par.IfNull(lettoreVociDestinazione("COD"), "") & "' " _
                    & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "' " _
                    & "GROUP BY PF_VOCI.ID,PF_CAPITOLI.COD,PF_VOCI.DESCRIZIONE,PF_VOCI.CODICE"


                Dim lettoreVociPositive As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim totaleBudgetP As Decimal = 0
                Dim totaleVariazioniP As Decimal = 0
                Dim totaleFinanziamentiVariatiP As Decimal = 0
                Dim pres As Integer = 0
                While lettoreVociPositive.Read
                    pres = 1
                    '3) PER OGNI RIGA DETERMINARE IL BUDGET TOTALE

                    If par.IfNull(lettoreVociPositive("F"), 1) = 0 Then
                        par.cmd.CommandText = "SELECT SUM(NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)) " _
                        & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                        & "WHERE ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE='" & par.IfNull(lettoreVociPositive("ID"), "") & "')"
                    Else
                        par.cmd.CommandText = "SELECT SUM(NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)) " _
                        & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                        & "WHERE ID_VOCE='" & par.IfNull(lettoreVociPositive("ID"), "") & "'"
                    End If

                    Dim lettoreBudget As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim budgetP As Decimal = 0
                    If lettoreBudget.Read Then
                        budgetP = par.IfNull(lettoreBudget(0), 0)
                    End If
                    lettoreBudget.Close()
                    Dim variazioniP As Decimal = par.IfNull(lettoreVociPositive("VARIAZIONI"), 0)
                    Dim finanziamentiVariati As Decimal = budgetP + variazioniP
                    tabellaVociPositive = tabellaVociPositive _
                                & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                             & "<td align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(lettoreVociPositive("COD"), "") & "</td>" _
                             & "<td align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(lettoreVociPositive("CODICE"), "") & "</td>" _
                             & "<td align=""left"" style=""width:62%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(lettoreVociPositive("DESCRIZIONE"), "") & "</td>" _
                             & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(budgetP, "##,##0.00") & "</td>" _
                             & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(variazioniP, "##,##0.00") & "</td>" _
                             & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(finanziamentiVariati, "##,##0.00") & "</td></tr>"
                    totaleBudgetP = totaleBudgetP + budgetP
                    totaleVariazioniP = totaleVariazioniP + variazioniP
                    totaleFinanziamentiVariatiP = totaleFinanziamentiVariatiP + finanziamentiVariati


                    RIGA = dt2.NewRow
                    RIGA.Item("COD") = par.IfNull(lettoreVociPositive("COD"), "")
                    RIGA.Item("CODICE") = par.IfNull(lettoreVociPositive("CODICE"), "")
                    RIGA.Item("VOCE") = par.IfNull(lettoreVociPositive("DESCRIZIONE"), "")
                    RIGA.Item("UFFICI") = ""
                    RIGA.Item("PREVISIONE") = Format(budgetP, "##,##0.00")
                    RIGA.Item("VARIAZIONI") = Format(variazioniP, "##,##0.00")
                    RIGA.Item("VARIATA") = Format(finanziamentiVariati, "##,##0.00")
                    dt2.Rows.Add(RIGA)


                End While
                lettoreVociPositive.Close()

                '****************************************************************************************
                Dim totaleBudgetN As Decimal = 0
                Dim totaleVariazioniN As Decimal = 0
                Dim totaleFinanziamentiVariatiN As Decimal = 0
                If pres = 1 Then
                    'RIEPILOGO SOMME TOTALI
                    tabellaVociPositive = tabellaVociPositive & "<tr style=""color:black;background-color:#cccccc; font-family:Arial;font-size:8pt; "">" _
                        & "<td style=""font-weight: normal; font-style: normal; text-decoration: none; "">&nbsp;</td>" _
                        & "<td style=""font-weight: normal; font-style: normal; text-decoration: none; "">&nbsp;</td>" _
                        & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE</td>" _
                        & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none; "">" & Format(totaleBudgetN + totaleBudgetP, "##,##0.00") & "</td>" _
                        & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none; "">" & Format(totaleVariazioniN + totaleVariazioniP, "##,##0.00") & "</td>" _
                        & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none; "">" & Format(totaleFinanziamentiVariatiN + totaleFinanziamentiVariatiP, "##,##0.00") & "</td></tr>"

                    RIGA = dt2.NewRow
                    RIGA.Item("COD") = ""
                    RIGA.Item("CODICE") = ""
                    RIGA.Item("VOCE") = "TOTALE"
                    RIGA.Item("PREVISIONE") = Format(totaleBudgetN + totaleBudgetP, "##,##0.00")
                    RIGA.Item("VARIAZIONI") = Format(totaleVariazioniN + totaleVariazioniP, "##,##0.00")
                    RIGA.Item("VARIATA") = Format(totaleFinanziamentiVariatiN + totaleFinanziamentiVariatiP, "##,##0.00")
                    dt2.Rows.Add(RIGA)

                End If

            End While
            lettoreVociDestinazione.Close()

            '#########################################################################

            RIGA = dt2.NewRow
            RIGA.Item("COD") = ""
            RIGA.Item("CODICE") = ""
            RIGA.Item("VOCE") = ""
            RIGA.Item("UFFICI") = ""
            RIGA.Item("PREVISIONE") = ""
            RIGA.Item("VARIAZIONI") = ""
            RIGA.Item("VARIATA") = ""
            dt2.Rows.Add(RIGA)

            'TITOLI SECONDO BLOCCO
            RIGA = dt2.NewRow
            RIGA.Item("COD") = "CAP. BIL. COM."
            RIGA.Item("CODICE") = "COD SOTTOVOCE"
            RIGA.Item("VOCE") = "DESCRIZIONE VOCI E SOTTOVOCI"
            RIGA.Item("UFFICI") = "UFFICI"
            RIGA.Item("PREVISIONE") = "PREVISIONE INIZIALE"
            RIGA.Item("VARIAZIONI") = "VARIAZIONI"
            RIGA.Item("VARIATA") = "PREVISIONE VARIATA"
            dt2.Rows.Add(RIGA)

            '################ RAGGRUPPAMENTO PER DATA #################
            par.cmd.CommandText = "SELECT DISTINCT TO_CHAR(TO_DATE(DATA_ORA,'yyyyMMddHH24MISS'),'dd/mm/yyyy') AS DATA_ORA " _
                & "FROM SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_VOCI " _
                & "WHERE EVENTI_VARIAZIONI.ID_VOCE_DA = PF_VOCI.ID " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "' ORDER BY DATA_ORA ASC"

            Dim lettoreData As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While lettoreData.Read

                '###########  RAGGRUPPAMENTO PER CAPITOLI ##############
                par.cmd.CommandText = "SELECT DISTINCT COD FROM SISCOM_MI.EVENTI_VARIAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI " _
                    & "WHERE PF_VOCI.ID = EVENTI_VARIAZIONI.ID_VOCE_DA " _
                    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    & "AND TO_CHAR(TO_DATE(EVENTI_VARIAZIONI.DATA_ORA,'yyyyMMddHH24MISS'),'dd/mm/yyyy')='" & lettoreData("DATA_ORA") & "' " _
                    & "ORDER BY COD"

                myReader = par.cmd.ExecuteReader

                While myReader.Read

                    '########### SELEZIONE DELLE VOCI DEL BILANCIO CHE HANNO SUBITO VARIAZIONI ############
                    par.cmd.CommandText = "SELECT SUM(EVENTI_VARIAZIONI.IMPORTO),PF_VOCI.ID_VOCE_MADRE " _
                        & "FROM SISCOM_MI.PF_VOCI, SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_CAPITOLI " _
                        & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                        & "AND EVENTI_VARIAZIONI.ID_VOCE_DA=PF_VOCI.ID " _
                        & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                        & "AND TO_CHAR(TO_DATE(DATA_ORA,'yyyyMMddHH24MISS'),'dd/mm/yyyy')='" & par.IfNull(lettoreData("DATA_ORA"), "") & "' " _
                        & "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID_VOCE_MADRE"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                    Dim dt As New Data.DataTable()
                    da.Fill(dt)


                    For Each RR As Data.DataRow In dt.Rows

                        par.cmd.CommandText = "SELECT SUM(EVENTI_VARIAZIONI.IMPORTO),PF_VOCI.ID " _
                        & "FROM SISCOM_MI.PF_VOCI, SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_CAPITOLI " _
                        & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                        & "AND EVENTI_VARIAZIONI.ID_VOCE_DA=PF_VOCI.ID " _
                        & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                        & "AND TO_CHAR(TO_DATE(DATA_ORA,'yyyyMMddHH24MISS'),'dd/mm/yyyy')='" & par.IfNull(lettoreData("DATA_ORA"), "") & "' " _
                        & "AND PF_VOCI.ID_VOCE_MADRE='" & RR.Item("ID_VOCE_MADRE") & "' " _
                        & "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID"

                        Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                        Dim dt1 As New Data.DataTable()
                        da2.Fill(dt1)
                        Dim sommabudget As Decimal
                        Dim sommavariazioni As Decimal
                        Dim sommavariata As Decimal

                        Dim CODICE_MADRE As String = ""
                        Dim CODICE As String = ""
                        Dim DESCRIZIONE As String = ""
                        Dim DESCRIZIONE_MADRE As String = ""

                        Dim totalesommabudget As Decimal = 0
                        Dim totalesommavariazioni As Decimal = 0
                        Dim totalesommavariata As Decimal = 0

                        TabellaRiepilogo = TabellaRiepilogo & "<br /><br /><table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color: #333333;" _
                                 & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse; font-family: Arial; font-size: 8pt;""><tr><td align=""left"">Variazione del " & lettoreData("DATA_ORA") & "</td></tr></table>" & TabellaIntestazione & "<table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" style=""color: #333333;" _
                                 & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;"">"

                        Dim INDICE As Integer = 0
                        For Each r As Data.DataRow In dt1.Rows


                            par.cmd.CommandText = "SELECT DISTINCT A.DESCRIZIONE AS DESCRIZIONE_MADRE,A.CODICE AS CODICE_MADRE,COD,SISCOM_MI.PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE,NOME,(NVL(VALORE_LORDO,0)+(NVL(ASSESTAMENTO_VALORE_LORDO,0))) AS BUDGET,-IMPORTO AS VARIAZIONI " _
                                & "FROM SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.PF_CAPITOLI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.PF_VOCI_STRUTTURA, SISCOM_MI.PF_VOCI A " _
                                & "WHERE " _
                                & "A.ID=PF_VOCI.ID_VOCE_MADRE AND " _
                                & "EVENTI_VARIAZIONI.ID_STRUTTURA=TAB_FILIALI.ID AND " _
                                & "EVENTI_VARIAZIONI.ID_STRUTTURA=PF_VOCI_STRUTTURA.ID_STRUTTURA AND " _
                                & "PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND " _
                                & "TO_CHAR(TO_DATE(DATA_ORA,'yyyyMMddHH24MISS'),'dd/mm/yyyy')='" & lettoreData("DATA_ORA") & "' AND " _
                                & "ID_VOCE_DA='" & r.Item("ID") & "' AND " _
                                & "PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID AND " _
                                & "PF_VOCI.ID=ID_VOCE_DA AND " _
                                & "COD='" & par.IfNull(myReader("COD"), "") & "' "


                            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                            sommabudget = 0
                            sommavariazioni = 0
                            sommavariata = 0


                            While LETTORE.Read
                                INDICE = INDICE + 1
                                Dim BUDGET As Decimal = par.IfNull(LETTORE("BUDGET"), "")
                                Dim VARIAZIONI As Decimal = par.IfNull(LETTORE("VARIAZIONI"), "")
                                Dim VARIATA As Decimal = BUDGET + VARIAZIONI
                                Dim righe As Integer = dt1.Rows.Count
                                If INDICE = 1 Then
                                    TabellaRiepilogo = TabellaRiepilogo _
                                    & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                                 & "<td  rowspan=""" & righe & """ align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("COD"), "") & "</td>" _
                                 & "<td align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("CODICE"), "") & "</td>" _
                                 & "<td align=""left"" style=""width:35%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("DESCRIZIONE"), "") & "</td>" _
                                 & "<td align=""left"" style=""width:27%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("NOME"), "") & "</td>" _
                                 & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(BUDGET, "##,##0.00") & "</td>" _
                                 & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIAZIONI, "##,##0.00") & "</td>" _
                                 & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIATA, "##,##0.00") & "</td></tr>"
                                Else
                                    TabellaRiepilogo = TabellaRiepilogo _
                                    & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                                 & "<td align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("CODICE"), "") & "</td>" _
                                 & "<td align=""left"" style=""width:35%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("DESCRIZIONE"), "") & "</td>" _
                                 & "<td align=""left"" style=""width:27%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("NOME"), "") & "</td>" _
                                 & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(BUDGET, "##,##0.00") & "</td>" _
                                 & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIAZIONI, "##,##0.00") & "</td>" _
                                 & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIATA, "##,##0.00") & "</td></tr>"
                                End If

                                RIGA = dt2.NewRow
                                RIGA.Item("COD") = par.IfNull(LETTORE("COD"), "")
                                RIGA.Item("CODICE") = par.IfNull(LETTORE("CODICE"), "")
                                RIGA.Item("VOCE") = par.IfNull(LETTORE("DESCRIZIONE"), "")
                                RIGA.Item("UFFICI") = par.IfNull(LETTORE("NOME"), "")
                                RIGA.Item("PREVISIONE") = Format(BUDGET, "##,##0.00")
                                RIGA.Item("VARIAZIONI") = Format(VARIAZIONI, "##,##0.00")
                                RIGA.Item("VARIATA") = Format(VARIATA, "##,##0.00")
                                dt2.Rows.Add(RIGA)

                                CODICE = par.IfNull(LETTORE("CODICE"), "")
                                DESCRIZIONE = par.IfNull(LETTORE("DESCRIZIONE"), "")

                                CODICE_MADRE = par.IfNull(LETTORE("CODICE_MADRE"), "")
                                DESCRIZIONE_MADRE = par.IfNull(LETTORE("DESCRIZIONE_MADRE"), "")

                                sommabudget = sommabudget + BUDGET
                                sommavariazioni = sommavariazioni + VARIAZIONI
                                sommavariata = sommavariata + VARIATA

                            End While

                            LETTORE.Close()

                            totalesommabudget = totalesommabudget + sommabudget
                            totalesommavariazioni = totalesommavariazioni + sommavariazioni
                            totalesommavariata = totalesommavariata + sommavariata

                        Next


                        Dim livelloVoce = Len(CODICE) - Len(Replace(CODICE, ".", ""))
                        If livelloVoce = 2 Then
                            CODICE_MADRE = CODICE
                            DESCRIZIONE_MADRE = DESCRIZIONE
                        End If

                        TabellaRiepilogo = TabellaRiepilogo _
                                 & "<tr style=""color: black; background-color: #cccccc; font-family: Arial; font-size: 8pt;"">" _
                                 & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;""></td>" _
                                 & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"">" & CODICE_MADRE & "</td>" _
                                 & "<td align=""left"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & DESCRIZIONE_MADRE & "</td>" _
                                 & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">TOTALE</td>" _
                                 & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(totalesommabudget, "##,##0.00") & "</td>" _
                                 & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(totalesommavariazioni, "##,##0.00") & "</td>" _
                                 & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(totalesommavariata, "##,##0.00") & "</td></tr></table>"

                        RIGA = dt2.NewRow
                        RIGA.Item("COD") = ""
                        RIGA.Item("CODICE") = CODICE_MADRE
                        RIGA.Item("VOCE") = "TOTALE"
                        RIGA.Item("UFFICI") = ""
                        RIGA.Item("PREVISIONE") = Format(totalesommabudget, "##,##0.00")
                        RIGA.Item("VARIAZIONI") = Format(totalesommavariazioni, "##,##0.00")
                        RIGA.Item("VARIATA") = Format(totalesommavariata, "##,##0.00")
                        dt2.Rows.Add(RIGA)
                        TabellaRiepilogo = TabellaRiepilogo & "</table>"
                    Next

                End While
                myReader.Close()

            End While
            lettoreData.Close()

            If tabellaVociPositive <> "" Then
                tabellaVociPositive = TabellaIntestazioneGen & tabellaVociPositive & "<table>"
                Session.Add("DT2DV", dt2)
            Else
                'NON SONO PRESENTI
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                TabellaRiepilogo = ""
                TabellaIntestazione = ""
                lblErrore.Text = "Non sono presenti variazioni per l'esercizio finanziario selezionato!"
                lblErrore.Visible = True

            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            btnExport.Visible = False
            btnStampaPDF.Visible = False
            TabellaRiepilogo = ""
            TabellaIntestazione = ""
            lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        '#### EXPORT IN EXCEL ####

        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim datatable As Data.DataTable
            'Dim datatableRiepilogo As Data.DataTable
            datatable = CType(HttpContext.Current.Session.Item("DT2DV"), Data.DataTable)


            sNomeFile = "DettaglioVariazioni" & Titolo & "_" & Format(Now, "yyyyMMddHHmmss")
            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetColumnWidth(1, 1, 20)
                .SetColumnWidth(2, 2, 20)
                .SetColumnWidth(3, 3, 120)
                .SetColumnWidth(4, 4, 70)
                .SetColumnWidth(5, 5, 30)
                .SetColumnWidth(6, 6, 30)
                .SetColumnWidth(7, 7, 30)
                K = 1
                For Each row In datatable.Rows
                    If datatable.Rows(i).Item("COD") = "CAP. BIL. COM." Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, datatable.Rows(i).Item("COD"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, datatable.Rows(i).Item("CODICE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, datatable.Rows(i).Item("VOCE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, datatable.Rows(i).Item("UFFICI"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, datatable.Rows(i).Item("PREVISIONE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, datatable.Rows(i).Item("VARIAZIONI"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, datatable.Rows(i).Item("VARIATA"))
                    Else
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, datatable.Rows(i).Item("COD"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, datatable.Rows(i).Item("CODICE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, datatable.Rows(i).Item("VOCE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, datatable.Rows(i).Item("UFFICI"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, datatable.Rows(i).Item("PREVISIONE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, datatable.Rows(i).Item("VARIAZIONI"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, datatable.Rows(i).Item("VARIATA"))
                    End If

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
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
            Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
            
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        Session.Remove("DT2DV")

    End Sub

    Protected Sub btnStampaPDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampaPDF.Click
        '### STAMPA PDF ###
        Try
            Dim NomeFile As String = "DettaglioVariazioni_" & Titolo & "_" & Format(Now, "yyyyMMddHHmmss")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            Dim TABELLA As String = "<html><head></head><body>" & tabellaVociPositive & TabellaRiepilogo & "</body></html>"
            sr.WriteLine(TABELLA)
            sr.Close()
            Dim url As String = NomeFile
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            Dim pdfConverter As PdfConverter = New PdfConverter
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PageWidth = 1100
            pdfConverter.PdfDocumentOptions.ShowHeader = True
            pdfConverter.PdfDocumentOptions.ShowFooter = True
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfHeaderOptions.HeaderText = "Dettaglio Variazioni tra Voci (Esercizio Finanziario " & Titolo & ")"
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
            IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
            'Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".pdf")
            FIN.Value = "1"
            Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & ".pdf','StampeDettaglio');</script>")

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        Session.Remove("DT2DV")

    End Sub

End Class