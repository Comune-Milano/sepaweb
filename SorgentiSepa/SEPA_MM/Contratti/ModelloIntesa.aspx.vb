Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Partial Class Contratti_ModelloIntesa
    Inherits PageSetIdMode
    Dim par As New CM.Global
    '*********************************
    Dim sUnita(19) As String
    Dim sDecina(9) As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim IdBolletta As String = Request.QueryString("ID")
        Dim TROVATO As Boolean = False
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim contenuto As String = ""

            par.cmd.CommandText = "SELECT unita_contrattuale.cod_unita_immobiliare,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOUI"",anagrafica.partita_iva,anagrafica.ragione_sociale,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,BOL_BOLLETTE.*,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL  AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_CONTRATTUALE.TIPOLOGIA AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID=BOL_BOLLETTE.COD_AFFITTUARIO AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID=" & IdBolletta
            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderJ.Read Then

                If par.IfNull(myReaderJ("FL_ANNULLATA"), "") = "1" Then
                    myReaderJ.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Response.Write("<script>alert('Bolletta Annullata!');self.close();</script>")

                End If
                If par.IfNull(myReaderJ("RIF_FILE"), "") = "MOD" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "REC" Then

                    If UCase(par.IfNull(myReaderJ("ragione_sociale"), "")) = "" Then
                        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModuloIntesa.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                        contenuto = sr1.ReadToEnd()
                        sr1.Close()
                    Else
                        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModuloIntesaRS.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                        contenuto = sr1.ReadToEnd()
                        sr1.Close()
                    End If


                    If par.IfNull(myReaderJ("RIF_FILE"), "") = "REC" Then
                        contenuto = Replace(contenuto, "$NOTA$", "Bolletta Provvisoria")
                    Else
                        contenuto = Replace(contenuto, "$NOTA$", "")
                    End If

                    'contenuto = Replace(contenuto, "$barre$", barcode1.Testo)
                    'BarCode(Format(IdBolletta, "000000000000"), IdBolletta)

                    'contenuto = Replace(contenuto, "$barre$", "<img src=" & Chr(34) & "ELABORAZIONI/" & IdBolletta & ".GIF" & Chr(34) & " alt=" & Chr(34) & "barre" & Chr(34) & "/>")
                    contenuto = Replace(contenuto, "$barre$", "")
                    contenuto = Replace(contenuto, "$indice$", Format(par.IfNull(myReaderJ("id"), "0"), "0000000000"))

                    Select Case UCase(Mid(par.IfNull(myReaderJ("COD_TIPOLOGIA_CONTR_LOC"), "XXX"), 1, 3))
                        Case "USD"
                            contenuto = Replace(contenuto, "$tipologia$", "USI DIVERSI")
                        Case "CON"
                            contenuto = Replace(contenuto, "$tipologia$", "CONCESSIONI")
                        Case "EQC"
                            contenuto = Replace(contenuto, "$tipologia$", "EQUO CANONE 392/78")
                        Case "ERP"
                            contenuto = Replace(contenuto, "$tipologia$", "ERP")
                        Case "L43"
                            contenuto = Replace(contenuto, "$tipologia$", "LEGGE 431/98")
                        Case "NON"
                            contenuto = Replace(contenuto, "$tipologia$", "NESSUNA TIP. (O.A.)")
                        Case Else
                            contenuto = Replace(contenuto, "$tipologia$", "---")
                    End Select

                    
                    contenuto = Replace(contenuto, "$codice$", par.IfNull(myReaderJ("cod_unita_immobiliare"), ""))
                    contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("INDIRIZZO"), ""))
                    contenuto = Replace(contenuto, "$cap$", Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), 1, 5))
                    contenuto = Replace(contenuto, "$comune$", Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), 7, InStr(par.IfNull(myReaderJ("CAP_CITTA"), ""), "(") - 7))
                    contenuto = Replace(contenuto, "$provincia$", Mid(par.IfNull(myReaderJ("CAP_CITTA"), ""), InStr(par.IfNull(myReaderJ("CAP_CITTA"), ""), "(") + 1, 2))
                    contenuto = Replace(contenuto, "$codicecontratto$", par.IfNull(myReaderJ("COD_CONTRATTO"), ""))
                    contenuto = Replace(contenuto, "$scadenza$", par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), "")))
                    contenuto = Replace(contenuto, "$causale$", UCase(par.IfNull(myReaderJ("note"), "")))

                    If UCase(par.IfNull(myReaderJ("ragione_sociale"), "")) = "" Then

                        contenuto = Replace(contenuto, "$cognome$", UCase(par.IfNull(myReaderJ("cognome"), "")))
                        contenuto = Replace(contenuto, "$nome$", UCase(par.IfNull(myReaderJ("nome"), "")))

                        contenuto = Replace(contenuto, "$fiscale$", UCase(par.IfNull(myReaderJ("cod_fiscale"), "")))
                        If Len(par.IfNull(myReaderJ("cod_fiscale"), "")) = 16 Then
                            contenuto = Replace(contenuto, "$1$", Mid(UCase(myReaderJ("cod_fiscale")), 1, 1))
                            contenuto = Replace(contenuto, "$2$", Mid(UCase(myReaderJ("cod_fiscale")), 2, 1))
                            contenuto = Replace(contenuto, "$3$", Mid(UCase(myReaderJ("cod_fiscale")), 3, 1))
                            contenuto = Replace(contenuto, "$4$", Mid(UCase(myReaderJ("cod_fiscale")), 4, 1))
                            contenuto = Replace(contenuto, "$5$", Mid(UCase(myReaderJ("cod_fiscale")), 5, 1))
                            contenuto = Replace(contenuto, "$6$", Mid(UCase(myReaderJ("cod_fiscale")), 6, 1))
                            contenuto = Replace(contenuto, "$7$", Mid(UCase(myReaderJ("cod_fiscale")), 7, 1))
                            contenuto = Replace(contenuto, "$8$", Mid(UCase(myReaderJ("cod_fiscale")), 8, 1))
                            contenuto = Replace(contenuto, "$9$", Mid(UCase(myReaderJ("cod_fiscale")), 9, 1))
                            contenuto = Replace(contenuto, "$10$", Mid(UCase(myReaderJ("cod_fiscale")), 10, 1))
                            contenuto = Replace(contenuto, "$11$", Mid(UCase(myReaderJ("cod_fiscale")), 11, 1))
                            contenuto = Replace(contenuto, "$12$", Mid(UCase(myReaderJ("cod_fiscale")), 12, 1))
                            contenuto = Replace(contenuto, "$13$", Mid(UCase(myReaderJ("cod_fiscale")), 13, 1))
                            contenuto = Replace(contenuto, "$14$", Mid(UCase(myReaderJ("cod_fiscale")), 14, 1))
                            contenuto = Replace(contenuto, "$15$", Mid(UCase(myReaderJ("cod_fiscale")), 15, 1))
                            contenuto = Replace(contenuto, "$16$", Mid(UCase(myReaderJ("cod_fiscale")), 16, 1))
                        End If
                    Else
                        contenuto = Replace(contenuto, "$cognome$", UCase(par.IfNull(myReaderJ("ragione_sociale"), "")))
                        contenuto = Replace(contenuto, "$fiscale$", UCase(par.IfNull(myReaderJ("partita_iva"), "")))
                        If Len(par.IfNull(myReaderJ("partita_iva"), "")) = 11 Then
                            contenuto = Replace(contenuto, "$1$", Mid(UCase(myReaderJ("partita_iva")), 1, 1))
                            contenuto = Replace(contenuto, "$2$", Mid(UCase(myReaderJ("partita_iva")), 2, 1))
                            contenuto = Replace(contenuto, "$3$", Mid(UCase(myReaderJ("partita_iva")), 3, 1))
                            contenuto = Replace(contenuto, "$4$", Mid(UCase(myReaderJ("partita_iva")), 4, 1))
                            contenuto = Replace(contenuto, "$5$", Mid(UCase(myReaderJ("partita_iva")), 5, 1))
                            contenuto = Replace(contenuto, "$6$", Mid(UCase(myReaderJ("partita_iva")), 6, 1))
                            contenuto = Replace(contenuto, "$7$", Mid(UCase(myReaderJ("partita_iva")), 7, 1))
                            contenuto = Replace(contenuto, "$8$", Mid(UCase(myReaderJ("partita_iva")), 8, 1))
                            contenuto = Replace(contenuto, "$9$", Mid(UCase(myReaderJ("partita_iva")), 9, 1))
                            contenuto = Replace(contenuto, "$10$", Mid(UCase(myReaderJ("partita_iva")), 10, 1))
                            contenuto = Replace(contenuto, "$11$", Mid(UCase(myReaderJ("partita_iva")), 11, 1))
                            'contenuto = Replace(contenuto, "$12$", Mid(UCase(myReaderJ("cod_fiscale")), 12, 1))
                            'contenuto = Replace(contenuto, "$13$", Mid(UCase(myReaderJ("cod_fiscale")), 13, 1))
                            'contenuto = Replace(contenuto, "$14$", Mid(UCase(myReaderJ("cod_fiscale")), 14, 1))
                            'contenuto = Replace(contenuto, "$15$", Mid(UCase(myReaderJ("cod_fiscale")), 15, 1))
                            'contenuto = Replace(contenuto, "$16$", Mid(UCase(myReaderJ("cod_fiscale")), 16, 1))
                        Else
                            contenuto = Replace(contenuto, "$1$", "")
                            contenuto = Replace(contenuto, "$2$", "")
                            contenuto = Replace(contenuto, "$3$", "")
                            contenuto = Replace(contenuto, "$4$", "")
                            contenuto = Replace(contenuto, "$5$", "")
                            contenuto = Replace(contenuto, "$6$", "")
                            contenuto = Replace(contenuto, "$7$", "")
                            contenuto = Replace(contenuto, "$8$", "")
                            contenuto = Replace(contenuto, "$9$", "")
                            contenuto = Replace(contenuto, "$10$", "")
                            contenuto = Replace(contenuto, "$11$", "")
                        End If

                    End If
                    contenuto = Replace(contenuto, "$codicecontratto$", UCase(par.IfNull(myReaderJ("cod_contratto"), "")))
                    TROVATO = True
                End If
            End If
            myReaderJ.Close()

            If TROVATO = True Then
                Dim IMPORTO As Double = 0
                Dim DETTAGLIO As String = ""
                Dim TOTALE As Double = 0

                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & IdBolletta & " ORDER BY BOL_BOLLETTE_VOCI.ID ASC"
                myReaderJ = par.cmd.ExecuteReader()

                Do While myReaderJ.Read
                    TOTALE = TOTALE + myReaderJ("IMPORTO")
                Loop
                myReaderJ.Close()
                contenuto = Replace(contenuto, "$importo$", "Euro " & Format(TOTALE, "0.00") & " ( " & NumeroInLettere(Format(TOTALE, "0.00")) & " )")

                Response.Write(contenuto)

                par.cmd.Dispose()
                par.OracleConn.Close()
            Else
                par.cmd.Dispose()
                par.OracleConn.Close()
                Response.Write("<script>alert('Questa bolletta non può essere stampata tramite modulo!');self.close();</script>")
            End If


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:ModelloIntesa - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub




    '******************************************************************************
    '                               NumeroToLettere
    '
    '                Converte il numero intero in lettere
    '
    ' Input : ImportoN                -->Importo Numerico
    '
    ' Ouput : NumeroToLettere         -->Il numero in lettere
    '******************************************************************************
    Function NumeroInLettere(ByVal Numero As String) As String

        '************************
        'Gestisce la virgola
        '************************
        Dim PosVirg As Integer
        Dim Lettere As String

        Numero$ = ChangeStr(Numero$, ".", "")
        PosVirg% = InStr(Numero$, ",")

        If PosVirg% Then
            Lettere$ = NumInLet(Mid(Numero$, 1, Len(Numero) + PosVirg% - 1))
            Lettere$ = Lettere$ & "\" & Format(CInt(Mid(Numero$, PosVirg% + 1, Len(Numero$))), "00")
        Else
            Lettere$ = NumInLet(CDbl(Numero$))
        End If

        NumeroInLettere = Lettere$

    End Function

    Private Function NumInLet(ByVal N As Double) As String

        '************************************************
        'inizializzo i due arry di numeri
        '************************************************
        SetNumeri()

        Dim ValT As Double     'Valore Temporaneo per la conversione
        Dim iCent As Integer    'Valore su cui calcolare le centinaia
        Dim L As String     'Importo in Lettere

        NumInLet = "zero"

        If N = 0 Then Exit Function

        ValT = N
        L = ""

        'miliardi
        iCent = Int(ValT / 1000000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = "unmiliardo"
            Else
                L = LCent(iCent) + "miliardi"
            End If
            ValT = ValT - CDbl(iCent) * 1000000000.0#
        End If

        'milioni
        iCent = Int(ValT / 1000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = L + "unmilione"
            Else
                L = L + LCent(iCent) + "milioni"
            End If
            ValT = ValT - CDbl(iCent) * 1000000.0#
        End If

        'miliaia
        iCent = Int(ValT / 1000)
        If iCent Then
            If iCent = 1 Then
                L = L + "mille"
            Else
                L = L + LCent(iCent) + "mila"
            End If
            ValT = ValT - CDbl(iCent) * 1000
        End If

        ''centinaia
        'If ValT Then
        '    L = L + LCent(CInt(ValT))
        'End If
        If ValT Then
            L = L + LCent(Fix(CDbl(ValT)))
        End If

        NumInLet = L

    End Function

    Function LCent(ByVal N As Integer) As String

        ' Ritorna xx% (1/999) convertito in lettere
        Dim Numero As String
        Dim Lettere As String
        Dim Centinaia As Integer
        Dim Decine As Integer
        Dim x As Integer
        Dim Unita As Integer
        Dim sDec As String

        Numero$ = Format(N, "000")

        Lettere$ = ""
        Centinaia% = Val(Left$(Numero$, 1))
        If Centinaia% Then
            If Centinaia% > 1 Then
                Lettere = sUnita(Centinaia%)
            End If
            Lettere = Lettere + "cento"
        End If

        Decine% = (N Mod 100)
        If Decine% Then
            Select Case Decine%
                Case Is >= 20                               'Decine
                    sDec = sDecina(Val(Mid$(Numero$, 2, 1)))
                    x% = Len(sDec)
                    Unita% = Val(Right$(Numero$, 1))          'Unita
                    If Unita% = 1 Or Unita% = 8 Then x% = x% - 1
                    Lettere$ = Lettere$ & Left(sDec, x%) & sUnita(Unita%)    'Tolgo l'ultima lettera della decina per i
                Case Else
                    Lettere$ = Lettere$ + sUnita(Decine)
            End Select
        End If

        LCent$ = Lettere$

    End Function


    Sub SetNumeri()

        '************************************************
        ' Stringhe per traslitterazione numeri
        '************************************************
        sUnita(1) = "uno"
        sUnita(2) = "due"
        sUnita(3) = "tre"
        sUnita(4) = "quattro"
        sUnita(5) = "cinque"
        sUnita(6) = "sei"
        sUnita(7) = "sette"
        sUnita(8) = "otto"
        sUnita(9) = "nove"
        sUnita(10) = "dieci"
        sUnita(11) = "undici"
        sUnita(12) = "dodici"
        sUnita(13) = "tredici"
        sUnita(14) = "quattordici"
        sUnita(15) = "quindici"
        sUnita(16) = "sedici"
        sUnita(17) = "diciassette"
        sUnita(18) = "diciotto"
        sUnita(19) = "diciannove"

        sDecina(1) = "dieci"
        sDecina(2) = "venti"
        sDecina(3) = "trenta"
        sDecina(4) = "quaranta"
        sDecina(5) = "cinquanta"
        sDecina(6) = "sessanta"
        sDecina(7) = "settanta"
        sDecina(8) = "ottanta"
        sDecina(9) = "novanta"

    End Sub

    '*********************************************************************
    '                ChangeStr - da usare con versioni minori del Vb6
    '
    'Input  = Stringa                           -->Da convertire
    '         Lettera da sostituire             -->Da convertire
    '         Nuova lettera da rimpiazzare      -->Da convertire
    '
    'Ouput  = Stringa rimpiazzata
    '
    '*********************************************************************
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String, _
                       ByRef NewChar As String) As String

        Dim TmpBuf As String
        Dim p As Integer

        On Error GoTo ErrChangeStr

        ChangeStr$ = ""   'Default Error

        TmpBuf$ = sBuffer$
        p% = InStr(TmpBuf$, OldChar$)
        Do While p > 0
            TmpBuf$ = Left$(TmpBuf$, p% - 1) + NewChar$ + Mid$(TmpBuf$, p% + Len(OldChar$))
            p% = InStr(p% + Len(NewChar$), TmpBuf$, OldChar$)
        Loop
        ChangeStr$ = TmpBuf$

        Exit Function

ErrChangeStr:
        ChangeStr$ = ""

    End Function

    Private Function BarCode(ByVal Input As String, ByVal indice As String)
        Dim ValidInput As String = "0123456789"
        Dim ValidOdd As String = "0001101001100100100110111101010001101100010101111011101101101110001011"
        Dim ValidEven As String = "0100111011001100110110100001001110101110010000101001000100010010010111"
        Dim Parities As String = "OOOOOOOOEOEEOOEEOEOOEEEOOEOOEEOEEOOEOEEEOEOEOEOEOEOEEOOEEOEO"
        If Input.Length <> 12 Then
            Response.Write("Invalid input")
            Response.End()
        End If
        Dim Digit, i As Integer
        For i = 1 To Input.Length
            If Instr(1, ValidInput, Mid(Input, i, 1)) = 0 Then
                Response.Write("Invalid input")
                Response.End()
            End If
        Next
        For i = 1 To Input.Length Step 2
            Digit += (Val(Mid(Input, i, 1)) * 3)
        Next
        For i = 2 To Input.Length Step 2
            Digit += Val(Mid(Input, i, 1))
        Next
        Digit = Digit Mod 10
        If Digit > 0 Then Digit = 10 - Digit
        Input = Input & Digit.ToString()
        Dim Parity As String = Mid(Parities, (Val(Mid(Input, 2, 1)) * 6) + 1, 6)
        Dim bmp As Bitmap = New Bitmap((Input.Length * 50) + 20, 50)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.FillRectangle(New SolidBrush(Color.White), 0, 0, (Input.Length * 50) + 20, 50)
        Dim p As New Pen(Color.Black, 1)
        Dim BarValue As String
        Dim BarX As Integer
        Dim BarSlice As Short
        g.DrawLine(p, BarX, 0, BarX, 50)
        BarX += 2
        g.DrawLine(p, BarX, 0, BarX, 50)
        BarX += 2
        For i = 2 To 7
            If Mid(Parity, i, 1) = "E" Then
                BarValue = Mid(ValidEven, ((InStr(1, ValidInput, Mid(Input, i, 1)) - 1) * 7) + 1, 7)
            Else
                BarValue = Mid(ValidOdd, ((InStr(1, ValidInput, Mid(Input, i, 1)) - 1) * 7) + 1, 7)
            End If
            For BarSlice = 1 To 7
                If Mid(BarValue, BarSlice, 1) = "1" Then
                    g.DrawLine(p, BarX, 0, BarX, 40)
                End If
                BarX += 1
            Next
        Next
        BarX += 1
        g.DrawLine(p, BarX, 0, BarX, 50)
        BarX += 2
        g.DrawLine(p, BarX, 0, BarX, 50)
        BarX += 2
        For i = 8 To 13
            BarValue = Mid(ValidOdd, ((InStr(1, ValidInput, Mid(Input, i, 1)) - 1) * 7) + 1, 7)
            For BarSlice = 1 To 7
                If Mid(BarValue, BarSlice, 1) = "0" Then
                    g.DrawLine(p, BarX, 0, BarX, 40)
                End If
                BarX += 1
            Next
        Next
        BarX += 1
        g.DrawLine(p, BarX, 0, BarX, 50)
        BarX += 2
        g.DrawLine(p, BarX, 0, BarX, 50)
        'bmp.Save(Response.OutputStream, ImageFormat.GIF)
        'g.Dispose()
        'bmp.Dispose()
        Dim MEMS As New System.IO.MemoryStream
        bmp.Save(MEMS, ImageFormat.Gif)
        System.IO.File.WriteAllBytes(Server.MapPath("ELABORAZIONI/") & indice & ".GIF", MEMS.GetBuffer())

        MEMS.Dispose()
        g.Dispose()
        bmp.Dispose()
    End Function

    'Private Function BarCode(ByVal Input As String, ByVal indice As String)
    '    Dim ValidInput As String = " !" & Chr(34) & "#$%&()**+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" & Chr(0) & Chr(1) & Chr(2) & Chr(3) & Chr(4) & Chr(5) & Chr(6) & Chr(7) & Chr(8) & Chr(9) & Chr(10) & Chr(255)
    '    Dim ValidCodes As String = "17401644163811761164110012241220112416081604157214361244123014841260125416501628161417641652190218681836183018921844184217521734159013041112109414161128112216721576157014641422113414961478114219101678158217681762177418801862181418961890181819141602193013281292120011581068106214241412123212181076107415541616197815561146134012121182150812681266195619401938175817821974140013101118151215061960195415021518188619661724168016926379"
    '    Dim Digit As Integer = 103
    '    Dim i As Integer
    '    For i = 1 To Input.Length
    '        Digit += (i * InStr(1, ValidInput, Mid(Input, i, 1)))
    '    Next
    '    Digit = Digit Mod 103
    '    Input = Chr(8) & Input & Mid(ValidInput, Digit, 1) & Chr(255)
    '    Dim bmp As Bitmap = New Bitmap((Input.Length * 11) + 13, 50)
    '    Dim g As Graphics = Graphics.FromImage(bmp)
    '    g.FillRectangle(New SolidBrush(Color.White), 0, 0, (Input.Length * 11) + 13, 50)
    '    Dim p As New Pen(Color.Black, 1)
    '    Dim BarValue, BarX As Integer
    '    Dim BarSlice As Short
    '    For i = 1 To Input.Length
    '        Try
    '            If InStr(1, ValidInput, Mid(Input, i, 1)) > 0 Then
    '                BarValue = Val(Mid(ValidCodes, ((InStr(1, ValidInput, Mid(Input, i, 1)) - 1) * 4) + 1, 4))
    '                Digit = 11
    '                If i = Input.Length Then Digit = 13
    '                For BarSlice = Digit To 0 Step -1
    '                    If BarValue >= 2 ^ BarSlice Then
    '                        g.DrawLine(p, BarX, 0, BarX, 50)
    '                        BarValue = BarValue - (2 ^ BarSlice)
    '                    End If
    '                    BarX += 1
    '                Next
    '            Else
    '                Response.Write("Invalid input")
    '                Response.End()
    '            End If
    '        Catch
    '        End Try
    '    Next

    '    Dim MEMS As New System.IO.MemoryStream
    '    bmp.Save(MEMS, ImageFormat.Gif)
    '    System.IO.File.WriteAllBytes(Server.MapPath("ELABORAZIONI/") & indice & ".GIF", MEMS.GetBuffer())

    '    MEMS.Dispose()
    '    g.Dispose()
    '    bmp.Dispose()
    'End Function

End Class
