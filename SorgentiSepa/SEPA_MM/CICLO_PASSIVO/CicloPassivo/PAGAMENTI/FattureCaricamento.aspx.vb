
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCaricamento
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


        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso...' ><br>Elaborazione in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var scarica; scarica = ''; var testo; testo = ''; var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)

        If Not IsPostBack Then
            Me.txtDataCaricamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            If Request.QueryString("TIPO") = "C" Then
                idTipo.Value = "C"
                Me.lblTitolo.Text = "Carica Pagamenti Custodi"
                Me.btnCarica.Text = "CARICA CUSTODI"
            Else
                idTipo.Value = "U"
                Me.lblTitolo.Text = "Carica Fatture Utenze"
            End If

            CaricaEsercizio()
            Me.txtDataCaricamento.Text = Format(Now, "dd/MM/yyyy")
        End If

    End Sub
    Private Sub CaricaEsercizio()

        Dim sql As String = ""
        sql = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY')||' - '||TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY')||' '||SISCOM_MI.PF_STATI.DESCRIZIONE as descrizione " _
                    & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                    & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                    & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                    & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by id desc"
        par.caricaComboBox(sql, cmbEsercizio, "ID", "descrizione", True)

        par.caricaComboBox("select cod_fornitore||' - '||ragione_sociale as descrizione,id  from siscom_mi.fornitori order by fornitori.ragione_sociale asc", cmbFornitore, "id", "descrizione", True)
        par.caricaComboBox("select id,nome from siscom_mi.tab_filiali order by nome asc", Me.cmbStruttra, "ID", "NOME", True)

    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        If Me.cmbEsercizio.SelectedValue <> -1 Then
            par.caricaComboBox("select id,codice||' - '||descrizione as descrizione from siscom_mi.pf_voci where id_voce_madre is not null and id_piano_finanziario =" & Me.cmbEsercizio.SelectedValue & " order by codice asc", cmbPfVoci, "id", "descrizione", True)
            par.caricaComboBox("SELECT * FROM siscom_mi.TAB_SERVIZI WHERE ID IN (SELECT id_servizio FROM siscom_mi.TAB_SERVIZI_VOCI WHERE id_voce IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE  id_piano_finanziario =" & Me.cmbEsercizio.SelectedValue & ")) ", cmbServizio, "ID", "descrizione", True)
            par.caricaComboBox("select id,descrizione from siscom_mi.TIPO_UTENZE order by descrizione asc", cmbTipoTracciato, "id", "descrizione", True)
            If idTipo.Value = "C" Then
                Me.cmbTipoTracciato.SelectedValue = "4"
                SettaValDefault()
                Me.cmbTipoTracciato.Enabled = False
            End If
        Else
            Me.cmbTipoTracciato.Items.Clear()
            Me.cmbPfVoci.Items.Clear()
            Me.cmbServizio.Items.Clear()
            Me.cmbPfVociImporto.Items.Clear()
        End If
    End Sub
    Private Sub CaricaPfVociImporto()
        If Me.cmbPfVoci.SelectedValue <> "-1" And Me.cmbServizio.SelectedValue <> "-1" Then
            par.caricaComboBox("SELECT ID,descrizione FROM siscom_mi.PF_VOCI_IMPORTO WHERE id_voce =" & Me.cmbPfVoci.SelectedValue & "  AND id_servizio =" & Me.cmbServizio.SelectedValue & " AND id_lotto = (SELECT ID FROM siscom_mi.LOTTI WHERE id_filiale = " & Session.Item("ID_STRUTTURA") & " AND id_esercizio_finanziario = (SELECT ID_ESERCIZIO_FINANZIARIO FROM siscom_mi.PF_MAIN WHERE ID = " & Me.cmbEsercizio.SelectedValue & ")) order by descrizione asc", cmbPfVociImporto, "ID", "DESCRIZIONE", True)
        Else
            Me.cmbPfVociImporto.Items.Clear()
        End If

    End Sub
    Protected Sub cmbServizio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged
        CaricaPfVociImporto()
    End Sub

    Protected Sub cmbPfVoci_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPfVoci.SelectedIndexChanged
        CaricaPfVociImporto()
    End Sub

    Protected Sub cmbTipoTracciato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoTracciato.SelectedIndexChanged
        SettaValDefault()
    End Sub
    Private Sub SettaValDefault()
        Try
            If Me.cmbEsercizio.SelectedValue <> "-1" Then

                connData.apri(False)
                Dim idVocePfImporto As Integer = -1
                par.cmd.CommandText = "select * from siscom_mi.PAGAMENTI_UTENZE_VOCI where id_piano_finanziario = " & Me.cmbEsercizio.SelectedValue & " and id_tipo_utenza = " & Me.cmbTipoTracciato.SelectedValue
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    Me.cmbFornitore.SelectedValue = par.IfNull(lettore("id_fornitore"), -1)
                    Me.cmbPfVoci.SelectedValue = par.IfNull(lettore("ID_VOCE_PF"), -1)
                    Me.cmbStruttra.SelectedValue = par.IfNull(lettore("ID_STRUTTURA"), -1)
                    idVocePfImporto = par.IfNull(lettore("ID_VOCE_PF_IMPORTO"), -1)
                    Me.idParam.Value = par.IfNull(lettore("id"), 0)
                Else
                    Me.cmbFornitore.SelectedValue = -1
                    Me.cmbPfVoci.SelectedValue = -1
                    Me.cmbStruttra.SelectedValue = -1
                    If Me.cmbServizio.SelectedValue <> -1 Then
                        Me.cmbServizio.SelectedValue = -1
                    End If
                    If par.IfEmpty(Me.cmbPfVociImporto.SelectedValue, -1) <> -1 Then

                        Me.cmbPfVociImporto.SelectedValue = -1
                    End If

                    idVocePfImporto = -1
                End If
                lettore.Close()
                If idVocePfImporto > 0 Then
                    par.cmd.CommandText = "select id_servizio from siscom_mi.pf_voci_importo where id = " & idVocePfImporto
                    Me.cmbServizio.SelectedValue = par.IfNull(par.cmd.ExecuteScalar, -1)
                End If

                If Me.cmbServizio.SelectedValue <> -1 Then
                    CaricaPfVociImporto()
                    If Me.cmbPfVociImporto.Items.Count > 0 Then
                        If Not IsNothing(Me.cmbPfVociImporto.Items.FindByValue(idVocePfImporto)) Then
                            Me.cmbPfVociImporto.SelectedValue = idVocePfImporto
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('La voce servizio definita nei parametri non è leggata all\'ufficio dell\'operatore collegato!')", True)

                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

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

            Try
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
            Catch ex As Exception
            End Try
            MyReader.Close()
        End Using
        connData.chiudi(True)
        If hasanomalie = False Then
            If String.IsNullOrEmpty(strGiaInserite) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate !')", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate.\nLe seguenti sono state escluse perchè già presenti:" & strGiaInserite & "')", True)

            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Si sono verificate delle anomalie nella lettura del file!');window.open('FattureCaricAnomlie.aspx?NOME_FILE=" & sFileName & "');", True)

        End If

    End Sub
    Private Sub ImportaH20(ByVal sPath As String, ByVal sFileName As String, ByVal idParamUt As Integer)
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
            Try
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
            Catch ex As Exception

                'pagina errore
            End Try
            MyReader.Close()
        End Using
        connData.chiudi(True)
        If hasAnomalie = False Then
            If String.IsNullOrEmpty(strGiaInserite) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate !')", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Fatture caricate.\nLe seguenti sono state escluse perchè già presenti:" & strGiaInserite & "')", True)

            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Si sono verificate delle anomalie nella lettura del file!');window.open('FattureCaricAnomlie.aspx?NOME_FILE=" & sFileName & "');", True)

        End If

    End Sub

    Private Sub ImportaCustodi(ByVal sPath As String, ByVal sFileName As String, ByVal idParamUt As Integer)
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

            Try
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
            Catch ex As Exception
            End Try
            MyReader.Close()
        End Using
        connData.chiudi(True)
        If hasanomalie = False Then
            If String.IsNullOrEmpty(strGiaInserite) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Pagamenti Custodi caricati!')", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('" & hasInsert & " Pagamenti Custodi caricati!\nI seguenti sono state esclusi perchè già presenti:" & strGiaInserite & "')", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Si sono verificate delle anomalie nella lettura del file!');window.open('CustodiAnomalie.aspx?NOME_FILE=" & sFileName & "');", True)

        End If

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
        par.cmd.CommandText = "insert into siscom_mi.pagamenti_utenze_anomalie (ID_PARAM_UTENZA,NOME_FILE,DATA_CARICAMENTO,ANNO,MESE,COD_CUSTODE,NOTE) values " _
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
        If Me.cmbStruttra.SelectedValue = -1 Then
            msg += "\n- Scegliere la struttura;"

        End If
        If Me.cmbEsercizio.SelectedValue = -1 Then
            msg += "\n- Scegliere il piano finanziario;"

        End If
        If Me.cmbTipoTracciato.SelectedValue = -1 Then
            msg += "\n- Scegliere il tipo tracciato;"

        End If
        If Me.cmbFornitore.SelectedValue = -1 Then
            msg += "\n- Scegliere il fornitore;"

        End If
        If par.IfEmpty(Me.cmbPfVoci.SelectedValue, -1) = -1 Then
            msg += "\n- Scegliere la voce del B.P.;"
        End If
        If par.IfEmpty(Me.cmbServizio.SelectedValue, -1) = -1 Then
            msg += "\n- Scegliere il servizio;"
        End If
        If par.IfEmpty(Me.cmbPfVociImporto.SelectedValue, -1) = -1 Then
            msg += "\n- Scegliere la voce servizio;"
        End If
        If String.IsNullOrEmpty(Me.txtDataCaricamento.Text) Then
            msg += "\n- Definire la data caricamento;"
        End If

        If FileUpload1.HasFile = False Then
            msg += "\n- Selezionare un file da caricare;"
        End If
        If Not String.IsNullOrEmpty(msg) Then
            Controlli = False

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Impossibile procedere! " & msg & "')", True)
        End If

    End Function

    Protected Sub btnCarica_Click(sender As Object, e As System.EventArgs) Handles btnCarica.Click
        Dim nFile As String = ""
        nFile = Server.MapPath("..\..\..\FileTemp")

        If Controlli() = True Then
            nFile = nFile & "\" & FileUpload1.FileName
            FileUpload1.SaveAs(nFile)

            Select Case cmbTipoTracciato.SelectedValue
                Case 1 '<--elettrico
                    ImportaA2A(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)

                Case 2 '<--gas
                    ImportaA2A(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)


                Case 3 '<--acqua
                    ImportaH20(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
                Case 4 '<--custodi
                    ImportaCustodi(Server.MapPath("..\..\..\FileTemp"), FileUpload1.FileName, idParam.Value)
            End Select

        End If

    End Sub

    Function AggiustaData(ByVal sData As String) As String
        Dim sTmp As String = ""
        If sData.Length = 10 Then sTmp = sData.Substring(6, 4) & sData.Substring(3, 2) & sData.Substring(0, 2)
        Return sTmp
    End Function

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")

    End Sub
End Class
