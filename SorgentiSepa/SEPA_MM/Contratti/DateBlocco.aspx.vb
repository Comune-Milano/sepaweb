
Partial Class Contratti_DateBlocco
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
                & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
                & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
                & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
                & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
                & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
                & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
                & "</td></tr></table></div></div>"
            Dim str As String = "<div align='center' id='dvvvPre' style='position: absolute; background-color: #ffffff; width: 200px; height: 100px; top: 200px; left: 300px; z-index: 10; border: 1px dashed #660000; font-size: 10px;'> <br /> <img src='Immagini/load.gif' alt='Caricamento in corso' /><br />Caricamento in corso...<br />  <div align=""left"" id=""AA""  style=""background-color: #FFFFFF; border: 1px solid #000000; width: 100px; font-size: 8pt; color: #000080; text-align: left;"">  <img alt='' src=""barra.gif"" id=""barra"" height=""10"" width=""100"" /></div> </div> <br /> <script language=""javascript"" type=""text/javascript"">        var tempo; tempo = 0; function Mostra() { document.getElementById('barra').style.width = tempo + 'px'; } setInterval(""Mostra()"", 2000);</script>"
            Response.Write(Loading)
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                Response.Flush()
                TextBoxDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                TextBoxDataEmissione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                caricaDateDiBlocco()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Page_Load - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try

    End Sub

    Private Sub caricaDateDiBlocco()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=47"
            TextBoxDataEmissione.Text = par.FormattaData(par.cmd.ExecuteScalar)
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=48"
            TextBoxDataPagamento.Text = par.FormattaData(par.cmd.ExecuteScalar)
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " caricaDateDiBlocco - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub ButtonApplicaModifiche_Click(sender As Object, e As System.EventArgs) Handles ButtonApplicaModifiche.Click
        Try
            connData.apri(True)
            par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE=" & par.FormatoDataDB(TextBoxDataEmissione.Text) & " WHERE ID=47"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE=" & par.FormatoDataDB(TextBoxDataPagamento.Text) & " WHERE ID=48"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Successo", "alert('Date di blocco impostate correttamente!');location.replace('DateBlocco.aspx');", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " ButtonApplicaModifiche_Click - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub ButtonEsci_Click(sender As Object, e As System.EventArgs) Handles ButtonEsci.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('pagina_home.aspx');", True)
    End Sub
End Class
