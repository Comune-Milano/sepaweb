Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RptMorEmesse
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaTabella()
        End If
    End Sub
    Private Sub CaricaTabella()
        Try
            Dim idCondomino As Integer = par.IfEmpty(Request.QueryString("IDCOND"), 0)
            Dim DataDal As String = par.IfEmpty(Request.QueryString("DAL"), "0")
            Dim DataAl As String = par.IfEmpty(Request.QueryString("AL"), "0")
            Dim Condizioni As String = ""
            Dim dt As New Data.DataTable
            Dim TOTMOROSITA As Decimal
            Dim TOTMOREMESSO As Decimal
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If idCondomino > 0 Then
                Condizioni = " and id_condominio = " & idCondomino
            End If
            If DataDal <> "0" Then
                Condizioni = Condizioni & " and rif_da >= " & par.AggiustaData(DataDal)
            End If
            If DataAl <> "0" Then
                Condizioni = Condizioni & " and rif_a <=" & par.AggiustaData(DataAl)
            End If
            par.cmd.CommandText = "SELECT DISTINCT cond_morosita.ID, id_condominio,condomini.denominazione,condomini.denominazione as condominio, " _
                                & "TO_CHAR(TO_DATE(rif_da,'yyyymmdd'),'dd/mm/yyyy') AS rif_da,TO_CHAR(TO_DATE(rif_a,'yyyymmdd'),'dd/mm/yyyy') AS rif_a,id_amministratore,(cognome||' '||nome ) AS amministratore, " _
                                & "TO_CHAR(SUM(nvl(bol_bollette.importo_totale,0)),'9G999G999G999G999G990D99') AS bollettato,TO_CHAR(SUM(nvl(cond_morosita_inquilini.importo,0)),'9G999G999G999G999G990D99')  AS morosita " _
                                & "FROM siscom_mi.bol_bollette,siscom_mi.cond_morosita, siscom_mi.condomini,siscom_mi.cond_amministratori,siscom_mi.cond_morosita_lettere,siscom_mi.cond_morosita_inquilini " _
                                & "WHERE  condomini.ID = id_condominio " & Condizioni & " AND " _
                                & "cond_amministratori.ID = id_amministratore AND " _
                                & "cond_morosita_lettere.id_morosita = cond_morosita.ID AND " _
                                & "cond_morosita_inquilini.id_morosita = cond_morosita_lettere.id_morosita AND " _
                                & "cond_morosita_inquilini.id_intestatario = cond_morosita_lettere.id_anagrafica AND " _
                                & "bol_bollette.rif_bollettino = cond_morosita_lettere.bollettino and " _
                                & "cond_morosita_lettere.bollettino IS NOT NULL " _
                                & "GROUP BY cond_morosita.ID,id_condominio,condomini.denominazione,rif_da,rif_a,id_amministratore,cognome,nome " _
                                & "ORDER BY rif_da ASC, rif_a ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            Dim row As Data.DataRow
            For Each row In dt.Rows
                row.Item("DENOMINAZIONE") = "<a href=""javascript:parent.main.location.replace('Condominio.aspx?IdCond=" & row.Item("ID_CONDOMINIO") & "&CALL=RptMorEmesse&IDC=" & Request.QueryString("IDCOND") & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "');"">" & row.Item("DENOMINAZIONE") & "</a>"
                TOTMOROSITA = TOTMOROSITA + CDec(par.IfEmpty(row.Item("MOROSITA"), 0))
                TOTMOREMESSO = TOTMOREMESSO + CDec(par.IfEmpty(row.Item("BOLLETTATO"), 0))
            Next
            row = dt.NewRow()
            row.Item("DENOMINAZIONE") = "TOTALE"
            row.Item("MOROSITA") = Format(TOTMOROSITA, "##,##0.00")
            row.Item("BOLLETTATO") = Format(TOTMOREMESSO, "##,##0.00")
            dt.Rows.Add(row)
            DgvMorEmesse.DataSource = dt
            DgvMorEmesse.DataBind()
            Session.Add("DTMOR", dt)
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If DgvMorEmesse.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DgvMorEmesse, "ExportMorEmesse", , , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Non è possibile esportare nessun dato.');</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("DTMOR")
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub
End Class
