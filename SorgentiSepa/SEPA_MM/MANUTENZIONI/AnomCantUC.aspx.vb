
Partial Class MANUTENZIONI_AnomCantUC
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            'par.OracleConn.Open()
            'par.SettaCommand(par)


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "Select * from SISCOM_MI.tipo_anomalie_uc"
            myReader1 = par.cmd.ExecuteReader()
            cmbtipoanomalie.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbtipoanomalie.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            'par.OracleConn.Close()

        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If Me.TxtValore.Text <> "" AndAlso Me.cmbtipoanomalie.SelectedValue <> -1 Then
            'par.OracleConn.Open()
            'par.SettaCommand(par)

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim i As Integer
            Dim dt As New Data.DataTable
            Dim stringa As String = "select id_tipologia from SISCOM_MI.anomalie_uc where id_unita_comune =" & Session.Item("ID")
            par.cmd.CommandText = stringa
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            If Not IsNothing(da) Then
                da.Fill(dt)
                For i = 0 To dt.Rows.Count - 1
                    If cmbtipoanomalie.SelectedValue = dt.Rows(i).Item("ID_TIPOLOGIA") Then
                        Response.Write("<script>alert('Dotazione già iserita!')</script>")
                        Exit Sub
                        Me.TxtValore.Text = ""
                        Me.cmbtipoanomalie.SelectedValue = -1

                    End If
                Next
            End If

            par.cmd.CommandText = "insert into SISCOM_MI.anomalie_uc (ID_UNITA_COMUNE, ID_TIPOLOGIA, VALORE) values (" & Session.Item("ID") & ", " & Me.cmbtipoanomalie.SelectedValue & ", " & par.VirgoleInPunti(Me.TxtValore.Text) & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            Me.TxtValore.Text = ""
            Me.cmbtipoanomalie.SelectedValue = -1
            'par.OracleConn.Close()
            Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")


        End If

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
End Class
