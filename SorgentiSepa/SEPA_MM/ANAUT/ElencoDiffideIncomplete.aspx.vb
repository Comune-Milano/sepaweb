Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoDiffideIncomplete
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

            If Request.QueryString("T") = "1" Then
                Label11.Text = "Elenco File Diffide Non Rispondenti"
            Else
                Label11.Text = "Elenco File Diffide Incomplete"
            End If
        End If
        Cerca()
    End Sub

    Private Function Cerca()
        Dim SS As String = ""
        If Session.Item("MOD_AU_ANNULLA_DIFF") = "1" Then

            If cmbBando.SelectedItem.Value <> "TUTTI" Then
                SS = " UTENZA_FILE_DIFFIDE.ID_AU=" & cmbBando.SelectedItem.Value
            End If

            Select Case cmbVisualizza.SelectedItem.Value
                Case "TUTTE"
                    If SS <> "" Then SS = SS & " AND "
                Case "VALIDE"
                    If SS <> "" Then SS = SS & " AND "
                    SS = SS & " UTENZA_FILE_DIFFIDE.data_annullo IS NULL AND "
                Case "ANNULLATE"
                    If SS <> "" Then SS = SS & " AND "
                    SS = SS & " UTENZA_FILE_DIFFIDE.data_annullo IS NOT NULL AND "
            End Select

            Dim tipo As String = ""

            If Request.QueryString("T") = "1" Then
                tipo = "1"
            Else
                tipo = "0"
            End If

            sStringaSQL1 = "SELECT UTENZA_BANDI.descrizione AS BANDO,TO_CHAR (TO_DATE (SUBSTR(DATA_CREAZIONE,1,8), 'YYYYmmdd'),'DD/MM/YYYY') AS CREAZIONE,PROTOCOLLO,CRITERI,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/ANAGRAFE_UTENZA/DIFFIDE/'||NOME_FILE||''','''','''');£>'||NOME_FILE||'</a>','$','&'),'£','" & Chr(34) & "') as NOME_FILE,DATA_ANNULLO,NOTE,(CASE WHEN DATA_ANNULLO IS NULL THEN (REPLACE(REPLACE('<a href=£javascript:void(0)£ onclick=£window.showModalDialog(''AnnullaDiffida.aspx?ID='||UTENZA_FILE_DIFFIDE.ID||''',''window'',''status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no'');Form1.submit();£>'||'ANNULLA'||'</a>','$','&'),'£','" & Chr(34) & "')) ELSE '' END) AS annulla FROM UTENZA_FILE_DIFFIDE,UTENZA_BANDI WHERE " & SS & " UTENZA_BANDI.ID=UTENZA_FILE_DIFFIDE.ID_AU AND TIPO=" & tipo & " ORDER BY UTENZA_BANDI.ID DESC,DATA_CREAZIONE DESC"

        Else
            sStringaSQL1 = "SELECT UTENZA_BANDI.descrizione AS BANDO,TO_CHAR (TO_DATE (SUBSTR(DATA_CREAZIONE,1,8), 'YYYYmmdd'),'DD/MM/YYYY') AS CREAZIONE,PROTOCOLLO,CRITERI,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/ANAGRAFE_UTENZA/DIFFIDE/'||NOME_FILE||''','''','''');£>'||NOME_FILE||'</a>','$','&'),'£','" & Chr(34) & "') as NOME_FILE,DATA_ANNULLO,NOTE,'' as ANNULLA FROM UTENZA_FILE_DIFFIDE,UTENZA_BANDI WHERE UTENZA_BANDI.ID=UTENZA_FILE_DIFFIDE.ID_AU AND TIPO=0 ORDER BY UTENZA_BANDI.ID DESC,DATA_CREAZIONE DESC"
        End If

        BindGrid()
    End Function

    Private Sub BindGrid()

        par.OracleConn.Open()
        Dim dt As New System.Data.DataTable

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_FILE_DIFFIDE")
        da.Fill(dt)

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        '        HttpContext.Current.Session.Add("AA1", dt)

        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

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

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub cmbBando_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        Cerca()
    End Sub

    Protected Sub cmbVisualizza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbVisualizza.SelectedIndexChanged
        Cerca()
    End Sub
End Class
