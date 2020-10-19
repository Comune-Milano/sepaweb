Imports System
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Class ARCHIVIO_RicercaScatole
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtCod.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim intest As Integer = 1
        If txtScatolaDa.Text <> "" Then
            If IsNumeric(txtScatolaDa.Text) = False Then
                Response.Write("<script>alert('Attenzione, verificare che il campo SCATOLA DA contenga valori validi');</script>")
                Exit Sub
            End If
        End If
        If txtScatolaA.Text <> "" Then
            If IsNumeric(txtScatolaA.Text) = False Then
                Response.Write("<script>alert('Attenzione, verificare che il campo SCATOLA A contenga valori validi');</script>")
                Exit Sub
            End If
        End If


        'If txtScatola.Text <> "" And Mid(txtScatola.Text, Len(par.IfEmpty(txtScatola.Text, " ")), 1) = "," Then
        '    Response.Write("<script>alert('Attenzione, verificare che il campo SCATOLA contenga valori validi');</script>")
        '    Exit Sub
        'Else
        '    Response.Write("<script>location.replace('RisultatoScatole.aspx?SCA=" & par.Cripta(txtScatola.Text) & "&UT=" & UCase(txtCod.Text) & "&EUS=" & UCase(txtEustorgio.Text) & "&FAL=" & txtFaldone.Text & "&SCADA=" & UCase(txtScatolaDa.Text) & "&SCAA=" & UCase(txtScatolaA.Text) & "');</script>")
        'End If
        If ChIntest.Checked = True Then
            intest = 1
        Else
            intest = 0
        End If

        If txtScatola.Text <> "" Then
            If Mid(txtScatola.Text, Len(txtScatola.Text), 1) = "," Then
                Response.Write("<script>alert('Attenzione, verificare che il campo SCATOLA contenga valori validi');</script>")
                Exit Sub
            Else
                'If IsNumeric(txtScatola.Text) = False Then
                '    Response.Write("<script>alert('Attenzione, verificare che il campo SCATOLA contenga valori validi');</script>")
                '    Exit Sub
                'Else
                Response.Write("<script>location.replace('RisultatoScatole.aspx?INT=" & intest & "&SCA=" & par.Cripta(txtScatola.Text) & "&UT=" & UCase(txtCod.Text) & "&EUS=" & UCase(txtEustorgio.Text) & "&FAL=" & txtFaldone.Text & "&SCADA=" & UCase(txtScatolaDa.Text) & "&SCAA=" & UCase(txtScatolaA.Text) & "');</script>")
                'End If
            End If

        Else
        Response.Write("<script>location.replace('RisultatoScatole.aspx?INT=" & intest & "&SCA=" & par.Cripta(txtScatola.Text) & "&UT=" & UCase(txtCod.Text) & "&EUS=" & UCase(txtEustorgio.Text) & "&FAL=" & txtFaldone.Text & "&SCADA=" & UCase(txtScatolaDa.Text) & "&SCAA=" & UCase(txtScatolaA.Text) & "');</script>")
        End If
        
    End Sub

    'Private Sub CREAPDF()
    '    Dim memStream As New MemoryStream
    '    Dim pdfTemplate As String = Server.MapPath("../FileTemp/f24_modello_editabile.pdf")
    '    Dim pdfReader As New PdfReader(pdfTemplate)
    '    Dim pdfStamper As New PdfStamper(pdfReader, memStream)
    '    pdfStamper.Writer.CloseStream = False
    '    Dim pdfFormFields As AcroFields = pdfStamper.AcroFields
    '    ' Setto i field che voglio valorizzare sul documento PDF
    '    ' Checkbox
    '    pdfFormFields.SetField("Check1", "1")
    '    pdfFormFields.SetField("Check2", "0")
    '    ' Campi testuali
    '    pdfFormFields.SetField("Text1", "Nome")
    '    pdfFormFields.SetField("Text2", "Cognome")
    '    ' Rimuovo tutti i fields
    '    pdfStamper.FormFlattening = True
    '    pdfStamper.Close()
    '    ' Faccio direttamente la response del contenuto
    '    Response.AppendHeader("content-disposition", "attachment; filename=modulo_utente.pdf")
    '    Response.AppendHeader("Content-Length", memStream.Length.ToString)
    '    Response.ContentType = "application/pdf"
    '    Response.BinaryWrite(memStream.ToArray)
    '    Response.Flush()
    '    memStream.Close()
    'End Sub
End Class
