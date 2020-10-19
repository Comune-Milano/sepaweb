Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoFileFronteSpizi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            cmbBando.Items.Add(New ListItem("TUTTI", "TUTTI"))
            par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi ORDER BY id desc", "DESCRIZIONE", "ID")
        End If
        Cerca()
    End Sub

    Private Function Cerca()
        If cmbBando.SelectedItem.Value <> "TUTTI" Then
            sStringaSQL1 = "select PROCESSO,TO_CHAR (TO_DATE (SUBSTR(INSERITE_DA,1,8), 'YYYYmmdd'),'DD/MM/YYYY') AS INSERITE_DA,TO_CHAR (TO_DATE (SUBSTR(INSERITE_A,1,8), 'YYYYmmdd'),'DD/MM/YYYY') AS INSERITE_A,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/ANAGRAFE_UTENZA/'||NOME_FILE||''','''','''');£>'||NOME_FILE||'</a>','$','&'),'£','" & Chr(34) & "') as NOME_FILE,NOTE from UTENZA_DICHIARAZIONI_F_SPIZIO WHERE ID_AU=" & cmbBando.SelectedItem.Value & " ORDER BY NOME_FILE DESC"
        Else
            sStringaSQL1 = "select PROCESSO,TO_CHAR (TO_DATE (SUBSTR(INSERITE_DA,1,8), 'YYYYmmdd'),'DD/MM/YYYY') AS INSERITE_DA,TO_CHAR (TO_DATE (SUBSTR(INSERITE_A,1,8), 'YYYYmmdd'),'DD/MM/YYYY') AS INSERITE_A,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/ANAGRAFE_UTENZA/'||NOME_FILE||''','''','''');£>'||NOME_FILE||'</a>','$','&'),'£','" & Chr(34) & "') as NOME_FILE,NOTE from UTENZA_DICHIARAZIONI_F_SPIZIO ORDER BY NOME_FILE DESC"
        End If

        BindGrid()
    End Function

    Private Sub BindGrid()

        par.OracleConn.Open()
        Dim dt As New System.Data.DataTable

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_DICHIARAZIONI_F_SPIZIO")
        da.Fill(dt)

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Public Property sStringaSQL1() As String
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

    Protected Sub cmbBando_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        Cerca()
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
