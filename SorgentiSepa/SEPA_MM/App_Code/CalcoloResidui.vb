Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CalcoloResidui
    Inherits System.Web.Services.WebService

    Shared par As New CM.Global
    Shared connData As CM.datiConnessione

    <WebMethod()> _
    Public Sub caricaDati(ByVal dataAggiornamento As String _
                           , ByVal selectEsercizioContabile As String _
                           , ByVal selectCompetenza As String _
                           , ByVal selectMacrocategoria As String _
                           , ByVal selectCategoria As String _
                           , ByVal selectVoci As String _
                           , ByVal selectTipologiaUI As String _
                           , ByVal condizioneAggiornamento As String _
                           , ByVal condizioneEmissioneDal As String _
                           , ByVal condizioneEmissioneAl As String _
                           , ByVal condizioneRiferimentoDal As String _
                           , ByVal condizioneRiferimentoAl As String _
                           , ByVal condizioneTipologiaBollettazione As String _
                           , ByVal condizioneListaVoci As String _
                           , ByVal condizioneCategoria As String _
                           , ByVal condizioneMacrocategoria As String _
                           , ByVal condizioneCapitoli As String _
                           , ByVal condizioneTipologiaUI As String _
                           , ByVal condizioneCompetenza As String _
                           , ByVal condizioneTipologiaCondominio As String _
                           , ByVal condizioneTipologiaContoCorrente As String _
                           , ByVal condizioneEsercizioContabile As String _
                           , ByVal groupByEsercizioContabile As String _
                           , ByVal groupByCompetenza As String _
                           , ByVal groupByMacrocategoria As String _
                           , ByVal groupByCategoria As String _
                           , ByVal groupByVoci As String _
                           , ByVal groupByTipologiaUI As String _
                           , ByVal vociSi As String _
                           , ByVal tipologiaUISi As String _
                           , ByVal macrocategoriaSi As String _
                           , ByVal dettaglioSi As String _
                           , ByVal condizioneListaVociRes As String _
                           , ByVal condizioneDataPagamentoAl As String _
                           , ByVal condizioneDataPagamentoDal As String _
                           , ByVal condizioneDataContabileDal As String _
                           , ByVal condizioneDataAggiornamento As String _
                           , ByVal condizioneCompetenzaRes As String _
                           , ByVal condizioneMacrocategoriaRes As String _
                           , ByVal condizioneCategoriaRes As String _
                           , ByVal condizioneCapitoliRes As String _
                           , ByVal competenzaSi As String _
                           , ByVal categoriaSi As String _
                           , ByVal capitoliSi As String _
                           , ByVal condizioneDataContabileAl As String _
                           , ByVal condizioneTipologiaBollettazioneRes As String _
                           , ByVal condizioneTipologiaUIRes As String _
                           , ByVal tabella As String _
                           , ByVal dettaglio As String _
                           , ByVal dettaglioGroup As String _
                           , ByVal ordinamento As Integer
                           )

        Try
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET TOTALE=1 WHERE NOME_TABELLA='" & tabella & "'"
            par.cmd.ExecuteNonQuery()

            Dim condizioneAnnulli As String = ""
            Dim condizioneTotale As String = ""
            Dim condizioneEmesso As String = ""
            Dim condizioneIncasso As String = ""
            If dataAggiornamento <> "" Then
                condizioneIncasso = " ''  as incassato, "
                condizioneEmesso = " TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D99')) AS emesso, "
                condizioneAnnulli = " TRIM(TO_CHAR(SUM(NVL(CASE WHEN (DATA_ANNULLO<=" & dataAggiornamento & ") THEN (IMPORTO) ELSE (0) END,0)),'999G999G990D99')) AS ANNULLI, "
                condizioneTotale = " '0,00' AS RESIDUO "
            Else

                condizioneIncasso = " ''  as incassato, "
                condizioneEmesso = " TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D99')) AS emesso, "
                condizioneAnnulli = " TRIM(TO_CHAR(SUM(NVL(CASE WHEN (DATA_ANNULLO<=" & Format(Now, "yyyyMMdd") & ") THEN (IMPORTO) ELSE (0) END,0)),'999G999G990D99')) AS ANNULLI, "
                condizioneTotale = " '0,00' AS RESIDUO "
            End If


            If ordinamento = 1 Then

                par.cmd.CommandText = " SELECT INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE, " _
                & selectEsercizioContabile _
                & dettaglio _
                & selectCompetenza _
                & selectMacrocategoria _
                & selectCategoria _
                & selectVoci _
                & selectTipologiaUI _
                & condizioneEmesso _
                & condizioneAnnulli _
                & condizioneIncasso _
                & condizioneTotale _
                & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI " _
                & " WHERE " _
                & condizioneAggiornamento _
                & condizioneEmissioneDal _
                & condizioneEmissioneAl _
                & condizioneRiferimentoDal _
                & condizioneRiferimentoAl _
                & condizioneTipologiaBollettazione _
                & condizioneListaVoci _
                & condizioneCategoria _
                & condizioneMacrocategoria _
                & condizioneCapitoli _
                & condizioneTipologiaUI _
                & condizioneCompetenza _
                & condizioneTipologiaCondominio _
                & condizioneTipologiaContoCorrente _
                & condizioneEsercizioContabile _
                & " AND BOL_BOLLETTE_VOCI_EMISSIONI.FL_ACCERTATO_BOL_BOLLETTE_VOCI=1 " _
                & " GROUP BY ROLLUP (INITCAP(BOLLETTAZIONE)," _
                & groupByEsercizioContabile _
                & dettaglioGroup _
                & groupByCompetenza _
                & groupByMacrocategoria _
                & groupByCategoria _
                & groupByVoci _
                & groupByTipologiaUI _
                & ") "

                Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dTable As New Data.DataTable
                dAdapter.Fill(dTable)
                Dim condizioneBollettazione As String = ""
                Dim condizioneCapitolo As String = ""
                Dim condizioneAnno As String = ""
                Dim condizioneBimestre As String = ""
                Dim condizioneCompetenzaEmissione As String = ""
                Dim condizioneMacrocategoriaEmissione As String = ""
                Dim condizioneVoce As String = ""
                Dim condizioneUsoUI As String = ""
                Dim condizionetipologiaUIEmissione As String = ""
                Dim valorePrecedente As Decimal = 0
                For Each Items As Data.DataRow In dTable.Rows
                    If Not IsDBNull(Items.Item(10)) Then
                        valorePrecedente = Items.Item(10)
                    Else
                        Items.Item(10) = valorePrecedente
                    End If
                Next
                Dim dataTableRibaltata As New Data.DataTable
                dataTableRibaltata.Columns.Clear()
                For Each colonna As Data.DataColumn In dTable.Columns
                    dataTableRibaltata.Columns.Add(colonna.ColumnName)
                Next
                Dim Nrighe As Integer = dTable.Rows.Count
                Dim riga As Data.DataRow
                Dim rigaPrec As Data.DataRow
                Dim rigaConfronto As Data.DataRow
                Dim bollettazionePrecedente As String = ""
                Dim annoPrecedente As String = ""
                Dim capitoloPrecedente As String = ""
                Dim bimestrePrecedente As String = ""
                Dim competenzaPrecedente As String = ""
                Dim macrocategoriaPrecedente As String = ""
                Dim categoriaPrecedente As String = ""
                Dim vocePrecedente As String = ""
                Dim usiAbitativiPrecedente As String = ""
                Dim tipoUIPrecedente As String = ""
                rigaPrec = dataTableRibaltata.NewRow
                For i As Integer = 0 To Nrighe - 1
                    riga = dataTableRibaltata.NewRow
                    rigaConfronto = dataTableRibaltata.NewRow
                    If i = 0 Then
                        Dim indice As Integer = 0
                        riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>Totale</font>"
                        indice += 1
                        riga.Item("CAPITOLO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("ANNO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("BIMESTRE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("COMPETENZA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("MACROCATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("CATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("VOCE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("USI_ABITATIVI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("TIPO_UI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("EMESSO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        indice += 1
                        riga.Item("ANNULLI") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        indice += 1
                        riga.Item("INCASSATO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        indice += 1
                        riga.Item("RESIDUO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        rigaPrec = riga

                    Else
                        Dim indice2 As Integer = 0

                        rigaConfronto.Item("BOLLETTAZIONE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CAPITOLO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("ANNO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("BIMESTRE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("COMPETENZA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("MACROCATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("VOCE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("USI_ABITATIVI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("TIPO_UI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("EMESSO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("ANNULLI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("INCASSATO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("RESIDUO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")

                        Dim indice As Integer = 0
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("BOLLETTAZIONE") = ""
                            Else
                                riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                capitoloPrecedente = ""
                                annoPrecedente = ""
                                bimestrePrecedente = ""
                                competenzaPrecedente = ""
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                usiAbitativiPrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("BOLLETTAZIONE") = ""
                        End If
                        indice += 1

                        If capitoliSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CAPITOLO") = ""
                                Else
                                    riga.Item("CAPITOLO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                    capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoPrecedente = ""
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CAPITOLO") = ""
                            End If

                        End If
                        indice += 1

                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If annoPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("ANNO") = ""
                                Else
                                    riga.Item("ANNO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoPrecedente = riga.Item("ANNO")
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("ANNO") = ""
                            End If
                        End If

                        indice += 1

                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If bimestrePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("BIMESTRE") = ""
                                Else
                                    riga.Item("BIMESTRE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    bimestrePrecedente = riga.Item("BIMESTRE")
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("BIMESTRE") = ""
                            End If
                        End If

                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If competenzaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("COMPETENZA") = ""
                            Else
                                riga.Item("COMPETENZA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                competenzaPrecedente = riga.Item("COMPETENZA")
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                usiAbitativiPrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("COMPETENZA") = ""
                        End If
                        indice += 1
                        If macrocategoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If macrocategoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("MACROCATEGORIA") = ""
                                Else
                                    riga.Item("MACROCATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    macrocategoriaPrecedente = riga.Item("MACROCATEGORIA")
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("MACROCATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If categoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If categoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CATEGORIA") = ""
                                Else
                                    riga.Item("CATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    categoriaPrecedente = riga.Item("CATEGORIA")
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If vociSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If vocePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("VOCE") = ""
                                Else
                                    riga.Item("VOCE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    vocePrecedente = riga.Item("VOCE")
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("VOCE") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If usiAbitativiPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("USI_ABITATIVI") = ""
                            Else
                                If IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice + 1)) Then
                                    riga.Item("USI_ABITATIVI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = ""
                                Else
                                    riga.Item("USI_ABITATIVI") = ""
                                End If
                            End If
                        Else
                            riga.Item("USI_ABITATIVI") = ""
                        End If
                        indice += 1
                        If tipologiaUISi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("TIPO_UI") = ""
                                Else
                                    riga.Item("TIPO_UI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = riga.Item("TIPO_UI")
                                End If
                            Else
                                riga.Item("TIPO_UI") = ""
                            End If
                            indice += 1
                        Else
                            indice += 1
                        End If
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("EMESSO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("EMESSO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("EMESSO") = ""
                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("ANNULLI") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("ANNULLI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("ANNULLI") = ""
                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("INCASSATO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("INCASSATO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("INCASSATO") = "<font style='font-weight:bold;font-style:italic;'></font>"
                            Else
                                riga.Item("INCASSATO") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("RESIDUO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("RESIDUO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("RESIDUO") = ""
                        End If

                    End If
                    'rigaPrec.Item("ACCERTATO") = par.IfNull(rigaConfronto.Item("ACCERTATO"), "")
                    If par.IfNull(rigaPrec.Item("TIPO_UI"), "") = par.IfNull(rigaConfronto.Item("TIPO_UI"), "") _
                        And par.IfNull(rigaPrec.Item("EMESSO"), "") = par.IfNull(rigaConfronto.Item("EMESSO"), "") _
                        And par.IfNull(rigaPrec.Item("ANNULLI"), "") = par.IfNull(rigaConfronto.Item("ANNULLI"), "") _
                        And par.IfNull(rigaPrec.Item("INCASSATO"), "") = par.IfNull(rigaConfronto.Item("INCASSATO"), "") _
                        And par.IfNull(rigaPrec.Item("RESIDUO"), "") = par.IfNull(rigaConfronto.Item("RESIDUO"), "") _
                        And par.IfNull(riga.Item("anno"), "") = "" _
                        And par.IfNull(riga.Item("capitolo"), "") = "" _
                        And par.IfNull(riga.Item("bimestre"), "") = "" _
                        And par.IfNull(riga.Item("competenza"), "") = "" _
                        And par.IfNull(riga.Item("macrocategoria"), "") = "" _
                        And par.IfNull(riga.Item("bollettazione"), "") = "" _
                        And par.IfNull(riga.Item("categoria"), "") = "" _
                        And par.IfNull(riga.Item("voce"), "") = "" _
                        And par.IfNull(riga.Item("USI_ABITATIVI"), "") = "" Then
                        'RIGHE UGUALI DA ELIMINARE

                        'If par.IfNull(rigaPrec.Item("TIPO_UI"), "") <> "" Then
                        'dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(13) = par.IfNull(rigaConfronto.Item("ACCERTATO"), "")
                        'End If

                        If par.IfNull(riga.Item("TIPO_UI"), "") = par.IfNull(dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(9), "") Then
                            riga.Item("TIPO_UI") = ""
                        End If

                    Else
                        'If (Nrighe - 1 - i) <> 1 Then 'And (Nrighe - 1 - i) <> 2
                        dataTableRibaltata.Rows.Add(riga)
                        rigaPrec = rigaConfronto
                        'End If
                    End If
                Next



                For i As Integer = 0 To dataTableRibaltata.Rows.Count - 1

                    If i = 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET PARZIALE=" & i + 1 & ",TOTALE=" & dataTableRibaltata.Rows.Count & " WHERE NOME_TABELLA='" & tabella & "'"
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET PARZIALE=" & i + 1 & " WHERE NOME_TABELLA='" & tabella & "'"
                        par.cmd.ExecuteNonQuery()
                    End If


                    If i = 0 Then
                        condizioneBollettazione = " AND BOLLETTAZIONE>'#' "
                        condizioneCapitolo = " AND CAPITOLO>'#' "
                        condizioneAnno = " AND ANNO>'#' "
                        condizioneBimestre = " AND BIMESTRE>'#' "
                        condizioneCompetenzaEmissione = " AND COMPETENZA>'#' "
                        condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA>'#' "
                        condizioneVoce = " AND VOCE>'#' "
                        condizioneUsoUI = " AND USI_ABITATIVI>'#' "
                        condizionetipologiaUIEmissione = " AND TIPO_UI>'#' "
                    Else
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("BOLLETTAZIONE"), "") <> "" Then
                            condizioneBollettazione = " AND BOLLETTAZIONE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("BOLLETTAZIONE")), "'", "''") & "' "
                            condizioneCapitolo = ""
                            condizioneAnno = ""
                            condizioneBimestre = ""
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("CAPITOLO"), "") <> "" Then
                            condizioneCapitolo = " AND CAPITOLO='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("CAPITOLO")), "'", "''") & "' "
                            condizioneAnno = ""
                            condizioneBimestre = ""
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("ANNO"), "") <> "" Then
                            condizioneAnno = " AND ANNO='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("ANNO")), "'", "''") & "' "
                            condizioneBimestre = ""
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("BIMESTRE"), "") <> "" Then
                            condizioneBimestre = " AND BIMESTRE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("BIMESTRE")), "'", "''") & "' "
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("COMPETENZA"), "") <> "" Then
                            condizioneCompetenzaEmissione = " AND COMPETENZA='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("COMPETENZA")), "'", "''") & "' "
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("MACROCATEGORIA"), "") <> "" Then
                            condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("MACROCATEGORIA")), "'", "''") & "' "
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("VOCE"), "") <> "" Then
                            condizioneVoce = " AND VOCE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("VOCE")), "'", "''") & "' "
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("USI_ABITATIVI"), "") <> "" Then
                            condizioneUsoUI = " AND USI_ABITATIVI='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("USI_ABITATIVI")), "'", "''") & "' "
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("TIPO_UI"), "") <> "" Then
                            condizionetipologiaUIEmissione = " AND TIPO_UI='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("TIPO_UI")), "'", "''") & "' "
                        End If
                    End If




                    par.cmd.CommandText = " select TRIM(TO_CHAR(nvl(SUM(NVL(IMPORTO_PAGATO,0)),0),'999G999G990D99')) AS IMPORTO " _
                        & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                        & " WHERE " _
                        & condizioneDataAggiornamento _
                        & condizioneDataContabileDal _
                        & condizioneDataContabileAl _
                        & condizioneDataPagamentoDal _
                        & condizioneDataPagamentoAl _
                        & condizioneRiferimentoAl _
                        & condizioneRiferimentoDal _
                        & condizioneEmissioneDal _
                        & condizioneEmissioneAl _
                        & condizioneTipologiaCondominio _
                        & condizioneTipologiaContoCorrente _
                        & condizioneBollettazione _
                        & condizioneCapitolo _
                        & condizioneAnno _
                        & condizioneBimestre _
                        & condizioneCompetenzaEmissione _
                        & condizioneMacrocategoriaEmissione _
                        & condizioneVoce _
                        & condizioneUsoUI _
                        & condizionetipologiaUIEmissione _
                        & condizioneTipologiaBollettazioneRes _
                        & condizioneListaVociRes _
                        & condizioneCategoriaRes _
                        & condizioneMacrocategoriaRes _
                        & condizioneCapitoliRes _
                        & condizioneTipologiaUIRes _
                        & condizioneCompetenzaRes _
                        & condizioneEsercizioContabile



                    Dim lettoreIncasso As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreIncasso.Read Then
                        If par.IfNull(lettoreIncasso(0), 0) <> 0 Then
                            If Left(dataTableRibaltata.Rows(i).Item("incassato"), 1) = "<" Then
                                dataTableRibaltata.Rows(i).Item("incassato") = "<font style='font-weight:bold;font-style:italic;'>" & par.IfNull(lettoreIncasso(0), 0) & "</font>"
                            Else
                                dataTableRibaltata.Rows(i).Item("incassato") = par.IfNull(lettoreIncasso(0), 0)
                            End If

                        Else
                            If Left(dataTableRibaltata.Rows(i).Item("incassato"), 1) = "<" Then
                                dataTableRibaltata.Rows(i).Item("incassato") = "<font style='font-weight:bold;font-style:italic;'>" & "0,00" & "</font>"
                            Else
                                dataTableRibaltata.Rows(i).Item("incassato") = "0,00"
                            End If

                        End If
                    End If
                    If Left(dataTableRibaltata.Rows(i).Item("residuo"), 1) = "<" Then
                        dataTableRibaltata.Rows(i).Item("residuo") = "<font style='font-weight:bold;font-style:italic;'>" & Format(CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("emesso"))) - CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("annulli"))) - CDec(par.IfNull(lettoreIncasso(0), 0)), "##,##0.00") & "</font>"
                    Else
                        dataTableRibaltata.Rows(i).Item("residuo") = Format(CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("emesso"))) - CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("annulli"))) - CDec(par.IfNull(lettoreIncasso(0), 0)), "##,##0.00")
                    End If

                    lettoreIncasso.Close()
                Next

                If dataTableRibaltata.Rows.Count > 0 Then
                    'Dim indiceVisibile As Integer = 1
                    'If Not capitoliSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile = 3
                    'If Not dettaglioSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not dettaglioSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not competenzaSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not macrocategoriaSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not categoriaSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not vociSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not tipologiaUISi Then
                    '    'DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    '    indiceVisibile += 1
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'DataGridResidui.DataSource = dataTableRibaltata

                    par.cmd.CommandText = "CREATE TABLE SEPA." & tabella _
                        & " (ID NUMBER,BOLLETTAZIONE   VARCHAR2 (200)," _
                        & " CAPITOLO         VARCHAR2 (200)," _
                        & " ANNO             VARCHAR2 (200)," _
                        & " BIMESTRE         VARCHAR2 (200)," _
                        & " COMPETENZA       VARCHAR2 (200)," _
                        & " MACROCATEGORIA   VARCHAR2 (200)," _
                        & " CATEGORIA        VARCHAR2 (200), " _
                        & " VOCE             VARCHAR2 (200)," _
                        & " USI_ABITATIVI    VARCHAR2 (200)," _
                        & " TIPOLOGIA_UI     VARCHAR2 (200)," _
                        & " EMESSO           VARCHAR2 (200)," _
                        & " ANNULLI          VARCHAR2 (200)," _
                        & " INCASSATO        VARCHAR2 (200)," _
                        & " RESIDUO          VARCHAR2 (200)" _
                        & ") "
                    par.cmd.ExecuteNonQuery()
                    Dim contatore As Integer = 0
                    For Each rigaRibaltata As Data.DataRow In dataTableRibaltata.Rows
                        contatore += 1
                        par.cmd.CommandText = "INSERT INTO SEPA." & tabella _
                            & " (ID,BOLLETTAZIONE," _
                            & " CAPITOLO," _
                            & " ANNO," _
                            & " BIMESTRE," _
                            & " COMPETENZA," _
                            & " MACROCATEGORIA," _
                            & " CATEGORIA, " _
                            & " VOCE," _
                            & " USI_ABITATIVI," _
                            & " TIPOLOGIA_UI," _
                            & " EMESSO," _
                            & " ANNULLI," _
                            & " INCASSATO," _
                            & " RESIDUO)" _
                            & " VALUES(" & contatore & ",'" & rigaRibaltata(0).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(1).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(2).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(3).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(4).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(5).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(6).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(7).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(8).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(9).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(10).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(11).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(12).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(13).ToString.Replace("'", "''") & "'" _
                            & ")"
                        par.cmd.ExecuteNonQuery()
                    Next



                    par.cmd.CommandText = "update siscom_mi.procedure_residui set esito=1,data_ora_fine='" & Format(Now, "yyyyMMddHHmmss") & "'  where nome_tabella='" & tabella & "'"
                    par.cmd.ExecuteNonQuery()


                    'Session.Remove("TAB_TEMPORANEA")
                    'DataGridResidui.DataBind()
                    'ImageButtonExcel.Visible = True
                    'ImageButtonStampa.Visible = True
                    ''ImageButtonStampaAccerta.Visible = True
                    'LabelTitolo.Text = "Situazione Residui"

                    'Else
                    '    LabelErrore.Text = "La ricerca non ha prodotto nessun risultato! Modificare i parametri di ricerca e riprovare"
                    '    ImageButtonExcel.Visible = False
                    '    ImageButtonStampa.Visible = False
                    '    'ImageButtonStampaAccerta.Visible = False
                    '    LabelTitolo.Text = "Situazione Residui"
                End If
                connData.chiudi()


            Else
                par.cmd.CommandText = " SELECT  " _
                & selectEsercizioContabile _
                & " INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE, " _
                & dettaglio _
                & selectCompetenza _
                & selectMacrocategoria _
                & selectCategoria _
                & selectVoci _
                & selectTipologiaUI _
                & condizioneEmesso _
                & condizioneAnnulli _
                & condizioneIncasso _
                & condizioneTotale _
                & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI " _
                & " WHERE " _
                & condizioneAggiornamento _
                & condizioneEmissioneDal _
                & condizioneEmissioneAl _
                & condizioneRiferimentoDal _
                & condizioneRiferimentoAl _
                & condizioneTipologiaBollettazione _
                & condizioneListaVoci _
                & condizioneCategoria _
                & condizioneMacrocategoria _
                & condizioneCapitoli _
                & condizioneTipologiaUI _
                & condizioneCompetenza _
                & condizioneTipologiaCondominio _
                & condizioneTipologiaContoCorrente _
                & condizioneEsercizioContabile _
                & " AND BOL_BOLLETTE_VOCI_EMISSIONI.FL_ACCERTATO_BOL_BOLLETTE_VOCI=1 " _
                & " GROUP BY ROLLUP (" _
                & groupByEsercizioContabile _
                & ",INITCAP(BOLLETTAZIONE) " _
                & dettaglioGroup _
                & groupByCompetenza _
                & groupByMacrocategoria _
                & groupByCategoria _
                & groupByVoci _
                & groupByTipologiaUI _
                & ") "

                Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dTable As New Data.DataTable
                dAdapter.Fill(dTable)
                Dim condizioneBollettazione As String = ""
                Dim condizioneCapitolo As String = ""
                Dim condizioneAnno As String = ""
                Dim condizioneBimestre As String = ""
                Dim condizioneCompetenzaEmissione As String = ""
                Dim condizioneMacrocategoriaEmissione As String = ""
                Dim condizioneVoce As String = ""
                Dim condizioneUsoUI As String = ""
                Dim condizionetipologiaUIEmissione As String = ""
                Dim valorePrecedente As Decimal = 0
                For Each Items As Data.DataRow In dTable.Rows
                    If Not IsDBNull(Items.Item(10)) Then
                        valorePrecedente = Items.Item(10)
                    Else
                        Items.Item(10) = valorePrecedente
                    End If
                Next
                Dim dataTableRibaltata As New Data.DataTable
                dataTableRibaltata.Columns.Clear()
                For Each colonna As Data.DataColumn In dTable.Columns
                    dataTableRibaltata.Columns.Add(colonna.ColumnName)
                Next
                Dim Nrighe As Integer = dTable.Rows.Count
                Dim riga As Data.DataRow
                Dim rigaPrec As Data.DataRow
                Dim rigaConfronto As Data.DataRow
                Dim bollettazionePrecedente As String = ""
                Dim annoPrecedente As String = ""
                Dim capitoloPrecedente As String = ""
                Dim bimestrePrecedente As String = ""
                Dim competenzaPrecedente As String = ""
                Dim macrocategoriaPrecedente As String = ""
                Dim categoriaPrecedente As String = ""
                Dim vocePrecedente As String = ""
                Dim usiAbitativiPrecedente As String = ""
                Dim tipoUIPrecedente As String = ""
                rigaPrec = dataTableRibaltata.NewRow
                For i As Integer = 0 To Nrighe - 1
                    riga = dataTableRibaltata.NewRow
                    rigaConfronto = dataTableRibaltata.NewRow
                    If i = 0 Then
                        Dim indice As Integer = 0
                        riga.Item("CAPITOLO") = "<font style='font-weight:bold;font-style:italic;'>Totale</font>"
                        indice += 1
                        riga.Item("BOLLETTAZIONE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("ANNO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("BIMESTRE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("COMPETENZA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("MACROCATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("CATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("VOCE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("USI_ABITATIVI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("TIPO_UI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("EMESSO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        indice += 1
                        riga.Item("ANNULLI") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        indice += 1
                        riga.Item("INCASSATO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        indice += 1
                        riga.Item("RESIDUO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        rigaPrec = riga

                    Else
                        Dim indice2 As Integer = 0

                        rigaConfronto.Item("CAPITOLO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("BOLLETTAZIONE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("ANNO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("BIMESTRE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("COMPETENZA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("MACROCATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("VOCE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("USI_ABITATIVI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("TIPO_UI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("EMESSO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("ANNULLI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("INCASSATO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("RESIDUO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")

                        Dim indice As Integer = 0


                        If capitoliSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CAPITOLO") = ""
                                Else
                                    riga.Item("CAPITOLO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                    capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    bollettazionePrecedente = ""
                                    annoPrecedente = ""
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CAPITOLO") = ""
                            End If

                        End If
                        indice += 1

                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("BOLLETTAZIONE") = ""
                            Else
                                riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                annoPrecedente = ""
                                bimestrePrecedente = ""
                                competenzaPrecedente = ""
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                usiAbitativiPrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("BOLLETTAZIONE") = ""
                        End If
                        indice += 1


                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If annoPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("ANNO") = ""
                                Else
                                    riga.Item("ANNO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoPrecedente = riga.Item("ANNO")
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("ANNO") = ""
                            End If
                        End If

                        indice += 1

                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If bimestrePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("BIMESTRE") = ""
                                Else
                                    riga.Item("BIMESTRE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    bimestrePrecedente = riga.Item("BIMESTRE")
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("BIMESTRE") = ""
                            End If
                        End If

                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If competenzaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("COMPETENZA") = ""
                            Else
                                riga.Item("COMPETENZA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                competenzaPrecedente = riga.Item("COMPETENZA")
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                usiAbitativiPrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("COMPETENZA") = ""
                        End If
                        indice += 1
                        If macrocategoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If macrocategoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("MACROCATEGORIA") = ""
                                Else
                                    riga.Item("MACROCATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    macrocategoriaPrecedente = riga.Item("MACROCATEGORIA")
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("MACROCATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If categoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If categoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CATEGORIA") = ""
                                Else
                                    riga.Item("CATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    categoriaPrecedente = riga.Item("CATEGORIA")
                                    vocePrecedente = ""
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If vociSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If vocePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("VOCE") = ""
                                Else
                                    riga.Item("VOCE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    vocePrecedente = riga.Item("VOCE")
                                    usiAbitativiPrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("VOCE") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If usiAbitativiPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("USI_ABITATIVI") = ""
                            Else
                                If IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice + 1)) Then
                                    riga.Item("USI_ABITATIVI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = ""
                                Else
                                    riga.Item("USI_ABITATIVI") = ""
                                End If
                                'tipoUIPrecedente = riga.Item("USI_ABITATIVI")
                            End If
                        Else
                            riga.Item("USI_ABITATIVI") = ""
                        End If
                        indice += 1
                        If tipologiaUISi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("TIPO_UI") = ""
                                Else
                                    riga.Item("TIPO_UI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = riga.Item("TIPO_UI")
                                End If
                            Else
                                riga.Item("TIPO_UI") = ""
                            End If
                            indice += 1
                        Else
                            indice += 1
                        End If
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("EMESSO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("EMESSO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("EMESSO") = ""
                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("ANNULLI") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("ANNULLI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("ANNULLI") = ""
                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("INCASSATO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("INCASSATO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("INCASSATO") = "<font style='font-weight:bold;font-style:italic;'></font>"
                            Else
                                riga.Item("INCASSATO") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Then
                                riga.Item("RESIDUO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("RESIDUO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("RESIDUO") = ""
                        End If

                    End If
                    'rigaPrec.Item("ACCERTATO") = par.IfNull(rigaConfronto.Item("ACCERTATO"), "")
                    If par.IfNull(rigaPrec.Item("TIPO_UI"), "") = par.IfNull(rigaConfronto.Item("TIPO_UI"), "") _
                        And par.IfNull(rigaPrec.Item("EMESSO"), "") = par.IfNull(rigaConfronto.Item("EMESSO"), "") _
                        And par.IfNull(rigaPrec.Item("ANNULLI"), "") = par.IfNull(rigaConfronto.Item("ANNULLI"), "") _
                        And par.IfNull(rigaPrec.Item("INCASSATO"), "") = par.IfNull(rigaConfronto.Item("INCASSATO"), "") _
                        And par.IfNull(rigaPrec.Item("RESIDUO"), "") = par.IfNull(rigaConfronto.Item("RESIDUO"), "") _
                        And par.IfNull(riga.Item("anno"), "") = "" _
                        And par.IfNull(riga.Item("capitolo"), "") = "" _
                        And par.IfNull(riga.Item("bimestre"), "") = "" _
                        And par.IfNull(riga.Item("competenza"), "") = "" _
                        And par.IfNull(riga.Item("macrocategoria"), "") = "" _
                        And par.IfNull(riga.Item("bollettazione"), "") = "" _
                        And par.IfNull(riga.Item("categoria"), "") = "" _
                        And par.IfNull(riga.Item("voce"), "") = "" _
                        And par.IfNull(riga.Item("USI_ABITATIVI"), "") = "" Then
                        'RIGHE UGUALI DA ELIMINARE

                        'If par.IfNull(rigaPrec.Item("TIPO_UI"), "") <> "" Then
                        'dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(13) = par.IfNull(rigaConfronto.Item("ACCERTATO"), "")
                        'End If

                        If par.IfNull(riga.Item("TIPO_UI"), "") = par.IfNull(dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(9), "") Then
                            riga.Item("TIPO_UI") = ""
                        End If

                    Else
                        'If (Nrighe - 1 - i) <> 1 Then 'And (Nrighe - 1 - i) <> 2
                        dataTableRibaltata.Rows.Add(riga)
                        rigaPrec = rigaConfronto
                        'End If
                    End If
                Next



                For i As Integer = 0 To dataTableRibaltata.Rows.Count - 1

                    If i = 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET PARZIALE=" & i + 1 & ",TOTALE=" & dataTableRibaltata.Rows.Count & " WHERE NOME_TABELLA='" & tabella & "'"
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET PARZIALE=" & i + 1 & " WHERE NOME_TABELLA='" & tabella & "'"
                        par.cmd.ExecuteNonQuery()
                    End If


                    If i = 0 Then
                        condizioneBollettazione = " AND BOLLETTAZIONE>'#' "
                        condizioneCapitolo = " AND CAPITOLO>'#' "
                        condizioneAnno = " AND ANNO>'#' "
                        condizioneBimestre = " AND BIMESTRE>'#' "
                        condizioneCompetenzaEmissione = " AND COMPETENZA>'#' "
                        condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA>'#' "
                        condizioneVoce = " AND VOCE>'#' "
                        condizioneUsoUI = " AND USI_ABITATIVI>'#' "
                        condizionetipologiaUIEmissione = " AND TIPO_UI>'#' "
                    Else
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("CAPITOLO"), "") <> "" Then
                            condizioneCapitolo = " AND CAPITOLO='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("CAPITOLO")), "'", "''") & "' "
                            condizioneBollettazione = ""
                            condizioneAnno = ""
                            condizioneBimestre = ""
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("BOLLETTAZIONE"), "") <> "" Then
                            condizioneBollettazione = " AND BOLLETTAZIONE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("BOLLETTAZIONE")), "'", "''") & "' "
                            condizioneAnno = ""
                            condizioneBimestre = ""
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("ANNO"), "") <> "" Then
                            condizioneAnno = " AND ANNO='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("ANNO")), "'", "''") & "' "
                            condizioneBimestre = ""
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("BIMESTRE"), "") <> "" Then
                            condizioneBimestre = " AND BIMESTRE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("BIMESTRE")), "'", "''") & "' "
                            condizioneCompetenzaEmissione = ""
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("COMPETENZA"), "") <> "" Then
                            condizioneCompetenzaEmissione = " AND COMPETENZA='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("COMPETENZA")), "'", "''") & "' "
                            condizioneMacrocategoriaEmissione = ""
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("MACROCATEGORIA"), "") <> "" Then
                            condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("MACROCATEGORIA")), "'", "''") & "' "
                            condizioneVoce = ""
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("VOCE"), "") <> "" Then
                            condizioneVoce = " AND VOCE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("VOCE")), "'", "''") & "' "
                            condizioneUsoUI = ""
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("USI_ABITATIVI"), "") <> "" Then
                            condizioneUsoUI = " AND USI_ABITATIVI='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("USI_ABITATIVI")), "'", "''") & "' "
                            condizionetipologiaUIEmissione = ""
                        End If
                        If par.IfNull(dataTableRibaltata.Rows(i).Item("TIPO_UI"), "") <> "" Then
                            condizionetipologiaUIEmissione = " AND TIPO_UI='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("TIPO_UI")), "'", "''") & "' "
                        End If
                    End If

                    par.cmd.CommandText = " select TRIM(TO_CHAR(nvl(SUM(NVL(IMPORTO_PAGATO,0)),0),'999G999G990D99')) AS IMPORTO " _
                        & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                        & " WHERE " _
                        & condizioneDataAggiornamento _
                        & condizioneDataContabileDal _
                        & condizioneDataContabileAl _
                        & condizioneDataPagamentoDal _
                        & condizioneDataPagamentoAl _
                        & condizioneRiferimentoAl _
                        & condizioneRiferimentoDal _
                        & condizioneEmissioneDal _
                        & condizioneEmissioneAl _
                        & condizioneTipologiaCondominio _
                        & condizioneTipologiaContoCorrente _
                        & condizioneBollettazione _
                        & condizioneCapitolo _
                        & condizioneAnno _
                        & condizioneBimestre _
                        & condizioneCompetenzaEmissione _
                        & condizioneMacrocategoriaEmissione _
                        & condizioneVoce _
                        & condizioneUsoUI _
                        & condizionetipologiaUIEmissione _
                        & condizioneTipologiaBollettazioneRes _
                        & condizioneListaVociRes _
                        & condizioneCategoriaRes _
                        & condizioneMacrocategoriaRes _
                        & condizioneCapitoliRes _
                        & condizioneTipologiaUIRes _
                        & condizioneCompetenzaRes _
                        & condizioneEsercizioContabile



                    Dim lettoreIncasso As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreIncasso.Read Then
                        If par.IfNull(lettoreIncasso(0), 0) <> 0 Then
                            If Left(dataTableRibaltata.Rows(i).Item("incassato"), 1) = "<" Then
                                dataTableRibaltata.Rows(i).Item("incassato") = "<font style='font-weight:bold;font-style:italic;'>" & par.IfNull(lettoreIncasso(0), 0) & "</font>"
                            Else
                                dataTableRibaltata.Rows(i).Item("incassato") = par.IfNull(lettoreIncasso(0), 0)
                            End If

                        Else
                            If Left(dataTableRibaltata.Rows(i).Item("incassato"), 1) = "<" Then
                                dataTableRibaltata.Rows(i).Item("incassato") = "<font style='font-weight:bold;font-style:italic;'>" & "0,00" & "</font>"
                            Else
                                dataTableRibaltata.Rows(i).Item("incassato") = "0,00"
                            End If

                        End If
                    End If
                    If Left(dataTableRibaltata.Rows(i).Item("residuo"), 1) = "<" Then
                        dataTableRibaltata.Rows(i).Item("residuo") = "<font style='font-weight:bold;font-style:italic;'>" & Format(CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("emesso"))) - CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("annulli"))) - CDec(par.IfNull(lettoreIncasso(0), 0)), "##,##0.00") & "</font>"
                    Else
                        dataTableRibaltata.Rows(i).Item("residuo") = Format(CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("emesso"))) - CDec(par.EliminaFont(dataTableRibaltata.Rows(i).Item("annulli"))) - CDec(par.IfNull(lettoreIncasso(0), 0)), "##,##0.00")
                    End If

                    lettoreIncasso.Close()
                Next

                If dataTableRibaltata.Rows.Count > 0 Then
                    'Dim indiceVisibile As Integer = 1
                    'If Not capitoliSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile = 3
                    'If Not dettaglioSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not dettaglioSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not competenzaSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not macrocategoriaSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not categoriaSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not vociSi Then
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'indiceVisibile += 1
                    'If Not tipologiaUISi Then
                    '    'DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    '    indiceVisibile += 1
                    '    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    'End If
                    'DataGridResidui.DataSource = dataTableRibaltata

                    par.cmd.CommandText = "CREATE TABLE SEPA." & tabella _
                        & " (ID NUMBER,BOLLETTAZIONE   VARCHAR2 (200)," _
                        & " CAPITOLO         VARCHAR2 (200)," _
                        & " ANNO             VARCHAR2 (200)," _
                        & " BIMESTRE         VARCHAR2 (200)," _
                        & " COMPETENZA       VARCHAR2 (200)," _
                        & " MACROCATEGORIA   VARCHAR2 (200)," _
                        & " CATEGORIA        VARCHAR2 (200), " _
                        & " VOCE             VARCHAR2 (200)," _
                        & " USI_ABITATIVI    VARCHAR2 (200)," _
                        & " TIPOLOGIA_UI     VARCHAR2 (200)," _
                        & " EMESSO           VARCHAR2 (200)," _
                        & " ANNULLI          VARCHAR2 (200)," _
                        & " INCASSATO        VARCHAR2 (200)," _
                        & " RESIDUO          VARCHAR2 (200)" _
                        & ") "
                    par.cmd.ExecuteNonQuery()
                    Dim contatore As Integer = 0
                    For Each rigaRibaltata As Data.DataRow In dataTableRibaltata.Rows
                        contatore += 1
                        par.cmd.CommandText = "INSERT INTO SEPA." & tabella _
                            & " (ID,BOLLETTAZIONE," _
                            & " CAPITOLO," _
                            & " ANNO," _
                            & " BIMESTRE," _
                            & " COMPETENZA," _
                            & " MACROCATEGORIA," _
                            & " CATEGORIA, " _
                            & " VOCE," _
                            & " USI_ABITATIVI," _
                            & " TIPOLOGIA_UI," _
                            & " EMESSO," _
                            & " ANNULLI," _
                            & " INCASSATO," _
                            & " RESIDUO)" _
                            & " VALUES(" & contatore & ",'" & rigaRibaltata(0).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(1).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(2).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(3).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(4).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(5).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(6).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(7).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(8).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(9).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(10).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(11).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(12).ToString.Replace("'", "''") & "'," _
                            & "'" & rigaRibaltata(13).ToString.Replace("'", "''") & "'" _
                            & ")"
                        par.cmd.ExecuteNonQuery()
                    Next



                    par.cmd.CommandText = "update siscom_mi.procedure_residui set esito=1,data_ora_fine='" & Format(Now, "yyyyMMddHHmmss") & "'  where nome_tabella='" & tabella & "'"
                    par.cmd.ExecuteNonQuery()


                    'Session.Remove("TAB_TEMPORANEA")
                    'DataGridResidui.DataBind()
                    'ImageButtonExcel.Visible = True
                    'ImageButtonStampa.Visible = True
                    ''ImageButtonStampaAccerta.Visible = True
                    'LabelTitolo.Text = "Situazione Residui"

                    'Else
                    '    LabelErrore.Text = "La ricerca non ha prodotto nessun risultato! Modificare i parametri di ricerca e riprovare"
                    '    ImageButtonExcel.Visible = False
                    '    ImageButtonStampa.Visible = False
                    '    'ImageButtonStampaAccerta.Visible = False
                    '    LabelTitolo.Text = "Situazione Residui"
                End If
                connData.chiudi()
            End If

        Catch ex As Exception
            par.cmd.CommandText = "update siscom_mi.procedure_residui set esito=2,NOTE='" & Replace(ex.Message, "'", "''") & "',DATA_ORA_FINE='" & Format(Now, "yyyyMMddHHmmss") & "'  where nome_tabella='" & tabella & "'"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            'Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
        End Try
    End Sub
End Class