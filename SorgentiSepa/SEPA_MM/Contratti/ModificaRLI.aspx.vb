Imports Telerik.Web.UI
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_ModificaRLI
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim ClasseIban As New LibCs.CheckBancari

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)

    End Sub

    Protected Sub RadGridRLI_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridRLI.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                'If tipoAdempimento.Value = "0" Then
                If True Then
                    par.cmd.CommandText = "SELECT (CASE when tipologia_immobile=1 then 'Immobile principale' else 'Pertinenza' end) TIPOIMMOB,DATI_IMMOBILI_RLI.id as IDI," _
                        & " DATI_IMMOBILI_RLI.* " _
                        & " FROM SISCOM_MI.DATI_IMMOBILI_RLI WHERE ID_FORNITURA=" & dataItem("id").Text
                    e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                End If
                'End If
            Case Else

        End Select
    End Sub

    'Private Sub CreaXML()
    '    Try
    '        connData.apri(True)
    '        Dim categCatast As String = ""
    '        Dim varPdfBase64 As String = ""
    '        Dim varImpegnoATrasmettere As String = ""
    '        'par.cmd.CommandText = "select * from siscom_mi.dati_generali_rli,siscom_mi.dati_immobili_rli where dati_generali_rli.id=dati_immobili_rli.id_fornitura and dati_generali_rli.id=" & idFornitura.Value
    '        par.cmd.CommandText = "select * from siscom_mi.dati_generali_rli,siscom_mi.dati_immobili_rli where dati_generali_rli.id=dati_immobili_rli.id_fornitura and dati_generali_rli.NOME_FILE_XML like '" & Mid(nome_file_xml.Value, 1, 22) & "%' order by dati_generali_rli.id asc"

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dtDatiG As New Data.DataTable
    '        da.Fill(dtDatiG)
    '        da.Dispose()
    '        Dim zipfic As String = ""
    '        Dim testoXML As String = ""
    '        Dim NomeFile As String = ""
    '        Dim strScript As String = ""

    '        Dim ElencoFile() As String
    '        Dim i As Integer = 0
    '        If dtDatiG.Rows.Count > 0 Then


    '            For Each row1 As Data.DataRow In dtDatiG.Rows


    '                NomeFile = par.IfNull(dtDatiG.Rows(0).Item("ufficio_territoriale"), "") & par.IfNull(dtDatiG.Rows(0).Item("ID_contratto"), 0) & "_" & Format(Now, "yyyyMMddHHmmss")
    '                ReDim Preserve ElencoFile(i)

    '                ElencoFile(i) = NomeFile

    '                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".xml"), False)
    '                If i = dtDatiG.Rows.Count - 1 Then
    '                    i = i - 1
    '                End If

    '                If i = 0 Or dtDatiG.Rows(i).Item("ID") <> dtDatiG.Rows(i + 1).Item("ID") Then



    '                    testoXML = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>" & vbCrLf _
    '                                         & "<!--created with SEPA@Web (www.sistemiesoluzionisrl.it)-->" & vbCrLf _
    '                                         & "<loc:Fornitura xmlns:sc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:common:v2" & Chr(34) & vbCrLf _
    '                                         & "xmlns:loc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:loc:v1" & Chr(34) & vbCrLf _
    '                                         & "xmlns:reg=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:reg:v1" & Chr(34) & " > " & vbCrLf _
    '                                         & "<loc:Intestazione>" & vbCrLf _
    '                                         & "<loc:CodiceFornitura>" & row1.Item("codice_fornitura").ToString & "</loc:CodiceFornitura>" & vbCrLf _
    '                                         & "<loc:TipoFornitore>" & row1.Item("tipo_fornitore").ToString & "</loc:TipoFornitore>" & vbCrLf _
    '                                         & "<loc:CodiceFiscaleFornitore>" & row1.Item("cod_fiscale_fornitore").ToString & "</loc:CodiceFiscaleFornitore>" & vbCrLf _
    '                                         & "</loc:Intestazione>" & vbCrLf _
    '                                         & "<loc:DatiGenerali>" & vbCrLf _
    '                                         & "<loc:UfficioCompetente>" & vbCrLf _
    '                                         & "<loc:UfficioTerritoriale>" & row1.Item("ufficio_territoriale").ToString & "</loc:UfficioTerritoriale>" & vbCrLf _
    '                                         & "</loc:UfficioCompetente>" & vbCrLf _
    '                                         & "<loc:Versamento>" & vbCrLf _
    '                                         & "<reg:CodiceABI>" & row1.Item("codice_Abi").ToString & "</reg:CodiceABI>" & vbCrLf _
    '                                         & "<reg:CodiceCAB>" & row1.Item("codice_cab").ToString & "</reg:CodiceCAB>" & vbCrLf _
    '                                         & "<reg:NumeroContoCorrente>" & row1.Item("numero_conto_corrente").ToString & "</reg:NumeroContoCorrente>" & vbCrLf _
    '                                         & "<reg:CIN>" & row1.Item("cin").ToString & "</reg:CIN>" & vbCrLf _
    '                                         & "<reg:CodiceFiscaleTitolareCC>" & row1.Item("cod_fiscale_titolare_cc").ToString & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
    '                                         & "</loc:Versamento>" & vbCrLf _
    '                                         & "<loc:ImportoDaVersare>" & row1.Item("importo_da_versare").ToString & "</loc:ImportoDaVersare>" & vbCrLf _
    '                                         & "</loc:DatiGenerali>" & vbCrLf

    '                    testoXML &= "<loc:Documento identificativo=" & Chr(34) & row1.Item("num_identificativo_doc").ToString & Chr(34) & "><loc:Frontespizio>" & vbCrLf _
    '                    & "<loc:IdentificativoProdSoftware>SEPA2009</loc:IdentificativoProdSoftware>" & vbCrLf _
    '                    & "<loc:NumeroModuliCompilati>1</loc:NumeroModuliCompilati>" & vbCrLf _
    '                    & "<loc:IDContratto>" & row1.Item("id_contratto").ToString & "</loc:IDContratto>" & vbCrLf _
    '                    & "<loc:Richiedente>" & vbCrLf _
    '                    & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_Richiedente").ToString & "</loc:CodiceFiscale>" & vbCrLf

    '                    'If persGiuridicaRich = True Then
    '                    testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
    '                    & "<loc:Denominazione>" & row1.Item("denominazione_richiedente").ToString & "</loc:Denominazione>" & vbCrLf _
    '                    & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
    '                    & "<loc:Rappresentante>" & vbCrLf _
    '                    & "<loc:CodiceFiscaleRappresentante>" & row1.Item("cod_fiscale_rappresentante").ToString & "</loc:CodiceFiscaleRappresentante>" & vbCrLf _
    '                    & "<loc:CognomeRappresentante>" & row1.Item("cognome_rappresentante").ToString & "</loc:CognomeRappresentante>" & vbCrLf _
    '                    & "<loc:NomeRappresentante>" & row1.Item("nome_rappresentante").ToString & "</loc:NomeRappresentante>" & vbCrLf _
    '                    & "<loc:CodiceCarica>" & row1.Item("codice_carica").ToString & "</loc:CodiceCarica>" & vbCrLf _
    '                    & "</loc:Rappresentante>" & vbCrLf
    '                    'Else
    '                    '    testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
    '                    '    & "<loc:Cognome>" & varCognomeRich & "</loc:Cognome>" & vbCrLf _
    '                    '    & "<loc:Nome>" & varNome & "</loc:Nome>" & vbCrLf _
    '                    '    & "</loc:PersoneFisiche>" & vbCrLf
    '                    'End If

    '                    testoXML &= "<loc:Firma>" & row1.Item("firma").ToString & "</loc:Firma>" & vbCrLf _
    '                            & "<loc:TipologiaRichiedente>1</loc:TipologiaRichiedente>" & vbCrLf _
    '                            & "<loc:Subentro>" & row1.Item("subentro").ToString & "</loc:Subentro>" & vbCrLf _
    '                            & "</loc:Richiedente>" & vbCrLf
    '                    testoXML &= "<loc:TipologiaContratto>" & row1.Item("tipologia_contratto").ToString & "</loc:TipologiaContratto>" & vbCrLf _
    '                      & "<loc:PagamentoInteraDurataContratto>" & row1.Item("pagamento_intera_durata_ru").ToString & "</loc:PagamentoInteraDurataContratto>" & vbCrLf _
    '                      & "<loc:EventiEccezionali>" & row1.Item("eventi_eccezionali").ToString & "</loc:EventiEccezionali>" & vbCrLf

    '                    testoXML &= "<loc:Importi>" & vbCrLf _
    '                        & "<loc:Imposte>" & vbCrLf _
    '                        & "<loc:ImpostaDiRegistro>" & row1.Item("imposta_registro").ToString & "</loc:ImpostaDiRegistro>" & vbCrLf _
    '                        & "<loc:ImpostaDiBollo>" & row1.Item("imposta_bollo").ToString & "</loc:ImpostaDiBollo>" & vbCrLf _
    '                        & "</loc:Imposte>" & vbCrLf _
    '                        & "<loc:Sanzioni>" & vbCrLf _
    '                        & "<loc:ImpostaDiRegistro>" & row1.Item("sanzione_imp_registro").ToString & "</loc:ImpostaDiRegistro>" & vbCrLf _
    '                        & "<loc:ImpostaDiBollo>" & row1.Item("sanzione_imp_bollo").ToString & "</loc:ImpostaDiBollo>" & vbCrLf _
    '                        & "</loc:Sanzioni>" & vbCrLf _
    '                        & "<loc:Interessi>" & row1.Item("tot_interessi").ToString & "</loc:Interessi>" & vbCrLf _
    '                        & "</loc:Importi>" & vbCrLf

    '                    testoXML &= "<loc:Registrazione>" & vbCrLf _
    '                        & "<loc:NumeroProgressivoCanone>" & row1.Item("num_progressivo_canone").ToString & "</loc:NumeroProgressivoCanone>" & vbCrLf _
    '                        & "<loc:Contratto>" & vbCrLf _
    '                        & "<loc:DataInizio>" & Replace(par.FormattaData(row1.Item("data_inizio_contratto").ToString), "/", "") & "</loc:DataInizio>" & vbCrLf _
    '                        & "<loc:DataFine>" & Replace(par.FormattaData(row1.Item("data_fine_contratto").ToString), "/", "") & "</loc:DataFine>" & vbCrLf _
    '                        & "<loc:ImportoCanone>" & CStr(row1.Item("importo_canone").ToString) & "</loc:ImportoCanone>" & vbCrLf _
    '                        & "<loc:DataStipula>" & Replace(par.FormattaData(row1.Item("data_stipula").ToString), "/", "") & "</loc:DataStipula>" & vbCrLf _
    '                        & "<loc:NumeroPagine>" & row1.Item("numero_pagine").ToString & "</loc:NumeroPagine>" & vbCrLf _
    '                        & "<loc:NumeroCopie>" & row1.Item("numero_copie").ToString & "</loc:NumeroCopie>" & vbCrLf _
    '                        & "<loc:ContrattoSoggettoIVA>" & row1.Item("contratto_Sogg_iva").ToString & "</loc:ContrattoSoggettoIVA>" & vbCrLf _
    '                    & "</loc:Contratto>" & vbCrLf

    '                    varPdfBase64 = par.ConversioneBase64(row1.Item("id_contratto").ToString)

    '                    testoXML &= "<loc:Allegati>" & vbCrLf _
    '                        & "<reg:FileType>application/pdf</reg:FileType>" & vbCrLf _
    '                        & "<reg:FileName>" & row1.Item("id_contratto").ToString & Format(Now, "yyyyMMdd") & "</reg:FileName>" & vbCrLf _
    '                        & "<reg:FileDescription>STAMPACONTRATTO</reg:FileDescription>" & vbCrLf _
    '                        & "<reg:ImageData>" & varPdfBase64 & "</reg:ImageData></loc:Allegati>"

    '                    testoXML &= "</loc:Registrazione>" & vbCrLf

    '                    varImpegnoATrasmettere = "<loc:ImpegnoATrasmettere>" & vbCrLf _
    '                        & " <reg:CFintermediario>" & row1.Item("cf_intermediario").ToString & "</reg:CFintermediario>" & vbCrLf _
    '                        & "<reg:ImpegnoATrasmettere>1</reg:ImpegnoATrasmettere>" & vbCrLf _
    '                        & " <reg:DataImpegno>" & Replace(par.FormattaData(par.IfNull(row1.Item("data_impegno").ToString, "")), "/", "") & "</reg:DataImpegno>" & vbCrLf _
    '                        & " <reg:FirmaIntermediario>1</reg:FirmaIntermediario>" & vbCrLf _
    '                        & "</loc:ImpegnoATrasmettere>" & vbCrLf

    '                    testoXML &= varImpegnoATrasmettere & "</loc:Frontespizio>" & vbCrLf


    '                    testoXML &= "<loc:Soggetti>" & vbCrLf _
    '                            & "<loc:PrimoModulo>" & vbCrLf _
    '                            & "<loc:Locatore>" & vbCrLf _
    '                            & "<loc:NumeroProgressivo>" & row1.Item("num_progressivo_locatore").ToString & "</loc:NumeroProgressivo>" & vbCrLf _
    '                            & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_locatore").ToString & "</loc:CodiceFiscale>" & vbCrLf _
    '                            & "<loc:SoggettivitaIVA>" & row1.Item("soggettivita_iva_locatore").ToString & "</loc:SoggettivitaIVA>" & vbCrLf _
    '                            & "<loc:SoggettiDiversiDaPF>" & vbCrLf _
    '                            & "<loc:Denominazione>" & row1.Item("denominazione_locatore").ToString.ToUpper & "</loc:Denominazione>" & vbCrLf _
    '                            & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
    '                            & "</loc:Locatore>" & vbCrLf


    '                    testoXML &= "<loc:Conduttore>" & vbCrLf _
    '                                & "<loc:NumeroProgressivo>" & row1.Item("num_progressivo_conduttore").ToString & "</loc:NumeroProgressivo>" & vbCrLf _
    '                                & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_conduttore").ToString & "</loc:CodiceFiscale>" & vbCrLf _
    '                                & "<loc:SoggettivitaIVA>" & row1.Item("soggettivita_iva_conduttore").ToString & "</loc:SoggettivitaIVA>" & vbCrLf

    '                    If row1.Item("comune_nascita_conduttore").ToString <> "" Then
    '                        testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
    '                            & "<sc:Cognome>" & row1.Item("cognome_conduttore").ToString & "</sc:Cognome>" & vbCrLf _
    '                            & "<sc:Nome>" & row1.Item("nome_conduttore").ToString & "</sc:Nome>" & vbCrLf _
    '                            & "<sc:Sesso>" & row1.Item("sesso_conduttore").ToString & "</sc:Sesso>" & vbCrLf _
    '                            & "<sc:DataNascita>" & Replace(par.FormattaData(row1.Item("data_nascita_conduttore").ToString), "/", "") & "</sc:DataNascita>" & vbCrLf _
    '                            & "<sc:ComuneNascita>" & row1.Item("comune_nascita_conduttore").ToString & "</sc:ComuneNascita>" & vbCrLf


    '                        testoXML &= "<sc:ProvinciaNascita>" & row1.Item("provincia_nascita_conduttore").ToString & "</sc:ProvinciaNascita>" & vbCrLf

    '                        testoXML &= "</loc:PersoneFisiche>" & vbCrLf _
    '                            & "</loc:Conduttore>" & vbCrLf

    '                    Else
    '                        testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
    '                            & "<loc:Denominazione>" & row1.Item("denominazione_conduttore").ToString & "</loc:Denominazione>" & vbCrLf _
    '                            & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
    '                            & "</loc:Conduttore>" & vbCrLf

    '                    End If

    '                    testoXML &= "</loc:PrimoModulo>" & vbCrLf

    '                    testoXML &= "</loc:Soggetti>" & vbCrLf

    '                    testoXML &= "<loc:Immobili>" & vbCrLf _
    '                        & "<loc:PrimoModulo>" & vbCrLf

    '                    testoXML &= "<loc:Immobile>" & vbCrLf _
    '                                & "<loc:NumeroProgressivoImmobile>" & row1.Item("num_progressivo").ToString & "</loc:NumeroProgressivoImmobile>" & vbCrLf _
    '                                & "<loc:TipologiaImmobile>" & row1.Item("tipologia_immobile").ToString & "</loc:TipologiaImmobile>" & vbCrLf _
    '                                & "<loc:CodiceComuneCatastale>" & row1.Item("codice_comune_catastale").ToString & "</loc:CodiceComuneCatastale>" & vbCrLf _
    '                                & "<loc:TipoCatasto>" & row1.Item("tipo_catasto").ToString & "</loc:TipoCatasto>" & vbCrLf _
    '                                & "<loc:PorzioneImmobile>" & row1.Item("porzione_immobile").ToString & "</loc:PorzioneImmobile>" & vbCrLf

    '                    If row1.Item("rendita_Catastale").ToString <> "" Then
    '                        If row1.Item("tipologia_immobile").ToString <> "2" Then
    '                            If row1.Item("subalterno").ToString <> "" Then
    '                                testoXML &= "<loc:DatiCatastali>" & vbCrLf _
    '                                    & "<reg:Foglio>" & row1.Item("foglio").ToString & "</reg:Foglio>" & vbCrLf _
    '                                    & "<reg:Particella>" & row1.Item("particella").ToString & "</reg:Particella>" & vbCrLf _
    '                                    & "<reg:Particella_Denominatore>" & row1.Item("particella_Denominatore").ToString & "</reg:Particella_Denominatore>" & vbCrLf

    '                                If Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) <> "    " Then
    '                                    testoXML &= "<reg:Subalterno>" & Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) & "</reg:Subalterno>" & vbCrLf
    '                                End If

    '                                testoXML &= "</loc:DatiCatastali>" & vbCrLf
    '                            Else
    '                                testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
    '                            End If
    '                        Else
    '                            testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
    '                        End If

    '                        testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
    '                            & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

    '                        If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
    '                            If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
    '                                categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
    '                            Else
    '                                categCatast = row1.Item("categoria_catastale").ToString
    '                            End If
    '                            testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
    '                        End If
    '                        If row1.Item("rendita_catastale").ToString <> "0" Then
    '                            testoXML &= "<loc:RenditaCatastale>" & row1.Item("rendita_catastale").ToString & "</loc:RenditaCatastale>" & vbCrLf
    '                        End If
    '                    Else
    '                        testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
    '                        testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
    '                            & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

    '                        If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
    '                            If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
    '                                categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
    '                            End If
    '                            testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
    '                        End If
    '                    End If
    '                    testoXML &= "<loc:TipologiaIndirizzo>" & row1.Item("tipologia_indirizzo").ToString & "</loc:TipologiaIndirizzo>" & vbCrLf _
    '                        & "<loc:Indirizzo>" & row1.Item("indirizzo").ToString & "</loc:Indirizzo>" & vbCrLf _
    '                        & "<loc:NumCivico>" & Replace(RTrim(LTrim(row1.Item("num_civico").ToString)), "/", "") & "</loc:NumCivico>" & vbCrLf _
    '                        & "</loc:Immobile>" & vbCrLf

    '                Else

    '                    testoXML &= "<loc:Immobile>" & vbCrLf _
    '                                & "<loc:NumeroProgressivoImmobile>" & row1.Item("num_progressivo").ToString & "</loc:NumeroProgressivoImmobile>" & vbCrLf _
    '                                & "<loc:TipologiaImmobile>" & row1.Item("tipologia_immobile").ToString & "</loc:TipologiaImmobile>" & vbCrLf _
    '                                & "<loc:CodiceComuneCatastale>" & row1.Item("codice_comune_catastale").ToString & "</loc:CodiceComuneCatastale>" & vbCrLf _
    '                                & "<loc:TipoCatasto>" & row1.Item("tipo_catasto").ToString & "</loc:TipoCatasto>" & vbCrLf _
    '                                & "<loc:PorzioneImmobile>" & row1.Item("porzione_immobile").ToString & "</loc:PorzioneImmobile>" & vbCrLf

    '                    If row1.Item("rendita_Catastale").ToString <> "" Then
    '                        If row1.Item("tipologia_immobile").ToString <> "2" Then
    '                            If row1.Item("subalterno").ToString <> "" Then
    '                                testoXML &= "<loc:DatiCatastali>" & vbCrLf _
    '                                    & "<reg:Foglio>" & row1.Item("foglio").ToString & "</reg:Foglio>" & vbCrLf _
    '                                    & "<reg:Particella>" & row1.Item("particella").ToString & "</reg:Particella>" & vbCrLf _
    '                                    & "<reg:Particella_Denominatore>" & row1.Item("particella_Denominatore").ToString & "</reg:Particella_Denominatore>" & vbCrLf

    '                                If Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) <> "    " Then
    '                                    testoXML &= "<reg:Subalterno>" & Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) & "</reg:Subalterno>" & vbCrLf
    '                                End If

    '                                testoXML &= "</loc:DatiCatastali>" & vbCrLf
    '                            Else
    '                                testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
    '                            End If
    '                        Else
    '                            testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
    '                        End If

    '                        testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
    '                            & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

    '                        If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
    '                            If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
    '                                categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
    '                            Else
    '                                categCatast = row1.Item("categoria_catastale").ToString
    '                            End If
    '                            testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
    '                        End If
    '                        If row1.Item("rendita_catastale").ToString <> "0" Then
    '                            testoXML &= "<loc:RenditaCatastale>" & row1.Item("rendita_catastale").ToString & "</loc:RenditaCatastale>" & vbCrLf
    '                        End If
    '                    Else
    '                        testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
    '                        testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
    '                            & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

    '                        If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
    '                            If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
    '                                categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
    '                            Else
    '                                categCatast = row1.Item("categoria_catastale").ToString
    '                            End If
    '                            testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
    '                        End If
    '                    End If
    '                    testoXML &= "<loc:TipologiaIndirizzo>" & row1.Item("tipologia_indirizzo").ToString & "</loc:TipologiaIndirizzo>" & vbCrLf _
    '                        & "<loc:Indirizzo>" & row1.Item("indirizzo").ToString & "</loc:Indirizzo>" & vbCrLf _
    '                        & "<loc:NumCivico>" & Replace(RTrim(LTrim(row1.Item("num_civico").ToString)), "/", "") & "</loc:NumCivico>" & vbCrLf _
    '                        & "</loc:Immobile>" & vbCrLf


    '                End If

    '                testoXML &= "</loc:PrimoModulo>" & vbCrLf _
    '                     & "</loc:Immobili>" & vbCrLf


    '                testoXML &= "</loc:Documento>" & vbCrLf

    '                testoXML = testoXML & "</loc:Fornitura>"

    '                sr.WriteLine(testoXML)
    '                sr.Close()
    '                i = i + 1


    '            Next

    '        End If

    '        connData.chiudi(True)

    '        If i > 0 Then
    '            Dim NomeFilezip As String = "REG_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

    '            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip)
    '            Dim kkK As Integer = 0

    '            Dim objCrc32 As New Crc32()
    '            Dim strmZipOutputStream As ZipOutputStream

    '            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '            strmZipOutputStream.SetLevel(6)

    '            Dim strFile As String

    '            For kkK = 0 To i - 1
    '                strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".xml")
    '                Dim strmFile As FileStream = File.OpenRead(strFile)
    '                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '                '
    '                strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '                Dim sFile As String = Path.GetFileName(strFile)
    '                Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '                Dim fi As New FileInfo(strFile)
    '                theEntry.DateTime = fi.LastWriteTime
    '                theEntry.Size = strmFile.Length
    '                strmFile.Close()
    '                objCrc32.Reset()
    '                objCrc32.Update(abyBuffer)
    '                theEntry.Crc = objCrc32.Value
    '                strmZipOutputStream.PutNextEntry(theEntry)
    '                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '                File.Delete(strFile)
    '            Next

    '            strmZipOutputStream.Finish()
    '            strmZipOutputStream.Close()

    '            Response.Redirect("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip, False)
    '        Else
    '            ' Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
    '        End If

    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Contratti - CreaXML - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Private Sub CreaXML()
        Try
            connData.apri(True)
            Dim categCatast As String = ""
            Dim varPdfBase64 As String = ""
            Dim varImpegnoATrasmettere As String = ""
            par.cmd.CommandText = "select * from siscom_mi.dati_generali_rli,siscom_mi.dati_immobili_rli where dati_generali_rli.id=dati_immobili_rli.id_fornitura and dati_generali_rli.id=" & idFornitura.Value

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDatiG As New Data.DataTable
            da.Fill(dtDatiG)
            da.Dispose()
            Dim zipfic As String = ""
            Dim testoXML As String = ""
            Dim NomeFile As String = ""
            Dim strScript As String = ""
            Dim varNumeroIBAN As String = ""
            Dim ElencoFile() As String
            Dim i As Integer = 0
            If dtDatiG.Rows.Count > 0 Then




                    NomeFile = par.IfNull(dtDatiG.Rows(0).Item("ufficio_territoriale"), "") & par.IfNull(dtDatiG.Rows(0).Item("ID_contratto"), 0) & "_" & Format(Now, "yyyyMMddHHmmss")
                ReDim Preserve ElencoFile(0)

                ElencoFile(0) = NomeFile

                    Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".xml"), False)

                varNumeroIBAN = ClasseIban.CalcolaIBAN("IT", dtDatiG.Rows(0).Item("cin").ToString & dtDatiG.Rows(0).Item("codice_Abi").ToString & dtDatiG.Rows(0).Item("codice_cab").ToString & dtDatiG.Rows(0).Item("numero_conto_corrente").ToString)

                For Each row1 As Data.DataRow In dtDatiG.Rows
                    If i = 0 Then

                        i = i + 1



                        testoXML = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>" & vbCrLf _
                                             & "<!--created with SEPA@Web (www.sistemiesoluzionisrl.it)-->" & vbCrLf _
                                             & "<loc:Fornitura xmlns:sc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:common:v2" & Chr(34) & vbCrLf _
                                             & "xmlns:loc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:loc:v1" & Chr(34) & vbCrLf _
                                             & "xmlns:reg=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:reg:v1" & Chr(34) & " > " & vbCrLf _
                                             & "<loc:Intestazione>" & vbCrLf _
                                             & "<loc:CodiceFornitura>" & row1.Item("codice_fornitura").ToString & "</loc:CodiceFornitura>" & vbCrLf _
                                             & "<loc:TipoFornitore>" & row1.Item("tipo_fornitore").ToString & "</loc:TipoFornitore>" & vbCrLf _
                                             & "<loc:CodiceFiscaleFornitore>" & row1.Item("cod_fiscale_fornitore").ToString & "</loc:CodiceFiscaleFornitore>" & vbCrLf _
                                             & "</loc:Intestazione>" & vbCrLf _
                                             & "<loc:DatiGenerali>" & vbCrLf _
                                             & "<loc:UfficioCompetente>" & vbCrLf _
                                             & "<loc:UfficioTerritoriale>" & row1.Item("ufficio_territoriale").ToString & "</loc:UfficioTerritoriale>" & vbCrLf _
                                             & "</loc:UfficioCompetente>" & vbCrLf _
                                             & "<loc:Versamento>" & vbCrLf _
                                          & "<reg:IBAN>" & varNumeroIBAN & "</reg:IBAN>" & vbCrLf _
                                          & "<reg:CodiceFiscaleTitolareCC>" & row1.Item("cod_fiscale_titolare_cc").ToString & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
                                          & "</loc:Versamento>" & vbCrLf _
                                             & "<loc:ImportoDaVersare>" & Format(CDec(row1.Item("importo_da_versare")), "0.00") & "</loc:ImportoDaVersare>" & vbCrLf _
                                             & "</loc:DatiGenerali>" & vbCrLf

                        testoXML &= "<loc:Documento identificativo=" & Chr(34) & row1.Item("num_identificativo_doc").ToString & Chr(34) & "><loc:Frontespizio>" & vbCrLf _
                        & "<loc:IdentificativoProdSoftware>SEPA2009</loc:IdentificativoProdSoftware>" & vbCrLf _
                        & "<loc:NumeroModuliCompilati>1</loc:NumeroModuliCompilati>" & vbCrLf _
                        & "<loc:IDContratto>" & row1.Item("id_contratto").ToString & "</loc:IDContratto>" & vbCrLf _
                        & "<loc:Richiedente>" & vbCrLf _
                        & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_Richiedente").ToString & "</loc:CodiceFiscale>" & vbCrLf

                        'If persGiuridicaRich = True Then
                        testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
                        & "<loc:Denominazione>" & row1.Item("denominazione_richiedente").ToString & "</loc:Denominazione>" & vbCrLf _
                        & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
                        & "<loc:Rappresentante>" & vbCrLf _
                        & "<loc:CodiceFiscaleRappresentante>" & row1.Item("cod_fiscale_rappresentante").ToString & "</loc:CodiceFiscaleRappresentante>" & vbCrLf _
                        & "<loc:CognomeRappresentante>" & row1.Item("cognome_rappresentante").ToString & "</loc:CognomeRappresentante>" & vbCrLf _
                        & "<loc:NomeRappresentante>" & row1.Item("nome_rappresentante").ToString & "</loc:NomeRappresentante>" & vbCrLf _
                        & "<loc:CodiceCarica>" & row1.Item("codice_carica").ToString & "</loc:CodiceCarica>" & vbCrLf _
                        & "</loc:Rappresentante>" & vbCrLf
                        'Else
                        '    testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
                        '    & "<loc:Cognome>" & varCognomeRich & "</loc:Cognome>" & vbCrLf _
                        '    & "<loc:Nome>" & varNome & "</loc:Nome>" & vbCrLf _
                        '    & "</loc:PersoneFisiche>" & vbCrLf
                        'End If

                        testoXML &= "<loc:Firma>" & row1.Item("firma").ToString & "</loc:Firma>" & vbCrLf _
                                & "<loc:TipologiaRichiedente>1</loc:TipologiaRichiedente>" & vbCrLf
                        If row1.Item("subentro").ToString <> "0" Then
                            testoXML &= "<loc:Subentro>" & row1.Item("subentro").ToString & "</loc:Subentro>" & vbCrLf
                        End If
                        testoXML &= "</loc:Richiedente>" & vbCrLf


                        testoXML &= "<loc:TipologiaContratto>" & row1.Item("tipologia_contratto").ToString & "</loc:TipologiaContratto>" & vbCrLf _
                       & "<loc:DataInizio>" & Replace(par.FormattaData(row1.Item("data_inizio_contratto").ToString), "/", "") & "</loc:DataInizio>" & vbCrLf _
                       & "<loc:DataFine>" & Replace(par.FormattaData(row1.Item("data_fine_contratto").ToString), "/", "") & "</loc:DataFine>" & vbCrLf _
                       & "<loc:ImportoCanone>" & CStr(row1.Item("importo_canone").ToString) & "</loc:ImportoCanone>" & vbCrLf

                        If row1.Item("pagamento_intera_durata_ru").ToString = "1" Then
                            testoXML &= "<loc:PagamentoInteraDurataContratto>" & row1.Item("pagamento_intera_durata_ru").ToString & "</loc:PagamentoInteraDurataContratto>" & vbCrLf

                        End If

                        testoXML &= "<loc:Importi>" & vbCrLf _
                        & "<loc:Imposte>" & vbCrLf _
                        & "<loc:ImpostaDiRegistro>" & row1.Item("imposta_registro").ToString & "</loc:ImpostaDiRegistro>" & vbCrLf _
                        & "<loc:ImpostaDiBollo>" & row1.Item("imposta_bollo").ToString & "</loc:ImpostaDiBollo>" & vbCrLf _
                        & "</loc:Imposte>" & vbCrLf

                        If row1.Item("sanzione_imp_registro").ToString <> "0" Then
                            testoXML &= "<loc:Sanzioni>" & vbCrLf _
                        & "<loc:ImpostaDiRegistro>" & row1.Item("sanzione_imp_registro").ToString & "</loc:ImpostaDiRegistro>" & vbCrLf
                            If row1.Item("sanzione_imp_bollo").ToString <> "0" Then
                                testoXML &= "<loc:ImpostaDiBollo>" & row1.Item("sanzione_imp_bollo").ToString & "</loc:ImpostaDiBollo>" & vbCrLf
                            End If
                            testoXML &= "</loc:Sanzioni>" & vbCrLf
                        End If

                        If row1.Item("tot_interessi") <> 0 Then
                            testoXML &= "<loc:Interessi>" & vbCrLf _
                        & "<loc:ImpostaDiRegistro>" & Format(CDec(row1.Item("tot_interessi").ToString), "0.00") & "</loc:ImpostaDiRegistro>" & vbCrLf

                            testoXML &= "</loc:Interessi>" & vbCrLf
                        End If
                        testoXML &= "</loc:Importi>" & vbCrLf


                        testoXML &= "<loc:Registrazione>" & vbCrLf _
                            & "<loc:Contratto>" & vbCrLf _
                            & "<loc:DataStipula>" & Replace(par.FormattaData(row1.Item("data_stipula").ToString), "/", "") & "</loc:DataStipula>" & vbCrLf _
                            & "<loc:NumeroPagine>" & row1.Item("numero_pagine").ToString & "</loc:NumeroPagine>" & vbCrLf _
                            & "<loc:NumeroCopie>" & row1.Item("numero_copie").ToString & "</loc:NumeroCopie>" & vbCrLf _
                        & "</loc:Contratto>" & vbCrLf

                        varPdfBase64 = par.ConversioneBase64(row1.Item("id_contratto").ToString)

                        testoXML &= "<loc:Allegati>" & vbCrLf _
                            & "<reg:FileType>application/pdf</reg:FileType>" & vbCrLf _
                            & "<reg:FileName>" & row1.Item("id_contratto").ToString & Format(Now, "yyyyMMdd") & "</reg:FileName>" & vbCrLf _
                            & "<reg:FileDescription>STAMPACONTRATTO</reg:FileDescription>" & vbCrLf _
                            & "<reg:ImageData>" & varPdfBase64 & "</reg:ImageData></loc:Allegati>"

                        testoXML &= "</loc:Registrazione>" & vbCrLf

                        varImpegnoATrasmettere = "<loc:ImpegnoATrasmettere>" & vbCrLf _
                            & " <reg:CFintermediario>" & row1.Item("cf_intermediario").ToString & "</reg:CFintermediario>" & vbCrLf _
                            & "<reg:ImpegnoATrasmettere>1</reg:ImpegnoATrasmettere>" & vbCrLf _
                            & " <reg:DataImpegno>" & Replace(Format(Now, "dd/MM/yyyy"), "/", "") & "</reg:DataImpegno>" & vbCrLf _
                            & " <reg:FirmaIntermediario>1</reg:FirmaIntermediario>" & vbCrLf _
                            & "</loc:ImpegnoATrasmettere>" & vbCrLf _
                        & " <loc:DataInvio>" & Replace(Format(Now, "dd/MM/yyyy"), "/", "") & "</loc:DataInvio>" & vbCrLf
                        testoXML &= varImpegnoATrasmettere & "</loc:Frontespizio>" & vbCrLf


                        testoXML &= "<loc:Soggetti>" & vbCrLf _
                                & "<loc:PrimoModulo>" & vbCrLf _
                                & "<loc:Locatore>" & vbCrLf _
                                & "<loc:NumeroProgressivo>" & row1.Item("num_progressivo_locatore").ToString & "</loc:NumeroProgressivo>" & vbCrLf _
                                & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_locatore").ToString & "</loc:CodiceFiscale>" & vbCrLf _
                                & "<loc:SoggettivitaIVA>" & row1.Item("soggettivita_iva_locatore").ToString & "</loc:SoggettivitaIVA>" & vbCrLf _
                                & "<loc:SoggettiDiversiDaPF>" & vbCrLf _
                                & "<loc:Denominazione>" & row1.Item("denominazione_locatore").ToString.ToUpper & "</loc:Denominazione>" & vbCrLf _
                                & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
                                & "</loc:Locatore>" & vbCrLf


                        testoXML &= "<loc:Conduttore>" & vbCrLf _
                                    & "<loc:NumeroProgressivo>" & row1.Item("num_progressivo_conduttore").ToString & "</loc:NumeroProgressivo>" & vbCrLf _
                                    & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_conduttore").ToString & "</loc:CodiceFiscale>" & vbCrLf _
                                    & "<loc:SoggettivitaIVA>" & row1.Item("soggettivita_iva_conduttore").ToString & "</loc:SoggettivitaIVA>" & vbCrLf

                        If row1.Item("comune_nascita_conduttore").ToString <> "" Then
                            testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
                                & "<sc:Cognome>" & row1.Item("cognome_conduttore").ToString & "</sc:Cognome>" & vbCrLf _
                                & "<sc:Nome>" & row1.Item("nome_conduttore").ToString & "</sc:Nome>" & vbCrLf _
                                & "<sc:Sesso>" & row1.Item("sesso_conduttore").ToString & "</sc:Sesso>" & vbCrLf _
                                & "<sc:DataNascita>" & Replace(par.FormattaData(row1.Item("data_nascita_conduttore").ToString), "/", "") & "</sc:DataNascita>" & vbCrLf _
                                & "<sc:ComuneNascita>" & row1.Item("comune_nascita_conduttore").ToString & "</sc:ComuneNascita>" & vbCrLf


                            testoXML &= "<sc:ProvinciaNascita>" & row1.Item("provincia_nascita_conduttore").ToString & "</sc:ProvinciaNascita>" & vbCrLf

                            testoXML &= "</loc:PersoneFisiche>" & vbCrLf


                        Else
                            testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
                                & "<loc:Denominazione>" & row1.Item("denominazione_conduttore").ToString & "</loc:Denominazione>" & vbCrLf _
                                & "</loc:SoggettiDiversiDaPF>" & vbCrLf


                        End If
                        testoXML &= "<loc:TipologiaConduttore>3</loc:TipologiaConduttore></loc:Conduttore></loc:PrimoModulo>" & vbCrLf


                        testoXML &= "</loc:Soggetti>" & vbCrLf

                        testoXML &= "<loc:Immobili>" & vbCrLf _
                            & "<loc:PrimoModulo>" & vbCrLf

                        testoXML &= "<loc:Immobile>" & vbCrLf _
                                    & "<loc:NumeroProgressivoImmobile>" & row1.Item("num_progressivo").ToString & "</loc:NumeroProgressivoImmobile>" & vbCrLf _
                                    & "<loc:TipologiaImmobile>" & row1.Item("tipologia_immobile").ToString & "</loc:TipologiaImmobile>" & vbCrLf _
                                    & "<loc:CodiceComuneCatastale>" & row1.Item("codice_comune_catastale").ToString & "</loc:CodiceComuneCatastale>" & vbCrLf _
                                    & "<loc:TipoCatasto>" & row1.Item("tipo_catasto").ToString & "</loc:TipoCatasto>" & vbCrLf _
                                    & "<loc:PorzioneImmobile>" & row1.Item("porzione_immobile").ToString & "</loc:PorzioneImmobile>" & vbCrLf

                        If row1.Item("rendita_Catastale").ToString <> "" Then
                            If row1.Item("tipologia_immobile").ToString <> "2" Then
                                If row1.Item("subalterno").ToString <> "" Then
                                    testoXML &= "<loc:DatiCatastali>" & vbCrLf _
                                        & "<reg:Foglio>" & row1.Item("foglio").ToString & "</reg:Foglio>" & vbCrLf _
                                        & "<reg:Particella>" & row1.Item("particella").ToString & "</reg:Particella>" & vbCrLf


                                    If Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) <> "    " Then
                                        testoXML &= "<reg:Subalterno>" & Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) & "</reg:Subalterno>" & vbCrLf
                                    End If

                                    testoXML &= "</loc:DatiCatastali>" & vbCrLf
                                Else
                                    testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
                                End If
                            Else
                                testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
                            End If

                            testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
                                & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

                            If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
                                If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
                                    categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
                                Else
                                    categCatast = row1.Item("categoria_catastale").ToString
                                End If
                                testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
                            End If
                           testoXML &= "<loc:RenditaCatastale>" & Format(par.IfNull(row1.Item("rendita_Catastale"), 0), "0.00") & "</loc:RenditaCatastale>" & vbCrLf
                        Else
                            testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
                            testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
                                & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

                            If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
                                If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
                                    categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
                                End If
                                testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
                            End If
                            testoXML &= "<loc:RenditaCatastale>" & Format(par.IfNull(row1.Item("rendita_Catastale"), 0), "0.00") & "</loc:RenditaCatastale>" & vbCrLf
                        End If
                        testoXML &= "<loc:TipologiaIndirizzo>" & row1.Item("tipologia_indirizzo").ToString & "</loc:TipologiaIndirizzo>" & vbCrLf _
                            & "<loc:Indirizzo>" & row1.Item("indirizzo").ToString & "</loc:Indirizzo>" & vbCrLf _
                            & "<loc:NumCivico>" & Replace(RTrim(LTrim(row1.Item("num_civico").ToString)), "/", "") & "</loc:NumCivico>" & vbCrLf _
                            & "</loc:Immobile>" & vbCrLf
                      

                    Else
                        testoXML &= "<loc:Immobile>" & vbCrLf _
                                    & "<loc:NumeroProgressivoImmobile>" & row1.Item("num_progressivo").ToString & "</loc:NumeroProgressivoImmobile>" & vbCrLf _
                                    & "<loc:TipologiaImmobile>" & row1.Item("tipologia_immobile").ToString & "</loc:TipologiaImmobile>" & vbCrLf _
                                    & "<loc:CodiceComuneCatastale>" & row1.Item("codice_comune_catastale").ToString & "</loc:CodiceComuneCatastale>" & vbCrLf _
                                    & "<loc:TipoCatasto>" & row1.Item("tipo_catasto").ToString & "</loc:TipoCatasto>" & vbCrLf _
                                    & "<loc:PorzioneImmobile>" & row1.Item("porzione_immobile").ToString & "</loc:PorzioneImmobile>" & vbCrLf

                        If row1.Item("rendita_Catastale").ToString <> "" Then
                            If row1.Item("tipologia_immobile").ToString <> "2" Then
                                If row1.Item("subalterno").ToString <> "" Then
                                    testoXML &= "<loc:DatiCatastali>" & vbCrLf _
                                        & "<reg:Foglio>" & row1.Item("foglio").ToString & "</reg:Foglio>" & vbCrLf _
                                        & "<reg:Particella>" & row1.Item("particella").ToString & "</reg:Particella>" & vbCrLf


                                    If Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) <> "    " Then
                                        testoXML &= "<reg:Subalterno>" & Mid(par.IfNull(row1.Item("subalterno"), "    "), 1, 4) & "</reg:Subalterno>" & vbCrLf
                                    End If

                                    testoXML &= "</loc:DatiCatastali>" & vbCrLf
                                Else
                                    testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
                                End If
                            Else
                                testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
                            End If

                            testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
                                & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

                            If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
                                If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
                                    categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
                                Else
                                    categCatast = row1.Item("categoria_catastale").ToString
                                End If
                                testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
                            End If
                            testoXML &= "<loc:RenditaCatastale>" & Format(par.IfNull(row1.Item("rendita_Catastale"), 0), "0.00") & "</loc:RenditaCatastale>" & vbCrLf
                        Else
                            testoXML &= "<loc:InViaDiAccatastamento>1</loc:InViaDiAccatastamento>" & vbCrLf
                            testoXML &= "<loc:Comune>" & row1.Item("comune").ToString & "</loc:Comune>" & vbCrLf _
                                & "<loc:Provincia>" & row1.Item("provincia").ToString & "</loc:Provincia>" & vbCrLf

                            If row1.Item("categoria_catastale").ToString <> "" And row1.Item("categoria_catastale").ToString <> "000" Then
                                If Mid(row1.Item("categoria_catastale").ToString, 2, 1) = "0" Then
                                    categCatast = Replace(row1.Item("categoria_catastale").ToString, Mid(row1.Item("categoria_catastale").ToString, 2, 1), "")
                                Else
                                    categCatast = row1.Item("categoria_catastale").ToString
                                End If
                                testoXML &= "<loc:CategoriaCatastale>" & categCatast & "</loc:CategoriaCatastale>" & vbCrLf
								
                            End If
							testoXML &= "<loc:RenditaCatastale>" & Format(par.IfNull(row1.Item("rendita_Catastale"), 0), "0.00") & "</loc:RenditaCatastale>" & vbCrLf
                        End If
                        testoXML &= "<loc:TipologiaIndirizzo>" & row1.Item("tipologia_indirizzo").ToString & "</loc:TipologiaIndirizzo>" & vbCrLf _
                            & "<loc:Indirizzo>" & row1.Item("indirizzo").ToString & "</loc:Indirizzo>" & vbCrLf _
                            & "<loc:NumCivico>" & Replace(RTrim(LTrim(row1.Item("num_civico").ToString)), "/", "") & "</loc:NumCivico>" & vbCrLf _
                            & "</loc:Immobile>" & vbCrLf


                    End If
                Next

                    testoXML &= "</loc:PrimoModulo>" & vbCrLf _
                         & "</loc:Immobili>" & vbCrLf

                If dtDatiG.Rows(0).Item("tipologia_contratto").ToString = "L1" Or dtDatiG.Rows(0).Item("tipologia_contratto").ToString = "L2" Then
                    testoXML &= "<loc:Relazioni>" & vbCrLf _
                               & "<loc:PrimoModulo>" & vbCrLf
                    For k As Integer = 1 To dtDatiG.Rows.Count

                        testoXML &= "<loc:LocazioneUsoAbitativo>" & vbCrLf _
                           & "<loc:ImmobileNumero>" & k & "</loc:ImmobileNumero>" & vbCrLf _
                           & "<loc:LocatoreNumero>1</loc:LocatoreNumero>" & vbCrLf _
                           & "<loc:PercentualePossesso>100,00</loc:PercentualePossesso>" & vbCrLf _
                           & "<loc:Cedolare>N</loc:Cedolare>" & vbCrLf _
                           & "</loc:LocazioneUsoAbitativo>" & vbCrLf


                    Next
                    testoXML &= "</loc:PrimoModulo>" & vbCrLf _
                         & "</loc:Relazioni>" & vbCrLf
                End If

                testoXML &= "</loc:Documento>" & vbCrLf

                    testoXML = testoXML & "</loc:Fornitura>"

                    sr.WriteLine(testoXML)
                    sr.Close()

            End If

            
            If i > 0 Then
                Dim NomeFilezip As String = "REG_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip)
                Dim kkK As Integer = 0

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)

                Dim strFile As String

                'For kkK = 0 To 1
                    strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".xml")
                    Dim strmFile As FileStream = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    '
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
                    File.Delete(strFile)
                'Next
                par.cmd.CommandText = "update siscom_mi.dati_generali_rli set nome_file_xml='" & NomeFilezip & "-" & ElencoFile(kkK) & ".xml' where id=" & idFornitura.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update siscom_mi.rapporti_utenza_imposte set file_scaricato='" & NomeFilezip & "-" & ElencoFile(kkK) & ".xml' where file_scaricato='" & par.IfNull(dtDatiG.Rows(0).Item("nome_file_xml"), "") & "' and id_contratto=" & par.IfNull(dtDatiG.Rows(0).Item("ID_contratto"), 0)
                par.cmd.ExecuteNonQuery()

                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                connData.chiudi(True)

                Response.Redirect("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip, False)
            Else
                ' Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Contratti - CreaXML - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub SettaFunzioniJS()

       
        Dim i As Integer = 0
        Dim di As GridDataItem
        For i = 0 To RadGridRLI.Items.Count - 1
            di = Me.RadGridRLI.Items(i)

            CType(di.Cells(2).FindControl("rendita_Catastale"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);CalcolaTotale(this);")
            CType(di.Cells(2).FindControl("rendita_Catastale"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
            CType(di.Cells(2).FindControl("rendita_Catastale"), RadTextBox).Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
        Next

        

    End Sub

    Private Sub CreaXML2()
        Try
            connData.apri(True)
            Dim categCatast As String = ""
            Dim varPdfBase64 As String = ""
            Dim varImpegnoATrasmettere As String = ""
            par.cmd.CommandText = "select * from siscom_mi.dati_generali_rli where dati_generali_rli.NOME_FILE_XML='" & nome_file_xml.Value & "' order by id asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDatiG As New Data.DataTable
            da.Fill(dtDatiG)
            da.Dispose()
            Dim zipfic As String = ""
            Dim testoXML As String = ""
            Dim NomeFile As String = ""
            Dim strScript As String = ""
            Dim varNumeroIBAN As String = ""
            Dim ElencoFile() As String
            Dim i As Integer = 0
            If dtDatiG.Rows.Count > 0 Then

                NomeFile = par.IfNull(dtDatiG.Rows(0).Item("ufficio_territoriale"), "") & "_" & Format(Now, "yyyyMMddHHmmss")
                ReDim Preserve ElencoFile(0)

                ElencoFile(0) = NomeFile

                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFile & ".xml"), False)

                varNumeroIBAN = ClasseIban.CalcolaIBAN("IT", dtDatiG.Rows(0).Item("cin").ToString & dtDatiG.Rows(0).Item("codice_Abi").ToString & dtDatiG.Rows(0).Item("codice_cab").ToString & dtDatiG.Rows(0).Item("numero_conto_corrente").ToString)


                testoXML = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>" & vbCrLf _
                                             & "<!--created with SEPA@Web (www.sistemiesoluzionisrl.it)-->" & vbCrLf _
                                             & "<loc:Fornitura xmlns:sc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:common:v2" & Chr(34) & vbCrLf _
                                             & "xmlns:loc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:loc:v1" & Chr(34) & vbCrLf _
                                             & "xmlns:reg=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:reg:v1" & Chr(34) & " > " & vbCrLf _
                                             & "<loc:Intestazione>" & vbCrLf _
                                             & "<loc:CodiceFornitura>" & dtDatiG.Rows(0).Item("codice_fornitura").ToString & "</loc:CodiceFornitura>" & vbCrLf _
                                             & "<loc:TipoFornitore>" & dtDatiG.Rows(0).Item("tipo_fornitore").ToString & "</loc:TipoFornitore>" & vbCrLf _
                                             & "<loc:CodiceFiscaleFornitore>" & dtDatiG.Rows(0).Item("cod_fiscale_fornitore").ToString & "</loc:CodiceFiscaleFornitore>" & vbCrLf _
                                             & "</loc:Intestazione>" & vbCrLf _
                                             & "<loc:DatiGenerali>" & vbCrLf _
                                             & "<loc:Versamento>" & vbCrLf _
                                          & "<reg:IBAN>" & varNumeroIBAN & "</reg:IBAN>" & vbCrLf _
                                          & "<reg:CodiceFiscaleTitolareCC>" & dtDatiG.Rows(0).Item("cod_fiscale_titolare_cc").ToString & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
                                          & "</loc:Versamento>" & vbCrLf _
                                             & "<loc:ImportoDaVersare>" & dtDatiG.Rows(0).Item("importo_da_versare").ToString & "</loc:ImportoDaVersare>" & vbCrLf _
                                             & "</loc:DatiGenerali>" & vbCrLf

                For Each row1 As Data.DataRow In dtDatiG.Rows

                    i = i + 1

                    testoXML &= "<loc:Documento identificativo=" & Chr(34) & row1.Item("num_identificativo_doc").ToString & Chr(34) & "><loc:Frontespizio>" & vbCrLf _
                    & "<loc:IdentificativoProdSoftware>SEPA2009</loc:IdentificativoProdSoftware>" & vbCrLf _
                    & "<loc:NumeroModuliCompilati>1</loc:NumeroModuliCompilati>" & vbCrLf _
                    & "<loc:IDContratto>" & row1.Item("id_contratto").ToString & "</loc:IDContratto>" & vbCrLf _
                    & "<loc:Richiedente>" & vbCrLf _
                    & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_Richiedente").ToString & "</loc:CodiceFiscale>" & vbCrLf

                    'If persGiuridicaRich = True Then
                    testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
                    & "<loc:Denominazione>" & row1.Item("denominazione_richiedente").ToString & "</loc:Denominazione>" & vbCrLf _
                    & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
                    & "<loc:Rappresentante>" & vbCrLf _
                    & "<loc:CodiceFiscaleRappresentante>" & row1.Item("cod_fiscale_rappresentante").ToString & "</loc:CodiceFiscaleRappresentante>" & vbCrLf _
                    & "<loc:CognomeRappresentante>" & row1.Item("cognome_rappresentante").ToString & "</loc:CognomeRappresentante>" & vbCrLf _
                    & "<loc:NomeRappresentante>" & row1.Item("nome_rappresentante").ToString & "</loc:NomeRappresentante>" & vbCrLf _
                    & "<loc:CodiceCarica>" & row1.Item("codice_carica").ToString & "</loc:CodiceCarica>" & vbCrLf _
                    & "</loc:Rappresentante>" & vbCrLf
                    'Else
                    '    testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
                    '    & "<loc:Cognome>" & varCognomeRich & "</loc:Cognome>" & vbCrLf _
                    '    & "<loc:Nome>" & varNome & "</loc:Nome>" & vbCrLf _
                    '    & "</loc:PersoneFisiche>" & vbCrLf
                    'End If

                    testoXML &= "<loc:Firma>" & row1.Item("firma").ToString & "</loc:Firma>" & vbCrLf _
                            & "<loc:TipologiaRichiedente>1</loc:TipologiaRichiedente>" & vbCrLf
                    If row1.Item("subentro").ToString <> "0" Then
                        testoXML &= "<loc:Subentro>" & row1.Item("subentro").ToString & "</loc:Subentro>" & vbCrLf
                    End If

                    testoXML &= "</loc:Richiedente>" & vbCrLf

                    testoXML &= "<loc:Importi>" & vbCrLf _
                        & "<loc:Imposte>" & vbCrLf _
                        & "<loc:ImpostaDiRegistro>" & row1.Item("imposta_registro").ToString & "</loc:ImpostaDiRegistro>" & vbCrLf _
                        & "<loc:ImpostaDiBollo>" & row1.Item("imposta_bollo").ToString & "</loc:ImpostaDiBollo>" & vbCrLf _
                        & "</loc:Imposte>" & vbCrLf

                    If row1.Item("sanzione_imp_registro").ToString <> "0" Then
                        testoXML &= "<loc:Sanzioni>" & vbCrLf _
                        & "<loc:ImpostaDiRegistro>" & row1.Item("sanzione_imp_registro").ToString & "</loc:ImpostaDiRegistro>" & vbCrLf
                        If row1.Item("sanzione_imp_bollo").ToString <> "0" Then
                            testoXML &= "<loc:ImpostaDiBollo>" & row1.Item("sanzione_imp_bollo").ToString & "</loc:ImpostaDiBollo>" & vbCrLf
                        End If
                        testoXML &= "</loc:Sanzioni>" & vbCrLf
                    End If

                    If row1.Item("tot_interessi") <> 0 Then
                        testoXML &= "<loc:Interessi>" & vbCrLf _
                        & "<loc:ImpostaDiRegistro>" & Format(CDec(row1.Item("tot_interessi").ToString), "0.00") & "</loc:ImpostaDiRegistro>" & vbCrLf

                        testoXML &= "</loc:Interessi>" & vbCrLf
                    End If
                    testoXML &= "</loc:Importi>" & vbCrLf


                    testoXML &= "<loc:AdempSucc>" & vbCrLf _
                           & "<loc:TipologiaAdempimento>" & row1.Item("tipologia_adempimento").ToString & "</loc:TipologiaAdempimento>" & vbCrLf

                    If row1.Item("tipologia_contratto").ToString = "L1" Or row1.Item("tipologia_contratto").ToString = "L2" Then
                        testoXML &= "<loc:CedolareSecca>3</loc:CedolareSecca>" & vbCrLf
                    End If

                    Select Case row1.Item("tipologia_adempimento").ToString
                        Case "1"
                            testoXML &= "<loc:Annualita>" & row1.Item("annualita").ToString & "</loc:Annualita>" & vbCrLf
                        Case Else
                            testoXML &= "<loc:DataFine>" & Replace(par.FormattaData(row1.Item("data_fine").ToString), "/", "") & "</loc:DataFine>" & vbCrLf
                    End Select

                    testoXML &= "<loc:Registrazione>" & vbCrLf _
                        & "<loc:UfficioDiRegistrazione>" & row1.Item("ufficio_registrazione").ToString & "</loc:UfficioDiRegistrazione>" & vbCrLf _
                        & "<loc:Anno>" & row1.Item("anno_registrazione").ToString & "</loc:Anno>" & vbCrLf _
                        & "<loc:Serie>" & row1.Item("serie_registrazione").ToString & "</loc:Serie>" & vbCrLf _
                        & "<loc:NumeroRegistrazione>" & row1.Item("numero_registrazione").ToString & "</loc:NumeroRegistrazione>" & vbCrLf _
                        & "</loc:Registrazione>" & vbCrLf

                    If row1.Item("tipologia_contratto").ToString = "L1" Or row1.Item("tipologia_contratto").ToString = "L2" Then
                        If row1.Item("tipologia_adempimento").ToString = 2 Then
                            testoXML &= "<loc:TipologiaProroga>1</loc:TipologiaProroga>" & vbCrLf
                        End If
                    End If

                    Dim varTipoSubentro As String = ""

                    par.cmd.CommandText = "Select * from domande_bando_vsa where id_motivo_domanda In (1,6) And fl_autorizzazione=1 And contratto_num=(select cod_contratto from siscom_mi.rapporti_utenza " _
                        & " where id= " & row1.Item("id_contratto").ToString & ")"
                    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderT.Read Then
                            If par.IfNull(myReaderT("id_causale_domanda"), "") = 8 Then
                                varTipoSubentro = "1"
                            Else
                                varTipoSubentro = "6"
                            End If
                        End If
                        myReaderT.Close()



                    If row1.Item("tipologia_adempimento").ToString = 6 Then
                        testoXML &= "<loc:TipologiaSubentro>" & varTipoSubentro & "</loc:TipologiaSubentro>" & vbCrLf
                    End If

                    testoXML &= "</loc:AdempSucc>" & vbCrLf




                    varImpegnoATrasmettere = "<loc:ImpegnoATrasmettere>" & vbCrLf _
                                             & " <reg:CFintermediario>" & row1.Item("cf_intermediario").ToString & "</reg:CFintermediario>" & vbCrLf _
                        & "<reg:ImpegnoATrasmettere>1</reg:ImpegnoATrasmettere>" & vbCrLf _
                        & " <reg:DataImpegno>" & Replace(Format(Now, "dd/MM/yyyy"), "/", "") & "</reg:DataImpegno>" & vbCrLf _
                        & " <reg:FirmaIntermediario>1</reg:FirmaIntermediario>" & vbCrLf _
                        & "</loc:ImpegnoATrasmettere>" & vbCrLf _
                                            & " <loc:DataInvio>" & Replace(Format(Now, "dd/MM/yyyy"), "/", "") & "</loc:DataInvio>" & vbCrLf

                    testoXML &= varImpegnoATrasmettere & "</loc:Frontespizio>" & vbCrLf

                    If row1.Item("cod_fiscale_conduttore").ToString <> "" Then
                        testoXML &= "<loc:Conduttore>" & vbCrLf _
                                    & "<loc:NumeroProgressivo>" & row1.Item("num_progressivo_conduttore").ToString & "</loc:NumeroProgressivo>" & vbCrLf _
                                    & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_conduttore").ToString & "</loc:CodiceFiscale>" & vbCrLf

                        If row1.Item("comune_nascita_conduttore").ToString <> "" Then
                            testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
                                & "<sc:Cognome>" & row1.Item("cognome_conduttore").ToString & "</sc:Cognome>" & vbCrLf _
                                & "<sc:Nome>" & row1.Item("nome_conduttore").ToString & "</sc:Nome>" & vbCrLf _
                                & "<sc:Sesso>" & row1.Item("sesso_conduttore").ToString & "</sc:Sesso>" & vbCrLf _
                                & "<sc:DataNascita>" & Replace(par.FormattaData(row1.Item("data_nascita_conduttore").ToString), "/", "") & "</sc:DataNascita>" & vbCrLf _
                                & "<sc:ComuneNascita>" & row1.Item("comune_nascita_conduttore").ToString & "</sc:ComuneNascita>" & vbCrLf


                            testoXML &= "<sc:ProvinciaNascita>" & row1.Item("provincia_nascita_conduttore").ToString & "</sc:ProvinciaNascita>" & vbCrLf

                            testoXML &= "</loc:PersoneFisiche>" & vbCrLf _
                                 & "<loc:Cedente>1</loc:Cedente>" & vbCrLf

                        End If
                        If row1.Item("denominazione_conduttore").ToString <> "" Then
                            testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
                                & "<loc:Denominazione>" & row1.Item("denominazione_conduttore").ToString & "</loc:Denominazione>" & vbCrLf _
                                & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
                                 & "<loc:Cedente>1</loc:Cedente>" & vbCrLf


                        End If
                        testoXML &= "<loc:TipologiaConduttore>3</loc:TipologiaConduttore></loc:Conduttore>" & vbCrLf
                        If row1.Item("cod_fiscale_conduttore_2").ToString <> "" Then
                            testoXML &= "<loc:Conduttore>" & vbCrLf _
                                        & "<loc:NumeroProgressivo>" & row1.Item("num_progressivo_conduttore_2").ToString & "</loc:NumeroProgressivo>" & vbCrLf _
                                        & "<loc:CodiceFiscale>" & row1.Item("cod_fiscale_conduttore_2").ToString & "</loc:CodiceFiscale>" & vbCrLf


                            If row1.Item("comune_nascita_conduttore_2").ToString <> "" Then
                                testoXML &= "<loc:PersoneFisiche>" & vbCrLf _
                                    & "<sc:Cognome>" & row1.Item("cognome_conduttore_2").ToString & "</sc:Cognome>" & vbCrLf _
                                    & "<sc:Nome>" & row1.Item("nome_conduttore_2").ToString & "</sc:Nome>" & vbCrLf _
                                    & "<sc:Sesso>" & row1.Item("sesso_conduttore_2").ToString & "</sc:Sesso>" & vbCrLf _
                                    & "<sc:DataNascita>" & Replace(par.FormattaData(row1.Item("data_nascita_conduttore_2").ToString), "/", "") & "</sc:DataNascita>" & vbCrLf _
                                    & "<sc:ComuneNascita>" & row1.Item("comune_nascita_conduttore_2").ToString & "</sc:ComuneNascita>" & vbCrLf


                                testoXML &= "<sc:ProvinciaNascita>" & row1.Item("provincia_nascita_conduttore_2").ToString & "</sc:ProvinciaNascita>" & vbCrLf

                                testoXML &= "</loc:PersoneFisiche>" & vbCrLf _
                                     & "<loc:Cedente>2</loc:Cedente>" & vbCrLf

                            End If

                            If row1.Item("denominazione_conduttore_2").ToString <> "" Then
                                testoXML &= "<loc:SoggettiDiversiDaPF>" & vbCrLf _
                                    & "<loc:Denominazione>" & row1.Item("denominazione_conduttore_2").ToString & "</loc:Denominazione>" & vbCrLf _
                                    & "</loc:SoggettiDiversiDaPF>" & vbCrLf _
                                     & "<loc:Cedente>2</loc:Cedente>" & vbCrLf


                            End If


                        End If
                        testoXML &= "<loc:TipologiaConduttore>3</loc:TipologiaConduttore></loc:Conduttore></loc:PrimoModulo>" & vbCrLf _
                        & "</loc:Soggetti>" & vbCrLf
                    End If

                    


                    testoXML &= "</loc:Documento>" & vbCrLf
                Next


                testoXML = testoXML & "</loc:Fornitura>"

                sr.WriteLine(testoXML)
                sr.Close()
            End If

            connData.chiudi(True)

            If i > 0 Then
                Dim NomeFilezip As String = "SUC_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFilezip)
                Dim kkK As Integer = 0

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)

                Dim strFile As String

                'For kkK = 0 To 1
                strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & ElencoFile(kkK) & ".xml")
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                '
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
                File.Delete(strFile)
                'Next

                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                Response.Redirect("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFilezip, False)
            Else
                ' Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
            End If




        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Contratti - CreaXML - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridRLI_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridRLI.ItemDataBound
        Try
            'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            '    Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            '    e.Item.Attributes.Add("onclick", "document.getElementById('idFornitura').value='" & dataItem("ID").Text & "';")
            'End If
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            Select Case e.Item.OwnerTableView.Name
                Case "Dettagli"
                    If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                        Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)

                        CType(userControl.FindControl("rendita_Catastale"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                        CType(userControl.FindControl("rendita_Catastale"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
                    End If
                Case Else
                    If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                        If e.Item.DataItem.GetType.Name = "DataRowView" Then



                            CType(e.Item.FindControl("DATA_INIZIO_CONTRATTO"), RadTextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                            CType(e.Item.FindControl("DATA_FINE_CONTRATTO"), RadTextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                            CType(e.Item.FindControl("DATA_STIPULA"), RadTextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                            CType(e.Item.FindControl("DATA_NASCITA_CONDUTTORE"), RadTextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")



                            CType(e.Item.FindControl("IMPORTO_DA_VERSARE"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                            CType(e.Item.FindControl("IMPORTO_DA_VERSARE"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")



                            CType(e.Item.FindControl("IMPOSTA_REGISTRO"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                            CType(e.Item.FindControl("IMPOSTA_REGISTRO"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


                            CType(e.Item.FindControl("IMPOSTA_BOLLO"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                            CType(e.Item.FindControl("IMPOSTA_BOLLO"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


                            CType(e.Item.FindControl("SANZIONE_IMP_REGISTRO"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                            CType(e.Item.FindControl("SANZIONE_IMP_REGISTRO"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


                            CType(e.Item.FindControl("SANZIONE_IMP_BOLLO"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                            CType(e.Item.FindControl("SANZIONE_IMP_BOLLO"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


                            CType(e.Item.FindControl("IMPORTO_CANONE"), RadTextBox).Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                            CType(e.Item.FindControl("IMPORTO_CANONE"), RadTextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")



                            If tipoAdempimento.Value = 0 Then
                                CType(e.Item.FindControl("ANNUALITA"), RadTextBox).Enabled = False
                                CType(e.Item.FindControl("DATA_FINE"), RadTextBox).Enabled = False
                                CType(e.Item.FindControl("UFFICIO_REGISTRAZIONE"), RadTextBox).Enabled = False
                                CType(e.Item.FindControl("ANNO_REGISTRAZIONE"), RadTextBox).Enabled = False
                                CType(e.Item.FindControl("SERIE_REGISTRAZIONE"), RadTextBox).Enabled = False
                                CType(e.Item.FindControl("NUMERO_REGISTRAZIONE"), RadTextBox).Enabled = False

                                CType(e.Item.FindControl("LBL_COD_FISCALE_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_COGNOME_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_NOME_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_DENOMINAZIONE_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_SESSO_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_DATA_NASCITA_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_COMUNE_NASCITA_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_PROVINCIA_NASCITA_CONDUTTORE"), Label).Visible = True


                                CType(e.Item.FindControl("COD_FISCALE_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("COGNOME_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("NOME_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("DENOMINAZIONE_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("SESSO_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("DATA_NASCITA_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("COMUNE_NASCITA_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("PROVINCIA_NASCITA_CONDUTTORE"), RadTextBox).Visible = True

                            Else
                                CType(e.Item.FindControl("LBL_DATA_INIZIO_CONTRATTO"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_DATA_FINE_CONTRATTO"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_IMPORTO_CANONE"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_DATA_STIPULA"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_NUMERO_PAGINE"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_NUMERO_COPIE"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_COD_FISCALE_LOCATORE"), Label).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("LBL_DENOMINAZIONE_LOCATORE"), Label).Style.Value = "visibility:hidden;"

                                CType(e.Item.FindControl("DATA_INIZIO_CONTRATTO"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("DATA_FINE_CONTRATTO"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("IMPORTO_CANONE"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("DATA_STIPULA"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("NUMERO_PAGINE"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("NUMERO_COPIE"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("COD_FISCALE_LOCATORE"), RadTextBox).Style.Value = "visibility:hidden;"
                                CType(e.Item.FindControl("DENOMINAZIONE_LOCATORE"), RadTextBox).Style.Value = "visibility:hidden;"
                            End If
                            If tipoAdempimento.Value = 3 Then
                                CType(e.Item.FindControl("LBL_COD_FISCALE_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_COGNOME_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_NOME_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_DENOMINAZIONE_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_SESSO_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_DATA_NASCITA_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_COMUNE_NASCITA_CONDUTTORE"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_PROVINCIA_NASCITA_CONDUTTORE"), Label).Visible = True

                                CType(e.Item.FindControl("LBL_COD_FISCALE_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_COGNOME_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_NOME_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_DENOMINAZIONE_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_SESSO_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_DATA_NASCITA_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_COMUNE_NASCITA_CONDUTTORE2"), Label).Visible = True
                                CType(e.Item.FindControl("LBL_PROVINCIA_NASCITA_CONDUTTORE2"), Label).Visible = True

                                CType(e.Item.FindControl("COD_FISCALE_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("COGNOME_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("NOME_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("DENOMINAZIONE_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("SESSO_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("DATA_NASCITA_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("COMUNE_NASCITA_CONDUTTORE"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("PROVINCIA_NASCITA_CONDUTTORE"), RadTextBox).Visible = True

                                CType(e.Item.FindControl("COD_FISCALE_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("COGNOME_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("NOME_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("DENOMINAZIONE_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("SESSO_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("DATA_NASCITA_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("COMUNE_NASCITA_CONDUTTORE2"), RadTextBox).Visible = True
                                CType(e.Item.FindControl("PROVINCIA_NASCITA_CONDUTTORE2"), RadTextBox).Visible = True
                            End If
                        End If
                    Else
                        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                            If idFornitura.Value = dataItem("ID").Text Then
                                e.Item.BackColor = Drawing.ColorTranslator.FromHtml("#8DA6CE")
                            End If
                            e.Item.Attributes.Add("onclick", "document.getElementById('idFornitura').value='" & dataItem("ID").Text & "';document.getElementById('tipoAdempimento').value='" & dataItem("TIPOLOGIA_ADEMPIMENTO").Text & "';document.getElementById('nome_file_xml').value='" & dataItem("NOME_FILE_XML").Text & "';")
                        End If

                    End If

            End Select
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridRLI_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridRLI_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridRLI.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT  nvl(tipologia_adempimento,0) as tipologia_adempimento,(case when nvl(tipologia_adempimento,0)=0 then 'Prima Registrazione' when nvl(tipologia_adempimento,0)=1 then 'Rinnovo' when nvl(tipologia_adempimento,0)=2 then 'Proroga' when nvl(tipologia_adempimento,0)=3 then 'Cessione' when nvl(tipologia_adempimento,0)=4 then 'Risoluzione' end ) as TIPO_ADEMPIMENTO," _
                & " getdata(data_impegno) as DATA_CREAZIONE," _
                & " getdata(DATA_FINE_CONTRATTO) as DATA_FINE_CONTRATTO1," _
                        & " getdata(DATA_INIZIO_CONTRATTO) as DATA_INIZIO_CONTRATTO1," _
                        & " getdata(DATA_NASCITA_CONDUTTORE) as DATA_NASCITA_CONDUTTORE1," _
                        & " getdata(dati_generali_rli.DATA_STIPULA) as DATA_STIPULA1," _
                        & " getdata(dati_generali_rli.DATA_FINE) as DATA_FINE1," _
                        & " getdata(DATA_NASCITA_CONDUTTORE_2) as DATA_NASCITA_CONDUTTORE2," _
                & " dati_generali_rli.*,cod_contratto " _
                & " from siscom_mi.dati_generali_rli,siscom_mi.rapporti_utenza,siscom_mi.rapporti_utenza_imposte where FILE_SCARICATO=nome_file_xml and id_fase_registrazione<>3 and rapporti_utenza_imposte.id_contratto=rapporti_utenza.id and dati_generali_rli.id_Contratto=rapporti_utenza.id order by dati_generali_rli.id asc"
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)

            'SettaFunzioniJS()
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridRLI_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub HideExpandColumnRecursive(ByVal tableView As GridTableView)
        Dim nestedViewItems As GridItem() = tableView.GetItems(GridItemType.NestedView)
        For Each nestedViewItem As GridNestedViewItem In nestedViewItems
            For Each nestedView As GridTableView In nestedViewItem.NestedTableViews
                Dim cell As TableCell = nestedView.ParentItem("ExpandColumn")
                If nestedView.Items.Count = 0 Then
                    cell.Controls(0).Visible = False
                    cell.Text = "&nbsp"
                    nestedViewItem.Visible = False
                End If
                If nestedView.HasDetailTables Then
                    HideExpandColumnRecursive(nestedView)
                End If
            Next
        Next
    End Sub

    Protected Sub RadGridRLI_PreRender(sender As Object, e As System.EventArgs) Handles RadGridRLI.PreRender
        'HideExpandColumnRecursive(RadGridRLI.MasterTableView)
    End Sub

    Protected Sub RadGridRLI_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridRLI.UpdateCommand
        Try
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim idContratto As Long = 0
            Dim fileScaricato As String = ""

            Select Case e.Item.OwnerTableView.Name
                Case "Dettagli"
                    Dim codice_comune_Catastale As String = ""
                    codice_comune_Catastale = CType(userControl.FindControl("codice_comune_Catastale"), RadTextBox).Text.ToUpper
                    Dim tipo_catasto As String = ""
                    tipo_catasto = CType(userControl.FindControl("tipo_catasto"), RadTextBox).Text.ToUpper
                    Dim porzione_immobile As String = ""
                    porzione_immobile = CType(userControl.FindControl("porzione_immobile"), RadTextBox).Text.ToUpper
                    Dim foglio As String = ""
                    foglio = CType(userControl.FindControl("foglio"), RadTextBox).Text.ToUpper
                    Dim particella As String = ""
                    particella = CType(userControl.FindControl("particella"), RadTextBox).Text.ToUpper
                    Dim particella_denominatore As String = ""
                    particella_denominatore = CType(userControl.FindControl("particella_denominatore"), RadTextBox).Text.ToUpper
                    Dim subalterno As String = ""
                    subalterno = CType(userControl.FindControl("subalterno"), RadTextBox).Text.ToUpper
                    Dim comune As String = ""
                    comune = CType(userControl.FindControl("comune"), RadTextBox).Text.ToUpper
                    Dim provincia As String = ""
                    provincia = CType(userControl.FindControl("provincia"), RadTextBox).Text.ToUpper
                    Dim categoria_Catastale As String = ""
                    categoria_Catastale = CType(userControl.FindControl("categoria_Catastale"), RadTextBox).Text.ToUpper
                    Dim rendita_Catastale As String = ""
                    rendita_Catastale = CType(userControl.FindControl("rendita_Catastale"), RadTextBox).Text.ToUpper
                    Dim tipologia_indirizzo As String = ""
                    tipologia_indirizzo = CType(userControl.FindControl("tipologia_indirizzo"), RadTextBox).Text.ToUpper
                    Dim indirizzo As String = ""
                    indirizzo = CType(userControl.FindControl("indirizzo"), RadTextBox).Text.ToUpper
                    Dim num_civico As String = ""
                    num_civico = CType(userControl.FindControl("num_civico"), RadTextBox).Text.ToUpper

                    Dim id As Integer = CType(userControl.FindControl("idImmob"), HiddenField).Value

                    par.cmd.CommandText = "UPDATE SISCOM_MI.DATI_IMMOBILI_RLI" _
                        & " SET " _
                        & " CODICE_COMUNE_CATASTALE = " & par.insDbValue(codice_comune_Catastale, True) _
                        & " ,TIPO_CATASTO            = " & par.insDbValue(tipo_catasto, True) _
                        & " ,PORZIONE_IMMOBILE       = " & par.insDbValue(porzione_immobile, True) _
                        & " ,FOGLIO                  = " & par.insDbValue(foglio, True) _
                        & " ,PARTICELLA              = " & par.insDbValue(particella, True) _
                        & " ,PARTICELLA_DENOMINATORE = " & par.insDbValue(particella_denominatore, True) _
                        & " ,SUBALTERNO              = " & par.insDbValue(subalterno, True) _
                        & " ,COMUNE                  = " & par.insDbValue(comune, True) _
                        & " ,PROVINCIA               = " & par.insDbValue(provincia, True) _
                        & " ,CATEGORIA_CATASTALE     = " & par.insDbValue(categoria_Catastale, True) _
                        & " ,RENDITA_CATASTALE       = " & par.insDbValue(rendita_Catastale, True) _
                        & " ,TIPOLOGIA_INDIRIZZO     = " & par.insDbValue(tipologia_indirizzo, True) _
                        & " ,INDIRIZZO               = " & par.insDbValue(indirizzo, True) _
                        & " ,NUM_CIVICO              = " & par.insDbValue(num_civico, True) _
                        & " WHERE  ID                = " & id
                    par.cmd.ExecuteNonQuery()
                Case Else
                    Dim cod_fiscale_fornitore As String = ""
                    cod_fiscale_fornitore = CType(userControl.FindControl("cod_fiscale_fornitore"), RadTextBox).Text.ToUpper
                    Dim ufficio_territoriale As String = ""
                    ufficio_territoriale = CType(userControl.FindControl("ufficio_territoriale"), RadTextBox).Text.ToUpper
                    Dim codice_abi As String = ""
                    codice_abi = CType(userControl.FindControl("codice_abi"), RadTextBox).Text
                    Dim codice_cab As String = ""
                    codice_cab = CType(userControl.FindControl("codice_cab"), RadTextBox).Text
                    Dim numero_conto_corrente As String = ""
                    numero_conto_corrente = CType(userControl.FindControl("numero_conto_corrente"), RadTextBox).Text
                    Dim cin As String = ""
                    cin = CType(userControl.FindControl("cin"), RadTextBox).Text.ToUpper
                    Dim cod_fiscale_titolare_cc As String = ""
                    cod_fiscale_titolare_cc = CType(userControl.FindControl("cod_fiscale_titolare_cc"), RadTextBox).Text.ToUpper
                    Dim importo_da_versare As String = ""
                    importo_da_versare = CType(userControl.FindControl("importo_da_versare"), RadTextBox).Text
                    Dim cod_fiscale_richiedente As String = ""
                    cod_fiscale_richiedente = CType(userControl.FindControl("cod_fiscale_richiedente"), RadTextBox).Text.ToUpper
                    Dim denominazione_richiedente As String = ""
                    denominazione_richiedente = CType(userControl.FindControl("denominazione_richiedente"), RadTextBox).Text.ToUpper
                    Dim cod_fiscale_rappresentante As String = ""
                    cod_fiscale_rappresentante = CType(userControl.FindControl("cod_fiscale_rappresentante"), RadTextBox).Text.ToUpper
                    Dim cognome_rappresentante As String = ""
                    cognome_rappresentante = CType(userControl.FindControl("cognome_rappresentante"), RadTextBox).Text.ToUpper
                    Dim nome_rappresentante As String = ""
                    nome_rappresentante = CType(userControl.FindControl("nome_rappresentante"), RadTextBox).Text.ToUpper
                    Dim imposta_registro As String = ""
                    imposta_registro = CType(userControl.FindControl("imposta_registro"), RadTextBox).Text
                    Dim imposta_bollo As String = ""
                    imposta_bollo = CType(userControl.FindControl("imposta_bollo"), RadTextBox).Text
                    Dim sanzione_imp_registro As String = ""
                    sanzione_imp_registro = CType(userControl.FindControl("sanzione_imp_registro"), RadTextBox).Text
                    Dim sanzione_imp_bollo As String = ""
                    sanzione_imp_bollo = CType(userControl.FindControl("sanzione_imp_bollo"), RadTextBox).Text
                    Dim data_inizio_contratto As String = ""
                    data_inizio_contratto = CType(userControl.FindControl("data_inizio_contratto"), RadTextBox).Text
                    Dim data_fine_contratto As String = ""
                    data_fine_contratto = CType(userControl.FindControl("data_fine_contratto"), RadTextBox).Text
                    Dim importo_canone As String = ""
                    importo_canone = CType(userControl.FindControl("importo_canone"), RadTextBox).Text
                    Dim data_stipula As String = ""
                    data_stipula = CType(userControl.FindControl("data_stipula"), RadTextBox).Text
                    Dim numero_pagine As String = ""
                    numero_pagine = CType(userControl.FindControl("numero_pagine"), RadTextBox).Text
                    Dim numero_copie As String = ""
                    numero_copie = CType(userControl.FindControl("numero_copie"), RadTextBox).Text
                    Dim cf_intermediario As String = ""
                    cf_intermediario = CType(userControl.FindControl("cf_intermediario"), RadTextBox).Text.ToUpper
                    Dim annualita As String = ""
                    annualita = CType(userControl.FindControl("annualita"), RadTextBox).Text
                    Dim ufficio_registrazione As String = ""
                    ufficio_registrazione = CType(userControl.FindControl("ufficio_registrazione"), RadTextBox).Text.ToUpper
                    Dim anno_registrazione As String = ""
                    anno_registrazione = CType(userControl.FindControl("anno_registrazione"), RadTextBox).Text
                    Dim data_fine As String = ""
                    data_fine = CType(userControl.FindControl("data_fine"), RadTextBox).Text
                    Dim serie_registrazione As String = ""
                    serie_registrazione = CType(userControl.FindControl("serie_registrazione"), RadTextBox).Text.ToUpper
                    Dim numero_registrazione As String = ""
                    numero_registrazione = CType(userControl.FindControl("numero_registrazione"), RadTextBox).Text
                    Dim cod_fiscale_locatore As String = ""
                    cod_fiscale_locatore = CType(userControl.FindControl("cod_fiscale_locatore"), RadTextBox).Text.ToUpper
                    Dim denominazione_locatore As String = ""
                    denominazione_locatore = CType(userControl.FindControl("denominazione_locatore"), RadTextBox).Text.ToUpper
                    Dim cod_fiscale_conduttore As String = ""
                    cod_fiscale_conduttore = CType(userControl.FindControl("cod_fiscale_conduttore"), RadTextBox).Text.ToUpper
                    Dim cognome_conduttore As String = ""
                    cognome_conduttore = CType(userControl.FindControl("cognome_conduttore"), RadTextBox).Text.ToUpper
                    Dim nome_conduttore As String = ""
                    nome_conduttore = CType(userControl.FindControl("nome_conduttore"), RadTextBox).Text.ToUpper
                    Dim denominazione_conduttore As String = ""
                    denominazione_conduttore = CType(userControl.FindControl("denominazione_conduttore"), RadTextBox).Text.ToUpper
                    Dim sesso_conduttore As String = ""
                    sesso_conduttore = CType(userControl.FindControl("sesso_conduttore"), RadTextBox).Text.ToUpper
                    Dim data_nascita_conduttore As String = ""
                    data_nascita_conduttore = CType(userControl.FindControl("data_nascita_conduttore"), RadTextBox).Text
                    Dim comune_nascita_conduttore As String = ""
                    comune_nascita_conduttore = CType(userControl.FindControl("comune_nascita_conduttore"), RadTextBox).Text.ToUpper
                    Dim provincia_nascita_conduttore As String = ""
                    provincia_nascita_conduttore = CType(userControl.FindControl("provincia_nascita_conduttore"), RadTextBox).Text.ToUpper

                    Dim cod_fiscale_conduttore2 As String = ""
                    cod_fiscale_conduttore2 = CType(userControl.FindControl("cod_fiscale_conduttore2"), RadTextBox).Text.ToUpper
                    Dim cognome_conduttore2 As String = ""
                    cognome_conduttore2 = CType(userControl.FindControl("cognome_conduttore2"), RadTextBox).Text.ToUpper
                    Dim nome_conduttore2 As String = ""
                    nome_conduttore2 = CType(userControl.FindControl("nome_conduttore2"), RadTextBox).Text.ToUpper
                    Dim denominazione_conduttore2 As String = ""
                    denominazione_conduttore2 = CType(userControl.FindControl("denominazione_conduttore2"), RadTextBox).Text.ToUpper
                    Dim sesso_conduttore2 As String = ""
                    sesso_conduttore2 = CType(userControl.FindControl("sesso_conduttore2"), RadTextBox).Text.ToUpper
                    Dim data_nascita_conduttore2 As String = ""
                    data_nascita_conduttore2 = CType(userControl.FindControl("data_nascita_conduttore2"), RadTextBox).Text
                    Dim comune_nascita_conduttore2 As String = ""
                    comune_nascita_conduttore2 = CType(userControl.FindControl("comune_nascita_conduttore2"), RadTextBox).Text.ToUpper
                    Dim provincia_nascita_conduttore2 As String = ""
                    provincia_nascita_conduttore2 = CType(userControl.FindControl("provincia_nascita_conduttore2"), RadTextBox).Text.ToUpper

                    par.cmd.CommandText = " update siscom_mi.dati_generali_rli set COD_FISCALE_FORNITORE=" & par.insDbValue(cod_fiscale_fornitore, True) _
                        & " ,UFFICIO_TERRITORIALE=" & par.insDbValue(ufficio_territoriale, True) _
                        & " ,CODICE_ABI=" & par.insDbValue(codice_abi, True) _
                        & " ,CODICE_CAB=" & par.insDbValue(codice_cab, True) _
                        & " ,NUMERO_CONTO_CORRENTE=" & par.insDbValue(numero_conto_corrente, True) _
                        & " ,CIN=" & par.insDbValue(cin, True) _
                        & " ,COD_FISCALE_TITOLARE_CC=" & par.insDbValue(cod_fiscale_titolare_cc, True) _
                        & " ,IMPORTO_DA_VERSARE=" & par.insDbValue(importo_da_versare, True) _
                        & " ,COD_FISCALE_RICHIEDENTE=" & par.insDbValue(cod_fiscale_richiedente, True) _
                        & " ,DENOMINAZIONE_RICHIEDENTE=" & par.insDbValue(denominazione_richiedente, True) _
                        & " ,COD_FISCALE_RAPPRESENTANTE=" & par.insDbValue(cod_fiscale_rappresentante, True) _
                        & " ,COGNOME_RAPPRESENTANTE=" & par.insDbValue(cognome_rappresentante, True) _
                        & " ,NOME_RAPPRESENTANTE=" & par.insDbValue(nome_rappresentante, True) _
                        & " ,IMPOSTA_REGISTRO=" & par.insDbValue(imposta_registro, True) _
                        & " ,IMPOSTA_BOLLO=" & par.insDbValue(imposta_bollo, True) _
                        & " ,SANZIONE_IMP_REGISTRO=" & par.insDbValue(sanzione_imp_registro, True) _
                        & " ,SANZIONE_IMP_BOLLO=" & par.insDbValue(sanzione_imp_bollo, True) _
                        & " ,DATA_INIZIO_CONTRATTO=" & par.insDbValue(data_inizio_contratto, True, True) _
                        & " ,DATA_FINE_CONTRATTO=" & par.insDbValue(data_fine_contratto, True, True) _
                        & " ,IMPORTO_CANONE=" & par.insDbValue(importo_canone, True) _
                        & " ,DATA_STIPULA=" & par.insDbValue(data_stipula, True, True) _
                        & " ,NUMERO_PAGINE=" & par.insDbValue(numero_pagine, False) _
                        & " ,NUMERO_COPIE=" & par.insDbValue(numero_copie, False) _
                        & " ,CF_INTERMEDIARIO=" & par.insDbValue(cf_intermediario, True) _
                        & " ,ANNUALITA=" & par.insDbValue(annualita, False) _
                        & " ,UFFICIO_REGISTRAZIONE=" & par.insDbValue(ufficio_registrazione, True) _
                        & " ,DATA_FINE=" & par.insDbValue(data_fine, True, True) _
                        & " ,ANNO_REGISTRAZIONE=" & par.insDbValue(anno_registrazione, True) _
                        & " ,SERIE_REGISTRAZIONE=" & par.insDbValue(serie_registrazione, True) _
                        & " ,NUMERO_REGISTRAZIONE=" & par.insDbValue(numero_registrazione, True) _
                        & " ,COD_FISCALE_LOCATORE=" & par.insDbValue(cod_fiscale_locatore, True) _
                        & " ,DENOMINAZIONE_LOCATORE=" & par.insDbValue(denominazione_locatore, True) _
                        & " ,COD_FISCALE_CONDUTTORE=" & par.insDbValue(cod_fiscale_conduttore, True) _
                        & " ,COGNOME_CONDUTTORE=" & par.insDbValue(cognome_conduttore, True) _
                        & " ,NOME_CONDUTTORE=" & par.insDbValue(nome_conduttore, True) _
                        & " ,DENOMINAZIONE_CONDUTTORE=" & par.insDbValue(denominazione_conduttore, True) _
                        & " ,SESSO_CONDUTTORE=" & par.insDbValue(sesso_conduttore, True) _
                        & " ,DATA_NASCITA_CONDUTTORE=" & par.insDbValue(data_nascita_conduttore, True, True) _
                        & " ,COMUNE_NASCITA_CONDUTTORE=" & par.insDbValue(comune_nascita_conduttore, True) _
                        & " ,PROVINCIA_NASCITA_CONDUTTORE=" & par.insDbValue(provincia_nascita_conduttore, True) _
                        & " ,COD_FISCALE_CONDUTTORE_2=" & par.insDbValue(cod_fiscale_conduttore, True) _
                        & " ,COGNOME_CONDUTTORE_2=" & par.insDbValue(cognome_conduttore2, True) _
                        & " ,NOME_CONDUTTORE_2=" & par.insDbValue(nome_conduttore2, True) _
                        & " ,DENOMINAZIONE_CONDUTTORE_2=" & par.insDbValue(denominazione_conduttore2, True) _
                        & " ,SESSO_CONDUTTORE_2=" & par.insDbValue(sesso_conduttore2, True) _
                        & " ,DATA_NASCITA_CONDUTTORE_2=" & par.insDbValue(data_nascita_conduttore2, True, True) _
                        & " ,COMUNE_NASCITA_CONDUTTORE_2=" & par.insDbValue(comune_nascita_conduttore2, True) _
                        & " ,PROVINCIA_NASCITA_CONDUTTORE_2=" & par.insDbValue(provincia_nascita_conduttore2, True) _
                        & " where ID = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM siscom_mi.dati_generali_rli where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        idContratto = par.IfNull(myReaderA("id_contratto"), 0)
                        fileScaricato = par.IfNull(myReaderA("nome_file_xml"), "")
                    End If
                    myReaderA.Close()


                    par.cmd.CommandText = " UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE" _
                        & " SET ANNO = " & par.insDbValue(anno_registrazione, False) & "," _
                        & " CF_PIVA  = " & par.insDbValue(cod_fiscale_locatore, True) & "," _
                        & " IMPORTO_CANONE  = " & par.insDbValue(importo_canone, False) & "," _
                        & " IMPORTO_SANZIONE = " & par.insDbValue(sanzione_imp_registro, False) & "," _
                        & " IMPORTO_TRIBUTO  = " & par.insDbValue(imposta_registro, False) & " " _
                        & " WHERE  ID_CONTRATTO = " & idContratto & " AND FILE_SCARICATO ='" & fileScaricato & "'"
                    par.cmd.ExecuteNonQuery()

            End Select

            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata", "'", "\'") & "','success');", True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Contratti - ModificaRLI - RadGridRLI_UpdateCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub btnScaricaXML_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        If tipoAdempimento.Value = "0" Then
            CreaXML()
        Else
            CreaXML2()
        End If
    End Sub

End Class
