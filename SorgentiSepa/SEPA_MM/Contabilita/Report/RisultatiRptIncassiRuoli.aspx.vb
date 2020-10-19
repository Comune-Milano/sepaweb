Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip

Partial Class Contabilita_Report_RisultatiRptIncassiRuoli
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)

        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            Response.Flush()
            codContratto.Value = Request.QueryString("CodContratto")
            If codContratto.Value <> "0" Then
                CaricaInfoRU()
            End If
            caricaDati()
            filtriRicerca = Session.Item("filtriRicerca")
            Session.Remove("filtriRicerca")
        End If
    End Sub

    Public Property filtriRicerca() As String
        Get
            If Not (ViewState("filtriRicerca") Is Nothing) Then
                Return CStr(ViewState("filtriRicerca"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("filtriRicerca") = value
        End Set
    End Property

    Private Sub caricaDati()

        '######## DATA PAGAMENTO ##################
        Dim DataPagamentoDal As String = ""
        If Not IsNothing(Request.QueryString("DataPagamentoDal")) Then
            DataPagamentoDal = Request.QueryString("DataPagamentoDal")
        End If
        Dim condizioneDataPagamentoDal As String = ""
        If DataPagamentoDal <> "" Then
            condizioneDataPagamentoDal = " AND BOL_BOLLETTE_PAGAMENTI_RUOLO.data_pagamento>='" & DataPagamentoDal & "' "
        End If
        Dim DataPagamentoAl As String = ""
        If Not IsNothing(Request.QueryString("DataPagamentoAl")) Then
            DataPagamentoAl = Request.QueryString("DataPagamentoAl")
        End If
        Dim condizioneDataPagamentoAl As String = ""
        If DataPagamentoAl <> "" Then
            condizioneDataPagamentoAl = " AND BOL_BOLLETTE_PAGAMENTI_RUOLO.data_pagamento<='" & DataPagamentoAl & "' "
        End If
        '##########################################
        '##########################################################################
        Dim dataRiferimentoDal As String = ""
        If Not IsNothing(Request.QueryString("RiferimentoDal")) Then
            dataRiferimentoDal = Request.QueryString("RiferimentoDal")
        End If
        Dim condizioneRiferimentoDal As String = ""
        If dataRiferimentoDal <> "" Then
            condizioneRiferimentoDal = " AND BOL_BOLLETTE.RIFERIMENTO_DA>='" & dataRiferimentoDal & "' "
        End If
        Dim dataRiferimentoAl As String = ""
        If Not IsNothing(Request.QueryString("RiferimentoAl")) Then
            dataRiferimentoAl = Request.QueryString("RiferimentoAl")
        End If
        Dim condizioneRiferimentoAl As String = ""
        If dataRiferimentoAl <> "" Then
            condizioneRiferimentoAl = " AND BOL_BOLLETTE.RIFERIMENTO_A<='" & dataRiferimentoAl & "' "
        End If
        '##########################################################################


        '######## TIPOLOGIA INCASSO ##################
        Dim tipologiaIncasso As String = ""
        Dim condizioneTipologiaIncasso As String = ""
        If Not IsNothing(Request.QueryString("TipologiaIncasso")) Then
            tipologiaIncasso = Request.QueryString("TipologiaIncasso")
            If tipologiaIncasso <> "-1" Then
                condizioneTipologiaIncasso = " AND BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_TIPO_PAGAMENTO = " & tipologiaIncasso
            End If
        End If
        '##########################################


        '######## condizione numero assegno ##################
        Dim numeroAssegno As String = ""
        If Not IsNothing(Request.QueryString("NumeroAssegno")) Then
            numeroAssegno = Request.QueryString("NumeroAssegno")
        End If
        Dim condizioneNumeroAssegno As String = ""
        If numeroAssegno <> "" Then
            condizioneNumeroAssegno = " AND ID_INCASSO_RUOLO IN (SELECT ID FROM SISCOM_MI.INCASSI_RUOLI " _
                & " WHERE NUMERO_ASSEGNO='" & numeroAssegno & "') "
        End If
        '##########################################


        '######## condizione COD. CONTRATTO ##################
        Dim codContratto As String = ""
        If Not IsNothing(Request.QueryString("CodContratto")) Then
            codContratto = Request.QueryString("CodContratto")
        End If
        Dim condizionecodContratto As String = ""
        If codContratto <> "" Then
            condizioneNumeroAssegno = " AND ID_CONTRATTO IN (SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA " _
                & " WHERE COD_CONTRATTO='" & codContratto.ToUpper & "') "
        End If
        '##########################################

        Dim selectMacroCategoria As String = ""
        Dim selectCategoria As String = ""
        Dim selectVoci As String = ""
        Dim selectTipologiaUI As String = ""
        Dim selectCompetenza As String = ""
        Dim groupByMacroCategoria As String = ""
        Dim groupByCategoria As String = ""
        Dim groupByVoci As String = ""
        Dim groupByTipologiaUI As String = ""
        Dim groupByCompetenza As String = ""

        Dim dettaglioSi As Boolean
        Dim categoriaSi As Boolean = False
        Dim competenzaSi As Boolean = False
        Dim macrocategoriaSi As Boolean = False
        Dim tipologiaUISi As Boolean = False
        Dim vociSi As Boolean = False

        Try
            connData.apri()

            par.cmd.CommandText = "SELECT INITCAP (NVL (TIPO_BOLLETTE.DESCRIZIONE, '')) AS BOLLETTAZIONE," _
            & "        t_voci_bolletta_Cap.descrizione  AS CAPITOLO," _
            & "       BOL_BOLLETTE_ES_CONTABILE.ANNO AS COMPETENZA_ACC," _
            & " '' AS ANNO," _
            & "'' AS BIMESTRE," _
            & "'' AS COMPETENZA," _
            & "'' AS MACROCATEGORIA," _
            & "'' AS CATEGORIA," _
            & "'' AS VOCE," _
            & "      INITCAP (" _
            & "       (CASE WHEN TIPO_USO = 2 THEN 'Usi Abitativi' ELSE 'Usi Diversi' END))" _
            & "      AS USI_ABITATIVI,'' AS TIPO_UI," _
            & "    TRIM ( TO_CHAR (" _
            & "       SUM (NVL (BOL_BOLLETTE_PAGAMENTI_RUOLO.IMPORTO_PAGATO, 0))," _
            & "       '999G999G990D99'))" _
            & "     AS IMPORTO" _
            & "   FROM SISCOM_MI.BOL_BOLLETTE_PAGAMENTI_RUOLO," _
            & "        siscom_mi.bol_bollette," _
            & "      siscom_mi.tipo_bollette," _
            & "      siscom_mi.BOL_BOLLETTE_ES_CONTABILE ,siscom_mi.t_voci_bolletta_Cap" _
             & "   WHERE " _
            & "       BOL_BOLLETTE.DATA_EMISSIONE BETWEEN BOL_BOLLETTE_ES_CONTABILE.VALIDITA_DA  AND BOL_BOLLETTE_ES_CONTABILE.VALIDITA_A" _
            & "     AND BOL_BOLLETTE_PAGAMENTI_RUOLO.id_bolletta = bol_bollette.id" _
            & "     AND tipo_bollette.id = bol_bollette.id_tipo and t_voci_bolletta_Cap.id=3 " _
            & condizioneDataPagamentoDal _
            & condizioneDataPagamentoAl _
            & condizioneRiferimentoAl _
            & condizioneRiferimentoDal _
            & condizioneTipologiaIncasso _
            & condizionecodContratto _
            & condizioneNumeroAssegno _
            & "   GROUP BY ROLLUP (INITCAP (NVL (TIPO_BOLLETTE.DESCRIZIONE, ''))," _
            & "  t_voci_bolletta_Cap.descrizione," _
            & " BOL_BOLLETTE_ES_CONTABILE.ANNO," _
            & " INITCAP ( (CASE WHEN TIPO_USO = 2 THEN 'Usi Abitativi' ELSE 'Usi Diversi'  END)))"


            Dim capitolisi As Boolean = True
            Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dTable As New Data.DataTable
            dAdapter.Fill(dTable)
            Dim valorePrecedente As Decimal = 0
            For Each Items As Data.DataRow In dTable.Rows
                If Not IsDBNull(Items.Item(11)) Then
                    valorePrecedente = Items.Item(11)
                Else
                    Items.Item(11) = valorePrecedente
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
            Dim annoCompetenza As String = ""
            Dim annoPrecedente As String = ""
            Dim capitoloPrecedente As String = ""
            Dim bimestrePrecedente As String = ""
            Dim competenzaPrecedente As String = ""
            Dim macrocategoriaPrecedente As String = ""
            Dim categoriaPrecedente As String = ""
            Dim vocePrecedente As String = ""
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
                    riga.Item("COMPETENZA_ACC") = dTable.Rows(Nrighe - 1 - i).Item(indice)
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
                    riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                    rigaPrec = riga

                Else
                    Dim indice2 As Integer = 0

                    rigaConfronto.Item("BOLLETTAZIONE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    indice2 += 1
                    rigaConfronto.Item("CAPITOLO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    indice2 += 1
                    rigaConfronto.Item("COMPETENZA_ACC") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
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
                    rigaConfronto.Item("IMPORTO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")

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
                            tipoUIPrecedente = ""
                        End If
                    Else
                        riga.Item("BOLLETTAZIONE") = ""
                    End If
                    indice += 1

                    If capitolisi Then
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
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("CAPITOLO") = ""
                        End If

                    End If
                    indice += 1
                    If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                        If annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            riga.Item("COMPETENZA_ACC") = ""
                        Else
                            riga.Item("COMPETENZA_ACC") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            annoPrecedente = ""
                            bimestrePrecedente = ""
                            competenzaPrecedente = ""
                            macrocategoriaPrecedente = ""
                            categoriaPrecedente = ""
                            vocePrecedente = ""
                            tipoUIPrecedente = ""
                        End If
                    Else
                        riga.Item("COMPETENZA_ACC") = ""
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
                            tipoUIPrecedente = ""
                        End If
                    Else
                        riga.Item("COMPETENZA") = ""
                    End If
                    indice += 1
                    If macroCategoriaSi Then
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If macrocategoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("MACROCATEGORIA") = ""
                            Else
                                riga.Item("MACROCATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                macrocategoriaPrecedente = riga.Item("MACROCATEGORIA")
                                categoriaPrecedente = ""
                                vocePrecedente = ""
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
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("VOCE") = ""
                        End If

                    End If
                    indice += 1
                    If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                        If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
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
                    If TipologiaUISi Then
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

                        If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Or par.IfNull(riga.Item("COMPETENZA_ACC"), "") <> "" Then
                            riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                        Else
                            riga.Item("IMPORTO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                        End If

                    Else
                        riga.Item("IMPORTO") = ""
                    End If

                End If
                If par.IfNull(rigaPrec.Item("TIPO_UI"), "") = par.IfNull(rigaConfronto.Item("TIPO_UI"), "") _
                    And par.IfNull(rigaPrec.Item("IMPORTO"), "") = par.IfNull(rigaConfronto.Item("IMPORTO"), "") _
                    And par.IfNull(riga.Item("competenza_acc"), "") = "" _
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

                    If par.IfNull(riga.Item("TIPO_UI"), "") = par.IfNull(dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(9), "") Then
                        riga.Item("TIPO_UI") = ""
                    End If
                Else
                    dataTableRibaltata.Rows.Add(riga)
                    rigaPrec = rigaConfronto
                End If
            Next

            If dataTableRibaltata.Rows.Count > 0 Then
                Dim indiceVisibile As Integer = 1
                If Not capitolisi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile = 3
                If Not dettaglioSi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not dettaglioSi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not competenzaSi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not macroCategoriaSi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not categoriaSi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not vociSi Then
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not TipologiaUISi Then
                    'DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    indiceVisibile += 1
                    DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                End If
                DataGridIncassi.DataSource = dataTableRibaltata
                DataGridIncassi.DataBind()
                LabelTitolo.Text = "Situazione Incassi Ruoli"
                DataGridIncassi.Visible = True
            Else
                DataGridIncassi.Visible = False
            End If
            connData.chiudi()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
        End Try

    End Sub

    Private Sub CaricaInfoRU()
        Try
            connData.apri()
            par.cmd.CommandText = " SELECT (CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END) AS NOMINATIVO," _
                & " COD_CONTRATTO,SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS STATO_RU " _
                & " ,SISCOM_MI.GETDATA(DATA_DECORRENZA) AS DATA_DECORRENZA " _
                & " ,SISCOM_MI.GETDATA(DATA_RICONSEGNA) AS DATA_RICONSEGNA " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE " _
                & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & " COD_TIPOLOGIA_OCCUPANTE IN ('INTE') " _
                & " AND ANAGRAFICA.ID=ID_ANAGRAFICA " _
                & " AND COD_CONTRATTO='" & codContratto.Value & "'"
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lettore.Read Then
                Nominativo.Text = par.IfNull(Lettore("NOMINATIVO"), "")
                Cod_Contratto.Text = par.IfNull(Lettore("COD_CONTRATTO"), "")
                Stato_ru.Text = par.IfNull(Lettore("STATO_RU"), "")
                DataInizio.Text = par.IfNull(Lettore("DATA_DECORRENZA"), "")
                DataFine.Text = par.IfNull(Lettore("DATA_RICONSEGNA"), "- - -")
            End If
            Lettore.Close()

            connData.chiudi()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")

        End Try
    End Sub

    Protected Sub ImageButtonStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonStampa.Click
        Dim Loading As String = "<div id=""divLoading5"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)
        Response.Flush()
        Try
            Dim nomeFile As String = ""
            nomeFile = StampaDataGridPDF_1(DataGridIncassi, "StampaIncassi", LabelTitolo.Text, , 1400, , , True, 48, True, filtriRicerca)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                Response.Write("<script>window.open('../../FileTemp/" & nomeFile & "');</script>")
            Else
                Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!');</script>")
        End Try
    End Sub

    Function StampaDataGridPDF_1(ByVal datagrid As DataGrid, ByVal nomeStampa As String, Optional ByVal titolo As String = "", Optional ByVal footer As String = "", Optional ByVal larghezzaPagina As Integer = 1800, Optional ByVal orientamentoLandscape As Boolean = True, Optional ByVal mostraNumeriPagina As Boolean = True, Optional ByVal contaRighe As Boolean = False, Optional righe As Integer = 25, Optional ByVal ripetiIntestazioniSoloConContaRighe As Boolean = False, Optional ByVal sottotitolo As String = "", Optional ByVal DataGrid2 As DataGrid = Nothing, Optional ByVal DataGrid3 As DataGrid = Nothing) As String
        Try
            'RENDERCONTROL DEL DATAGRID
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            stringWriter = New System.IO.StringWriter
            sourcecode = New HtmlTextWriter(stringWriter)
            datagrid.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = Html & stringWriter.ToString
            'ELIMINAZIONE EVENTUALI HYPERLINK
            Html = par.EliminaLink(Html)
            If contaRighe = True And righe > 0 Then
                Dim TitoliDaRipetere As String = ""
                If ripetiIntestazioniSoloConContaRighe = True Then
                    Dim indiceInizioPrimoTR As Integer = Html.IndexOf("</tr>")
                    TitoliDaRipetere = Left(Html, indiceInizioPrimoTR + 5)
                End If


                Dim htmldaConsiderare As String = Html
                Dim nuovoHtml As String = ""
                Dim indiceTRiniziale As Integer = 0
                Dim indiceTRfinale As Integer = 0
                Dim contatoreRighe As Integer = 0
                Dim stringaAdd As String = ""
                While indiceTRiniziale <> -1
                    indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                    If indiceTRiniziale <> -1 Then
                        contatoreRighe += 1
                        htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                        indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                        If indiceTRfinale <> -1 Then
                            stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                        End If
                    End If
                    If contatoreRighe = righe Then
                        nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(Html, Html.IndexOf("<tr ") - 1)
                        contatoreRighe = 0
                    Else
                        nuovoHtml &= stringaAdd
                    End If
                End While
                Html = Left(Html, Html.IndexOf("<tr ") - 1) & nuovoHtml
            End If


            If Not IsNothing(DataGrid2) Then
                Dim html2 As String = ""

                stringWriter = New System.IO.StringWriter
                sourcecode = New HtmlTextWriter(stringWriter)
                DataGrid2.RenderControl(sourcecode)
                sourcecode.Flush()
                html2 = html2 & stringWriter.ToString
                'ELIMINAZIONE EVENTUALI HYPERLINK
                html2 = par.EliminaLink(html2)
                If contaRighe = True And righe > 0 Then
                    Dim TitoliDaRipetere As String = ""
                    If ripetiIntestazioniSoloConContaRighe = True Then
                        Dim indiceInizioPrimoTR As Integer = html2.IndexOf("</tr>")
                        TitoliDaRipetere = Left(html2, indiceInizioPrimoTR + 5)
                    End If


                    Dim htmldaConsiderare As String = html2
                    Dim nuovoHtml As String = ""
                    Dim indiceTRiniziale As Integer = 0
                    Dim indiceTRfinale As Integer = 0
                    Dim contatoreRighe As Integer = 0
                    Dim stringaAdd As String = ""
                    While indiceTRiniziale <> -1
                        indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                        If indiceTRiniziale <> -1 Then
                            contatoreRighe += 1
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                            indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                            If indiceTRfinale <> -1 Then
                                stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                                htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                            End If
                        End If
                        If contatoreRighe = righe Then
                            nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(html2, html2.IndexOf("<tr ") - 1)
                            contatoreRighe = 0
                        Else
                            nuovoHtml &= stringaAdd
                        End If
                    End While
                    html2 = Left(html2, html2.IndexOf("<tr ") - 1) & nuovoHtml
                End If
                Html = Html & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & html2
            End If


            If Not IsNothing(DataGrid3) Then
                Dim html3 As String = ""

                stringWriter = New System.IO.StringWriter
                sourcecode = New HtmlTextWriter(stringWriter)
                DataGrid3.RenderControl(sourcecode)
                sourcecode.Flush()
                html3 = html3 & stringWriter.ToString
                'ELIMINAZIONE EVENTUALI HYPERLINK
                html3 = par.EliminaLink(html3)
                If contaRighe = True And righe > 0 Then
                    Dim TitoliDaRipetere As String = ""
                    If ripetiIntestazioniSoloConContaRighe = True Then
                        Dim indiceInizioPrimoTR As Integer = html3.IndexOf("</tr>")
                        TitoliDaRipetere = Left(html3, indiceInizioPrimoTR + 5)
                    End If


                    Dim htmldaConsiderare As String = html3
                    Dim nuovoHtml As String = ""
                    Dim indiceTRiniziale As Integer = 0
                    Dim indiceTRfinale As Integer = 0
                    Dim contatoreRighe As Integer = 0
                    Dim stringaAdd As String = ""
                    While indiceTRiniziale <> -1
                        indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                        If indiceTRiniziale <> -1 Then
                            contatoreRighe += 1
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                            indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                            If indiceTRfinale <> -1 Then
                                stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                                htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                            End If
                        End If
                        If contatoreRighe = righe Then
                            nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(html3, html3.IndexOf("<tr ") - 1)
                            contatoreRighe = 0
                        Else
                            nuovoHtml &= stringaAdd
                        End If
                    End While
                    html3 = Left(html3, html3.IndexOf("<tr ") - 1) & nuovoHtml
                End If
                Html = Html & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & html3
            End If





            Dim url As String = Server.MapPath("~\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = System.Web.HttpContext.Current.Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PageWidth = larghezzaPagina
            If orientamentoLandscape = True Then
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            Else
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            End If
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 65
            pdfConverter1.PdfHeaderOptions.HeaderText = UCase(titolo)
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 8
            pdfConverter1.PdfHeaderOptions.HeaderTextAlign = HorizontalTextAlign.Left
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold


            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = sottotitolo
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 7
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")


            'pdfConverter1.PdfHeaderOptions.HeaderImage = Drawing.Image.FromFile(Server.MapPath("~\NuoveImm\") & "rett2.png")


            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            pdfConverter1.PdfFooterOptions.FooterText = "Report Situazione Incassi, stampato da " & Session.Item("NOME_OPERATORE") & " il " & Format(Now, "dd/MM/yyyy") & " alle " & Format(Now, "HH:mm")
            pdfConverter1.PdfFooterOptions.FooterTextFontName = "Arial"
            pdfConverter1.PdfFooterOptions.FooterTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfFooterOptions.FooterTextFontSize = 8
            'pdfConverter1.PdfFooterOptions.FooterText = UCase(footer)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            pdfConverter1.PdfFooterOptions.FooterHeight = 30
            pdfConverter1.PdfFooterOptions.DrawFooterLine = True
            If mostraNumeriPagina = True Then
                pdfConverter1.PdfFooterOptions.PageNumberText = "Pag."
                pdfConverter1.PdfFooterOptions.PageNumberTextFontName = "Arial"
                pdfConverter1.PdfFooterOptions.PageNumberTextFontSize = 8
                pdfConverter1.PdfFooterOptions.ShowPageNumber = True
                pdfConverter1.PdfFooterOptions.PageNumberTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            Else
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            End If

            Dim nomefile As String = nomeStampa & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile, Server.MapPath("~\NuoveImm\"))

            Return nomefile
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub ImageButtonExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExcel.Click
        Dim xls As New ExcelSiSol
        Dim nomefile1 As String = ""
        Dim nomefile2 As String = ""
        Dim nomefile3 As String = ""
        If DataGridIncassi.Visible = True Then
            nomefile1 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassiRuoli", "ExportIncassiRuoli", DataGridIncassi, True, , True)
        End If
        Dim nome As String = "Incassi_ruoli"
        'COSTRUZIONE ZIPFILE
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String
        zipfic = Server.MapPath("~\FileTemp\" & nome & ".zip")
        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        Dim strFile As String
        Dim strmFile As FileStream
        Dim theEntry As ZipEntry
        If File.Exists(Server.MapPath("~\FileTemp\") & nomefile1) Then
            strFile = Server.MapPath("~\FileTemp\" & nomefile1)
            strmFile = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(strFile)
            theEntry = New ZipEntry(sFile)
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
        End If
       
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()
        Dim FileNameZip As String = nome & ".zip"

        If File.Exists(Server.MapPath("~\FileTemp\") & FileNameZip) Then
            Response.Redirect("../../FileTemp/" & FileNameZip, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

        'Dim nomeFile As String = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridIncassi, "ExportIncassi", , , , False)
        'If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '    Response.Redirect("../../FileTemp/" & nomeFile, False)
        'Else
        '    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        'End If
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub
End Class
