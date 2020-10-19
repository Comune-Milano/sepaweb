
Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_Spalmatore_RptEmessoTotale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Private Sub Contratti_Spalmatore_RptBollContabili_Load(sender As Object, e As EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            LeggiUltimaData()
        End If
    End Sub

    Private Sub LeggiUltimaData()
        Try
            connData.apri()
            dataScad = Format(Now, "yyyyMMdd")

            par.cmd.CommandText = "select nvl(data_scadenza_boll,to_char(sysdate, 'yyyyMMdd')) from SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST where id = (SELECT max(id) FROM SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST esg)"
            dataScad = par.IfEmpty(par.cmd.ExecuteScalar, Format(Now, "yyyyMMdd"))

            txtDataScad.SelectedDate = par.FormattaData(dataScad)

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Public Property dataScad() As String
        Get
            If Not (ViewState("par_dataScad") Is Nothing) Then
                Return CStr(ViewState("par_dataScad"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataScad") = value
        End Set

    End Property
    Private Sub CreaRptEmessoTotale()
        Try

            Dim condizioneDateScad As String = ""
            Dim condizioneDateScad2 As String = ""
            dataScad = par.AggiustaData(CDate(txtDataScad.SelectedDate).ToShortDateString)

            If dataScad <> "" Then
                condizioneDateScad = " and b.data_scadenza <= '" & dataScad & "' "
                condizioneDateScad2 = " and bo.data_scadenza <= '" & dataScad & "' "
            End If

            par.cmd.CommandText = "select DATA_ESTRAZIONE," _
                & "       DATA_SCADENZA_BOLLETTE," _
                & "       RU," _
                & "       ID_CONTRATTO," _
                & "       COD_TIPOLOGIA_CONTR_LOC," _
                & "       TOT_EMESSO_CONTABILE + nvl(importo_spese_mor_rat, 0) TOT_EMESSO_CONTABILE," _
                & "       TOT_MONTE_CANONE," _
                & "       TOT_MONTE_CANONE_PAG," _
                & "       TOT_PAGATO_CONTABILE + nvl(importo_spese_mor_rat_pag, 0) TOT_PAGATO_CONTABILE," _
                & "       TOT_PARTITA_GIRO," _
                & "       TOT_PARTITA_GIRO_PAGATO," _
                & "       TOT_RUOLO," _
                & "       TOT_RUOLO_PAGATO," _
                & "       TOT_SALDO_CONTABILE +" _
                & "       (nvl(importo_spese_mor_rat, 0) - nvl(importo_spese_mor_rat_pag, 0)) TOT_SALDO_CONTABILE," _
                & "       TOT_SALDO_PER_SPALMATORE +" _
                & "       (nvl(importo_spese_mor_rat, 0) - nvl(importo_spese_mor_rat_pag, 0)) TOT_SALDO_PER_SPALMATORE" _
                & "  from (select bo.id_contratto id_cnt," _
                & "               sum(nvl(bov.importo, 0)) importo_spese_mor_rat," _
                & "               sum(nvl(bov.imp_pagato, 0)) importo_spese_mor_rat_pag" _
                & "          from siscom_mi.bol_bollette bo, siscom_mi.bol_bollette_voci bov" _
                & "         where bo.id = bov.id_bolletta" _
                & "           and bo.id_tipo in (4, 5)" _
                & "           and bo.fl_annullata = 0 " _
                & condizioneDateScad2 _
                & "           and bo.id_bolletta_storno is null" _
                & "           /*and bov.id_voce in (678, 407, 95, 628)*/" _
                & "         group by id_contratto) y," _
                & "  ( select  DATA_eSTRAZIONE,DATA_sCADENZA_BOLLETTE,RU,ID_CONTRATTO,COD_TIPOLOGIA_CONTR_LOC," _
& " SUM(TOT_EMESSO_CONTABILE) tot_emesso_contabile,SUM(TOT_MONTE_CANONE) tot_monte_canone,SUM(TOT_MONTE_cANONE_PAG) tot_monte_canone_pag,SUM(TOT_PAGATO_CONTABILE) tot_pagato_contabile,SUM(TOT_PARTITA_GIRO) tot_partita_giro," _
& " SUM(TOT_PARTITA_GIRO_PAGATO) tot_partita_giro_pagato,SUM(TOT_RUOLO) tot_ruolo,SUM(TOT_RUOLO_PAGATO) tot_ruolo_pagato,SUM(TOT_SALDO_CONTABILE) tot_saldo_contabile,SUM(TOT_SALDO_PER_SPALMATORE) TOT_SALDO_PER_SPALMATORE" _
& " from ( " _
                & "  (  (select to_char(sysdate, 'DD/MM/YYYY') data_estrazione," _
                    & "            '" & par.FormattaData(dataScad) & "' data_scadenza_bollette," _
                    & "            u.cod_contratto ru," _
                    & "            u.id id_contratto," _
                    & "            u.cod_tipologia_contr_loc," _
                    & "            SUM(b.EMESSO_CONTABILE) tot_emesso_contabile," _
                    & "            sum(x.tot_monte_canone) tot_monte_canone," _
                    & "            sum(x.tot_monte_canone_pag) tot_monte_canone_pag," _
                    & "            SUM(b.INCASSATO_CONTABILE) tot_pagato_contabile," _
                    & "            sum(nvl(b.quota_sind_b, 0)) tot_partita_giro," _
                    & "            sum(nvl(b.quota_sind_pagata_b, 0)) tot_partita_giro_pagato," _
                    & "            sum(nvl(b.importo_ruolo, 0)) tot_ruolo," _
                    & "            sum(nvl(b.imp_ruolo_pagato, 0)) tot_ruolo_pagato," _
                    & "            sum(b.saldo_contabile) tot_saldo_contabile," _
                    & "            sum(b.EMESSO_CONTABILE) - sum(b.INCASSATO_CONTABILE) -" _
                    & "            sum(nvl(b.importo_ruolo, 0)) TOT_SALDO_PER_SPALMATORE" _
                    & "       from siscom_mi.rapporti_utenza u," _
                    & "            siscom_mi.v_saldo_bollette b," _
                    & "            (select v.id_bolletta," _
                    & "                    sum(v.importo) tot_monte_canone," _
                    & "                    sum(v.imp_pagato) tot_monte_canone_pag" _
                    & "               from siscom_mi.bol_bollette_voci v" _
                    & "              where v.id_voce in" _
                    & "                    (select t.id" _
                    & "                       from SISCOM_MI.T_VOCI_BOLLETTA t" _
                    & "                      where t.gruppo = 4" _
                    & "                        and t.tipo_voce in (2, 10, 13))" _
                    & "              group by v.id_bolletta) x" _
                    & "      where u.id = b.id_contratto" _
                    & "        and b.id = x.id_bolletta(+) " _
                    & condizioneDateScad _
                    & "        and b.id_tipo not in (3, 4, 5, 9, 22, 25)" _
                    & "        and b.fl_annullata = 0" _
                    & "        and b.id_bolletta_storno is null" _
                    & "        and id_bolletta_Ric is null and id_Rateizzazione is null " _
                    & "      group by u.cod_contratto, u.id, u.cod_tipologia_contr_loc)" _
                    & "      UNION " _
                    & " (select to_char(sysdate, 'DD/MM/YYYY') data_estrazione," _
                    & "            '" & par.FormattaData(dataScad) & "' data_scadenza_bollette," _
                    & "            u.cod_contratto ru," _
                    & "            u.id id_contratto," _
                    & "            u.cod_tipologia_contr_loc," _
                    & "            0 tot_emesso_contabile," _
                    & "            0 tot_monte_canone," _
                    & "            0 tot_monte_canone_pag," _
                    & "            0 tot_pagato_contabile," _
                    & "            0 tot_partita_giro," _
                    & "            0 tot_partita_giro_pagato," _
                    & "            0 tot_ruolo," _
                    & "            0 tot_ruolo_pagato," _
                    & "            0 tot_saldo_contabile," _
                    & "            0 TOT_SALDO_PER_SPALMATORE" _
                    & "       from siscom_mi.rapporti_utenza u," _
                    & "            siscom_mi.v_saldo_bollette b," _
                    & "            (select v.id_bolletta," _
                    & "                    sum(v.importo) tot_monte_canone," _
                    & "                    sum(v.imp_pagato) tot_monte_canone_pag" _
                    & "               from siscom_mi.bol_bollette_voci v" _
                    & "              where v.id_voce in" _
                    & "                    (select t.id" _
                    & "                       from SISCOM_MI.T_VOCI_BOLLETTA t" _
                    & "                      where t.gruppo = 4" _
                    & "                        and t.tipo_voce in (2, 10, 13))" _
                    & "              group by v.id_bolletta) x" _
                    & "      where u.id = b.id_contratto" _
                    & "        and b.id = x.id_bolletta(+) " _
                    & condizioneDateScad _
                    & "        and b.id_tipo not in (3, 4, 5, 9, 22, 25)" _
                    & "        and b.fl_annullata = 0" _
                    & "        and b.id_bolletta_storno is null" _
                    & "        and (id_bolletta_Ric is not null or id_rateizzazione is not null) " _
                    & "      group by u.cod_contratto, u.id, u.cod_tipologia_contr_loc)" _
                    & "      )) " _
                    & " group by " _
                    & " DATA_eSTRAZIONE,DATA_sCADENZA_BOLLETTE,RU,ID_CONTRATTO,COD_TIPOLOGIA_CONTR_LOC)" _
                    & "      z" _
                & " where z.id_contratto = y.id_cnt(+)"

            EstraiDati(par.cmd.CommandText, 14)
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Estrazione avviata correttamente!", 300, 150, "Info", "apriMaschera", Nothing)

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CreaRptEmessoTotaleOld()
        Try

            Dim condizioneDateScad As String = ""
            If dataScad <> "" Then
                condizioneDateScad = " and b.data_scadenza <= '" & dataScad & "'"
            End If

            par.cmd.CommandText = "select DATA_ESTRAZIONE," _
                    & "       DATA_SCADENZA_BOLLETTE," _
                    & "       RU," _
                    & "       ID_CONTRATTO," _
                    & "       COD_TIPOLOGIA_CONTR_LOC," _
                    & "       TOT_EMESSO_CONTABILE + nvl(importo_spese_mor_rat, 0) TOT_EMESSO_CONTABILE," _
                    & "        (select sum(importo) from siscom_mi.bol_bollette_voci,siscom_mi.bol_bollette " _
                    & "        where bol_bollette_voci.id_bolletta=bol_bollette.id and bol_Bollette.id_contratto=z.id_contratto " _
                    & "           and bol_bollette.data_scadenza <= '" & dataScad & "'" _
                    & "           and bol_bollette.id_tipo not in (3, 4, 5, 9, 22, 25)" _
                    & "           and bol_bollette.fl_annullata = 0" _
                    & "           and bol_bollette.id_bolletta_storno is null" _
                    & "         and exists (select id from siscom_mi.t_Voci_bolletta where t_voci_bolletta.id=id_voce and gruppo = 4 and tipo_voce in (2, 10, 13))) as tot_monte_canone, " _
                    & "                          (select sum(imp_pagato) from siscom_mi.bol_bollette_voci,siscom_mi.bol_bollette " _
                    & "        where bol_bollette_voci.id_bolletta=bol_bollette.id and bol_Bollette.id_contratto=z.id_contratto " _
                    & "           and bol_bollette.data_scadenza <= '" & dataScad & "'" _
                    & "           and bol_bollette.id_tipo not in (3, 4, 5, 9, 22, 25)" _
                    & "           and bol_bollette.fl_annullata = 0" _
                    & "           and bol_bollette.id_bolletta_storno is null" _
                    & "         and exists (select id from siscom_mi.t_Voci_bolletta where t_voci_bolletta.id=id_voce" _
                    & "                         and gruppo = 4 and tipo_voce in (2, 10, 13))) as tot_monte_canone_pag," _
                    & "       TOT_PAGATO_CONTABILE + nvl(importo_spese_mor_rat_pag, 0) TOT_PAGATO_CONTABILE," _
                    & "       TOT_PARTITA_GIRO," _
                    & "       TOT_PARTITA_GIRO_PAGATO," _
                    & "       TOT_RUOLO," _
                    & "       TOT_RUOLO_PAGATO," _
                    & "       TOT_SALDO_CONTABILE +" _
                    & "       (nvl(importo_spese_mor_rat, 0) - nvl(importo_spese_mor_rat_pag, 0)) TOT_SALDO_CONTABILE," _
                    & "       TOT_SALDO_PER_SPALMATORE +" _
                    & "       (nvl(importo_spese_mor_rat, 0) - nvl(importo_spese_mor_rat_pag, 0)) TOT_SALDO_PER_SPALMATORE" _
                    & "  from (select bo.id_contratto id_cnt," _
                    & "               sum(nvl(bov.importo, 0)) importo_spese_mor_rat," _
                    & "               sum(nvl(bov.imp_pagato, 0)) importo_spese_mor_rat_pag" _
                    & "          from siscom_mi.bol_bollette bo, siscom_mi.bol_bollette_voci bov" _
                    & "         where bo.id = bov.id_bolletta" _
                    & "           and bo.id_tipo in (4, 5)" _
                    & "           and bo.fl_annullata = 0" _
                    & "           and bo.id_bolletta_storno is null" _
                    & "           and bov.id_voce in (678, 407, 95, 628)" _
                    & "         group by id_contratto) y," _
                    & "       (select to_char(sysdate, 'DD/MM/YYYY') data_estrazione," _
                    & "               '" & par.FormattaData(dataScad) & "' data_scadenza_bollette," _
                    & "               u.cod_contratto ru," _
                    & "               u.id id_contratto," _
                    & "               u.cod_tipologia_contr_loc," _
                    & "               SUM(b.EMESSO_CONTABILE) tot_emesso_contabile," _
                    & "               SUM(b.INCASSATO_CONTABILE) tot_pagato_contabile," _
                    & "               sum(nvl(b.quota_sind_b, 0)) tot_partita_giro," _
                    & "               sum(nvl(b.quota_sind_pagata_b, 0)) tot_partita_giro_pagato," _
                    & "               sum(nvl(b.importo_ruolo, 0)) tot_ruolo," _
                    & "               sum(nvl(b.imp_ruolo_pagato, 0)) tot_ruolo_pagato," _
                    & "               sum(b.saldo_contabile) tot_saldo_contabile," _
                    & "               sum(b.EMESSO_CONTABILE) - sum(b.INCASSATO_CONTABILE) -" _
                    & "               sum(nvl(b.importo_ruolo, 0)) TOT_SALDO_PER_SPALMATORE" _
                    & "          from siscom_mi.rapporti_utenza u," _
                    & "               siscom_mi.v_saldo_bollette b" _
                    & "         where u.id = b.id_contratto " _
                    & condizioneDateScad _
                    & "           and b.id_tipo not in (3, 4, 5, 9, 22, 25)" _
                    & "           and b.fl_annullata = 0" _
                    & "           and b.id_bolletta_storno is null" _
                    & "         group by u.cod_contratto, u.id, u.cod_tipologia_contr_loc) z" _
                    & " where z.id_contratto = y.id_cnt(+)"

            EstraiDati(par.cmd.CommandText, 14)
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Estrazione avviata correttamente!", 300, 150, "Info", "apriMaschera", Nothing)

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub EstraiDati(ByVal strQuery As String, ByVal idTipoReport As Integer)
        Try
            Dim sComando As String = strQuery
            connData.apri()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = par.cmd.ExecuteScalar

            If Len(strQuery) < 4000 Then

                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE, Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'" & strQuery.ToString.Replace("'", "''") & "', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,NULL)"
            Else
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE,Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,:TEXT_DATA)"


                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = strQuery
                End With

                par.cmd.Parameters.Add(paramData)
                paramData = Nothing


            End If
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnProcedi_Click(sender As Object, e As EventArgs) Handles btnProcedi.Click
        CreaRptEmessoTotale()
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub
End Class
