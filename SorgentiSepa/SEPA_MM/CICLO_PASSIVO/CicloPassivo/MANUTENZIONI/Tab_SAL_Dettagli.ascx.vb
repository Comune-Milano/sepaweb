'DETTAGLI SAL

Imports System.Collections
Imports Telerik.Web.UI

Partial Class Tab_SAL_Dettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub DataGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            Dim dt As Data.DataTable = Session("dtDettagli")


            TryCast(sender, RadGrid).DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
