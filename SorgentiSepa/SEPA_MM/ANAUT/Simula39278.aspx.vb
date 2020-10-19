
Partial Class ANAUT_Simula39278
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "select * from utenza_bandi order by id desc", "DESCRIZIONE", "ID")
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblBando.Text = ex.Message
            End Try
        End If
    End Sub


    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim scriptblock As String = "<script language='javascript' type='text/javascript'>" _
                        & "window.open('Simula39278_1.aspx?ID=" & cmbBando.SelectedItem.Value & "','','');" _
                        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
        End If
    End Sub
End Class
