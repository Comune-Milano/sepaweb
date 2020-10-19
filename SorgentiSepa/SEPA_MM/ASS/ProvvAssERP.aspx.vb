Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_ProvvAssERP
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim CFPIVA As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            CFPIVA = Request.QueryString("CF")
            sTAMPA()
        End If

    End Sub

    Private Function sTAMPA()
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



        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ProvvAssERP.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))
            If Request.QueryString("OF") <> "0" Then
                contenuto = Replace(contenuto, "$offerta$", "Offerta alloggio n." & Request.QueryString("OF"))
            Else
                contenuto = Replace(contenuto, "$offerta$", "")
            End If

            If Request.QueryString("ID") <> "-1" Then
                If Request.QueryString("ID") > 8000000 Then
                    par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando_VSA.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE,DOMANDE_BANDO_VSA WHERE  TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and alloggi.cod_alloggio=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND UNITA_IMMOBILIARI.ID=UNITA_ASSEGNATE.ID_UNITA AND DOMANDE_BANDO_VSA.ID=UNITA_ASSEGNATE.ID_DOMANDA AND UNITA_ASSEGNATE.ID_DOMANDA=" & Request.QueryString("ID")
                Else
                    If Request.QueryString("ID") < 500000 Then
                        'par.cmd.CommandText = "SELECT t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando.*,comp_nucleo.cognome,comp_nucleo.nome FROM t_tipo_via,alloggi,domande_bando,comp_nucleo WHERE t_tipo_via.cod=alloggi.tipo_indirizzo and alloggi.cod_alloggio=domande_bando.num_alloggio and comp_nucleo.id_dichiarazione=domande_bando.id_dichiarazione and comp_nucleo.progr=domande_bando.progr_componente and domande_bando.ID=" & Request.QueryString("ID")
                        par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE,DOMANDE_BANDO WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and alloggi.cod_alloggio=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND UNITA_IMMOBILIARI.ID=UNITA_ASSEGNATE.ID_UNITA AND DOMANDE_BANDO.ID=UNITA_ASSEGNATE.ID_DOMANDA AND UNITA_ASSEGNATE.ID_DOMANDA=" & Request.QueryString("ID")
                    Else
                        'par.cmd.CommandText = "SELECT t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando_cambi.*,comp_nucleo_cambi.cognome,comp_nucleo_cambi.nome FROM t_tipo_via,alloggi,domande_bando_cambi,comp_nucleo_cambi WHERE t_tipo_via.cod=alloggi.tipo_indirizzo and alloggi.cod_alloggio=domande_bando_cambi.num_alloggio and comp_nucleo_cambi.id_dichiarazione=domande_bando_cambi.id_dichiarazione and comp_nucleo_cambi.progr=domande_bando_cambi.progr_componente and domande_bando_cambi.ID=" & Request.QueryString("ID")
                        par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando_CAMBI.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE,DOMANDE_BANDO_CAMBI WHERE  TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and alloggi.cod_alloggio=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND UNITA_IMMOBILIARI.ID=UNITA_ASSEGNATE.ID_UNITA AND DOMANDE_BANDO_CAMBI.ID=UNITA_ASSEGNATE.ID_DOMANDA AND UNITA_ASSEGNATE.ID_DOMANDA=" & Request.QueryString("ID")
                    End If
                End If

                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    contenuto = Replace(contenuto, "$protocollo$", "rif. Domada Pg. " & par.IfNull(myReaderA("pg"), ""))
                    contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReaderA("cognome_RS"), "") & " " & par.IfNull(myReaderA("nome"), ""))
                    contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderA("interno"), ""))
                    contenuto = Replace(contenuto, "$comune$", par.IfNull(myReaderA("comunealloggio"), ""))
                    contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderA("DPIANO"), ""))
                    contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderA("scala"), ""))
                    contenuto = Replace(contenuto, "$locali$", par.IfNull(myReaderA("num_locali"), ""))
                    contenuto = Replace(contenuto, "$superficie$", par.IfNull(myReaderA("sup"), ""))
                    contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderA("tipovia"), "") & " " & par.IfNull(myReaderA("indirizzo"), "") & ", " & par.IfNull(myReaderA("NUM_CIVICO"), ""))

                    If Request.QueryString("ID") > 8000000 Then
                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & par.IfNull(myReaderA("ID"), "-1") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                            & "','F176','','I')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        If Request.QueryString("ID") < 500000 Then
                            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & par.IfNull(myReaderA("ID"), "-1") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F176','','I')"
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & par.IfNull(myReaderA("ID"), "-1") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F176','','I')"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                Else
                    If Request.QueryString("ID") > 8000000 Then
                        par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando_VSA.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE,DOMANDE_BANDO_VSA WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and ALLOGGI.ID=UNITA_ASSEGNATE.ID_UNITA AND DOMANDE_BANDO_VSA.ID=UNITA_ASSEGNATE.ID_DOMANDA AND UNITA_ASSEGNATE.ID_DOMANDA=" & Request.QueryString("ID")
                    Else

                        If Request.QueryString("ID") < 500000 Then
                            par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE,DOMANDE_BANDO WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and ALLOGGI.ID=UNITA_ASSEGNATE.ID_UNITA AND DOMANDE_BANDO.ID=UNITA_ASSEGNATE.ID_DOMANDA AND UNITA_ASSEGNATE.ID_DOMANDA=" & Request.QueryString("ID")
                        Else
                            par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",domande_bando_CAMBI.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE,DOMANDE_BANDO_CAMBI WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and ALLOGGI.ID=UNITA_ASSEGNATE.ID_UNITA AND DOMANDE_BANDO_CAMBI.ID=UNITA_ASSEGNATE.ID_DOMANDA AND UNITA_ASSEGNATE.ID_DOMANDA=" & Request.QueryString("ID")

                        End If
                    End If
                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderB.Read Then

                        contenuto = Replace(contenuto, "$protocollo$", par.IfNull(myReaderB("pg"), ""))
                        contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReaderB("cognome_RS"), "") & " " & par.IfNull(myReaderB("nome"), ""))
                        contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderB("interno"), ""))
                        contenuto = Replace(contenuto, "$comune$", par.IfNull(myReaderB("comunealloggio"), ""))
                        contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderB("DPIANO"), ""))
                        contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderB("scala"), ""))
                        contenuto = Replace(contenuto, "$locali$", par.IfNull(myReaderB("num_locali"), ""))
                        contenuto = Replace(contenuto, "$superficie$", par.IfNull(myReaderB("sup"), ""))
                        contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderB("tipovia"), "") & " " & par.IfNull(myReaderB("indirizzo"), "") & ", " & par.IfNull(myReaderB("NUM_CIVICO"), ""))

                        If Request.QueryString("ID") > 8000000 Then
                            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & par.IfNull(myReaderB("ID"), "-1") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F176','','I')"
                            par.cmd.ExecuteNonQuery()
                        Else
                            If Request.QueryString("ID") < 500000 Then
                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & par.IfNull(myReaderB("ID"), "-1") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F176','','I')"
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & par.IfNull(myReaderB("ID"), "-1") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F176','','I')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If
                    End If
                    myReaderB.Close()
                End If
                myReaderA.Close()

            Else
                par.cmd.CommandText = "SELECT UNITA_ASSEGNATE.COGNOME_RS,UNITA_ASSEGNATE.NOME,t_tipo_via.descrizione as ""tipovia"",alloggi.num_civico,alloggi.indirizzo,alloggi.sup,alloggi.num_locali,alloggi.scala,alloggi.piano,ALLOGGI.NUM_ALLOGGIO AS ""INTERNO"",alloggi.comune as ""comunealloggio"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""DPIANO"" FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,t_tipo_via,alloggi,SISCOM_MI.UNITA_ASSEGNATE WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND t_tipo_via.cod=alloggi.tipo_indirizzo and alloggi.cod_alloggio=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND UNITA_IMMOBILIARI.ID=UNITA_ASSEGNATE.ID_UNITA AND UNITA_ASSEGNATE.CF_PIVA='" & CFPIVA & "'"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    contenuto = Replace(contenuto, "$protocollo$", "Offerta Diretta")
                    contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReaderA("cognome_RS"), "") & " " & par.IfNull(myReaderA("nome"), ""))
                    contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderA("interno"), ""))
                    contenuto = Replace(contenuto, "$comune$", par.IfNull(myReaderA("comunealloggio"), ""))
                    contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderA("DPIANO"), ""))
                    contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderA("scala"), ""))
                    contenuto = Replace(contenuto, "$locali$", par.IfNull(myReaderA("num_locali"), ""))
                    contenuto = Replace(contenuto, "$superficie$", par.IfNull(myReaderA("sup"), ""))
                    contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderA("tipovia"), "") & " " & par.IfNull(myReaderA("indirizzo"), "") & ", " & par.IfNull(myReaderA("NUM_CIVICO"), ""))
                End If
                End If

                'myReader.Close()




                NomeFile = Format(Now, "yyyyMMddHHmmss")

                'apro e memorizzo il testo base del contratto




                'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("PROVVEDIMENTI\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
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


                'pdfConverter.SavePdfFromUrlToFile(Server.MapPath("PROVVEDIMENTI\") & NomeFile & ".htm", Server.MapPath("PROVVEDIMENTI\") & NomeFile & ".pdf")

                'System.IO.File.Delete(Server.MapPath("PROVVEDIMENTI\") & NomeFile & ".htm")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Redirect("PROVVEDIMENTI\" & NomeFile & ".htm")




        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Provvedimento Assegnazione ERP" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function
End Class
