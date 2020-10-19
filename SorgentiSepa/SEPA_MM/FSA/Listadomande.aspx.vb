
Partial Class FSA_Listadomande
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim OPERATORE As String
    Dim ID_BANDO As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            ID_BANDO = Request.QueryString("IDBANDO")

            par.cmd.CommandText = "select DOMANDE_BANDO_fsa.PG,TO_CHAR(TO_DATE(DOMANDE_BANDO_fsa.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG"",DOMANDE_BANDO_fsa.ID from DOMANDE_BANDO_fsa,DICHIARAZIONI_fsa where  DICHIARAZIONI_fsa.ID_CAF=" & par.IfNull(Session.Item("ID_CAF"), "-1") & " and DOMANDE_BANDO_fsa.ID_DICHIARAZIONE=DICHIARAZIONI_fsa.ID AND DOMANDE_BANDO_fsa.FL_RINNOVO='1' AND DOMANDE_BANDO_fsa.ID_STATO='2' AND DICHIARAZIONI_fsa.ID_STATO<>2 AND DOMANDE_BANDO_fsa.ID_BANDO=" & ID_BANDO
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


            Response.Write("<strong><span style='font-family: Arial'><table border='0' cellpadding cellspacing width='450' background='../IMG/Elenco_1.jpg' height='32'><tr><td height='32'>&nbsp;</tr></table>")
            Response.Write("<table width='450' bgcolor='#DFE2E5' style='border-right: dimgray 1px solid; border-left: dimgray 1px solid;'><tr><td>&nbsp;</td></tr>")

            par.cmd.CommandText = ""
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader

            While myReader.Read
                par.cmd.CommandText = "select OPERATORI.OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI,DOMANDE_BANDO_fsa,EVENTI_BANDI_fsa WHERE EVENTI_BANDI_fsa.TIPO_OPERATORE='I' AND EVENTI_BANDI_fsa.ID_DOMANDA=" & myReader("ID") & " AND DOMANDE_BANDO_fsa.ID=" & myReader("ID") & " AND OPERATORI.ID=EVENTI_BANDI_fsa.ID_OPERATORE ORDER BY EVENTI_BANDI_fsa.DATA_ORA DESC"
                myReader11 = par.cmd.ExecuteReader()
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
        End If
    End Sub
End Class
