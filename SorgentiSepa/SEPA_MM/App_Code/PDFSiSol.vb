Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web.UI.DataVisualization.Charting
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.List
Imports iTextSharp.text.pdf.draw
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip

Public Class PDFSiSol
    Dim par As New CM.Global
    'Definizione delle Strutture
    ''' <summary>Struttura per la Gestione della Creazione del File PDF sulla risorsa</summary>
    Public Structure StrutturaNuovoFile
        Public NomeFileStruttura As String
        Public NomeFileOriginale As String
        Public PercorsoFileStruttura As String
        Public PercorsoFileStrutturaDownload As String
        Public SuccessoPercorsoFileStrutturaDownload As Boolean
    End Structure
    'Definizione dei Metodi di Overload
    ''' <summary>Enum per la Gestione delle Dimensioni della Pagina PDF</summary>
    Public Enum DimensioniPagina
        A0 = 1
        A1 = 2
        A2 = 3
        A3 = 4
        A4 = 5
        A5 = 6
        A6 = 7
        A7 = 8
        A8 = 9
        A9 = 10
        A10 = 11
        ARCH_A = 12
        ARCH_B = 13
        ARCH_C = 14
        ARCH_D = 15
        ARCH_E = 16
        B0 = 17
        B1 = 18
        B2 = 19
        B3 = 20
        B4 = 21
        B5 = 22
        B6 = 23
        B7 = 24
        B8 = 25
        B9 = 26
        B10 = 27
        CROWN_OCTAVO = 28
        CROWN_QUARTO = 29
        DEMY_OCTAVO = 30
        DEMY_QUARTO = 31
        EXECUTIVE = 32
        FLSA = 33
        FLSE = 34
        HALFLETTER = 35
        ID_1 = 36
        ID_2 = 37
        ID_3 = 38
        LARGE_CROWN_OCTAVO = 39
        LARGE_CROWN_QUARTO = 40
        LEDGER = 41
        LEGAL = 42
        LETTER = 43
        NOTE = 44
        PENGUIN_LARGE_PAPERBACK = 45
        PENGUIN_SMALL_PAPERBACK = 46
        POSTCARD = 47
        ROYAL_OCTAVO = 48
        ROYAL_QUARTO = 49
        SMALL_PAPERBACK = 50
        TABLOID = 51
        _11X17 = 52
        UserDef = 53
    End Enum
    ''' <summary>Enum per la Gestione dei Margini della Pagina</summary>
    Public Enum TipoMargini
        Predefiniti = 0
        Personalizzati = 1
    End Enum
    ''' <summary>Enum per la Gestione dell'Orientamento della Pagina</summary>
    Public Enum OrientamentoPagina
        Verticale = 0
        Orizzontale = 1
    End Enum
    ''' <summary>Enum per la Gestione della Famiglia del Font</summary>
    Public Enum FontPDF
        Courier = 0
        Helvetica = 1
        Times = 2
        Arial = 3
    End Enum
    ''' <summary>Enum per la Gestione dello Stile del Font</summary>
    Public Enum FontStylePDF
        Bold = 0
        BoldItalic = 1
        Italic = 2
        Normale = 3
        Barrato = 4
        Sottolineato = 5
    End Enum
    ''' <summary>Enum per la Gestione del Colore del Font Testo</summary>
    Public Enum FontColorPDF
        Nero = 0
        Blu = 1
        Ciano = 2
        GrigioScuro = 3
        Grigio = 4
        Verde = 5
        GrigioChiaro = 6
        Magenta = 7
        Arancione = 8
        Rosa = 9
        Rosso = 10
        Bianco = 11
        Giallo = 12
    End Enum
    ''' <summary>Enum per la Gestione dell'Allignamento degli Elementi</summary>
    Public Enum AllignamentoElementi
        Baseline = 0
        Bottom = 1
        Center = 2
        Giustificato = 3
        Left = 4
        Middle = 5
        Right = 6
        Top = 7
    End Enum
    ''' <summary>Enum per la gestione dell'allignamento del Testo</summary>
    Public Enum AllignamentoTesto
        Sinistra = 0
        Centrato = 1
        Giustificato = 2
        Destra = 3
    End Enum
    ''' <summary>Enum per la Gestione dei Valori Predefini nel file PDF</summary>
    Public Enum SetValori
        Predefiniti = 0 'Tramite Funzione
        Personalizzati = 1
        PredefinitiAnchor = 2
        PredefinitiTableHeader = 3
    End Enum
    ''' <summary>Enum per la Gestione del colore del testo del Watermark</summary>
    Public Enum Colori
        Nero = 0
        Blu = 1
        Ciano = 2
        GrigioScruro = 3
        Verde = 4
        GrigioChiaro = 5
        Magenta = 6
        Arancione = 7
        Rosa = 8
        Rosso = 9
        Bianco = 10
        Giallo = 11
    End Enum
    ''' <summary>Enum per la Gestione del Tipo di Return del File PDF</summary>
    Public Enum TipoReturnFilePdf
        Redirect = 0
        Download = 1
        Href = 2
    End Enum
    ''' <summary>Enum per la Gestione del Tipo della List semplice</summary>
    Public Enum TipoList
        Numerica = 0
        NonNumerica = 1
    End Enum
    ''' <summary>Enum per la Gestione della List di Tipo Greco/Romana</summary>
    Public Enum TipoListGrecoRomana
        ListaRomana = 0
        ListaGreca = 1
    End Enum
    ''' <summary>Enum per la Gestione dei Puntatori Grafici delle List</summary>
    Public Enum SimboloListSimbolo
        Forbici = 36
        Aereo = 40
        Lettera = 41
        ManoIndice = 42
        Matita = 46
        Stilografica = 50
        Confirm = 52
        Cancel = 54
        Stella = 72
        Vortice = 85
        Quadrifoglio = 93
        Sole = 98
        CerchioPieno = 108
        CerchioVuoto = 109
        QuadratoPiero = 110
        QuadratoVuto = 111
        Trinagolo = 115
        Quadri = 169
        Freccia = 213
        Freccia2 = 226
    End Enum
    'Definizione delle Property per la gestione dei Valori Predefiniti
    Dim _Famiglia As FontPDF = FontPDF.Times
    Dim _Dimensione As Single = 12
    Dim _StyleFont As FontStylePDF = FontStylePDF.Normale
    Dim _ColorFont As FontColorPDF = FontColorPDF.Nero
    ''' <summary>Property per la definizione della Famiglia del Font Predefinita</summary>
    ''' <value>Valore Famiglia Font</value>
    Public Property PropertyFamiglia() As FontPDF
        Get
            Return _Famiglia
        End Get
        Set(ByVal value As FontPDF)
            _Famiglia = value
        End Set
    End Property
    ''' <summary>Property per la definizione della Dimensione del Font Predefinito</summary>
    ''' <value>Valore Dimensione Font</value>
    Public Property PropertyDimensione() As Single
        Get
            Return _Dimensione
        End Get
        Set(ByVal value As Single)
            _Dimensione = value
        End Set
    End Property
    ''' <summary>Property per la definizione dello Stile del Font Predefinito</summary>
    ''' <value>Valore Stile Font</value>
    Public Property PropertyStyleFont() As FontStylePDF
        Get
            Return _StyleFont
        End Get
        Set(ByVal value As FontStylePDF)
            _StyleFont = value
        End Set
    End Property
    ''' <summary>Property per la definizione del Colore del Font Predefinito</summary>
    ''' <value>Valore Colore Font</value>
    Public Property PropertyColorFont() As FontColorPDF
        Get
            Return _ColorFont
        End Get
        Set(ByVal value As FontColorPDF)
            _ColorFont = value
        End Set
    End Property
    'Definizione delle Funzioni per l'Inizializzazione dei Componenti di ItextSharp
    ''' <summary>Procedura per l' inizializzazione del Document</summary>
    ''' <param name="Pagina">Definizione della Pagina settata che deve formare il Document.</param>
    Public Function IstanziaDocument(ByVal Pagina As Rectangle) As Document
        IstanziaDocument = New Document(Pagina)
        Return IstanziaDocument
    End Function
    ''' <summary>Procedura per l' inizializzazione del PdfReader</summary>
    ''' <param name="PercorsoNomeFile">Definizione del Percorso e del Nome del File per la Creazione del Reader.</param>
    Public Function IstanziaReader(ByVal PercorsoNomeFile As String) As PdfReader
        IstanziaReader = New PdfReader(PercorsoNomeFile)
        Return IstanziaReader
    End Function
    ''' <summary>Procedura per l' inizializzazione del PdfReader</summary>
    ''' <param name="Document">Definizione del Document per la Creazione del Nuovo Writer.</param>
    ''' <param name="FileStream">Definizione del FileStream per la Creazione del Nuovo Writer.</param>
    Public Function IstanziaWriter(ByVal Document As Document, ByVal FileStream As FileStream) As PdfWriter
        IstanziaWriter = PdfWriter.GetInstance(Document, FileStream)
        Return IstanziaWriter
    End Function
    ''' <summary>Procedura per l' inizializzazione della Pagina PDF</summary>
    ''' <param name="Writer">Definizione del Writer per l'instazia del Content.</param>
    Public Function IstanziaPdfContentByte(ByVal Writer As PdfWriter) As PdfContentByte
        IstanziaPdfContentByte = Writer.DirectContent
        Return IstanziaPdfContentByte
    End Function
    ''' <summary>Procedura per l' inizializzazione della Pagina PDF</summary>
    ''' <param name="ObjLettura">Definizione dell'Oggetto da cui prelevare la Pagina.</param>
    Public Function IstanziaPagePdf(ByVal ObjLettura As Object) As Rectangle
        IstanziaPagePdf = ObjLettura.GetPageSizeWithRotation(1)
        Return IstanziaPagePdf
    End Function
    ''' <summary>Procedura per l' inizializzazione del Chuck (è la più piccola parte di testo significativa che può essere aggiunta all’interno di un Document)</summary>
    Public Function IstanziaChuck() As Chunk
        IstanziaChuck = New Chunk
        Return IstanziaChuck
    End Function
    ''' <summary>Procedura per l' inizializzazione del Phrase (è un insieme di Phrase e/o Chunk opportunamente disposti e formattati)</summary>
    Public Function IstanziaPhrase() As Phrase
        IstanziaPhrase = New Phrase
        Return IstanziaPhrase
    End Function
    ''' <summary>Procedura per l' inizializzazione del Paragraph (è un insieme di Phrase e/o Chunk opportunamente disposti e formattati)</summary>
    Public Function IstanziaParagraph() As Paragraph
        IstanziaParagraph = New Paragraph
        Return IstanziaParagraph
    End Function
    ''' <summary>Procedura per l' inizializzazione dell'Anchor (Collegamento Ipertestuale)</summary>
    Public Function IstanziaAnchor() As Anchor
        IstanziaAnchor = New Anchor
        Return IstanziaAnchor
    End Function
    ''' <summary>Procedura per l'inizializzazione del File PDF</summary>
    Public Function IstanziaFile() As StrutturaNuovoFile
        IstanziaFile = New StrutturaNuovoFile
        Return IstanziaFile
    End Function
    'Definizione delle Funzioni per settare le opzioni predefinite del File
    ''' <summary>Procedura per impostare i valori predefiniti per la formazione del file PDF</summary>
    ''' <param name="Famiglia">Definisce la Famiglia del Font dell'intero file PDF.</param>
    ''' <param name="Dimensione">Definisce la Dimensione del Font dell'intero file PDF.</param>
    ''' <param name="Stile">Definisce lo Stile del Font dell'intero file PDF.</param>
    ''' <param name="Colore">Definisce il Colore del Font dell'intero file PDF.</param>
    Public Function SettaValoriPredefini(Optional ByVal Famiglia As FontPDF = FontPDF.Times, Optional ByVal Dimensione As Single = 12, Optional ByVal Stile As FontStylePDF = FontStylePDF.Normale, Optional ByVal Colore As FontColorPDF = FontColorPDF.Nero) As String
        PropertyFamiglia = Famiglia
        PropertyDimensione = Dimensione
        PropertyStyleFont = Stile
        PropertyColorFont = Colore
        SettaValoriPredefini = ""
    End Function
    'Definizione delle Funzioni per la Creazione del File PDF
    ''' <summary>Settaggio delle Dimensioni Personalizzate della pagina PDF</summary>
    ''' <param name="Dimensioni">Definizione delle dimensioni della Pagina che si sta creando.</param>
    ''' <param name="Orientamento">Definizione dell'Orientamento della Pagina.</param>
    Private Function SettaDimensionePagina(ByVal Dimensioni As DimensioniPagina, ByVal Orientamento As OrientamentoPagina) As Rectangle
        Dim Pagina As New Rectangle(PageSize.A4)
        Select Case Dimensioni
            Case 1
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A0)
                Else
                    Pagina = New Rectangle(PageSize.A0.Rotate)
                End If
            Case 2
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A1)
                Else
                    Pagina = New Rectangle(PageSize.A1.Rotate)
                End If
            Case 3
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A2)
                Else
                    Pagina = New Rectangle(PageSize.A2.Rotate)
                End If
            Case 4
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A3)
                Else
                    Pagina = New Rectangle(PageSize.A3.Rotate)
                End If
            Case 5
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A4)
                Else
                    Pagina = New Rectangle(PageSize.A4.Rotate)
                End If
            Case 6
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A5)
                Else
                    Pagina = New Rectangle(PageSize.A5.Rotate)
                End If
            Case 7
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A6)
                Else
                    Pagina = New Rectangle(PageSize.A6.Rotate)
                End If
            Case 8
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A7)
                Else
                    Pagina = New Rectangle(PageSize.A7.Rotate)
                End If
            Case 9
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A8)
                Else
                    Pagina = New Rectangle(PageSize.A8.Rotate)
                End If
            Case 10
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A9)
                Else
                    Pagina = New Rectangle(PageSize.A9.Rotate)
                End If
            Case 11
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.A10)
                Else
                    Pagina = New Rectangle(PageSize.A10.Rotate)
                End If
            Case 12
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ARCH_A)
                Else
                    Pagina = New Rectangle(PageSize.ARCH_A.Rotate)
                End If
            Case 13
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ARCH_B)
                Else
                    Pagina = New Rectangle(PageSize.ARCH_B.Rotate)
                End If
            Case 14
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ARCH_C)
                Else
                    Pagina = New Rectangle(PageSize.ARCH_C.Rotate)
                End If
            Case 15
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ARCH_D)
                Else
                    Pagina = New Rectangle(PageSize.ARCH_D.Rotate)
                End If
            Case 16
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ARCH_E)
                Else
                    Pagina = New Rectangle(PageSize.ARCH_E.Rotate)
                End If
            Case 17
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B0)
                Else
                    Pagina = New Rectangle(PageSize.B0.Rotate)
                End If
            Case 18
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B1)
                Else
                    Pagina = New Rectangle(PageSize.B1.Rotate)
                End If
            Case 19
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B2)
                Else
                    Pagina = New Rectangle(PageSize.B2.Rotate)
                End If
            Case 20
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B3)
                Else
                    Pagina = New Rectangle(PageSize.B3.Rotate)
                End If
            Case 21
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B4)
                Else
                    Pagina = New Rectangle(PageSize.B4.Rotate)
                End If
            Case 22
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B5)
                Else
                    Pagina = New Rectangle(PageSize.B5.Rotate)
                End If
            Case 23
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B6)
                Else
                    Pagina = New Rectangle(PageSize.B6.Rotate)
                End If
            Case 24
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B7)
                Else
                    Pagina = New Rectangle(PageSize.B7.Rotate)
                End If
            Case 25
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B8)
                Else
                    Pagina = New Rectangle(PageSize.B8.Rotate)
                End If
            Case 26
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B9)
                Else
                    Pagina = New Rectangle(PageSize.B9.Rotate)
                End If
            Case 27
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.B10)
                Else
                    Pagina = New Rectangle(PageSize.B10.Rotate)
                End If
            Case 28
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.CROWN_OCTAVO)
                Else
                    Pagina = New Rectangle(PageSize.CROWN_OCTAVO.Rotate)
                End If
            Case 29
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.CROWN_QUARTO)
                Else
                    Pagina = New Rectangle(PageSize.CROWN_QUARTO.Rotate)
                End If
            Case 30
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.DEMY_OCTAVO)
                Else
                    Pagina = New Rectangle(PageSize.DEMY_OCTAVO.Rotate)
                End If
            Case 31
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.DEMY_QUARTO)
                Else
                    Pagina = New Rectangle(PageSize.DEMY_QUARTO.Rotate)
                End If
            Case 32
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.EXECUTIVE)
                Else
                    Pagina = New Rectangle(PageSize.EXECUTIVE.Rotate)
                End If
            Case 33
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.FLSA)
                Else
                    Pagina = New Rectangle(PageSize.FLSA.Rotate)
                End If
            Case 34
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.FLSE)
                Else
                    Pagina = New Rectangle(PageSize.FLSE.Rotate)
                End If
            Case 35
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.HALFLETTER)
                Else
                    Pagina = New Rectangle(PageSize.HALFLETTER.Rotate)
                End If
            Case 36
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ID_1)
                Else
                    Pagina = New Rectangle(PageSize.ID_1.Rotate)
                End If
            Case 37
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ID_2)
                Else
                    Pagina = New Rectangle(PageSize.ID_2.Rotate)
                End If
            Case 38
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ID_3)
                Else
                    Pagina = New Rectangle(PageSize.ID_3.Rotate)
                End If
            Case 39
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.LARGE_CROWN_OCTAVO)
                Else
                    Pagina = New Rectangle(PageSize.LARGE_CROWN_OCTAVO.Rotate)
                End If
            Case 40
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.LARGE_CROWN_QUARTO)
                Else
                    Pagina = New Rectangle(PageSize.LARGE_CROWN_QUARTO.Rotate)
                End If
            Case 41
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.LEDGER)
                Else
                    Pagina = New Rectangle(PageSize.LEDGER.Rotate)
                End If
            Case 42
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.LEGAL)
                Else
                    Pagina = New Rectangle(PageSize.LEGAL.Rotate)
                End If
            Case 43
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.LETTER)
                Else
                    Pagina = New Rectangle(PageSize.LETTER.Rotate)
                End If
            Case 44
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.NOTE)
                Else
                    Pagina = New Rectangle(PageSize.NOTE.Rotate)
                End If
            Case 45
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.PENGUIN_LARGE_PAPERBACK)
                Else
                    Pagina = New Rectangle(PageSize.PENGUIN_LARGE_PAPERBACK.Rotate)
                End If
            Case 46
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.PENGUIN_SMALL_PAPERBACK)
                Else
                    Pagina = New Rectangle(PageSize.PENGUIN_SMALL_PAPERBACK.Rotate)
                End If
            Case 47
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.POSTCARD)
                Else
                    Pagina = New Rectangle(PageSize.POSTCARD.Rotate)
                End If
            Case 48
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ROYAL_OCTAVO)
                Else
                    Pagina = New Rectangle(PageSize.ROYAL_OCTAVO.Rotate)
                End If
            Case 49
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.ROYAL_QUARTO)
                Else
                    Pagina = New Rectangle(PageSize.ROYAL_QUARTO.Rotate)
                End If
            Case 50
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.SMALL_PAPERBACK)
                Else
                    Pagina = New Rectangle(PageSize.SMALL_PAPERBACK.Rotate)
                End If
            Case 51
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize.TABLOID)
                Else
                    Pagina = New Rectangle(PageSize.TABLOID.Rotate)
                End If
            Case 52
                If Orientamento = 0 Then
                    Pagina = New Rectangle(PageSize._11X17)
                Else
                    Pagina = New Rectangle(PageSize._11X17.Rotate)
                End If
            Case Else

        End Select
        Return Pagina
    End Function
    ''' <summary>Creazione della Nuova Pagina per la Creazione del PDF</summary>
    ''' <param name="Dimensioni">Definizione delle Dimensioni per la Creazione della Pagina.</param>
    ''' <param name="Orientamento">Definizione dell'Orientamento della Pagina.</param>
    ''' <param name="DimensioniW">Definizione della Dimensione Orizzontale per la Creazione della pagina.</param>
    ''' <param name="DimensioniH">Definizione della Dimensione Verticale per la Creazione della pagina.</param>
    Public Function CreaPagina(ByVal Dimensioni As DimensioniPagina, ByVal Orientamento As OrientamentoPagina, Optional DimensioniW As Single = 0, Optional DimensioniH As Single = 0) As Rectangle
        If Dimensioni = 53 Then
            CreaPagina = New Rectangle(DimensioniW, DimensioniH)
        Else
            CreaPagina = SettaDimensionePagina(Dimensioni, Orientamento)
        End If
        Return CreaPagina
    End Function
    ''' <summary>Set Colore Background della Nuova Pagina per la Creazione del PDF</summary>
    ''' <param name="Pagina">Definizione dalla Pagina che si sta creando per la Creazione del PDF.</param>
    ''' <param name="Colore">Definizione del Colore in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    Public Function SetBackgroundColorPagina(ByVal Pagina As Rectangle, ByVal Colore As String) As Rectangle
        If Not String.IsNullOrEmpty(Colore) And Len(Colore) = 7 Then
            Pagina.BackgroundColor = New Color(Drawing.ColorTranslator.FromHtml(Colore))
        End If
        Return Pagina
    End Function
    ''' <summary>Set Margini a elemento</summary>
    ''' <param name="Obj">Definizione dall'oggetto a cui settare i margini.</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    Public Function SetBorder(ByVal Obj As Object, Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0) As Object
        If (Not String.IsNullOrEmpty(BorderTopColor) And Len(BorderTopColor) = 7) Or BorderTopWidth <> 0 Then
            If TypeOf Obj Is Rectangle Or TypeOf Obj Is HeaderFooter Or TypeOf Obj Is PdfPCell Or TypeOf Obj Is Cell Or TypeOf Obj Is Image Then
                Obj.BorderColorTop = New Color(Drawing.ColorTranslator.FromHtml(BorderTopColor))
                Obj.BorderWidthTop = BorderTopWidth
            End If
        End If
        If (Not String.IsNullOrEmpty(BorderLeftColor) And Len(BorderLeftColor) = 7) Or BorderLeftWidth <> 0 Then
            If TypeOf Obj Is Rectangle Or TypeOf Obj Is HeaderFooter Or TypeOf Obj Is PdfPCell Or TypeOf Obj Is Cell Or TypeOf Obj Is Image Then
                Obj.BorderColorLeft = New Color(Drawing.ColorTranslator.FromHtml(BorderLeftColor))
                Obj.BorderWidthLeft = BorderLeftWidth
            End If
        End If
        If (Not String.IsNullOrEmpty(BorderRightColor) And Len(BorderRightColor) = 7) Or BorderRightWidth <> 0 Then
            If TypeOf Obj Is Rectangle Or TypeOf Obj Is HeaderFooter Or TypeOf Obj Is PdfPCell Or TypeOf Obj Is Cell Or TypeOf Obj Is Image Then
                Obj.BorderColorRight = New Color(Drawing.ColorTranslator.FromHtml(BorderRightColor))
                Obj.BorderWidthRight = BorderRightWidth
            End If
        End If
        If (Not String.IsNullOrEmpty(BorderBottomColor) And Len(BorderBottomColor) = 7) Or BorderBottomWidth <> 0 Then
            If TypeOf Obj Is Rectangle Or TypeOf Obj Is HeaderFooter Or TypeOf Obj Is PdfPCell Or TypeOf Obj Is Cell Or TypeOf Obj Is Image Then
                Obj.BorderColorBottom = New Color(Drawing.ColorTranslator.FromHtml(BorderBottomColor))
                Obj.BorderWidthBottom = BorderBottomWidth
            End If
        End If
        Return Obj
    End Function
    ''' <summary>Set Colore Background della Nuova Pagina per la Creazione del PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta creando per la Creazione del PDF.</param>
    ''' <param name="Margini">Definizione del Tipo di Margine. Predefiniti: 20, 20, 20, 20; Personalizzati devono essere inseriti altrimenti: 0, 0, 0, 0</param>
    ''' <param name="Top">Definizione del Margine Top in Single.</param>
    ''' <param name="Left">Definizione del Margine Left in Single.</param>
    ''' <param name="Right">Definizione del Margine Right in Single.</param>
    ''' <param name="Bottom">Definizione del Margine Bottom in Single.</param>
    Public Function SetMargini(ByVal Document As Document, ByVal Margini As TipoMargini, Optional ByVal Top As Single = 0, Optional ByVal Left As Single = 0, Optional ByVal Right As Single = 0, Optional Bottom As Single = 0) As Document
        If Margini = 0 Then
            Document.SetMargins(20, 20, 20, 20)
        Else
            Document.SetMargins(Left, Right, Top, Bottom)
        End If
        Return Document
    End Function
    ''' <summary>Procedura per la Creazione del File PDF sulla risorsa</summary>
    ''' <param name="Document">Definizione del Documento che si sta creando per la Creazione del PDF.</param>
    ''' <param name="NomeFile">Definizione del Nome del File PDF che si sta creando.</param>
    ''' <param name="Percorso">Definizione del Percorso del File PDF che si sta creando.</param>
    ''' <param name="Autore">Definizione del MetaData Autore del File PDF che si sta creando.</param>
    ''' <param name="ParoleChiave">Definizione del MetaData KeyWords del File PDF che si sta creando.</param>
    ''' <param name="Titolo">Definizione del MetaData Titolo del File PDF che si sta creando.</param>
    ''' <param name="Sottotitolo">Definizione del MetaData SottoTitolo del File PDF che si sta creando.</param>
    Public Function CreaFile(ByVal Document As Document, ByVal NomeFile As String, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional ByVal Autore As String = "S&S Sistemi & Soluzioni S.r.l.", Optional ByVal ParoleChiave As String = "", Optional ByVal Titolo As String = "", Optional ByVal Sottotitolo As String = "", Optional ByVal DataOraNomeFile As Boolean = True) As StrutturaNuovoFile
        Dim FilePDF As New StrutturaNuovoFile
        FilePDF.SuccessoPercorsoFileStrutturaDownload = False
        If ContaCaratteriStringa("~", Percorso) > 0 Then
            FilePDF.PercorsoFileStrutturaDownload = Percorso.Replace("~\/", "")
            For i As Integer = 0 To 50
                If Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("" & FilePDF.PercorsoFileStrutturaDownload)) Then
                    If System.Web.HttpContext.Current.Server.MapPath(Percorso) = System.Web.HttpContext.Current.Server.MapPath("" & FilePDF.PercorsoFileStrutturaDownload) Then
                        FilePDF.SuccessoPercorsoFileStrutturaDownload = True
                        Exit For
                    Else
                        FilePDF.PercorsoFileStrutturaDownload = "..\/" & FilePDF.PercorsoFileStrutturaDownload
                        i = i + 1
                    End If
                Else
                    FilePDF.PercorsoFileStrutturaDownload = "..\/" & FilePDF.PercorsoFileStrutturaDownload
                    i = i + 1
                End If
            Next
        Else
            FilePDF.SuccessoPercorsoFileStrutturaDownload = True
            FilePDF.PercorsoFileStrutturaDownload = Percorso
        End If
        FilePDF.NomeFileOriginale = NomeFile
        FilePDF.PercorsoFileStruttura = Percorso
        If DataOraNomeFile Then
            NomeFile = NomeFile & Format(Now, "yyyyMMddHHmmss")
        End If
        FilePDF.NomeFileStruttura = NomeFile
        PdfWriter.GetInstance(Document, New FileStream(System.Web.HttpContext.Current.Server.MapPath(Percorso & NomeFile & ".pdf"), FileMode.Create))
        SetMetaData(Document, Autore, ParoleChiave, Titolo, Sottotitolo)
        Return FilePDF
    End Function
    ''' <summary>Conta il numero di volte che si presenta un carattere all'interno della Stringa</summary>
    ''' <param name="car">Definizione del carattere da ricercare nella stringa.</param>
    ''' <param name="str">Definizione della stringa su cui ricercare il carattere.</param>
    Private Function ContaCaratteriStringa(car As String, str As String) As Long
        If Len(car) <> 1 Then Err.Raise(5)
        ContaCaratteriStringa = Len(str) - Len(Replace(str, car, "", , , vbTextCompare))
        Return ContaCaratteriStringa
    End Function
    ''' <summary>Procedura per la Assegnazione dei MetaData del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta creando per la Creazione del PDF.</param>
    ''' <param name="Autore">Definizione del MetaData Autore del File PDF che si sta creando.</param>
    ''' <param name="ParoleChiave">Definizione del MetaData KeyWords del File PDF che si sta creando.</param>
    ''' <param name="Titolo">Definizione del MetaData Titolo del File PDF che si sta creando.</param>
    ''' <param name="Sottotitolo">Definizione del MetaData SottoTitolo del File PDF che si sta creando.</param>
    Private Function SetMetaData(ByVal Document As Document, ByVal Autore As String, ByVal ParoleChiave As String, ByVal Titolo As String, ByVal Sottotitolo As String) As Document
        Document.AddAuthor(Autore)
        Document.AddCreationDate()
        Document.AddCreator("© S&S Sistemi & Soluzioni S.r.l.")
        Document.AddKeywords(ParoleChiave)
        Document.AddProducer()
        Document.AddTitle(Titolo)
        Document.AddSubject(Sottotitolo)
        Return Document
    End Function
    ''' <summary>Procedura per l'Apertura del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    Public Function ApriDocumento(ByVal Document As Document) As Document
        Document.Open()
        Return Document
    End Function
    ''' <summary>Procedura per il Controllo dello stato del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    Public Function ControllaStatoDocumento(ByVal Document As Document) As Boolean
        If Document.IsOpen() Then
            ControllaStatoDocumento = True
        Else
            ControllaStatoDocumento = False
        End If
        Return ControllaStatoDocumento
    End Function
    ''' <summary>Procedura per la creazione di una nuova pagina del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    ''' <param name="Obj">Definizione dell'oggetto che si sta utilizzando.</param>
    Public Function NuovaPaginaOggetto(ByVal Document As Document, ByVal Obj As Object) As Object
        If ControllaStatoDocumento(Document) Then
            If TypeOf Obj Is Document Then
                CType(Obj, Document).NewPage()
            ElseIf TypeOf Obj Is Chapter Then
                CType(Obj, Chapter).NewPage()
            ElseIf TypeOf Obj Is Section Then
                CType(Obj, Section).NewPage()
            End If
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per la creazione di una nuova pagina Personalizzata del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    ''' <param name="Dimensioni">Definizione delle dimensioni della Pagina del Documento.</param>
    ''' <param name="Orientamento">Definizione dell'Orientamento della Pagina del Documento.</param>
    ''' <param name="Obj">Definizione dell'Oggetto su cui aggiungere una nuova Pagina. (Opzionale. Default: Document)</param>
    Public Function NuovaPaginaPersonalizzata(ByVal Document As Document, ByVal Dimensioni As DimensioniPagina, ByVal Orientamento As OrientamentoPagina, Optional ByVal Obj As Object = Nothing) As Document
        If ControllaStatoDocumento(Document) Then
            Select Case Dimensioni
                Case 1
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A0)
                    Else
                        Document.SetPageSize(PageSize.A0.Rotate)
                    End If
                Case 2
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A1)
                    Else
                        Document.SetPageSize(PageSize.A1.Rotate)
                    End If
                Case 3
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A2)
                    Else
                        Document.SetPageSize(PageSize.A2.Rotate)
                    End If
                Case 4
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A3)
                    Else
                        Document.SetPageSize(PageSize.A3.Rotate)
                    End If
                Case 5
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A4)
                    Else
                        Document.SetPageSize(PageSize.A4.Rotate)
                    End If
                Case 6
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A5)
                    Else
                        Document.SetPageSize(PageSize.A5.Rotate)
                    End If
                Case 7
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A6)
                    Else
                        Document.SetPageSize(PageSize.A6.Rotate)
                    End If
                Case 8
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A7)
                    Else
                        Document.SetPageSize(PageSize.A7.Rotate)
                    End If
                Case 9
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A8)
                    Else
                        Document.SetPageSize(PageSize.A8.Rotate)
                    End If
                Case 10
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A9)
                    Else
                        Document.SetPageSize(PageSize.A9.Rotate)
                    End If
                Case 11
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.A10)
                    Else
                        Document.SetPageSize(PageSize.A10.Rotate)
                    End If
                Case 12
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ARCH_A)
                    Else
                        Document.SetPageSize(PageSize.ARCH_A.Rotate)
                    End If
                Case 13
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ARCH_B)
                    Else
                        Document.SetPageSize(PageSize.ARCH_B.Rotate)
                    End If
                Case 14
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ARCH_C)
                    Else
                        Document.SetPageSize(PageSize.ARCH_C.Rotate)
                    End If
                Case 15
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ARCH_D)
                    Else
                        Document.SetPageSize(PageSize.ARCH_D.Rotate)
                    End If
                Case 16
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ARCH_E)
                    Else
                        Document.SetPageSize(PageSize.ARCH_E.Rotate)
                    End If
                Case 17
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B0)
                    Else
                        Document.SetPageSize(PageSize.B0.Rotate)
                    End If
                Case 18
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B1)
                    Else
                        Document.SetPageSize(PageSize.B1.Rotate)
                    End If
                Case 19
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B2)
                    Else
                        Document.SetPageSize(PageSize.B2.Rotate)
                    End If
                Case 20
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B3)
                    Else
                        Document.SetPageSize(PageSize.B3.Rotate)
                    End If
                Case 21
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B4)
                    Else
                        Document.SetPageSize(PageSize.B4.Rotate)
                    End If
                Case 22
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B5)
                    Else
                        Document.SetPageSize(PageSize.B5.Rotate)
                    End If
                Case 23
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B6)
                    Else
                        Document.SetPageSize(PageSize.B6.Rotate)
                    End If
                Case 24
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B7)
                    Else
                        Document.SetPageSize(PageSize.B7.Rotate)
                    End If
                Case 25
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B8)
                    Else
                        Document.SetPageSize(PageSize.B8.Rotate)
                    End If
                Case 26
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B9)
                    Else
                        Document.SetPageSize(PageSize.B9.Rotate)
                    End If
                Case 27
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.B10)
                    Else
                        Document.SetPageSize(PageSize.B10.Rotate)
                    End If
                Case 28
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.CROWN_OCTAVO)
                    Else
                        Document.SetPageSize(PageSize.CROWN_OCTAVO.Rotate)
                    End If
                Case 29
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.CROWN_QUARTO)
                    Else
                        Document.SetPageSize(PageSize.CROWN_QUARTO.Rotate)
                    End If
                Case 30
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.DEMY_OCTAVO)
                    Else
                        Document.SetPageSize(PageSize.DEMY_OCTAVO.Rotate)
                    End If
                Case 31
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.DEMY_QUARTO)
                    Else
                        Document.SetPageSize(PageSize.DEMY_QUARTO.Rotate)
                    End If
                Case 32
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.EXECUTIVE)
                    Else
                        Document.SetPageSize(PageSize.EXECUTIVE.Rotate)
                    End If
                Case 33
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.FLSA)
                    Else
                        Document.SetPageSize(PageSize.FLSA.Rotate)
                    End If
                Case 34
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.FLSE)
                    Else
                        Document.SetPageSize(PageSize.FLSE.Rotate)
                    End If
                Case 35
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.HALFLETTER)
                    Else
                        Document.SetPageSize(PageSize.HALFLETTER.Rotate)
                    End If
                Case 36
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ID_1)
                    Else
                        Document.SetPageSize(PageSize.ID_1.Rotate)
                    End If
                Case 37
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ID_2)
                    Else
                        Document.SetPageSize(PageSize.ID_2.Rotate)
                    End If
                Case 38
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ID_3)
                    Else
                        Document.SetPageSize(PageSize.ID_3.Rotate)
                    End If
                Case 39
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.LARGE_CROWN_OCTAVO)
                    Else
                        Document.SetPageSize(PageSize.LARGE_CROWN_OCTAVO.Rotate)
                    End If
                Case 40
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.LARGE_CROWN_QUARTO)
                    Else
                        Document.SetPageSize(PageSize.LARGE_CROWN_QUARTO.Rotate)
                    End If
                Case 41
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.LEDGER)
                    Else
                        Document.SetPageSize(PageSize.LEDGER.Rotate)
                    End If
                Case 42
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.LEGAL)
                    Else
                        Document.SetPageSize(PageSize.LEGAL.Rotate)
                    End If
                Case 43
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.LETTER)
                    Else
                        Document.SetPageSize(PageSize.LETTER.Rotate)
                    End If
                Case 44
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.NOTE)
                    Else
                        Document.SetPageSize(PageSize.NOTE.Rotate)
                    End If
                Case 45
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.PENGUIN_LARGE_PAPERBACK)
                    Else
                        Document.SetPageSize(PageSize.PENGUIN_LARGE_PAPERBACK.Rotate)
                    End If
                Case 46
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.PENGUIN_SMALL_PAPERBACK)
                    Else
                        Document.SetPageSize(PageSize.PENGUIN_SMALL_PAPERBACK.Rotate)
                    End If
                Case 47
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.POSTCARD)
                    Else
                        Document.SetPageSize(PageSize.POSTCARD.Rotate)
                    End If
                Case 48
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ROYAL_OCTAVO)
                    Else
                        Document.SetPageSize(PageSize.ROYAL_OCTAVO.Rotate)
                    End If
                Case 49
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.ROYAL_QUARTO)
                    Else
                        Document.SetPageSize(PageSize.ROYAL_QUARTO.Rotate)
                    End If
                Case 50
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.SMALL_PAPERBACK)
                    Else
                        Document.SetPageSize(PageSize.SMALL_PAPERBACK.Rotate)
                    End If
                Case 51
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize.TABLOID)
                    Else
                        Document.SetPageSize(PageSize.TABLOID.Rotate)
                    End If
                Case 52
                    If Orientamento = 0 Then
                        Document.SetPageSize(PageSize._11X17)
                    Else
                        Document.SetPageSize(PageSize._11X17.Rotate)
                    End If
                Case Else

            End Select
            If Obj = Nothing Then
                Document.NewPage()
            Else
                If TypeOf Obj Is Document Then
                    CType(Obj, Document).NewPage()
                ElseIf TypeOf Obj Is Chapter Then
                    CType(Obj, Chapter).NewPage()
                ElseIf TypeOf Obj Is Section Then
                    CType(Obj, Section).NewPage()
                End If
            End If
        End If
        Return Document
    End Function
    ''' <summary>Procedura per la Chiusura del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    ''' <param name="FilePDF">Definizione del File PDF da chiudere.</param>
    ''' <param name="Page">Definizione della Pagine.</param>
    ''' <param name="Type">Definizione del Tipo delle Pagina.</param>
    Public Function ChiudiDocumento(ByRef Document As Document, ByVal FilePDF As StrutturaNuovoFile, ByVal Page As System.Web.UI.Page, Type As System.Type) As Boolean
        ChiudiDocumento = False
        If Document.IsOpen() Then
            Try
                Document.Close()
                'Threading.Thread.Sleep(1000)
                ChiudiDocumento = True
            Catch ex1 As Exception
                'Threading.Thread.Sleep(1000)
                If File.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf") Then
                    For i As Integer = 0 To 10
                        Try
                            File.Delete(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf")
                            Exit For
                        Catch ex2 As Exception
                            i += 1
                            'Threading.Thread.Sleep(1000)
                        End Try
                    Next
                End If
                ScriptManager.RegisterClientScriptBlock(Page, Type, "msg", "alert('Errore nella procedura di creazione Report!');self.close();", True)
                ChiudiDocumento = False
            End Try
        End If
        Return ChiudiDocumento
    End Function
    'Optional ByVal WaterMark As Boolean = False, Optional ByVal ColoreWaterMark As Colori = Colori.Rosso, Optional RotazioneWaterMark As Single = 45, Optional ByVal TestoWaterMark As String = "S&S Sistemi & Soluzioni S.r.l.", Optional ByVal OpacitaWaterMark As Single = 0.400000006F - ' <param name="WaterMark">Definizione se inserire il WaterMark(Filigrana) al File Pdf. (True: SI, False: NO)</param> ' <param name="ColoreWaterMark">Definizione del colore del testo del Watermark</param> ' <param name="RotazioneWaterMark">Definizione dei gradi ri rotazione del testo del Watermark</param> ' <param name="TestoWaterMark">Definizione del testo del Watermark</param> ' <param name="OpacitaWaterMark">Definizione dell'opacità del Watermark</param>
    ''' <summary>Procedura per il Return del File Pdf Generato</summary>
    ''' <param name="FilePDF">Definizione del File Pdf su cui effettuare il Return.</param>
    ''' <param name="Page">Definizione della Pagine.</param>
    ''' <param name="Type">Definizione del Tipo delle Pagina.</param>
    ''' <param name="TipoReturnFilePDF">Definizione del tipo di Return del File Pdf.</param>
    Public Function RitornaFilePDF(ByVal FilePDF As StrutturaNuovoFile, ByVal Page As System.Web.UI.Page, Type As System.Type, Optional ByVal TipoReturnFilePDF As TipoReturnFilePdf = TipoReturnFilePdf.Redirect) As Boolean
        RitornaFilePDF = False
        'If WaterMark = True Then
        '    FilePDF = InserisciWatermark(FilePDF, ColoreWaterMark, RotazioneWaterMark, TestoWaterMark, OpacitaWaterMark)
        'End If
        If File.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf") Then
            If FilePDF.SuccessoPercorsoFileStrutturaDownload = False Then TipoReturnFilePDF = 0
            Select Case TipoReturnFilePDF
                Case 0
                    HttpContext.Current.Response.Redirect(FilePDF.PercorsoFileStruttura & FilePDF.NomeFileStruttura & ".pdf", False)
                Case 1
                    CreaZipFile(FilePDF)
                    ScriptManager.RegisterStartupScript(Page, Type, "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('" & FilePDF.PercorsoFileStrutturaDownload & FilePDF.NomeFileStruttura & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');} self.close();", True)
                Case 2
                    ScriptManager.RegisterStartupScript(Page, Type, "hrefFile", "avvio(); function avvio() { window.open('" & FilePDF.PercorsoFileStrutturaDownload & FilePDF.NomeFileStruttura & ".pdf','_blank','height=800,width=1024,resizable=1,scroll=1');} self.close();", True)
            End Select
        Else
            If File.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf") Then
                For i As Integer = 0 To 10
                    Try
                        File.Delete(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf")
                        Exit For
                    Catch ex2 As Exception
                        i += 1
                        'Threading.Thread.Sleep(1000)
                    End Try
                Next
            End If
            ScriptManager.RegisterClientScriptBlock(Page, Type, "msg", "alert('Errore nella procedura di creazione Report!');self.close();", True)
        End If
        RitornaFilePDF = True
    End Function
    ''' <summary>Procedura per la Creazione dello Zip del File PDF</summary>
    ''' <param name="FilePDF">Definizione del File PDF da aggiungere allo Zip.</param>
    Private Function CreaZipFile(ByVal FilePDF As StrutturaNuovoFile) As String
        CreaZipFile = ""
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim strFile As String
        strFile = System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura & FilePDF.NomeFileStruttura & ".pdf")
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
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
        Dim zipfic As String
        zipfic = System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura & FilePDF.NomeFileStruttura & ".zip")
        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()
        File.Delete(strFile)
        Return CreaZipFile
    End Function
    ''' <summary>Procedura per impostare l'Header del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nell'header.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Allignamento">Definizione dell'Allignamento che si vuole utilizzare.</param>
    ''' <param name="AfterTesto">Definizione del testo che deve essere scritto successivamente al'header.</param>
    ''' <param name="BeforeTesto">Definizione del testo che deve essere scritto prima dell'header.</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="AllignamentoAfter">Definizione dell'Allignamento del Testo After dell'Header</param>
    ''' <param name="AllignamentoBefore">Definizione dell'Allignamento del Testo Before dell'Header</param>
    ''' <param name="SpazioAfter">Definizione dello spazio per il Testo After dell'Header</param>
    ''' <param name="SpazioBefore">Definizione dello spazio per il Testo Before dell'Header</param>
    Public Function ImpostaHeader(ByVal Document As Document, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Allignamento As AllignamentoElementi = AllignamentoElementi.Left, Optional ByVal AfterTesto As String = "", Optional ByVal BeforeTesto As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal AllignamentoAfter As AllignamentoTesto = AllignamentoTesto.Sinistra, Optional ByVal AllignamentoBefore As AllignamentoTesto = AllignamentoTesto.Sinistra, Optional ByVal SpazioAfter As Single = 0, Optional ByVal SpazioBefore As Single = 0) As Document
        Dim header As New HeaderFooter(New Phrase(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)), False)
        header.Alignment = AllignamentoElementiPDF(header, Allignamento)
        If Not String.IsNullOrEmpty(AfterTesto) Then
            Dim paragrafo = CreaParagraph(AfterTesto, ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)
            SettaParagraph(paragrafo, AllignamentoAfter, , , , SpazioAfter)
            header.After = paragrafo
        End If
        If Not String.IsNullOrEmpty(BeforeTesto) Then
            Dim paragrafo = CreaParagraph(BeforeTesto, ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)
            SettaParagraph(paragrafo, AllignamentoBefore, , , , , SpazioBefore)
            header.Before = paragrafo
        End If
        SetBorder(header, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        Document.Header = header
        Return Document
    End Function
    ''' <summary>Procedura per resettare l'Header del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    Public Function ResetHeader(ByVal Document As Document) As Document
        Document.ResetHeader()
        Return Document
    End Function
    ''' <summary>Procedura per impostare il Footer del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nell'header.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="AfterTesto">Definizione del testo che deve essere scritto successivamente al footer.</param>
    ''' <param name="BeforeTesto">Definizione del testo che deve essere scritto prima del footer.</param>
    ''' <param name="Allignamento">Definizione dell'Allignamento Orizzontale che si vuole utilizzare.</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="AllignamentoAfter">Definizione dell'Allignamento del Testo After dell'Header</param>
    ''' <param name="AllignamentoBefore">Definizione dell'Allignamento del Testo Before dell'Header</param>
    ''' <param name="SpazioAfter">Definizione dello spazio per il Testo After dell'Header</param>
    ''' <param name="SpazioBefore">Definizione dello spazio per il Testo Before dell'Header</param>
    Public Function ImpostaFooter(ByVal Document As Document, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Testo As String = "Pagina: ", Optional ByVal Allignamento As AllignamentoElementi = AllignamentoElementi.Right, Optional ByVal AfterTesto As String = "", Optional ByVal BeforeTesto As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal AllignamentoAfter As AllignamentoTesto = AllignamentoTesto.Sinistra, Optional ByVal AllignamentoBefore As AllignamentoTesto = AllignamentoTesto.Sinistra, Optional ByVal SpazioAfter As Single = 0, Optional ByVal SpazioBefore As Single = 0) As Document
        Dim footer As New HeaderFooter(New Phrase(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)), True)
        footer.Alignment = AllignamentoElementiPDF(footer, Allignamento)
        If Not String.IsNullOrEmpty(AfterTesto) Then
            Dim paragrafo = CreaParagraph(AfterTesto, ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)
            SettaParagraph(paragrafo, AllignamentoAfter, , , , SpazioAfter)
            footer.After = paragrafo
        End If
        If Not String.IsNullOrEmpty(BeforeTesto) Then
            Dim paragrafo = CreaParagraph(BeforeTesto, ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)
            SettaParagraph(paragrafo, AllignamentoBefore, , , , , SpazioBefore)
            footer.Before = paragrafo
        End If
        SetBorder(footer, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        Document.Footer = footer
        Return Document
    End Function
    ''' <summary>Procedura per resettare il Footer del File PDF</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    Public Function ResetFooter(ByVal Document As Document) As Document
        Document.ResetFooter()
        Return Document
    End Function
    ''' <summary>Procedura per la Definizione del Font che verrà utilizzato</summary>
    ''' <param name="Valori">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="Famiglia">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Dimensione">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Stile">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Colore">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    Public Function ImpostaFont(ByVal Valori As SetValori, Optional ByVal Famiglia As FontPDF = FontPDF.Times, Optional ByVal Dimensione As Single = 12, Optional ByVal Stile As FontStylePDF = FontStylePDF.Normale, Optional ByVal Colore As FontColorPDF = FontColorPDF.Nero) As Font
        If Valori <> 1 Then
            Famiglia = PropertyFamiglia
            Dimensione = PropertyDimensione
            If Valori <> 2 Then
                If Valori <> 3 Then
                    Stile = PropertyStyleFont
                End If
                Colore = PropertyColorFont
            End If
        End If
        ImpostaFont = New Font
        Select Case Famiglia 'Gestione Font Testo
            Case 0
                ImpostaFont.SetFamily(BaseFont.COURIER)
            Case 1
                ImpostaFont.SetFamily(BaseFont.HELVETICA)
            Case 2
                ImpostaFont.SetFamily(BaseFont.TIMES_ROMAN)
            Case 3
                ImpostaFont.SetFamily(Font.TIMES_ROMAN)
            Case Else
                ImpostaFont.SetFamily(BaseFont.TIMES_ROMAN)
        End Select
        ImpostaFont.Size = Dimensione 'Gestione della Dimensione del Font Testo
        Select Case Stile 'Gestione dello Style del Font Testo
            Case 0
                ImpostaFont.SetStyle(Font.BOLD)
            Case 1
                ImpostaFont.SetStyle(Font.BOLDITALIC)
            Case 2
                ImpostaFont.SetStyle(Font.ITALIC)
            Case 3
                ImpostaFont.SetStyle(Font.NORMAL)
            Case 4
                ImpostaFont.SetStyle(Font.STRIKETHRU)
            Case 5
                ImpostaFont.SetStyle(Font.UNDERLINE)
            Case Else
                ImpostaFont.SetStyle(Font.NORMAL)
        End Select
        Select Case Colore 'Gestione del Colore del Font Testo
            Case 0
                ImpostaFont.SetColor(0, 0, 0)
            Case 1
                ImpostaFont.SetColor(0, 0, 255)
            Case 2
                ImpostaFont.SetColor(0, 255, 255)
            Case 3
                ImpostaFont.SetColor(64, 64, 64)
            Case 4
                ImpostaFont.SetColor(128, 128, 128)
            Case 5
                ImpostaFont.SetColor(0, 255, 0)
            Case 6
                ImpostaFont.SetColor(192, 192, 192)
            Case 7
                ImpostaFont.SetColor(255, 0, 255)
            Case 8
                ImpostaFont.SetColor(255, 200, 0)
            Case 9
                ImpostaFont.SetColor(255, 175, 175)
            Case 10
                ImpostaFont.SetColor(255, 0, 0)
            Case 11
                ImpostaFont.SetColor(255, 255, 255)
            Case 12
                ImpostaFont.SetColor(255, 255, 0)
            Case Else
                ImpostaFont.SetColor(0, 0, 0)
        End Select
        Return ImpostaFont
    End Function
    ''' <summary>Procedura per la Modifica dell'Allignamento dell'Oggetto</summary>
    ''' <param name="Elemento">Definizione dell' elemento che si vuole utilizzare per l'Alligamneto.</param>
    ''' <param name="Allignamento">Definizione del tipo di Allignamento che si vuole utilizzare.</param>
    Public Function AllignamentoElementiPDF(ByVal Elemento As Object, ByVal Allignamento As AllignamentoElementi) As Object
        Select Case Allignamento
            Case 0
                Return Element.ALIGN_BASELINE
            Case 1
                Return Element.ALIGN_BOTTOM
            Case 2
                Return Element.ALIGN_CENTER
            Case 3
                Return Element.ALIGN_JUSTIFIED
            Case 4
                Return Element.ALIGN_LEFT
            Case 5
                Return Element.ALIGN_MIDDLE
            Case 6
                Return Element.ALIGN_RIGHT
            Case 7
                Return Element.ALIGN_TOP
            Case Else
                Return False
        End Select
    End Function
    ''' <summary>Procedura per settare il colore di un Oggetto.</summary>
    ''' <param name="Colore">Definizione del Colore da settare.</param>
    Private Function SetColor(ByVal Colore As Colori) As Color
        Select Case Colore
            Case 0
                Return PatternColor.BLACK
            Case 1
                Return PatternColor.BLUE
            Case 2
                Return PatternColor.CYAN
            Case 3
                Return PatternColor.DARK_GRAY
            Case 4
                Return PatternColor.GREEN
            Case 5
                Return PatternColor.LIGHT_GRAY
            Case 6
                Return PatternColor.MAGENTA
            Case 7
                Return PatternColor.ORANGE
            Case 8
                Return PatternColor.PINK
            Case 9
                Return PatternColor.RED
            Case 10
                Return PatternColor.WHITE
            Case 11
                Return PatternColor.YELLOW
            Case Else
                Return PatternColor.BLACK
        End Select
    End Function
    ''' <summary>Procedura per impostare il ritorno a Capo con un rigo vuoto nel Documento</summary>
    ''' <param name="Obj">Definizione dell'oggetto che si sta utilizzando.</param>
    ''' <param name="NumReturn">Se definito va a capo per il numero di volte inserite nel parametro.</param>
    Public Function RitornoACapoVuoto(ByVal Obj As Object, Optional ByVal NumReturn As Integer = 0) As Document
        Dim RitornoACapoTesto = New Phrase(vbCrLf)
        If NumReturn > 1 Then
            For i = 1 To NumReturn
                If TypeOf Obj Is Document Then
                    CType(Obj, Document).Add(RitornoACapoTesto)
                ElseIf TypeOf Obj Is Chapter Then
                    CType(Obj, Chapter).Add(RitornoACapoTesto)
                ElseIf TypeOf Obj Is Section Then
                    CType(Obj, Section).Add(RitornoACapoTesto)
                End If
            Next
        Else
            If TypeOf Obj Is Document Then
                CType(Obj, Document).Add(RitornoACapoTesto)
            ElseIf TypeOf Obj Is Chapter Then
                CType(Obj, Chapter).Add(RitornoACapoTesto)
            ElseIf TypeOf Obj Is Section Then
                CType(Obj, Section).Add(RitornoACapoTesto)
            End If
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per impostare il ritorno a Capo senza un rigo vuoto nel Documento</summary>
    ''' <param name="Obj">Definizione dell'oggetto che si sta utilizzando.</param>
    Public Function RitornoACapoSenzaVuoto(ByVal Obj As Object) As Object
        Dim RitornoACapoTesto = New Chunk(Environment.NewLine)
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(RitornoACapoTesto)
        ElseIf TypeOf Obj Is Phrase Then
            CType(Obj, Phrase).Add(RitornoACapoTesto)
        ElseIf TypeOf Obj Is Paragraph Then
            CType(Obj, Paragraph).Add(RitornoACapoTesto)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(RitornoACapoTesto)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(RitornoACapoTesto)
        End If
        Return Obj
    End Function
    'Definizione delle Funzioni per l'Inserimento di Oggetti all'interno del file PDF
    ''' <summary>Procedura per l' inserimento di un Chunk (è la più piccola parte di testo significativa che può essere aggiunta all’interno di un Document)</summary>
    ''' <param name="Obj">Definizione dell'oggetto che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreBackGround">Definizione del colore che si vuole utilizzare per backgroung del Chunk.</param>
    ''' <param name="Collegamento">Definizione del collegamento hyperlink che si vuole ancorare al Chunk.</param>
    ''' <param name="DimSottolineatura">Definizione della dimensione della sottolineatura aggiuntiva del Chunk.</param>
    ''' <param name="PosSottolineatura">Definizione della posizione della sottolineatura aggiuntiva del Chunk.</param>
    Public Function InserisciNuovaChunk(ByVal Obj As Object, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ColoreBackGround As String = "", Optional Collegamento As String = "", Optional DimSottolineatura As Single = 0, Optional PosSottolineatura As Single = 0) As Object
        Dim Chunk = New Chunk(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))
        If Not String.IsNullOrEmpty(ColoreBackGround) And Len(ColoreBackGround) = 7 Then
            Chunk.SetBackground(New Color(Drawing.ColorTranslator.FromHtml(ColoreBackGround)))
        End If
        If Not String.IsNullOrEmpty(Collegamento) Then
            Chunk.SetAnchor(Collegamento)
        End If
        If DimSottolineatura > 0 Then
            Chunk.SetUnderline(DimSottolineatura, PosSottolineatura)
        End If
        AggiungiChunk(Obj, Chunk)
        Return Obj
    End Function
    ''' <summary>Procedura per la creazione di un Chunk (è la più piccola parte di testo significativa che può essere aggiunta all’interno di un Document)</summary>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreBackGround">Definizione del colore che si vuole utilizzare per backgroung del Chunk.</param>
    ''' <param name="Collegamento">Definizione del collegamento hyperlink che si vuole ancorare al Chunk.</param>
    ''' <param name="DimSottolineatura">Definizione della dimensione della sottolineatura aggiuntiva del Chunk.</param>
    ''' <param name="PosSottolineatura">Definizione della posizione della sottolineatura aggiuntiva del Chunk.</param>
    Public Function CreaChunk(ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ColoreBackGround As String = "", Optional Collegamento As String = "", Optional DimSottolineatura As Single = 0, Optional PosSottolineatura As Single = 0) As Chunk
        CreaChunk = New Chunk(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))
        If Not String.IsNullOrEmpty(ColoreBackGround) And Len(ColoreBackGround) = 7 Then
            CreaChunk.SetBackground(New Color(Drawing.ColorTranslator.FromHtml(ColoreBackGround)))
        End If
        If Not String.IsNullOrEmpty(Collegamento) Then
            CreaChunk.SetAnchor(Collegamento)
        End If
        If DimSottolineatura > 0 Then
            CreaChunk.SetUnderline(DimSottolineatura, PosSottolineatura)
        End If
        Return CreaChunk
    End Function
    ''' <summary>Procedura per l' inserimento di un Chunk (è la più piccola parte di testo significativa che può essere aggiunta all’interno di un Document)</summary>
    ''' <param name="Obj">Definizione dell'oggetto che si sta utilizzando.</param>
    ''' <param name="Chunk">Definizione del Chunk che si vuole inserire nell'oggetto.</param>
    Public Function AggiungiChunk(ByVal Obj As Object, Chunk As Chunk) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Chunk)
        ElseIf TypeOf Obj Is Phrase Then
            CType(Obj, Phrase).Add(Chunk)
        ElseIf TypeOf Obj Is Paragraph Then
            CType(Obj, Paragraph).Add(Chunk)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Chunk)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Chunk)
        ElseIf TypeOf Obj Is PdfPCell Then
            CType(Obj, PdfPCell).AddElement(Chunk)
        ElseIf TypeOf Obj Is Cell Then
            CType(Obj, Cell).AddElement(Chunk)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Chunk)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per l' inserimento di una Phrase (è semplicemente un insieme di Chunk che può essere inserito, in maniera immediata, all’interno di un Document)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    Public Function InserisciNuovaPhrase(ByVal Obj As Object, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero) As Object
        Dim Phrase = New Phrase(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))
        AggiungiPhrase(Obj, Phrase)
        Return Obj
    End Function
    ''' <summary>Procedura per la Creazione di una Phrase (è semplicemente un insieme di Chunk che può essere inserito, in maniera immediata, all’interno di un Document)</summary>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    Public Function CreaPhrase(ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero) As Phrase
        CreaPhrase = New Phrase(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))
        Return CreaPhrase
    End Function
    ''' <summary>Procedura per l' inserimento di una Phrase (è semplicemente un insieme di Chunk che può essere inserito, in maniera immediata, all’interno di un Document)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Phrase">Definizione della Phrase che si vuole inserire nell'oggetto.</param>
    Public Function AggiungiPhrase(ByVal Obj As Object, ByVal Phrase As Phrase) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Phrase)
        ElseIf TypeOf Obj Is Paragraph Then
            CType(Obj, Paragraph).Add(Phrase)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Phrase)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Phrase)
        ElseIf TypeOf Obj Is PdfPCell Then
            CType(Obj, PdfPCell).AddElement(Phrase)
        ElseIf TypeOf Obj Is Cell Then
            CType(Obj, Cell).AddElement(Phrase)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Phrase)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per l' inserimento di un Paragraph (è un insieme di Phrase e/o Chunk opportunamente disposti e formattati)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    Public Function InserisciNuovoParagraph(ByVal Obj As Object, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero) As Object
        Dim Paragraph = New Paragraph(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))
        AggiungiParagraph(Obj, Paragraph)
        Return Obj
    End Function
    ''' <summary>Procedura per la creazione di un Paragraph (è un insieme di Phrase e/o Chunk opportunamente disposti e formattati)</summary>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    Public Function CreaParagraph(ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero) As Paragraph
        CreaParagraph = New Paragraph(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))
        Return CreaParagraph
    End Function
    ''' <summary>Procedura per il settaggio di un Paragraph (è un insieme di Phrase e/o Chunk opportunamente disposti e formattati)</summary>
    ''' <param name="Paragraph">Definizione del Paragraph che si vuole settare.</param>
    ''' <param name="Allignamento">Definizione dell'allignamento che si vuole utilizzare per il Paragraph che si vuole settare.</param>
    ''' <param name="RietroPrimaRiga">Consente di applicare un valore float per far rientrare la prima riga per il Paragraph che si vuole settare.</param>
    ''' <param name="SpazioSinistra">Consente di aggiungere spazio a sinistra per il Paragraph che si vuole settare.</param>
    ''' <param name="SpazioDestra">Consente di aggiungere spazio a destra per il Paragraph che si vuole settare.</param>
    ''' <param name="SpazioSopra">Consente di aggiungere una quantità specificata di spazio sopra il Paragrafo per il Paragraph che si vuole settare.</param>
    ''' <param name="SpazioSotto">Consente di aggiungere una quantità specificata di spazio sotto il Paragrafo per il Paragraph che si vuole settare.</param>
    Public Function SettaParagraph(ByVal Paragraph As Paragraph, Optional ByVal Allignamento As AllignamentoTesto = AllignamentoTesto.Sinistra, Optional RietroPrimaRiga As Single = 0, Optional SpazioSinistra As Single = 0, Optional SpazioDestra As Single = 0, Optional SpazioSopra As Single = 0, Optional SpazioSotto As Single = 0) As Paragraph
        Select Case Allignamento 'Gestione dell'Allignamento del Paragrafo
            Case 0
                Paragraph.SetAlignment("Left")
            Case 1
                Paragraph.SetAlignment("Center")
            Case 2
                Paragraph.SetAlignment("Justify")
            Case 3
                Paragraph.SetAlignment("Right")
            Case Else

        End Select
        If RietroPrimaRiga <> 0 Then
            Paragraph.FirstLineIndent = RietroPrimaRiga
        End If
        If SpazioSinistra <> 0 Then
            Paragraph.IndentationLeft = SpazioSinistra
        End If
        If SpazioDestra <> 0 Then
            Paragraph.IndentationRight = SpazioDestra
        End If
        If SpazioSopra <> 0 Then
            Paragraph.SpacingBefore = SpazioSopra
        End If
        If SpazioSotto <> 0 Then
            Paragraph.SpacingAfter = SpazioSotto
        End If
        Return Paragraph
    End Function
    ''' <summary>Procedura per l' inserimento di un Paragraph (è un insieme di Phrase e/o Chunk opportunamente disposti e formattati)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Paragraph">Definizione del Paragraph che si vuole inserire nell'oggetto.</param>
    Public Function AggiungiParagraph(ByVal Obj As Object, ByVal Paragraph As Paragraph) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Paragraph)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Paragraph)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Paragraph)
        ElseIf TypeOf Obj Is PdfPCell Then
            CType(Obj, PdfPCell).AddElement(Paragraph)
        ElseIf TypeOf Obj Is Cell Then
            CType(Obj, Cell).AddElement(Paragraph)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Paragraph)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per l' inserimento di un Chapter (è per iText una speciale Section atta a contenere, appunto, un insieme Section opportunamente sequenziate)</summary>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chapter.</param>
    ''' <param name="NumeroNodo">Definizione del Numero del Nodo del Chapter</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="TitoloBookmark">Definizione del Titolo del Chapter all'interno del Bookmark</param>
    ''' <param name="BookmarkAperto">Definizione se il Chapter deve essere aperto (True) o chiuso (False) all'interno del Bookmark</param>
    Public Function CreaChapter(ByVal Testo As String, ByVal NumeroNodo As Integer, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional TitoloBookmark As String = "", Optional BookmarkAperto As Boolean = True) As Chapter
        CreaChapter = New Chapter(New Paragraph(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)), NumeroNodo)
        If Not String.IsNullOrEmpty(TitoloBookmark) Then
            CreaChapter.BookmarkTitle = TitoloBookmark
        End If
        CreaChapter.BookmarkOpen = BookmarkAperto
        Return CreaChapter
    End Function
    ''' <summary>Procedura per l' inserimento di un Chapter (è per iText una speciale Section atta a contenere, appunto, un insieme Section opportunamente sequenziate)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Chapter">Definizione del Chapter che si vuole inserire nell'oggetto.</param>
    Public Function AggiungiChapter(ByVal Obj As Object, ByVal Chapter As Chapter) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Chapter)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per l'inserimento di una Section (è una parte del Document costituita da un insieme di Paragraph opportunamente disposti)</summary>
    ''' <param name="Obj">Definizione dell'oggetto che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nella Section</param>
    ''' <param name="NumeroNodo">Definizione del Numero del Nodo della Section</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="TitoloBookmark">Definizione del Titolo del Section all'interno del Bookmark</param>
    ''' <param name="BookmarkAperto">Definizione se il Section deve essere aperto (True) o chiuso (False) all'interno del Bookmark</param>
    Public Function AggiungiSection(ByVal Obj As Object, ByVal Testo As String, ByVal NumeroNodo As Integer, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional TitoloBookmark As String = "", Optional BookmarkAperto As Boolean = True) As Section
        AggiungiSection = Nothing
        If TypeOf Obj Is Chapter Then
            AggiungiSection = CType(Obj, Chapter).AddSection(20.0F, New Paragraph(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)), NumeroNodo)
        ElseIf TypeOf Obj Is Section Then
            AggiungiSection = CType(Obj, Section).AddSection(20.0F, New Paragraph(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)), NumeroNodo)
        End If
        If Not String.IsNullOrEmpty(TitoloBookmark) Then
            AggiungiSection.BookmarkTitle = TitoloBookmark
        End If
        AggiungiSection.BookmarkOpen = BookmarkAperto
        Return AggiungiSection
    End Function
    ''' <summary>Procedura per l' inserimento di un'Annotazione all'interno del Documento</summary>
    ''' <param name="Document">Definizione del Documento che si sta utilizzando.</param>
    ''' <param name="Titolo">Definizione del Titolo dell'Annotazione che si sta creando.</param>
    ''' <param name="Testo">Definizione del Testo dell'Annotazione che si sta creando.</param>
    ''' <param name="PosX">Definizione della Posizione X dell' Annotazione che si sta creando.</param>
    ''' <param name="PosY">Definizione della Posizione Y dell' Annotazione che si sta creando.</param>
    Public Function InserisciAnnotazioneDocumento(ByVal Document As Document, ByVal Titolo As String, ByVal Testo As String, Optional ByVal PosX As Single = 10.0F, Optional ByVal PosY As Single = 500.0F) As Document
        If ControllaStatoDocumento(Document) Then
            Dim Annotazione = New iTextSharp.text.Annotation(Titolo, Testo, PosX, PosY, 100.0F, 100.0F)
            Document.Add(Annotazione)
        End If
        Return Document
    End Function
    ''' <summary>Procedura per l' inserimento di un Anchor (Collegamento Ipertestuale)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nell' Anchor.</param>
    ''' <param name="Collegamento">Definizione del Collegamento Ipertestuale che si vuole aggiungere all'Anchor.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata). In Anchor ricorda di usare PredefinitiAnchor!</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    Public Function InserisciNuovoAnchor(ByVal Obj As Object, ByVal Testo As String, ByVal Collegamento As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12) As Object
        Dim Anchor As New Anchor(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, FontStylePDF.Sottolineato, FontColorPDF.Blu))
        Anchor.Reference = Collegamento
        AggiungiAnchor(Obj, Anchor)
        Return Obj
    End Function
    ''' <summary>Procedura per la creazione di un Anchor (Collegamento Ipertestuale)</summary>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nell' Anchor.</param>
    ''' <param name="Collegamento">Definizione del Collegamento Ipertestuale che si vuole aggiungere all'Anchor.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata). In Anchor ricorda di usare PredefinitiAnchor!</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    Public Function CreaAnchor(ByVal Testo As String, ByVal Collegamento As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12) As Anchor
        CreaAnchor = New Anchor(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, FontStylePDF.Sottolineato, FontColorPDF.Blu))
        CreaAnchor.Reference = Collegamento
        Return CreaAnchor
    End Function
    ''' <summary>Procedura per l' inserimento di un Anchor (Collegamento Ipertestuale)</summary>
    ''' <param name="Obj">Definizione dell' Oggetto che si sta utilizzando.</param>
    ''' <param name="Anchor">Definizione dell'Anchor che si vuole aggiungere all'oggetto.</param>
    Public Function AggiungiAnchor(ByVal Obj As Object, ByVal Anchor As Anchor) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Anchor)
        ElseIf TypeOf Obj Is Phrase Then
            CType(Obj, Phrase).Add(Anchor)
        ElseIf TypeOf Obj Is Paragraph Then
            CType(Obj, Paragraph).Add(Anchor)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Anchor)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Anchor)
        ElseIf TypeOf Obj Is PdfPCell Then
            CType(Obj, PdfPCell).AddElement(Anchor)
        ElseIf TypeOf Obj Is Cell Then
            CType(Obj, Cell).AddElement(Anchor)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Anchor)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per settare il colore del testo del Watermark</summary>
    ''' <param name="PdfContentByte">Definizione del Content su cui inserire il Watermark</param>
    ''' <param name="Color">Definizione del colore del Testo del Watermark</param>
    Private Function SetColorWatermark(ByVal PdfContentByte As PdfContentByte, ByVal Color As Colori) As PdfContentByte
        Select Case Color
            Case 0
                PdfContentByte.SetColorFill(PatternColor.BLACK)
            Case 1
                PdfContentByte.SetColorFill(PatternColor.BLUE)
            Case 2
                PdfContentByte.SetColorFill(PatternColor.CYAN)
            Case 3
                PdfContentByte.SetColorFill(PatternColor.DARK_GRAY)
            Case 4
                PdfContentByte.SetColorFill(PatternColor.GREEN)
            Case 5
                PdfContentByte.SetColorFill(PatternColor.LIGHT_GRAY)
            Case 6
                PdfContentByte.SetColorFill(PatternColor.MAGENTA)
            Case 7
                PdfContentByte.SetColorFill(PatternColor.ORANGE)
            Case 8
                PdfContentByte.SetColorFill(PatternColor.PINK)
            Case 9
                PdfContentByte.SetColorFill(PatternColor.RED)
            Case 10
                PdfContentByte.SetColorFill(PatternColor.WHITE)
            Case 11
                PdfContentByte.SetColorFill(PatternColor.YELLOW)
            Case Else

        End Select
        Return PdfContentByte
    End Function
    ''' <summary>Procedura per l'inserimento del Watermark all'interno del File PDF</summary>
    ''' <param name="FilePDF">Definizione del File Pdf su cui settare il Watermark</param>
    ''' <param name="Colore">Definizione del colore del testo del Watermark</param>
    ''' <param name="Rotazione">Definizione dei gradi ri rotazione del testo del Watermark</param>
    ''' <param name="Testo">Definizione del testo del Watermark</param>
    ''' <param name="Opacita">Definizione dell'opacità del Watermark</param>
    Private Function InserisciWatermark(ByVal FilePDF As StrutturaNuovoFile, Optional ByVal Colore As Colori = Colori.Rosso, Optional Rotazione As Single = 45, Optional ByVal Testo As String = "S&S Sistemi & Soluzioni S.r.l.", Optional ByVal Opacita As Single = 0.400000006F) As StrutturaNuovoFile
        Dim PdfReader As PdfReader = Nothing
        For i As Integer = 0 To 10
            Try
                PdfReader = New PdfReader(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf")
                Exit For
            Catch ex As Exception
                i += 1
                'Threading.Thread.Sleep(1000)
            End Try
        Next
        Dim NomeFile As String = FilePDF.NomeFileOriginale & Format(Now, "yyyyMMddHHmmss")
        Dim FileStream As New FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & NomeFile & ".pdf", FileMode.OpenOrCreate)
        Dim PdfStamper As New PdfStamper(PdfReader, FileStream)
        For Pagine As Integer = 1 To PdfReader.NumberOfPages
            Dim Pagina As iTextSharp.text.Rectangle = PdfReader.GetPageSizeWithRotation(Pagine)
            Dim PdfContentByte As PdfContentByte = PdfStamper.GetUnderContent(Pagine)
            PdfContentByte.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 40)
            Dim PdfGState As New PdfGState()
            PdfGState.FillOpacity = 0.400000006F
            PdfContentByte.SetGState(PdfGState)
            SetColorWatermark(PdfContentByte, Colore)
            PdfContentByte.BeginText()
            PdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Testo, Pagina.Width / 2, Pagina.Height / 2, Rotazione)
            PdfContentByte.EndText()
        Next
        PdfStamper.Close()
        FileStream.Close()
        If File.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf") Then
            For i As Integer = 0 To 10
                Try
                    File.Delete(System.Web.HttpContext.Current.Server.MapPath(FilePDF.PercorsoFileStruttura) & FilePDF.NomeFileStruttura & ".pdf")
                    Exit For
                Catch ex2 As Exception
                    i += 1
                    'Threading.Thread.Sleep(1000)
                End Try
            Next
        End If
        FilePDF.NomeFileStruttura = NomeFile
        Return FilePDF
    End Function
    ''' <summary>Procedura per la creazione di una PdfPTable</summary>
    ''' <param name="NumColonne">Definizione del numero di colonne che devono comporre la Tabella.</param>
    ''' <param name="Width">Definizione della larghezza in Percentuale della Tabella.</param>
    ''' <param name="Allignamento">Definizione dell'Allignamento della Table all'interno della Pagina.</param>
    ''' <param name="SpazioPrima">Definizione dello spazio che si trova prima della Tabella all'interno della Pagina.</param>
    ''' <param name="SpazioDopo">Definizione dello spazio che si trova dopo la Tabella all'interno della Pagina.</param>
    ''' <param name="BloccoDimensioni">Definizione se le Dimensioni della Tabella all'interno della Pagina devono essere bloccate.</param>
    Public Function CreaPdfPTable(ByVal NumColonne As Integer, ByVal Width As Single, ByVal Allignamento As AllignamentoElementi, Optional ByVal SpazioPrima As Single = 0, Optional ByVal SpazioDopo As Single = 0, Optional ByVal BloccoDimensioni As Boolean = False) As PdfPTable
        Dim Table = New PdfPTable(NumColonne)
        Table.WidthPercentage = Width
        Table.HorizontalAlignment = AllignamentoElementiPDF(Table, Allignamento)
        If SpazioPrima <> 0 Then
            Table.SpacingBefore = SpazioPrima
        End If
        If SpazioDopo <> 0 Then
            Table.SpacingAfter = SpazioDopo
        End If
        If BloccoDimensioni = True Then Table.LockedWidth = True
        Return Table
    End Function
    ''' <summary>Procedura per la creazione di una Table</summary>
    ''' <param name="NumColonne">Definizione del numero di colonne che devono comporre la Tabella.</param>
    ''' <param name="Allignamento">Definizione dell'Allignamento della Tabella all'interno del Documento.</param>
    ''' <param name="Width">Definizione della larghezza della Tabella.</param>
    ''' <param name="CellPadding">Definizione del CellPadding della Table.</param>
    ''' <param name="CellSpacing">Definizione del CellSpacing della Table.</param>
    ''' <param name="Padding">Definizione del Padding della Table.</param>
    ''' <param name="Spacing">Definizione dello Spacing della Table.</param>
    ''' <param name="Offset">Definizione dell'Offset della Table.</param>
    ''' <param name="RotazioneTable">Definizione se la Table deve essere rotata. (True: Si, False: No)</param>
    ''' <param name="Top">Definizione dello spazio superiore alla Table.</param>
    ''' <param name="Left">Definizione dello spazio sinistro alla Table.</param>
    ''' <param name="Right">Definizione dello spazio destro alla Table.</param>
    ''' <param name="Bottom">Definizione dello spazio inferiore alla Table.</param>
    Public Function CreaTable(ByVal NumColonne As Integer, ByVal Allignamento As AllignamentoElementi, Optional ByVal Width As Single = 0, Optional ByVal CellPadding As Single = 1, Optional ByVal CellSpacing As Single = 1, Optional ByVal Padding As Single = 1, Optional Spacing As Single = 1, Optional ByVal Offset As Single = 0, Optional ByVal RotazioneTable As Boolean = False, Optional Top As Single = 0, Optional Left As Single = 0, Optional Right As Single = 0, Optional Bottom As Single = 0) As Table
        Dim Table = New Table(NumColonne)
        Table.Alignment = AllignamentoElementiPDF(Table, Allignamento)
        Table.Cellpadding = CellPadding
        Table.Cellspacing = CellSpacing
        Table.Padding = Padding
        Table.Spacing = Spacing
        If Width <> 0 Then Table.Width = Width
        If RotazioneTable = True Then Table.Rotate()
        If Offset <> 0 Then Table.Offset = Offset
        If Top <> 0 Then Table.Top = Top
        If Left <> 0 Then Table.Left = Left
        If Right <> 0 Then Table.Right = Right
        If Bottom <> 0 Then Table.Bottom = Bottom
        Return Table
    End Function
    ''' <summary>Procedura per settare la Table</summary>
    ''' <param name="Table">Definizione della Table a cui si vogliono settare i parametri.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Table.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Table in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Table. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    Public Function SettaTable(ByVal Table As Table, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "") As Table
        If Not String.IsNullOrEmpty(BackgroundColor) And Len(BackgroundColor) = 7 Then
            Table.BackgroundColor = New Color(Drawing.ColorTranslator.FromHtml(BackgroundColor))
        End If
        If SettaBordiTotali = True Then
            Table.Border = Border
            If Not String.IsNullOrEmpty(BorderColor) And Len(BorderColor) = 7 Then
                Table.BorderColor = New Color(Drawing.ColorTranslator.FromHtml(BorderColor))
            End If
        Else
            SetBorder(Table, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        End If
        Return Table
    End Function
    ''' <summary>Procedura per completare la riga della Tabella che si sta creando, il resto delle celle saranno vuote</summary>
    ''' <param name="Table">Definizione della Tabella sulla quale si stanno creando le righe.</param>
    Public Function CompletaRigaTabella(ByVal Table As Object) As Object
        If TypeOf Table Is PdfPTable Then
            CType(Table, PdfPTable).CompleteRow()
        End If
        Return Table
    End Function
    ''' <summary>Procedura per eliminare una riga dalla Tabella</summary>
    ''' <param name="Table">Definizione della Tabella su cui si vuole cancellare la riga.</param>
    ''' <param name="CancellaTutte">Definizione se cancellare tutte le righe presenti fino al momento dell'operazione all'interno della Table. (Valido solo per la Table e non per la PdfPTable)</param>
    ''' <param name="Riga">Se inserito cancella la riga in base al numero inserito, altrimenti cancella l'ultimo rigo inserito.</param>
    Public Function EliminaRigaTabella(ByVal Table As Object, ByVal CancellaTutte As Boolean, Optional ByVal Riga As Integer = 0) As Object
        If CancellaTutte = True Then
            If TypeOf Table Is Table Then
                CType(Table, Table).DeleteAllRows()
            End If
        Else
            If Riga = 0 Then
                If TypeOf Table Is Table Then
                    Table.DeleteLastRow()
                ElseIf TypeOf Table Is PdfPTable Then
                    Table.DeleteLastRow()
                End If
            Else
                If TypeOf Table Is Table Then
                    Table.DeleteRow(Riga)
                ElseIf TypeOf Table Is PdfPTable Then
                    Table.DeleteRow(Riga)
                End If
            End If
        End If
        Return Table
    End Function
    ''' <summary>Procedura per cancellare una colonna all'interno della Table (Valido solo per la Table e non per la PdfPTable)</summary>
    ''' <param name="Table">Definizione della Table a cui si vuole eliminare una Colonna.</param>
    ''' <param name="Colonna">Definizione del numero della Colonna che si vuole eliminare dalla Table.</param>
    Public Function EliminaColonnaTabella(ByVal Table As Table, ByVal Colonna As Integer) As Table
        Table.DeleteColumn(Colonna)
        Return Table
    End Function
    ''' <summary>Procedura per aggiungere una Table all'interno del Document</summary>
    ''' <param name="Obj">Definizione dell'Oggetto che si sta utilizzando.</param>
    ''' <param name="Table">Definizione della Tabella che si vuole inserire nel Document.</param>
    Public Function AggiungiTable(ByVal Obj As Object, ByVal Table As Object) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Table)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Table)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Table)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Table)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per inserire una PdfPCell all'interno della PdfPTable</summary>
    ''' <param name="Table">Definizione della Tabella a cui aggiungere la Cella.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nella Cella.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="PaddingTotale">Definizione se Settare i Padding tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Padding">Definizione del Padding della Cella.</param>
    ''' <param name="PaddingTop">Definizione del Padding Superiore della Cella.</param>
    ''' <param name="PaddingLeft">Definizione del Padding Sinistro della Cella.</param>
    ''' <param name="PaddingRight">Definizione del Padding Destro della Cella.</param>
    ''' <param name="PaddingBottom">Definizione del Padding Inferiore della Cella.</param>
    ''' <param name="Rotazione">Definizione in gradi della rotazione del Testo all'interno della Cella.</param>
    ''' <param name="AltezzaMinima">Definizione dell'Altezza minina che deve assumere la cella (e la riga) all'interno della Table.</param>
    ''' <param name="Larghezza">Definizione della Larghezza che deve assumere la Cella all'interno della Table.</param>
    Public Function InserisciPdfPCell(ByVal Table As PdfPTable, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Colspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal PaddingTotale As Boolean = False, Optional ByVal Padding As Single = 0, Optional PaddingTop As Single = 0, Optional PaddingLeft As Single = 0, Optional PaddingRight As Single = 0, Optional PaddingBottom As Single = 0, Optional Rotazione As Integer = 0, Optional AltezzaMinima As Single = 0, Optional ByVal Larghezza As Single = 0) As PdfPTable
        Dim Cell = New PdfPCell(New Phrase(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)))
        If Colspan > 1 Then Cell.Colspan = Colspan
        Cell.HorizontalAlignment = AllignamentoElementiPDF(Cell, AllignamentoOrizzontale)
        Cell.VerticalAlignment = AllignamentoElementiPDF(Cell, AllignamentoVerticale)
        If NoWrap Then Cell.NoWrap = True
        If SettaBordiTotali = True Then
            Cell.Border = Border
            If Not String.IsNullOrEmpty(BorderColor) And Len(BorderColor) = 7 Then
                Cell.BorderColor = New Color(Drawing.ColorTranslator.FromHtml(BorderColor))
            End If
        Else
            SetBorder(Cell, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        End If
        If Not String.IsNullOrEmpty(BackgroundColor) And Len(BackgroundColor) = 7 Then
            Cell.BackgroundColor = New Color(Drawing.ColorTranslator.FromHtml(BackgroundColor))
        End If
        If PaddingTotale = True Then
            Cell.Padding = Padding
        Else
            If PaddingTop <> 0 Then
                Cell.PaddingTop = PaddingTop
            End If
            If PaddingLeft <> 0 Then
                Cell.PaddingLeft = PaddingLeft
            End If
            If PaddingRight <> 0 Then
                Cell.PaddingRight = PaddingRight
            End If
            If PaddingRight <> 0 Then
                Cell.PaddingRight = PaddingRight
            End If
        End If
        If Rotazione <> 0 Then
            Cell.Rotation = Rotazione
        End If
        If AltezzaMinima <> 0 Then
            Cell.MinimumHeight = AltezzaMinima
        End If
        If Larghezza <> 0 Then
            Cell.Width = Larghezza
        End If
        AggiungiCell(Table, Cell)
        Return Table
    End Function
    ''' <summary>Procedura per creare una PdfPCell da utilizzare successivamente</summary>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    Public Function CreaPdfPCell(Optional ByVal Colspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False) As PdfPCell
        Dim Cell = New PdfPCell
        If Colspan > 1 Then Cell.Colspan = Colspan
        Cell.HorizontalAlignment = AllignamentoElementiPDF(Cell, AllignamentoOrizzontale)
        Cell.VerticalAlignment = AllignamentoElementiPDF(Cell, AllignamentoVerticale)
        If NoWrap Then Cell.NoWrap = True
        Return Cell
    End Function
    ''' <summary>Procedura per il settaggio di una PdfPCell</summary>
    ''' <param name="Cell">Definizione della Cella che si vuole Settare.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="PaddingTotale">Definizione se Settare i Padding tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Padding">Definizione del Padding della Cella.</param>
    ''' <param name="PaddingTop">Definizione del Padding Superiore della Cella.</param>
    ''' <param name="PaddingLeft">Definizione del Padding Sinistro della Cella.</param>
    ''' <param name="PaddingRight">Definizione del Padding Destro della Cella.</param>
    ''' <param name="PaddingBottom">Definizione del Padding Inferiore della Cella.</param>
    ''' <param name="Rotazione">Definizione in gradi della rotazione del Testo all'interno della Cella.</param>
    ''' <param name="AltezzaMinima">Definizione dell'Altezza minina che deve assumere la cella (e la riga) all'interno della Table.</param>
    ''' <param name="Larghezza">Definizione della Larghezza che deve assumere la Cella all'interno della Table.</param>
    Public Function SettaPdfPCell(ByVal Cell As PdfPCell, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal PaddingTotale As Boolean = False, Optional ByVal Padding As Single = 0, Optional PaddingTop As Single = 0, Optional PaddingLeft As Single = 0, Optional PaddingRight As Single = 0, Optional PaddingBottom As Single = 0, Optional Rotazione As Integer = 0, Optional AltezzaMinima As Single = 0, Optional ByVal Larghezza As Single = 0) As PdfPCell
        If SettaBordiTotali = True Then
            Cell.Border = Border
            If Not String.IsNullOrEmpty(BorderColor) And Len(BorderColor) = 7 Then
                Cell.BorderColor = New Color(Drawing.ColorTranslator.FromHtml(BorderColor))
            End If
        Else
            SetBorder(Cell, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        End If
        If Not String.IsNullOrEmpty(BackgroundColor) And Len(BackgroundColor) = 7 Then
            Cell.BackgroundColor = New Color(Drawing.ColorTranslator.FromHtml(BackgroundColor))
        End If
        If PaddingTotale = True Then
            Cell.Padding = Padding
        Else
            If PaddingTop <> 0 Then
                Cell.PaddingTop = PaddingTop
            End If
            If PaddingLeft <> 0 Then
                Cell.PaddingLeft = PaddingLeft
            End If
            If PaddingRight <> 0 Then
                Cell.PaddingRight = PaddingRight
            End If
            If PaddingRight <> 0 Then
                Cell.PaddingRight = PaddingRight
            End If
        End If
        If Rotazione <> 0 Then
            Cell.Rotation = Rotazione
        End If
        If AltezzaMinima <> 0 Then
            Cell.MinimumHeight = AltezzaMinima
        End If
        If Larghezza <> 0 Then
            Cell.Width = Larghezza
        End If
        Return Cell
    End Function
    ''' <summary>Procedura per l'inserimento di una Cell all'interno di una Table</summary>
    ''' <param name="Table">Definizione della Tabella a cui aggiungere la Cella.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nella Cella.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="Rowspan">Definizione del Numero di righe che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="LineeMax">Definizione del numero massimo di linee di testo che si devono trovare all'interno della Cella.</param>
    ''' <param name="Width">Definizione della larghezza della Cella all'interno della Table.</param>
    Public Function InserisciCell(ByVal Table As Table, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Colspan As Integer = 0, Optional ByVal Rowspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal LineeMax As Integer = 0, Optional ByVal Width As Single = 0) As Table
        Dim Cell = New Cell(New Phrase(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont)))
        If Colspan > 1 Then Cell.Colspan = Colspan
        If Rowspan > 1 Then Cell.Rowspan = Rowspan
        Cell.HorizontalAlignment = AllignamentoElementiPDF(Cell, AllignamentoOrizzontale)
        Cell.VerticalAlignment = AllignamentoElementiPDF(Cell, AllignamentoVerticale)
        If NoWrap Then Cell.NoWrap = True
        If SettaBordiTotali = True Then
            Cell.Border = Border
            If Not String.IsNullOrEmpty(BorderColor) And Len(BorderColor) = 7 Then
                Cell.BorderColor = New Color(Drawing.ColorTranslator.FromHtml(BorderColor))
            End If
        Else
            SetBorder(Cell, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        End If
        If Not String.IsNullOrEmpty(BackgroundColor) And Len(BackgroundColor) = 7 Then
            Cell.BackgroundColor = New Color(Drawing.ColorTranslator.FromHtml(BackgroundColor))
        End If
        If LineeMax <> 0 Then Cell.MaxLines = LineeMax
        If Width <> 0 Then Cell.Width = Width
        AggiungiCell(Table, Cell)
        Return Table
    End Function
    ''' <summary>Procedura per la creazione di una Cell da utilizzare successivamente</summary>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="Rowspan">Definizione del Numero di righe che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    Public Function CreaCell(Optional ByVal Colspan As Integer = 0, Optional ByVal Rowspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False) As Cell
        Dim Cell = New Cell
        If Colspan > 1 Then Cell.Colspan = Colspan
        If Rowspan > 1 Then Cell.Rowspan = Rowspan
        Cell.HorizontalAlignment = AllignamentoElementiPDF(Cell, AllignamentoOrizzontale)
        Cell.VerticalAlignment = AllignamentoElementiPDF(Cell, AllignamentoVerticale)
        If NoWrap Then Cell.NoWrap = True
        Return Cell
    End Function
    ''' <summary>Procedura per il settaggio una Cell</summary>
    ''' <param name="Cell">Definizione della Cell a cui si vogliono settare i parametri.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="LineeMax">Definizione del numero massimo di linee di testo che si devono trovare all'interno della Cella.</param>
    ''' <param name="Width">Definizione della larghezza della Cella all'interno della Table.</param>
    Public Function SettaCell(ByVal Cell As Cell, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal LineeMax As Integer = 0, Optional ByVal Width As Single = 0) As Cell
        If SettaBordiTotali = True Then
            Cell.Border = Border
            If Not String.IsNullOrEmpty(BorderColor) And Len(BorderColor) = 7 Then
                Cell.BorderColor = New Color(Drawing.ColorTranslator.FromHtml(BorderColor))
            End If
        Else
            SetBorder(Cell, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        End If
        If Not String.IsNullOrEmpty(BackgroundColor) And Len(BackgroundColor) = 7 Then
            Cell.BackgroundColor = New Color(Drawing.ColorTranslator.FromHtml(BackgroundColor))
        End If
        If LineeMax <> 0 Then Cell.MaxLines = LineeMax
        If Width <> 0 Then Cell.Width = Width
        Return Cell
    End Function
    ''' <summary>Procedura per aggiungere una Cella alla Table</summary>
    ''' <param name="Table">Definizione della Tabella sulla quale si vuole aggiungere la Cella</param>
    ''' <param name="Cell">Definizione della Cella che si vuole aggiungere alla Tabella</param>
    Public Function AggiungiCell(ByVal Table As Object, ByVal Cell As Object) As Object
        If TypeOf Table Is Table And TypeOf Cell Is Cell Then
            CType(Table, Table).AddCell(CType(Cell, Cell))
        ElseIf TypeOf Table Is PdfPTable And TypeOf Cell Is PdfPCell Then
            CType(Table, PdfPTable).AddCell(CType(Cell, PdfPCell))
        End If
        Return Table
    End Function
    ''' <summary>Procedura per l'inserimento di una Colonna all'interno di una Table</summary>
    ''' <param name="Table">Definizione della Tabella a cui aggiungere Colonne.</param>
    ''' <param name="NumeroColonne">Definizione del Numero di Colonne da Aggiungere alla Tabella</param>
    Public Function AggiungiColonnaTable(ByVal Table As Table, Optional ByVal NumeroColonne As Integer = 1) As Table
        CType(Table, Table).AddColumns(NumeroColonne)
        Return Table
    End Function
    ''' <summary>Procedura per la Creazione di una List</summary>
    ''' <param name="Tipo">Definizione del Tipo di List da creare.</param>
    ''' <param name="ListaAlfabetica">Definizione se il Tipo di List deve essere Alfabetica. (True: SI, False: NO)</param>
    ''' <param name="OffsetLaterale">Definizione dello spazio tra il simbolo e il testo della List.</param>
    ''' <param name="SpazioSinistra">Definizione dello Spazio a sinista prima della List.</param>
    ''' <param name="SpazioDestra">Definizione dello Spazio a destra dopo la List.</param>
    Public Function CreaList(ByVal Tipo As TipoList, Optional ByVal ListaAlfabetica As Boolean = False, Optional ByVal OffsetLaterale As Single = 15, Optional SpazioSinistra As Single = 0, Optional SpazioDestra As Single = 0) As List
        Select Case Tipo
            Case 0
                CreaList = New List(ORDERED, ListaAlfabetica, OffsetLaterale)
            Case 1
                CreaList = New List(UNORDERED, ListaAlfabetica, OffsetLaterale)
            Case Else
                CreaList = New List(ORDERED, ListaAlfabetica, OffsetLaterale)
        End Select
        If SpazioSinistra <> 0 Then CreaList.IndentationLeft = SpazioSinistra
        If SpazioDestra <> 0 Then CreaList.IndentationRight = SpazioDestra
        Return CreaList
    End Function
    ''' <summary>Procedura per la creazione di una List di Tipo Greco/Romana</summary>
    ''' <param name="Tipo">Definizione del Tipo di List da creare.</param>
    ''' <param name="OffsetLaterale">Definizione dello spazio tra il simbolo e il testo della List.</param>
    ''' <param name="SpazioSinistra">Definizione dello Spazio a sinista prima della List.</param>
    ''' <param name="SpazioDestra">Definizione dello Spazio a destra dopo la List.</param>
    Public Function CreaListGrecoRomana(ByVal Tipo As TipoListGrecoRomana, Optional ByVal OffsetLaterale As Integer = 15, Optional SpazioSinistra As Single = 0, Optional SpazioDestra As Single = 0) As List
        Select Case Tipo
            Case 0
                CreaListGrecoRomana = New RomanList(OffsetLaterale)
            Case 1
                CreaListGrecoRomana = New GreekList(OffsetLaterale)
            Case Else
                CreaListGrecoRomana = New RomanList(OffsetLaterale)
        End Select
        If SpazioSinistra <> 0 Then CreaListGrecoRomana.IndentationLeft = SpazioSinistra
        If SpazioDestra <> 0 Then CreaListGrecoRomana.IndentationRight = SpazioDestra
        Return CreaListGrecoRomana
    End Function
    ''' <summary>Procedura per la creazione di una List con il puntatore espresso con un simbolo</summary>
    ''' <param name="Simbolo">Definizione del simbolo del puntatore della List.</param>
    ''' <param name="OffsetLaterale">Definizione dello spazio tra il simbolo e il testo della List.</param>
    ''' <param name="SpazioSinistra">Definizione dello Spazio a sinista prima della List.</param>
    ''' <param name="SpazioDestra">Definizione dello Spazio a destra dopo la List.</param>
    Public Function CreaListSimbolo(ByVal Simbolo As SimboloListSimbolo, Optional ByVal OffsetLaterale As Integer = 15, Optional SpazioSinistra As Single = 0, Optional SpazioDestra As Single = 0) As List
        CreaListSimbolo = New ZapfDingbatsList(Simbolo, OffsetLaterale)
        If SpazioSinistra <> 0 Then CreaListSimbolo.IndentationLeft = SpazioSinistra
        If SpazioDestra <> 0 Then CreaListSimbolo.IndentationRight = SpazioDestra
        Return CreaListSimbolo
    End Function
    ''' <summary>Procedura per la creazione di una List Personalizzata</summary>
    ''' <param name="Simbolo">Definizione del Simbolo della List.</param>
    ''' <param name="OffsetLaterale">Definizione dello spazio tra il simbolo e il testo della List.</param>
    ''' <param name="SpazioSinistra">Definizione dello Spazio a sinista prima della List.</param>
    ''' <param name="SpazioDestra">Definizione dello Spazio a destra dopo la List.</param>
    Public Function CreaListaPersonalizzata(ByVal Simbolo As String, Optional ByVal OffsetLaterale As Integer = 15, Optional SpazioSinistra As Single = 0, Optional SpazioDestra As Single = 0) As List
        CreaListaPersonalizzata = New List(UNORDERED, False, OffsetLaterale)
        CreaListaPersonalizzata.SetListSymbol(Simbolo)
        If SpazioSinistra <> 0 Then CreaListaPersonalizzata.IndentationLeft = SpazioSinistra
        If SpazioDestra <> 0 Then CreaListaPersonalizzata.IndentationRight = SpazioDestra
        Return CreaListaPersonalizzata
    End Function
    ''' <summary>Procedura per la Creazione di un Nodo nella List Semplice/GrecoRomana/Simbolo/Personalizzata</summary>
    ''' <param name="List">Definizione della List alla quale inserire il nodo.</param>
    ''' <param name="Testo">Definizione del testo del Nodo della List.</param>
    Public Function AggiungiNodoListSemplice(ByVal List As Object, ByVal Testo As String) As Object
        If TypeOf List Is List Then
            CType(List, List).Add(Testo)
        ElseIf TypeOf List Is RomanList Then
            CType(List, RomanList).Add(Testo)
        ElseIf TypeOf List Is GreekList Then
            CType(List, GreekList).Add(Testo)
        ElseIf TypeOf List Is ZapfDingbatsList Then
            CType(List, ZapfDingbatsList).Add(Testo)
        End If
        Return List
    End Function
    ''' <summary>Procedura per la Creazione di un Nodo nella List di tipo ListItem</summary>
    ''' <param name="List">Definizione della List alla quale inserire il nodo.</param>
    ''' <param name="Testo">Definizione del Testo che si vuole inserire nel Chunk.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    Public Function AggiungiNodoListItem(ByVal List As Object, ByVal Testo As String, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero) As List
        If TypeOf List Is List Then
            CType(List, List).Add(New ListItem(New Chunk(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))))
        ElseIf TypeOf List Is RomanList Then
            CType(List, RomanList).Add(New ListItem(New Chunk(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))))
        ElseIf TypeOf List Is GreekList Then
            CType(List, GreekList).Add(New ListItem(New Chunk(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))))
        ElseIf TypeOf List Is ZapfDingbatsList Then
            CType(List, ZapfDingbatsList).Add(New ListItem(New Chunk(Testo, ImpostaFont(ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont))))
        End If
        Return List
    End Function
    ''' <summary>Procedura per l'inserimento di una List all'interno di un'oggetto</summary>
    ''' <param name="Obj">Definizione dell'oggetto a cui si vuole aggiungere la List.</param>
    ''' <param name="List">Definizione della List che si vuole aggiungere all'oggetto.</param>
    Public Function AggiungiList(ByVal Obj As Object, List As Object) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(List)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(List)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(List)
        ElseIf TypeOf Obj Is List Then
            CType(Obj, List).Add(List)
        ElseIf TypeOf Obj Is GreekList Then
            CType(Obj, GreekList).Add(List)
        ElseIf TypeOf Obj Is RomanList Then
            CType(Obj, RomanList).Add(List)
        ElseIf TypeOf Obj Is ZapfDingbatsList Then
            CType(Obj, ZapfDingbatsList).Add(List)
        ElseIf TypeOf Obj Is ListItem Then
            CType(Obj, ListItem).Add(List)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(List)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per la Creazione di un'Immagine</summary>
    ''' <param name="PercorsoImmagine">Definizione del Percorso del File Image sul Server.</param>
    ''' <param name="WrapText">Definizione se l'immagine deve essere allignamento con un testo al suo fianco.</param>
    ''' <param name="Allignamento">Definizione dell'Allignamento dell'Immagine all'interno dell'Oggetto.</param>
    Public Function CreaImmagine(ByVal PercorsoImmagine As String, Optional ByVal WrapText As Boolean = False, Optional ByVal Allignamento As AllignamentoElementi = AllignamentoElementi.Left) As Image
        CreaImmagine = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(PercorsoImmagine))
        If WrapText = True Then
            CreaImmagine.Alignment = Image.TEXTWRAP & AllignamentoElementiPDF(CreaImmagine, Allignamento)
        Else
            CreaImmagine.Alignment = AllignamentoElementiPDF(CreaImmagine, Allignamento)
        End If
        Return CreaImmagine
    End Function
    ''' <summary>Procedura per la scala di un'Immagine in Percentuale (%)</summary>
    ''' <param name="Image">Definizione dell'Immagine da scalare.</param>
    ''' <param name="Percentuale">Definizione della percentuale di scala.</param>
    Public Function ScalaImagePercentuale(ByVal Image As Image, ByVal Percentuale As Single) As Image
        Image.ScalePercent(Percentuale)
        Return Image
    End Function
    ''' <summary>Procedura per la scala di un'Immagine in Fit</summary>
    ''' <param name="Image">Definizione dell'Immagine da scalare.</param>
    ''' <param name="Width">Definizione della Larghezza di Scala.</param>
    ''' <param name="Height">Definizione dell'Altezza di Scala.</param>
    Public Function ScalaImmagineFit(ByVal Image As Image, ByVal Width As Single, ByVal Height As Single) As Image
        Image.ScaleToFit(Width, Height)
        Return Image
    End Function
    ''' <summary>Procedura per ridimensionare un'Immagine in pixel</summary>
    ''' <param name="Image">Definizione dell'Immagine da ridimensionare.</param>
    ''' <param name="Width">Definizione della Larghezza che deve adottare l'Immagine.</param>
    ''' <param name="Height">Definizione dell'Altezza che deve adottare l'Immagine</param>
    Public Function RidimensionaImage(ByVal Image As Image, Optional ByVal Width As Single = 0, Optional ByVal Height As Single = 0) As Image
        If Width <> 0 Then Image.ScaleAbsoluteWidth(Width)
        If Height <> 0 Then Image.ScaleAbsoluteHeight(Height)
        Return Image
    End Function
    ''' <summary>Procedura per settare le posizioni assolute dell'Immagine</summary>
    ''' <param name="Image">Definizione dell'Immagine da settare.</param>
    ''' <param name="PosOrizzontale">Definizione della Posizione Orizzontale dell'Immagine.</param>
    ''' <param name="PosVerticale">Definizione della Posizione Verticale dell'Immagine.</param>
    Public Function SettaPosAssoluteImage(ByVal Image As Image, ByVal PosOrizzontale As Single, ByVal PosVerticale As Single) As Image
        Image.SetAbsolutePosition(PosOrizzontale, PosVerticale)
        Return Image
    End Function
    ''' <summary>Procedura per settare la Trasparenza dell'Immagine</summary>
    ''' <param name="Image">Definizione dell'Immagine su cui settare la Trasparenza.</param>
    ''' <param name="Valore">Definizione del Valore della Trasparenza.</param>
    ''' <param name="Indice">Definizione dell'Indice di Trasparenza. N.B. Deve essere minore rispetto al Valore (Se Maggiore/Uguale sarà settato a 0)</param>
    Public Function SettaTrasparenzaImage(ByVal Image As Image, Valore As Object, Optional ByVal Indice As Integer = 0) As Image
        If Indice >= Valore Then
            Indice = 0
        End If
        Image.Transparency.SetValue(Valore, Indice)
        Return Image
    End Function
    ''' <summary>Procedura per Settare i Bordi dell'Immagine</summary>
    ''' <param name="Image">Definizione dell'Immagine da settare.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo dell'Immagine.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo dell'Immagine in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    Public Function SettaBordiImage(ByVal Image As Image, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0) As Image
        If SettaBordiTotali = True Then
            Image.Border = Rectangle.BOX
            If Not String.IsNullOrEmpty(BorderColor) And Len(BorderColor) = 7 Then
                Image.BorderColor = New Color(Drawing.ColorTranslator.FromHtml(BorderColor))
            End If
            Image.BorderWidth = Border
        Else
            Image.Border = Rectangle.BOX
            SetBorder(Image, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth)
        End If
        Return Image
    End Function
    ''' <summary>Procedura per il settaggio delle impostazioni dell'Immagine</summary>
    ''' <param name="Image">Definizione dell'Immagine da settare.</param>
    ''' <param name="OffSetTop">Definizione dell'Offset Superiore dell'Immagine.</param>
    ''' <param name="OffsetSinistra">Definizione dell'Offset Sinistro dell'Immagine.</param>
    ''' <param name="OffsetDestra">Definizione dell'Offset Destro dell'Immagine.</param>
    ''' <param name="OffsetBottom">Definizione dell'Offset Inferiore dell'Immagine.</param>
    ''' <param name="Rotazione">Definizione dei gradi di Rotazione dell'Immagine.</param>
    ''' <param name="GrayFill">Definizione del GrayFill dell'Immagine.</param>
    ''' <param name="SpacingAfter">Definizione dello Spazio vuoto dopo l'Immagine.</param>
    ''' <param name="SpacingBefore">Definizione dello Spazio vuto prima dell'Immagine.</param>
    ''' <param name="IndentationLeft">Definizione dell'incavatura sinistra dell'Immagine.</param>
    ''' <param name="IndentationRight">Definizione dell'incavatura destra dell'Immagine.</param>
    Public Function SettaImmagine(ByVal Image As Image, Optional ByVal OffSetTop As Single = 0, Optional ByVal OffsetSinistra As Single = 0, Optional ByVal OffsetDestra As Single = 0, Optional ByVal OffsetBottom As Single = 0, Optional ByVal Rotazione As Single = 0, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional GrayFill As Single = 0, Optional SpacingAfter As Single = 0, Optional ByVal SpacingBefore As Single = 0, Optional ByVal IndentationLeft As Single = 0, Optional ByVal IndentationRight As Single = 0) As Image
        If OffSetTop <> 0 Then Image.Top = OffSetTop
        If OffsetSinistra <> 0 Then Image.Left = OffsetSinistra
        If OffsetDestra <> 0 Then Image.Right = OffsetDestra
        If OffsetBottom <> 0 Then Image.Bottom = OffsetBottom
        If Rotazione <> 0 Then Image.InitialRotation = Rotazione
        If SpacingAfter <> 0 Then Image.SpacingAfter = SpacingAfter
        If SpacingBefore <> 0 Then Image.SpacingBefore = SpacingBefore
        If IndentationLeft <> 0 Then Image.IndentationLeft = IndentationLeft
        If IndentationRight <> 0 Then Image.IndentationRight = IndentationRight
        If GrayFill <> 0 Then Image.GrayFill = GrayFill
        Return Image
    End Function
    ''' <summary>Procedura per l'inserimento di un'immagine all'interno di un'oggetto</summary>
    ''' <param name="Obj">Definizione dell'oggetto in cui inserire l'Immagine.</param>
    ''' <param name="Image">Definizione dell'Immagine da inserire all'interno dell'Oggetto.</param>
    Public Function AggiungiImmagine(ByVal Obj As Object, ByVal Image As Image) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Image)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Image)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Image)
        ElseIf TypeOf Obj Is PdfPCell Then
            CType(Obj, PdfPCell).AddElement(Image)
        ElseIf TypeOf Obj Is Cell Then
            CType(Obj, Cell).AddElement(Image)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Image)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per l'inserimento di una Linea di Separazione all'interno di un'oggetto</summary>
    ''' <param name="Obj">Definizione dell'Oggetto in cui inserire la Linea di Separazione.</param>
    ''' <param name="Spessore">Definizione dello spessore della Linea di Separazione.</param>
    ''' <param name="Width">Definizione della Percentuale di Lunghezza della Linea di Separazione rispetto all'Oggetto.</param>
    ''' <param name="Colore">Definizione del Colore della Linea di Separazione.</param>
    ''' <param name="Allignamento">Definizione dell'Allignamento della Linea di Separazione.</param>
    ''' <param name="Offset">Definizione dell'Offset della Linea di Separazione.</param>
    Public Function InserisciLineaSeparazione(ByVal Obj As Object, ByVal Spessore As Single, ByVal Width As Single, Optional ByVal Colore As Colori = Colori.Nero, Optional ByVal Allignamento As AllignamentoElementi = AllignamentoElementi.Center, Optional ByVal Offset As Single = 0) As Object
        Dim Linea = New LineSeparator(Spessore, Width, SetColor(Colore), AllignamentoElementiPDF(Me, Allignamento), Offset)
        AggiungiLineaSeparazione(Obj, Linea)
        Return Obj
    End Function
    ''' <summary>Procedura per l'inserimento di una Linea di Separazione all'interno di un'oggetto</summary>
    ''' <param name="Obj">Definizione dell'oggetto in cui inserire la Linea di Separazione.</param>
    ''' <param name="Linea">Definizione della Linea da inserire nell'oggetto.</param>
    Private Function AggiungiLineaSeparazione(ByVal Obj As Object, ByVal Linea As LineSeparator) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(Linea)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(Linea)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(Linea)
        ElseIf TypeOf Obj Is MultiColumnText Then
            CType(Obj, MultiColumnText).AddElement(Linea)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per la creazione delle colonne di Layout delle Pagine</summary>
    ''' <param name="Height">Definizione dell'altezza della Colonna.</param>
    ''' <param name="Top">Definizione del Top della Colonna. (N.B. Il valore viene settato solo se prima e stata inserita un'altezza valida)</param>
    Public Function CreaColonnaLayout(Optional ByVal Height As Single = 0, Optional ByVal Top As Single = 0) As MultiColumnText
        If Top = 0 And Height = 0 Then
            CreaColonnaLayout = New MultiColumnText()
        ElseIf Top = 0 And Height <> 0 Then
            CreaColonnaLayout = New MultiColumnText(Height)
        Else
            CreaColonnaLayout = New MultiColumnText(Top, Height)
        End If
        Return CreaColonnaLayout
    End Function
    ''' <summary>Procedura per l'inserimento di una Colonna regolare all'interno di una MultiColonna</summary>
    ''' <param name="MultiColumn">Definizione della MultiColonna su cui aggiungere una o più Colonne.</param>
    ''' <param name="Width">Definizione della larghezza della Colonna.</param>
    ''' <param name="NumeroColonne">Definizione del Numero di Colonne da inserire.</param>
    ''' <param name="SpazioSinistra">Definizione dello Spazio a Sinistra della Colonna</param>
    ''' <param name="SpazioDestro">Definizione dello Spazio a Destra della Colonna.</param>
    Public Function AggiungiColonnaRegolareLayout(ByVal MultiColumn As MultiColumnText, ByVal Width As Single, ByVal NumeroColonne As Integer, Optional ByVal SpazioSinistra As Single = 0, Optional ByVal SpazioDestro As Single = 0) As MultiColumnText
        MultiColumn.AddRegularColumns(SpazioSinistra, SpazioDestro, Width, NumeroColonne)
        Return MultiColumn
    End Function
    ''' <summary>Procedura per l'inserimento di una Colonna semplice all'interno di una MultiColonna</summary>
    ''' <param name="MultiColumn">Definizione della MultiColonna su cui aggiungere una o più Colonne.</param>
    ''' <param name="SpazioSinistra">Definizione dello Spazio a Sinistra della Colonna</param>
    ''' <param name="SpazioDestro">Definizione dello Spazio a Destra della Colonna.</param>
    Public Function AggiungiColonnaSempliceLayout(ByVal MultiColumn As MultiColumnText, Optional ByVal SpazioSinistra As Single = 0, Optional ByVal SpazioDestro As Single = 0) As MultiColumnText
        MultiColumn.AddSimpleColumn(SpazioSinistra, SpazioDestro)
        Return MultiColumn
    End Function
    ''' <summary>Procedura per l'avanzamento alla colonna successiva della MultiColumn</summary>
    ''' <param name="MultiColumn">Definizione della MultiColumn in cui avanzare di Colonna.</param>
    Public Function AvanzaColonnaLayout(ByVal MultiColumn As MultiColumnText) As MultiColumnText
        MultiColumn.NextColumn()
        Return MultiColumn
    End Function
    ''' <summary>Procedura per l'inserimento di una MultiColonna all'interno di un'Oggetto</summary>
    ''' <param name="Obj">Definizione dell'oggetto in cui inserire la MultiColonna.</param>
    ''' <param name="MultiColumn">Definizione della MultiColonna da inserire nell'Oggetto.</param>
    Public Function AggiungiMultiColonna(ByVal Obj As Object, ByVal MultiColumn As MultiColumnText) As Object
        If TypeOf Obj Is Document Then
            CType(Obj, Document).Add(MultiColumn)
        ElseIf TypeOf Obj Is Chapter Then
            CType(Obj, Chapter).Add(MultiColumn)
        ElseIf TypeOf Obj Is Section Then
            CType(Obj, Section).Add(MultiColumn)
        End If
        Return Obj
    End Function
    ''' <summary>Procedura per inserire un Chart all'interno di un'Oggetto</summary>
    ''' <param name="Obj">Definizione dell'Oggetto in cui inserire il Chart.</param>
    ''' <param name="Charts">Definizione del Chart da inserire nell'Oggetto.</param>
    'Public Function InserisciChart(ByVal Obj As Object, ByVal Charts As Chart) As Object
    '    Dim chartimage = CreaImmagineChart(Charts)
    '    AggiungiImmagine(Obj, chartimage)
    '    Return Obj
    'End Function
    ' ''' <summary>Procedura per la creazione dell'Immagine del Chart</summary>
    ' ''' <param name="Charts">Definizione del Chart che deve formare la nuova Immagine.</param>
    'Public Function CreaImmagineChart(ByVal Charts As Chart) As Image
    '    Dim stream As MemoryStream = New MemoryStream
    '    Charts.SaveImage(stream, ChartImageFormat.Png)
    '    Dim chartImage As Image = Image.GetInstance(stream.GetBuffer)
    '    Return chartImage
    'End Function
    ''' <summary>Procedura per aggiungere un ChartImage all'interno di un'Oggetto.</summary>
    ''' <param name="Obj">Definizione dell'Oggetto in cui inserire la ChartImage.</param>
    ''' <param name="chartimage">Definizione della ChartImage che deve essere inserita nell'Oggetto.</param>
    Public Function AggiungiChart(ByVal Obj As Object, ByVal chartimage As Image) As Object
        AggiungiImmagine(Obj, chartimage)
        Return Obj
    End Function
    'Definizione delle Funzioni per l'automatizzazione di procedura creazione File PDF
    ''' <summary>Procedura per la stampa di un DataTable all'interno di un file PDF</summary>
    ''' <param name="Document">Definizione del Document su cui eseguire la stampa della Table.</param>
    ''' <param name="dt">Definizione del sorgente DataTable su cui effettuare la Stampa.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="PaddingTotale">Definizione se Settare i Padding tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Padding">Definizione del Padding della Cella.</param>
    ''' <param name="PaddingTop">Definizione del Padding Superiore della Cella.</param>
    ''' <param name="PaddingLeft">Definizione del Padding Sinistro della Cella.</param>
    ''' <param name="PaddingRight">Definizione del Padding Destro della Cella.</param>
    ''' <param name="PaddingBottom">Definizione del Padding Inferiore della Cella.</param>
    ''' <param name="Rotazione">Definizione in gradi della rotazione del Testo all'interno della Cella.</param>
    ''' <param name="AltezzaMinima">Definizione dell'Altezza minina che deve assumere la cella (e la riga) all'interno della Table.</param>
    ''' <param name="Larghezza">Definizione della Larghezza che deve assumere la Cella all'interno della Table.</param>
    Public Function CreaPDFdaDataTable(ByVal Document As Document, ByVal dt As Data.DataTable, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Colspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal PaddingTotale As Boolean = False, Optional ByVal Padding As Single = 0, Optional ByVal PaddingTop As Single = 0, Optional PaddingLeft As Single = 0, Optional PaddingRight As Single = 0, Optional PaddingBottom As Single = 0, Optional Rotazione As Integer = 0, Optional AltezzaMinima As Single = 0, Optional ByVal Larghezza As Single = 0, Optional RepeatHeader As Boolean = False) As Document
        RenameColDataTable(dt)
        Dim NumeroColonneDT As Integer = dt.Columns.Count
        Dim IndiceColonne As Integer = 1
        Dim Table = CreaPdfPTable(dt.Columns.Count, 100, PDFSiSol.AllignamentoElementi.Center)
		If RepeatHeader = True Then Table.HeaderRows = 1
        For j = 0 To NumeroColonneDT - 1 Step 1
            InserisciPdfPCell(Table, dt.Columns.Item(j).ColumnName, PDFSiSol.SetValori.PredefinitiTableHeader, , , FontStylePDF.Bold, , , AllignamentoOrizzontale)
        Next
        For Each riga As Data.DataRow In dt.Rows
            For IndiceColonne = 0 To NumeroColonneDT - 1
                InserisciPdfPCell(Table, Replace(par.IfNull(riga.Item(IndiceColonne), ""), "&nbsp;", ""), ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont, Colspan, AllignamentoOrizzontale, AllignamentoVerticale, NoWrap, SettaBordiTotali, Border, BorderColor, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth, BackgroundColor, PaddingTotale, Padding, PaddingTop, PaddingLeft, PaddingRight, PaddingBottom, Rotazione, AltezzaMinima, Larghezza)
            Next
        Next
        AggiungiTable(Document, Table)
        Return Document
    End Function
    ''' <summary>Procedura per la modifica del Titolo delle Colonne della DataTable</summary>
    ''' <param name="dt">Definizione della DataTable su cui effettuare la procedura di modifica titolo.</param>
    Public Function RenameColDataTable(ByRef dt As Data.DataTable) As Data.DataTable
        For j = 0 To dt.Columns.Count - 1 Step 1
            dt.Columns.Item(j).ColumnName = dt.Columns.Item(j).ColumnName.Replace("_", " ")
        Next
        Return dt
    End Function
    ''' <summary>Procedura per la stampa di un DataGrid all'interno di un file PDF</summary>
    ''' <param name="Document">Definizione del Document su cui eseguire la stampa della Table.</param>
    ''' <param name="DataGrid">Definizione del sorgente DataGrid su cui effettuare la Stampa.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="PaddingTotale">Definizione se Settare i Padding tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Padding">Definizione del Padding della Cella.</param>
    ''' <param name="PaddingTop">Definizione del Padding Superiore della Cella.</param>
    ''' <param name="PaddingLeft">Definizione del Padding Sinistro della Cella.</param>
    ''' <param name="PaddingRight">Definizione del Padding Destro della Cella.</param>
    ''' <param name="PaddingBottom">Definizione del Padding Inferiore della Cella.</param>
    ''' <param name="Rotazione">Definizione in gradi della rotazione del Testo all'interno della Cella.</param>
    ''' <param name="AltezzaMinima">Definizione dell'Altezza minina che deve assumere la cella (e la riga) all'interno della Table.</param>
    ''' <param name="Larghezza">Definizione della Larghezza che deve assumere la Cella all'interno della Table.</param>
    Public Function CreaPdfdaDataGrid(ByVal Document As Document, ByVal DataGrid As DataGrid, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Colspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal PaddingTotale As Boolean = False, Optional ByVal Padding As Single = 0, Optional ByVal PaddingTop As Single = 0, Optional PaddingLeft As Single = 0, Optional PaddingRight As Single = 0, Optional PaddingBottom As Single = 0, Optional Rotazione As Integer = 0, Optional AltezzaMinima As Single = 0, Optional ByVal Larghezza As Single = 0) As Document
        Dim NumeroColonneDatagrid As Integer = DataGrid.Columns.Count
        Dim NumeroColonneVisibiliDatagrid As Integer = 0
        For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
            If DataGrid.Columns.Item(indiceColonna).Visible = True Then
                NumeroColonneVisibiliDatagrid = NumeroColonneVisibiliDatagrid + 1
            End If
        Next
        Dim Table = CreaPdfPTable(NumeroColonneVisibiliDatagrid, 100, PDFSiSol.AllignamentoElementi.Center)
        For j = 0 To NumeroColonneDatagrid - 1 Step 1
            If DataGrid.Columns.Item(j).Visible = True Then
                InserisciPdfPCell(Table, DataGrid.Columns.Item(j).HeaderText, PDFSiSol.SetValori.PredefinitiTableHeader, , , FontStylePDF.Bold, , , AllignamentoOrizzontale)
            End If
        Next
        For Each Items As DataGridItem In DataGrid.Items
            For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                If DataGrid.Columns.Item(IndiceColonne).Visible = True Then
                    InserisciPdfPCell(Table, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont, Colspan, AllignamentoOrizzontale, AllignamentoVerticale, NoWrap, SettaBordiTotali, Border, BorderColor, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth, BackgroundColor, PaddingTotale, Padding, PaddingTop, PaddingLeft, PaddingRight, PaddingBottom, Rotazione, AltezzaMinima, Larghezza)
                End If
            Next
        Next
        AggiungiTable(Document, Table)
        Return Document
    End Function
    ''' <summary>Procedura per la stampa di un DataGrid con la base dati di una DataTable all'interno di un file PDF</summary>
    ''' <param name="Document">Definizione del Document su cui eseguire la stampa della Table.</param>
    ''' <param name="DataGrid">Definizione del sorgente DataGrid su cui effettuare la Stampa.</param>
    ''' <param name="dt">Definizione del sorgente DataTable su cui effettuare la Stampa.</param>
    ''' <param name="ValoriFont">Definizione dei Valori che devono formare il Font. (Predefiniti: Setta il Font con i valori che sono stati definiti con l'apposita funzione, Personalizzati: Vanno settati i valori altrimenti il font non assumera la forma desiderata)</param>
    ''' <param name="FamigliaFont">Definizione del carattere che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="DimensioneFont">Definizione della dimensione che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="StileFont">Definizione dello stile che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="ColoreFont">Definizione del colore che si vuole utilizzare per impostare il Font.</param>
    ''' <param name="Colspan">Definizione del Numero di colonne che devono formare la Cella.</param>
    ''' <param name="AllignamentoOrizzontale">Definizione dell'Allignamento Orizzontale all'interno della Cella.</param>
    ''' <param name="AllignamentoVerticale">Definizione dell'Allignamento Verticale all'interno della Cella.</param>
    ''' <param name="NoWrap">Definizione in valore Booleano se il testo all'interno della cella deve andare a capo.</param>
    ''' <param name="SettaBordiTotali">Definizione se Settare i Bordi tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Border">Definizione dello spessore del bordo della Cella.</param>
    ''' <param name="BorderColor">Definizione del Colore del bordo della Cella in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopColor">Definizione del Colore Top in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderTopWidth">Definizione dello spessore del bordo Top in px.</param>
    ''' <param name="BorderLeftColor">Definizione del Colore Left in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderLeftWidth">Definizione dello spessore del bordo Left in px.</param>
    ''' <param name="BorderRightColor">Definizione del Colore Right in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderRightWidth">Definizione dello spessore del bordo Right in px.</param>
    ''' <param name="BorderBottomColor">Definizione del Colore Bottom in Esadecimale. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="BorderBottomWidth">Definizione dello spessore del bordo Bottom in px.</param>
    ''' <param name="BackgroundColor">Definizione del Colore di Sfondo della Cella. (Ricordati di usare il '#' davanti alle cifre del Colore)</param>
    ''' <param name="PaddingTotale">Definizione se Settare i Padding tutti insieme o uno alla volta. (True: Tutti, False: Uno alla Volta)</param>
    ''' <param name="Padding">Definizione del Padding della Cella.</param>
    ''' <param name="PaddingTop">Definizione del Padding Superiore della Cella.</param>
    ''' <param name="PaddingLeft">Definizione del Padding Sinistro della Cella.</param>
    ''' <param name="PaddingRight">Definizione del Padding Destro della Cella.</param>
    ''' <param name="PaddingBottom">Definizione del Padding Inferiore della Cella.</param>
    ''' <param name="Rotazione">Definizione in gradi della rotazione del Testo all'interno della Cella.</param>
    ''' <param name="AltezzaMinima">Definizione dell'Altezza minina che deve assumere la cella (e la riga) all'interno della Table.</param>
    ''' <param name="Larghezza">Definizione della Larghezza che deve assumere la Cella all'interno della Table.</param>
    Public Function CreaPdfdaDataGridWithDT(ByVal Document As Document, ByVal DataGrid As DataGrid, ByVal dt As Data.DataTable, ByVal ValoriFont As SetValori, Optional ByVal FamigliaFont As FontPDF = FontPDF.Times, Optional ByVal DimensioneFont As Single = 12, Optional ByVal StileFont As FontStylePDF = FontStylePDF.Normale, Optional ByVal ColoreFont As FontColorPDF = FontColorPDF.Nero, Optional ByVal Colspan As Integer = 0, Optional AllignamentoOrizzontale As AllignamentoElementi = AllignamentoElementi.Left, Optional AllignamentoVerticale As AllignamentoElementi = AllignamentoElementi.Middle, Optional NoWrap As Boolean = False, Optional ByVal SettaBordiTotali As Boolean = False, Optional ByVal Border As Single = 0, Optional ByVal BorderColor As String = "", Optional ByVal BorderTopColor As String = "", Optional ByVal BorderTopWidth As Integer = 0, Optional ByVal BorderLeftColor As String = "", Optional ByVal BorderLeftWidth As Integer = 0, Optional ByVal BorderRightColor As String = "", Optional ByVal BorderRightWidth As Integer = 0, Optional ByVal BorderBottomColor As String = "", Optional ByVal BorderBottomWidth As Integer = 0, Optional ByVal BackgroundColor As String = "", Optional ByVal PaddingTotale As Boolean = False, Optional ByVal Padding As Single = 0, Optional ByVal PaddingTop As Single = 0, Optional PaddingLeft As Single = 0, Optional PaddingRight As Single = 0, Optional PaddingBottom As Single = 0, Optional Rotazione As Integer = 0, Optional AltezzaMinima As Single = 0, Optional ByVal Larghezza As Single = 0) As Document
        Dim NumeroColonneDatagrid As Integer = DataGrid.Columns.Count
        Dim NumeroColonneVisibiliDatagrid As Integer = 0
        For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
            If DataGrid.Columns.Item(indiceColonna).Visible = True Then
                NumeroColonneVisibiliDatagrid = NumeroColonneVisibiliDatagrid + 1
            End If
        Next
        Dim Table = CreaPdfPTable(NumeroColonneVisibiliDatagrid, 100, PDFSiSol.AllignamentoElementi.Center)
        For j = 0 To NumeroColonneDatagrid - 1 Step 1
            If DataGrid.Columns.Item(j).Visible = True Then
                InserisciPdfPCell(Table, DataGrid.Columns.Item(j).HeaderText, PDFSiSol.SetValori.PredefinitiTableHeader, , , FontStylePDF.Bold, , , AllignamentoOrizzontale)
            End If
        Next
        For Each riga As Data.DataRow In dt.Rows
            For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                If DataGrid.Columns.Item(IndiceColonne).Visible = True Then
                    InserisciPdfPCell(Table, Replace(par.IfNull(riga.Item(IndiceColonne), ""), "&nbsp;", ""), ValoriFont, FamigliaFont, DimensioneFont, StileFont, ColoreFont, Colspan, AllignamentoOrizzontale, AllignamentoVerticale, NoWrap, SettaBordiTotali, Border, BorderColor, BorderTopColor, BorderTopWidth, BorderLeftColor, BorderLeftWidth, BorderRightColor, BorderRightWidth, BorderBottomColor, BorderBottomWidth, BackgroundColor, PaddingTotale, Padding, PaddingTop, PaddingLeft, PaddingRight, PaddingBottom, Rotazione, AltezzaMinima, Larghezza)
                End If
            Next
        Next
        AggiungiTable(Document, Table)
        Return Document
    End Function
    ''' <summary>Funzione per Istanziare una risorsa PdfStamper</summary>
    Public Function IstanziaPdfStamper() As PdfStamper
        IstanziaPdfStamper = Nothing
    End Function
    ''' <summary>Funzione per Generare un file PDF proveniente da un Template grafico</summary>
    ''' <param name="PdfStamper">Definizione del PdfStamper</param>
    ''' <param name="PercorsoTemplate">Definizione del percorso relativo al Template PDF da utilizzare (Attenzione: No Server.MapPath)</param>
    ''' <param name="PercorsoNewPdf">Definizione del percorso relativo alla creazione del nuovo File PDF (Attenzione: No Server.MapPath)</param>
    Public Function GeneratePDFfromTemplate(ByRef PdfStamper As PdfStamper, ByVal PercorsoTemplate As String, ByVal PercorsoNewPdf As String) As PdfStamper
        Dim pdfTemplate As String = System.Web.HttpContext.Current.Server.MapPath(PercorsoNewPdf)
        Dim pdfReader As PdfReader = Nothing
        Dim pdfOutputFile As New FileStream(pdfTemplate, FileMode.Create)
        Dim path As String = System.Web.HttpContext.Current.Server.MapPath(PercorsoTemplate)
        pdfReader = New PdfReader(path)
        PdfStamper = New PdfStamper(pdfReader, pdfOutputFile)
        pdfReader.Close()
        Return PdfStamper
    End Function
    ''' <summary>Procedura per la Chiusura di un PDF creato tramite un Template</summary>
    ''' <param name="PdfStamper">Definizione del PdfStamper</param>
    Public Function ClosePDFfromTemplate(ByRef PdfStamper As PdfStamper) As PdfStamper
        Dim overContent As PdfContentByte = PdfStamper.GetOverContent(1)
        PdfStamper.FormFlattening = True
        PdfStamper.Close()
        Return PdfStamper
    End Function
    ''' <summary>Procedura per settare AcroFields di Tipo: TESTO</summary>
    ''' <param name="PdfStamper">Definizione del PdfStamper</param>
    ''' <param name="Key">Definizione dell'ID dell'AcroFields</param>
    ''' <param name="Value">Definizione del Valore che assumerà l'AcroFields</param>
    Public Function SetAcroFieldsText(ByRef PdfStamper As PdfStamper, ByVal Key As String, ByVal Value As String) As PdfStamper
        Dim Form As AcroFields = PdfStamper.AcroFields
        '18/08/2017 MTeresa - Viene impostata la "Sostituzione Font" dopo aver creato il PdfStamper per consentire la visualizzazione dei caratteri speciali nella stampa come l'apice singolo
        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        PdfStamper.AcroFields.AddSubstitutionFont(bf)
        '18/08/2017 MTeresa - FINE
        Form.SetField(Key, Value)
        Return PdfStamper
    End Function
    ''' <summary>Procedura per settare AcroField di Tipo: IMAGE</summary>
    ''' <param name="PdfStamper">Definizione del PdfStamper</param>
    ''' <param name="Key">Definizione dell'ID dell'AcroFields</param>
    ''' <param name="PercorsoImage">Definizione del Percorso dell'Immagine</param>
    ''' <param name="ScaleImage">Definizione se scalare l'Immagine con le Dimensioni del Rectangle dell'AcroFields</param>
    Public Function SetAcroFieldsImage(ByRef PdfStamper As PdfStamper, ByVal Key As String, ByVal PercorsoImage As String, Optional ByVal ScaleImage As Boolean = False) As PdfStamper
        Dim overContent As PdfContentByte = PdfStamper.GetOverContent(1)
        Dim Form As AcroFields = PdfStamper.AcroFields
        Dim ImageArea() As Single = Form.GetFieldPositions(Key)
        Dim instanceImg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(PercorsoImage))
        Dim imageRect As Rectangle = New Rectangle(ImageArea(1), ImageArea(2), ImageArea(3), ImageArea(4))
        If ScaleImage Then instanceImg.ScaleToFit(imageRect.Width, imageRect.Height)
        instanceImg.SetAbsolutePosition(ImageArea(3) - instanceImg.ScaledWidth + (imageRect.Width - instanceImg.ScaledWidth) / 2, ImageArea(2) + (imageRect.Height - instanceImg.ScaledHeight) / 2)
        overContent.AddImage(instanceImg)
        PdfStamper.FormFlattening = True
        Return PdfStamper
    End Function
    ''' <summary>Procedura per settare AcroFields di Tipo: DATAMATRIX</summary>
    ''' <param name="PdfStamper">Definizione del PdfStamper</param>
    ''' <param name="Key">Definizione dell'ID dell'AcroFields</param>
    ''' <param name="Testo">Definizione del Testo di Codifica del DataMatrix</param>
    ''' <param name="Percorso">Definizione del Percorso del Barcode</param>
    ''' <param name="Height">Definizione dell'Altezza del DataMatrix</param>
    ''' <param name="Width">Definizione della larghezza del DataMatrix</param>
    ''' <param name="ScaleImage">Definizione se scalare l'Immagine con le Dimensioni del Rectangle dell'AcroFields</param>
    'Public Function SetAcroFieldsDataMatrix(ByRef PdfStamper As PdfStamper, ByVal Key As String, ByVal Testo As String, Optional ByVal Percorso As String = "~\FileTemp\", Optional ByVal Height As Integer = 16, Optional ByVal Width As Integer = 48, Optional ByVal ScaleImage As Boolean = False) As PdfStamper
    '    Dim PercorsoImage As String = par.RicavaDataMatrix(Testo, Percorso, Height, Width)
    '    SetAcroFieldsImage(PdfStamper, Key, Percorso & PercorsoImage, ScaleImage)
    '    File.Delete(System.Web.HttpContext.Current.Server.MapPath(Percorso & PercorsoImage))
    '    Return PdfStamper
    'End Function
    ''' <summary>Procedura per settare AcroFields di Tipo: BARCODE128</summary>
    ''' <param name="PdfStamper">Definizione del PdfStamper</param>
    ''' <param name="Key">Definizione dell'ID dell'AcroFields</param>
    ''' <param name="Testo">Definizione del Testo di Codifica del BarCode</param>
    ''' <param name="Percorso">Definizione del Percorso del Barcode</param>
    ''' <param name="BarHeight">Definizione dell'Altezza del Barcode</param>
    ''' <param name="ImageWidth">Definizione dell'Altezza dell'Immagine del Barcode</param>
    ''' <param name="ImageHeight">Definizione della Larghezza dell'Immagine del Barcode</param>
    ''' <param name="ScaleImage">Definizione se scalare l'Immagine con le Dimensioni del Rectangle dell'AcroFields</param>
    'Public Function SetAcroFieldsBarCode(ByRef PdfStamper As PdfStamper, ByVal Key As String, ByVal Testo As String, Optional ByVal Percorso As String = "~\FileTemp\", Optional ByVal BarHeight As Integer = 30, Optional ByVal ImageWidth As Integer = 400, Optional ByVal ImageHeight As Integer = 30, Optional ByVal ScaleImage As Boolean = False) As PdfStamper
    '    Dim PercorsoImage As String = par.RicavaBarCode128(Testo, Percorso, BarHeight, ImageWidth, ImageHeight)
    '    SetAcroFieldsImage(PdfStamper, Key, Percorso & PercorsoImage, ScaleImage)
    '    File.Delete(System.Web.HttpContext.Current.Server.MapPath(Percorso & PercorsoImage))
    '    Return PdfStamper
    'End Function
    ''' <summary>Enum per la Decodifica della Conversione</summary>
    Public Enum ConvertMM
        Pixel = 1
        Points = 2
    End Enum
    ''' <summary>Funzione per LaConversione da Millimetri</summary>
    ''' <param name="Millimetri">Definizione dei Millimetri</param>
    ''' <param name="Convert">Defizione del Tipo di Conversione</param>
    Public Function ConvertMMTo(ByVal Millimetri As Decimal, ByVal Convert As ConvertMM) As Decimal
        ConvertMMTo = 0
        If Convert = 1 Then
            ConvertMMTo = Millimetri * 3.779527559D
        ElseIf Convert = 2 Then
            ConvertMMTo = Millimetri * 2.834645669D
        End If
    End Function
    ''' <summary>Procedura per la Creazione di un File PDF da Merge</summary>
    ''' <param name="ListaFilePdf">Definizione della Lista di file PDF su cui effettuare il Merge</param>
    ''' <param name="NomeFile">Definizione del Nome del File da Creare</param>
    ''' <param name="PercorsoFinale">Definizione del Percorso del File PDF da creare</param>
    Public Function MergePdf(ByVal ListaFilePdf As String(), ByVal NomeFile As String, Optional ByVal PercorsoFinale As String = "~/FileTemp/") As String
        Dim f As Integer = 0
        Dim reader As New PdfReader(System.Web.HttpContext.Current.Server.MapPath(ListaFilePdf(f)))
        Dim n As Integer = reader.NumberOfPages
        Dim document As New Document(reader.GetPageSizeWithRotation(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(System.Web.HttpContext.Current.Server.MapPath(PercorsoFinale) & NomeFile & ".pdf", FileMode.Create))
        document.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        Dim page As PdfImportedPage
        Dim rotation As Integer
        While f < ListaFilePdf.Length
            Dim i As Integer = 0
            While i < n
                i += 1
                document.SetPageSize(reader.GetPageSizeWithRotation(i))
                document.NewPage()
                page = writer.GetImportedPage(reader, i)
                rotation = reader.GetPageRotation(i)
                If rotation = 90 OrElse rotation = 270 Then
                    cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(i).Height)
                Else
                    cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)
                End If
            End While
            f += 1
            If f < ListaFilePdf.Length Then
                reader = New PdfReader(System.Web.HttpContext.Current.Server.MapPath(ListaFilePdf(f)))
                n = reader.NumberOfPages
            End If
        End While
        document.Close()
        For Each stringa As String In ListaFilePdf
            File.Delete(System.Web.HttpContext.Current.Server.MapPath(stringa))
        Next
        Return NomeFile & ".pdf"
    End Function
    ''' <summary>Gestione per l'Inserimento del Modello all'interno del Content</summary>
    ''' <param name="ContentByte">Definizione del Content su cui inserire il Modello.</param>
    ''' <param name="Writer">Definizione del Writer per la Creazione.</param>
    ''' <param name="Reader">Definizione del Reader da Cui Leggere il Modello</param>
    ''' <param name="NumeroPagine">Definizione del Numero di Pagine da Importare</param>
    Public Function ImportaModelloWriter(ByVal ContentByte As PdfContentByte, ByVal Writer As PdfWriter, ByVal Reader As PdfReader, ByVal Document As Document, Optional ByVal NumeroPagine As Integer = 1) As PdfContentByte
        Dim page As PdfImportedPage = Nothing
        For i As Integer = 1 To NumeroPagine Step 1
            page = Writer.GetImportedPage(Reader, i)
            ContentByte.AddTemplate(page, 0, 0)
            If i <> NumeroPagine Then NuovaPaginaOggetto(Document, Document)
        Next
        Return ContentByte
    End Function
End Class