Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class AMMSEPA_RisultatoMavErrori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim datadal As String
    Dim dataal As String
    Dim nfile As String
    'Dim bolletta As String
    Dim sStringaSql As String
    Dim dt As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()

            datadal = UCase(Request.QueryString("DAL"))
            dataal = UCase(Request.QueryString("AL"))
            nfile = UCase(Request.QueryString("FI"))
            'bolletta = UCase(Request.QueryString("BO"))
            Cerca()
            Response.Flush()
        End If
    End Sub

    Private Sub Cerca(Optional esporta As Boolean = False)
        Try
            Dim bTrovato As Boolean
            'Dim sValore As String
            Dim sCompara As String


            bTrovato = False
            sStringaSql = ""


            If datadal <> "" Then
                sCompara = " >= "
                bTrovato = True
                sStringaSql = sStringaSql & "SUBSTR(SISCOM_MI.rendiconto_err.data_log,1,8)" & sCompara & " '" & datadal & "' "
                ' sStringaSql = sStringaSql & " SISCOM_MI.rendiconto_err.DATA_ORA " & sCompara & " '" & par.PulisciStrSql(dataal) & "%' "
            End If

            If dataal <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sCompara = " <= "
                bTrovato = True
                sStringaSql = sStringaSql & "SUBSTR(SISCOM_MI.rendiconto_err.data_log,1,8)" & sCompara & " '" & dataal & "' "

            End If

            If nfile <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sCompara = " LIKE "
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(SISCOM_MI.rendiconto_err.file) " & sCompara & " '%" & par.PulisciStrSql(nfile) & "%' "
            End If


            'If bolletta <> "" Then
            '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            '    sCompara = " = "

            '    bTrovato = True
            '    sStringaSql = sStringaSql & " SISCOM_MI.rendiconto_err.ID_BOLLETTA " & sCompara & " '" & par.PulisciStrSql(bolletta) & "' "
            'End If




            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(SUBSTR(SISCOM_MI.rendiconto_err.data_log,1,8),'YYYYmmdd'),'DD/MM/YYYY')||' '||SUBSTR(SISCOM_MI.rendiconto_err.ora_log,1,2)||':'||SUBSTR(SISCOM_MI.rendiconto_err.ora_log,2,2)||':'||SUBSTR(SISCOM_MI.rendiconto_err.ora_log,4,2) AS ""DATA_ORA"", SISCOM_MI.rendiconto_err.nome_file AS ""FILE"", SISCOM_MI.rendiconto_err.log as DESCRIZIONE FROM SISCOM_MI.rendiconto_err"


            If sStringaSql <> "" Then
                sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql
            End If
            sStringaSQL1 = sStringaSQL1 & " ORDER BY data_log DESC"

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = sStringaSQL1
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If esporta = True Then
                If dt.Rows.Count > 0 Then
                    Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(dt, DataGrid1, "ExportLogErrori", 90 / 100, , , False)
                    If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                        Response.Redirect("..\/FileTemp\/" & nomefile, False)
                    Else
                        Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                    End If
                Else
                    Response.Write("<script>alert('Nessun dato da esportare!');</script>")
                End If

            Else
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()

            End If

            Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
            par.OracleConn.Close()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try

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

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        'LBLID.Text = e.Item.Cells(0).Text
        'Label2.Text = "Hai selezionato: " & e.Item.Cells(1).Text & " " & e.Item.Cells(2).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""MavErrori.aspx""</script>")
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

        Try
            Cerca(True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try

        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub
End Class
