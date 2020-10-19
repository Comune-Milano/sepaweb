Imports Telerik.Web.UI

Partial Class Contratti_ParametriCodProcessi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)
    End Sub

    Protected Sub RadGridProcessi_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridProcessi.BatchEditCommand
        Try
            connData.apri(True)
            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    par.cmd.CommandText = "UPDATE T_MOTIVO_DOMANDA_VSA SET DESCRIZIONE = " & par.insDbValue(newValues("DESCRIZIONE"), True).ToUpper & ",COD_PROCESSO_KOFAX= " & par.insDbValue(newValues("COD_PROCESSO_KOFAX"), True).ToUpper _
                                        & " WHERE ID = " & par.insDbValue(newValues("ID"), False)
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            RadGridProcessi.Rebind()
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " RadGridProcessi_NeedDataSource - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub RadGridProcessi_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridProcessi.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=1 ORDER BY DESCRIZIONE ASC"
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " RadGridProcessi_NeedDataSource - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "function PaginaHome() {document.location.href = 'pagina_home.aspx';};PaginaHome();", True)
    End Sub
End Class
