
Partial Class Gestione_locatari_ElencoIstanze
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            idContratto.Value = par.IfEmpty(Request.QueryString("IDC"), 0)
        End If
    End Sub

    Protected Sub dgvIstanze_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvIstanze.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If

            Dim query As String = "select domande_bando_vsa.pg,id_dichiarazione as id_dich,to_date(data_presentazione, 'yyyymmdd') as data_presentazione, " _
                                  & " (select descrizione from t_stato_istanza where t_stato_istanza.id=domande_bando_vsa.id_stato_istanza) as stato," _
                                  & " (select descrizione from t_motivo_domanda_vsa where t_motivo_domanda_vsa.id=domande_bando_vsa.id_motivo_domanda) as tipo_istanza," _
                                 & " (SELECT distinct descrizione" _
                                 & " FROM t_stati_decisionali, iter_autorizzativo_istanza" _
                                 & " WHERE T_STATI_DECISIONALI.id = id_stato_decisione" _
                                 & " AND iter_autorizzativo_istanza.id_istanza =" _
                                 & " domande_bando_vsa.id" _
                                 & " AND iter_autorizzativo_istanza.ID =" _
                                 & " (SELECT MAX (iter_autorizzativo_istanza.ID)" _
                                 & " FROM iter_autorizzativo_istanza" _
                                 & " WHERE iter_autorizzativo_istanza.id_istanza =" _
                                 & " domande_bando_vsa.id)) as decisione" _
                                  & " from domande_bando_vsa,dichiarazioni_vsa where dichiarazioni_vsa.id=id_dichiarazione " _
                                  & " and fl_nuova_normativa=1 and id_contratto=" & idContratto.Value _
                                  & " order by domande_bando_vsa.pg desc"
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)

            If apertanow Then
                connData.chiudi(False)
            End If
            dgvIstanze.DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvDich_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDich.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If

            Dim strSQL0 As String = ""
            Dim strSQL As String = ""
            Dim conta As Integer = 0
            Dim annoReddito As String = ""
            Dim completaSQL As String = ""

            Dim condizioneQuery As String = ""
            Dim idDomandaAggRedd As Long = 0
            par.cmd.CommandText = "select cod_contratto,id_domanda from siscom_mi.rapporti_utenza where id=" & idContratto.Value & " and id_domanda in (select id from domande_bando_vsa where id_motivo_domanda=11)"
            Dim myReaderE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderE.Read Then
                idDomandaAggRedd = par.IfNull(myReaderE(1), 0)
                If idDomandaAggRedd <> 0 Then
                    condizioneQuery = "AND (DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & par.IfNull(myReaderE("cod_contratto"), "") & "' or DOMANDE_BANDO_VSA.id= " & idDomandaAggRedd & ")"
                End If
            End If
            myReaderE.Close()


            If idDomandaAggRedd = 0 Then
                par.cmd.CommandText = "select cod_contratto,id_isee from siscom_mi.rapporti_utenza where id=" & idContratto.Value & " and id_isee in (select id_dichiarazione from domande_bando_vsa where id_motivo_domanda=11)"
                myReaderE = par.cmd.ExecuteReader()
                If myReaderE.Read Then
                    idDomandaAggRedd = par.IfNull(myReaderE(1), 0)
                    If idDomandaAggRedd <> 0 Then
                        condizioneQuery = "AND (DOMANDE_BANDO_VSA.CONTRATTO_NUM = '" & par.IfNull(myReaderE("cod_contratto"), "") & "' or dichiarazioni_vsa.id= " & idDomandaAggRedd & ")"
                    End If
                End If
                myReaderE.Close()
            End If


            If idDomandaAggRedd = 0 Then
                condizioneQuery = "AND DOMANDE_BANDO_VSA.CONTRATTO_NUM = (select cod_contratto from siscom_mi.rapporti_utenza where id=" & idContratto.Value & ")"
            End If


            Dim dtDomande As New Data.DataTable

            strSQL = "Select DOMANDE_BANDO_VSA.ID as ID_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,T_STATI_DICHIARAZIONE.DESCRIZIONE As STATO_DICH,(Case When DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE = 1 Then 'AUTORIZZATA' ELSE (CASE WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 1 THEN 'SOTTOPOSTA A DECISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 2 THEN 'ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 3 THEN 'NON ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 4 THEN 'SOTTOPOSTA A REVISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 5 THEN 'REVISIONE ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 6 THEN 'REVISIONE NON ACCOLTA' ELSE 'NESSUNA DECISIONE' END) END) AS STATO_DOM, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA, " _
& "(CASE WHEN DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 2 OR DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 3 THEN replace(replace('<img alt=£Cambio£ title=£Cambia intestatario domanda£ src=£../NuoveImm/Img_CambioDich.png£ onclick=£window.open(''../VSA/Locatari/CambioIntestazione1.aspx?ID='||DOMANDE_BANDO_VSA.ID||'$PG='||DOMANDE_BANDO_VSA.PG||''',''CambioDich'',''height=550,top=200,left=350,width=670'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') ELSE '' END) as CAMBIO " _
& "FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,VSA_DECISIONI_REV_C,T_STATI_DICHIARAZIONE WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE and DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD AND VSA_DECISIONI_REV_C.DATA = (SELECT MAX(DATA) FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=DOMANDE_BANDO_VSA.ID) " _
                & " and nvl(DOMANDE_BANDO_VSA.fl_nuova_normativa,0)=0 AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA " & condizioneQuery & " AND VSA_DECISIONI_REV_C.ID_DOMANDA(+) = DOMANDE_BANDO_VSA.ID " _
                & "UNION " _
                & "SELECT DOMANDE_BANDO_VSA.ID as ID_DOM, T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,T_STATI_DICHIARAZIONE.DESCRIZIONE AS STATO_DICH,'NESSUNA DECISIONE' AS STATO_DOM, " _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'',''top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA, " _
& "(CASE WHEN DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 2 OR DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = 3 THEN replace(replace('<img alt=£Cambio£ title=£Cambia intestatario domanda£ src=£../NuoveImm/Img_CambioDich.png£ onclick=£window.open(''../VSA/Locatari/CambioIntestazione1.aspx?ID='||DOMANDE_BANDO_VSA.ID||'$PG='||DOMANDE_BANDO_VSA.PG||''',''CambioDich'',''height=550,top=200,left=350,width=670'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') ELSE '' END) as CAMBIO " _
& "FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_STATI_DICHIARAZIONE WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE and DICHIARAZIONI_VSA.ID_STATO=T_STATI_DICHIARAZIONE.COD " _
                & " and nvl(DOMANDE_BANDO_VSA.fl_nuova_normativa,0)=0  AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA  " & condizioneQuery & " AND DOMANDE_BANDO_VSA.ID NOT IN (SELECT id_domanda FROM VSA_DECISIONI_REV_C) ORDER BY PG_DOMANDA DESC"
            Dim dt As New Data.DataTable

            dt = par.getDataTableGrid(strSQL)

            If apertanow Then
                connData.chiudi(False)
            End If
            dgvDich.DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
