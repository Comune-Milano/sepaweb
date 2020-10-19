
Partial Class AMMSEPA_OperatoreSUA_RisultatoassegnaStrutturaSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public scelta As String = ""
    Public sStringaSQL As String = ""
    Dim contatore As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        Try
            Dim MyItem As ListItem
            If CheckBox1.Checked = True Then
                For Each MyItem In ckbutenti.Items
                    MyItem.Selected = True
                Next
            Else
                For Each MyItem In ckbutenti.Items
                    MyItem.Selected = False
                Next
            End If
            If Not IsPostBack Then
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbFiliale", "SELECT id,nome FROM siscom_mi.tab_filiali order by nome", "NOME", "ID")
                scelta = Request.QueryString("scelta")
                If scelta = "Tutte" Then
                    sStringaSQL = "SELECT operatore " _
                                & "FROM operatori " _
                                & "WHERE id_caf = 2 AND sepa_web=1 AND operatore is not null " _
                                & "ORDER BY OPERATORE"
                ElseIf scelta = "Nessuna" Then
                    sStringaSQL = "SELECT operatore " _
                                & "FROM operatori " _
                                & "WHERE operatori.id_ufficio = 0 AND id_caf=2 AND sepa_web = 1 AND operatore is not null " _
                                & "ORDER BY OPERATORE"
                Else
                    sStringaSQL = "SELECT operatore " _
                                & "FROM operatori, siscom_mi.tab_filiali " _
                                & "WHERE tab_filiali.ID = operatori.id_ufficio AND tab_filiali.ID = '" & par.PulisciStrSql(scelta) & "' AND operatore is not null " _
                                & "ORDER BY OPERATORE"
                End If
                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
                While myReader.Read
                    ckbutenti.Items.Add(par.IfNull(myReader(0), ""))
                    contatore = contatore + 1
                End While
                cmd.Dispose()
                myReader.Close()
                par.OracleConn.Close()
                Label10.Text = "Totale operatori: " & contatore
                If contatore = 0 Then
                    ckbutenti.Visible = False
                    Label8.Visible = False
                    cmbFiliale.Visible = False
                    btnVisualizza.Visible = False
                    Label11.Text = "Non sono stati trovati operatori nella ricerca effettuata."
                    Label11.Visible = True
                    CheckBox1.Visible = False
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""assegnaStrutturaSUA.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Try
            Dim conta As Integer = 0
            If scelta2.Value = 1 Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                Dim MyItem As ListItem
                For Each MyItem In ckbutenti.Items
                    If MyItem.Selected = True Then
                        par.cmd.CommandText = "update operatori set id_ufficio =" & cmbFiliale.SelectedValue & " where operatore ='" & Replace(MyItem.Value, "'", "''") & "'"
                        par.cmd.ExecuteNonQuery()
                        conta = conta + 1
                    End If
                Next
                par.OracleConn.Close()
                If conta = 0 Then
                    Response.Write("<script>alert('Nessun operatore selezionato!');</script>")
                Else
                    Response.Write("<script>alert('Operazione Effettuata!');document.location.href=""assegnaStrutturaSUA.aspx"";</script>")
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
