Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_GestioneAssestamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public VISI As String = "hidden"
    Public visialt As String = "0px"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        
        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div id=""splash"" style=""border: thin dashed #000066; position: absolute; z-index: 500;" _
            & "text-align: center; font-size: 10px; width: 777px; height: 525px; visibility: visible;" _
            & "vertical-align: top; line-height: normal; top: 15px; left: 12px; background-color: #FFFFFF;"">" _
            & "<table style=""height: 100%; width: 100%"">" _
            & "<tr><td style=""width: 100%; height: 100%; vertical-align: middle; text-align: center"">" _
            & "<img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' />" _
            & "<br /><br /><span id=""lblCaricamento"" style=""font-family:Arial;font-size:10pt;"">caricamento in corso...</span>" _
            & "</td></tr></table></div>"

        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            URLdiProvenienza.Value = Request.ServerVariables("HTTP_REFERER")
            'Response.Flush()
            TipoAllegato.Value = par.getIdOggettoAllegatiWs("Assestamento")
            controllaStatoAssestamento()
        End If
    End Sub
    Protected Sub controllaStatoAssestamento()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID='" & Request.QueryString("id") & "' AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
            End If
            lettoreEsercizio.Close()
            '#######


            If Not IsNothing(Request.QueryString("id")) Then
                'controllo lo stato dell'assestamento 
                par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID='" & Request.QueryString("id") & "'"
                Dim LettoreStato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idAss As Integer = 0
                If LettoreStato.Read Then
                    idAss = par.IfNull(LettoreStato(0), "")
                    IDSTATO.Value = idAss
                    Select Case idAss
                        Case 0
                            'errore id assestamento non trovato
                            Response.Write("<script>alert('Nessun assestamento selezionato!');location.href='ScegliAssestamento.aspx';</script>")

                        Case 1
                            caricamentoImporti()
                            'convalida.Visible = False
                            'approva.Visible = False
                            'stampaass.Value = "0"
                        Case 2
                            Response.Redirect("ConvAssestAler.aspx?id=" & Request.QueryString("id"))
                            'statoBozza()
                            'DgvApprAssCapitoli.Visible = False
                            'DgvApprAssest.Visible = False
                            'convalida.Visible = True
                            'approva.Visible = False
                            'stampaass.Value = "0"
                        Case 3
                            'assestamento approvato dal Gestore
                            Response.Redirect("ConvAssestComune.aspx?id=" & Request.QueryString("id"))
                            'assestamentoApprovatoAler()
                            'convalida.Visible = False
                            'modifica.Visible = False
                            'approva.Visible = True
                            'naVisibile.Value = "1"
                            'stampaass.Value = "1"
                            'URLdiProvenienza.Value = "ScegliAssestamento.aspx"
                        Case 4

                        Case 5
                            assestamentoApprovatoComune()
                            modifica.Visible = False
                            convalida.Visible = False
                            approva.Visible = False
                            stampaass.Value = "1"
                            URLdiProvenienza.Value = "ScegliAssestamento.aspx"
                        Case Else
                            'errore id assestamento
                            Response.Write("<script>alert('Si è verificato un errore nel caricamento dell\'assestamento!');location.href='ScegliAssestamento.aspx';</script>")
                    End Select
                Else
                    Response.Write("<script>alert('Si è verificato un errore nel caricamento dell\'assestamento!');location.href='ScegliAssestamento.aspx';</script>")
                    Exit Sub
                End If
                LettoreStato.Close()
            Else
                Response.Redirect("ScegliAssestamento.aspx")
                Exit Sub
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Protected Sub statoBozza()

        AssestamentoCompletato.Text = "ASSESTAMENTO DA CONVALIDARE"
        Dim indicecolonna As Integer = TrovaIndiceColonna(datagrid2, "ASSESTAMENTO")
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtriepilogo As New Data.DataTable

            dtriepilogo.Clear()
            dtriepilogo.Rows.Clear()
            dtriepilogo.Columns.Clear()
            dtriepilogo.Columns.Add("CODICE")
            dtriepilogo.Columns.Add("VOCE")
            dtriepilogo.Columns.Add("BUDGET")
            dtriepilogo.Columns.Add("ASSESTAMENTI_PRECEDENTI")
            dtriepilogo.Columns.Add("VARIAZIONI")
            dtriepilogo.Columns.Add("SPESO")
            dtriepilogo.Columns.Add("RESIDUO")
            dtriepilogo.Columns.Add("ASSESTAMENTO")

            Dim RIGA As Data.DataRow

            '#### SELEZIONO L'ASSESTAMENTO ####
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            If IdAssestamento.Value = 0 Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id_stato = 2"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    IdAssestamento.Value = par.IfNull(lettore("ID"), 0)
                End If
                lettore.Close()
            End If
            '##################################

            '#### CREO ELENCO VOCI ######
            par.cmd.CommandText = "SELECT DISTINCT ID_ASSESTAMENTO,ID_VOCE,DESCRIZIONE,CODICE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI," _
                & "SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PF_ASSESTAMENTO_VOCI.ID_VOCE AND ID_ASSESTAMENTO=" & IdAssestamento.Value & " ORDER BY CODICE ASC"
            Dim ElencoVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While ElencoVoci.Read
                'IMPOSTO BUDGET E ASSESTAMENTO
                par.cmd.CommandText = "SELECT " _
                    & "TRIM(TO_CHAR(SUM(NVL(VALORE_LORDO,0)),'999999990D9999')) AS VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)),'999999990D9999')) AS ASSESTAMENTO_VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(VARIAZIONI, 0)),'999999990D9999')) AS VARIAZIONI " _
                    & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') "

                Dim LettoreImporti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim BUDGET As Decimal = 0
                Dim ASSESTAMENTI_PRECEDENTI As Decimal = 0
                Dim VARIAZIONI As Decimal = 0

                If LettoreImporti.Read Then
                    '############ MODIFICA BUDGET...IL BUDGET SARà SEMPRE L'IMPORTO DEL VALORE_LORDO...AGGIUNGO COLONNA VARIAZIONI E COLONNA ASSESTAMENTI PRECEDENTI
                    'BUDGET = CDec(par.IfNull(LettoreImporti("VALORE_LORDO"), 0)) + CDec(par.IfNull(LettoreImporti("ASSESTAMENTO_VALORE_LORDO"), 0)) + CDec(par.IfNull(LettoreImporti("VARIAZIONI"), 0))
                    BUDGET = CDec(par.IfNull(LettoreImporti("VALORE_LORDO"), 0))
                    ASSESTAMENTI_PRECEDENTI = CDec(par.IfNull(LettoreImporti("ASSESTAMENTO_VALORE_LORDO"), 0))
                    VARIAZIONI = CDec(par.IfNull(LettoreImporti("VARIAZIONI"), 0))
                    '#############################################################
                End If
                LettoreImporti.Close()


                '###### DATA LIMITE ######
                Dim DataLimite As String = ""
                par.cmd.CommandText = "SELECT DATA_APP_COMUNE AS DATA FROM SISCOM_MI.PF_ASSESTAMENTO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value
                Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreEsercizio.Read Then
                    DataLimite = par.IfNull(lettoreEsercizio("DATA"), "")
                End If
                lettoreEsercizio.Close()

                '############ MODIFICA VARIAZIONI POSTERIORI #############
                Dim VARIAZIONI_NEGATIVE As Decimal = 0
                Dim VARIAZIONI_POSITIVE As Decimal = 0

                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_VARIAZIONI " _
                    & "WHERE ID_VOCE_DA IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND DATA_ORA>'" & DataLimite & "'"

                Dim LettoreVariazioniNegative As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreVariazioniNegative.Read Then
                    VARIAZIONI_NEGATIVE = par.IfNull(LettoreVariazioniNegative(0), 0)
                End If
                LettoreVariazioniNegative.Close()
                VARIAZIONI = VARIAZIONI + CDec(VARIAZIONI_NEGATIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_VARIAZIONI " _
                    & "WHERE ID_VOCE_A IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND DATA_ORA>'" & DataLimite & "'"

                Dim LettoreVariazioniPositive As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreVariazioniPositive.Read Then
                    VARIAZIONI_POSITIVE = par.IfNull(LettoreVariazioniPositive(0), 0)
                End If
                LettoreVariazioniPositive.Close()
                VARIAZIONI = VARIAZIONI - CDec(VARIAZIONI_POSITIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'NEL RIEPILOGO GENERALE NON è NECESSARIO TENERE IN CONTO LE VARIAZIONI TRA STRUTTURE POICHè NEI TRASFERIMENTI INTERESSATI SARANNO UGUALI E OPPPOSTE


                '#########################################################


                '############ MODIFICA ASSESTAMENTI POSTERIORI ############
                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO_APPROVATO,0)) AS IMPORTO_APPROVATO " _
                    & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO " _
                    & "WHERE ID_VOCE IN (SELECT ID " _
                    & "FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_ASSESTAMENTO >=" & IdAssestamento.Value _
                    & "AND PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO=PF_ASSESTAMENTO.ID " _
                    & "AND ID_STATO=5 "

                Dim ASSESTAMENTI_POST As Decimal = 0
                Dim lettoreAssestamentiPost As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreAssestamentiPost.Read Then
                    ASSESTAMENTI_POST = par.IfNull(lettoreAssestamentiPost("IMPORTO_APPROVATO"), 0)
                End If
                lettoreAssestamentiPost.Close()
                ASSESTAMENTI_PRECEDENTI = ASSESTAMENTI_PRECEDENTI - ASSESTAMENTI_POST
                '##########################################################




                'IMPOSTO SPESE E CALCOLO RESIDUO
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0)),'999G999G990D99') " _
                    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    & "WHERE (ID_STATO=0 or ID_STATO=1) " _
                    & "AND DATA_PRENOTAZIONE<='" & DataLimite & "' " _
                    & "AND ID_PAGAMENTO IS NULL " _
                    & "AND ID_VOCE_PF IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') "

                Dim lettoreImportoSpeso As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim SPESO As Decimal = 0
                If lettoreImportoSpeso.Read Then
                    SPESO = par.IfNull(lettoreImportoSpeso(0), 0)
                End If
                lettoreImportoSpeso.Close()

                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)),'999G999G990D99') " _
                    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    & "WHERE ID_PAGAMENTO IS NOT NULL " _
                    & "AND DATA_PRENOTAZIONE<='" & DataLimite & "' " _
                    & "AND ID_STATO>1 " _
                    & "AND ID_VOCE_PF IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') "

                Dim lettoreImportoApprovato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoApprovato.Read Then
                    SPESO = SPESO + par.IfNull(lettoreImportoApprovato(0), 0)
                End If
                lettoreImportoApprovato.Close()


                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO,SUM(NVL(IMPORTO_APPROVATO,0)) AS IMPORTO_APPROVATO FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_ASSESTAMENTO = " & IdAssestamento.Value


                Dim RICHIESTO As Decimal = 0
                Dim lettoreImportoRichiesto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoRichiesto.Read Then
                    RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                    datagrid2.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO RICHIESTO"
                End If
                lettoreImportoRichiesto.Close()

                Dim RESIDUO As Decimal = BUDGET + VARIAZIONI + ASSESTAMENTI_PRECEDENTI - SPESO
                RIGA = dtriepilogo.NewRow()
                RIGA.Item("CODICE") = par.IfNull(ElencoVoci("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(ElencoVoci("DESCRIZIONE"), "")
                RIGA.Item("BUDGET") = Format(BUDGET, "##,##0.00")
                RIGA.Item("ASSESTAMENTI_PRECEDENTI") = Format(ASSESTAMENTI_PRECEDENTI, "##,##0.00")
                RIGA.Item("VARIAZIONI") = Format(VARIAZIONI, "##,##0.00")
                RIGA.Item("SPESO") = Format(SPESO, "##,##0.00")
                RIGA.Item("RESIDUO") = Format(RESIDUO, "##,##0.00")
                RIGA.Item("ASSESTAMENTO") = Format(RICHIESTO, "##,##0.00")
                dtriepilogo.Rows.Add(RIGA)

            End While
            ElencoVoci.Close()
            datagrid2.DataSource = dtriepilogo
            datagrid2.DataBind()
            datagrid2.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub assestamentoApprovatoComune()
        VISI = "visible"
        visialt = "440px;"
        AssestamentoCompletato.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim ElVoci As String = ""
            Dim Approvato As Decimal = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader
            Dim indiceColonna As Integer = TrovaIndiceColonna(DgvApprAssest, "APPROVATO")
            Dim indiceColonnaCap As Integer = TrovaIndiceColonna(DgvApprAssCapitoli, "ASSESTAMENTO")

            If IdAssestamento.Value = 0 Then
                If par.IfEmpty(Request.QueryString("ID"), "") <> "" Then
                    IdAssestamento.Value = Request.QueryString("ID")
                Else
                    'par.cmd.CommandText = "select max(id) from siscom_mi.pf_assestamento where (id_stato = 3 or id_stato = 5)"
                    par.cmd.CommandText = "SELECT MAX(ID),ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE (ID_STATO=3 OR ID_STATO=5) GROUP BY ID_STATO ORDER BY MAX(ID) DESC"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        IdAssestamento.Value = par.IfNull(lettore(0), 0)
                        If par.IfNull(lettore(1), 3) = 5 Then
                            'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                        ElseIf par.IfNull(lettore(1), 3) = 3 Then
                            'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                        End If
                    End If
                End If
            End If
            If IdAssestamento.Value = 0 Then
                Response.Write("<script>alert('Nessun Assestamento approvato dal Gestore trovato!Impossibile procedere');document.location.href='../../pagina_home.aspx';</script>")
                Exit Sub
            End If

            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, CASE WHEN SISCOM_MI.PF_ASSESTAMENTO.ID_STATO=5 THEN TO_CHAR(TO_DATE(DATA_APP_COMUNE,'yyyyMMdd'),'dd/MM/yyyy') ELSE TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA_ASS, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                'esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                If par.IfNull(lettoreEsercizio("id_stato"), 1) = 5 Then
                    'lblAssestamentoData.Text = "Assestamento approvato il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                Else
                    'lblAssestamentoData.Text = "Assestamento del " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                End If
            End If
            lettoreEsercizio.Close()
            '#######


            par.cmd.CommandText = "SELECT PF_VOCI.*,PF_CAPITOLI.COD AS COD_CAPITOLO, PF_CAPITOLI.DESCRIZIONE AS CAPITOLO ,'' AS APPROVATO " _
                                & "FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID = ID_CAPITOLO AND  ( PF_VOCI.ID IN " _
                                & "(SELECT PF_ASSESTAMENTO_VOCI.ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                & "WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & " )OR PF_VOCI.ID_VOCE_MADRE IN " _
                                & "(SELECT PF_ASSESTAMENTO_VOCI.ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                & "WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & " )) ORDER BY CODICE"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            For Each r As Data.DataRow In dt.Rows
                Approvato = 0
                ElVoci = ""

                '*******SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE
                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI " _
                                  & "  where ID=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & r.Item("ID") & ") order by CODICE"

                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    If ElVoci = "" Then
                        ElVoci = par.IfNull(lettore(0), "")
                    Else
                        ElVoci = ElVoci & "," & par.IfNull(lettore(0), "")
                    End If

                End While
                lettore.Close()
                '**********END ---- SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE

                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO

                par.cmd.CommandText = "select id_voce,importo_approvato from siscom_mi.pf_assestamento_voci where id_voce in ( " & ElVoci & ") AND ID_ASSESTAMENTO=" & IdAssestamento.Value

                lettore = par.cmd.ExecuteReader

                While lettore.Read
                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & lettore("ID_VOCE")

                    lettore2 = par.cmd.ExecuteReader()
                    If lettore2.Read Then

                    Else

                        Approvato = Approvato + Decimal.Parse(par.IfNull(lettore("importo_approvato"), "0"))

                    End If
                    lettore2.Close()

                End While
                lettore.Close()

                r.Item("APPROVATO") = Format(Approvato, "##,##0.00")
                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO


            Next

            Me.DgvApprAssest.DataSource = dt
            Me.DgvApprAssest.DataBind()

            par.cmd.CommandText = "SELECT cod, descrizione," _
                                & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(importo_approvato,0)) FROM siscom_mi.pf_assestamento_voci " _
                                & "WHERE id_assestamento = " & IdAssestamento.Value & " and id_voce IN (SELECT ID FROM siscom_mi.pf_voci " _
                                & "WHERE id_capitolo = pf_capitoli.ID)),0),'9G999G999G990D99')) AS assestamento " _
                                & "FROM siscom_mi.pf_capitoli"

            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            dt.Dispose()
            dt = New Data.DataTable

            da.Fill(dt)
            Me.DgvApprAssCapitoli.DataSource = dt
            Me.DgvApprAssCapitoli.DataBind()




            par.cmd.CommandText = "select id_stato from siscom_mi.pf_assestamento where id = " & IdAssestamento.Value
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore(0), 1) <> 3 Then
                    'frmSoloLettura()
                End If
            End If
            lettore.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub assestamentoApprovatoAler_old()
        AssestamentoCompletato.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
        Dim indicecolonna As Integer = TrovaIndiceColonna(datagrid2, "ASSESTAMENTO")
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtriepilogo As New Data.DataTable

            dtriepilogo.Clear()
            dtriepilogo.Rows.Clear()
            dtriepilogo.Columns.Clear()
            dtriepilogo.Columns.Add("CODICE")
            dtriepilogo.Columns.Add("VOCE")
            dtriepilogo.Columns.Add("BUDGET")
            dtriepilogo.Columns.Add("ASSESTAMENTI_PRECEDENTI")
            dtriepilogo.Columns.Add("VARIAZIONI")
            dtriepilogo.Columns.Add("SPESO")
            dtriepilogo.Columns.Add("RESIDUO")
            dtriepilogo.Columns.Add("ASSESTAMENTO")

            Dim RIGA As Data.DataRow

            '#### SELEZIONO L'ASSESTAMENTO ####
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            If IdAssestamento.Value = 0 Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id_stato = 3"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    IdAssestamento.Value = par.IfNull(lettore("ID"), 0)
                End If
                lettore.Close()
            End If
            '##################################

            '#### CREO ELENCO VOCI ######
            par.cmd.CommandText = "SELECT DISTINCT ID_ASSESTAMENTO,ID_VOCE,DESCRIZIONE,CODICE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI," _
                & "SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PF_ASSESTAMENTO_VOCI.ID_VOCE AND ID_ASSESTAMENTO=" & IdAssestamento.Value & " ORDER BY CODICE ASC"
            Dim ElencoVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While ElencoVoci.Read
                'IMPOSTO BUDGET E ASSESTAMENTO
                par.cmd.CommandText = "SELECT " _
                    & "TRIM(TO_CHAR(SUM(NVL(VALORE_LORDO,0)),'999999990D9999')) AS VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)),'999999990D9999')) AS ASSESTAMENTO_VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(VARIAZIONI, 0)),'999999990D9999')) AS VARIAZIONI " _
                    & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') "

                Dim LettoreImporti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim BUDGET As Decimal = 0
                Dim ASSESTAMENTI_PRECEDENTI As Decimal = 0
                Dim VARIAZIONI As Decimal = 0

                If LettoreImporti.Read Then
                    '############ MODIFICA BUDGET...IL BUDGET SARà SEMPRE L'IMPORTO DEL VALORE_LORDO...AGGIUNGO COLONNA VARIAZIONI E COLONNA ASSESTAMENTI PRECEDENTI
                    'BUDGET = CDec(par.IfNull(LettoreImporti("VALORE_LORDO"), 0)) + CDec(par.IfNull(LettoreImporti("ASSESTAMENTO_VALORE_LORDO"), 0)) + CDec(par.IfNull(LettoreImporti("VARIAZIONI"), 0))
                    BUDGET = CDec(par.IfNull(LettoreImporti("VALORE_LORDO"), 0))
                    ASSESTAMENTI_PRECEDENTI = CDec(par.IfNull(LettoreImporti("ASSESTAMENTO_VALORE_LORDO"), 0))
                    VARIAZIONI = CDec(par.IfNull(LettoreImporti("VARIAZIONI"), 0))
                    '#############################################################
                End If
                LettoreImporti.Close()


                '###### DATA LIMITE ######
                Dim DataLimite As String = ""
                par.cmd.CommandText = "SELECT DATA_APP_COMUNE AS DATA FROM SISCOM_MI.PF_ASSESTAMENTO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value
                Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreEsercizio.Read Then
                    DataLimite = par.IfNull(lettoreEsercizio("DATA"), "")
                End If
                lettoreEsercizio.Close()

                '############ MODIFICA VARIAZIONI POSTERIORI #############
                Dim VARIAZIONI_NEGATIVE As Decimal = 0
                Dim VARIAZIONI_POSITIVE As Decimal = 0

                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_VARIAZIONI " _
                    & "WHERE ID_VOCE_DA IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND DATA_ORA>'" & DataLimite & "'"

                Dim LettoreVariazioniNegative As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreVariazioniNegative.Read Then
                    VARIAZIONI_NEGATIVE = par.IfNull(LettoreVariazioniNegative(0), 0)
                End If
                LettoreVariazioniNegative.Close()
                VARIAZIONI = VARIAZIONI + CDec(VARIAZIONI_NEGATIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_VARIAZIONI " _
                    & "WHERE ID_VOCE_A IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND DATA_ORA>'" & DataLimite & "'"

                Dim LettoreVariazioniPositive As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreVariazioniPositive.Read Then
                    VARIAZIONI_POSITIVE = par.IfNull(LettoreVariazioniPositive(0), 0)
                End If
                LettoreVariazioniPositive.Close()
                VARIAZIONI = VARIAZIONI - CDec(VARIAZIONI_POSITIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'NEL RIEPILOGO GENERALE NON è NECESSARIO TENERE IN CONTO LE VARIAZIONI TRA STRUTTURE POICHè NEI TRASFERIMENTI INTERESSATI SARANNO UGUALI E OPPPOSTE


                '#########################################################


                '############ MODIFICA ASSESTAMENTI POSTERIORI ############
                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO_APPROVATO,0)) AS IMPORTO_APPROVATO " _
                    & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO " _
                    & "WHERE ID_VOCE IN (SELECT ID " _
                    & "FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_ASSESTAMENTO >=" & IdAssestamento.Value _
                    & "AND PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO=PF_ASSESTAMENTO.ID " _
                    & "AND ID_STATO=5 "

                Dim ASSESTAMENTI_POST As Decimal = 0
                Dim lettoreAssestamentiPost As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreAssestamentiPost.Read Then
                    ASSESTAMENTI_POST = par.IfNull(lettoreAssestamentiPost("IMPORTO_APPROVATO"), 0)
                End If
                lettoreAssestamentiPost.Close()
                ASSESTAMENTI_PRECEDENTI = ASSESTAMENTI_PRECEDENTI - ASSESTAMENTI_POST
                '##########################################################




                'IMPOSTO SPESE E CALCOLO RESIDUO
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0)),'999G999G990D99') " _
                    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    & "WHERE (ID_STATO=0 or ID_STATO=1) " _
                    & "AND DATA_PRENOTAZIONE<='" & DataLimite & "' " _
                    & "AND ID_PAGAMENTO IS NULL " _
                    & "AND ID_VOCE_PF IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') "

                Dim lettoreImportoSpeso As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim SPESO As Decimal = 0
                If lettoreImportoSpeso.Read Then
                    SPESO = par.IfNull(lettoreImportoSpeso(0), 0)
                End If
                lettoreImportoSpeso.Close()

                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)),'999G999G990D99') " _
                    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    & "WHERE ID_PAGAMENTO IS NOT NULL " _
                    & "AND DATA_PRENOTAZIONE<='" & DataLimite & "' " _
                    & "AND ID_STATO>1 " _
                    & "AND ID_VOCE_PF IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') "

                Dim lettoreImportoApprovato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoApprovato.Read Then
                    SPESO = SPESO + par.IfNull(lettoreImportoApprovato(0), 0)
                End If
                lettoreImportoApprovato.Close()


                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO,SUM(NVL(IMPORTO_APPROVATO,0)) AS IMPORTO_APPROVATO FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_ASSESTAMENTO = " & IdAssestamento.Value


                Dim RICHIESTO As Decimal = 0
                Dim lettoreImportoRichiesto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoRichiesto.Read Then
                    RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                    datagrid2.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                End If
                lettoreImportoRichiesto.Close()

                Dim RESIDUO As Decimal = BUDGET + VARIAZIONI + ASSESTAMENTI_PRECEDENTI - SPESO
                RIGA = dtriepilogo.NewRow()
                RIGA.Item("CODICE") = par.IfNull(ElencoVoci("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(ElencoVoci("DESCRIZIONE"), "")
                RIGA.Item("BUDGET") = Format(BUDGET, "##,##0.00")
                RIGA.Item("ASSESTAMENTI_PRECEDENTI") = Format(ASSESTAMENTI_PRECEDENTI, "##,##0.00")
                RIGA.Item("VARIAZIONI") = Format(VARIAZIONI, "##,##0.00")
                RIGA.Item("SPESO") = Format(SPESO, "##,##0.00")
                RIGA.Item("RESIDUO") = Format(RESIDUO, "##,##0.00")
                RIGA.Item("ASSESTAMENTO") = Format(RICHIESTO, "##,##0.00")
                dtriepilogo.Rows.Add(RIGA)

            End While
            ElencoVoci.Close()
            datagrid2.DataSource = dtriepilogo
            datagrid2.DataBind()
            datagrid2.Visible = True

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub assestamentoApprovatoAler()
        VISI = "visible"
        visialt = "440px;"
        AssestamentoCompletato.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim ElVoci As String = ""
            Dim Approvato As Decimal = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader
            Dim indiceColonna As Integer = TrovaIndiceColonna(DgvApprAssest, "APPROVATO")
            Dim indiceColonnaCap As Integer = TrovaIndiceColonna(DgvApprAssCapitoli, "ASSESTAMENTO")

            If IdAssestamento.Value = 0 Then
                If par.IfEmpty(Request.QueryString("ID"), "") <> "" Then
                    IdAssestamento.Value = Request.QueryString("ID")
                Else
                    'par.cmd.CommandText = "select max(id) from siscom_mi.pf_assestamento where (id_stato = 3 or id_stato = 5)"
                    par.cmd.CommandText = "SELECT MAX(ID),ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE (ID_STATO=3 OR ID_STATO=5) GROUP BY ID_STATO ORDER BY MAX(ID) DESC"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        IdAssestamento.Value = par.IfNull(lettore(0), 0)
                        If par.IfNull(lettore(1), 3) = 5 Then
                            'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                        ElseIf par.IfNull(lettore(1), 3) = 3 Then
                            'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                        End If
                    End If
                End If
            End If
            If IdAssestamento.Value = 0 Then
                Response.Write("<script>alert('Nessun Assestamento approvato dal Gestore trovato!Impossibile procedere');document.location.href='../../pagina_home.aspx';</script>")
                Exit Sub
            End If

            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, CASE WHEN SISCOM_MI.PF_ASSESTAMENTO.ID_STATO=5 THEN TO_CHAR(TO_DATE(DATA_APP_COMUNE,'yyyyMMdd'),'dd/MM/yyyy') ELSE TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA_ASS, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                'esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                If par.IfNull(lettoreEsercizio("id_stato"), 1) = 5 Then
                    'lblAssestamentoData.Text = "Assestamento approvato il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                Else
                    'lblAssestamentoData.Text = "Assestamento del " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                End If
            End If
            lettoreEsercizio.Close()
            '#######


            par.cmd.CommandText = "SELECT PF_VOCI.*,PF_CAPITOLI.COD AS COD_CAPITOLO, PF_CAPITOLI.DESCRIZIONE AS CAPITOLO ,'' AS APPROVATO " _
                                & "FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID = ID_CAPITOLO AND  ( PF_VOCI.ID IN " _
                                & "(SELECT PF_ASSESTAMENTO_VOCI.ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                & "WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & " )OR PF_VOCI.ID_VOCE_MADRE IN " _
                                & "(SELECT PF_ASSESTAMENTO_VOCI.ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                & "WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & " )) ORDER BY CODICE"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            For Each r As Data.DataRow In dt.Rows
                Approvato = 0
                ElVoci = ""

                '*******SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE
                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI " _
                                  & "  where ID=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & r.Item("ID") & ") order by CODICE"

                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    If ElVoci = "" Then
                        ElVoci = par.IfNull(lettore(0), "")
                    Else
                        ElVoci = ElVoci & "," & par.IfNull(lettore(0), "")
                    End If

                End While
                lettore.Close()
                '**********END ---- SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE

                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO

                par.cmd.CommandText = "select id_voce,importo_approvato from siscom_mi.pf_assestamento_voci where id_voce in ( " & ElVoci & ") AND ID_ASSESTAMENTO=" & IdAssestamento.Value

                lettore = par.cmd.ExecuteReader

                While lettore.Read
                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & lettore("ID_VOCE")

                    lettore2 = par.cmd.ExecuteReader()
                    If lettore2.Read Then

                    Else

                        Approvato = Approvato + Decimal.Parse(par.IfNull(lettore("importo_approvato"), "0"))

                    End If
                    lettore2.Close()

                End While
                lettore.Close()

                r.Item("APPROVATO") = Format(Approvato, "##,##0.00")
                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO


            Next

            Me.DgvApprAssest.DataSource = dt
            Me.DgvApprAssest.DataBind()

            par.cmd.CommandText = "SELECT cod, descrizione," _
                                & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(importo_approvato,0)) FROM siscom_mi.pf_assestamento_voci " _
                                & "WHERE id_assestamento = " & IdAssestamento.Value & " and id_voce IN (SELECT ID FROM siscom_mi.pf_voci " _
                                & "WHERE id_capitolo = pf_capitoli.ID)),0),'9G999G999G990D99')) AS assestamento " _
                                & "FROM siscom_mi.pf_capitoli"

            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            dt.Dispose()
            dt = New Data.DataTable

            da.Fill(dt)
            Me.DgvApprAssCapitoli.DataSource = dt
            Me.DgvApprAssCapitoli.DataBind()




            par.cmd.CommandText = "select id_stato from siscom_mi.pf_assestamento where id = " & IdAssestamento.Value
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore(0), 1) <> 3 Then
                    'frmSoloLettura()
                End If
            End If
            lettore.Close()
            approva.Visible = True

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub caricamentoImporti()
        Response.Redirect("AssestamentoPerStruttura.aspx")
    End Sub
    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function
    Protected Sub convalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles convalida.Click
        If ConfALerCompleto.Value = 1 Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "update siscom_mi.pf_assestamento_voci set importo_approvato = nvl(importo,0) where (importo_approvato is null or importo_approvato = importo)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "update siscom_mi.pf_assestamento set id_stato = 3 , data_app_aler = '" & Format(Now, "yyyyMMdd") & "' where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()
                WriteEvent("F86", "IL GESTORE HA CONVALIDATO L'ASSESTAMENTO")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento ha ora stato: APPROVATO DAL GESTORE!');location.href='GestioneAssestamento.aspx?id=" & IdAssestamento.Value & "'</script>")
            Catch ex As Exception
                lblErrore.Visible = True
                lblErrore.Text = "Convalida - " & ex.Message
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub
    Protected Sub modifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles modifica.Click
        Response.Redirect("AssestamentoPerStruttura.aspx?id=" & IdAssestamento.Value)
    End Sub
    Protected Sub WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                & "(" & IdAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', '" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',NULL,NULL,'0' )"
            par.cmd.ExecuteNonQuery()


            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try
    End Sub
    Protected Sub approva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles approva.Click
        If ConfCompleto.Value = 1 Then
            Dim indiceColonna As Integer = TrovaIndiceColonna(DgvApprAssest, "APPROVATO")
            Dim indiceColonnaCap As Integer = TrovaIndiceColonna(DgvApprAssCapitoli, "ASSESTAMENTO")
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"

                '******MODIFICA STATO ASSESTAMENTO******
                par.cmd.CommandText = "update siscom_mi.pf_assestamento set id_stato = 5, data_app_comune = " & Format(Now, "yyyyMMdd") & " where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()
                '***************************************


                '******UPDATE ASSESTAMENTO_VALORE_LORDO IN PF_VOCI_STRUTTURA PER LE VOCI COINVOLTE******* 'MODIFICATO IL 23092011 IMPORTO_APPROVATO>0 CON IMPORTO_APPROVATO<>0 
                par.cmd.CommandText = "UPDATE siscom_mi.pf_voci_struttura SET assestamento_valore_lordo = nvl(assestamento_valore_lordo,0) + " _
                                    & "nvl((SELECT importo_approvato FROM siscom_mi.pf_assestamento_voci " _
                                    & "WHERE id_assestamento = " & IdAssestamento.Value & " AND pf_assestamento_voci.id_voce = pf_voci_struttura.id_voce " _
                                    & "AND pf_assestamento_voci.id_struttura= pf_voci_struttura.id_struttura " _
                                    & "AND importo_approvato<>0),0)"
                par.cmd.ExecuteNonQuery()
                '****************************************************************************************


                '******RECUPERO VOCI RIEPILOGATIVE******* 'MODIFICATO IL 23092011 IMPORTO_APPROVATO>0 CON IMPORTO_APPROVATO<>0
                par.cmd.CommandText = "SELECT PF_VOCI.ID_VOCE_MADRE AS ID_V,ID_STRUTTURA " _
                    & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI, SISCOM_MI.PF_VOCI " _
                    & "WHERE IMPORTO_APPROVATO <> 0 AND ID_ASSESTAMENTO='" & IdAssestamento.Value & "' " _
                    & "AND PF_VOCI.ID=PF_ASSESTAMENTO_VOCI.ID_VOCE " _
                    & "AND PF_VOCI.ID_VOCE_MADRE IN (SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE LEVEL = 2 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NULL)) " _
                    & "GROUP BY PF_VOCI.ID_VOCE_MADRE,ID_STRUTTURA"

                Dim lettoreVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettoreVoci.Read
                    '*******AGGIORNAMENTO DELL'ASSESTAMENTO IN PF_VOCI_STRUTTURA*******
                    Dim IdVoceA As String = par.IfNull(lettoreVoci("ID_V"), "")
                    Dim IdStrutt As String = par.IfNull(lettoreVoci("ID_STRUTTURA"), "")
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET ASSESTAMENTO_VALORE_LORDO=NVL(ASSESTAMENTO_VALORE_LORDO,0)+ " _
                            & "(SELECT SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI MADRE " _
                            & "WHERE PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID " _
                            & "AND MADRE.ID=PF_VOCI.ID_VOCE_MADRE " _
                            & "AND MADRE.ID ='" & IdVoceA & "' AND ID_STRUTTURA='" & IdStrutt & "') " _
                            & "WHERE PF_VOCI_STRUTTURA.ID_VOCE='" & IdVoceA & "' " _
                            & "AND PF_VOCI_STRUTTURA.ID_STRUTTURA='" & IdStrutt & "'"
                    par.cmd.ExecuteNonQuery()
                End While
                lettoreVoci.Close()
                '***************************************************

                WriteEvent("F86", "ASSESTAMENTO APPROVATO DAL COMUNE")
                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento è stato approvato!');location.href='GestioneAssestamento.aspx?id=" & IdAssestamento.Value & "';</script>")

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
                lblErrore.Text = "Procedi - " & ex.Message
            End Try
        End If
    End Sub
    Protected Sub datagrid2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles datagrid2.SelectedIndexChanged
        Try
            If salvaok.Value = 1 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "update siscom_mi.pf_assestamento set note_comune = '" & par.PulisciStrSql(Me.TxtNote.Text.ToUpper) & "', id_stato = 1 where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = "update siscom_mi.pf_assestamento_voci set completo = 0 where id_assestamento = " & IdAssestamento.Value
                'par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento non è stato approvato!');</script>")
                WriteEvent("F87", "ASSESTAMENTO NON APPROVATO DAL COMUNE")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'CaricaVoci()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnSalvaNoteRifiuto - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub btnSalvaNoteRifiuto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaNoteRifiuto.Click
        Try
            If salvaok.Value = 1 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "update siscom_mi.pf_assestamento set note_comune = '" & par.PulisciStrSql(Me.TxtNote.Text.ToUpper) & "', id_stato = 1 where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = "update siscom_mi.pf_assestamento_voci set completo = 0 where id_assestamento = " & IdAssestamento.Value
                'par.cmd.ExecuteNonQuery()
                par.cmd.Dispose()
                par.OracleConn.Close()
                WriteEvent("F87", "ASSESTAMENTO NON APPROVATO DAL COMUNE")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento non è stato approvato!');location.href='ScegliAssestamento.aspx'</script>")
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnSalvaNoteRifiuto - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        If URLdiProvenienza.Value <> "" Then
            Response.Redirect(URLdiProvenienza.Value)
        End If
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        controllaStatoAssestamento()
        If IDSTATO.Value = 3 Then
            GeneraPDF()
        ElseIf IDSTATO.Value = 5 Then
            GeneraPDFComune()
        End If
    End Sub
    Private Sub GeneraPDF()
        Try
            '***************************************************************************************************
            '******************************************CREO IL FILE PDF*****************************************
            '***************************************************************************************************

            Dim NomeFile As String = "ASS" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            datagrid2.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter



            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PageWidth = 800
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = Me.labelTitolo.Text & esercizio.Text
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            Response.Write("<script>self.close();window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")



        Catch ex As Exception

        End Try

    End Sub
    Private Sub GeneraPDFComune()
        Try
            '***************************************************************************************************
            '******************************************CREO IL FILE PDF*****************************************
            '***************************************************************************************************

            Dim NomeFile As String = "ASS" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim Html2 As String = ""
            Dim Html_finale As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim stringWriter2 As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            DgvApprAssCapitoli.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString
            Dim sourcecode2 As New HtmlTextWriter(stringWriter2)
            DgvApprAssest.RenderControl(sourcecode2)
            Html2 = stringWriter2.ToString
            Html_finale = Html & "<p style='page-break-before: always'>&nbsp;</p>" & Html2

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PageWidth = 800
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = Me.labelTitolo.Text & esercizio.Text
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            'pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            pdfConverter1.SavePdfFromHtmlStringToFile(Html_finale, url & NomeFile)
            Response.Write("<script>self.close();window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")
        Catch ex As Exception

        End Try

    End Sub
End Class
