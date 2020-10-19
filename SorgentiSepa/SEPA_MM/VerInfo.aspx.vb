Imports System.IO
Imports ExpertPdf.HtmlToPdf

Partial Class VerInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")
            If objFile.FileExists(Server.MapPath("ver.txt")) Then
                Dim sr As New StreamReader(Server.MapPath("ver.txt"))
                Dim letto As String

                While sr.Peek <> -1
                    letto = sr.ReadLine
                    If letto.Contains("VERSIONE") Then
                        Me.lblReader.Text &= "<strong><em>" & letto & "</em></strong><br/>"
                    Else
                        Me.lblReader.Text &= letto & "<br/>"

                    End If
                End While
                sr.Close()
                sr.Dispose()
                Me.lblReader.Text = Me.lblReader.Text.Replace("*", "*****************************************************************************************")
                Me.lblReader.Text = Me.lblReader.Text.Replace("☺", ".")
            End If

        End If

    End Sub



    Private Sub PDFversione()


        Dim pdf As New PDFSiSol
        Dim nome As String = "VersioneDel_" & Format(Now, "yyyyMMddHHmmss")
        Dim FilePDF = pdf.IstanziaFile
        Dim pagina = pdf.CreaPagina(PDFSiSol.DimensioniPagina.A4, PDFSiSol.OrientamentoPagina.Verticale)
        Dim Document = pdf.IstanziaDocument(pagina)
        pdf.SetMargini(Document, PDFSiSol.TipoMargini.Personalizzati, 30, 50, 30, 30)
        FilePDF = pdf.CreaFile(Document, nome, "FileTemp\", , "VersioneInfo", "VersioneInfo", "VersioneInfo", False)
        pdf.SettaValoriPredefini(PDFSiSol.FontPDF.Arial, 11, PDFSiSol.FontStylePDF.Normale, PDFSiSol.FontColorPDF.Nero)
        pdf.ImpostaFooter(Document, PDFSiSol.SetValori.Personalizzati, PDFSiSol.FontPDF.Arial, 8, PDFSiSol.FontStylePDF.BoldItalic, , "pag.", , , , , , , , , , , , , PDFSiSol.AllignamentoTesto.Destra, -10)

        pdf.ApriDocumento(Document)
        Dim Table = pdf.CreaTable(3, PDFSiSol.AllignamentoElementi.Left, 90, 0, 0, 0, 0)
        pdf.SettaTable(Table, True, 0)
        Dim Cell = pdf.CreaCell(0, 0, PDFSiSol.AllignamentoElementi.Center)
        pdf.SettaCell(Cell, True, 0)
        Dim Logo As iTextSharp.text.Image
        Logo = pdf.CreaImmagine("IMG/logo da sito.bmp")
        pdf.AggiungiImmagine(Cell, Logo)
        pdf.InserisciCell(Table, "", PDFSiSol.SetValori.Predefiniti, , , , , , , , , , True, 0)
        pdf.AggiungiCell(Table, Cell)
        pdf.InserisciCell(Table, "", PDFSiSol.SetValori.Predefiniti, , , , , , , , , , True, 0)
        pdf.InserisciCell(Table, "SEPA@WEB ", PDFSiSol.SetValori.Personalizzati, PDFSiSol.FontPDF.Arial, 6, PDFSiSol.FontStylePDF.Bold, PDFSiSol.FontColorPDF.Nero, 3, , PDFSiSol.AllignamentoElementi.Center, , , True, 0)
        'pdf.InserisciCell(Table, vbCrLf, PDFSiSol.SetValori.Predefiniti, , , , , 3, , , , , True, 0)
        pdf.InserisciCell(Table, vbCrLf, PDFSiSol.SetValori.Predefiniti, , , , , 3, , , , , True, 0)
        pdf.InserisciCell(Table, "INFO AGGIORNAMENTI VERSIONE", PDFSiSol.SetValori.Personalizzati, PDFSiSol.FontPDF.Arial, 10, PDFSiSol.FontStylePDF.Bold, PDFSiSol.FontColorPDF.Nero, 3, , PDFSiSol.AllignamentoElementi.Center, , , True, 0)
        pdf.AggiungiTable(Document, Table)
        pdf.RitornoACapoVuoto(Document)
        pdf.RitornoACapoVuoto(Document)

        Dim objFile As Object
        Dim testo As String = ""
        objFile = Server.CreateObject("Scripting.FileSystemObject")
        If objFile.FileExists(Server.MapPath("ver.txt")) Then
            Dim sr As New StreamReader(Server.MapPath("ver.txt"))
            Dim letto As String

            While sr.Peek <> -1
                letto = sr.ReadLine
                If letto.Contains("VERSIONE") Then
                    testo = letto
                    testo = testo.Replace("*", "******************************************")
                    testo = testo.Replace("☺", ".")

                    pdf.InserisciNuovaPhrase(Document, testo, PDFSiSol.SetValori.Predefiniti, PDFSiSol.FontPDF.Arial, , PDFSiSol.FontStylePDF.Normale, )

                Else
                    testo = letto
                    testo = testo.Replace("*", "******************************************")
                    testo = testo.Replace("☺", ".")

                    pdf.InserisciNuovaPhrase(Document, testo, PDFSiSol.SetValori.Predefiniti, PDFSiSol.FontPDF.Arial, , PDFSiSol.FontStylePDF.Normale, )
                    pdf.RitornoACapoSenzaVuoto(Document)

                End If

            End While
            sr.Close()
            sr.Dispose()
        End If

        pdf.ChiudiDocumento(Document, FilePDF, Me, Me.GetType)
        Response.Write("<script>window.open('FileTemp/" & nome & ".pdf','','');</script>")


    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        PDFversione()
    End Sub
End Class
