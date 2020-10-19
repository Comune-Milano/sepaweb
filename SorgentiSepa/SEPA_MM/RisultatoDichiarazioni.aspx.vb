Namespace CM

Partial Class RisultatoDichiarazioni
    Inherits PageSetIdMode
    Dim sValorePG As String
    Dim sValoreCF As String
    Dim par As New [Global]()

#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim sCercatoPg As String
            Dim sCercatoCf As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If Not IsPostBack Then
                sValoreCF = Request.QueryString("CF")
                sValorePG = Request.QueryString("PG")
                If sValoreCF <> "" Then
                    sCercatoCf = sValoreCF
                Else
                    sCercatoCf = "TUTTI"
                End If
                If sValorePG <> "" Then
                    sCercatoPg = sValorePG
                Else
                    sCercatoPg = "TUTTI"
                End If
                Label1.Text = "Hai cercato: Protocollo " & sCercatoPg & " - Cod. Fiscale " & sCercatoCf
                LBLID.Text = "-1"
                Cerca()
            End If
        End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String



        bTrovato = False
        sStringaSql = ""
        If sValoreCF <> "" Then
            sValore = sValoreCF
            bTrovato = True
            sStringaSql = " COMP_NUCLEO.COD_FISCALE='" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValorePG
            bTrovato = True
            sStringaSql = " DICHIARAZIONI.PG='" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If

        If sStringaSql <> "" Then
            sStringaSql = sStringaSql & " AND "
        End If

            sStringaSql = "SELECT DICHIARAZIONI.ID,COMP_NUCLEO.PROGR," _
                       & "DICHIARAZIONI.PG,TO_CHAR(TO_DATE(DICHIARAZIONI.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
                       & "COMP_NUCLEO.COD_FISCALE," _
                       & "COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,COMP_NUCLEO.DATA_NASCITA " _
                       & "FROM DICHIARAZIONI,COMP_NUCLEO WHERE " & sStringaSql & " COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                       & " AND DICHIARAZIONI.ID_STATO=1 AND " _
                       & "DICHIARAZIONI.ID_CAF = " & Session.Item("ID_CAF") _
                       & " AND DICHIARAZIONI.ID NOT IN (SELECT ID_DICHIARAZIONE FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE IS NOT NULL) " _
                       & " ORDER BY DICHIARAZIONI.PG ASC,COMP_NUCLEO.PROGR ASC"

        BindGrid()
    End Function

        Public Property sStringaSQL() As String
            Get
                If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                    Return CStr(ViewState("par_sStringaSQL1"))
                Else
                    Return ""
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_sStringaSQL1") = value
            End Set

        End Property

    Private Sub BindGrid()

            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DICHIARAZIONI,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label4.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            da.Dispose()
            ds.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
            e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub




    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand

            If par.RicavaEta(e.Item.Cells(5).Text) >= 18 Then
                LBLID.Text = e.Item.Cells(0).Text
                LBLPROGR.Text = e.Item.Cells(1).Text
                Label2.Text = "Hai selezionato: " & e.Item.Cells(2).Text & " - " & e.Item.Cells(3).Text & " " & e.Item.Cells(4).Text
            Else
                LBLID.Text = "-1"
                Label2.Text = "Il componente selezionato non ha raggiunto la maggiore età!"
            End If
        End Sub


        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        End Sub

        Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button3.Click
            If LBLID.Text = "-1" Or LBLID.Text = "" Then
                Response.Write("<script>alert('Nessuna dichiarazione selezionata!')</script>")
            Else
                Response.Redirect("domanda.aspx?ID=-1&ID1=" & LBLID.Text & "&PROGR=" & LBLPROGR.Text)
            End If
        End Sub
    End Class

End Namespace
