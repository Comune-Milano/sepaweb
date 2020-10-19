Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCaricamentoNew
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim Str As String = ""
    Public percentuale As Long = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = dgvTipoUtenze.ClientID
            Me.txtDataCaricamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            If Request.QueryString("TIPO") = "C" Then
                idTipo.Value = "C"
                Me.lblTitolo.Text = "Custodi - Caricamento file"
                Me.RadButtonCarica.Text = "Carica custodi"
            ElseIf Request.QueryString("TIPO") = "M" Then
                idTipo.Value = "M"
                Me.lblTitolo.Text = "Multe - Caricamento file"
                Me.RadButtonCarica.Text = "Carica multe"
            ElseIf Request.QueryString("TIPO") = "COSAP" Then
                idTipo.Value = "COSAP"
                Me.lblTitolo.Text = "Cosap - Caricamento file"
                Me.RadButtonCarica.Text = "Carica COSAP"
            Else
                idTipo.Value = "U"
                Me.lblTitolo.Text = "Utenze - Caricamento file"
            End If

            CaricaEsercizio()
            Me.txtDataCaricamento.Text = Format(Now, "dd/MM/yyyy")
        End If

    End Sub
    Public Property NumeroElementi() As Integer
        Get
            If Not (ViewState("par_NumeroElementi") Is Nothing) Then
                Return CStr(ViewState("par_NumeroElementi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NumeroElementi") = value
        End Set
    End Property
    Private Sub CaricaEsercizio()

        Try
            Dim sql As String = "Select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY')||' - '||TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY')||' '||SISCOM_MI.PF_STATI.DESCRIZIONE as descrizione " _
                    & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                    & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                    & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                    & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by id desc"
            par.caricaComboTelerik(sql, cmbEsercizio, "ID", "descrizione", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - CaricaEsercizio - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try


    End Sub
    Protected Sub dgvTipoUtenze_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvTipoUtenze.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - dgvTipoUtenze_ItemCommand - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvTipoUtenze.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)



                e.Item.Attributes.Add("onclick", "document.getElementById('idPiano').value = '" & dataItem("id_piano_finanziario").Text & "';" _
                          & "document.getElementById('idTipoUtenza').value = '" & dataItem("id_tipo_utenza").Text & "';" _
                          & "document.getElementById('idFornitore').value = '" & dataItem("id_fornitore").Text & "';" _
                          & "document.getElementById('idVocePf').value = '" & dataItem("id_voce_pf").Text & "';" _
                          & "document.getElementById('idVocePfImporto').value = '" & dataItem("id_voce_pf_importo").Text & "';" _
                          & "document.getElementById('idStruttura').value = '" & dataItem("id_struttura").Text & "';" _
                          & "document.getElementById('idParam').value = '" & dataItem("id").Text & "';")


            End If

            If isExporting.Value = "1" Then
                If e.Item.ItemIndex > 0 Then
                    Dim context As RadProgressContext = RadProgressContext.Current
                    If context.SecondaryTotal <> NumeroElementi Then
                        context.SecondaryTotal = NumeroElementi
                    End If
                    context.SecondaryValue = e.Item.ItemIndex.ToString()
                    context.SecondaryPercent = Int((e.Item.ItemIndex.ToString() * 100) / NumeroElementi)
                    context.CurrentOperationText = "Export excel in corso"
                End If
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)

            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - DataGrid1_ItemDataBound - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try

    End Sub


