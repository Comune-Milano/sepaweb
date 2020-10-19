Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_ChiusuraContratto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Write(Request.QueryString("cod"))
        Try
            Dim FChiusura As String = Chiusura()
            Dim TassaRif As String = TassaRifiuti()

            Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
            pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
            pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait

            Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

            Dim Licenza As String = Session.Item("LicenzaPdfMerge")
            If Licenza <> "" Then
                pdfMerge.LicenseKey = Licenza
            End If

            pdfMerge.AppendPDFFile(FChiusura)
            pdfMerge.AppendPDFFile(TassaRif)


            Dim FileAppend As String = "CX_" & Request.QueryString("cod") & "_" & Format(Now, ("yyyyMMddHHmmss")) & ".pdf"
            pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\" & FileAppend))

            Response.Redirect("..\..\ALLEGATI\CONTRATTI\StampeLettere\" & FileAppend)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Function Chiusura() As String
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
        Dim UFFICIO = ""


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\ChiusuraContratto.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim TIPOLOGIA As String = ""
            Dim ID_UNITA As String = ""
            Dim MODERATO As String = ""

            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS STATO,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO='" & Request.QueryString("cod") & "'"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                TIPOLOGIA = par.IfNull(myReaderA("COD_TIPOLOGIA_CONTR_LOC"), "")
                ID_UNITA = par.IfNull(myReaderA("ID_UNITA"), "-1")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_UI WHERE ID=" & ID_UNITA & " ORDER BY DATA DESC"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                MODERATO = "1"
            End If
            myReaderA.Close()

            If Mid(TIPOLOGIA, 1, 3) = "ERP" And MODERATO = "1" Then
                TIPOLOGIA = "L431/98"
            End If

            Select Case Mid(TIPOLOGIA, 1, 3)
                Case "L43"
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=85"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=86"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=87"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=88"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=89"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=90"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=91"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=92"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=93"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=94"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()
                Case "EQC"
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=105"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=106"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=107"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=108"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=109"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=110"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=111"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=112"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=113"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=114"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                Case "USD"
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=95"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=96"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=97"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=98"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=99"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=100"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=101"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=102"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=103"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=104"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()


                Case Else
                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=67"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$ufficio$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=68"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$cognomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=69"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nomecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=70"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$nascita$", par.FormattaData(par.IfNull(myReaderA("VALORE"), "")))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=71"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comunecedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=72"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$provinciacedente$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=73"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$indirizzoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=74"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$telefonoDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=76"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$contratto$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=75"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        contenuto = Replace(contenuto, "$comuneresidenzaDirettore$", par.IfNull(myReaderA("VALORE"), ""))
                    End If
                    myReaderA.Close()
            End Select


            par.cmd.CommandText = "SELECT rapporti_utenza.data_scadenza_rinnovo,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME,UNITA_CONTRATTUALE.*,RAPPORTI_UTENZA.DEST_USO FROM SISCOM_MI.TIPO_LIVELLO_PIANO,COMUNI_NAZIONI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE TIPO_LIVELLO_PIANO.COD=UNITA_CONTRATTUALE.COD_TIPO_LIVELLO_PIANO AND COMUNI_NAZIONI.COD=UNITA_CONTRATTUALE.COD_COMUNE AND RAPPORTI_UTENZA.COD_CONTRATTO='" & Request.QueryString("COD") & "' AND  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                contenuto = Replace(contenuto, "$comune$", par.IfNull(myReaderA("NOME"), ""))
                contenuto = Replace(contenuto, "$provincia$", par.IfNull(myReaderA("SIGLA"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderA("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReaderA("CIVICO"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderA("CAP"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderA("PIANO"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderA("SCALA"), ""))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReaderA("INTERNO"), ""))
                contenuto = Replace(contenuto, "$datascadenza$", par.FormattaData(par.IfNull(myReaderA("data_scadenza_rinnovo"), "")))

                contenuto = Replace(contenuto, "$vani$", "")
                contenuto = Replace(contenuto, "$accessori$", "")
                contenuto = Replace(contenuto, "$ingressi$", "")
                Select Case par.IfNull(myReaderA("dest_USO"), "0")
                    Case "R"
                        contenuto = Replace(contenuto, "$usoimmobile$", "RESIDENZIALE")
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
                contenuto = Replace(contenuto, "$comune$", "")
                contenuto = Replace(contenuto, "$provincia$", "")
                contenuto = Replace(contenuto, "$indirizzo$", "")
                contenuto = Replace(contenuto, "$civico$", "")
                contenuto = Replace(contenuto, "$cap$", "")
                contenuto = Replace(contenuto, "$piano$", "")
                contenuto = Replace(contenuto, "$scala$", "")
                contenuto = Replace(contenuto, "$interno$", "")
                contenuto = Replace(contenuto, "$vani$", "")
                contenuto = Replace(contenuto, "$accessori$", "")
                contenuto = Replace(contenuto, "$ingressi$", "")
                contenuto = Replace(contenuto, "$usoimmobile$", "")
                '0200014030500F06001
            End If
            myReaderA.Close()


            par.cmd.CommandText = "select anagrafica.ragione_sociale,RAPPORTI_UTENZA.ID,ANAGRAFICA.DATA_DOC,ANAGRAFICA.RILASCIO_DOC,ANAGRAFICA.NUM_DOC,ANAGRAFICA.TIPO_DOC,ANAGRAFICA.TELEFONO,ANAGRAFICA.RESIDENZA,COMUNI_NAZIONI.SIGLA,COMUNI_NAZIONI.NOME AS ""COMUNE_NASCITA"",ANAGRAFICA.DATA_NASCITA,anagrafica.cognome,anagrafica.nome,RAPPORTI_UTENZA.ID_COMMISSARIATO from COMUNI_NAZIONI,SISCOM_MI.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza where ANAGRAFICA.COD_COMUNE_NASCITA=COMUNI_NAZIONI.COD (+) AND rapporti_utenza.cod_contratto='" & Request.QueryString("COD") & "' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("ragione_sociale"), "") = "" Then
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
                    If par.IfNull(myReader("TIPO_DOC"), "0") = "0" Then
                        DOCUMENTO = "CARTA DI IDENTITA'"
                    End If
                    If par.IfNull(myReader("TIPO_DOC"), "0") = "1" Then
                        DOCUMENTO = "PASSAPORTO"
                    End If

                    NUMERO_DOCUMENTO = par.IfNull(myReader("NUM_DOC"), "")
                    DATA_DOCUMENTO = par.FormattaData(par.IfNull(myReader("DATA_DOC"), ""))
                    AUTORITA = par.IfNull(myReader("RILASCIO_DOC"), "")
                Else
                    COGNOME = par.IfNull(myReader("ragione_sociale"), "")
                    NOME = ""
                    DATA_NASCITA = ""
                    PROVINCIA_NASCITA = ""
                    COMUNE_NASCITA = ""
                    CITTADINANZA = ""
                    DOCUMENTO = ""

                    NUMERO_DOCUMENTO = ""
                    DATA_DOCUMENTO = ""
                    AUTORITA = ""
                End If


                If InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) > 0 Then
                    RESIDENZA = Mid(par.IfNull(myReader("RESIDENZA"), ""), 1, InStr(par.IfNull(myReader("RESIDENZA"), ""), "(", CompareMethod.Text) - 1)
                    INDIRIZZO = Mid(par.IfNull(myReader("RESIDENZA"), ""), InStr(par.IfNull(myReader("RESIDENZA"), ""), "CAP", CompareMethod.Text) + 9, Len(par.IfNull(myReader("RESIDENZA"), "")))
                Else
                    RESIDENZA = ""
                    INDIRIZZO = par.IfNull(myReader("RESIDENZA"), "")
                End If
                If par.IfNull(myReader("TELEFONO"), "") <> "0" Then
                    TELEFONO = par.IfNull(myReader("TELEFONO"), "")
                Else
                    TELEFONO = ""
                End If



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
                    & "'F26','DATI INCOMPLETI')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F26','')"
                    par.cmd.ExecuteNonQuery()
                End If



            End If
            myReader.Close()




            NomeFile = "Ch_" & Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto

            contenuto = Replace(contenuto, "$cognome$", COGNOME)
            contenuto = Replace(contenuto, "$nome$", NOME)
            contenuto = Replace(contenuto, "$datanascita$", DATA_NASCITA)
            contenuto = Replace(contenuto, "$comunenascita$", COMUNE_NASCITA)
            contenuto = Replace(contenuto, "$provincianascita$", PROVINCIA_NASCITA)
            contenuto = Replace(contenuto, "$cittadinanza$", CITTADINANZA)
            contenuto = Replace(contenuto, "$residenza$", RESIDENZA)
            contenuto = Replace(contenuto, "$indirizzo$", INDIRIZZO)
            contenuto = Replace(contenuto, "$telefono$", TELEFONO)
            contenuto = Replace(contenuto, "$documento$", DOCUMENTO)
            contenuto = Replace(contenuto, "$numerodocumento$", NUMERO_DOCUMENTO)
            contenuto = Replace(contenuto, "$datadocumento$", DATA_DOCUMENTO)
            contenuto = Replace(contenuto, "$autorita$", AUTORITA)


            'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Dim url As String = Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm"
            Dim pdfConverter As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfDocumentOptions.ShowFooter = False
            pdfConverter.PdfDocumentOptions.LeftMargin = 5
            pdfConverter.PdfDocumentOptions.RightMargin = 5
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False

            'pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\FileTemp\") & NomeFile & ".pdf")
            pdfConverter.SavePdfFromHtmlStringToFileWithTempFile(contenuto, Server.MapPath("..\..\FileTemp\") & NomeFile & ".pdf", Server.MapPath("..\..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            System.IO.File.Delete(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm")

            Chiusura = Server.MapPath("..\..\FileTemp\") & NomeFile & ".pdf"

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Denuncia Cessione Fabbricato" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Function

    Private Function TassaRifiuti() As String
        Try
            Dim NomeFile As String
            Dim IdContratto As Long = 0
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\Dich_TassaRifiuti.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "SELECT anagrafica.cognome, anagrafica.nome FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto ='" & Request.QueryString("COD") & "' AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' AND anagrafica.ID = soggetti_contrattuali.id_anagrafica"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$cognome$", par.IfNull(myReader("COGNOME"), ""))
                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                IdContratto = par.IfNull(myReader2("ID"), "")
            End If
            myReader2.Close()

            'SELECT unita_contrattuale.* FROM siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto = '0300042030700A01201' AND unita_contrattuale.id_contratto = rapporti_utenza.ID
            par.cmd.CommandText = "SELECT unita_contrattuale.* FROM siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza WHERE  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND rapporti_utenza.cod_contratto = '" & Request.QueryString("COD") & "' AND unita_contrattuale.id_contratto = rapporti_utenza.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$civico$", par.IfNull(myReader("CIVICO"), "__________________"))
            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & IdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F90','Dichiarazione Tassa Rifiuti')"
            par.cmd.ExecuteNonQuery()


            'Dim PercorsoBarCode As String = par.RicavaBarCode(23, IdContratto)
            'contenuto = Replace(contenuto, "$barcode$", "..\..\..\FileTemp\" & PercorsoBarCode)


            NomeFile = "TS_" & Format(Now, "yyyyMMddHHmmss")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Dim url As String = Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm"
            Dim pdfConverter As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfDocumentOptions.ShowFooter = False
            pdfConverter.PdfDocumentOptions.LeftMargin = 5
            pdfConverter.PdfDocumentOptions.RightMargin = 5
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False

            ' pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\FileTemp\") & NomeFile & ".pdf")
            pdfConverter.SavePdfFromHtmlStringToFileWithTempFile(contenuto, Server.MapPath("..\..\FileTemp\") & NomeFile & ".pdf", Server.MapPath("..\..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))
            System.IO.File.Delete(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm")

            TassaRifiuti = Server.MapPath("..\..\FileTemp\") & NomeFile & ".pdf"

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Lettera Rifiuti" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Function
End Class
