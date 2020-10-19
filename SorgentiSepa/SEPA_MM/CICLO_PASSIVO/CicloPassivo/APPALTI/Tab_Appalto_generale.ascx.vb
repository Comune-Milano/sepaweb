' TAB LEGALE DEL FORNITORE FISICO

Imports Telerik.Web.UI

Partial Class Tab_Appalto_generale
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If durata.Value <> "" Then
            DirectCast(Me.Page.FindControl("txtdurata"), TextBox).Text = durata.Value
        End If

        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
                FrmSolaLettura()


            End If
            If CType(Me.Page, Object).vIdAppalti > 0 Then
                If par.IfEmpty(Me.txtImpTotPlusOneriCan.Text, 0) > 0 Then
                    Dim script2 As String = "openModalInRadClose('" + CType(Me.Page.FindControl("modalRadWindow"), RadWindow).ClientID + "', 'ResiduoCanone.aspx?TOTCAN=" & Me.txtImpTotPlusOneriCan.Text.Replace(".", "") & "&ID=" & CType(Me.Page, Object).vIdAppalti & "', 1000, 550);"
                    Me.lblResiduoCan.Text = "<a href=""javascript:" & script2 & "void(0);"">Residuo</a>"
                    'Me.lblResiduoCan.Text = "<a href=""javascript:window.showModalDialog('ResiduoCanone.aspx?TOTCAN=" & Me.txtImpTotPlusOneriCan.Text.Replace(".", "") & "&ID=" & CType(Me.Page, Object).vIdAppalti & "',window, 'dialogWidth:930px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');void(0);"">Res.</a>"
                End If
                If par.IfEmpty(Me.txtImpTotPlusOneriCons.Text, 0) > 0 Then
                    Dim script2 As String = "openModalInRadClose('" + CType(Me.Page.FindControl("modalRadWindow"), RadWindow).ClientID + "', 'ResiduoConsumo.aspx?TOTCON=" & Me.txtImpTotPlusOneriCons.Text.Replace(".", "") & "&ID=" & CType(Me.Page, Object).vIdAppalti & "', 1000, 550);"
                    Me.lblResiduoCons.Text = "<a href=""javascript:" & script2 & "void(0);"">Residuo</a>"
                    ' Me.lblResiduoCons.Text = "<a href=""javascript:window.showModalDialog('ResiduoConsumo.aspx?TOTCON=" & Me.txtImpTotPlusOneriCons.Text.Replace(".", "") & "&ID=" & CType(Me.Page, Object).vIdAppalti & "',window, 'dialogWidth:930px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');void(0);"">Res.</a>"
                End If

            End If
        End If



    End Sub

    Private Sub FrmSolaLettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabAppaltoGenerale"
            'Me.lblErrore.Visible = True
            'lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub txtonericanone_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtonericanone.TextChanged
        CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()
    End Sub

    Protected Sub txtonericonsumo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtonericonsumo.TextChanged
        CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()
    End Sub

    Protected Sub btnPrintPagParz_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrintPagParz.Click
        CType(Me.Page, Object).PdfPagamento(DirectCast(Me.Page.FindControl("idPagRitLegge"), HiddenField).Value)

    End Sub

    Protected Sub btnInfoRitLegge_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnInfoRitLegge.Click

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
            End If

            par.cmd.CommandText = "SELECT SUM(RIT_LEGGE_IVATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & ")))  AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and id_pagamento in (select id from siscom_mi.pagamenti where id=prenotazioni.id_pagamento and id_stato>0)"
            Dim myReaderlotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                txtfondoritenute.Text = Format(par.IfNull(myReaderlotto(0), 0), "#,##0.00")
            End If
            myReaderlotto.Close()

            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try

    End Sub

    Protected Sub btnInfoRitLegge2_Click(sender As Object, e As System.EventArgs) Handles btnInfoRitLegge2.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
            End If

            par.cmd.CommandText = "SELECT SUM(RIT_LEGGE_IVATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTOID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & ")))  AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and id_pagamento in (select id from siscom_mi.pagamenti where id=prenotazioni.id_pagamento and id_stato>0)"
            Dim myReaderlotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                txtfondoritenute.Text = Format(par.IfNull(myReaderlotto(0), 0), "#,##0.00")
            End If
            myReaderlotto.Close()

            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub btnStampaCDP_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampaCDP.Click
        CType(Me.Page, Object).StampaAnticipo()
    End Sub
End Class
