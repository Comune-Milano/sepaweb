Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.IO


<WebService(Description:="Sepa@Web-Gestione Fornitori", Namespace:="https://.it/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Fornitori
    Inherits System.Web.Services.WebService
    Public Esito As String
    Public Elenco As String = ""
    Dim par As New CM.Global
    Public XMLError As String


    Function VerificaPIVA(ByVal Numero As String) As Boolean
        Dim Risultato, PIVA, PIVACodiceControllo, SommaA, SommaB, SommaC, I, Cifra, CodiceControllo

        PIVA = Numero
        Risultato = True

        If Len(PIVA) = 11 And IsNumeric(PIVA) Then
            PIVACodiceControllo = Right(PIVA, 1)

            SommaA = 0
            SommaB = 0
            For I = 1 To 10
                If I Mod 2 = 1 Then
                    SommaA = SommaA + CInt(Mid(PIVA, I, 1))
                Else
                    Cifra = 0
                    Cifra = CInt(Mid(PIVA, I, 1)) * 2
                    If Cifra >= 10 Then
                        Cifra = CInt(Mid(Cifra, 1, 1)) + CInt(Mid(Cifra, 2, 1))
                    End If
                    SommaB = SommaB + Cifra
                End If
            Next

            SommaC = 0
            SommaC = SommaA + SommaB

            If SommaC >= 10 Then
                SommaC = SommaC - (CInt(Mid(SommaC, 1, 1)) * 10)
            End If

            CodiceControllo = 10 - SommaC

            If CInt(PIVACodiceControllo) = CInt(CodiceControllo) Then
                Risultato = True
            Else
                Risultato = False
            End If
        Else
            Risultato = False
        End If

        VerificaPIVA = Risultato
    End Function


    Private Function LeggiXML(ByVal miofile As String) As String
        Dim Testo As String = ""
        Dim j As Long
        Dim reader As XmlTextReader = New XmlTextReader(miofile)
        Dim reader1 As XmlTextReader = New XmlTextReader(miofile)
        Dim CODICEF As String = ""
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim TUTTOOK As String = "0"
        Dim ElencoErrori As String = ""
        Dim cf As String = ""
        Dim piva As String = ""
        Dim INDICE_FORNITORE As Long = -1
        Dim ChiaveEnteEsterno As String = "-1"
        Dim indirizzo As String = ""
        Dim chiavi() As String
        Dim kk As Integer = 0
        Dim dainserire As Boolean = True
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            Dim pw As String = ""

            Do While (reader1.Read())
                Select Case reader1.NodeType
                    Case XmlNodeType.Element
                        If reader1.Name = "ElencoFornitori" Then
                            While reader1.MoveToNextAttribute()
                                If reader1.Name = "Password" Then
                                    pw = reader1.Value
                                End If
                            End While
                        End If
                        If reader1.Name = "Indirizzi" Then
                            While reader1.MoveToNextAttribute()
                                If reader1.Name = "ChiaveEnteEsterno" Then
                                    ReDim Preserve chiavi(kk)
                                    chiavi(kk) = reader1.Value
                                    kk = kk + 1
                                End If
                            End While
                        End If
                        If reader1.Name = "Indirizzi" Then
                            While reader1.MoveToNextAttribute()
                                If reader1.Name = "ChiaveEnteEsterno" Then
                                    ReDim Preserve chiavi(kk)
                                    chiavi(kk) = reader1.Value
                                    kk = kk + 1
                                End If
                            End While
                        End If
                End Select
            Loop
            reader1.Close()

            If pw <> "SEPAFORNITORI" & Format(Now, "yyyyMMdd") Then
                dainserire = False
                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                TUTTOOK = "1"
                ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>Password errata. Trasmissione non accettata</descrizioneTecnicaRisultato>"
                LeggiXML = Encode("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
                & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
                & "<Risposta codiceRisultato=" & Chr(34) & "1" & Chr(34) & ">" & ElencoErrori & "</Risposta>")
                reader.Close()
                Exit Function
            End If

            For j = 0 To kk - 1

            Next



            'par.cmd.CommandText = "select * from SISCOM_MI.FORNITORI"
            'Dim myReaderAB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Do While myReaderAB.Read
            '    Try
            '        par.cmd.CommandText = "delete from siscom_mi.fornitori where id=" & myReaderAB("id")
            '        par.cmd.ExecuteNonQuery()
            '    Catch ex As Exception

            '    End Try
            'Loop
            'myReaderAB.Close()

            TUTTOOK = "0"
            j = 0
            LeggiXML = ""
            Do While (reader.Read())
                Select Case reader.NodeType
                    Case XmlNodeType.Element
                        If reader.Name = "Fornitore" Then
                            CODICEF = ""
                            Testo = ""
                            dainserire = True
                            cf = ""
                            piva = ""
                            INDICE_FORNITORE = -1
                            While reader.MoveToNextAttribute()
                                If reader.Name = "CodiceFornitore" Then
                                    Testo = Testo & "COD_FORNITORE='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    CODICEF = UCase(par.PulisciStrSql(reader.Value))
                                End If
                                If reader.Name = "TipoFornitore" Then
                                    Testo = Testo & "TIPO='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                End If
                                If reader.Name = "NomeFornitore" Then
                                    Testo = Testo & "RAGIONE_SOCIALE='" & Replace(UCase(par.PulisciStrSql(reader.Value)), "&AMP;", "&") & "',"
                                End If
                                If reader.Name = "CodiceFiscale" Then
                                    Testo = Testo & "COD_FISCALE='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    cf = UCase(par.PulisciStrSql(reader.Value))
                                End If
                                If reader.Name = "PartitaIva" Then
                                    Testo = Testo & "PARTITA_IVA='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    piva = UCase(par.PulisciStrSql(reader.Value))
                                End If
                                If reader.Name = "AliquotaRitAcconto" Then
                                    Testo = Testo & "RIT_ACCONTO=" & par.VirgoleInPunti(reader.Value) & ","
                                End If
                            End While


                            If cf <> "" Then
                                'If par.ControllaCF(cf) = False Then
                                '    TUTTOOK = "1"
                                '    ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>Cod.Fornitore " & CODICEF & ": Il tag CodiceFiscale contiene un codice fiscale non valido.</descrizioneTecnicaRisultato>"
                                '    dainserire = False
                                'End If
                                If Len(cf) > 16 Then
                                    TUTTOOK = "1"
                                    ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>Cod.Fornitore " & CODICEF & ": Il tag CodiceFiscale contiene un codice fiscale non valido.</descrizioneTecnicaRisultato>"
                                    dainserire = False
                                End If
                            End If

                            'If piva <> "" Then
                            '    If VerificaPIVA(piva) = False Then
                            '        TUTTOOK = "1"
                            '        ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>Cod.Fornitore " & CODICEF & ": Il tag PartitaIva non e' valido.</descrizioneTecnicaRisultato>"
                            '        dainserire = False
                            '    End If
                            'End If

                            If piva <> "" Then
                                If Len(piva) <> 11 Then
                                    If VerificaPIVA(piva) = False Then
                                        TUTTOOK = "1"
                                        ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>Cod.Fornitore " & CODICEF & ": Il tag PartitaIva non e' valido.</descrizioneTecnicaRisultato>"
                                        dainserire = False
                                    End If
                                End If
                            End If

                            If dainserire = True Then
                                par.cmd.CommandText = "select * from siscom_mi.FORNITORI WHERE COD_FORNITORE='" & CODICEF & "'"
                                myReader1 = par.cmd.ExecuteReader()
                                If myReader1.HasRows = False Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI (ID,TIPO,COD_FORNITORE) VALUES (SISCOM_MI.SEQ_FORNITORI.NEXTVAL,'F','" & CODICEF & "')"
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = "select SISCOM_MI.SEQ_FORNITORI.CURRVAL from dual"
                                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then
                                        INDICE_FORNITORE = myReaderA(0)
                                    End If
                                    myReaderA.Close()

                                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_INDIRIZZI (ID,ID_FORNITORE) VALUES (SISCOM_MI.SEQ_FORNITORI_INDIRIZZI.NEXTVAL," & INDICE_FORNITORE & ")"
                                    'par.cmd.ExecuteNonQuery()

                                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_IBAN (ID,ID_FORNITORE) VALUES (SISCOM_MI.SEQ_FORNITORI_IBAN.NEXTVAL," & INDICE_FORNITORE & ")"
                                    'par.cmd.ExecuteNonQuery()

                                Else
                                    If myReader1.Read Then
                                        INDICE_FORNITORE = myReader1("ID")
                                    End If
                                End If
                                myReader1.Close()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI SET " & Mid(Testo, 1, Len(Testo) - 1) & " WHERE ID=" & INDICE_FORNITORE
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If

                        If INDICE_FORNITORE <> -1 Then
                            If reader.Name = "Indirizzi" Then
                                Testo = ""
                                dainserire = True
                                ChiaveEnteEsterno = ""
                                indirizzo = ""

                                While reader.MoveToNextAttribute()
                                    If reader.Name = "ChiaveEnteEsterno" Then
                                        ChiaveEnteEsterno = UCase(par.PulisciStrSql(reader.Value))
                                    End If

                                    If reader.Name = "Indirizzo" Then
                                        Testo = Testo & "INDIRIZZO='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                        indirizzo = UCase(par.PulisciStrSql(reader.Value))
                                    End If

                                    If reader.Name = "Citta" Then
                                        Testo = Testo & "COMUNE='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "Cap" Then
                                        Testo = Testo & "CAP='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "Telefono" Then
                                        Testo = Testo & "TELEFONO_1='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "Fax" Then
                                        Testo = Testo & "FAX='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "Email" Then
                                        Testo = Testo & "EMAIL='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "Provincia" Then
                                        Testo = Testo & "PR='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                End While

                                If ChiaveEnteEsterno = "" Then
                                    TUTTOOK = "1"
                                    ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>Indirizzo " & indirizzo & ": Il tag ChiaveEnteEsterno non e' valido.</descrizioneTecnicaRisultato>"
                                    dainserire = False
                                End If

                                If dainserire = True Then
                                    par.cmd.CommandText = "select * from siscom_mi.FORNITORI_indirizzi WHERE ID_FORNITORE=" & INDICE_FORNITORE & " AND CHIAVE_ENTE_ESTERNO=" & ChiaveEnteEsterno
                                    myReader1 = par.cmd.ExecuteReader()
                                    If myReader1.HasRows = False Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_INDIRIZZI (ID,ID_FORNITORE,CHIAVE_ENTE_ESTERNO) VALUES (SISCOM_MI.SEQ_FORNITORI_INDIRIZZI.NEXTVAL," & INDICE_FORNITORE & ",'" & ChiaveEnteEsterno & "')"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                    myReader1.Close()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI_INDIRIZZI SET " & Mid(Testo, 1, Len(Testo) - 1) & " WHERE ID_FORNITORE=" & INDICE_FORNITORE & " AND CHIAVE_ENTE_ESTERNO='" & ChiaveEnteEsterno & "'"
                                    par.cmd.ExecuteNonQuery()
                                End If

                            End If
                        End If

                        If INDICE_FORNITORE <> -1 Then
                            If reader.Name = "DatiPagamento" Then
                                Testo = ""
                                dainserire = True
                                ChiaveEnteEsterno = ""
                                indirizzo = ""

                                While reader.MoveToNextAttribute()
                                    If reader.Name = "ChiaveEnteEsterno" Then
                                        ChiaveEnteEsterno = UCase(par.PulisciStrSql(reader.Value))
                                    End If

                                    If reader.Name = "IBAN" Then
                                        Testo = Testo & "IBAN='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                        indirizzo = UCase(par.PulisciStrSql(reader.Value))
                                    End If

                                    If reader.Name = "MetodoPagamento" Then
                                        Testo = Testo & "METODO_PAGAMENTO='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "TipoPagamento" Then
                                        Testo = Testo & "TIPO_PAGAMENTO='" & UCase(par.PulisciStrSql(reader.Value)) & "',"
                                    End If

                                    If reader.Name = "Attivo" Then
                                        Testo = Testo & "FL_ATTIVO=" & UCase(par.PulisciStrSql(reader.Value)) & ","
                                    End If

                                End While

                                If ChiaveEnteEsterno = "" Then
                                    TUTTOOK = "1"
                                    ElencoErrori = ElencoErrori & "<descrizioneTecnicaRisultato>IBAN " & indirizzo & ": Il tag ChiaveEnteEsterno non e' valido.</descrizioneTecnicaRisultato>"
                                    dainserire = False
                                End If

                                If dainserire = True Then
                                    par.cmd.CommandText = "select * from siscom_mi.FORNITORI_IBAN WHERE ID_FORNITORE=" & INDICE_FORNITORE & " AND CHIAVE_ENTE_ESTERNO=" & ChiaveEnteEsterno
                                    myReader1 = par.cmd.ExecuteReader()
                                    If myReader1.HasRows = False Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_IBAN (ID,ID_FORNITORE,CHIAVE_ENTE_ESTERNO) VALUES (SISCOM_MI.SEQ_FORNITORI_IBAN.NEXTVAL," & INDICE_FORNITORE & ",'" & ChiaveEnteEsterno & "')"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                    myReader1.Close()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI_IBAN SET " & Mid(Testo, 1, Len(Testo) - 1) & " WHERE ID_FORNITORE=" & INDICE_FORNITORE & " AND CHIAVE_ENTE_ESTERNO='" & ChiaveEnteEsterno & "'"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        End If

                End Select
            Loop
            reader.Close()

            ' If TUTTOOK = "0" Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_ELENCO_TRASMISSIONI (DATA_TRASMISSIONE,FILE_TRASMESSO) " _
               & "VALUES ('" & Format(Now, "yyyyMMddHHmmss") & "',:TESTO) "

            Dim objStream As Stream = File.Open(miofile, FileMode.Open)
            Dim buffer(objStream.Length) As Byte
            objStream.Read(buffer, 0, objStream.Length)
            objStream.Close()
            Dim parmData As New Oracle.DataAccess.Client.OracleParameter
            With parmData
                .Direction = Data.ParameterDirection.Input
                .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                .ParameterName = "TESTO"
                .Value = buffer
            End With
            par.cmd.Parameters.Add(parmData)
            par.cmd.ExecuteNonQuery()
            'End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If TUTTOOK = "0" Then
                LeggiXML = Encode("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
                    & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
                    & "<Risposta codiceRisultato=" & Chr(34) & "0" & Chr(34) & "><descrizioneTecnicaRisultato></descrizioneTecnicaRisultato></Risposta>")
            Else
                LeggiXML = Encode("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
                    & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
                    & "<Risposta codiceRisultato=" & Chr(34) & "2" & Chr(34) & ">" & ElencoErrori & "</Risposta>")
            End If




        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            LeggiXML = Encode("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
             & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
             & "<Risposta codiceRisultato=" & Chr(34) & "1" & Chr(34) & "><descrizioneTecnicaRisultato>" & Replace(ex.Message, "è", "e'") & "</descrizioneTecnicaRisultato></Risposta>")
        End Try
    End Function



    Function Encode(ByVal dec As String) As String

        Dim bt() As Byte
        ReDim bt(dec.Length)

        bt = System.Text.Encoding.ASCII.GetBytes(dec)

        Dim enc As String
        enc = System.Convert.ToBase64String(bt)

        Return enc
    End Function

    Function Decode(ByVal enc As String) As String
        Dim bt() As Byte

        bt = System.Convert.FromBase64String(enc)
        Dim dec As String
        dec = System.Text.Encoding.ASCII.GetString(bt)
        Return dec
    End Function

    <WebMethod(Description:="Inserimento Elenco Fornitori")> _
    Public Function TrasferisciFile(ByVal TestoXML As String) As String
        Try
            Dim sbXML As String = ""
            Dim sbXSD As String = ""
            Dim password As String = "FORNITORI" & Format(Now, "yyyyMMdd")
            Dim NomeFile As String = Server.MapPath("FileTemp\" & "FornitoriXML_" & Format(Now, "yyyyMMddHHmmss") & ".xml")


            'Dim wrapper As New Simple3Des(password)
            'Dim plainText As String = wrapper.DecryptData(TestoXML)

            Dim plainText As String = Decode(TestoXML)
            sbXML = plainText
            sbXML = Replace(sbXML, "&amp;", "&")
            sbXML = Replace(plainText, "&", "&amp;")



            'legge il file xsd
            Dim MyfileXsd As New System.IO.StreamReader(Server.MapPath("Fornitori.xsd"), True)
            sbXSD = MyfileXsd.ReadToEnd
            MyfileXsd.Close()

            Dim m As Boolean


            'Encode

            m = LoadXMLData(sbXML, sbXSD)
            If XMLError <> "" Then
                Esito = Encode(("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
                        & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
                        & "<Risposta codiceRisultato=" & Chr(34) & "1" & Chr(34) & "><descrizioneTecnicaRisultato>" & Replace(XMLError, "è", "e'") & "</descrizioneTecnicaRisultato></Risposta>"))
            Else

                Dim sr As StreamWriter = New StreamWriter(NomeFile, False, System.Text.Encoding.Default)
                sr.WriteLine(sbXML)
                sr.Close()
                Esito = LeggiXML(NomeFile)


            End If


        Catch ex1 As System.Security.Cryptography.CryptographicException

            Esito = Encode("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
            & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
            & "<Risposta codiceRisultato=" & Chr(34) & "1" & Chr(34) & "><descrizioneTecnicaRisultato>" & Replace(ex1.Message, "è", "e'") & "</descrizioneTecnicaRisultato></Risposta>")

        Catch ex As Exception

            Esito = Encode("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
                        & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
                        & "<Risposta codiceRisultato=" & Chr(34) & "1" & Chr(34) & "><descrizioneTecnicaRisultato>" & Replace(ex.Message, "è", "e'") & "</descrizioneTecnicaRisultato></Risposta>")


        End Try
        Return Esito
    End Function


    Public Function LoadXMLData(ByVal XMLData As String, ByVal XSDSchema As String) As Boolean
        Dim MyXMLDocument As New System.Xml.XmlDocument
        Try


            XMLError = ""
            'Crea il documento XML utilizzando il codice passato in XMLData

            Dim MyXSDTextReader As New System.IO.StringReader(XSDSchema)
            'crea lo schema con l'handler all'evento di validazione che mi interessa
            Dim MyXMLSchema As System.Xml.Schema.XmlSchema = System.Xml.Schema.XmlSchema.Read(MyXSDTextReader, New System.Xml.Schema.ValidationEventHandler(AddressOf XMLEvent))

            Dim MyXMLTextReader As New System.IO.StringReader(XMLData)

            MyXMLDocument.Load(MyXMLTextReader)
            MyXMLDocument.Schemas.Add(MyXMLSchema)
            MyXMLDocument.Validate(New System.Xml.Schema.ValidationEventHandler(AddressOf XMLEvent))

        Catch ex As Exception
            XMLError = "File xml errato. Verificare che i tag di apertura e chiusura coincidano e che il file sia sintatticamente corretto!"
        End Try


    End Function

    Private Sub XMLEvent(ByVal sender As Object, ByVal e As System.Xml.Schema.ValidationEventArgs)
        XMLError += (e.Message & vbCrLf)
    End Sub

    <WebMethod(Description:="Elenco Inserimenti Effettuati")> _
    Public Function ElencoInserimenti() As String
        Try
            Dim i As Integer = 0
            Dim s As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from siscom_mi.FORNITORI_ELENCO_TRASMISSIONI order by data_trasmissione desc"
            Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            s = ""

            Do While myReaderAA.Read
                i = i + 1
                '20110909122642
                s = s & "<Trasmissione><DataInvio>" & par.FormattaData(Mid(myReaderAA("data_trasmissione"), 1, 8)) & "</DataInvio>" & Mid(myReaderAA("data_trasmissione"), 9, 2) & ":" & Mid(myReaderAA("data_trasmissione"), 11, 2) & "<OraInvio></OraInvio></Trasmissione>"
            Loop
            myReaderAA.Close()


            Esito = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
                & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
                & "<Risultati>" & s & "</Risultati>"

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Esito = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>" _
               & vbCrLf & "<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->" & vbCrLf _
               & "<Risultati><errore>" & ex.Message & "</errore></Risultati>"
        End Try

        Return Esito
    End Function

    <WebMethod(Description:="Hash")> _
    Public Function VerificaHash512(ByVal Testo As String, ByVal IdOp As Long) As Boolean
        Dim Esito As Boolean = False
        Try
            Dim pw As String = ""
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "select * from operatori where id=" & IdOp
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                pw = par.IfNull(myReader("pw"), "")
            End If
            myReader.Close()

            Dim PwMatch As Boolean = par.VerifyHash(par.DeCripta(Testo), "SHA512", Trim(par.IfNull(pw, ""))).ToString()
            If PwMatch = False Then
                Esito = False
            Else
                Esito = True
            End If
        Catch ex As Exception
            Esito = False
        End Try

        Return Esito
    End Function

    <WebMethod(Description:="Hash")> _
    Public Function ImpostaHash512(ByVal Testo As String) As String
        Dim Esito As String = ""
        Try
            Esito = par.ComputeHash(par.DeCripta(Testo), "SHA512", Nothing)
        Catch ex As Exception
            Esito = ""
        End Try

        Return Esito
    End Function

    <WebMethod(Description:="Chiave")> _
    Public Function ImpostaChiave(ByVal Testo As String) As String
        Dim Esito As String = "0"
        'Try
        '    If par.DeCriptaMolto(Validazione) <> Format(Now, "yyyyMMddHH") Then
        '        Esito = "0"
        '        Return Esito
        '        Exit Function
        '    End If
        'Catch ex As Exception
        '    Esito = "0"
        '    Return Esito
        'End Try
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "INSERT INTO AU_LIGHT_CONCESSIONI VALUES ('" & Testo & "')"
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Esito = "1"
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Esito = "0"
        End Try
        Return Esito
    End Function

End Class