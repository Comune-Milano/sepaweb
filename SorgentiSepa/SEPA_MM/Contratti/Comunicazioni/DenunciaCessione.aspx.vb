Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_TestoContratti_Ospitalita
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Private Function caricaRespFiliale(ByVal idContra As String, ByVal conten As String) As String
        Try
            Dim Responsabile As String = ""
            Dim Acronimo As String = ""
            Dim dataPresent As String = ""

            dataPresent = Format(Now, "yyyyMMdd")
            If dataPresent < "20141201" Then
                dataPresent = "20141201"
            End If

            conten = Replace(conten, "$firmaResp$", "Il resp. della sede territoriale")

            par.cmd.CommandText = "select cod_tipologia_contr_loc from siscom_mi.rapporti_utenza where id=" & idContra
            Dim tipoRU As String = par.IfNull(par.cmd.ExecuteScalar, "")
            If tipoRU = "USD" Then
                par.cmd.CommandText = " SELECT indirizzi.descrizione AS descr," _
                        & "        indirizzi.civico," _
                        & "        indirizzi.cap," _
                        & "        indirizzi.localita," _
                        & "        COMUNI_NAZIONI.SIGLA," _
                        & "        tab_filiali.*" _
                        & "   FROM COMUNI_NAZIONI, siscom_mi.indirizzi, siscom_mi.tab_filiali" _
                        & "  WHERE     indirizzi.ID = tab_filiali.id_indirizzo" _
                        & "        AND UPPER (responsabile) = 'DAVIDE FULGINI'" _
                        & "        AND data_nascita_resp IS NOT NULL" _
                        & "        AND COMUNI_NAZIONI.NOME(+) = UPPER (TAB_FILIALI.LUOGO_NASCITA_RESP)"
            Else
                par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr, indirizzi.civico,indirizzi.cap, indirizzi.localita,COMUNI_NAZIONI.SIGLA FROM COMUNI_NAZIONI,siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.FILIALI_UI WHERE unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto =" & idContra & " AND UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID AND indirizzi.ID = tab_filiali.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND INIZIO_VALIDITA <='" & dataPresent & "' AND FINE_VALIDITA >= '" & dataPresent & "' AND COMUNI_NAZIONI.NOME (+)=UPPER(TAB_FILIALI.LUOGO_NASCITA_RESP)"
            End If

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                conten = Replace(conten, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
                conten = Replace(conten, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                conten = Replace(conten, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
                conten = Replace(conten, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))

                Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
                Acronimo = par.IfNull(myReader("ACRONIMO"), "")
                conten = Replace(conten, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                conten = Replace(conten, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))

                conten = Replace(conten, "$responsabile$", Responsabile)

                conten = Replace(conten, "$acronimo$", Acronimo)
                conten = Replace(conten, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))
                conten = Replace(conten, "$centrodicosto$", "GL0000/" & Acronimo & "/" & Request.QueryString("PROT"))
                If par.IfNull(myReader("firma"), "") <> "" Then
                    conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../../../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader("firma"), "") & "' />")
                Else
                    conten = Replace(conten, "$firmaresponsabile$", "")
                End If

                conten = Replace(conten, "$nascita$", par.IfNull(myReader("DATA_NASCITA_RESP"), ""))
                conten = Replace(conten, "$comunecedente$", par.IfNull(myReader("LUOGO_NASCITA_RESP"), ""))
                conten = Replace(conten, "$provinciacedente$", par.IfNull(myReader("SIGLA"), ""))

            Else
                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita,COMUNI_NAZIONI.SIGLA from COMUNI_NAZIONI,siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContra & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale AND COMUNI_NAZIONI.NOME (+)=UPPER(TAB_FILIALI.LUOGO_NASCITA_RESP)"""
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
                        conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../../../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader2("firma"), "") & "' />")
                    Else
                        conten = Replace(conten, "$firmaresponsabile$", "")
                    End If
                    conten = Replace(conten, "$nascita$", par.IfNull(myReader2("DATA_NASCITA_RESP"), ""))
                    conten = Replace(conten, "$comunecedente$", par.IfNull(myReader2("LUOGO_NASCITA_RESP"), ""))
                    conten = Replace(conten, "$provinciacedente$", par.IfNull(myReader2("SIGLA"), ""))
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
                    conten = Replace(conten, "$nascita$", "")
                    conten = Replace(conten, "$comunecedente$", "")
                    conten = Replace(conten, "$provinciacedente$", "")

                End If
                myReader2.Close()
            End If
            myReader.Close()

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
        Response.Write(Request.QueryString("cod"))

        Dim NomeFile As String

        Dim COGNOME As String = ""
        Dim NOME As String = ""
        Dim DATA_NASCITA As String = ""
        Dim COMUNE_NASCITA As String = ""
        Dim PROVINCIA_NASCITA As String = ""
        Dim CITTADINANZA As String = ""
        Dim RESIDENZA As String = ""
        Dim INDIRIZZO As String = ""
        Dim TELEFONO As String = ""
        Dim DOCUMENTO As String = ""
        Dim NUMERO_DOCUMENTO As String = ""
        Dim DATA_DOCUMENTO As String = ""
        Dim AUTORITA As String = ""
        Dim UFFICIO As String = ""

        Dim TIPOLOGIA As String = ""
        Dim ID_UNITA As String = ""
        Dim MODERATO As String = ""
        Dim IndiceContratto As String = "-1"
        Dim talloggio As String = ""



        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\DenunciaCessione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            'Dim contenuto As String = sr1.ReadToEnd()
            'sr1.Close()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\comunic.Questura2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.id,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,UNITA_CONTRATTUALE.ID_UNITA,unita_contrattuale.tipologia as talloggio FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND  UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO='" & Request.QueryString("cod") & "'"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                TIPOLOGIA = par.IfNull(myReaderA("COD_TIPOLOGIA_CONTR_LOC"), "")
                talloggio = par.IfNull(myReaderA("talloggio"), "")
                ID_UNITA = par.IfNull(myReaderA("ID_UNITA"), "-1")
                IndiceContratto = par.IfNull(myReaderA("ID"), "-1")
            End If
            myReaderA.Close()

            If ID_UNITA = "" Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("Non è possibile stampare questo modulo.")
                Exit Sub
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_UI WHERE ID=" & ID_UNITA & " ORDER BY DATA DESC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                MODERATO = "1"
            End If
            myReaderA.Close()


            If Mid(TIPOLOGIA, 1, 3) = "ERP" And Request.QueryString("L") = "D" Then
                TIPOLOGIA = "L431/98"
            End If


            If Mid(TIPOLOGIA, 1, 3) = "ERP" And MODERATO = "1" Then
                TIPOLOGIA = "L431/98"
            End If

            contenuto = caricaRespFiliale(IndiceContratto, contenuto)




            Dim miocod As String = ""

            par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME,UNITA_CONTRATTUALE.*,RAPPORTI_UTENZA.DEST_USO FROM SISCOM_MI.TIPO_LIVELLO_PIANO,COMUNI_NAZIONI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND RAPPORTI_UTENZA.COD_CONTRATTO='" & Request.QueryString("COD") & "' AND  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                miocod = par.IfNull(myReaderA("cod_unita_immobiliare"), "")
                contenuto = Replace(contenuto, "$comunealloggio$", par.IfNull(myReaderA("NOME"), ""))
                contenuto = Replace(contenuto, "$provincia$", par.IfNull(myReaderA("SIGLA"), ""))
                contenuto = Replace(contenuto, "$indirizzoalloggio$", par.IfNull(myReaderA("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReaderA("CIVICO"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderA("CAP"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderA("PIANO"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderA("SCALA"), ""))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderA("INTERNO"), ""))

                contenuto = Replace(contenuto, "$ingressi$", "")
                Select Case par.IfNull(myReaderA("dest_USO"), "0")
                    Case "R"
                        contenuto = Replace(contenuto, "$usoimmobile$", "ABITAZIONE")
                    Case "N"
                        contenuto = Replace(contenuto, "$usoimmobile$", "NEGOZIO COMMERCIALE")
                    Case "B"
                        contenuto = Replace(contenuto, "$usoimmobile$", "POSTO AUTO")
                    Case "L"
                        contenuto = Replace(contenuto, "$usoimmobile$", "LABORATORIO")
                    Case "0"
                        contenuto = Replace(contenuto, "$usoimmobile$", "________________")
                    Case "C"
                        contenuto = Replace(contenuto, "$usoimmobile$", "RESIDENZIALE-COOPERATIVE")
                    Case Else
                        contenuto = Replace(contenuto, "$usoimmobile$", "________________")
                End Select

            Else
                contenuto = Replace(contenuto, "$comunealloggio$", "")
                contenuto = Replace(contenuto, "$provincia$", "")
                contenuto = Replace(contenuto, "$indirizzoalloggio$", "")
                contenuto = Replace(contenuto, "$civico$", "")
                contenuto = Replace(contenuto, "$cap$", "")
                contenuto = Replace(contenuto, "$piano$", "")
                contenuto = Replace(contenuto, "$scala$", "")
                contenuto = Replace(contenuto, "$interno$", "")
                contenuto = Replace(contenuto, "$ingressi$", "")
                contenuto = Replace(contenuto, "$usoimmobile$", "")
                '0200014030500F06001
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.ui_usi_diversi where cod_alloggio='" & miocod & "'"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.HasRows = True Then
                If myReaderA.Read Then
                    contenuto = Replace(contenuto, "$vani$", par.IfNull(myReaderA("num_locali"), ""))
                    contenuto = Replace(contenuto, "$accessori$", par.IfNull(myReaderA("num_servizi"), ""))
                End If
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT * from alloggi where cod_alloggio='" & miocod & "'"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                contenuto = Replace(contenuto, "$vani$", par.IfNull(myReaderA("num_locali"), ""))
                contenuto = Replace(contenuto, "$accessori$", par.IfNull(myReaderA("num_servizi"), ""))
            End If
            myReaderA.Close()

            contenuto = Replace(contenuto, "$vani$", "")
            contenuto = Replace(contenuto, "$accessori$", "")

            par.cmd.CommandText = "select anagrafica.doc_soggiorno,RAPPORTI_UTENZA.ID,ANAGRAFICA.DATA_DOC,ANAGRAFICA.RILASCIO_DOC,ANAGRAFICA.NUM_DOC,ANAGRAFICA.TIPO_DOC,ANAGRAFICA.TELEFONO,ANAGRAFICA.RESIDENZA,ANAGRAFICA.INDIRIZZO_RESIDENZA,ANAGRAFICA.CIVICO_RESIDENZA,ANAGRAFICA.CAP_RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA,ANAGRAFICA.PROVINCIA_RESIDENZA,COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME AS ""COMUNE_NASCITA"",ANAGRAFICA.DATA_NASCITA,anagrafica.cognome,anagrafica.nome,RAPPORTI_UTENZA.ID_COMMISSARIATO from COMUNI_NAZIONI,SISCOM_MI.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza where ANAGRAFICA.COD_COMUNE_NASCITA=COMUNI_NAZIONI.COD (+) AND rapporti_utenza.cod_contratto='" & Request.QueryString("COD") & "' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                COGNOME = par.IfNull(myReader("COGNOME"), "")
                NOME = par.IfNull(myReader("NOME"), "")
                DATA_NASCITA = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))

                If par.IfNull(myReader("SIGLA"), "") <> "E" Then
                    COMUNE_NASCITA = par.IfNull(myReader("COMUNE_NASCITA"), "")
                    PROVINCIA_NASCITA = par.IfNull(myReader("SIGLA"), "")
                    CITTADINANZA = "ITALIANA"
                Else
                    PROVINCIA_NASCITA = par.IfNull(myReader("COMUNE_NASCITA"), "")
                    COMUNE_NASCITA = ""
                    CITTADINANZA = PROVINCIA_NASCITA

                End If
                'If InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) > 0 Then
                '    RESIDENZA = Mid(par.IfNull(myReader("RESIDENZA"), ""), 1, InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) - 1)
                '    INDIRIZZO = Mid(par.IfNull(myReader("RESIDENZA"), ""), InStr(par.IfNull(myReader("RESIDENZA"), ""), "CAP", CompareMethod.Text) + 9, Len(par.IfNull(myReader("RESIDENZA"), "")))
                'Else
                '    RESIDENZA = ""
                '    INDIRIZZO = par.IfNull(myReader("RESIDENZA"), "")
                'End If

                RESIDENZA = par.IfNull(myReader("COMUNE_RESIDENZA"), "") & "(" & par.IfNull(myReader("PROVINCIA_RESIDENZA"), "") & ") CAP " & par.IfNull(myReader("CAP_RESIDENZA"), "") & " " & par.IfNull(myReader("INDIRIZZO_RESIDENZA"), "") & " " & par.IfNull(myReader("CIVICO_RESIDENZA"), "")
                INDIRIZZO = RESIDENZA

                TELEFONO = par.IfNull(myReader("TELEFONO"), "")

                If par.IfNull(myReader("TIPO_DOC"), "0") = "0" Then
                    DOCUMENTO = "CARTA D'IDENTITA'"
                End If
                If par.IfNull(myReader("TIPO_DOC"), "0") = "1" Then
                    DOCUMENTO = "PASSAPORTO"
                End If

                NUMERO_DOCUMENTO = par.IfNull(myReader("NUM_DOC"), "")
                DATA_DOCUMENTO = par.FormattaData(par.IfNull(myReader("DATA_DOC"), ""))
                AUTORITA = par.IfNull(myReader("RILASCIO_DOC"), "")

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID=" & par.IfNull(myReader("ID_COMMISSARIATO"), -1)
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    contenuto = Replace(contenuto, "$commissariato$", par.IfNull(myReaderA("DESCRIZIONE"), ""))
                Else
                    contenuto = Replace(contenuto, "$commissariato$", "")
                End If
                myReaderA.Close()

                If NUMERO_DOCUMENTO = "" Then
                    Response.Write("<script>alert('Attenzione, mancano i dati relativi al documento di riconoscimento! Aprire l\'anagrafica e verificare!');</script>")
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F11','DATI INCOMPLETI')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F11','')"
                    par.cmd.ExecuteNonQuery()
                End If

                contenuto = Replace(contenuto, "$soggiorno$", par.IfNull(myReader("doc_soggiorno"), ""))


            End If
            myReader.Close()




            NomeFile = Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto



            contenuto = Replace(contenuto, "$cognome$", COGNOME)
            contenuto = Replace(contenuto, "$nome$", NOME)
            contenuto = Replace(contenuto, "$nascitaconduttore$", DATA_NASCITA)
            contenuto = Replace(contenuto, "$comuneconduttore$", COMUNE_NASCITA)
            contenuto = Replace(contenuto, "$provinciaconduttore$", PROVINCIA_NASCITA)
            contenuto = Replace(contenuto, "$cittadinanza$", CITTADINANZA)
            contenuto = Replace(contenuto, "$residenza$", RESIDENZA) ' & ") - " & INDIRIZZO)
            contenuto = Replace(contenuto, "$indirizzo$", INDIRIZZO)
            contenuto = Replace(contenuto, "$telefono$", TELEFONO)
            contenuto = Replace(contenuto, "$tipodocumento$", DOCUMENTO)
            If DOCUMENTO = "CARTA D'IDENTITA'" Then
                contenuto = Replace(contenuto, "$numerodocumento$", NUMERO_DOCUMENTO)
                contenuto = Replace(contenuto, "$datarilascio$", DATA_DOCUMENTO)
                contenuto = Replace(contenuto, "$numerodocumento2$", "")
                contenuto = Replace(contenuto, "$datarilascio2$", "")
            Else
                contenuto = Replace(contenuto, "$numerodocumento$", NUMERO_DOCUMENTO)
                contenuto = Replace(contenuto, "$datarilascio$", DATA_DOCUMENTO)
                contenuto = Replace(contenuto, "$numerodocumento2$", NUMERO_DOCUMENTO)
                contenuto = Replace(contenuto, "$datarilascio2$", DATA_DOCUMENTO)
            End If
            contenuto = Replace(contenuto, "$autorita$", AUTORITA)



            'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\Cessione_") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            'Dim url As String = NomeFile
            'Dim pdfConverter As PdfConverter = New PdfConverter
            ' ''pdfConverter.LicenseKey = "P38cBx6AWW7b9c81TjEGxnrazP+J7rOjs+9omJ3TUycauK+cL WdrITM5T59hdW5r"
            'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            'pdfConverter.PdfDocumentOptions.ShowHeader = False
            'pdfConverter.PdfDocumentOptions.ShowFooter = False
            'pdfConverter.PdfDocumentOptions.LeftMargin = 5
            'pdfConverter.PdfDocumentOptions.RightMargin = 5
            'pdfConverter.PdfDocumentOptions.TopMargin = 5
            'pdfConverter.PdfDocumentOptions.BottomMargin = 5
            'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            'pdfConverter.PdfDocumentOptions.ShowHeader = False
            'pdfConverter.PdfFooterOptions.FooterText = ("")
            'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            'pdfConverter.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter.PdfFooterOptions.PageNumberText = ""
            'pdfConverter.PdfFooterOptions.ShowPageNumber = False


            'pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\StampeLettere\") & NomeFile & ".htm", Server.MapPath("..\StampeLettere\") & NomeFile & ".pdf")
            'System.IO.File.Delete(Server.MapPath("..\StampeLettere\") & NomeFile & ".htm")

            'Response.Redirect("..\StampeLettere\" & NomeFile & ".pdf")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Redirect("..\..\ALLEGATI\CONTRATTI\StampeLettere\Cessione_" & NomeFile & ".htm")







        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Denuncia Cessione Fabbricato" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
