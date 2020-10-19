Imports Telerik.Web.UI
Imports System.Data

Partial Class FSA_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String

    Dim vIdManutenzione As String

    'Dim sValoreComplesso As String
    'Dim sValoreEdificio As String
    'Dim sValoreImpianto As String
    'Dim sVerifiche As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                '' lblTitolo.Text = "ELENCO EVENTI MANUTENZIONE"

                'vIdManutenzione = Request.QueryString("ID_MANUTENZIONE")

                ''sValoreComplesso = Request.QueryString("CO")
                ''sValoreEdificio = Request.QueryString("ED")
                ''sValoreImpianto = Request.QueryString("IM")
                ''sVerifiche = Request.QueryString("VER")


                'sStringaSql = "select TO_DATE(SISCOM_MI.EVENTI_MANUTENZIONE.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                '                  & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE,SISCOM_MI.EVENTI_MANUTENZIONE.COD_EVENTO,SISCOM_MI.EVENTI_MANUTENZIONE.MOTIVAZIONE," _
                '                  & " SEPA.OPERATORI.OPERATORE,SISCOM_MI.EVENTI_MANUTENZIONE.ID_OPERATORE,SEPA.CAF_WEB.COD_CAF " _
                '           & " from SEPA.CAF_WEB, SISCOM_MI.EVENTI_MANUTENZIONE, SISCOM_MI.TAB_EVENTI,SEPA.OPERATORI " _
                '           & " where SISCOM_MI.EVENTI_MANUTENZIONE.ID_MANUTENZIONE= " & vIdManutenzione _
                '             & " and SISCOM_MI.EVENTI_MANUTENZIONE.COD_EVENTO=SISCOM_MI.TAB_EVENTI.COD (+) " _
                '             & " and SISCOM_MI.EVENTI_MANUTENZIONE.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
                '             & " and SEPA.CAF_WEB.ID=SEPA.OPERATORI.ID_CAF " _
                '          & " order by EVENTI_MANUTENZIONE.DATA_ORA desc, EVENTI_MANUTENZIONE.COD_EVENTO desc"


                'par.OracleConn.Open()
                'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                ''lblTotale.Text = "0"
                ''Do While myReader.Read()
                ''    lblTotale.Text = CInt(lblTotale.Text) + 1
                ''Loop

                ''lblTotale.Text = "TOTALE EVENTI TROVATI: " & lblTotale.Text
                ''myReader.Close()

                ''*** CARICO LA GRIGLIA
                'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                'Dim ds As New Data.DataSet()
                'da.Fill(ds)
                'DataGrid1.DataSource = ds
                'DataGrid1.DataBind()
                'ds.Dispose()
                ''*******************************


                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'par.OracleConn.Dispose()

                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If

    End Sub

    Private Sub Visualizza(ByVal IdImpianto As Long, ByVal Codice As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            Response.Write("Impianti, CODICE: <strong>" & Codice & "</strong><br />")
            Response.Write("<br />")
            'End If
            'myReader.Close()

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DESCRIZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>ENTE</strong></span></td>")

            Response.Write("</tr>")

            par.cmd.CommandText = "select TO_CHAR(TO_DATE(EVENTI_IMPIANTI.DATA_ORA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_ORA""," _
                                      & " TAB_EVENTI.DESCRIZIONE,EVENTI_IMPIANTI.COD_EVENTO,EVENTI_IMPIANTI.MOTIVAZIONE," _
                                      & " OPERATORI.OPERATORE,EVENTI_IMPIANTI.ID_OPERATORE,CAF_WEB.COD_CAF " _
                               & " from CAF_WEB, siscom_mi.EVENTI_IMPIANTI, siscom_mi.TAB_EVENTI,SEPA.OPERATORI " _
                               & " where EVENTI_IMPIANTI.ID_IMPIANTO= " & IdImpianto _
                                 & " and EVENTI_IMPIANTI.COD_EVENTO=TAB_EVENTI.COD (+) " _
                                 & " and EVENTI_IMPIANTI.ID_OPERATORE=OPERATORI.ID (+) " _
                                 & " and CAF_WEB.ID=OPERATORI.ID_CAF " _
                              & " order by EVENTI_IMPIANTI.DATA_ORA desc"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myReader.Read()

                MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
                If IsDate(MiaData) = True Then
                    MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
                Else
                    MiaData = ""
                End If

                OPERATORE = par.IfNull(myReader("OPERATORE"), "")

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_EVENTO"), "") & " - " & par.IfNull(myReader("DESCRIZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_CAF"), "") & "</span></td>")

                Response.Write("</tr>")

            Loop
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Eventi", "Eventi", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub DataGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            vIdManutenzione = Request.QueryString("ID_MANUTENZIONE")

            'sValoreComplesso = Request.QueryString("CO")
            'sValoreEdificio = Request.QueryString("ED")
            'sValoreImpianto = Request.QueryString("IM")
            'sVerifiche = Request.QueryString("VER")


            sStringaSql = "select TO_DATE(SISCOM_MI.EVENTI_MANUTENZIONE.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                              & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE,SISCOM_MI.EVENTI_MANUTENZIONE.COD_EVENTO,SISCOM_MI.EVENTI_MANUTENZIONE.MOTIVAZIONE," _
                              & " SEPA.OPERATORI.OPERATORE,SISCOM_MI.EVENTI_MANUTENZIONE.ID_OPERATORE,SEPA.CAF_WEB.COD_CAF " _
                       & " from SEPA.CAF_WEB, SISCOM_MI.EVENTI_MANUTENZIONE, SISCOM_MI.TAB_EVENTI,SEPA.OPERATORI " _
                       & " where SISCOM_MI.EVENTI_MANUTENZIONE.ID_MANUTENZIONE= " & vIdManutenzione _
                         & " and SISCOM_MI.EVENTI_MANUTENZIONE.COD_EVENTO=SISCOM_MI.TAB_EVENTI.COD (+) " _
                         & " and SISCOM_MI.EVENTI_MANUTENZIONE.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
                         & " and SEPA.CAF_WEB.ID=SEPA.OPERATORI.ID_CAF " _
                      & " order by EVENTI_MANUTENZIONE.DATA_ORA desc, EVENTI_MANUTENZIONE.COD_EVENTO desc"
            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
        Catch ex As Exception

        End Try
    End Sub
End Class
