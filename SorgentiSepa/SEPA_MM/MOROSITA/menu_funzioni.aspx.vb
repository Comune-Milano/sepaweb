
Partial Class MOROSITA_menu_funzioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Function Spegni()
        avviso.Visible = False
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") <> "" Then
            lblOperatore.Text = Session.Item("OPERATORE")
            If Session.Item("PW") = "SEPA" Then
                avviso.Visible = True
            Else
                avviso.Visible = False
            End If
            LinkButton1.Attributes.Add("OnClick", "javascript:parent.main.Uscita=1;")
        Else
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

    End Sub

    'Protected Sub imgPw_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPw.Click
    '    If Session.Item("LAVORAZIONE") = "1" Then
    '        Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
    '        Exit Sub
    '    End If
    '    Response.Write("<script>window.open(""../impostapw.aspx"",""main"")</script>")
    'End Sub

    'Protected Sub imgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEsci.Click
    '    If Session.Item("LAVORAZIONE") = "1" Then
    '        Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
    '        Exit Sub
    '    End If

    '    par.OracleConn.Open()
    '    Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("UPDATE OPERATORI_WEB SET COLLEGATO='0' WHERE ID=" & Session.Item("ID_OPERATORE"), par.OracleConn)
    '    cmd.ExecuteNonQuery()

    '    cmd.CommandText = "UPDATE OPERATORI_WEB_LOG SET DATA_ORA_OUT='" & Format(Now, "yyyyMMddHHmm") & "' WHERE ID_OPERATORE='" & Session.Item("ID_OPERATORE") & "' AND DATA_ORA_IN='" & Session.Item("DATA_IN") & "'"
    '    cmd.ExecuteNonQuery()

    '    par.OracleConn.Close()

    '    Session.RemoveAll()
    '    Session.Abandon()
    '    Session.Clear()
    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    HttpContext.Current.Session.Abandon()
    '    Call par.ChiudiDB()
    '    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
    'End Sub

    Protected Sub avviso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles avviso.Click
        Response.Write("<script>window.open('../avviso.aspx','Avviso','top=0,left=0,width=400,height=150');</script>")
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Exit Sub
        End If
        Response.Write("<script>top.location.href=""../Chiusura.htm""</script>")
    End Sub
End Class
