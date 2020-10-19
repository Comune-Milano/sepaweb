Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports ExpertPdf.HtmlToPdf

Partial Class MOROSITA_MesseInMora
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim listaQuerySomme As New System.Collections.Generic.List(Of String)
    Dim dataestrazione As String = ""
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
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
        If IsNothing(Request.QueryString("d")) Or Request.QueryString("d") = "" Then
            dataestrazione = Format(Now, "yyyyMMdd")
        Else
            dataestrazione = Request.QueryString("d")
        End If
        If Not IsPostBack Then
            Response.Flush()
            If IsNothing(Session.Item("LISTAMOROSITAMM")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati, ripetere la ricerca!');location.href='RicercaDebitoriMultiSelezioneMor.aspx';", True)
            Else
                ListaMM = Session.Item("LISTAMOROSITAMM")
                caricaDati()
            End If
            If Not IsNothing(Session.Item("PARAMETRIDIRICERCA")) Then
                parametriDiRicerca.Text = Session.Item("PARAMETRIDIRICERCA")
                Session.Remove("PARAMETRIDIRICERCA")
            Else
                parametriDiRicerca.Text = ""
            End If
            Session.Remove("LISTAMOROSITAMM")
        End If
    End Sub
    Public Property ListaMM() As Object
        Get
            If Not (ViewState("ListaMM") Is Nothing) Then
                Return CObj(ViewState("ListaMM"))
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Object)
            ViewState("ListaMM") = value
        End Set
    End Property
    Private Sub caricaDati()
        Try
            ApriConnessione()
            '--------------- protocollo ---------------------------------
            Dim listaContratti As String = ""
            If Not IsNothing(ListaMM) Then
                Dim i As Integer = 0
                Dim fine As Boolean = False
                While i < 30 And fine = False
                    listaContratti = ""
                    For j As Integer = 0 To 999
                        If 1000 * i + j < ListaMM.Count Then
                            listaContratti &= ListaMM(1000 * i + j) & ","
                        Else
                            fine = True
                            Exit For
                        End If
                    Next
                    If listaContratti <> "" Then
                        listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                        listaQuerySomme.Add(" ID_contratto in (" & listaContratti & ") OR ")
                    End If
                    i += 1
                End While
            End If
            Dim condizioneContratti As String = ""
            For Each Items As String In listaQuerySomme
                condizioneContratti &= Items
            Next
            If condizioneContratti <> "" Then
                condizioneContratti = " and (" & Left(condizioneContratti, Len(condizioneContratti) - 3) & ")"
            End If
            '--------------- TUTTI -----------------------------
            Dim querySomme As String = " SELECT data_protocollo,PROTOCOLLO_ALER, " _
                & " PROGR, " _
                & " COUNT (distinct cod_contratto) AS NUMERO_LETTERE, " _
                & " SUM (NVL (IMPORTO_INIZIALE, 0)) AS SOMMA_INIZIale " _
                & " FROM SISCOM_MI.MOROSITA_LETTERE, SISCOM_MI.MOROSITA " _
                & " WHERE " _
                & " MOROSITA.ID = MOROSITA_LETTERE.ID_MOROSITA " _
                & " and data_protocollo<='" & dataestrazione & "' " _
                & condizioneContratti _
                & " GROUP BY PROTOCOLLO_ALER, PROGR,data_protocollo order by progr asc "

            par.cmd.CommandText = querySomme
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim LettoreProtocolli As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            Dim numeroMMinviate As Integer = 0
            Dim importoTotaleMM As Decimal = 0
            Dim numeroInquiliniBollettinoMM As Integer = 0
            Dim importoTotaleIncassatoMM As Decimal = 0
            Dim numeroInquiliniDilazioneDebito As Integer = 0
            Dim ImportoTotaleDilazione As Decimal = 0
            Dim numeroBollettiniEmessi As Integer = 0
            Dim numeroBollettiniSaldati As Integer = 0
            Dim ImportoTotaleIncassatoDebito As Decimal = 0
            Dim riepilogoGenerale As String = ""

            While LettoreProtocolli.Read
                numeroMMinviate += par.IfNull(LettoreProtocolli("numero_lettere"), 0)
                importoTotaleMM += par.IfNull(LettoreProtocolli("somma_iniziale"), 0)
                Dim progr As Integer = par.IfNull(LettoreProtocolli("progr"), 0)
                par.cmd.CommandText = "SELECT COUNT (DISTINCT ID_CONTRATTO) FROM SISCOM_MI.BOL_BOLLETTE " _
                    & "WHERE RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE  " _
                    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA " _
                    & "WHERE PROGR=" & progr & ")) and importo_pagato>0 " _
                    & "AND bol_Bollette.data_emissione<='" & dataestrazione & "' " _
                    & condizioneContratti

                Dim numeroMesseinMora As Integer = 0
                Dim lettoreMor As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreMor.Read Then
                    numeroMesseinMora = par.IfNull(lettoreMor(0), 0)
                End If
                lettoreMor.Close()
                numeroInquiliniBollettinoMM += numeroMesseinMora
                Dim importoMesseinMora As Decimal = 0
                'par.cmd.CommandText = "SELECT SUM(NVL(BOL_BOLLETTE_PAGAMENTI.IMPORTO_PAGATO,0))," _
                '    & "SUM(MAX(NVL(BOL_BOLLETTE.IMPORTO_PAGATO,0))) " _
                '    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_PAGAMENTI " _
                '    & "WHERE RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                '    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA " _
                '    & "WHERE PROGR=" & progr & ")) AND IMPORTO_PAGATO>0 " _
                '    & "AND BOL_BOLLETTE.ID=BOL_BOLLETTE_PAGAMENTI.ID_BOLLETTA(+) "
                par.cmd.CommandText = " SELECT NVL (BOL_BOLLETTE.IMPORTO_PAGATO, 0), " _
                    & " (SELECT MAX (NVL (IMPORTO_PAGATO, 0)) " _
                    & " FROM SISCOM_MI.BOL_BOLLETTE_PAGAMENTI " _
                    & " WHERE DATA_PAGAMENTO<='" & dataestrazione & "' " _
                    & " AND ID_BOLLETTA(+) = BOL_BOLLETTE.ID) " _
                    & " FROM SISCOM_MI.BOL_BOLLETTE " _
                    & " WHERE RIF_BOLLETTINO IN (SELECT BOLLETTINO " _
                    & " FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE ID_MOROSITA = (SELECT ID " _
                    & " FROM SISCOM_MI.MOROSITA " _
                    & " WHERE PROGR = " & progr & ")) " _
                    & " AND BOL_BOLLETTE.IMPORTO_PAGATO > 0 " _
                    & condizioneContratti

                lettoreMor = par.cmd.ExecuteReader
                While lettoreMor.Read
                    importoMesseinMora += par.IfNull(lettoreMor(1), 0)
                End While
                lettoreMor.Close()
                importoTotaleIncassatoMM += importoMesseinMora
                Dim dilazioni As Integer = 0
                par.cmd.CommandText = "SELECT COUNT(DISTINCT ID_CONTRATTO) " _
                    & "FROM SISCOM_MI.BOL_BOLLETTE WHERE bol_Bollette.data_emissione<='" & dataestrazione & "' AND ID_RATEIZZAZIONE IS NOT NULL " _
                    & "AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA WHERE PROGR=" & progr & ")) " _
                    & condizioneContratti

                lettoreMor = par.cmd.ExecuteReader
                If lettoreMor.Read Then
                    dilazioni = par.IfNull(lettoreMor(0), 0)
                End If
                lettoreMor.Close()
                numeroInquiliniDilazioneDebito += dilazioni
                Dim ImportoTotale As Decimal = 0
                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO_TOTALE,0)) " _
                    & " FROM SISCOM_MI.BOL_BOLLETTE WHERE bol_Bollette.data_emissione<='" & dataestrazione & "' " _
                    & " AND ID_RATEIZZAZIONE IS NOT NULL " _
                    & " AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA WHERE PROGR=" & progr & ")) " _
                    & condizioneContratti

                lettoreMor = par.cmd.ExecuteReader
                If lettoreMor.Read Then
                    ImportoTotale = par.IfNull(lettoreMor(0), 0)
                End If
                lettoreMor.Close()
                ImportoTotaleDilazione += ImportoTotale
                Dim importoTotaleIncassato As Decimal = 0
                par.cmd.CommandText = "SELECT (NVL(IMPORTO_PAGATO,0)), " _
                    & "(SELECT MAX (NVL (IMPORTO_PAGATO, 0))  FROM SISCOM_MI.BOL_BOLLETTE_PAGAMENTI  " _
                    & " WHERE DATA_PAGAMENTO<='" & dataestrazione & "' AND  ID_BOLLETTA(+) = BOL_BOLLETTE.ID) " _
                    & "FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_RATEIZZAZIONE IS NOT NULL " _
                    & "AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA WHERE PROGR=" & progr & ")) " _
                    & condizioneContratti


                lettoreMor = par.cmd.ExecuteReader
                While lettoreMor.Read
                    importoTotaleIncassato += par.IfNull(lettoreMor(0), 1)
                End While
                lettoreMor.Close()
                ImportoTotaleIncassatoDebito += importoTotaleIncassato
                Dim nBolletteEmesse As Integer = 0
                par.cmd.CommandText = "SELECT COUNT(*) " _
                    & " FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT " _
                    & " WHERE BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE " _
                    & " AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL " _
                    & " AND RIF_BOLLETTINO IN (SELECT BOLLETTINO " _
                    & " FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE ID_MOROSITA = (SELECT ID " _
                    & " FROM SISCOM_MI.MOROSITA " _
                    & " WHERE PROGR = " & progr & ")) " _
                    & " AND ID_BOLLETTA IS NOT NULL " _
                    & " AND bol_Bollette.data_emissione<='" & dataestrazione & "' " _
                    & condizioneContratti

                lettoreMor = par.cmd.ExecuteReader
                If lettoreMor.Read Then
                    nBolletteEmesse = par.IfNull(lettoreMor(0), 0)
                End If
                lettoreMor.Close()
                numeroBollettiniEmessi += nBolletteEmesse
                Dim nBolletteSaldate As Integer = 0
                par.cmd.CommandText = "SELECT COUNT(*) " _
                    & " FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT " _
                    & " WHERE BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE " _
                    & " AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL " _
                    & " AND RIF_BOLLETTINO IN (SELECT BOLLETTINO " _
                    & " FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE ID_MOROSITA = (SELECT ID " _
                    & " FROM SISCOM_MI.MOROSITA " _
                    & " WHERE PROGR = " & progr & ")) " _
                    & " AND ID_BOLLETTA IS NOT NULL " _
                    & " AND IMPORTO_PAGATO>0 " _
                    & " AND bol_Bollette.data_emissione<='" & dataestrazione & "' " _
                    & condizioneContratti

                lettoreMor = par.cmd.ExecuteReader
                If lettoreMor.Read Then
                    nBolletteSaldate = par.IfNull(lettoreMor(0), 0)
                End If
                lettoreMor.Close()
                numeroBollettiniSaldati += nBolletteSaldate
                par.cmd.CommandText = "SELECT distinct id_contratto,CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE ELSE COGNOME ||' '|| NOME END AS INTESTATARIO,cod_contratto FROM SISCOM_MI.MOROSITA_LETTERE,SISCOM_MI.ANAGRAFICA, " _
                    & " SISCOM_MI.MOROSITA  WHERE ANAGRAFICA.ID=MOROSITA_LETTERE.ID_ANAGRAFICA AND progr=" & progr & " and  MOROSITA.ID = MOROSITA_LETTERE.ID_MOROSITA  " _
                    & condizioneContratti _
                    & " order by 2 asc "

                Dim LettoreContratti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim dettagliInquilini As String = ""
                Dim primo As Boolean = True
                While LettoreContratti.Read
                    par.cmd.CommandText = "SELECT COUNT (DISTINCT ID_CONTRATTO) FROM SISCOM_MI.BOL_BOLLETTE " _
                    & "WHERE BOL_BOLLETTE.DATA_eMISSIONE<='" & dataestrazione & "' AND " _
                    & "RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE  " _
                    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA " _
                    & "WHERE PROGR=" & progr & ")) and importo_pagato>0 " _
                    & "and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    Dim numeroMesseinMoraInq As String = "No"
                    Dim lettoreMorInq As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreMorInq.Read Then
                        If par.IfNull(lettoreMorInq(0), 0) = 0 Then
                            numeroMesseinMoraInq = "No"
                        Else
                            numeroMesseinMoraInq = "Sì"
                        End If
                    End If
                    lettoreMorInq.Close()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE " _
                       & "WHERE BOL_BOLLETTE.DATA_eMISSIONE<='" & dataestrazione & "' AND " _
                       & "RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE  " _
                       & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA " _
                       & "WHERE PROGR=" & progr & ")) " _
                       & "and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    Dim ImportoSanato As Decimal = 0
                    Dim ImportoTotaleInq As Decimal = 0
                    lettoreMorInq = par.cmd.ExecuteReader
                    While lettoreMorInq.Read
                        ImportoSanato += par.IfNull(lettoreMorInq("importo_ric_pagato_b"), 0)
                        'ImportoTotaleInq += par.IfNull(lettoreMorInq("importo_totale"), 0)
                        ImportoTotaleInq += par.IfNull(lettoreMorInq("importo_ric_b"), 0)
                    End While
                    lettoreMorInq.Close()
                    par.cmd.CommandText = "SELECT COUNT(DISTINCT ID_CONTRATTO) " _
                    & "FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.DATA_eMISSIONE<='" & dataestrazione & "' AND " _
                    & "ID_RATEIZZAZIONE IS NOT NULL " _
                    & "AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA WHERE PROGR=" & progr & "))" _
                    & "and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    Dim debitoDilazionato As String = ""
                    lettoreMorInq = par.cmd.ExecuteReader
                    If lettoreMorInq.Read Then
                        If par.IfNull(lettoreMorInq(0), 0) = 0 Then
                            debitoDilazionato = "No"
                        Else
                            debitoDilazionato = "Sì"
                        End If
                    End If
                    lettoreMorInq.Close()
                    par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO_TOTALE,0)) " _
                    & "FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.DATA_eMISSIONE<='" & dataestrazione & "' AND " _
                    & "ID_RATEIZZAZIONE IS NOT NULL " _
                    & "AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                    & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA WHERE PROGR=" & progr & "))" _
                    & "and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    Dim debitoDilazione As Decimal = 0
                    lettoreMorInq = par.cmd.ExecuteReader
                    If lettoreMorInq.Read Then
                        debitoDilazione = par.IfNull(lettoreMorInq(0), 0)
                    End If
                    lettoreMorInq.Close()
                    Dim importoTotaleIncassatoInq As Decimal = 0
                    par.cmd.CommandText = "SELECT (NVL(IMPORTO_PAGATO,0))," _
                        & "(SELECT MAX (NVL (IMPORTO_PAGATO, 0))  FROM SISCOM_MI.BOL_BOLLETTE_PAGAMENTI " _
                        & "WHERE DATA_PAGAMENTO<='" & dataestrazione & "' AND  ID_BOLLETTA(+) = BOL_BOLLETTE.ID)  " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_RATEIZZAZIONE IS NOT NULL " _
                        & "AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE " _
                        & "WHERE ID_MOROSITA=(SELECT ID FROM SISCOM_MI.MOROSITA WHERE PROGR=" & progr & "))" _
                        & "and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    lettoreMorInq = par.cmd.ExecuteReader
                    While lettoreMorInq.Read
                        importoTotaleIncassatoInq += par.IfNull(lettoreMorInq(0), 1)
                    End While
                    lettoreMorInq.Close()
                    Dim nBolletteEmesseInq As Integer = 0
                    par.cmd.CommandText = "SELECT COUNT(*) " _
                        & " FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT " _
                        & " WHERE BOL_BOLLETTE.DATA_eMISSIONE<='" & dataestrazione & "' AND " _
                        & " BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE " _
                        & " AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL " _
                        & " AND RIF_BOLLETTINO IN (SELECT BOLLETTINO " _
                        & " FROM SISCOM_MI.MOROSITA_LETTERE " _
                        & " WHERE ID_MOROSITA = (SELECT ID " _
                        & " FROM SISCOM_MI.MOROSITA " _
                        & " WHERE PROGR = " & progr & ")) " _
                        & " AND ID_BOLLETTA IS NOT NULL " _
                        & " and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    lettoreMorInq = par.cmd.ExecuteReader
                    If lettoreMorInq.Read Then
                        nBolletteEmesseInq = par.IfNull(lettoreMorInq(0), 0)
                    End If
                    lettoreMorInq.Close()
                    Dim nBolletteSaldateInq As Integer = 0
                    par.cmd.CommandText = "SELECT COUNT(*) " _
                        & " FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT " _
                        & " WHERE BOL_BOLLETTE.DATA_eMISSIONE<='" & dataestrazione & "' AND " _
                        & " BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE " _
                        & " AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL " _
                        & " AND RIF_BOLLETTINO IN (SELECT BOLLETTINO " _
                        & " FROM SISCOM_MI.MOROSITA_LETTERE " _
                        & " WHERE ID_MOROSITA = (SELECT ID " _
                        & " FROM SISCOM_MI.MOROSITA " _
                        & " WHERE PROGR = " & progr & ")) " _
                        & " AND ID_BOLLETTA IS NOT NULL " _
                        & " AND IMPORTO_PAGATO>0 " _
                        & " and id_contratto=" & par.IfNull(LettoreContratti(0), 0)
                    lettoreMorInq = par.cmd.ExecuteReader
                    If lettoreMorInq.Read Then
                        nBolletteSaldateInq = par.IfNull(lettoreMorInq(0), 0)
                    End If
                    lettoreMorInq.Close()
                    If Request.QueryString("t") = 2 Then
                        If primo = True Then
                            primo = False
                            dettagliInquilini &= "<tr> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' colspan='2' class='style7'> " _
                                & " Inquilino / Codice Contratto " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Importo della M.M. " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Pagato Bollettino messa in mora (S/N) " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Importo incassato " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Ridilazionato il debito (S/N) " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Importo dilazionato " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Numero bollettini emessi " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Numero bollettini saldati " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " Importo incassato " _
                                & " </td> " _
                                & " </tr> " _
                                & " <tr> " _
                                & " <td style='text-align:left;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' colspan='2' class='style7'> " _
                                & " <table width='100%' ><tr><td style='width:50%;text-align:left;' class='style7'>" & par.IfNull(LettoreContratti("INTESTATARIO"), "") & "</td><td class='style7' style='width:50%'>" & par.IfNull(LettoreContratti("cod_contratto"), "") & "</td></tr></table> " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & Format(ImportoTotaleInq, "##,#0.00") & " " _
                                & " </td> " _
                                & " <td style='text-align:center;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & numeroMesseinMoraInq & " " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & "  " & Format(ImportoSanato, "##,#0.00") & " " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & debitoDilazionato & " " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & Format(debitoDilazione, "##,#0.00") & " " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                 & " " & nBolletteEmesseInq & " " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & nBolletteSaldateInq & " " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & Format(importoTotaleIncassatoInq, "##,0.00") & " " _
                                & " </td> " _
                                & " </tr> "
                        Else
                            dettagliInquilini &= " <tr> " _
                                & " <td style='text-align:left;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' colspan='2' class='style7'> " _
                                & " <table width='100%' ><tr><td style='width:50%;text-align:left;' class='style7'>" & par.IfNull(LettoreContratti("INTESTATARIO"), "") & "</td><td class='style7' style='width:50%'>" & par.IfNull(LettoreContratti("cod_contratto"), "") & "</td></tr></table> " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & Format(ImportoTotaleInq, "##,#0.00") & " " _
                                & " </td> " _
                                & " <td style='text-align:center;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & numeroMesseinMoraInq & " " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & "  " & Format(ImportoSanato, "##,#0.00") & " " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & debitoDilazionato & " " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & Format(debitoDilazione, "##,#0.00") & " " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & nBolletteEmesseInq & " " _
                                & " </td> " _
                                & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & nBolletteSaldateInq & " " _
                                & " </td> " _
                                & " <td style='text-align:right;width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                                & " " & Format(importoTotaleIncassatoInq, "##,0.00") & " " _
                                & " </td> " _
                                & " </tr> "
                        End If
                    End If
                End While
                If dettagliInquilini <> "" Then
                    dettagliInquilini = " <tr> " _
                            & " <td colspan='10' style='font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                            & " &nbsp; " _
                            & " </td> " _
                            & " </tr> " _
                            & "<tr> " _
                            & " <td colspan='10' style='font-family: Arial;font-weight: bold;color: #000000;font-size: 8pt;width: 100%;height: 40px;text-align: center;background-color: #D9D9D9;' class='style5'> " _
                            & " Dettaglio inquilini " _
                            & " </td> " _
                            & " </tr> " _
                            & " <tr> " _
                            & " <td colspan='2' style='color: #000000; font-size: 8pt;font-family: Arial; font-weight: bold; text-align: center; height: 30px; background-color: #D9D9D9;' class='style6'> " _
                            & " Dati inquilino " _
                            & " </td> " _
                            & " <td style='color: #000000; font-size: 8pt;font-family: Arial; font-weight: bold; text-align: center; height: 30px; background-color: #D9D9D9;' class='style6'> " _
                            & " Importo M.M. " _
                            & " </td> " _
                            & " <td style='color: #000000; font-size: 8pt;font-family: Arial; font-weight: bold; text-align: center; height: 30px; background-color: #D9D9D9;' colspan='2' class='style6'> " _
                            & " Posizioni sanate " _
                            & " </td> " _
                            & " <td style='color: #000000; font-size: 8pt;font-family: Arial; font-weight: bold; text-align: center; height: 30px; background-color: #D9D9D9;' colspan='5' class='style6'> " _
                            & " Rateizzazione del debito " _
                            & " </td> " _
                            & " </tr>" _
                            & dettagliInquilini
                End If
                LettoreContratti.Close()
                riepilogoGenerale = "<table border='1' cellpadding='0' cellspacing='0' width='100%'> <tr><td colspan='10' style='border:0px;'>&nbsp;</td></tr>" _
                        & " <tr> " _
                        & " <td colspan='10' style='background-color: #507CD1;font-family: Arial;font-weight: bold;color: #FFFFFF; width: 100%;height: 40px;text-align: center; '  " _
                        & " class='style2'> " _
                        & " Riepilogo Generale " _
                        & " </td> " _
                        & " </tr> " _
                        & " <tr> " _
                        & " <td colspan='10' style='font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " &nbsp; " _
                        & " </td> " _
                        & " </tr> " _
                        & " <tr> " _
                        & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                        & " Numero M.M. inviate " _
                        & " </td> " _
                        & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                        & " Data invio " _
                        & " </td> " _
                        & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                        & " Importo M.M. " _
                        & " </td> " _
                        & " <td colspan='2' style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                        & " Posizioni sanate " _
                        & " </td> " _
                        & " <td colspan='5' style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                        & " Rateizzazione del debito " _
                        & " </td> " _
                        & " </tr> " _
                        & " <tr> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Numero lettere M.M. inviate per protocollo " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Data invio M.M. " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Importo totale della M.M. " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Numero inquilini che hanno pagato il Bollettino della M.M. " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Importo totale incassato " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Inquilini che hanno ridilazionato il debito " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Importo totale " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Numero bollettini emessi " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Numero bollettini saldati " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " Importo totale incassato " _
                        & " </td> " _
                        & " </tr> " _
                        & " <tr> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & numeroMMinviate & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " &nbsp;</td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & Format(importoTotaleMM, "##,#0.00") & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & numeroInquiliniBollettinoMM & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & Format(importoTotaleIncassatoMM, "##,#0.00") & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & numeroInquiliniDilazioneDebito & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & Format(ImportoTotaleDilazione, "##,#0.00") & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & numeroBollettiniEmessi & " " _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & numeroBollettiniSaldati & "" _
                        & " </td> " _
                        & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                        & " " & Format(ImportoTotaleIncassatoDebito, "##,#0.00") & " " _
                        & " </td> " _
                        & " </tr> </table>"
                RiepilogoProtocolli.Text &= " <table border='1' cellpadding='0' cellspacing='0' width='100%'> " _
                    & " <tr><td colspan='10' style='border:0px;'>&nbsp;</td></tr>" _
                    & " <tr> " _
                    & " <td colspan='10' style='background-color: #507CD1;font-family: Arial;font-weight: bold;color: #FFFFFF; width: 100%;height: 40px;text-align: center; '  " _
                    & " class='style2'> " _
                    & " Protocollo Gestore " & par.IfNull(LettoreProtocolli("protocollo_aler"), "") & "- Messa in Mora n° " & par.IfNull(LettoreProtocolli("progr"), "") & " del " & par.FormattaData(LettoreProtocolli("data_protocollo")) _
                    & " </td> " _
                    & " </tr> " _
                    & " <tr> " _
                    & " <td colspan='10' style='font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " &nbsp; " _
                    & " </td> " _
                    & " </tr> " _
                    & " <tr> " _
                    & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                    & " Numero M.M. inviate " _
                    & " </td> " _
                    & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                    & " Data invio " _
                    & " </td> " _
                    & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' class='style3'> " _
                    & " Importo M.M. " _
                    & " </td> " _
                    & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' colspan='2' class='style3'> " _
                    & " Posizioni sanate " _
                    & " </td> " _
                    & " <td style='color: #000000; font-size: 8pt; height: 30px; background-color: #D9D9D9; font-family: Arial; text-align: center; font-weight: 700;' colspan='5' class='style3'> " _
                    & " Rateizzazione del debito " _
                    & " </td> " _
                    & " </tr> " _
                    & " <tr> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Numero lettere M.M. inviate per protocollo " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Data invio M.M. " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Importo totale della M.M. " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Numero inquilini che hanno pagato il Bollettino della M.M. " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Importo totale incassato " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Inquilini che hanno ridilazionato il debito " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Importo totale " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Numero bollettini emessi " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Numero bollettini saldati " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " Importo totale incassato " _
                    & " </td> " _
                    & " </tr> " _
                    & " <tr> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & par.IfNull(LettoreProtocolli("numero_lettere"), 0) & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " &nbsp;</td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & Format(par.IfNull(LettoreProtocolli("somma_iniziale"), 0), "##,#0.00") & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & numeroMesseinMora & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & Format(importoMesseinMora, "##,#0.00") & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & dilazioni & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & Format(ImportoTotale, "##,#0.00") & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & nBolletteEmesse & " " _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & nBolletteSaldate & "" _
                    & " </td> " _
                    & " <td style='width: 10%;font-family: Arial;text-align: center;font-size: 8pt;' class='style7'> " _
                    & " " & Format(importoTotaleIncassato, "##,#0.00") & " " _
                    & " </td> " _
                    & " </tr> " _
                    & dettagliInquilini _
                    & " </table> "
            End While
            If Not LettoreProtocolli.HasRows Then
                RiepilogoProtocolli.Text = "<b>Nessun protocollo estratto</b>"
            End If
            If Request.QueryString("t") = "1" Then
                If riepilogoGenerale <> "" Then
                    RiepilogoProtocolli.Text = riepilogoGenerale
                End If
            Else
                If riepilogoGenerale <> "" Then
                    RiepilogoProtocolli.Text &= riepilogoGenerale
                End If
            End If
            LettoreProtocolli.Close()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub
    Protected Sub ApriConnessione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
            par.cmd.Dispose()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExcel.Click
        'Dim Loading As String = "<div id=""divLoading5"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
        '   & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
        '   & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
        '   & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
        '   & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
        '   & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
        '   & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
        '   & "</td></tr></table></div></div>"
        'Response.Write(Loading)
        'Response.Flush()
        Try
            ApriConnessione()
            '#### EXPORT IN EXCEL ####
            Try
                Dim myExcelFile As New CM.ExcelFile
                Dim K As Long
                Dim sNomeFile As String
                sNomeFile = "MESSE IN MORA_" & Format(Now, "yyyyMMddHHmmss")
                With myExcelFile
                    .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                    .PrintGridLines = False
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                    .SetDefaultRowHeight(14)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                    .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
                    .SetColumnWidth(1, 21, 30)
                    K = 1
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE CONTRATTO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "INTESTATARIO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "DEBITO TOTALE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "IMPORTO EX GESTORI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "IMPORTO GESTORE PER CANONI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO GESTORE PER SERVIZI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "TIPOLOGIA RAPPORTO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "POSIZIONE CONTRATTUALE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "UNITA' IMMOBILIARE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "TIPO UI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "INDIRIZZO COMPLETO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "RICHIESTA FONDO SOCIALE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "INVIATA MESSA IN MORA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "AVVIATA PRATICA LEGALE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "PROTOCOLLO MESSA IN MORA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "DATA MESSA IN MORA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "DILAZIONE DEBITO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, "IMPORTO DILAZIONE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, "N° BOLLETTINI DILAZIONE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, "N° BOLLETTINI DILAZIONE SALDATI", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, "IMPORTO SALDATO", 0)
                    K += 1
                    For Each codiceContratto As String In ListaMM
                        par.cmd.CommandText = " SELECT DISTINCT tipo_lettera, num_lettere, " _
                            & " NVL(MOROSITA_LETTERE.IMPORTO_INIZIALE,0) AS IMPORTO_INIZIALE, " _
                            & " NVL(MOROSITA_LETTERE.IMPORTO_CANONE,0) AS IMPORTO_CANONE, " _
                            & " NVL(MOROSITA_LETTERE.IMPORTO_ONERI,0) AS IMPORTO_SERVIZI " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                            & " SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.INDIRIZZI,SISCOM_MI.MOROSITA_LETTERE, " _
                            & " SISCOM_MI.MOROSITA " _
                            & " WHERE RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO " _
                            & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA  " _
                            & " AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                            & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                            & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                            & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                            & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                            & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                            & " AND MOROSITA_LETTERE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                            & " AND MOROSITA_LETTERE.ID_MOROSITA=MOROSITA.ID " _
                            & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                            & " and RAPPORTI_UTENZA.ID =" & codiceContratto

                        Dim sommaDebitoTotale As Decimal = 0
                        Dim sommaCanoni As Decimal = 0
                        Dim sommaServizi As Decimal = 0
                        Dim sommaGlobal As Decimal = 0

                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dt As New Data.DataTable
                        da.Fill(dt)
                        Dim tipoLettera As String = ""
                        Dim numLettera As Integer = 0
                        For Each Items As Data.DataRow In dt.Rows
                            tipoLettera = Items.Item(0)
                            numLettera = Items.Item(1)
                            If numLettera = 1 Then
                                Select Case tipoLettera
                                    Case "AB"
                                        sommaGlobal += Items.Item(2)
                                    Case "CD"
                                        sommaCanoni += Items.Item(3)
                                        sommaServizi += Items.Item(4)
                                    Case "EF"
                                        sommaGlobal += Items.Item(2)
                                    Case Else
                                End Select
                            ElseIf numLettera = 2 Then
                                Select Case tipoLettera
                                    Case "AB"
                                        sommaCanoni += Items.Item(3)
                                        sommaServizi += Items.Item(4)
                                    Case "CD"
                                        sommaCanoni += Items.Item(3)
                                        sommaServizi += Items.Item(4)
                                    Case "EF"
                                        sommaGlobal += Items.Item(2)
                                    Case Else
                                End Select
                            End If

                        Next
                        sommaDebitoTotale += sommaGlobal + sommaCanoni + sommaServizi
                        par.cmd.CommandText = " SELECT DISTINCT " _
                            & " RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,  " _
                            & " CASE WHEN ANAGRAFICA.RAGIONE_SOCIALE IS NOT NULL THEN TRIM (RAGIONE_SOCIALE) ELSE RTRIM (LTRIM (COGNOME || ' ' || ANAGRAFICA.NOME)) END AS INTESTATARIO, " _
                            & " TRIM (RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) AS TIPOLOGIA_RAPPORTO, SUBSTR (TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE, 1, 25) AS POSIZIONE_CONTRATTUALE, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, TIPOLOGIA_UNITA_IMMOBILIARI.COD,  " _
                            & " TRIM (INDIRIZZI.DESCRIZIONE) || ' ' || TRIM (INDIRIZZI.CIVICO) || ' ' || (SELECT TRIM (NOME) FROM SEPA.COMUNI_NAZIONI WHERE COD = INDIRIZZI.COD_COMUNE) AS INDIRIZZO, 'NO' AS RICHIESTA_FONDO_SOCIALE, " _
                            & " (CASE WHEN ( (SELECT COUNT (ID) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO = RAPPORTI_UTENZA.ID AND COD_STATO = 'M00') > 0) THEN ('SI''') ELSE ('NO') END) AS INVIATA_MM, " _
                            & " (CASE WHEN ( (SELECT COUNT (ID) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO = RAPPORTI_UTENZA.ID AND COD_STATO = 'M20') > 0) THEN ('SI''') ELSE ('NO') END) AS AVVIATA_PRATICA_LEGALE, " _
                            & " MOROSITA.PROTOCOLLO_ALER,to_char(to_date(MOROSITA.DATA_CREAZIONE,'yyyyMMdd'),'dd/MM/yyyy') as data_creazione, " _
                            & " (SELECT COUNT(DISTINCT RAPPORTI_UTENZA.ID) FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "' AND ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID)) AS DILAZIONE, " _
                            & " CASE WHEN (SELECT SUM(NVL(IMPORTO_TOTALE,0)) FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "' AND ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID)) IS NULL THEN 0 ELSE (SELECT SUM(NVL(IMPORTO_TOTALE,0)) FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "' AND ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID)) END AS IMPORTO_DILAZIONE, " _
                            & " (SELECT COUNT(*) FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID) AND ID_BOLLETTA IS NOT NULL AND BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "') AS BOLLETTINI_EMESSI, " _
                            & " (SELECT COUNT(*) FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE BOL_BOLLETTE.IMPORTO_PAGATO=BOL_BOLLETTE.IMPORTO_TOTALE AND BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID) AND ID_BOLLETTA IS NOT NULL AND BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "') AS BOLLETTINI_SALDATI, " _
                            & " CASE WHEN (SELECT SUM(NVL(IMPORTO_PAGATO,0)) FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE BOL_BOLLETTE.IMPORTO_PAGATO=BOL_BOLLETTE.IMPORTO_TOTALE AND BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID) AND ID_BOLLETTA IS NOT NULL AND BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "') IS NULL THEN 0 ELSE (SELECT SUM(NVL(IMPORTO_PAGATO,0)) FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE BOL_BOLLETTE.IMPORTO_PAGATO=BOL_BOLLETTE.IMPORTO_TOTALE AND BOL_BOLLETTE.ID_RATEIZZAZIONE = BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NOT NULL AND RIF_BOLLETTINO IN (SELECT BOLLETTINO FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID) AND ID_BOLLETTA IS NOT NULL AND BOL_BOLLETTE.DATA_EMISSIONE<='" & dataestrazione & "') END AS IMPORTO_SALDATO " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                            & " SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.INDIRIZZI,SISCOM_MI.MOROSITA_LETTERE, " _
                            & " SISCOM_MI.MOROSITA " _
                            & " WHERE RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO " _
                            & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA  " _
                            & " AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                            & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                            & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                            & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                            & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                            & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                            & " AND MOROSITA_LETTERE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                            & " AND MOROSITA_LETTERE.ID_MOROSITA=MOROSITA.ID " _
                            & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                            & " and RAPPORTI_UTENZA.ID =" & codiceContratto

                        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim index As Integer = 1
                        Dim dilazione As String = "No"
                        If Lettore.Read Then
                            If Lettore("dilazione") = 0 Then
                                dilazione = "No"
                            Else
                                dilazione = "Sì"
                            End If
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(Lettore("codice_contratto"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(Lettore("intestatario"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, sommaDebitoTotale, 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, sommaGlobal, 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, sommaCanoni, 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, sommaServizi, 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(Lettore("TIPOLOGIA_RAPPORTO"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(Lettore("posizione_contrattuale"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(Lettore("cod_unita_immobiliare"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(Lettore("cod"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(Lettore("indirizzo"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(Lettore("richiesta_fondo_sociale"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(Lettore("inviata_mm"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(Lettore("avviata_pratica_legale"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(Lettore("protocollo_aler"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(Lettore("data_Creazione"), ""), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, dilazione, 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.IfNull(Lettore("importo_dilazione"), ""), 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.IfNull(Lettore("bollettini_emessi"), ""), 1)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.IfNull(Lettore("bollettini_Saldati"), ""), 1)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.IfNull(Lettore("importo_saldato"), ""), 4)
                            K += 1
                        End If
                        Lettore.Close()
                    Next

                    .CloseFile()
                End With
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim sFile As String = Path.GetFileName(strFile)
                Dim theEntry As ZipEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                File.Delete(strFile)
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "loading", "document.getElementById('divLoading5').style.visibility = 'hidden';", True)
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "var larghezza=Math.floor(screen.width/2)-100;var altezza=Math.floor(screen.height/2)-50;window.open('..\/FileTemp/\" & sNomeFile & ".zip','_blank','top='+altezza+',left='+larghezza+',width=100,height=50,resizable=0');", True)
                Response.Redirect("..\/FileTemp/\" & sNomeFile & ".zip", True)
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "window.open('..\/FileTemp/\" & sNomeFile & ".zip','_self','');", True)
            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "alert('" & ex.Message & "');", True)
            End Try
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub

    Protected Sub btnIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "location.href='RicercaDebitoriMultiSelezioneMor.aspx';", True)
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Try
            'RENDERCONTROL DEL DATAGRID
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            stringWriter = New System.IO.StringWriter
            sourcecode = New HtmlTextWriter(stringWriter)
            RiepilogoProtocolli.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = "<table width=""100%""><tr><td style=""height: 17.25pt; width: 100%; color: #262626; font-size: 11.0pt; font-weight: 700;" _
                    & "font-style: italic; text-decoration: none; font-family: Book Antiqua , serif; " _
                    & "text-align: left; vertical-align: bottom; white-space: normal; border-left: 1.0pt solid windowtext; " _
                    & "border-right-style: none; border-right-color: inherit; border-right-width: medium; " _
                    & "border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px; " _
                    & "padding-right: 1px; padding-top: 1px; background: #D9D9D9;"">" & parametriDiRicerca.Text & "</td></tr>" _
                    & "</table>" & Html & stringWriter.ToString
            Dim url As String = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = System.Web.HttpContext.Current.Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PageWidth = 1200
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 40
            pdfConverter1.PdfHeaderOptions.HeaderText = UCase("Report situazione messe in mora")
            'pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ()
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10
            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Black
            'pdfConverter1.PdfFooterOptions.FooterText = UCase(footer)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            Dim nomefile As String = "StampaMesseInMora" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "windowopen", "window.open('..\/FileTemp/\" & nomefile & "','','')", True)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('" & ex.Message & "');", True)
        End Try
    End Sub
End Class
