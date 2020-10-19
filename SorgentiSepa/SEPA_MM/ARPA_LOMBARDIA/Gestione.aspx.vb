
Partial Class ARPA_LOMBARDIA_Gestione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim lock As New SepacomLock

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridGestione.ClientID.ToString.Replace("ctl00", "MasterHomePage")
            Select Case Request.QueryString("T")
                Case 1
                    lblTitolo.Text = "Tipo Propriet&agrave; Fabbricato"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_TIPO_PROPRIETA_FABBRICATO"
                Case 2
                    lblTitolo.Text = "Tipologia Gestore"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_GESTORE"
                    btnGestione.Visible = True
                Case 3
                    lblTitolo.Text = "Prefisso Indirizzo"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_PREFISSO_INDIRIZZO"
                    btnGestione.Visible = True
                Case 4
                    lblTitolo.Text = "Tipologia Piano"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_PIANO"
                    btnGestione.Visible = True
                Case 5
                    lblTitolo.Text = "Sì/No"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_SINO"
                Case 6
                    lblTitolo.Text = "Destinazioni d'Uso"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_DESTINAZIONI_USO"
                    btnGestione.Visible = True
                Case 7
                    lblTitolo.Text = "Stato dell'Unit&agrave;"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_STATO_UNITA"
                Case 8
                    lblTitolo.Text = "Tipologia Soggetto Occupante"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_TIPO_SOGGETTO_OCCUPANTE"
                Case 9
                    lblTitolo.Text = "Area ISEE"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_AREA_ISEE"
                Case 10
                    lblTitolo.Text = "Fascia Area ISEE"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_FASCIA_ISEE"
                Case 11
                    lblTitolo.Text = "Sesso"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_SESSO"
                Case 12
                    lblTitolo.Text = "Tipologia Parentela"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_PARENTELA"
                    btnGestione.Visible = True
                Case 13
                    lblTitolo.Text = "Condizione Lavorativa"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_COND_LAVORO"
                Case 14
                    lblTitolo.Text = "Nucleo Familiare"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_NUCLEO_FAMILIARE"
                Case 15
                    lblTitolo.Text = "Cittadinanza"
                    TabellaGestione = CType(Me.Master, Object).StringaSiscom & "ARPA_CITTADINANZA"
            End Select
            lblTitolo.Text = "Gestione " & lblTitolo.Text
            HFTipoGestione.Value = Request.QueryString("T")
        End If
    End Sub
    Protected Sub RadGridGestione_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridGestione.NeedDataSource
        Try
            Dim Query As String = "SELECT ID, VALORE, DESCRIZIONE, to_number(regexp_substr(VALORE, '\d+')) AS VALORE_ORDER " _
                                & "FROM " & TabellaGestione
            RadGridGestione.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Gestione - RadGridGestione_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
