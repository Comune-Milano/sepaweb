
Partial Class Contratti_ElencoSolleciti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Label2.Text = "<table cellpadding='0' cellspacing='0' style='width:100%;'><tr><td style='font-family: ARIAL; font-size: 10pt; font-weight: bold'>DATA INVIO SOLLECITO</td></tr>"
        
                Dim i As Integer = 0

                Dim COLORE As String = "#FFFFFF"

                par.cmd.CommandText = "SELECT BOL_BOLLETTE_SOLLECITI.data_invio,bol_bollette.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_SOLLECITI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_SOLLECITI.ID_BOLLETTA AND ID_BOLLETTA=" & Request.QueryString("ID") & " order by data_invio desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read()
                    Label1.Text = "Solleciti Boll. " & Format(myReader("id"), "0000000000")
                    Label3.Text = "Importo boll:" & Format(myReader("importo_totale"), "##,##0.00")
                    If COLORE = "#DBDBDB" Then
                        COLORE = "#FFFFFF"
                    Else
                        COLORE = "#DBDBDB"
                    End If
                    i = i + 1
                    Label2.Text = Label2.Text & "<tr><td style='font-family: ARIAL; font-size: 10pt; font-weight: normal; background-color: " & COLORE & "'>" & par.FormattaData(myReader("DATA_INVIO")) & "</td></tr>"

                Loop
                myReader.Close()
                If i = 0 Then
                    Label2.Text = Label2.Text & "<tr><td style='font-family: ARIAL; font-size: 10pt; font-weight: normal; background-color: #FFFFFF'>Nessun Sollecito emesso.</td></tr>"
                End If

                Label2.Text = Label2.Text & "</table>"

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                Label2.Text = ex.Message

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try


        End If
    End Sub
End Class
