
Partial Class Contratti_Report_DownloadDocGest
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msgEX", "document.getElementById('btnPopUp').click();", True)
        End If
        'Response.Redirect("~\FileTemp\ExportGest20140110175242.xls", False)
        'Response.Redirect(Request.QueryString("NOME"), False)
        'ReturnDownloadWithMask(Request.QueryString("NOME"))
    End Sub

    Public Sub ReturnDownloadWithMask(ByVal NomeFile As String, Optional ByVal Percorso As String = "~/FileTemp/")
        HttpContext.Current.Response.ContentType = "application/force-download"
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" & NomeFile)
        HttpContext.Current.Response.BufferOutput = True
        HttpContext.Current.Response.BinaryWrite(FileIO.FileSystem.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath(Percorso) & NomeFile))
    End Sub

    Protected Sub btnPopUp_Click(sender As Object, e As System.EventArgs) Handles btnPopUp.Click
        Response.Write("<script>location.replace('..\/..\/FileTemp\/" & Request.QueryString("NOME") & "');</script>")
    End Sub
End Class
