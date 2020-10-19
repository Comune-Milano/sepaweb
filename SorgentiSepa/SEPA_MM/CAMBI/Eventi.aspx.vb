
Partial Class CAMBI_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Visualizza(CLng(Request.QueryString("ID")))
        End If
    End Sub

    Private Function Visualizza(ByVal IdDomanda As Long)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_CAMBI.PG,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME FROM DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI WHERE COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE AND COMP_NUCLEO_CAMBI.PROGR=0 AND DOMANDE_BANDO_CAMBI.ID=" & IdDomanda
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If MYREADER.READ Then
                Response.Write("DOMANDA BANDO CAMBI PROTOCOLLO: <strong>" & par.IfNull(MYREADER("PG"), "") & "</strong><br />")
                Response.Write("INTESTATA A: <strong>" & par.IfNull(MYREADER("COGNOME"), "") & " " & par.IfNull(MYREADER("NOME"), "") & "</strong><br />")
            End If
            MYREADER.CLOSE()

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>STATO</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DESCRIZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
            Response.Write("</tr>")

            par.cmd.CommandText = "SELECT EVENTI_BANDI_cambi.TIPO_OPERATORE,EVENTI_BANDI_cambi.ID_OPERATORE,EVENTI_BANDI_cambi.DATA_ORA,TAB_STATI.DESCRIZIONE AS ""DESCR"",TAB_EVENTI.DESCRIZIONE " _
           & ",EVENTI_BANDI_cambi.COD_EVENTO,OPERATORI.OPERATORE,EVENTI_BANDI_cambi.MOTIVAZIONE FROM EVENTI_BANDI_cambi,TAB_EVENTI," _
           & " TAB_STATI,OPERATORI WHERE EVENTI_BANDI_cambi.ID_DOMANDA=" & IdDomanda _
           & " AND EVENTI_BANDI_cambi.COD_EVENTO=TAB_EVENTI.COD (+) " _
           & " AND EVENTI_BANDI_cambi.STATO_PRATICA=TAB_STATI.COD (+) " _
           & " AND EVENTI_BANDI_cambi.ID_OPERATORE=OPERATORI.ID (+) ORDER BY DATA_ORA DESC" 'FOR UPDATE"

            myReader = par.cmd.ExecuteReader()

            Do While myReader.Read()



                MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
                If IsDate(MiaData) = True Then
                    MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
                Else
                    MiaData = ""
                End If

                If par.IfNull(myReader("TIPO_OPERATORE"), "I") = "E" Then

                    par.cmd.CommandText = "SELECT DESCRIZIONE FROM OPERATORI WHERE ID=" & par.IfNull(myReader("ID_OPERATORE"), -1)
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
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("DESCR"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_EVENTO"), "") & " - " & par.IfNull(myReader("DESCRIZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                Response.Write("</tr>")

            Loop
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
