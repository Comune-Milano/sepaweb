Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Threading
Partial Class Contabilita_Report_RisultatiGestioneResidui
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            If controlloAvviaProcedura() Then
                avviaProcedura()
            End If
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

    Public Property selectVoci() As String
        Get
            If Not (ViewState("selectVoci") Is Nothing) Then
                Return CStr(ViewState("selectVoci"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("selectVoci") = value
        End Set
    End Property

    Public Property selectTipologiaUI() As String
        Get
            If Not (ViewState("selectTipologiaUI") Is Nothing) Then
                Return CStr(ViewState("selectTipologiaUI"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("selectTipologiaUI") = value
        End Set
    End Property

    Public Property condizioneNumeroAssegno() As String
        Get
            If Not (ViewState("condizioneTipologiaIncasso") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaIncasso"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneNumeroAssegno") = value
        End Set
    End Property

    Public Property condizioneTipologiaIncasso() As String
        Get
            If Not (ViewState("condizioneTipologiaIncasso") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaIncasso"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaIncasso") = value
        End Set
    End Property

    Public Property selectMacrocategoria() As String
        Get
            If Not (ViewState("selectMacrocategoria") Is Nothing) Then
                Return CStr(ViewState("selectMacrocategoria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("selectMacrocategoria") = value
        End Set
    End Property

    Public Property selectEsercizioContabile() As String
        Get
            If Not (ViewState("selectEsercizioContabile") Is Nothing) Then
                Return CStr(ViewState("selectEsercizioContabile"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("selectEsercizioContabile") = value
        End Set
    End Property

    Public Property selectCompetenza() As String
        Get
            If Not (ViewState("selectCompetenza") Is Nothing) Then
                Return CStr(ViewState("selectCompetenza"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("selectCompetenza") = value
        End Set
    End Property

    Public Property selectCategoria() As String
        Get
            If Not (ViewState("selectCategoria") Is Nothing) Then
                Return CStr(ViewState("selectCategoria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("selectCategoria") = value
        End Set
    End Property

    Public Property groupByVoci() As String
        Get
            If Not (ViewState("groupByVoci") Is Nothing) Then
                Return CStr(ViewState("groupByVoci"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("groupByVoci") = value
        End Set
    End Property

    Public Property groupByTipologiaUI() As String
        Get
            If Not (ViewState("groupByTipologiaUI") Is Nothing) Then
                Return CStr(ViewState("groupByTipologiaUI"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("groupByTipologiaUI") = value
        End Set
    End Property

    Public Property groupByMacrocategoria() As String
        Get
            If Not (ViewState("groupByMacrocategoria") Is Nothing) Then
                Return CStr(ViewState("groupByMacrocategoria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("groupByMacrocategoria") = value
        End Set
    End Property

    Public Property groupByEsercizioContabile() As String
        Get
            If Not (ViewState("groupByEsercizioContabile") Is Nothing) Then
                Return CStr(ViewState("groupByEsercizioContabile"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("groupByEsercizioContabile") = value
        End Set
    End Property

    Public Property groupByCompetenza() As String
        Get
            If Not (ViewState("groupByCompetenza") Is Nothing) Then
                Return CStr(ViewState("groupByCompetenza"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("groupByCompetenza") = value
        End Set
    End Property

    Public Property groupByCategoria() As String
        Get
            If Not (ViewState("groupByCategoria") Is Nothing) Then
                Return CStr(ViewState("groupByCategoria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("groupByCategoria") = value
        End Set
    End Property

    Public Property dataAggiornamento() As String
        Get
            If Not (ViewState("dataAggiornamento") Is Nothing) Then
                Return CStr(ViewState("dataAggiornamento"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("dataAggiornamento") = value
        End Set
    End Property

    Public Property condizioneTipologiaUI() As String
        Get
            If Not (ViewState("condizioneTipologiaUI") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaUI"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaUI") = value
        End Set
    End Property

    Public Property condizioneTipologiaUIres() As String
        Get
            If Not (ViewState("condizioneTipologiaUIres") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaUIres"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaUIres") = value
        End Set
    End Property

    Public Property condizioneTipologiaContoCorrente() As String
        Get
            If Not (ViewState("condizioneTipologiaContoCorrente") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaContoCorrente"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaContoCorrente") = value
        End Set
    End Property

    Public Property condizioneTipologiaCondominio() As String
        Get
            If Not (ViewState("condizioneTipologiaCondominio") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaCondominio"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaCondominio") = value
        End Set
    End Property

    Public Property condizioneRiferimentoDal() As String
        Get
            If Not (ViewState("condizioneRiferimentoDal") Is Nothing) Then
                Return CStr(ViewState("condizioneRiferimentoDal"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneRiferimentoDal") = value
        End Set
    End Property

    Public Property condizioneRiferimentoAl() As String
        Get
            If Not (ViewState("condizioneRiferimentoAl") Is Nothing) Then
                Return CStr(ViewState("condizioneRiferimentoAl"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneRiferimentoAl") = value
        End Set
    End Property

    Public Property condizioneListaVociRes() As String
        Get
            If Not (ViewState("condizioneListaVociRes") Is Nothing) Then
                Return CStr(ViewState("condizioneListaVociRes"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneListaVociRes") = value
        End Set
    End Property

    Public Property condizioneMacrocategoria() As String
        Get
            If Not (ViewState("condizioneMacrocategoria") Is Nothing) Then
                Return CStr(ViewState("condizioneMacrocategoria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneMacrocategoria") = value
        End Set
    End Property

    Public Property condizioneTipologiaBollettazioneRes() As String
        Get
            If Not (ViewState("condizioneTipologiaBollettazioneRes") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaBollettazioneRes"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaBollettazioneRes") = value
        End Set
    End Property

    Public Property condizioneEmissioneDal() As String
        Get
            If Not (ViewState("condizioneEmissioneDal") Is Nothing) Then
                Return CStr(ViewState("condizioneEmissioneDal"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneEmissioneDal") = value
        End Set
    End Property

    Public Property condizioneListaVoci() As String
        Get
            If Not (ViewState("condizioneListaVoci") Is Nothing) Then
                Return CStr(ViewState("condizioneListaVoci"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneListaVoci") = value
        End Set
    End Property

    Public Property condizioneEsercizioContabile() As String
        Get
            If Not (ViewState("condizioneEsercizioContabile") Is Nothing) Then
                Return CStr(ViewState("condizioneEsercizioContabile"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneEsercizioContabile") = value
        End Set
    End Property

    Public Property condizioneEmissioneAl() As String
        Get
            If Not (ViewState("condizioneEmissioneAl") Is Nothing) Then
                Return CStr(ViewState("condizioneEmissioneAl"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneEmissioneAl") = value
        End Set
    End Property

    Public Property condizioneDataPagamentoDal() As String
        Get
            If Not (ViewState("condizioneDataPagamentoDal") Is Nothing) Then
                Return CStr(ViewState("condizioneDataPagamentoDal"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneDataPagamentoDal") = value
        End Set
    End Property

    Public Property condizioneMacrocategoriaRes() As String
        Get
            If Not (ViewState("condizioneMacrocategoriaRes") Is Nothing) Then
                Return CStr(ViewState("condizioneMacrocategoriaRes"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneMacrocategoriaRes") = value
        End Set
    End Property

    Public Property condizioneDataAggiornamento() As String
        Get
            If Not (ViewState("condizioneDataAggiornamento") Is Nothing) Then
                Return CStr(ViewState("condizioneDataAggiornamento"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneDataAggiornamento") = value
        End Set
    End Property

    Public Property condizioneDataContabileDal() As String
        Get
            If Not (ViewState("condizioneDataContabileDal") Is Nothing) Then
                Return CStr(ViewState("condizioneDataContabileDal"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneDataContabileDal") = value
        End Set
    End Property

    Public Property condizioneTipologiaBollettazione() As String
        Get
            If Not (ViewState("condizioneTipologiaBollettazione") Is Nothing) Then
                Return CStr(ViewState("condizioneTipologiaBollettazione"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneTipologiaBollettazione") = value
        End Set
    End Property

    Public Property condizioneDataPagamentoAl() As String
        Get
            If Not (ViewState("condizioneDataPagamentoAl") Is Nothing) Then
                Return CStr(ViewState("condizioneDataPagamentoAl"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneDataPagamentoAl") = value
        End Set
    End Property

    Public Property condizioneCompetenza() As String
        Get
            If Not (ViewState("condizioneCompetenza") Is Nothing) Then
                Return CStr(ViewState("condizioneCompetenza"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneCompetenza") = value
        End Set
    End Property

    Public Property condizioneCategoria() As String
        Get
            If Not (ViewState("condizioneCategoria") Is Nothing) Then
                Return CStr(ViewState("condizioneCategoria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneCategoria") = value
        End Set
    End Property

    Public Property condizioneDataContabileAl() As String
        Get
            If Not (ViewState("condizioneDataContabileAl") Is Nothing) Then
                Return CStr(ViewState("condizioneDataContabileAl"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneDataContabileAl") = value
        End Set
    End Property

    Public Property condizioneCompetenzaRes() As String
        Get
            If Not (ViewState("condizioneCompetenzaRes") Is Nothing) Then
                Return CStr(ViewState("condizioneCompetenzaRes"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneCompetenzaRes") = value
        End Set
    End Property

    Public Property condizioneCategoriaRes() As String
        Get
            If Not (ViewState("condizioneCategoriaRes") Is Nothing) Then
                Return CStr(ViewState("condizioneCategoriaRes"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneCategoriaRes") = value
        End Set
    End Property

    Public Property condizioneCapitoliRes() As String
        Get
            If Not (ViewState("condizioneCapitoliRes") Is Nothing) Then
                Return CStr(ViewState("condizioneCapitoliRes"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneCapitoliRes") = value
        End Set
    End Property

    Public Property condizioneCapitoli() As String
        Get
            If Not (ViewState("condizioneCapitoli") Is Nothing) Then
                Return CStr(ViewState("condizioneCapitoli"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneCapitoli") = value
        End Set
    End Property

    Public Property condizioneAggiornamento() As String
        Get
            If Not (ViewState("condizioneAggiornamento") Is Nothing) Then
                Return CStr(ViewState("condizioneAggiornamento"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("condizioneAggiornamento") = value
        End Set
    End Property

    Public Property vociSi() As Boolean
        Get
            If Not (ViewState("vociSi") Is Nothing) Then
                Return CBool(ViewState("vociSi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("vociSi") = value
        End Set
    End Property

    Public Property dettaglioSi() As Boolean
        Get
            If Not (ViewState("dettaglioSi") Is Nothing) Then
                Return CBool(ViewState("dettaglioSi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("dettaglioSi") = value
        End Set
    End Property

    Public Property tipologiaUIsi() As Boolean
        Get
            If Not (ViewState("tipologiaUIsi") Is Nothing) Then
                Return CBool(ViewState("tipologiaUIsi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("tipologiaUIsi") = value
        End Set
    End Property

    Public Property macrocategoriaSi() As Boolean
        Get
            If Not (ViewState("macrocategoriaSi") Is Nothing) Then
                Return CBool(ViewState("macrocategoriaSi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("macrocategoriaSi") = value
        End Set
    End Property

    Public Property categoriaSi() As Boolean
        Get
            If Not (ViewState("categoriaSi") Is Nothing) Then
                Return CBool(ViewState("categoriaSi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("categoriaSi") = value
        End Set
    End Property

    Public Property capitoliSi() As Boolean
        Get
            If Not (ViewState("capitoliSi") Is Nothing) Then
                Return CBool(ViewState("capitoliSi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("capitoliSi") = value
        End Set
    End Property

    Public Property competenzaSi() As Boolean
        Get
            If Not (ViewState("competenzaSi") Is Nothing) Then
                Return CBool(ViewState("competenzaSi"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("competenzaSi") = value
        End Set
    End Property

    'Protected Sub chiudiConnessione()
    '    par.cmd.Dispose()
    '    If Not IsNothing(par.OracleConn) Then
    '        par.OracleConn.Close()
    '    End If
    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    'End Sub
    'Protected Sub ApriConnessione()
    '    If par.OracleConn.State = Data.ConnectionState.Closed Then
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    'End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Private Sub caricaDati()

        Try
            'connData.apri(True)
            '‘‘par.cmd.Transaction = connData.Transazione
            connData.apri()
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

            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET ID_BOL_BOLLETTE_VOCI=ID_BOL_BOLLETTE_VOCI WHERE DATA_EMISSIONE_BOL_BOLLETTE=0"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = " SELECT INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE, " _
                & selectEsercizioContabile _
                & " ANNO_RIF AS ANNO, " _
                & " INITCAP(BIMESTRE) AS BIMESTRE, " _
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
                & " GROUP BY ROLLUP (INITCAP(BOLLETTAZIONE)," _
                & groupByEsercizioContabile _
                & " ANNO_RIF, " _
                & " INITCAP(BIMESTRE) " _
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
                    If macrocategoriaSi Then
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
                            'tipoUIPrecedente = riga.Item("USI_ABITATIVI")
                        End If
                    Else
                        riga.Item("USI_ABITATIVI") = ""
                    End If
                    indice += 1
                    If tipologiaUIsi Then
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
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET PARZIALE=" & i + 1 & ",TOTALE=" & dataTableRibaltata.Rows.Count & " WHERE NOME_TABELLA='" & Session.Item("TAB_TEMPORANEA") & "'"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_RESIDUI SET PARZIALE=" & i + 1 & " WHERE NOME_TABELLA='" & Session.Item("TAB_TEMPORANEA") & "'"
                    par.cmd.ExecuteNonQuery()
                End If


                If i = 0 Then
                    condizioneBollettazione = " AND BOLLETTAZIONE>'0' "
                    condizioneCapitolo = " AND CAPITOLO>'0' "
                    condizioneAnno = " AND ANNO>'0' "
                    condizioneBimestre = " AND BIMESTRE>'0' "
                    condizioneCompetenzaEmissione = " AND COMPETENZA>'0' "
                    condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA>'0' "
                    condizioneVoce = " AND VOCE>'0' "
                    condizioneUsoUI = " AND USI_ABITATIVI>'0' "
                    condizionetipologiaUIEmissione = " AND TIPO_UI>'0' "
                Else
                    If par.IfNull(dataTableRibaltata.Rows(i).Item("BOLLETTAZIONE"), "") <> "" Then
                        condizioneBollettazione = " AND BOLLETTAZIONE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("BOLLETTAZIONE")), "'", "''") & "' "
                    Else
                        condizioneBollettazione = " AND BOLLETTAZIONE>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("CAPITOLO"), "") <> "" Then
                        condizioneCapitolo = " AND CAPITOLO='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("CAPITOLO")), "'", "''") & "' "
                    Else
                        condizioneCapitolo = " AND CAPITOLO>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("ANNO"), "") <> "" Then
                        condizioneAnno = " AND ANNO='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("ANNO")), "'", "''") & "' "
                    Else
                        condizioneAnno = " AND ANNO>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("BIMESTRE"), "") <> "" Then
                        condizioneBimestre = " AND BIMESTRE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("BIMESTRE")), "'", "''") & "' "
                    Else
                        condizioneBimestre = " AND BIMESTRE>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("COMPETENZA"), "") <> "" Then
                        condizioneCompetenzaEmissione = " AND COMPETENZA='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("COMPETENZA")), "'", "''") & "' "
                    Else
                        condizioneCompetenzaEmissione = " AND COMPETENZA>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("MACROCATEGORIA"), "") <> "" Then
                        condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("MACROCATEGORIA")), "'", "''") & "' "
                    Else
                        condizioneMacrocategoriaEmissione = " AND MACROCATEGORIA>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("VOCE"), "") <> "" Then
                        condizioneVoce = " AND VOCE='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("VOCE")), "'", "''") & "' "
                    Else
                        condizioneVoce = " AND VOCE>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("USI_ABITATIVI"), "") <> "" Then
                        condizioneUsoUI = " AND USI_ABITATIVI='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("USI_ABITATIVI")), "'", "''") & "' "
                    Else
                        condizioneUsoUI = " AND USI_ABITATIVI>'0' "
                    End If

                    If par.IfNull(dataTableRibaltata.Rows(i).Item("TIPO_UI"), "") <> "" Then
                        condizionetipologiaUIEmissione = " AND TIPO_UI='" & Replace(par.EliminaFont(dataTableRibaltata.Rows(i).Item("TIPO_UI")), "'", "''") & "' "
                    Else
                        condizionetipologiaUIEmissione = " AND TIPO_UI>'0' "
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
                    & condizioneTipologiaUIres _
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
                Dim indiceVisibile As Integer = 1
                If Not capitoliSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile = 3
                If Not dettaglioSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not dettaglioSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not competenzaSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not macrocategoriaSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not categoriaSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not vociSi Then
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                indiceVisibile += 1
                If Not tipologiaUIsi Then
                    'DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                    indiceVisibile += 1
                    DataGridResidui.Columns.Item(indiceVisibile).Visible = False
                End If
                DataGridResidui.DataSource = dataTableRibaltata

                par.cmd.CommandText = "CREATE TABLE SEPA." & Session.Item("TAB_TEMPORANEA") _
                    & " (BOLLETTAZIONE   VARCHAR2 (200)," _
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

                For Each rigaRibaltata As Data.DataRow In dataTableRibaltata.Rows
                    par.cmd.CommandText = "INSERT INTO SEPA." & Session.Item("TAB_TEMPORANEA") _
                        & " (BOLLETTAZIONE," _
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
                        & " VALUES('" & rigaRibaltata(0).ToString.Replace("'", "''") & "'," _
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



                par.cmd.CommandText = "update siscom_mi.procedure_residui set esito=1,data_ora_fine='" & Format(Now, "yyyyMMddHHmmss") & "'  where nome_tabella='" & Session.Item("TAB_TEMPORANEA") & "'"
                par.cmd.ExecuteNonQuery()
                Session.Remove("TAB_TEMPORANEA")

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
        Catch ex As Exception
            connData.chiudi()
            connData.apri()
            par.cmd.CommandText = "update siscom_mi.procedure_residui set esito=2,NOTE='" & Replace(ex.Message, "'", "''") & "',DATA_ORA_FINE='" & Format(Now, "yyyyMMddHHmmss") & "'  where nome_tabella='" & Session.Item("TAB_TEMPORANEA") & "'"
            par.cmd.ExecuteNonQuery()
            Session.Remove("TAB_TEMPORANEA")
            connData.chiudi()
            'Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
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
            Dim nomeFile As String = StampaDataGridPDF_1(DataGridResidui, "StampaResidui", LabelTitolo.Text, , 1400, , , True, 50, True, filtriRicerca)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                Response.Write("<script>window.open('../../FileTemp/" & nomeFile & "');</script>")
                HiddenFieldPrimoPiano.Value = "1"
            Else
                Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!');</script>")
        End Try
    End Sub

    Function StampaDataGridPDF_1(ByVal datagrid As DataGrid, ByVal nomeStampa As String, Optional ByVal titolo As String = "", Optional ByVal footer As String = "", Optional ByVal larghezzaPagina As Integer = 1200, Optional ByVal orientamentoLandscape As Boolean = True, Optional ByVal mostraNumeriPagina As Boolean = True, Optional ByVal contaRighe As Boolean = False, Optional righe As Integer = 25, Optional ByVal ripetiIntestazioniSoloConContaRighe As Boolean = False, Optional ByVal sottotitolo As String = "", Optional ByVal DataGrid2 As DataGrid = Nothing, Optional ByVal DataGrid3 As DataGrid = Nothing) As String
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
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 63
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
            pdfConverter1.PdfFooterOptions.FooterText = "Report Situazione Residui, stampato da " & Session("NOME_OPERATORE") & " il " & Format(Now, "dd/MM/yyyy") & " alle " & Format(Now, "HH:mm")
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
        'Dim nomefile1 As String = ""
        'If DataGridResidui.Visible = True Then
        '    nomefile1 = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridResidui, "ExportResidui", , False, , False)
        'End If
        'Dim nome As String = "Residui"
        ''COSTRUZIONE ZIPFILE
        'Dim objCrc32 As New Crc32()
        'Dim strmZipOutputStream As ZipOutputStream
        'Dim zipfic As String
        'zipfic = Server.MapPath("~\FileTemp\" & nome & ".zip")
        'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        'strmZipOutputStream.SetLevel(6)
        'Dim strFile As String
        'Dim strmFile As FileStream
        'Dim theEntry As ZipEntry
        'If File.Exists(Server.MapPath("~\FileTemp\") & nomefile1) Then
        '    strFile = Server.MapPath("~\FileTemp\" & nomefile1)
        '    strmFile = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    theEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    File.Delete(strFile)
        'End If
        'strmZipOutputStream.Finish()
        'strmZipOutputStream.Close()
        'Dim FileNameZip As String = nome & ".zip"

        'If File.Exists(Server.MapPath("~\FileTemp\") & FileNameZip) Then
        '    Response.Redirect("../../FileTemp/" & FileNameZip, False)
        'Else
        '    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        'End If

        Dim nomeFile As String = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridResidui, "ExportResidui", , , , False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Function EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(ByVal datagrid As DataGrid, Optional ByVal nomeFile As String = "", Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True, Optional ByVal Titolo As String = "", Optional ByVal creazip As Boolean = True) As String
        Try
            'CONTO IL NUMERO DELLE COLONNE DEL DATAGRID
            Dim NumeroColonneDatagrid As Integer = datagrid.Columns.Count
            'CONTO IL NUMERO DELLE COLONNE VISIBILI DEL DATAGRID
            Dim NumeroColonneVisibiliDatagrid As Integer = 0
            For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
                If datagrid.Columns.Item(indiceColonna).Visible = True Then
                    NumeroColonneVisibiliDatagrid = NumeroColonneVisibiliDatagrid + 1
                End If
            Next
            'INIZIALIZZAZIONE RIGHE, COLONNE E FILENAME
            Dim FileExcel As New CM.ExcelFile
            Dim indiceRighe As Long = 0
            Dim IndiceColonne As Long = 1
            Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
            Dim LarghezzaMinimaColonna As Integer = 30
            Dim allineamentoCella As String = "Center"
            Dim LarghezzaDataGrid As Integer = Math.Max(datagrid.Width.Value, 200)
            Dim TipoLarghezzaDataGrid As UnitType = datagrid.Width.Type
            Dim LarghezzaColonnaHeader As Decimal = 0
            Dim LarghezzaColonnaItem As Decimal = 0
            'SETTO A ZERO LA VARIABILE DELLE RIGHE
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(Server.MapPath("~\FileTemp\" & FileName & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                Dim indiceVisibile As Integer = 1
                If Titolo <> "" Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, Titolo, 0)
                    indiceRighe += 1
                    IndiceColonne += 1
                End If
                For j = 0 To NumeroColonneDatagrid - 1 Step 1
                    'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                    If datagrid.Columns.Item(j).Visible = True Then
                        If datagrid.Columns.Item(j).HeaderStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value * 100 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If

                        If datagrid.Columns.Item(j).ItemStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value * 100 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * Math.Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, Math.Max(LarghezzaMinimaColonna, 30))
                        'GESTIONE DELLE INTESTAZIONI

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, indiceVisibile, datagrid.Columns.Item(j).HeaderText, 0)
                        indiceVisibile = indiceVisibile + 1
                    End If
                Next
                indiceRighe = indiceRighe + 1
                For Each Items As DataGridItem In datagrid.Items
                    indiceRighe = indiceRighe + 1
                    Dim Cella As Integer = 0
                    For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                        'RIEPILOGO ALLINEAMENTI
                        'CENTER 2,LEFT 1,RIGHT 3
                        'CONSIDERO DI FORMATO NUMERICO TUTTE LE CELLE CON ALLINEAMENTO A DESTRA
                        If datagrid.Columns.Item(IndiceColonne).Visible = True Then
                            allineamentoCella = datagrid.Columns.Item(IndiceColonne).ItemStyle.HorizontalAlign
                            Select Case EliminazioneLink
                                Case False
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                    End Select

                                Case True
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                    End Select
                                Case Else
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                    End Select
                            End Select
                            Cella = Cella + 1
                        End If
                    Next

                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            If creazip = True Then
                'COSTRUZIONE ZIPFILE
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream

                Dim strFile As String
                strFile = Server.MapPath("~\FileTemp\" & FileName & ".xls")
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
                Dim zipfic As String
                zipfic = Server.MapPath("~\FileTemp\" & FileName & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                File.Delete(strFile)
                Dim FileNameZip As String = FileName & ".zip"
                Return FileNameZip
            Else
                Dim FileNameExcel As String = FileName & ".xls"
                Return FileNameExcel
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Function DataTableALCSV(ByVal table As Data.DataTable, ByVal filename As String, ByVal sepcar As String, Optional ByVal creazip As Boolean = True) As String
        Dim sr As System.IO.StreamWriter = Nothing
        Dim sep As String = sepcar
        Dim intestazione As String = ""
        Dim flag_inizio As Integer = 0
        Dim indiceRighe As Long = 0
        Dim nome As String = filename & Format(Now, "yyyyMMddHHmmss")
        Try
            'CREO IL FILE CSV
            If table.Rows.Count <= 65536 Then
                Dim nomefile As String = nome & ".csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    Dim stringa As String = ""
                    For Each col As Data.DataColumn In table.Columns
                        If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                            If row(col.ColumnName).ToString <> "" Then
                                stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                stringa = par.RimuoviHTML(stringa)
                            Else
                                stringa = stringa & "" & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Else
                            stringa = stringa & row(col.ColumnName) & sep
                            stringa = par.RimuoviHTML(stringa)
                        End If
                    Next
                    sr.WriteLine(stringa)
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Dim FileNameCSV As String = nomefile
                Return FileNameCSV
            Else
                Dim nomefile1 As String = nome & "_1.csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile1))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    If indiceRighe <= 65535 Then
                        Dim stringa As String = ""
                        For Each col As Data.DataColumn In table.Columns
                            If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                                If row(col.ColumnName).ToString <> "" Then
                                    stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                Else
                                    stringa = stringa & "" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                End If
                            Else
                                stringa = stringa & row(col.ColumnName) & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Next
                        sr.WriteLine(stringa)
                    End If
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Dim nomefile2 As String = nome & "_2.csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile2))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    If indiceRighe > 65535 Then
                        Dim stringa As String = ""
                        For Each col As Data.DataColumn In table.Columns
                            If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                                If row(col.ColumnName).ToString <> "" Then
                                    stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                Else
                                    stringa = stringa & "" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                End If
                            Else
                                stringa = stringa & row(col.ColumnName) & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Next
                        sr.WriteLine(stringa)
                    End If
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                'CREAZIONE FILE ZIP
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                zipfic = Server.MapPath("..\FileTemp\" & nome & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & nomefile1)
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
                File.Delete(strFile)

                strFile = Server.MapPath("..\FileTemp\" & nomefile2)
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                Dim sFile1 As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile1)
                fi = New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer1)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                File.Delete(strFile)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                Dim FileNameZip As String = nome & ".zip"
                Return FileNameZip
            End If
        Catch ex As Exception
            If Not sr Is Nothing Then
                sr.Close()
            End If
            Return ""
        End Try
    End Function

    Private Sub avviaProcedura()

        Dim ordinamento As Integer = 0
        If Request.QueryString("Ordinamento") = "1" Then
            ordinamento = 1
        End If

        If Request.QueryString("Dettaglio") = "1" Then
            dettaglioSi = True
        Else
            dettaglioSi = False
        End If

        macrocategoriaSi = False
        categoriaSi = False
        tipologiaUIsi = False
        vociSi = False
        competenzaSi = False
        capitoliSi = True

        '##########################################################################
        dataAggiornamento = ""
        If Not IsNothing(Request.QueryString("DataAggiornamento")) AndAlso Trim(Request.QueryString("DataAggiornamento")) <> "" Then
            dataAggiornamento = Request.QueryString("DataAggiornamento")
        Else
            dataAggiornamento = Format(Now, "yyyyMMdd")
        End If

        condizioneAggiornamento = ""
        If dataAggiornamento <> "" Then
            condizioneAggiornamento = " DATA_INSERIMENTO_BOL_BOLLETTE<=" & dataAggiornamento
        End If
        '##########################################################################

        Dim dataEmissioneDal As String = ""
        If Not IsNothing(Request.QueryString("DataEmissioneDal")) Then
            dataEmissioneDal = Request.QueryString("DataEmissioneDal")
        End If
        condizioneEmissioneDal = ""
        If dataEmissioneDal <> "" Then
            condizioneEmissioneDal = " AND DATA_EMISSIONE_BOL_BOLLETTE>=" & dataEmissioneDal
        End If
        Dim dataEmissioneAl As String = ""
        If Not IsNothing(Request.QueryString("DataEmissioneAl")) Then
            dataEmissioneAl = Request.QueryString("DataEmissioneAl")
        End If
        condizioneEmissioneAl = ""
        If dataEmissioneAl <> "" Then
            condizioneEmissioneAl = " AND DATA_EMISSIONE_BOL_BOLLETTE<=" & dataEmissioneAl
        End If
        '##########################################################################
        Dim dataRiferimentoDal As String = ""
        If Not IsNothing(Request.QueryString("DataRiferimentoDal")) Then
            dataRiferimentoDal = Request.QueryString("DataRiferimentoDal")
        End If
        condizioneRiferimentoDal = ""
        If dataRiferimentoDal <> "" Then
            condizioneRiferimentoDal = " AND RIFERIMENTO_DA_BOL_BOLLETTE>=" & dataRiferimentoDal
        End If
        Dim dataRiferimentoAl As String = ""
        If Not IsNothing(Request.QueryString("DataRiferimentoAl")) Then
            dataRiferimentoAl = Request.QueryString("DataRiferimentoAl")
        End If
        condizioneRiferimentoAl = ""
        If dataRiferimentoAl <> "" Then
            condizioneRiferimentoAl = " AND RIFERIMENTO_A_BOL_BOLLETTE<=" & dataRiferimentoAl
        End If
        '##########################################################################
        Dim listaBollettazione As System.Collections.Generic.List(Of String) = Session.Item("listaTipologiaBollettazione")
        'Session.Remove("listaTipologiaBollettazione")
        condizioneTipologiaBollettazione = ""
        If Not IsNothing(listaBollettazione) Then
            For Each Items As String In listaBollettazione
                condizioneTipologiaBollettazione &= Items & "," & CStr(CInt(Items) + 100) & ","
            Next
        End If
        If condizioneTipologiaBollettazione <> "" Then
            'condizioneTipologiaBollettazione &= "22,"
            condizioneTipologiaBollettazione = Left(condizioneTipologiaBollettazione, Len(condizioneTipologiaBollettazione) - 1)
            condizioneTipologiaBollettazione = " AND ID_TIPO_BOL_BOLLETTE IN (" & condizioneTipologiaBollettazione & ") "
        Else
            'condizioneTipologiaBollettazione = " AND (BOL_BOLLETTE.ID_TIPO IN (1,2,7) OR BOL_BOLLETTE.ID_TIPO>20) "
            condizioneTipologiaBollettazione = " "
        End If
        '##########################################################################
        '##########################################################################
        Dim listaBollettazioneRes As System.Collections.Generic.List(Of String) = Session.Item("listaTipologiaBollettazione")
        Session.Remove("listaTipologiaBollettazione")
        condizioneTipologiaBollettazioneRes = ""
        If Not IsNothing(listaBollettazioneRes) Then
            For Each Items As String In listaBollettazioneRes
                condizioneTipologiaBollettazioneRes &= Items & ","
            Next
        End If
        If condizioneTipologiaBollettazioneRes <> "" Then
            condizioneTipologiaBollettazioneRes = Left(condizioneTipologiaBollettazioneRes, Len(condizioneTipologiaBollettazioneRes) - 1)
            condizioneTipologiaBollettazioneRes = " AND ID_TIPO_BOLLETTA IN (" & condizioneTipologiaBollettazioneRes & ") "
        Else
            'condizioneTipologiaBollettazione = " AND (BOL_BOLLETTE.ID_TIPO IN (1,2,7) OR BOL_BOLLETTE.ID_TIPO>20) "
            condizioneTipologiaBollettazioneRes = " "
        End If
        '##########################################################################


        Dim listaEserciziContabili As System.Collections.Generic.List(Of String) = Session.Item("listaEserciziContabili")
        Session.Remove("listaEserciziContabili")
        Dim condizioneListaEserciziContabili As String = ""
        If Not IsNothing(listaEserciziContabili) Then
            For Each Items As String In listaEserciziContabili
                condizioneListaEserciziContabili &= Items & ","
            Next
        End If
        condizioneEsercizioContabile = ""


        selectEsercizioContabile = ""

        groupByEsercizioContabile = ""
        If condizioneListaEserciziContabili <> "" Then
            condizioneListaEserciziContabili = Left(condizioneListaEserciziContabili, Len(condizioneListaEserciziContabili) - 1)
        End If
        If condizioneListaEserciziContabili <> "" Then
            condizioneEsercizioContabile = " AND ID_ES_CONTABILE IN (" & condizioneListaEserciziContabili & ") "
        End If

        'fromEsercizioContabile = ",SISCOM_MI.T_VOCI_BOLLETTA_TIPI_CAP,SISCOM_MI.T_VOCI_BOLLETTA_CAP "
        selectEsercizioContabile = "INITCAP(CAPITOLO) AS CAPITOLO,"
        groupByEsercizioContabile = "INITCAP(CAPITOLO) "
        'fromEsercizioContabileB = ",SISCOM_MI.T_VOCI_BOLLETTA_TIPI_CAP,SISCOM_MI.T_VOCI_BOLLETTA_TIPI,SISCOM_MI.T_VOCI_BOLLETTA_CAP,SISCOM_MI.RAPPORTI_UTENZA "

        '##########################################################################
        Dim listaVoci As System.Collections.Generic.List(Of String) = Session.Item("listaVoci")
        'Session.Remove("listaVoci")
        condizioneListaVoci = ""
        If Not IsNothing(listaVoci) Then
            For Each Items As String In listaVoci
                condizioneListaVoci &= Items & ","
            Next
        End If
        If condizioneListaVoci <> "" Then
            condizioneListaVoci = Left(condizioneListaVoci, Len(condizioneListaVoci) - 1)
            condizioneListaVoci = " AND ID_VOCE_BOL_BOLLETTE_VOCI IN (" & condizioneListaVoci & ") "
            vociSi = True
        End If
        '##########################################################################
        '##########################################################################
        Dim listaVociRes As System.Collections.Generic.List(Of String) = Session.Item("listaVoci")
        Session.Remove("listaVoci")
        condizioneListaVociRes = ""
        If Not IsNothing(listaVociRes) Then
            For Each Items As String In listaVociRes
                condizioneListaVociRes &= Items & ","
            Next
        End If
        If condizioneListaVociRes <> "" Then
            condizioneListaVociRes = Left(condizioneListaVociRes, Len(condizioneListaVociRes) - 1)
            condizioneListaVociRes = " AND ID_T_VOCE_BOLLETTA IN (" & condizioneListaVociRes & ") "
            vociSi = True
        End If
        '##########################################################################
        Dim listaCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaCategorie")
        'Session.Remove("listaCategorie")
        condizioneCategoria = ""
        If Not IsNothing(listaCategorie) Then
            For Each Items As String In listaCategorie
                condizioneCategoria &= Items & ","
            Next
        End If
        If condizioneCategoria <> "" Then
            condizioneCategoria = Left(condizioneCategoria, Len(condizioneCategoria) - 1)
            condizioneCategoria = " AND TIPO_VOCE_T_VOCI_BOLLETTA IN (" & condizioneCategoria & ") "
            categoriaSi = True
        End If
        '##########################################################################
        '##########################################################################
        Dim listaCategorieRes As System.Collections.Generic.List(Of String) = Session.Item("listaCategorie")
        Session.Remove("listaCategorie")
        condizioneCategoriaRes = ""
        If Not IsNothing(listaCategorieRes) Then
            For Each Items As String In listaCategorieRes
                condizioneCategoriaRes &= Items & ","
            Next
        End If
        If condizioneCategoriaRes <> "" Then
            condizioneCategoriaRes = Left(condizioneCategoriaRes, Len(condizioneCategoriaRes) - 1)
            condizioneCategoriaRes = " AND ID_TIPO_VOCE_BOLLETTA IN (" & condizioneCategoriaRes & ") "
            categoriaSi = True
        End If
        '##########################################################################
        Dim listaMacroCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaMacrocategorie")
        'Session.Remove("listaMacrocategorie")
        condizioneMacrocategoria = ""
        If Not IsNothing(listaMacroCategorie) Then
            For Each Items As String In listaMacroCategorie
                condizioneMacrocategoria &= Items & ","
            Next
        End If
        If condizioneMacrocategoria <> "" Then
            condizioneMacrocategoria = Left(condizioneMacrocategoria, Len(condizioneMacrocategoria) - 1)
            condizioneMacrocategoria = " AND GRUPPO_T_VOCI_BOLLETTA IN (" & condizioneMacrocategoria & ") "
            macrocategoriaSi = True
        End If
        '##########################################################################
        '##########################################################################
        Dim listaMacroCategorieRes As System.Collections.Generic.List(Of String) = Session.Item("listaMacrocategorie")
        Session.Remove("listaMacrocategorie")
        condizioneMacrocategoriaRes = ""
        If Not IsNothing(listaMacroCategorieRes) Then
            For Each Items As String In listaMacroCategorieRes
                condizioneMacrocategoriaRes &= Items & ","
            Next
        End If
        If condizioneMacrocategoriaRes <> "" Then
            condizioneMacrocategoriaRes = Left(condizioneMacrocategoriaRes, Len(condizioneMacrocategoriaRes) - 1)
            condizioneMacrocategoriaRes = " AND id_gruppo_voce_bolletta IN (" & condizioneMacrocategoriaRes & ") "
            macrocategoriaSi = True
        End If
        '##########################################################################
        Dim listaCapitoli As System.Collections.Generic.List(Of String) = Session.Item("listaCapitoli")
        'Session.Remove("listaCapitoli")
        condizioneCapitoli = ""
        If Not IsNothing(listaCapitoli) Then
            For Each Items As String In listaCapitoli
                condizioneCapitoli &= Items & ","
            Next
        End If
        If condizioneCapitoli <> "" Then
            condizioneCapitoli = Left(condizioneCapitoli, Len(condizioneCapitoli) - 1)
            condizioneCapitoli = " AND ID_T_VOCI_BOLLETTA_CAP IN (" & condizioneCapitoli & ") "
        End If
        '##########################################################################
        '##########################################################################
        Dim listaCapitoliRes As System.Collections.Generic.List(Of String) = Session.Item("listaCapitoli")
        Session.Remove("listaCapitoli")
        condizioneCapitoliRes = ""
        If Not IsNothing(listaCapitoliRes) Then
            For Each Items As String In listaCapitoliRes
                condizioneCapitoliRes &= Items & ","
            Next
        End If
        If condizioneCapitoliRes <> "" Then
            condizioneCapitoliRes = Left(condizioneCapitoliRes, Len(condizioneCapitoliRes) - 1)
            condizioneCapitoliRes = " AND cap IN (" & condizioneCapitoliRes & ") "
        End If
        '##########################################################################
        Dim listatipologieUI As System.Collections.Generic.List(Of String) = Session.Item("listatipologieUI")
        'Session.Remove("listatipologieUI")
        condizioneTipologiaUI = ""
        If Not IsNothing(listatipologieUI) Then
            For Each Items As String In listatipologieUI
                condizioneTipologiaUI &= "'" & Items & "',"
            Next
        End If
        If condizioneTipologiaUI <> "" Then
            condizioneTipologiaUI = Left(condizioneTipologiaUI, Len(condizioneTipologiaUI) - 1)
            condizioneTipologiaUI = " AND COD_TIPOLOGIA_UI IN (" & condizioneTipologiaUI & ") "
            tipologiaUIsi = True
        End If
        '##########################################################################
        '##########################################################################
        Dim listatipologieUIRes As System.Collections.Generic.List(Of String) = Session.Item("listatipologieUI")
        Session.Remove("listatipologieUI")
        condizioneTipologiaUIres = ""
        If Not IsNothing(listatipologieUIRes) Then
            For Each Items As String In listatipologieUIRes
                condizioneTipologiaUIres &= "'" & Items & "',"
            Next
        End If
        If condizioneTipologiaUIres <> "" Then
            condizioneTipologiaUIres = Left(condizioneTipologiaUIres, Len(condizioneTipologiaUIres) - 1)
            condizioneTipologiaUIres = " AND TIPOLOGIA_UNITA_IMMOBILIARE IN (" & condizioneTipologiaUIres & ") "
            tipologiaUIsi = True
        End If
        '##########################################################################
        Dim listaCompetenza As System.Collections.Generic.List(Of String) = Session.Item("listaCompetenza")
        'Session.Remove("listaCompetenza")
        condizioneCompetenza = ""
        If Not IsNothing(listaCompetenza) Then
            For Each Items As String In listaCompetenza
                condizioneCompetenza &= Items & ","
            Next
        End If
        If condizioneCompetenza <> "" Then
            condizioneCompetenza = Left(condizioneCompetenza, Len(condizioneCompetenza) - 1)
            condizioneCompetenza = " AND COMPETENZA_T_VOCI_BOLLETTA IN (" & condizioneCompetenza & ") "

            competenzaSi = True
        End If
        '##########################################################################
        '##########################################################################
        Dim listaCompetenzaRes As System.Collections.Generic.List(Of String) = Session.Item("listaCompetenza")
        Session.Remove("listaCompetenza")
        condizioneCompetenzaRes = ""
        If Not IsNothing(listaCompetenzaRes) Then
            For Each Items As String In listaCompetenzaRes
                condizioneCompetenzaRes &= Items & ","
            Next
        End If
        If condizioneCompetenzaRes <> "" Then
            condizioneCompetenzaRes = Left(condizioneCompetenzaRes, Len(condizioneCompetenzaRes) - 1)
            condizioneCompetenzaRes = " AND ID_COMPETENZA IN (" & condizioneCompetenzaRes & ") "

            competenzaSi = True
        End If
        '##########################################################################
        Dim tipologiaCondominio As String = ""
        condizioneTipologiaCondominio = ""
        If Not IsNothing(Request.QueryString("Condominio")) Then
            tipologiaCondominio = Request.QueryString("Condominio")
            Select Case tipologiaCondominio
                Case -1
                    'nessuna condizione
                    condizioneTipologiaCondominio = ""
                Case 0
                    'non in condominio
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=0 "
                Case 1
                    'condomini gestione diretta
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=1 "
                Case 2
                    'condomini gestione indiretta
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=2 "
                Case 3
                    'tutti i condomini
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO>0 "
                Case Else
                    'nessuna condizione
                    condizioneTipologiaCondominio = ""
            End Select
        End If

        '######## TIPOLOGIA CONTO CORRENTE ##################
        Dim tipologiaContoCorrente As String = ""
        condizioneTipologiaContoCorrente = ""
        If Not IsNothing(Request.QueryString("TipologiaContoCorrente")) Then
            tipologiaContoCorrente = Request.QueryString("TipologiaContoCorrente")
            Select Case tipologiaContoCorrente
                Case 0
                    'TUTTI
                    condizioneTipologiaContoCorrente = " AND ID_CC>0 "
                Case 1
                    '59
                    condizioneTipologiaContoCorrente = " AND ID_CC=1 "
                Case 2
                    '60
                    condizioneTipologiaContoCorrente = " AND ID_CC=2 "
                Case Else
                    'TUTTI
                    condizioneTipologiaContoCorrente = " AND ID_CC>0 "
            End Select
        End If
        '##########################################

        '*************************************************************************************************************************
        'per gli incassi
        '######## DATA AGGIORNAMENTO ##################
        If Not IsNothing(Request.QueryString("DataAggiornamento")) And Request.QueryString("DataAggiornamento") <> "" Then
            dataAggiornamento = Request.QueryString("DataAggiornamento")
        Else
            dataAggiornamento = Format(Now, "yyyyMMdd")
        End If
        condizioneDataAggiornamento = ""
        If dataAggiornamento <> "" Then
            condizioneDataAggiornamento = " data_operazione<='" & dataAggiornamento & "' "
        End If
        '##########################################
        '######## DATA CONTABILE ##################
        Dim DataContabileDal As String = ""
        If Not IsNothing(Request.QueryString("DataContabileDa")) Then
            DataContabileDal = Request.QueryString("DataContabileDa")
        End If
        condizioneDataContabileDal = ""
        If DataContabileDal <> "" Then
            condizioneDataContabileDal = " AND DATA_VALUTA>=" & DataContabileDal & " "
        End If

        Dim DataContabileAl As String = ""
        If Not IsNothing(Request.QueryString("DataContabileA")) Then
            DataContabileAl = Request.QueryString("DataContabileA")
        End If
        condizioneDataContabileAl = ""
        If DataContabileAl <> "" Then
            condizioneDataContabileAl = " AND DATA_VALUTA<=" & DataContabileAl & " "
        End If
        '##########################################
        '######## DATA PAGAMENTO ##################
        Dim DataPagamentoDal As String = ""
        If Not IsNothing(Request.QueryString("DataPagamentoDal")) Then
            DataPagamentoDal = Request.QueryString("DataPagamentoDal")
        End If
        condizioneDataPagamentoDal = ""
        If DataPagamentoDal <> "" Then
            condizioneDataPagamentoDal = " AND data_pagamento>=" & DataPagamentoDal & " "
        End If
        Dim DataPagamentoAl As String = ""
        If Not IsNothing(Request.QueryString("DataPagamentoAl")) Then
            DataPagamentoAl = Request.QueryString("DataPagamentoAl")
        End If
        condizioneDataPagamentoAl = ""
        If DataPagamentoAl <> "" Then
            condizioneDataPagamentoAl = " AND data_pagamento<=" & DataPagamentoAl & " "
        End If
        '##########################################
        '######## TIPOLOGIA INCASSO ##################
        Dim tipologiaIncasso As String = ""
        condizioneTipologiaIncasso = ""
        If Not IsNothing(Request.QueryString("TipologiaIncasso")) Then
            tipologiaIncasso = Request.QueryString("TipologiaIncasso")
            Select Case tipologiaIncasso
                Case 0
                    'TUTTE
                    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO > 0 "
                Case 1
                    'MAV
                    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 1 "
                Case 2
                    'EXTRAMAV
                    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO >= 2 "
                Case Else
                    'TUTTE
                    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO > 0 "
            End Select
        End If
        '##########################################
        '######## condizione numero assegno ##################
        Dim numeroAssegno As String = ""
        If Not IsNothing(Request.QueryString("NumeroAssegno")) Then
            numeroAssegno = Request.QueryString("NumeroAssegno")
        End If
        condizioneNumeroAssegno = ""
        If numeroAssegno <> "" Then
            condizioneNumeroAssegno = " AND ID_VOCE_BOLLETTA IN (SELECT ID_VOCE_BOLLETTA FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT WHERE ID_EVENTO_PRINCIPALE IN (SELECT ID FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI WHERE ID_INCASSO_EXTRAMAV IN (SELECT ID FROM SISCOM_MI.INCASSI_EXTRAMAV WHERE NUMERO_ASSEGNO='" & numeroAssegno & "'))) "
        End If
        '##########################################

        '*************************************************************************************************************************

        selectMacrocategoria = ""
        selectCategoria = ""
        selectVoci = ""
        selectTipologiaUI = ""
        selectCompetenza = ""
        groupByMacrocategoria = ""
        groupByCategoria = ""
        groupByVoci = ""
        groupByTipologiaUI = ""
        groupByCompetenza = ""

        If macrocategoriaSi Then
            selectMacrocategoria = " INITCAP(substr(nvl(MACROCATEGORIA,''),1,17)) AS MACROCATEGORIA, "
            groupByMacrocategoria = ",nvl(MACROCATEGORIA,'') "
        Else
            selectMacrocategoria = " '' AS MACROCATEGORIA, "
            groupByMacrocategoria = ""
        End If

        If categoriaSi Then
            selectCategoria = " INITCAP(substr(nvl(CATEGORIA,''),1,17)) AS CATEGORIA, "
            groupByCategoria = ",nvl(CATEGORIA,'') "
        Else
            selectCategoria = " '' AS CATEGORIA, "
            groupByCategoria = ""
        End If

        If vociSi Then
            selectVoci = " INITCAP(substr(nvl(VOCE,''),1,17)) AS VOCE, "
            groupByVoci = ",nvl(VOCE,'') "
        Else
            selectVoci = " '' AS VOCE, "
            groupByVoci = ""
        End If

        If competenzaSi Then
            selectCompetenza = " INITCAP(COMPETENZA) AS COMPETENZA, "
            groupByCompetenza = " ,COMPETENZA "
        Else
            selectCompetenza = " '' AS COMPETENZA, "
            groupByCompetenza = ""
        End If

        If tipologiaUIsi Then
            selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI, INITCAP(NVL(TIPO_UI,'')) AS TIPO_UI, "
            groupByTipologiaUI = ",USI_ABITATIVI,TIPO_UI "
        Else
            selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI,'' AS TIPO_UI, "
            groupByTipologiaUI = ", USI_ABITATIVI "
        End If

        Dim dettaglio As String = ""
        Dim dettaglioGroup As String = ""
        If dettaglioSi Then
            dettaglio = " ANNO_RIF AS ANNO, " _
                & " INITCAP(BIMESTRE) AS BIMESTRE, "
            dettaglioGroup = ",ANNO_RIF, BIMESTRE "
        Else
            dettaglio = " '' AS ANNO, '' AS BIMESTRE, "
            dettaglioGroup = " "

        End If

        filtriRicerca = Session.Item("filtriRicerca")
        Session.Remove("filtriRicerca")
        Session.Add("TAB_TEMPORANEA", "PROC_RESIDUI_" & Format(Now, "yyyyMMddHHmmss"))




        Dim parameter As String = ""
        parameter &= "DATAAGGIORNAMENTO:" & dataAggiornamento & "#"
        parameter &= "SELECTESERCIZIOCONTABILE:" & selectEsercizioContabile & "#"
        parameter &= "SELECTCOMPETENZA:" & selectCompetenza & "#"
        parameter &= "SELECTMACROCATEGORIA:" & selectMacrocategoria & "#"
        parameter &= "SELECTCATEGORIA:" & selectCategoria & "#"
        parameter &= "SELECTVOCI:" & selectVoci & "#"
        parameter &= "SELECTTIPOLOGIAUI:" & selectTipologiaUI & "#"
        parameter &= "CONDIZIONEAGGIORNAMENTO:" & condizioneAggiornamento & "#"
        parameter &= "CONDIZIONEEMISSIONEDAL:" & condizioneEmissioneDal & "#"
        parameter &= "CONDIZIONEEMISSIONEAL:" & condizioneEmissioneAl & "#"
        parameter &= "CONDIZIONERIFERIMENTODAL:" & condizioneRiferimentoDal & "#"
        parameter &= "CONDIZIONERIFERIMENTOAL:" & condizioneRiferimentoAl & "#"
        parameter &= "CONDIZIONETIPOLOGIABOLLETTAZIONE:" & condizioneTipologiaBollettazione & "#"
        parameter &= "CONDIZIONELISTAVOCI:" & condizioneListaVoci & "#"
        parameter &= "CONDIZIONECATEGORIA:" & condizioneCategoria & "#"
        parameter &= "CONDIZIONEMACROCATEGORIA:" & condizioneMacrocategoria & "#"
        parameter &= "CONDIZIONECAPITOLI:" & condizioneCapitoli & "#"
        parameter &= "CONDIZIONETIPOLOGIAUI:" & condizioneTipologiaUI & "#"
        parameter &= "CONDIZIONECOMPETENZA:" & condizioneCompetenza & "#"
        parameter &= "CONDIZIONETIPOLOGIACONDOMINIO:" & condizioneTipologiaCondominio & "#"
        parameter &= "CONDIZIONETIPOLOGIACONTOCORRENTE:" & condizioneTipologiaContoCorrente & "#"
        parameter &= "CONDIZIONEESERCIZIOCONTABILE:" & condizioneEsercizioContabile & "#"
        parameter &= "GROUPBYESERCIZIOCONTABILE:" & groupByEsercizioContabile & "#"
        parameter &= "GROUPBYCOMPETENZA:" & groupByCompetenza & "#"
        parameter &= "GROUPBYMACROCATEGORIA:" & groupByMacrocategoria & "#"
        parameter &= "GROUPBYCATEGORIA:" & groupByCategoria & "#"
        parameter &= "GROUPBYVOCI:" & groupByVoci & "#"
        parameter &= "GROUPBYTIPOLOGIAUI:" & groupByTipologiaUI & "#"
        parameter &= "VOCISI:" & vociSi & "#"
        parameter &= "TIPOLOGIAUISI:" & tipologiaUIsi & "#"
        parameter &= "MACROCATEGORIASI:" & macrocategoriaSi & "#"
        parameter &= "DETTAGLIOSI:" & dettaglioSi & "#"
        parameter &= "CONDIZIONELISTAVOCIRES:" & condizioneListaVociRes & "#"
        parameter &= "CONDIZIONEDATAPAGAMENTOAL:" & condizioneDataPagamentoAl & "#"
        parameter &= "CONDIZIONEDATAPAGAMENTODAL:" & condizioneDataPagamentoDal & "#"
        parameter &= "CONDIZIONEDATACONTABILEDAL:" & condizioneDataContabileDal & "#"
        parameter &= "CONDIZIONEDATAAGGIORNAMENTO:" & condizioneDataAggiornamento & "#"
        parameter &= "CONDIZIONECOMPETENZARES:" & condizioneCompetenzaRes & "#"
        parameter &= "CONDIZIONEMACROCATEGORIARES:" & condizioneMacrocategoriaRes & "#"
        parameter &= "CONDIZIONECATEGORIARES:" & condizioneCategoriaRes & "#"
        parameter &= "CONDIZIONECAPITOLIRES:" & condizioneCapitoliRes & "#"
        parameter &= "COMPETENZASI:" & competenzaSi & "#"
        parameter &= "CATEGORIASI:" & categoriaSi & "#"
        parameter &= "CAPITOLISI:" & capitoliSi & "#"
        parameter &= "CONDIZIONEDATACONTABILEAL:" & condizioneDataContabileAl & "#"
        parameter &= "CONDIZIONETIPOLOGIABOLLETTAZIONERES:" & condizioneTipologiaBollettazioneRes & "#"
        parameter &= "CONDIZIONETIPOLOGIAUIRES:" & condizioneTipologiaUIres & "#"
        parameter &= "TAB_TEMPORANEA:" & Session.Item("TAB_TEMPORANEA") & "#"
        parameter &= "DETTAGLIO:" & dettaglio & "#"
        parameter &= "DETTAGLIOGROUP:" & dettaglioGroup & "#"
        parameter &= "ORDINAMENTO:" & ordinamento
        Dim valore As Integer = 0

        Dim parameter2 As String = ""
        If Len(parameter) > 4000 Then
            parameter2 = Right(parameter, Len(parameter) - 4000)
            parameter = Left(parameter, 4000)
        End If
        Try
            connData.apri()
            par.cmd.CommandText = "select SISCOM_MI.seq_PROCEDURE_RESIDUI.NEXTVAL from dual"
            valore = par.cmd.ExecuteScalar
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PROCEDURE_RESIDUI (" _
                & " ID, NOME_TABELLA, DATA_ORA_inizio, " _
                & " PARAMETRI_RICERCA, NOME_OPERATORE,esito,ordinamento,PARAMETER,PARAMETER2) " _
                & " VALUES (" & valore & "," _
                & " '" & Session.Item("TAB_TEMPORANEA") & "'," _
                & "'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & " '" & CStr(filtriRicerca).Replace("'", "''") & "'," _
                & " '" & Session.Item("OPERATORE") & "',0," & ordinamento & ",'" & par.PulisciStrSql(parameter) & "','" & par.PulisciStrSql(parameter2) & "')"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Response.Write("<script>alert('Si è verificato un errore durante l\'operazione. Riprovare!');self.close();</script>")
            Exit Sub
        End Try

        Try

            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & valore
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/CalcoloResidui3.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()

            'Dim ws As New localhost.CalcoloResidui
            'ws.BegincaricaDati(dataAggiornamento _
            '                           , selectEsercizioContabile _
            '                           , selectCompetenza _
            '                           , selectMacrocategoria _
            '                           , selectCategoria _
            '                           , selectVoci _
            '                           , selectTipologiaUI _
            '                           , condizioneAggiornamento _
            '                           , condizioneEmissioneDal _
            '                           , condizioneEmissioneAl _
            '                           , condizioneRiferimentoDal _
            '                           , condizioneRiferimentoAl _
            '                           , condizioneTipologiaBollettazione _
            '                           , condizioneListaVoci _
            '                           , condizioneCategoria _
            '                           , condizioneMacrocategoria _
            '                           , condizioneCapitoli _
            '                           , condizioneTipologiaUI _
            '                           , condizioneCompetenza _
            '                           , condizioneTipologiaCondominio _
            '                           , condizioneTipologiaContoCorrente _
            '                           , condizioneEsercizioContabile _
            '                           , groupByEsercizioContabile _
            '                           , groupByCompetenza _
            '                           , groupByMacrocategoria _
            '                           , groupByCategoria _
            '                           , groupByVoci _
            '                           , groupByTipologiaUI _
            '                           , vociSi _
            '                           , tipologiaUIsi _
            '                           , macrocategoriaSi _
            '                           , dettaglioSi _
            '                           , condizioneListaVociRes _
            '                           , condizioneDataPagamentoAl _
            '                           , condizioneDataPagamentoDal _
            '                           , condizioneDataContabileDal _
            '                           , condizioneDataAggiornamento _
            '                           , condizioneCompetenzaRes _
            '                           , condizioneMacrocategoriaRes _
            '                           , condizioneCategoriaRes _
            '                           , condizioneCapitoliRes _
            '                           , competenzaSi _
            '                           , categoriaSi _
            '                           , capitoliSi _
            '                           , condizioneDataContabileAl _
            '                           , condizioneTipologiaBollettazioneRes _
            '                           , condizioneTipologiaUIres _
            '                           , Session.Item("TAB_TEMPORANEA"), dettaglio, dettaglioGroup, ordinamento, Nothing, Nothing)
            Response.Write("<script>alert('Generazione report residui avviata correttamente!');if(window.opener){window.opener.location.replace('ElencoResidui.aspx');};self.close();</script>")

        Catch ex As Exception
            Response.Write("<script>alert('Impossibile avviare il report!');self.close();</script>")
            Exit Sub

        End Try
    End Sub

    Private Function controlloAvviaProcedura() As Boolean
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PROCEDURE_RESIDUI WHERE NOME_OPERATORE='" & Session.Item("OPERATORE") & "' AND ESITO=0 "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim controllo As Boolean
            If lettore.HasRows Then
                Response.Write("<script>alert('E\' in esecuzione un\'altra generazione del report residui!\nAttendere che quest\'ultima sia terminata prima di\ngenerarne un\'altra.');if(window.opener){window.opener.location.replace('ElencoResidui.aspx');};self.close();</script>")
                controllo = False
            Else
                controllo = True
            End If
            lettore.Close()
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            Return controllo
        Catch ex As Exception
            connData.chiudi()
            Response.Write("<script>alert('Si è verificato un errore durante l\'operazione. Riprovare!');</script>")
            Return True
        End Try
    End Function
End Class

