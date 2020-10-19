Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Xml

Partial Class ANAUT_ScegliXML
    Inherits PageSetIdMode
    Public XMLError As String

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

    Public Sub LoadFile(ByVal FileImport As String)

        'legge il file xml
        txtXsd.Text = ""
        txtxml.Text = ""
        Dim MyfileXml As New System.IO.StreamReader(FileImport, True)
        txtxml.Text = MyfileXml.ReadToEnd
        MyfileXml.Close()

        'legge il file xsd
        Dim MyfileXsd As New System.IO.StreamReader(Server.MapPath("Dichiarazioni.xsd"), True)
        txtXsd.Text = MyfileXsd.ReadToEnd
        MyfileXsd.Close()

    End Sub

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
                    If Len(FileUpload1.FileName) < 20 Or Len(FileUpload1.FileName) > 22 Then
                        sErrore = sErrore & "<p><b><font face='Arial' size='2'>Errore: Lunghezza del file non valida!</b></p>"
                        errore = True
                    End If
                    If Mid(FileUpload1.FileName, 2, 1) <> "_" Then
                        If Mid(FileUpload1.FileName, 3, 1) <> "_" Then
                            If Mid(FileUpload1.FileName, 4, 1) <> "_" Then
                                sErrore = sErrore & "<p><b><font face='Arial' size='2'>Errore: Nome del file non valido!</b></p>"
                                errore = True
                            End If
                        End If
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
                            Dim mdbStream As FileStream = File.Create(Mid(Server.MapPath("IMPORT\" & FileUpload1.FileName), 1, Len(Server.MapPath("IMPORT\" & FileUpload1.FileName)) - 3) & "xml")
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
                                    myname = Dir(Server.MapPath("IMPORT\") & Session.Item("ID_CAF") & "_*.xml")  ' Recupera la prima voce.
                                    Do While myname <> ""   ' Avvia il ciclo.
                                        'MsgBox(myname)
                                        If myname <> Mid(FileUpload1.FileName, 1, Len(FileUpload1.FileName) - 3) & "xml" Then
                                            File.Delete(Server.MapPath("IMPORT\" & myname))
                                        End If
                                        myname = Dir()   ' Legge la voce successiva.
                                    Loop

                                    Response.Write("<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><b><font face='Arial' size='2'>Upload del file completato con successo!<p></b></p>")
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

    Private Function LeggiXML(ByVal miofile As String) As String
        Dim Tot_Dichiarazioni As Long
        Dim j As Long
        Dim reader As XmlTextReader = New XmlTextReader(miofile)

        j = 0
        LeggiXML = ""
        Do While (reader.Read())
            If reader.Name = "Dichiarazione" And reader.NodeType <> XmlNodeType.EndElement Then
                j = j + 1
            End If
            Select Case reader.NodeType
                Case XmlNodeType.Element 'Display beginning of element.
                    'Console.Write("<" + reader.Name)
                    If reader.HasAttributes Then 'If attributes exist
                        While reader.MoveToNextAttribute()
                            'Display attribute name and value.
                            If reader.Name = "IdentificatoreRichiesta" Then
                                If reader.Value <> Mid(miofile, Len(miofile) - 20, 17) Then
                                    reader.Close()
                                    LeggiXML = "<p><b><font face='Arial' size='2'>Errore: Attributo IdentificatoreRichiesta non corrisponde al nome del file!</b></p>"
                                    'Exit Function
                                End If
                            End If
                            If reader.Name = "NumeroDichiarazioni" Then
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
            LeggiXML = "<p><b><font face='Arial' size='2'>Errore: Attributo NumeroDichiarazioni (" & Tot_Dichiarazioni & ") non corrisponde con il numero delle dichiarazioni (" & j & ") contenute nel file</b></p>"

        End If
    End Function


    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
