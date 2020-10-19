
Partial Class Contabilita_CicloPassivo_Plan_EventiPF
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Visualizza(Request.QueryString("id"), Request.QueryString("P"))
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
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DESCRIZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>ENTE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>STRUTTURA</strong></span></td>")
            Response.Write("</tr>")

            If Session.Item("LIVELLO") = "1" Or Session.Item("BP_CONV_ALER") = "1" Then
                par.cmd.CommandText = "SELECT TAB_FILIALI.NOME,CAF_WEB.COD_CAF,PF_EVENTI.ID_OPERATORE,PF_EVENTI.DATA_ORA,TAB_EVENTI.DESCRIZIONE " _
               & ",PF_EVENTI.COD_EVENTO,PF_EVENTI.MOTIVAZIONE,OPERATORI.OPERATORE FROM SISCOM_MI.TAB_FILIALI,CAF_WEB,siscom_mi.PF_EVENTI,siscom_mi.TAB_EVENTI," _
               & " SEPA.OPERATORI WHERE PF_EVENTI.ID_PIANO_FINANZIARIO=" & idpf _
               & " AND PF_EVENTI.COD_EVENTO=TAB_EVENTI.COD (+) " _
               & " AND PF_EVENTI.ID_OPERATORE=OPERATORI.ID (+) AND CAF_WEB.ID=OPERATORI.ID_CAF AND PF_EVENTI.ID_STRUTTURA=TAB_FILIALI.ID (+) ORDER BY DATA_ORA DESC"

            Else

                par.cmd.CommandText = "SELECT TAB_FILIALI.NOME,CAF_WEB.COD_CAF,PF_EVENTI.ID_OPERATORE,PF_EVENTI.DATA_ORA,TAB_EVENTI.DESCRIZIONE " _
                    & ",PF_EVENTI.COD_EVENTO,PF_EVENTI.MOTIVAZIONE,OPERATORI.OPERATORE FROM SISCOM_MI.TAB_FILIALI,CAF_WEB,siscom_mi.PF_EVENTI,siscom_mi.TAB_EVENTI," _
                    & " SEPA.OPERATORI WHERE PF_EVENTI.ID_PIANO_FINANZIARIO=" & idpf _
                    & " AND PF_EVENTI.COD_EVENTO=TAB_EVENTI.COD (+) " _
                    & " AND PF_EVENTI.ID_OPERATORE=OPERATORI.ID (+) AND CAF_WEB.ID=OPERATORI.ID_CAF AND PF_EVENTI.ID_STRUTTURA=TAB_FILIALI.ID (+) " _
                    & " AND (PF_EVENTI.ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " or id_struttura=-1) ORDER BY DATA_ORA DESC"
            End If

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myReader.Read()

                MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
                If IsDate(MiaData) = True Then
                    MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
                Else
                    MiaData = ""
                End If


                OPERATORE = par.IfNull(myReader("OPERATORE"), "")


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
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("NOME"), "") & "</span></td>")
                Response.Write("</tr>")

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
