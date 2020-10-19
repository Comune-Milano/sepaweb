Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Drawing

Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_CompletaAssestamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public DG1 As String = "400px"
    Dim dtriepilogoStampa As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Session.Item("MOD_ASS_COMPILA") <> 1 Then
                Response.Write("<script>alert('Operatore non abilitato alla compilazione dell\'assestamento!');location.href='../../pagina_home.aspx';</script>")
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
                Response.Flush()
                If Not IsNothing(Request.QueryString("IDassestamento")) Then

                    'CONTROLLO E IMPOSTO L'ASSESTAMENTO E IL SUO STATO SELEZIONATO
                    IdAssestamento.Value = Request.QueryString("IDassestamento")
                    IDSTATO.Value = Request.QueryString("IDstato")
                    CaricaElencoStrutture()
                    If IDSTATO.Value <> "1" And IDSTATO.Value <> "2" Then
                        compl.Value = IDSTATO.Value
                    End If
                    ControlloCompleto()
                Else
                    CaricaAssestamento()
                    CaricaElencoStrutture()
                    ControlloCompleto()
                End If



                'modifica 15 novembre

                controlloStruttureCOMPLETO()

                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°




                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                '### date ####
                par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
                Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreEsercizio.Read Then
                    esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                End If
                lettoreEsercizio.Close()
                '#######

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub controlloStruttureCOMPLETO()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        '### date ####
        par.cmd.CommandText = "SELECT COUNT(*) FROM siscom_mi.PF_ASSESTAMENTO_VOCI WHERE pf_assestamento_voci.id_assestamento=" & IdAssestamento.Value & " AND IMPORTO IS NULL"
        Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim NUMERO As Integer = 0
        If lettoreEsercizio.Read Then
            NUMERO = par.IfNull(lettoreEsercizio(0), 0)
        End If
        lettoreEsercizio.Close()
        '#######
        If NUMERO = 0 Then
            compl.Value = 11
            ControlloCompleto()
        End If

        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub ControlloCompleto()
        Dim indicecolonna As Integer = TrovaIndiceColonna(datagrid2, "ASSESTAMENTO")
        

        Try
            
            
            Select Case compl.Value
                Case 10
                    'STRUTTURE NON COMPLETATE
                    Label1.Text = "Elenco Strutture non lavorate"
                    AssestamentoCompletato.Text = ""
                    btnStampa.Visible = False
                Case 11
                    'STRUTTURE COMPLETATE
                    Label1.Text = "Tutte le strutture sono state completate. Clicca su completa per completare l'assestamento!"
                    AssestamentoCompletato.Text = ""
                    btnStampa.Visible = False
                    RiepilogoAssestamento()
                Case 12
                    'ASSESTAMENTO CONVALIDABILE
                    Label1.Text = "L'assestamento è convalidabile dal Gestore"
                    AssestamentoCompletato.Text = ""
                    btnStampa.Visible = True
                    btnProcedi.Visible = True
                    RiepilogoAssestamento()
                    'RIDIMENSIONO IL DIV
                    DG1 = "413px"
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    '### date ####
                    par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ASS FROM SISCOM_MI.PF_ASSESTAMENTO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value
                    Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreEsercizio.Read Then
                        If par.IfNull(lettoreEsercizio("ID_STATO"), 1) = 1 Then
                            Label1.Text = "L'assestamento del " & par.IfNull(lettoreEsercizio("DATA_ASS"), "") & " è convalidabile dal Gestore"
                        End If
                    End If
                    lettoreEsercizio.Close()
                    '#######
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Case 3
                    'APPROVATO DAL GESTORE
                    Label1.Text = "L'assestamento è stato approvato dal Gestore"
                    AssestamentoCompletato.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                    btnStampa.Visible = True
                    btnProcedi.Visible = False
                    RiepilogoAssestamento()
                    'RIDIMENSIONO IL DIV
                    DG1 = "413px"
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    '### date ####
                    par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, CASE WHEN SISCOM_MI.PF_ASSESTAMENTO.ID_STATO=3 THEN TO_CHAR(TO_DATE(DATA_APP_ALER,'yyyyMMdd'),'dd/MM/yyyy') ELSE TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA_ASS, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
                    Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreEsercizio.Read Then
                        esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                        If par.IfNull(lettoreEsercizio("id_stato"), 1) = 3 Then
                            Label1.Text = "L'assestamento è stato approvato dal Gestore il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                        End If
                    End If
                    lettoreEsercizio.Close()
                    '#######
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    
                Case 5
                    'ASSESTAMENTO IN STATO 5
                    Label1.Text = "L'assestamento è stato approvato dal Comune"
                    AssestamentoCompletato.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
                    btnStampa.Visible = True
                    btnProcedi.Visible = False
                    RiepilogoAssestamento()
                    'RIDIMENSIONO IL DIV
                    DG1 = "413px"
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    '### date ####
                    par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, CASE WHEN SISCOM_MI.PF_ASSESTAMENTO.ID_STATO=5 THEN TO_CHAR(TO_DATE(DATA_APP_ALER,'yyyyMMdd'),'dd/MM/yyyy') ELSE TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA_ASS, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
                    Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreEsercizio.Read Then
                        esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                        If par.IfNull(lettoreEsercizio("id_stato"), 1) = 5 Then
                            Label1.Text = "L'assestamento è stato approvato dal Comune il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                        End If
                    End If
                    lettoreEsercizio.Close()
                    '#######
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    
            End Select
            
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
   
    End Sub

    Protected Sub RiepilogoAssestamento()
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
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id_stato = 1"
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

                Select Case compl.Value
                    Case 5
                        par.cmd.CommandText = "SELECT DATA_APP_COMUNE AS DATA FROM SISCOM_MI.PF_ASSESTAMENTO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value
                        Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreEsercizio.Read Then
                            DataLimite = par.IfNull(lettoreEsercizio("DATA"), "")
                        End If
                        lettoreEsercizio.Close()
                    Case 3
                        par.cmd.CommandText = "SELECT DATA_APP_ALER AS DATA FROM SISCOM_MI.PF_ASSESTAMENTO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value
                        Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreEsercizio.Read Then
                            DataLimite = par.IfNull(lettoreEsercizio("DATA"), "")
                        End If
                        lettoreEsercizio.Close()
                    Case Else
                        par.cmd.CommandText = "SELECT DATA_INSERIMENTO AS DATA FROM SISCOM_MI.PF_ASSESTAMENTO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value
                        Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreEsercizio.Read Then
                            DataLimite = par.IfNull(lettoreEsercizio("DATA"), "")
                        End If
                        lettoreEsercizio.Close()
                End Select


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
                    If IDSTATO.Value = "5" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        datagrid2.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                    ElseIf IDSTATO.Value = "3" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        datagrid2.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                    Else
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                    End If

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

            datagrid1.Visible = False
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

    Protected Sub CaricaAssestamento()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If IdAssestamento.Value = "0" Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id_stato = 1"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    IdAssestamento.Value = par.IfNull(lettore("ID"), "0")
                    IDSTATO.Value = par.IfNull(lettore("ID_STATO"), "0")
                End If
            End If

            If IdAssestamento.Value = "0" Then
                Response.Write("<script>alert('Attenzione! Non esiste nessun Assestamento con stato Caricamento Importi!');parent.main.location.replace('../../pagina_home.aspx');</script>")
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Completa Assestamento - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub CaricaElencoStrutture()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT DISTINCT ID,NOME AS STRUTTURA FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=PF_ASSESTAMENTO_VOCI.ID_STRUTTURA " _
                & "AND ID_ASSESTAMENTO='" & IdAssestamento.Value & "' AND IMPORTO IS NULL ORDER BY NOME"
            Dim lettoreStrutture As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            Dim dt As New Data.DataTable
            dt.Clear()
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("STRUTTURA")
            Dim RIGA As Data.DataRow
            While (lettoreStrutture.Read)
                RIGA = dt.NewRow
                RIGA.Item("STRUTTURA") = "<a href=""Assestamento.aspx?IDs=" & par.IfNull(lettoreStrutture("ID"), -1) & """>" & par.IfNull(lettoreStrutture("STRUTTURA"), "") & "</a>"
                dt.Rows.Add(RIGA)
            End While
            lettoreStrutture.Read()

            If dt.Rows.Count = 0 Then

                par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=PF_ASSESTAMENTO_VOCI.ID_STRUTTURA " _
                & "AND ID_ASSESTAMENTO='" & IdAssestamento.Value & "' AND COMPLETO=0 ORDER BY NOME"

                Dim LettoreCompleto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim nris As Integer = 0
                If LettoreCompleto.Read Then
                    nris = par.IfNull(LettoreCompleto(0), 0)
                End If
                LettoreCompleto.Close()

                If nris = 0 Then
                    compl.Value = 12
                Else
                    compl.Value = 11
                End If


            Else
                datagrid2.Visible = False
                datagrid1.DataSource = dt
                datagrid1.DataBind()
                datagrid1.Visible = True
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Completa Assestamento - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try


    End Sub

    Protected Sub WriteEvent(ByVal CodEvento As String, ByVal idVoce As Integer, ByVal Importo As Decimal, Optional ByVal Motivazione As String = "")
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_ASSESTAMENTO WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value _
                                & " AND ID_OPERATORE = " & Session.Item("ID_OPERATORE") _
                                & " AND ID_VOCE = " & idVoce & " AND ID_STRUTTURA = " & Request.QueryString("IDs") _
                                & " AND IMPORTO = " & par.VirgoleInPunti(Importo)
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader


            If lettore.Read Then
                '*********************SE ESISTE GIA' L'EVENTO IDENTICA PER VOCE,STRUTTURA,OPERATORE ED IMPORTO NON SCRIVE L'EVENTO PERCHè GIà ESISTENTE
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                    & "(" & IdAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "'," & idVoce & "," & Request.QueryString("IDs") & "," & par.VirgoleInPunti(Importo) & " )"
                par.cmd.ExecuteNonQuery()
            End If


            If ConnOpenNow = True Then

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try
    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click

        Dim labelTitolo As String = ""
        Select Case IDSTATO.Value
            Case "1"
                labelTitolo = "ASSESTAMENTO ESERCIZIO FINANZIARIO " & esercizio.Text & " - CARICAMENTO IMPORTI"
            Case "3"
                labelTitolo = "ASSESTAMENTO ESERCIZIO FINANZIARIO " & esercizio.Text & " - CONVALIDA GESTORE"
            Case "5"
                labelTitolo = "ASSESTAMENTO ESERCIZIO FINANZIARIO " & esercizio.Text & " - CONVALIDA COMUNE"
            Case Else
                labelTitolo = ""
        End Select
        StampaAssestamento()
        Try
            Dim NomeFile As String = "ASS_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            Me.datagrid3.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If

            'pdfConverter.PageWidth = "750"
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter.PdfHeaderOptions.HeaderText = labelTitolo
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Blue
            pdfConverter.PdfHeaderOptions.HeaderTextFontStyle = FontStyle.Bold
            pdfConverter.PdfDocumentOptions.ShowHeader = True
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.PdfDocumentOptions.ShowFooter = True
            pdfConverter.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            If ConfCompleto.Value = 1 Then
                'COMPLETIAMO L'ASSESTAMENTO

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                'AGGIORNIAMO A 1 IL CAMPO COMPLETO PER TUTTE LE VOCI PER L'ID_ASSESTAMENTO IN QUESTIONE
                par.cmd.CommandText = "UPDATE SISCOM_MI.PF_ASSESTAMENTO_VOCI SET COMPLETO=1,IMPORTO=(CASE WHEN IMPORTO IS NULL THEN 0 ELSE IMPORTO END) WHERE ID_ASSESTAMENTO='" & IdAssestamento.Value & "'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.PF_ASSESTAMENTO SET ID_STATO=2 WHERE ID='" & IdAssestamento.Value & "'"
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                'ASSESTAMENTO CONVALIDABILE
                compl.Value = 12
                ControlloCompleto()
                Response.Write("<script>alert('Operazione effettuata con successo! L\'assestamento è adesso convalidabile dal Gestore ');location.href='GestioneAssestamento.aspx?id=" & IdAssestamento.Value & "'</script>")
                Exit Sub
            End If
            Response.Redirect("GestioneAssestamento.aspx?id=" & IdAssestamento.Value)
        Catch ex As Exception

            Me.lblErrore.Visible = True
            lblErrore.Text = "Completa - " & ex.Message
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

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

    Protected Sub StampaAssestamento()
        Dim indicecolonna As Integer = TrovaIndiceColonna(datagrid3, "APPROVATO")
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            dtriepilogoStampa.Clear()
            dtriepilogoStampa.Rows.Clear()
            dtriepilogoStampa.Columns.Clear()
            dtriepilogoStampa.Columns.Add("CODICE")
            dtriepilogoStampa.Columns.Add("VOCE")
            dtriepilogoStampa.Columns.Add("RICHIESTO")
            dtriepilogoStampa.Columns.Add("APPROVATO")
            Dim RIGA As Data.DataRow

            '#### SELEZIONO L'ASSESTAMENTO ####
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            If IdAssestamento.Value = 0 Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id_stato = 1"
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
                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO,SUM(NVL(IMPORTO_APPROVATO,0)) AS IMPORTO_APPROVATO FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_ASSESTAMENTO = " & IdAssestamento.Value
                Dim RICHIESTO As Decimal = 0
                Dim APPROVATO As Decimal = 0
                Dim lettoreImportoRichiesto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoRichiesto.Read Then
                    If IdStato.Value = "5" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                        APPROVATO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        datagrid3.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                    ElseIf IdStato.Value = "3" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                        APPROVATO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        datagrid3.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                    Else
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                        APPROVATO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                    End If

                End If
                lettoreImportoRichiesto.Close()

                RIGA = dtriepilogoStampa.NewRow()
                RIGA.Item("CODICE") = par.IfNull(ElencoVoci("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(ElencoVoci("DESCRIZIONE"), "")
                RIGA.Item("RICHIESTO") = Format(RICHIESTO, "##,##0.00")
                RIGA.Item("APPROVATO") = Format(APPROVATO, "##,##0.00")
                dtriepilogoStampa.Rows.Add(RIGA)

            End While
            ElencoVoci.Close()
            datagrid3.DataSource = dtriepilogoStampa
            datagrid3.DataBind()
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

    Protected Sub indietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles indietro.Click
        Response.Redirect("AssestamentoPerStruttura.aspx?id=" & IdAssestamento.Value)
    End Sub
End Class
