
Partial Class Condomini_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Visualizza(Request.QueryString("IDS"))
        End If


    End Sub
    Private Function Visualizza(ByVal idSegnalazione As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            'Response.Write("CONDOMINI, ID_CONDOMINIO: <strong>" & Codice & "</strong><br />")
            Response.Write("<br />")
            'End If
            'myReader.Close()

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>TIPO EVENTO</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")

            Response.Write("</tr>")

            If par.IfEmpty(idSegnalazione, 0) > 0 Then


                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS NUM, SEPA.OPERATORI.OPERATORE, EVENTI_SEGNALAZIONI.DATA_ORA AS DATA_EVENTO, SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, EVENTI_SEGNALAZIONI.MOTIVAZIONE FROM SISCOM_MI.EVENTI_SEGNALAZIONI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, SISCOM_MI.SEGNALAZIONI WHERE EVENTI_SEGNALAZIONI.COD_EVENTO = TAB_EVENTI.COD AND EVENTI_SEGNALAZIONI.ID_SEGNALAZIONE = SEGNALAZIONI.ID AND EVENTI_SEGNALAZIONI.ID_OPERATORE = OPERATORI.ID AND ID_SEGNALAZIONE =  " & idSegnalazione & " ORDER BY DATA_ORA DESC"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()



                    MiaData = Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 1, 4)
                    If IsDate(MiaData) = True Then
                        MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 11, 2)
                    Else
                        MiaData = ""
                    End If


                    OPERATORE = par.IfNull(myReader("OPERATORE"), "")


                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("TIPO_EVENTO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")

                    Response.Write("</tr>")

                Loop
                myReader.Close()
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
