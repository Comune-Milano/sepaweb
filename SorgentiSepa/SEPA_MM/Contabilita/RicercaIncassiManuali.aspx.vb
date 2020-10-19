
Partial Class Contabilita_RicercaIncassiManuali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        caricamentoInCorso()
        If Not IsPostBack Then
            Response.Flush()
            CaricaTipologie()
        End If
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        aggiungiFunzioniJavascript()
    End Sub
    Private Sub aggiungiFunzioniJavascript()
        For Each Controllo As Control In Me.form1.Controls
            If TypeOf Controllo Is TextBox Then
                Select Case DirectCast(Controllo, TextBox).Attributes("class")
                    Case "numero"
                        DirectCast(Controllo, TextBox).Attributes.Add("onKeyPress", "javascript:SostPuntVirg(event,this);")
                        DirectCast(Controllo, TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                    Case "data"
                        DirectCast(Controllo, TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    Case Else
                End Select
            End If
        Next
    End Sub
    Protected Sub chiudiConnessione()
        If Not IsNothing(par.cmd) Then
            par.cmd.Dispose()
        End If
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirectHome", "parent.main.location.replace('pagina_home.aspx')", True)
    End Sub
    Protected Sub ImageButtonRicerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRicerca.Click
        Dim parametriRicerca As String = ""
        parametriRicerca &= "nominativo=" & TextBoxNominativo.Text
        parametriRicerca &= "&incassatoDa=" & TextBoxImportoIncassatoDa.Text
        parametriRicerca &= "&incassatoA=" & TextBoxImportoIncassatoA.Text
        parametriRicerca &= "&causale=" & TextBoxCausale.Text
        parametriRicerca &= "&dataIncassoDa=" & TextBoxDataIncassoDa.Text
        parametriRicerca &= "&dataIncassoA=" & TextBoxDataIncassoA.Text
        parametriRicerca &= "&Tipologia=" & cmbTipoPagamento.SelectedValue
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirectRicerca", "parent.main.location.replace('RisultatiRicercaIncassiManuali.aspx?" & parametriRicerca & "')", True)
    End Sub
    Private Sub caricamentoInCorso()
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
             & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../NuoveImm/SfondoMascheraContratti2.jpg');z-index:1000;"">" _
             & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
             & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
             & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
             & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
             & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
             & "</td></tr></table></div></div>"
        Response.Write(Loading)
    End Sub
    Private Sub CaricaTipologie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT -1 as ID,'---' AS DESCRIZIONE FROM DUAL UNION SELECT ID, DESCRIZIONE FROM SISCOM_MI.TIPO_PAG_PARZ ORDER BY DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            cmbTipoPagamento.DataSource = dt
            cmbTipoPagamento.DataTextField = "DESCRIZIONE"
            cmbTipoPagamento.DataValueField = "ID"
            cmbTipoPagamento.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "errLoad", "alert('Si è verificato un errore durante il caricamento della pagina!')", True)
        End Try
    End Sub
End Class
