
Partial Class ElencoOperatoriA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TESTO As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

        lblCaf.Text = "ENTE: " & Session.Item("caaf")
        TESTO = "<table width='100%'><tr><td><span style='font-size: 10pt; font-family: Arial'>OPERATORE</span></td><td><span style='font-size: 10pt; font-family: Arial'>COGNOME</span></td><td><span style='font-size: 10pt; font-family: Arial'>NOME</span></td></tr>"
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from OPERATORI where FL_DA_CONFERMARE='0' AND ID_CAF=" & Session.Item("ID_CAF") & " ORDER BY OPERATORE,COGNOME,NOME"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                TESTO = TESTO & "<tr><td><span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("operatore"), "") & "</span></td><td><span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("cognome"), "") & "</span></td><td><span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("nome"), "") & "</span></td></tr>"

            Loop
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            TESTO = TESTO & "</table>"
            LBLtABELLA.Text = TESTO

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try


    End Sub
End Class
