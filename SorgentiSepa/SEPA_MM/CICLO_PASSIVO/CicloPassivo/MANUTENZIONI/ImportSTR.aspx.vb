Imports Telerik.Web.UI
Imports System.IO
Imports System.Math
Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_ImportSTR
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_CONSUNTIVAZIONE_STR") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
        End If
    End Sub
    Protected Sub btnCaricaFile_Click(sender As Object, e As System.EventArgs) Handles btnCaricaFile.Click
        Try
            Dim nFile As String = ""
            If Not Directory.Exists(Server.MapPath("..\..\..\ALLEGATI\STR")) Then
                Directory.CreateDirectory(Server.MapPath("..\..\..\ALLEGATI\STR"))
            End If
            Dim fileName As String = ""
            connData.apri(True)
            Dim errore As Integer = 0
            Dim i As Integer = 0
            Dim orarioOperazione As String = Format(Now, "yyyyMMddHHmmss")
            For Each file As UploadedFile In rdpUpload.UploadedFiles
                fileName = file.GetName()
                nFile = file.GetNameWithoutExtension() & "_" & orarioOperazione & file.GetExtension
                nFile = Server.MapPath("..\..\..\ALLEGATI\STR\") & nFile
                file.SaveAs(nFile)
                Using pck As New OfficeOpenXml.ExcelPackage()
                    Using stream = IO.File.Open(nFile, FileMode.Open)
                        pck.Load(stream)
                    End Using
                    Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(4)
                    Dim xls As New ExcelSiSol
                    Dim dtFoglio As New Data.DataTable
                    dtFoglio = xls.WorksheetToDataTable(ws, True)

                    Dim contatoreCodiceProgetto As Integer = 0
                    For Each riga As Data.DataRow In dtFoglio.Rows
                        If riga.Item(0).ToString = "" Then
                            contatoreCodiceProgetto += 1
                            riga.Item(4) = "#"
                        Else
                            contatoreCodiceProgetto = 0
                        End If
                        If contatoreCodiceProgetto >= 3 Then
                            riga.Item(4) = "#"
                        End If
                    Next
                    dtFoglio.AcceptChanges()
                    Dim dataView As New Data.DataView(dtFoglio)
                    dataView.Sort = "Codice ODL ASC"
                    dtFoglio = dataView.ToTable
                    Dim ordinePrecedente As String = ""
                    Dim oneriSicurezza As Decimal = 0
                    Dim aLordoEsclusiOneri As Decimal = 0
                    Dim ribassoDasta As Decimal = 0
                    Dim aNettoEsclusiOneri As Decimal = 0
                    Dim aNettoCompresiOneri As Decimal = 0
                    Dim iva As Decimal = 0
                    Dim aNettoCompresiOneriEiva As Decimal = 0
                    Dim ritenutaLegge As Decimal = 0
                    Dim prezzoTot As Decimal = 0

                    Dim oneriSicurezzaT As Decimal = 0
                    Dim aLordoEsclusiOneriT As Decimal = 0
                    Dim ribassoDastaT As Decimal = 0
                    Dim aNettoEsclusiOneriT As Decimal = 0
                    Dim aNettoCompresiOneriT As Decimal = 0
                    Dim ivaT As Decimal = 0
                    Dim aNettoCompresiOneriEivaT As Decimal = 0
                    Dim ritenutaLeggeT As Decimal = 0
                    Dim prezzoTotT As Decimal = 0

                    Dim idMan As Integer = 0

                    i = 0
                    'IMPOSTATA FUORI DAL CICLO PER L'ULTIMO UPDATE DI PRENOTAZIONI E MANUTENZIONI
                    Dim idPfVoceImporto As Integer
                    'Elimino tutte le consuntivazioni per gli ODL in oggetto
                    For Each riga As Data.DataRow In dtFoglio.Rows
                        If riga.Item(4) <> "#" Then
                            Dim odl As String = riga(4).ToString
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE ID_MANUTENZIONI_INTERVENTI IN (SELECT ID FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_MANUTENZIONE=(SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE PROGR||'/'||ANNO='" & odl & "'))"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next



                    For Each riga As Data.DataRow In dtFoglio.Rows
                        i += 1
                        If riga.Item(4) <> "#" Then
                            Dim codiceProgettoVision As String = riga.Item(0).ToString
                            Dim numeroContratto As String = riga.Item(1).ToString()
                            Dim numeroSal As String = riga.Item(2).ToString()
                            Dim dataSal As String = riga.Item(3).ToString()
                            Dim ordine As String = riga(4).ToString
                            Dim codiceElemento As String = riga.Item(5).ToString
                            Dim codiceDGR As String = riga.Item(6).ToString
                            Dim importoStringa As String = riga.Item(7).ToString
                            Dim importoOneri As String = riga.Item(8).ToString
                            If riga.Item(3).ToString() = "" Then
                                errore = 5
                                Exit For
                            End If
                            If codiceProgettoVision <> "" _
                                And numeroContratto <> "" _
                                And numeroSal <> "" _
                                And dataSal <> "" _
                                And ordine <> "" _
                                And codiceDGR <> "" _
                                And CStr(importoStringa) <> "" Then

                                Dim importo As Decimal = CDec(importoStringa)
                                Dim importoOneriStringa As String = "0"
                                If importoOneri <> "" AndAlso CDec(importoOneri) > 0 Then
                                    importoOneriStringa = CStr(importoOneri).Replace(",", ".")
                                End If
                                If ordinePrecedente = "" Then
                                    ordinePrecedente = ordine
                                End If
                                If ordinePrecedente <> ordine Then

                                    oneriSicurezzaT = oneriSicurezza
                                    aLordoEsclusiOneriT = aLordoEsclusiOneri
                                    ribassoDastaT = ribassoDasta
                                    aNettoEsclusiOneriT = aNettoEsclusiOneri
                                    aNettoCompresiOneriT = aNettoCompresiOneri
                                    ivaT = iva
                                    aNettoCompresiOneriEivaT = aNettoCompresiOneriEiva
                                    ritenutaLeggeT = ritenutaLegge
                                    prezzoTotT = prezzoTot

                                    Dim oneriImportati As String = "IMPORTO_ONERI_CONS=0,"
                                    par.cmd.CommandText = "select nvl(SUM(oneri_sic),-1) from siscom_mi.import_str where codice_odl=(select progr||'/'||anno from siscom_mi.manutenzioni where id=" & idMan & ") " _
                                        & " and DATA_ORA_OPERAZIONE = (select max(DATA_ORA_OPERAZIONE) from siscom_mi.import_str where codice_odl=(select progr||'/'||anno from siscom_mi.manutenzioni where id=" & idMan & "))"
                                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If lettore.Read Then
                                        If lettore(0) <> -1 Then
                                            oneriImportati = "IMPORTO_ONERI_CONS=" & par.VirgoleInPunti(lettore(0)) & ","
                                        End If
                                    End If

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET " _
                                        & " IMPORTO_APPROVATO=" & par.VirgoleInPunti(CDec(aNettoCompresiOneriEivaT + ritenutaLeggeT)) & "," _
                                        & " ID_STATO=1," _
                                        & " ID_VOCE_PF_IMPORTO=" & idPfVoceImporto & "," _
                                        & " RIT_LEGGE_IVATA=" & par.VirgoleInPunti(ritenutaLeggeT) _
                                        & " WHERE ID=(SELECT ID_PRENOTAZIONE_PAGAMENTO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID=" & idMan & ")"
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI SET " _
                                        & " IMPORTO_CONSUNTIVATO=" & par.VirgoleInPunti(prezzoTotT) & "," _
                                        & " STATO=2," _
                                        & " ID_PF_VOCE_IMPORTO=" & idPfVoceImporto & "," _
                                        & " RIT_LEGGE=" & par.VirgoleInPunti(ritenutaLeggeT) & "," _
                                        & oneriImportati _
                                        & " IMPORTO_TOT=" & par.VirgoleInPunti(CDec(aNettoCompresiOneriEivaT + ritenutaLeggeT)) _
                                        & " WHERE ID=" & idMan
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_MANUTENZIONE " _
                                        & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                                        & " VALUES " _
                                        & " (" & idMan & ", " & Session.Item("ID_OPERATORE") & ", '" & orarioOperazione & "', 'F292', 'CONSUNTIVAZIONE EFFETTUATA TRAMITE STR') "
                                    par.cmd.ExecuteNonQuery()

                                    oneriSicurezza = 0
                                    aLordoEsclusiOneri = 0
                                    ribassoDasta = 0
                                    aNettoEsclusiOneri = 0
                                    aNettoCompresiOneri = 0
                                    iva = 0
                                    aNettoCompresiOneriEiva = 0
                                    ritenutaLegge = 0
                                    prezzoTot = 0

                                End If
                                'par.cmd.CommandText = "select id from siscom_mi.codifica_Str where CODIFICA_STR.CODICE='" & codiceDGR & "'"

                                par.cmd.CommandText = " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN (  " _
                                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO  " _
                                    & " CONNECT BY PRIOR ID=ID_OLD " _
                                    & " START WITH ID = (SELECT ID FROM SISCOM_MI.CODIFICA_STR WHERE CODIFICA_STR.CODICE='" & codiceDGR & "') " _
                                    & " UNION " _
                                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO  " _
                                    & " CONNECT BY PRIOR ID_OLD=ID " _
                                    & " START WITH ID = (SELECT ID FROM SISCOM_MI.CODIFICA_STR WHERE CODIFICA_STR.CODICE='" & codiceDGR & "') " _
                                    & " ) " _
                                    & " AND ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" _
                                    & " (SELECT MANUTENZIONI.ID_PIANO_FINANZIARIO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR||'/'||ANNO='" & ordine & "')) "

                                idPfVoceImporto = par.IfNull(par.cmd.ExecuteScalar, 0)
                                ordine = Trim(ordine)
                                'Dim indiceSlash As Integer = ordine.ToString.IndexOf("/") + 1
                                'Dim codiceODLprogr As Integer = CInt(Left(ordine, Len(ordine) - indiceSlash - 1))
                                'Dim codiceODLanno As Integer = CInt(Right(ordine, Len(ordine) - indiceSlash))
                                'ERRORE NON GESTITO APPOSITAMENTE PRODOTTO DALL'ASSENZA DI NUMERO ODL SCRITTO MALE
                                Try
                                    Dim codiceODLprogr As Integer = CInt(ordine.Split("/")(0))
                                    Dim codiceODLanno As Integer = CInt(ordine.Split("/")(1))
                                Catch ex As Exception
                                End Try
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPORT_STR ( " _
                                    & " CODICE_DGR, CODICE_ELEMENTO, CODICE_ODL,  " _
                                    & " CODICE_PROGETTO_VISION, DATA_SAL, IMPORTO,  " _
                                    & " NUMERO_CONTRATTO, NUMERO_SAL, " _
                                    & " ID_OPERATORE, NOME_FILE, DATA_ORA_OPERAZIONE,ONERI_SIC)  " _
                                    & " VALUES ('" & codiceDGR & "', " _
                                    & " '" & codiceElemento & "', " _
                                    & " '" & ordine & "', " _
                                    & " '" & codiceProgettoVision & "', " _
                                    & " '" & dataSal & "', " _
                                    & Replace(importo, ",", ".") & ", " _
                                    & " '" & numeroContratto & "', " _
                                    & " '" & numeroSal & "', " _
                                    & Session.Item("ID_OPERATORE") & "," _
                                    & " '" & nFile & "'," _
                                    & orarioOperazione _
                                    & "," & importoOneriStringa & ") "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE PROGR||'/'||ANNO='" & ordine & "'"
                                Dim idManutenzione As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                                idMan = idManutenzione

                                If idManutenzione > 0 Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.INTEGRAZIONE_STR SET STATO=0 WHERE ID_MANUTENZIONE=" & idManutenzione
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = " select max(id) from siscom_mi.manutenzioni_interventi where id_manutenzione=" & idManutenzione _
                                        & " and (case when length('" & codiceElemento & "')=9 then id_edificio " _
                                        & " when length('" & codiceElemento & "')=7 then id_complesso  " _
                                        & " when length('" & codiceElemento & "')=12 then id_unita_comune " _
                                        & " when length('" & codiceElemento & "')=13 then id_scala " _
                                        & " when length('" & codiceElemento & "')=15 then id_impianto " _
                                        & " when length('" & codiceElemento & "')=17 then id_unita_immobiliare " _
                                        & " else null end) " _
                                        & " = " _
                                        & " (case when length('" & codiceElemento & "')=9 then (select id from siscom_mi.edifici where edifici.cod_edificio='" & codiceElemento & "') " _
                                        & " when length('" & codiceElemento & "')=7 then (select id from siscom_mi.complessi_immobiliari where complessi_immobiliari.cod_complesso='" & codiceElemento & "')  " _
                                        & " when length('" & codiceElemento & "')=12 then (select id from siscom_mi.unita_comuni where unita_comuni.cod_unita_comune='" & codiceElemento & "') " _
                                        & " when length('" & codiceElemento & "')=13 then (select scale_edifici.id from siscom_mi.scale_edifici,siscom_mi.edifici where scale_edifici.id_edificio=edifici.id and cod_edificio||lpad(descrizione,4,'0')='" & codiceElemento & "') " _
                                        & " when length('" & codiceElemento & "')=15 then (select id from siscom_mi.impianti where impianti.cod_impianto='" & codiceElemento & "') " _
                                        & " when length('" & codiceElemento & "')=17 then (select id from siscom_mi.unita_immobiliari where unita_immobiliari.cod_unita_immobiliare='" & codiceElemento & "') " _
                                        & " else null end) and not exists (select id_manutenzioni_interventi from siscom_mi.manutenzioni_consuntivi where id_manutenzioni_interventi=manutenzioni_interventi.id)"
                                    Dim idIntervento As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                                    If idIntervento <> 0 Then

                                        par.cmd.CommandText = "delete from siscom_mi.manutenzioni_consuntivi where id_manutenzioni_interventi = " & idIntervento
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = "Insert into siscom_mi.MANUTENZIONI_CONSUNTIVI (ID, ID_MANUTENZIONI_INTERVENTI, COD_ARTICOLO, DESCRIZIONE, ID_UM, QUANTITA, PREZZO_UNITARIO, PREZZO_TOTALE) " _
                                            & " Values (siscom_mi.seq_manutenzioni_consuntivi.nextval, " & idIntervento & ", '***', '***', 4, 1, " & par.VirgoleInPunti(importo) & ", " & par.VirgoleInPunti(importo) & ")"
                                        par.cmd.ExecuteNonQuery()
                                        par.cmd.CommandText = " SELECT MAX (appalti_lotti_servizi.sconto_consumo) AS sconto_consumo, " _
                                            & " SUM (appalti_lotti_servizi.importo_consumo) AS importo_consumo, " _
                                            & " MAX (appalti_lotti_servizi.iva_consumo) AS iva_consumo, " _
                                            & " max(appalti.fl_rit_legge) as rit_legge, " _
                                            & " sum(prezzo_totale) as consuntivo, " _
                                            & " sum(appalti_lotti_servizi.oneri_sicurezza_consumo) as oneri " _
                                            & " FROM siscom_mi.appalti_lotti_servizi,  " _
                                            & " siscom_mi.appalti,  " _
                                            & " siscom_mi.manutenzioni, " _
                                            & " siscom_mi.manutenzioni_interventi, " _
                                            & " siscom_mi.manutenzioni_consuntivi " _
                                            & " WHERE appalti.id = appalti_lotti_Servizi.id_appalto " _
                                            & " AND manutenzioni.id_appalto = appalti.id " _
                                            & " and manutenzioni_interventi.id=MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI " _
                                            & " and manutenzioni_interventi.id_manutenzione=manutenzioni.id " _
                                            & " and appalti_lotti_Servizi.id_pf_voce_importo=manutenzioni.id_pf_voce_importo " _
                                            & " AND manutenzioni.id =" & idManutenzione _
                                            & " and manutenzioni_interventi.id=" & idIntervento _
                                            & " MINUS SELECT NULL,NULL,NULL,NULL,NULL,NULL FROM DUAL "
                                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim scontoConsumo As Decimal = 0
                                        Dim importoConsumo As Decimal = 0
                                        Dim ivaConsumo As Decimal = 0
                                        Dim ritLegge As Integer = 0
                                        Dim prezzoTotale As Decimal = 0
                                        Dim oneri As Decimal = 0
                                        If lettore.HasRows Then
                                            If lettore.Read Then
                                                scontoConsumo = par.IfNull(lettore("SCONTO_CONSUMO"), 0)
                                                importoConsumo = par.IfNull(lettore("IMPORTO_CONSUMO"), 0)
                                                ivaConsumo = par.IfNull(lettore("IVA_CONSUMO"), 0)
                                                ritLegge = par.IfNull(lettore("RIT_LEGGE"), 0)
                                                prezzoTotale = par.IfNull(lettore("CONSUNTIVO"), 0)
                                                oneri = par.IfNull(lettore("ONERI"), 0)
                                                Dim ImportoOneriFisso As Decimal = -1
                                                If importoOneri <> "" Then
                                                    ImportoOneriFisso = CDec(importoOneri)
                                                End If
                                                Dim DizionarioValoriOrdine As Generic.Dictionary(Of String, Decimal) = par.CalcoloImportiOrdineConParametri(scontoConsumo, Round(oneri / importoConsumo * 100, 4), ivaConsumo, prezzoTotale, ritLegge, ImportoOneriFisso)
                                                prezzoTot += DizionarioValoriOrdine("LORDO_COMPRESI_ONERI")
                                                oneriSicurezza += DizionarioValoriOrdine("ONERI_SICUREZZA")
                                                aLordoEsclusiOneri += DizionarioValoriOrdine("LORDO_ESCLUSI_ONERI")
                                                ribassoDasta += DizionarioValoriOrdine("RIBASSO")
                                                aNettoEsclusiOneri += DizionarioValoriOrdine("NETTO_ESCLUSI_ONERI")
                                                aNettoCompresiOneri += DizionarioValoriOrdine("NETTO_COMPRESI_ONERI")
                                                iva += DizionarioValoriOrdine("IVA")
                                                aNettoCompresiOneriEiva += DizionarioValoriOrdine("NETTO_COMPRESI_ONERI_IVA")
                                                ritenutaLegge += DizionarioValoriOrdine("RIT_LEGGE_IVATA")
                                            End If
                                        Else
                                            errore = 3
                                            lettore.Close()
                                            Exit For
                                        End If
                                        lettore.Close()
                                    Else
                                        'In precedenza era previsto un errore, invece vogliono che sia inserito un altro intervento
                                        'errore = 1
                                        'Exit For
                                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL FROM DUAL"
                                        idIntervento = par.cmd.ExecuteScalar
                                        par.cmd.CommandText = "SELECT ID,'COMPLESSO','COMPLESSO' FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COD_COMPLESSO='" & codiceElemento & "' " _
                                            & " AND ID IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & ") " _
                                            & " UNION " _
                                            & " SELECT ID,'EDIFICIO','EDIFICIO' FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO='" & codiceElemento & "' " _
                                            & " AND ID_COMPLESSO IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & ")" _
                                            & " UNION " _
                                            & " SELECT ID,'UNITA IMMOBILIARE','UNITA IMMOBILIARE' FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceElemento & "' " _
                                            & " AND ID_edificio in (select id from siscom_mi.edifici where id_Complesso IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & "))" _
                                            & " UNION " _
                                            & " SELECT ID,TIPOLOGIA_IMPIANTI.DESCRIZIONE,'IMPIANTO' FROM SISCOM_MI.IMPIANTI,SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE IMPIANTI.COD_TIPOLOGIA=TIPOLOGIA_IMPIANTI.COD AND COD_IMPIANTO='" & codiceElemento & "' " _
                                            & " AND ID_COMPLESSO IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & ")" _
                                            & " UNION " _
                                            & " SELECT SCALE_EDIFICI.ID,'SCALA','SCALA' FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI WHERE SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID AND COD_EDIFICIO||LPAD(SCALE_EDIFICI.DESCRIZIONE,4,'0')='" & codiceElemento & "' " _
                                            & " AND ID_edificio in (select id from siscom_mi.edifici where id_Complesso IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & "))" _
                                            & " UNION " _
                                            & " SELECT UNITA_COMUNI.ID,'UNITA COMUNE','UNITA COMUNE' FROM SISCOM_MI.UNITA_COMUNI WHERE UNITA_COMUNI.COD_UNITA_COMUNE='" & codiceElemento & "' " _
                                            & " AND ( ID_COMPLESSO IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & ")" _
                                            & " or ID_edificio in (select id from siscom_mi.edifici where id_Complesso IN (select (case when id_complesso is not null then id_complesso else (select edifici.id_complesso from siscom_mi.edifici where edifici.id=manutenzioni.id_Edificio) end) from siscom_mi.manutenzioni where id=" & idManutenzione & ")))"

                                        Dim lettoreElemento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        If lettoreElemento.Read Then
                                            Dim tipologia = par.IfNull(lettoreElemento(1), "")
                                            Dim tipologiaGlobale = par.IfNull(lettoreElemento(2), "")
                                            Dim idImpianto As String = "NULL"
                                            Dim idComplesso As String = "NULL"
                                            Dim idEdificio As String = "NULL"
                                            Dim idUnitaImmobiliare As String = "NULL"
                                            Dim idUnitaComune As String = "NULL"
                                            Dim idScala As String = "NULL"
                                            Select Case tipologiaGlobale
                                                Case "COMPLESSO"
                                                    idComplesso = par.IfNull(lettoreElemento(0), 0)
                                                Case "EDIFICIO"
                                                    idEdificio = par.IfNull(lettoreElemento(0), 0)
                                                Case "UNITA IMMOBILIARE"
                                                    idUnitaImmobiliare = par.IfNull(lettoreElemento(0), 0)
                                                Case "UNITA_COMUNE"
                                                    idUnitaComune = par.IfNull(lettoreElemento(0), 0)
                                                Case "IMPIANTO"
                                                    idImpianto = par.IfNull(lettoreElemento(0), 0)
                                                Case "SCALA"
                                                    idScala = par.IfNull(lettoreElemento(0), 0)
                                            End Select
                                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_INTERVENTI ( " _
                                                & " ID, ID_MANUTENZIONE, TIPOLOGIA,  " _
                                                & " ID_IMPIANTO, ID_COMPLESSO, ID_EDIFICIO,  " _
                                                & " ID_UNITA_IMMOBILIARE, ID_UNITA_COMUNE, IMPORTO_PRESUNTO,  " _
                                                & " FL_BLOCCATO, ID_SCALA)  " _
                                                & " VALUES (" & idIntervento & ", " _
                                                & idManutenzione & " , " _
                                                & " '" & tipologia & "', " _
                                                & idImpianto & ", " _
                                                & idComplesso & ", " _
                                                & idEdificio & ", " _
                                                & idUnitaImmobiliare & ", " _
                                                & idUnitaComune & ", " _
                                                & "0, " _
                                                & "0, " _
                                                & idScala & ")"
                                            par.cmd.ExecuteNonQuery()
                                            par.cmd.CommandText = "Insert into siscom_mi.MANUTENZIONI_CONSUNTIVI (ID, ID_MANUTENZIONI_INTERVENTI, COD_ARTICOLO, DESCRIZIONE, ID_UM, QUANTITA, PREZZO_UNITARIO, PREZZO_TOTALE) " _
                                                & " Values (siscom_mi.seq_manutenzioni_consuntivi.nextval, " & idIntervento & ", '***', '***', 4, 1, " & par.VirgoleInPunti(importo) & ", " & par.VirgoleInPunti(importo) & ")"
                                            par.cmd.ExecuteNonQuery()
                                            par.cmd.CommandText = " SELECT MAX (appalti_lotti_servizi.sconto_consumo) AS sconto_consumo, " _
                                                & " SUM (appalti_lotti_servizi.importo_consumo) AS importo_consumo, " _
                                                & " MAX (appalti_lotti_servizi.iva_consumo) AS iva_consumo, " _
                                                & " max(appalti.fl_rit_legge) as rit_legge, " _
                                                & " sum(prezzo_totale) as consuntivo, " _
                                                & " sum(appalti_lotti_servizi.oneri_sicurezza_consumo) as oneri " _
                                                & " FROM siscom_mi.appalti_lotti_servizi,  " _
                                                & " siscom_mi.appalti,  " _
                                                & " siscom_mi.manutenzioni, " _
                                                & " siscom_mi.manutenzioni_interventi, " _
                                                & " siscom_mi.manutenzioni_consuntivi " _
                                                & " WHERE appalti.id = appalti_lotti_Servizi.id_appalto " _
                                                & " AND manutenzioni.id_appalto = appalti.id " _
                                                & " and manutenzioni_interventi.id=MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI " _
                                                & " and manutenzioni_interventi.id_manutenzione=manutenzioni.id " _
                                                & " and appalti_lotti_Servizi.id_pf_voce_importo=manutenzioni.id_pf_voce_importo " _
                                                & " AND manutenzioni.id =" & idManutenzione _
                                                & " and manutenzioni_interventi.id=" & idIntervento _
                                                & " MINUS SELECT NULL,NULL,NULL,NULL,NULL,NULL FROM DUAL "
                                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                            Dim scontoConsumo As Decimal = 0
                                            Dim importoConsumo As Decimal = 0
                                            Dim ivaConsumo As Decimal = 0
                                            Dim ritLegge As Integer = 0
                                            Dim prezzoTotale As Decimal = 0
                                            Dim oneri As Decimal = 0
                                            If lettore.HasRows Then
                                                If lettore.Read Then
                                                    scontoConsumo = par.IfNull(lettore("SCONTO_CONSUMO"), 0)
                                                    importoConsumo = par.IfNull(lettore("IMPORTO_CONSUMO"), 0)
                                                    ivaConsumo = par.IfNull(lettore("IVA_CONSUMO"), 0)
                                                    ritLegge = par.IfNull(lettore("RIT_LEGGE"), 0)
                                                    prezzoTotale = par.IfNull(lettore("CONSUNTIVO"), 0)
                                                    oneri = par.IfNull(lettore("ONERI"), 0)
                                                    Dim ImportoOneriFisso As Decimal = -1
                                                    If importoOneri <> "" Then
                                                        ImportoOneriFisso = CDec(importoOneri)
                                                    End If
                                                    Dim DizionarioValoriOrdine As Generic.Dictionary(Of String, Decimal) = par.CalcoloImportiOrdineConParametri(scontoConsumo, Round(oneri / importoConsumo * 100, 4), ivaConsumo, prezzoTotale, ritLegge, ImportoOneriFisso)
                                                    prezzoTot += DizionarioValoriOrdine("LORDO_COMPRESI_ONERI")
                                                    oneriSicurezza += DizionarioValoriOrdine("ONERI_SICUREZZA")
                                                    aLordoEsclusiOneri += DizionarioValoriOrdine("LORDO_ESCLUSI_ONERI")
                                                    ribassoDasta += DizionarioValoriOrdine("RIBASSO")
                                                    aNettoEsclusiOneri += DizionarioValoriOrdine("NETTO_ESCLUSI_ONERI")
                                                    aNettoCompresiOneri += DizionarioValoriOrdine("NETTO_COMPRESI_ONERI")
                                                    iva += DizionarioValoriOrdine("IVA")
                                                    aNettoCompresiOneriEiva += DizionarioValoriOrdine("NETTO_COMPRESI_ONERI_IVA")
                                                    ritenutaLegge += DizionarioValoriOrdine("RIT_LEGGE_IVATA")
                                                End If
                                            Else
                                                errore = 3
                                                lettore.Close()
                                                Exit For
                                            End If
                                            lettore.Close()
                                        Else
                                            lettoreElemento.Close()
                                            errore = 1
                                            Exit For
                                        End If
                                        lettoreElemento.Close()
                                    End If
                                Else
                                    errore = 2
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        End If

                    Next

                    If errore = 0 Then

                        If idMan = 0 Then
                            errore = 4
                            Exit For
                        Else
                            oneriSicurezzaT = oneriSicurezza
                            aLordoEsclusiOneriT = aLordoEsclusiOneri
                            ribassoDastaT = ribassoDasta
                            aNettoEsclusiOneriT = aNettoEsclusiOneri
                            aNettoCompresiOneriT = aNettoCompresiOneri
                            ivaT = iva
                            aNettoCompresiOneriEivaT = aNettoCompresiOneriEiva
                            ritenutaLeggeT = ritenutaLegge
                            prezzoTotT = prezzoTot

                            Dim oneriImportati As String = "IMPORTO_ONERI_CONS=0,"
                            par.cmd.CommandText = "select nvl(SUM(oneri_sic),-1) from siscom_mi.import_str where codice_odl=(select progr||'/'||anno from siscom_mi.manutenzioni where id=" & idMan & ") " _
                                & " and DATA_ORA_OPERAZIONE = (select max(DATA_ORA_OPERAZIONE) from siscom_mi.import_str where codice_odl=(select progr||'/'||anno from siscom_mi.manutenzioni where id=" & idMan & "))"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                If lettore(0) <> -1 Then
                                    oneriImportati = "IMPORTO_ONERI_CONS=" & par.VirgoleInPunti(lettore(0)) & ","
                                End If
                            End If

                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET " _
                                & " IMPORTO_APPROVATO=" & par.VirgoleInPunti(CDec(aNettoCompresiOneriEivaT + ritenutaLeggeT)) & "," _
                                & " ID_STATO=1," _
                                & " ID_VOCE_PF_IMPORTO=" & idPfVoceImporto & "," _
                                & " RIT_LEGGE_IVATA=" & par.VirgoleInPunti(ritenutaLeggeT) _
                                & " WHERE ID=(SELECT ID_PRENOTAZIONE_PAGAMENTO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID=" & idMan & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI SET " _
                                & " IMPORTO_CONSUNTIVATO=" & par.VirgoleInPunti(prezzoTotT) & "," _
                                & " STATO=2," _
                                & " ID_PF_VOCE_IMPORTO=" & idPfVoceImporto & "," _
                                & " RIT_LEGGE=" & par.VirgoleInPunti(ritenutaLeggeT) & "," _
                                & oneriImportati _
                                & " IMPORTO_TOT=" & par.VirgoleInPunti(CDec(aNettoCompresiOneriEivaT + ritenutaLeggeT)) _
                                & " WHERE ID=" & idMan
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_MANUTENZIONE " _
                                        & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                                        & " VALUES " _
                                        & " (" & idMan & ", " & Session.Item("ID_OPERATORE") & ", '" & orarioOperazione & "', 'F292', 'CONSUNTIVAZIONE EFFETTUATA TRAMITE STR') "
                            par.cmd.ExecuteNonQuery()

                            oneriSicurezza = 0
                            aLordoEsclusiOneri = 0
                            ribassoDasta = 0
                            aNettoEsclusiOneri = 0
                            aNettoCompresiOneri = 0
                            iva = 0
                            aNettoCompresiOneriEiva = 0
                            ritenutaLegge = 0
                            prezzoTot = 0
                        End If
                    Else
                        Exit For
                    End If
                End Using
            Next
            If errore > 0 Then
                connData.chiudi(False)
                Dim motivazione As String = ""
                Select Case errore
                    Case 1
                        motivazione = "Intervento consuntivato su una porzione di patrimonio al di fuori del complesso selezionato, file " & nFile & " - riga " & i
                    Case 2
                        motivazione = "Manutenzione non individuata, file " & nFile & " - riga " & i
                    Case 3
                        motivazione = "Appalto non individuato, file " & nFile & " - riga " & i
                    Case 4
                        motivazione = "Consuntivo non individuato, file " & nFile & " - riga " & i
                    Case 5
                        motivazione = "E' obbligatorio valorizzare il campo data SAL su tutti i consuntivi"
                End Select
                connData.apri()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANOMALIE_STR(ID,DATA_ORA_OPERAZIONE,ID_OPERATORE,MOTIVAZIONE) " _
                    & "VALUES (SISCOM_MI.SEQ_ANOMALIE_STR.NEXTVAL," & orarioOperazione & "," & Session.Item("ID_OPERATORE") & ",'" & motivazione.ToString.Replace("'", "''") & "')"
                par.cmd.ExecuteNonQuery()
                connData.chiudi()

                Select Case errore
                    Case 1
                        motivazione = "Intervento consuntivato su una porzione di patrimonio al di fuori del complesso selezionato, file " & nFile & " - riga " & i
                    Case 2
                        motivazione = "Manutenzione non individuata, file " & nFile & " - riga " & i
                    Case 3
                        motivazione = "Appalto non individuato, file " & nFile & " - riga " & i
                    Case 4
                        motivazione = "Consuntivo non individuato, file " & nFile & " - riga " & i
                    Case 5
                        motivazione = "E\' obbligatorio valorizzare il campo data SAL su tutti i consuntivi"
                End Select

                Response.Write("<script>alert('" & motivazione & "');</script>")
            Else
                rdpUpload.Dispose()
                connData.chiudi(True)
                Response.Write("<script>alert('Import terminato e manutenzioni consuntivate!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: btnCaricaFile_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
End Class