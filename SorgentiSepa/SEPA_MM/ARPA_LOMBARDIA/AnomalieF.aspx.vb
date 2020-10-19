
Partial Class ARPA_LOMBARDIA_AnomalieF
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridAnomalie.ClientID.ToString.Replace("ctl00", "MasterOpen")
            Select Case Request.QueryString("T").ToString
                Case "1"
                    lblTitolo.Text = "Anomalia Unità non in Proprietà Edifici"
            End Select
            HFTipoGestione.Value = Request.QueryString("T")
        End If
    End Sub
    Protected Sub RadGridAnomalie_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAnomalie.NeedDataSource
        Try
            Dim Query As String = ""
            Select Case HFTipoGestione.Value.ToString
                Case "1"
                    Query = "SELECT EDIFICI.ID, '<a href=" & Chr(34) & "javascript:ApriAnomalieEdifici(' || EDIFICI.ID || ');" & Chr(34) & ">' || EDIFICI.COD_EDIFICIO || '</a>' AS COD_EDIFICIO, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "'Non è stato inserito in numero di UI per gli edifici non in proprietà' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "EDIFICI, " & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE INDIRIZZI.ID(+) = EDIFICI.ID_INDIRIZZO_PRINCIPALE " _
                          & "AND EDIFICI.ID <> 1 AND EDIFICI.ID IN (" _
                          & "SELECT DISTINCT ID_EDIFICIO FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL') " _
                          & "AND NVL(UNITA_NON_PROPRIETA, -1) = -1 AND EDIFICI.ID IN (SELECT DISTINCT ID_EDIFICIO FROM " & CType(Me.Master, Object).StringaSiscom & "COND_EDIFICI) " _
                          & "ORDER BY EDIFICI.COD_EDIFICIO ASC"
            End Select
            RadGridAnomalie.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Anomalie - RadGridAnomalie_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim Query As String = ""
            Select Case HFTipoGestione.Value.ToString
                Case "1"
                    Query = "SELECT EDIFICI.COD_EDIFICIO, " _
                          & "INDIRIZZI.DESCRIZIONE || ', ' || INDIRIZZI.CIVICO || ' - ' || INDIRIZZI.CAP || ' ' || INDIRIZZI.LOCALITA AS INDIRIZZO, " _
                          & "'Non è stato inserito in numero di UI per gli edifici non in proprietà' AS MOTIVAZIONE_ERRORE, '' AS DATO_ERRORE " _
                          & "FROM " & CType(Me.Master, Object).StringaSiscom & "EDIFICI, " & CType(Me.Master, Object).StringaSiscom & "INDIRIZZI " _
                          & "WHERE INDIRIZZI.ID(+) = EDIFICI.ID_INDIRIZZO_PRINCIPALE " _
                          & "AND EDIFICI.ID <> 1 AND EDIFICI.ID IN (" _
                          & "SELECT DISTINCT ID_EDIFICIO FROM " & CType(Me.Master, Object).StringaSiscom & "UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL') " _
                          & "AND NVL(UNITA_NON_PROPRIETA, 0) = 0 AND EDIFICI.ID IN (SELECT DISTINCT ID_EDIFICIO FROM " & CType(Me.Master, Object).StringaSiscom & "COND_EDIFICI) " _
                          & "ORDER BY EDIFICI.COD_EDIFICIO ASC"
            End Select
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeExcel As String = ""
                Dim nomeWorkSheet As String = ""
                Select Case HFTipoGestione.Value.ToString
                    Case "1"
                        nomeExcel = "ExportAnomalieEdificiNoPropUI"
                        nomeWorkSheet = "AnomalieEdificiNoPropUI"
                End Select
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, nomeExcel, nomeWorkSheet, dt)
                If System.IO.File.Exists(Server.MapPath("../FileTemp/" & nomeFile)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    RadNotificationNote.Text = "Errore durante l'Export. Riprovare!!"
                    RadNotificationNote.Show()
                End If
            Else
                RadNotificationNote.Text = par.Messaggio_NoExport
                RadNotificationNote.Show()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_AnomalieF - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
