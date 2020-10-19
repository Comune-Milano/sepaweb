Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ApplicazioneCanoniDettagli
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property lId() As Long
        Get
            If Not (ViewState("par_lId") Is Nothing) Then
                Return CLng(ViewState("par_lId"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lId") = value
        End Set
    End Property

    Public Property lPROVENIENZA() As Long
        Get
            If Not (ViewState("par_lPROVENIENZA") Is Nothing) Then
                Return CLng(ViewState("par_lPROVENIENZA"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lPROVENIENZA") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If



        Dim str As String = "<div id=""dvvvPre"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
             & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
             & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
             & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
             & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
             & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
             & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
             & "</td></tr></table></div></div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()

            lId = Request.QueryString("ID")
            lPROVENIENZA = Request.QueryString("T")
            Carica()
            H1.Value = "Estrazione_" & Format(Now, "yyyyMMddHHmmss")

        End If
    End Sub

    Private Function Carica()
        Try
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow

            par.OracleConn.Open()
            par.SettaCommand(par)




            Dim S1 As String = ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim S2 As String = ""
            Dim ss As String = "("

            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("TIPOLOGIA_CONTRATTO")
            dt.Columns.Add("PG_DICHIARAZIONE_AU")
            dt.Columns.Add("DATA_STIPULA")
            dt.Columns.Add("NUM_COMP")
            dt.Columns.Add("MINORI_15")
            dt.Columns.Add("MAGGIORI_65")
            dt.Columns.Add("NUM_COMP_66")
            dt.Columns.Add("NUM_COMP_100")
            dt.Columns.Add("NUM_COMP_100_CON")
            dt.Columns.Add("PSE")
            dt.Columns.Add("VSE")
            dt.Columns.Add("REDDITI_DIPENDENTI")
            dt.Columns.Add("REDDITI_ALTRI")
            dt.Columns.Add("COEFF_NUCLEO_FAM")
            dt.Columns.Add("LIMITE_PENSIONI")
            dt.Columns.Add("REDD_PREV_DIP")
            dt.Columns.Add("REDD_COMPLESSIVO")
            dt.Columns.Add("REDD_IMMOBILIARI")
            dt.Columns.Add("REDD_MOBILIARI")
            dt.Columns.Add("ISE")
            dt.Columns.Add("DETRAZIONI")
            dt.Columns.Add("DETRAZIONI_FRAGILITA")
            dt.Columns.Add("ISEE")
            dt.Columns.Add("ISR")
            dt.Columns.Add("ISP")
            dt.Columns.Add("ISEE_27")
            dt.Columns.Add("ID_AREA_ECONOMICA")
            dt.Columns.Add("SOTTO_AREA")
            dt.Columns.Add("LIMITE_ISEE")
            dt.Columns.Add("DECADENZA_ALL_ADEGUATO")
            dt.Columns.Add("DECADENZA_VAL_ICI")
            dt.Columns.Add("PATRIMONIO_SUP")
            dt.Columns.Add("ANNO_COSTRUZIONE")
            dt.Columns.Add("LOCALITA")
            dt.Columns.Add("NUMERO_PIANO")
            dt.Columns.Add("PRESENTE_ASCENSORE")
            dt.Columns.Add("PIANO")
            dt.Columns.Add("DEM")
            dt.Columns.Add("ZONA")
            dt.Columns.Add("COSTOBASE")
            dt.Columns.Add("VETUSTA")
            dt.Columns.Add("CONSERVAZIONE")
            dt.Columns.Add("SUP_NETTA")
            dt.Columns.Add("SUPCONVENZIONALE")
            dt.Columns.Add("ALTRE_SUP")
            dt.Columns.Add("SUP_ACCESSORI")
            dt.Columns.Add("VALORE_LOCATIVO")
            dt.Columns.Add("PERC_VAL_LOC")
            dt.Columns.Add("CANONE_CLASSE")
            dt.Columns.Add("PERC_ISTAT_APPLICATA")
            dt.Columns.Add("CANONE_CLASSE_ISTAT")
            dt.Columns.Add("INC_MAX")
            dt.Columns.Add("CANONE_SOPPORTABILE")
            dt.Columns.Add("CANONE_MINIMO_AREA")
            dt.Columns.Add("CANONE")
            dt.Columns.Add("CANONE_ATTUALE")
            dt.Columns.Add("ADEGUAMENTO")
            dt.Columns.Add("ISTAT")
            dt.Columns.Add("CANONE_91")
            dt.Columns.Add("NOTE")
            dt.Columns.Add("ANNOTAZIONI")

            dt.Columns.Add("TIPO_CANONE_APPLICATO")
            dt.Columns.Add("SCONTO_COSTO_BASE")
            dt.Columns.Add("CANONE_1243_12")
            dt.Columns.Add("DELTA_1243_12")
            dt.Columns.Add("ESCLUSIONE_1243_12")

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC_APPLICAZIONI_AU_FILE WHERE ID=" & lId
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Label3.Text = par.FormattaData(Mid(myReader1("data_applicazione"), 1, 8)) & " " & Mid(myReader1("data_applicazione"), 9, 2) & ":" & Mid(myReader1("data_applicazione"), 11, 2)
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT DECODE(PRESENZA_MAG_65,0,'NO',1,'SI') AS MAGGIORI_15,DECODE(PRESENZA_MIN_15,0,'NO',1,'SI') AS MINORI_15,TO_CHAR(TO_DATE(SUBSTR(RAPPORTI_UTENZA.DATA_STIPULA,1,8),'yyyymmdd'),'dd/mm/yyyy') AS DATA_STIPULA,PG,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,CANONI_EC.* FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.CANONI_EC_APPLICAZIONI_AU,SISCOM_MI.CANONI_EC WHERE UTENZA_DICHIARAZIONI.ID=CANONI_EC.ID_DICHIARAZIONE AND CANONI_EC.TIPO_PROVENIENZA=" & lPROVENIENZA & " AND RAPPORTI_UTENZA.ID=CANONI_EC.ID_CONTRATTO AND CANONI_EC.ID=CANONI_EC_APPLICAZIONI_AU.ID_CANONI_EC AND ID_CANONI_EC_APP_FILE = " & lId
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                ROW = dt.NewRow()
                ROW.Item("COD_CONTRATTO") = myReader("COD_CONTRATTO")
                ROW.Item("TIPOLOGIA_CONTRATTO") = myReader("TIPO_CONTRATTO")
                ROW.Item("PG_DICHIARAZIONE_AU") = myReader("PG")
                ROW.Item("DATA_STIPULA") = par.FormattaData(myReader("DATA_STIPULA"))
                ROW.Item("NUM_COMP") = par.IfNull(myReader("NUM_COMP"), "")
                ROW.Item("MINORI_15") = par.IfNull(myReader("MINORI_15"), "")
                ROW.Item("MAGGIORI_65") = par.IfNull(myReader("MAGGIORI_65"), "")
                ROW.Item("NUM_COMP_66") = par.IfNull(myReader("NUM_COMP_66"), "")
                ROW.Item("NUM_COMP_100") = par.IfNull(myReader("NUM_COMP_100"), "")
                ROW.Item("NUM_COMP_100_CON") = par.IfNull(myReader("NUM_COMP_100_CON"), "")
                ROW.Item("PSE") = par.IfNull(myReader("PSE"), "")
                ROW.Item("VSE") = par.IfNull(myReader("VSE"), "")
                ROW.Item("REDDITI_DIPENDENTI") = Format(CDbl(par.IfNull(myReader("REDDITI_DIP"), "0")), "##,##0.00")
                ROW.Item("REDDITI_ALTRI") = Format(CDbl(par.IfNull(myReader("REDDITI_ATRI"), "")), "##,##0.00")
                ROW.Item("COEFF_NUCLEO_FAM") = par.IfNull(myReader("COEFF_NUCLEO_FAM"), "")
                ROW.Item("LIMITE_PENSIONI") = par.IfNull(myReader("LIMITE_PENSIONI"), "")
                If par.IfNull(myReader("REDD_PREV_DIP"), "") = "1" Then
                    ROW.Item("REDD_PREV_DIP") = "SI"
                Else
                    ROW.Item("REDD_PREV_DIP") = "NO"
                End If
                ROW.Item("REDD_COMPLESSIVO") = Format(CDbl(par.IfNull(myReader("REDD_COMPLESSIVO"), "0")), "##,##0.00")
                ROW.Item("REDD_IMMOBILIARI") = Format(CDbl(par.IfNull(myReader("REDD_IMMOBILIARI"), "0")), "##,##0.00")
                ROW.Item("REDD_MOBILIARI") = Format(CDbl(par.IfNull(myReader("REDD_MOBILIARI"), "")), "##,##0.00")
                ROW.Item("ISE") = par.IfNull(myReader("ISE"), "")
                ROW.Item("DETRAZIONI") = Format(CDbl(par.IfNull(myReader("DETRAZIONI"), "0")), "##,##0.00")
                ROW.Item("DETRAZIONI_FRAGILITA") = Format(CDbl(par.IfNull(myReader("DETRAZIONI_FRAGILITA"), "0")), "##,##0.00")
                ROW.Item("ISEE") = par.IfNull(myReader("ISEE"), "")
                ROW.Item("ISR") = par.IfNull(myReader("ISR"), "")
                ROW.Item("ISP") = par.IfNull(myReader("ISP"), "")
                ROW.Item("ISEE_27") = par.IfNull(myReader("ISEE_27"), "")
                Select Case par.IfNull(myReader("ID_AREA_ECONOMICA"), "")
                    Case 1
                        ROW.Item("ID_AREA_ECONOMICA") = "PROTEZIONE"
                    Case 2
                        ROW.Item("ID_AREA_ECONOMICA") = "ACCESSO"
                    Case 3
                        ROW.Item("ID_AREA_ECONOMICA") = "PERMANENZA"
                    Case 4
                        ROW.Item("ID_AREA_ECONOMICA") = "DECADENZA"
                    Case Else
                        ROW.Item("ID_AREA_ECONOMICA") = ""
                End Select

                ROW.Item("SOTTO_AREA") = par.IfNull(myReader("SOTTO_AREA"), "")
                If par.IfNull(myReader("LIMITE_ISEE"), "") = "1" Then
                    ROW.Item("LIMITE_ISEE") = "SI"
                Else
                    ROW.Item("LIMITE_ISEE") = "NO"
                End If
                If par.IfNull(myReader("DECADENZA_ALL_ADEGUATO"), "0") = "1" Then
                    ROW.Item("DECADENZA_ALL_ADEGUATO") = "SI"
                Else
                    ROW.Item("DECADENZA_ALL_ADEGUATO") = "NO"
                End If
                If par.IfNull(myReader("DECADENZA_VAL_ICI"), "0") = "1" Then
                    ROW.Item("DECADENZA_VAL_ICI") = "SI"
                Else
                    ROW.Item("DECADENZA_VAL_ICI") = "NO"
                End If
                If par.IfNull(myReader("PATRIMONIO_SUP"), "0") = "1" Then
                    ROW.Item("PATRIMONIO_SUP") = "SI"
                Else
                    ROW.Item("PATRIMONIO_SUP") = "NO"
                End If

                ROW.Item("ANNO_COSTRUZIONE") = par.IfNull(myReader("ANNO_COSTRUZIONE"), "")
                ROW.Item("LOCALITA") = par.IfNull(myReader("LOCALITA"), "")
                ROW.Item("NUMERO_PIANO") = par.IfNull(myReader("NUMERO_PIANO"), "")
                If par.IfNull(myReader("PRESENTE_ASCENSORE"), "0") = "1" Then
                    ROW.Item("PRESENTE_ASCENSORE") = "SI"
                Else
                    ROW.Item("PRESENTE_ASCENSORE") = "NO"
                End If
                ROW.Item("PIANO") = par.IfNull(myReader("PIANO"), "")
                ROW.Item("DEM") = par.IfNull(myReader("DEM"), "")
                ROW.Item("ZONA") = par.IfNull(myReader("ZONA"), "")
                ROW.Item("COSTOBASE") = par.IfNull(myReader("COSTOBASE"), "")
                ROW.Item("VETUSTA") = par.IfNull(myReader("VETUSTA"), "")
                ROW.Item("CONSERVAZIONE") = par.IfNull(myReader("CONSERVAZIONE"), "")
                ROW.Item("SUP_NETTA") = par.IfNull(myReader("SUP_NETTA"), "")
                ROW.Item("SUPCONVENZIONALE") = par.IfNull(myReader("SUPCONVENZIONALE"), "")
                ROW.Item("ALTRE_SUP") = par.IfNull(myReader("ALTRE_SUP"), "")
                ROW.Item("SUP_ACCESSORI") = par.IfNull(myReader("SUP_ACCESSORI"), "")
                ROW.Item("VALORE_LOCATIVO") = par.IfNull(myReader("VALORE_LOCATIVO"), "")
                ROW.Item("PERC_VAL_LOC") = par.IfNull(myReader("PERC_VAL_LOC"), "")
                ROW.Item("CANONE_CLASSE") = Format(CDbl(par.IfNull(myReader("CANONE_CLASSE"), "0")), "##,##0.00")
                ROW.Item("PERC_ISTAT_APPLICATA") = par.IfNull(myReader("PERC_ISTAT_APPLICATA"), "")
                ROW.Item("CANONE_CLASSE_ISTAT") = Format(CDbl(par.IfNull(myReader("CANONE_CLASSE_ISTAT"), "0")), "##,##0.00")
                ROW.Item("INC_MAX") = par.IfNull(myReader("INC_MAX"), "")
                ROW.Item("CANONE_SOPPORTABILE") = Format(CDbl(par.IfNull(myReader("CANONE_SOPPORTABILE"), "0")), "##,##0.00")
                ROW.Item("CANONE_MINIMO_AREA") = Format(CDbl(par.IfNull(myReader("CANONE_MINIMO_AREA"), "0")), "##,##0.00")
                ROW.Item("CANONE") = Format(CDbl(par.IfNull(myReader("CANONE"), "0")), "##,##0.00")
                ROW.Item("CANONE_ATTUALE") = par.IfNull(myReader("CANONE_ATTUALE"), "0")
                ROW.Item("ADEGUAMENTO") = par.IfNull(myReader("ADEGUAMENTO"), "0,00")
                ROW.Item("ISTAT") = par.IfNull(myReader("ISTAT"), "0")
                ROW.Item("NOTE") = par.IfNull(myReader("NOTE"), "")
                ROW.Item("ANNOTAZIONI") = par.IfNull(myReader("ANNOTAZIONI"), "")
                If par.IfNull(myReader("CANONE_91"), "") <> "" Then
                    ROW.Item("CANONE_91") = Format(CDbl(par.IfNull(myReader("CANONE_91"), "0")), "##,##0.00")
                Else
                    ROW.Item("CANONE_91") = ""
                End If

                ROW.Item("TIPO_CANONE_APPLICATO") = par.IfNull(myReader("TIPO_CANONE_APP"), "")

                ROW.Item("SCONTO_COSTO_BASE") = Replace(par.IfNull(myReader("SCONTO_COSTO_BASE"), ""), "1000", "")
                ROW.Item("CANONE_1243_12") = par.IfNull(myReader("CANONE_1243_12"), "")
                ROW.Item("DELTA_1243_12") = par.IfNull(myReader("DELTA_1243_12"), "")
                ROW.Item("ESCLUSIONE_1243_12") = par.IfNull(myReader("ESCLUSIONE_1243_12"), "")

                dt.Rows.Add(ROW)
            End While
            HttpContext.Current.Session.Add("ElencoRisultati", dt)

            myReader.Close()
            Label2.Text = " - " & dt.Rows.Count & " nella lista"
            BindGrid()


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Sub BindGrid()

        Dim dt As New System.Data.DataTable
        dt = CType(HttpContext.Current.Session.Item("ElencoRisultati"), Data.DataTable)
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()

    End Sub

    Protected Sub imgExport0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport0.Click
        ExportXLS()
    End Sub

    Private Function ExportXLS()
        Try
            Dim DT As New Data.DataTable

            DT = CType(HttpContext.Current.Session.Item("ElencoRisultati"), Data.DataTable)

            If DT.Rows.Count > 0 Then

                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(DT, DataGrid1, "ExportApplicazioni", , False, , False)

                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception

        End Try
    End Function

    Protected Sub imgExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
        ExportXLS()
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
