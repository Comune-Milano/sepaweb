
Partial Class Contratti_Report_Visualizza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        Label1.Text = "Distinta rate emesse dal " & par.IfEmpty(par.FormattaData(Request.QueryString("DAL")), "--") & " al " & par.IfEmpty(par.FormattaData(Request.QueryString("AL")), "--") & " Periodo di riferimento: Dal " & par.IfEmpty(par.FormattaData(Request.QueryString("DAL1")), "--") & " al " & par.IfEmpty(par.FormattaData(Request.QueryString("AL1")), "--")
        dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)

        DataGridRateEmesse.DataSource = dt
        DataGridRateEmesse.DataBind()

    End Sub
End Class
