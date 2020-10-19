
Partial Class ANAUT_com_spese
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        If txtImporto.Text = "" Then
            L2.Visible = True
        Else
            L2.Visible = False
        End If

        If IsNumeric(txtImporto.Text) = False Then
            L2.Visible = True
        Else
            If Val(txtImporto.Text) > 10000 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore superiore a 10.000 Euro)"
            End If
        End If

        If InStr(txtImporto.Text, ".") = 0 Then
            L2.Visible = False
            If InStr(txtImporto.Text, ",") = 0 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore interi)"
            End If
        Else
            L2.Visible = True
            L2.Text = "(Valore interi)"
        End If

        If L2.Visible = True Then
            Exit Sub
        End If

        ModificaSpese()
        
    End Sub

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack = True Then
            vIdConnessione = Request.QueryString("IDCONN")
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))
            txtComponente.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CM"), 1, 52))
            txtImporto.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("IM"), 1, 6))
            If txtImporto.Text = "" Then txtImporto.Text = "0"
            txtDescrizione.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DS"), 1, 17))
            txtDescrizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If

        SettaControlModifiche(Me)

    End Sub


    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBoxList Then
                DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
    End Sub

    Private Sub ModificaSpese()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            par.cmd.CommandText = "UPDATE UTENZA_COMP_ELENCO_SPESE SET IMPORTO=" & par.VirgoleInPunti(par.IfEmpty(txtImporto.Text, 0)) & ",DESCRIZIONE='" & par.PulisciStrSql(txtDescrizione.Text) & "' WHERE ID=" & txtRiga.Text
            par.cmd.ExecuteNonQuery()

            salvaSpese.Value = "1"
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Commit()
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaSpese.Value & ");", True)

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ModificaSpese" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
