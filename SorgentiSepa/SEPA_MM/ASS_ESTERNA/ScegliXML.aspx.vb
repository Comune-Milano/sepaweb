Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Xml
Imports System.IO

Partial Class ASS_ESTERNA_SscegliXML
    Inherits PageSetIdMode
    Public XMLError As String
    Dim par As New CM.Global

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Private Function LeggiXML(ByVal miofile As String) As String
        Dim Tot_Dichiarazioni As Long
        Dim j As Long
        Dim reader As XmlTextReader = New XmlTextReader(miofile)

        j = 0
        LeggiXML = ""
        Do While (reader.Read())
            If reader.Name = "Abbinamento" And reader.NodeType <> XmlNodeType.EndElement Then
                j = j + 1
            End If
            Select Case reader.NodeType
                Case XmlNodeType.Element 'Display beginning of element.
                    'Console.Write("<" + reader.Name)
                    If reader.HasAttributes Then 'If attributes exist
                        While reader.MoveToNextAttribute()
                            'Display attribute name and value.
                            If reader.Name = "IdentificatoreRichiesta" Then
                                If reader.Value <> Mid(miofile, Len(miofile) - 27, 24) Then
                                    reader.Close()
                                    LeggiXML = "<p><b><font face='Arial' size='2'>Errore: Attributo IdentificatoreRichiesta non corrisponde al nome del file!</b></p>"
                                    'Exit Function
                                End If
                            End If
                            If reader.Name = "NumeroAbbinamenti" Then
                                Tot_Dichiarazioni = reader.Value
                            End If
                            'Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                        End While
                    End If
                    'Console.WriteLine(">")
                Case XmlNodeType.Text 'Display the text in each element.
                    'Console.WriteLine(reader.Value)
                Case XmlNodeType.EndElement 'Display end of element.
                    'Console.Write("</" + reader.Name)
                    'Console.WriteLine(">")
            End Select
        Loop
        'Console.ReadLine()
        If Tot_Dichiarazioni <> j Then
            reader.Close()
            LeggiXML = "<p><b><font face='Arial' size='2'>Errore: Attributo NumeroAbbinamenti (" & Tot_Dichiarazioni & ") non corrisponde con il numero degli abbinamenti (" & j & ") contenute nel file</b></p>"

        End If
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim errore As Boolean
        Dim sErrore As String = ""
        Dim m As Boolean
        Dim myname As String

        Try
            If GiaFatto = 0 Then
                GiaFatto = 1
                If FileUpload1.HasFile = True Then
                    TextBox1.Visible = False
                    errore = False
                    If Len(FileUpload1.FileName) <> 28 Then
                        sErrore = sErrore & "<p><b><font face='Arial' size='2'>Errore: Lunghezza del file non valida!</b></p>"
                        errore = True
                    End If
                    If Mid(FileUpload1.FileName, 1, 10) <> "AlerInvio_" Then
                        sErrore = sErrore & "<p><b><font face='Arial' size='2'>Errore: Nome del file non valido!</b></p>"
                        errore = True
                    End If
                    If UCase(Mid(FileUpload1.FileName, Len(FileUpload1.FileName) - 2, 3)) <> "ZIP" Then
                        sErrore = sErrore & "<p><b><font face='Arial' size='2'>Errore: Tipo file non valido!</b></p>"
                        errore = True
                    End If

                    If File.Exists(Server.MapPath("IMPORT\" & FileUpload1.FileName)) = True Then
                        sErrore = sErrore & "<p><b><font face='Arial' size='2'>Errore: Il file che si tenta di inviare è gia presente sul server!</b></p>"
                        errore = True
                    End If
                    If errore = False Then
                        FileUpload1.SaveAs(Server.MapPath("IMPORT\" & FileUpload1.FileName))
                        If LCase(FileUpload1.PostedFile.ContentType) = "application/x-zip-compressed" Then

                            Dim ZipStream As New ZipInputStream(File.OpenRead(Server.MapPath("IMPORT\" & FileUpload1.FileName)))
                            Dim tmpEntry As ZipEntry = ZipStream.GetNextEntry()
                            Dim mdbStream As IO.FileStream = File.Create(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml")
                            Dim data(2048) As Byte
                            Dim size As Integer = 2048
                            size = ZipStream.Read(data, 0, data.Length)
                            While size > 0
                                mdbStream.Write(data, 0, size)
                                size = ZipStream.Read(data, 0, data.Length)
                            End While
                            mdbStream.Close()
                            ZipStream.Close()

                            LoadFile(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml")
                            m = LoadXMLData(txtxml.Text, txtXsd.Text)
                            If XMLError = "" Then
                                sErrore = LeggiXML(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml")
                                If sErrore = "" Then
                                    myname = Dir(Server.MapPath("IMPORT\") & "AlerInvio*.xml")  ' Recupera la prima voce.
                                    Do While myname <> ""   ' Avvia il ciclo.
                                        'MsgBox(myname)
                                        If myname <> Mid(FileUpload1.FileName, 1, Len(FileUpload1.FileName) - 3) & "xml" Then
                                            File.Delete(Server.MapPath("IMPORT\" & myname))
                                        End If
                                        myname = Dir()   ' Legge la voce successiva.
                                    Loop

                                    Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><b><font face='Arial' size='2'>Upload del file completato con successo!<p></b></p>")
                                    Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><b><font face='Arial' size='2'>Import dei dati e generazione report in corso...<p></b></p>")
                                    ImportDati(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml", FileUpload1.FileName)
                                Else
                                    Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><b>" & sErrore & "<font face='Arial' size='2'>Upload Fallito!</b></p>")
                                    File.Delete(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml")
                                    File.Delete(Server.MapPath("IMPORT\" & FileUpload1.FileName))
                                End If
                            Else
                                File.Delete(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml")
                                File.Delete(Server.MapPath("IMPORT\" & FileUpload1.FileName))
                                Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><img src='../IMG/loading.gif' width='30' height='30'/>Caricamento descrizione errore in corso...<br><br><br><font face='Arial' size='1'>Documento xml sintatticamente errato!<p><b><font face='Arial' size='2'>Upload Fallito!</b></p>")
                                TextBox1.Visible = True
                                TextBox1.Text = XMLError

                            End If
                        Else
                            sErrore = sErrore & "<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><p><b><font face='Arial' size='2'>Errore: Tipo file non valido!</b></p>"
                            File.Delete(Server.MapPath(FileUpload1.PostedFile.FileName))
                        End If
                    Else
                        Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>" & sErrore & "<p><b><font face='Arial' size='3'>Upload Fallito!</b></p>")
                    End If
                Else
                    Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><p><b><font face='Arial' size='2'>Selezionare un file!</b></p>")
                End If
                'Response.Write("<script>parent.funzioni.aa.close();</script>")
            Else
                GiaFatto = 0
            End If
        Catch ex1 As HttpUnhandledException

            Beep()
        End Try
    End Sub

    Private Function ImportDati(ByVal sFile As String, ByVal sFileCorto As String)
        Dim reader As XmlTextReader = New XmlTextReader(sFile)
        Dim Stringasql As String = ""
        Dim IdDomandaSepa As Long
        Dim FileReport As String = ""
        Dim sIncGenerali As String = ""
        Dim NumElementi As String = ""
        Dim Virgola As String = ""
        Dim DaInserire As Boolean
        Dim InserireAlloggio As Boolean
        Dim IdAlloggio As Long
        Dim DataOperazione As String = ""
        Dim EsitoAbbinamento As String = ""
        Dim CodiceAlloggio As String = ""
        Dim TipoRifiuto As String = ""
        Dim DescrizioneRifiuto As String = ""


        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then

            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            sIncGenerali = "<table border='1' cellpadding='0' cellspacing='0' width='100%' bgcolor='#00FF00'>" & vbCrLf _
                        & "<tr>" & vbCrLf _
                        & "<td width='20%' bgcolor='#00FF00'><font face='Arial' size='2'><b>Chiave SEPA (IdDomandaSepa)</b></font></td>" & vbCrLf _
                        & "<td width='80%'><b><font size='2' face='Arial'>Descrizione</font></b></td>" & vbCrLf _
                        & "</tr>"

            Do While (reader.Read())
                Select Case reader.NodeType
                    Case XmlNodeType.Element 'Display beginning of element.
                        If reader.Name = "ListaAbbinamenti" Then
                            While reader.MoveToNextAttribute()
                                If reader.Name = "NumeroAbbinamenti" Then
                                    NumElementi = reader.Value
                                End If
                            End While
                        End If
                        If reader.Name = "Abbinamento" Then

                            Stringasql = ""
                            DaInserire = True
                            InserireAlloggio = False
                            DescrizioneRifiuto = ""
                            CodiceAlloggio = ""

                            While reader.MoveToNextAttribute()
                                If reader.Name = "IdDomandaSepa" Then
                                    IdDomandaSepa = CLng(reader.Value)
                                End If

                                If reader.Name = "DataOperazione" Then
                                    DataOperazione = reader.Value
                                End If

                                If reader.Name = "TipoRifiuto" Then
                                    TipoRifiuto = reader.Value
                                End If

                                If reader.Name = "EsitoAbbinamento" Then
                                    EsitoAbbinamento = reader.Value
                                End If

                                If reader.Name = "DescrizioneRifiuto" Then
                                    DescrizioneRifiuto = par.PulisciStrSql(reader.Value)
                                End If


                                If reader.Name = "Comune" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "COMUNE='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "Ascensore" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "ASCENSORE='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "TipoAlloggio" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "TIPO_ALLOGGIO=" & par.PulisciStrSql(reader.Value)
                                End If

                                If reader.Name = "NumeroAlloggio" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "NUM_ALLOGGIO='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "NumeroLocali" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "NUM_LOCALI='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "Scala" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "SCALA='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "Civico" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "NUM_CIVICO='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "TipoIndirizzo" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "TIPO_INDIRIZZO=" & par.PulisciStrSql(reader.Value)
                                End If

                                If reader.Name = "CodiceAlloggio" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "COD_ALLOGGIO='" & par.PulisciStrSql(reader.Value) & "'"
                                    CodiceAlloggio = reader.Value
                                    par.cmd.CommandText = "select * from ALLOGGI where COD_ALLOGGIO='" & par.PulisciStrSql(reader.Value) & "'"
                                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReader2.Read Then
                                        If par.IfNull(myReader2("STATO"), "0") <> "5" Then
                                            sIncGenerali = sIncGenerali & vbCrLf _
                                                        & "<tr>" & vbCrLf _
                                                        & "<td width='20%'  bgcolor='#FFFFFF'><font face='Arial' size='2'><b>" & IdDomandaSepa & "</b></font></td>" & vbCrLf _
                                                        & "<td width='80%'  bgcolor='#FFFFFF'><font size='2' face='Arial'>L'alloggio codice (" & reader.Value & ") risulta essere già prenotato o assegnato ad altro utente! NON IMPORTATA!</font></td>" & vbCrLf _
                                                        & "</tr>"
                                            DaInserire = False
                                        Else
                                            InserireAlloggio = False
                                        End If
                                        IdAlloggio = par.IfNull(myReader2("id"), -1)
                                    Else
                                        IdAlloggio = -1
                                        InserireAlloggio = True
                                    End If
                                    myReader2.Close()
                                End If

                                If reader.Name = "Superficie" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "SUP=" & par.VirgoleInPunti(reader.Value)
                                End If

                                If reader.Name = "Indirizzo" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "INDIRIZZO='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "Zona" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "ZONA='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                If reader.Name = "Piano" Then
                                    If Stringasql <> "" Then
                                        Virgola = ","
                                    Else
                                        Virgola = ""
                                    End If
                                    Stringasql = Stringasql & Virgola & "PIANO='" & par.PulisciStrSql(reader.Value) & "'"
                                End If

                                'Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                                'Response.Write(reader.Name & ":" & reader.Value)
                            End While



                            par.cmd.CommandText = "select FL_ASS_ESTERNA from DOMANDE_BANDO where ID=" & IdDomandaSepa
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                If par.IfNull(myReader1("FL_ASS_ESTERNA"), "0") = "0" Then
                                    sIncGenerali = sIncGenerali & vbCrLf _
                                                & "<tr>" & vbCrLf _
                                                & "<td width='20%'  bgcolor='#FFFFFF'><font face='Arial' size='2'><b>" & IdDomandaSepa & "</b></font></td>" & vbCrLf _
                                                & "<td width='80%'  bgcolor='#FFFFFF'><font size='2' face='Arial'>Questa domanda non risulta essere in assegnazione esterna. NON IMPORTATA!</font></td>" & vbCrLf _
                                                & "</tr>"
                                    DaInserire = False
                                End If
                            Else
                                sIncGenerali = sIncGenerali & vbCrLf _
                                            & "<tr>" & vbCrLf _
                                            & "<td width='20%'  bgcolor='#FFFFFF'><font face='Arial' size='2'><b>" & IdDomandaSepa & "</b></font></td>" & vbCrLf _
                                            & "<td width='80%'  bgcolor='#FFFFFF'><font size='2' face='Arial'>Questa domanda non è presente nell'archivio comunale. NON IMPORTATA!</font></td>" & vbCrLf _
                                            & "</tr>"
                                DaInserire = False
                            End If
                            myReader1.Close()

                            If EsitoAbbinamento = "0" And DaInserire = True Then
                                InserireAlloggio = False
                                DaInserire = False
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_ASS_ESTERNA='0' WHERE ID=" & IdDomandaSepa
                                par.cmd.ExecuteNonQuery()
                                sIncGenerali = sIncGenerali & vbCrLf _
                                            & "<tr>" & vbCrLf _
                                            & "<td width='20%'  bgcolor='#FFFFFF'><font face='Arial' size='2'><b>" & IdDomandaSepa & "</b></font></td>" & vbCrLf _
                                            & "<td width='80%'  bgcolor='#FFFFFF'><font size='2' face='Arial'>DOMANDA IMPORTATA SENZA MOVIMENTI.</font></td>" & vbCrLf _
                                            & "</tr>"
                                DaInserire = False

                            End If


                            If DaInserire = True Then
                                If InserireAlloggio = True Then
                                    par.cmd.CommandText = "INSERT INTO ALLOGGI (ID,PROPRIETA) VALUES (SEQ_ALLOGGI.NEXTVAL,1)"
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "SELECT SEQ_ALLOGGI.CURRVAL FROM DUAL"
                                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReader3.Read Then
                                        IdAlloggio = par.IfNull(myReader3(0), -1)
                                    End If
                                    myReader3.Close()
                                End If

                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_INVITO='1',FL_ASS_ESTERNA='0' WHERE ID=" & IdDomandaSepa
                                par.cmd.ExecuteNonQuery()

                                Select Case EsitoAbbinamento
                                    Case "1"
                                        par.cmd.CommandText = "update alloggi set " & Stringasql & ",stato=8,assegnato='1',prenotato='1',id_pratica=" & IdDomandaSepa & ",data_prenotato='" & DataOperazione & "' where id=" & IdAlloggio
                                        par.cmd.ExecuteNonQuery()
                                    Case "2"
                                        par.cmd.CommandText = "update alloggi set " & Stringasql & ",stato=5,assegnato='0',prenotato='0',id_pratica=NULL,data_prenotato='' where id=" & IdAlloggio
                                        par.cmd.ExecuteNonQuery()
                                End Select

                                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & DataOperazione & "',6,5," & IdAlloggio & ",-1,'Inserimento automatico da assegnazione esterna')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & DataOperazione & "'"
                                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader5.HasRows = False Then
                                    par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & DataOperazione & "',0,0,0,0,0,0)"
                                    par.cmd.ExecuteNonQuery()
                                End If
                                myReader5.Close()

                                par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET DISPONIBILI=DISPONIBILI+1 WHERE DATA='" & DataOperazione & "'"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA=1 WHERE ID=" & IdDomandaSepa
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & IdDomandaSepa
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & DataOperazione & "',2,7," & IdAlloggio & "," & IdDomandaSepa & ",'Prenotazione automatica da assegnazione esterna')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & DataOperazione & "'"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) VALUES (" & IdDomandaSepa & "," & Session.Item("ID_OPERATORE") & ",'" & DataOperazione & "000000','9','F10','PROPOSTA DA ENTE ESTERNO','I')"
                                par.cmd.ExecuteNonQuery()


                                Select Case EsitoAbbinamento
                                    Case "1"
                                        par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,ESITO,DATA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & IdDomandaSepa & "," & IdAlloggio & ",'" & DataOperazione & "',1,'1','" & DataOperazione & "')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & DataOperazione & "',1,8," & IdAlloggio & "," & IdDomandaSepa & ",'Accettazione da ente esterno')"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET CODICE_ALLOGGIO='" & CodiceAlloggio & "',NUM_ALLOGGIO='" & CodiceAlloggio & "' WHERE ID=" & IdDomandaSepa
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,ASSEGNATI=ASSEGNATI+1 WHERE DATA='" & DataOperazione & "'"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) VALUES (" & IdDomandaSepa & "," & Session.Item("ID_OPERATORE") & ",'" & DataOperazione & "000000','9','F13','ASSEGNAZIONE DA ENTE ESTERNO','I')"
                                        par.cmd.ExecuteNonQuery()
                                        sIncGenerali = sIncGenerali & vbCrLf _
                                                & "<tr>" & vbCrLf _
                                                & "<td width='20%'  bgcolor='#FFFFFF'><font face='Arial' size='2'><b>" & IdDomandaSepa & "</b></font></td>" & vbCrLf _
                                                & "<td width='80%'  bgcolor='#FFFFFF'><font size='2' face='Arial'>DOMANDA IMPORTATA CON ASSEGNAZIONE ALLOGGIO COD. " & CodiceAlloggio & "</font></td>" & vbCrLf _
                                                & "</tr>"

                                    Case "2"
                                        Select Case TipoRifiuto
                                            Case "1"
                                                par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,ESITO,MOTIVAZIONE,DATA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & IdDomandaSepa & "," & IdAlloggio & ",'" & DataOperazione & "',1,'0','STATO MANUTENTIVO - " & DescrizioneRifiuto & "','" & DataOperazione & "')"
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & DataOperazione & "',0,5," & IdAlloggio & "," & IdDomandaSepa & ",'STATO MANUTENTIVO - " & DescrizioneRifiuto & "')"
                                                par.cmd.ExecuteNonQuery()

                                            Case "2"
                                                par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,ESITO,MOTIVAZIONE,DATA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & IdDomandaSepa & "," & IdAlloggio & ",'" & DataOperazione & "',1,'0','STATO DI ACCESSIBILITA - " & DescrizioneRifiuto & "','" & DataOperazione & "')"
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & DataOperazione & "',0,5," & IdAlloggio & "," & IdDomandaSepa & ",'STATODI ACCESSIBILITA - " & DescrizioneRifiuto & "')"
                                                par.cmd.ExecuteNonQuery()

                                            Case "3"
                                                par.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,ESITO,MOTIVAZIONE,DATA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & IdDomandaSepa & "," & IdAlloggio & ",'" & DataOperazione & "',1,'0','GRAVI MOTIVAZIONI DOCUMENTATE - " & DescrizioneRifiuto & "','" & DataOperazione & "')"
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & DataOperazione & "',0,5," & IdAlloggio & "," & IdDomandaSepa & ",'GRAVI MOTIVAZIONI DOCUMENTATE - " & DescrizioneRifiuto & "')"
                                                par.cmd.ExecuteNonQuery()
                                        End Select


                                        par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_INVITO='0',FL_PROPOSTA='0' WHERE ID=" & IdDomandaSepa
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,DISPONIBILI=DISPONIBILI+1 WHERE DATA='" & DataOperazione & "'"
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) VALUES (" & IdDomandaSepa & "," & Session.Item("ID_OPERATORE") & ",'" & DataOperazione & "000000','9','F16','RIFIUTO DA ENTE ESTERNO','I')"
                                        par.cmd.ExecuteNonQuery()

                                        sIncGenerali = sIncGenerali & vbCrLf _
                                                & "<tr>" & vbCrLf _
                                                & "<td width='20%'  bgcolor='#FFFFFF'><font face='Arial' size='2'><b>" & IdDomandaSepa & "</b></font></td>" & vbCrLf _
                                                & "<td width='80%'  bgcolor='#FFFFFF'><font size='2' face='Arial'>DOMANDA IMPORTATA CON RIFIUTO ALLOGGIO COD. " & CodiceAlloggio & "</font></td>" & vbCrLf _
                                                & "</tr>"


                                End Select



                            End If
                        End If


                    Case XmlNodeType.Text 'Display the text in each element.

                    Case XmlNodeType.EndElement 'Display end of element.

                End Select
            Loop

            FileReport = "<html xmlns='http://www.w3.org/1999/xhtml'>" & vbCrLf _
                        & "<head>" & vbCrLf _
                        & "<meta http-equiv='Content-Language' content='it'>" & vbCrLf _
                        & "<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>" & vbCrLf _
                        & "<title>Report Importazione XML</title>" & vbCrLf _
                        & "</head>" & vbCrLf _
                        & "<body>" & vbCrLf _
                        & "<p align='center'><b><font face='Arial' size='3'>Riepilogo Report</font></b></p>" & vbCrLf _
                        & "<p align='left'><b><font face='Arial' size='3'>Data: " & Format(Now, "dd/MM/yyyy") & "</font></b></p>" & vbCrLf _
                        & "<p align='left'><b><font face='Arial' size='3'>Ente: MM S.P.A.</font></b></p>" & vbCrLf _
                        & "<p align='left'><b><font face='Arial' size='3'>N. Elementi: " & NumElementi & "</font></b></p>" & vbCrLf _
                        & "<p align='left'><b><font face='Arial' size='3'>File XML Esaminato: " & sFileCorto & "</font></b></p>" & vbCrLf _
                        & "<p align='left'><b><font face='Arial' size='3'>Elenco Incongruenze Abbinamenti</font></b></p>" & vbCrLf

            FileReport = FileReport & sIncGenerali & "</table></body></html>"
            File.WriteAllText(Mid(Server.MapPath("REPORT\" & sFileCorto), 1, Len(Server.MapPath("REPORT\" & sFileCorto)) - 3) & "HTML", FileReport, Encoding.UTF8)
            par.cmd.CommandText = "INSERT INTO DEC_IMPORT (ID,DATA_IMPORT,NOME_FILE,N_DICHIARAZIONI,ID_CAF) VALUES (SEQ_UTENZA_IMPORT.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "','" & sFileCorto & "'," & NumElementi & "," & Session.Item("ID_CAF") & ")"
            par.cmd.ExecuteNonQuery()
            reader.Close()
            File.Delete(sFile)

            par.OracleConn.Close()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Function

    Public Property GiaFatto() As Integer
        Get
            If Not (ViewState("par_GiaFatto") Is Nothing) Then
                Return CInt(ViewState("par_GiaFatto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_GiaFatto") = value
        End Set

    End Property

    Public Function LoadXMLData(ByVal XMLData As String, ByVal XSDSchema As String) As Boolean

        XMLError = ""
        'Crea il documento XML utilizzando il codice passato in XMLData
        Dim MyXMLDocument As New System.Xml.XmlDocument
        Dim MyXSDTextReader As New System.IO.StringReader(XSDSchema)
        'crea lo schema con l'handler all'evento di validazione che mi interessa
        Dim MyXMLSchema As System.Xml.Schema.XmlSchema = System.Xml.Schema.XmlSchema.Read(MyXSDTextReader, New System.Xml.Schema.ValidationEventHandler(AddressOf XMLEvent))

        Dim MyXMLTextReader As New System.IO.StringReader(XMLData)
        MyXMLDocument.Load(MyXMLTextReader)
        MyXMLDocument.Schemas.Add(MyXMLSchema)
        MyXMLDocument.Validate(New System.Xml.Schema.ValidationEventHandler(AddressOf XMLEvent))



    End Function

    Private Sub XMLEvent(ByVal sender As Object, ByVal e As System.Xml.Schema.ValidationEventArgs)
        XMLError += (e.Message & vbCrLf)
    End Sub

    Public Sub LoadFile(ByVal FileImport As String)

        'legge il file xml
        txtXsd.Text = ""
        txtxml.Text = ""
        Dim MyfileXml As New System.IO.StreamReader(FileImport, True)
        txtxml.Text = MyfileXml.ReadToEnd
        MyfileXml.Close()

        'legge il file xsd
        Dim MyfileXsd As New System.IO.StreamReader(Server.MapPath("AlerInvio.xsd"), True)
        txtXsd.Text = MyfileXsd.ReadToEnd
        MyfileXsd.Close()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            GiaFatto = 0
            'Button1.Attributes.Add("OnClick", "javascript:Attendi();")
        End If
    End Sub
End Class
