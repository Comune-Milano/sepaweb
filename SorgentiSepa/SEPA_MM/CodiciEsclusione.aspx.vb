
Partial Class CodiciEsclusione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        par.OracleConn.Open()
        par.SettaCommand(par)

        par.cmd.CommandText = "SELECT sigla,descrizione FROM t_tipo_esclusione_domande order by sigla asc"
        Dim myreader55 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Response.Write("<p><b><font size='4'>ELENCO CODICI ESCLUSIONE</font></b></p>")

        While myreader55.Read
            Response.Write("<p><b><font size='3'>" & par.IfNull(myreader55("sigla"), " ") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>" & par.IfNull(myreader55("descrizione"), " ") & "</font></p>")
        End While
        myreader55.Close()
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
End Class
