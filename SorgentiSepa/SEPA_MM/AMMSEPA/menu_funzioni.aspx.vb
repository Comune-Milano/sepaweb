Namespace CM

Partial Class menu_funzioni
        Inherits PageSetIdMode
        Dim par As New CM.Global

#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

    Public Function Spegni()

    End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("OPERATORE") = "" Then
                Response.Redirect("~/AccessoNegato.htm", True)
                Exit Sub
            End If
        End Sub

        'Private Sub imgPw_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPw.Click

        '        If Session.Item("LAVORAZIONE") = "1" Then
        '            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
        '            Exit Sub
        '        End If
        '        Response.Write("<script>window.open(""../impostapw.aspx"",""main"")</script>")
        'End Sub

        'Private Sub imgEsci_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEsci.Click
        '        If Session.Item("LAVORAZIONE") = "1" Then
        '            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
        '            Exit Sub
        '        End If

        '        par.OracleConn.Open()
        '        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("UPDATE OPERATORI SET COLLEGATO='0' WHERE ID=" & Session.Item("ID_OPERATORE"), par.OracleConn)
        '        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

        '        'cmd.CommandText = "UPDATE OPERATORI SET COLLEGATO='0' WHERE ID=" & Session.Item("ID_OPERATORE")
        '        cmd.ExecuteNonQuery()

        '        cmd.CommandText = "UPDATE OPERATORI_WEB_LOG SET DATA_ORA_OUT='" & Format(Now, "yyyyMMddHHmm") & "' WHERE ID_OPERATORE='" & Session.Item("ID_OPERATORE") & "' AND DATA_ORA_IN='" & Session.Item("DATA_IN") & "'"
        '        cmd.ExecuteNonQuery()

        '        par.OracleConn.Close()

        '        Session.RemoveAll()
        '        Session.Abandon()
        '        Session.Clear()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '        HttpContext.Current.Session.Abandon()
        '        Call PAR.ChiudiDB()
        '        Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        'End Sub


        'Private Sub avviso_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles avviso.Click

        '        Response.Write("<script>window.open('../avviso.aspx','Avviso','top=0,left=0,width=400,height=150');</script>")

        'End Sub


End Class

End Namespace
