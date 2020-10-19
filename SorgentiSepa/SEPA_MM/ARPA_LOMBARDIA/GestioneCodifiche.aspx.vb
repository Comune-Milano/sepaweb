Imports Telerik.Web.UI

Partial Class ARPA_LOMBARDIA_GestioneCodifiche
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing

    Public Property TabellaCodifica() As String
        Get
            If Not (ViewState("TabellaCodifica") Is Nothing) Then
                Return CStr(ViewState("TabellaCodifica"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("TabellaCodifica") = value
        End Set
    End Property
    Public Property IdTabellaCodifica() As String
        Get
            If Not (ViewState("IdTabellaCodifica") Is Nothing) Then
                Return CStr(ViewState("IdTabellaCodifica"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("IdTabellaCodifica") = value
        End Set
    End Property
    Public Property DescrizioneTabellaCodifica() As String
        Get
            If Not (ViewState("DescrizioneTabellaCodifica") Is Nothing) Then
                Return CStr(ViewState("DescrizioneTabellaCodifica"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("DescrizioneTabellaCodifica") = value
        End Set
    End Property
    Public Property TabellaGestione() As String
        Get
            If Not (ViewState("TabellaGestione") Is Nothing) Then
                Return CStr(ViewState("TabellaGestione"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("TabellaGestione") = value
        End Set
    End Property
    Public Property WhereQuery() As String
        Get
            If Not (ViewState("WhereQuery") Is Nothing) Then
                Return CStr(ViewState("WhereQuery"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("WhereQuery") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridGestioneCodifiche.ClientID.ToString.Replace("ctl00", "MasterOpen")
            Select Case Request.QueryString("T")
                Case 2
                    lblTitolo.Text = "Tipologia Gestore"
                    TabellaCodifica = CType(Me.Master, Object).StringaSiscom & "TAB_GESTORI_ARCHIVIO"
                    IdTabellaCodifica = "COD"
                    DescrizioneTabellaCodifica = "DESCRIZIONE_PATRIMONIO"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_GESTORE"
                    WhereQuery = "WHERE FL_ARPA = 1"
                Case 3
                    lblTitolo.Text = "Prefisso Indirizzo"
                    TabellaCodifica = CType(Me.Master, Object).StringaSiscom & "TIPOLOGIA_INDIRIZZO"
                    IdTabellaCodifica = "COD"
                    DescrizioneTabellaCodifica = "DESCRIZIONE"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_PREFISSO_INDIRIZZO"
                    WhereQuery = ""
                Case 4
                    lblTitolo.Text = "Tipologia Indirizzo"
                    TabellaCodifica = CType(Me.Master, Object).StringaSiscom & "TIPO_LIVELLO_PIANO"
                    IdTabellaCodifica = "COD"
                    DescrizioneTabellaCodifica = "DESCRIZIONE"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_PIANO"
                    WhereQuery = ""
                Case 6
                    lblTitolo.Text = "Destinazione d'Uso"
                    TabellaCodifica = CType(Me.Master, Object).StringaSiscom & "DESTINAZIONI_USO_RL_UI"
                    IdTabellaCodifica = "ID"
                    DescrizioneTabellaCodifica = "DESCRIZIONE"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_DESTINAZIONI_USO"
                    WhereQuery = ""
                Case 12
                    lblTitolo.Text = "Tipologia Parentela"
                    TabellaCodifica = "T_TIPO_PARENTELA"
                    IdTabellaCodifica = "COD"
                    DescrizioneTabellaCodifica = "DESCRIZIONE"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_PARENTELA"
                    WhereQuery = ""
            End Select
            lblTitolo.Text = "Gestione Codifiche " & lblTitolo.Text
        End If
    End Sub
    Protected Sub RadGridGestioneCodifiche_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridGestioneCodifiche.NeedDataSource
        Try
            Dim Query As String = "SELECT " & IdTabellaCodifica & " AS ID, " & DescrizioneTabellaCodifica & " AS DESCRIZIONE, COD_ARPA " _
                                & "FROM " & TabellaCodifica & " " & WhereQuery
            RadGridGestioneCodifiche.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_GestioneCodifiche - RadGridGestioneCodifiche_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub RadGridGestioneCodifiche_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridGestioneCodifiche.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = e.Item
                par.caricaComboTelerik("SELECT ID, VALORE || ' - ' || DESCRIZIONE AS DESCRIZIONE FROM " & TabellaGestione & " ORDER BY 2 ASC", CType(dataItem.FindControl("ddlVoceGestione"), RadComboBox), "ID", "DESCRIZIONE", True)
                CType(dataItem.FindControl("ddlVoceGestione"), RadComboBox).SelectedValue = dataItem("COD_ARPA").Text.ToString
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_GestioneCodifiche - RadGridGestioneCodifiche_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            For Each item As GridDataItem In RadGridGestioneCodifiche.Items
                par.cmd.CommandText = "UPDATE " & TabellaCodifica & " SET COD_ARPA = " & par.RitornaNullSeMenoUno(CType(item.FindControl("ddlVoceGestione"), RadComboBox).SelectedValue, False) & " WHERE " & IdTabellaCodifica & " = " & par.insDbValue(item("ID").Text.ToString, True)
                par.cmd.ExecuteNonQuery()
            Next
            connData.chiudi(True)
            RadNotificationNote.Text = par.Messaggio_Operazione_Eff
            RadNotificationNote.Show()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_GestioneCodifiche - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
