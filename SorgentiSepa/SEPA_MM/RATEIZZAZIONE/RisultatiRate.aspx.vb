
Partial Class RATEIZZAZIONE_RisultatiRate
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        Dim Risultati As String = Request.QueryString("RESULT")
        Response.Write("<script>alert('" & Session.Item("titolo") & "!');</script>")

        If Not IsPostBack Then
            Response.Flush()
            Dim dtResult As New Data.DataTable
            dtResult = CType(HttpContext.Current.Session.Item("dtResult"), Data.DataTable)
            If dtResult.Rows.Count > 0 Then
                Dim testoTabella As String
                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                             & "<tr>" _
                             & "<td style='height: 19px; border-bottom-style: ridge; border-bottom-width: thin; border-bottom-color: #0000FF'>" _
                             & "<span style='font-size: 10pt; font-family: Arial;'><strong>SONO STATI GENERATI I SEGUENTI FILES</strong></span></td>" _
                             & "<td></td><tr><td>&nbsp</td><td>&nbsp</td></tr>"

                For Each r As Data.DataRow In dtResult.Rows
                    testoTabella = testoTabella & "<tr>" _
                    & "<td style='height: 19px; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0'>" _
                    & "<span style='font-size: 8pt; font-family: Arial;'><strong><a href=" & Chr(34) & r.Item("LINK") & Chr(34) & " target = " & Chr(34) & "_blank" & Chr(34) & ">" & r.Item("DESCRIZIONE") & "</a></strong></span></td></tr>"
                Next
                'testoTabella = testoTabella & "<tr><td>&nbsp</td><td>&nbsp</td></tr><tr><td>&nbsp</td><td>&nbsp</td></tr><tr><td>&nbsp</td><td>&nbsp</td></tr><tr>" _
                '             & "<td style='height: 19px;'>" _
                '             & "<strong><span style='font-family: Arial;color: #FF3300;'><font size='3'>ATTENZIONE:</font></span></strong><br/>" _
                '             & "<span style='font-size: 10pt; font-family: Arial;'><strong>" & Session.Item("titolo") & "</strong></span></td>" _
                '             & "<td></td>"

                testoTabella = testoTabella & "</table>"

                TBL_RISULTATI.Text = testoTabella
                Session.Remove("dtResult")
                Session.Remove("titolo")
            End If
            Response.Write("<script>alert('Operazione completata correttamente!');</script>")

        End If
    End Sub
End Class
