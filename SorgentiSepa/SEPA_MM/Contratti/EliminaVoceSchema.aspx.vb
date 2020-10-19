
Partial Class Contratti_EliminaVoceSchema
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("CN")), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("CN")), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "delete from siscom_mi.bol_schema WHERE ID=" & par.DeCriptaMolto(Request.QueryString("ID"))
                par.cmd.ExecuteNonQuery()
                '0300030080200X29903

                Response.Write("La voce è stata eliminata.<br/><a href='javascript:void(0)' onclick='javascript:opener.document.getElementById('form1').submit();self.close();'>Chiudi finestra</a>")
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
End Class
