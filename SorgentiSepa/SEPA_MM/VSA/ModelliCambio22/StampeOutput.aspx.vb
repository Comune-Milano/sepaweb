Imports ExpertPdf.HtmlToPdf
Imports System.IO

Partial Class ASS_Cambi_in_Emergenza_Riempi_Stampe_StampeOutput
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                Select Case Request.QueryString("TIPODOC")
                    '***** Modelli REVISIONE CANONE *****
                    Case "CompNucleo"
                        ComposizioneNucleo()
                    Case "DocMancante"
                        DocMancante()
                    Case "EsNegativo"
                        EsitoNegativo()
                    Case "EsNegatAllAdeg"
                        EsitoNegativoAllAdeguato()
                    Case "EsNegatISEE"
                        EsitoNegativoISEE()
                    Case "EsNegatMoros"
                        EsitoNegativoMorosita()
                    Case "EsNegatRequis"
                        EsitoNegativoRequisiti()
                    Case "EsPositivo"
                        EsitoPositivo()
                    Case "EsPositMoros"
                        EsitoPositivoMorosita()
                    Case "EsNegatRicorso"
                        RicorsoEsitoNegativo()
                    Case "EsPositRicorso"
                        RicorsoEsitoPositivo()

                End Select

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

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
        pdf.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
        pdf.PdfFooterOptions.DrawFooterLine = False
    End Sub

    Private Sub ComposizioneNucleo()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ComposizioneNucleo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            'Dim lettoreIDc As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(9, idDomanda.Value), contenuto)






            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '               & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '               & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '               & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '               & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '               & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '               & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If














            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)



            Dim nomefile As String = "C1_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))




            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub DocMancante()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("DocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))




            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If





            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            Dim NumDoc As Integer = 0
            Dim tbDocManc As String = "<table style='width:100%;'>"
            Dim ndx As Integer = 1
            Dim strTbl As String = ""
            Dim strTbl2 As String = ""

            par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE AS DESCR FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE VSA_DOC_NECESSARI.ID = VSA_DOC_MANCANTI.ID_DOC AND VSA_DOC_MANCANTI.ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ") & " "
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da1.Fill(dt)
            da1.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    NumDoc = NumDoc - 1
                    strTbl = "<tr id='" & ndx & "'><td style='font-size:14pt;font-family :Arial ;'>" & ndx & ".</td><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & Trim(par.Elimina160(par.IfNull(row.Item("DESCR"), ""))) & ";</td></tr>"
                    tbDocManc = tbDocManc & strTbl
                    ndx = ndx + 1
                Next
            End If

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

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(10, idDomanda.Value), contenuto)
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            ' Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)



            Dim nomefile As String = "C2_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))





            ' Response.Redirect("..\..\FileTemp\" & nomefile, False)

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegativo()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            'Dim lettoreIDc As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()


            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & idDomanda.Value & " AND (COD_DECISIONE=3 OR COD_DECISIONE=8) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM T_COND_ESITO_NEGATIVO,VSA_DOM_ESITI_NEG WHERE T_COND_ESITO_NEGATIVO.ID = VSA_DOM_ESITI_NEG.ID_COND_ESITO AND VSA_DOM_ESITI_NEG.ID_DOMANDA=" & idDomanda.Value
                Dim lettoreNeg As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreNeg.Read Then
                    contenuto = Replace(contenuto, "$motivazioni$", par.IfNull(myReader("NOTE"), ""))
                End If
                lettoreNeg.Close()
            Else
                contenuto = Replace(contenuto, "$motiviNEG$", " ")
            End If
            myReader.Close()




            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If







            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(11, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C3_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegativoAllAdeguato()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoNegativoAllAdeguato.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & Dom_Alloggio_ERP1_txtNumContratto & "'"
            'Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()

            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If


            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(12, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C4_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegativoISEE()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoNegativoISEE.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            'Dim lettoreIDc As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()

            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then
            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))
            '    contenuto = Replace(contenuto, "$telfax$", "Tel. " & par.IfNull(myReaderK("n_telefono"), "___________") & " Fax " & par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$testoresponsabile$", " ")
            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$telfax$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            '    contenuto = Replace(contenuto, "$testoresponsabile$", " ")
            'End If


            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(13, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C5_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegativoMorosita()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoNegativoMorosita.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""




            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*, " _
                & "DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*, DOMANDE_BANDO_VSA.CONTRATTO_NUM AS COD_CONTR " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
                codContr = par.IfNull(myReader("COD_CONTR"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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





            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If





            If codContr <> "" Then

                par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
                Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreIDc.Read Then
                    idContratto.Value = lettoreIDc(0)
                End If
                lettoreIDc.Close()




                par.cmd.CommandText = " select sum(importo_totale-importo_pagato) AS MOROSITA from siscom_mi.bol_bollette where id_tipo = 4 and id_Contratto=" & idContratto.Value & " and importo_totale <> NVL(importo_pagato,0)"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then

                    contenuto = Replace(contenuto, "$morosita$", par.IfNull(myReader("MOROSITA"), "_____,__"))
                Else
                    contenuto = Replace(contenuto, "$morosita$", "")
                End If
                myReader.Close()

            Else
                contenuto = Replace(contenuto, "$morosita$", "_____,__")


            End If




            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(14, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C6_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegativoRequisiti()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoNegativoRequisiti.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            'Dim lettoreIDc As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()

            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))


            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If


            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(15, idDomanda.Value), contenuto)
            'contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C7_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoPositivo()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            'Dim lettoreIDc As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()


            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))


            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(16, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C8_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoPositivoMorosita()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("EsitoPositivoMorosita.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""



            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*, " _
                & "DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*, DOMANDE_BANDO_VSA.CONTRATTO_NUM AS COD_CONTR " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
                codContr = par.IfNull(myReader("COD_CONTR"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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


            If codContr <> "" Then


                par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
                Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreIDc.Read Then
                    idContratto.Value = lettoreIDc(0)
                End If
                lettoreIDc.Close()




                par.cmd.CommandText = " select sum(importo_totale-importo_pagato) AS MOROSITA from siscom_mi.bol_bollette where id_tipo = 4 and id_Contratto=" & idContratto.Value & " and importo_totale <> NVL(importo_pagato,0)"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then

                    contenuto = Replace(contenuto, "$morosita$", par.IfNull(myReader("MOROSITA"), ""))

                Else

                    contenuto = Replace(contenuto, "$morosita$", "_____,__")
                End If
                myReader.Close()

            Else

                contenuto = Replace(contenuto, "$morosita$", "_____,__")
            End If

            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(17, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C9_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub RicorsoEsitoNegativo()  'trovare datadocumento e motivazioni
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("RicorsoEsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "")))

                ' contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & idDomanda.Value & " AND (COD_DECISIONE=6 OR COD_DECISIONE=10) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$motivazioni$", par.IfNull(myReader("NOTE"), ""))
            Else
                contenuto = Replace(contenuto, "$motivazioni$", " ")
            End If
            myReader.Close()

            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"

            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If



            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(18, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C10_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub RicorsoEsitoPositivo()  'trovare datadocumento e motivazioni
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("RicorsoEsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$datadomanda$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "")))

                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            'par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            'Dim lettoreIDc As Data.OracleClient.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreIDc.Read Then
            '    idContratto.Value = lettoreIDc(0)
            'End If
            'lettoreIDc.Close()

            'par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, tab_filiali.centro_di_costo, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
            '             & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
            '             & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
            '             & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
            '             & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
            '             & " And tab_filiali.id_indirizzo = indirizzi.ID " _
            '             & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderK.Read Then

            '    contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
            '    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
            '    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
            '    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
            '    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
            '    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
            '    contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))
            '    'contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), " "))
            '    contenuto = Replace(contenuto, "$centrodicosto$", "GL0000/" & par.IfNull(myReaderK("acronimo"), "") & "/" & Request.QueryString("PROT"))



            'Else
            '    contenuto = Replace(contenuto, "$responsabile$", "___________")
            '    contenuto = Replace(contenuto, "$nomefiliale$", "___________")
            '    contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
            '    contenuto = Replace(contenuto, "$capfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$cittafiliale$", "___________")
            '    contenuto = Replace(contenuto, "$telfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$faxfiliale$", "___________")
            '    contenuto = Replace(contenuto, "$referente$", "___________")
            '    contenuto = Replace(contenuto, "$centrodicosto$", " ")
            'End If


            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(19, idDomanda.Value), contenuto)
            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = Replace(contenuto, "$data$", DateTime.Now.ToString("dd/MM/yyyy"))
            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\ALLEGATI\LOCATARI\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = "C11_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\ALLEGATI\LOCATARI\"))

            Response.Redirect("..\..\ALLEGATI\LOCATARI\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub







    '-------------



    Private Sub SollecitoDocMancante()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modelli\SollecitoDocMancante.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "")))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & idDomanda.Value & " AND COD_DECISIONE=3 ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$motiviNEG$", par.IfNull(myReader("NOTE"), ""))
            Else
                contenuto = Replace(contenuto, "$motiviNEG$", " ")
            End If
            myReader.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(5, idDomanda.Value), contenuto)

            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\FileTemp\"))

            Response.Redirect("..\..\FileTemp\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoPositivo1()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modelli\ComEsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(16, idDomanda.Value), contenuto)

            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\FileTemp\"))

            Response.Redirect("..\..\FileTemp\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegativo1()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modelli\ComEsitoNegativo1.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codUI$", Request.QueryString("CODUNITA"))
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$pgdom$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(11, idDomanda.Value), contenuto)

            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\FileTemp\"))

            Response.Redirect("..\..\FileTemp\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegaConOss()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modelli\ComEsitoNegaConOss.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codUI$", Request.QueryString("CODUNITA"))
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$pgdom$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "")))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(18, idDomanda.Value), contenuto)

            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\FileTemp\"))

            Response.Redirect("..\..\FileTemp\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoNegaNoOss()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modelli\ComEsitoNegaNoOss.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codUI$", Request.QueryString("CODUNITA"))
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$pgdom$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "")))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & idDomanda.Value & " AND COD_DECISIONE=3"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$dataEsNeg$", par.FormattaData(par.IfNull(myReader("DATA"), "")))
            End If
            myReader.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(5, idDomanda.Value), contenuto)

            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\FileTemp\"))

            Response.Redirect("..\..\FileTemp\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub EsitoPosConOss()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Modelli\ComEsitoPosConOss.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim siglaRes As String = ""
            Dim codContr As String = ""

            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$codUI$", Request.QueryString("CODUNITA"))
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$pgdom$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReader("DATA_PRESENTAZIONE"), "")))
                idDomanda.Value = par.IfNull(myReader("ID_DOM"), "")
                contenuto = Replace(contenuto, "$dataOss$", par.FormattaData(par.IfNull(myReader("DATA_OSSERVAZIONI"), "")))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & idDomanda.Value
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

            par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
            Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDc.Read Then
                idContratto.Value = lettoreIDc(0)
            End If
            lettoreIDc.Close()

            'contenuto = SostituisciBarcode(Server.MapPath("..\..\FileTemp\") & par.RicavaBarCode(19, idDomanda.Value), contenuto)

            contenuto = caricaRespFiliale(idContratto.Value, contenuto)

            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            nomefile = "C1_" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\FileTemp\"))

            Response.Redirect("..\..\FileTemp\" & nomefile, False)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            System.Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
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

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ")
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader0.Read Then
                dataPresent = par.IfNull(myReader0("DATA_PRESENTAZIONE"), "")
                If idContra = "" Then
                    par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & par.IfNull(myReader0("CONTRATTO_NUM"), "") & "'"
                    Dim myReaderM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReaderM.Read Then
                        idContra = myReaderM("ID")
                    End If
                    myReaderM.Close()
                End If
            End If
            myReader0.Close()

            If dataPresent < "20141201" Then
                dataPresent = "20141201"
            End If


            par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr, indirizzi.civico,indirizzi.cap, indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.FILIALI_UI WHERE unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto =" & idContra & " AND UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID AND indirizzi.ID = tab_filiali.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND INIZIO_VALIDITA <='" & dataPresent & "' AND FINE_VALIDITA >= '" & dataPresent & "'"
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
                conten = Replace(conten, "$telfax$", "Tel. " & par.IfNull(myReader("n_telefono"), "") & " Fax " & par.IfNull(myReader("n_fax"), ""))
                If par.IfNull(myReader("firma"), "") <> "" Then
                    conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader("firma"), "") & "' />")
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
                    conten = Replace(conten, "$telfax$", "Tel. " & par.IfNull(myReader2("n_telefono"), "") & " Fax " & par.IfNull(myReader2("n_fax"), ""))
                    If par.IfNull(myReader2("firma"), "") <> "" Then
                        conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader2("firma"), "") & "' />")
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
                    conten = Replace(conten, "$telfax$", "")
                    conten = Replace(conten, "$firmaresponsabile$", "")

                End If
                myReader2.Close()
                End If
            myReader.Close()

            conten = Replace(conten, "$referente$", Session.Item("NOME_OPERATORE"))
            conten = Replace(conten, "$testoresponsabile$", "")

            conten = Replace(conten, "$firmaResp$", "Il Responsabile della Sede")
            conten = Replace(conten, "$sede$", "MILANO")
            conten = Replace(conten, "$coordinatore$", Responsabile)
            conten = Replace(conten, "$firmaCoord$", "")
            conten = Replace(conten, "$cognCoord$", "")
            conten = Replace(conten, "$nomeCoord$", "")
            conten = Replace(conten, "$dataNascCoord$", "")
            conten = Replace(conten, "$luogoNascCoord$", "")
            conten = Replace(conten, "$provinciaNascCoord$", "")
            conten = Replace(conten, "$indirizzoCondomini$", "Comune di Milano")
            conten = Replace(conten, "$firmaresponsabile$", "")

            ''05/04/2012 FIRME MODELLI ----- Per  le filiali provincia, utilizzare la firma del Coordinatore filiali Provincia, mai il responsabile di filiale
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
            '    conten = Replace(conten, "$indirizzoCondomini$", "Comune di Milano")

            'End If

        Catch ex As Exception
            conten = Replace(conten, "$nomefiliale$", "")
            conten = Replace(conten, "$indirizzofiliale$", "")
            conten = Replace(conten, "$capfiliale$", "")
            conten = Replace(conten, "$cittafiliale$", "")
            conten = Replace(conten, "$telfiliale$", "")
            conten = Replace(conten, "$faxfiliale$", "")
            conten = Replace(conten, "$responsabile$", "")
            conten = Replace(conten, "$acronimo$", "")
            conten = Replace(conten, "$nverde$", "")
            conten = Replace(conten, "$centrodicosto$", "")

            conten = Replace(conten, "$firmaResp$", "Il Responsabile di Filiale")
            conten = Replace(conten, "$sede$", "MILANO")
            conten = Replace(conten, "$coordinatore$", "")
            conten = Replace(conten, "$firmaCoord$", "")
            conten = Replace(conten, "$cognCoord$", "")
            conten = Replace(conten, "$nomeCoord$", "")
            conten = Replace(conten, "$dataNascCoord$", "")
            conten = Replace(conten, "$luogoNascCoord$", "")
            conten = Replace(conten, "$provinciaNascCoord$", "")
            conten = Replace(conten, "$indirizzoCondomini$", "Comune di Milano")
            conten = Replace(conten, "$telfax$", "")
            conten = Replace(conten, "$testoresponsabile$", "")
            conten = Replace(conten, "$referente$", Session.Item("NOME_OPERATORE"))
            conten = Replace(conten, "$firmaresponsabile$", "")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return conten

    End Function



    ' REL_PRAT_ALL_CCAA_ERP dove esito=4

End Class
