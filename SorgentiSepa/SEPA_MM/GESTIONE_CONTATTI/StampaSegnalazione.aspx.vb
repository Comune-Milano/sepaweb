Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class CALL_CENTER_StampaSegnalazione
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModelloStampaSegnalazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()
                Dim NomeFile1 As String = "Segnalazione_" & Request.QueryString("ID") & "_" & Format(Now, "yyyyMMddHHmmss")
                'par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.SEGNALAZIONI.ID),SISCOM_MI.SEGNALAZIONI.*,SISCOM_MI.SEGNALAZIONI_NOTE.SOLLECITO,SISCOM_MI.tab_stati_segnalazioni.descrizione as statos " _
                '    & "FROM SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TAB_STATI_SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI,SISCOM_MI.SEGNALAZIONI_NOTE WHERE SISCOM_MI.SEGNALAZIONI_NOTE.ID_SEGNALAZIONE (+) = SISCOM_MI.SEGNALAZIONI.ID AND " _
                '    & "SISCOM_MI.SEGNALAZIONI.ID_STATO=SISCOM_MI.TAB_STATI_SEGNALAZIONI.ID AND SISCOM_MI.SEGNALAZIONI.ID=" & Request.QueryString("ID")
                par.cmd.CommandText = "SELECT SISCOM_MI.SEGNALAZIONI.*,SISCOM_MI.tab_stati_segnalazioni.descrizione as statos,(SELECT MAX (SOLLECITO) " _
                    & " FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE SEGNALAZIONI_NOTE.ID_SEGNALAZIONE=SEGNALAZIONI.ID) AS SOLLECITO, " _
                    & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE where tipo_segnalazione.id=id_tipo_segnalazione) as tipo1, " _
                    & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE_livello_2 where tipo_segnalazione_livello_2.id=id_tipo_segn_livello_2) as tipo2, " _
                    & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE_livello_3 where tipo_segnalazione_livello_3.id=id_tipo_segn_livello_3) as tipo3, " _
                    & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE_livello_4 where tipo_segnalazione_livello_4.id=id_tipo_segn_livello_4) as tipo4 " _
                    & " FROM SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TAB_STATI_SEGNALAZIONI " _
                    & " WHERE SISCOM_MI.SEGNALAZIONI.ID_STATO=SISCOM_MI.TAB_STATI_SEGNALAZIONI.ID AND SISCOM_MI.SEGNALAZIONI.ID=" & Request.QueryString("ID")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    contenuto = Replace(contenuto, "$datastampa$", Format(Now, "dd/MM/yyyy"))
                    contenuto = Replace(contenuto, "$stato$", par.IfNull(myReader1("STATOS"), ""))
                    contenuto = Replace(contenuto, "$datainserimento$", par.FormattaData(Mid(par.IfNull(myReader1("data_ora_richiesta"), ""), 1, 8)))
                    Dim tipologie As String = ""
                    If par.IfNull(myReader1("tipo1"), "") <> "" Then
                        tipologie &= par.IfNull(myReader1("tipo1"), "")
                    End If
                    If par.IfNull(myReader1("tipo2"), "") <> "" Then
                        tipologie &= " - " & par.IfNull(myReader1("tipo2"), "")
                    End If
                    If par.IfNull(myReader1("tipo3"), "") <> "" Then
                        tipologie &= " - " & par.IfNull(myReader1("tipo3"), "")
                    End If
                    If par.IfNull(myReader1("tipo4"), "") <> "" Then
                        tipologie &= " - " & par.IfNull(myReader1("tipo4"), "")
                    End If
                    contenuto = Replace(contenuto, "$tipologiarichiesta$", tipologie)
                    'Select Case par.IfNull(myReader1("tipo_richiesta"), "")
                    '    Case "0"
                    '        contenuto = Replace(contenuto, "$tipologiarichiesta$", "RICHIESTA INFORMAZIONI")
                    '    Case "1"
                    '        contenuto = Replace(contenuto, "$tipologiarichiesta$", "SEGNALAZIONE GUASTI")
                    '        'Case "2"
                    '        '    contenuto = Replace(contenuto, "$tipologiarichiesta$", "RECLAMO")
                    '        'Case "3"
                    '        '    contenuto = Replace(contenuto, "$tipologiarichiesta$", "PROPOSTE")
                    '        'Case "4"
                    '        '    contenuto = Replace(contenuto, "$tipologiarichiesta$", "VARIE")
                    'End Select
                    contenuto = Replace(contenuto, "$numeroSegnalazione$", par.IfNull(myReader1("id"), ""))
                    contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReader1("cognome_rs"), ""))
                    contenuto = Replace(contenuto, "$nome$", par.IfNull(myReader1("nome"), ""))
                    contenuto = Replace(contenuto, "$telefono1$", par.IfNull(myReader1("telefono1"), ""))
                    contenuto = Replace(contenuto, "$telefono2$", par.IfNull(myReader1("telefono2"), ""))
                    contenuto = Replace(contenuto, "$mail$", par.IfNull(myReader1("mail"), ""))
                    contenuto = Replace(contenuto, "$richiesta$", par.IfNull(myReader1("descrizione_ric"), ""))
                    par.cmd.CommandText = "select (case when length(data_ora)>=12 then to_char(to_Date(substr(data_ora,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||substr(data_ora,9,2)||':'||substr(data_ora,11,2) else null end )  as data,note from siscom_mi.segnalazioni_note where id_segnalazione=" & Request.QueryString("ID") & " order by 1"
                    Dim note As String = ""
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    While lettore.Read
                        note &= par.IfNull(lettore("data"), "") & " - " & par.EliminaLink(par.IfNull(lettore("note"), "")).Replace("Clicca per Visualizzare", "") & "<br />"
                    End While
                    lettore.Close()
                    'contenuto = Replace(contenuto, "$note$", par.IfNull(myReader1("note"), ""))
                    contenuto = Replace(contenuto, "$note$", note)
                    par.cmd.CommandText = "select siscom_mi.tipologie_guasti.descrizione as guasto from siscom_mi.tipologie_guasti where siscom_mi.tipologie_guasti.id in (select siscom_mi.segnalazioni.id_tipologie from " _
                        & "siscom_mi.tipologie_guasti,siscom_mi.segnalazioni where siscom_mi.tipologie_guasti.id = siscom_mi.segnalazioni.id_tipologie and siscom_mi.segnalazioni.id =" & Request.QueryString("ID") & ")"
                    Dim myRead As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myRead.Read Then
                        contenuto = Replace(contenuto, "$tipointervento$", par.IfNull(myRead("guasto"), ""))
                    Else
                        contenuto = Replace(contenuto, "$tipointervento$", "---")
                    End If
                    Dim sollecito As String = ""
                    If par.IfNull(myReader1("SOLLECITO"), 0) = 1 Then
                        par.cmd.CommandText = "SELECT max(rownum) as SOLL from SISCOM_MI.SEGNALAZIONI_NOTE WHERE SISCOM_MI.SEGNALAZIONI_NOTE.SOLLECITO = 1 AND SISCOM_MI.SEGNALAZIONI_NOTE.ID_SEGNALAZIONE=" & Request.QueryString("ID")
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader.Read Then
                            sollecito = "num. " & par.IfNull(myReader("SOLL"), "")
                        End If
                    Else
                        sollecito = "NO"
                    End If
                    contenuto = Replace(contenuto, "$sollecito$", sollecito)
                    If par.IfNull(myReader1("passata_a"), -1) <> -1 And par.IfNull(myReader1("tipo_richiesta"), 0) <> 0 Then
                        '$passata$
                        par.cmd.CommandText = "select tab_filiali.nome as filiale,operatori.cognome,operatori.nome from siscom_mi.tab_filiali,operatori where operatori.id=" & par.IfNull(myReader1("passata_a"), "-1") & " and tab_filiali.id=operatori.id_ufficio (+)"
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader3.Read Then

                            contenuto = Replace(contenuto, "$passata$", "Passata a operatore " & par.IfNull(myReader3("cognome"), "") & " " & par.IfNull(myReader3("nome"), "") & " filiale " & par.IfNull(myReader3("filiale"), "") & "<br>")
                            myReader3.Close()
                        End If
                        myReader3.Close()
                    Else
                        contenuto = Replace(contenuto, "$passata$", "<br>")
                    End If
                    If par.IfNull(myReader1("id_complesso"), -1) <> -1 Then
                        par.cmd.CommandText = "select 'Complesso '||cod_complesso||' - '||denominazione as titolo from siscom_mi.complessi_immobiliari where id=" & par.IfNull(myReader1("id_complesso"), "")
                    End If
                    If par.IfNull(myReader1("id_edificio"), -1) <> -1 Then
                        par.cmd.CommandText = "select 'Edificio '||cod_edificio||' - '||denominazione as titolo from siscom_mi.edifici where id=" & par.IfNull(myReader1("id_edificio"), "")
                    End If

                    If par.IfNull(myReader1("id_unita"), -1) <> -1 Then
                        par.cmd.CommandText = "select id,cod_unita_immobiliare as codice,'Unità Cod.'||cod_unita_immobiliare as titolo from siscom_mi.unita_immobiliari where id=" & par.IfNull(myReader1("id_unita"), "")
                    End If
                    Dim riferitaa As String = ""
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        If par.IfNull(myReader1("id_unita"), -1) <> -1 Then
                            riferitaa = par.IfNull(myReader2("titolo"), "")

                            par.cmd.CommandText = "select rapporti_utenza.id as idc,ANAGRAFICA.*,INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||INDIRIZZI.LOCALITA AS INDIRIZZO " _
                                & " from siscom_mi.unita_contrattuale,siscom_mi.soggetti_contrattuali," _
                                & " SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI " _
                                & " where RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                & " AND siscom_mi.getstatocontratto(rapporti_utenza.id)<>'CHIUSO' " _
                                & " AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                                & " AND soggetti_contrattuali.cod_tipologia_occupante='INTE' " _
                                & " AND soggetti_contrattuali.id_contratto=unita_contrattuale.id_contratto " _
                                & " and unita_contrattuale.id_unita=" & par.IfNull(myReader2("ID"), "0") _
                                & " AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA " _
                                & " AND INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO "
                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            Do While myReader3.Read
                                If par.IfNull(myReader3("RAGIONE_SOCIALE"), "") <> "" Then
                                    riferitaa = riferitaa & " - " & UCase(par.IfNull(myReader3("INDIRIZZO"), "")) & " - INTESTATARIO: " & UCase(par.IfNull(myReader3("RAGIONE_SOCIALE"), ""))
                                Else
                                    riferitaa = riferitaa & " - " & UCase(par.IfNull(myReader3("INDIRIZZO"), "")) & " - INTESTATARIO: " & UCase(par.IfNull(myReader3("COGNOME"), "")) & " " & UCase(par.IfNull(myReader3("NOME"), ""))
                                End If
                            Loop
                            myReader3.Close()
                        Else
                            riferitaa = par.IfNull(myReader2("titolo"), "")
                        End If
                    End If
                    myReader2.Close()
                    contenuto = Replace(contenuto, "$riferitaa$", riferitaa)
                End If
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\Contratti\Varie\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
                'sr.WriteLine(contenuto)
                'sr.Close()
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
                pdfConverter1.PdfDocumentOptions.LeftMargin = 40
                pdfConverter1.PdfDocumentOptions.RightMargin = 40
                pdfConverter1.PdfDocumentOptions.TopMargin = 30
                pdfConverter1.PdfDocumentOptions.BottomMargin = 30
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfFooterOptions.FooterText = ("")
                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
                pdfConverter1.SavePdfFromHtmlStringToFile(contenuto, Server.MapPath("..\FileTemp\") & NomeFile1 & ".pdf")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Dim i As Integer = 0
                For i = 0 To 10000
                Next
                Response.Write("<script>window.open('../FileTemp/" & NomeFile1 & ".pdf','stampa','');window.close();</script>")
            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
End Class
