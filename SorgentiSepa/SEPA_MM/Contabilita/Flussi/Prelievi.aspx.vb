Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb


Partial Class Contabilita_Flussi_Prelievi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""
        Dim Tabella As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Try
                Response.Flush()
                Dim importo As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim tot As Double = 0
                Dim Periodo As String = Request.QueryString("P")

                Dim tot1 As Double = 0
                Dim tot2 As Double = 0
                Dim tot3 As Double = 0


                DT.Columns.Add("COMPETENZA")
                DT.Columns.Add("VOCE")
                DT.Columns.Add("RIFERIMENTO")
                DT.Columns.Add("IMPORTO")


                Dim RIGA As System.Data.DataRow

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "Prelievi di " & par.DeCripta(Request.QueryString("T"))
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "COMUNE"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)



                Tabella = "<p style='font-family: arial; font-size: 12pt; font-weight: bold'>Prelievi di " & par.DeCripta(Request.QueryString("T")) & "</p><br/><br/><table style='width:100%;'>"
                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COMPETENZA COMUNE</td><td>VOCE</td><td>RIFERIMENTO PERIODO</td><td style='text-align: right'>IMPORTO</td></tr>"

                par.cmd.CommandText = "select * from siscom_mi.incassi_mesi where vecchio=0 and competenza=1 and periodo=" & Periodo & " ORDER BY RIFERIMENTO ASC,VOCE ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read
                    importo = ""
                    If myReader("IMPORTO") < 1 And myReader("IMPORTO") > -1 Then
                        importo = "0" & Format(myReader("IMPORTO"), "##,##0.00")
                    Else
                        importo = Format(myReader("IMPORTO"), "##,##0.00")
                    End If

                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & myReader("VOCE") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4) & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'>" & importo & "</td></tr>"
                    tot = tot + myReader("IMPORTO")

                    RIGA = DT.NewRow()
                    RIGA.Item("COMPETENZA") = ""
                    RIGA.Item("VOCE") = myReader("VOCE")
                    RIGA.Item("RIFERIMENTO") = par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4)
                    RIGA.Item("IMPORTO") = importo
                    DT.Rows.Add(RIGA)

                Loop
                myReader.Close()
                If tot >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If

                tot1 = tot1 + tot


                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "GESTORE"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"
                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COMPETENZA GESTORE</td><td>VOCE</td><td>RIFERIMENTO PERIODO</td><td style='text-align: right'>IMPORTO</td></tr>"
                tot = 0
                par.cmd.CommandText = "select * from siscom_mi.incassi_mesi where vecchio=0 and competenza=2 and periodo=" & Periodo & " ORDER BY RIFERIMENTO ASC,VOCE ASC"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read

                    importo = ""
                    If myReader("IMPORTO") < 1 And myReader("IMPORTO") > -1 Then
                        importo = "0" & Format(myReader("IMPORTO"), "##,##0.00")
                    Else
                        importo = Format(myReader("IMPORTO"), "##,##0.00")
                    End If

                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & myReader("VOCE") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4) & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'>" & importo & "</td></tr>"
                    tot = tot + myReader("IMPORTO")

                    RIGA = DT.NewRow()
                    RIGA.Item("COMPETENZA") = ""
                    RIGA.Item("VOCE") = myReader("VOCE")
                    RIGA.Item("RIFERIMENTO") = par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4)
                    RIGA.Item("IMPORTO") = importo
                    DT.Rows.Add(RIGA)

                Loop
                myReader.Close()
                If tot >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If
                tot1 = tot1 + tot

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "QUOTE SINDACALI"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>QUOTE SINDACALI</td><td>VOCE</td><td>RIFERIMENTO PERIODO</td><td style='text-align: right'>IMPORTO</td></tr>"
                tot = 0
                par.cmd.CommandText = "select * from siscom_mi.incassi_mesi where vecchio=0 and (competenza=3 OR competenza=10 OR competenza=11 OR competenza=12) and periodo=" & Periodo & " ORDER BY RIFERIMENTO ASC,VOCE ASC"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read

                    importo = ""
                    If myReader("IMPORTO") < 1 And myReader("IMPORTO") > -1 Then
                        importo = "0" & Format(myReader("IMPORTO"), "##,##0.00")
                    Else
                        importo = Format(myReader("IMPORTO"), "##,##0.00")
                    End If


                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & myReader("VOCE") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4) & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'>" & importo & "</td></tr>"
                    tot = tot + myReader("IMPORTO")

                    RIGA = DT.NewRow()
                    RIGA.Item("COMPETENZA") = ""
                    RIGA.Item("VOCE") = myReader("VOCE")
                    RIGA.Item("RIFERIMENTO") = par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4)
                    RIGA.Item("IMPORTO") = importo
                    DT.Rows.Add(RIGA)


                Loop
                myReader.Close()
                tot1 = tot1 + tot
                If tot >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If


                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = ""
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = "TOTALE"
                RIGA.Item("IMPORTO") = tot1
                DT.Rows.Add(RIGA)

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>&nbsp;</td></tr>"
                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>Totale</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot1, "##,##0.00") & "</td></tr>"
                Tabella = Tabella & "</table>"


                Tabella = Tabella & "<p style='page-break-before: always'>&nbsp;</p><br/><br/><p style='font-family: arial; font-size: 10pt; font-weight: bold'>Prelievi di " & par.DeCripta(Request.QueryString("T")) & " - QUOTE CON DATA VALUTA PRECEDENTI A " & par.DeCripta(Request.QueryString("T")) & " MA NON ANCORA PRELEVATE</p><br/><br/>"

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "Prelievi di " & par.DeCripta(Request.QueryString("T")) & " - QUOTE CON DATA VALUTA PRECEDENTI A " & par.DeCripta(Request.QueryString("T")) & " MA NON ANCORA PRELEVATE"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "COMUNE"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                tot = 0
                Tabella = Tabella & "<table style='width:100%;'>"
                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COMPETENZA COMUNE</td><td>VOCE</td><td>RIFERIMENTO PERIODO</td><td style='text-align: right'>IMPORTO</td></tr>"

                par.cmd.CommandText = "select * from siscom_mi.incassi_mesi where vecchio=1 and competenza=1 and periodo=" & Periodo & " ORDER BY RIFERIMENTO ASC,VOCE ASC"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read

                    importo = ""
                    If myReader("IMPORTO") < 1 And myReader("IMPORTO") > -1 Then
                        importo = "0" & Format(myReader("IMPORTO"), "##,##0.00")
                    Else
                        importo = Format(myReader("IMPORTO"), "##,##0.00")
                    End If

                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & myReader("VOCE") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4) & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'>" & importo & "</td></tr>"
                    tot = tot + myReader("IMPORTO")

                    RIGA = DT.NewRow()
                    RIGA.Item("COMPETENZA") = ""
                    RIGA.Item("VOCE") = myReader("VOCE")
                    RIGA.Item("RIFERIMENTO") = par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4)
                    RIGA.Item("IMPORTO") = importo
                    DT.Rows.Add(RIGA)

                Loop
                myReader.Close()
                If tot >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If
                tot2 = tot2 + tot

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "GESTORE"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"
                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COMPETENZA GESTORE</td><td>VOCE</td><td>RIFERIMENTO PERIODO</td><td style='text-align: right'>IMPORTO</td></tr>"
                tot = 0
                par.cmd.CommandText = "select * from siscom_mi.incassi_mesi where vecchio=1 and competenza=2 and periodo=" & Periodo & " ORDER BY RIFERIMENTO ASC,VOCE ASC"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read
                    importo = ""
                    If myReader("IMPORTO") < 1 And myReader("IMPORTO") > -1 Then
                        importo = "0" & Format(myReader("IMPORTO"), "##,##0.00")
                    Else
                        importo = Format(myReader("IMPORTO"), "##,##0.00")
                    End If

                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & myReader("VOCE") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4) & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'>" & importo & "</td></tr>"
                    tot = tot + myReader("IMPORTO")

                    RIGA = DT.NewRow()
                    RIGA.Item("COMPETENZA") = ""
                    RIGA.Item("VOCE") = myReader("VOCE")
                    RIGA.Item("RIFERIMENTO") = par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4)
                    RIGA.Item("IMPORTO") = importo
                    DT.Rows.Add(RIGA)
                Loop
                myReader.Close()
                If tot >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If

                tot2 = tot2 + tot

                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = "QUOTE SINDACALI"
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = ""
                RIGA.Item("IMPORTO") = ""
                DT.Rows.Add(RIGA)

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"

                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>QUOTE SINDACALI</td><td>VOCE</td><td>RIFERIMENTO PERIODO</td><td style='text-align: right'>IMPORTO</td></tr>"
                tot = 0
                par.cmd.CommandText = "select * from siscom_mi.incassi_mesi where vecchio=1 and (competenza=3 OR competenza=10 OR competenza=11 OR competenza=12) and periodo=" & Periodo & " ORDER BY RIFERIMENTO ASC,VOCE ASC"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read

                    importo = ""
                    If myReader("IMPORTO") < 1 And myReader("IMPORTO") > -1 Then
                        importo = "0" & Format(myReader("IMPORTO"), "##,##0.00")
                    Else
                        importo = Format(myReader("IMPORTO"), "##,##0.00")
                    End If

                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & myReader("VOCE") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>" & par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4) & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right'>" & importo & "</td></tr>"
                    tot = tot + myReader("IMPORTO")
                    RIGA = DT.NewRow()
                    RIGA.Item("COMPETENZA") = ""
                    RIGA.Item("VOCE") = myReader("VOCE")
                    RIGA.Item("RIFERIMENTO") = par.ConvertiMese(CInt(Mid(myReader("RIFERIMENTO"), 5, 2))) & " " & Mid(myReader("RIFERIMENTO"), 1, 4)
                    RIGA.Item("IMPORTO") = importo
                    DT.Rows.Add(RIGA)

                Loop
                myReader.Close()
                If tot >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If

                tot2 = tot2 + tot
                Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>&nbsp;</td></tr>"
                If tot2 >= 1 Then
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>Totale</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot2, "##,##0.00") & "</td></tr>"
                Else
                    Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>Totale</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>0,00</td></tr>"
                End If


                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = ""
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = "TOTALE"
                RIGA.Item("IMPORTO") = tot2
                DT.Rows.Add(RIGA)

                tot3 = tot1 + tot2
                'Tabella = Tabella & "<tr style='font-family: ARIAL; font-size: 10pt;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;'>Totale Generale</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;text-align: right; font-weight: bold'>" & Format(tot3, "##,##0.00") & "</td></tr>"

                Tabella = Tabella & "</table>"
                Tabella = Tabella & "<br/><p style='font-family: ARIAL; font-size: 12pt; font-weight: bold'>Totale Generale: " & Format(tot3, "##,##0.00") & "</p>"



                RIGA = DT.NewRow()
                RIGA.Item("COMPETENZA") = ""
                RIGA.Item("VOCE") = ""
                RIGA.Item("RIFERIMENTO") = "TOTALE Generale"
                RIGA.Item("IMPORTO") = tot3
                DT.Rows.Add(RIGA)

                Session.Add("e_MIADTS3", DT)


                lblTabella.Text = Tabella
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If



    End Sub

    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        Dim url As String = Server.MapPath("..\..\FileTemp\")
        Dim pdfConverter1 As PdfConverter = New PdfConverter

        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdfConverter1.LicenseKey = Licenza
        End If

        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfDocumentOptions.ShowFooter = False
        pdfConverter1.PdfDocumentOptions.LeftMargin = 10
        pdfConverter1.PdfDocumentOptions.RightMargin = 10
        pdfConverter1.PdfDocumentOptions.TopMargin = 10
        pdfConverter1.PdfDocumentOptions.BottomMargin = 10
        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfFooterOptions.FooterText = ("")
        pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        pdfConverter1.PdfFooterOptions.PageNumberText = ""
        pdfConverter1.PdfFooterOptions.ShowPageNumber = False

        Dim nomefile As String = "Export_Prelievi_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(lblTabella.Text, url & nomefile)

        Response.Write("<script>window.open('../../FileTemp/" & nomefile & "','Incassi','');</script>")
    End Sub

    Private Sub eXCEL()
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow



            DT = CType(HttpContext.Current.Session.Item("e_MIADTS3"), Data.DataTable)
            sNomeFile = "Export_Prelievi_" & Format(Now, "yyyyMMddHHmmss")
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
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COMPETENZA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "VOCE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "RIFERIMENTO INCASSO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "IMPORTO", 0)

                K = 2
                For Each row In DT.Rows

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(DT.Rows(i).Item("COMPETENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(DT.Rows(i).Item("VOCE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(DT.Rows(i).Item("RIFERIMENTO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(DT.Rows(i).Item("IMPORTO"), 0)))



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
            '
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
            'For i = 0 To 1500
            '    If File.Exists() Then
            'Next
            'Response.Write("<script>window.open('../../FileTemp/" & sNomeFile & ".zip','Incassi','');</script>")
            Response.Redirect("../../FileTemp/" & sNomeFile & ".zip")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub imgStampa0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa0.Click
        eXCEL()
    End Sub
End Class
