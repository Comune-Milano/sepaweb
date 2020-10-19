
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_CustodiAnomalie
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            CaricaAnomalie()

        End If

    End Sub
    Private Sub CaricaAnomalie()
        connData.apri(False)
        Dim CondFileName As String = ""
        If Not IsNothing(Request.QueryString("NOME_FILE")) Then
            CondFileName = " AND NOME_FILE = '" & par.PulisciStrSql(Request.QueryString("NOME_FILE")) & "'"
            Me.lblTitolo.Text = "ANOMALIE CARICAMENTO FILE CUSTODI - " & Request.QueryString("NOME_FILE")
        Else
            Me.lblTitolo.Text = "ANOMALIE CARICAMENTO FILE CUSTODI"
        End If
        par.cmd.CommandText = "SELECT  TIPO_UTENZE.descrizione AS tipo_utenza ,FORNITORI.ragione_sociale AS fornitore, NOME_FILE, ANNO, MESE,COD_CUSTODE,NOTE, " _
                            & "Getdata(data_caricamento) AS data_caricamento,data_caricamento AS ordCaricamento " _
                            & "FROM siscom_mi.PAGAMENTI_CUSTODI_ANOMALIE,siscom_mi.TIPO_UTENZE,siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                            & "WHERE PAGAMENTI_UTENZE_VOCI.ID = ID_PARAM_UTENZA AND FORNITORI.ID = id_fornitore AND TIPO_UTENZE.ID = id_tipo_utenza " _
                            & "ORDER BY ordCaricamento DESC, nome_file ASC "
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        Session.Add("dtExport", dt)
        Me.dgvAnomalie.DataSource = dt
        Me.dgvAnomalie.DataBind()

        connData.chiudi(False)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "FtUtenze", "Export", CType(Session.Item("dtExport"), Data.DataTable))
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
        End If

    End Sub
End Class
