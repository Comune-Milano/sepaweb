Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_Assestamento
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("MOD_ASS_COMPILA") <> 1 Then
            Response.Redirect("../../pagina_home.aspx", True)
            Exit Sub
        End If
        Try

           

            Me.completa.Visible = False

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
            lblErrore.Text = ""
            If Not IsPostBack Then
                Response.Flush()
                StrutturaCompletata.Text = ""
                ControlloAssestamento()
                CaricaVoci(Request.QueryString("IDs"))
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Protected Sub VerificaCompleta()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'CONTROLLO CHE TUTTI GLI IMPORTI PER LA STRUTTURA SIANO STATI SALVATI
            par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI WHERE ID_ASSESTAMENTO='" & IdAssestamento.Value & "' AND IMPORTO IS NULL " _
                & "AND ID_STRUTTURA='" & Request.QueryString("IDs") & "' "
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                If par.IfNull(reader(0), 0) = 0 Then
                    'STRUTTURA COMPLETATA
                    'StrutturaCompletata.Text = "STRUTTURA COMPLETATA"
                    StrutturaCompletata.Text = ""
                Else
                    StrutturaCompletata.Text = ""
                End If
            End If
            reader.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ControlloAssestamento()
        'CONTROLLO CHE CI SIA UN ASSESTAMENTO IN FASE DI CARICAMENTO IMPORTI o DA CONVALIDARE
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ID,ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID_STATO=1 OR ID_STATO=2"
            Dim READER As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If READER.Read Then
                idstato.Value = par.IfNull(READER(1), "0")
                idassestamento.Value = par.IfNull(READER(0), "0")
            End If
            READER.Close()
            If idstato.Value = 2 Then
                completa.Visible = False
                convalida.Visible = True
            ElseIf idstato.Value = 1 Then
                convalida.Visible = False
            End If


            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & idassestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
            End If
            lettoreEsercizio.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub CaricaVoci(ByVal idStruttura As String)
        'Dim indicecolonna As Integer = TrovaIndiceColonna(DataGrid3, "ASSESTAMENTO")
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio2.Read Then
                esercizio.Text = par.IfNull(lettoreEsercizio2("inizio"), "") & " - " & par.IfNull(lettoreEsercizio2("fine"), "")
            End If
            lettoreEsercizio2.Close()
            '#######


            '###### MODIFICA STRUTTURA #############
            Dim dtriepilogo As New Data.DataTable
            dtriepilogo.Clear()
            dtriepilogo.Rows.Clear()
            dtriepilogo.Columns.Clear()
            dtriepilogo.Columns.Add("CODICE")
            dtriepilogo.Columns.Add("DESCRIZIONE")
            dtriepilogo.Columns.Add("BUDGET_INIZIALE")
            dtriepilogo.Columns.Add("ASSESTAMENTI_PRECEDENTI")
            dtriepilogo.Columns.Add("VARIAZIONI")
            dtriepilogo.Columns.Add("SPESO")
            dtriepilogo.Columns.Add("RESIDUO")
            dtriepilogo.Columns.Add("ASSESTAMENTO")
            dtriepilogo.Columns.Add("TIPO")
            dtriepilogo.Columns.Add("ID")
            dtriepilogo.Columns.Add("ID_PIANO_FINANZIARIO")
            dtriepilogo.Columns.Add("APPROVATO")
            Dim RIGA As Data.DataRow
            'SELEZIONO NOME STRUTTURA
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & Request.QueryString("IDs")
            Dim lettoreIDs As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreIDs.Read Then
                TXTStruttura.Text = par.IfNull(lettoreIDs(0), "")
            End If
            lettoreIDs.Close()
            '#### SELEZIONO L'ASSESTAMENTO ####
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim id_Stato As Integer = 0
            If IdAssestamento.Value = 0 Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id_stato = 1 OR ID_STATO=2"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    IdAssestamento.Value = par.IfNull(lettore("ID"), 0)
                    id_Stato = par.IfNull(lettore(1), 0)
                End If
                lettore.Close()
            End If
            '##################################
            If id_Stato = 2 Then
                completa.Visible = False
            End If
            If id_Stato = 1 Then
                convalida.Visible = False
            End If
            '#### CREO ELENCO VOCI ######
            par.cmd.CommandText = "SELECT DISTINCT ID_ASSESTAMENTO,ID_VOCE,DESCRIZIONE,CODICE," _
                & "CASE WHEN ID_VOCE IN (SELECT DISTINCT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                & "START WITH PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NULL) " _
                & ") " _
                & "THEN 'FIGLIO' " _
                & "ELSE 'MADRE' " _
                & "END AS TIPO," _
                & "ID_PIANO_FINANZIARIO " _
                & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI," _
                & "SISCOM_MI.PF_VOCI " _
                & "WHERE PF_VOCI.ID = PF_ASSESTAMENTO_VOCI.ID_VOCE AND ID_ASSESTAMENTO=" & IdAssestamento.Value & " ORDER BY CODICE ASC"
            Dim ElencoVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While ElencoVoci.Read
                'IMPOSTO BUDGET E ASSESTAMENTO
                par.cmd.CommandText = "SELECT " _
                    & "TRIM(TO_CHAR(SUM(NVL(VALORE_LORDO,0)),'999999990D9999')) AS VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)),'999999990D9999')) AS ASSESTAMENTO_VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(VARIAZIONI, 0)),'999999990D9999')) AS VARIAZIONI " _
                    & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                    & "WHERE ID_STRUTTURA='" & idStruttura & "' " _
                    & "AND ID_VOCE IN (" _
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
                    & "AND ID_STRUTTURA='" & idStruttura & "' " _
                    & "AND SUBSTR(DATA_ORA,1,8)>'" & DataLimite & "'"

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
                    & "AND ID_STRUTTURA='" & idStruttura & "' " _
                    & "AND SUBSTR(DATA_ORA,1,8)>'" & DataLimite & "'"

                Dim LettoreVariazioniPositive As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreVariazioniPositive.Read Then
                    VARIAZIONI_POSITIVE = par.IfNull(LettoreVariazioniPositive(0), 0)
                End If
                LettoreVariazioniPositive.Close()
                VARIAZIONI = VARIAZIONI - CDec(VARIAZIONI_POSITIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°


                Dim TRASF_FONDI_NEGATIVE As Decimal = 0
                Dim TRASF_FONDI_POSITIVE As Decimal = 0

                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_TRASF_FONDI " _
                    & "WHERE ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_STRUTTURA_DA='" & idStruttura & "' " _
                    & "AND SUBSTR(DATA_ORA,1,8)>'" & DataLimite & "'"

                Dim LettoreTRASF_FONDINegative As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreTRASF_FONDINegative.Read Then
                    TRASF_FONDI_NEGATIVE = par.IfNull(LettoreTRASF_FONDINegative(0), 0)
                End If
                LettoreTRASF_FONDINegative.Close()
                VARIAZIONI = VARIAZIONI + CDec(TRASF_FONDI_NEGATIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_TRASF_FONDI " _
                    & "WHERE ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_STRUTTURA_A='" & idStruttura & "' " _
                    & "AND SUBSTR(DATA_ORA,1,8)>'" & DataLimite & "'"

                Dim LettoreTRASF_FONDIPositive As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreTRASF_FONDIPositive.Read Then
                    TRASF_FONDI_POSITIVE = par.IfNull(LettoreTRASF_FONDIPositive(0), 0)
                End If
                LettoreTRASF_FONDIPositive.Close()
                VARIAZIONI = VARIAZIONI - CDec(TRASF_FONDI_POSITIVE)
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
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
                    & " AND PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO=PF_ASSESTAMENTO.ID " _
                    & "AND ID_STRUTTURA='" & idStruttura & "' " _
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
                    & "AND ID_STRUTTURA='" & idStruttura & "' " _
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
                    & "AND ID_STRUTTURA='" & idStruttura & "' " _
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
                    & "AND ID_ASSESTAMENTO = " & IdAssestamento.Value _
                    & " AND ID_STRUTTURA='" & idStruttura & "' "

                Dim RICHIESTO As Decimal = 0
                Dim lettoreImportoRichiesto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoRichiesto.Read Then
                    If IDSTATO.Value = "5" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        'DataGrid3.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                    ElseIf IDSTATO.Value = "3" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        'DataGrid3.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                    Else
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                    End If

                End If
                lettoreImportoRichiesto.Close()

                Dim RESIDUO As Decimal = BUDGET + VARIAZIONI + ASSESTAMENTI_PRECEDENTI - SPESO
                RIGA = dtriepilogo.NewRow()
                RIGA.Item("CODICE") = par.IfNull(ElencoVoci("CODICE"), "")
                RIGA.Item("DESCRIZIONE") = par.IfNull(ElencoVoci("DESCRIZIONE"), "")
                RIGA.Item("BUDGET_INIZIALE") = Format(BUDGET, "##,##0.00")
                RIGA.Item("ASSESTAMENTI_PRECEDENTI") = Format(ASSESTAMENTI_PRECEDENTI, "##,##0.00")
                RIGA.Item("VARIAZIONI") = Format(VARIAZIONI, "##,##0.00")
                RIGA.Item("SPESO") = Format(SPESO, "##,##0.00")
                RIGA.Item("RESIDUO") = Format(RESIDUO, "##,##0.00")
                RIGA.Item("ASSESTAMENTO") = Format(RICHIESTO, "##,##0.00")
                RIGA.Item("TIPO") = par.IfNull(ElencoVoci("TIPO"), "")
                RIGA.Item("ID") = par.IfNull(ElencoVoci("ID_VOCE"), "")
                RIGA.Item("APPROVATO") = " "
                dtriepilogo.Rows.Add(RIGA)

            End While
            ElencoVoci.Close()

            DataGrid3.DataSource = dtriepilogo
            DataGrid3.DataBind()
            DataGrid3.Visible = True
            SettaEditSottovoci()
            AddJavascriptFunction()
            VerificaCompleta()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Sub SettaEditSottovoci()
        Try

            Dim code As String = ""
            Dim codeNext As String = ""
            Dim i As Integer = 0
            Dim di As DataGridItem

            For i = 0 To Me.DataGrid3.Items.Count - 1
                di = Me.DataGrid3.Items(i)

                If di.Cells(TrovaIndiceColonna(DataGrid3, "TIPO")).Text = "MADRE" Then
                    DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).ReadOnly = True
                    DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).BorderColor = Drawing.Color.Transparent
                    DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).BackColor = Drawing.Color.Transparent
                Else
                    DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).BackColor = Drawing.Color.LightBlue
                    DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).ForeColor = Drawing.Color.Black

                End If
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "SettaEditSottovoci - " & ex.Message
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
            Me.lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Update()

    End Sub

    Private Sub Update()

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim Importo As Decimal = 0
            Dim completo As Integer = 0

            'UPDATE VOCI DELLA STRUTTURA SELEZIONATA E DELL'ASSESTAMENTO IN CORSO
            For Each item As DataGridItem In DataGrid3.Items

                If DirectCast(item.Cells(7).FindControl("txtImpAssest"), TextBox).ReadOnly = False Then
                    Try
                        Importo = CDec(DirectCast(item.Cells(7).FindControl("txtImpAssest"), TextBox).Text.Replace(".", ""))
                    Catch ex As Exception
                        'SE L'IMPORTO è LASCIATO VOLUTAMENTE VUOTO
                        Importo = 0
                    End Try
                Else
                    'IMPORTO POSTO A ZERO PER LE VOCI NON FOGLIE
                    Importo = 0
                End If
                par.cmd.CommandText = "UPDATE SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                        & "SET IMPORTO = '" & Importo & "' " _
                                        & ", COMPLETO = '" & completo & "' " _
                                        & " WHERE ID_VOCE = '" & item.Cells(TrovaIndiceColonna(DataGrid3, "ID")).Text & "' " _
                                        & " AND ID_STRUTTURA = '" & Request.QueryString("IDs") & "' " _
                                        & " AND ID_ASSESTAMENTO= '" & IdAssestamento.Value & "'"
                par.cmd.ExecuteNonQuery()
                WriteEvent("F02", item.Cells(TrovaIndiceColonna(DataGrid3, "ID")).Text, Importo, "IMPORTO ASSESTAMENTO")

            Next
            '------------------------------------------------------------------------------

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            CaricaVoci(Request.QueryString("IDs"))
            Response.Write("<script>alert('Operazione eseguita correttamente!La richiesta di assestamento è stata memorizzata!');</script>")

            '##################################################################################################################################


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Update - " & ex.Message
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            For Each di As DataGridItem In DataGrid3.Items

                DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                DirectCast(di.Cells(7).FindControl("txtImpAssest"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "AddJavascriptFunction - " & ex.Message
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

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("../../pagina_home.aspx")
    End Sub

    Protected Sub indietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles indietro.Click
        Response.Redirect("GestioneAssestamento.aspx?id=" & IdAssestamento.Value)
    End Sub

    Protected Sub completa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles completa.Click
        Response.Redirect("CompletaAssestamento.aspx?IDassestamento=" & IdAssestamento.Value & "&IDstato=" & IDSTATO.Value)
    End Sub

    Protected Sub convalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles convalida.Click
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
            Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento ha ora stato: APPROVATO DAL GESTORE!');location.href='GestioneAssestamento.aspx?id=" & IdAssestamento.Value & "'</script>")
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "Convalida - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        
    End Sub
    Public Function WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Dim idPadre As String = "null"
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                & "(" & IdAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', '" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',NULL,NULL,NULL )"
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
        Return idPadre
    End Function

End Class
