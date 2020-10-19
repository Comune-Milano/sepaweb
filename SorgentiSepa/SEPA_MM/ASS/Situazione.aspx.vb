
Partial Class ASS_Situazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            '    sValorePG = par.PulisciStrSql(Request.QueryString("PG"))
            '    sValoreID = par.PulisciStrSql(Request.QueryString("ID"))
            '    If IsNumeric(sValoreID) Then
            '        lblPg.Text = "PG: " & sValorePG
            Try
                Label9.Text = "Data Interrogazione : " & Format(Now, "dd/MM/yyyy")

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT count(id) FROM DOMANDE_bando WHERE fl_controlla_requisiti='2'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label5.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()


                par.cmd.CommandText = "SELECT count(id) FROM DOMANDE_bando WHERE fl_invito='1'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label6.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT count(id) FROM alloggi WHERE stato=7 and prenotato='1' and id_pratica in (select id from domande_bando)"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label7.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()


                par.cmd.CommandText = "SELECT count(id) FROM DOMANDE_bando_CAMBI WHERE fl_controlla_requisiti='2'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label13.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()


                par.cmd.CommandText = "SELECT count(id) FROM DOMANDE_bando_CAMBI WHERE fl_invito='1'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label14.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT count(id) FROM alloggi WHERE stato=7 and prenotato='1' and id_pratica in (select id from domande_bando_CAMBI)"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label15.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT count(id) FROM alloggi WHERE stato=5"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Label8.Text = par.IfNull(myReader(0), "0")
                End If
                myReader.Close()

                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If
    End Sub
End Class
