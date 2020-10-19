
Partial Class Contabilita_CicloPassivo_Plan_NuoviEventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            CaricaVoci()

        End If
    End Sub

    Private Sub CaricaVoci()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Me.CheckBoxList1.Items.Clear()

            If Session.Item("ID_OPERATORE") = "1" Then
                par.cmd.CommandText = "select * from siscom_mi.pf_convalide_voci,siscom_mi.pf_voci where pf_voci.id=pf_convalide_voci.id_voce and pf_convalide_voci.visto=0 and pf_voci.id in (select id_voce from siscom_mi.pf_voci_operatori) order by data_ora asc"
            Else
                par.cmd.CommandText = "select * from siscom_mi.pf_convalide_voci,siscom_mi.pf_voci where pf_voci.id=pf_convalide_voci.id_voce and pf_convalide_voci.visto=0 and pf_voci.id in (select id_voce from siscom_mi.pf_voci_operatori where id_operatore=" & Session.Item("ID_CAF") & ") order by data_ora asc"
            End If
            Dim elemento As String = ""

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                elemento = par.FormattaData(Mid(myReader("data_ora"), 1, 8)) & " " & Mid(myReader("data_ora"), 9, 2) & ":" & Mid(myReader("data_ora"), 11, 2) & " " & par.MiaFormat(par.IfNull(myReader("codice"), " "), 10) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), " "), 50) & " " & par.MiaFormat(par.IfNull(myReader("note"), " "), 50)

                CheckBoxList1.Items.Add(New ListItem(elemento, par.IfNull(myReader("ID"), -1)))
            End While

            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write(ex.Message)
        End Try
    End Sub
End Class
