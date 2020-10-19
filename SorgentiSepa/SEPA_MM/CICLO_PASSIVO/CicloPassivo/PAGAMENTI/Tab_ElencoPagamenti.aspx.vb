' TAB ELENCO PAGAMENTI x ID_VOCE_PF (PF_VOCI.ID)
Imports System.Collections

Partial Class Tab_ElencoPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Expires = 0

            If Not IsPostBack Then
                'ID_VOCE_PF (PF_VOCI.ID)
                If Request.QueryString("V") <> "" Then
                    iD_VocePF = Request.QueryString("V")
                End If
                Select Case Request.QueryString("TIPO_RICERCA")
                    Case 1
                        DataGrid1.Visible = True
                        DataGrid2.Visible = False
                        DataGrid3.Visible = False
                        HFGriglia.Value = DataGrid1.ClientID
                        BindGrid1()

                    Case 2
                        DataGrid1.Visible = False
                        DataGrid2.Visible = True
                        DataGrid3.Visible = False

                        BindGrid2()
                        HFGriglia.Value = DataGrid2.ClientID
                    Case 3
                        DataGrid1.Visible = False
                        DataGrid2.Visible = False
                        DataGrid3.Visible = True

                        ' BindGrid3()
                        HFGriglia.Value = DataGrid3.ClientID

                End Select

                'Me.Session.Add("MODIFYMODAL", 0)


            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Public Property iD_VocePF() As Long
        Get
            If Not (ViewState("par_iD_VocePF") Is Nothing) Then
                Return CLng(ViewState("par_iD_VocePF"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_iD_VocePF") = value
        End Set

    End Property




    'SOLO PRENOTATI GRID1
    Private Sub BindGrid1()

        Dim StringaSql_PRE_1_0 As String = ""      'AMMINISTRAZIONE CONDOMINI BOZZA
        Dim StringaSql_PRE_1_1 As String = ""     'AMMINISTRAZIONE CONDOMINI EMESSO

        Dim StringaSql_PRE_2_0 As String = ""      'MOROSITA CONDOMINI BOZZA
        Dim StringaSql_PRE_2_1 As String = ""      'MOROSITA CONDOMINI EMESSO

        Dim StringaSql_PRE_3_0 As String = ""      'ODL MANUTENZIONI BOZZA
        Dim StringaSql_PRE_3_1 As String = ""      'ODL MANUTENZIONI EMESSO

        Dim StringaSql_PRE_4_0 As String = ""      'ALTRI PAGAMENTI BOZZA
        Dim StringaSql_PRE_4_1 As String = ""      'ALTRI PAGAMENTI EMESSO

        Dim StringaSql_PRE_5_0 As String = ""     'RITENUTA ACCONTO BOZZA
        Dim StringaSql_PRE_5_1 As String = ""     'RITENUTA ACCONTO EMESSO

        Dim StringaSql_PRE_6_0 As String = ""     'A CANONE BOZZA
        Dim StringaSql_PRE_6_1 As String = ""     'A CANONE EMESSO

        Dim StringaSql_PRE_7_0 As String = ""     'RRS MANUTENZIONI BOZZA
        Dim StringaSql_PRE_7_1 As String = ""     'RRS MANUTENZIONI EMESSO


        Dim SommaPrenotato0 As Decimal = 0
        Dim SommaPrenotato1 As Decimal = 0

        Dim FlagConnessione As Boolean

        Dim sFiliale As String = ""
        Dim sSelect1 As String = ""


        If Request.QueryString("ID_STRUTTURA") <> "" Then
            sFiliale = " ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA")
        End If

        'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
        '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
        'End If


        Try
            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            If par.IfEmpty(Request.QueryString("IDSEL"), 0) = 1 Then
                sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & iD_VocePF & "))"
            Else
                sSelect1 = "=" & iD_VocePF
            End If
            sSelect1 = "=" & iD_VocePF

            'IMPORTO PRENOTATO ************************************************************************************************
                                         

            'TIPO_PAGAMENTO = 1 AMMINISTRAZIONE CONDOMINI 
            StringaSql_PRE_1_0 = " Select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                                     & " 'AMMINISTRAZIONE CONDOMINI' as TIPO_PAGAMENTO, " _
                                     & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                                     & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                     & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                     & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                     & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                     & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                     & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                             & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                             & " where PRENOTAZIONI.TIPO_PAGAMENTO=1 " _
                             & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                             & "   and PRENOTAZIONI.ID_STATO=0 " _
                             & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                             & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                             & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_1_0 = StringaSql_PRE_1_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_1_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
            '                    & " 'AMMINISTRAZIONE CONDOMINI' as TIPO_PAGAMENTO, " _
            '                    & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=1 " _
            '            & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
            '            & "   and PRENOTAZIONI.ID_STATO=1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_1_1 = StringaSql_PRE_1_1 & " and PRENOTAZIONI." & sFiliale
            'End If

            'TIPO_PAGAMENTO=2       MOROSITA CONDOMINI
            StringaSql_PRE_2_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                         & " 'MOROSITA CONDOMINI' as TIPO_PAGAMENTO, " _
                         & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                         & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                         & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                         & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                         & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                         & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                 & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                 & " where PRENOTAZIONI.TIPO_PAGAMENTO=2 " _
                 & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                 & "   and PRENOTAZIONI.ID_STATO=0 " _
                 & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                 & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                 & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_2_0 = StringaSql_PRE_2_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_2_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
            '                    & " 'MOROSITA CONDOMINI' as TIPO_PAGAMENTO, " _
            '                    & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=2 " _
            '            & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
            '            & "   and PRENOTAZIONI.ID_STATO=1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_2_1 = StringaSql_PRE_2_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=3       ODL MANUTENZIONI   
            StringaSql_PRE_3_0 = "union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
                                & " 'ODL MANUTENZIONI' as TIPO_PAGAMENTO, " _
                                & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO = 3 " _
                        & " and manutenzioni.stato<>5 " _
                        & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                        & "   and PRENOTAZIONI.ID_STATO=0 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_3_0 = StringaSql_PRE_3_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_3_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
            '                    & " 'ODL MANUTENZIONI' as TIPO_PAGAMENTO, " _
            '                    & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO = 3 " _
            '            & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
            '            & "   and PRENOTAZIONI.ID_STATO=1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_3_1 = StringaSql_PRE_3_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=4       ORDINI e PAGAMENTI
            StringaSql_PRE_4_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
                                   & " 'ORDINI e PAGAMENTI' as TIPO_PAGAMENTO, " _
                                   & " ODL.DESCRIZIONE as DESCRIZIONE1," _
                                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                   & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                   & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                   & " '' as NUM_REPERTORIO " _
                           & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
                           & " where PRENOTAZIONI.TIPO_PAGAMENTO = 4 " _
                           & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                           & "   and PRENOTAZIONI.ID_STATO=0 " _
                           & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                           & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE " _
                           & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_4_0 = StringaSql_PRE_4_0 & " and PRENOTAZIONI." & sFiliale
            End If


            'StringaSql_PRE_4_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
            '                       & " 'ORDINI e PAGAMENTI' as TIPO_PAGAMENTO, " _
            '                       & " ODL.DESCRIZIONE as DESCRIZIONE1," _
            '                        & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                        & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                        & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                       & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
            '                       & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                       & " '' as NUM_REPERTORIO " _
            '               & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
            '               & " where PRENOTAZIONI.TIPO_PAGAMENTO = 4 " _
            '               & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
            '               & "   and PRENOTAZIONI.ID_STATO=1 " _
            '               & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '               & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE " _
            '               & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_4_1 = StringaSql_PRE_4_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=5       RITENUTA ACCONTO
            StringaSql_PRE_5_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
                                   & " 'RITENUTA ACCONTO' as TIPO_PAGAMENTO, " _
                                   & " ODL.DESCRIZIONE as DESCRIZIONE1," _
                                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
                                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                   & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                   & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                   & " '' as NUM_REPERTORIO " _
                           & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
                           & " where PRENOTAZIONI.TIPO_PAGAMENTO = 5 " _
                           & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                           & "   and PRENOTAZIONI.ID_STATO=0 " _
                           & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                           & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE_RIT " _
                           & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_5_0 = StringaSql_PRE_5_0 & " and PRENOTAZIONI." & sFiliale
            End If


            'StringaSql_PRE_5_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
            '                       & " 'RITENUTA ACCONTO' as TIPO_PAGAMENTO, " _
            '                       & " ODL.DESCRIZIONE as DESCRIZIONE1," _
            '                        & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                        & "     then  FORNITORI.COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
            '                        & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                       & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
            '                       & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                       & " '' as NUM_REPERTORIO " _
            '               & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
            '               & " where PRENOTAZIONI.TIPO_PAGAMENTO = 5 " _
            '               & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
            '               & "   and PRENOTAZIONI.ID_STATO=1 " _
            '               & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '               & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE_RIT " _
            '               & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_5_1 = StringaSql_PRE_5_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=6       PAGAMENTI a CANONE
            StringaSql_PRE_6_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                                & " 'PAGAMENTI a CANONE' as TIPO_PAGAMENTO, " _
                                & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO=6 " _
                        & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                        & "   and PRENOTAZIONI.ID_STATO=0 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_6_0 = StringaSql_PRE_6_0 & " and PRENOTAZIONI." & sFiliale
            End If

            StringaSql_PRE_6_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                                & " 'PAGAMENTI a CANONE' as TIPO_PAGAMENTO, " _
                                & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO=6 " _
                        & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                        & "   and PRENOTAZIONI.ID_STATO=1 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_6_1 = StringaSql_PRE_6_1 & " and PRENOTAZIONI." & sFiliale
            End If


            'TIPO_PAGAMENTO=7       RRS MANUTENZIONI   
            StringaSql_PRE_7_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
                                & " 'RRS MANUTENZIONI' as TIPO_PAGAMENTO, " _
                                & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO=7 " _
                        & " and manutenzioni.stato<>5 " _
                        & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
                        & "   and PRENOTAZIONI.ID_STATO=0 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_7_0 = StringaSql_PRE_7_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_7_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
            '                    & " 'RRS MANUTENZIONI' as TIPO_PAGAMENTO, " _
            '                    & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_PRENOTATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=7 " _
            '            & "   and PRENOTAZIONI.ID_PAGAMENTO Is null" _
            '            & "   and PRENOTAZIONI.ID_STATO=1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_7_1 = StringaSql_PRE_7_1 & " and PRENOTAZIONI." & sFiliale
            'End If

            '*************************************************************

            par.cmd.CommandText = StringaSql_PRE_1_0 _
                                & StringaSql_PRE_2_0 _
                                & StringaSql_PRE_3_0 _
                                & StringaSql_PRE_4_0 _
                                & StringaSql_PRE_5_0 _
                                & StringaSql_PRE_6_0 & StringaSql_PRE_6_1 _
                                & StringaSql_PRE_7_0 _
                                 & "order by TIPO_PAGAMENTO asc, ID_PRENOTAZIONE desc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable()

            da.Fill(ds)

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            da.Dispose()
            ds.Dispose()


            'SOMMA BOZZA + EMESSO
            par.cmd.CommandText = " select  sum(nvl(PRENOTAZIONI.IMPORTO_PRENOTATO,0)) as IMPORTO_PRENOTATO " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=0 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                SommaPrenotato0 = par.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()

            ''SOMMA EMESSO
            'par.cmd.CommandText = " select  sum(nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0)) as IMPORTO_PRENOTATO " _
            '                    & " from    SISCOM_MI.PRENOTAZIONI " _
            '                    & " where   ID_STATO=1 " _
            '                    & "   and   ID_PAGAMENTO is null " _
            '                    & "   and   ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            'End If

            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then
            '    SommaPrenotato1 = par.IfNull(myReader1(0), 0)
            'End If
            'myReader1.Close()


            SommaPrenotato1 = SommaPrenotato0 '+ SommaPrenotato1
            Me.lbl_Tot_PRE.Text = Format(SommaPrenotato1, "##,##0.00")
            '*************************

            Me.lbl_Prenotato.Visible = True
            Me.lbl_Tot_PRE.Visible = True

            Me.lbl_Consuntivo.Visible = False
            Me.lbl_Tot_CONS.Visible = False

            Me.lbl_Pagato.Visible = False
            Me.lbl_Tot_PAG.Visible = False


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try


    End Sub


    'SOLO CONSUNTIVATI anche se sono stata PAGATI (per il momento fin quando non ci dicono come fare, 
    ' perchè più prenotazioni  con voci doverse possono avere un unico pagamento

    Private Sub BindGrid2()

        Dim StringaSql_PRE_1_0 As String = ""       'AMMINISTRAZIONE CONDOMINI BOZZA
        Dim StringaSql_PRE_1_1 As String = ""       'AMMINISTRAZIONE CONDOMINI EMESSO

        Dim StringaSql_PRE_2_0 As String = ""      'MOROSITA CONDOMINI BOZZA
        Dim StringaSql_PRE_2_1 As String = ""      'MOROSITA CONDOMINI EMESSO

        Dim StringaSql_PRE_3_0 As String = ""      'ODL MANUTENZIONI BOZZA
        Dim StringaSql_PRE_3_1 As String = ""      'ODL MANUTENZIONI EMESSO

        Dim StringaSql_PRE_4_0 As String = ""     'ALTRI PAGAMENTI BOZZA
        Dim StringaSql_PRE_4_1 As String = ""      'ALTRI PAGAMENTI EMESSO

        Dim StringaSql_PRE_5_0 As String = ""     'RITENUTA ACCONTO BOZZA
        Dim StringaSql_PRE_5_1 As String = ""     'RITENUTA ACCONTO EMESSO

        Dim StringaSql_PRE_6_0 As String = ""    'A CANONE BOZZA
        Dim StringaSql_PRE_6_1 As String = ""     'A CANONE EMESSO

        Dim StringaSql_PRE_7_0 As String = ""      'RRS MANUTENZIONI BOZZA
        Dim StringaSql_PRE_7_1 As String = ""     'RRS MANUTENZIONI EMESSO


        Dim SommaConsuntivato As Decimal = 0

        Dim FlagConnessione As Boolean

        Dim sFiliale As String = ""
        Dim sSelect1 As String = ""


        If Request.QueryString("ID_STRUTTURA") <> "" Then
            sFiliale = " ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA")
        End If

        'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
        '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
        'End If


        Try
            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            If par.IfEmpty(Request.QueryString("IDSEL"), 0) = 1 Then
                sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & iD_VocePF & "))"
            Else
                sSelect1 = "=" & iD_VocePF
            End If
            sSelect1 = "=" & iD_VocePF

            'IMPORTO PRENOTATO ************************************************************************************************
            'and PRENOTAZIONI.ID_PAGAMENTO Is not null

            'TIPO_PAGAMENTO = 1 AMMINISTRAZIONE CONDOMINI 
            StringaSql_PRE_1_0 = " Select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                                     & " 'AMMINISTRAZIONE CONDOMINI' as TIPO_PAGAMENTO, " _
                                     & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                                     & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                     & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                     & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                     & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                                     & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                     & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                             & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                             & " where PRENOTAZIONI.TIPO_PAGAMENTO=1 " _
                             & "   and PRENOTAZIONI.ID_STATO>=1 " _
                             & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                             & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                             & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_1_0 = StringaSql_PRE_1_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_1_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
            '                    & " 'AMMINISTRAZIONE CONDOMINI' as TIPO_PAGAMENTO, " _
            '                    & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=1 " _
            '            & "   and PRENOTAZIONI.ID_STATO>=1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_1_1 = StringaSql_PRE_1_1 & " and PRENOTAZIONI." & sFiliale
            'End If

            'TIPO_PAGAMENTO=2       MOROSITA CONDOMINI
            StringaSql_PRE_2_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                         & " 'MOROSITA CONDOMINI' as TIPO_PAGAMENTO, " _
                         & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                         & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                         & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                         & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                         & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                         & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                 & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                 & " where PRENOTAZIONI.TIPO_PAGAMENTO=2 " _
                 & "   and PRENOTAZIONI.ID_STATO>=1 " _
                 & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                 & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                 & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_2_0 = StringaSql_PRE_2_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_2_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
            '                    & " 'MOROSITA CONDOMINI' as TIPO_PAGAMENTO, " _
            '                    & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=2 " _
            '            & "   and PRENOTAZIONI.ID_STATO>=1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_2_1 = StringaSql_PRE_2_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=3       ODL MANUTENZIONI   
            StringaSql_PRE_3_0 = "union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
                                & " 'ODL MANUTENZIONI' as TIPO_PAGAMENTO, " _
                                & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO = 3 " _
                        & " and manutenzioni.stato<>5 " _
                        & "   and PRENOTAZIONI.ID_STATO>=1 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_3_0 = StringaSql_PRE_3_0 & " and PRENOTAZIONI." & sFiliale
            End If


            'TIPO_PAGAMENTO=4       ORDINI e PAGAMENTI
            StringaSql_PRE_4_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
                                   & " 'ORDINI e PAGAMENTI' as TIPO_PAGAMENTO, " _
                                   & " ODL.DESCRIZIONE as DESCRIZIONE1," _
                                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                   & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                                   & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                   & " '' as NUM_REPERTORIO " _
                           & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
                           & " where PRENOTAZIONI.TIPO_PAGAMENTO = 4 " _
                           & "   and PRENOTAZIONI.ID_STATO>=1 " _
                           & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                           & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE " _
                           & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_4_0 = StringaSql_PRE_4_0 & " and PRENOTAZIONI." & sFiliale
            End If


            'StringaSql_PRE_4_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
            '                       & " 'ORDINI e PAGAMENTI' as TIPO_PAGAMENTO, " _
            '                       & " ODL.DESCRIZIONE as DESCRIZIONE1," _
            '                        & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                        & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                        & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                       & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
            '                       & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                       & " '' as NUM_REPERTORIO " _
            '               & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
            '               & " where PRENOTAZIONI.TIPO_PAGAMENTO = 4 " _
            '               & "   and PRENOTAZIONI.ID_PAGAMENTO Is not null" _
            '               & "   and PRENOTAZIONI.ID_STATO>1 " _
            '               & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '               & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE " _
            '               & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_4_1 = StringaSql_PRE_4_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=5       RITENUTA ACCONTO
            StringaSql_PRE_5_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
                                   & " 'RITENUTA ACCONTO' as TIPO_PAGAMENTO, " _
                                   & " ODL.DESCRIZIONE as DESCRIZIONE1," _
                                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
                                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                   & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                                   & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                   & " '' as NUM_REPERTORIO " _
                           & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
                           & " where PRENOTAZIONI.TIPO_PAGAMENTO = 5 " _
                           & "   and PRENOTAZIONI.ID_STATO>=1 " _
                           & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                           & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE_RIT " _
                           & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_5_0 = StringaSql_PRE_5_0 & " and PRENOTAZIONI." & sFiliale
            End If


            'StringaSql_PRE_5_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as PROG_ANNO," _
            '                       & " 'RITENUTA ACCONTO' as TIPO_PAGAMENTO, " _
            '                       & " ODL.DESCRIZIONE as DESCRIZIONE1," _
            '                        & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                        & "     then  FORNITORI.COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
            '                        & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                       & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
            '                       & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                       & " '' as NUM_REPERTORIO " _
            '               & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL " _
            '               & " where PRENOTAZIONI.TIPO_PAGAMENTO = 5 " _
            '               & "   and PRENOTAZIONI.ID_PAGAMENTO Is not null" _
            '               & "   and PRENOTAZIONI.ID_STATO>1 " _
            '               & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '               & "   and PRENOTAZIONI.ID                   =ODL.ID_PRENOTAZIONE_RIT " _
            '               & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_5_1 = StringaSql_PRE_5_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=6       PAGAMENTI a CANONE (per i PAGAMENTI a canone è diverso )
            StringaSql_PRE_6_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
                                & " 'PAGAMENTI a CANONE' as TIPO_PAGAMENTO, " _
                                & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO=6 " _
                        & "   and PRENOTAZIONI.ID_PAGAMENTO is not null " _
                        & "   and PRENOTAZIONI.ID_STATO>1 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_6_0 = StringaSql_PRE_6_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_6_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,'' as PROG_ANNO," _
            '                    & " 'PAGAMENTI a CANONE' as TIPO_PAGAMENTO, " _
            '                    & " PRENOTAZIONI.DESCRIZIONE as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=6 " _
            '            & "   and PRENOTAZIONI.ID_PAGAMENTO Is not null" _
            '            & "   and PRENOTAZIONI.ID_STATO>1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_6_1 = StringaSql_PRE_6_1 & " and PRENOTAZIONI." & sFiliale
            'End If


            'TIPO_PAGAMENTO=7       RRS MANUTENZIONI   
            StringaSql_PRE_7_0 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
                                & " 'RRS MANUTENZIONI' as TIPO_PAGAMENTO, " _
                                & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                & " TRIM(TO_CHAR( nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0) ,'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
                                & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
                                & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
                        & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
                        & " where PRENOTAZIONI.TIPO_PAGAMENTO=7 " _
                        & " and manutenzioni.stato<>5 " _
                        & "   and PRENOTAZIONI.ID_STATO>=1 " _
                        & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
                        & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                        & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
                        & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            If sFiliale <> "" Then
                StringaSql_PRE_7_0 = StringaSql_PRE_7_0 & " and PRENOTAZIONI." & sFiliale
            End If

            'StringaSql_PRE_7_1 = " union select PRENOTAZIONI.ID as ID_PRENOTAZIONE,(SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as PROG_ANNO," _
            '                    & " 'RRS MANUTENZIONI' as TIPO_PAGAMENTO, " _
            '                    & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
            '                    & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
            '                    & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
            '                    & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
            '                    & " TRIM(TO_CHAR(  nvl(PRENOTAZIONI.IMPORTO_APPROVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_APPROVATO, " _
            '                    & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PRENOTAZIONE, " _
            '                    & " APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
            '            & " from SISCOM_MI.PRENOTAZIONI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
            '            & " where PRENOTAZIONI.TIPO_PAGAMENTO=7 " _
            '            & "   and PRENOTAZIONI.ID_PAGAMENTO Is not  null" _
            '            & "   and PRENOTAZIONI.ID_STATO>1 " _
            '            & "   and PRENOTAZIONI.ID_FORNITORE         =SISCOM_MI.FORNITORI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID                   =MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
            '            & "   and PRENOTAZIONI.ID_APPALTO           =APPALTI.ID (+) " _
            '            & "   and PRENOTAZIONI.ID_VOCE_PF" & sSelect1

            'If sFiliale <> "" Then
            '    StringaSql_PRE_7_1 = StringaSql_PRE_7_1 & " and PRENOTAZIONI." & sFiliale
            'End If

            '*************************************************************

            par.cmd.CommandText = StringaSql_PRE_1_0 _
                                & StringaSql_PRE_2_0 _
                                & StringaSql_PRE_3_0 _
                                & StringaSql_PRE_4_0 _
                                & StringaSql_PRE_5_0 _
                                & StringaSql_PRE_6_0 _
                                & StringaSql_PRE_7_0 _
                                 & "order by TIPO_PAGAMENTO asc, ID_PRENOTAZIONE desc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable()

            da.Fill(ds)

            DataGrid2.DataSource = ds
            DataGrid2.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            da.Dispose()
            ds.Dispose()


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_VOCE_PF" & sSelect1 _
                                & "   and  ID_STATO>=1 " _
                                & "   and  ID_PAGAMENTO Is not null "

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                SommaConsuntivato = par.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()



            Me.lbl_Tot_CONS.Text = Format(SommaConsuntivato, "##,##0.00")
            'lbl_Tot_PAG.Text = Format(SommaLiquidato, "##,##0.00")


            Me.lbl_Prenotato.Visible = False
            Me.lbl_Tot_PRE.Visible = False

            Me.lbl_Consuntivo.Visible = True
            Me.lbl_Tot_CONS.Visible = True

            Me.lbl_Pagato.Visible = False
            Me.lbl_Tot_PAG.Visible = False



            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try


    End Sub

    ''SOLO CONSUNTIVATI  GRID2
    'Private Sub BindGrid2()

    '    Dim StringaSql_PAG_1_5 As String    'AMMINISTRAZIONE CONDOMINI
    '    Dim StringaSql_PAG_2_5 As String    'MOROSITA CONDOMINI 
    '    Dim StringaSql_PAG_3_5 As String    'ODL MANUTENZIONI
    '    Dim StringaSql_PAG_4_5 As String    'ALTRI PAGAMENTI
    '    Dim StringaSql_PAG_5_5 As String    'RITENUTA ACCONTO
    '    Dim StringaSql_PAG_6_5 As String    'A CANONE
    '    Dim StringaSql_PAG_7_5 As String    'RRS MANUTENZIONI

    '    Dim SommaConsuntivato As Decimal = 0
    '    Dim SommaLiquidato As Decimal = 0

    '    Dim StringaSqlLiquidato As String
    '    Dim sHaving As String

    '    Dim FlagConnessione As Boolean

    '    Dim sFiliale As String = ""
    '    Dim sSelect1 As String = ""


    '    If Request.QueryString("ID_STRUTTURA") <> "" Then
    '        sFiliale = " ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA")
    '    End If

    '    'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
    '    '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
    '    'End If


    '    Try
    '        '*******************APERURA CONNESSIONE*********************
    '        FlagConnessione = False
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            FlagConnessione = True
    '        End If

    '        If par.IfEmpty(Request.QueryString("IDSEL"), 0) = 1 Then
    '            sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
    '                             & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & iD_VocePF & "))"
    '        Else
    '            sSelect1 = "=" & iD_VocePF
    '        End If
    '        sSelect1 = "=" & iD_VocePF


    '        '*************************************************************
    '        'IMPORTO CONSUNTIVATO 

    '        StringaSqlLiquidato = " nvl((select sum(PAGAMENTI_LIQUIDATI.IMPORTO) " _
    '                            & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
    '                            & " where  ID_PAGAMENTO=PAGAMENTI.ID),0) "

    '        sHaving = " and ( NVL(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ">0) "

    '        ''AMMINISTRAZIONE CONDOMINI
    '        StringaSql_PAG_1_5 = " select  distinct(PAGAMENTI.ID) as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                                & " 'AMMINISTRAZIONE CONDOMINI' as TIPO_PAGAMENTO, " _
    '                                & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                                & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                                & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                                & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                                & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                                & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                            & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                            & " where PAGAMENTI.TIPO_PAGAMENTO=1 " _
    '                            & "   and PAGAMENTI.ID_STATO>0 " _
    '                            & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                            & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+) " & sHaving _
    '                            & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                                       & " where  ID_VOCE_PF" & sSelect1 _
    '                                                                       & "   and  ID_STATO>1 " _
    '                                                                       & "   and  TIPO_PAGAMENTO=1 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_1_5 = StringaSql_PAG_1_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_1_5 = StringaSql_PAG_1_5 & ")"
    '        End If


    '        'MOROSITA CONDOMINI 
    '        StringaSql_PAG_2_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                       & " 'MOROSITA CONDOMINI' as TIPO_PAGAMENTO, " _
    '                       & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                       & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                       & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                       & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                       & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                       & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '               & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '               & " where PAGAMENTI.TIPO_PAGAMENTO=2 " _
    '               & "   and PAGAMENTI.ID_STATO>0 " _
    '               & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '               & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+)  " & sHaving _
    '               & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                          & " where  ID_VOCE_PF" & sSelect1 _
    '                                                          & "   and  ID_STATO>1 " _
    '                                                          & "   and  TIPO_PAGAMENTO=2 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_2_5 = StringaSql_PAG_2_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_2_5 = StringaSql_PAG_2_5 & ")"
    '        End If


    '        'ODL MANUTENZIONI
    '        StringaSql_PAG_3_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'ODL MANUTENZIONI' as TIPO_PAGAMENTO, " _
    '                            & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=3 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID            =MANUTENZIONI.ID_PAGAMENTO " _
    '                    & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+)  " & sHaving _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=3 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_3_5 = StringaSql_PAG_3_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_3_5 = StringaSql_PAG_3_5 & ")"
    '        End If


    '        'ALTRI PAGAMENTI
    '        StringaSql_PAG_4_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'ORDINI e PAGAMENTI' as TIPO_PAGAMENTO, " _
    '                            & " ODL.DESCRIZIONE as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,'' as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL" _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=4 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID            =ODL.ID_PAGAMENTO  " & sHaving _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=4 "

    '        If sFiliale <> "" Then
    '            StringaSql_PAG_4_5 = StringaSql_PAG_4_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_4_5 = StringaSql_PAG_4_5 & ")"
    '        End If


    '        'RITENUTA ACCONTO
    '        StringaSql_PAG_5_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'RITENUTA ACCONTO' as TIPO_PAGAMENTO, " _
    '                            & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,'' as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI" _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=5 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+)  " & sHaving _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=5 "

    '        If sFiliale <> "" Then
    '            StringaSql_PAG_5_5 = StringaSql_PAG_5_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_5_5 = StringaSql_PAG_5_5 & ")"
    '        End If


    '        'A CANONE
    '        StringaSql_PAG_6_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'PAGAMENTI a CANONE' as TIPO_PAGAMENTO, " _
    '                            & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=6 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+)  " & sHaving _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=6 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_6_5 = StringaSql_PAG_6_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_6_5 = StringaSql_PAG_6_5 & ")"
    '        End If


    '        'RRS MANUTENZIONI
    '        StringaSql_PAG_7_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'RRS MANUTENZIONI' as TIPO_PAGAMENTO, " _
    '                            & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR(nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) - " & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=7 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID            =MANUTENZIONI.ID_PAGAMENTO " _
    '                    & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+)  " & sHaving _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=7 "

    '        If sFiliale <> "" Then
    '            StringaSql_PAG_7_5 = StringaSql_PAG_7_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_7_5 = StringaSql_PAG_7_5 & ")"
    '        End If


    '        par.cmd.CommandText = StringaSql_PAG_1_5 _
    '                            & StringaSql_PAG_2_5 _
    '                            & StringaSql_PAG_3_5 _
    '                            & StringaSql_PAG_4_5 _
    '                            & StringaSql_PAG_5_5 _
    '                            & StringaSql_PAG_6_5 _
    '                            & StringaSql_PAG_7_5 & "order by TIPO_PAGAMENTO asc, ID_PAGAMENTO desc "

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim ds As New Data.DataTable()

    '        da.Fill(ds)


    '        DataGrid2.DataSource = ds
    '        DataGrid2.DataBind()

    '        da.Dispose()
    '        ds.Dispose()


    '        'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
    '        par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
    '                            & " from   SISCOM_MI.PRENOTAZIONI " _
    '                            & " where  ID_VOCE_PF" & sSelect1 _
    '                            & "   and  ID_STATO>1 "

    '        If sFiliale <> "" Then
    '            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
    '        End If

    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
    '        myReader1 = par.cmd.ExecuteReader
    '        If myReader1.Read Then
    '            SommaConsuntivato = par.IfNull(myReader1(0), 0)
    '        End If
    '        myReader1.Close()



    '        'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
    '        '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO "

    '        par.cmd.CommandText = "select sum(PAGAMENTI_LIQUIDATI.IMPORTO) " _
    '                           & " from  SISCOM_MI.PAGAMENTI_LIQUIDATI " _
    '                           & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
    '                                                    & " from   SISCOM_MI.PRENOTAZIONI " _
    '                                                    & " where  ID_VOCE_PF" & sSelect1 _
    '                                                    & "   and  ID_STATO>1 "

    '        If sFiliale <> "" Then
    '            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & "  )"
    '        Else
    '            par.cmd.CommandText = par.cmd.CommandText & ")"
    '        End If


    '        myReader1 = par.cmd.ExecuteReader
    '        If myReader1.Read Then
    '            'Select Case par.IfNull(myReader1("ID_STATO"), 0)
    '            '    'Case 1  'EMESSO
    '            '    '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
    '            '    Case 5  'LIQUIDATO
    '            SommaLiquidato = SommaLiquidato + par.IfNull(myReader1(0), 0)
    '            'End Select
    '        End If
    '        myReader1.Close()


    '        SommaConsuntivato = SommaConsuntivato - SommaLiquidato

    '        Me.lbl_Tot_CONS.Text = Format(SommaConsuntivato, "##,##0.00")
    '        'lbl_Tot_PAG.Text = Format(SommaLiquidato, "##,##0.00")


    '        Me.lbl_Prenotato.Visible = False
    '        Me.lbl_Tot_PRE.Visible = False

    '        Me.lbl_Pagato.Visible = False
    '        Me.lbl_Tot_PAG.Visible = False

    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If


    '    Catch ex As Exception
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Response.Write(ex.Message)
    '    End Try


    'End Sub


    ''CONSUNTIVATI  e PAGATI GRID3
    'Private Sub BindGrid3()

    '    Dim StringaSql_PAG_1_5 As String    'AMMINISTRAZIONE CONDOMINI
    '    Dim StringaSql_PAG_2_5 As String    'MOROSITA CONDOMINI 
    '    Dim StringaSql_PAG_3_5 As String    'ODL MANUTENZIONI
    '    Dim StringaSql_PAG_4_5 As String    'ALTRI PAGAMENTI
    '    Dim StringaSql_PAG_5_5 As String    'RITENUTA ACCONTO
    '    Dim StringaSql_PAG_6_5 As String    'A CANONE
    '    Dim StringaSql_PAG_7_5 As String    'RRS MANUTENZIONI

    '    Dim StringaSqlLiquidato As String

    '    Dim SommaConsuntivato As Decimal = 0
    '    Dim SommaLiquidato As Decimal = 0

    '    Dim FlagConnessione As Boolean

    '    Dim sFiliale As String = ""
    '    Dim sSelect1 As String = ""


    '    If Request.QueryString("ID_STRUTTURA") <> "" Then
    '        sFiliale = " ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA")
    '    End If

    '    'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
    '    '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
    '    'End If


    '    Try
    '        '*******************APERURA CONNESSIONE*********************
    '        FlagConnessione = False
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            FlagConnessione = True
    '        End If

    '        If par.IfEmpty(Request.QueryString("IDSEL"), 0) = 1 Then
    '            sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
    '                             & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & iD_VocePF & "))"
    '        Else
    '            sSelect1 = "=" & iD_VocePF
    '        End If
    '        sSelect1 = "=" & iD_VocePF


    '        '*************************************************************
    '        'IMPORTO CONSUNTIVATO e PAGATO


    '        StringaSqlLiquidato = " nvl((select sum(PAGAMENTI_LIQUIDATI.IMPORTO) " _
    '                            & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
    '                            & " where  ID_PAGAMENTO=PAGAMENTI.ID),0) "


    '        ''AMMINISTRAZIONE CONDOMINI
    '        StringaSql_PAG_1_5 = " select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                        & " 'AMMINISTRAZIONE CONDOMINI' as TIPO_PAGAMENTO, " _
    '                        & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                        & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                        & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                        & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                        & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                        & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                        & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                & " where PAGAMENTI.TIPO_PAGAMENTO=1 " _
    '                & "   and PAGAMENTI.ID_STATO>0 " _
    '                & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+) " _
    '                & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                           & " where  ID_VOCE_PF" & sSelect1 _
    '                                                           & "   and  ID_STATO>1 " _
    '                                                           & "   and  TIPO_PAGAMENTO=1 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_1_5 = StringaSql_PAG_1_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_1_5 = StringaSql_PAG_1_5 & ")"
    '        End If


    '        'MOROSITA CONDOMINI 
    '        StringaSql_PAG_2_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                       & " 'MOROSITA CONDOMINI' as TIPO_PAGAMENTO, " _
    '                       & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                       & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                       & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                       & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                       & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                       & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                       & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '               & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '               & " where PAGAMENTI.TIPO_PAGAMENTO=2 " _
    '               & "   and PAGAMENTI.ID_STATO>0 " _
    '               & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '               & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+) " _
    '               & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                          & " where  ID_VOCE_PF" & sSelect1 _
    '                                                          & "   and  ID_STATO>1 " _
    '                                                          & "   and  TIPO_PAGAMENTO=2 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_2_5 = StringaSql_PAG_2_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_2_5 = StringaSql_PAG_2_5 & ")"
    '        End If


    '        'ODL MANUTENZIONI
    '        StringaSql_PAG_3_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'ODL MANUTENZIONI' as TIPO_PAGAMENTO, " _
    '                            & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=3 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID            =MANUTENZIONI.ID_PAGAMENTO " _
    '                    & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+) " _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=3 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_3_5 = StringaSql_PAG_3_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_3_5 = StringaSql_PAG_3_5 & ")"
    '        End If


    '        'ALTRI PAGAMENTI
    '        StringaSql_PAG_4_5 = " union select  PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'ORDINI e PAGAMENTI' as TIPO_PAGAMENTO, " _
    '                            & " ODL.DESCRIZIONE as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,'' as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.ODL" _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=4 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID            =ODL.ID_PAGAMENTO " _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=4 "

    '        If sFiliale <> "" Then
    '            StringaSql_PAG_4_5 = StringaSql_PAG_4_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_4_5 = StringaSql_PAG_4_5 & ")"
    '        End If


    '        'RITENUTA ACCONTO
    '        StringaSql_PAG_5_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'RITENUTA ACCONTO' as TIPO_PAGAMENTO, " _
    '                            & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,'' as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI" _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=5 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=5 "

    '        If sFiliale <> "" Then
    '            StringaSql_PAG_5_5 = StringaSql_PAG_5_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_5_5 = StringaSql_PAG_5_5 & ")"
    '        End If


    '        'A CANONE
    '        StringaSql_PAG_6_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'PAGAMENTI a CANONE' as TIPO_PAGAMENTO, " _
    '                            & " PAGAMENTI.DESCRIZIONE as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE,APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=6 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+) " _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=6 "
    '        If sFiliale <> "" Then
    '            StringaSql_PAG_6_5 = StringaSql_PAG_6_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_6_5 = StringaSql_PAG_6_5 & ")"
    '        End If


    '        'RRS MANUTENZIONI
    '        StringaSql_PAG_7_5 = " union select PAGAMENTI.ID as ID_PAGAMENTO,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as PROG_ANNO," _
    '                            & " 'RRS MANUTENZIONI' as TIPO_PAGAMENTO, " _
    '                            & " ('(ODL='||SISCOM_MI.MANUTENZIONI.PROGR||'/ANNO='||SISCOM_MI.MANUTENZIONI.ANNO||') '||MANUTENZIONI.DESCRIZIONE  ) as DESCRIZIONE1," _
    '                            & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
    '                            & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
    '                            & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
    '                            & " TRIM(TO_CHAR( nvl(PAGAMENTI.IMPORTO_CONSUNTIVATO,0),'9G999G999G999G999G990D99')) as IMPORTO_CONSUNTIVATO, " _
    '                            & " TRIM(TO_CHAR(" & StringaSqlLiquidato & ",'9G999G999G999G999G990D99')) as IMPORTO_PAGATO, " _
    '                            & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE, APPALTI.NUM_REPERTORIO as NUM_REPERTORIO " _
    '                    & " from SISCOM_MI.PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI " _
    '                    & " where PAGAMENTI.TIPO_PAGAMENTO=7 " _
    '                    & "   and PAGAMENTI.ID_STATO>0 " _
    '                    & "   and PAGAMENTI.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
    '                    & "   and PAGAMENTI.ID            =MANUTENZIONI.ID_PAGAMENTO " _
    '                    & "   and PAGAMENTI.ID_APPALTO    =APPALTI.ID (+) " _
    '                    & "   and PAGAMENTI.ID in (select ID_PAGAMENTO from   SISCOM_MI.PRENOTAZIONI " _
    '                                                               & " where  ID_VOCE_PF" & sSelect1 _
    '                                                               & "   and  ID_STATO>1 " _
    '                                                               & "   and  TIPO_PAGAMENTO=7 "

    '        If sFiliale <> "" Then
    '            StringaSql_PAG_7_5 = StringaSql_PAG_7_5 & " and " & sFiliale & " )"
    '        Else
    '            StringaSql_PAG_7_5 = StringaSql_PAG_7_5 & ")"
    '        End If


    '        par.cmd.CommandText = StringaSql_PAG_1_5 _
    '                            & StringaSql_PAG_2_5 _
    '                            & StringaSql_PAG_3_5 _
    '                            & StringaSql_PAG_4_5 _
    '                            & StringaSql_PAG_5_5 _
    '                            & StringaSql_PAG_6_5 _
    '                            & StringaSql_PAG_7_5 & "order by TIPO_PAGAMENTO asc ,ID_PAGAMENTO desc"

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim ds As New Data.DataTable()

    '        da.Fill(ds)


    '        DataGrid3.DataSource = ds
    '        DataGrid3.DataBind()

    '        da.Dispose()
    '        ds.Dispose()


    '        'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
    '        par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
    '                            & " from   SISCOM_MI.PRENOTAZIONI " _
    '                            & " where  ID_VOCE_PF" & sSelect1 _
    '                            & "   and  ID_STATO>1 "

    '        If sFiliale <> "" Then
    '            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
    '        End If
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
    '        myReader1 = par.cmd.ExecuteReader
    '        If myReader1.Read Then
    '            SommaConsuntivato = par.IfNull(myReader1(0), 0)
    '        End If
    '        myReader1.Close()



    '        'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
    '        '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO "

    '        par.cmd.CommandText = "select sum(PAGAMENTI_LIQUIDATI.IMPORTO) " _
    '                           & " from  SISCOM_MI.PAGAMENTI_LIQUIDATI " _
    '                           & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
    '                                                    & " from   SISCOM_MI.PRENOTAZIONI " _
    '                                                    & " where  ID_VOCE_PF" & sSelect1 _
    '                                                    & "   and  ID_STATO>1 "

    '        If sFiliale <> "" Then
    '            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & "  )"
    '        Else
    '            par.cmd.CommandText = par.cmd.CommandText & ")"
    '        End If


    '        myReader1 = par.cmd.ExecuteReader
    '        If myReader1.Read Then
    '            'Select Case par.IfNull(myReader1("ID_STATO"), 0)
    '            '    'Case 1  'EMESSO
    '            '    '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
    '            '    Case 5  'LIQUIDATO
    '            SommaLiquidato = SommaLiquidato + par.IfNull(myReader1(0), 0)
    '            'End Select
    '        End If
    '        myReader1.Close()


    '        SommaConsuntivato = SommaConsuntivato - SommaLiquidato

    '        Me.lbl_Tot_CONS.Text = Format(SommaConsuntivato, "##,##0.00")
    '        Me.lbl_Tot_PAG.Text = Format(SommaLiquidato, "##,##0.00")


    '        Me.lbl_Prenotato.Visible = False
    '        Me.lbl_Tot_PRE.Visible = False



    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If


    '    Catch ex As Exception
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Response.Write(ex.Message)
    '    End Try


    'End Sub



    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
        Response.Write("<script>window.close();</script>")
    End Sub
End Class
