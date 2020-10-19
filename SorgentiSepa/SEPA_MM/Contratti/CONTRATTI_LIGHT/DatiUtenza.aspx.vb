﻿
Partial Class Contratti_CONTRATTI_LIGHT_DatiUtenza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Saldo As Double = 0
    Dim TotEmesso As Double = 0
    Dim TotIncassato As Double = 0
    Dim TotSaldo As Double = 0
    Dim TotContabile As Double = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        CaricaDatiContrattuali()
    End Sub

    Private Sub CaricaDatiContrattuali()

        Dim testoTabella As String = ""
        Dim testoTabSaldo As String = ""
        Dim strSql As String = ""
        Dim COLORE As String = "#E6E6E6"
        Dim EmessoContabile As Double = 0
        Dim incassato As Double = 0

        If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then
            strSql = "SELECT anagrafica.ragione_sociale, RAPPORTI_UTENZA.ID AS ID_CONTRATTO,RAPPORTI_UTENZA.ID_AU,ANAGRAFICA.ID AS ""ID_ANAGRAFICA"", CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"", CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"", RAPPORTI_UTENZA.COD_CONTRATTO,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"" ,INDIRIZZI.CIVICO, UNITA_IMMOBILIARI.INTERNO,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,INDIRIZZI.CAP, TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""LIV_PIANO"", TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS ""TIPO_CONTRATTO"", TIPOLOGIA_CONTRATTO_LOCAZIONE.RIF_LEGISLATIVO, to_char(to_date(RAPPORTI_UTENZA.DATA_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_DECORRENZA, TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA ,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_FINE_LOC"",SISCOM_MI.Getstatocontratto(RAPPORTI_UTENZA.ID) AS ""STATO"" " _
                    & "FROM   SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  " _
                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO(+) " _
                    & "AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID(+) " _
                    & "AND RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                    & "AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                    & "AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
                    & "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID(+)" _
                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD(+) " _
                    & "AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID =" & Request.QueryString("IDCONT") & " AND ID_ANAGRAFICA = " & Request.QueryString("IDANA")
        Else
            Response.Write("<SCRIPT>alert('Non è possibile trovare informazioni anagrafiche dell\'inquilino!');</SCRIPT>")
            Exit Sub
        End If


        IDANA = Request.QueryString("IDANA")
        IdCont.Value = Request.QueryString("IDCONT")

        ' COSTRUZIONE COLONNE TABELLA DATI CONTRATTUALI
        testoTabella = "<table cellpadding='1' cellspacing='0' width='100%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.CONTRATT</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>INTESTATARIO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.UNITA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO UNITA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>INDIRIZZO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>INT.</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>SC.</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>INIZIO LOC.</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>FINE LOC.</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO CONTRATTO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO RAPPORTO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>PERTINENZE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "</td>" _
                        & "<td style='height: 19px'>" _
                        & "</td>" _
                        & "</tr>"
        'COSTRUZIONE COLONNE TABELLA SALDO GENERALE
        testoTabSaldo = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                & "<tr >" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.CONTRATT</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA INIZIO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA FINE</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>EMESSO IN BOLLETTA</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>EMESSO CONTABILE</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>INCASSATO</strong></span></td>" _
                & "<td style='height: 19px;text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>SALDO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "</tr>"

        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
            Dim UltimoPagam As String = 0
            'par.cmd.CommandText = "SELECT to_char(to_date(MAX(DATA_PAGAMENTO),'yyyymmdd'),'dd/mm/yyyy') AS ULTIMA_BOLL FROM SISCOM_MI.BOL_BOLLETTE WHERE n_rata<>999 and N_RATA<>99"
            'Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderTEMP.Read Then
            UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) ' par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
            'End If
            'myReaderTEMP.Close()
            'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI


            par.cmd.CommandText = strSql
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Dim nc As String = ""
            If myReader.Read Then
                Me.LblIntestazione.Text = " - " & par.IfNull(myReader("INTESTATARIO"), "") & " - informazioni aggiornate al: " & UltimoPagam

                If par.IfNull(myReader("ragione_sociale"), "") = "" Then
                    nc = par.IfNull(myReader("INTESTATARIO"), "") & "</span></td>"
                Else
                    nc = par.IfNull(myReader("INTESTATARIO"), "") & "</span></td>"
                End If

                Dim TIPO As String = ""

                par.cmd.CommandText = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.descrizione FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND COD_UNITA_IMMOBILIARE = '" & par.IfNull(myReader("cod_unita_immobiliare"), "") & "'"

                Dim Leggi As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If Leggi.Read Then
                    TIPO = "<td style='height: 19px'>" _
                         & "<span style='font-size: 8pt; font-family: Arial'>" & Leggi(0) & "</span></td>"
                Else
                    TIPO = "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>"
                End If
                Leggi.Close()
                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & nc _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("cod_unita_immobiliare"), "") & "</span></td>" _
                & TIPO _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("INDIRIZZO"), "") & "," & par.IfNull(myReader("CIVICO"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("INTERNO"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("SCALA"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("DATA_FINE_LOC"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO_CONTRATTO"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("STATO"), "") & "</span></td>"


                '******PERTINENZE CONTRATTUALIZZATE******
                par.cmd.CommandText = "SELECT UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE, UNITA_CONTRATTUALE.ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NOT NULL AND RAPPORTI_UTENZA.ID= UNITA_CONTRATTUALE.ID_CONTRATTO AND ID_CONTRATTO =" & par.IfNull(myReader("ID_CONTRATTO"), "")
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.HasRows Then
                    testoTabella = testoTabella & "<td style='height: 19px;text-align:right'>"
                    Do While myReader2.Read
                        testoTabella = testoTabella & "<span style='font-size: 8pt; font-family: Arial'>>" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "</span><br />"
                    Loop
                    testoTabella = testoTabella & "</td>"
                Else
                    testoTabella = testoTabella & "<td style='height: 19px'>" _
                    & "</td>"
                End If
                myReader2.Close()
                '******FINE PERTINENZE CONTRATTUALIZZATE******
                par.cmd.CommandText = "SELECT " _
                                    & "SUM(NVL(IMPORTO_TOTALE,0)- NVL(IMPORTO_RIC_B,0)) EMESSO_BOLLETTA," _
                                    & "SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS EMESSO_CONTABILE, " _
                                    & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS INCASSATO, " _
                                    & "(SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0))-(SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)))) AS saldo " _
                                    & "FROM SISCOM_MI.BOL_BOLLETTE " _
                                    & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                                    & "AND ID_CONTRATTO = " & par.IfNull(myReader("ID_CONTRATTO"), "")
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    testoTabSaldo = testoTabSaldo _
                    & "<tr bgcolor = '" & COLORE & "'>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("DATA_FINE_LOC"), "") & "</span></td>" _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader3("EMESSO_BOLLETTA"), 0), "##,##0.00") & "</span></td>"

                    TotEmesso = TotEmesso + par.IfNull(myReader3("EMESSO_BOLLETTA"), 0)
                    EmessoContabile = par.IfNull(myReader3("EMESSO_CONTABILE"), 0)

                    testoTabSaldo = testoTabSaldo _
                        & "<td style='height: 19px; text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(EmessoContabile, 0), "##,##0.00") & "</span></td>"

                    TotContabile = TotContabile + EmessoContabile
                    incassato = par.IfNull(myReader3("INCASSATO"), 0)
                    Saldo = EmessoContabile - incassato
                    TotSaldo = TotSaldo + Saldo
                    TotIncassato = TotIncassato + incassato
                    testoTabSaldo = testoTabSaldo _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader3("INCASSATO"), 0), "##,##0.00") & "</span></td>" _
                        & "<td style='height: 19px; text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Saldo, "##,##0.00") & "</span></td>"
                    Saldo = 0


                    testoTabSaldo = testoTabSaldo & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "</tr>"

                    If COLORE = "#FFFFFF" Then
                        COLORE = "#E6E6E6"
                    Else
                        COLORE = "#FFFFFF"
                    End If

                End If

            End If

            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            '*********************TOTALI PER COLONNA
            testoTabSaldo = testoTabSaldo & vbCrLf _
            & "<tr >" _
            & "<td style='height: 19px'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
            & "<td style='height: 19px'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
            & "<td style='height: 19px'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
            & "<td style='height: 19px; text-align:right'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotEmesso, "##,##0.00") & "</strong></span></td>" _
            & "<td style='height: 19px; text-align:right'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotContabile, "##,##0.00") & "</strong></span></td>" _
            & "<td style='height: 19px;text-align:right'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotIncassato, "##,##0.00") & "</strong></span></td>" _
            & "<td style='height: 19px; text-align:right'>" _
            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotSaldo, "##,##0.00") & "</strong></span></td>" _
            & "<td style='height: 19px'>" _
            & "</td>" _
            & "<td style='height: 19px'>" _
            & "</td>" _
            & "</tr>"


            Me.TBL_DATI_CONTRATTUALI.Text = testoTabella & "</table>"
            Me.TBL_SALDO_GENERALE.Text = testoTabSaldo & "</table>"

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Public Property IDANA() As String
        Get
            If Not (ViewState("par_IDANA") Is Nothing) Then
                Return CStr(ViewState("par_IDANA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IDANA") = value
        End Set

    End Property
End Class
