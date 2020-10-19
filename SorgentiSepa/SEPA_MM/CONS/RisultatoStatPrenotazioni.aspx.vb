
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
            Response.Write("<strong><span style='font-size: 14pt'>PRENOTAZIONI EFFETTUATE</span></strong></td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("&nbsp;</td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span id='Label1' style='display:inline-block;width:447px;z-index: 100; left: 0px; position: static;top: 0px'>Ente: " & Session.Item("CAAF") & "<br>Prenotazioni effettuate dal " & par.FormattaData(DAL) & " al " & par.FormattaData(AL) & "</span></td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("</table>")
            Response.Write("<br>")

            Response.Write("<table width='100%'>")

            Response.Write("<tr>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>APPUNTAMENTO</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>N. DOMANDA</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>NOMINATIVO</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>OPERATORE</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>EFFETTUATA IL</b></td>")

            Response.Write("</tr>")

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,OPERATORI.OPERATORE AS ""DESCRIZIONE"",EVENTI_BANDI.MOTIVAZIONE,EVENTI_BANDI.DATA_ORA FROM COMP_NUCLEO,DOMANDE_BANDO,OPERATORI,EVENTI_BANDI WHERE COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND EVENTI_BANDI.COD_EVENTO='F151' AND EVENTI_BANDI.ID_DOMANDA=DOMANDE_BANDO.ID AND EVENTI_BANDI.ID_OPERATORE=OPERATORI.ID AND OPERATORI.ID_CAF=" & Session.Item("ID_CAF") & " AND EVENTI_BANDI.DATA_ORA>='" & par.AggiustaData(DAL) & "000001' AND EVENTI_BANDI.DATA_ORA<='" & par.AggiustaData(AL) & "235959' ORDER BY EVENTI_BANDI.DATA_ORA DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                Response.Write("<tr>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("PG"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.FormattaData(Mid(myReader("DATA_ORA"), 1, 8)) & "</td>")


                Response.Write("</tr>")
            End While
            myReader.Close()

            Response.Write("</table>")


            Response.Write("<br>")

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td style='text-align: center'>")
            Response.Write("<strong><span style='font-size: 14pt'>CANCELLAZIONI PRENOTAZIONI </span></strong></td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("&nbsp;</td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span id='Label1' style='display:inline-block;width:447px;z-index: 100; left: 0px; position: static;top: 0px'>Ente: " & Session.Item("CAAF") & "<br>Cancellazioni effettuate dal " & par.FormattaData(DAL) & " al " & par.FormattaData(AL) & "</span></td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("</table>")
            Response.Write("<br>")

            Response.Write("<table width='100%'>")

            Response.Write("<tr>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>APPUNTAMENTO</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>N. DOMANDA</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>NOMINATIVO</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>OPERATORE</b></td>")

            Response.Write("<td width='20%'>")
            Response.Write("<p><b>EFFETTUATA IL</b></td>")

            Response.Write("</tr>")

            par.cmd.CommandText = "select COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,OPERATORI.OPERATORE AS ""DESCRIZIONE"",EVENTI_BANDI.MOTIVAZIONE,EVENTI_BANDI.DATA_ORA FROM COMP_NUCLEO,DOMANDE_BANDO,OPERATORI,EVENTI_BANDI WHERE COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND EVENTI_BANDI.COD_EVENTO='F152' AND EVENTI_BANDI.ID_DOMANDA=DOMANDE_BANDO.ID AND EVENTI_BANDI.ID_OPERATORE=OPERATORI.ID AND OPERATORI.ID_CAF=" & Session.Item("ID_CAF") & " AND EVENTI_BANDI.DATA_ORA>='" & par.AggiustaData(DAL) & "000001' AND EVENTI_BANDI.DATA_ORA<='" & par.AggiustaData(AL) & "235959' ORDER BY EVENTI_BANDI.DATA_ORA DESC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                Response.Write("<tr>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("PG"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.IfNull(myReader("DESCRIZIONE"), "") & "</td>")

                Response.Write("<td width='20%'>")
                Response.Write("<p>" & par.FormattaData(Mid(myReader("DATA_ORA"), 1, 8)) & "</td>")


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
