Imports System.IO
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class MOROSITA_RisultatiDebitori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public ID_Rapporti As New System.Collections.ArrayList()
    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Dim Importo1 As String
    Dim Importo2 As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If
        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
             & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../NuoveImm/SfondoMascheraContratti2.jpg');z-index:1000;"">" _
             & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
             & "margin-top: -48px; background-image: url('Immagini/sfondo2.png');"">" _
             & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
             & "<img src=""Immagini/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
             & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
             & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            lstListaRapporti.Clear()
            impostaQueryRicerca()
            creaDatagrid()
            'impostaSomme()
            If Session.Item("LIVELLO") = "1" Then
                btnProcedi.Visible = True
            End If
            If Session.Item("MOD_MOROSITA_SL") = "1" Then
                FrmSoloLettura()
            End If
        End If
    End Sub
    Private Sub impostaQueryRicerca()
        Try
            Dim sValore As String
            Dim sCompara As String
            Dim a1 As Boolean = False
            Dim a2 As Boolean = False
            Dim a3 As Boolean = False
            Dim a4 As Boolean = False
            Dim a5 As Boolean = False
            Dim a6 As Boolean = False
            Dim sOccupanti As String = ""
            Dim sData1 As String = ""
            Dim sData2 As String = ""
            Dim sStringaSql As String = ""
            Dim sStringa_SELECT_1 As String = ""
            Dim sStringa_SELECT_2 As String = ""
            Dim sStringa_SELECT_3 As String = ""
            Dim sStringa_SELECT_4 As String = ""
            Dim sStringa_FROM_WHERE As String = ""
            Dim sStringaSqlDATA_RIF As String = ""
            Dim sHaving As String = ""
            Dim sGroupBy As String = ""
            Dim gen As Epifani.ListaGenerale
            Dim FlagConnessione As Boolean
            '*********************************************************
            Dim struttura As String = Request.QueryString("FI")  'FILIALE (STRUTTURA)
            Dim areaCanone As String = Request.QueryString("AREAC")  'AREA CANONE (CANONI_EC.ID_AREA_ECONOMICA)
            Dim complesso As String = Request.QueryString("CO")      'COMPLESSO
            Dim edificio As String = Request.QueryString("ED")       'EDIFICIO
            Dim indirizzo As String = Request.QueryString("IN")      'INDIRIZZO
            Dim civico As String = Request.QueryString("CI")         'CIVICO
            Dim ti As String = UCase(Request.QueryString("TI"))      'TIPOLOGIA U.I.
            Dim stipulaDal As String = Request.QueryString("DAL_S")        'DATA STIPULA DAL
            Dim stipulaAL As String = Request.QueryString("AL_S")          'DATA STIPULA AL
            Dim DataRIF_Dal As String = Request.QueryString("DAL")   'DATA BOL_BOLLETTE.RIFERIMENTO_DA
            Dim DataRIF_Al As String = Request.QueryString("AL")     'DATA BOL_BOLLETTE.RIFERIMENTO_A
            Dim Codice As String = Request.QueryString("CD")         'CODICE RAPPORTO
            Dim cognome As String = Request.QueryString("CG")        'COGNOME
            Dim Nome As String = Request.QueryString("NM")           'NOME
            Importo1 = Request.QueryString("IMP1")     'IMPORTO DA
            Importo2 = Request.QueryString("IMP2")     'IMPORTO A
            Dim BolScaduteDA As String = Request.QueryString("BOLDA") 'BOLLETTE SCADUTE DA
            Dim BolScaduteA As String = Request.QueryString("BOLA")  'BOLLETTE SCADUTE A
            Dim TipoRapporto As String = Request.QueryString("RAPP")
            Dim TipoContr As String = Request.QueryString("TIPOCONTR")
            Dim StatoContratto As String = Request.QueryString("ST")
            Dim TipoRicerca As String = Request.QueryString("MORA")        'TIPOLOGIA RICERCA MORA PRIMA o DOPO 30.09.2009
            Dim Ordinamento As String = Request.QueryString("ORD")         'ORDINAMENTO
            '*********************************************************
            'COMPLESSO-EDIFICIO-INDIRIZZO-CIVICO
            '*********************************************************
            If par.IfEmpty(indirizzo, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO in (select ID from SISCOM_MI.INDIRIZZI " _
                    & " where DESCRIZIONE='" & par.PulisciStrSql(indirizzo) & "'"
                If par.IfEmpty(civico, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and CIVICO='" & par.PulisciStrSql(civico) & "')"
                Else
                    sStringaSql = sStringaSql & " )"
                End If
            ElseIf par.IfEmpty(edificio, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.EDIFICI.ID =" & edificio
            ElseIf par.IfEmpty(complesso, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID =" & complesso
            ElseIf par.IfEmpty(struttura, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE in (" & struttura & ")"
            End If
            '*********************************************************
            'COGNOME e NOME
            '*********************************************************
            If cognome <> "" Then
                sValore = Strings.UCase(cognome)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If Nome <> "" Then
                sValore = Strings.UCase(Nome)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            '********************************************************
            'CODICE RAPPORTO
            '********************************************************
            If Codice <> "" Then
                sValore = Strings.UCase(Codice)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            '***********************************************************
            'TIPOLOGIA U.I.
            '***********************************************************
            If ti <> "" Then
                sStringaSql = sStringaSql & " and unita_immobiliari.cod_TIPOLOGIA ='" & par.PulisciStrSql(ti) & "' "
            End If
            '*********************************************************
            'DATA STIPULA CONTRATTO
            '*********************************************************
            If stipulaDal <> "" Then
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.DATA_STIPULA>='" & stipulaDal & "' "
            End If
            If stipulaAL <> "" Then
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.DATA_STIPULA<='" & stipulaAL & "' "
            End If
            '***********************************************************
            'TIPOLOGIA DI RAPPORTO
            '***********************************************************
            If par.IfEmpty(TipoRapporto, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='" & TipoRapporto & "' "
            End If
            '***********************************************************
            'STATO DEL CONTRATTO
            '***********************************************************
            If par.IfEmpty(StatoContratto, "") = "CHIUSO" Then
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.DATA_RICONSEGNA is not null "
            ElseIf par.IfEmpty(StatoContratto, "") = "IN CORSO" Then
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.DATA_RICONSEGNA is null "
            End If
            '***********************************************************
            'TIPO SPECIFICO
            '***********************************************************
            If TipoRapporto = "ERP" Then
                If TipoContr <> "0" And TipoContr <> "2" Then 'Canone convenz., Art.22, Forze dell'ordine
                    sStringaSql = sStringaSql & "and RAPPORTI_UTENZA.PROVENIENZA_ASS = " & TipoContr & " "
                End If
                If TipoContr = "2" Then 'Erp Moderato
                    sStringaSql = sStringaSql & "and UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = " & TipoContr & " "
                End If
            End If
            If TipoRapporto = "L43198" Then
                If TipoContr <> "-1" Then
                    sStringaSql = sStringaSql & "and RAPPORTI_UTENZA.DEST_USO = '" & TipoContr & "' "
                End If
            End If
            '***********************************************************
            If Mid(TipoRicerca, 1, 1) = "1" Then
                a1 = True
            End If
            If Mid(TipoRicerca, 2, 1) = "1" Then
                a2 = True
            End If
            If Mid(TipoRicerca, 3, 1) = "1" Then
                a3 = True
            End If
            If Mid(TipoRicerca, 4, 1) = "1" Then
                a4 = True
            End If
            If Mid(TipoRicerca, 5, 1) = "1" Then
                a5 = True
            End If
            If Mid(TipoRicerca, 6, 1) = "1" Then
                a6 = True
            End If
            '***********************************************************
            'AREA CANONE
            '***********************************************************
            If par.IfEmpty(areaCanone, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and  RAPPORTI_UTENZA.ID in (select distinct(ID_CONTRATTO) " _
                    & " from SISCOM_MI.CANONI_EC " _
                    & " where ID_AREA_ECONOMICA in (" & areaCanone & ")) "
            End If
            '***********************************************************
            'DATA COMPETENZA BOLLETTA
            '***********************************************************
            sStringaSqlDATA_RIF = ""
            If DataRIF_Dal <> "" Then
                If DataRIF_Al <> "" Then
                    '07/01/2014 AGGIUNTO FILTRO IMPORTO_RUOLO = 0
                    sStringaSqlDATA_RIF = " and RAPPORTI_UTENZA.ID in " _
                        & " (select ID_CONTRATTO " _
                        & "  from SISCOM_MI.BOL_BOLLETTE  " _
                        & " where FL_ANNULLATA = 0 " _
                        & "   and ID_BOLLETTA_RIC Is NULL and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                        & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                        & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                        & "   and ( (BOL_BOLLETTE.RIFERIMENTO_DA>='" & DataRIF_Dal & "' and BOL_BOLLETTE.RIFERIMENTO_DA<='" & DataRIF_Al & "') " _
                        & "   and   (BOL_BOLLETTE.RIFERIMENTO_A>='" & DataRIF_Dal & "' and BOL_BOLLETTE.RIFERIMENTO_A<='" & DataRIF_Al & "')  ) "
                    'If (a1 = True And a3 = False And a4 = False) Or (a2 = True And a3 = False And a4 = False) Or (a5 = True And a3 = False And a4 = False) Or (a6 = True And a3 = False And a4 = False) Then
                    If (a1 = True And a3 = False And a4 = False And a5 = False And a6 = False) Or (a2 = True And a3 = False And a4 = False And a5 = False And a6 = False) Then
                        'AB
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and (BOL_BOLLETTE.ID<0 OR BOL_BOLLETTE.FL_SOLLECITO=1)*/ "
                    ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                        'CD
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and BOL_BOLLETTE.FL_SOLLECITO=1*/  "
                    ElseIf (a5 = True And a1 = False And a2 = False And a3 = False And a4 = False) Or (a6 = True And a1 = False And a2 = False And a3 = False And a4 = False) Then
                        'EF
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " and ID<0 "
                    Else
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and (BOL_BOLLETTE.ID<0 OR BOL_BOLLETTE.FL_SOLLECITO=1)*/ "
                    End If

                    sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0  ) "

                    'sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & "   and  ((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0))) > 0 )"

                    'sStringaSqlDATA_RIF = " and ((BOL_BOLLETTE.RIFERIMENTO_DA>='" & sValoreDataRIF_Dal & "' and BOL_BOLLETTE.RIFERIMENTO_DA<='" & sValoreDataRIF_Al & "') " _
                    '                     & " and (BOL_BOLLETTE.RIFERIMENTO_A>='" & sValoreDataRIF_Dal & "' and BOL_BOLLETTE.RIFERIMENTO_A<='" & sValoreDataRIF_Al & "'))"

                Else
                    'sStringaSqlDATA_RIF = " and (BOL_BOLLETTE.RIFERIMENTO_DA>='" & sValoreDataRIF_Dal & "' " _
                    '                     & " and BOL_BOLLETTE.RIFERIMENTO_A>='" & sValoreDataRIF_Dal & "')"
                    '07/01/2014 AGGIUNTO FILTRO IMPORTO_RUOLO
                    sStringaSqlDATA_RIF = " and RAPPORTI_UTENZA.ID in " _
                                            & " (select ID_CONTRATTO " _
                                            & "  from SISCOM_MI.BOL_BOLLETTE  " _
                                             & " where FL_ANNULLATA = 0 " _
                                             & "   and ID_BOLLETTA_RIC Is NULL and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                             & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                             & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                             & "   and BOL_BOLLETTE.RIFERIMENTO_DA>='" & DataRIF_Dal & "'  "
                    '                                                                 & "   and  BOL_BOLLETTE.RIFERIMENTO_A>='" & DataRIF_Dal & "')  "

                    If (a1 = True And a3 = False And a4 = False And a5 = False And a6 = False) Or (a2 = True And a3 = False And a4 = False And a5 = False And a6 = False) Then
                        'AB
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and (BOL_BOLLETTE.ID<0 OR BOL_BOLLETTE.FL_SOLLECITO=1)*/ "
                    ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                        'CD
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and BOL_BOLLETTE.FL_SOLLECITO=1*/  "
                    ElseIf (a5 = True And a1 = False And a2 = False And a3 = False And a4 = False) Or (a6 = True And a1 = False And a2 = False And a3 = False And a4 = False) Then
                        'EF
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " and ID<0 "
                    Else
                        sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and (BOL_BOLLETTE.ID<0 OR BOL_BOLLETTE.FL_SOLLECITO=1)*/ "
                    End If
                    sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 ) "
                End If
            ElseIf DataRIF_Al <> "" Then
                'sStringaSqlDATA_RIF = " and (BOL_BOLLETTE.RIFERIMENTO_DA<='" & sValoreDataRIF_Al & "' " _
                '                     & " and BOL_BOLLETTE.RIFERIMENTO_A<='" & sValoreDataRIF_Al & "')"
                '07/01/2014 AGGIUNTO FILTRO IMPORTO_RUOLO
                sStringaSqlDATA_RIF = " and RAPPORTI_UTENZA.ID in " _
                                        & " (select ID_CONTRATTO " _
                                        & "  from SISCOM_MI.BOL_BOLLETTE  " _
                                         & " where FL_ANNULLATA = 0 " _
                                         & "   and ID_BOLLETTA_RIC Is NULL and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                         & "   and BOL_BOLLETTE.ID_TIPO not in (4,7)  " _
                                         & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                         & "   and  BOL_BOLLETTE.RIFERIMENTO_A<='" & DataRIF_Al & "' "
                ' & "   and (BOL_BOLLETTE.RIFERIMENTO_DA<='" & DataRIF_Al & "' " _

                If (a1 = True And a3 = False And a4 = False And a5 = False And a6 = False) Or (a2 = True And a3 = False And a4 = False And a5 = False And a6 = False) Then
                    'AB
                    sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and (BOL_BOLLETTE.ID<0 OR BOL_BOLLETTE.FL_SOLLECITO=1)*/ "
                ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                    'CD
                    sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and BOL_BOLLETTE.FL_SOLLECITO=1*/  "
                ElseIf (a5 = True And a1 = False And a2 = False And a3 = False And a4 = False) Or (a6 = True And a1 = False And a2 = False And a3 = False And a4 = False) Then
                    'EF
                    sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " and ID<0 "
                Else
                    sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & " /*and (BOL_BOLLETTE.ID<0 OR BOL_BOLLETTE.FL_SOLLECITO=1)*/ "
                End If

                sStringaSqlDATA_RIF = sStringaSqlDATA_RIF & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 ) "

            End If
            '***********************************************************
            If (a1 = True And a3 = False And a4 = False And a5 = False And a6 = False) Or (a2 = True And a3 = False And a4 = False And a5 = False And a6 = False) Then
                'Aa) e Bb) MODELLO DI   MESSA IN MORA SE DISPONIBILE  IL SALDO AL 30.9.2009
                sStringaSql = sStringaSql & " and  ( NVL(SISCOM_MI.SALDI.SALDO_1,0)>0  and NVL(SISCOM_MI.SALDI.SALDO_2,0)>0) "
                'perchè la bolletta può essere pagata ma parzialmente
            ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                'C)e D) MODELLO DI MESSA IN MORA SE MANCA IL SALDO AL 30.9.2009 
            ElseIf (a5 = True And a1 = False And a2 = False And a3 = False And a4 = False) Or (a6 = True And a1 = False And a2 = False And a3 = False And a4 = False) Then
                sStringaSql = sStringaSql & " and  (NVL(SISCOM_MI.SALDI.SALDO_1,0)>0  and NVL(SISCOM_MI.SALDI.SALDO_2,0)=0) "
            Else
                sStringaSql = sStringaSql & " and (   (NVL(SISCOM_MI.SALDI.SALDO_1,0)>0  and NVL(SISCOM_MI.SALDI.SALDO_2,0)>0) " _
                                               & " or (NVL(SISCOM_MI.SALDI.SALDO_1,0)=0  and NVL(SISCOM_MI.SALDI.SALDO_2,0)>0) " _
                                               & " or (NVL(SISCOM_MI.SALDI.SALDO_1,0)>0  and NVL(SISCOM_MI.SALDI.SALDO_2,0)=0) ) "
            End If
            If (a2 = True And a1 = False And a3 = False And a5 = False) Or (a4 = True And a1 = False And a3 = False And a5 = False) Or (a6 = True And a1 = False And a3 = False And a5 = False) Then
                'Bb)  e D) PER  OCCUPANTI ABUSIVI 
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR='ILLEG' "
            ElseIf (a1 = True And a2 = False And a4 = False And a6 = False) Or (a3 = True And a2 = False And a4 = False And a6 = False) Or (a5 = True And a2 = False And a4 = False And a6 = False) Then
                'Aa) e C) PER ALLOGGI ERP, IMMOBILI DIVERSI E ALLOGGI LOCATI EX L. n.392/78 
                sStringaSql = sStringaSql & " and RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR<>'ILLEG' "
            End If
            Dim sSelectImp_Negativo As String = ""
            'If Importo1 <> "" Then
            '    If Importo2 <> "" Then
            '        'IMPORTO 1 + IMPORTO2
            '        sHaving = " and (     NVL(SISCOM_MI.SALDI.SALDO,0) >=" & Importo1 _
            '                      & " and NVL(SISCOM_MI.SALDI.SALDO,0) <=" & Importo2 & " ) "
            '    Else
            '        'IMPORTO1
            '        sHaving = " and NVL(SISCOM_MI.SALDI.SALDO,0) >=" & Importo1
            '    End If
            'ElseIf Importo2 <> "" Then
            '    'IMPORTO2
            '    sHaving = " and (     NVL(SISCOM_MI.SALDI.SALDO,0) >0" _
            '                  & " and NVL(SISCOM_MI.SALDI.SALDO,0) <=" & Importo2 & " ) "
            'Else
            sHaving = " and NVL(SISCOM_MI.SALDI.SALDO,0) >0 "
            'End If
            If BolScaduteDA <> "" Then
                If BolScaduteA <> "" Then
                    If (a1 = True And a3 = False And a4 = False) Or (a2 = True And a3 = False And a4 = False) Or (a5 = True And a3 = False And a4 = False) Or (a6 = True And a3 = False And a4 = False) Then
                        'PRIMA DEL 2009
                        sStringaSql = sStringaSql & " and ( saldi.N_BOL_SCADUTE_1>=" & BolScaduteDA & " and  saldi.N_BOL_SCADUTE_1<=" & BolScaduteA & ") "

                    ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                        'DOPO DEL 2009
                        sStringaSql = sStringaSql & " and ( saldi.N_BOL_SCADUTE_2>=" & BolScaduteDA & " and  saldi.N_BOL_SCADUTE_2<=" & BolScaduteA & ") "
                    Else
                        'TUTTI
                        sStringaSql = sStringaSql & " and ( (saldi.N_BOL_SCADUTE_2 + saldi.N_BOL_SCADUTE_1)>=" & BolScaduteDA & " and  (saldi.N_BOL_SCADUTE_2 + saldi.N_BOL_SCADUTE_1)<=" & BolScaduteA & ")"
                    End If
                Else

                    If (a1 = True And a3 = False And a4 = False) Or (a2 = True And a3 = False And a4 = False) Or (a5 = True And a3 = False And a4 = False) Or (a6 = True And a3 = False And a4 = False) Then
                        'PRIMA DEL 2009
                        sStringaSql = sStringaSql & " and  saldi.N_BOL_SCADUTE_1>=" & BolScaduteDA

                    ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                        'DOPO DEL 2009
                        sStringaSql = sStringaSql & " and saldi.N_BOL_SCADUTE_2>=" & BolScaduteDA
                    Else
                        'TUTTI
                        sStringaSql = sStringaSql & " and  (saldi.N_BOL_SCADUTE_2 + saldi.N_BOL_SCADUTE_1)>=" & BolScaduteDA
                    End If

                    ' sHaving = sHaving & " and count(BOL_BOLLETTE.ID_CONTRATTO)>=" & sValoreBolScaduteDA
                End If
            ElseIf BolScaduteA <> "" Then

                If (a1 = True And a3 = False And a4 = False) Or (a2 = True And a3 = False And a4 = False) Or (a5 = True And a3 = False And a4 = False) Or (a6 = True And a3 = False And a4 = False) Then
                    'PRIMA DEL 2009
                    sStringaSql = sStringaSql & " and  saldi.N_BOL_SCADUTE_1<=" & BolScaduteA

                ElseIf (a3 = True And a1 = False And a2 = False And a5 = False And a6 = False) Or (a4 = True And a1 = False And a2 = False And a5 = False And a6 = False) Then
                    'DOPO DEL 2009
                    sStringaSql = sStringaSql & " and saldi.N_BOL_SCADUTE_2<=" & BolScaduteA
                Else
                    'TUTTI
                    sStringaSql = sStringaSql & " and  (saldi.N_BOL_SCADUTE_2 + saldi.N_BOL_SCADUTE_1)<=" & BolScaduteA
                End If

                'sHaving = sHaving & " and count(BOL_BOLLETTE.ID_CONTRATTO)<=" & sValoreBolScaduteA

            End If

            If Request.QueryString("FILTDATE") = 0 Then
                sStringa_SELECT_1 = "select NVL(SISCOM_MI.SALDI.SALDO,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||trim(RAPPORTI_UTENZA.COD_CONTRATTO)||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''top=0,left=0'');£>'||" _
                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & "     then  trim(RAGIONE_SOCIALE) " _
                        & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                    & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                          & "     then  trim(RAGIONE_SOCIALE) " _
                          & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 "

            Else

                sStringa_SELECT_1 = "select NVL (siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & DataRIF_Dal & "','" & DataRIF_Al & "',NULL), 0)  AS DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||trim(RAPPORTI_UTENZA.COD_CONTRATTO)||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''top=0,left=0'');£>'||" _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & "     then  trim(RAGIONE_SOCIALE) " _
                                    & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & DataRIF_Dal & "','" & DataRIF_Al & "',NULL),0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & "     then  trim(RAGIONE_SOCIALE) " _
                                    & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 "

                'sStringa_SELECT_1 = "select NVL (siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & DataRIF_Dal & "','" & DataRIF_Al & "',NULL), 0)  AS DEBITO, RAPPORTI_UTENZA.ID, " _
                '    & " rapporti_utenza.cod_contratto as  COD_CONTRATTO ," _
                '    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                '    & "     then  trim(RAGIONE_SOCIALE) " _
                '    & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME) end ) " _
                '    & " as  INTESTATARIO ," _
                '    & " TRIM (TO_CHAR( NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & DataRIF_Dal & "','" & DataRIF_Al & "',NULL),0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                '    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                '    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                '    & " unita_immobiliari.cod_unita_immobiliare as  COD_UNITA_IMMOBILIARE ," _
                '    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                '    & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                '    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                '    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                '    & "     then  trim(RAGIONE_SOCIALE) " _
                '    & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 "

            End If


            sStringa_FROM_WHERE = " from  " _
                                  & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                  & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE," _
                                  & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                  & " SISCOM_MI.ANAGRAFICA," _
                                  & " SISCOM_MI.INDIRIZZI," _
                                  & " SISCOM_MI.EDIFICI," _
                                  & " SISCOM_MI.UNITA_CONTRATTUALE," _
                                  & " SISCOM_MI.UNITA_IMMOBILIARI," _
                                  & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                  & " SISCOM_MI.SALDI " _
                                  & " where  " _
                                  & "       EDIFICI.ID_COMPLESSO                      =COMPLESSI_IMMOBILIARI.ID " _
                                  & "  and  UNITA_IMMOBILIARI.ID_EDIFICIO             =EDIFICI.ID" _
                                  & "  and  UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                                  & "  and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID " _
                                  & "  and  UNITA_CONTRATTUALE.ID_CONTRATTO           =RAPPORTI_UTENZA.ID " _
                                  & "  and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
                                  & "  and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                  & "  and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID" _
                                  & "  and SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                                  & "  and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  " _
                                  & " and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID<>1 " _
                                  & "  and SALDI.ID_CONTRATTO=RAPPORTI_UTENZA.ID "

            'Select Ricerca
            sStringaSQL1 = sStringa_SELECT_1 & sStringa_FROM_WHERE & sStringaSql & sStringaSqlDATA_RIF & sHaving & sGroupBy & " ORDER BY " & Ordinamento
            '***************************************************************




            'Select EXCEL
            Session.Add("MIADT", sStringa_SELECT_2 & sStringa_FROM_WHERE & sStringaSql & sStringaSqlDATA_RIF & sHaving & sGroupBy & " ORDER BY " & Ordinamento)





            '****************************************
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try




    End Sub

    Private Sub creaDatagrid()
        Dim gen As Epifani.ListaGenerale
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Try
            Session.Remove("dtresultMorosi")
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If Not String.IsNullOrEmpty(Importo1) Or Not String.IsNullOrEmpty(Importo2) Then
                Dim dtSelect As String = ""
                If Not String.IsNullOrEmpty(Importo1) Then
                    dtSelect &= "debito >=" & Importo1
                End If
                If Not String.IsNullOrEmpty(Importo2) Then

                    If Not String.IsNullOrEmpty(dtSelect) Then
                        dtSelect &= " and "
                    End If
                    dtSelect &= "debito <= " & Importo2

                End If
                Dim dv As New Data.DataView(dt)
                dv.RowFilter = dtSelect
                If dv.ToTable.Rows.Count >= 1 Then

                    dt = dv.ToTable
                Else
                    dt.Clear()
                End If

                'r = dt.Select(dtSelect)

            End If

            Session.Add("dtresultMorosi", dt)


            For Each r As Data.DataRow In dt.Rows
                gen = New Epifani.ListaGenerale(lstListaRapporti.Count, par.IfNull(r.Item("id"), -1))
                lstListaRapporti.Add(gen)
                gen = Nothing
            Next
            If dt.Rows.Count > 1 Then
                txtSommaParziale.Text = Format(CDec(par.IfEmpty(dt.Compute("SUM(debito)", String.Empty), 0)), "##,##0.00")
                txtTotaleMorosita.Text = Format(CDec(par.IfEmpty(dt.Compute("SUM(debito)", String.Empty), 0)), "##,##0.00")
            End If

            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            Dim i As Integer = dt.Rows.Count
            lblNrisultati.Text = "(" & DataGrid1.Items.Count & " nella pagina - Totale :" & dt.Rows.Count & ") in " & i & " Rapporti"
            '****************************
            txtRapportiSelezionati.Text = dt.Rows.Count
            lblTotaleRapporti.Text = "Totale Rapporti"
            lblNTotaleRapporti.Text = i
            '****************************
            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("CheckBox1")
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then
                        chkExport.Checked = True
                        Exit For
                    End If
                Next
            Next
            gen = Nothing
            da.Dispose()
            dt.Dispose()
            chiudiConnessione()
            calcolaSomme()
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub
    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim i As Integer
        Dim Trovato As Boolean
        Dim gen As Epifani.ListaGenerale
        If e.NewPageIndex >= 0 Then
            ' X la pagina Max 200 record 
            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("CheckBox1")
                ' AddHandler chkExport.CheckedChanged, AddressOf cazzo
                If chkExport.Checked Then
                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next
                    If Trovato = False Then
                        gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(0).Text)
                        lstListaRapporti.Add(gen)
                        Me.Label3.Value = Val(Label3.Value) + 1
                        gen = Nothing
                    End If
                Else
                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next
                    If Trovato = True Then
                        i = 0
                        For Each gen In lstListaRapporti
                            If gen.STR = oDataGridItem.Cells(0).Text Then

                                lstListaRapporti.RemoveAt(i)
                                'Me.Label3.Value = Val(Label3.Value) - 1
                                Exit For
                            End If
                            i = i + 1
                        Next
                        gen = Nothing
                        Dim indice As Integer = 0
                        For Each gen In lstListaRapporti
                            gen.ID = indice
                            indice += 1
                        Next
                    End If
                End If
            Next
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            creaDatagrid()
        End If
    End Sub
    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaDebitori.aspx""</script>")
    End Sub
    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set
    End Property
    'Public Property sStringaSQL4() As String
    '    Get
    '        If Not (ViewState("par_sStringaSQL4") Is Nothing) Then
    '            Return CStr(ViewState("par_sStringaSQL4"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_sStringaSQL4") = value
    '    End Set
    'End Property
    'Public Property sStringaSQL2() As String
    '    Get
    '        If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
    '            Return CStr(ViewState("par_sStringaSQL2"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_sStringaSQL2") = value
    '    End Set

    'End Property
    'Public Property sStringaSQL3() As String
    '    Get
    '        If Not (ViewState("par_sStringaSQL3") Is Nothing) Then
    '            Return CStr(ViewState("par_sStringaSQL3"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_sStringaSQL3") = value
    '    End Set
    'End Property
    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim gen As Epifani.ListaGenerale
        ' X la pagina Max 200 record 
        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("CheckBox1")
            chkExport.Checked = False
            For Each gen In lstListaRapporti
                If gen.STR = oDataGridItem.Cells(0).Text Then
                    chkExport.Checked = True
                    Exit For
                End If
            Next
        Next
        gen = Nothing
    End Sub
    Protected Sub btnSelTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelTutti.Click
        Dim gen As Epifani.ListaGenerale
        Try
            lstListaRapporti.Clear()
            Dim dt As Data.DataTable
            dt = Session.Item("dtresultMorosi")
            For Each r As Data.DataRow In dt.Rows
                gen = New Epifani.ListaGenerale(lstListaRapporti.Count, par.IfNull(r.Item("id"), -1))
                lstListaRapporti.Add(gen)
                gen = Nothing
            Next
            txtSommaParziale.Text = Format(CDec(par.IfEmpty(dt.Compute("SUM(debito)", String.Empty), 0)), "##,##0.00")
            txtTotaleMorosita.Text = Format(CDec(par.IfEmpty(dt.Compute("SUM(debito)", String.Empty), 0)), "##,##0.00")


            txtRapportiSelezionati.Text = lblNTotaleRapporti.Text
            txtSommaParziale.Text = txtTotaleMorosita.Text
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnDeselTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselTutti.Click
        lstListaRapporti.Clear()
        txtRapportiSelezionati.Text = "0"
        txtSommaParziale.Text = "0,00"
    End Sub
    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim struttura As String = Request.QueryString("FI")  'FILIALE (STRUTTURA)
        Dim areaCanone As String = Request.QueryString("AREAC")  'AREA CANONE (CANONI_EC.ID_AREA_ECONOMICA)
        Dim complesso As String = Request.QueryString("CO")      'COMPLESSO
        Dim edificio As String = Request.QueryString("ED")       'EDIFICIO
        Dim indirizzo As String = Request.QueryString("IN")      'INDIRIZZO
        Dim civico As String = Request.QueryString("CI")         'CIVICO
        Dim ti As String = UCase(Request.QueryString("TI"))      'TIPOLOGIA U.I.
        Dim stipulaDal As String = Request.QueryString("DAL_S")        'DATA STIPULA DAL
        Dim stipulaAL As String = Request.QueryString("AL_S")          'DATA STIPULA AL
        Dim DataRIF_Dal As String = Request.QueryString("DAL")   'DATA BOL_BOLLETTE.RIFERIMENTO_DA
        Dim DataRIF_Al As String = Request.QueryString("AL")     'DATA BOL_BOLLETTE.RIFERIMENTO_A
        Dim Codice As String = Request.QueryString("CD")         'CODICE RAPPORTO
        Dim cognome As String = Request.QueryString("CG")        'COGNOME
        Dim Nome As String = Request.QueryString("NM")           'NOME
        Dim Importo1 As String = Request.QueryString("IMP1")     'IMPORTO DA
        Dim Importo2 As String = Request.QueryString("IMP2")     'IMPORTO A
        Dim BolScaduteDA As String = Request.QueryString("BOLDA") 'BOLLETTE SCADUTE DA
        Dim BolScaduteA As String = Request.QueryString("BOLA")  'BOLLETTE SCADUTE A
        Dim TipoRapporto As String = Request.QueryString("RAPP")
        Dim TipoContr As String = Request.QueryString("TIPOCONTR")
        Dim StatoContratto As String = Request.QueryString("ST")
        Dim TipoRicerca As String = Request.QueryString("MORA")        'TIPOLOGIA RICERCA MORA PRIMA o DOPO 30.09.2009
        Dim Ordinamento As String = Request.QueryString("ORD")
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim Trovato As Boolean
        Dim i As Integer
        Dim gen As Epifani.ListaGenerale
        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("CheckBox1")
            If chkExport.Checked Then
                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next
                If Trovato = False Then
                    gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(0).Text)
                    lstListaRapporti.Add(gen)
                    'Me.Label3.Value = Val(Label3.Value) + 1
                    gen = Nothing
                End If
            Else
                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next
                If Trovato = True Then
                    i = 0
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then

                            lstListaRapporti.RemoveAt(i)
                            'Me.Label3.Value = Val(Label3.Value) - 1
                            Exit For
                        End If
                        i = i + 1
                    Next
                    gen = Nothing
                    Dim indice As Integer = 0
                    For Each gen In lstListaRapporti
                        gen.ID = indice
                        indice += 1
                    Next
                End If
            End If
        Next
        If lstListaRapporti.Count > 0 Then
            If lstListaRapporti.Count > 1000 Then
                Response.Write("<script>alert('Attenzione...Non è possibile selezionare più di mille inquilini!')</script>")
                Exit Sub
            End If
            Dim filtComp As String = ""
            If Request.QueryString("FILTDATE") = 1 Then
                filtComp = "&FILTDATE=1&FDAL=" & Request.QueryString("DAL") & "&FAL=" & Request.QueryString("AL")
            Else
                filtComp = "&FILTDATE=0"
            End If
            Response.Write("<script>location.replace('CreaLettere.aspx?FI=" & struttura _
                                                                   & "&CO=" & complesso _
                                                                   & "&ED=" & edificio _
                                                                   & "&IN=" & par.VaroleDaPassare(indirizzo) _
                                                                   & "&CI=" & par.VaroleDaPassare(civico) _
                                                                   & "&CG=" & par.VaroleDaPassare(cognome) _
                                                                   & "&NM=" & par.VaroleDaPassare(Nome) _
                                                                   & "&CD=" & par.VaroleDaPassare(Codice) _
                                                                   & "&TI=" & ti _
                                                                   & "&DAL_S=" & stipulaDal _
                                                                   & "&AL_S=" & stipulaAL _
                                                                   & "&DAL=" & DataRIF_Dal _
                                                                   & "&AL=" & DataRIF_Al _
                                                                   & "&IMP1=" & Importo1 _
                                                                   & "&IMP2=" & Importo2 _
                                                                   & "&BOLDA=" & BolScaduteDA _
                                                                   & "&BOLA=" & BolScaduteA _
                                                                   & "&MORA=" & TipoRicerca _
                                                                   & "&RAPP=" & TipoRapporto _
                                                                   & "&TIPOCONTR=" & TipoContr _
                                                                   & "&ST=" & StatoContratto _
                                                                   & "&AREAC=" & par.VaroleDaPassare(areaCanone) & filtComp _
                                                                   & "&ORD=" & Ordinamento _
                                                & "');</script>")

        Else
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim FlagConnessione As Boolean
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim dt As New Data.DataTable
            Dim ds As New Data.DataSet()
            dt = Session.Item("dtresultMorosi")
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")
            i = 0
            With myExcelFile
                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DEBITO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "POSIZIONE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD. UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE", 0)
                .SetColumnWidth(1, 1, 22)   'CODICE
                .SetColumnWidth(2, 2, 40)   'INTESTATARIO
                .SetColumnWidth(3, 4, 12)   'DEBITO e TIPO
                .SetColumnWidth(5, 6, 18)   'TIPO e POSIZIONE
                .SetColumnWidth(7, 7, 15)   'TIPO UNITA'
                .SetColumnWidth(8, 8, 35)   'INDIRIZZO
                .SetColumnWidth(9, 10, 15)  'CIVICO e COMUNE
                K = 2
              
                For Each row As Data.DataRow In dt.Rows

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(row.Item("COD_CONTRATTO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(row.Item("INTESTATARIO2"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(row.Item("DEBITO2"), "")), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(row.Item("COD_TIPOLOGIA_CONTR_LOC"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(row.Item("POSIZIONE_CONTRATTO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(row.Item("COD_UNITA_IMMOBILIARE"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(row.Item("COD_TIPOLOGIA"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(row.Item("CIVICO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(row.Item("COMUNE_UNITA"), "")), 0)
                    i = i + 1
                    K = K + 1
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
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")
        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
        'Dim dt As New Data.DataTable
        'dt = Session.Item("dtresultMorosi")


        'If DataGrid1.Items.Count > 0 Then
        '    Dim nomefile As String = par.EsportaExcelDaDT(dt, "ExportMorosita", True, False, False, False)
        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
        '        Response.Redirect("..\/FileTemp\/" & nomefile, False)
        '    Else
        '        Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        '    End If
        'Else
        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun risultato da esportare!');", True)
        'End If



    End Sub
    Private Sub FrmSoloLettura()
        Me.btnSelTutti.Visible = False
        Me.btnDeselTutti.Visible = False
        Me.btnProcedi.Visible = False
        Me.SLet.Value = 1
    End Sub
    Private Sub calcolaSomme()
        For Each di As DataGridItem In DataGrid1.Items
            DirectCast(di.Cells(2).FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:Somma(" & par.VirgoleInPunti(di.Cells(4).Text.Replace("&nbsp;", "0").Replace(".", "")) & ",this);")
        Next
    End Sub
    'Private Sub impostaSomme()
    '    Try
    '        ApriConnessione()
    '        par.cmd.CommandText = sStringaSQL4
    '        Dim lettoreSommaTotale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        If lettoreSommaTotale.Read Then
    '            txtSommaParziale.Text = par.IfNull(lettoreSommaTotale(0), 0)
    '            txtTotaleMorosita.Text = par.IfNull(lettoreSommaTotale(0), 0)
    '        End If
    '        lettoreSommaTotale.Close()
    '        chiudiConnessione()
    '    Catch ex As Exception
    '        chiudiConnessione()
    '    End Try
    'End Sub
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


    Protected Sub btnSelPrimi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSelPrimi.Click
        lstListaRapporti.Clear()
        txtRapportiSelezionati.Text = "0"
        txtSommaParziale.Text = "0,00"


        Dim gen As Epifani.ListaGenerale
        Try
            lstListaRapporti.Clear()
            Dim dt As Data.DataTable
            dt = Session.Item("dtresultMorosi")
            Dim i As Integer = 0
            Dim sumIdSelected As Decimal = 0
            For Each r As Data.DataRow In dt.Rows
                gen = New Epifani.ListaGenerale(lstListaRapporti.Count, par.IfNull(r.Item("id"), -1))
                lstListaRapporti.Add(gen)
                gen = Nothing
                i = i + 1
                sumIdSelected += r.Item("debito").ToString

                If i = 150 Then
                    Exit For
                End If
            Next

            txtSommaParziale.Text = Format(sumIdSelected, "##,##0.00")
            txtTotaleMorosita.Text = Format(CDec(par.IfEmpty(dt.Compute("SUM(debito)", String.Empty), 0)), "##,##0.00")


            txtRapportiSelezionati.Text = i
            'txtSommaParziale.Text = txtTotaleMorosita.Text
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class