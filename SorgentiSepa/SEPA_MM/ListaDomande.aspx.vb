
Partial Class ListaDomande
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim OPERATORE As String
    Dim ID_BANDO As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            ID_BANDO = Request.QueryString("IDBANDO")
            par.cmd.CommandText = "select DOMANDE_BANDO.PG,TO_CHAR(TO_DATE(DOMANDE_BANDO.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG"",DICHIARAZIONI.ID from DOMANDE_BANDO,DICHIARAZIONI where  DICHIARAZIONI.ID_CAF=" & par.IfNull(Session.Item("ID_CAF"), "-1") & " and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.FL_RINNOVO='1' AND DOMANDE_BANDO.ID_STATO='2' AND DICHIARAZIONI.ID_STATO<>2 AND DOMANDE_BANDO.ID_BANDO=" & ID_BANDO
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'Response.Write("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
            'Response.Write("<tr>")
            'Response.Write("<td width='100%'>")
            'Response.Write("<p align='center'><b><font face='Arial' size='3'>Domande da elaborare e stampare, in seguito a modifiche delle dichiarazioni.</font></b><p>&nbsp;</td></td>")
            'Response.Write("</tr>")
            'Response.Write("<tr>")
            'Response.Write("<td width='100%'><b><font face='Arial' color='#000080' size='2'>PROTOCOLLO</font></b><p>&nbsp;</td></td>")
            'Response.Write("</tr>")
            'Response.Write("<tr>")
            'Response.Write("<td width='100%'><b><font face='Arial' color='#000080' size='2'> </font></b></td>")
            'Response.Write("</tr>")

            Response.Write("<strong><span style='font-family: Arial'><table border='0' cellpadding cellspacing width='450' background='../IMG/Elenco_1.jpg' height='32'><tr><td height='32'>&nbsp;</tr></table>")
            Response.Write("<table width='450' bgcolor='#DFE2E5' style='border-right: dimgray 1px solid; border-left: dimgray 1px solid;'><tr><td>&nbsp;</td></tr>")

            par.cmd.CommandText = ""


            While myReader.Read
                par.cmd.CommandText = "select OPERATORI.OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI,DICHIARAZIONI,EVENTI_DICHIARAZIONI WHERE EVENTI_DICHIARAZIONI.TIPO_OPERATORE='I' AND EVENTI_DICHIARAZIONI.ID_PRATICA=" & myReader("ID") & " AND DICHIARAZIONI.ID=" & myReader("ID") & " AND OPERATORI.ID=EVENTI_DICHIARAZIONI.ID_OPERATORE ORDER BY EVENTI_DICHIARAZIONI.DATA_ORA DESC"
                Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader11.Read Then
                    OPERATORE = par.IfNull(myReader11("DESCRIZIONE"), "")
                Else
                    OPERATORE = ""
                End If
                Response.Write("<tr>")
                Response.Write("<td width='100%'><b><font face='Arial' size='1'>" & myReader("PG") & " del " & myReader("DATA_PG") & " UTENTE: " & OPERATORE & "</font></b></td>")
                Response.Write("</tr>")
                myReader11.Close()
            End While
            myReader.Close()
            Response.Write("</table>")
            Response.Write("<table width='450' bgcolor='#DFE2E5' style='border-right: dimgray 1px solid; border-left: dimgray 1px solid; border-bottom: dimgray 1px solid;'><tr><td>&nbsp;</td></tr>")
            Response.Write("</table>")

            Response.Write("</span></strong>")
            'Response.Write("</table>")
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If
    End Sub
End Class
