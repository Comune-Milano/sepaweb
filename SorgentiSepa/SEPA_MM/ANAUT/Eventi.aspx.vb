
Partial Class ANAUT_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            vIdConnessione = Request.QueryString("IDCONN")
            Visualizza(CLng(Request.QueryString("ID")))
        End If
    End Sub

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Private Function Visualizza(ByVal IdDomanda As Long)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.PG,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID AND UTENZA_COMP_NUCLEO.PROGR=0 AND UTENZA_DICHIARAZIONI.ID=" & IdDomanda
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Response.Write("<table width='100%' cellpadding='0' cellspacing='0'>")
                Response.Write("<tr bgcolor='Maroon'>")
                Response.Write("<td align='center'>")
                Response.Write("<span style='font-size: 12pt; font-family: Arial; color: #FFFFFF'><strong>EVENTI SCHEDA A.U.</strong></span></td>")
                Response.Write("<td>")
                Response.Write("</tr>")

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>&nbsp;&nbsp;</span></td>")
                Response.Write("<td>")
                Response.Write("</tr>")

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>ANAGRAFE UTENZA PROTOCOLLO: <strong>" & par.IfNull(myReader("PG"), "") & "</strong></span></td>")
                Response.Write("<td>")
                Response.Write("</tr>")

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>INTESTATA A: <strong>" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & "</strong></span></td>")
                Response.Write("<td>")
                Response.Write("</tr>")
                Response.Write("</table>")

            End If
            myReader.Close()

            Response.Write("<br/><table width='100%' cellpadding='0' cellspacing='0'>")
            Response.Write("<tr bgcolor='Maroon'>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial;color: #FFFFFF'><strong>DATA</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial;color: #FFFFFF'><strong>DESCRIZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial;color: #FFFFFF'><strong>MOTIVAZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial;color: #FFFFFF'><strong>OPERATORE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial;color: #FFFFFF'><strong>ENTE</strong></span></td>")
            Response.Write("</tr>")

            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>&nbsp;&nbsp;</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>&nbsp;&nbsp;</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>&nbsp;&nbsp;</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>&nbsp;&nbsp;</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>&nbsp;&nbsp;</strong></span></td>")
            Response.Write("</tr>")

            par.cmd.CommandText = "SELECT CAF_WEB.COD_CAF,UTENZA_EVENTI_DICHIARAZIONI.TIPO_OPERATORE,UTENZA_EVENTI_DICHIARAZIONI.ID_OPERATORE,UTENZA_EVENTI_DICHIARAZIONI.DATA_ORA,TAB_EVENTI.DESCRIZIONE " _
           & ",UTENZA_EVENTI_DICHIARAZIONI.COD_EVENTO,UTENZA_EVENTI_DICHIARAZIONI.MOTIVAZIONE,OPERATORI.OPERATORE,UTENZA_EVENTI_DICHIARAZIONI.MOTIVAZIONE FROM UTENZA_EVENTI_DICHIARAZIONI,TAB_EVENTI," _
           & " OPERATORI,CAF_WEB WHERE UTENZA_EVENTI_DICHIARAZIONI.ID_PRATICA=" & IdDomanda _
           & " AND UTENZA_EVENTI_DICHIARAZIONI.COD_EVENTO=TAB_EVENTI.COD (+) " _
           & " AND UTENZA_EVENTI_DICHIARAZIONI.ID_OPERATORE=OPERATORI.ID (+) AND CAF_WEB.ID=OPERATORI.ID_CAF ORDER BY DATA_ORA DESC" 'FOR UPDATE"

            myReader = par.cmd.ExecuteReader()

            Do While myReader.Read()



                MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
                If IsDate(MiaData) = True Then
                    MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
                Else
                    MiaData = ""
                End If

                If par.IfNull(myReader("TIPO_OPERATORE"), "I") = "E" Then
                    par.cmd.CommandText = "SELECT OPERATORE FROM OPERATORI WHERE ID=" & par.IfNull(myReader("ID_OPERATORE"), -1)
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        OPERATORE = "OP. WEB:" & par.IfNull(myReader1("DESCRIZIONE"), "")
                    End If
                    myReader1.Close()
                Else
                    OPERATORE = par.IfNull(myReader("OPERATORE"), "")
                End If

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_EVENTO"), "") & " - " & par.IfNull(myReader("DESCRIZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_CAF"), "") & "</span></td>")
                Response.Write("</tr>")

            Loop
            myReader.Close()
            par.OracleConn.Close()
        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
