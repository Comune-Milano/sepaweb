
Partial Class ListaPrenotazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim colore As String

    Dim i As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Response.Write("<p><b><font face='Arial' size='3'>Elenco Prenotazioni Domande in data " & Format(Now, "dd/MM/yyyy") & "</font></b></p>")
                Response.Write("<table border='1' cellpadding='0' cellspacing='0' width='100%'>")

                Response.Write("<tr>")
                Response.Write("<td width='20%'><b>COGNOME</b></td>")
                Response.Write("<td width='22%'><b>NOME</b></td>")
                Response.Write("<td width='13%'><b>CODICE FISCALE</b></td>")
                Response.Write("<td width='25%'><b>RECAPITO</b></td>")
                Response.Write("<td width='10%'><b>DATA INSER.</b></td>")
                Response.Write("<td width='10%'><b>ENTE INSER.</b></td>")
                Response.Write("</tr>")
                i = 0
                par.cmd.CommandText = "SELECT * FROM DOMANDE_PRENOTAZIONI ORDER BY COGNOME ASC,NOME ASC"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                colore = "bgcolor='#FFFFCC'"
                While myReader1.Read()

                    Response.Write("<tr>")
                    Response.Write("<td width='20%' " & colore & "><font face='Arial' size='2'>" & myReader1("COGNOME") & "</td>")
                    Response.Write("<td width='22%' " & colore & "><font face='Arial' size='2'>" & myReader1("NOME") & "</td>")
                    Response.Write("<td width='13%' " & colore & "><font face='Arial' size='2'>" & par.IfNull(myReader1("CF"), "&nbsp;") & "</td>")
                    Response.Write("<td width='25%' " & colore & "><font face='Arial' size='2'>" & par.IfNull(myReader1("TELEFONO"), "&nbsp;") & "</td>")
                    Response.Write("<td width='10%' " & colore & "><font face='Arial' size='2'>" & par.FormattaData(Mid(myReader1("DATA_INS"), 1, 8)) & "</td>")
                    Response.Write("<td width='10%' " & colore & "><font face='Arial' size='2'>" & myReader1("ENTE") & "</td>")
                    Response.Write("</tr>")
                    If i Mod 2 = 0 Then
                        colore = "bgcolor='#FFFFCC'"
                    Else
                        colore = "bgcolor='#CCFFCC'"
                    End If
                    i = i + 1
                End While
                myReader1.Close()

                Response.Write("</table>")
                Response.Write("<p><b><font face='Arial' size='3'>N° Domande " & i & "</font></b></p>")
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch EX1 As Oracle.DataAccess.Client.OracleException
                Response.Write(EX1.ToString)
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                Response.Write("ERRORE DURANTE LA FASE DI LETTURA!")
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub
End Class
