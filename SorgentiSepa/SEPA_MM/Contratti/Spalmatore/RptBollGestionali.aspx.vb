Imports System.IO
Imports Telerik.Web.UI
Partial Class Contratti_Spalmatore_RptBollGestionali
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Sub Contratti_Spalmatore_RptBollGestionali_Load(sender As Object, e As EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            Dim sb As String = "function f(){radconfirm('Vuoi esportare tutte le scritture gestionali presenti a sistema?', procedi, 400,150,null,'Attenzione',null); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);"
            Me.Page.ClientScript.RegisterStartupScript(GetType(Page), "messaggioNoPostback" & Format(Now, "ddMMyyyyHHmmss"), sb.ToString(), True)
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        par.cmd.CommandText = " select distinct to_char(sysdate, 'DD/MM/YYYY') data_estrazione," _
                    & "       u.cod_contratto ru," _
                    & "       g.id_contratto," _
                    & "       siscom_mi.getdata(u.data_decorrenza) data_decorrenza_contratto," _
                    & "       siscom_mi.getdata(u.data_riconsegna) data_chiusura_contratto," _
                    & "       u.cod_tipologia_contr_loc tipo_ru," _
                    & "       (CASE" _
                    & "         WHEN u.PROVENIENZA_ASS = 1 AND m.ID_DESTINAZIONE_USO <> 2 then" _
                    & "          'ERP Sociale'" _
                    & "         WHEN m.ID_DESTINAZIONE_USO = 2 THEN" _
                    & "          'ERP Moderato'" _
                    & "         WHEN u.PROVENIENZA_ASS = 12 THEN" _
                    & "          'CANONE CONVENZ.'" _
                    & "         WHEN u.PROVENIENZA_ASS = 10 THEN" _
                    & "          'FORZE DELL''ORDINE'" _
                    & "         WHEN u.DEST_USO = 'C' THEN" _
                    & "          'Cooperative'" _
                    & "         WHEN u.DEST_USO = 'P' THEN" _
                    & "          '431 P.O.R.'" _
                    & "         WHEN u.DEST_USO = 'D' THEN" _
                    & "          '431/98 ART.15 R.R.1/2004'" _
                    & "         WHEN u.DEST_USO = 'V' THEN" _
                    & "          '431/98 ART.15 C.2 R.R.1/2004'" _
                    & "         WHEN u.DEST_USO = 'S' THEN" _
                    & "          '431/98 Speciali'" _
                    & "         WHEN u.DEST_USO = '0' THEN" _
                    & "          'Standard'" _
                    & "       END) AS tipo_ru_specifico," _
                    & "       g.id id_gestionale," _
                    & "       g.id_tipo," _
                    & "       t.descrizione tipo_gestionale," _
                    & "       t.acronimo," _
                    & "       siscom_mi.getdata(g.riferimento_da) riferimento_da," _
                    & "       siscom_mi.getdata(g.riferimento_a) riferimento_a," _
                    & "       siscom_mi.getdata(g.data_emissione) data_emissione," _
                    & "       g.importo_totale," _
                    & "       (CASE" _
                    & "       WHEN g.importo_totale < 0 THEN 'CREDITO'" _
                    & "       WHEN g.importo_totale > 0 THEN 'DEBITO'" _
                    & "       ELSE 'ZERO'" _
                    & "       END)" _
                    & "       tipo_importo," _
                    & "       decode(g.tipo_applicazione," _
                    & "              'T'," _
                    & "              'SI'," _
                    & "              'N'," _
                    & "              'NO'," _
                    & "              'P'," _
                    & "              'PARZIALE'," _
                    & "              'Non definito') Lavorato," _
                    & "       siscom_mi.getdata(g.data_applicazione) data_lavorazione," _
                    & "       decode(g.fl_sbloccata, 0, 'ROSSO', 'VERDE') semaforo," _
                    & "       (case when  (SELECT COUNT (id_contratto)" _
                    & "     FROM siscom_mi.SPALM_KPI1" _
                    & "    WHERE id_contratto = u.id)>0 then 'SI' else 'NO' end)" _
                    & "     AS  KP1," _
                    & "       nvl(y.venduta, 'ND') UI_VENDUTA," _
                    & "       (case when (SELECT COUNT (id_contratto)" _
                    & "    FROM siscom_mi.SPALM_ART_15" _
                    & "   WHERE id_contratto = u.id)>0 then 'SI' else 'NO' end)" _
                    & "    AS ART_15_C2_BIS," _
                    & "       decode(nvl(x.id, 0), 0, 'NO', 'SI') RU_CON_RAT," _
                    & "       g.note" _
                    & "  from siscom_mi.rapporti_utenza u," _
                    & "       siscom_mi.bol_bollette_gest g," _
                    & "       siscom_mi.TIPO_BOLLETTE_GEST t," _
                    & "       SISCOM_MI.UNITA_CONTRATTUALE c," _
                    & "       SISCOM_MI.UNITA_IMMOBILIARI m," _
                    & "       (select i.id, decode(td.descrizione, 'Venduta', 'SI', 'NO') venduta" _
                    & "          from siscom_mi.unita_immobiliari  i," _
                    & "               SISCOM_MI.TIPO_DISPONIBILITA td" _
                    & "         where i.cod_tipo_disponibilita = td.cod) y," _
                    & "       (select id, id_contratto" _
                    & "          from siscom_mi.bol_rateizzazioni r" _
                    & "         where r.fl_annullata = 0) x" _
                    & " where u.id = g.id_contratto" _
                    & "   and c.ID_CONTRATTO(+) = u.ID" _
                    & "   AND m.ID(+) = c.ID_UNITA" _
                    & "   and g.id_tipo = t.id" _
                    & "   and c.id_unita_principale is null " _
                    & "   and nvl(g.id_unita, 0) = y.id(+)" _
                    & "   and u.id = x.id_contratto(+) "
        EstraiDati(par.cmd.CommandText, 15)
        CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Estrazione avviata correttamente!", 300, 150, "Info", "apriMaschera", Nothing)
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
End Class
