
Imports Telerik.Web.UI

Partial Class Gestione_locatari_ElencoEventi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If
        Me.connData = New CM.datiConnessione(par, False, False)
    End Sub

    Private Sub dgvEventi_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvEventi.NeedDataSource
        Try
            Dim idDomanda As Long = Request.QueryString("IDDOM")
            par.cmd.CommandText = "select TO_DATE(EVENTI_BANDI_VSA.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                  & " tab_eventi.cod ||' - '|| tab_eventi.descrizione as descrizione,EVENTI_BANDI_VSA.MOTIVAZIONE," _
                  & " OPERATORI.OPERATORE,EVENTI_BANDI_VSA.ID_OPERATORE,CAF_WEB.COD_CAF " _
                  & " from CAF_WEB, EVENTI_BANDI_VSA,TAB_EVENTI,OPERATORI,TAB_STATI " _
                  & " where EVENTI_BANDI_VSA.ID_DOMANDA= " & idDomanda _
                  & " and EVENTI_BANDI_VSA.COD_EVENTO=TAB_EVENTI.COD (+) " _
                  & " and EVENTI_BANDI_VSA.ID_OPERATORE=OPERATORI.ID (+) " _
                  & " AND EVENTI_BANDI_VSA.STATO_PRATICA=TAB_STATI.COD (+)" _
                  & " and CAF_WEB.ID=OPERATORI.ID_CAF " _
                  & " order by EVENTI_BANDI_VSA.DATA_ORA desc,EVENTI_BANDI_VSA.COD_EVENTO desc"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)

            TryCast(sender, RadGrid).DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
