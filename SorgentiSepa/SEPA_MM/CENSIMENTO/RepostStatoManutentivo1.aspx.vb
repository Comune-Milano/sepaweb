Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class CENSIMENTO_RepostStatoManutentivo1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try
                If Request.QueryString("A") = "1" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim data_sloggio As String = Request.QueryString("SS")


                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModuloBiancoPieno.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                'contenuto = Replace(contenuto, "$datasloggio$", par.FormattaData(data_sloggio))


                par.cmd.CommandText = "select TAB_QUARTIERI.NOME AS NOME_Q,complessi_immobiliari.id as idq,COMPLESSI_IMMOBILIARI.ID_QUARTIERE,edifici.id as idf,edifici.fl_piano_vendita," _
                                    & "EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA," _
                                    & "indirizzi.cap,comuni_nazioni.nome as comune,SISCOM_MI.UNITA_IMMOBILIARI.interno," _
                                    & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from " _
                                    & "SISCOM_MI.TAB_QUARTIERI,siscom_mi.tipo_livello_piano,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici," _
                                    & "SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND " _
                                    & "edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) " _
                                    & "and indirizzi.cod_comune=comuni_nazioni.cod (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and " _
                                    & "TAB_QUARTIERI.ID=COMPLESSI_IMMOBILIARI.ID_QUARTIERE (+) AND unita_immobiliari.ID=" & Request.QueryString("ID")


                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    '
                    If par.IfNull(myReader3("fl_piano_vendita"), "0") = "1" Then
                        contenuto = Replace(contenuto, "$indirizzo$", Session.Item("INDIRIZZOUNITA") & "<b>Unità inserita nel piano vendita!!</b>")
                    Else
                        contenuto = Replace(contenuto, "$indirizzo$", Session.Item("INDIRIZZOUNITA"))
                    End If

                    If par.IfNull(myReader3("condominio"), "0") = "1" Then

                        contenuto = Replace(contenuto, "$condominio$", "SI")
                    Else

                        contenuto = Replace(contenuto, "$condominio$", "NO")
                    End If

                    If par.IfNull(myReader3("GEST_RISC_DIR"), "0") = "1" Then
                        contenuto = Replace(contenuto, "$diretta$", "SI")
                    Else
                        contenuto = Replace(contenuto, "$diretta$", "NO")
                    End If

                    contenuto = Replace(contenuto, "$quartiere$", par.IfNull(myReader3("NOME_Q"), ""))

                    IndiceComplesso = par.IfNull(myReader3("IDQ"), "-1")
                    IndiceEdificio = par.IfNull(myReader3("IDF"), "-1")
                    CodiceUnita = par.IfNull(myReader3("cod_unita_immobiliare"), "-1")
                Else
                    contenuto = Replace(contenuto, "$indirizzo$", Session.Item("INDIRIZZOUNITA"))
                    contenuto = Replace(contenuto, "$condominio$", "")
                    contenuto = Replace(contenuto, "$diretta$", "")
                    contenuto = Replace(contenuto, "$quartiere$", "")
                End If
                myReader3.Close()


                Dim ff As String = ""

                par.cmd.CommandText = "select * from alloggi where cod_alloggio='" & CodiceUnita & "'"
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    contenuto = Replace(contenuto, "$disponibilita$", par.FormattaData(par.IfNull(myReader3("data_disponibilita"), "")))
                    ff = par.IfNull(myReader3("data_disponibilita"), "")
                Else
                    ' contenuto = Replace(contenuto, "$disponibilita$", "")
                End If
                myReader3.Close()

                If ff = "" Then
                    par.cmd.CommandText = "select * from siscom_mi.ui_usi_diversi where cod_alloggio='" & CodiceUnita & "'"
                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        contenuto = Replace(contenuto, "$disponibilita$", par.FormattaData(par.IfNull(myReader3("data_disponibilita"), "")))
                    Else
                        contenuto = Replace(contenuto, "$disponibilita$", "")
                    End If
                    myReader3.Close()
                End If

                If Request.QueryString("TIPO") = "0" Then
                    par.cmd.CommandText = "select siscom_mi.unita_STATO_MANUTENTIVO.* from siscom_mi.unita_STATO_MANUTENTIVO WHERE ID_UNITA=" & Request.QueryString("ID")
                Else
                    par.cmd.CommandText = "select siscom_mi.unita_STATO_MAN_S.* from siscom_mi.unita_STATO_MAN_S WHERE ID_UNITA=" & Request.QueryString("ID") & " and data_memo='" & Request.QueryString("DATA") & "'"
                End If

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    contenuto = Replace(contenuto, "$datasloggio$", par.FormattaData(par.IfNull(myReader("DATA_S"), "")))

                    contenuto = Replace(contenuto, "$dataconsegnaST$", par.FormattaData(par.IfNull(myReader("DATA_CONSEGNA_STR"), "")))
                    contenuto = Replace(contenuto, "$dataripresaST$", par.FormattaData(par.IfNull(myReader("DATA_RIPRESA_STR"), "")))

                    par.cmd.CommandText = "select descrizione from siscom_mi.tab_strutture where id=" & par.IfNull(myReader("ID_STRUTTURA_COMP"), "-1")
                    Dim myReaderX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX2.Read Then
                        contenuto = Replace(contenuto, "$struttura$", par.IfNull(myReaderX2("descrizione"), ""))
                    Else
                        contenuto = Replace(contenuto, "$struttura$", "")
                    End If
                    myReaderX2.Close()

                    Select Case par.IfNull(myReader("ST_DEP_CHIAVI"), "")
                        Case "0"
                            contenuto = Replace(contenuto, "$depchiavi$", "")
                        Case "1"
                            contenuto = Replace(contenuto, "$depchiavi$", "")
                        Case "2"
                            contenuto = Replace(contenuto, "$depchiavi$", "")
                        Case "3"
                            contenuto = Replace(contenuto, "$depchiavi$", "")

                        Case Else
                            contenuto = Replace(contenuto, "$depchiavi$", "---")
                    End Select


                    contenuto = Replace(contenuto, "$zona$", par.IfNull(myReader("ZONA"), "01"))

                    Select Case par.IfNull(myReader("TIPO_ALLOGGIO"), "-1")
                        Case "0"
                            contenuto = Replace(contenuto, "$tipoalloggio$", "MONO")
                        Case "1"
                            contenuto = Replace(contenuto, "$tipoalloggio$", "BI PICCOLO")
                        Case "2"
                            contenuto = Replace(contenuto, "$tipoalloggio$", "BI GRANDE")
                        Case "3"
                            contenuto = Replace(contenuto, "$tipoalloggio$", "TRILOCALI")
                        Case "4"
                            contenuto = Replace(contenuto, "$tipoalloggio$", "QUATTRO O + LOCALI")
                        Case "10"
                            contenuto = Replace(contenuto, "$tipoalloggio$", "NON DEFINITO")
                    End Select

                    contenuto = Replace(contenuto, "$numlocali$", par.IfNull(myReader("NUM_LOCALI"), "0"))
                    contenuto = Replace(contenuto, "$numservizi$", par.IfNull(myReader("NUM_SERVIZI"), "0"))
                    contenuto = Replace(contenuto, "$motivazioni$", par.IfNull(myReader("MOTIVAZIONI"), ""))
                    contenuto = Replace(contenuto, "$sopralluogo$", par.FormattaData(par.IfNull(myReader("DATA_S"), "")))
                    contenuto = Replace(contenuto, "$presloggio$", par.FormattaData(par.IfNull(myReader("DATA_PRE_SLOGGIO"), "")))



                    Select Case par.IfNull(myReader("riassegnabile"), 0)
                        Case "0"
                            contenuto = Replace(contenuto, "$stato$", "NON AGIBILE")
                        Case "1"
                            contenuto = Replace(contenuto, "$stato$", "AGIBILE")
                    End Select

                    Select Case par.IfNull(myReader("Tipo_Riassegnabile"), 0)
                        Case "0"
                            contenuto = Replace(contenuto, "$stato1$", "(ri) ASSEGNABILE CON LAVORI")
                        Case "1"
                            contenuto = Replace(contenuto, "$stato1$", "(ri) ASSEGNABILI SENZA LAVORI")
                        Case "2"
                            contenuto = Replace(contenuto, "$stato1$", "NON RIASSEGNABILE")
                    End Select

                    Select Case par.IfNull(myReader("p_blindata"), "0")
                        Case "0"
                            contenuto = Replace(contenuto, "$blindata$", "NO")
                        Case "1"
                            contenuto = Replace(contenuto, "$blindata$", "SI")
                    End Select

                    Select Case par.IfNull(myReader("handicap"), "0")
                        Case "0"
                            contenuto = Replace(contenuto, "$handicap$", "NO")
                        Case "1"
                            contenuto = Replace(contenuto, "$handicap$", "NO")
                    End Select

                    Select Case par.IfNull(myReader("REC_GRTP"), "0")
                        Case "1"
                            contenuto = Replace(contenuto, "$grtp$", "SI")
                        Case "0"
                            contenuto = Replace(contenuto, "$grtp$", "NO")
                    End Select

                    contenuto = Replace(contenuto, "$notegrtp$", par.IfNull(myReader("noteGRTP"), ""))
                    contenuto = Replace(contenuto, "$note$", Replace(par.IfNull(myReader("note"), ""), vbCrLf, "<br/>"))
                    contenuto = Replace(contenuto, "$datafinelavori$", par.FormattaData(par.IfNull(myReader("DATA_pre_S"), "")))

                    contenuto = Replace(contenuto, "$dataaddebiti$", par.FormattaData(par.IfNull(myReader("DATA_Q"), "")))
                    contenuto = Replace(contenuto, "$notedanni$", Replace(par.IfNull(myReader("note_danni"), ""), vbCrLf, "<br/>"))

                    contenuto = Replace(contenuto, "$importodanni$", par.IfNull(myReader("IMPORTO_DANNI"), "0,00"))
                    contenuto = Replace(contenuto, "$importotrasporto$", par.IfNull(myReader("IMPORTO_TRASPORTO"), "0,00"))

                    Select Case par.IfNull(myReader("FL_ABUSIVO"), "0")
                        Case "1"
                            contenuto = Replace(contenuto, "$abusivo$", "SI")
                        Case "0"
                            contenuto = Replace(contenuto, "$abusivo$", "NO")
                    End Select

                    Dim Soluzioni As String = ""
                    Dim TipoPorta As String = ""

                    If par.IfNull(myReader("SOL_PB"), "0") = "0" Then

                    Else
                        TipoPorta = TipoPorta & "PORTA BLINDATA" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_PB1"), "0") = "0" Then

                    Else
                        TipoPorta = TipoPorta & "PORTA RINFORZATA" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_PB2"), "0") = "0" Then

                    Else
                        TipoPorta = TipoPorta & "PORTA NORMALE" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_PB3"), "0") = "0" Then

                    Else
                        TipoPorta = TipoPorta & "PORTA DI ALTRO TIPO" & "<br/>"
                    End If

                    contenuto = Replace(contenuto, "$tipoporta$", TipoPorta)
                    contenuto = Replace(contenuto, "$notetipoporta$", par.IfNull(myReader("note_tipo_porta"), ""))


                    If par.IfNull(myReader("SOL_GP"), "0") = "0" Then

                    Else
                        Soluzioni = Soluzioni & "GRATA PORTA" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_GF"), "0") = "0" Then

                    Else
                        Soluzioni = Soluzioni & "GRATA FINESTRE" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_LA"), "0") = "0" Then

                    Else
                        Soluzioni = Soluzioni & "LASTRATURA PORTE" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_LA1"), "0") = "0" Then

                    Else
                        Soluzioni = Soluzioni & "LASTRATURA FINESTRE" & "<br/>"
                    End If

                    If par.IfNull(myReader("SOL_AL"), "0") = "0" Then

                    Else
                        Soluzioni = Soluzioni & "ALTRO" & "<br/>"
                    End If

                    If par.IfNull(myReader("ALLARME"), "0") = "0" Then

                    Else
                        Soluzioni = Soluzioni & "ALLARME" & "<br/>"
                    End If

                    contenuto = Replace(contenuto, "$soluzioni$", Soluzioni)
                    contenuto = Replace(contenuto, "$notesoluzioni$", par.IfNull(myReader("note_sicurezza"), ""))

                    contenuto = Replace(contenuto, "$dataconsegna$", par.FormattaData(par.IfNull(myReader("DATA_consegna_chiavi"), "")))

                    contenuto = Replace(contenuto, "$dataripresa$", par.FormattaData(par.IfNull(myReader("DATA_ripresa_chiavi"), "")))

                    contenuto = Replace(contenuto, "$consegnatea$", par.IfNull(myReader("consegnate_a"), ""))



                Else

                    Response.Write("<script>alert('Non ci sono dati da stampare. Assicurarsi di aver salvato.');</script>")
                    contenuto = Replace(contenuto, "$zona$", "")
                    contenuto = Replace(contenuto, "$tipoalloggio$", "")
                    contenuto = Replace(contenuto, "$numlocali$", "")
                    contenuto = Replace(contenuto, "$numservizi$", "")
                    contenuto = Replace(contenuto, "$motivazioni$", "")
                    contenuto = Replace(contenuto, "$sopralluogo$", "")
                    contenuto = Replace(contenuto, "$presloggio$", "")
                    contenuto = Replace(contenuto, "$stato$", "")
                    contenuto = Replace(contenuto, "$stato1$", "")
                    contenuto = Replace(contenuto, "$blindata$", "")
                    contenuto = Replace(contenuto, "$handicap$", "")
                    contenuto = Replace(contenuto, "$grtp$", "")
                    contenuto = Replace(contenuto, "$notegrtp$", "")
                    contenuto = Replace(contenuto, "$note$", "")
                    contenuto = Replace(contenuto, "$datafinelavori$", "")
                    contenuto = Replace(contenuto, "$dataaddebiti$", "")
                    contenuto = Replace(contenuto, "$notedanni$", "")
                    contenuto = Replace(contenuto, "$importodanni$", "")
                    contenuto = Replace(contenuto, "$importotrasporto$", "")
                    contenuto = Replace(contenuto, "$abusivo$", "")
                    contenuto = Replace(contenuto, "$tipoporta$", "")
                    contenuto = Replace(contenuto, "$notetipoporta$", "")

                    contenuto = Replace(contenuto, "$soluzioni$", "")
                    contenuto = Replace(contenuto, "$notesoluzioni$", "")

                    contenuto = Replace(contenuto, "$dataconsegna$", "")

                    contenuto = Replace(contenuto, "$dataripresa$", "")

                    contenuto = Replace(contenuto, "$consegnatea$", "")
                    contenuto = Replace(contenuto, "$depchiavi$", "")

                    contenuto = Replace(contenuto, "$dataconsegnaST$", "")
                    contenuto = Replace(contenuto, "$dataripresaST$", "")
                    contenuto = Replace(contenuto, "$struttura$", "")

                End If
                myReader.Close()
                Dim interventi As String = ""

                If Request.QueryString("TIPO") = "0" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO_INT WHERE ID_UNITA=" & Request.QueryString("ID")
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_STATO_MAN_INT_s WHERE ID_UNITA=" & Request.QueryString("ID") & " and data_memo='" & Request.QueryString("DATA") & "'"

                End If
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader2.Read
                    Select Case myReader2("ID_INTERVENTO")
                        Case 1
                            interventi = interventi & "SISTEMAZIONI INFISSI,"
                        Case 2
                            interventi = interventi & "SOSTITUZIONE INFISSI,"
                        Case 3
                            interventi = interventi & "SISTEMAZIONE IMPIANTO ELETTRICO,"
                        Case 4
                            interventi = interventi & "SOSTITUZIONE IMPIANTO ELETTRICO,"
                        Case 5
                            interventi = interventi & "SISTEMAZIONE IMPIANTO IDRAULICO CUCINA,"
                        Case 6
                            interventi = interventi & "SOSTITUZIONE IMPIANTO IDRAULICO CUCINA,"
                        Case 7
                            interventi = interventi & "SISTEMAZIONE IMPIANTO IDRAULICO BAGNO,"
                        Case 8
                            interventi = interventi & "SOSTITUZIONE IMPIANTO IDRAULICO BAGNO,"
                        Case 9
                            interventi = interventi & "SISTEMAZIONE SERRANDE,"
                        Case 10
                            interventi = interventi & "SOSTITUZIONE SERRANDE,"
                        Case 11
                            interventi = interventi & "SISTEMAZIONE PORTA INGRESSO,"
                        Case 12
                            interventi = interventi & "SOSTITUZIONE PORTA INGRESSO,"
                        Case 13
                            interventi = interventi & "SISTEMAZIONE SANITARI BAGNO,"
                        Case 14
                            interventi = interventi & "SOSTITUZIONE SANITARI BAGNO,"
                        Case 15
                            interventi = interventi & "SISTEMAZIONE LAVELLO CUCINA,"
                        Case 16
                            interventi = interventi & "SOSTITUZIONE LAVELLO CUCINA,"
                        Case 17
                            interventi = interventi & "SISTEMAZIONE CALORIFERI,"
                        Case 18
                            interventi = interventi & "SOSTITUZIONE CALORIFERI,"
                        Case 19
                            interventi = interventi & "SISTEMAZIONE PAVIMENTI ,"
                        Case 20
                            interventi = interventi & "SOSTITUZIONE PAVIMENTI ,"
                        Case 21
                            interventi = interventi & "FORO AREAZIONE CUCINA,"
                    End Select


                Loop
                myReader2.Close()

                contenuto = Replace(contenuto, "$interventi$", interventi)

                If Request.QueryString("A") = "1" Then

                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Dim url As String = Server.MapPath("..\FileTemp\")
                Dim pdfConverter1 As PdfConverter = New PdfConverter

                Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter1.LicenseKey = Licenza
                End If

                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
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

                Dim nomefile As String = "Export_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))

                Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Modulo','');self.close();</script>")


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub


    Public Property IndiceComplesso() As String
        Get
            If Not (ViewState("par_IndiceComplesso") Is Nothing) Then
                Return CStr(ViewState("par_IndiceComplesso"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceComplesso") = value
        End Set

    End Property

    Public Property IndiceEdificio() As String
        Get
            If Not (ViewState("par_IndiceEdificio") Is Nothing) Then
                Return CStr(ViewState("par_IndiceEdificio"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceEdificio") = value
        End Set

    End Property

    Public Property CodiceUnita() As String
        Get
            If Not (ViewState("par_CodiceUnita") Is Nothing) Then
                Return CStr(ViewState("par_CodiceUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CodiceUnita") = value
        End Set

    End Property
End Class
