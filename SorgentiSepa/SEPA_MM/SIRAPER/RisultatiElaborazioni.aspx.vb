Imports System.IO

Partial Class SIRAPER_RisultatiElaborazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim SiglaEnte As String = ""
    Dim TipoEnte As String = ""
    Dim DataRiferimetoDa As String = ""
    Dim DataRiferimentoA As String = ""
    Dim CodFiscale As String = ""
    Dim PIva As String = ""
    Dim RagioneSociale As String = ""

    Public Property dataTableRisultatiElaborazioni() As Data.DataTable
        Get
            If Not (ViewState("dataTableRisultatiElaborazioni") Is Nothing) Then
                Return ViewState("dataTableRisultatiElaborazioni")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableRisultatiElaborazioni") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            If Not IsNothing(Session.Item("lstId")) Then
                Session.Remove("lstId")
            End If
            SettaQueryString()
            CercaElaborazione()
        End If
    End Sub
    Private Sub SettaQueryString()
        Try
            If Request.QueryString("SE") <> Nothing Then SiglaEnte = Request.QueryString("SE")
            If Request.QueryString("TE") <> Nothing Then TipoEnte = Request.QueryString("TE")
            If Request.QueryString("DRD") <> Nothing Then DataRiferimetoDa = Request.QueryString("DRD")
            If Request.QueryString("DRA") <> Nothing Then DataRiferimentoA = Request.QueryString("DRA")
            If Request.QueryString("CF") <> Nothing Then CodFiscale = Request.QueryString("CF")
            If Request.QueryString("PI") <> Nothing Then PIva = Request.QueryString("PI")
            If Request.QueryString("RS") <> Nothing Then RagioneSociale = Request.QueryString("RS")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: RisultatiElaborazioni - SettaQueryString - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CercaElaborazione()
        Try
            Dim condizione As String = ""
            Dim sValore As String = ""
            Dim sCompara As String = ""
            par.cmd.CommandText = "SELECT SIRAPER.ID, SIGLA_ENTE, TIPO_ENTE_SIRAPER.DESCRIZIONE AS TIPO_ENTE, COD_FISCALE_ENTE, P_IVA_ENTE, RAG_SOCIALE, TO_CHAR(TO_DATE(DATA_RIFERIMENTO, 'yyyymmdd'),'dd/mm/yyyy') AS DATA_RIFERIMENTO, " _
                                & "ANNO_RIFERIMENTO, TO_CHAR(TO_DATE(DATA_TRASMISSIONE, 'yyyymmdd'),'dd/mm/yyyy') AS DATA_TRASMISSIONE, " _
                                & "SIRAPER_VERSIONI.DESCRIZIONE AS VERSIONE, (SELECT SISCOM_MI.GETDATA(MIN(SUBSTR(DATA_ORA, 1, 8))) FROM SISCOM_MI.SIRAPER_EVENTI WHERE ID_SIRAPER = SIRAPER.ID AND COD_EVENTO = 'S07') AS DATA_ELABORAZIONE " _
                                & "FROM SISCOM_MI.SIRAPER, SISCOM_MI.TIPO_ENTE_SIRAPER, SISCOM_MI.SIRAPER_VERSIONI " _
                                & "WHERE TIPO_ENTE_SIRAPER.COD(+) = SIRAPER.TIPO_ENTE AND SIRAPER_VERSIONI.ID(+) = SIRAPER.ID_SIRAPER_VERSIONE "
            If SiglaEnte <> "" Then
                sValore = SiglaEnte.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                par.cmd.CommandText += "AND SIGLA_ENTE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If TipoEnte <> "" Then
                par.cmd.CommandText += "AND TIPO_ENTE = " & TipoEnte & " "
            End If
            If DataRiferimetoDa <> "" Then
                par.cmd.CommandText += "AND DATA_RIFERIMENTO >= " & DataRiferimetoDa & " "
            End If
            If DataRiferimentoA <> "" Then
                par.cmd.CommandText += "AND DATA_RIFERIMENTO <= " & DataRiferimentoA & " "
            End If
            If CodFiscale <> "" Then
                sValore = CodFiscale.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                par.cmd.CommandText += "AND COD_FISCALE_ENTE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If PIva <> "" Then
                sValore = PIva.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                par.cmd.CommandText += "AND P_IVA_ENTE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If RagioneSociale <> "" Then
                sValore = RagioneSociale.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                par.cmd.CommandText += "AND RAG_SOCIALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            par.cmd.CommandText += "ORDER BY 8 ASC, 6 ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            dataTableRisultatiElaborazioni = New Data.DataTable
            da.Fill(dataTableRisultatiElaborazioni)
            da.Dispose()
            BindGrid()
            Select Case dataTableRisultatiElaborazioni.Rows.Count
                Case 0
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun elaborazione trovato, per i criteri di ricerca definiti!');location.href ='Ricerca.aspx';", True)
                Case 1
                    Response.Redirect("Siraper.aspx?ID=" & dataTableRisultatiElaborazioni.Rows(0).Item("id") & "&IND=0", False)
                Case Else

            End Select
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: RisultatiElaborazioni - CercaElaborazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Me.dgvElaborazioni.DataSource = dataTableRisultatiElaborazioni
            Me.dgvElaborazioni.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: RisultatiElaborazioni - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvElaborazioni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvElaborazioni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('MasterPage_MainContent_txtmia').value='Hai selezionato l\'elaborazione: " & e.Item.Cells(1).Text.Replace("'", "\'") & ", " & e.Item.Cells(2).Text.Replace("'", "\'") & ", " & e.Item.Cells(4).Text.Replace("'", "\'") & "';document.getElementById('MasterPage_MainContent_idSelezionato').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('MasterPage_MainContent_btnVisualizza').click();")
        End If
    End Sub
    Protected Sub btnExportXls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXls.Click
        If dgvElaborazioni.Items.Count > 0 Then
            Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(dataTableRisultatiElaborazioni, Me.dgvElaborazioni, "ExportElaborazioni", , , , False)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "downloadFile('../FileTemp/" & nomefile & "');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun risultato da esportare!');", True)
        End If
    End Sub
    Protected Sub dgvElaborazioni_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvElaborazioni.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvElaborazioni.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
    Protected Sub BtnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnCerca.Click
        Response.Redirect("Ricerca.aspx", False)
    End Sub
    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If par.IfEmpty(idSelezionato.Value, 0) > 0 Then
            Dim condizioni As String = ""
            condizioni &= "&SE=" & Request.QueryString("SE")
            condizioni &= "&TE=" & Request.QueryString("TE")
            condizioni &= "&DRD=" & Request.QueryString("DRD")
            condizioni &= "&DRA=" & Request.QueryString("DRA")
            condizioni &= "&CF=" & Request.QueryString("CF")
            condizioni &= "&PI=" & Request.QueryString("PI")
            condizioni &= "&RS=" & Request.QueryString("RS")
            Response.Redirect("Siraper.aspx?ID=" & idSelezionato.Value & condizioni, False)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Selezionare il fascicolo da visualizzare!');", True)
        End If
    End Sub
End Class
