
Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_ImportaODL
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            caricaODL()
        End If
    End Sub
    Private Sub caricaODL()
        Try
            connData.apri(True)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.IMPORTA_ODL WHERE FL_IMPORTATO=0 AND NVL(SAL,0)=0"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
            Dim idPagamento As String = par.IfNull(par.cmd.ExecuteScalar, "")

            Dim nrInseriti As Integer = 0
            Dim progr As String = ""
            Dim reg As String = ""
            Dim verbale As String = ""
            Dim cliente As String = ""
            Dim manutentore As String = ""
            Dim matricola As String = ""
            Dim tc As String = ""
            Dim intervento As String = ""
            Dim eff As String = ""
            Dim tipo As String = ""
            Dim esecutore As String = ""
            Dim amministratore As String = ""
            Dim importo As String = ""
            Dim n As String = ""
            Dim odl As String = ""
            Dim stato As String = ""
            Dim sal As String = ""
            Dim fl_importato As String = ""
            Dim idEdificio As String = ""
            Dim idManutenzione As String = ""
            Dim idPfVociImporto As String = ""
            Dim descrizione As String = ""
            Dim idAppalto As String = ""
            Dim idPianoFinanziario As String = ""
            Dim idPrenotazionePagamento As String = ""
            Dim ritLegge As String = ""
            Dim importoTot As String = ""
            Dim importoResiduo As String = ""
            Dim ivaConsumo As String = ""
            Dim idStruttura As String = ""
            Dim idIntervento As String = ""
            Dim idImpianto As String = ""
            Dim idPrenotazione As String = ""
            Dim idFornitore As String = ""
            Dim idPfVoce As String = ""

            Dim anno As String = ""
            Dim importoTotale As Decimal = 0
            For Each elemento As Data.DataRow In dt.Rows

                progr = par.IfNull(elemento.Item("PROGR"), "")
                reg = par.FormatoDataDB(par.IfNull(elemento.Item("REG"), ""))
                verbale = par.IfNull(elemento.Item("VERBALE"), "")
                cliente = par.IfNull(elemento.Item("CLIENTE"), "")
                manutentore = par.IfNull(elemento.Item("MANUTENTORE"), "")
                matricola = par.IfNull(elemento.Item("MATRICOLA"), "")
                tc = par.IfNull(elemento.Item("TC"), "")
                intervento = par.IfNull(elemento.Item("INTERVENTO"), "")
                eff = par.FormatoDataDB(par.IfNull(elemento.Item("EFF"), ""))
                tipo = par.IfNull(elemento.Item("TIPO"), "")
                esecutore = par.IfNull(elemento.Item("ESECUTORE"), "")
                amministratore = par.IfNull(elemento.Item("AMMINISTRATORE"), "")
                importo = par.IfNull(elemento.Item("IMPORTO"), "")
                n = par.IfNull(elemento.Item("N"), "")
                odl = par.IfNull(elemento.Item("ODL"), "")
                stato = par.IfNull(elemento.Item("STATO"), "")
                sal = par.IfNull(elemento.Item("SAL"), "")
                fl_importato = par.IfNull(elemento.Item("FL_IMPORTATO"), "")

                par.cmd.CommandText = "SELECT DISTINCT ID_EDIFICIO FROM SISCOM_MI.IMPIANTI_UI WHERE ID_IMPIANTO IN (SELECT ID FROM SISCOM_MI.I_SOLLEVAMENTO WHERE MATRICOLA='" & matricola & "')"
                idEdificio = "NULL"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idEdificio = par.IfNull(lettore(0), "NULL")
                End If
                lettore.Close()

                If idEdificio <> "NULL" And importo <> "" Then
                    nrInseriti += 1
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MANUTENZIONI.NEXTVAL FROM DUAL"
                    idManutenzione = par.IfNull(par.cmd.ExecuteScalar, "")

                    descrizione = ""
                    idPianoFinanziario = ""
                    idPrenotazionePagamento = ""
                    ritLegge = ""
                    importoTot = ""
                    importoResiduo = "NULL"
                    ivaConsumo = ""
                    idStruttura = ""
                    idFornitore = ""
                    idPfVoce = ""
                    anno = ""
                    If importo = "130" Then
                        idPfVociImporto = "7988"
                        descrizione = "Verifica biennale imp. ascensore Matricola " & matricola & "."
                        idAppalto = "2015"
                        idPianoFinanziario = "5"
                        ritLegge = "0.17"
                        importoTot = "34.26"
                        ivaConsumo = "22"
                        idStruttura = "105"
                        idFornitore = "1268"
                        idPfVoce = "2120"
                        anno = "2015"
                    Else
                        idPfVociImporto = "7993"
                        descrizione = "Verifica straodinaria imp. ascensore Matricola " & matricola & "."
                        idAppalto = "2015"
                        idPianoFinanziario = "5"
                        ritLegge = "0.22"
                        importoTot = "44.8"
                        ivaConsumo = "22"
                        idStruttura = "105"
                        idFornitore = "1268"
                        idPfVoce = "2132"
                        anno = "2015"
                    End If

                    importoTotale += CDec(importo) - CDec(ritLegge)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.MANUTENZIONI (" _
                        & " ID, STATO, ODL, " _
                        & " ANNO, ID_COMPLESSO, ID_EDIFICIO, " _
                        & " ID_SCALA, ID_SERVIZIO, ID_PF_VOCE_IMPORTO, " _
                        & " DESCRIZIONE, DATA_INIZIO_ORDINE, DATA_FINE_ORDINE, " _
                        & " IMPORTO_PRESUNTO, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, " _
                        & " IMPORTO_CONSUNTIVATO, ID_APPALTO, ID_SEGNALAZIONI, " _
                        & " ID_PAGAMENTO, ID_PIANO_FINANZIARIO, ID_FIGLIO, " _
                        & " ID_PRENOTAZIONE_PAGAMENTO, RIT_LEGGE, RIMBORSI, " _
                        & " IMPORTO_TOT, IMPORTO_RESIDUO, NOTE, " _
                        & " ID_UNITA_IMMOBILIARI, DANNEGGIANTE, DANNEGGIATO, " _
                        & " IVA_CONSUMO, ID_PF_VOCE, ID_STRUTTURA, " _
                        & " IMPORTO_ONERI_PREV, IMPORTO_ONERI_CONS, PROGR, " _
                        & " IVA_CONSUMO_P, DATA_INIZIO_ORDINE_TMP, DATA_ANNULLO, " _
                        & " FL_AUTORIZZAZIONE, OPERATORE_AUTORIZZAZIONE, DATA_PGI, " _
                        & " DATA_TDL) " _
                        & " VALUES (" & idManutenzione & "," _
                        & " 4," _
                        & " NULL," _
                        & Left(reg, 4) & "," _
                        & " NULL," _
                        & idEdificio & "," _
                        & " NULL," _
                        & " 4," _
                        & idPfVociImporto & " ," _
                        & "'" & descrizione & "'," _
                        & "'" & reg & "'," _
                        & "'" & reg & "'," _
                        & importo & "," _
                        & "'" & eff & "'," _
                        & "'" & eff & "'," _
                        & importo & "," _
                        & idAppalto & "," _
                        & " NULL," _
                        & " NULL," _
                        & idPianoFinanziario & "," _
                        & " NULL," _
                        & " NULL," _
                        & ritLegge & "," _
                        & "0," _
                        & importoTot & "," _
                        & importoResiduo & "," _
                        & "''," _
                        & " NULL," _
                        & " NULL," _
                        & " NULL," _
                        & ivaConsumo & "," _
                        & " NULL," _
                        & idStruttura & "," _
                        & " NULL," _
                        & " 0," _
                        & " NULL," _
                        & " 22," _
                        & " NULL," _
                        & " NULL," _
                        & " 1," _
                        & " 1616," _
                        & " NULL," _
                        & " NULL" _
                        & " )"
                    par.cmd.ExecuteNonQuery()



                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL FROM DUAL"
                    idIntervento = par.IfNull(par.cmd.ExecuteScalar, "")

                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.I_SOLLEVAMENTO WHERE MATRICOLA='" & matricola & "'"
                    idImpianto = par.IfNull(par.cmd.ExecuteScalar, "")

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.MANUTENZIONI_INTERVENTI (" _
                        & " ID, ID_MANUTENZIONE, TIPOLOGIA, " _
                        & " ID_IMPIANTO, ID_COMPLESSO, ID_EDIFICIO, " _
                        & " ID_UNITA_IMMOBILIARE, ID_UNITA_COMUNE, IMPORTO_PRESUNTO, " _
                        & " FL_BLOCCATO) " _
                        & " VALUES (" & idIntervento & "," _
                        & idManutenzione & " ," _
                        & " 'SOLLEVAMENTO'," _
                        & idImpianto & "," _
                        & " NULL," _
                        & " NULL," _
                        & " NULL," _
                        & " NULL," _
                        & importo & "," _
                        & " 0 )"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.MANUTENZIONI_CONSUNTIVI (" _
                        & " ID, ID_MANUTENZIONI_INTERVENTI, COD_ARTICOLO, " _
                        & " DESCRIZIONE, ID_UM, QUANTITA, " _
                        & " PREZZO_UNITARIO, PREZZO_TOTALE) " _
                        & " VALUES (SISCOM_MI.SEQ_MANUTENZIONI_CONSUNTIVI.NEXTVAL," _
                        & idIntervento & " ," _
                        & " '-'," _
                        & "'" & descrizione & "'," _
                        & " 4," _
                        & " 1," _
                        & importo & "," _
                        & importo & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL"
                    idPrenotazione = par.IfNull(par.cmd.ExecuteScalar, "")

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (" _
                        & " ID, ID_FORNITORE, ID_APPALTO, " _
                        & " ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO, " _
                        & " ID_PAGAMENTO, TIPO_PAGAMENTO, DESCRIZIONE, " _
                        & " DATA_PRENOTAZIONE, IMPORTO_PRENOTATO, IMPORTO_APPROVATO, " _
                        & " PROGR_FORNITORE, ANNO, DATA_SCADENZA, " _
                        & " DATA_STAMPA, ID_STRUTTURA, RIT_LEGGE_IVATA, " _
                        & " PERC_IVA, ID_PAGAMENTO_RIT_LEGGE, DATA_PRENOTAZIONE_TMP, " _
                        & " DATA_CONSUNTIVAZIONE, DATA_CERTIFICAZIONE, DATA_CERT_RIT_LEGGE, " _
                        & " IMPORTO_LIQUIDATO, DATA_ANNULLO, IMPORTO_RIT_LIQUIDATO, " _
                        & " FUORI_CAMPO_IVA, IMPONIBILE, IVA) " _
                        & " VALUES (" & idPrenotazione & "," _
                        & idFornitore & "," _
                        & idAppalto & "," _
                        & idPfVoce & "," _
                        & idPfVociImporto & "," _
                        & "2," _
                        & "NULL," _
                        & "3," _
                        & "'" & descrizione & "'," _
                        & "'" & eff & "'," _
                        & importo & "," _
                        & importo & "," _
                        & " NULL," _
                        & "'" & anno & "'," _
                        & " NULL," _
                        & " NULL," _
                        & idStruttura & "," _
                        & ritLegge & "," _
                        & " 22," _
                        & " NULL," _
                        & " '00000000'," _
                        & "'" & eff & "'," _
                        & "'" & eff & "'," _
                        & " NULL," _
                        & " NULL," _
                        & " NULL," _
                        & " NULL," _
                        & " 0," _
                        & " NULL," _
                        & " NULL)"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI SET ID_PRENOTAZIONE_PAGAMENTO=" & idPrenotazione & " WHERE ID=" & idManutenzione
                    par.cmd.ExecuteNonQuery()
                    Dim ris As Integer = 0
                    par.cmd.CommandText = "UPDATE SISCOM_MI.IMPORTA_ODL SET SAL=1 WHERE PROGR='" & progr & "'"
                    ris = par.cmd.ExecuteNonQuery()
                    If ris = 0 Then
                        Beep()
                    End If
                End If
            Next
            If nrInseriti > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (" _
                    & " ID, ANNO, PROGR, " _
                    & " DATA_EMISSIONE, DATA_STAMPA, DESCRIZIONE, " _
                    & " IMPORTO_CONSUNTIVATO, ID_FORNITORE, ID_APPALTO, " _
                    & " ID_STATO, DATA_MANDATO, NUMERO_MANDATO, " _
                    & " TIPO_PAGAMENTO, PROGR_FORNITORE, PROGR_APPALTO, " _
                    & " STATO_FIRMA, RIT_LEGGE, CONTO_CORRENTE, " _
                    & " DATA_EMISSIONE_TMP, DATA_SAL, ID_IBAN_FORNITORE, " _
                    & " DATA_EMISSIONE_PAGAMENTO, DATA_AGGIORNAMENTO, ID_TIPO_MODALITA_PAG, " _
                    & " ID_TIPO_PAGAMENTO, DATA_SCADENZA, DESCRIZIONE_BREVE, " _
                    & " DATA_TRASMISSIONE, ORA_TRASMISSIONE) " _
                    & " VALUES (" & idPagamento & "," _
                    & "'" & anno & "'," _
                    & " NULL," _
                    & "'" & eff & "'," _
                    & "'" & eff & "'," _
                    & " NULL," _
                    & par.VirgoleInPunti(importoTotale) & "," _
                    & idFornitore & "," _
                    & idAppalto & "," _
                    & " 1," _
                    & " NULL," _
                    & " NULL," _
                    & " 3," _
                    & " NULL," _
                    & " NULL," _
                    & " 0," _
                    & " 0," _
                    & " NULL," _
                    & " NULL," _
                    & "'" & eff & "'," _
                    & " NULL," _
                    & "'" & eff & "'," _
                    & "'" & eff & "'," _
                    & " 2," _
                    & " 9," _
                    & " NULL," _
                    & " NULL," _
                    & " NULL," _
                    & " NULL)"
                par.cmd.ExecuteNonQuery()
                Dim ris As Integer = 0
                par.cmd.CommandText = "UPDATE SISCOM_MI.IMPORTA_ODL SET FL_IMPORTATO=1 WHERE FL_IMPORTATO=0 AND SAL=1"
                ris = par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET ID_PAGAMENTO=" & idPagamento & " WHERE ID_PAGAMENTO IS NULL AND DATA_PRENOTAZIONE_TMP='00000000'"
                ris = par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET DATA_PRENOTAZIONE_TMP=NULL WHERE DATA_PRENOTAZIONE_TMP='00000000'"
                ris = par.cmd.ExecuteNonQuery()

            End If
            Response.Write("<script>alert('ok');</script>")
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Response.Write("<script>alert('errore');</script>")
        End Try
    End Sub
End Class