#Region "Import Function"
    Private Sub ImportaA2A(ByVal sPath As String, ByVal sFileName As String, ByVal idParamUt As Integer)
        Dim myRow As String()
        Dim ocmd As Oracle.DataAccess.Client.OracleCommand
        Dim nRows As Int32 = 0
        'Variabili Testata
        Dim NomeDocumento As String, DataEmissione As String, DataScadenza As String, NumeroFattura As String, AnnoFattura As String
        Dim Contatore As Integer = 0
        'Variabili Calcolo
        Dim DataInizioPeriodo As String, DataFinePeriodo As String

        'Variabili Contratto
        Dim Pod As String, NomeViaFornitura As String, NumeroCivicoFornitura As String, BarratoFornitura As String, CAPFornitura As String, LocalitaFornitura As String, ProvinciaFornitura As String

        Dim TotaleOneriDiversi As Decimal
        Dim Iva As Decimal
        Dim BaseImponibile As Decimal
        Dim TotaleBolletta As Decimal
        Dim TotaleBollettino As Decimal

        'TESTATA
        NomeDocumento = ""
        DataEmissione = ""
        DataScadenza = ""
        AnnoFattura = ""
        NumeroFattura = ""
        DataInizioPeriodo = ""
        DataFinePeriodo = ""
        'CONTRATTI
        Pod = ""
        NomeViaFornitura = ""
        NumeroCivicoFornitura = ""
        BarratoFornitura = ""
        CAPFornitura = ""
        LocalitaFornitura = ""
        ProvinciaFornitura = ""

        'RIEPILOGO
        TotaleOneriDiversi = 0
        BaseImponibile = 0
        Iva = 0
        TotaleBolletta = 0
        TotaleBollettino = 0

        connData.apri(True)
        ocmd = connData.Connessione.CreateCommand
        Try


            If Not sPath.EndsWith("\") Then
                sPath &= "\"
            End If
            Dim hasanomalie As Boolean = False
            Dim hasInsert As Integer = 0
            Dim TotRighe As Integer = 0
            Response.Flush()
            Dim nrigaconta As Integer
            Using MyReaderConta As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                MyReaderConta.TextFieldType = FileIO.FieldType.Delimited
                MyReaderConta.SetDelimiters(";")

                While Not MyReaderConta.EndOfData
                    nrigaconta += 1
                    myRow = MyReaderConta.ReadFields

                    If myRow(0) = "440" Then
                        TotRighe += 1

                    End If
                End While
                MyReaderConta.Close()
            End Using
            If TotRighe = 0 Then
                TotRighe = 1
            End If
            Dim strGiaInserite As String = ""

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)

                'Specify that reading from a comma-delimited file'
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")

                While Not MyReader.EndOfData
                    nRows += 1
                    myRow = MyReader.ReadFields()

                    If myRow(0) = "010" Then
                        'TESTATA
                        NomeDocumento = myRow(1)
                        DataEmissione = AggiustaData(myRow(2))
                        DataScadenza = AggiustaData(myRow(3))
                        AnnoFattura = myRow(4)
                        NumeroFattura = myRow(7)
                    ElseIf myRow(0) = "040" Then
                        'CALCOLO
                        DataInizioPeriodo = AggiustaData(myRow(1))
                        DataFinePeriodo = AggiustaData(myRow(2))
                    ElseIf myRow(0) = "050" Then
                        'CONTRATTI
                        Pod = myRow(6)
                        NomeViaFornitura = myRow(28)
                        NumeroCivicoFornitura = myRow(29)
                        BarratoFornitura = myRow(30)
                        CAPFornitura = myRow(31)
                        LocalitaFornitura = myRow(32)
                        ProvinciaFornitura = myRow(33)

                    ElseIf myRow(0) = "440" Then
                        'RIEPILOGO
                        TotaleOneriDiversi = myRow(4)
                        BaseImponibile = myRow(6)
                        Iva = myRow(7)
                        TotaleBolletta = myRow(8)
                        TotaleBollettino = myRow(10)

                        If TotaleOneriDiversi <> 0 Then TotaleOneriDiversi = TotaleOneriDiversi / 100
                        If BaseImponibile <> 0 Then BaseImponibile = BaseImponibile / 100
                        If Iva <> 0 Then Iva = Iva / 100
                        If TotaleBolletta <> 0 Then TotaleBolletta = TotaleBolletta / 100
                        If TotaleBollettino <> 0 Then TotaleBollettino = TotaleBollettino / 100

                        'ElseIf myRow(0) = "470" Then
                        'FINE BLOCCO

                        Try
                            'INSERT DATI 
                            ocmd.CommandText = "INSERT INTO SISCOM_MI.FATTURE_UTENZE (id, ID_PARAM_UTENZA, nome_file,data_caricamento, data_emissione, data_scadenza, numero_fattura, anno_fattura, data_inizio_periodo, data_fine_periodo, pod, " _
                                             & " nome_via_fornitura, numero_civico_fornitura, barrato_fornitura,  cap_fornitura, localita_fornitura, provincia_fornitura, Totale_Oneri_Diversi, Base_Imponibile, IVA, Totale_Bolletta, Totale_Bollettino ) VALUES ( " _
                                             & " SISCOM_MI.SEQ_fatture_utenze.NEXTVAL  /* id */, " _
                                             & idParamUt & "/* ID_PARAM_UTENZA */," _
                                             & StrSql(sFileName) & "/* nome_file */," _
                                             & par.insDbValue(Me.txtDataCaricamento.Text, True, True) & "/*DATA_CARICAMENTO*/," _
                                             & StrSql(DataEmissione) & "  /* data_emissione */, " _
                                             & StrSql(DataScadenza) & "  /* data_scadenza */, " _
                                             & StrSql(NumeroFattura) & "  /* numero_fattura */," _
                                             & StrSql(AnnoFattura) & "  /* anno_fattura */, " _
                                             & StrSql(DataInizioPeriodo) & "  /* data_inizio_periodo */, " _
                                             & StrSql(DataFinePeriodo) & "  /* data_fine_periodo */, " _
                                             & StrSql(Pod) & "  /* pod */, " _
                                             & StrSql(NomeViaFornitura) & "  /* nome_via_fornitura */, " _
                                             & StrSql(NumeroCivicoFornitura) & "  /* numero_civico_fornitura */, " _
                                             & StrSql(BarratoFornitura) & "  /* barrato_fornitura */, " _
                                             & StrSql(CAPFornitura) & "  /* cap_fornitura */, " _
                                             & StrSql(LocalitaFornitura) & "  /* localita_fornitura */, " _
                                             & StrSql(ProvinciaFornitura) & " /* provincia_fornitura */, " _
                                             & par.VirgoleInPunti(TotaleOneriDiversi.ToString) & " /* totale_oneri_diversi */, " _
                                             & par.VirgoleInPunti(BaseImponibile.ToString) & " /* base_imponibile */, " _
                                             & par.VirgoleInPunti(Iva.ToString) & " /* iva */, " _
                                             & par.VirgoleInPunti(TotaleBolletta.ToString) & " /* totale_bolletta */, " _
                                             & par.VirgoleInPunti(TotaleBollettino.ToString) & " /* totale_bollettino */  )"
                            ocmd.ExecuteNonQuery()
                            hasInsert += 1
                        Catch oracle_ex As Oracle.DataAccess.Client.OracleException
                            If oracle_ex.Number = 1 Then
                                'Fattura gia presente
                                strGiaInserite += "\n - " & NumeroFattura & "/" & AnnoFattura
                            Else
                                'altri errori
                                ScriviAnomalia(idParamUt, sFileName, Me.txtDataCaricamento.Text, NumeroFattura, AnnoFattura, oracle_ex.Message)
                                hasanomalie = True

                            End If

                        End Try


                        'TESTATA
                        NomeDocumento = ""
                        DataEmissione = ""
                        DataScadenza = ""
                        AnnoFattura = ""
                        NumeroFattura = ""
                        DataInizioPeriodo = ""
                        DataFinePeriodo = ""
                        'CONTRATTI
                        Pod = ""
                        NomeViaFornitura = ""
                        NumeroCivicoFornitura = ""
                        BarratoFornitura = ""
                        CAPFornitura = ""
                        LocalitaFornitura = ""
                        ProvinciaFornitura = ""

                        'RIEPILOGO
                        TotaleOneriDiversi = 0
                        BaseImponibile = 0
                        Iva = 0
                        TotaleBolletta = 0
                        TotaleBollettino = 0
                        Contatore = Contatore + 1
                        percentuale = (Contatore * 100) / TotRighe
                        Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                        Response.Flush()

                    End If
                End While

                MyReader.Close()
            End Using
            connData.chiudi(True)
            If hasanomalie = False Then
                If String.IsNullOrEmpty(strGiaInserite) Then
                    RadWindowManager1.RadAlert(hasInsert & " Fatture caricate !", 300, 150, "Attenzione", "", "null")
                Else
                    RadWindowManager1.RadAlert(hasInsert & " Fatture caricate.\nLe seguenti sono state escluse perchè già presenti:" & strGiaInserite & "!", 300, 150, "Attenzione", "", "null")

                End If
            Else
                RadWindowManager1.RadAlert(hasInsert & " Fatture caricate!Si sono verificate delle anomalie nella lettura del file!", 300, 150, "Attenzione", "", "null")

            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - ImportaA2A - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)

        End Try
    End Sub
    Private Sub ImportaH20(ByVal sPath As String, ByVal sFileName As String, ByVal idParamUt As Integer)
        Try

            Dim myRow As String()
            Dim ocmd As Oracle.DataAccess.Client.OracleCommand
            Dim nRows As Int32 = 0
            'Variabili Testata
            Dim NomeDocumento As String, DataEmissione As String, DataScadenza As String, NumeroFattura As String, AnnoFattura As String, bNuovaTestata As Boolean = False
            Dim Contatore As Integer = 0

            'Variabili Calcolo
            Dim DataInizioPeriodo As String, DataFinePeriodo As String

            'Variabili Contratto
            Dim Pod As String, NomeViaFornitura As String, NumeroCivicoFornitura As String, BarratoFornitura As String, CAPFornitura As String, LocalitaFornitura As String, ProvinciaFornitura As String, bNuovoContratto As Boolean = False

            Dim TotaleOneriDiversi As Decimal
            Dim Iva As Decimal
            Dim BaseImponibile As Decimal
            Dim TotaleBolletta As Decimal
            Dim TotaleBollettino As Decimal


            connData.apri(True)
            ocmd = connData.Connessione.CreateCommand

            If Not sPath.EndsWith("\") Then
                sPath &= "\"
            End If
            Dim hasAnomalie As Boolean = False
            Dim hasInsert As Integer = 0
            Dim TotRighe As Integer = 0
            Response.Flush()
            Dim nrigaconta As Integer
            Using MyReaderConta As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                MyReaderConta.TextFieldType = FileIO.FieldType.Delimited
                MyReaderConta.SetDelimiters(";")

                While Not MyReaderConta.EndOfData
                    nrigaconta += 1
                    myRow = MyReaderConta.ReadFields

                    TotRighe += 1

                End While
                MyReaderConta.Close()
            End Using
            Dim strGiaInserite As String = ""
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")
                While Not MyReader.EndOfData
                    myRow = MyReader.ReadFields()
                    nRows += 1
                    If nRows > 1 Then

                        'TESTATA
                        NomeDocumento = ""
                        DataEmissione = ""
                        DataScadenza = ""
                        AnnoFattura = ""
                        NumeroFattura = ""
                        bNuovaTestata = False
                        DataInizioPeriodo = ""
                        DataFinePeriodo = ""

                        'CONTRATTI
                        Pod = ""
                        NomeViaFornitura = ""
                        NumeroCivicoFornitura = ""
                        BarratoFornitura = ""
                        CAPFornitura = ""
                        LocalitaFornitura = ""
                        ProvinciaFornitura = ""
                        bNuovoContratto = False

                        'RIEPILOGO
                        TotaleOneriDiversi = 0
                        BaseImponibile = 0
                        Iva = 0
                        TotaleBolletta = 0
                        TotaleBollettino = 0

                        'TESTATA
                        NomeDocumento = sFileName
                        DataEmissione = AggiustaData(myRow(51))
                        DataScadenza = AggiustaData(myRow(52))
                        AnnoFattura = myRow(14)
                        NumeroFattura = myRow(15)
                        bNuovaTestata = True

                        'CALCOLO
                        DataInizioPeriodo = AggiustaData(myRow(25))
                        DataFinePeriodo = AggiustaData(myRow(26))

                        'CONTRATTI
                        Pod = myRow(11)
                        NomeViaFornitura = myRow(4) & " " & myRow(5)
                        NumeroCivicoFornitura = myRow(6)
                        BarratoFornitura = myRow(7)
                        CAPFornitura = myRow(10)
                        LocalitaFornitura = myRow(8)
                        ProvinciaFornitura = myRow(9)
                        bNuovoContratto = True
                        'RIEPILOGO
                        TotaleOneriDiversi = myRow(53)
                        BaseImponibile = myRow(44)
                        Iva = myRow(45)
                        TotaleBolletta = myRow(22)
                        TotaleBollettino = myRow(54)

                        'If TotaleOneriDiversi <> 0 Then TotaleOneriDiversi = TotaleOneriDiversi / 100
                        'If BaseImponibile <> 0 Then BaseImponibile = BaseImponibile / 100
                        'If Iva <> 0 Then Iva = Iva / 100
                        'If TotaleBolletta <> 0 Then TotaleBolletta = TotaleBolletta / 100
                        'If TotaleBollettino <> 0 Then TotaleBollettino = TotaleBollettino / 100

                        Try
                            'INSERT DATI 
                            ocmd.CommandText = "INSERT INTO SISCOM_MI.FATTURE_UTENZE (id,ID_PARAM_UTENZA,nome_file,data_caricamento, data_emissione, data_scadenza, numero_fattura, anno_fattura, data_inizio_periodo, data_fine_periodo, pod, " _
                                             & " nome_via_fornitura, numero_civico_fornitura, barrato_fornitura,  cap_fornitura, localita_fornitura, provincia_fornitura, Totale_Oneri_Diversi, Base_Imponibile, IVA, Totale_Bolletta, Totale_Bollettino ) VALUES ( " _
                                             & " SISCOM_MI.SEQ_fatture_utenze.NEXTVAL  /* id */, " _
                                             & idParamUt & "/* ID_PARAM_UTENZA */," _
                                             & StrSql(sFileName) & "/* nome_file */," _
                                             & par.insDbValue(Me.txtDataCaricamento.Text, True, True) & "/*DATA_CARICAMENTO*/," _
                                             & StrSql(DataEmissione) & "  /* data_emissione */, " _
                                             & StrSql(DataScadenza) & "  /* data_scadenza */, " _
                                             & StrSql(NumeroFattura) & "  /* numero_fattura */," _
                                             & StrSql(AnnoFattura) & "  /* anno_fattura */, " _
                                             & StrSql(DataInizioPeriodo) & "  /* data_inizio_periodo */, " _
                                             & StrSql(DataFinePeriodo) & "  /* data_fine_periodo */, " _
                                             & StrSql(Pod) & "  /* pod */, " _
                                             & StrSql(NomeViaFornitura) & "  /* nome_via_fornitura */, " _
                                             & StrSql(NumeroCivicoFornitura) & "  /* numero_civico_fornitura */, " _
                                             & StrSql(BarratoFornitura) & "  /* barrato_fornitura */, " _
                                             & StrSql(CAPFornitura) & "  /* cap_fornitura */, " _
                                             & StrSql(LocalitaFornitura) & "  /* localita_fornitura */, " _
                                             & StrSql(ProvinciaFornitura) & " /* provincia_fornitura */, " _
                                             & par.VirgoleInPunti(TotaleOneriDiversi.ToString) & " /* totale_oneri_diversi */, " _
                                             & par.VirgoleInPunti(BaseImponibile.ToString) & " /* base_imponibile */, " _
                                             & par.VirgoleInPunti(Iva.ToString) & " /* iva */, " _
                                             & par.VirgoleInPunti(TotaleBolletta.ToString) & " /* totale_bolletta */, " _
                                             & par.VirgoleInPunti(TotaleBollettino.ToString) & " /* totale_bollettino */  )"
                            ocmd.ExecuteNonQuery()
                            hasInsert += 1
                            Contatore = Contatore + 1
                            percentuale = (Contatore * 100) / TotRighe
                            Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                            Response.Flush()

                        Catch oracle_ex As Oracle.DataAccess.Client.OracleException
                            If oracle_ex.Number = 1 Then
                                'Fattura gia presente
                                strGiaInserite += "\n - " & NumeroFattura & "/" & AnnoFattura
                            Else
                                ScriviAnomalia(idParamUt, sFileName, Me.txtDataCaricamento.Text, NumeroFattura, AnnoFattura, oracle_ex.Message)
                                hasAnomalie = True
                            End If

                        End Try
                    End If
                End While
                MyReader.Close()
            End Using
            connData.chiudi(True)
            If hasAnomalie = False Then
                If String.IsNullOrEmpty(strGiaInserite) Then
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate !')", True)
                    RadWindowManager1.RadAlert(hasInsert & " Fatture caricate!", 300, 150, "Attenzione", Nothing, Nothing)
                Else
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate.\nLe seguenti sono state escluse perchè già presenti:" & strGiaInserite & "')", True)
                    RadWindowManager1.RadAlert(hasInsert & " Fatture caricate.\nLe seguenti sono state escluse perchè già presenti:" & strGiaInserite, 300, 150, "Attenzione", Nothing, Nothing)

                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate!Si sono verificate delle anomalie nella lettura del file!');window.open('FattureCaricAnomlie.aspx?NOME_FILE=" & sFileName & "');", True)

            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - ImportaH20 - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)

        End Try

    End Sub

    Private Sub ImportaCustodi(ByVal sPath As String, ByVal sFileName As String, ByVal idParamUt As Integer)
        Try

            Dim myRow As String()
            Dim ocmd As Oracle.DataAccess.Client.OracleCommand
            Dim nRows As Int32 = 0
            'Variabili Testata
            Dim NomeDocumento As String
            Dim Contatore As Integer = 0

            'Variabili CUSTODI
            Dim Sigla As String, annoC As String, meseC As String, codCustode As String

            Dim Importo As Decimal

            'TESTATA
            NomeDocumento = "sFileName"
            'RIEPILOGO
            Importo = 0

            connData.apri(True)
            ocmd = connData.Connessione.CreateCommand


            If Not sPath.EndsWith("\") Then
                sPath &= "\"
            End If
            Dim hasanomalie As Boolean = False
            Dim hasInsert As Integer = 0
            Dim TotRighe As Integer = 0
            Response.Flush()
            Dim nrigaconta As Integer
            Dim strGiaInserite As String = ""

            Using MyReaderConta As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                MyReaderConta.TextFieldType = FileIO.FieldType.Delimited
                MyReaderConta.SetDelimiters(";")

                While Not MyReaderConta.EndOfData
                    nrigaconta += 1
                    myRow = MyReaderConta.ReadFields

                    TotRighe += 1

                End While
                MyReaderConta.Close()
            End Using
            If TotRighe = 0 Then
                TotRighe = 1
            End If
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)

                'Specify that reading from a comma-delimited file'
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")

                While Not MyReader.EndOfData
                    nRows += 1
                    myRow = MyReader.ReadFields()

                    If nRows > 1 Then
                        Sigla = myRow(0)
                        annoC = myRow(1)
                        meseC = myRow(2)
                        codCustode = myRow(3)
                        Importo = myRow(4)




                        Try
                            'INSERT DATI 
                            ocmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI_CUSTODI (ID, DATA_CARICAMENTO, NOME_FILE, SIGLA, ANNO, MESE, COD_CUSTODE, IMPORTO,  ID_PARAM_UTENZA) VALUES ( " _
                                             & " SISCOM_MI.SEQ_PAGAMENTI_CUSTODI.NEXTVAL , " _
                                             & par.insDbValue(txtDataCaricamento.Text, True, True) & "," _
                                             & StrSql(sFileName) & "," _
                                             & StrSql(Sigla) & "  , " _
                                             & StrSql(annoC) & " , " _
                                             & StrSql(meseC) & "  ," _
                                             & StrSql(codCustode) & "  , " _
                                             & par.VirgoleInPunti(Importo) & ", " _
                                             & idParamUt & ")"
                            ocmd.ExecuteNonQuery()
                            hasInsert += 1
                        Catch oracle_ex As Oracle.DataAccess.Client.OracleException
                            If oracle_ex.Number = 1 Then
                                'Fattura gia presente
                                strGiaInserite += "\n - " & codCustode & " - " & meseC & "/" & annoC

                            Else
                                'altri errori
                                ScriviAnomaliaCustodi(idParamUt, sFileName, Me.txtDataCaricamento.Text, annoC, meseC, codCustode, oracle_ex.Message)
                                hasanomalie = True

                            End If

                        End Try

                    End If


                    'TESTATA
                    Sigla = ""
                    annoC = ""
                    meseC = ""
                    codCustode = ""
                    Importo = 0
                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / TotRighe
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()

                End While
                MyReader.Close()
            End Using
            connData.chiudi(True)
            If hasanomalie = False Then
                If String.IsNullOrEmpty(strGiaInserite) Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Pagamenti Custodi caricati!')", True)
                    RadNotificationNote.Text = "Sono stati inseriti " & hasInsert & " pagamenti custodi"
                    RadNotificationNote.AutoCloseDelay = "1000"
                    RadNotificationNote.Show()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Sono stati inseriti " & hasInsert & " pagamenti custodi.\nI restanti pagamenti erano già stati caricati');", True)

                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Sono stati inseriti " & hasInsert & " pagamenti custodi.Si sono verificate delle anomalie nella lettura del file!');window.open('CustodiAnomalie.aspx?NOME_FILE=" & sFileName & "');", True)


            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - ImportaCustodi - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)

        End Try
    End Sub

