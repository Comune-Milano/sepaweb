
Partial Class Contabilita_CicloPassivo_Plan_Annotazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Visualizza(Request.QueryString("IDP"), Request.QueryString("P"))
        End If
    End Sub

    Private Function Visualizza(ByVal idpf As String, ByVal periodo As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            Response.Write("PIANO FINANZIARIO: <strong>" & periodo & "</strong><br />")
            Response.Write("<br />")

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>NOTE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>ESITO</strong></span></td>")
            Response.Write("</tr>")

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_CONVALIDE WHERE ID_PIANO_FINANZIARIO=" & idpf & " ORDER BY DATA_ORA DESC"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myReader.Read()

                MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
                If IsDate(MiaData) = True Then
                    MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
                Else
                    MiaData = ""
                End If

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("NOTE"), "") & "</span></td>")
                Response.Write("<td>")
                If par.IfNull(myReader("tipo"), "0") = "0" Then
                    If par.IfNull(myReader("ESITO"), "") = "1" Then
                        Response.Write("<span style='font-size: 10pt; font-family: Arial'>CONVALIDATO GESTORE</span></td>")
                    Else
                        Response.Write("<span style='font-size: 10pt; font-family: Arial'>NON CONVALIDATO GESTORE</span></td>")
                    End If
                Else
                    If par.IfNull(myReader("ESITO"), "") = "1" Then
                        Response.Write("<span style='font-size: 10pt; font-family: Arial'>CONVALIDATO COMUNE</span></td>")
                    Else
                        Response.Write("<span style='font-size: 10pt; font-family: Arial'>NON CONVALIDATO COMUNE</span></td>")
                    End If
                End If
                Response.Write("<td>")
                Response.Write("</tr>")


                If par.IfNull(myReader("tipo"), "0") <> "0" And par.IfNull(myReader("ESITO"), "") <> "1" Then

                    par.cmd.CommandText = "SELECT PF_CONVALIDE_VOCI.*,PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_CONVALIDE_VOCI WHERE PF_VOCI.ID=PF_CONVALIDE_VOCI.ID_VOCE AND ID_CONVALIDA=" & myReader("id") & " ORDER BY codice asc,id asc"
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader123.Read
                        Response.Write("<tr >")
                        Response.Write("<td>")
                        Response.Write("</td>")
                        Response.Write("<td style='background-color: #D7D7D7'><span style='font-size: 8pt; font-family: Arial;font-style: italic'>" & par.IfNull(myReader123("CODICE"), "") & "-" & par.IfNull(myReader123("DESCRIZIONE"), "") & "</span>")
                        Response.Write("</td>")
                        Response.Write("<td style='background-color: #D7D7D7'><span style='font-size: 8pt; font-family: Arial;font-style: italic'>" & par.IfNull(myReader123("NOTE"), "") & "</span></td>")
                        Response.Write("</tr>")
                    Loop
                    myReader123.Close()
                End If

            Loop
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
