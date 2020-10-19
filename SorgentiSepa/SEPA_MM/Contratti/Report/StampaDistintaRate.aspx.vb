


Partial Class Contratti_Report_StampaDistintaRate
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try
                Dim Str As String
                'Dim totaleRiga As Double
                Dim TotAffitto As Double
                Dim TotAltro As Double
                Dim TotSpese As Double
                Dim TotRegCont As Double
                Dim TotSuTotRiga As Double
                Dim I As Long = 0
                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()
                NomeFile = "DistintaRateEmesse"

                '********CONNESSIONE*********
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                Dim Condizione As String = ""




                If Request.QueryString("DAL") <> "" Then
                    Condizione = "AND  data_emissione>='" & Request.QueryString("DAL") & "' "
                End If
                If Request.QueryString("AL") <> "" Then
                    Condizione = Condizione & " AND data_emissione<='" & Request.QueryString("AL") & "'"
                End If

                If Request.QueryString("DAL1") <> "" Then
                    Condizione = "AND  RIFERIMENTO_DA>='" & Request.QueryString("DAL1") & "' "
                End If
                If Request.QueryString("AL1") <> "" Then
                    Condizione = Condizione & " AND RIFERIMENTO_A<='" & Request.QueryString("AL1") & "'"
                End If

                If Request.QueryString("T") = "Attiva" Then
                    Condizione = Condizione & " AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV') "
                End If

                If Request.QueryString("T") = "Bollettazione" Then
                    Condizione = Condizione & " AND BOL_BOLLETTE.RIF_FILE<>'MOD' and BOL_BOLLETTE.RIF_FILE<>'MAV'  and BOL_BOLLETTE.RIF_FILE<>'REC' "
                End If

                If Request.QueryString("T") = "Virt.Manuale" Then
                    Condizione = Condizione & " AND (BOL_BOLLETTE.RIF_FILE='REC') "
                End If

                'par.cmd.CommandText = "SELECT ROWNUM, bol_bollette.ID,bol_bollette.N_RATA as ""RATA"",bol_bollette.INTESTATARIO,((to_char(to_date(data_emissione,'yyyymmdd'),'dd/mm/yyyy'))||' - - '||(to_char(to_date(data_scadenza,'yyyymmdd'),'dd/mm/yyyy'))) as ""PERIODO"",(select TO_CHAR (sum(importo), '9G999G990D99') from siscom_mi.bol_bollette_voci where (id_voce=1 or id_voce=36 or id_voce=118 or id_voce=218) and id_bolletta=BOL_BOLLETTE.ID) AS ""AFFITTO"",(select TO_CHAR(sum(importo),'9G999G990D99') from siscom_mi.bol_bollette_voci where id_voce<>1 and id_voce<>93 and id_voce<>36 and id_voce<>118 and id_voce<>218 and id_bolletta=BOL_BOLLETTE.ID) AS ""SPESE"",(select TO_CHAR(sum(importo),'9G999G990D99') from siscom_mi.bol_bollette_voci where id_voce=93 and id_bolletta=BOL_BOLLETTE.ID) AS ""REGISTRAZIONE"",TO_CHAR((NVL((SELECT SUM(importo)FROM siscom_mi.bol_bollette_voci WHERE (id_voce=1 OR id_voce=36 OR id_voce=118 OR id_voce=218) AND id_bolletta=BOL_BOLLETTE.ID),0) + NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE id_voce<>1 AND id_voce<>93 AND id_voce<>36 AND id_voce<>118 AND id_voce<>218 AND id_bolletta=BOL_BOLLETTE.ID),0)+ NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE id_voce=93 AND id_bolletta=BOL_BOLLETTE.ID),0) ),'9G999G990D99') AS TOT FROM SISCOM_MI.bol_bollette WHERE bol_bollette.FL_ANNULLATA='0' AND bol_bollette.n_rata<>'99999' AND bol_bollette.fl_stampato='1' " & Condizione & " ORDER BY bol_bollette.INTESTATARIO ASC"
                'par.cmd.CommandText = "SELECT ROWNUM, bol_bollette.ID,bol_bollette.N_RATA AS RATA,bol_bollette.INTESTATARIO,((TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy'))||' - - '||(TO_CHAR(TO_DATE(data_scadenza,'yyyymmdd'),'dd/mm/yyyy'))) AS PERIODO,(SELECT TO_CHAR (SUM(importo), '9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE id_capitolo=1) AND id_bolletta=BOL_BOLLETTE.ID) AS AFFITTO,(SELECT TO_CHAR(SUM(importo),'9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE id_capitolo=3) AND id_bolletta=BOL_BOLLETTE.ID) AS SPESE,(SELECT TO_CHAR(SUM(importo),'9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE id_capitolo=2) AND id_bolletta=BOL_BOLLETTE.ID) AS REGISTRAZIONE,TO_CHAR((NVL((SELECT SUM(importo)FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE id_capitolo=1) AND id_bolletta=BOL_BOLLETTE.ID),0) + NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE id_capitolo=2) AND id_bolletta=BOL_BOLLETTE.ID),0)+ NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE id_capitolo=3)AND id_bolletta=BOL_BOLLETTE.ID),0) ),'9G999G990D99') AS TOT FROM SISCOM_MI.bol_bollette WHERE bol_bollette.FL_ANNULLATA='0' AND bol_bollette.n_rata<>'99999' AND bol_bollette.fl_stampato='1' " & Condizione & " ORDER BY bol_bollette.INTESTATARIO ASC"

                par.cmd.CommandText = "SELECT ROWNUM, bol_bollette.ID,CASE WHEN BOL_BOLLETTE.N_RATA=99 THEN 'MA' WHEN BOL_BOLLETTE.N_RATA=999 THEN 'AU' WHEN BOL_BOLLETTE.N_RATA=99999 THEN 'CO' ELSE TO_CHAR(BOL_BOLLETTE.N_RATA) END||'/'||BOL_BOLLETTE.ANNO AS RATA,bol_bollette.INTESTATARIO,((TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy'))||' - - '||(TO_CHAR(TO_DATE(data_scadenza,'yyyymmdd'),'dd/mm/yyyy'))) AS PERIODO," _
                                   & "(SELECT TO_CHAR(SUM(importo),'9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE COMPETENZA=1) AND id_bolletta=BOL_BOLLETTE.ID) AS COMUNE," _
                                   & "(SELECT TO_CHAR(SUM(importo),'9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE (COMPETENZA=2)) AND id_bolletta=BOL_BOLLETTE.ID) AS ALER," _
                                   & "(SELECT TO_CHAR(SUM(importo),'9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE COMPETENZA=3) AND id_bolletta=BOL_BOLLETTE.ID) AS SINDACATO, " _
                                   & "(SELECT TO_CHAR(SUM(importo),'9G999G990D99') FROM siscom_mi.bol_bollette_voci WHERE id_voce IN (SELECT ID FROM siscom_mi.t_voci_bolletta WHERE COMPETENZA=0) AND id_bolletta=BOL_BOLLETTE.ID) AS ALTRO " _
                                   & "FROM SISCOM_MI.bol_bollette WHERE BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL AND (BOL_BOLLETTE.FL_ANNULLATA='0' OR (BOL_BOLLETTE.FL_ANNULLATA<>'0' AND DATA_PAGAMENTO IS NOT NULL)) AND bol_bollette.n_rata<>'99999' AND bol_bollette.fl_stampato='1' " & Condizione & "  ORDER BY bol_bollette.INTESTATARIO ASC,BOL_BOLLETTE.ID DESC"

                'Sostituita la select, per visualizzare il puntino di separatore delle migliaia

                'par.cmd.CommandText = "SELECT  bol_bollette.ID,bol_bollette.N_RATA as ""RATA"",bol_bollette.INTESTATARIO,((to_char(to_date(data_emissione,'yyyymmdd'),'dd/mm/yyyy'))||' - - '||(to_char(to_date(data_scadenza,'yyyymmdd'),'dd/mm/yyyy'))) as ""PERIODO"",(select sum(importo) from siscom_mi.bol_bollette_voci where (id_voce=1 or id_voce=36 or id_voce=118 or id_voce=218) and id_bolletta=BOL_BOLLETTE.ID) AS ""AFFITTO"",(select sum(importo) from siscom_mi.bol_bollette_voci where id_voce<>1 and id_voce<>93 and id_voce<>36 and id_voce<>118 and id_voce<>218 and id_bolletta=BOL_BOLLETTE.ID) AS ""SPESE"",(select sum(importo) from siscom_mi.bol_bollette_voci where id_voce=93 and id_bolletta=BOL_BOLLETTE.ID) AS ""REGISTRAZIONE"",(NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE (id_voce=1 OR id_voce=36 OR id_voce=118 OR id_voce=218) AND id_bolletta=BOL_BOLLETTE.ID),0) + NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE id_voce<>1 AND id_voce<>93 AND id_voce<>36 AND id_voce<>118 AND id_voce<>218 AND id_bolletta=BOL_BOLLETTE.ID),0)  + NVL((SELECT SUM(importo) FROM siscom_mi.bol_bollette_voci WHERE id_voce=93 AND id_bolletta=BOL_BOLLETTE.ID),0) ) AS TOT FROM SISCOM_MI.bol_bollette WHERE bol_bollette.FL_ANNULLATA='0' AND bol_bollette.n_rata<>'99999' AND bol_bollette.fl_stampato='1'" & Condizione & " ORDER BY bol_bollette.n_rata ASC"

                Dim row As System.Data.DataRow
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt2 As New Data.DataTable

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Dim COLONNA As New System.Data.DataColumn
                    COLONNA.ColumnName = "CONTATORE"
                    dt.Columns.Add(COLONNA)

                    Dim COLONNA1 As New System.Data.DataColumn
                    COLONNA1.ColumnName = "TOT"
                    dt.Columns.Add(COLONNA1)

                    For Each row In dt.Rows

                        TotAffitto = TotAffitto + par.IfNull(dt.Rows(I).Item("COMUNE"), 0)
                        TotSpese = TotSpese + par.IfNull(dt.Rows(I).Item("ALER"), 0)
                        TotRegCont = TotRegCont + par.IfNull(dt.Rows(I).Item("SINDACATO"), 0)
                        TotAltro = TotAltro + par.IfNull(dt.Rows(I).Item("ALTRO"), 0)

                        'TotSuTotRiga = TotSuTotRiga + par.IfNull(dt.Rows(I).Item("TOT"), 0)
                        TotSuTotRiga = TotSuTotRiga + par.IfNull(dt.Rows(I).Item("COMUNE"), 0) + par.IfNull(dt.Rows(I).Item("ALER"), 0) + par.IfNull(dt.Rows(I).Item("SINDACATO"), 0) + par.IfNull(dt.Rows(I).Item("ALTRO"), 0)

                        dt.Rows(I).Item("TOT") = Format(CDbl(par.IfNull(dt.Rows(I).Item("COMUNE"), 0)) + CDbl(par.IfNull(dt.Rows(I).Item("ALER"), 0)) + CDbl(par.IfNull(dt.Rows(I).Item("SINDACATO"), 0)) + CDbl(par.IfNull(dt.Rows(I).Item("ALTRO"), 0)), "##,##0.00")

                        dt.Rows(I).Item("CONTATORE") = I + 1


                        I = I + 1
                    Next
                    row = dt.NewRow()
                    row.Item("PERIODO") = "TOTALI FINE"
                    row.Item("COMUNE") = Format(TotAffitto, "##,##0.00")
                    row.Item("ALER") = Format(TotSpese, "##,##0.00")
                    row.Item("SINDACATO") = Format(TotRegCont, "##,##0.00")
                    row.Item("ALTRO") = Format(TotAltro, "##,##0.00")
                    row.Item("TOT") = Format(TotSuTotRiga, "##,##0.00")

                    dt.Rows.Add(row)

                    Response.Write("<p style='font-weight: 700; text-align: center'>DISTINTA RATE</p>")
                    Response.Write("<p style='text-align: left'>La Distina delle rate è stata creata. Premere i pulsanti sottostanti per esportare o visualizzare.</p>")


                    'DataGridRateEmesse.DataSource = dt
                    'DataGridRateEmesse.DataBind()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")
                    Response.Write("<script language='javascript'> { self.close() }</script>")
                End If

                HttpContext.Current.Session.Add("AA", dt)
                imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=0','Export','');")
                imgVisualizza.Attributes.Add("onclick", "javascript:window.open('Visualizza.aspx?DAL1=" & Trim(Request.QueryString("DAL1")) & "&AL1=" & Request.QueryString("AL1") & "&DAL=" & Trim(Request.QueryString("DAL")) & "&AL=" & Request.QueryString("AL") & "','ElencoDistinta','');")

            Catch ex As Exception
                Me.lblErrore.Visible = True
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message

            End Try
        End If



    End Sub


    Private Property NomeFile() As String
        Get
            If Not (ViewState("par_NomeFile") Is Nothing) Then
                Return CStr(ViewState("par_NomeFile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_NomeFile") = value
        End Set

    End Property



End Class
