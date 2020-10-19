
Partial Class CAMBI_RisultatiDichiarazioni
    Inherits PageSetIdMode
    Dim sValorePG As String
    Dim sValoreCF As String
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sCercatoPg As String
        Dim sCercatoCf As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

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

    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String



        bTrovato = False
        sStringaSQL = ""
        If sValoreCF <> "" Then
            sValore = sValoreCF
            bTrovato = True
            sStringaSQL = " COMP_NUCLEO_cambi.COD_FISCALE='" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSQL = sStringaSQL & " AND "
            sValore = sValorePG
            bTrovato = True
            sStringaSQL = " DICHIARAZIONI_cambi.PG='" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If

        If sStringaSQL <> "" Then
            sStringaSQL = sStringaSQL & " AND "
        End If

        sStringaSQL = "SELECT DICHIARAZIONI_cambi.ID,COMP_NUCLEO_cambi.PROGR," _
                   & "DICHIARAZIONI_cambi.PG,TO_CHAR(TO_DATE(DICHIARAZIONI_cambi.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
                   & "COMP_NUCLEO_cambi.COD_FISCALE," _
                   & "COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,COMP_NUCLEO_cambi.DATA_NASCITA " _
                   & "FROM DICHIARAZIONI_cambi,COMP_NUCLEO_cambi WHERE " & sStringaSQL & " COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID " _
                   & " AND DICHIARAZIONI_cambi.ID_STATO=1 AND " _
                   & "DICHIARAZIONI_cambi.ID_CAF = " & Session.Item("ID_CAF") _
                   & " AND DICHIARAZIONI_cambi.ID NOT IN (SELECT ID_DICHIARAZIONE FROM DOMANDE_BANDO_cambi WHERE ID_DICHIARAZIONE IS NOT NULL) " _
                   & " ORDER BY DICHIARAZIONI_cambi.PG ASC,COMP_NUCLEO_cambi.PROGR ASC"

        BindGrid()
    End Sub

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
        da.Fill(ds, "DICHIARAZIONI_cambi,COMP_NUCLEO_cambi")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label7.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        da.Dispose()
        ds.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        If par.RicavaEta(e.Item.Cells(5).Text) >= 18 Then
            LBLID.Text = e.Item.Cells(0).Text
            LBLPROGR.Text = e.Item.Cells(1).Text
            Label2.Text = "Hai selezionato: " & e.Item.Cells(2).Text & " - " & e.Item.Cells(3).Text & " " & e.Item.Cells(4).Text
        Else
            LBLID.Text = "-1"
            Label2.Text = "Il componente selezionato non ha raggiunto la maggiore età!"
        End If
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button3.Click
        Dim scriptblock As String = ""

        If LBLID.Text = "-1" Or LBLID.Text = "" Then
            Response.Write("<script>alert('Nessuna dichiarazione selezionata!')</script>")
        Else
            'Response.Redirect("domanda.aspx?ID=-1&ID1=" & LBLID.Text & "&PROGR=" & LBLPROGR.Text)
            scriptblock = "<script language='javascript' type='text/javascript'>" _
           & "location.replace('domanda.aspx?ID=-1&ID1=" & LBLID.Text & "&PROGR=" & LBLPROGR.Text & "');" _
           & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
            End If
        End If
    End Sub


End Class
