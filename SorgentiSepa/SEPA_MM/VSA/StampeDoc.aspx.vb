Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Drawing

Partial Class VSA_StampeDoc
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim idc As Long = 0


    Private Sub SettaPdf(ByVal pdf As PdfConverter)
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdf.LicenseKey = Licenza
        End If

        pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdf.PdfDocumentOptions.ShowHeader = False
        pdf.PdfDocumentOptions.ShowFooter = False
        pdf.PdfDocumentOptions.LeftMargin = 30
        pdf.PdfDocumentOptions.RightMargin = 30
        pdf.PdfDocumentOptions.TopMargin = 30
        pdf.PdfDocumentOptions.BottomMargin = 10
        pdf.PdfDocumentOptions.GenerateSelectablePdf = True

        pdf.PdfDocumentOptions.ShowHeader = False
        pdf.PdfDocumentOptions.ShowFooter = True
        'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
        pdf.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
        pdf.PdfFooterOptions.DrawFooterLine = False
        'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
        'pdfConverter1.PdfFooterOptions.ShowPageNumber = True



    End Sub

    Private Sub ZippaFiles(ByVal nomefile As String)
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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

    End Sub

    Private Function SostituisciBarcode(ByVal PercorsoBarCode As String, ByVal testoHTML As String) As String
        'Passare "PercorsoBarCode" con il percorso in cui viene salvato il documento. 
        'Ad es.: Server.MapPath("..\FileTemp\") + par.RicavaBarCode(3, id_dom)

        testoHTML = Replace(testoHTML, "$barcode$", PercorsoBarCode)

        Return testoHTML

    End Function

    Private Function caricaRespFiliale(ByVal idContra As String, ByVal conten As String) As String
        Try
            Dim Responsabile As String = ""
            Dim Acronimo As String = ""
            Dim dataPresent As String = ""
            Dim CentroDiCosto As String = ""
            Dim StringaIntera As String = ""

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ")
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader0.Read Then
                dataPresent = par.IfNull(myReader0("DATA_PRESENTAZIONE"), "")
            End If
            myReader0.Close()

            'If dataPresent < "20141201" Then
            '    dataPresent = "20141201"
            'Else
            '    dataPresent = Format(Now, "yyyyMMdd")
            'End If

            dataPresent = Format(Now, "yyyyMMdd")

            par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr, indirizzi.civico,indirizzi.cap, indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.FILIALI_UI WHERE unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto =" & idContra & " AND UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID AND indirizzi.ID = tab_filiali.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND INIZIO_VALIDITA <='" & dataPresent & "' AND FINE_VALIDITA >= '" & dataPresent & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                conten = Replace(conten, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
                conten = Replace(conten, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                conten = Replace(conten, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
                conten = Replace(conten, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))

                Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
                Acronimo = par.IfNull(myReader("ACRONIMO"), "")
                CentroDiCosto = par.IfNull(myReader("CENTRO_DI_COSTO"), "")

                conten = Replace(conten, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                conten = Replace(conten, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))

                conten = Replace(conten, "$responsabile$", Responsabile)
                conten = Replace(conten, "$acronimo$", "PCC/" & Acronimo)
                conten = Replace(conten, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

                If CentroDiCosto <> "" Then
                    StringaIntera = CentroDiCosto & "/"
                End If
                If Acronimo <> "" Then
                    StringaIntera = StringaIntera & Acronimo & "/"
                End If

                conten = Replace(conten, "$cds/acr/pg$", StringaIntera & Request.QueryString("PROT"))
                conten = Replace(conten, "$centrodicosto$", StringaIntera & Request.QueryString("PROT"))


                If par.IfNull(myReader("firma"), "") <> "" Then
                    'conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader("firma"), "") & "' />")
                    conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & par.IfNull(myReader("firma"), "") & "' />")
                Else
                    conten = Replace(conten, "$firmaresponsabile$", "")
                End If
            Else
                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContra & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    conten = Replace(conten, "$nomefiliale$", par.IfNull(myReader2("NOME"), ""))
                    conten = Replace(conten, "$indirizzofiliale$", par.IfNull(myReader2("DESCR"), "") & " " & par.IfNull(myReader2("CIVICO"), ""))
                    conten = Replace(conten, "$capfiliale$", par.IfNull(myReader2("CAP"), ""))
                    conten = Replace(conten, "$cittafiliale$", par.IfNull(myReader2("LOCALITA"), ""))

                    Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
                    Acronimo = par.IfNull(myReader("ACRONIMO"), "")
                    conten = Replace(conten, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                    conten = Replace(conten, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                    conten = Replace(conten, "$responsabile$", Responsabile)
                    conten = Replace(conten, "$acronimo$", Acronimo)
                    conten = Replace(conten, "$nverde$", par.IfNull(myReader2("N_TELEFONO_VERDE"), ""))
                    conten = Replace(conten, "$centrodicosto$", "GL0000/" & Acronimo & "/" & Request.QueryString("PROT"))
                    If par.IfNull(myReader2("firma"), "") <> "" Then
                        ' conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader2("firma"), "") & "' />")
                        conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & par.IfNull(myReader2("firma"), "") & "' />")

                    Else
                        conten = Replace(conten, "$firmaresponsabile$", "")
                    End If
                Else
                    conten = Replace(conten, "$nomefiliale$", "")
                    conten = Replace(conten, "$indirizzofiliale$", "")
                    conten = Replace(conten, "$capfiliale$", "")
                    conten = Replace(conten, "$cittafiliale$", "")
                    Responsabile = ""
                    Acronimo = ""
                    conten = Replace(conten, "$telfiliale$", "")
                    conten = Replace(conten, "$faxfiliale$", "")
                    conten = Replace(conten, "$responsabile$", Responsabile)
                    conten = Replace(conten, "$acronimo$", Acronimo)
                    conten = Replace(conten, "$nverde$", "")
                    conten = Replace(conten, "$centrodicosto$", "")
                    conten = Replace(conten, "$firmaresponsabile$", "")
                End If
                myReader2.Close()
            End If
                myReader.Close()

                conten = Replace(conten, "$referente$", Session.Item("NOME_OPERATORE"))

                conten = Replace(conten, "$firmaResp$", "Il resp. della sede territoriale")


                'If Acronimo = "FILE" Or Acronimo = "FIRO" Or Acronimo = "FISE" Then
                '    conten = Replace(conten, "$firmaResp$", "Il Responsabile di Coordinamento di Filiali")
                '    conten = Replace(conten, "$sede$", "")
                '    conten = Replace(conten, "$coordinatore$", "Luigi Serati")
                '    conten = Replace(conten, "$firmaCoord$", "Luigi Serati")
                '    conten = Replace(conten, "$cognCoord$", "SERATI")
                '    conten = Replace(conten, "$nomeCoord$", "LUIGI")
                '    conten = Replace(conten, "$dataNascCoord$", "09/11/1952")
                '    conten = Replace(conten, "$luogoNascCoord$", "INVERUNO")
                '    conten = Replace(conten, "$provinciaNascCoord$", "MI")
                '    conten = Replace(conten, "$indirizzoCondomini$", "Milano Sud Ovest, Legnano, Rozzano")
                'Else
                '    conten = Replace(conten, "$firmaResp$", "Il Responsabile di Filiale")
                '    conten = Replace(conten, "$sede$", "MILANO")
                '    conten = Replace(conten, "$coordinatore$", Responsabile)
                '    conten = Replace(conten, "$firmaCoord$", "Giuseppe Riefolo")
                '    conten = Replace(conten, "$cognCoord$", "RIEFOLO")
                '    conten = Replace(conten, "$nomeCoord$", "GIUSEPPE")
                '    conten = Replace(conten, "$dataNascCoord$", "08/01/1954")
                '    conten = Replace(conten, "$luogoNascCoord$", "BARLETTA")
                '    conten = Replace(conten, "$provinciaNascCoord$", "BT")
            conten = Replace(conten, "$indirizzoCondomini$", "Via T. Pini, 1")
                'End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return conten

    End Function


    Private Function caricaStruttRefer(ByVal conten As String) As String
        Try
            Dim Responsabile As String = ""
            Dim Acronimo As String = ""

            par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
                                  & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile,  tab_filiali.centro_di_costo, " _
                                  & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
                                  & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
                                  & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
                                  & " And tab_filiali.id_indirizzo = indirizzi.ID " _
                                  & " AND operatori.ID ='" & Session.Item("ID_OPERATORE") & "'"
            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderJ.Read Then
                conten = Replace(conten, "$responsabile$", par.IfNull(myReaderJ("responsabile"), "___________"))
                conten = Replace(conten, "$firmaResp$", "Il Responsabile della Struttura")
                conten = Replace(conten, "$coordinatore$", par.IfNull(myReaderJ("responsabile"), "___________"))
                conten = Replace(conten, "$nomefiliale$", par.IfNull(myReaderJ("nome"), "___________"))
                conten = Replace(conten, "$indirizzofiliale$", par.IfNull(myReaderJ("indirizzo"), "___________"))
                conten = Replace(conten, "$capfiliale$", par.IfNull(myReaderJ("cap"), "___________"))
                conten = Replace(conten, "$cittafiliale$", par.IfNull(myReaderJ("localita"), "___________"))
                conten = Replace(conten, "$telfiliale$", par.IfNull(myReaderJ("n_telefono"), "___________"))
                conten = Replace(conten, "$faxfiliale$", par.IfNull(myReaderJ("n_fax"), "___________"))
                conten = Replace(conten, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderJ("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            End If
            myReaderJ.Close()

            conten = Replace(conten, "$referente$", Session.Item("NOME_OPERATORE"))

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return conten

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Select Case Request.QueryString("TIPO")
                '***** Modelli REVISIONE CANONE *****
                Case "RichRC"
                    pdfRichCanone()
                Case "DocMancRC"
                    pdfDocMancante()
                Case "AvvProcRC"
                    pdfAvvioProc()
                Case "AutoCertRC"
                    pdfAutocert()
                Case "EsNegatRC"
                    pdfEsitoNeg()
                Case "EsNegRiesameRC", "EsNegRiesNOoss"
                    pdfEsitoNegRies()
                Case "RapportoRECA", "EsPositRC", "EsitoPosDEF", "EsPosRCProvv"
                    rappSintetici()
                Case "SoprallRID"
                    pdfSoprall()
                    '***** Modelli AMPLIAMENTO *****
                Case "RicRichiesta"
                    pdfRicRichiesta()
                Case "DocMancanteAMPL"
                    pdfDocMancante()
                Case "StFamigliaAMPL"
                    pdfStatoFamiglia()
                Case "MoreUxorioAMPL"
                    pdfMoreUxorio()
                Case "AssistenzaAMPL"
                    pdfConvAssist()
                Case "AvvioProcAMPL"
                    pdfAvvioProc()
                Case "EsPositivoAMPL", "PresaAttoRientro", "EsPosRiesAMPL", "EsPosRiesRientro"
                    pdfEsitoPos()
                Case "EsNegativoAMPL"
                    pdfEsitoNeg()
                Case "PermanenzaAMPL1", "PermanenzaAMPL2"
                    pdfPermReqR()
                Case "SoprallAMPL"
                    pdfSoprall()
                Case "ComSopralAMPL"
                    pdfComSopral()
                Case "EsNegRiesameAMPL", "EsNegRiesameNOoss", "ProvvDefComune"
                    pdfEsitNegRies()
                Case "RapportoANF"
                    rappSintetici()

                    '***** Modelli SUBENTRO *****
                Case "AvvioProcSUB"
                    pdfAvvioProc()
                Case "DomandaSUB", "DomandaSUBFFOO"
                    pdfDomSub()
                Case "DocMancanteSUB"
                    pdfDocMancante()
                Case "PermReqRSUB"
                    pdfPermReqR()
                Case "SoprallSUB"
                    pdfSoprall()
                Case "ComSopralSUB"
                    pdfComSopral()
                Case "EsitoPosSUB", "EsitoPosFFOO", "EsitoPosFFOO2"
                    pdfEsitoPosSub()
                Case "EsitoPosComSUB"
                    pdfEsitoPosCom()
                Case "EsitoPosDRCSUB", "EsPosCondom"
                    pdfEsitoPosDRC()
                Case "ComGovSUB"
                    pdfComGov()
                Case "EsitNegSUB", "EsitNegFFOO1", "EsitNegFFOO2", "EsitNegFFOO3", "LettTrasfor", "LettDecadComune"
                    pdfEsitNeg()
                Case "EsitPosRiSUB"
                    pdfEsitPosRi()
                Case "EsitNegRiesSUB", "EsitNegRiesSUBNoOS"
                    pdfEsitNegRies()
                Case "RapportoVAIN", "RapportoVAINFFOO"
                    rappSintetici()


                    '***** Modelli VOLTURA *****
                Case "RicezRicVOL"
                    pdfDomSub()
                Case "AvvProcedVOL"
                    pdfAvvioProc()
                Case "ModuloSoprallVOL"
                    pdfSoprall()
                Case "SoprUtenteVOL"
                    pdfComSopral()
                Case "EsitoPosiVOL"
                    pdfEsitoPosSub()
                Case "EsitoNegaVOL"
                    pdfEsitNeg()
                Case "EsitoPosRiesVOL"
                    pdfEsitPosRi()
                Case "EsitoNegatRiesVOL"
                    pdfEsitNegRies()
                Case "EsitoNegatRiesNOVOL"
                    pdfEsitNegRies()
                Case "RapportoCAIN"
                    rappSintetici()
                Case "ComDEB"
                    pdfStampaDebito()


                    '***** Modelli OSPITALITA *****
                Case "RichOSP", "RichOSPbada", "RichOSPscol"
                    pdfDomSub()
                Case "StFamigliaOSP"
                    pdfStatoFamiglia()
                Case "DocMancanteOSP"
                    pdfDocMancante()
                Case "AvvioProcOSP"
                    pdfAvvioProc()
                Case "SopralluogoOSP"
                    pdfSoprall()
                Case "ComSoprallOSP"
                    pdfComSopral()
                Case "EsiNegatOSP"
                    pdfEsitNeg()
                Case "EsPositOSP", "EsPositOSPbada", "EsPositOSPscol"
                    pdfEsitoPosSub()
                Case "EsPosRiesOSP", "EsPosRiesOSPbada", "EsPosRiesOSPscol"
                    pdfEsitPosRi()
                Case "EsitoNegRiesOsservOSP", "EsitoNegRiesNoOsservOSP"
                    pdfEsitNegRies()
                Case "RapportoOSP"
                    rappSintetici()



                    '***** Modelli CAMBIO CONSENSUALE *****
                Case "RichCAMB"
                    pdfDomSub()
                Case "RichCAMB2"
                    pdfRichiesta2()
                Case "DichPermanenza", "DichPermanenza2"
                    pdfPermReqR()
                Case "DocMancCAMB", "DocMancCAMB2"
                    pdfDocMancante()
                Case "AvvProcedCAMB", "AvvProcedCAMB2"
                    pdfAvvioProc()
                Case "SoprallCAMB"
                    pdfSoprall()
                Case "ComSoprallCAMB", "ComSoprallCAMB2"
                    pdfComSopral()
                Case "EsPositCAMB", "EsPositCAMB2"
                    pdfEsitoPos()
                Case "EsNegaCAMB", "EsNegaCAMB2"
                    pdfEsitNeg()
                Case "ComDEB"
                    pdfStampaDebito()
                Case "RapportoCACO"
                    RicavaRappSint()

                Case "Frontespizio"
                    GeneraFrontespizio()
            End Select
        End If
    End Sub

    Private Function ottieniIDContr(ByVal codContr As String) As Long
        Dim idContr As Long = 0
        Try
            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContr = lettoreIDc(0)
            End If
            lettoreIDc.Close()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return idContr
    End Function

    Private Function ottieniDataPres(ByVal idDom As Long) As String
        Dim dataPres As String = ""
        Try
            par.cmd.CommandText = "select data_presentazione from domande_bando_vsa where id=" & idDom
            Dim lettoreData As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreData.Read Then
                dataPres = par.FormattaData(par.IfNull(lettoreData(0), ""))
            End If
            lettoreData.Close()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return dataPres
    End Function



#Region "Funzioni Stampa Rapporti Sintetici"

    Private Function caricaSituazione(ByVal tipoImport As String, ByVal idImport As String) As String

        Dim tabella1 As String = "<table>"
        Dim luogoNasc As String = ""
        Dim altriRedd As Integer = 0
        Dim redditi As Boolean = False
        Try
            If tipoImport = 1 Then 'DATA IMPORTATI DA VSA
                par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,T_TIPO_PARENTELA.DESCRIZIONE as PARENTE FROM COMP_NUCLEO_VSA,T_TIPO_PARENTELA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & idImport & " AND COMP_NUCLEO_VSA.GRADO_PARENTELA = T_TIPO_PARENTELA.COD AND NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE(+)=COMP_NUCLEO_VSA.ID order by COMP_NUCLEO_VSA.ID asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReader.Read
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        luogoNasc = par.IfNull(myReader2("NOME"), "")
                    End If
                    myReader2.Close()

                    tabella1 = tabella1 & "<tr><td><b>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</b> Nato/a il " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "<br/> a " & luogoNasc & ": <b>" & myReader("PARENTE") & "</b></td></tr>"

                    par.cmd.CommandText = "SELECT SUM(dipendente) AS DIPENDENTE,SUM(pensione) AS PENSIONE,SUM(non_imponibili) AS NON_IMP,SUM(autonomo) AS AUTONOMO,SUM(dom_ag_fab) AS FABB,SUM(occasionali) AS OCCAS FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE=" & myReader("ID")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("DIPENDENTE"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Lavoro dipendente: " & par.Converti(Format(par.IfNull(myReader3("DIPENDENTE"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("PENSIONE"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Pensione: " & par.Converti(Format(par.IfNull(myReader3("PENSIONE"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("NON_IMP"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Non imponibili: " & par.Converti(Format(par.IfNull(myReader3("NON_IMP"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("AUTONOMO"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Autonomo: " & par.Converti(Format(par.IfNull(myReader3("AUTONOMO"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                            altriRedd = 1
                        End If
                        If par.IfNull(myReader3("FABB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Dom./Agr./Fabbr.: " & par.Converti(Format(par.IfNull(myReader3("FABB"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("OCCAS"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Occasionali: " & par.Converti(Format(par.IfNull(myReader3("OCCAS"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If redditi = False Then
                            tabella1 = tabella1 & "<tr><td><i>Nessun reddito</i></td></tr>"
                        End If
                        'tabella1 = tabella1 & "<tr><td><i>" & par.IfNull(myReader3("TIPO_LAV"), "") & "</i></td></tr>"
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS DETRAZIONI FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("DETRAZIONI"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Detrazioni: " & par.Converti(Format(par.IfNull(myReader3("DETRAZIONI"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS PATR_MOB FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("PATR_MOB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Patrimonio Mobiliare: " & par.Converti(Format(par.IfNull(myReader3("PATR_MOB"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(VALORE) AS PATR_IMMOB FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("PATR_IMMOB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Patrimonio Immobiliare: " & par.Converti(Format(par.IfNull(myReader3("PATR_IMMOB"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()


                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS INTEGR FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("INTEGR"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Altri redditi: " & par.Converti(Format(par.IfNull(myReader3("INTEGR"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()
                End While
                myReader.Close()


                tabella1 = tabella1 & "</table>"

            End If
            If tipoImport = 2 Then 'DATI IMPORTATI DA ANAGRAFE UTENZA
                par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE as PARENTE FROM UTENZA_COMP_NUCLEO,T_TIPO_PARENTELA WHERE ID_DICHIARAZIONE=" & idImport & " AND UTENZA_COMP_NUCLEO.GRADO_PARENTELA = T_TIPO_PARENTELA.COD"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReader.Read
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM UTENZA_COMP_NUCLEO WHERE ID=" & myReader("ID") & ")"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        luogoNasc = par.IfNull(myReader2("NOME"), "")
                    End If
                    myReader2.Close()

                    tabella1 = tabella1 & "<tr><td><b>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</b> Nato/a il " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "<br/> a " & luogoNasc & ": <b>" & myReader("PARENTE") & "</b></td></tr>"

                    'par.cmd.CommandText = "SELECT (CASE WHEN SUM(UTENZA_REDDITI.DIPENDENTE) <> 0 THEN 'Lavoro dipendente: ' ||SUM(UTENZA_REDDITI.DIPENDENTE)|| '' WHEN SUM(UTENZA_REDDITI.PENSIONE) <> 0 THEN 'Pensione: ' ||SUM(UTENZA_REDDITI.PENSIONE)||'' WHEN SUM(UTENZA_REDDITI.AUTONOMO) <> 0 THEN 'Lavoro Autonomo: ' ||SUM(UTENZA_REDDITI.AUTONOMO)||'' end) as TIPO_LAV FROM UTENZA_REDDITI WHERE ID_COMPONENTE=" & myReader("ID")
                    'Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If myReader3.Read = False Then
                    '    tabella1 = tabella1 & "<tr><td><i>Nessun reddito</i></td></tr>"
                    'Else
                    '    tabella1 = tabella1 & "<tr><td><i>" & par.IfNull(myReader3("TIPO_LAV"), "") & "</i></td></tr>"
                    'End If
                    'myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(dipendente) AS DIPENDENTE,SUM(pensione) AS PENSIONE,SUM(non_imponibili) AS NON_IMP,SUM(autonomo) AS AUTONOMO,SUM(dom_ag_fab) AS FABB,SUM(occasionali) AS OCCAS FROM UTENZA_REDDITI WHERE ID_COMPONENTE=" & myReader("ID")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("DIPENDENTE"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Lavoro dipendente: " & par.Converti(Format(par.IfNull(myReader3("DIPENDENTE"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("PENSIONE"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Pensione: " & par.Converti(Format(par.IfNull(myReader3("PENSIONE"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("NON_IMP"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Non imponibili: " & par.Converti(Format(par.IfNull(myReader3("NON_IMP"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("AUTONOMO"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Autonomo: " & par.Converti(Format(par.IfNull(myReader3("AUTONOMO"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                            altriRedd = 1
                        End If
                        If par.IfNull(myReader3("FABB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Dom./Agr./Fabbr.: " & par.Converti(Format(par.IfNull(myReader3("FABB"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("OCCAS"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Occasionali: " & par.Converti(Format(par.IfNull(myReader3("OCCAS"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If redditi = False Then
                            tabella1 = tabella1 & "<tr><td><i>Nessun reddito</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS DETRAZIONI FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("DETRAZIONI"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Detrazioni: " & par.Converti(Format(par.IfNull(myReader3("DETRAZIONI"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS PATR_MOB FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("PATR_MOB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Patrimonio Mobiliare: " & par.Converti(Format(par.IfNull(myReader3("PATR_MOB"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(VALORE) AS PATR_IMMOB FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("PATR_IMMOB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Patrimonio Immobiliare: " & par.Converti(Format(par.IfNull(myReader3("PATR_IMMOB"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS INTEGR FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("INTEGR"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Altri redditi: " & par.Converti(Format(par.IfNull(myReader3("INTEGR"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()
                End While
                myReader.Close()

                tabella1 = tabella1 & "</table>"

            End If
            If tipoImport = 3 Then
                par.cmd.CommandText = "SELECT COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE as PARENTE FROM COMP_NUCLEO,T_TIPO_PARENTELA WHERE ID_DICHIARAZIONE=" & idImport & " AND COMP_NUCLEO.GRADO_PARENTELA = T_TIPO_PARENTELA.COD order by COMP_NUCLEO.ID asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReader.Read
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO WHERE ID=" & myReader("ID") & ")"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        luogoNasc = par.IfNull(myReader2("NOME"), "")
                    End If
                    myReader2.Close()

                    tabella1 = tabella1 & "<tr><td><b>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</b> Nato/a il " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & " a " & luogoNasc & ": <b>" & myReader("PARENTE") & "</b></td></tr>"

                    par.cmd.CommandText = "SELECT SUM(dipendente) AS DIPENDENTE,SUM(pensione) AS PENSIONE,SUM(non_imponibili) AS NON_IMP,SUM(autonomo) AS AUTONOMO,SUM(dom_ag_fab) AS FABB,SUM(occasionali) AS OCCAS FROM DOMANDE_REDDITI WHERE ID_COMPONENTE=" & myReader("ID")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("DIPENDENTE"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Lavoro dipendente: " & par.Converti(Format(par.IfNull(myReader3("DIPENDENTE"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("PENSIONE"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Pensione: " & par.Converti(Format(par.IfNull(myReader3("PENSIONE"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("NON_IMP"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Non imponibili: " & par.Converti(Format(par.IfNull(myReader3("NON_IMP"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("AUTONOMO"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Autonomo: " & par.Converti(Format(par.IfNull(myReader3("AUTONOMO"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                            altriRedd = 1
                        End If
                        If par.IfNull(myReader3("FABB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Dom./Agr./Fabbr.: " & par.Converti(Format(par.IfNull(myReader3("FABB"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If par.IfNull(myReader3("OCCAS"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Occasionali: " & par.Converti(Format(par.IfNull(myReader3("OCCAS"), 0), "##,##0.00")) & "</i></td></tr>"
                            redditi = True
                        End If
                        If redditi = False Then
                            tabella1 = tabella1 & "<tr><td><i>Nessun reddito</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS DETRAZIONI FROM COMP_DETRAZIONI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("DETRAZIONI"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Detrazioni: " & par.Converti(Format(par.IfNull(myReader3("DETRAZIONI"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS PATR_MOB FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("PATR_MOB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Patrimonio Mobiliare: " & par.Converti(Format(par.IfNull(myReader3("PATR_MOB"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()

                    par.cmd.CommandText = "SELECT SUM(VALORE) AS PATR_IMMOB FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        If par.IfNull(myReader3("PATR_IMMOB"), 0) <> 0 Then
                            tabella1 = tabella1 & "<tr><td><i>Patrimonio Immobiliare: " & par.Converti(Format(par.IfNull(myReader3("PATR_IMMOB"), 0), "##,##0.00")) & "</i></td></tr>"
                        End If
                    End If
                    myReader3.Close()
                End While
                myReader.Close()


                tabella1 = tabella1 & "</table>"

            End If
            If tipoImport = 4 Then
                par.cmd.CommandText = "SELECT ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & idImport & " AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReader.Read
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD ='" & myReader("COD_COMUNE_NASCITA") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        luogoNasc = par.IfNull(myReader2("NOME"), "")
                    End If
                    myReader2.Close()

                    tabella1 = tabella1 & "<tr><td><b>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</b> Nato/a il " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "<br/> a " & luogoNasc & ": <b>" & myReader("PARENTE") & "</b></td></tr>"
                End While
                myReader.Close()

                tabella1 = tabella1 & "</table>"

            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return tabella1

    End Function

    Private Function ConversioneInOrdinale(ByVal numero As Integer) As String
        Dim ordinale As String = ""

        Select Case numero
            Case 1
                ordinale = "primo"
            Case 2
                ordinale = "secondo"
            Case 3
                ordinale = "terzo"
            Case 4
                ordinale = "quarto"
            Case 5
                ordinale = "quinto"
            Case 6
                ordinale = "sesto"
            Case 7
                ordinale = "settimo"
            Case 8
                ordinale = "ottavo"
            Case 9
                ordinale = "nono"
            Case 10
                ordinale = "decimo"
        End Select

        Return ordinale

    End Function

    Private Function MesiProssimaBollett(ByVal ProssimaBoll As String) As String
        Dim PeriodoMesi As String = ""

        Select Case ProssimaBoll
            Case "01"
                PeriodoMesi = "gennaio - febbraio"
            Case "03"
                PeriodoMesi = "marzo - aprile"
            Case "05"
                PeriodoMesi = "maggio - giugno"
            Case "07"
                PeriodoMesi = "luglio - agosto"
            Case "09"
                PeriodoMesi = "settembre - ottobre"
            Case "11"
                PeriodoMesi = "novembre - dicembre"
        End Select

        Return PeriodoMesi

    End Function

    Private Sub RicavaRappSint()
        Try
            Dim codContr As String = ""
            Dim codContr2 As String = ""
            Dim codUi As String = ""
            Dim codUi2 As String = ""
            Dim idDich As Long = 0
            Dim idDich2 As Long = 0

            Dim contenuto1 As String = ""
            Dim contenuto2 As String = ""

            Dim contenutoGlobale As String = ""
            Dim sr1 As StreamReader
            If Request.QueryString("TIPO") = "RapportoCACO" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\Rapporto_CACO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim contenutoORIG As String = contenuto
            Dim contenutoCC As String = contenuto

            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")
            codUi = Request.QueryString("CODUNITA")
            idDich = Request.QueryString("IDDICHIARAZ")

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM siscom_mi.unita_contrattuale, siscom_mi.rapporti_utenza WHERE rapporti_utenza.ID = unita_contrattuale.id_contratto AND rapporti_utenza.cod_contratto = '" & codContr2 & "' AND tipologia = 'AL'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                codUI2 = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT ID_DICHIARAZIONE FROM DOMANDE_BANDO_VSA WHERE PG IN (SELECT PG_COLLEGATO FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDich & ")"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                idDich2 = par.IfNull(myReader(0), 0)
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            contenuto1 = rappSinteticoCC(1, contenuto, contenutoCC, codContr, codUi, idDich)
            'contenuto = contenutoORIG
            contenuto2 = rappSinteticoCC(2, contenuto1, contenuto1, codContr2, codUi2, idDich2)


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = ""
            nomefile = "R1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto2, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function rappSinteticoCC(ByVal dom As Integer, ByVal contenuto As String, ByVal contenuto2 As String, ByVal codContr As String, ByVal codUi As String, ByVal idDich As Long) As String
        Dim stringaFinale As String = ""
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            'Dim sr1 As StreamReader
            'If Request.QueryString("TIPO") = "RapportoCACO" Then
            '    sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\Rapporto_CACO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            'End If

            'Dim contenuto As String = sr1.ReadToEnd()
            'sr1.Close()

            Dim id_dom As Long
            Dim luogoNasc As String = ""
            Dim siglaNas As String = ""
            Dim codFisc As String = ""
            Dim siglaRes As String = ""
            Dim tipoImport As String = ""
            Dim idTipoImport As String = ""
            Dim dataEvento As String = ""

            Dim richiedente As String = ""
            Dim causaleDom As String = ""
            Dim dataInizioVal As String = ""
            Dim annoRedditi As Integer
            Dim dataAutorizz As String = ""
            Dim IDUNITA As Integer = 0

            Dim REDD_DIP As Double = 0
            Dim REDD_ALT As Double = 0

            Dim LOCATIVO As String = ""
            Dim comunicazioni As String = ""
            Dim sISEE As String = ""
            Dim sISE As String = ""
            Dim sISR As String = ""
            Dim sISP As String = ""
            Dim sVSE As String = ""
            Dim sREDD_DIP As String = ""
            Dim sREDD_ALT As String = ""
            Dim sLimitePensione As String = ""
            Dim sPER_VAL_LOC As String = ""
            Dim sPERC_INC_MAX_ISE_ERP As String = ""
            Dim sCANONE_MIN As String = ""
            Dim sISE_MIN As String = ""
            Dim sCanone As String = ""
            Dim sNOTE As String = ""
            Dim sDEM As String = ""
            Dim sSUPCONVENZIONALE As String = ""
            Dim sCOSTOBASE As String = ""
            Dim sZONA As String = ""
            Dim sMOTIVODECADENZA As String = ""

            Dim sPIANO As String = ""
            Dim sCONSERVAZIONE As String = ""
            Dim sVETUSTA As String = ""
            Dim sPSE As String = ""
            Dim sINCIDENZAISE As String = ""
            Dim sCOEFFFAM As String = ""
            Dim sSOTTOAREA As String = ""
            Dim sNUMCOMP As String = ""
            Dim sNUMCOMP66 As String = ""
            Dim sNUMCOMP100 As String = ""
            Dim sNUMCOMP100C As String = ""
            Dim sPREVDIP As String = ""
            Dim sDETRAZIONI As String = ""
            Dim sMOBILIARI As String = ""
            Dim sIMMOBILIARI As String = ""
            Dim sCOMPLESSIVO As String = ""
            Dim sDETRAZIONIF As String = ""
            Dim sANNOCOSTRUZIONE As String = ""
            Dim sLOCALITA As String = ""
            Dim sASCENSORE As String = ""
            Dim sDESCRIZIONEPIANO As String = ""
            Dim sSUPNETTA As String = ""
            Dim sALTRESUP As String = ""
            Dim sMINORI15 As String = ""
            Dim sMAGGIORI65 As String = ""
            Dim sSUPACCESSORI As String = ""
            Dim sVALORELOCATIVO As String = ""
            Dim sCANONECLASSE As String = ""
            Dim sCANONESOPP As String = ""
            Dim sVALOCIICI As String = ""
            Dim sALLOGGIOIDONEO As String = ""
            Dim sISTAT As String = ""
            Dim sCANONECLASSEISTAT As String = ""
            Dim sANNOINIZIOVAL As String = ""
            Dim sANNOFINEVAL As String = ""

            Dim TestoFile As String = ""
            Dim AreaEconomica As Integer = 0
            Dim NuovoCanone As Double = 0


            'codUi = Request.QueryString("CODUNITA")
            'codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codUI$", codUi)
            contenuto2 = Replace(contenuto2, "$codUICC$", codUi)

            contenuto = Replace(contenuto, "$codcontratto$", codContr)
            contenuto2 = Replace(contenuto2, "$codcontrattoCC$", codContr)


            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & idDich & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoNasc = par.IfNull(myReader("NOME"), "")
                siglaNas = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$provnascita$", siglaNas)
            contenuto2 = Replace(contenuto2, "$provnascitaCC$", siglaNas)

            contenuto = Replace(contenuto, "$comunasc$", luogoNasc)
            contenuto2 = Replace(contenuto2, "$comunascCC$", luogoNasc)

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE COD_UNITA_IMMOBILIARE='" & codUi & "' AND ID_UNITA_PRINCIPALE IS NULL"
            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1234.Read() Then
                IDUNITA = par.IfNull(myReader1234("ID_UNITA"), 0)
            End If
            myReader1234.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_UNITA_PRINCIPALE='" & IDUNITA & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$uidipend$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                contenuto2 = Replace(contenuto2, "$uidipendCC$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
            Else
                contenuto = Replace(contenuto, "$uidipend$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                contenuto2 = Replace(contenuto2, "$uidipendCC$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DOMANDE_BANDO_VSA.PG_COLLEGATO AS PG_DOMCOLL,DICHIARAZIONI_VSA.PG AS PG_DICH,DICHIARAZIONI_VSA.ID AS IDDICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPOVIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,SINDACATI_VSA.DESCRIZIONE AS SINDACATO " _
            & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA,SINDACATI_VSA WHERE DICHIARAZIONI_VSA.ID_SINDACATO_VSA =SINDACATI_VSA.ID(+) AND DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
            & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & idDich & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$modelliStampati$", ElencoStampe(par.IfNull(myReader("IDDICH"), "")))

                tipoImport = par.IfNull(myReader("TIPO_D_IMPORT"), "")
                idTipoImport = par.IfNull(myReader("ID_D_IMPORT"), "")
                richiedente = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")
                codFisc = par.IfNull(myReader("COD_FISCALE"), "")
                annoRedditi = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                dataAutorizz = par.FormattaData(par.IfNull(myReader("DATA_AUTORIZZAZIONE"), ""))
                contenuto = Replace(contenuto, "$pg_dom$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PG_DOMCOLL"), ""))
                contenuto2 = Replace(contenuto2, "$pg_domCC$", par.IfNull(myReader("PG_DOM"), ""))

                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto2 = Replace(contenuto2, "$pgdichiarazioneCC$", par.IfNull(myReader("PG_DICH"), ""))

                contenuto = Replace(contenuto, "$richiedente$", richiedente)
                contenuto2 = Replace(contenuto2, "$richiedenteCC$", richiedente)

                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                contenuto2 = Replace(contenuto2, "$proceduraCC$", par.IfNull(myReader("MOT_DOMANDA"), ""))

                contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("TELEFONO_REC_DNTE"), ""))
                contenuto2 = Replace(contenuto2, "$telefonoCC$", par.IfNull(myReader("TELEFONO_REC_DNTE"), ""))

                contenuto = Replace(contenuto, "$dataNasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto2 = Replace(contenuto2, "$dataNascCC$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))

                contenuto = Replace(contenuto, "$codFisc$", codFisc)
                contenuto2 = Replace(contenuto2, "$codFiscCC$", codFisc)

                contenuto = Replace(contenuto, "$numDoc$", par.IfNull(myReader("CARTA_I"), ""))
                contenuto2 = Replace(contenuto2, "$numDocCC$", par.IfNull(myReader("CARTA_I"), ""))

                contenuto = Replace(contenuto, "$dataDoc$", par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")))
                contenuto2 = Replace(contenuto2, "$dataDocCC$", par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")))

                contenuto = Replace(contenuto, "$comuneRilas$", par.IfNull(myReader("CARTA_I_RILASCIATA"), ""))
                contenuto2 = Replace(contenuto2, "$comuneRilasCC$", par.IfNull(myReader("CARTA_I_RILASCIATA"), ""))

                contenuto = Replace(contenuto, "$permsogg$", par.IfNull(myReader("PERMESSO_SOGG_N"), ""))
                contenuto2 = Replace(contenuto2, "$permsoggCC$", par.IfNull(myReader("PERMESSO_SOGG_N"), ""))

                contenuto = Replace(contenuto, "$rilpermsog$", par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_DATA"), "")))
                contenuto2 = Replace(contenuto2, "$rilpermsogCC$", par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_DATA"), "")))


                dataEvento = par.IfNull(myReader("DATA_EVENTO"), "")
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$annoReddito$", annoRedditi)
                contenuto2 = Replace(contenuto2, "$annoRedditoCC$", annoRedditi)

                par.cmd.CommandText = "select * from TEMPI_PROCESSI_VSA where ID_MOTIVO_DOMANDA=" & par.IfNull(myReader("ID_MOTIVO_DOMANDA"), "")
                Dim myReaderGG As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderGG.Read Then
                    contenuto = Replace(contenuto, "$tempoProcesso$", par.IfNull(myReaderGG("TEMPO_GG"), 0))
                End If
                myReaderGG.Close()

                Select Case par.IfNull(myReader("MOD_PRESENTAZIONE"), "-1")
                    Case 0
                        contenuto = Replace(contenuto, "$modPres$", "di persona")
                    Case 1
                        contenuto = Replace(contenuto, "$modPres$", "a mezzo posta")
                    Case 2
                        contenuto = Replace(contenuto, "$modPres$", "verifica d'ufficio")
                    Case 3
                        contenuto = Replace(contenuto, "$modPres$", "sindacato - " & par.IfNull(myReader("SINDACATO"), "") & "")
                    Case 4
                        contenuto = Replace(contenuto, "$modPres$", "altro - " & par.IfNull(myReader("MOD_PRES_ALTRO"), "") & "")
                End Select

                'dataInizioVal = par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VAL"), ""))
                'contenuto = Replace(contenuto, "$isee$", par.IfNull(myReader("ISEE"), 0))
                'contenuto = Replace(contenuto, "$iserp$", par.Converti(Format(par.IfNull(myReader("ISE_ERP"), 0), "##,##0.00")))
                'contenuto = Replace(contenuto, "$pse$", par.IfNull(par.IfNull(myReader("PSE"), 0), 0))
                'contenuto = Replace(contenuto, "$dataInizioVal$", dataInizioVal)
                'contenuto = Replace(contenuto, "$dataFineVal$", par.FormattaData(par.IfNull(myReader("DATA_FINE_VAL"), "")))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
                contenuto2 = Replace(contenuto2, "$numalloggioCC$", par.IfNull(myReader("INTERNO"), ""))

                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO"), ""))
                contenuto2 = Replace(contenuto2, "$indirizzoCC$", par.IfNull(myReader("INDIRIZZO"), ""))

                contenuto = Replace(contenuto, "$numcivico$", par.IfNull(myReader("CIVICO"), ""))
                contenuto2 = Replace(contenuto2, "$numcivicoCC$", par.IfNull(myReader("CIVICO"), ""))

                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReader("CAP"), ""))
                contenuto2 = Replace(contenuto2, "$capCC$", par.IfNull(myReader("CAP"), ""))

                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("COMUNE"), ""))
                contenuto2 = Replace(contenuto2, "$localitaCC$", par.IfNull(myReader("COMUNE"), ""))


                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPOCONT FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
            & "WHERE TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AND COD_CONTRATTO='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                idc = par.IfNull(myReader("ID"), "")
                contenuto = Replace(contenuto, "$datadecorr$", par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")))
                contenuto2 = Replace(contenuto2, "$datadecorrCC$", par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")))

                contenuto = Replace(contenuto, "$tipocontr$", par.IfNull(myReader("TIPOCONT"), ""))
                contenuto2 = Replace(contenuto2, "$tipocontrCC$", par.IfNull(myReader("TIPOCONT"), ""))

                If par.IfNull(myReader("PROVENIENZA_ASS"), "") = 10 Then
                    contenuto = Replace(contenuto, "$flagFFOO$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                    contenuto2 = Replace(contenuto2, "$flagFFOOCC$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                Else
                    contenuto = Replace(contenuto, "$flagFFOO$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                    contenuto2 = Replace(contenuto2, "$flagFFOOCC$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                End If
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM VSA_SOPRALLUOGHI WHERE ID_DOMANDA=" & id_dom
            Dim myReaderSopr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderSopr.Read Then
                contenuto = Replace(contenuto, "$datasopralluogo$", par.FormattaData(par.IfNull(myReader("DATA_SOPRALLUOGO"), "")))
                contenuto2 = Replace(contenuto2, "$datasopralluogoCC$", par.FormattaData(par.IfNull(myReader("DATA_SOPRALLUOGO"), "")))

                If par.IfNull(myReaderSopr("NOTE"), "") <> "" Then
                    contenuto = Replace(contenuto, "$sopralluogo$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/> Note: <i>" & par.IfNull(myReaderSopr("NOTE"), "") & "</i>")
                    contenuto2 = Replace(contenuto2, "$sopralluogoCC$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/> Note: <i>" & par.IfNull(myReaderSopr("NOTE"), "") & "</i>")
                Else
                    contenuto = Replace(contenuto, "$sopralluogo$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                    contenuto2 = Replace(contenuto2, "$sopralluogoCC$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                End If
            Else
                contenuto = Replace(contenuto, "$sopralluogo$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                contenuto2 = Replace(contenuto2, "$sopralluogoCC$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                contenuto = Replace(contenuto, "$datasopralluogo$", "")
                contenuto2 = Replace(contenuto2, "$datasopralluogoCC$", "")
            End If
            myReaderSopr.Close()


            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,UNITA_CONTRATTUALE.ID_UNITA,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
            & "EDIFICI.DENOMINAZIONE AS NOMEedificio,TIPO_DISPONIBILITA.DESCRIZIONE AS TIPOdispon " _
            & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_DISPONIBILITA," _
            & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE UNITA_IMMOBILIARI.id_unita_principale is null AND " _
            & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
            & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD  " _
            & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SC"), "--"))
                contenuto2 = Replace(contenuto2, "$scalaCC$", par.IfNull(myReader("SC"), "--"))

                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIAN"), "--"))
                contenuto2 = Replace(contenuto2, "$pianoCC$", par.IfNull(myReader("PIAN"), "--"))

                contenuto = Replace(contenuto, "$tipoUI$", par.IfNull(myReader("COD_TIPOLOGIA"), ""))
                contenuto2 = Replace(contenuto2, "$tipoUICC$", par.IfNull(myReader("COD_TIPOLOGIA"), ""))


                If par.IfNull(myReader("TIPOdispon"), "") <> "Occupata abusivamente" Then
                    contenuto = Replace(contenuto, "$occupabus$", "NO")
                    contenuto2 = Replace(contenuto2, "$occupabusCC$", "NO")
                Else
                    contenuto = Replace(contenuto, "$occupabus$", par.IfNull(myReader("TIPOdispon"), ""))
                    contenuto2 = Replace(contenuto2, "$occupabusCC$", par.IfNull(myReader("TIPOdispon"), ""))
                End If
                contenuto = Replace(contenuto, "$numVani$", par.IfNull(myReader("NUM_VANI"), ""))
                contenuto2 = Replace(contenuto2, "$numVaniCC$", par.IfNull(myReader("NUM_VANI"), ""))

                par.cmd.CommandText = "SELECT CONDOMINI.DENOMINAZIONE AS COND,AUTOGESTIONI.DENOMINAZIONE AS AUTOGEST FROM SISCOM_MI.EDIFICI,SISCOM_MI.COND_EDIFICI,SISCOM_MI.CONDOMINI,SISCOM_MI.AUTOGESTIONI_EDIFICI," _
                    & "SISCOM_MI.AUTOGESTIONI WHERE EDIFICI.ID = COND_EDIFICI.ID_EDIFICIO AND COND_EDIFICI.ID_CONDOMINIO = CONDOMINI.ID AND AUTOGESTIONI_EDIFICI.ID_EDIFICIO = EDIFICI.ID " _
                    & "AND AUTOGESTIONI_EDIFICI.ID_AUTOGESTIONE = AUTOGESTIONI.ID AND EDIFICI.ID = " & myReader("ID_EDIFICIO")
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$condominio$", par.IfNull(myReader("COND"), " "))
                    contenuto2 = Replace(contenuto2, "$condominioCC$", par.IfNull(myReader("COND"), " "))

                    contenuto = Replace(contenuto, "$autogestione$", par.IfNull(myReader("AUTOGEST"), " "))
                    contenuto2 = Replace(contenuto2, "$autogestioneCC$", par.IfNull(myReader("AUTOGEST"), " "))
                Else
                    contenuto = Replace(contenuto, "$condominio$", "no")
                    contenuto2 = Replace(contenuto2, "$condominioCC$", "no")

                    contenuto = Replace(contenuto, "$autogestione$", "no")
                    contenuto2 = Replace(contenuto2, "$autogestioneCC$", "no")

                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & myReader("ID_UNITA") & " AND COD_TIPOLOGIA='SUP_CONV'"
                Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader4.Read Then
                    contenuto = Replace(contenuto, "$supMQ$", par.IfNull(myReader4("VALORE"), ""))
                    contenuto2 = Replace(contenuto2, "$supMQCC$", par.IfNull(myReader4("VALORE"), ""))
                Else
                    contenuto = Replace(contenuto, "$supMQ$", "")
                    contenuto2 = Replace(contenuto2, "$supMQCC$", "")
                End If
                myReader4.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,SISCOM_MI.ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS RUOLO_PAR,TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCC,TIPOLOGIA_TITOLO.DESCRIZIONE AS VALENZA,SISCOM_MI.RAPPORTI_UTENZA.DATA_STIPULA " _
            & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.TIPOLOGIA_OCCUPANTE,SISCOM_MI.TIPOLOGIA_TITOLO WHERE " _
            & "RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_TITOLO = TIPOLOGIA_TITOLO.COD " _
            & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = TIPOLOGIA_OCCUPANTE.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND COD_FISCALE='" & codFisc & "' AND rapporti_utenza.cod_contratto ='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$cittadinanza$", par.IfNull(myReader("CITTADINANZA"), ""))
                contenuto2 = Replace(contenuto2, "$cittadinanzaCC$", par.IfNull(myReader("CITTADINANZA"), ""))

                If par.IfNull(myReader("TIPO_DOC"), 0) = 0 Then
                    contenuto = Replace(contenuto, "$tipoDoc$", "CARTA D'IDENTITA'")
                    contenuto2 = Replace(contenuto2, "$tipoDocCC$", "CARTA D'IDENTITA'")

                End If
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
            contenuto2 = Replace(contenuto2, "$originarioCC$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")


            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,SISCOM_MI.ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS RUOLO_PAR,TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCC,TIPOLOGIA_TITOLO.DESCRIZIONE AS VALENZA,SISCOM_MI.RAPPORTI_UTENZA.DATA_STIPULA " _
            & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.TIPOLOGIA_OCCUPANTE,SISCOM_MI.TIPOLOGIA_TITOLO WHERE " _
            & "RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_TITOLO = TIPOLOGIA_TITOLO.COD " _
            & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = TIPOLOGIA_OCCUPANTE.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND rapporti_utenza.cod_contratto ='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto2 = Replace(contenuto2, "$intestatarioCC$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))

                contenuto = Replace(contenuto, "$codFiscINT$", par.IfNull(myReader("COD_FISCALE"), ""))
                contenuto2 = Replace(contenuto2, "$codFiscINTCC$", par.IfNull(myReader("COD_FISCALE"), ""))

            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE AS DESCR FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_MANCANTI.ID_DOC AND VSA_DOC_MANCANTI.ID_DICHIARAZIONE=" & idDich & " "
            myReader = par.cmd.ExecuteReader
            Dim strTbl2 As String = ""
            While myReader.Read
                strTbl2 = strTbl2 & Trim(par.Elimina160(par.IfNull(myReader("DESCR"), ""))) & " "

            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$docMancante$", strTbl2)
            contenuto2 = Replace(contenuto2, "$docMancanteCC$", strTbl2)

            contenuto = caricaRespFiliale(idc, contenuto)


            par.cmd.CommandText = "SELECT COUNT(SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA) AS NUMCOMP FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.rapporti_utenza,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
            & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=rapporti_utenza.ID AND rapporti_utenza.cod_contratto ='" & codContr & "' AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numComp$", par.IfNull(myReader("NUMCOMP"), 0))
                contenuto2 = Replace(contenuto2, "$numCompCC$", par.IfNull(myReader("NUMCOMP"), 0))

            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_ALLEGATI.ID_DOC AND VSA_DOC_ALLEGATI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            While myReader.Read
                strTbl = strTbl & par.IfNull(myReader("DESCRIZIONE"), "") & " "
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$docAllegati$", strTbl)
            contenuto2 = Replace(contenuto2, "$docAllegatiCC$", strTbl)



            '--------------- CERCO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELL'EVENTO ----------------
            'contenuto = Replace(contenuto, "$anno1$", Left(dataEvento, 4))
            Dim annoInd As Integer = 0
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE INIZIO_VALIDITA_CAN<='" & dataEvento & "' AND FINE_VALIDITA_CAN>='" & dataEvento & "' AND ID_CONTRATTO=" & idc & " ORDER BY DATA_CALCOLO DESC "
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                annoInd = Left(par.IfNull(myReader("INIZIO_VALIDITA_CAN"), ""), 4) - 2

                contenuto = Replace(contenuto, "$anno1$", annoInd)
                contenuto2 = Replace(contenuto2, "$anno1CC$", annoInd)

                Select Case myReader("ID_AREA_ECONOMICA")
                    Case 1
                        contenuto = Replace(contenuto, "$area1$", "PROTEZIONE")
                        contenuto2 = Replace(contenuto2, "$area1CC$", "PROTEZIONE")
                    Case 2
                        contenuto = Replace(contenuto, "$area1$", "ACCESSO")
                        contenuto2 = Replace(contenuto2, "$area1CC$", "ACCESSO")
                    Case 3
                        contenuto = Replace(contenuto, "$area1$", "PERMANENZA")
                        contenuto2 = Replace(contenuto2, "$area1CC$", "PERMANENZA")
                    Case 4
                        contenuto = Replace(contenuto, "$area1$", "DECADENZA")
                        contenuto2 = Replace(contenuto2, "$area1CC$", "DECADENZA")
                End Select
                contenuto = Replace(contenuto, "$classe1$", par.IfNull(myReader("SOTTO_AREA"), ""))
                contenuto2 = Replace(contenuto2, "$classe1CC$", par.IfNull(myReader("SOTTO_AREA"), ""))

                contenuto = Replace(contenuto, "$isee1$", Format(CDbl(par.IfNull(myReader("ISEE"), 0)), "##,##0.00"))
                contenuto2 = Replace(contenuto2, "$isee1CC$", Format(CDbl(par.IfNull(myReader("ISEE"), 0)), "##,##0.00"))


            Else
                contenuto = Replace(contenuto, "$anno1$", "---")
                contenuto2 = Replace(contenuto2, "$anno1CC$", "---")

                contenuto = Replace(contenuto, "$area1$", "Non presente")
                contenuto2 = Replace(contenuto2, "$area1CC$", "Non presente")

                contenuto = Replace(contenuto, "$classe1$", "Non presente")
                contenuto2 = Replace(contenuto2, "$classe1CC$", "Non presente")

                contenuto = Replace(contenuto, "$isee1$", "Non presente")
                contenuto2 = Replace(contenuto2, "$isee1CC$", "Non presente")

            End If
            myReader.Close()

            Dim tabella1 As String = ""
            tabella1 = caricaSituazione(tipoImport, idTipoImport)

            contenuto = Replace(contenuto, "$tabellaComponenti$", tabella1)
            contenuto2 = Replace(contenuto2, "$tabellaComponentiCC$", tabella1)

            Dim tabella2 As String = ""
            tabella2 = caricaSituazione(1, idDich)
            contenuto = Replace(contenuto, "$tabellaComponenti2$", tabella2)
            contenuto2 = Replace(contenuto2, "$tabellaComponenti2CC$", tabella2)


            '--------------- CARICO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELLA PRESENTAZIONE ----------------
            TestoFile = Replace(par.CalcolaCanone27(CDbl(id_dom), 3, IDUNITA, codContr, NuovoCanone, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, Right(ottieniDataPres(id_dom), 4)), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO " & Right(ottieniDataPres(id_dom), 4))
            Select Case AreaEconomica
                Case 1
                    contenuto = Replace(contenuto, "$area2$", "PROTEZIONE")
                    contenuto2 = Replace(contenuto2, "$area2CC$", "PROTEZIONE")
                Case 2
                    contenuto = Replace(contenuto, "$area2$", "ACCESSO")
                    contenuto2 = Replace(contenuto2, "$area2CC$", "ACCESSO")
                Case 3
                    contenuto = Replace(contenuto, "$area2$", "PERMANENZA")
                    contenuto2 = Replace(contenuto2, "$area2CC$", "PERMANENZA")
                Case 4
                    contenuto = Replace(contenuto, "$area2$", "DECADENZA")
                    contenuto2 = Replace(contenuto2, "$area2CC$", "DECADENZA")
            End Select
            contenuto = Replace(contenuto, "$classe2$", sSOTTOAREA)
            contenuto2 = Replace(contenuto2, "$classe2CC$", sSOTTOAREA)

            contenuto = Replace(contenuto, "$isee2$", Format(CDbl(sISEE), "##,##0.00"))
            contenuto2 = Replace(contenuto2, "$isee2CC$", Format(CDbl(sISEE), "##,##0.00"))


            contenuto = Replace(contenuto, "$anno2$", annoRedditi)
            contenuto2 = Replace(contenuto2, "$anno2CC$", annoRedditi)

            If sISEE > 35000 Or sSOTTOAREA = "D8" Or sSOTTOAREA = "D9" Then
                contenuto = Replace(contenuto, "$permRequisiti$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                contenuto2 = Replace(contenuto2, "$permRequisitiCC$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
            Else
                contenuto = Replace(contenuto, "$permRequisiti$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
                contenuto2 = Replace(contenuto2, "$permRequisitiCC$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
            End If
            '--------------- CARICO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELLA PRESENTAZIONE ----------------

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("REQUISITO6"), "0") = "1" Then
                    contenuto = Replace(contenuto, "$reqReddito$", "sì")
                    contenuto2 = Replace(contenuto2, "$reqRedditoCC$", "sì")
                Else
                    contenuto = Replace(contenuto, "$reqReddito$", "no")
                    contenuto2 = Replace(contenuto2, "$reqRedditoCC$", "no")
                End If
                If par.IfNull(myReader("REQUISITO7"), "0") = "0" Then
                    contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
                    contenuto2 = Replace(contenuto2, "$originarioCC$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
                Else
                    contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                    contenuto2 = Replace(contenuto2, "$originarioCC$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                End If
                If par.IfNull(myReader("REQUISITO15"), "0") = "0" Then
                    contenuto = Replace(contenuto, "$flagOltre$", "sì")
                    contenuto2 = Replace(contenuto2, "$flagOltreCC$", "sì")
                Else
                    contenuto = Replace(contenuto, "$flagOltre$", "no")
                    contenuto2 = Replace(contenuto2, "$flagOltreCC$", "no")
                End If
                If par.IfNull(myReader("REQUISITO18"), "0") = "0" Then
                    contenuto = Replace(contenuto, "$flagSovraff$", "sì")
                    contenuto2 = Replace(contenuto2, "$flagSovraffCC$", "sì")
                Else
                    contenuto = Replace(contenuto, "$flagSovraff$", "no")
                    contenuto2 = Replace(contenuto2, "$flagSovraff$", "no")
                End If
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$flag$", "<img src='../block_SI.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
            contenuto2 = Replace(contenuto2, "$flagCC$", "<img src='../block_SI.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")

            contenuto = Replace(contenuto, "$dataEvento$", par.FormattaData(dataEvento))

            par.cmd.CommandText = "SELECT T_TIPO_DECISIONI_VSA.DESCRIZIONE,VSA_DECISIONI_REV_C.* FROM VSA_DECISIONI_REV_C,T_TIPO_DECISIONI_VSA WHERE ID_DOMANDA=" & id_dom & " AND VSA_DECISIONI_REV_C.COD_DECISIONE=T_TIPO_DECISIONI_VSA.COD and COD=1"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$proposta$", par.IfNull(myReader("DESCRIZIONE"), ""))
                contenuto = Replace(contenuto, "$dataDecisione1$", par.FormattaData(par.IfNull(myReader("DATA"), "")))
            Else
                contenuto = Replace(contenuto, "$proposta$", "")
                contenuto = Replace(contenuto, "$dataDecisione1$", "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT T_TIPO_DECISIONI_VSA.DESCRIZIONE,VSA_DECISIONI_REV_C.* FROM VSA_DECISIONI_REV_C,T_TIPO_DECISIONI_VSA WHERE ID_DOMANDA=" & id_dom & " AND VSA_DECISIONI_REV_C.COD_DECISIONE=T_TIPO_DECISIONI_VSA.COD and (COD=2 OR COD=3)"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("COD_DECISIONE"), "") = 2 Then
                    contenuto = Replace(contenuto, "$decisione$", "POSITIVO")
                Else
                    contenuto = Replace(contenuto, "$decisione$", "NEGATIVO")
                End If
                contenuto = Replace(contenuto, "$dataDecisione$", par.FormattaData(par.IfNull(myReader("DATA"), "")))
                contenuto = Replace(contenuto, "$osservaz$", par.IfNull(myReader("NOTE"), ""))
            Else
                contenuto = Replace(contenuto, "$decisione$", "")
                contenuto = Replace(contenuto, "$dataDecisione$", "")
                contenuto = Replace(contenuto, "$osservaz$", "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataUltima$", dataAutorizz)

            par.cmd.CommandText = "SELECT * FROM EVENTI_BANDI_VSA WHERE ID_DOMANDA = " & id_dom & " AND (COD_EVENTO = 'F179' or COD_EVENTO = 'F165' or COD_EVENTO = 'F196' or COD_EVENTO = 'F197' or COD_EVENTO = 'F204')"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$autorizzProvv$", "sì")
                If par.IfNull(myReader("COD_EVENTO"), "") = "F204" Then
                    contenuto = Replace(contenuto, "$fraseultima$", "Registrazione delle variazioni approvate")
                Else
                    contenuto = Replace(contenuto, "$fraseultima$", "Registrazione delle variazioni al nucleo familiare approvate")
                End If

            Else
                contenuto = Replace(contenuto, "$autorizzProvv$", "")
                'contenuto = Replace(contenuto, "$dataUltima$", "")
                contenuto = Replace(contenuto, "$fraseultima$", "")
                contenuto = Replace(contenuto, "$DatiFinali$", "")

            End If
            myReader.Close()

            'TABELLA RIESAME 
            Dim tblNegativo As String = ""
            Dim esito As String = ""
            par.cmd.CommandText = "SELECT T_TIPO_DECISIONI_VSA.DESCRIZIONE,VSA_DECISIONI_REV_C.* FROM VSA_DECISIONI_REV_C,T_TIPO_DECISIONI_VSA WHERE ID_DOMANDA=" & id_dom & " AND VSA_DECISIONI_REV_C.COD_DECISIONE=T_TIPO_DECISIONI_VSA.COD and (cod=4 or cod=5 or cod=6) AND DATA = (SELECT MAX(DATA) FROM VSA_DECISIONI_REV_C WHERE (COD_DECISIONE=4 or COD_DECISIONE=5 OR COD_DECISIONE=6))"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("COD_DECISIONE"), "") = 5 Then
                    esito = "POSITIVO"
                ElseIf par.IfNull(myReader("COD_DECISIONE"), "") = 6 Then
                    esito = "NEGATIVO"
                ElseIf par.IfNull(myReader("COD_DECISIONE"), "") = 4 Then
                    esito = par.IfNull(myReader("DESCRIZIONE"), "")
                End If

                tblNegativo = tblNegativo & "<table width='100%'><tr style='background-color: #CCCCFF' class='LeftNormale'><td width='400px'>Fase di Riesame</td><td>Presa in carico da " & Session.Item("NOME_OPERATORE") & ".</td></tr>" _
                & "<tr class='LeftNormale'><td>Parere</td><td><b>" & esito & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Eventuali Osservazioni</td><td><b>" & par.IfNull(myReader("NOTE"), "") & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Data Parere</td><td><b>" & par.FormattaData(par.IfNull(myReader("DATA"), "")) & "</b></td></tr>"
                tblNegativo = tblNegativo & "</table>"
            End If
            myReader.Close()

            If tblNegativo <> "" Then
                contenuto = Replace(contenuto, "$tblRiesame$", tblNegativo)
            Else
                contenuto = Replace(contenuto, "$tblRiesame$", "&nbsp")
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

        If dom = 1 Then
            stringaFinale = contenuto
        Else
            stringaFinale = contenuto2
        End If

        Return stringaFinale

    End Function

    Private Sub rappSintetici()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader
            If Request.QueryString("TIPO") = "RapportoANF" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\Rapporto_ANF.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RapportoRECA" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\Rapporto_RECA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RapportoCAIN" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\Rapporto_CAIN.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RapportoVAIN" Or Request.QueryString("TIPO") = "RapportoVAINFFOO" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\Rapporto_VAIN.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RapportoOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\Rapporto_OSPI.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPositRC" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\ComEsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPosRCProvv" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\ComEsitoPositivoProvv.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoPosDEF" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\ComEsitoPositDefinitivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim luogoNasc As String = ""
            Dim luogoNascNewComp As String = ""
            Dim siglaNas As String = ""
            Dim codFisc As String = ""
            Dim siglaRes As String = ""
            Dim tipoImport As String = ""
            Dim idTipoImport As String = ""
            Dim dataEvento As String = ""
            Dim DETRAZIONI_FRAGILE As Long
            Dim DETRAZIONI_FR As Long
            Dim TOT_SPESE As Long
            Dim INV_100_CON As Integer
            Dim INV_100_NO As Integer
            Dim INV_66_99 As Integer
            Dim TASSO_RENDIMENTO As Double
            Dim DETRAZIONI As Long
            Dim FIGURATIVO_MOBILI As Double
            Dim ISR_ERP As Double
            Dim ISEE_ERP As Double
            Dim REDD_DIP As Double = 0
            Dim REDD_ALT As Double = 0

            Dim LOCATIVO As String = ""
            Dim comunicazioni As String = ""
            Dim sISEE As String = ""
            Dim sISE As String = ""
            Dim sISR As String = ""
            Dim sISP As String = ""
            Dim sVSE As String = ""
            Dim sREDD_DIP As String = ""
            Dim sREDD_ALT As String = ""
            Dim sLimitePensione As String = ""
            Dim sPER_VAL_LOC As String = ""
            Dim sPERC_INC_MAX_ISE_ERP As String = ""
            Dim sCANONE_MIN As String = ""
            Dim sISE_MIN As String = ""
            Dim sCanone As String = ""
            Dim sNOTE As String = ""
            Dim sDEM As String = ""
            Dim sSUPCONVENZIONALE As String = ""
            Dim sCOSTOBASE As String = ""
            Dim sZONA As String = ""
            Dim sMOTIVODECADENZA As String = ""

            Dim sPIANO As String = ""
            Dim sCONSERVAZIONE As String = ""
            Dim sVETUSTA As String = ""
            Dim sPSE As String = ""
            Dim sINCIDENZAISE As String = ""
            Dim sCOEFFFAM As String = ""
            Dim sSOTTOAREA As String = ""
            Dim sNUMCOMP As String = ""
            Dim sNUMCOMP66 As String = ""
            Dim sNUMCOMP100 As String = ""
            Dim sNUMCOMP100C As String = ""
            Dim sPREVDIP As String = ""
            Dim sDETRAZIONI As String = ""
            Dim sMOBILIARI As String = ""
            Dim sIMMOBILIARI As String = ""
            Dim sCOMPLESSIVO As String = ""
            Dim sDETRAZIONIF As String = ""
            Dim sANNOCOSTRUZIONE As String = ""
            Dim sLOCALITA As String = ""
            Dim sASCENSORE As String = ""
            Dim sDESCRIZIONEPIANO As String = ""
            Dim sSUPNETTA As String = ""
            Dim sALTRESUP As String = ""
            Dim sMINORI15 As String = ""
            Dim sMAGGIORI65 As String = ""
            Dim sSUPACCESSORI As String = ""
            Dim sVALORELOCATIVO As String = ""
            Dim sCANONECLASSE As String = ""
            Dim sCANONESOPP As String = ""
            Dim sVALOCIICI As String = ""
            Dim sALLOGGIOIDONEO As String = ""
            Dim sISTAT As String = ""
            Dim sCANONECLASSEISTAT As String = ""
            Dim sANNOINIZIOVAL As String = ""
            Dim sANNOFINEVAL As String = ""
            Dim parte1 As String = ""
            Dim parte2 As String = ""
            Dim parte3 As String = ""
            Dim parte4 As String = ""


            Dim ANNO_INIZIO As Integer = 0
            Dim PER_ANNI As Integer = 0
            Dim dataFine As String = ""
            Dim dataInizioValidita As String = ""
            Dim dataFineBollettato As String = ""
            Dim dataBollettUltima As String = ""
            Dim IDUNITA As Integer = 0
            Dim TotaleCreditoDebito As Decimal = 0
            Dim TotaleCreditoANNO As Decimal = 0
            Dim TotaleCredito As Decimal = 0
            Dim TestoFile As String = ""
            Dim AreaEconomica As Integer = 0
            Dim NuovoCanone As Double = 0
            Dim NuovoTransit As Double = 0
            Dim TotaleEmesso As Decimal = 0

            Dim richiedente As String = ""
            Dim causaleDom As String = ""
            Dim dataInizioVal As String = ""
            Dim annoRedditi As Integer
            Dim dataAutorizz As String = ""

            Dim situazRedd As Boolean = True
            Dim ID_ISEE As Long = 0
            Dim IdDomBando As Long = 0

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codUI$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoNasc = par.IfNull(myReader("NOME"), "")
                siglaNas = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$modelliStampati$", ElencoStampe(Request.QueryString("IDDICHIARAZ")))

            contenuto = Replace(contenuto, "$provnascita$", siglaNas)
            contenuto = Replace(contenuto, "$comunasc$", luogoNasc)
            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE COD_UNITA_IMMOBILIARE='" & codUi & "' AND ID_UNITA_PRINCIPALE IS NULL"
            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1234.Read() Then
                IDUNITA = par.IfNull(myReader1234("ID_UNITA"), 0)
            End If
            myReader1234.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_UNITA_PRINCIPALE='" & IDUNITA & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$uidipend$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
            Else
                contenuto = Replace(contenuto, "$uidipend$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPOVIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,SINDACATI_VSA.DESCRIZIONE AS SINDACATO " _
            & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA,SINDACATI_VSA WHERE DICHIARAZIONI_VSA.ID_SINDACATO_VSA =SINDACATI_VSA.ID(+) AND DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
            & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"

            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                tipoImport = par.IfNull(myReader("TIPO_D_IMPORT"), "")
                idTipoImport = par.IfNull(myReader("ID_D_IMPORT"), "")
                richiedente = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")
                codFisc = par.IfNull(myReader("COD_FISCALE"), "")
                causaleDom = par.IfNull(myReader("CAUSALE_DOM"), "")
                annoRedditi = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                dataAutorizz = par.FormattaData(par.IfNull(myReader("DATA_AUTORIZZAZIONE"), ""))
                contenuto = Replace(contenuto, "$pg_dom$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$richiedente$", richiedente)
                contenuto = Replace(contenuto, "$causale$", causaleDom)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("TELEFONO_REC_DNTE"), ""))
                contenuto = Replace(contenuto, "$dataNasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$codFisc$", codFisc)
                contenuto = Replace(contenuto, "$numDoc$", par.IfNull(myReader("CARTA_I"), ""))
                contenuto = Replace(contenuto, "$dataDoc$", par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")))
                contenuto = Replace(contenuto, "$comuneRilas$", par.IfNull(myReader("CARTA_I_RILASCIATA"), ""))
                contenuto = Replace(contenuto, "$permsogg$", par.IfNull(myReader("PERMESSO_SOGG_N"), ""))
                contenuto = Replace(contenuto, "$rilpermsog$", par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_DATA"), "")))

                dataEvento = par.IfNull(myReader("DATA_EVENTO"), "")
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$annoReddito$", annoRedditi)
                par.cmd.CommandText = "select * from TEMPI_PROCESSI_VSA where ID_MOTIVO_DOMANDA=" & par.IfNull(myReader("ID_MOTIVO_DOMANDA"), "")
                Dim myReaderGG As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderGG.Read Then
                    contenuto = Replace(contenuto, "$tempoProcesso$", par.IfNull(myReaderGG("TEMPO_GG"), 0))
                End If
                myReaderGG.Close()

                Select Case par.IfNull(myReader("MOD_PRESENTAZIONE"), "-1")
                    Case 0
                        contenuto = Replace(contenuto, "$modPres$", "di persona")
                    Case 1
                        contenuto = Replace(contenuto, "$modPres$", "a mezzo posta")
                    Case 2
                        contenuto = Replace(contenuto, "$modPres$", "verifica d'ufficio")
                    Case 3
                        contenuto = Replace(contenuto, "$modPres$", "sindacato - " & par.IfNull(myReader("SINDACATO"), "") & "")
                    Case 4
                        contenuto = Replace(contenuto, "$modPres$", "altro - " & par.IfNull(myReader("MOD_PRES_ALTRO"), "") & "")
                End Select

                dataInizioVal = par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VAL"), ""))
                contenuto = Replace(contenuto, "$isee$", par.IfNull(myReader("ISEE"), 0))
                contenuto = Replace(contenuto, "$iserp$", par.Converti(Format(par.IfNull(myReader("ISE_ERP"), 0), "##,##0.00")))
                contenuto = Replace(contenuto, "$pse$", par.IfNull(par.IfNull(myReader("PSE"), 0), 0))
                contenuto = Replace(contenuto, "$dataInizioVal$", dataInizioVal)
                contenuto = Replace(contenuto, "$dataFineVal$", par.FormattaData(par.IfNull(myReader("DATA_FINE_VAL"), "")))

                INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
                INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
                INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
                TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), 0))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$numcivico$", par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReader("CAP"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("COMUNE"), ""))

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)

            End If
            myReader.Close()

            Dim dataBollettazGener As String = ""
            par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza_prossima_bol WHERE prossima_bolletta IS NOT NULL ORDER BY prossima_bolletta DESC"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Select Case Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), ""), 5, 2)
                    Case "01", "03", "05", "07", "08", "10", "12"
                        dataBollettazGener = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "31")))
                    Case "02"
                        dataBollettazGener = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "28")))
                    Case "04", "06", "09", "11"
                        dataBollettazGener = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "30")))
                End Select
            End If
            myReader.Close()

            Dim periodoNextBoll As String = ""
            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPOCONT FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
            & "WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AND COD_CONTRATTO='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                ID_ISEE = par.IfNull(myReader("ID_ISEE"), 0)
                idc = par.IfNull(myReader("ID"), "")
                'Select Case Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), ""), 5, 2)
                '    Case "01", "03", "05", "07", "08", "10", "12"
                '        dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "31")))
                '    Case "02"
                '        dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "28")))
                '    Case "04", "06", "09", "11"
                '        dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "30")))
                'End Select
                '*** CASE ERRATO NEL CALCOLO DI dataBollettUltima, MARCO 18/07/2017 *** 
                dataBollettUltima = CDate(par.FormattaData(myReader("PROSSIMA_BOLLETTA") & "01")).AddDays(-1)

                periodoNextBoll = MesiProssimaBollett(Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), ""), 5, 2))
                contenuto = Replace(contenuto, "$periodoBoll$", periodoNextBoll)

                'dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "31")))
                contenuto = Replace(contenuto, "$datadecorr$", par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")))
                contenuto = Replace(contenuto, "$tipocontr$", par.IfNull(myReader("TIPOCONT"), ""))
                If par.IfNull(myReader("PROVENIENZA_ASS"), "") = 10 Then
                    contenuto = Replace(contenuto, "$flagFFOO$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                Else
                    contenuto = Replace(contenuto, "$flagFFOO$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * from DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & ID_ISEE
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                IdDomBando = par.IfNull(myReader("ID"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_SOPRALLUOGHI WHERE ID_DOMANDA=" & id_dom
            Dim myReaderSopr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderSopr.Read Then
                If par.IfNull(myReaderSopr("NOTE"), "") <> "" Then
                    contenuto = Replace(contenuto, "$sopralluogo$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/> Note: <i>" & par.IfNull(myReaderSopr("NOTE"), "") & "</i>")
                Else
                    contenuto = Replace(contenuto, "$sopralluogo$", "<img src='../block_SI_Checked.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
                End If
            Else
                contenuto = Replace(contenuto, "$sopralluogo$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
            End If
            myReaderSopr.Close()


            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,UNITA_CONTRATTUALE.ID_UNITA,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
            & "EDIFICI.DENOMINAZIONE AS NOMEedificio,TIPO_DISPONIBILITA.DESCRIZIONE AS TIPOdispon " _
            & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_DISPONIBILITA," _
            & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE UNITA_IMMOBILIARI.id_unita_principale is null AND " _
            & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
            & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD  " _
            & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SC"), "--"))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIAN"), "--"))
                contenuto = Replace(contenuto, "$tipoUI$", par.IfNull(myReader("COD_TIPOLOGIA"), ""))

                If par.IfNull(myReader("TIPOdispon"), "") <> "Occupata abusivamente" Then
                    contenuto = Replace(contenuto, "$occupabus$", "NO")
                Else
                    contenuto = Replace(contenuto, "$occupabus$", par.IfNull(myReader("TIPOdispon"), ""))
                End If
                contenuto = Replace(contenuto, "$numVani$", par.IfNull(myReader("NUM_VANI"), ""))

                par.cmd.CommandText = "SELECT CONDOMINI.DENOMINAZIONE AS COND,AUTOGESTIONI.DENOMINAZIONE AS AUTOGEST FROM SISCOM_MI.EDIFICI,SISCOM_MI.COND_EDIFICI,SISCOM_MI.CONDOMINI,SISCOM_MI.AUTOGESTIONI_EDIFICI," _
                    & "SISCOM_MI.AUTOGESTIONI WHERE EDIFICI.ID = COND_EDIFICI.ID_EDIFICIO AND COND_EDIFICI.ID_CONDOMINIO = CONDOMINI.ID AND AUTOGESTIONI_EDIFICI.ID_EDIFICIO = EDIFICI.ID " _
                    & "AND AUTOGESTIONI_EDIFICI.ID_AUTOGESTIONE = AUTOGESTIONI.ID AND EDIFICI.ID = " & myReader("ID_EDIFICIO")
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$condominio$", par.IfNull(myReader("COND"), " "))
                    contenuto = Replace(contenuto, "$autogestione$", par.IfNull(myReader("AUTOGEST"), " "))
                Else
                    contenuto = Replace(contenuto, "$condominio$", "no")
                    contenuto = Replace(contenuto, "$autogestione$", "no")
                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & myReader("ID_UNITA") & " AND COD_TIPOLOGIA='SUP_CONV'"
                Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader4.Read Then
                    contenuto = Replace(contenuto, "$supMQ$", par.IfNull(myReader4("VALORE"), ""))
                Else
                    contenuto = Replace(contenuto, "$supMQ$", "")
                End If
                myReader4.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,SISCOM_MI.ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS RUOLO_PAR,TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCC,TIPOLOGIA_TITOLO.DESCRIZIONE AS VALENZA,SISCOM_MI.RAPPORTI_UTENZA.DATA_STIPULA " _
            & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.TIPOLOGIA_OCCUPANTE,SISCOM_MI.TIPOLOGIA_TITOLO WHERE " _
            & "RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_TITOLO = TIPOLOGIA_TITOLO.COD " _
            & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = TIPOLOGIA_OCCUPANTE.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND COD_FISCALE='" & codFisc & "' AND rapporti_utenza.cod_contratto ='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$cittadinanza$", par.IfNull(myReader("CITTADINANZA"), ""))
                If par.IfNull(myReader("TIPO_DOC"), 0) = 0 Then
                    contenuto = Replace(contenuto, "$tipoDoc$", "CARTA D'IDENTITA'")
                End If
                'If par.IfNull(myReader("DATA_INIZIO"), "") > par.IfNull(myReader("DATA_STIPULA"), "") Then
                '    contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                'Else
                '    contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
                'End If
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")

            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,SISCOM_MI.ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS RUOLO_PAR,TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCC,TIPOLOGIA_TITOLO.DESCRIZIONE AS VALENZA,SISCOM_MI.RAPPORTI_UTENZA.DATA_STIPULA " _
            & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.TIPOLOGIA_OCCUPANTE,SISCOM_MI.TIPOLOGIA_TITOLO WHERE " _
            & "RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_TITOLO = TIPOLOGIA_TITOLO.COD " _
            & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = TIPOLOGIA_OCCUPANTE.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND rapporti_utenza.cod_contratto ='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$codFiscINT$", par.IfNull(myReader("COD_FISCALE"), ""))
            End If
            myReader.Close()

            'contenuto = Replace(contenuto, "$numComp$", par.IfNull(myReader("N_COMP_NUCLEO"), ""))
            par.cmd.CommandText = "SELECT COUNT(SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA) AS NUMCOMP FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.rapporti_utenza,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
            & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=rapporti_utenza.ID AND rapporti_utenza.cod_contratto ='" & codContr & "' AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numComp$", par.IfNull(myReader("NUMCOMP"), 0))
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_ALLEGATI.ID_DOC AND VSA_DOC_ALLEGATI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            While myReader.Read
                strTbl = strTbl & par.IfNull(myReader("DESCRIZIONE"), "") & " "
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$docAllegati$", strTbl)

            par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE AS DESCR FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_MANCANTI.ID_DOC AND VSA_DOC_MANCANTI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim strTbl2 As String = ""
            While myReader.Read
                strTbl2 = strTbl2 & Trim(par.Elimina160(par.IfNull(myReader("DESCR"), ""))) & " "

            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$docMancante$", strTbl2)

            If Request.QueryString("TIPO") = "RapportoVAINFFOO" Then
                contenuto = caricaStruttRefer(contenuto)
            Else
                contenuto = caricaRespFiliale(idc, contenuto)
            End If

            '--------------- CERCO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELL'EVENTO ----------------
            'contenuto = Replace(contenuto, "$anno1$", Left(dataEvento, 4))

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE INIZIO_VALIDITA_CAN<='" & dataEvento & "' AND FINE_VALIDITA_CAN>='" & dataEvento & "' AND ID_CONTRATTO=" & idc & " ORDER BY DATA_CALCOLO DESC "
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                Select Case par.IfNull(myReader("ID_AREA_ECONOMICA"), -1)
                    Case 1
                        contenuto = Replace(contenuto, "$area1$", "PROTEZIONE")
                    Case 2
                        contenuto = Replace(contenuto, "$area1$", "ACCESSO")
                    Case 3
                        contenuto = Replace(contenuto, "$area1$", "PERMANENZA")
                    Case 4
                        contenuto = Replace(contenuto, "$area1$", "DECADENZA")
                End Select
                contenuto = Replace(contenuto, "$classe1$", par.IfNull(myReader("SOTTO_AREA"), ""))
                contenuto = Replace(contenuto, "$isee1$", Format(CDbl(par.IfNull(myReader("ISEE"), 0)), "##,##0.00"))
            Else
                contenuto = Replace(contenuto, "$area1$", "Non presente")
                contenuto = Replace(contenuto, "$classe1$", "Non presente")
                contenuto = Replace(contenuto, "$isee1$", "Non presente")
            End If
            myReader.Close()

            Dim annoIndagine As Integer = 0

            Select Case tipoImport
                Case 1
                    par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & idTipoImport
                    Dim myReaderUT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderUT.Read Then
                        annoIndagine = par.IfNull(myReaderUT("ANNO_SIT_ECONOMICA"), 0)
                    End If
                    myReaderUT.Close()
                Case 2
                    par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & idTipoImport
                    Dim myReaderUT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderUT.Read Then
                        annoIndagine = par.IfNull(myReaderUT("ANNO_SIT_ECONOMICA"), 0)
                    End If
                    myReaderUT.Close()
                Case 3
                    par.cmd.CommandText = "SELECT BANDI.ANNO_ISEE FROM BANDI,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=" & idTipoImport & " AND BANDI.ID=DOMANDE_BANDO.ID_BANDO"
                    Dim myReaderUT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderUT.Read Then
                        annoIndagine = par.IfNull(myReaderUT("ANNO_ISEE"), 0)
                    End If
                    myReaderUT.Close()

            End Select


            If tipoImport = 4 Then
                par.cmd.CommandText = "SELECT T_MOTIVO_DOMANDA_VSA.*,ANNO_SIT_ECONOMICA,DOMANDE_BANDO_VSA.ID AS IDDOM,DOMANDE_BANDO_VSA.* FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.DATA_FINE_VAL<='" & dataEvento & "' AND DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & codContr & "' ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
                Dim myReaderVSA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderVSA.Read Then
                    tipoImport = 1
                    idTipoImport = par.IfNull(myReaderVSA("ID_DICHIARAZIONE"), "")
                    annoIndagine = par.IfNull(myReaderVSA("ANNO_SIT_ECONOMICA"), 0)
                Else
                    par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.DATA_FINE_VAL<='" & dataEvento & "' AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                    & "AND RAPPORTO='" & codContr & "' ORDER BY ID_BANDO DESC"
                    Dim myReaderAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAU.Read Then
                        tipoImport = 2
                        idTipoImport = par.IfNull(myReaderAU("ID"), "")
                        annoIndagine = par.IfNull(myReaderAU("ANNO_SIT_ECONOMICA"), 0)
                    End If
                    myReaderAU.Close()
                End If
                myReaderVSA.Close()
            End If

            Dim tabella1 As String = ""
            tabella1 = caricaSituazione(tipoImport, idTipoImport)


            contenuto = Replace(contenuto, "$tabellaComponenti$", tabella1)
            '--------------- FINE CERCO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELL'EVENTO ----------------

            Dim contaNewComp As Integer = 0
            Dim tblNewComp As String = "<table width='100%'>"
            If Request.QueryString("TIPO") = "RapportoOSP" Then
                par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.DATA_NASC AS DATA_NASCITA,COMP_NUCLEO_OSPITI_VSA.*,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA FROM COMP_NUCLEO_OSPITI_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO WHERE ID_DOMANDA=" & id_dom & " AND COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA = DOMANDE_BANDO_VSA.ID AND T_TIPO_INDIRIZZO.COD=COMP_NUCLEO_OSPITI_VSA.ID_TIPO_IND_RES_DNTE"
            Else
                par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,NUOVI_COMP_NUCLEO_VSA.*,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA,T_TIPO_INDIRIZZO WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE AND T_TIPO_INDIRIZZO.COD=NUOVI_COMP_NUCLEO_VSA.ID_TIPO_IND_RES_DNTE"
            End If
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                contaNewComp = contaNewComp + 1
                If Request.QueryString("TIPO") = "RapportoOSP" Then
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_OSPITI_VSA WHERE ID=" & myReader("ID") & ")"
                Else
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID_COMPONENTE") & ")"
                    tblNewComp = tblNewComp & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>"
                End If
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    luogoNascNewComp = par.IfNull(myReader2("NOME"), "")
                End If
                myReader2.Close()

                '& "<tr class='LeftNormale'><td>Tipo documento d'identità</td><td>&nbsp</td></tr>" _


                tblNewComp = tblNewComp & "<tr><td style='font-weight: bold; font-size: 16px; font-family: Arial;color: #0000FF;width:400px'><b>" & contaNewComp & " Ospite</b></td><td>&nbsp;</td></tr>" _
                & "<tr><td>&nbsp;</td></tr><tr class='LeftNormale'><td>Nominativo Ospite</td><td><b>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Data Nascita</td><td><b>" & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "</b></td></tr><tr class='LeftNormale'><td>Luogo Nascita</td><td><b>" & luogoNascNewComp & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Codice Fiscale</td><td><b>" & par.IfNull(myReader("COD_FISCALE"), "") & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Numero documento</td><td><b>" & par.IfNull(myReader("CARTA_I"), "") & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Data rilascio</td><td><b>" & par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")) & "</b></td></tr><tr class='LeftNormale'><td>Autorità rilasciante</td><td><b>" & par.IfNull(myReader("CARTA_I_RILASCIATA"), "") & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Località</td><td><b>" & par.IfNull(myReader("COMUNE_RES_DNTE"), "") & "</b></td></tr><tr class='LeftNormale'><td>Ev.permesso di soggiorno (N. e data)</td><td><b>" & par.IfNull(myReader("PERMESSO_SOGG_N"), "") & " " & par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_DATA"), "")) & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Scadenza Pemesso di Soggiorno</td><td>&nbsp</td></tr>" _
                & "<tr class='LeftNormale'><td>Residenza " & ConversioneInOrdinale(contaNewComp) & " Ospite</td><td><b>" & par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), "") & " " _
                & par.IfNull(myReader("CAP_RES_DNTE"), "") & " - " & par.IfNull(myReader("COMUNE_RES_DNTE"), "") & "</b></td></tr><tr><td>&nbsp</td></tr>"

            End While
            myReader.Close()
            tblNewComp = tblNewComp & "</table>"

            contenuto = Replace(contenuto, "$numNuoviComp$", contaNewComp)
            contenuto = Replace(contenuto, "$tabellaNuoviComp$", tblNewComp)

            Dim tabella2 As String = ""
            tabella2 = caricaSituazione(1, Request.QueryString("IDDICHIARAZ"))
            contenuto = Replace(contenuto, "$tabellaComponenti2$", tabella2)

            'Dim dataCanoneUltimo As String = ""
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & idc & " AND DATA_CALCOLO = (SELECT MAX(DATA_CALCOLO) FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & idc & ")"
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then
            '    Select Case myReader("ID_AREA_ECONOMICA")
            '        Case 1
            '            contenuto = Replace(contenuto, "$area2$", "PROTEZIONE")
            '        Case 2
            '            contenuto = Replace(contenuto, "$area2$", "ACCESSO")
            '        Case 3
            '            contenuto = Replace(contenuto, "$area2$", "PERMANENZA")
            '    End Select
            '    contenuto = Replace(contenuto, "$classe2$", par.IfNull(myReader("SOTTO_AREA"), ""))
            '    dataCanoneUltimo = par.IfNull(myReader("DATA_CALCOLO"), "")
            'End If
            'myReader.Close()


            '--------------- CARICO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELLA PRESENTAZIONE ----------------
            TestoFile = Replace(par.CalcolaCanone27RECA(id_dom, 3, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, Right(ottieniDataPres(id_dom), 4)), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO " & Right(ottieniDataPres(id_dom), 4))

            'MAX 09_10_2019 COMMENTIAMO PERCHè IN FASE DI STAMPA ESITO POSITIVO DEFINITIVO INSERISCE I DATI DEL PRIMO ANNO DI VALIDITA' E NON DELL'ULTIMO.
            'Select Case AreaEconomica
            '    Case 1
            '        contenuto = Replace(contenuto, "$area2$", "PROTEZIONE")
            '    Case 2
            '        contenuto = Replace(contenuto, "$area2$", "ACCESSO")
            '    Case 3
            '        contenuto = Replace(contenuto, "$area2$", "PERMANENZA")
            '    Case 4
            '        contenuto = Replace(contenuto, "$area2$", "DECADENZA")
            'End Select
            'contenuto = Replace(contenuto, "$classe2$", sSOTTOAREA)
            'contenuto = Replace(contenuto, "$isee2$", Format(CDbl(sISEE), "##,##0.00"))
            ''...FINE

            contenuto = Replace(contenuto, "$anno2$", annoRedditi)
            If sISEE > 35000 Or sSOTTOAREA = "D8" Or sSOTTOAREA = "D9" Then
                contenuto = Replace(contenuto, "$permRequisiti$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
            Else
                contenuto = Replace(contenuto, "$permRequisiti$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
            End If
            '--------------- CARICO SITUAZIONE REDDITUALE e ANAGRAFICA ALLA DATA DELLA PRESENTAZIONE ----------------



            Dim REDDITO_COMPLESSIVO As Double = 0
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE= " & Request.QueryString("IDDICHIARAZ")
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                Dim myReaderR1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReaderR1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR1("REDDITO_IRPEF"), 0) + par.IfNull(myReaderR1("PROV_AGRARI"), 0)
                End While
                myReaderR1.Close()


                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                Dim myReaderR2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReaderR2.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR2("IMPORTO"), 0)
                End While
                myReaderR2.Close()


                DETRAZIONI_FRAGILE = 0
                par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                Dim myReaderDetr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderDetr.HasRows Then
                    While myReaderDetr.Read
                        DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReaderDetr("IMPORTO"), 0)
                        TOT_SPESE = TOT_SPESE + par.IfNull(myReaderDetr("IMPORTO"), 0)

                        If DETRAZIONI_FRAGILE > 10000 Then
                            DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                        Else
                            DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        End If

                    End While
                    myReaderDetr.Close()
                Else
                    If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        TOT_SPESE = TOT_SPESE + 10000
                    End If
                    myReaderDetr.Close()
                End If

            End While
            myReader.Close()

            par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from DOMANDE_REDDITI_VSA where ID_DOMANDA=" & id_dom
            Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderW.Read Then
                REDD_DIP = par.IfNull(myReaderW(0), 0)
            End If
            myReaderW.Close()

            par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from DOMANDE_REDDITI_VSA where ID_DOMANDA=" & id_dom
            myReaderW = par.cmd.ExecuteReader()
            If myReaderW.Read Then
                REDD_ALT = par.IfNull(myReaderW(0), 0)
            End If
            myReaderW.Close()

            If REDD_DIP > ((REDD_ALT + REDD_DIP) * 80) / 100 Then
                contenuto = Replace(contenuto, "$reddPrev$", "DIPENDENTE")
            Else
                contenuto = Replace(contenuto, "$reddPrev$", "ALTRO")
            End If

            DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP
            contenuto = Replace(contenuto, "$reddTot$", par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")))
            contenuto = Replace(contenuto, "$reddEff$", par.Converti(Format(ISR_ERP, "##,##0.00")))

            'Dim flag1 As Boolean = False
            'Dim flag2 As Boolean = False
            'Dim flag3 As Boolean = False
            'Dim flag4 As Boolean = False
            'Dim flag5 As Boolean = False
            'par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & id_dom
            'myReaderW = par.cmd.ExecuteReader()
            'If myReaderW.HasRows Then
            '    While myReaderW.Read
            '        If myReaderW("ID") = 1 Then
            '            contenuto = Replace(contenuto, "$flag1$", "sì")
            '            flag1 = True
            '        ElseIf flag1 = False Then
            '            contenuto = Replace(contenuto, "$flag1$", "<img src=block.gif width=10 height=10 border=1>")
            '        End If

            '        If myReaderW("ID") = 2 Then
            '            contenuto = Replace(contenuto, "$flag2$", "sì")
            '            flag2 = True
            '        ElseIf flag2 = False Then
            '            contenuto = Replace(contenuto, "$flag2$", "<img src=block.gif width=10 height=10 border=1>")
            '        End If

            '        If myReaderW("ID") = 3 Then
            '            contenuto = Replace(contenuto, "$flag3$", "sì")
            '            flag3 = True
            '        ElseIf flag3 = False Then
            '            contenuto = Replace(contenuto, "$flag3$", "<img src=block.gif width=10 height=10 border=1>")
            '        End If

            '        If myReaderW("ID") = 4 Then
            '            contenuto = Replace(contenuto, "$flag4$", "sì")
            '            flag4 = True
            '        ElseIf flag4 = False Then
            '            contenuto = Replace(contenuto, "$flag4$", "<img src=block.gif width=10 height=10 border=1>")
            '        End If

            '        If myReaderW("ID") = 5 Then
            '            contenuto = Replace(contenuto, "$flag5$", "sì")
            '            flag5 = True
            '        ElseIf flag5 = False Then
            '            contenuto = Replace(contenuto, "$flag5$", "<img src=block.gif width=10 height=10 border=1>")
            '        End If
            '    End While
            'End If
            'myReaderW.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                'For i As Integer = 1 To 19
                '    If par.IfNull(myReader("REQUISITO" & i & ""), "0") = "1" Then
                '        contenuto = Replace(contenuto, "$permRequisiti$", "sì")
                '    Else
                '        contenuto = Replace(contenuto, "$permRequisiti$", "no")
                '        Exit For
                '    End If
                'Next
                If par.IfNull(myReader("REQUISITO6"), "0") = "1" Then
                    contenuto = Replace(contenuto, "$reqReddito$", "sì")
                Else
                    contenuto = Replace(contenuto, "$reqReddito$", "no")
                End If
                If par.IfNull(myReader("REQUISITO7"), "0") = "0" Then
                    contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
                Else
                    contenuto = Replace(contenuto, "$originario$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
                End If
                If par.IfNull(myReader("REQUISITO15"), "0") = "0" Then
                    contenuto = Replace(contenuto, "$flagOltre$", "sì")
                Else
                    contenuto = Replace(contenuto, "$flagOltre$", "no")
                End If
                If par.IfNull(myReader("REQUISITO18"), "0") = "0" Then
                    contenuto = Replace(contenuto, "$flagSovraff$", "sì")
                Else
                    contenuto = Replace(contenuto, "$flagSovraff$", "no")
                End If
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT DISTINCT(EDIFICI.ID) FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codUi & "' " _
            & "AND EDIFICI.ID IN (SELECT DISTINCT(ID_EDIFICIO) FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO = EDIFICI.ID)"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$condom$", "<img src='../block_SI_Checked.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO.gif' alt='checked' width='20' height='20' border='1'>")
            Else
                contenuto = Replace(contenuto, "$condom$", "<img src='../block_SI.gif' alt='si' width='20' height='20' border='1'/> <img src='../block_NO_Checked.gif' alt='checked' width='20' height='20' border='1'>")
            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$flag$", "<img src='../block_SI.gif' alt='no' width='20' height='20' border='1'> <img src='../block_NO.gif' alt='si' width='20' height='20' border='1'/>")
            contenuto = Replace(contenuto, "$dataEvento$", par.FormattaData(dataEvento))

            par.cmd.CommandText = "SELECT T_TIPO_DECISIONI_VSA.DESCRIZIONE,VSA_DECISIONI_REV_C.* FROM VSA_DECISIONI_REV_C,T_TIPO_DECISIONI_VSA WHERE ID_DOMANDA=" & id_dom & " AND VSA_DECISIONI_REV_C.COD_DECISIONE=T_TIPO_DECISIONI_VSA.COD and COD=1"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$proposta$", par.IfNull(myReader("DESCRIZIONE"), ""))
                contenuto = Replace(contenuto, "$dataDecisione1$", par.FormattaData(par.IfNull(myReader("DATA"), "")))
            Else
                contenuto = Replace(contenuto, "$proposta$", "")
                contenuto = Replace(contenuto, "$dataDecisione1$", "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT T_TIPO_DECISIONI_VSA.DESCRIZIONE,VSA_DECISIONI_REV_C.* FROM VSA_DECISIONI_REV_C,T_TIPO_DECISIONI_VSA WHERE ID_DOMANDA=" & id_dom & " AND VSA_DECISIONI_REV_C.COD_DECISIONE=T_TIPO_DECISIONI_VSA.COD and (COD=2 OR COD=3)"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("COD_DECISIONE"), "") = 2 Then
                    contenuto = Replace(contenuto, "$decisione$", "POSITIVO")
                Else
                    contenuto = Replace(contenuto, "$decisione$", "NEGATIVO")
                End If
                contenuto = Replace(contenuto, "$dataDecisione$", par.FormattaData(par.IfNull(myReader("DATA"), "")))
                contenuto = Replace(contenuto, "$osservaz$", par.IfNull(myReader("NOTE"), ""))
            Else
                contenuto = Replace(contenuto, "$decisione$", "")
                contenuto = Replace(contenuto, "$dataDecisione$", "")
                contenuto = Replace(contenuto, "$osservaz$", "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataUltima$", dataAutorizz)

            Dim tblRichiedente As String = ""

            par.cmd.CommandText = "SELECT * FROM EVENTI_BANDI_VSA WHERE ID_DOMANDA = " & id_dom & " AND (COD_EVENTO = 'F179' or COD_EVENTO = 'F165' or COD_EVENTO = 'F196' or COD_EVENTO = 'F197' or COD_EVENTO = 'F204')"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$autorizzProvv$", "sì")
                'contenuto = Replace(contenuto, "$dataUltima$", par.FormattaData(par.IfNull(myReader("DATA_ORA"), "")))
                If par.IfNull(myReader("COD_EVENTO"), "") = "F204" Then
                    contenuto = Replace(contenuto, "$fraseultima$", "Registrazione delle variazioni approvate")
                Else
                    contenuto = Replace(contenuto, "$fraseultima$", "Registrazione delle variazioni al nucleo familiare approvate")
                End If

                contenuto = Replace(contenuto, "$DatiFinali$", "Registrazione della variazione")
                tblRichiedente = "<table width='100%'><tr class='LeftNormale'><td width='40%'>Nominativo subentrante</td><td><b>" & richiedente & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>C.F. del subentrante</td><td><b>" & codFisc & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Data decorrenza subentro</td><td><b>" & par.FormattaData(Date.Parse(par.FormattaData(dataAutorizz), New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")) & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Motivo subentro</td><td><b>" & causaleDom & "</b></td></tr></table>"
            Else
                contenuto = Replace(contenuto, "$autorizzProvv$", "")
                'contenuto = Replace(contenuto, "$dataUltima$", "")
                contenuto = Replace(contenuto, "$fraseultima$", "")
                contenuto = Replace(contenuto, "$DatiFinali$", "")

            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$quadroSubentrante$", tblRichiedente)

            'TABELLA RIESAME 
            Dim tblNegativo As String = ""
            Dim esito As String = ""
            par.cmd.CommandText = "SELECT T_TIPO_DECISIONI_VSA.DESCRIZIONE,VSA_DECISIONI_REV_C.* FROM VSA_DECISIONI_REV_C,T_TIPO_DECISIONI_VSA WHERE ID_DOMANDA=" & id_dom & " AND VSA_DECISIONI_REV_C.COD_DECISIONE=T_TIPO_DECISIONI_VSA.COD and (cod=4 or cod=5 or cod=6) AND DATA = (SELECT MAX(DATA) FROM VSA_DECISIONI_REV_C WHERE (COD_DECISIONE=4 or COD_DECISIONE=5 OR COD_DECISIONE=6))"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("COD_DECISIONE"), "") = 5 Then
                    esito = "POSITIVO"
                ElseIf par.IfNull(myReader("COD_DECISIONE"), "") = 6 Then
                    esito = "NEGATIVO"
                ElseIf par.IfNull(myReader("COD_DECISIONE"), "") = 4 Then
                    esito = par.IfNull(myReader("DESCRIZIONE"), "")
                End If

                tblNegativo = tblNegativo & "<table width='100%'><tr style='background-color: #CCCCFF' class='LeftNormale'><td width='400px'>Fase di Riesame</td><td>Presa in carico da " & Session.Item("NOME_OPERATORE") & ".</td></tr>" _
                & "<tr class='LeftNormale'><td>Parere</td><td><b>" & esito & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Eventuali Osservazioni</td><td><b>" & par.IfNull(myReader("NOTE"), "") & "</b></td></tr>" _
                & "<tr class='LeftNormale'><td>Data Parere</td><td><b>" & par.FormattaData(par.IfNull(myReader("DATA"), "")) & "</b></td></tr>"
                tblNegativo = tblNegativo & "</table>"
            End If
            myReader.Close()

            If tblNegativo <> "" Then
                contenuto = Replace(contenuto, "$tblRiesame$", tblNegativo)
            Else
                contenuto = Replace(contenuto, "$tblRiesame$", "&nbsp")
            End If

            Dim tabellaCanonePRE As String = ""
            Dim tabellaCanonePOST As String = ""
            Dim tabellaCanone As String = "<table width='52%'>"
            Dim prov As Integer = 0
            Dim importdaContr As Boolean = False

            Select Case tipoImport
                Case 2
                    prov = 0
                Case 1
                    prov = 3
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.* FROM DOMANDE_BANDO_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=" & idTipoImport
                    Dim myReaderVSA1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderVSA1.Read Then
                        idTipoImport = par.IfNull(myReaderVSA1("ID"), "")
                    End If
                    myReaderVSA1.Close()
                Case 3
                    prov = 1
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO.* FROM DOMANDE_BANDO WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=" & idTipoImport
                    Dim myReaderBando As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderBando.Read Then
                        idTipoImport = par.IfNull(myReaderBando("ID"), "")
                    End If
                    myReaderBando.Close()
                Case 4
                    importdaContr = True
                    par.cmd.CommandText = "SELECT T_MOTIVO_DOMANDA_VSA.*,DOMANDE_BANDO_VSA.ID AS IDDOM,DOMANDE_BANDO_VSA.* FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.DATA_FINE_VAL<='" & dataEvento & "' AND DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & codContr & "' ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
                    Dim myReaderVSA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderVSA.Read Then
                        prov = 3
                        idTipoImport = par.IfNull(myReaderVSA("ID_DOM"), "")
                    Else
                        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.DATA_FINE_VAL<='" & dataEvento & "' AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                        & "AND RAPPORTO='" & codContr & "' ORDER BY ID_BANDO DESC"
                        Dim myReaderAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderAU.Read Then
                            prov = 0
                            idTipoImport = par.IfNull(myReaderAU("ID"), "")
                        Else
                            situazRedd = False
                        End If
                        myReaderAU.Close()
                    End If
                    myReaderVSA.Close()
            End Select

            contenuto = Replace(contenuto, "$anno1$", "")


            'If Request.QueryString("TIPO") = "RapportoRECA" Or Request.QueryString("TIPO") = "EsitoPosDEF" Or Request.QueryString("TIPO") = "EsPosRCProvv" Or Request.QueryString("TIPO") = "EsPositRC" Then

            'contenuto = Replace(contenuto, "$anno1$", annoIndagine)

            par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.DATA_INIZIO_VAL,DICHIARAZIONI_VSA.DATA_FINE_VAL,DICHIARAZIONI_VSA.ANNO_SIT_ECONOMICA,DOMANDE_BANDO_vsa.DATA_EVENTO,DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA FROM DICHIARAZIONI_vsa,DOMANDE_BANDO_vsa WHERE DOMANDE_BANDO_vsa.ID=" & id_dom & " AND DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    ANNO_INIZIO = CInt(Mid(par.IfNull(myReader0("DATA_INIZIO_VAL"), Year(Now)), 1, 4))
                    PER_ANNI = DateDiff(DateInterval.Year, CDate(par.FormattaData(myReader0("DATA_INIZIO_VAL"))), CDate(par.FormattaData(myReader0("DATA_FINE_VAL"))))
                    dataInizioValidita = par.IfNull(myReader0("DATA_INIZIO_VAL"), "")

                    If par.IfNull(myReader0("DATA_FINE_VAL"), "") = "29991231" Then
                        dataFine = Year(Now) & "1231"
                    Else
                        dataFine = par.IfNull(myReader0("DATA_FINE_VAL"), "")
                    End If
                    If par.IfNull(myReader0("ID_CAUSALE_DOMANDA"), "") = "27" Then
                        PER_ANNI = 0
                    End If
                End If
                myReader0.Close()

                Dim riferimentoDa As String = ""
                For I = ANNO_INIZIO To ANNO_INIZIO + PER_ANNI
                    TotaleCreditoANNO = 0

                    LOCATIVO = ""
                    comunicazioni = ""
                    sISEE = ""
                    sISE = ""
                    sISR = ""
                    sISP = ""
                    sVSE = ""
                    sREDD_DIP = ""
                    sREDD_ALT = ""
                    sLimitePensione = ""
                    sPER_VAL_LOC = ""
                    sPERC_INC_MAX_ISE_ERP = ""
                    sCANONE_MIN = ""
                    sISE_MIN = ""
                    sCanone = ""
                    sNOTE = ""
                    sDEM = ""
                    sSUPCONVENZIONALE = ""
                    sCOSTOBASE = ""
                    sZONA = ""
                    sMOTIVODECADENZA = ""
                    sPIANO = ""
                    sCONSERVAZIONE = ""
                    sVETUSTA = ""
                    sPSE = ""
                    sINCIDENZAISE = ""
                    sCOEFFFAM = ""
                    sSOTTOAREA = ""
                    sNUMCOMP = ""
                    sNUMCOMP66 = ""
                    sNUMCOMP100 = ""
                    sNUMCOMP100C = ""
                    sPREVDIP = ""
                    sDETRAZIONI = ""
                    sMOBILIARI = ""
                    sIMMOBILIARI = ""
                    sCOMPLESSIVO = ""
                    sDETRAZIONIF = ""
                    sANNOCOSTRUZIONE = ""
                    sLOCALITA = ""
                    sASCENSORE = ""
                    sDESCRIZIONEPIANO = ""
                    sSUPNETTA = ""
                    sALTRESUP = ""
                    sMINORI15 = ""
                    sMAGGIORI65 = ""
                    sSUPACCESSORI = ""
                    sVALORELOCATIVO = ""
                    sCANONECLASSE = ""
                    sCANONESOPP = ""
                    sVALOCIICI = ""
                    sALLOGGIOIDONEO = ""
                    sISTAT = ""
                    sCANONECLASSEISTAT = ""
                    sANNOINIZIOVAL = ""
                    sANNOFINEVAL = ""
                    tabellaCanonePRE = "<table width='100%'>"
                    Dim nomeArea As String = ""


                    'RICAVO STRINGHE PER LA SITUAZIONE PRE-RECA
                    If situazRedd = True Then
                        TestoFile = Replace(par.CalcolaCanone27RECA(idTipoImport, prov, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO ")

                        If importdaContr = True Then
                            tabellaCanonePRE = tabellaCanonePRE & "<tr><td align='center' colspan='2' style='font-family: Arial;font-size: 12pt;'><i>(dati riferiti all'ultima dichiarazione presentata)</i></td></tr>"
                        End If

                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>N. comp.</td><td><b>" & sNUMCOMP & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>N. comp. minori 15 anni</td><td><b>" & sMINORI15 & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>N. comp. maggiori 65 anni</td><td><b>" & sMAGGIORI65 & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>N. comp. invalidi 66%-99%</td><td><b>" & sNUMCOMP66 & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>N. comp. invalidi 100%</td><td><b>" & sNUMCOMP100 & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>N. comp. invalidi 100% con ind. acc.</td><td><b>" & sNUMCOMP100C & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Detrazioni</td><td><b>" & Format(CDbl(sDETRAZIONI), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Detrazioni per fragilità</td><td><b>" & Format(CDbl(sDETRAZIONIF), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Valori mobiliari</td><td><b>" & Format(CDbl(sMOBILIARI), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Valori immobiliari</td><td><b>" & Format(CDbl(sIMMOBILIARI), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Reddito Complessivo</td><td><b>" & Format(CDbl(sCOMPLESSIVO), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>ISEE ERP EFF</td><td><b>" & Format(CDbl(sISEE), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>ISE ERP EFF</td><td><b>" & Format(CDbl(sISE), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Redditi Dipendenti o Assimilati</td><td><b>" & Format(CDbl(par.IfEmpty(sREDD_DIP, "0")), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePRE = tabellaCanonePRE & "<tr class='LeftNormale'><td>Altri tipi di reddito Imponibili</td><td><b>" & Format(CDbl(par.IfEmpty(sREDD_ALT, "0")), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr></table>"

                        contenuto = Replace(contenuto, "$stampaProspettoPRE$", tabellaCanonePRE)
                    Else
                        contenuto = Replace(contenuto, "$stampaProspettoPRE$", "")
                    End If
                    If I < 2010 Then
                        tabellaCanone = tabellaCanone & ProspettiPREreca(I, codContr)
                    End If

                    Dim idDichCan_EC As Long = 0
                    Dim idDOMCan_EC As Long = 0
                    If I = 2010 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and (TIPO_PROVENIENZA=1 OR TIPO_PROVENIENZA=2 OR TIPO_PROVENIENZA=5 OR TIPO_PROVENIENZA=6) AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.Read Then
                            idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                            If idDichCan_EC <> 0 Then
                                If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 2 Then
                                    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)
                                Else
                                    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderID.Read Then
                                        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                    End If
                                    myReaderID.Close()
                                    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)
                                End If
                            End If
                        End If
                        myReaderRX.Close()

                        If idDichCan_EC <> 0 Then
                            Select Case AreaEconomica
                                Case 1
                                    nomeArea = "Protezione"
                                Case 2
                                    nomeArea = "Accesso"
                                Case 3
                                    nomeArea = "Permanenza"
                                Case 4
                                    nomeArea = "Decadenza"
                            End Select

                            tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Classe</td><td><b>" & sSOTTOAREA & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE_MIN, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & sISTAT & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"
                        End If

                        If idDichCan_EC = 0 Then
                            par.CalcolaCanone27RECA(IdDomBando, 1, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                            Select Case AreaEconomica
                                Case 1
                                    nomeArea = "Protezione"
                                Case 2
                                    nomeArea = "Accesso"
                                Case 3
                                    nomeArea = "Permanenza"
                                Case 4
                                    nomeArea = "Decadenza"
                            End Select

                            tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Classe</td><td><b>" & sSOTTOAREA & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE_MIN, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & sISTAT & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"

                        End If

                    End If


                    If I = 2011 Or I = 2012 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and (TIPO_PROVENIENZA=1 OR TIPO_PROVENIENZA=2 OR TIPO_PROVENIENZA=5 OR TIPO_PROVENIENZA=6) AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='2011' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='2011' ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.Read Then
                            idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                            If idDichCan_EC <> 0 Then
                                If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 2 Then
                                    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                Else
                                    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderID.Read Then
                                        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                    End If
                                    myReaderID.Close()
                                    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                End If
                            End If
                        Else
                            par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 1 ORDER BY DATA_CALCOLO DESC"
                            Dim myReaderRX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderRX2.Read Then
                                idDichCan_EC = par.IfNull(myReaderRX2("ID_DICHIARAZIONE"), 0)
                                If idDichCan_EC <> 0 Then
                                    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                End If
                            End If
                            myReaderRX.Close()
                        End If
                        If idDichCan_EC <> 0 Then
                            Select Case AreaEconomica
                                Case 1
                                    nomeArea = "Protezione"
                                Case 2
                                    nomeArea = "Accesso"
                                Case 3
                                    nomeArea = "Permanenza"
                                Case 4
                                    nomeArea = "Decadenza"
                            End Select

                            tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Classe</td><td><b>" & sSOTTOAREA & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE_MIN, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & sISTAT & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"
                        End If

                        If idDichCan_EC = 0 Then
                            par.CalcolaCanone27RECA(IdDomBando, 1, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                            Select Case AreaEconomica
                                Case 1
                                    nomeArea = "Protezione"
                                Case 2
                                    nomeArea = "Accesso"
                                Case 3
                                    nomeArea = "Permanenza"
                                Case 4
                                    nomeArea = "Decadenza"
                            End Select

                            tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Classe</td><td><b>" & sSOTTOAREA & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE_MIN, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & sISTAT & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone), "##,##0.00") & "</b></td></tr>"
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"
                        End If
                    End If



                    TestoFile = Replace(par.CalcolaCanone27RECA(CDbl(id_dom), 3, IDUNITA, codContr, NuovoCanone, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONIF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO " & I)

                    'If I = 2009 Then
                    If I = ANNO_INIZIO + PER_ANNI Then

                        Select Case AreaEconomica
                            Case 1
                                contenuto = Replace(contenuto, "$area2$", "PROTEZIONE")
                            Case 2
                                contenuto = Replace(contenuto, "$area2$", "ACCESSO")
                            Case 3
                                contenuto = Replace(contenuto, "$area2$", "PERMANENZA")
                            Case 4
                                contenuto = Replace(contenuto, "$area2$", "DECADENZA")
                        End Select
                        contenuto = Replace(contenuto, "$classe2$", sSOTTOAREA)
                        contenuto = Replace(contenuto, "$isee2$", Format(CDbl(sISEE), "##,##0.00"))

                        contenuto = Replace(contenuto, "$annoEvento$", "2009")
                        contenuto = Replace(contenuto, "$nuovoCanone$", par.Converti(Format(NuovoCanone, "##,##0.00")))
                        contenuto = Replace(contenuto, "$nuovoCanoneMensile$", par.Converti(Format(NuovoCanone / 12, "##,##0.00")))
                    End If
                    'End If

                    If I = ANNO_INIZIO Then
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Detrazioni</td><td><b>" & Format(CDbl(sDETRAZIONI), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Detrazioni per fragilità</td><td><b>" & Format(CDbl(sDETRAZIONIF), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Valori mobiliari</td><td><b>" & Format(CDbl(sMOBILIARI), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Valori immobiliari</td><td><b>" & Format(CDbl(sIMMOBILIARI), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Reddito Complessivo</td><td><b>" & Format(CDbl(sCOMPLESSIVO), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>ISEE ERP EFF</td><td><b>" & Format(CDbl(sISEE), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>ISE ERP EFF</td><td><b>" & Format(CDbl(sISE), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Redditi Dipendenti o Assimilati</td><td><b>" & Format(CDbl(par.IfEmpty(sREDD_DIP, "0")), "##,##0.00") & "</b></td></tr>"
                        tabellaCanonePOST = tabellaCanonePOST & "<tr class='LeftNormale'><td>Altri tipi di reddito Imponibili</td><td><b>" & Format(CDbl(par.IfEmpty(sREDD_ALT, "0")), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"

                        contenuto = Replace(contenuto, "$stampaProspettoPOST$", tabellaCanonePOST)
                    End If

                    tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE ANNO " & I & " </td><td>&nbsp</td></tr>"
                    Select Case AreaEconomica
                        Case 1
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>Protezione</b></td></tr>"
                        Case 2
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>Accesso</b></td></tr>"
                        Case 3
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>Permanenza</b></td></tr>"
                    End Select
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Classe</td><td><b>" & sSOTTOAREA & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE_MIN, 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDbl(par.IfEmpty(sISE, 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & sISTAT & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime calcolato</td><td><b>" & Format(CDbl(NuovoCanone / 12), "##,##0.00") & "</b></td></tr>"

                    If NuovoTransit <> 0 Then
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Importo ann. canone transitorio</td><td><b>" & Format(CDbl(NuovoTransit), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"
                    Else
                        tabellaCanone = tabellaCanone & "<tr><td>&nbsp</td></tr>"
                    End If

                    If NuovoCanone <> 0 Then
                        Dim GiorniDiffEmesso As Long = 0
                        Dim TotGiorniEmesso As Decimal = 0

                        Dim GiorniDiffEmessoNuovo As Long = 0
                        Dim TotGiorniEmessoNuovo As Decimal = 0
                        'Dim DiffDaApplicare As Decimal = 0

                        TotGiorniEmessoNuovo = Format(NuovoCanone / 12, "0.00")
                        Dim inizioCalcolo As String = ""
                        Dim fineCalcolo As String = ""
                        TotaleEmesso = 0


                        contenuto = Replace(contenuto, "$canoneNew$", Format(CDbl(TotGiorniEmessoNuovo), "##,##0.00"))

                        Dim ggCredito As Integer = 0

                        riferimentoDa = I & "1231"

                        par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                        & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                        & "525,10001,10002,30003,530," _
                        & "30075,1,10072,10087,10125," _
                        & "10135,20003,20019,20020," _
                        & "20023,20096,20097,553," _
                        & "10075,10128,20021,10127," _
                        & "10126,512,10074,534,10073," _
                        & "604,30071,603,30068,506," _
                        & "647,653,599,648,30080,622," _
                        & "30123,30124,508,10160,509," _
                        & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                        & "AND RIFERIMENTO_DA<='" & riferimentoDa & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                        & "AND ID_CONTRATTO=" & idc & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                        Dim myReaderBoll As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReaderBoll.Read
                            'DIFFERENZA IN GIORNI TRA INIZIO VALIDITA' E FINE VALIDITA'
                            If I & "0101" < dataInizioValidita Then
                                inizioCalcolo = dataInizioValidita
                            Else
                                inizioCalcolo = I & "0101"
                            End If

                            If I & "1231" < dataFine Then
                                fineCalcolo = I & "1231"
                            Else
                                If I = Year(Now) Then
                                    fineCalcolo = par.AggiustaData(dataBollettUltima)
                                Else
                                    fineCalcolo = dataFine
                                End If
                            End If

                            Dim numMesiBolle As Integer = 0
                            numMesiBolle = DateDiff(DateInterval.Month, CDate(par.FormattaData(inizioCalcolo)), CDate(par.FormattaData(fineCalcolo))) - 1
                            GiorniDiffEmesso = numMesiBolle * 30
                            If GiorniDiffEmesso < 0 Then
                                GiorniDiffEmesso = 0
                            End If
                            Dim giorniPrimoMese As Integer = 0
                            Dim giorniUltimoMese As Integer = 0

                            If CInt(inizioCalcolo.Substring(6, 2)) > 30 Then
                                giorniPrimoMese = 30
                            Else
                                giorniPrimoMese = (30 - inizioCalcolo.Substring(6, 2)) + 1
                            End If
                            If CInt(fineCalcolo.Substring(6, 2)) = 28 And CInt(fineCalcolo.Substring(4, 2)) = 2 Then
                                giorniUltimoMese = 30
                            Else
                                If CInt(fineCalcolo.Substring(6, 2)) = 31 Then
                                    giorniUltimoMese = 30
                                Else
                                    giorniUltimoMese = fineCalcolo.Substring(6, 2)
                                End If
                            End If

                            GiorniDiffEmesso = GiorniDiffEmesso + giorniPrimoMese + giorniUltimoMese

                            If I = Year(Now) Then
                                TotGiorniEmesso = ((par.IfNull(myReaderBoll("imp_EMESSO"), 0) * (360 / GiorniDiffEmesso)) / 360) * GiorniDiffEmesso
                            Else
                                TotGiorniEmesso = ((par.IfNull(myReaderBoll("imp_EMESSO"), 0) * (1)) / 360) * GiorniDiffEmesso
                            End If


                            TotaleEmesso = TotaleEmesso + Format(TotGiorniEmesso, "0.00")
                        Loop
                        myReaderBoll.Close()
                    End If
                    If dataInizioValidita > I & "0101" Then
                        If I = Year(Now) Then
                            dataFineBollettato = dataBollettUltima
                        Else
                            dataFineBollettato = "31/12/" & I
                        End If
                        If PER_ANNI = 0 Then
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Bollettato nell'anno " & I & " rapportato al periodo di riferimento (dal " & par.FormattaData(dataInizioValidita) & " al " & dataFineBollettato & ") : </td><td><b>" & Format(CDbl(TotaleEmesso), "##,##0.00") & "</b></td></tr>"
                        Else
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Bollettato nell'anno " & I & " rapportato al periodo di riferimento (dal " & par.FormattaData(dataInizioValidita) & " al " & dataFineBollettato & ") : </td><td><b>" & Format(CDbl(TotaleEmesso), "##,##0.00") & "</b></td></tr><tr><td><p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p></td></tr>"
                        End If
                    Else
                        If I = Year(Now) Then
                            If dataBollettazGener > dataBollettUltima Then
                                dataFineBollettato = dataBollettUltima
                            Else
                                dataFineBollettato = par.FormattaData(dataBollettazGener)
                            End If
                        Else
                            dataFineBollettato = "31/12/" & I
                        End If
                        If PER_ANNI = 0 Then
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Bollettato nell'anno " & I & " rapportato al periodo di riferimento (dal 01/01/" & I & " al " & dataFineBollettato & ") : </td><td><b>" & Format(CDbl(TotaleEmesso), "##,##0.00") & "</b></td></tr>"
                        Else
                            tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Bollettato nell'anno " & I & " rapportato al periodo di riferimento (dal 01/01/" & I & " al " & dataFineBollettato & ") : </td><td><b>" & Format(CDbl(TotaleEmesso), "##,##0.00") & "</b></td></tr><tr><td><p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p></td></tr>"
                        End If
                    End If

                Next

            'End If

            'Dim stringaPREreca1 As String = ""
            'par.cmd.CommandText = "SELECT * FROM CANONI_PRE_RECA WHERE ID_DOMANDA='" & id_dom & "' AND NUM_PARTE = 3"
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    stringaPREreca1 = par.IfNull(myReader("TESTO_CANONE"), "")
            '    stringaPREreca1 = Replace(stringaPREreca1, vbCrLf, "</br>")
            '    stringaPREreca1 = Replace(stringaPREreca1, Mid(stringaPREreca1, 56, 13), "")
            '    tabellaCanonePRE = tabellaCanonePRE & "<tr><td>" & stringaPREreca1 & "</td></tr></table>"
            '    contenuto = Replace(contenuto, "$stampaProspettoPRE$", tabellaCanonePRE)
            'End If
            'myReader.Close()


            contenuto = Replace(contenuto, "$stampaProspetto$", tabellaCanone)
            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            If Request.QueryString("TIPO") = "EsPositRC" Or Request.QueryString("TIPO") = "EsPosRCProvv" Or Request.QueryString("TIPO") = "EsitoPosDEF" Then
                nomefile = "09_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "R1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            Me.ZippaFiles(nomefile)
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ElencoStampe(ByVal idDich As Long) As String
        Try
            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()
            Dim pos As Integer
            Dim j As Integer

            MiaSHTML = "<table style='border: solid 1px #000000; width: 100%; border-collapse: collapse;' cellspacing='5'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr style='font-weight: bold; font-size: 16px; font-family: Arial'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td>Tipo stampa</td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td>Data stampa</td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/LOCATARI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & idDich & "*.zip")
                If InStr(foundFile, "20_") = 0 Then
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                End If
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If i > 0 Then
                For j = 0 To i - 1
                    MiaSHTML = MiaSHTML & "<tr class='LeftNormale'>" & vbCrLf
                    Select Case Mid(RicavaFile(ElencoFile(j)), 1, 2)
                        Case "00"
                            MiaSHTML = MiaSHTML & "<td>Richiesta Revisione Canone</td>" & vbCrLf
                        Case "01"
                            MiaSHTML = MiaSHTML & "<td>Richiesta Documentazione Mancante</td>" & vbCrLf
                        Case "02"
                            MiaSHTML = MiaSHTML & "<td>Avvio Procedimento</td>" & vbCrLf
                        Case "03"
                            MiaSHTML = MiaSHTML & "<td>Autocertificazione</td>" & vbCrLf
                        Case "04"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Negativo</td>" & vbCrLf
                        Case "05"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Negativo Riesame</td>" & vbCrLf
                        Case "06"
                            MiaSHTML = MiaSHTML & "<td>Stato Famiglia Ospite</td>" & vbCrLf
                        Case "07"
                            MiaSHTML = MiaSHTML & "<td>Conviv. More Uxorio</td>" & vbCrLf
                        Case "08"
                            MiaSHTML = MiaSHTML & "<td>Conviv. per Assistenza</td>" & vbCrLf
                        Case "09"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Positivo</td>" & vbCrLf

                        Case "S1"
                            MiaSHTML = MiaSHTML & "<td>Domanda di subentro nell'intestazione</td>" & vbCrLf
                        Case "S2"
                            MiaSHTML = MiaSHTML & "<td>Dichiaraz. sost. perm. requisiti</td>" & vbCrLf
                        Case "S3"
                            MiaSHTML = MiaSHTML & "<td>Sopralluogo</td>" & vbCrLf
                        Case "S4"
                            MiaSHTML = MiaSHTML & "<td>Com. Sopralluogo</td>" & vbCrLf
                        Case "S5"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Positivo Commiss.</td>" & vbCrLf
                        Case "S6"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Positivo Dir. Crediti</td>" & vbCrLf
                        Case "S7"
                            MiaSHTML = MiaSHTML & "<td>Comunicazione FF.OO. al Commissario di Governo </td>" & vbCrLf
                        Case "S8"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Positivo del Riesame</td>" & vbCrLf

                        Case "V1"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Negativo Senza Osservaz.</td>" & vbCrLf
                        Case "V2"
                            MiaSHTML = MiaSHTML & "<td>Modulo Richiesta</td>" & vbCrLf

                        Case "O1"
                            MiaSHTML = MiaSHTML & "<td>Richiesta Ospitalità Generica</td>" & vbCrLf
                        Case "O2"
                            MiaSHTML = MiaSHTML & "<td>Richiesta Ospitalità Badanti</td>" & vbCrLf
                        Case "O3"
                            MiaSHTML = MiaSHTML & "<td>Richiesta Ospitalità Scolast.</td>" & vbCrLf
                        Case "O5"
                            MiaSHTML = MiaSHTML & "<td>Esito Positivo per badanti</td>" & vbCrLf
                        Case "O6"
                            MiaSHTML = MiaSHTML & "<td>Esito Positivo per autorizz.scolastica</td>" & vbCrLf
                        Case "O7"
                            MiaSHTML = MiaSHTML & "<td>Esito Positivo Riesame per badanti</td>" & vbCrLf
                        Case "O8"
                            MiaSHTML = MiaSHTML & "<td>Esito Positivo Riesame per autorizz.scolastica</td>" & vbCrLf
                        Case "O9"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz. Esito Negativo Con Osservaz.</td>" & vbCrLf

                        Case "C1"
                            MiaSHTML = MiaSHTML & "<td>Richiesta Cambio Consensuale</td>" & vbCrLf


                            'Nuovi Documenti per Ampliamento 28/02/2012
                        Case "A1"
                            MiaSHTML = MiaSHTML & "<td>Presa d'atto per rientro</td>" & vbCrLf

                        Case "A2"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz.Esito Positivo Riesame</td>" & vbCrLf

                        Case "A3"
                            MiaSHTML = MiaSHTML & "<td>Comunicaz.Esito Positivo Riesame Rientro</td>" & vbCrLf

                        Case "A4"
                            MiaSHTML = MiaSHTML & "<td>Permanenza Requisiti ERP (titolare)</td>" & vbCrLf

                        Case "A5"
                            MiaSHTML = MiaSHTML & "<td>Permanenza Requisiti ERP (ospite)</td>" & vbCrLf

                        Case "R1"
                            MiaSHTML = MiaSHTML & "<td>Rapporto Sintetico</td>" & vbCrLf
                        Case Else
                            MiaSHTML = MiaSHTML & "<td>&nbsp</td>" & vbCrLf


                    End Select

                    MiaSHTML = MiaSHTML & "<td>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</td>" & vbCrLf

                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If
                    If j = 10 Then Exit For
                Next j
            Else
                MiaSHTML = MiaSHTML & "<td>Nessuna stampa</td>" & vbCrLf
            End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            'Label3.Text = MiaSHTML

            Return MiaSHTML

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Function

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Private Function ProspettiPREreca(ByVal I As Integer, ByVal codContr As String) As String
        Dim CanonePREreca As Decimal = 0
        Dim istat2009 As String = "2,025"
        Dim nomeArea As String = ""
        Dim tabellaCanone As String = ""

        If I = 2008 Or I = 2009 Then
            par.cmd.CommandText = "SELECT * from SISCOM_MI.RAPPORTI_UTENZA_EXTRA where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "')"
            Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderRX.Read Then
                CanonePREreca = par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0)
                If I = 2009 Then
                    If par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") >= 12 And par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") < 27 Then
                        CanonePREreca = CanonePREreca + ((CanonePREreca * CDbl(istat2009)) / 100)
                    End If
                End If
                tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"

                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Importo ann.canone a regime</td><td><b>" & Format(CDec(CanonePREreca), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Importo mensile canone a regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0) / 12), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Importo ann.canone transitorio</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_TRANSITORIO"), 0)), "##,##0.00") & "</b></td></tr>"


                If I = 2008 Then
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Importo ann.canone graduato</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_PRIMO_ANNO"), 0)), "##,##0.00") & "</b></td></tr>"
                Else
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Importo ann.canone graduato</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_SECONDO_ANNO"), 0)), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"
                End If
            End If
            myReaderRX.Close()
        End If

        If I = 2011 Then
            par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
            Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderRX.Read Then
                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                    Case 1
                        nomeArea = "Protezione"
                    Case 2
                        nomeArea = "Accesso"
                    Case 3
                        nomeArea = "Permanenza"
                    Case 4
                        nomeArea = "Decadenza"
                End Select

                tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"

                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Fascia</td><td><b>" & par.IfNull(myReaderRX("SOTTO_AREA"), "") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0) & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"

            Else

                par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 1 ORDER BY DATA_CALCOLO DESC"
                Dim myReaderRX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderRX2.Read Then

                    Select Case par.IfNull(myReaderRX2("ID_AREA_ECONOMICA"), -1)
                        Case 1
                            nomeArea = "Protezione"
                        Case 2
                            nomeArea = "Accesso"
                        Case 3
                            nomeArea = "Permanenza"
                        Case 4
                            nomeArea = "Decadenza"
                    End Select

                    tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"


                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Fascia</td><td><b>" & par.IfNull(myReaderRX2("SOTTO_AREA"), "") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("ISEE_27"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("ISE"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE_CLASSE"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & par.IfNull(myReaderRX2("PERC_ISTAT_APPLICATA"), 0) & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE"), 0) / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"


                End If
                myReaderRX2.Close()
            End If
            myReaderRX.Close()
        End If

        If I = 2012 Then
            par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 3 ORDER BY DATA_CALCOLO DESC"
            Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderRX.Read Then
                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                    Case 1
                        nomeArea = "Protezione"
                    Case 2
                        nomeArea = "Accesso"
                    Case 3
                        nomeArea = "Permanenza"
                    Case 4
                        nomeArea = "Decadenza"
                End Select

                tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"


                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Fascia</td><td><b>" & par.IfNull(myReaderRX("SOTTO_AREA"), "") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0) & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00") & "</b></td></tr>"
                tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"


            Else
                par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 2 ORDER BY DATA_CALCOLO DESC"
                Dim myReaderRX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderRX2.Read Then
                    Select Case par.IfNull(myReaderRX2("ID_AREA_ECONOMICA"), -1)
                        Case 1
                            nomeArea = "Protezione"
                        Case 2
                            nomeArea = "Accesso"
                        Case 3
                            nomeArea = "Permanenza"
                        Case 4
                            nomeArea = "Decadenza"
                    End Select

                    tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"

                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Fascia</td><td><b>" & par.IfNull(myReaderRX2("SOTTO_AREA"), "") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("ISEE_27"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("ISE"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE_CLASSE"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & par.IfNull(myReaderRX2("PERC_ISTAT_APPLICATA"), 0) & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE"), 0)), "##,##0.00") & "</b></td></tr>"
                    tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX2("CANONE"), 0) / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"


                Else
                    par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContr & "') and ID_BANDO_AU = 1 ORDER BY DATA_CALCOLO DESC"
                    Dim myReaderRX3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRX3.Read Then
                        Select Case par.IfNull(myReaderRX3("ID_AREA_ECONOMICA"), -1)
                            Case 1
                                nomeArea = "Protezione"
                            Case 2
                                nomeArea = "Accesso"
                            Case 3
                                nomeArea = "Permanenza"
                            Case 4
                                nomeArea = "Decadenza"
                        End Select

                        tabellaCanone = tabellaCanone & "<tr><td align='center' colspan='2' style='font-weight: bold;font-size: 16px; font-family: Arial;color: #0000FF;'>DETERMINAZIONE DEL CANONE PRE-RECA " & I & "</td></tr>"

                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Area</td><td><b>" & nomeArea & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Fascia</td><td><b>" & par.IfNull(myReaderRX3("SOTTO_AREA"), "") & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISEE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX3("ISEE_27"), 0)), "##,##0.00") & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>ISE-ERP L.R.27</td><td><b>" & Format(CDec(par.IfNull(myReaderRX3("ISE"), 0)), "##,##0.00") & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe</td><td><b>" & Format(CDec(par.IfNull(myReaderRX3("CANONE_CLASSE"), 0)), "##,##0.00") & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>% ISTAT applicata canone classe</td><td><b>" & par.IfNull(myReaderRX3("PERC_ISTAT_APPLICATA"), 0) & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone classe con ISTAT</td><td><b>" & Format(CDec(par.IfNull(myReaderRX3("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00") & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP annuale regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX3("CANONE"), 0)), "##,##0.00") & "</b></td></tr>"
                        tabellaCanone = tabellaCanone & "<tr class='LeftNormale'><td>Canone ERP mensile regime</td><td><b>" & Format(CDec(par.IfNull(myReaderRX3("CANONE"), 0) / 12), "##,##0.00") & "</b></td></tr><tr><td>&nbsp</td></tr>"

                    End If
                    myReaderRX3.Close()
                End If
                myReaderRX2.Close()
            End If
            myReaderRX.Close()
        End If

        Return tabellaCanone

    End Function
#End Region


#Region "StampeDocumenti per REVISIONE CANONE"
    Protected Sub pdfRichCanone()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("TestoModelli\RicRevCanone.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim luogoNasc As String
            Dim luogoRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim sigla As String = ""
            Dim id_dom As Long
            Dim tbAllegati As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 8

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoNasc = par.IfNull(myReader("NOME"), "")
                sigla = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                'contenuto = Replace(contenuto, "$data$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datanascita$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$comunenascita$", luogoNasc)
                contenuto = Replace(contenuto, "$provincia$", sigla)
                contenuto = Replace(contenuto, "$localita$", luogoRes)
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$annoredditi$", par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                If myReader("ANNO_SIT_ECONOMICA") = "2006" Or myReader("ANNO_SIT_ECONOMICA") = "2007" Then
                    contenuto = Replace(contenuto, "$dataDecorr$", "01/01/2008")
                Else
                    contenuto = Replace(contenuto, "$dataDecorr$", "01/01/2010")
                End If
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataInizioVal$", par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VAL"), "")))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_ALLEGATI.ID_DOC AND VSA_DOC_ALLEGATI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            Dim strTbl2 As String = ""
            While myReader.Read
                NumDoc = NumDoc - 1
                strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
                tbAllegati = tbAllegati & strTbl
                ndx = ndx + 1
            End While
            myReader.Close()

            If tbAllegati.Contains(ndx - 1) Then
                strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            End If

            tbAllegati = Replace(tbAllegati, strTbl, strTbl2)

            If NumDoc > 0 Then
                For i As Integer = 0 To NumDoc - 1
                    tbAllegati = tbAllegati & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>&nbsp;</td></tr>"
                Next
            End If

            tbAllegati = tbAllegati & "</table>"

            contenuto = Replace(contenuto, "$docallegati$", tbAllegati)

            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = "00_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Protected Sub pdfDocMancante()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "DocMancanteAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\DocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DocMancRC" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\DocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DocMancanteOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\DocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DocMancCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\DocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DocMancCAMB2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\DocMancante2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DocMancanteSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\DocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim siglaRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim codcontr2 As String = ""

            Dim tbDocManc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 8
            Dim id_dom As Long


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codcontr2 = Request.QueryString("NUMCONT2")

            If Request.QueryString("TIPO") = "DocMancCAMB2" Then
                idc = ottieniIDContr(codcontr2)
            Else
                idc = ottieniIDContr(codContr)
            End If

            contenuto = Replace(contenuto, "$codunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)


            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
            '    & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
            '    & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
            & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
            & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD(+) AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.*,RAPPORTI_UTENZA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codcontr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo1_2$", par.IfNull(myReader("CAP_RESIDENZA"), "") & " " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("PROVINCIA_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2_2$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo0_2$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("COD_TIPO_LIVELLO_PIANO"), ""))
                contenuto = Replace(contenuto, "$codunita2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE AS DESCR FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_MANCANTI.ID_DOC AND VSA_DOC_MANCANTI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            Dim strTbl2 As String = ""
            While myReader.Read
                NumDoc = NumDoc - 1
                strTbl = "<tr id='" & ndx & "'><td style='font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & Trim(par.Elimina160(par.IfNull(myReader("DESCR"), ""))) & ";</td></tr>"
                tbDocManc = tbDocManc & strTbl
                ndx = ndx + 1
            End While
            myReader.Close()

            If tbDocManc.Contains(ndx - 1) Then
                strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            End If

            tbDocManc = Replace(tbDocManc, strTbl, strTbl2)

            If NumDoc > 0 Then
                For i As Integer = 0 To NumDoc - 1
                    tbDocManc = tbDocManc & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>&nbsp;</td></tr>"
                Next
            End If

            tbDocManc = tbDocManc & "</table>"


            contenuto = Replace(contenuto, "$docMancante$", tbDocManc)

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = "01_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub pdfAvvioProc()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "AvvioProcAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\AvvioProcedimento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "AvvProcRC" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\AvvioProcedimento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "AvvProcedVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\AvvioProcedimento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "AvvioProcOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\AvvioProcedimento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "AvvProcedCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\AvvioProcedimento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "AvvProcedCAMB2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\AvvioProcedimento2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "AvvioProcSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\AvvioProcedimento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim tbDoc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 8
            Dim luogoRes As String
            Dim siglaRes As String
            Dim id_dom As Long
            Dim codContr As String = ""
            Dim codContr2 As String = ""

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")

            If Request.QueryString("TIPO") = "AvvProcedCAMB2" Then
                idc = ottieniIDContr(codContr2)
            Else
                idc = ottieniIDContr(codContr)
            End If

            contenuto = Replace(contenuto, "$cod_ui$", codUi)
            contenuto = Replace(contenuto, "$codcontr$", codContr)
            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            'contenuto = Replace(contenuto, "$indirizzo2$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

                par.cmd.CommandText = "select * from TEMPI_PROCESSI_VSA where ID_MOTIVO_DOMANDA=" & par.IfNull(myReader("ID_MOTIVO_DOMANDA"), "")
                Dim myReaderGG As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderGG.Read Then
                    contenuto = Replace(contenuto, "$tempoProcesso$", par.IfNull(myReaderGG("TEMPO_GG"), 0))
                End If
                myReaderGG.Close()

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.*,RAPPORTI_UTENZA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo1_2$", par.IfNull(myReader("CAP_RESIDENZA"), "") & " " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("PROVINCIA_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2_2$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo0_2$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("COD_TIPO_LIVELLO_PIANO"), ""))
                contenuto = Replace(contenuto, "$codunita2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_ALLEGATI.ID_DOC AND VSA_DOC_ALLEGATI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            Dim strTbl2 As String = ""
            While myReader.Read
                NumDoc = NumDoc - 1
                strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
                tbDoc = tbDoc & strTbl
                ndx = ndx + 1
            End While
            myReader.Close()

            If tbDoc.Contains(ndx - 1) Then
                strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            End If

            tbDoc = Replace(tbDoc, strTbl, strTbl2)

            If NumDoc > 0 Then
                For i As Integer = 0 To NumDoc - 1
                    tbDoc = tbDoc & "<tr><td style='text-align: left; font-size:14pt;font-family:Arial ;'>&nbsp;</td></tr>"
                Next
            End If

            tbDoc = tbDoc & "</table>"

            contenuto = Replace(contenuto, "$doc$", tbDoc)

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = "02_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub pdfEsitoNeg()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsNegativoAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\ComEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Else
                sr1 = New StreamReader(Server.MapPath("TestoModelli\ComEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim siglaRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim tbDoc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 5


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            'contenuto = Replace(contenuto, "$indirizzo2$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            'MOTIVI ESITO NEGATIVO MEMORIZZATI NEL CAMPO NOTE
            Dim motivi As String = ""
            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=3 OR COD_DECISIONE=8) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                motivi = par.IfNull(myReader("NOTE"), "")
                motivi = Replace(motivi, ",", ";<br/>")
                contenuto = Replace(contenuto, "$motivi$", motivi & ".")
            Else
                contenuto = Replace(contenuto, "$motivi$", " ")
            End If
            myReader.Close()


            'par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & id_dom
            'myReader = par.cmd.ExecuteReader
            'Dim ndx As Integer = 1
            'Dim strTbl As String = ""
            'Dim strTbl2 As String = ""
            'While myReader.Read
            '    NumDoc = NumDoc - 1
            '    strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
            '    tbDoc = tbDoc & strTbl
            '    ndx = ndx + 1
            'End While
            'myReader.Close()

            'If tbDoc.Contains(ndx - 1) Then
            '    strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            'End If

            'tbDoc = Replace(tbDoc, strTbl, strtbl2)

            'If NumDoc > 0 Then
            '    For i As Integer = 0 To NumDoc - 1
            '        tbDoc = tbDoc & "<tr id='1'><td style='text-align: left; font-size:14pt;font-family:Arial ;'>&nbsp;</td></tr>"
            '    Next
            'End If

            'tbDoc = tbDoc & "</table>"

            'contenuto = Replace(contenuto, "$motivi$", tbDoc)


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            '05/04/2012
            Dim motivazDocMancante As String = ""
            If Request.QueryString("TIPO") = "EsNegatRC" Then
                par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=3 OR COD_DECISIONE=8) ORDER BY COD_DECISIONE ASC"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & id_dom
                    Dim lettoreNeg As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettoreNeg.Read Then
                        If par.IfNull(lettoreNeg("ID_COND_ESITO"), "") = "32" Then
                            par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE AS DESCR FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_MANCANTI.ID_DOC AND VSA_DOC_MANCANTI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
                            Dim lettoreDoc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            While lettoreDoc.Read
                                motivazDocMancante = motivazDocMancante & Trim(par.Elimina160(par.IfNull(lettoreDoc("DESCR"), ""))) & "<br/> "
                            End While
                            lettoreDoc.Close()
                        End If
                        contenuto = Replace(contenuto, "$motiviNEG$", par.IfNull(myReader("NOTE"), "") & ":<br/> " & motivazDocMancante)
                    End If
                    lettoreNeg.Close()
                    contenuto = Replace(contenuto, "$motiviNEG$", par.IfNull(myReader("NOTE"), ""))
                Else
                    contenuto = Replace(contenuto, "$motiviNEG$", " ")
                End If
                myReader.Close()
            End If

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = "04_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub pdfEsitoNegRies()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader
            If Request.QueryString("TIPO") = "EsNegRiesameRC" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\ComEsitoNegRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Else
                sr1 = New StreamReader(Server.MapPath("TestoModelli\ComEsitoNegRiesDecOsservazio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim siglaRes As String
            Dim id_dom As Long
            Dim codUi As String = ""
            Dim codContr As String = ""


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")

            End If
            myReader.Close()

            'contenuto = Replace(contenuto, "$indirizzo2$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "__/__/____")))

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            Dim motivazDocMancante As String = ""
            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=6 OR COD_DECISIONE=10) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & id_dom
                Dim lettoreNeg As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreNeg.Read Then
                    If par.IfNull(lettoreNeg("ID_COND_ESITO"), "") = "32" Then
                        par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE AS DESCR FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_MANCANTI.ID_DOC AND VSA_DOC_MANCANTI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
                        Dim lettoreDoc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While lettoreDoc.Read
                            motivazDocMancante = motivazDocMancante & Trim(par.Elimina160(par.IfNull(lettoreDoc("DESCR"), ""))) & "<br/> "
                        End While
                        lettoreDoc.Close()
                    End If
                    contenuto = Replace(contenuto, "$motiviNeg$", par.IfNull(myReader("NOTE"), "") & ":<br/> " & motivazDocMancante)
                End If
                lettoreNeg.Close()
                contenuto = Replace(contenuto, "$motiviNeg$", par.IfNull(myReader("NOTE"), ""))
            Else
                contenuto = Replace(contenuto, "$motiviNeg$", "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()


            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True


            'sostituire nuovo codice da qui
            Dim nomefile As String = ""
            If Request.QueryString("TIPO") = "EsNegRiesameRC" Then
                nomefile = "V1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "O9_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub pdfAutocert()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("TestoModelli\AutoCertificazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            Dim contenutoORIG As String = contenuto

            sr1.Close()
            Dim luogoNasc As String
            Dim luogoRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim siglaNas As String = ""
            Dim siglaRes As String = ""
            Dim intestatario As String = ""
            Dim tbAllegati As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 8
            Dim codFisc As String = ""

            Dim tblGenerale As String = ""


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoNasc = par.IfNull(myReader("NOME"), "")
                siglaNas = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.PG AS PG_DICH,COMP_NUCLEO_VSA.ID AS COD_UTENTE,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*," _
                & "T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE = T_TIPO_INDIRIZZO.COD AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ")
            Dim myReaderGenerale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReaderGenerale.Read
                Dim tblTestata As String = "<table width='100%'>"
                codFisc = par.IfNull(myReaderGenerale("COD_FISCALE"), "")
                If par.IfNull(myReaderGenerale("PERC_INVAL"), 0) = "0" Then
                    contenuto = Replace(contenuto, "$invalidita$", "---")
                Else
                    contenuto = Replace(contenuto, "$invalidita$", "SI")
                End If
                intestatario = par.PulisciStrSql((par.IfNull(myReaderGenerale("COGNOME"), "")) & " " & Trim(par.IfNull(myReaderGenerale("NOME"), "")))
                contenuto = Replace(contenuto, "$codutente$", par.IfNull(myReaderGenerale("COD_UTENTE"), ""))
                contenuto = Replace(contenuto, "$soggetto$", par.IfNull(myReaderGenerale("COGNOME"), "") & " " & par.IfNull(myReaderGenerale("NOME"), ""))
                contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReaderGenerale("COGNOME"), ""))
                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReaderGenerale("NOME"), ""))
                contenuto = Replace(contenuto, "$cf$", codFisc)
                contenuto = Replace(contenuto, "$datanascita$", par.FormattaData(par.IfNull(myReaderGenerale("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$sesso$", par.IfNull(myReaderGenerale("SESSO"), ""))
                '$naznascita$
                contenuto = Replace(contenuto, "$provnascita$", siglaNas)
                contenuto = Replace(contenuto, "$comunasc$", luogoNasc)
                contenuto = Replace(contenuto, "$indresidenza$", par.IfNull(myReaderGenerale("TIPO_VIA"), "") & " " & par.IfNull(myReaderGenerale("IND_RES_DNTE"), "") & " civ." & par.IfNull(myReaderGenerale("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$comune$", luogoRes)
                contenuto = Replace(contenuto, "$provincia$", siglaRes)
                contenuto = Replace(contenuto, "$annoredditi$", par.IfNull(myReaderGenerale("ANNO_SIT_ECONOMICA"), ""))

                contenuto = Replace(contenuto, "$numdoc$", par.IfNull(myReaderGenerale("CARTA_I"), ""))
                contenuto = Replace(contenuto, "$datadoc$", par.FormattaData(par.IfNull(myReaderGenerale("CARTA_I_DATA"), "")))
                contenuto = Replace(contenuto, "$enteidentita$", par.IfNull(myReaderGenerale("CARTA_I_RILASCIATA"), ""))
                contenuto = Replace(contenuto, "$permsogg$", par.IfNull(myReaderGenerale("PERMESSO_SOGG_N"), ""))
                contenuto = Replace(contenuto, "$rilpermsog$", par.FormattaData(par.IfNull(myReaderGenerale("PERMESSO_SOGG_DATA"), "")))
                contenuto = Replace(contenuto, "$rinnovopermsogg$", par.IfNull(myReaderGenerale("PERMESSO_SOGG_RINNOVO"), ""))
                contenuto = Replace(contenuto, "$dcadrinps$", par.FormattaData(par.IfNull(myReaderGenerale("PERMESSO_SOGG_SCADE"), "")))
                contenuto = Replace(contenuto, "$pgdom$", par.IfNull(myReaderGenerale("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReaderGenerale("PG_DICH"), ""))


                tblTestata = tblTestata & "<tr><td><table style='width: 100%;'> " _
                & "<tr class='LeftGrande'><td>CODICE UTENTE</td> " _
                & "<td><b>" & par.IfNull(myReaderGenerale("COD_UTENTE"), "") & "</b></td><td style='text-align: right' width='10%'>PG DOM.:</td> " _
                & "<td><b>" & par.IfNull(myReaderGenerale("PG_DOM"), "") & "</b> - PG DICH.:<b>" & par.IfNull(myReaderGenerale("PG_DICH"), "") & "</b></td> " _
                & "<td style='text-align: right'>CODICE CONTRATTO</td> " _
                & "<td style='text-align: right'><b>" & codContr & "</b></td></tr> " _
                & "</table></td></tr><table width='100%'><tr class='CentratoGrande'><td>&nbsp;</td></tr> " _
                & "<tr class='CentratoGrande'><td>DICHIARAZIONE A NORMA DEGLI ARTICOLI 46 E 47 DEL DPR N.445/2000</td></tr> " _
                & "<tr><td>&nbsp;</td></tr></table>"

                contenuto = Replace(contenuto, "$testata$", tblTestata)


                par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.*,SISCOM_MI.ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS RUOLO_PAR,TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCC,TIPOLOGIA_TITOLO.DESCRIZIONE AS VALENZA " _
                    & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.TIPOLOGIA_OCCUPANTE,SISCOM_MI.TIPOLOGIA_TITOLO WHERE " _
                    & "RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_TITOLO = TIPOLOGIA_TITOLO.COD " _
                    & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = TIPOLOGIA_OCCUPANTE.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND (COD_FISCALE='" & codFisc & "' OR COGNOME||' '||NOME = '" & intestatario & "') AND rapporti_utenza.cod_contratto ='" & codContr & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then

                    contenuto = Replace(contenuto, "$valenza$", par.IfNull(myReader2("VALENZA"), ""))
                    contenuto = Replace(contenuto, "$ruolfamil$", par.IfNull(myReader2("RUOLO_PAR"), ""))
                    contenuto = Replace(contenuto, "$tipoccupante$", par.IfNull(myReader2("TIPO_OCC"), ""))
                    contenuto = Replace(contenuto, "$cittadinanza$", par.IfNull(myReader2("CITTADINANZA"), ""))
                    contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader2("TELEFONO"), ""))
                    contenuto = Replace(contenuto, "$dataingncleo$", par.FormattaData(par.IfNull(myReader2("DATA_INIZIO"), "")))
                    contenuto = Replace(contenuto, "$datauscita$", par.FormattaData(par.IfNull(myReader2("DATA_FINE"), "")))
                    contenuto = Replace(contenuto, "$tipodoc$", par.IfNull(myReader2("TIPO_DOC"), ""))

                End If
                myReader2.Close()

                par.cmd.CommandText = "SELECT SUM(DIPENDENTE) AS DIPENDENTE,SUM(PENSIONE) AS PENSIONE, SUM(AUTONOMO) AS AUTONOMO, SUM(OCCASIONALI) AS OCCASIONALI " _
                    & "FROM DOMANDE_REDDITI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = DOMANDE_REDDITI_VSA.ID_COMPONENTE AND COMP_NUCLEO_VSA.ID=" & par.IfNull(myReaderGenerale("COD_UTENTE"), "")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$eurodipend$", Format(CDec(par.IfNull(myReader("DIPENDENTE"), "0")), "##,##0.00"))
                    contenuto = Replace(contenuto, "$europens$", Format(CDec(par.IfNull(myReader("PENSIONE"), "0")), "##,##0.00"))
                    contenuto = Replace(contenuto, "$rlavautonomo$", Format(CDec(par.IfNull(myReader("AUTONOMO"), "0")), "##,##0.00"))
                    contenuto = Replace(contenuto, "$rlavoccasionale$", Format(CDec(par.IfNull(myReader("OCCASIONALI"), "0")), "##,##0.00"))
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IRPEF FROM COMP_DETRAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = COMP_DETRAZIONI_VSA.ID_COMPONENTE AND COMP_DETRAZIONI_VSA.ID_TIPO = 0 AND COMP_NUCLEO_VSA.ID=" & par.IfNull(myReaderGenerale("COD_UTENTE"), "")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$irpefdovuta$", Format(CDec(par.IfNull(myReader("IRPEF"), "0")), "##,##0.00"))
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT SUM(IMPORTO) AS SANITARIA FROM COMP_DETRAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = COMP_DETRAZIONI_VSA.ID_COMPONENTE AND COMP_DETRAZIONI_VSA.ID_TIPO = 1 AND COMP_NUCLEO_VSA.ID=" & par.IfNull(myReaderGenerale("COD_UTENTE"), "")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$spesesanitarie$", Format(CDec(par.IfNull(myReader("SANITARIA"), "0")), "##,##0.00"))
                End If
                myReader.Close()

                'campi di cui non conosciamo il valore
                contenuto = Replace(contenuto, "$naznascita$", " ")
                contenuto = Replace(contenuto, "$fiscalcarico$", "SI")
                contenuto = Replace(contenuto, "$statocivile$", " ")
                contenuto = Replace(contenuto, "$fax$", " ")
                contenuto = Replace(contenuto, "$email$", " ")
                contenuto = Replace(contenuto, "$condizioneprof$", " ")
                contenuto = Replace(contenuto, "$motivazione$", " ")
                contenuto = Replace(contenuto, "$luogorilascio$", " ")
                contenuto = Replace(contenuto, "$rilpermsog$", " ")
                contenuto = Replace(contenuto, "$rinnovopermsogg$", " ")
                contenuto = Replace(contenuto, "$dcadrinps$", " ")
                contenuto = Replace(contenuto, "$eurotuir$", " ")
                contenuto = Replace(contenuto, "$euroassim$", " ")
                contenuto = Replace(contenuto, "$gglavorodip$", " ")
                contenuto = Replace(contenuto, "$ggassimilati$", " ")
                contenuto = Replace(contenuto, "$ggdisocc$", " ")
                contenuto = Replace(contenuto, "$ggpensione$", " ")
                contenuto = Replace(contenuto, "$tipopensione$", " ")
                contenuto = Replace(contenuto, "$rpensinvciv$", " ")
                contenuto = Replace(contenuto, "$rpensguerra$", " ")
                contenuto = Replace(contenuto, "$rrenditainail$", " ")
                contenuto = Replace(contenuto, "$rterreni$", " ")
                contenuto = Replace(contenuto, "$rfabbricati$", " ")
                contenuto = Replace(contenuto, "$rsussidi$", " ")
                contenuto = Replace(contenuto, "$raccomp$", " ")
                contenuto = Replace(contenuto, "$rassegnofigli$", " ")
                contenuto = Replace(contenuto, "$rassegnocong$", " ")
                contenuto = Replace(contenuto, "$raltri$", " ")
                contenuto = Replace(contenuto, "$ragrari$", " ")
                contenuto = Replace(contenuto, "$ggautonomopi$", " ")
                contenuto = Replace(contenuto, "$ggautonomospi$", " ")
                contenuto = Replace(contenuto, "$ggoccasionali$", " ")
                contenuto = Replace(contenuto, "$addcomun$", " ")
                contenuto = Replace(contenuto, "$onerided$", " ")
                contenuto = Replace(contenuto, "$spricovero$", " ")
                contenuto = Replace(contenuto, "$addregion$", " ")
                contenuto = Replace(contenuto, "$noreddito$", " ")
                contenuto = Replace(contenuto, "$rlavautonompi$", " ")

                contenuto = Replace(contenuto, "$dataodierna$", Format(Now, "dd/MM/yyyy"))


                par.cmd.CommandText = "SELECT SUM(IMPORTO) AS PATR_MOB FROM COMP_PATR_MOB_VSA,COMP_NUCLEO_VSA " _
                    & "WHERE COMP_NUCLEO_VSA.ID = COMP_PATR_MOB_VSA.ID_COMPONENTE AND COMP_NUCLEO_VSA.ID=" & par.IfNull(myReaderGenerale("COD_UTENTE"), "")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$eurpatrmob$", Format(CDec(par.IfNull(myReader("PATR_MOB"), "0")), "##,##0.00"))
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT SUM(VALORE) AS PATR_IMMOB FROM COMP_PATR_IMMOB_VSA,COMP_NUCLEO_VSA " _
                    & "WHERE COMP_NUCLEO_VSA.ID = COMP_PATR_IMMOB_VSA.ID_COMPONENTE AND COMP_NUCLEO_VSA.ID=" & par.IfNull(myReaderGenerale("COD_UTENTE"), "")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$eurpatrimmob$", Format(CDec(par.IfNull(myReader("PATR_IMMOB"), "0")), "##,##0.00"))
                End If
                myReader.Close()

                tblGenerale = tblGenerale & contenuto
                contenuto = contenutoORIG
            End While
            myReaderGenerale.Close()



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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = "03_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(tblGenerale, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Sub

#End Region



#Region "StampeDocumenti per AMPLIAMENTO"

    Protected Sub pdfRicRichiesta()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModelliAmpliamento\RicAmpliamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim luogoNasc As String
            Dim luogoRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim sigla As String = ""
            Dim id_dom As Long
            Dim tbAllegati As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 8

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoNasc = par.IfNull(myReader("NOME"), "")
                sigla = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                'contenuto = Replace(contenuto, "$data$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datanascita$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$comunenascita$", luogoNasc)
                contenuto = Replace(contenuto, "$provincia$", sigla)
                contenuto = Replace(contenuto, "$localita$", luogoRes)
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$annoredditi$", par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                If myReader("ANNO_SIT_ECONOMICA") = "2006" Or myReader("ANNO_SIT_ECONOMICA") = "2007" Then
                    contenuto = Replace(contenuto, "$dataDecorr$", "01/01/2008")
                Else
                    contenuto = Replace(contenuto, "$dataDecorr$", "01/01/2010")
                End If
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataInizioVal$", par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VAL"), "")))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            Dim ElencoAmp As String = ""
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                ElencoAmp = ElencoAmp & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " a decorrere dal " & par.FormattaData(par.IfNull(myReader("DATA_INGRESSO_NUCLEO"), "")) & ";<br/>"
            End While
            myReader.Close()
            contenuto = Replace(contenuto, "$nuovoCompon$", ElencoAmp)

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_ALLEGATI.ID_DOC AND VSA_DOC_ALLEGATI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            Dim strTbl2 As String = ""
            While myReader.Read
                NumDoc = NumDoc - 1
                strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
                tbAllegati = tbAllegati & strTbl
                ndx = ndx + 1
            End While
            myReader.Close()

            If tbAllegati.Contains(ndx - 1) Then
                strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            End If

            tbAllegati = Replace(tbAllegati, strTbl, strTbl2)

            If NumDoc > 0 Then
                For i As Integer = 0 To NumDoc - 1
                    tbAllegati = tbAllegati & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>&nbsp;</td></tr>"
                Next
            End If

            tbAllegati = tbAllegati & "</table>"

            contenuto = Replace(contenuto, "$docallegati$", tbAllegati)

            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = "A0_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Protected Sub pdfMoreUxorio()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModelliAmpliamento\ConvMoreUxorio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim id_dom As Long
            Dim codUi As String = ""
            Dim codContr As String = ""


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$localita$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            Dim elencoNuovi As String = ""
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                elencoNuovi = elencoNuovi & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & ";<br/>"
            End While
            myReader.Close()
            contenuto = Replace(contenuto, "$nuovoCompon$", elencoNuovi)

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    'contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$sportello$", "")

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True


            'sostituire nuovo codice da qui
            Dim nomefile As String = "07_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub pdfConvAssist()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModelliAmpliamento\ConvAssistenza.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim id_dom As Long
            Dim codUi As String = ""
            Dim codContr As String = ""


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$localita$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$nuovoCompon$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                    contenuto = Replace(contenuto, "$datanasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                    contenuto = Replace(contenuto, "$luogonasc$", par.IfNull(myReader2("NOME"), ""))
                End If
            End While
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
                contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
                contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
                contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
                contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
                contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$sportello$", "")

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True


            'sostituire nuovo codice da qui
            Dim nomefile As String = "08_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub pdfEsitoPos()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsPositivoAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\ComEsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPositCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\ComEsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPositCAMB2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\ComEsitoPositivo2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "PresaAttoRientro" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\PresaAttoRientro.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPosRiesAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\EsitoPositivoRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPosRiesRientro" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\PresaAttoRientroRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'Dim luogoRes As String
            Dim siglaRes As String = ""
            Dim id_dom As Long
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim codContr2 As String = ""
            Dim componenti As String = ""
            Dim idCausaleDom As Integer = 0


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")

            contenuto = Replace(contenuto, "$codunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)
            contenuto = Replace(contenuto, "$codcontratto2$", codContr2)

            If Request.QueryString("TIPO") = "EsPositCAMB2" Then
                idc = ottieniIDContr(codContr2)
            Else
                idc = ottieniIDContr(codContr)
            End If

            'contenuto = Replace(contenuto, "$indirizzo2$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                contenuto = Replace(contenuto, "$dataEvento$", par.FormattaData(par.IfNull(myReader("DATA_EVENTO"), "")))

                contenuto = Replace(contenuto, "$dataRiesame$", par.FormattaData(par.IfNull(myReader("DATA_EVENTO"), "")))


                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "__/__/____")))
                idCausaleDom = par.IfNull(myReader("id_causale_domanda"), 0)

                Select Case idCausaleDom
                    Case "5"
                        contenuto = Replace(contenuto, "$numeroArticolo$", "dell'ex art. 20, comma 3")
                    Case "6"
                        contenuto = Replace(contenuto, "$numeroArticolo$", "dell'art. 20, comma 7")
                End Select

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA = " & id_dom & " AND COD_DECISIONE = '2'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                contenuto = Replace(contenuto, "$dataEsPositivo$", par.FormattaData(par.IfNull(lettore("DATA"), "")))
            End If
            lettore.Close()

            'par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA = " & id_dom & " AND COD_DECISIONE = '5'"
            'lettore = par.cmd.ExecuteReader()
            'If lettore.Read Then
            '    contenuto = Replace(contenuto, "$dataRiesame$", par.FormattaData(par.IfNull(lettore("DATA"), "")))
            'End If
            'lettore.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                componenti += par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "<br/>"
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$nuovoCompon$", componenti)

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$citta$", "")
            contenuto = Replace(contenuto, "$via$", "")
            contenuto = Replace(contenuto, "$civico$", "")
            contenuto = Replace(contenuto, "$cap$", "")

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.*,RAPPORTI_UTENZA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo1_2$", par.IfNull(myReader("CAP_RESIDENZA"), "") & " " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("PROVINCIA_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2_2$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo0_2$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("COD_TIPO_LIVELLO_PIANO"), ""))
                contenuto = Replace(contenuto, "$cod_ui2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()


            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True



            Dim nomefile As String = "" '"09_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            If Request.QueryString("TIPO") = "PresaAttoRientro" Then
                nomefile = "A1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "EsPosRiesAMPL" Then
                nomefile = "A2_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "EsPosRiesRientro" Then
                nomefile = "A3_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "09_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If


            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub pdfStatoFamiglia()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "StFamigliaAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\AutocertStatoFamiglia.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Else
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\AutocertStatoFamiglia.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String = ""
            Dim luogoNascita As String = ""
            Dim id_dom As Long
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim tblCompon As String = ""
            Dim tblCompon2 As String = ""
            Dim idNuovoComp As Long

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$contratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$localita$", luogoRes)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoNascita = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$luogonascita$", luogoNascita)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datanasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    idNuovoComp = par.IfNull(myReader("ID"), "")
                    contenuto = Replace(contenuto, "$nuovoCompon$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                    contenuto = Replace(contenuto, "$datanasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                    contenuto = Replace(contenuto, "$luogonasc$", par.IfNull(myReader2("NOME"), ""))
                End If
            End While
            myReader.Close()


            '******* tabella componenti nucleo *******
            tblCompon = "<table style='border: thin solid #000000; width: 100%; border-collapse: collapse;'><tr>" _
                & "<td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>Cognome e nome</td>" _
                & "<td align='center' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;'>Luogo e data di nascita</td>" _
                & "<td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>Luogo di residenza</td></tr>"

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " and ID <>" & idNuovoComp & " ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader
            While myReader.Read

                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    tblCompon = tblCompon & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>" _
                        & "<td align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader2("NOME"), "") & " " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "</td>" _
                        & "<td align='left' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>" & luogoRes & "</td></tr>"
                End If
                myReader2.Close()

            End While
            myReader.Close()

            tblCompon = tblCompon & "</table>"
            contenuto = Replace(contenuto, "$tabellaComponenti$", tblCompon)



            '************** 27/03/2012 tabella per DATI OSPITE ***************
            tblCompon2 = "<table>"
            If Request.QueryString("TIPO") = "StFamigliaAMPL" Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & Request.QueryString("IDDICHIARAZ") _
                & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE AND ID = (SELECT MIN (NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE) AS ID FROM NUOVI_COMP_NUCLEO_VSA," _
                & "COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE AND ID_DICHIARAZIONE = " & Request.QueryString("IDDICHIARAZ") & " )"
            Else
                par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.*,COMP_NUCLEO_OSPITI_VSA.DATA_NASC AS DATA_NASCITA FROM COMP_NUCLEO_OSPITI_VSA,DOMANDE_BANDO_VSA WHERE ID_DOMANDA = " & id_dom _
                & " AND DOMANDE_BANDO_VSA.ID = COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA AND COMP_NUCLEO_OSPITI_VSA.ID = (SELECT MIN (COMP_NUCLEO_OSPITI_VSA.ID) AS ID FROM COMP_NUCLEO_OSPITI_VSA," _
                & "DOMANDE_BANDO_VSA WHERE DOMANDE_BANDO_VSA.ID = COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA AND ID_DOMANDA = " & id_dom & ")"
            End If
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If Request.QueryString("TIPO") = "StFamigliaAMPL" Then
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Else
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_OSPITI_VSA WHERE ID=" & myReader("ID") & ")"
                End If

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$datanasc2$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                    contenuto = Replace(contenuto, "$referenteNucleo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                    contenuto = Replace(contenuto, "$luogonascita2$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()
            End If
            myReader.Close()


            tblCompon2 = "<table style='border: thin solid #000000; width: 100%; border-collapse: collapse;'><tr>" _
            & "<td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>Cognome e nome</td>" _
            & "<td align='center' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;'>Luogo e data di nascita</td>" _
            & "<td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>Luogo di residenza</td></tr>"

            If Request.QueryString("TIPO") = "StFamigliaAMPL" Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE ORDER BY PROGR ASC"
            Else
                par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.*,COMP_NUCLEO_OSPITI_VSA.DATA_NASC AS DATA_NASCITA FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_OSPITI_VSA WHERE ID_DOMANDA=" & id_dom & " AND DOMANDE_BANDO_VSA.ID = COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA ORDER BY COMP_NUCLEO_OSPITI_VSA.ID ASC"
            End If
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                If Request.QueryString("TIPO") = "StFamigliaAMPL" Then
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Else
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_OSPITI_VSA WHERE ID=" & myReader("ID") & ")"
                End If

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    tblCompon2 = tblCompon2 & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>" _
                        & "<td align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader2("NOME"), "") & " " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "</td>" _
                        & "<td align='left' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000'>" & par.IfNull(myReader("COMUNE_RES_DNTE"), "") & " </td></tr>"
                End If
                myReader2.Close()

            End While
            myReader.Close()

            tblCompon2 = tblCompon2 & "</table>"
            contenuto = Replace(contenuto, "$tabellaCompNuovi$", tblCompon2)

            '************** FINE tabella per DATI OSPITE ***************




            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))

            contenuto = Replace(contenuto, "$sportello$", "")

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)


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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True


            'sostituire nuovo codice da qui
            Dim nomefile As String = ""
            nomefile = "06_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

#End Region


#Region "StampeDocumenti per SUBENTRO"

    Private Sub pdfDomSub()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "DomandaSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\SubentroIntest.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DomandaSUBFFOO" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\RichiestaVariazAssegnazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RicezRicVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\RicezRichiestat.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RichOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\RichOspitalita.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RichOSPbada" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\RichOspitBadante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RichOSPscol" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\RichOspitScolast.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "RichCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\RichCambioConsens.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim tblCompon As String = ""
            Dim codContr2 As String = ""
            Dim autorizzato As Integer = 0

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")

            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            If codContr2 <> "" Then
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Else
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD(+) AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            End If
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("FL_AUTORIZZAZIONE"), -1) = 1 Then
                    autorizzato = 1
                End If
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LUOGO"), ""))
                If codContr2 = "" Then
                    contenuto = Replace(contenuto, "$causaleDom$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                End If
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataNasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$numDoc$", par.IfNull(myReader("CARTA_I"), ""))
                contenuto = Replace(contenuto, "$enteRilascio$", par.IfNull(myReader("CARTA_I_RILASCIATA"), ""))
                contenuto = Replace(contenuto, "$dataRilascio$", par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")))

                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE ID = " & par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$comuneNasc$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()
                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE ID = " & par.IfNull(myReader("ID_LUOGO_RES_DNTE"), "")
                myReader2 = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$citta$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SCALA"), ""))
            End If
            myReader.Close()

            Dim condizione As String = ""
            If autorizzato = 1 Then
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'EXINTE'"
            Else
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            End If
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' " & condizione & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$exintestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                If par.IfNull(myReader("TIPO_DOC"), 0) = 0 Then
                    contenuto = Replace(contenuto, "$tipoDoc$", "CARTA D'IDENTITA'")
                End If
                contenuto = Replace(contenuto, "$numeroDoc$", par.IfNull(myReader("NUM_DOC"), "--"))
                contenuto = Replace(contenuto, "$dataDoc$", par.FormattaData(par.IfNull(myReader("DATA_DOC"), "--")))
                contenuto = Replace(contenuto, "$cittadinanza$", par.IfNull(myReader("CITTADINANZA"), "--"))
                contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("TELEFONO"), "--"))
                contenuto = Replace(contenuto, "$datanasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$comuResid$", par.IfNull(myReader("COMUNE_RESIDENZA"), ""))

                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReader("COD_FISCALE"), "").ToString.Substring(11, 4) & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$comNasc$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()
            End If
            myReader.Close()

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$residenza$", par.IfNull(myReader("COMUNE_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$numCivico2$", par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$numalloggio2$", par.IfNull(myReader("INTERNO"), ""))
                contenuto = Replace(contenuto, "$codiceunita2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()



            Dim tblCompon2 As String = ""
            tblCompon2 = "<table cellpadding='1px' style='border: 1px solid #000000; width: 100%; border-collapse: collapse;'><tr>" _
            & "<td nowrap align='center' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>Cognome e nome</td>" _
            & "<td nowrap align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Luogo e data di nascita</td>" _
            & "<td nowrap align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Cod. fiscale</td>" _
            & "<td nowrap align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Num. Documento</td>" _
            & "<td nowrap align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Data Rilascio</td>" _
            & "<td nowrap align='center' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>Luogo di residenza</td></tr>"

            par.cmd.CommandText = "SELECT COMP_NUCLEO_OSPITI_VSA.*,COMP_NUCLEO_OSPITI_VSA.DATA_NASC AS DATA_NASCITA FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_OSPITI_VSA WHERE ID_DOMANDA=" & id_dom & " AND DOMANDE_BANDO_VSA.ID = COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA ORDER BY COMP_NUCLEO_OSPITI_VSA.ID ASC"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                If Request.QueryString("TIPO") = "StFamigliaAMPL" Then
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Else
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_OSPITI_VSA WHERE ID=" & myReader("ID") & ")"
                End If

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    tblCompon2 = tblCompon2 & "<tr><td nowrap align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>" _
                        & "<td nowrap align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader2("NOME"), "") & " " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "</td>" _
                        & "<td nowrap align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader("COD_FISCALE"), "") & "</td>" _
                        & "<td nowrap align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader("CARTA_I"), "") & "</td>" _
                        & "<td nowrap align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")) & "</td>" _
                        & "<td nowrap align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & par.IfNull(myReader("COMUNE_RES_DNTE"), "") & " </td></tr>"
                End If
                myReader2.Close()

            End While
            myReader.Close()

            tblCompon2 = tblCompon2 & "</table>"
            contenuto = Replace(contenuto, "$tabellaCompNuovi$", tblCompon2)




            '***** Carica Informazioni Ospite ******
            Dim nomeOspite As String = ""
            Dim cfiscale As String = ""
            Dim dataNasc As String = ""
            Dim comNasc As String = ""
            If Request.QueryString("TIPO") = "RichOSPbada" Or Request.QueryString("TIPO") = "RichOSPscol" Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA WHERE ID_DOMANDA=" & id_dom
                myReader = par.cmd.ExecuteReader
                While myReader.Read
                    nomeOspite &= par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " "
                    cfiscale &= par.IfNull(myReader("COD_FISCALE"), "") & " "
                    dataNasc &= par.FormattaData(par.IfNull(myReader("DATA_NASC"), "")) & " "
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReader("COD_FISCALE"), "").ToString.Substring(11, 4) & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        comNasc &= par.IfNull(myReader2("NOME"), "") & " "
                    End If
                    myReader2.Close()
                End While
                myReader.Close()
                contenuto = Replace(contenuto, "$nomeOspite$", nomeOspite)
                contenuto = Replace(contenuto, "$codFiscale2$", cfiscale)
                contenuto = Replace(contenuto, "$dataNasc2$", dataNasc)
                contenuto = Replace(contenuto, "$comNasc2$", comNasc)
            End If
            '***** FINE Carica Informazioni Ospite ******

            '******* TABELLA COMPONENTI NUCLEO *******
            tblCompon = "<br/><table style='border: 1px solid #000000; width: 100%; border-collapse: collapse;'><tr>" _
                & "<td align='center' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>N.</td>" _
                & "<td align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Cognome e nome</td>" _
                & "<td align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Luogo e data di nascita</td>" _
                & "<td align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Cod. fiscale</td>" _
                & "<td align='center' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>Grado di parentela</td></tr>"

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,T_TIPO_PARENTELA WHERE COMP_NUCLEO_VSA.GRADO_PARENTELA = T_TIPO_PARENTELA.COD  AND ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader
            Dim numComp As Integer = 0
            While myReader.Read
                numComp = numComp + 1
                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    tblCompon = tblCompon & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & numComp & "</td>" _
                        & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>" _
                        & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader2("NOME"), "") & " " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "</td>" _
                        & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader("COD_FISCALE"), "") & "</td>" _
                        & "<td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td></tr>"
                End If
                myReader2.Close()
            End While
            myReader.Close()

            tblCompon = tblCompon & "</table>"
            contenuto = Replace(contenuto, "$tabellaComponenti$", tblCompon)
            '*********** FINE TABELLA COMPONENTI NUCLEO

            contenuto = SostituisciBarcode(Server.MapPath("..\FileTemp\") & par.RicavaBarCode(6, id_dom), contenuto)


            contenuto = Replace(contenuto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))
            If Request.QueryString("TIPO") = "DomandaSUBFFOO" Then
                contenuto = caricaStruttRefer(contenuto)
            Else
                contenuto = caricaRespFiliale(idc, contenuto)
            End If

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = ""
            If Request.QueryString("TIPO") = "DomandaSUB" Or Request.QueryString("TIPO") = "DomandaSUBFFOO" Then
                nomefile = "S1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "RicezRicVOL" Then
                nomefile = "V2_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "RichOSP" Then
                nomefile = "O1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "RichOSPbada" Then
                nomefile = "O2_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "RichOSPscol" Then
                nomefile = "O3_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "RichCAMB" Or Request.QueryString("TIPO") = "RichCAMB2" Then
                nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfRichiesta2()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "RichCAMB2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\RichCambioConsens2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim tblCompon As String = ""
            Dim codContr2 As String = ""

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")

            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)
            contenuto = Replace(contenuto, "$codcontratto2$", codContr2)

            idc = ottieniIDContr(codContr2)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))

                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LUOGO"), ""))
                'If codContr2 = "" Then
                '    contenuto = Replace(contenuto, "$causaleDom$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                'End If
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                id_dom = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                If par.IfNull(myReader("TIPO_DOC"), 0) = 0 Then
                    contenuto = Replace(contenuto, "$tipoDoc$", "CARTA D'IDENTITA'")
                End If
                contenuto = Replace(contenuto, "$numeroDoc$", par.IfNull(myReader("NUM_DOC"), "--"))
                contenuto = Replace(contenuto, "$dataDoc$", par.FormattaData(par.IfNull(myReader("DATA_DOC"), "--")))
                contenuto = Replace(contenuto, "$cittadinanza$", par.IfNull(myReader("CITTADINANZA"), "--"))
                contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("TELEFONO"), "--"))
                contenuto = Replace(contenuto, "$datanasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("COMUNE_RESIDENZA"), ""))

                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReader("COD_FISCALE"), "").ToString.Substring(11, 4) & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$comNasc$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()
            End If
            myReader.Close()

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("TIPO_DOC"), 0) = 0 Then
                    contenuto = Replace(contenuto, "$tipoDoc2$", "CARTA D'IDENTITA'")
                End If
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$comuResid2$", par.IfNull(myReader("COMUNE_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$numCivico2$", par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$numalloggio2$", par.IfNull(myReader("INTERNO"), ""))
                contenuto = Replace(contenuto, "$codiceunita2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
                contenuto = Replace(contenuto, "$datanasc2$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM SISCOM_MI.ANAGRAFICA WHERE ID=" & myReader("ID") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$comNasc2$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()

                contenuto = Replace(contenuto, "$numeroDoc2$", par.IfNull(myReader("NUM_DOC"), "--"))
                contenuto = Replace(contenuto, "$dataDoc2$", par.FormattaData(par.IfNull(myReader("DATA_DOC"), "--")))
                contenuto = Replace(contenuto, "$cittadinanza2$", par.IfNull(myReader("CITTADINANZA"), "--"))
                contenuto = Replace(contenuto, "$telefono2$", par.IfNull(myReader("TELEFONO"), "--"))

            End If
            myReader.Close()


            '******* TABELLA PER COMPONENTI NUCLEO *******
            tblCompon = "<br/><table style='border: 1px solid #000000; width: 100%; border-collapse: collapse;'><tr>" _
                & "<td align='center' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>N.</td>" _
                & "<td align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Cognome e nome</td>" _
                & "<td align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Luogo e data di nascita</td>" _
                & "<td align='center' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>Cod. fiscale</td>" _
                & "<td align='center' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>Grado di parentela</td></tr>"

            Dim numComp As Integer = 0
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA WHERE " _
                    & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA=TIPOLOGIA_PARENTELA.COD " _
                    & "AND COD_CONTRATTO='" & codContr2 & "'"
            myReader = par.cmd.ExecuteReader
            While myReader.Read = True
                numComp = numComp + 1
                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM SISCOM_MI.ANAGRAFICA WHERE ID=" & myReader("ID") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    tblCompon = tblCompon & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & numComp & "</td>" _
                        & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>" _
                        & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader2("NOME"), "") & " " & par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")) & "</td>" _
                        & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(myReader("COD_FISCALE"), "") & "</td>" _
                        & "<td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td></tr>"

                End If
                myReader2.Close()
            End While
            myReader.Close()

            tblCompon = tblCompon & "</table>"
            contenuto = Replace(contenuto, "$tabellaComponenti$", tblCompon)


            contenuto = Replace(contenuto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfPermReqR()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader
            If Request.QueryString("TIPO") = "PermReqRSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\CertRinunciante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DichPermanenza" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\DichPermRequisitiERP.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "DichPermanenza2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\DichPermRequisitiERP2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "PermanenzaAMPL1" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\PermanenzaRequisiti1.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "PermanenzaAMPL2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\PermanenzaRequisiti2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim codContr2 As String = ""
            Dim id_dom As Long

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")
            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))

            If Request.QueryString("TIPO") = "DichPermanenza2" Then
                idc = ottieniIDContr(codContr2)
            Else
                idc = ottieniIDContr(codContr)
            End If

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
            & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
            & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_COR"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LUOGO_COR"), ""))
            End If
            myReader.Close()

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.*,RAPPORTI_UTENZA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$residenza$", par.IfNull(myReader("COMUNE_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), ""))
                contenuto = Replace(contenuto, "$numCivico2$", par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$numalloggio2$", par.IfNull(myReader("INTERNO"), ""))
                contenuto = Replace(contenuto, "$codiceunita2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()


            If Request.QueryString("TIPO") = "PermanenzaAMPL2" Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
                myReader = par.cmd.ExecuteReader
                While myReader.Read
                    par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM COMP_NUCLEO_VSA WHERE ID=" & myReader("ID") & ")"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        contenuto = Replace(contenuto, "$nuovoComp$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                        contenuto = Replace(contenuto, "$dataNasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                        contenuto = Replace(contenuto, "$luogoNasc$", par.IfNull(myReader2("NOME"), ""))
                    End If
                End While
                myReader.Close()
            End If


            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = "" '"S2_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")


            If Request.QueryString("TIPO") = "PermanenzaAMPL1" Then
                nomefile = "A4_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "PermanenzaAMPL2" Then
                nomefile = "A5_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "S2_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If


            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfSoprall()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader
            If Request.QueryString("TIPO") = "SoprallSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\Sopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "ModuloSoprallVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\Sopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "SopralluogoOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\Sopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "SoprallCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\Sopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "SoprallAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\Sopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "SoprallRID" Then
                sr1 = New StreamReader(Server.MapPath("TestoModelli\Sopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim codContr2 As String = ""
            Dim id_dom As Long
            Dim id_contr As Long

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")
            contenuto = Replace(contenuto, "$codiceunita$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            If codContr2 <> "" Then
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Else
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD(+) AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            End If
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReader("CAP_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LUOGO"), ""))
                contenuto = Replace(contenuto, "$numComp$", par.IfNull(myReader("N_COMP_NUCLEO"), ""))
                If codContr2 = "" Then
                    contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                Else
                    contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                End If
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("TELEFONO_REC_DNTE"), ""))

                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            'End If
            'myReader.Close()

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPOCONT FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                & "WHERE TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AND COD_CONTRATTO='" & codContr & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                id_contr = myReader("ID")
                contenuto = Replace(contenuto, "$datadecorr$", par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")))
                contenuto = Replace(contenuto, "$tipocontr$", par.IfNull(myReader("TIPOCONT"), ""))
                'If par.IfNull(myReader("MESSA_IN_MORA"), "") = "0" Then
                '    contenuto = Replace(contenuto, "$mora$", "NO")
                'Else
                '    contenuto = Replace(contenuto, "$mora$", "SI")
                'End If
                'If par.IfNull(myReader("PRATICA_AL_LEGALE"), "") = "0" Then
                '    contenuto = Replace(contenuto, "$praticaleg$", "NO")
                'Else
                '    contenuto = Replace(contenuto, "$praticaleg$", "SI")
                'End If
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$saldocontabile$", par.CalcolaSaldoAttuale(id_contr))
            contenuto = Replace(contenuto, "$datacontabile$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,UNITA_CONTRATTUALE.ID_UNITA,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
                & "EDIFICI.DENOMINAZIONE AS NOMEedificio,TIPO_DISPONIBILITA.DESCRIZIONE AS TIPOdispon " _
                & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_DISPONIBILITA," _
                & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE UNITA_IMMOBILIARI.id_unita_principale is null AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
                & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD " _
                & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & id_contr
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SC"), "--"))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIAN"), "--"))

                If par.IfNull(myReader("TIPOdispon"), "") <> "Occupata abusivamente" Then
                    contenuto = Replace(contenuto, "$occupabus$", "NO")
                Else
                    contenuto = Replace(contenuto, "$occupabus$", par.IfNull(myReader("TIPOdispon"), ""))
                End If
                contenuto = Replace(contenuto, "$vani$", par.IfNull(myReader("NUM_VANI"), ""))

                par.cmd.CommandText = "SELECT CONDOMINI.DENOMINAZIONE AS COND,AUTOGESTIONI.DENOMINAZIONE AS AUTOGEST FROM SISCOM_MI.EDIFICI,SISCOM_MI.COND_EDIFICI,SISCOM_MI.CONDOMINI,SISCOM_MI.AUTOGESTIONI_EDIFICI," _
                    & "SISCOM_MI.AUTOGESTIONI WHERE EDIFICI.ID = COND_EDIFICI.ID_EDIFICIO AND COND_EDIFICI.ID_CONDOMINIO = CONDOMINI.ID AND AUTOGESTIONI_EDIFICI.ID_EDIFICIO = EDIFICI.ID " _
                    & "AND AUTOGESTIONI_EDIFICI.ID_AUTOGESTIONE = AUTOGESTIONI.ID AND EDIFICI.ID = " & myReader("ID_EDIFICIO")
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$condominio$", par.IfNull(myReader("COND"), " "))
                    contenuto = Replace(contenuto, "$autogestione$", par.IfNull(myReader("AUTOGEST"), " "))
                Else
                    contenuto = Replace(contenuto, "$condominio$", "")
                    contenuto = Replace(contenuto, "$autogestione$", "")
                End If
                myReader2.Close()

                'par.cmd.CommandText = "SELECT dimensioni.valore AS sup_conv,edifici.* FROM siscom_mi.scale_edifici,siscom_mi.dimensioni,siscom_mi.edifici,siscom_mi.tipologia_unita_immobiliari,siscom_mi.unita_immobiliari " _
                '    & "WHERE unita_immobiliari.id_scala = scale_edifici.ID(+) AND unita_immobiliari.ID = dimensioni.id_unita_immobiliare AND dimensioni.cod_tipologia = 'SUP_CONV' AND edifici.ID = unita_immobiliari.id_edificio " _
                '    & "AND tipologia_unita_immobiliari.cod = unita_immobiliari.cod_tipologia(+) AND unita_immobiliari.ID =" & myReader("ID_UNITA")
                'Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'If myReader3.Read Then
                '    contenuto = Replace(contenuto, "$supmq$", par.IfNull(myReader3("SUP_CONV"), ""))
                '    contenuto = Replace(contenuto, "$nomeEdificio$", par.IfNull(myReader3("DENOMINAZIONE"), ""))
                'End If
                'myReader3.Close()

                par.cmd.CommandText = "SELECT DENOMINAZIONE FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.id_unita_principale is null and UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID =" & myReader("ID_UNITA")
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader3.Read Then
                    contenuto = Replace(contenuto, "$nomeEdificio$", par.IfNull(myReader3("DENOMINAZIONE"), ""))
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & myReader("ID_UNITA") & " AND COD_TIPOLOGIA='SUP_CONV'"
                Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader4.Read Then
                    contenuto = Replace(contenuto, "$supmq$", par.IfNull(myReader4("VALORE"), ""))
                Else
                    contenuto = Replace(contenuto, "$supmq$", "")
                End If
                myReader4.Close()

            End If
            myReader.Close()


            'par.cmd.CommandText = "SELECT * FROM VSA_SOPRALLUOGHI WHERE ID_DOMANDA=" & id_dom
            'Dim myReaderSopr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If myReaderSopr.Read Then
            '    contenuto = Replace(contenuto, "$noteSoprall$", "<i>" & par.IfNull(myReaderSopr("NOTE"), "") & "</i>")
            'End If
            'myReaderSopr.Close()


            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = "S3_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfComSopral()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader
            If Request.QueryString("TIPO") = "ComSopralSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\ComSopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "SoprUtenteVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\ComSopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "ComSoprallOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComSopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "ComSoprallCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\ComSopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "ComSoprallCAMB2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\ComSopralluogo2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "ComSopralAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\ComSopralluogo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim codContr2 As String = ""
            Dim id_dom As Long
            Dim luogoRes As String = ""
            Dim siglaRes As String = ""

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")
            contenuto = Replace(contenuto, "$cod_ui$", codUi)

            If Request.QueryString("TIPO") = "ComSoprallCAMB2" Then
                idc = ottieniIDContr(codContr2)
            Else
                idc = ottieniIDContr(codContr)
            End If

            contenuto = Replace(contenuto, "$codContr$", codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            If codContr2 <> "" Then
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Else
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD(+) AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            End If
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                If codContr2 = "" Then
                    contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                End If
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))



            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.*,RAPPORTI_UTENZA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo1_2$", par.IfNull(myReader("CAP_RESIDENZA"), "") & " " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("PROVINCIA_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2_2$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo0_2$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("COD_TIPO_LIVELLO_PIANO"), ""))
                contenuto = Replace(contenuto, "$cod_ui2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = "S4_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub pdfEsitoPosSub()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim luogoRes As String = ""
            Dim siglaRes As String = ""
            Dim ospiti As String = ""
            Dim autorizzato As Integer = 0


            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsitoPosSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\EsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoPosiVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\ComEsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPositOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComEsitoPos.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPositOSPbada" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComEsitoPosBadante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPositOSPscol" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComEsitoPosScolast.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoPosFFOO" Then
                Select Case Request.QueryString("CAUS")
                    Case 11
                        sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\EsitoVariazAssegFINE_lavoro.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    Case 12
                        sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\EsitoVariazAssegDECESSO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    Case 13
                        sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\EsitoVariazAssegSEPARAZ.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                End Select
            ElseIf Request.QueryString("TIPO") = "EsitoPosFFOO2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\EsitoVariazAssegDECESSO_2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$cod_ui$", codUi)

            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$localita$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                    & "FROM DICHIARAZIONI_VSA,T_CAUSALI_DOMANDA_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                    & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))

                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                autorizzato = par.IfNull(myReader("FL_AUTORIZZAZIONE"), -1)
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            Dim condizione As String = ""
            If autorizzato = 1 Then
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'EXINTE'"
            Else
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            End If
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' " & condizione & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$exintestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
                contenuto = Replace(contenuto, "$citta$", par.IfNull(myReader("COMUNE"), ""))
                contenuto = Replace(contenuto, "$via$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo3$", "SCALA " & par.IfNull(myReader("SCALA"), "") & " INTERNO " & par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT NOME,COGNOME FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ")
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                ospiti += par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "<br/>"
                contenuto = Replace(contenuto, "$dataScad$", par.FormattaData(par.IfNull(myReader("DATA_FINE_OSPITE"), "")))
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$nomeOspite$", ospiti)

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            If Request.QueryString("TIPO") = "EsitoPosFFOO" Or Request.QueryString("TIPO") = "EsitoPosFFOO2" Then
                contenuto = caricaStruttRefer(contenuto)
            Else
                contenuto = caricaRespFiliale(idc, contenuto)
            End If

            contenuto = SostituisciBarcode(Server.MapPath("..\FileTemp\") & par.RicavaBarCode(5, id_dom), contenuto)


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = ""

            If Request.QueryString("TIPO") = "EsPositOSPbada" Then
                nomefile = "O4_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "EsPositOSPscol" Then
                nomefile = "O5_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "09_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfEsitoPosCom()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModelliSubentro\EsitoPosCommissariato.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            'Dim id_contr As Long

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT ANAGRAFICA.*,RAPPORTI_UTENZA.ID AS IDCONT FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
            & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
            & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReader("COGNOME"), ""))
                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datanasc$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$luogo$", par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("PROVINCIA_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReader("CAP_RESIDENZA"), ""))
                idc = myReader("IDCONT")

                par.cmd.CommandText = "SELECT NOME,SIGLA FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReader("COD_FISCALE"), "").ToString.Substring(11, 4) & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$comunenasc$", par.IfNull(myReader2("NOME"), ""))
                    contenuto = Replace(contenuto, "$provincia$", par.IfNull(myReader2("SIGLA"), ""))
                End If
                myReader2.Close()
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,COMUNI_NAZIONI.SIGLA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
            & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,COMUNI_NAZIONI WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND COMUNI_NAZIONI.ID(+)=DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE " _
            & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$nuovointest$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico2$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$cap2$", par.IfNull(myReader("CAP_RES_DNTE"), ""))

                contenuto = Replace(contenuto, "$luogo2$", par.IfNull(myReader("LUOGO"), "").ToString.ToUpper & " " & par.IfNull(myReader("SIGLA"), ""))
                contenuto = Replace(contenuto, "$causaleDom$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$datanasc2$", par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")))
                contenuto = Replace(contenuto, "$dataEvento$", par.FormattaData(par.IfNull(myReader("DATA_EVENTO"), "")))

                If par.IfNull(myReader("CARTA_I"), "") <> "" Then

                    contenuto = Replace(contenuto, "$tipodoc$", "Carta d'identità")
                    contenuto = Replace(contenuto, "$numdoc$", par.IfNull(myReader("CARTA_I"), ""))
                    contenuto = Replace(contenuto, "$datarilascio$", par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), "")))
                    contenuto = Replace(contenuto, "$autorita$", par.IfNull(myReader("CARTA_I_RILASCIATA"), ""))
                Else
                    contenuto = Replace(contenuto, "$tipodoc$", "")
                    contenuto = Replace(contenuto, "$numdoc$", "")
                    contenuto = Replace(contenuto, "$datarilascio$", "")
                    contenuto = Replace(contenuto, "$autorita$", "")

                End If

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReader("COD_FISCALE"), "").ToString.Substring(11, 4) & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$comunenasc2$", par.IfNull(myReader2("NOME"), ""))
                    contenuto = Replace(contenuto, "$provincia2$", par.IfNull(myReader2("SIGLA"), ""))
                    If par.IfNull(myReader2("cod"), "").ToString.Contains("Z") Then
                        contenuto = Replace(contenuto, "$cittadinanza$", par.IfNull(myReader2("NOME"), ""))
                        contenuto = Replace(contenuto, "$permsogg$", par.IfNull(myReader("PERMESSO_SOGG_N"), ""))
                    Else
                        contenuto = Replace(contenuto, "$cittadinanza$", "ITALIA")
                        contenuto = Replace(contenuto, "$permsogg$", " ")
                    End If
                End If
                myReader2.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOUI,UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
                & "INDIRIZZI.DESCRIZIONE AS INDIR,INDIRIZZI.CIVICO AS CIV,INDIRIZZI.CAP AS CAPIND,INDIRIZZI.LOCALITA AS LOC " _
                & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI," _
                & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE " _
                & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
                & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND UNITA_IMMOBILIARI.id_unita_principale is null " _
                & "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD = unita_immobiliari.COD_TIPOLOGIA AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$indirizzo3$", par.IfNull(myReader("INDIR"), ""))
                contenuto = Replace(contenuto, "$localita3$", par.IfNull(myReader("LOC"), ""))
                contenuto = Replace(contenuto, "$civico3$", par.IfNull(myReader("CIV"), ""))
                contenuto = Replace(contenuto, "$cap3$", par.IfNull(myReader("CAPIND"), ""))
                contenuto = Replace(contenuto, "$numvani$", par.IfNull(myReader("NUM_VANI"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SC"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIAN"), ""))
                contenuto = Replace(contenuto, "$tipoUI$", par.IfNull(myReader("TIPOUI"), "").ToString.ToLower)
            Else
                contenuto = Replace(contenuto, "$indirizzo3$", "")
                contenuto = Replace(contenuto, "$localita3$", "")
                contenuto = Replace(contenuto, "$civico3$", "")
                contenuto = Replace(contenuto, "$cap3$", "")
                contenuto = Replace(contenuto, "$numvani$", "")
                contenuto = Replace(contenuto, "$scala$", "")
                contenuto = Replace(contenuto, "$piano$", "")

            End If
            myReader.Close()


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$telefono$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))

            'End If
            'myReader.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = "S5_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfStampaDebito()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim codContr As String = ""
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "ComDEB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\ComAccertamDebito.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            codContr = Request.QueryString("NUMCONT")
            idc = ottieniIDContr(codContr)

            contenuto = Replace(contenuto, "$codContr$", codContr)
            contenuto = Replace(contenuto, "$dataoggi$", Format(Now, "dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$saldoAttuale$", Format(par.CalcolaSaldoAttuale(idc), "##,##0.00"))
            contenuto = Replace(contenuto, "$importoAccertato$", Format(CDec(par.IfEmpty(Request.QueryString("IMP"), 0)), "##,##0.00"))
            contenuto = Replace(contenuto, "$richiedente$", Request.QueryString("INTEST"))

            If Request.QueryString("NRA") = "1" Then
                contenuto = Replace(contenuto, "$numrate$", "in un'unica soluzione")
            Else
                contenuto = Replace(contenuto, "$numrate$", "in " & Request.QueryString("NRA") & " rate")
            End If

            par.cmd.CommandText = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOUI,UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
            & "INDIRIZZI.DESCRIZIONE AS INDIR,INDIRIZZI.CIVICO AS CIV,INDIRIZZI.CAP AS CAPIND,INDIRIZZI.LOCALITA AS LOC " _
            & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI," _
            & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE " _
            & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
            & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND UNITA_IMMOBILIARI.id_unita_principale is null " _
            & "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD = unita_immobiliari.COD_TIPOLOGIA AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIR"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LOC"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIV"), ""))
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            If Request.QueryString("TIPO") = "ComDEB" Then
                nomefile = "MD_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub pdfEsitoPosDRC()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim autorizzato As Integer = 0
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsPosCondom" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\EsitoPositivoCondomini.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Else
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\EsitoPositivoMorosita.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim tbAllegati As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 5

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codui$", codUi)

            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
            & "FROM DICHIARAZIONI_VSA,T_CAUSALI_DOMANDA_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
            & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nuovointest$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                'contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("IND_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))

                'id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                autorizzato = par.IfNull(myReader("FL_AUTORIZZAZIONE"), -1)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT NOME,COGNOME FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                                  & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                                  & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            Dim condizione As String = ""
            If autorizzato = 1 Then
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'EXINTE'"
            Else
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            End If
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' " & condizione & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$exintestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_ALLEGATI.ID_DOC AND VSA_DOC_ALLEGATI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            myReader = par.cmd.ExecuteReader
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            Dim strTbl2 As String = ""
            While myReader.Read
                NumDoc = NumDoc - 1
                strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
                tbAllegati = tbAllegati & strTbl
                ndx = ndx + 1
            End While
            myReader.Close()

            If tbAllegati.Contains(ndx - 1) Then
                strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            End If

            tbAllegati = Replace(tbAllegati, strTbl, strTbl2)

            If NumDoc > 0 Then
                For i As Integer = 0 To NumDoc - 1
                    tbAllegati = tbAllegati & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>&nbsp;</td></tr>"
                Next
            End If

            tbAllegati = tbAllegati & "</table>"

            contenuto = Replace(contenuto, "$docallegati$", tbAllegati)

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = ""
            If Request.QueryString("TIPO") = "EsPosCondom" Then
                nomefile = "SC_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "S6_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfComGov()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModelliSubentro\ComForzeOrdine.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codui$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_CAUSALI_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdom$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdich$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nuovointest$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$via$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            'End If
            'myReader.Close()

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = "S7_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfEsitNeg()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim codContr2 As String = ""
            Dim id_dom As Long
            Dim luogoRes As String = ""
            Dim siglaRes As String = ""
            Dim tbDoc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 5
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsitNegSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\ComEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitNegFFOO1" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\LetteraDecadArt18lett_f.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitNegFFOO2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\LetteraDecadReddAlto.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitNegFFOO3" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\LetteraDecadDimissioni.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "LettDecadComune" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\AlComunePerDECADENZA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoNegaVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\ComEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsiNegatOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsNegaCAMB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\ComEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsNegaCAMB2" Then
                sr1 = New StreamReader(Server.MapPath("ModelliCambioCons\ComEsitoNegativo2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "LettTrasfor" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentroFFOO\LettereDiTrasformazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            codContr2 = Request.QueryString("NUMCONT2")
            contenuto = Replace(contenuto, "$codunita$", codUi)

            contenuto = Replace(contenuto, "$contratto$", codContr)
            contenuto = Replace(contenuto, "$codcontratto2$", codContr2)

            If Request.QueryString("TIPO") = "EsNegaCAMB2" Then
                idc = ottieniIDContr(codContr2)
            Else
                idc = ottieniIDContr(codContr)
            End If

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            If codContr2 <> "" Then
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Else
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.PG_COLLEGATO AS PGDOMCOLL,DICHIARAZIONI_VSA.PG_COLLEGATO AS PGDICHCOLL,DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            End If
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("PGDOMCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), "") & "/" & par.IfNull(myReader("PGDOMCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                End If
                If par.IfNull(myReader("PGDICHCOLL"), "") <> "" Then
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), "") & "/" & par.IfNull(myReader("PGDICHCOLL"), ""))
                Else
                    contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                End If
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$via$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))

                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))

                id_dom = par.IfNull(myReader("ID_DOM"), "")

                If codContr2 = "" Then
                    contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                End If
                contenuto = Replace(contenuto, "$annoISEE$", par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            par.cmd.CommandText = "SELECT DATA_DECORRENZA FROM siscom_mi.rapporti_utenza WHERE ID=" & idc
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$dataDecorr$", par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), "")))
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo3$", "SCALA " & par.IfNull(myReader("SCALA"), "") & " INTERNO " & par.IfNull(myReader("INTERNO"), ""))

                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
                contenuto = Replace(contenuto, "$citta$", par.IfNull(myReader("COMUNE"), ""))
            End If
            myReader.Close()


            'par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & id_dom
            'myReader = par.cmd.ExecuteReader
            'Dim ndx As Integer = 1
            'Dim strTbl As String = ""
            'Dim strTbl2 As String = ""
            'While myReader.Read
            '    NumDoc = NumDoc - 1
            '    strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
            '    tbDoc = tbDoc & strTbl
            '    ndx = ndx + 1
            'End While
            'myReader.Close()

            'If tbDoc.Contains(ndx - 1) Then
            '    strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            'End If

            'tbDoc = Replace(tbDoc, strTbl, strTbl2)

            'If NumDoc > 0 Then
            '    For i As Integer = 0 To NumDoc - 1
            '        tbDoc = tbDoc & "<tr id='1'><td style='text-align: left; font-size:14pt;font-family:Arial ;'>&nbsp;</td></tr>"
            '    Next
            'End If

            'tbDoc = tbDoc & "</table>"

            'contenuto = Replace(contenuto, "$motivi$", tbDoc)

            Dim motivi As String = ""
            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=3 or COD_DECISIONE=8) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                motivi = par.IfNull(myReader("NOTE"), "")
                motivi = Replace(motivi, ",", ";<br/>")
                contenuto = Replace(contenuto, "$motivi$", motivi & ".")
            Else
                contenuto = Replace(contenuto, "$motivi$", "")
            End If
            myReader.Close()


            'Query per ricavare info dal nuovo Intestatario con cui si effettua il CAMBIO CONSENSUALE
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.*,UNITA_IMMOBILIARI.*,RAPPORTI_UTENZA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND COD_CONTRATTO ='" & codContr2 & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestCambio$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo1_2$", par.IfNull(myReader("CAP_RESIDENZA"), "") & " " & par.IfNull(myReader("COMUNE_RESIDENZA"), "") & " " & par.IfNull(myReader("PROVINCIA_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo2_2$", par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.IfNull(myReader("CIVICO_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$indirizzo0_2$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("COD_TIPO_LIVELLO_PIANO"), ""))
                contenuto = Replace(contenuto, "$cod_ui2$", par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), ""))
            End If
            myReader.Close()


            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            If Request.QueryString("TIPO") = "EsitNegFFOO1" Or Request.QueryString("TIPO") = "EsitNegFFOO2" Or Request.QueryString("TIPO") = "EsitNegFFOO3" Or Request.QueryString("TIPO") = "LettTrasfor" Or Request.QueryString("TIPO") = "LettDecadComune" Then
                contenuto = caricaStruttRefer(contenuto)
            Else
                contenuto = caricaRespFiliale(idc, contenuto)
            End If


            ''******** 11/09/2012 CODICE A BARRE ********
            Dim PercorsoBarCode As String = par.RicavaBarCode(3, id_dom)

            If Request.QueryString("TIPO") = "LettTrasfor" Then
                PercorsoBarCode = par.RicavaBarCode(5, id_dom)
            Else
                PercorsoBarCode = par.RicavaBarCode(3, id_dom)
            End If
            ''contenuto = Replace(contenuto, "$barcode$", Server.MapPath("..\FileTemp\") & PercorsoBarCode)
            contenuto = SostituisciBarcode(Server.MapPath("..\FileTemp\") & PercorsoBarCode, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = "04_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub


    Private Sub pdfEsitPosRi()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsitPosRiSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\EsitoPositivoRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoPosRiesVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\EsitoPositivoRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPosRiesOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\EsitoPositivoRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPosRiesOSPbada" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\EsitoPositivoRiesBadante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsPosRiesOSPscol" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\EsitoPositivoRiesScolast.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()


            Dim luogoRes As String
            Dim siglaRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim sigla As String = ""
            Dim id_dom As Long
            Dim ospiti As String = ""
            Dim autorizzato As Integer = 0


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$cod_ui$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_NAS_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                'luogoNasc = par.IfNull(myReader("NOME"), "")
                sigla = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$localita$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
            & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_CAUSALI_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
            & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReader("CAP_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))

                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)


                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "__/__/____")))
                autorizzato = par.IfNull(myReader("FL_AUTORIZZAZIONE"), -1)
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            Dim condizione As String = ""
            If autorizzato = 1 Then
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'EXINTE'"
            Else
                condizione = "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            End If
            par.cmd.CommandText = "SELECT NOME,COGNOME,ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
                & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "COD_CONTRATTO ='" & codContr & "' " & condizione & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$exintestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT NOME,COGNOME FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA WHERE " _
            & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
            & "COD_CONTRATTO ='" & codContr & "' AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ")
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                ospiti += par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "<br/>"
                contenuto = Replace(contenuto, "$dataScad$", par.FormattaData(par.IfNull(myReader("DATA_FINE_OSPITE"), "")))
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$nomeOspite$", ospiti)

            'par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            'myReader = par.cmd.ExecuteReader
            'If myReader.Read Then

            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
            '    contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            'End If
            'myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui
            Dim nomefile As String = ""

            If Request.QueryString("TIPO") = "EsPosRiesOSPbada" Then
                nomefile = "O7_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            ElseIf Request.QueryString("TIPO") = "EsPosRiesOSPscol" Then
                nomefile = "O8_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            Else
                nomefile = "S8_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub pdfEsitNegRies()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim luogoRes As String = ""
            Dim siglaRes As String = ""
            Dim tbDoc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 5
            Dim ospiti As String = ""
            Dim componenti As String = ""
            Dim sr1 As StreamReader

            If Request.QueryString("TIPO") = "EsitNegRiesSUB" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\ComEsitoNegRiesConOss.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitNegRiesSUBNoOS" Then
                sr1 = New StreamReader(Server.MapPath("ModelliSubentro\ComEsitoNegRiesNoOss.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoNegatRiesVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\ComEsitoNegRiesDecOsservazio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoNegatRiesNOVOL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliVoltura\ComEsitoNegRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoNegRiesOsservOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComEsitoNegConOsserv.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsitoNegRiesNoOsservOSP" Then
                sr1 = New StreamReader(Server.MapPath("ModelliOspitalita\ComEsitoNegNoOsserv.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsNegRiesameAMPL" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\ComEsitoNegRiesDecOsservazio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "EsNegRiesameNOoss" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\ComEsitoNegRiesame.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            ElseIf Request.QueryString("TIPO") = "ProvvDefComune" Then
                sr1 = New StreamReader(Server.MapPath("ModelliAmpliamento\ProvvedimDefinitivoCom.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End If

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codunita$", codUi)

            contenuto = Replace(contenuto, "$contratto$", codContr)
            contenuto = Replace(contenuto, "$cod_ui$", codUi)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$localita$", luogoRes)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                    & "FROM DICHIARAZIONI_VSA,T_CAUSALI_DOMANDA_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                    & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$datapg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("IND_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))

                id_dom = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$causale$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "__/__/____")))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND COD_DECISIONE=3"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$dataEsNeg$", par.FormattaData(par.IfNull(myReader("DATA"), "")))
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$citta$", "")
            contenuto = Replace(contenuto, "$via$", "")
            contenuto = Replace(contenuto, "$civico$", "")
            contenuto = Replace(contenuto, "$cap$", "")

            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=6 OR COD_DECISIONE=10) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$motiviNeg$", par.IfNull(myReader("NOTE"), ""))
            Else
                contenuto = Replace(contenuto, "$motiviNeg$", " ")
            End If
            myReader.Close()


            'par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & id_dom
            'myReader = par.cmd.ExecuteReader
            'Dim ndx As Integer = 1
            'Dim strTbl As String = ""
            'Dim strTbl2 As String = ""
            'While myReader.Read
            '    NumDoc = NumDoc - 1
            '    strTbl = "<tr id='" & ndx & "'><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader("DESCRIZIONE"), "") & ";</td></tr>"
            '    tbDoc = tbDoc & strTbl
            '    ndx = ndx + 1
            'End While
            'myReader.Close()

            'If tbDoc.Contains(ndx - 1) Then
            '    strTbl2 = Replace(strTbl, Right(strTbl, 11), ".</td></tr>")
            'End If

            'tbDoc = Replace(tbDoc, strTbl, strTbl2)

            'If NumDoc > 0 Then
            '    For i As Integer = 0 To NumDoc - 1
            '        tbDoc = tbDoc & "<tr id='1'><td style='text-align: left; font-size:14pt;font-family:Arial ;'>&nbsp;</td></tr>"
            '    Next
            'End If

            'tbDoc = tbDoc & "</table>"

            'contenuto = Replace(contenuto, "$motiviNeg$", tbDoc)


            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ")
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                ospiti += par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "<br/>"
                contenuto = Replace(contenuto, "$dataScad$", par.FormattaData(par.IfNull(myReader("DATA_FINE_OSPITE"), "")))
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$nomeOspite$", ospiti)


            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.cod_unita_immobiliare='" & codUi & "' and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
                contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))
                contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReader("RESPONSABILE"), ""))
                contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
                contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReader("ACRONIMO"), ""))
                contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
                contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReader("ACRONIMO"), "") & "/" & Request.QueryString("PROT"))
                contenuto = Replace(contenuto, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA,NUOVI_COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.ID = NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                componenti += par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "<br/>"
            End While
            myReader.Close()

            contenuto = Replace(contenuto, "$nuovoCompon$", componenti)


            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()


            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            'sostituire nuovo codice da qui

            Dim nomefile As String = ""

            If Request.QueryString("TIPO") = "EsitNegRiesSUBNoOS" Or Request.QueryString("TIPO") = "EsitoNegatRiesNO" Or Request.QueryString("TIPO") = "EsitoNegRiesNoOsservOSP" Or Request.QueryString("TIPO") = "EsNegRiesameNOoss" Then
                nomefile = "V1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "EsitNegRiesSUB" Or Request.QueryString("TIPO") = "EsitoNegRiesOsservOSP" Or Request.QueryString("TIPO") = "EsNegRiesameAMPL" Then
                nomefile = "O9_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            ElseIf Request.QueryString("TIPO") = "ProvvDefComune" Then
                nomefile = "A6_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            Else
                nomefile = "05_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            End If

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI

            Me.ZippaFiles(nomefile)

            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub



#End Region

#Region "Frontespizio"

    Private Sub GeneraFrontespizio()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim idDom As Long = 0
            Dim Data_Ora_Stampa As String = Format(Now, "yyyyMMddHHmmss")
            Dim CodiceProcesso As String = ""
            Dim IndiceProcesso As String = "4"
            Dim Progressivo As Integer = 0
            Dim BarCodeDaStampare As String = ""

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Frontespizio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            codContr = Request.QueryString("NUMCONT")
            contenuto = Replace(contenuto, "$codicecontratto$", codContr)

            Dim BarcodeMetodo As String = "TELERIK"
            par.cmd.CommandText = "select valore from parameter where id=129"
            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
            End If
            myReaderS.Close()

            par.cmd.CommandText = "select RAPPORTI_UTENZA.ID,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI," _
                & " SISCOM_MI.RAPPORTI_UTENZA WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO='" & codContr & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("ragione_sociale"), "") = "" Then
                    contenuto = Replace(contenuto, "$dichiarante$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                Else
                    contenuto = Replace(contenuto, "$dichiarante$", par.IfNull(myReader("ragione_sociale"), ""))
                End If
                idc = par.IfNull(myReader("id"), "-1")
            Else
                contenuto = Replace(contenuto, "$dichiarante$", "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzounita$", par.IfNull(myReader("descrizione"), "") & ", " & par.IfNull(myReader("civico"), "") & "   " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("nome"), "") & " " & par.IfNull(myReader("sigla"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.id as idDom,DOMANDE_BANDO_VSA.pg as pgDOM,TO_CHAR(TO_DATE(data_presentazione,'yyyyMMddHH24MISS'),'dd/mm/yyyy - HH24:mi') as data_pres," _
                   & "TO_CHAR(TO_DATE(data_autorizzazione,'yyyyMMddHH24MISS'),'dd/mm/yyyy - HH24:mi') as data_autorizz," _
                   & " T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM," _
                   & " T_MOTIVO_DOMANDA_VSA.COD_PROCESSO_KOFAX " _
                   & "FROM DICHIARAZIONI_VSA,T_CAUSALI_DOMANDA_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE  " _
                   & " DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE  " _
                   & "AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD(+) " _
                   & " AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
                   & " AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                idDom = par.IfNull(myReader("idDom"), 0)
                CodiceProcesso = par.IfNull(myReader("COD_PROCESSO_KOFAX"), "XXXXX")
                contenuto = Replace(contenuto, "$pgprocesso$", par.IfNull(myReader("pgDOM"), ""))
                contenuto = Replace(contenuto, "$nomeprocesso$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                contenuto = Replace(contenuto, "$tipospecifico$", par.IfNull(myReader("CAUSALE_DOM"), ""))
                contenuto = Replace(contenuto, "$datapresentazione$", par.IfNull(myReader("data_pres"), ""))
                contenuto = Replace(contenuto, "$dataautorizzazione$", par.IfNull(myReader("data_autorizz"), ""))
            Else
                CodiceProcesso = Request.QueryString("CODK")
                par.cmd.CommandText = "select * from T_MOTIVO_DOMANDA_VSA where COD_PROCESSO_KOFAX='" & CodiceProcesso & "'"
                Dim myReader00 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader00.Read Then
                    contenuto = Replace(contenuto, "$nomeprocesso$", par.IfNull(myReader00("DESCRIZIONE"), ""))
                End If
                myReader00.Close()
                contenuto = Replace(contenuto, "$pgprocesso$", "")
                contenuto = Replace(contenuto, "$tipospecifico$", "")
                contenuto = Replace(contenuto, "$datapresentazione$", "")
                contenuto = Replace(contenuto, "$dataautorizzazione$", "")
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$datastampa$", Mid(Data_Ora_Stampa, 7, 2) & "/" & Mid(Data_Ora_Stampa, 5, 2) & "/" & Mid(Data_Ora_Stampa, 1, 4) & " - " & Mid(Data_Ora_Stampa, 9, 2) & ":" & Mid(Data_Ora_Stampa, 11, 2))

            If Not Directory.Exists(Server.MapPath("..\ALLEGATI\LOCATARI\FRONTESPIZI\")) Then
                Directory.CreateDirectory(Server.MapPath("..\ALLEGATI\LOCATARI\FRONTESPIZI\"))
            End If

            Dim FileCodice As String = ""
            par.cmd.CommandText = "SELECT MAX(PROGRESSIVO) FROM PROCESSI_BARCODE_STAMPE WHERE ID_PROCESSO=4 AND ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Progressivo = par.IfNull(myReader(0), 0) + 1
                BarCodeDaStampare = UCase(codContr) & CodiceProcesso & Format(Progressivo, "00")
                par.cmd.CommandText = "INSERT INTO PROCESSI_BARCODE_STAMPE (ID,ID_PROCESSO,ID_CONTRATTO,PROGRESSIVO,DATA_ORA,CODICE) VALUES (SEQ_PROCESSI_BARCODE_STAMPE.NEXTVAL," & IndiceProcesso & "," & idc & "," & Progressivo & ",'" & Data_Ora_Stampa & "','" & BarCodeDaStampare & "')"
                par.cmd.ExecuteNonQuery()
                contenuto = Replace(contenuto, "$codice$", "*" & BarCodeDaStampare & "*")
                If BarcodeMetodo = "TELERIK" Then
                    FileCodice = par.CreaBarCode128(BarCodeDaStampare, "ALLEGATI\LOCATARI\FRONTESPIZI\", False)
                Else
                    FileCodice = RicavaBarCode39(BarCodeDaStampare, "ALLEGATI\LOCATARI\FRONTESPIZI\")
                End If
                contenuto = Replace(contenuto, "$barcode$", FileCodice)
            End If
            myReader.Close()

            Dim url As String = Server.MapPath("..\FileTemp\")
            
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 5
            pdfConverter1.PdfDocumentOptions.RightMargin = 5
            pdfConverter1.PdfDocumentOptions.TopMargin = 5
            pdfConverter1.PdfDocumentOptions.BottomMargin = 5
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False


            Dim nomefile As String = "Frontespizio_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\ALLEGATI\LOCATARI\FRONTESPIZI\"))


            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\FRONTESPIZI\" & nomefile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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

            Response.Redirect("..\ALLEGATI\LOCATARI\FRONTESPIZI\" & nomefile & ".zip", False)


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Response.Write(ex.Message)
        End Try

    End Sub
    Public Function RicavaBarCode39(ByVal Codice As String, ByVal DoveSalvare As String, Optional ByVal BarHeight As Integer = 40, Optional ByVal ImageWidth As Integer = 480, Optional ByVal ImageHeight As Integer = 40) As String
        Try
            Dim NomeFile As String = "CodeBar_" & Codice & "_" & Format(Now, "yyyyMMddHHmmss") & ".jpg"
            Dim codeBarImage As New System.Drawing.Bitmap(ImageWidth, ImageHeight)
            Dim barcode As New iTextSharp.text.pdf.Barcode39
            barcode.Code = Codice
            barcode.StartStopText = False
            barcode.Extended = False
            barcode.BarHeight = 28.0F
            barcode.Size = 12.0F
            barcode.N = 3.20000005F
            barcode.Baseline = 12.0F
            barcode.X = 1.09000003F
            codeBarImage = barcode.CreateDrawingImage(Color.Black, Color.White)
            codeBarImage.Save(System.Web.HttpContext.Current.Server.MapPath("~\" & DoveSalvare & "\") & NomeFile, System.Drawing.Imaging.ImageFormat.Jpeg)
            RicavaBarCode39 = NomeFile
        Catch ex As Exception
            RicavaBarCode39 = ""
        End Try
    End Function



#End Region


End Class
