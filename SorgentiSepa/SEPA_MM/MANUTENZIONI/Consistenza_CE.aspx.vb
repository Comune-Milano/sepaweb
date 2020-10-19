
Partial Class MANUTENZIONI_MiniConsEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "Select * from SISCOM_MI.tipologia_consistenza_ce"
            myReader1 = par.cmd.ExecuteReader()
            cmbTipocONS.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipocONS.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            'par.OracleConn.Close()
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        If Session.Item("TIPO") = "EDIF" Then
            If Me.txtQuantita.Text <> "" AndAlso Me.cmbTipocONS.SelectedValue <> -1 Then



                Dim i As Integer
                Dim dt As New Data.DataTable
                Dim stringa As String = "select id_tipologia from SISCOM_MI.consistenza_ce where id_edificio =" & Session.Item("ID")
                par.cmd.CommandText = stringa
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If cmbTipocONS.SelectedValue = dt.Rows(i).Item("ID_TIPOLOGIA") Then
                        Response.Write("<script>alert('Dotazione già iserita!')</script>")
                        Exit Sub
                        Me.txtQuantita.Text = ""
                        Me.cmbTipocONS.SelectedValue = -1

                    End If
                Next
                par.cmd.CommandText = "insert into SISCOM_MI.consistenza_ce (ID_EDIFICIO, ID_TIPOLOGIA, QUANT) values (" & Session.Item("ID") & ", " & Me.cmbTipocONS.SelectedValue & ", " & Me.txtQuantita.Text & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Me.txtQuantita.Text = ""
                Me.cmbTipocONS.SelectedValue = -1
                Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")


            End If
        ElseIf Session.Item("TIPO") = "COMP" Then

            If Me.txtQuantita.Text <> "" AndAlso Me.cmbTipocONS.SelectedValue <> -1 Then

                Dim i As Integer
                Dim dt As New Data.DataTable
                Dim stringa As String = "select id_tipologia from SISCOM_MI.consistenza_ce where id_complesso =" & Session.Item("ID")

                par.cmd.CommandText = stringa
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If cmbTipocONS.SelectedValue = dt.Rows(i).Item("ID_TIPOLOGIA") Then
                        Response.Write("<script>alert('Dotazione già iserita!')</script>")
                        Exit Sub
                        Me.txtQuantita.Text = ""
                        Me.cmbTipocONS.SelectedValue = -1

                    End If
                Next
                par.cmd.CommandText = "insert into SISCOM_MI.consistenza_ce (ID_COMPLESSO, ID_TIPOLOGIA, QUANT) values (" & Session.Item("ID") & ", " & Me.cmbTipocONS.SelectedValue & ", " & Me.txtQuantita.Text & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Me.txtQuantita.Text = ""
                Me.cmbTipocONS.SelectedValue = -1
                Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")


            End If
        End If

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
End Class
