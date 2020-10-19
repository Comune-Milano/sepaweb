Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoConvocabili
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable
    Dim INDICEBANDO As Long = 0
    Dim DataInzio As String = ""

    Dim Tipo1 As Integer = 0
    Dim Tipo2 As Integer = 0
    Dim Tipo3 As Integer = 0
    Dim Tipo4 As Integer = 0
    Dim Tipo5 As Integer = 0
    Dim Tipo6 As Integer = 0
    Dim Tipo7 As Integer = 0
    Dim Tipo8 As Integer = 0
    Dim Tipo9 As Integer = 0
    Dim Tipo10 As Integer = 0
    Dim Tipo11 As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDatiAU()
            CaricaDati()
        End If
    End Sub

    Private Function CaricaDatiAU()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If



            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then

                INDICEBANDO = PAR.IfNull(myReader("ID"), 0)
                DataInzio = PAR.IfNull(myReader("INIZIO_CANONE"), "")


                Tipo1 = PAR.IfNull(myReader("ERP_1"), 0) 'ERP SOCIALE
                Tipo2 = PAR.IfNull(myReader("ERP_2"), 0) 'ERP MODERATO
                Tipo3 = PAR.IfNull(myReader("ERP_ART_22"), 0) 'ART 200 C 10
                Tipo4 = PAR.IfNull(myReader("ERP_4"), 0)
                Tipo5 = PAR.IfNull(myReader("ERP_5"), 0)
                Tipo10 = PAR.IfNull(myReader("ERP_3"), 0)
                Tipo6 = PAR.IfNull(myReader("L43198"), 0)
                Tipo7 = PAR.IfNull(myReader("L39278"), 0)
                Tipo8 = PAR.IfNull(myReader("ERP_FF_OO"), 0) 'FF.OO.
                Tipo9 = PAR.IfNull(myReader("ERP_CONV"), 0) 'ERP CONVENZIONATO
                Tipo11 = PAR.IfNull(myReader("OA"), 0)

                Dim ss As String = ""
                
                If Tipo1 = 1 Then ss = ss & "Erp Sociale,"
                If Tipo2 = 1 Then ss = ss & "Erp Moderato,"
                If Tipo3 = 1 Then ss = ss & "ART.22 C.10 RR 1/2004,"
                If Tipo4 = 1 Then ss = ss & "4.	art.15 comma 2-vizi amministrativi,"
                If Tipo5 = 1 Then ss = ss & "5.	Legge 10/86,"
                If Tipo6 = 1 Then ss = ss & "431/98,"
                If Tipo7 = 1 Then ss = ss & "392/78,"
                If Tipo8 = 1 Then ss = ss & "Erp FF.OO.,"
                If Tipo9 = 1 Then ss = ss & "Erp Convenzionato,"
                If Tipo10 = 1 Then ss = ss & "Erp Art.15 let. a, b - 431 Deroga,"
                If Tipo11 = 1 Then ss = ss & "Occupazioni Abusive,"
                ss = Mid(ss, 1, Len(ss) - 1)
                Label1.Text = "Convocabili AU: " & PAR.IfNull(myReader("descrizione"), "") & " (" & ss & ") - " & PAR.DeCripta(Request.QueryString("Q")) & " - " & Format(Now, "dd/MM/yyyy HH:mm") & " - " & Session.Item("operatore")

            End If
            myReader.Close()


            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Function

    Private Function CaricaDati()
        Try
            Dim S As String = ""
            Dim S1 As String = ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim S2 As String = ""
            Dim ss As String = "("

            If Request.QueryString("TIPOC") = "" Then

                If Tipo1 = 1 Then 'erp sociale
                    ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
                End If

                If Tipo2 = 1 Then 'erp moderato
                    ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
                End If

                If Tipo3 = 1 Then 'ART 22 C 10
                    ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
                End If

                If Tipo8 = 1 Then 'FF.OO.
                    'ss = ss & " unita_immobiliari.id_destinazione_uso = 10 or "
                    ss = ss & " rapporti_utenza.provenienza_ass = 10 or "
                End If

                If Tipo9 = 1 Then 'convenzionato
                    ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
                End If

                If Tipo6 = 1 Then
                    ss = ss & " rapporti_utenza.dest_uso = 'P' or rapporti_utenza.dest_uso = 'S' OR rapporti_utenza.dest_uso = '0' or "
                End If

                If Tipo10 = 1 Then
                    ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
                End If

                If Tipo4 = 1 And ss = "(" Then
                    ss = ss & "rapporti_utenza.dest_uso='X' OR "
                End If

                If Tipo5 = 1 And ss = "(" Then
                    ss = ss & "rapporti_utenza.dest_uso='X' OR "
                End If

                If Tipo11 = 1 Then 'OCCUPAZIONI ABUSIVE
                    ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
                End If

                If ss = "(" Then
                    ss = "(rapporti_utenza.dest_uso='X') "
                Else
                    ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
                End If

            Else

               

              

                'If PAR.IfNull(myReader("ERP_4"), "") = "1" Then
                '    cmbTipoContratto.Items.Add(New ListItem("ERP art.15 comma 2-vizi amministrativi", "6"))
                'End If

                'If PAR.IfNull(myReader("L39278"), "") = "1" Then
                '    cmbTipoContratto.Items.Add(New ListItem("392/78", "8"))
                'End If

                'If PAR.IfNull(myReader("L43198"), "") = "1" Then
                '    cmbTipoContratto.Items.Add(New ListItem("431/98", "9"))
                'End If

                Select Case Request.QueryString("TIPOC")
                    Case "1"
                        ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
                    Case "2"
                        ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
                    Case "3"
                        'ss = ss & " unita_immobiliari.id_destinazione_uso = 10 or "
                        ss = ss & " rapporti_utenza.provenienza_ass = 10 or "
                    Case "4"
                        ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
                    Case "5"
                        ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
                    Case "6"

                    Case "7"
                        ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
                    Case "8"

                    Case "9"

                    Case "10"
                        ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
                End Select

                If ss = "(" Then
                    ss = "(rapporti_utenza.dest_uso='X') "
                Else
                    ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
                End If

            End If

            '            Tabella = "SELECT RAPPORTI_UTENZA.ID AS IDC," _
            '& "rapporti_utenza.cod_contratto, " _
            '& "CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTESTATARIO, " _
            '& "tab_quartieri.nome AS QUARTIERE, " _
            '& "EDIFICI.DENOMINAZIONE AS EDIFICIO, " _
            '& "INDIRIZZI.DESCRIZIONE||' ,'||INDIRIZZI.CIVICO||' '||INDIRIZZI.CAP||' '||INDIRIZZI.LOCALITA AS INDIRIZZO_UI, " _
            '& "tipo_cor,via_cor,civico_cor,luogo_cor,sigla_cor,cap_cor,TAB_FILIALI.ID AS ID_FILIALE, " _
            '& "TAB_FILIALI.NOME AS FILIALE, " _
            '& "UTENZA_SPORTELLI.DESCRIZIONE AS SEDE,UTENZA_SPORTELLI.ID AS ID_SPORTELLO, " _
            '& "COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO, " _
            '& "(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 THEN 'ERP Sociale' WHEN  " _
            '& "UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004'  " _
            '& "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN  " _
            '& "RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO, " _
            '& "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_STIPULA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_STIPULA, " _
            '& "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_SLOGGIO, " _
            '& "CASE WHEN UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA='VEND' THEN 'X'  ELSE '' END AS UI_VENDUTA, " _
            '& "(SELECT DISTINCT 'X' FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANNO=2013 AND (ID_VOCE=300 OR ID_VOCE=301 OR ID_VOCE=302 OR ID_VOCE=303)) AS BOLL_SPESE, " _
            '& " " _
            '& "EC1.NUM_COMP,EC1.NUM_COMP_66,EC1.NUM_COMP_100,EC1.NUM_COMP_100_CON,EC1.minori_15,EC1.maggiori_65,CASE WHEN EC1.REDD_PREV_DIP='1' THEN 'X'  ELSE '' END AS PREVALENTE_DIPENDENTE, " _
            '& "CASE WHEN EC1.REDD_IMMOBILIARI='0' THEN ''  ELSE 'X' END AS REDDITI_IMMOBILIARI, " _
            '& " " _
            '& "CASE WHEN CANONI_EC.ID_AREA_ECONOMICA=4 THEN 'DECADENZA' WHEN CANONI_EC.ID_AREA_ECONOMICA=1 THEN 'PROTEZIONE' WHEN CANONI_EC.ID_AREA_ECONOMICA=2 THEN 'ACCESSO' WHEN CANONI_EC.ID_AREA_ECONOMICA=3 THEN 'PERMANENZA' END AS AREA_ECONOMICA, " _
            '& "CANONI_EC.SOTTO_AREA AS CLASSE " _
            '& " " _
            '& "FROM  " _
            '& "SISCOM_MI.CANONI_EC,SISCOM_MI.CANONI_EC EC1, " _
            '& "UTENZA_SPORTELLI_PATRIMONIO,UTENZA_SPORTELLI,siscom_mi.TAB_FILIALI,UTENZA_FILIALI,SISCOM_MI.INDIRIZZI,siscom_mi.tab_quartieri,siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali, " _
            '& "siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari " _
            '& "WHERE  " & ss & " " & PAR.DeCripta(Request.QueryString("S")) & " RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM UTENZA_LISTE_CDETT,UTENZA_LISTE_CONV WHERE UTENZA_LISTE_CONV.STATO<>2 AND UTENZA_LISTE_CONV.ID_AU=" & Request.QueryString("IDB") & " AND UTENZA_LISTE_CDETT.ID_LISTA=UTENZA_LISTE_CONV.ID ) and " _
            '& "EC1.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EC1.DATA_CALCOLO=(SELECT MAX(DATA_CALCOLO) FROM SISCOM_MI.CANONI_EC EC WHERE EC.ID_CONTRATTO=EC1.ID_CONTRATTO AND EC1.INIZIO_VALIDITA_CAN<='" & DataInzio & "' AND EC1.FINE_VALIDITA_CAN>='" & DataInzio & "') AND " _
            '& "CANONI_EC.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND CANONI_EC.DATA_CALCOLO=(SELECT MAX(DATA_CALCOLO) FROM SISCOM_MI.CANONI_EC EC WHERE EC.ID_CONTRATTO=CANONI_EC.ID_CONTRATTO) AND " _
            '& "UTENZA_SPORTELLI.ID=UTENZA_SPORTELLI_PATRIMONIO.ID_SPORTELLO AND " _
            '& "UTENZA_SPORTELLI_PATRIMONIO.ID_unita=UNITA_IMMOBILIARI.id AND " _
            '& "UTENZA_FILIALI.ID=UTENZA_SPORTELLI.ID_FILIALE (+) AND " _
            '& "TAB_FILIALI.ID=UTENZA_FILIALI.ID_STRUTTURA (+) AND " _
            '& "EDIFICI.ID<>1 AND " _
            '& "INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND " _
            '& "unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL AND " _
            '& "unita_immobiliari.ID=unita_contrattuale.id_unita AND edifici.ID=unita_immobiliari.id_edificio AND " _
            '& "complessi_immobiliari.ID=edifici.id_complesso AND " _
            '& "tab_quartieri.ID=complessi_immobiliari.id_quartiere AND " _
            '& "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND  " _
            '& "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND " _
            '& "bozza=0 AND (data_riconsegna IS NULL OR NVL(data_riconsegna,'29991231')>='" & DataInzio & "')  "

            Tabella = "SELECT RAPPORTI_UTENZA.ID AS IDC," _
                    & "rapporti_utenza.cod_contratto, " _
                    & "CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTESTATARIO, " _
                    & "tab_quartieri.nome AS QUARTIERE, " _
                    & "EDIFICI.DENOMINAZIONE AS EDIFICIO, " _
                    & "INDIRIZZI.DESCRIZIONE||' ,'||INDIRIZZI.CIVICO||' '||INDIRIZZI.CAP||' '||INDIRIZZI.LOCALITA AS INDIRIZZO_UI, " _
                    & "tipo_cor,via_cor,civico_cor,luogo_cor,sigla_cor,cap_cor,TAB_FILIALI.ID AS ID_FILIALE, " _
                    & "TAB_FILIALI.NOME AS FILIALE, " _
                    & "UTENZA_SPORTELLI.DESCRIZIONE AS SEDE,UTENZA_SPORTELLI.ID AS ID_SPORTELLO, " _
                    & "COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO, " _
                    & "(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 THEN 'ERP Sociale' WHEN  " _
                    & "UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004'  " _
                    & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN  " _
                    & "RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO, " _
                    & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_STIPULA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_STIPULA, " _
                    & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_SLOGGIO, " _
                    & "CASE WHEN UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA='VEND' THEN 'X'  ELSE '' END AS UI_VENDUTA, " _
                    & "'' AS BOLL_SPESE, " _
                    & "'' AS NUM_COMP,'' AS NUM_COMP_66,'' AS NUM_COMP_100, '' AS NUM_COMP_100_CON,'' AS minori_15,'' AS maggiori_65,'' AS PREVALENTE_DIPENDENTE, " _
                    & "'' AS REDDITI_IMMOBILIARI, " _
                    & "'' AS AREA_ECONOMICA, " _
                    & "'' AS CLASSE " _
                    & "FROM  " _
                    & "UTENZA_SPORTELLI_PATRIMONIO,UTENZA_SPORTELLI,siscom_mi.TAB_FILIALI,UTENZA_FILIALI,SISCOM_MI.INDIRIZZI,siscom_mi.tab_quartieri,siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali, " _
                    & "siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari " _
                    & "WHERE  " & ss & " " & PAR.DeCripta(Request.QueryString("S")) & " RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM UTENZA_LISTE_CDETT,UTENZA_LISTE_CONV WHERE UTENZA_LISTE_CONV.STATO<>2 AND UTENZA_LISTE_CONV.ID_AU=" & INDICEBANDO & " AND UTENZA_LISTE_CDETT.ID_LISTA=UTENZA_LISTE_CONV.ID ) and " _
                    & " " _
                    & " " _
                    & "UTENZA_SPORTELLI.ID=UTENZA_SPORTELLI_PATRIMONIO.ID_SPORTELLO AND " _
                    & "UTENZA_SPORTELLI_PATRIMONIO.ID_unita=UNITA_IMMOBILIARI.id AND " _
                    & "UTENZA_FILIALI.ID=UTENZA_SPORTELLI.ID_FILIALE (+) AND " _
                    & "TAB_FILIALI.ID=UTENZA_FILIALI.ID_STRUTTURA (+) AND " _
                    & "EDIFICI.ID<>1 AND " _
                    & "INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND " _
                    & "unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL AND " _
                    & "unita_immobiliari.ID=unita_contrattuale.id_unita AND edifici.ID=unita_immobiliari.id_edificio AND " _
                    & "complessi_immobiliari.ID=edifici.id_complesso AND " _
                    & "tab_quartieri.ID (+)=complessi_immobiliari.id_quartiere AND " _
                    & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND  " _
                    & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND " _
                    & "bozza=0 AND unita_immobiliari.COD_TIPOLOGIA='AL' AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND (data_riconsegna IS NULL OR NVL(data_riconsegna,'29991231')>='" & DataInzio & "')  "

            'PAR.cmd.CommandText = Tabella
            'da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
            'da.Fill(DT)


            'DataGridRateEmesse.DataSource = DT
            'DataGridRateEmesse.DataBind()
            'Session.Add("MIADT", DT)
            'Label2.Text = DT.Rows.Count & " nella lista"
            BindGrid()
        Catch ex As Exception
            'Beep()
        End Try
    End Function

    Private Sub BindGrid()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

        PAR.cmd.CommandText = Tabella
        da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
        da.Fill(DT)


        DataGridRateEmesse.DataSource = DT
        DataGridRateEmesse.DataBind()
        Session.Add("MIADT", DT)
        Label2.Text = DT.Rows.Count & " nella lista"

    End Sub

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            If DT.Rows.Count > 0 Then
                DT.Columns.Remove("IDC")
                DT.Columns.Remove("ID_FILIALE")
                DT.Columns.Remove("ID_SPORTELLO")
                'Dim nomefile As String = PAR.EsportaExcelDaDT(DT, "ExportConvocabili", , False, , True)
                Dim nomefile As String = PAR.EsportaExcelDaDTWithDatagrid(DT, DataGridRateEmesse, "ExportConvocabili", , False, , False)

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
        'Try


        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow

        '    DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
        '    sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

        '    i = 0

        '    With myExcelFile

        '        .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)

        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, Label1.Text)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, "")
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, "")

        '        K = 2
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CONTRATTO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "INTESTATARIO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "QUARTIERE", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "EDIFICIO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "INDIRIZZO UNITA", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "TIPO IND. CORR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "IND. CORR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "CIVICO CORR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "LUOGO CORR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "PROVINCIA CORR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "CAP CORR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "FILIALE/STRUTTURA", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "SEDE/SPORTELLO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "TIPOLOGIA CONTR.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "TIPO SPECIFICO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "DATA STIPULA", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "DATA SLOGGIO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, "UNITA VENDUTA", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, "BOLLETTAZIONE SPESE", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, "NUM.COMPONENTI", 12)

        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, "MINORI 15 ANNI", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, "MAGGIORI 65 ANNI", 12)

        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, "NUM.COMP. 66-99% INV.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, "NUM.COMP. 100% INV.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, "NUM.COMP. 100% INV. ACC.", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, "REDD. PREVALENTEMENTE DIPENDENTE", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, "REDDITI IMMOBILIARI", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, "AREA ECONOMICA", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, "CLASSE", 12)



        '        K = 3
        '        For Each row In DT.Rows
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD_CONTRATTO"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INTESTATARIO"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("QUARTIERE"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("EDIFICIO"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INDIRIZZO_UI"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_COR"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("VIA_COR"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CIVICO_COR"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("LUOGO_COR"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("SIGLA_COR"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CAP_COR"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FILIALE"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("SEDE"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_CONTRATTO"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_SPECIFICO"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_STIPULA"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_SLOGGIO"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("UI_VENDUTA"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("BOLL_SPESE"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP"), "")))

        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("MINORI_15"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("MAGGIORI_65"), "")))

        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP_66"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP_100"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP_100_CON"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PREVALENTE_DIPENDENTE"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("REDDITI_IMMOBILIARI"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("AREA_ECONOMICA"), "")))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CLASSE"), "")))

        '            i = i + 1
        '            K = K + 1
        '        Next

        '        .CloseFile()
        '    End With

        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String

        '    zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    '
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    '
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()

        '    File.Delete(strFile)
        '    Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")


        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If txtDescrizione.Text <> "" Then
            Try
                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                PAR.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into UTENZA_LISTE_CONV values (SEQ_UTENZA_LISTE_CONV.NEXTVAL," & Request.QueryString("IDB") & ",'" & PAR.PulisciStrSql(txtDescrizione.Text) & "','" & Format(Now, "yyyyMMddHHmm") & "','" & PAR.PulisciStrSql(Session.Item("OPERATORE")) & "','" & PAR.PulisciStrSql(Label1.Text) & "',0)"
                PAR.cmd.ExecuteNonQuery()

                Dim S As String = ""
                PAR.cmd.CommandText = "SELECT SEQ_UTENZA_LISTE_CONV.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader.Read() Then
                    S = myReader(0)
                End If
                myReader.Close()


                Dim I As Integer = 0
                Dim row As System.Data.DataRow
                DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
                For Each row In DT.Rows


                    PAR.cmd.CommandText = "Insert into UTENZA_LISTE_CDETT (ID_LISTA, ID_CONTRATTO, COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, TIPO_COR, VIA_COR, CIVICO_COR, LUOGO_COR, SIGLA_COR, CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE,MINORI_15,MAGGIORI_65,ID_SPORTELLO,ID_TAB_FILIALI) " _
                        & "Values " _
                        & "(" & S & ", " & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("IDC"), "")) & ", '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("COD_CONTRATTO"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("INTESTATARIO"), "")) & "','" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("QUARTIERE"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("EDIFICIO"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("INDIRIZZO_UI"), "")) & "', '" _
                        & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("TIPO_COR"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("VIA_COR"), "")) & "','" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("CIVICO_COR"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("LUOGO_COR"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("SIGLA_COR"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("CAP_COR"), "")) _
                        & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("FILIALE"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("SEDE"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("TIPO_CONTRATTO"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("TIPO_SPECIFICO"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("DATA_STIPULA"), "")) _
                        & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("DATA_SLOGGIO"), "")) & "','" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("UI_VENDUTA"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("BOLL_SPESE"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("NUM_COMP"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("NUM_COMP_66"), "")) & "', '" _
                        & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("NUM_COMP_100"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("NUM_COMP_100_CON"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("PREVALENTE_DIPENDENTE"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("REDDITI_IMMOBILIARI"), "")) & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("AREA_ECONOMICA"), "")) _
                        & "', '" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("CLASSE"), "")) & "','" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("MINORI_15"), "")) & "','" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("MAGGIORI_65"), "")) & "'," & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_SPORTELLO"), "")) & "," & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_FILIALE"), "")) & ")"
                    PAR.cmd.ExecuteNonQuery()

                    I = I + 1

                Next


                PAR.myTrans.Commit()
                PAR.OracleConn.Close()
                PAR.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione effettuata! La lista è richiamabile dalla funzione Elenco Liste Conv.');self.close();</script>")

            Catch ex As Exception
                PAR.myTrans.Rollback()
                PAR.OracleConn.Close()
                PAR.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label1.Text = ex.Message
                Label1.Visible = True
            End Try
        Else
            Response.Write("<script>alert('Inserire una descrizione per la lista');</script>")
        End If
    End Sub

    Protected Sub DataGridRateEmesse_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridRateEmesse.PageIndexChanged
        If e.NewPageIndex >= 0 Then

            DataGridRateEmesse.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridRateEmesse_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridRateEmesse.SelectedIndexChanged

    End Sub
End Class
