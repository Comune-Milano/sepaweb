Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_RegistrazioneTelematica
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    'Public XMLError As String

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


    Private Function ValidaFile(ByVal file As String) As String
        Try


            Dim settings As New XmlReaderSettings()

            settings.ProhibitDtd = False
            settings.IgnoreWhitespace = True
            settings.ValidationType = ValidationType.DTD

            'settings.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);


            Dim reader As XmlReader
            reader = XmlReader.Create(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\" & file), settings)

            While (reader.Read())

            End While
            ValidaFile = ""
            reader.Close()
        Catch ex As Exception
            ValidaFile = ex.Message

        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If
        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataInvio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)
            Response.Flush()

            Dim bTrovato As Boolean = False
            Dim sStringaSql As String = ""
            Dim locatore As String = ""
            Dim cflocatore As String = ""
            Dim direttore As String = ""
            Dim Comunelocatore As String = ""
            Dim Provlocatore As String = ""
            Dim Indirizzolocatore As String = ""
            Dim Civicolocatore As String = ""

            Dim StringaFile As String = ""
            Dim NomeFile As String = ""
            Dim TipoPagamento As String = ""
            Dim BOLLO As String = ""
            Dim CanoneCorrente As Double = 0
            Dim ElencoFile() as string
            Dim i As Integer = 0
            Dim KK As Integer = 0
            Dim Locatari As String = ""


            Dim Conduttore As String = ""
            Dim Titolo As String = ""
            Dim SessoConduttore As String = "S"

            Dim CognomeConduttore As String = ""
            Dim NomeConduttore As String = ""
            Dim ComuneNascitaConduttore As String = ""
            Dim ProvinciaNascitaConduttore As String = ""
            Dim DataNascitaConduttore As String = ""
            Dim CodiceFiscaleConduttore As String = ""

            Dim SUP_UTILE_NETTA As Double = 0
            Dim SUP_BALCONI As Double = 0
            Dim SUP_ESCLUSIVA As Double = 0
            Dim VAL_MILLESIMO As Double = 0
            Dim NUM_VANI As Double = 0
            Dim SUP_VERDE As Double = 0
            Dim STRINGA_SUP As String = ""
            Dim ListaConduttori As String = ""


            'calcolo interessi
            Dim baseCalcolo As Double = 0
            Dim DataCalcolo As String = ""
            Dim DataInizio As String = ""

            Dim sanzione As String = "0"


            Dim IMPORTOREGISTRAZIONE As Double = 0
            Dim POS As Integer = 0

            Dim Giorni As Integer = 0
            Dim GiorniAnno As Integer = 0
            Dim dataPartenza As String = ""

            Dim Totale As Double = 0
            Dim TotalePeriodo As Double = 0
            Dim indice As Long = 0
            Dim tasso As Double = 0
            Dim DataFine As String = ""

            Dim POS1 As Long = 0
            Dim POS2 As Long = 0


            Dim cfpiva As String = ""

            PAR.OracleConn.Open()
            par.SettaCommand(par)
            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim Interessi As New SortedDictionary(Of Integer, Double)
            Interessi.Clear()

            Dim zipfic As String
            Dim NomeFilezip As String = "REG_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip)

            PAR.cmd.CommandText = "select * from siscom_mi.tab_interessi_legaLI order by anno desc"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReaderA.Read
                Interessi.Add(myReaderA("anno"), myReaderA("tasso"))
            Loop
            myReaderA.Close()


            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=8"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                locatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=10"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                cflocatore = PAR.IfNull(myReader("valore"), "")
            End If
            myReader.Close()


            If txtStipulaDal.Text <> "" Then
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA>='" & PAR.PulisciStrSql(PAR.AggiustaData(txtStipulaDal.Text)) & "' "
            End If

            If txtStipulaAl.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA<='" & PAR.PulisciStrSql(PAR.AggiustaData(txtStipulaAl.Text)) & "' "
            End If

            If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

            PAR.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA WHERE SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (COD_UFFICIO_REG IS NULL OR COD_UFFICIO_REG='-1') AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='') AND FL_STAMPATO=1 AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' " & sStringaSql
            myReader = PAR.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                myReader.Close()
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                Response.Write("<script>alert('Attenzione, ci sono dei contratti in cui non è specificato l\'ufficio registro!');</script>")
                Exit Sub
            End If

            PAR.cmd.CommandText = "select distinct COD_UFFICIO_REG from SISCOM_MI.RAPPORTI_UTENZA WHERE SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='') AND BOZZA='0' AND FL_STAMPATO=1 AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' " & sStringaSql
            myReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read

                NomeFile = PAR.IfNull(myReader(0), "") & "_" & Format(Now, "yyyyMMddHHmmss")
                ReDim Preserve ElencoFile(i)

                ElencoFile(i) = NomeFile
                i = i + 1

                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".xml"), False, System.Text.Encoding.Default)
                Dim srDettagli As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".txt"), False, System.Text.Encoding.Default)
                Dim srErrori As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\Errori_" & NomeFile & ".txt"), False, System.Text.Encoding.Default)


                srDettagli.WriteLine("IDENTIFICATIVO CONTRATTO" & vbTab & "CODICE CONTRATTO" & vbTab & "CODICE UTENTE" & vbTab & "TIPO RAPPORTO" & vbTab & "IMPORTO REGISTRAZIONE" & vbTab & "SANZIONI" & vbTab & "INTERESSI" & vbTab & "DATA INVIO AG.ENTRATE" & vbTab & "DATA RIFERIMENTO")

                sr.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "ISO-8859-1" & Chr(34) & "?>")
                sr.WriteLine("<!DOCTYPE FileContratti SYSTEM " & Chr(34) & "filecontratti.dtd" & Chr(34) & ">")
                sr.WriteLine("<!-- created with SEPA@Web (www.sistemiesoluzionisrl.it) -->")
                sr.WriteLine("<FileContratti CodiceUfficio=" & Chr(34) & PAR.IfNull(myReader(0), "") & Chr(34) & " CodiceFiscaleConto=" & Chr(34) & "01349670156" & Chr(34) & " CodiceFiscaleFornitore=" & Chr(34) & cflocatore & Chr(34) & " ValutaPrelievo=" & Chr(34) & "E" & Chr(34) & ">")

                Dim ERRORI As String = ""
                Dim ok As String = ""
                Dim data_riferimento As String = ""
                Dim gioniDiff As Integer = 0
                Dim DataStipula As String = ""
                Dim CodiceContratto As String = ""
                Dim CodiceUtente As String = ""
                Dim SupNetta As String = ""
                Dim TIPO_RAPPORTO As String = ""
                Dim CodiceUtente1 As String = "-1"
                Dim tiporegistrazione As String = "P"
                Dim CodiceTributo As String = ""

                PAR.cmd.CommandText = "select RAPPORTI_UTENZA.*  from SISCOM_MI.RAPPORTI_UTENZA WHERE COD_UFFICIO_REG='" & PAR.IfNull(myReader(0), "") & "' AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='') AND BOZZA='0' AND FL_STAMPATO=1 " & sStringaSql & " ORDER BY RAPPORTI_UTENZA.DATA_STIPULA ASC"
                'PAR.cmd.CommandText = "select RAPPORTI_UTENZA.*  from SISCOM_MI.RAPPORTI_UTENZA WHERE COD_UFFICIO_REG='" & PAR.IfNull(myReader(0), "") & "' AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (REG_TELEMATICA='MAX') AND BOZZA='0' AND FL_STAMPATO=1 " & sStringaSql

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReader1.Read
                    TIPO_RAPPORTO = Mid(PAR.IfNull(myReader1("COD_TIPOLOGIA_CONTR_LOC"), "---"), 1, 3)


                    cfpiva = ""

                    'If PAR.SoluzioneUnica(PAR.IfNull(myReader1("imp_canone_iniziale"), 0), PAR.IfNull(myReader1("durata_anni"), 4)) <= 67 Then
                        'tiporegistrazione = "T"
                        'CodiceTributo = "107T"
                    'Else
                        'tiporegistrazione = "P"
                        'CodiceTributo = "115T"
                    'End If

                    'If PAR.IfNull(myReader1("versamento_tr"), "A") = "U" Then
                    '    tiporegistrazione = "T"
                    'Else
                    '    tiporegistrazione = "P"
                    'End If


                    PAR.cmd.CommandText = "select id_anagrafica,cod_fiscale,partita_iva from siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica where anagrafica.id=soggetti_contrattuali.id_anagrafica and cod_tipologia_occupante='INTE' AND id_contratto=" & PAR.IfNull(myReader1("ID"), "-1")
                    Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader1234.Read Then
                        CodiceUtente = Format(PAR.IfNull(myReader1234("id_anagrafica"), ""), "0000000000")
                        CodiceUtente1 = PAR.IfNull(myReader1234("id_anagrafica"), "-1")

                        If PAR.IfNull(myReader1234("partita_iva"), "") <> "" Then
                            cfpiva = myReader1234("partita_iva")
                        Else
                            cfpiva = PAR.IfNull(myReader1234("cod_fiscale"), "")
                        End If
                    End If
                    myReader1234.Close()

                    PAR.cmd.CommandText = "select valore from siscom_mi.dimensioni,siscom_mi.unita_contrattuale where dimensioni.cod_tipologia='SUP_NETTA' AND dimensioni.id_unita_immobiliare=unita_contrattuale.id_unita and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & PAR.IfNull(myReader1("ID"), "-1")
                    myReader1234 = PAR.cmd.ExecuteReader()
                    If myReader1234.Read Then
                        SupNetta = PAR.IfNull(PAR.IfNull(myReader1234("valore"), ""), "---")
                    End If
                    myReader1234.Close()

                    'interessi
                    baseCalcolo = 0
                    DataCalcolo = ""
                    DataInizio = ""


                    sanzione = "0"
                    IMPORTOREGISTRAZIONE = 0
                    POS = 0
                    CodiceContratto = PAR.IfNull(myReader1("cod_contratto"), "")
                    DataStipula = PAR.FormattaData(PAR.IfNull(myReader1("data_stipula"), "29991231"))


                    If PAR.IfNull(myReader1("data_stipula"), "29991231") < PAR.IfNull(myReader1("data_decorrenza"), "29991231") Then
                        data_riferimento = PAR.IfNull(myReader1("data_stipula"), "29991231")
                    Else
                        data_riferimento = PAR.IfNull(myReader1("data_decorrenza"), "29991231")
                    End If


                    PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_REGISTRAZIONE WHERE ID_CONTRATTO=" & myReader1("ID")
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                    Dim bw As BinaryWriter

                    If myReader2.Read() Then
                        Dim fileName As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & Format(Now, "yyyyMMddHHmmss") & ".xml")
                        Dim fs = New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                        bw = New BinaryWriter(fs)
                        bw.Write(myReader2("testo_XML"))
                        bw.Flush()
                        bw.Close()



                        Dim sr1 As StreamReader = New StreamReader(fileName, System.Text.Encoding.GetEncoding("iso-8859-1"))
                        Dim contenuto As String = sr1.ReadToEnd()
                        sr1.Close()


                        gioniDiff = DateDiff(DateInterval.Day, CDate(PAR.FormattaData(data_riferimento)), CDate(txtDataInvio.Text))
                        POS = InStr(1, contenuto, "ImportoRegistrazione=")
                        If POS > 0 Then
                            IMPORTOREGISTRAZIONE = Mid(contenuto, POS + 22, InStr(POS + 22, contenuto, Chr(34)) - (POS + 22))
                        Else
                            IMPORTOREGISTRAZIONE = 0
                        End If


                        If gioniDiff >= 31 And gioniDiff <= 90 Then
                            'ImportoSanzioniRegistrazione=
                            sanzione = Format((IMPORTOREGISTRAZIONE * 12) / 100, "0.00")
                        End If

                        If gioniDiff >= 91 And gioniDiff <= 365 Then
                            'ImportoSanzioniRegistrazione=
                            sanzione = Format((IMPORTOREGISTRAZIONE * 15) / 100, "0.00")

                        End If

                        If gioniDiff >= 366 Then
                            'ImportoSanzioniRegistrazione=
                            sanzione = Format((IMPORTOREGISTRAZIONE * 120) / 100, "0.00")
                        End If
                        Totale = 0
                        If sanzione > 0 Then
                            DataCalcolo = PAR.AggiustaData(txtDataInvio.Text)
                            DataInizio = Format(DateAdd(DateInterval.Day, 30, CDate(PAR.FormattaData(data_riferimento))), "yyyyMMdd")

                            Giorni = 0
                            GiorniAnno = 0
                            dataPartenza = DataInizio
                            Totale = 0
                            TotalePeriodo = 0

                            For KK = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                                If KK = CInt(Mid(DataCalcolo, 1, 4)) Then
                                    DataFine = PAR.FormattaData(DataCalcolo)
                                Else
                                    DataFine = "31/12/" & KK

                                End If

                                GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & KK), CDate("31/12/" & KK)) + 1

                                Giorni = DateDiff(DateInterval.Day, CDate(PAR.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                                If KK < 1990 Then
                                    tasso = 5
                                Else
                                    If Interessi.ContainsKey(KK) = True Then
                                        tasso = Interessi(KK)
                                    End If
                                End If

                                TotalePeriodo = Format((((IMPORTOREGISTRAZIONE * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                                Totale = Totale + TotalePeriodo

                                dataPartenza = KK + 1 & "0101"

                            Next
                        Else
                            Totale = 0
                            TotalePeriodo = 0
                        End If

                        'ImportoRegistrazione="100,00"

                        POS1 = InStr(1, contenuto, "TipoPagamento=")
                        POS2 = InStr(POS1 + 15, contenuto, Chr(34))
                        contenuto = Replace(contenuto, Mid(contenuto, POS1, POS2 - POS1), "TipoPagamento=" & Chr(34) & tiporegistrazione)

                        contenuto = Replace(contenuto, "$peritenze$", "$pertinenze$")

                        contenuto = Replace(contenuto, "é", "e'")
                        contenuto = Replace(contenuto, "ì", "i'")
                        contenuto = Replace(contenuto, "à", "a'")
                        contenuto = Replace(contenuto, "è", "e'")
                        contenuto = Replace(contenuto, "ò", "o'")
                        contenuto = Replace(contenuto, "¿", "'")
                        contenuto = Replace(contenuto, "<ProvinciaNascitaConduttore>E</ProvinciaNascitaConduttore>", "<ProvinciaNascitaConduttore>EE</ProvinciaNascitaConduttore>")
                        contenuto = Replace(contenuto, "<DataStipula></DataStipula>", "<DataStipula>" & DataStipula & "</DataStipula>")
                        contenuto = Replace(contenuto, "CODICE UTENTE    NUMERO CONTRATTO", "CODICE UTENTE " & CodiceUtente & " NUMERO CONTRATTO " & CodiceContratto)
                        contenuto = Replace(contenuto, "mq. ---", "mq. " & SupNetta)
                        contenuto = Replace(contenuto, "$superfici$", "mq. " & SupNetta)
                        contenuto = Replace(contenuto, "Rendita  Canone", "Rendita --- Canone")
                        contenuto = Replace(contenuto, "$derogaart15$", "")

                        'Rendita  Canone

                        'CODICE UTENTE   NUMERO CONTRATTO " &  

                        POS1 = InStr(1, contenuto, "ImportoBollo=")
                        POS2 = InStr(POS1 + 14, contenuto, Chr(34))
                        contenuto = Replace(contenuto, Mid(contenuto, POS1, POS2 - POS1), "ImportoBollo=" & Chr(34) & "0,00")

                        POS1 = InStr(1, contenuto, "NumeroPagine=")
                        POS2 = InStr(POS1 + 14, contenuto, Chr(34))
                        contenuto = Replace(contenuto, Mid(contenuto, POS1, POS2 - POS1), "NumeroPagine=" & Chr(34) & "2")

                        If sanzione <> "0" Then
                            contenuto = Replace(contenuto, "ImportoSanzioniRegistrazione=" & Chr(34) & Chr(34), "ImportoSanzioniRegistrazione=" & Chr(34) & sanzione & Chr(34))
                        Else
                            contenuto = Replace(contenuto, "ImportoSanzioniRegistrazione=" & Chr(34) & Chr(34), "ImportoSanzioniRegistrazione=" & Chr(34) & "0,00" & Chr(34))
                        End If
                        contenuto = Replace(contenuto, "ImportoInteressi=" & Chr(34) & Chr(34), "ImportoInteressi=" & Chr(34) & Format(Totale, "0.00") & Chr(34))


                        'ImportoInteressi=""
                        If InStr(contenuto, "$totanticipo$") > 0 Then
                            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.XML_CONTRATTI WHERE ID_CONTRATTO=" & myReader1("ID")
                            Dim myReader222 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                            If myReader222.Read Then
                                contenuto = Replace(contenuto, "$totanticipo$", PAR.IfNull(myReader222("S1"), "0,00"))
                            Else
                                contenuto = Replace(contenuto, "$totanticipo$", "0,00")
                            End If
                            myReader222.Close()
                        End If

                        If InStr(contenuto, "SessoConduttore=" & Chr(34) & "S" & Chr(34), CompareMethod.Text) > 0 Then
                            If InStr(contenuto, "<CognomeRap>", CompareMethod.Text) = 0 Then
                                Dim CFC As String = ""
                                Dim POSS As Integer = 0

                                POSS = InStr(contenuto, "<CodiceFiscaleConduttore>", CompareMethod.Text)
                                CFC = Mid(contenuto, POSS + 25, 16)
                                If Mid(CFC, 10, 2) > 40 Then
                                    contenuto = Replace(contenuto, "SessoConduttore=" & Chr(34) & "S" & Chr(34), "SessoConduttore=" & Chr(34) & "F" & Chr(34))
                                Else
                                    contenuto = Replace(contenuto, "SessoConduttore=" & Chr(34) & "S" & Chr(34), "SessoConduttore=" & Chr(34) & "M" & Chr(34))
                                End If

                            End If
                        End If


                        If InStr(contenuto, "SessoConduttore=" & Chr(34) & Chr(34)) > 0 Then
                            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.anagrafica WHERE ID=" & CodiceUtente1
                            Dim myReader222 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                            If myReader222.Read Then
                                contenuto = Replace(contenuto, "SessoConduttore=" & Chr(34) & Chr(34), "SessoConduttore=" & Chr(34) & PAR.IfNull(myReader222("SESSO"), "M") & Chr(34))
                            Else
                                contenuto = Replace(contenuto, "SessoConduttore=" & Chr(34) & Chr(34), "M")
                            End If
                            myReader222.Close()
                        End If

                        Dim pertinenze As String = ""
                        PAR.cmd.CommandText = "select unita_contrattuale.id_unita from siscom_mi.unita_contrattuale where unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & myReader1("id")
                        Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReaderX1.Read Then
                            PAR.cmd.CommandText = "select tipologia_unita_immobiliari.descrizione from siscom_mi.unita_immobiliari,siscom_mi.tipologia_unita_immobiliari where tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and unita_immobiliari.id_unita_principale=" & PAR.IfNull(myReaderX1("id_unita"), "-1")
                            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                            Do While myReaderX.Read
                                pertinenze = pertinenze & PAR.IfNull(myReaderX("descrizione"), "") & " , "
                            Loop
                            myReaderX.Close()
                            If pertinenze <> "" Then
                                pertinenze = "dotata di " & Replace(Mid(pertinenze, 1, Len(pertinenze) - 3), ",", "e") & " quali elementi accessori"
                            End If
                        End If
                        myReaderX1.Close()

                        contenuto = Replace(contenuto, "$pertinenze$", pertinenze)


                        If InStr(contenuto, "$") > 0 Then
                            ERRORI = ERRORI & "Errore nel rapporto codice: " & myReader1("cod_contratto") & " Trovato segnaposto non sostituito.</br>"
                            srErrori.WriteLine("Errore nel rapporto codice: " & myReader1("cod_contratto") & " Trovato segnaposto non sostituito." & vbCrLf)
                            ok = "NO"
                        Else
                            sr.WriteLine(Mid(contenuto, 1, InStr(contenuto, "</Contratto>") + 12))
                            ok = "SI"
                        End If
                        File.Delete(fileName)

                    End If
                    myReader2.Close()
                    If ok = "SI" Then
                        Dim tpg As String = ""

                        If tiporegistrazione = "P" Then
                            tpg = "A"
                        Else
                            tpg = "U"
                        End If
                        PAR.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET versamento_tr='" & tpg & "', REG_TELEMATICA='" & NomeFile & "' WHERE ID=" & myReader1("ID")
                        PAR.cmd.ExecuteNonQuery()

                        srDettagli.WriteLine(myReader1("ID") & vbTab & CodiceContratto & vbTab & CodiceUtente & vbTab & TIPO_RAPPORTO & vbTab & Format(IMPORTOREGISTRAZIONE, "0.00") & vbTab & Format(CDbl(sanzione), "0.00") & vbTab & Format(Totale, "0.00") & vbTab & txtDataInvio.Text & vbTab & PAR.FormattaData(data_riferimento))

                        PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI,file_scaricato,CF_PIVA) VALUES (" & myReader1("ID") & "," & Year(Now) & ",'" & CodiceTributo & "','" & Format(Now, "yyyyMMdd") & "','" & PAR.AggiustaData(txtDataInvio.Text) & "'," & PAR.VirgoleInPunti(myReader1("IMP_CANONE_INIZIALE")) & "," & PAR.VirgoleInPunti(IMPORTOREGISTRAZIONE) & "," & Giorni & "," & PAR.VirgoleInPunti(sanzione) & "," & PAR.VirgoleInPunti(Totale) & ",'" & NomeFilezip & "','" & cfpiva & "')"
                        PAR.cmd.ExecuteNonQuery()
                    Else

                    End If
                Loop
                myReader1.Close()
                sr.WriteLine("</FileContratti>")
                sr.Close()
                srDettagli.Close()
                srErrori.Close()

                'non posso validare perchè i campi relativi al legale rappresentante non sono ammessi (cognomeRap)

                'Dim a As String = ValidaFile(NomeFile & ".XML")
                'If a <> "" Then
                '    myReader.Close()
                '    Label12.Visible = True
                '    Label12.Text = "Errore di validazione nel file <a href='elaborazioni/" & NomeFile & ".xml' target='_blank'>" & NomeFile & "</a> : " & a & "<br />PROCESSO INTERROTTO. Risolvere il problema e riprovare!"
                '    PAR.myTrans.Rollback()
                '    PAR.OracleConn.Close()
                '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '    Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")

                '    If ERRORI <> "" Then
                '        Session.Add("REPORTERRORIREG", ERRORI)
                '        Label12.Visible = True
                '        Label12.Text = Label12.Text & "</br><a href='ErroriRegRapporti.aspx' target='_blank'>Clicca qui per visualizzare i rapporti che presentano dati incongruenti</a>"
                '    End If

                '    Exit Sub
                'End If

                If ERRORI <> "" Then
                    Session.Add("REPORTERRORIREG", ERRORI)
                    Label12.Visible = True
                    Label12.Text = Label12.Text & "</br><a href='ErroriRegRapporti.aspx' target='_blank'>Clicca qui per visualizzare i rapporti che presentano dati incongruenti</a>"
                Else
                    File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\Errori_" & NomeFile & ".txt"))
                End If


            Loop
            myReader.Close()


            If i > 0 Then

                Dim kkK As Integer = 0

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                '
                Dim strFile As String

                For kkK = 0 To i - 1
                    strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".xml")
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
                    File.Delete(strFile)


                    strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".txt")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    '
                    strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)

                    Dim sFile1 As String = Path.GetFileName(strFile)
                    theEntry = New ZipEntry(sFile1)
                    fi = New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer1)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                    File.Delete(strFile)

                    If File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\Errori_" & ElencoFile(kkK) & ".txt")) Then
                        strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\Errori_" & ElencoFile(kkK) & ".txt")
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer12(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        '
                        strmFile.Read(abyBuffer12, 0, abyBuffer12.Length)

                        Dim sFile12 As String = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile12)
                        fi = New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer12)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer12, 0, abyBuffer12.Length)
                        File.Delete(strFile)

                    End If


                Next
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                PAR.myTrans.Commit()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                Response.Write("<script>location.href='ElencoRegistrazioni.aspx';</script>")
            Else
                Response.Write("<script>alert('Nessun Rapporto da trasmettere!');</script>")
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
            End If

        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Registrazione Contratti - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
        ''Response.Write("<script>location.replace('RisultatoContratti.aspx?DAL=" & PAR.AggiustaData(txtStipulaDal.Text) & "&AL=" & PAR.AggiustaData(txtStipulaAl.Text) & "');</script>")

    End Sub


End Class