#End Region
    Function StrSql(ByVal sStr As String) As String
        Dim n As Integer
        Dim nPos, nLen
        Dim sTmp As String = ""
        Dim s As String = ""

        n = InStr(1, sStr, "'")
        If n > 0 Then
            nLen = Len(sStr)
            For nPos = 1 To nLen
                s = Mid(sStr, nPos, 1)
                If s = "'" Then
                    s = "'" + s
                End If
                sTmp = sTmp + s
            Next
        Else
            sTmp = sStr
        End If

        If IsNumeric(sTmp) Then
            sTmp = CStr(sTmp)
        End If

        sTmp = "'" + sTmp + "'"
        Return sTmp
    End Function
    Private Sub ScriviAnomalia(ByVal idParamUt As Integer, ByVal nomeFile As String, ByVal dataCaric As String, ByVal numFattura As String, ByVal annoFattura As String, ByVal note As String)
        par.cmd.CommandText = "insert into siscom_mi.fatture_utenze_anomalie (ID_PARAM_UTENZA,NOME_FILE,DATA_CARICAMENTO,NUMERO_FATTURA,ANNO_FATTURA,NOTE) values " _
                                & "( " & idParamUt & ", " _
                                & par.insDbValue(nomeFile, True, False) & ", " _
                                & par.insDbValue(dataCaric, True, True) & ", " _
                                & par.insDbValue(numFattura, True, False) & ", " _
                                & par.insDbValue(annoFattura, True, False) & ", " _
                                & par.insDbValue(note, True) & ")"
        par.cmd.ExecuteNonQuery()

    End Sub
    Private Sub ScriviAnomaliaCustodi(ByVal idParamUt As Integer, ByVal nomeFile As String, ByVal dataCaric As String, ByVal ANNO As String, ByVal MESE As String, ByVal CODCUSTODE As String, ByVal NOTE As String)
        par.cmd.CommandText = "insert into siscom_mi.pagamenti_custodi_anomalie (ID_PARAM_UTENZA,NOME_FILE,DATA_CARICAMENTO,ANNO,MESE,COD_CUSTODE,NOTE) values " _
                                & "( " & idParamUt & ", " _
                                & par.insDbValue(nomeFile, True, False) & ", " _
                                & par.insDbValue(dataCaric, True, True) & ", " _
                                & par.insDbValue(ANNO, True, False) & ", " _
                                & par.insDbValue(MESE, True, False) & ", " _
                                & par.insDbValue(CODCUSTODE, True) & ", " _
                                & par.insDbValue(NOTE, True) & ")"
        par.cmd.ExecuteNonQuery()

    End Sub

    Private Function Controlli() As Boolean
        Controlli = True
        Dim msg As String = ""
        If String.IsNullOrEmpty(Me.txtDataCaricamento.Text) Then
            msg += "\n- Definire la data caricamento;"
        End If

        If FileUpload1.HasFile = False Then
            msg += "\n- Selezionare un file da caricare;"
        End If
        If idParam.Value = 0 Then
            msg += "\n- Selezionare un tipo tracciato;"

        End If

        If Not String.IsNullOrEmpty(msg) Then
            Controlli = False

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "msgEroore", "alert('Impossibile procedere! " & msg & "')", True)
        End If
    End Function


    Function AggiustaData(ByVal sData As String) As String
        Dim sTmp As String = ""
        If sData.Length = 10 Then sTmp = sData.Substring(6, 4) & sData.Substring(3, 2) & sData.Substring(0, 2)
        Return sTmp
    End Function

    Private Sub ImportaMulte(ByVal sPath As String, ByVal sFileName As String, ByVal idParamMult As Integer)
        Try
            Dim Contatore As Integer = 0
            connData.apri(True)
            Dim myRow As String()
            Dim ocmd As Oracle.DataAccess.Client.OracleCommand
            Dim nRows As Int32 = 0
            ocmd = connData.Connessione.CreateCommand
            If Not sPath.EndsWith("\") Then
                sPath &= "\"
            End If
            Dim hasAnomalie As Boolean = False
            Dim hasInsert As Integer = 0
            Dim TotRighe As Integer = 0
            Response.Flush()
            Dim nrigaconta As Integer = 0

            'multe variables
            Dim codPatr As String = ""
            Dim idPatr As Integer = 0
            Dim RifInizio As String = ""
            Dim RifFine As String = ""
            Dim Note As String = ""
            Dim Importo As Double = 0
            Dim tipoPatr As String = ""


            Using MyReaderConta As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                MyReaderConta.TextFieldType = FileIO.FieldType.Delimited
                MyReaderConta.SetDelimiters(";")

                While Not MyReaderConta.EndOfData
                    If nrigaconta = 133 Then
                        Beep()
                    End If
                    If nrigaconta > 0 Then
                        nrigaconta += 1
                        myRow = MyReaderConta.ReadFields
                        TotRighe += 1
                    Else
                        myRow = MyReaderConta.ReadFields
                        nrigaconta += 1
                    End If

                End While
                MyReaderConta.Close()
            End Using
            nrigaconta = 0
            Dim rImported As Integer = 0
            Dim totImport As Double = 0
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                'Specify that reading from a comma-delimited file'
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")



                While Not MyReader.EndOfData
                    If nrigaconta = 0 Then
                        myRow = MyReader.ReadFields()
                        If Not myRow(0).ToString.ToUpper.Contains("PATRIMONIO") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 1 deve essere intitolata CODICE_PATRIMONIO!');", True)
                            Exit Sub
                        End If

                        If Not myRow(1).ToString.ToUpper.Contains("INIZIO") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 2 deve essere intitolata Periodo di riferimento inizio !');", True)
                            Exit Sub
                        End If
                        If Not myRow(2).ToString.ToUpper.Contains("FINE") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 3 deve essere intitolata Periodo di riferimento fine!');", True)
                            Exit Sub
                        End If
                        If Not myRow(3).ToString.ToUpper.Contains("NOTE") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 4 deve essere intitolata Note!');", True)
                            Exit Sub
                        End If
                        If Not myRow(4).ToString.ToUpper.Contains("IMPORTO") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 5 deve essere intitolata Importo!');", True)
                            Exit Sub
                        End If
                        nrigaconta += 1
                    End If
                    If nrigaconta > 0 Then

                        codPatr = ""
                        RifInizio = ""
                        RifFine = ""
                        Note = ""
                        Importo = 0
                        idPatr = 0
                        tipoPatr = ""

                        nRows += 1
                        myRow = MyReader.ReadFields()


                        codPatr = myRow(0)
                        RifInizio = myRow(1)
                        RifFine = myRow(2)
                        Note = myRow(3)
                        If myRow(4).ToString.Contains(".") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nAnomalia alla riga " & nrigaconta & "');", True)

                            Exit Sub
                        Else
                            If Not String.IsNullOrEmpty(myRow(4)) Then
                                Importo = Math.Round(CDec(myRow(4)), 2)
                            End If

                        End If



                        Select Case codPatr.Length

                            Case 7 'COMPLESSO
                                tipoPatr = " ID_COMPLESSO "

                                par.cmd.CommandText = "select id from siscom_mi.complessi_immobiliari where cod_complesso = '" & codPatr & "'"
                                idPatr = par.cmd.ExecuteScalar
                            Case 9 'EDIFICIO
                                tipoPatr = " ID_EDIFICIO "
                                par.cmd.CommandText = "select id from siscom_mi.edifici where cod_edificio = '" & codPatr & "'"
                                idPatr = par.cmd.ExecuteScalar

                            Case 17 'UNITA
                                tipoPatr = " ID_UNITA "
                                par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where cod_unita_immobiliare = '" & codPatr & "'"
                                idPatr = par.cmd.ExecuteScalar

                            Case Else
                                connData.chiudi(False)
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nCodice patrimonio non corretto!\nAnomalia alla riga " & nrigaconta & "');", True)
                                Exit Sub

                        End Select

                        RifInizio = RifInizio & "01"
                        RifFine = RifFine & DateTime.DaysInMonth(Mid(RifFine, 1, 4), Mid(RifFine, 5, 2))

                        Dim idPf As Integer = 0
                        Dim idstato = 0
                        Dim anno As Integer = 0
                        anno = RifInizio.Substring(0, 4)
                        While idstato <> 5

                            par.cmd.CommandText = "select pf_main.id,id_stato " _
                                                & "from siscom_mi.pf_main,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                                                & "where T_ESERCIZIO_FINANZIARIO.id = pf_main.ID_ESERCIZIO_FINANZIARIO " _
                                                & "and substr(T_ESERCIZIO_FINANZIARIO.inizio,1,4) = " & anno & ""
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                If lettore("id_stato") = 5 Then
                                    idstato = 5
                                    idPf = lettore("id")
                                Else
                                    anno = anno + 1
                                End If
                            End If
                            lettore.Close()

                        End While

                        If idPf <> 0 Then

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.MULTE (" _
                                                & "   ID, NOME_FILE, DATA_CARICAMENTO, " _
                                                & "   DATA_INIZIO_PERIODO, " _
                                                & "   DATA_FINE_PERIODO, IMPORTO, " _
                                                & "   ID_PARAM_UTENZA, COD_PATRIMONIO,NOTE, " & tipoPatr & ") " _
                                                & "VALUES (siscom_mi.seq_multe.nextval/* ID */," _
                                                & " " & par.insDbValue(sFileName, True) & "/* NOME_FILE */," _
                                                & " " & par.insDbValue(Format(Now, "yyyyMMdd"), True) & "/* DATA_CARICAMENTO */," _
                                                & " " & par.insDbValue(RifInizio, True) & "/* DATA_INIZIO_PERIODO */," _
                                                & " " & par.insDbValue(RifFine, True) & "/* DATA_FINE_PERIODO */," _
                                                & " " & par.insDbValue(Importo, False) & " /* IMPORTO */," _
                                                & " " & par.insDbValue(idParamMult, False) & "/* ID_PARAM_UTENZA */," _
                                                & " " & par.insDbValue(codPatr, True) & "/* COD_PATRIMONIO */," _
                                                & " " & par.insDbValue(Note, True) & "/* NOTE */," _
                                                & " " & par.insDbValue(idPatr, False) & "/* ID_UNITA ID_COMPLESSO ID_EDIFICIO */ )"
                            par.cmd.ExecuteNonQuery()
                            rImported += 1
                            totImport += Importo
                        Else

                            Exit While
                        End If
                        nrigaconta += 1
                        Contatore = Contatore + 1
                        percentuale = (Contatore * 100) / TotRighe
                        Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                        Response.Flush()
                    Else
                        myRow = MyReader.ReadFields()
                        nrigaconta += 1
                    End If


                End While
            End Using
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & rImported & " Pagamenti Multe caricati!');", True)



        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - ImportaMulte - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)

        End Try

    End Sub




    Private Sub ImportaCosap(ByVal sPath As String, ByVal sFileName As String, ByVal idParamCosap As Integer)
        Try
            Dim Contatore As Integer = 0
            connData.apri(True)
            Dim myRow As String()
            Dim ocmd As Oracle.DataAccess.Client.OracleCommand
            Dim nRows As Int32 = 0
            ocmd = connData.Connessione.CreateCommand
            If Not sPath.EndsWith("\") Then
                sPath &= "\"
            End If
            Dim hasAnomalie As Boolean = False
            Dim hasInsert As Integer = 0
            Dim TotRighe As Integer = 0
            Response.Flush()
            Dim nrigaconta As Integer = 0

            'multe variables
            Dim codPatr As String = ""
            Dim idPatr As Integer = 0
            Dim RifInizio As String = ""
            Dim RifFine As String = ""
            Dim Note As String = ""
            Dim Importo As Double = 0
            Dim tipoPatr As String = ""


            Using MyReaderConta As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                MyReaderConta.TextFieldType = FileIO.FieldType.Delimited
                MyReaderConta.SetDelimiters(";")

                While Not MyReaderConta.EndOfData
                    If nrigaconta = 82 Then
                        Beep()
                    End If
                    If nrigaconta > 0 Then
                        nrigaconta += 1

                        myRow = MyReaderConta.ReadFields
                        TotRighe += 1
                    Else
                        myRow = MyReaderConta.ReadFields
                        nrigaconta += 1
                    End If

                End While
                MyReaderConta.Close()
            End Using
            nrigaconta = 0
            Dim rImported As Integer = 0
            Dim totImport As Double = 0
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sPath & sFileName)
                'Specify that reading from a comma-delimited file'
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")



                While Not MyReader.EndOfData
                    If nrigaconta = 0 Then
                        myRow = MyReader.ReadFields()
                        If Not myRow(0).ToString.ToUpper.Contains("PATRIMONIO") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 1 deve essere intitolata CODICE_PATRIMONIO!');", True)
                            Exit Sub
                        End If

                        If Not myRow(1).ToString.ToUpper.Contains("INIZIO") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 2 deve essere intitolata Periodo di riferimento inizio !');", True)
                            Exit Sub
                        End If
                        If Not myRow(2).ToString.ToUpper.Contains("FINE") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 3 deve essere intitolata Periodo di riferimento fine!');", True)
                            Exit Sub
                        End If
                        If Not myRow(3).ToString.ToUpper.Contains("NOTE") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 4 deve essere intitolata Note!');", True)
                            Exit Sub
                        End If
                        If Not myRow(4).ToString.ToUpper.Contains("IMPORTO") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nIl file non corrisponde a quello previsto dalle specifiche!\nLa colonna 5 deve essere intitolata Importo!');", True)
                            Exit Sub
                        End If
                        nrigaconta += 1
                    End If
                    If nrigaconta > 0 Then

                        codPatr = ""
                        RifInizio = ""
                        RifFine = ""
                        Note = ""
                        Importo = 0
                        idPatr = 0
                        tipoPatr = ""

                        nRows += 1
                        myRow = MyReader.ReadFields()


                        codPatr = myRow(0)
                        RifInizio = myRow(1)
                        RifFine = myRow(2)
                        Note = myRow(3)
                        If myRow(4).ToString.Contains(".") Then
                            connData.chiudi(False)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nAnomalia alla riga " & nrigaconta & "');", True)

                            Exit Sub
                        Else
                            If Not String.IsNullOrEmpty(myRow(4)) Then
                                Importo = Math.Round(CDec(myRow(4)), 2)
                            End If

                        End If



                        Select Case codPatr.Length

                            Case 7 'COMPLESSO
                                tipoPatr = " ID_COMPLESSO "

                                par.cmd.CommandText = "select id from siscom_mi.complessi_immobiliari where cod_complesso = '" & codPatr & "'"
                                idPatr = par.cmd.ExecuteScalar
                            Case 9 'EDIFICIO
                                tipoPatr = " ID_EDIFICIO "
                                par.cmd.CommandText = "select id from siscom_mi.edifici where cod_edificio = '" & codPatr & "'"
                                idPatr = par.cmd.ExecuteScalar

                            Case 17 'UNITA
                                tipoPatr = " ID_UNITA "
                                par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where cod_unita_immobiliare = '" & codPatr & "'"
                                idPatr = par.cmd.ExecuteScalar

                            Case Else
                                connData.chiudi(False)
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Operazione interrotta!\nCodice patrimonio non corretto!\nAnomalia alla riga " & nrigaconta & "');", True)
                                Exit Sub

                        End Select

                        RifInizio = RifInizio & "01"
                        RifFine = RifFine & DateTime.DaysInMonth(Mid(RifFine, 1, 4), Mid(RifFine, 5, 2))

                        Dim idPf As Integer = 0
                        Dim idstato = 0
                        Dim anno As Integer = 0
                        anno = RifInizio.Substring(0, 4)
                        While idstato <> 5

                            par.cmd.CommandText = "select pf_main.id,id_stato " _
                                                & "from siscom_mi.pf_main,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                                                & "where T_ESERCIZIO_FINANZIARIO.id = pf_main.ID_ESERCIZIO_FINANZIARIO " _
                                                & "and substr(T_ESERCIZIO_FINANZIARIO.inizio,1,4) = " & anno & ""
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                If lettore("id_stato") = 5 Then
                                    idstato = 5
                                    idPf = lettore("id")
                                Else
                                    anno = anno + 1
                                End If
                            End If
                            lettore.Close()

                        End While

                        If idPf <> 0 Then

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COSAP (" _
                                                & "   ID, NOME_FILE, DATA_CARICAMENTO, " _
                                                & "   DATA_INIZIO_PERIODO, " _
                                                & "   DATA_FINE_PERIODO, IMPORTO, " _
                                                & "   ID_PARAM_UTENZA, COD_PATRIMONIO,NOTE, " & tipoPatr & ") " _
                                                & "VALUES (siscom_mi.SEQ_COSAP.nextval/* ID */," _
                                                & " " & par.insDbValue(sFileName, True) & "/* NOME_FILE */," _
                                                & " " & par.insDbValue(Format(Now, "yyyyMMdd"), True) & "/* DATA_CARICAMENTO */," _
                                                & " " & par.insDbValue(RifInizio, True) & "/* DATA_INIZIO_PERIODO */," _
                                                & " " & par.insDbValue(RifFine, True) & "/* DATA_FINE_PERIODO */," _
                                                & " " & par.insDbValue(Importo, False) & " /* IMPORTO */," _
                                                & " " & par.insDbValue(idParamCosap, False) & "/* ID_PARAM_UTENZA */," _
                                                & " " & par.insDbValue(codPatr, True) & "/* COD_PATRIMONIO */," _
                                                & " " & par.insDbValue(Note, True) & "/* NOTE */," _
                                                & " " & par.insDbValue(idPatr, False) & "/* ID_UNITA ID_COMPLESSO ID_EDIFICIO */ )"
                            par.cmd.ExecuteNonQuery()
                            rImported += 1
                            totImport += Importo
                        Else

                            Exit While
                        End If
                        nrigaconta += 1
                        Contatore = Contatore + 1
                        percentuale = (Contatore * 100) / TotRighe
                        Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                        Response.Flush()
                    Else
                        myRow = MyReader.ReadFields()
                        nrigaconta += 1
                    End If


                End While
            End Using
            connData.chiudi(True)
            RadWindowManager1.RadAlert(rImported & " Pagamenti Cosap caricati!", 300, 150, "Attenzione", "", "null")



        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Fatture Utenze - ImportaMulte - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub RadButtonCarica_Click(sender As Object, e As System.EventArgs) Handles RadButtonCarica.Click
        Dim nFile As String = ""
        nFile = Server.MapPath("..\..\..\FileTemp")
        If Controlli() = True Then
            nFile = nFile & "\" & FileUpload1.FileName
            FileUpload1.SaveAs(nFile)
            Select Case idTipoUtenza.Value
                Case 0 '<--gas
                    ImportaA2A(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 1, 6 '<--elettrico
                    ImportaA2A(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 2 '<--calore
                    ImportaA2A(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 3 '<--acqua
                    ImportaH20(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 4 '<--custodi
                    ImportaCustodi(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 5
                    connData.apri(False)
                    par.cmd.CommandText = "delete from siscom_mi.multe where id_prenotazione is null"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(False)
                    ImportaMulte(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 7
                    connData.apri(False)
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COSAP WHERE ID_PRENOTAZIONE IS NULL"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(False)
                    ImportaCosap(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
            End Select
        End If
        Me.idParam.Value = 0
        Me.idPiano.Value = 0
        Me.idTipoUtenza.Value = 0
        Me.idFornitore.Value = 0
        Me.idVocePf.Value = 0
        Me.idVocePfImporto.Value = 0
        Me.idStruttura.Value = 0

        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
    Protected Sub RadButtonEsci_Click(sender As Object, e As System.EventArgs) Handles RadButtonEsci.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub dgvTipoUtenze_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvTipoUtenze.NeedDataSource
        Try
            Dim condTipoUtenza As String = ""
            Select Case idTipo.Value
                Case "C"
                    condTipoUtenza = " And pagamenti_utenze_voci.id_tipo_utenza = 4"
                Case "U"
                    condTipoUtenza = " And (pagamenti_utenze_voci.id_tipo_utenza < 4 Or pagamenti_utenze_voci.id_tipo_utenza = 6)"
                Case "M"
                    condTipoUtenza = " And pagamenti_utenze_voci.id_tipo_utenza = 5"
                Case "COSAP"
                    condTipoUtenza = " And pagamenti_utenze_voci.id_tipo_utenza = 7"
            End Select
            Dim condesercizio As String = ""
            If Me.cmbEsercizio.SelectedValue <> "-1" Then
                condesercizio = " and pagamenti_utenze_voci.id_piano_finanziario = " & Me.cmbEsercizio.SelectedValue
            End If
            par.cmd.CommandText = "SELECT pagamenti_utenze_voci.id,PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO, ID_TIPO_UTENZA, " _
                                & " ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_FORNITORE, id_struttura,(SELECT Getdata(inizio) ||' - '|| Getdata(fine) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID =(SELECT id_esercizio_finanziario FROM SISCOM_MI.PF_MAIN WHERE ID = pagamenti_utenze_voci.id_piano_finanziario)) AS pf , " _
                                & "TIPO_UTENZE.descrizione AS TIPO_UTENZE,(PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) AS VOCE_PIANO,PF_VOCI_IMPORTO.descrizione AS voce_bp,FORNITORI.ragione_sociale AS fornitore,tab_filiali.nome as struttura ,decode(fl_attivo,0,'NO',1,'SI') AS ATTIVO " _
                                & "FROM siscom_mi.PAGAMENTI_UTENZE_VOCI,siscom_mi.TIPO_UTENZE,siscom_mi.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI,siscom_mi.FORNITORI,siscom_mi.tab_filiali " _
                                & "WHERE TIPO_UTENZE.ID = id_tipo_utenza " _
                                & "AND  id_voce_pf_importo = PF_VOCI_IMPORTO.ID(+) AND id_voce_pf = PF_VOCI.ID and PAGAMENTI_UTENZE_VOCI.fl_attivo = 1 AND FORNITORI.ID = id_fornitore and tab_filiali.id = id_struttura " & condTipoUtenza & condesercizio
            Me.idParam.Value = 0
            Me.idPiano.Value = 0
            Me.idTipoUtenza.Value = 0
            Me.idFornitore.Value = 0
            Me.idVocePf.Value = 0
            Me.idVocePfImporto.Value = 0
            Me.idStruttura.Value = 0
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            NumeroElementi = dt.Rows.Count
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " dgvTipoUtenze_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbEsercizio.SelectedIndexChanged
        dgvTipoUtenze.Rebind()
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvTipoUtenze.AllowPaging = False
        dgvTipoUtenze.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvTipoUtenze.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvTipoUtenze.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvTipoUtenze.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvTipoUtenze.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvTipoUtenze.AllowPaging = True
        dgvTipoUtenze.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvTipoUtenze.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CUSTODI", "CUSTODI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvTipoUtenze.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And dgvTipoUtenze.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub
End Class
