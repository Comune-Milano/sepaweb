Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Math
Imports System.Drawing

Partial Class RiepilogoPerStrutture
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TabellaRiepilogo As String = ""
    Public TabellaThead As String = ""
    Public Titolo As String = ""
    Dim dt2 As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_VARIAZIONI_SL") <> 1 And Session.Item("BP_VARIAZIONI") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblErrore.Visible = False
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
            TabellaThead = "<table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color: #333333;" _
                & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;"">" _
                & "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                & "<td align=""center"" style=""width:7%; font-weight: bold; font-style: normal; text-decoration: none; "">CAP. BIL. COM.</td>" _
                & "<td align=""center"" style=""width:7%;font-weight: bold; font-style: normal; text-decoration: none; "">COD SOTTOVOCE</td>" _
                & "<td align=""center"" style=""width:35%;font-weight: bold; font-style: normal; text-decoration: none; "">DESCRIZIONE VOCI E SOTTOVOCI</td>" _
                & "<td align=""center"" style=""width:27%;font-weight: bold; font-style: normal; text-decoration: none; "">UFFICI</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">PREVISIONE INIZIALE</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">VARIAZIONI</td>" _
                & "<td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">PREVISIONE VARIATA</td><tr></table>"

            If Not IsNothing(Session.Item("DT2DRV")) Then
                Session.Remove("DT2DRV")
            End If
            '§§§§§§§§§§§§§§§§§§§§§§§§
            '#### dt per export ####
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

            '###########  RAGGRUPPAMENTO PER CAPITOLI ##############
            par.cmd.CommandText = "SELECT DISTINCT PF_CAPITOLI.COD,PF_CAPITOLI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_CAPITOLI " _
                & "WHERE (SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID)= 0 " _
                & "AND ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "' AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE AND PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO ORDER BY COD ASC"
            myReader = par.cmd.ExecuteReader

            While myReader.Read

                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                'VECCHIA SELEZIONE DELLE VOCI CHE HANNO SUBITO SOLO VARIAZIONI   

                '########### SELEZIONE DELLE VOCI DEL BILANCIO CHE HANNO SUBITO VARIAZIONI ############
                'par.cmd.CommandText = "SELECT SUM(EVENTI_VARIAZIONI.IMPORTO),PF_VOCI.ID_VOCE_MADRE " _
                '    & "FROM SISCOM_MI.PF_VOCI, SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_CAPITOLI " _
                '    & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                '    & "AND (EVENTI_VARIAZIONI.ID_VOCE_A=PF_VOCI.ID " _
                '    & "OR EVENTI_VARIAZIONI.ID_VOCE_DA=PF_VOCI.ID) " _
                '    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                '    & "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID_VOCE_MADRE"

                'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                'Dim dt As New Data.DataTable()
                'da.Fill(dt)

                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                'NUOVA SELEZIONE DELLE VOCI CHE HANNO SUBITO VARIAZIONI O TRASFERIMENTI FONDI

                '########### SELEZIONE DELLE VOCI DEL BILANCIO CHE HANNO SUBITO VARIAZIONI ############
                par.cmd.CommandText = "SELECT PF_VOCI.ID_VOCE_MADRE " _
                    & "FROM SISCOM_MI.PF_VOCI, SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_CAPITOLI " _
                    & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                    & "AND (EVENTI_VARIAZIONI.ID_VOCE_A=PF_VOCI.ID " _
                    & "OR EVENTI_VARIAZIONI.ID_VOCE_DA=PF_VOCI.ID) " _
                    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    & "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID_VOCE_MADRE " _
                    & "UNION " _
                    & "SELECT pf_voci.id_voce_madre " _
                    & "FROM siscom_mi.pf_voci, siscom_mi.eventi_trasf_fondi,siscom_mi.pf_capitoli " _
                    & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                    & "AND eventi_trasf_fondi.id_voce = pf_voci.ID " _
                    & "AND pf_voci.id_capitolo = pf_capitoli.ID " _
                    & "AND cod = '" & par.IfNull(myReader("COD"), "") & "' " _
                    & "GROUP BY pf_voci.id_voce_madre "

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable()
                da.Fill(dt)

                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°


                For Each RR As Data.DataRow In dt.Rows


                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    'VECCHIO
                    'par.cmd.CommandText = "SELECT SUM(EVENTI_VARIAZIONI.IMPORTO),PF_VOCI.ID " _
                    '& "FROM SISCOM_MI.PF_VOCI, SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_CAPITOLI " _
                    '& "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                    '& "AND (EVENTI_VARIAZIONI.ID_VOCE_A=PF_VOCI.ID " _
                    '& "OR EVENTI_VARIAZIONI.ID_VOCE_DA=PF_VOCI.ID) " _
                    '& "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    '& "AND PF_VOCI.ID_VOCE_MADRE='" & RR.Item("ID_VOCE_MADRE") & "' " _
                    '& "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID"

                    par.cmd.CommandText = "SELECT PF_VOCI.ID " _
                    & "FROM SISCOM_MI.PF_VOCI, SISCOM_MI.EVENTI_VARIAZIONI, SISCOM_MI.PF_CAPITOLI " _
                    & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                    & "AND (EVENTI_VARIAZIONI.ID_VOCE_A=PF_VOCI.ID " _
                    & "OR EVENTI_VARIAZIONI.ID_VOCE_DA=PF_VOCI.ID) " _
                    & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                    & "AND PF_VOCI.ID_VOCE_MADRE='" & RR.Item("ID_VOCE_MADRE") & "' " _
                    & "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID " _
                    & "UNION " _
                    & "SELECT pf_voci.ID " _
                    & "FROM siscom_mi.pf_voci, siscom_mi.eventi_TRASF_FONDI," _
                    & "siscom_mi.pf_capitoli " _
                    & "WHERE ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' " _
                    & "AND eventi_TRASF_FONDI.id_voce = pf_voci.ID " _
                    & "AND pf_voci.id_capitolo = pf_capitoli.ID " _
                    & "AND PF_VOCI.ID_VOCE_MADRE='" & RR.Item("ID_VOCE_MADRE") & "' " _
                    & "AND COD='" & par.IfNull(myReader("COD"), "") & "' GROUP BY PF_VOCI.ID "

                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                    Dim dt1 As New Data.DataTable()
                    da2.Fill(dt1)
                    Dim sommabudget As Decimal
                    Dim sommavariazioni As Decimal
                    Dim sommavariata As Decimal

                    Dim CODICE_MADRE As String = ""
                    Dim CODICE As String = ""

                    Dim totalesommabudget As Decimal = 0
                    Dim totalesommavariazioni As Decimal = 0
                    Dim totalesommavariata As Decimal = 0
                    TabellaRiepilogo = TabellaRiepilogo & "<table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color: #333333;" _
                             & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;"">"

                    For Each r As Data.DataRow In dt1.Rows
                        par.cmd.CommandText = "SELECT A.CODICE AS CODICE_MADRE,COD,PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE," _
                            & "NOME,NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0) AS BUDGET,VARIAZIONI " _
                            & "FROM SISCOM_MI.PF_VOCI_STRUTTURA, SISCOM_MI.PF_VOCI, SISCOM_MI.PF_CAPITOLI, SISCOM_MI.TAB_FILIALI,SISCOM_MI.PF_VOCI A " _
                            & "WHERE PF_VOCI.ID_VOCE_MADRE=A.ID " _
                            & "AND PF_VOCI_STRUTTURA.ID_VOCE = '" & r.Item("ID") & "' " _
                            & "AND PF_VOCI_STRUTTURA.ID_STRUTTURA=TAB_FILIALI.ID " _
                            & "AND PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID " _
                            & "AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE " _
                            & "AND (NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)>0 " _
                            & "OR VARIAZIONI <>0) ORDER BY NOME ASC "
                        Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim NUMR As Integer = 0

                        sommabudget = 0
                        sommavariazioni = 0
                        sommavariata = 0
                        While LETTORE.Read
                            Dim BUDGET As Decimal = par.IfNull(LETTORE("BUDGET"), "")
                            Dim VARIAZIONI As Decimal = par.IfNull(LETTORE("VARIAZIONI"), "")
                            Dim VARIATA As Decimal = BUDGET + VARIAZIONI
                            If NUMR = 0 Then
                                TabellaRiepilogo = TabellaRiepilogo _
                             & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                             & "<td align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("COD"), "") & "</td>" _
                             & "<td align=""center"" style=""width:7%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("CODICE"), "") & "</td>" _
                             & "<td align=""left""  style=""width:35%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("DESCRIZIONE"), "") & "</td>" _
                             & "<td align=""left""  style=""width:27%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("NOME"), "") & "</td>" _
                             & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(BUDGET, "##,##0.00") & "</td>" _
                             & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIAZIONI, "##,##0.00") & "</td>" _
                             & "<td align=""right""  style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIATA, "##,##0.00") & "</td></tr>"
                                NUMR = 1
                                RIGA = dt2.NewRow
                                RIGA.Item("COD") = par.IfNull(LETTORE("COD"), "")
                                RIGA.Item("CODICE") = par.IfNull(LETTORE("CODICE"), "")
                                RIGA.Item("VOCE") = par.IfNull(LETTORE("DESCRIZIONE"), "")
                                RIGA.Item("UFFICI") = par.IfNull(LETTORE("NOME"), "")
                                RIGA.Item("PREVISIONE") = Format(BUDGET, "##,##0.00")
                                RIGA.Item("VARIAZIONI") = Format(VARIAZIONI, "##,##0.00")
                                RIGA.Item("VARIATA") = Format(VARIATA, "##,##0.00")
                                dt2.Rows.Add(RIGA)
                            Else

                                TabellaRiepilogo = TabellaRiepilogo & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                             & "<td align=""center"" style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ></td>" _
                             & "<td align=""center"" style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ></td>" _
                             & "<td align=""left""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ></td>" _
                             & "<td align=""left""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LETTORE("NOME"), "") & "</td>" _
                             & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(BUDGET, "##,##0.00") & "</td>" _
                             & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIAZIONI, "##,##0.00") & "</td>" _
                             & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(VARIATA, "##,##0.00") & "</td></tr>"
                                RIGA = dt2.NewRow
                                RIGA.Item("COD") = ""
                                RIGA.Item("CODICE") = ""
                                RIGA.Item("VOCE") = ""
                                RIGA.Item("UFFICI") = par.IfNull(LETTORE("NOME"), "")
                                RIGA.Item("PREVISIONE") = Format(BUDGET, "##,##0.00")
                                RIGA.Item("VARIAZIONI") = Format(VARIAZIONI, "##,##0.00")
                                RIGA.Item("VARIATA") = Format(VARIATA, "##,##0.00")
                                dt2.Rows.Add(RIGA)

                            End If




                            CODICE = par.IfNull(LETTORE("CODICE"), "")
                            sommabudget = sommabudget + BUDGET
                            sommavariazioni = sommavariazioni + VARIAZIONI
                            sommavariata = sommavariata + VARIATA
                            CODICE_MADRE = par.IfNull(LETTORE("CODICE_MADRE"), "")

                        End While

                        LETTORE.Close()

                        totalesommabudget = totalesommabudget + sommabudget
                        totalesommavariazioni = totalesommavariazioni + sommavariazioni
                        totalesommavariata = totalesommavariata + sommavariata

                        TabellaRiepilogo = TabellaRiepilogo _
                             & "<tr style=""color: red; background-color: #dddddd; font-family: Arial; font-size: 8pt;"">" _
                             & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;""></td>" _
                             & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;""></td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">TOTALE SOTTOVOCE " & CODICE & "</td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;""></td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(sommabudget, "##,##0.00") & "</td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(sommavariazioni, "##,##0.00") & "</td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(sommavariata, "##,##0.00") & "</td></tr>"

                        RIGA = dt2.NewRow
                        RIGA.Item("COD") = ""
                        RIGA.Item("CODICE") = ""
                        RIGA.Item("VOCE") = "TOTALE SOTTOVOCE " & CODICE
                        RIGA.Item("UFFICI") = ""
                        RIGA.Item("PREVISIONE") = Format(sommabudget, "##,##0.00")
                        RIGA.Item("VARIAZIONI") = Format(sommavariazioni, "##,##0.00")
                        RIGA.Item("VARIATA") = Format(sommavariata, "##,##0.00")
                        dt2.Rows.Add(RIGA)

                    Next

                    TabellaRiepilogo = TabellaRiepilogo _
                             & "<tr style=""color: black; background-color: #cccccc; font-family: Arial; font-size: 8pt;"">" _
                             & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;""></td>" _
                             & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;""></td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">TOTALE VOCE " & CODICE_MADRE & "</td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;""></td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(totalesommabudget, "##,##0.00") & "</td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(totalesommavariazioni, "##,##0.00") & "</td>" _
                             & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(totalesommavariata, "##,##0.00") & "</td></tr></table>"

                    RIGA = dt2.NewRow
                    RIGA.Item("COD") = ""
                    RIGA.Item("CODICE") = ""
                    RIGA.Item("VOCE") = "TOTALE SOTTOVOCE " & CODICE_MADRE
                    RIGA.Item("UFFICI") = ""
                    RIGA.Item("PREVISIONE") = Format(totalesommabudget, "##,##0.00")
                    RIGA.Item("VARIAZIONI") = Format(totalesommavariazioni, "##,##0.00")
                    RIGA.Item("VARIATA") = Format(totalesommavariata, "##,##0.00")
                    dt2.Rows.Add(RIGA)

                Next

            End While
            myReader.Close()

            If dt2.Rows.Count = 0 Then
                TabellaRiepilogo = ""
                TabellaThead = ""
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "Non sono presenti variazioni per l'esercizio finanziario selezionato!"
                lblErrore.Visible = True
            End If
            Session.Add("DT2DRV", dt2)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            btnExport.Visible = False
            btnStampaPDF.Visible = False
            TabellaRiepilogo = ""
            TabellaThead = ""
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
            datatable = CType(HttpContext.Current.Session.Item("DT2DRV"), Data.DataTable)
            sNomeFile = "RiepilogoVariazioniPerStrutture" & Titolo & "_" & Format(Now, "yyyyMMddHHmm")
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
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CAP. BIL. COM.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD SOTTOVOCE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DESCRIZIONE VOCI E SOTTOVOCI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "UFFICI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PREVISIONE INIZIALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "VARIAZIONI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "PREVISIONE VARIATA", 0)
                K = 2
                For Each row In datatable.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, datatable.Rows(i).Item("COD"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, datatable.Rows(i).Item("CODICE"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, datatable.Rows(i).Item("VOCE"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, datatable.Rows(i).Item("UFFICI"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, datatable.Rows(i).Item("PREVISIONE"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, datatable.Rows(i).Item("VARIAZIONI"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, datatable.Rows(i).Item("VARIATA"))
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
            Session.Remove("DT2DRV")
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnStampaPDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampaPDF.Click
        '### STAMPA PDF ###
        Try
            Dim NomeFile As String = "RiepilogoVariazioniPerStrutture_" & Titolo & "_" & Format(Now, "yyyyMMddHHmmss")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            Dim TABELLA As String = "<html><head></head><body>" & TabellaThead & TabellaRiepilogo & "</table></body></html>"
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
            pdfConverter.PdfHeaderOptions.HeaderText = "Riepilogo Variazioni tra Voci e tra Strutture - Dettaglio per Strutture (Esercizio Finanziario " & Titolo & ")"
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
            IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
            'Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".pdf")
            FIN.Value = "1"
            Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & ".pdf','StampeStrutture');</script>")

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

        Session.Remove("DT2DRV")

    End Sub

End Class