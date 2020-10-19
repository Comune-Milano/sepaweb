
Partial Class CONS_RisultatoStatPrenotazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)
            Response.Flush()

            DAL = Request.QueryString("DAL")
            AL = Request.QueryString("AL")

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td style='text-align: center'>")
            Response.Write("<strong><span style='font-size: 14pt'>STATISTICHE CONSULTAZIONI</span></strong></td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("&nbsp;</td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span id='Label1' style='display:inline-block;width:447px;z-index: 100; left: 0px; position: static;top: 0px'>Ente: " & Session.Item("CAAF") & "<br>Consultazioni effettuate dal " & par.FormattaData(DAL) & " al " & par.FormattaData(AL) & "</span></td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("</table>")
            Response.Write("<br>")

            Response.Write("<table width='100%'>")

            Response.Write("<tr>")
            Response.Write("<td width='33%'>")
            Response.Write("<p><b>DATA</b></td>")
            Response.Write("<td width='33%'>")
            Response.Write("<p><b>N. DOMANDA</b></td>")
            Response.Write("<td width='34%'>")
            Response.Write("<p><b>OPERATORE</b></td>")
            Response.Write("</tr>")

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select DOMANDE_BANDO.PG,OPERATORI.OPERATORE AS ""DESCRIZIONE"",CONSULTAZIONI_WEB.DATA_VISIONATO FROM DOMANDE_BANDO,OPERATORI,CONSULTAZIONI_WEB WHERE CONSULTAZIONI_WEB.ID_VISIONATO=DOMANDE_BANDO.ID AND CONSULTAZIONI_WEB.TIPO_VISIONATO='DO' AND CONSULTAZIONI_WEB.ID_OPERATORE=OPERATORI.ID AND OPERATORI.ID_CAF=" & Session.Item("ID_CAF") & " AND CONSULTAZIONI_WEB.DATA_VISIONATO>='" & par.AggiustaData(DAL) & "' AND CONSULTAZIONI_WEB.DATA_VISIONATO<='" & par.AggiustaData(AL) & "' ORDER BY CONSULTAZIONI_WEB.ID DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                Response.Write("<tr>")
                Response.Write("<td width='33%'>")
                Response.Write("<p>" & par.FormattaData(myReader("DATA_VISIONATO")) & "</td>")
                Response.Write("<td width='33%'>")
                Response.Write("<p>" & par.IfNull(myReader("PG"), "") & "</td>")
                Response.Write("<td width='34%'>")
                Response.Write("<p>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td>")
                Response.Write("</tr>")
            End While
            myReader.Close()

            Response.Write("</table>")


            Response.Write("<br>")

            Response.Write("<table width='100%'>")

            Response.Write("<tr>")
            Response.Write("<td width='33%'>")
            Response.Write("<p><b>DATA</b></td>")
            Response.Write("<td width='33%'>")
            Response.Write("<p><b>N. DICHIARAZIONE</b></td>")
            Response.Write("<td width='34%'>")
            Response.Write("<p><b>OPERATORE</b></td>")
            Response.Write("</tr>")

            par.cmd.CommandText = "select DICHIARAZIONI.PG,OPERATORI.OPERATORE AS ""DESCRIZIONE"",CONSULTAZIONI_WEB.DATA_VISIONATO FROM DICHIARAZIONI,OPERATORI,CONSULTAZIONI_WEB WHERE CONSULTAZIONI_WEB.ID_VISIONATO=DICHIARAZIONI.ID AND CONSULTAZIONI_WEB.TIPO_VISIONATO='DI' AND CONSULTAZIONI_WEB.ID_OPERATORE=OPERATORI.ID AND OPERATORI.ID_CAF=" & Session.Item("ID_CAF") & " AND CONSULTAZIONI_WEB.DATA_VISIONATO>='" & par.AggiustaData(DAL) & "' AND CONSULTAZIONI_WEB.DATA_VISIONATO<='" & par.AggiustaData(AL) & "' ORDER BY CONSULTAZIONI_WEB.ID DESC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                Response.Write("<tr>")
                Response.Write("<td width='33%'>")
                Response.Write("<p>" & par.FormattaData(myReader("DATA_VISIONATO")) & "</td>")
                Response.Write("<td width='33%'>")
                Response.Write("<p>" & par.IfNull(myReader("PG"), "") & "</td>")
                Response.Write("<td width='34%'>")
                Response.Write("<p>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td>")
                Response.Write("</tr>")
            End While
            myReader.Close()


            Response.Write("</table>")

            par.OracleConn.Close()
        End If
    End Sub

    Public Property DAL() As String
        Get
            If Not (ViewState("par_DAL") Is Nothing) Then
                Return CStr(ViewState("par_DAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DAL") = value
        End Set

    End Property

    Public Property AL() As String
        Get
            If Not (ViewState("par_AL") Is Nothing) Then
                Return CStr(ViewState("par_AL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_AL") = value
        End Set

    End Property
End Class
