
Partial Class ANAUT_RedditoConv
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Pratica_Id As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Pratica_Id = Request.QueryString("ID")
            If Pratica_Id <> "" Then
                CalcolaRedditoDatabase(Pratica_Id)
            Else
                Response.Write("Errore nel passaggio dei parametri!")
            End If
        End If

    End Sub

    Function PuntiInVirgole(ByVal N As Object) As String
        Dim S As String
        Dim pos As Integer
        If IsDBNull(N) Then
            PuntiInVirgole = "NULL"
        Else
            S = N
            pos = InStr(1, S, ".")

            If pos > 1 Then
                Mid(S, pos, 1) = ","
            End If
            PuntiInVirgole = S
        End If
    End Function

    Private Function CalcolaRedditoDatabase(ByVal Pratica_Id As String)

        Dim RA As Decimal
        Dim RD As Decimal
        Dim RF As Decimal
        Dim OD As Decimal

        Dim RA1 As Decimal
        Dim RD1 As Decimal
        Dim RF1 As Decimal
        Dim OD1 As Decimal

        Dim REDDITO_ERP As Decimal
        Dim REDDITO_EQUO As Decimal
        Dim REDDITO_INVAL1 As Integer
        Dim REDDITO_INVAL2 As Integer
        Dim REDDITO_FIGLIO As Decimal
        Dim REDDITO_MINORE As Decimal
        Dim REDDITO_CONV As Decimal
        Dim LIMITE_REDDITO1 As Decimal
        Dim LIMITE_REDDITO2 As Decimal
        Dim LIMITE_REDDITO3 As Decimal
        Dim LIMITE_REDDITO4 As Decimal
        Dim LIMITE_REDDITO_FIGLIO As Decimal
        Dim LIMITE_REDDITO_MINORE As Decimal
        Dim MINORI As Integer
        Dim FIGLI As Integer

        Dim I As Integer

        Dim Invalidi As Integer
        Dim Maggiorazione As Decimal
        Dim percentuale_app As Integer
        Dim COMPONENTI As Integer
        Dim PERC_LAVORO_DIP As Integer
        Dim ComponentiNucleo()
        Dim RedditoNucleo()
        Dim TotaleLordo As Decimal
        Dim COLORE As String
        Dim stringafile As String = ""

        TotaleLordo = 0

        Try


            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT PARAMETER.* FROM PARAMETER"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                Select Case myReader("ID")
                    Case 0
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            REDDITO_ERP = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Limite Reddito ERP non valido!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 1
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            REDDITO_EQUO = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Limite Reddito Ex Equo Canone non valido!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 4
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            REDDITO_INVAL1 = PuntiInVirgole(Val(par.IfNull(myReader("VALORE"), 0)))
                        Else
                            Response.Write("Maggiorazione per un invalido non valida!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 5
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            REDDITO_INVAL2 = PuntiInVirgole(Val(par.IfNull(myReader("VALORE"), 0)))
                        Else
                            Response.Write("Maggiorazione per uno o più invalidi non valida!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 2
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            REDDITO_FIGLIO = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Detrazioni per figlio a carico non valide!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 3
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            REDDITO_MINORE = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Detrazioni per minore a carico non valide!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 9 '1/2 PERSONE
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            LIMITE_REDDITO1 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Detrazioni per minore a carico non valide!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 10 '3/4 PERSONE
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            LIMITE_REDDITO2 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Detrazioni per minore a carico non valide!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 11 '5/6 PERSONE
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            LIMITE_REDDITO3 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Detrazioni per minore a carico non valide!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 12 '7 O + PERSONE
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            LIMITE_REDDITO4 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Detrazioni per minore a carico non valide!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 13 'REDDITO PER FIGLIO A CARICO
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            LIMITE_REDDITO_FIGLIO = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Limite Reddito per figlio a carico non valido!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 15 'REDDITO PER MINROE A CARICO
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            LIMITE_REDDITO_MINORE = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Limite Reddito per Minore a carico non valido!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If
                    Case 17 'PERCENTUALE DI DETRAZIONE PER REDDITO DA DIPENDENTE/PENSIONE E FIGLI
                        If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                            PERC_LAVORO_DIP = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                        Else
                            Response.Write("Percentuale di detrazione per lavoro dipendente o pensione non valido!")
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Function
                        End If

                End Select

            End While
            myReader.Close()


            RA = 0
            RD = 0
            RF = 0
            OD = 0

            RA1 = 0
            RD1 = 0
            RF1 = 0
            OD1 = 0

            MINORI = 0
            FIGLI = 0


            par.cmd.CommandText = "select * from DICHIARAZIONI_CAMBI where id=" & Pratica_Id
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                stringafile = "<p><b><font face='Arial' size='4'>Dichiarazione N. " & myReader("pg") & "&nbsp;</font></b></p>"
                MINORI = Val(par.IfNull(myReader("MINORI_CARICO"), 0))
            Else
                MINORI = 0
            End If
            myReader.Close()


            par.cmd.CommandText = "select COUNT(ID) from comp_nucleo_cambi where id_dichiarazione=" & Pratica_Id & " order by progr asc"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                COMPONENTI = myReader(0)
            Else
                COMPONENTI = 0
            End If
            myReader.Close()

            ReDim ComponentiNucleo(COMPONENTI)
            ReDim RedditoNucleo(COMPONENTI)


            par.cmd.CommandText = "select * from comp_nucleo_cambi where id_dichiarazione=" & Pratica_Id & " order by progr asc"
            myReader = par.cmd.ExecuteReader()



            'If myReader.Read Then
            I = 1
            While myReader.Read
                ComponentiNucleo(I) = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")
                If myReader("GRADO_PARENTELA") = 3 And myReader("PROGR") <> 0 Then
                    'If myReader("CARICO") = "1" Then 'Or myreader("CARICO") = "-1" Then
                    'FIGLI = FIGLI + 1
                    'End If
                    'End If
                Else
                    'If myreader("ETA") < 18 And myreader("GRADO_PARENTELA") <> 3 Then
                    'If myReader("GRADO_PARENTELA") <> 3 Then
                    '    If myReader("CARICO") = "1" Then 'Or myreader("CARICO") = "-1" Then
                    '        MINORI = MINORI + 1
                    '    End If
                    '    'End If
                    'End If
                End If
                If par.IfNull(myReader("PERC_INVAL"), 0) > 66 Then
                    Invalidi = Invalidi + 1
                End If
                I = I + 1
            End While

            myReader.Close()



            Maggiorazione = 0
            Select Case Invalidi
                Case 1
                    Maggiorazione = ((REDDITO_ERP * REDDITO_INVAL1) / 100)
                    percentuale_app = REDDITO_INVAL1
                Case Is > 1
                    Maggiorazione = ((REDDITO_ERP * REDDITO_INVAL2) / 100)
                    percentuale_app = REDDITO_INVAL2
                Case 0
                    Maggiorazione = 0
            End Select



            par.cmd.CommandText = "select * from domande_redditi_cambi where id_domanda=" & Pratica_Id
            myReader = par.cmd.ExecuteReader()
            I = 1
            While myReader.Read

                RA = RA + par.IfNull(myReader("AUTONOMO"), 0) + par.IfNull(myReader("occasionali"), 0)
                RA1 = RA1 + par.IfNull(myReader("AUTONOMO"), 0) + par.IfNull(myReader("occasionali"), 0)


                RD = RD + par.IfNull(myReader("dipendente"), 0) + par.IfNull(myReader("pensione"), 0)
                RD1 = RD1 + par.IfNull(myReader("dipendente"), 0) + par.IfNull(myReader("pensione"), 0)


                RF = RF + par.IfNull(myReader("dom_ag_fab"), 0)
                RF1 = RF1 + par.IfNull(myReader("dom_ag_fab"), 0)


                OD = OD + par.IfNull(myReader("oneri"), 0)
                OD1 = OD1 + par.IfNull(myReader("oneri"), 0)

                RedditoNucleo(I - 1) = Format(RA1 + RD1 + RF1 - OD1, "##,##0.00")
                TotaleLordo = TotaleLordo + Format(RA1 + RD1 + RF1 - OD1, "##,##0.00")
                RA1 = 0
                RD1 = 0
                RF1 = 0
                OD1 = 0

                I = I + 1
            End While
            myReader.Close()

            Select Case COMPONENTI
                Case 1, 2
                    If RF > LIMITE_REDDITO1 Then
                        Response.Write("Limite Reddito da beni Immobili (1/2 persone) superato!")
                        Response.Write("</BR>")
                    End If
                Case 3, 4
                    If RF > LIMITE_REDDITO2 Then
                        Response.Write("Limite Reddito da beni Immobili (3/4 persone) superato!")
                        Response.Write("</BR>")
                    End If
                Case 5, 6
                    If RF > LIMITE_REDDITO3 Then
                        Response.Write("Limite Reddito da beni Immobili (5/6 persone) superato!")
                        Response.Write("</BR>")
                    End If
                Case Else
                    If RF > LIMITE_REDDITO4 Then
                        Response.Write("Limite Reddito da beni Immobili (7 o + persone) superato!")
                        Response.Write("</BR>")
                    End If
            End Select

            'REDDITO_CONV = RA + (RD - ((RD * PERC_LAVORO_DIP) / 100)) - ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO)) + RF - OD
            Dim MM As Decimal

            If RD > 0 And RA = 0 And RF = 0 Then
                MM = (RD - ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO)) - OD)
                REDDITO_CONV = RA + (MM - ((MM * PERC_LAVORO_DIP) / 100))
            Else
                If RD = 0 And (RA > 0 Or RF > 0) Then
                    MM = ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO))
                    REDDITO_CONV = RA + RF - OD - MM
                Else
                    MM = (RD - ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO)))
                    REDDITO_CONV = RA + (MM - ((MM * PERC_LAVORO_DIP) / 100)) + RF - OD
                End If
            End If

            'stringafile = "<p><b><font face='Arial' size='4'>Domanda N. " & pgDomanda & "&nbsp;</font></b></p>"

            stringafile = stringafile & "<table border='0' cellpadding='0' cellspacing='0' width='100%'>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#FFFFCC'>Limite Reddito Convenzionale</td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#FFFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#FFFFCC'>Euro</td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#FFFFCC'> " & Format(REDDITO_ERP, "##,##0.00") & "</td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#CCFFCC'>N° Invalidi</td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#CCFFCC'> " & Invalidi & "</td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#FFFFCC'>Maggiorazione (" & percentuale_app & "%)</td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#FFFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#FFFFCC'>Euro</td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#FFFFCC'> " & Format(Maggiorazione, "##,##0.00") & "</td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#CCFFCC'>&nbsp;&nbsp; </td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#FFFFCC'>Limite Reddito Convenzionale effettivo</td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#FFFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#FFFFCC'>Euro</td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#FFFFCC'> " & Format(Maggiorazione + REDDITO_ERP, "##,##0.00") & "</td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#CCFFCC'>&nbsp; </td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#FFFFCC'>Componenti a Carico " & MINORI + FIGLI & "</td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#FFFFCC'>Totale Detrazioni</td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#FFFFCC'>Euro</td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#FFFFCC'> " & Format((FIGLI + MINORI) * REDDITO_FIGLIO, "##,##0.00") & "</td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#CCFFCC'>&nbsp; </td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#CCFFCC'></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%' bgcolor='#FFFFCC'><B>Reddito Convenzionale Calcolato</B></td>"
            stringafile = stringafile & "<td width='8%' bgcolor='#FFFFCC'></td>"
            stringafile = stringafile & "<td width='2%' bgcolor='#FFFFCC'><b>Euro</b></td>"
            stringafile = stringafile & "<td width='50%' bgcolor='#FFFFCC'><B>&nbsp;" & Format(REDDITO_CONV, "##,##0.00") & "</b></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='21%'></td>"
            stringafile = stringafile & "<td width='8%'></td>"
            stringafile = stringafile & "<td width='2%'></td>"
            stringafile = stringafile & "<td width='50%'></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "</table>"
            stringafile = stringafile & "</BR>"


            stringafile = stringafile & "</BR>"


            stringafile = stringafile & "<table border='0' cellpadding='0' cellspacing='0' width='100%'>"

            COLORE = "#CCFFCC"
            For I = 1 To UBound(ComponentiNucleo)
                stringafile = stringafile & "<tr>"
                stringafile = stringafile & "<td width='29%' bgcolor='" & COLORE & "'>" & ComponentiNucleo(I) & "</td>"
                stringafile = stringafile & "<td width='2%' bgcolor='" & COLORE & "'>Euro</td>"
                stringafile = stringafile & "<td width='50%' bgcolor='" & COLORE & "'> " & IfEmpty(RedditoNucleo(I - 1), "0,00") & "</td>"
                stringafile = stringafile & "</tr>"
                If COLORE = "#CCFFCC" Then
                    COLORE = "#FFFFCC"
                Else
                    COLORE = "#CCFFCC"
                End If
            Next I

            stringafile = stringafile & "<tr>"
            stringafile = stringafile & "<td width='29%'><B>TOTALE lordo</b></td>"
            stringafile = stringafile & "<td width='2%'><b>Euro</b></td>"
            stringafile = stringafile & "<td width='50%'><B>&nbsp;" & Format(TotaleLordo, "##,##0.00") & "</b></td>"
            stringafile = stringafile & "</tr>"

            stringafile = stringafile & "</table>"

            stringafile = stringafile & "<p><b><font face='Arial' size='3'>Dettaglio Redditi</font></b></p>"

            stringafile = stringafile & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf

            stringafile = stringafile & "<tr>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>COMPONENTE</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>CONDIZIONE</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>PROFESSIONE</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>DIPENDENTE</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>PENSIONE</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>AUTONOMO</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>NON IMPON.</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>OCCASIONALI</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>DOM./AGR./FABB.</font></td>" & vbCrLf
            stringafile = stringafile & "<td width='10%'><font face='Arial' size='1'>DEDUCIBILI</font></td>" & vbCrLf
            stringafile = stringafile & "</tr>"

            par.cmd.CommandText = "select domande_redditi_cambi.*,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME from domande_redditi_cambi,COMP_NUCLEO_cambi where domande_redditi_cambi.ID_COMPONENTE=COMP_NUCLEO_cambi.ID AND domande_redditi_cambi.id_domanda=" & Pratica_Id
            myReader = par.cmd.ExecuteReader()

            I = 1
            COLORE = "#CCFFCC"
            While myReader.Read
                stringafile = stringafile & "<tr>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & AssociaCondizione(par.IfNull(myReader("condizione"), "")) & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & AssociaOccupazione(par.IfNull(myReader("professione"), "")) & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("dipendente"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("pensione"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("AUTONOMO"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("non_imponibili"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("OCCASIONALI"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("DOM_AG_FAB"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "<td width='10%' bgcolor='" & COLORE & "'><font face='Arial' size='1'>" & Format(CDbl(par.IfNull(myReader("oneri"), 0)), "##,##0.00") & "</font></td>" & vbCrLf
                stringafile = stringafile & "</tr>"
                If COLORE = "#CCFFCC" Then
                    COLORE = "#FFFFCC"
                Else
                    COLORE = "#CCFFCC"
                End If




                I = I + 1
            End While

            myReader.Close()

            stringafile = stringafile & "</table>"








            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(stringafile)

            'If lblSecondario.Visible = False Then
            '    MyExecuteSql("UPDATE PRATICHE SET REDDITO_CONV=" & VirgoleInPunti(REDDITO_CONV) & ",FL_REDDITO_PENSIONATO=" & TIPO3 & ",FL_REDDITO_AUTONOMO=" & TIPO1 & ",FL_REDDITO_DIPENDENTE=" & TIPO2 & " WHERE ID=" & Pratica_Id)
            '    MyDb.CommitTrans()
            '    MyDb.BeginTrans()
            'End If
            ''Else
            ''If lblSecondario.Visible = False Then
            '' MyExecuteSql("UPDATE PRATICHE SET REDDITO_CONV=" & VirgoleInPunti(REDDITO_CONV) & ",FL_REDDITO_PENSIONATO=" & TIPO3 & ",FL_REDDITO_AUTONOMO=" & TIPO1 & ",FL_REDDITO_DIPENDENTE=" & TIPO2 & " WHERE ID=" & Pratica_Id)
            '' End If
            ''End If
        Catch ex As Exception
            Response.Write(ex.ToString)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function

    Private Function AssociaCondizione(ByVal s As String) As String
        AssociaCondizione = "--"
        Select Case s
            Case "1"
                AssociaCondizione = "01-Occupato"
            Case "2"
                AssociaCondizione = "02-In cerca di prima occupazione"
            Case "3"
                AssociaCondizione = "03-Disoccupato"
            Case "4"
                AssociaCondizione = "04-Casalinga"
            Case "5"
                AssociaCondizione = "05-Studente"
            Case "6"
                AssociaCondizione = "06-Infante"
            Case "7"
                AssociaCondizione = "07-Pensionato"
            Case "8"
                AssociaCondizione = "08-In servizio militare di leva"
            Case "9"
                AssociaCondizione = "09-Altra condizione non professionale"
            Case "10"
                AssociaCondizione = "10-Varie"
            Case "11"
                AssociaCondizione = "11-Lavoro Saltuario"
        End Select
    End Function

    Private Function AssociaOccupazione(ByVal s As String) As String
        AssociaOccupazione = "--"
        Select Case s
            Case "1"
                AssociaOccupazione = "01-Dirigente"
            Case "2"
                AssociaOccupazione = "02-Impiegato"
            Case "3"
                AssociaOccupazione = "03-Operaio"
            Case "4"
                AssociaOccupazione = "04-Apprendista"
            Case "5"
                AssociaOccupazione = "05-Lavoratore a domicilio"
            Case "6"
                AssociaOccupazione = "06-Militare in carriera"
            Case "7"
                AssociaOccupazione = "07-Imprenditore"
            Case "8"
                AssociaOccupazione = "08-Libero professionista"
            Case "9"
                AssociaOccupazione = "09-Lavoratore in proprio"
            Case "10"
                AssociaOccupazione = "10-Coadiuvante"
            Case "11"
                AssociaOccupazione = "11-Titolare di una pensione"
            Case "12"
                AssociaOccupazione = "12-Titolare di due pensione"
            Case "13"
                AssociaOccupazione = "13-Titolare di tre pensione"
            Case "14"
                AssociaOccupazione = "14-Titolare di quattro pensione"
            Case "15"
                AssociaOccupazione = "15-Aiuti economici"
        End Select

    End Function

End Class
