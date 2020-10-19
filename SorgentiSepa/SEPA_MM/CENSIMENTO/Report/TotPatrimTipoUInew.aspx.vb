Imports System.Data
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_Report_TotPatrimTipoUI2
    Inherits PageSetIdMode
    Dim par As New CM.Global
    
    Dim listaTotali As New System.Collections.Generic.SortedList(Of Decimal, System.Collections.Generic.SortedList(Of String, Long))
    Dim listaTotali2 As New System.Collections.Generic.SortedList(Of Decimal, System.Collections.Generic.SortedList(Of String, Decimal))

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
            caricaTotalizzazioni()
        End If
    End Sub


    Private Sub caricaTotalizzazioni()

        Dim intestTabella1 As String = "<tr>"
        Dim intestTabella2 As String = "<tr>"
        Dim intestTabella3 As String = "<tr>"
        Dim datiTabella As String = ""
        Dim stileIntest As String = " bgcolor='#507CD1' style ='font-size: 8pt;font-family: Arial;font-weight: bold;color: White;text-align: center;white-space:nowrap;'"
        Dim tipiUI As String = Request.QueryString("UI")
        Dim tipologia As String = ""
        Dim POS As Integer = 1
        Dim POS1 As Integer
        Dim contaTipo As Integer = 0
        Dim s As String = ""
        Dim s2 As String = ""
        Dim CONDIZIONEalloggi As String = ""
        Dim numTipiUI As Integer = 0
        Dim dtTipoUI As New Data.DataTable
        Dim check As Integer = 0

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD ASC"
        Dim daTipoUI As Oracle.DataAccess.Client.OracleDataAdapter
        daTipoUI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
        daTipoUI.Fill(dtTipoUI)
        numTipiUI = dtTipoUI.Rows.Count

        intestTabella1 = intestTabella1 & "<td rowspan='3' " & stileIntest & ">COMPLESSO</td>"
        intestTabella1 = intestTabella1 & "<td rowspan='3' " & stileIntest & ">INDIRIZZO: via e num. civico</td>"
        intestTabella1 = intestTabella1 & "<td rowspan='3' " & stileIntest & ">EDIFICIO</td>"

        While POS <= Len(tipiUI)

            POS1 = InStr(POS, tipiUI, ",", CompareMethod.Text)

            If POS1 = 0 Then POS1 = Len(tipiUI) + 1
            tipologia = Mid(tipiUI, POS, POS1 - POS)

            POS = POS1 + 1

            If tipologia = "AL" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">ALLOGGI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "AU" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">AUTORIMESSA</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "P" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">PORTINERIE</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "S" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">DEPOSITI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "N" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">NEGOZI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "L" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">LABORATORI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "D" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">DEPOSITO</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "E" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">EDICOLA</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "F" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">ALBERGO</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "O" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">GARAGE</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "R" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">SUPERMARKET</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "RIST" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">RISTORANTI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "SC" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">SCUOLA/ASILI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "SEAS" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">ASSOCIAZIONE</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "U" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">UFFICI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "B" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">BOX</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "H" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">POSTO AUTO COPERTO</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "I" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">POSTO AUTO SCOPERTO</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "M" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">MOTOBOX</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "K" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">INSEGNA</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "MF" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">MONUMENTO/FONTANA</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "T" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">TERRENI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "ST" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">ALTRA STRUTTURA</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "C" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">CANTINA</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "G" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">GIARDINO</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "SO" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">SOFFITTO</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If
            If tipologia = "V" Then
                intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">BOX COMUNI</td>"
                intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            End If

            contaTipo = contaTipo + 1
        End While

        If contaTipo < numTipiUI Then
            intestTabella2 = intestTabella2 & "<td colspan='2' " & stileIntest & ">ALTRE TIPOLOGIE</td>"
            intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td>"
            contaTipo = contaTipo + 1
            intestTabella1 = intestTabella1 & "<td colspan= '" & contaTipo * 2 & "' " & stileIntest & ">TIPOLOGIA UNITA' IMMOBILIARI</td>"
            contaTipo = contaTipo - 1
        Else
            intestTabella1 = intestTabella1 & "<td colspan= '" & contaTipo * 2 & "' " & stileIntest & ">TIPOLOGIA UNITA' IMMOBILIARI</td>"
        End If

        intestTabella1 = intestTabella1 & "<td colspan='2' rowspan='2' " & stileIntest & ">TOTALI</td></tr>"
        intestTabella2 = intestTabella2 & "</tr>"
        intestTabella3 = intestTabella3 & "<td" & stileIntest & ">Num.</td><td" & stileIntest & ">MQ</td></tr>"

        If Request.QueryString("C") = "-1" Then
            s = " ORDER BY DENOMINAZIONE,DESCRIZIONE ASC" 'AND id_complesso <=100000005
            s2 = " ORDER BY DENOMINAZIONE,INDIRIZZO ASC"
        ElseIf Request.QueryString("C") = "-10" Then
            s = " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE =" & Request.QueryString("F") & " ORDER BY DESCRIZIONE ASC"
        Else
            s = " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID =" & Request.QueryString("C") & " ORDER BY DESCRIZIONE ASC"
        End If

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim contaNum As Integer = 0
            Dim contaMQ As Decimal = 0

            Dim strNum As String = ""
            Dim strMQ As String = ""
            datiTabella = ""
            Dim arraylistNUM2 As New ArrayList
            arraylistNUM2.Clear()
            Dim totalecomNUM As Integer = 0
            Dim totalecomMQ As Decimal = 0
            Dim indiceGenerale2 As Integer = 0
            Dim indiceGenerale As Integer = 0
            Dim contaTipologia As Integer = 0
            Dim stileDati As String = "align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'"
            Dim dt1 As New Data.DataTable
            Dim dt2 As New Data.DataTable

            Dim contaNumGenerale As Integer = 0
            Dim contaMQGenerale As Decimal = 0

            par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE),SISCOM_MI.INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,SISCOM_MI.COMPLESSI_IMMOBILIARI.ID AS ID_COMPLESSO,SISCOM_MI.INDIRIZZI.ID AS ID_INDIRIZZO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI " _
                & "WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " & s
            Dim da2 As Oracle.DataAccess.Client.OracleDataAdapter
            da2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da2.Fill(dt2)
            Dim indice2 As Integer = 0
            Dim indicePerTOT2 As Integer = 0
            Dim arraylistNUM3 As New ArrayList

            Me.leggiTotaliComplessiNUM()
            Dim listaTotaliComplessoCorrente As System.Collections.Generic.SortedList(Of String, Long)

            Me.leggiTotaliComplessiMQ()
            Dim listaTotaliComplessoCorrente2 As System.Collections.Generic.SortedList(Of String, Decimal)
            Dim contaNumCompless As Integer = 0
            Dim contaMQCompless As Decimal = 0
            For Each r As Data.DataRow In dt2.Rows
                Response.Flush()
                Dim idComplesso As Decimal
                idComplesso = Convert.ToDecimal(r.Item("ID_COMPLESSO"))

                If listaTotali.ContainsKey(idComplesso) Then
                    listaTotaliComplessoCorrente = listaTotali(idComplesso)
                Else
                    listaTotaliComplessoCorrente = New System.Collections.Generic.SortedList(Of String, Long)
                End If

                If listaTotali2.ContainsKey(idComplesso) Then
                    listaTotaliComplessoCorrente2 = listaTotali2(idComplesso)
                Else
                    listaTotaliComplessoCorrente2 = New System.Collections.Generic.SortedList(Of String, Decimal)
                End If

                indice2 = indice2 + 1
                indiceGenerale2 = indiceGenerale2 + 1
                par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE),SISCOM_MI.INDIRIZZI.DESCRIZIONE||', '||INDIRIZZI.CIVICO AS INDIRIZZO,SISCOM_MI.EDIFICI.COD_EDIFICIO,SISCOM_MI.COMPLESSI_IMMOBILIARI.ID AS ID_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID" _
                    & " AND SISCOM_MI.INDIRIZZI.ID= " & r.Item("ID_INDIRIZZO") & " " & s2 & ""

                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dt1.Rows.Clear()
                da.Fill(dt1)

                Dim contaNumCivico As Integer = 0
                Dim contaMQCivico As Decimal = 0
                Dim totaleContaNum As Integer = 0
                Dim totaleContamq As Decimal = 0
                Dim totalecomplNUM As Integer = 0
                Dim totalecomplMQ As Decimal = 0
                Dim arraylistNUM As New ArrayList
                arraylistNUM.Clear()
                arraylistNUM3.Clear()

                Dim indicePerTOT As Integer = 0
                Dim indice As Integer = 0

                For Each rr As Data.DataRow In dt1.Rows

                    contaNum = 0
                    contaMQ = 0
                    indice = indice + 1
                    indiceGenerale = indiceGenerale + 1
                    If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                        datiTabella = datiTabella & "<tr><td style ='font-color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & rr.Item(0) & "</td>" _
                            & "<td style ='font-color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & rr.Item(1) & "</td>" _
                            & "<td style ='font-color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & rr.Item(2) & "</td>"
                    End If

                    POS = 1

                    While POS <= Len(tipiUI)

                        POS1 = InStr(POS, tipiUI, ",", CompareMethod.Text)
                        If POS1 = 0 Then POS1 = Len(tipiUI) + 1

                        tipologia = Mid(tipiUI, POS, POS1 - POS)

                        POS = POS1 + 1

                        If tipologia = "AL" Then
                            CONDIZIONEalloggi = " SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND "
                        Else
                            'CONDIZIONEalloggi = " SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND "
                            CONDIZIONEalloggi = " SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND "
                        End If

                        If indiceGenerale = 1 And indiceGenerale2 = 1 And Request.QueryString("C") = "-1" Then
                            '################### TOTALE DI TUTTO IL PATRIMONIO ####################

                            'Colonna Num.
                            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS NUM " _
                                & " FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                & " WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                                & " AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' " _
                                & " AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' " _
                                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID " _
                                & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <>1"

                            Dim lettoretotale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettoretotale.Read Then
                                arraylistNUM2.Add(par.IfNull(lettoretotale("NUM"), 0))
                                totalecomNUM = totalecomNUM + par.IfNull(lettoretotale("NUM"), 0)
                            End If
                            lettoretotale.Close()

                            'Colonna MQ
                            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ " _
                                & "FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                & "WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND " _
                                & "SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND " _
                                & "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND " _
                                & "SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND " _
                                & "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' AND  " _
                                & CONDIZIONEalloggi _
                                & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID" _
                                & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <>1"
                            'End If

                            lettoretotale = par.cmd.ExecuteReader
                            If lettoretotale.Read Then
                                arraylistNUM2.Add(par.IfNull(lettoretotale("MQ"), 0))
                                totalecomMQ = totalecomMQ + par.IfNull(lettoretotale("MQ"), 0)
                            End If
                            lettoretotale.Close()
                            '################### FINE TOTALE DI TUTTO IL PATRIMONIO ####################
                        End If


                        par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS NUM FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & rr.Item("COD_EDIFICIO") & ""
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            strNum = par.IfNull(myReader1("NUM"), "0")
                            If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                                datiTabella = datiTabella & "<td align='right' style ='font-color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & strNum & "</td>"
                            End If
                            contaNum = contaNum + strNum
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' AND " & CONDIZIONEalloggi & " SISCOM_MI.EDIFICI.COD_EDIFICIO =" & rr.Item("COD_EDIFICIO") & ""
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            strMQ = par.IfNull(myReader2("MQ"), "0")
                            If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                                datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & strMQ & "</td>"
                            End If
                            contaMQ = contaMQ + strMQ
                        End If
                        myReader2.Close()

                        If indice = dt1.Rows.Count Then
                            '-------------------------- TOTALI CIVICO ---------------------------

                            'Colonna Num.
                            If Request.QueryString("C") <> "-1" Then
                                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS NUM " _
                            & " FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                            & " WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                            & " AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' " _
                            & " AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' " _
                            & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID " _
                            & " AND SISCOM_MI.EDIFICI.COD_EDIFICIO in " _
                            & " (SELECT SISCOM_MI.EDIFICI.COD_EDIFICIO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI " _
                            & " WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO " _
                            & " AND SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                            & " AND INDIRIZZI.DESCRIZIONE='" & par.PulisciStrSql(r.Item("DESCRIZIONE")) & "' AND INDIRIZZI.CIVICO='" & r.Item("CIVICO") & "' " _
                            & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID ='" & Request.QueryString("C") & "')"
                            Else
                                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS NUM " _
                            & " FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI " _
                            & " WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                            & " AND SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD = '" & tipologia & "' " _
                            & " AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' " _
                            & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID " _
                            & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO" _
                            & " AND SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID" _
                            & " AND SISCOM_MI.INDIRIZZI.ID ='" & r.Item("ID_INDIRIZZO") & "'"
                            End If

                            Dim leggiNUM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If leggiNUM.Read Then
                                arraylistNUM.Add(par.IfNull(leggiNUM("NUM"), 0))
                            End If
                            leggiNUM.Close()

                            'Colonna MQ
                            If Request.QueryString("C") <> "-1" Then
                                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ " _
                                & "FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI " _
                                & "WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND " _
                                & "SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND " _
                                & "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND " _
                                & "SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND " _
                                & "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' AND  " _
                                & CONDIZIONEalloggi _
                                & "SISCOM_MI.EDIFICI.COD_EDIFICIO in " _
                                & "(SELECT SISCOM_MI.EDIFICI.COD_EDIFICIO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI " _
                                & "WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND " _
                                & "SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 AND " _
                                & "INDIRIZZI.DESCRIZIONE='" & par.PulisciStrSql(r.Item("DESCRIZIONE")) & "' AND INDIRIZZI.CIVICO='" & r.Item("CIVICO") & "' " _
                                & "AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID ='" & Request.QueryString("C") & "') "
                            Else
                                par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ " _
                                & "FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI " _
                                & "WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND " _
                                & "SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND " _
                                & "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND " _
                                & "SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND " _
                                & "SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD = '" & tipologia & "' AND  " _
                                & CONDIZIONEalloggi _
                                & " SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID " _
                                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID " _
                                & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO" _
                                & " AND SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID" _
                                & " AND SISCOM_MI.INDIRIZZI.ID ='" & r.Item("ID_INDIRIZZO") & "'"
                                ' par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ " _
                                '& "FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI " _
                                '& "WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND " _
                                '& "SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND " _
                                '& "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND " _
                                '& "SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND " _
                                '& "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = '" & tipologia & "' AND  " _
                                '& CONDIZIONEalloggi _
                                '& "SISCOM_MI.EDIFICI.COD_EDIFICIO in " _
                                '& "(SELECT SISCOM_MI.EDIFICI.COD_EDIFICIO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI " _
                                '& "WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO AND " _
                                '& "SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 AND " _
                                '& "INDIRIZZI.DESCRIZIONE='" & par.PulisciStrSql(r.Item("DESCRIZIONE")) & "' AND INDIRIZZI.CIVICO='" & r.Item("CIVICO") & "' " _
                                '& " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID ='" & r.Item("ID_COMPLESSO") & "')"
                            End If

                            Dim leggiMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If leggiMQ.Read Then
                                arraylistNUM.Add(par.IfNull(leggiMQ("MQ"), 0))
                            End If
                            leggiMQ.Close()
                            '-------------------------- FINE TOTALI CIVICO ---------------------------
                        End If

                        If indice = 1 Then

                            Dim totale As Decimal
                            totale = Me.leggiTotaleComplessi(listaTotaliComplessoCorrente, tipologia)

                            arraylistNUM3.Add(Format(totale, "##,##0"))
                            totalecomplNUM += totale

                            Dim totale2 As Decimal
                            totale2 = Me.leggiTotaleComplessi2(listaTotaliComplessoCorrente2, tipologia)

                            If totale2 = 0 Then
                                arraylistNUM3.Add(totale2)
                            Else
                                arraylistNUM3.Add(Format(totale2, "##,##0.00"))
                            End If

                            totalecomplMQ += totale2

                            '-------------------------TOTALI COMPLESSO----------------------------
                        End If
                        contaTipologia = contaTipologia + 1

                    End While


                    '****************** QUERY PER LE TIPOLOGIE CHE NON VENGONO SELEZIONATE ****************

                    If contaTipo < numTipiUI Then

                        POS = 1
                        Dim tipologia2 As String = ""
                        Dim stringaUnica As String = ""
                        While POS <= Len(tipiUI)

                            POS1 = InStr(POS, tipiUI, ",", CompareMethod.Text)
                            If POS1 = 0 Then POS1 = Len(tipiUI) + 1
                            tipologia2 = Mid(tipiUI, POS, POS1 - POS)
                            stringaUnica = stringaUnica & "'" & tipologia2 & "',"
                            POS = POS1 + 1

                        End While

                        stringaUnica = Mid(stringaUnica, 1, Len(stringaUnica) - 1)

                        par.cmd.CommandText = "SELECT TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS NUM FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA NOT IN (" & stringaUnica & ") AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & rr.Item("COD_EDIFICIO") & ""
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            strNum = par.IfNull(myReader1("NUM"), "0")
                            If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                                datiTabella = datiTabella & "<td align='right' style ='font-color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & strNum & "</td>"
                            End If
                            contaNum = contaNum + strNum
                            contaNumCivico = contaNumCivico + strNum
                            contaNumGenerale = contaNumGenerale + strNum
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <>'AL' OR  SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='AL') AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA NOT IN (" & stringaUnica & ") AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & rr.Item("COD_EDIFICIO") & ""
                        'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' AND (SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA <>'AL' OR  SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA ='AL') AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA NOT IN (" & stringaUnica & ") AND SISCOM_MI.EDIFICI.COD_EDIFICIO =" & rr.Item("COD_EDIFICIO") & ""
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            strMQ = par.IfNull(myReader2("MQ"), "0")
                            If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                                datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & strMQ & "</td>"
                            End If
                            contaMQ = contaMQ + strMQ
                            contaMQCivico = contaMQCivico + strMQ
                            contaMQGenerale = contaMQGenerale + strMQ
                        End If
                        myReader2.Close()

                    End If

                    If Request.QueryString("TOT") = "0" Or Request.QueryString("TOT") = "3" Then
                        datiTabella = datiTabella _
                         & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaNum, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaMQ, "##,##0.00") & "</td>" _
                        & "</tr>"
                    End If

                    totaleContamq = totaleContamq + contaMQ
                    totaleContaNum = totaleContaNum + contaNum

                    indicePerTOT = indicePerTOT + 1
                Next

                If idcomplessoHidden.Value <> 0 Or check = 0 Then
                    check = 1
                    If r.Item("ID_COMPLESSO") <> idcomplessoHidden.Value Then
                        contaNumCompless = 0
                        contaMQCompless = 0
                    End If
                    contaNumCompless = contaNumCompless + contaNumCivico
                    contaMQCompless = contaMQCompless + contaMQCivico
                End If
                idcomplessoHidden.Value = r.Item("ID_COMPLESSO")

                totalecomplNUM = totalecomplNUM + contaNumCompless
                totalecomplMQ = totalecomplMQ + contaMQCompless

                'Creazione righe dati tabella

                If Request.QueryString("TOT") = "0" Then
                    If indice = dt1.Rows.Count Then
                        datiTabella = datiTabella & "<tr><td>&nbsp</td><td>&nbsp</td>" _
                            & "<strong><td style ='color:red;font-size: 8pt;font-weight:bold;font-family: Arial;white-space:nowrap;'>TOTALE CIVICO</td></strong>"

                        For Each num1 In arraylistNUM
                            datiTabella = datiTabella & "<td align='right' style ='color:red;font-size: 8pt;font-family: Arial;'>" & num1 & "</td>"
                        Next

                        If contaTipo < numTipiUI Then
                            datiTabella = datiTabella _
                                & "<td align='right' style ='color:red;font-size: 8pt;font-family: Arial;'>" & Format(contaNumCivico, "##,##0") & "</td><td align='right' style ='color:red;font-size: 8pt;font-family: Arial;'>" & Format(contaMQCivico, "##,##0.00") & "</td>"
                        End If

                        datiTabella = datiTabella _
                            & "<td align='right' style ='color:red;font-size: 8pt;font-family: Arial;'>" & Format(totaleContaNum, "##,##0") & "</td><td align='right' style ='color:red;font-size: 8pt;font-family: Arial;'>" & Format(totaleContamq, "##,##0.00") & "</td>" _
                            & "</tr>"
                    End If
                End If

                If Request.QueryString("TOT") = "1" Then
                    If indice = dt1.Rows.Count Then
                        datiTabella = datiTabella & "<tr><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & dt1.Rows(0).Item("DENOMINAZIONE") & "</td><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & dt1.Rows(0).Item("INDIRIZZO") & "</td>" _
                            & "<strong><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>TOTALE CIVICO</td></strong>"

                        For Each num1 In arraylistNUM
                            datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & num1 & "</td>"
                        Next

                        If contaTipo < numTipiUI Then
                            datiTabella = datiTabella _
                                    & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaNumCivico, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaMQCivico, "##,##0.00") & "</td>"
                        End If

                        datiTabella = datiTabella _
                            & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(totaleContaNum, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(totaleContamq, "##,##0.00") & "</td>" _
                            & "</tr>"
                    End If
                End If

                If indice2 < dt2.Rows.Count Then
                    If dt2.Rows(indicePerTOT2).Item("DENOMINAZIONE") <> dt2.Rows(indicePerTOT2 + 1).Item("DENOMINAZIONE") Then
                        '------------------- RIGA TOTALE COMPLESSO --------------------
                        If Request.QueryString("TOT") = "0" Then
                            datiTabella = datiTabella & "<tr bgcolor=""Gainsboro""><td style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;white-space:nowrap;'>" & dt2.Rows(indicePerTOT2).Item("DENOMINAZIONE") & "</td>" _
                                & "<strong><td style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;white-space:nowrap;'>TOTALE COMPLESSO</td></strong><td>&nbsp</td>"

                            For Each num3 In arraylistNUM3
                                datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & num3 & "</td>"
                            Next

                            If contaTipo < numTipiUI Then
                                datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(contaNumCompless, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaMQCompless, "##,##0.00") & "</td>"
                            End If

                            datiTabella = datiTabella _
                                & "<td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(totalecomplNUM, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(totalecomplMQ, "##,##0.00") & "</td>" _
                                & "</tr>"

                        End If
                        If Request.QueryString("TOT") = "2" Then
                            datiTabella = datiTabella & "<tr><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & dt2.Rows(indicePerTOT2).Item("DENOMINAZIONE") & "</td>" _
                               & "<strong><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>TOTALE COMPLESSO</td></strong><td>&nbsp</td>"

                            For Each num3 In arraylistNUM3
                                datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & num3 & "</td>"
                            Next

                            If contaTipo < numTipiUI Then
                                datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaNumCompless, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaMQCompless, "##,##0.00") & "</td>"
                            End If

                            datiTabella = datiTabella _
                                & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(totalecomplNUM, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(totalecomplMQ, "##,##0.00") & "</td>" _
                                & "</tr>"
                        End If
                        '------------------- FINE RIGA TOTALE COMPLESSO --------------------
                    End If

                Else
                    '------------------- RIGA TOTALE ULTIMO COMPLESSO ----------------------
                    If Request.QueryString("TOT") = "0" Then
                        datiTabella = datiTabella & "<tr tr bgcolor=""Gainsboro""><td style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;white-space:nowrap;'>" & dt2.Rows(indicePerTOT2).Item("DENOMINAZIONE") & "</td>" _
                                & "<strong><td style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;white-space:nowrap;'>TOTALE COMPLESSO</td></strong><td>&nbsp</td>"

                        For Each num3 In arraylistNUM3
                            datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & num3 & "</td>"
                        Next

                        If contaTipo < numTipiUI Then
                            datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(contaNumCompless, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(contaMQCompless, "##,##0.00") & "</td>"
                        End If

                        datiTabella = datiTabella _
                            & "<td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(totalecomplNUM, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(totalecomplMQ, "##,##0.00") & "</td>" _
                            & "</tr>"
                    End If
                    If Request.QueryString("TOT") = "2" Then
                        datiTabella = datiTabella & "<tr><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>" & dt2.Rows(indicePerTOT2).Item("DENOMINAZIONE") & "</td>" _
                           & "<strong><td style ='color:#333333;font-size: 8pt;font-family: Arial;white-space:nowrap;'>TOTALE COMPLESSO</td></strong><td>&nbsp</td>"

                        For Each num3 In arraylistNUM3
                            datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & num3 & "</td>"
                        Next

                        If contaTipo < numTipiUI Then
                            datiTabella = datiTabella & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaNumCompless, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(contaMQCompless, "##,##0.00") & "</td>"
                        End If

                        datiTabella = datiTabella _
                            & "<td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(totalecomplNUM, "##,##0") & "</td><td align='right' style ='color:#333333;font-size: 8pt;font-family: Arial;'>" & Format(totalecomplMQ, "##,##0.00") & "</td>" _
                            & "</tr>"
                    End If
                    '------------------- FINE RIGA TOTALE ULTIMO COMPLESSO --------------------
                End If
                indicePerTOT2 = indicePerTOT2 + 1
            Next

            If Request.QueryString("C") = "-1" Then
                totalecomNUM = totalecomNUM + contaNumGenerale
                totalecomMQ = totalecomMQ + contaMQGenerale
            End If

            '------------------- RIGA TOTALE GENERALE --------------------
            If Request.QueryString("C") = "-1" Then

                datiTabella = datiTabella & "<tr bgcolor=""RoyalBlue""><td style ='color:white;font-size: 8pt;font-weight:bold;text-decoration:underline;font-family: Arial;white-space:nowrap;'>TOTALE GENERALE</td>" _
                            & "<td style ='font-color:white;font-size: 8pt;font-weight:bold;font-family: Arial;white-space:nowrap;'>&nbsp</td>" _
                            & "<td style ='font-color:white;font-size: 8pt;font-family: Arial;white-space:nowrap;'>&nbsp</td>"

                For Each num2 In arraylistNUM2
                    datiTabella = datiTabella & "<td align='right' style ='color:white;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & num2 & "</td>"
                Next

                If contaTipo < numTipiUI Then
                    datiTabella = datiTabella _
                        & "<td align='right' style ='color:white;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(contaNumGenerale, "##,##0") & "</td><td align='right' style ='color:white;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(contaMQGenerale, "##,##0.00") & "</td>"
                End If

                datiTabella = datiTabella _
                    & "<td align='right' style ='color:white;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(totalecomNUM, "##,##0") & "</td><td align='right' style ='color:white;font-size: 8pt;font-weight:bold;font-family: Arial;'>" & Format(totalecomMQ, "##,##0.00") & "</td>" _
                    & "</tr>"
            End If
            '-------------------FINE RIGA TOTALE GENERALE--------------------

            TXTtotalizz.Text = intestTabella1 & intestTabella2 & intestTabella3 & datiTabella

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Function leggiTotaliComplessiNUM() As Boolean

        Dim dt As New System.Data.DataTable
        Dim esito As Boolean = False
        Try
            Dim sql As String = ""

            sql &= " SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID ,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD,TRIM(TO_CHAR(COUNT(SISCOM_MI.UNITA_IMMOBILIARI.ID),'9G999G999')) AS NUM "
            sql &= " FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI"
            sql &= " WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD"
            sql &= " AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND'"
            sql &= " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID "
            sql &= " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO"
            sql &= " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1"
            sql &= " group by SISCOM_MI.COMPLESSI_IMMOBILIARI.ID ,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD"
            sql &= " order by SISCOM_MI.COMPLESSI_IMMOBILIARI.ID"

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(sql, par.OracleConn)

            da.Fill(dt)
            listaTotali.Clear()

            Dim listaCorrente As System.Collections.Generic.SortedList(Of String, Long)

            For Each row As DataRow In dt.Rows
                Dim ID As Decimal

                ID = Convert.ToDecimal(row("ID"))
                If Not listaTotali.ContainsKey(ID) Then
                    listaCorrente = New System.Collections.Generic.SortedList(Of String, Long)
                    listaTotali.Add(ID, listaCorrente)
                Else
                    listaCorrente = listaTotali(ID)
                End If
                listaCorrente.Add(row("COD").ToString(), row("NUM"))
            Next

            da.Dispose()
            dt.Dispose()

            esito = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return (esito)
    End Function

    Function leggiTotaliComplessiMQ() As Boolean

        Dim dt As New System.Data.DataTable
        Dim esito As Boolean = False
        Try
            Dim sql As String = ""
            'SUPERFICIE UR MODIFICATA CON NETTA IL 10/04/2012
            sql = "SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD, TRIM(TO_CHAR(SUM(SISCOM_MI.DIMENSIONI.VALORE),'9G999G990D99')) AS MQ " _
                & "FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                & "WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID " _
                & "AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = SISCOM_MI.UNITA_IMMOBILIARI.ID " _
                & "AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                & "AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                & "AND SISCOM_MI.STATO_UI(SISCOM_MI.UNITA_IMMOBILIARI.ID) <> 'VEND' " _
                & "AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO " _
                & "AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                & "AND SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = (CASE WHEN COD = 'AL' THEN 'SUP_NETTA' ELSE 'SUP_NETTA' END) " _
                & "group by SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                & "order by SISCOM_MI.COMPLESSI_IMMOBILIARI.ID"


            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(sql, par.OracleConn)

            da.Fill(dt)

            listaTotali2.Clear()

            Dim listaCorrente As System.Collections.Generic.SortedList(Of String, Decimal)

            For Each row As DataRow In dt.Rows
                Dim ID As Decimal
                ID = Convert.ToDecimal(row("ID"))
                If Not listaTotali2.ContainsKey(ID) Then
                    listaCorrente = New System.Collections.Generic.SortedList(Of String, Decimal)
                    listaTotali2.Add(ID, listaCorrente)
                Else
                    listaCorrente = listaTotali2(ID)
                End If
                listaCorrente.Add(row("COD").ToString(), row("MQ"))
            Next

            da.Dispose()
            dt.Dispose()

            esito = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return (esito)

    End Function

    Function leggiTotaleComplessi(lista As System.Collections.Generic.SortedList(Of String, Long), cod As String) As Long

        If lista.ContainsKey(cod) Then
            Return (lista(cod))
        Else
            Return (0)
        End If

    End Function

    Function leggiTotaleComplessi2(lista As System.Collections.Generic.SortedList(Of String, Decimal), cod As String) As Decimal

        If lista.ContainsKey(cod) Then
            Return (lista(cod))
        Else
            Return (0)
        End If

    End Function

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

        Try
            Response.Clear()
            Dim sNomeFile As String
            sNomeFile = "Tot_dati_patrim_tipoUI" & Format(Now, "yyyyMMddHHmm")
            Response.AppendHeader("content-disposition", "attachment;filename=" & sNomeFile & ".xls")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"

            Dim stringWriter As New StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)

            TXTtotalizz.Text = "<table>" & TXTtotalizz.Text & "</table>"
            TXTtotalizz.RenderControl(sourcecode)
            Response.Write(stringWriter.ToString())
            'Response.End()
            Response.Flush()
            Response.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        ' '' ''Try
        ' '' ''    Dim myExcelFile As New CM.ExcelFile
        ' '' ''    Dim i As Long
        ' '' ''    Dim K As Long
        ' '' ''    Dim sNomeFile As String
        ' '' ''    Dim row As System.Data.DataRow
        ' '' ''    Dim POS As Integer = 1
        ' '' ''    Dim POS1 As Integer
        ' '' ''    Dim tipiUI As String = Request.QueryString("UI")
        ' '' ''    Dim tipologia As String = ""
        ' '' ''    Dim contaTipo As Integer = 3
        ' '' ''    Dim contaTipo2 As Integer = 3

        ' '' ''    dtExport = CType(HttpContext.Current.Session.Item("dtable"), Data.DataTable)
        ' '' ''    sNomeFile = "Tot_dati_patrim_tipoUI" & Format(Now, "yyyyMMddHHmm")
        ' '' ''    i = 0

        ' '' ''    With myExcelFile
        ' '' ''        .CreateFile(Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls"))
        ' '' ''        .PrintGridLines = False
        ' '' ''        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        ' '' ''        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        ' '' ''        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        ' '' ''        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        ' '' ''        .SetDefaultRowHeight(14)
        ' '' ''        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        ' '' ''        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        ' '' ''        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)

        ' '' ''        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 1, "COMPLESSO", 0)
        ' '' ''        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 2, "INDIRIZZO E NUM. CIVICO", 0)
        ' '' ''        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 3, "EDIFICIO", 0)
        ' '' ''        While POS <= Len(tipiUI)

        ' '' ''            contaTipo = contaTipo + 1
        ' '' ''            POS1 = InStr(POS, tipiUI, ",", CompareMethod.Text)

        ' '' ''            If POS1 = 0 Then POS1 = Len(tipiUI) + 1
        ' '' ''            tipologia = Mid(tipiUI, POS, POS1 - POS)

        ' '' ''            POS = POS1 + 1
        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, contaTipo, "NUM_" & tipologia & """", 0)
        ' '' ''            contaTipo = contaTipo + 1
        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, contaTipo, "MQ_" & tipologia & """", 0)
        ' '' ''        End While


        ' '' ''        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, contaTipo, "NUM. TOTALI", 0)
        ' '' ''        contaTipo = contaTipo + 1
        ' '' ''        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, contaTipo, "MQ TOTALI", 0)

        ' '' ''        K = 4
        ' '' ''        For Each row In dtExport.Rows
        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dtExport.Rows(i).Item("COMPLESSO"), ""))
        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dtExport.Rows(i).Item("INDIRIZZO"), ""))
        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dtExport.Rows(i).Item("COD_EDIFICIO"), ""))

        ' '' ''            While POS <= Len(tipiUI)

        ' '' ''                contaTipo2 = contaTipo2 + 1
        ' '' ''                POS1 = InStr(POS, tipiUI, ",", CompareMethod.Text)

        ' '' ''                If POS1 = 0 Then POS1 = Len(tipiUI) + 1
        ' '' ''                tipologia = Mid(tipiUI, POS, POS1 - POS)

        ' '' ''                POS = POS1 + 1

        ' '' ''                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, contaTipo2, par.IfNull(dtExport.Rows(i).Item("NUM_" & tipologia & """"), ""))

        ' '' ''                contaTipo2 = contaTipo2 + 1
        ' '' ''                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, contaTipo2, par.IfNull(dtExport.Rows(i).Item("MQ_" & tipologia & """"), ""))

        ' '' ''            End While

        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, contaTipo2, par.IfNull(dtExport.Rows(i).Item("NUM_TOTALI"), ""))
        ' '' ''            contaTipo2 = contaTipo2 + 1
        ' '' ''            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, contaTipo2, par.IfNull(dtExport.Rows(i).Item("MQ_TOTALI"), ""))


        ' '' ''            i = i + 1
        ' '' ''            K = K + 1
        ' '' ''        Next
        ' '' ''        .CloseFile()
        ' '' ''    End With

        ' '' ''    Dim objCrc32 As New Crc32()
        ' '' ''    Dim strmZipOutputStream As ZipOutputStream
        ' '' ''    Dim zipfic As String

        ' '' ''    zipfic = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".zip")

        ' '' ''    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        ' '' ''    strmZipOutputStream.SetLevel(6)

        ' '' ''    Dim strFile As String
        ' '' ''    strFile = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls")
        ' '' ''    Dim strmFile As FileStream = File.OpenRead(strFile)
        ' '' ''    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        ' '' ''    '
        ' '' ''    strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        ' '' ''    Dim sFile As String = Path.GetFileName(strFile)
        ' '' ''    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        ' '' ''    Dim fi As New FileInfo(strFile)
        ' '' ''    theEntry.DateTime = fi.LastWriteTime
        ' '' ''    theEntry.Size = strmFile.Length
        ' '' ''    strmFile.Close()
        ' '' ''    objCrc32.Reset()
        ' '' ''    objCrc32.Update(abyBuffer)
        ' '' ''    theEntry.Crc = objCrc32.Value
        ' '' ''    strmZipOutputStream.PutNextEntry(theEntry)
        ' '' ''    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        ' '' ''    strmZipOutputStream.Finish()
        ' '' ''    strmZipOutputStream.Close()

        ' '' ''    File.Delete(strFile)
        ' '' ''    Response.Redirect("..\..\FileTemp\" & sNomeFile & ".zip")

        ' '' ''Catch ex As Exception
        ' '' ''    Response.Write(ex.Message)
        ' '' ''End Try
    End Sub
End Class
