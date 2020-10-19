
Partial Class MANUTENZIONI_Locali
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        If Session.Item("TIPO") = "EDIF" Then


            If Me.cmbLocali.SelectedValue <> -1 Then
                Dim i As Integer
                Dim dt As New Data.DataTable
                Dim stringa As String = "select id_locale from SISCOM_MI.locali_no_accessibili where id_edificio =" & Session.Item("ID")

                par.cmd.CommandText = stringa
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If cmbLocali.SelectedValue = dt.Rows(i).Item("ID_LOCALE") Then
                        Response.Write("<script>alert('Locale già iserito!')</script>")
                        Exit Sub
                        Me.cmbLocali.SelectedValue = -1

                    End If
                Next
                par.cmd.CommandText = "insert into SISCOM_MI.locali_no_accessibili (ID_EDIFICIO,id_locale) values (" & Session.Item("ID") & ", " & Me.cmbLocali.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Me.cmbLocali.SelectedValue = -1
                Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
            End If
        ElseIf Session.Item("TIPO") = "COMP" Then

            If Me.cmbLocali.SelectedValue <> -1 Then
                Dim i As Integer
                Dim dt As New Data.DataTable
                Dim stringa As String = "select id_locale from SISCOM_MI.locali_no_accessibili where id_complesso =" & Session.Item("ID")

                par.cmd.CommandText = stringa
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If cmbLocali.SelectedValue = dt.Rows(i).Item("ID_LOCALE") Then
                        Response.Write("<script>alert('Locale già iserito!')</script>")
                        Exit Sub

                        Me.cmbLocali.SelectedValue = -1

                    End If
                Next
                par.cmd.CommandText = "insert into SISCOM_MI.locali_no_accessibili (ID_COMPLESSO,id_locale) values (" & Session.Item("ID") & ", " & Me.cmbLocali.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Me.cmbLocali.SelectedValue = -1
                'par.OracleConn.Close()
                Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "Select * from SISCOM_MI.locali"
            myReader1 = par.cmd.ExecuteReader()
            cmbLocali.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbLocali.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
        End If

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Write("<script>window.close();</script>")

    End Sub
End Class
