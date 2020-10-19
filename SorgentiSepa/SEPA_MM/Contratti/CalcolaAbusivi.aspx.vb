
Partial Class Contratti_CalcolaAbusivi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim importo As Double = 0
            Dim testo As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM siscom_mi.ELABORAZIONE_CONGUAGLI WHERE SUBSTR(cod_contratto,1,2)='01' AND NVL(canregime_GESTORI_2008,0)=0 AND TIPO_CONTRATTO='NONE'"
            Dim myReaderAb As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderAb.Read
                par.cmd.CommandText = "select * from siscom_mi.unita_contrattuale where unita_contrattuale.id_contratto=" & myReaderAb("id_contratto") & " and unita_contrattuale.id_unita_principale is null"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    testo = par.CalcolaCanoneAbusivi(myReader("id_unita"), importo)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ELABORAZIONE_CONGUAGLI SET NOTE='" & par.PulisciStrSql(testo) & "',canregime_gestori_2008=" & par.VirgoleInPunti(importo) & " WHERE ID_CONTRATTO=" & myReaderAb("ID_CONTRATTO")
                    par.cmd.ExecuteNonQuery()
                End If
                myReader.Close()
            Loop
            myReaderAb.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
