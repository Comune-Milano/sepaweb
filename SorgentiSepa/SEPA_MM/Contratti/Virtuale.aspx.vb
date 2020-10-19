
Partial Class Contratti_Virtuale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If IsPostBack = False Then
            Try
                Response.Flush()

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.RiempiDList(Page, par.OracleConn, "cmbTipoViaCor", "select * from siscom_mi.tipologia_indirizzo order by descrizione asc", "DESCRIZIONE", "COD")
                cmbTipoViaCor.SelectedIndex = -1
                cmbTipoViaCor.Items.FindByText("VIA").Selected = True

                par.RiempiDList(Page, par.OracleConn, "cmbTipoViaResidenza", "select * from siscom_mi.tipologia_indirizzo order by descrizione asc", "DESCRIZIONE", "COD")
                cmbTipoViaResidenza.SelectedIndex = -1
                cmbTipoViaResidenza.Items.FindByText("VIA").Selected = True

                par.RiempiDList(Page, par.OracleConn, "cmbTipoViaUnita", "select * from siscom_mi.tipologia_indirizzo order by descrizione asc", "DESCRIZIONE", "COD")
                cmbTipoViaUnita.SelectedIndex = -1
                cmbTipoViaUnita.Items.FindByText("VIA").Selected = True

                Dim lsiFrutto As ListItem
                lsiFrutto = New ListItem("--", "-1")
                cmbVoce1.Items.Add(lsiFrutto)
                cmbVoce2.Items.Add(lsiFrutto)
                cmbVoce3.Items.Add(lsiFrutto)
                cmbVoce4.Items.Add(lsiFrutto)
                cmbVoce5.Items.Add(lsiFrutto)
                cmbVoce6.Items.Add(lsiFrutto)
                cmbVoce7.Items.Add(lsiFrutto)
                cmbVoce8.Items.Add(lsiFrutto)
                cmbVoce9.Items.Add(lsiFrutto)
                cmbVoce10.Items.Add(lsiFrutto)
                cmbVoce11.Items.Add(lsiFrutto)
                cmbVoce12.Items.Add(lsiFrutto)
                cmbVoce13.Items.Add(lsiFrutto)
                cmbVoce14.Items.Add(lsiFrutto)
                cmbVoce15.Items.Add(lsiFrutto)
                cmbVoce16.Items.Add(lsiFrutto)
                cmbVoce17.Items.Add(lsiFrutto)
                cmbVoce18.Items.Add(lsiFrutto)
                cmbVoce19.Items.Add(lsiFrutto)
                cmbVoce20.Items.Add(lsiFrutto)




                par.cmd.CommandText = "select * from siscom_mi.t_voci_bolletta WHERE ID<>95 and id<>407 AND SELEZIONABILE=1  order by descrizione asc"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReaderA.Read
                    lsiFrutto = New ListItem(myReaderA("DESCRIZIONE"), myReaderA("ID"))
                    cmbVoce1.Items.Add(lsiFrutto)
                    cmbVoce2.Items.Add(lsiFrutto)
                    cmbVoce3.Items.Add(lsiFrutto)
                    cmbVoce4.Items.Add(lsiFrutto)
                    cmbVoce5.Items.Add(lsiFrutto)
                    cmbVoce6.Items.Add(lsiFrutto)
                    cmbVoce7.Items.Add(lsiFrutto)
                    cmbVoce8.Items.Add(lsiFrutto)
                    cmbVoce9.Items.Add(lsiFrutto)
                    cmbVoce10.Items.Add(lsiFrutto)
                    cmbVoce11.Items.Add(lsiFrutto)
                    cmbVoce12.Items.Add(lsiFrutto)
                    cmbVoce13.Items.Add(lsiFrutto)
                    cmbVoce14.Items.Add(lsiFrutto)
                    cmbVoce15.Items.Add(lsiFrutto)
                    cmbVoce16.Items.Add(lsiFrutto)
                    cmbVoce17.Items.Add(lsiFrutto)
                    cmbVoce18.Items.Add(lsiFrutto)
                    cmbVoce19.Items.Add(lsiFrutto)
                    cmbVoce20.Items.Add(lsiFrutto)
                Loop
                myReaderA.Close()


                If Month(Now) >= 1 And Month(Now) <= 2 Then
                    Prossimo_Periodo = Year(Now) & "03"
                End If

                If Month(Now) >= 3 And Month(Now) <= 4 Then
                    Prossimo_Periodo = Year(Now) & "05"
                End If

                If Month(Now) >= 5 And Month(Now) <= 6 Then
                    Prossimo_Periodo = Year(Now) & "07"
                End If

                If Month(Now) >= 7 And Month(Now) <= 8 Then
                    Prossimo_Periodo = Year(Now) & "09"
                End If

                If Month(Now) >= 9 And Month(Now) <= 10 Then
                    Prossimo_Periodo = Year(Now) & "11"
                End If

                If Month(Now) >= 11 And Month(Now) <= 12 Then
                    Prossimo_Periodo = Year(Now) + 1 & "01"
                End If
                'Label2235.Text = "Prossima Bollettazione Massiva: " & par.ConvertiMese(CInt(Mid(Prossimo_Periodo, 5, 2))) & "/" & Mid(Prossimo_Periodo, 1, 4)


                ListaArretrati.Items.Add(New ListItem("Ottobre 2009", "200910"))
                ListaArretrati.Items.Add(New ListItem("Novembre 2009", "200911"))
                ListaArretrati.Items.Add(New ListItem("Dicembre 2009", "200912"))

                Dim ANNO As Long = 2010
                Dim MESI As Long = 0
                Dim I As Long = 1
                Dim MESE_DA_SCRIVERE As Long = 1

                'MESI = DateDiff(DateInterval.Month, CDate("01/01/2010"), CDate(par.FormattaData(Prossimo_Periodo & "01")))
                'For I = 1 To MESI
                '    If I > 12 Then
                '        ANNO = ANNO + 1
                '        MESE_DA_SCRIVERE = 1
                '    End If

                '    ListaArretrati.Items.Add(New ListItem(par.ConvertiMese(MESE_DA_SCRIVERE) & " " & ANNO, ANNO & Format(MESE_DA_SCRIVERE, "00")))
                '    MESE_DA_SCRIVERE = MESE_DA_SCRIVERE + 1
                'Next



                Dim dataProssimoPeriodo As String = ""
                Dim numerorata As Integer = par.ProssimaRata(12, Format(Now, "yyyyMMdd"), dataProssimoPeriodo)



                par.cmd.CommandText = "select * from siscom_mi.BOL_BOLLETTE WHERE id>0 and N_RATA<>99 AND N_RATA<>999 AND N_RATA<>99999 and rif_file<>'REC' ORDER BY ID DESC"
                Dim myReaderW1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderW1.Read = True Then
                    If par.IfNull(myReaderW1("ANNO"), 0) & Format(par.IfNull(myReaderW1("N_RATA"), 0), "00") & "01" >= dataProssimoPeriodo Then
                        Select Case Format(par.IfNull(myReaderW1("N_RATA"), 0), "00")
                            Case "01", "03", "05", "07", "08", "10", "12"
                                numerorata = par.ProssimaRata(12, par.IfNull(myReaderW1("ANNO"), 0) & Format(par.IfNull(myReaderW1("N_RATA"), 0), "00") & "31", dataProssimoPeriodo)
                            Case "02"
                                numerorata = par.ProssimaRata(12, par.IfNull(myReaderW1("ANNO"), 0) & Format(par.IfNull(myReaderW1("N_RATA"), 0), "00") & "28", dataProssimoPeriodo)
                            Case Else
                                numerorata = par.ProssimaRata(12, par.IfNull(myReaderW1("ANNO"), 0) & Format(par.IfNull(myReaderW1("N_RATA"), 0), "00") & "30", dataProssimoPeriodo)
                        End Select
                    End If
                End If
                myReaderW1.Close()

                Prossimo_Periodo = Mid(dataProssimoPeriodo, 1, 6)

                MESI = DateDiff(DateInterval.Month, CDate("01/01/2010"), CDate(par.FormattaData(Prossimo_Periodo & "01")))
                For I = 1 To MESI
                    If I > 12 Then
                        If ANNO <> Year(Now) Then
                            ANNO = ANNO + 1
                            MESE_DA_SCRIVERE = 1
                        End If
                    End If

                    ListaArretrati.Items.Add(New ListItem(par.ConvertiMese(MESE_DA_SCRIVERE) & " " & ANNO, ANNO & Format(MESE_DA_SCRIVERE, "00")))
                    MESE_DA_SCRIVERE = MESE_DA_SCRIVERE + 1
                Next



                Label2235.Text = "Prossima Bollettazione Massiva: " & par.ConvertiMese(CInt(Mid(dataProssimoPeriodo, 5, 2))) & "/" & Mid(dataProssimoPeriodo, 1, 4)

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message
                lblErrore.Visible = True

            End Try

        End If

        txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtRS.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtpiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtComuneResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtComuneSpedizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtComuneUnita.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

        txtIndirizzoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtIndirizzoSpedizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtIndirizzoUnita.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

        txtCivicoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtCivicoSpedizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtCivicoUnita.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub

    Public Property id_anagrafica() As Long
        Get
            If Not (ViewState("par_id_anagrafica") Is Nothing) Then
                Return CLng(ViewState("par_id_anagrafica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id_anagrafica") = value
        End Set
    End Property



    Public Property Prossimo_Periodo() As String
        Get
            If Not (ViewState("par_Prossimo_Periodo") Is Nothing) Then
                Return CStr(ViewState("par_Prossimo_Periodo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Prossimo_Periodo") = value
        End Set
    End Property

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If HiddenField1.Value = "1" Then
            Try
                'Dim id_anagrafica As Long = 0


                lblErrore.Text = ""

                If CMBTIPORAPPORTO.SELECTEDITEM.VALUE = "1" And (TXTCOGNOME.TEXT = "" Or TXTNOME.TEXT = "") Then
                    Response.Write("<script>alert('Inserire Cognome e Nome per i rapporti di tipo ERP!');</script>")
                    Exit Sub
                End If

                If txtpiva.TEXT = "" Then
                    If txtCognome.Text = "" Or txtNome.Text = "" Then
                        Response.Write("<script>alert('Inserire Cognome e Nome!');</script>")
                        Exit Sub
                    End If
                    If Len(txtCF.Text) <> 16 Then
                        Response.Write("<script>alert('Codice Fiscale non valido!');</script>")
                        Exit Sub
                    End If
                    If par.ControllaCF(txtCF.Text) = False Then
                        Response.Write("<script>alert('Codice Fiscale non valido!');</script>")
                        Exit Sub
                    End If
                    If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
                        Response.Write("<script>alert('Codice Fiscale non valido!');</script>")
                        Exit Sub
                    End If
                Else
                    If TXTRS.TEXT <> "" Then

                    Else
                        Response.Write("<script>alert('Inserire la ragione Sociale!');</script>")
                        Exit Sub
                    End If

                End If
                If txtComuneResidenza.Text = "" Or txtPrResidenza.Text = "" Or txtIndirizzoResidenza.Text = "" Or txtCivicoResidenza.Text = "" Or txtCapResidenza.Text = "" Then
                    Response.Write("<script>alert('Inserire i valori relativi all\'indirizzo di residenza!');</script>")
                    Exit Sub
                End If


                If txtComuneUnita.Text = "" Or txtPrUnita.Text = "" Or txtIndirizzoUnita.Text = "" Or txtCivicoUnita.Text = "" Or txtCapUnita.Text = "" Then
                    Response.Write("<script>alert('Inserire i valori relativi all\'indirizzo dell\' Unità!');</script>")
                    Exit Sub
                End If

                If txtComuneSpedizione.Text = "" Or txtPrSpedizione.Text = "" Or txtIndirizzoSpedizione.Text = "" Or txtCivicoSpedizione.Text = "" Or txtCapSpedizione.Text = "" Then
                    Response.Write("<script>alert('Inserire i valori relativi all\'indirizzo di spedizione!');</script>")
                    Exit Sub
                End If

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                Dim CODICE_COMUNE_UNITA As String = ""
                Dim CODICE_COMUNE_RESIDENZA As String = ""
                Dim CODICE_COMUNE_SPEDIZIONE As String = ""
                Dim BOLLO As Double = 0
                Dim APPLICABOLLO As Double = 0
                Dim SPESEmav As Double = 0

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & UCase(par.PulisciStrSql(txtComuneUnita.Text)) & "'"
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    CODICE_COMUNE_UNITA = par.IfNull(myReaderX("COD"), "")
                End If
                myReaderX.Close()


                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
                myReaderX = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    BOLLO = CDbl(par.PuntiInVirgole(myReaderX("VALORE")))
                End If
                myReaderX.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
                myReaderX = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderX("VALORE")))
                End If
                myReaderX.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
                myReaderX = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    SPESEmav = CDbl(par.PuntiInVirgole(myReaderX("VALORE")))
                End If
                myReaderX.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & UCase(par.PulisciStrSql(txtComuneResidenza.Text)) & "'"
                myReaderX = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    CODICE_COMUNE_RESIDENZA = par.IfNull(myReaderX("COD"), "")
                End If
                myReaderX.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & UCase(par.PulisciStrSql(txtComuneSpedizione.Text)) & "'"
                myReaderX = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    CODICE_COMUNE_SPEDIZIONE = par.IfNull(myReaderX("COD"), "")
                End If
                myReaderX.Close()

                Dim CITTADINANZA As String = ""

                If CODICE_COMUNE_SPEDIZIONE <> "" And CODICE_COMUNE_RESIDENZA <> "" And CODICE_COMUNE_UNITA <> "" Then



                    Dim miaData As String = ""

                    If txtCF.TEXT <> "" Then

                        If Val(Mid(txtCF.Text, 10, 2)) > 40 Then
                            miaData = Format(Val(Mid(txtCF.Text, 10, 2)) - 40, "00")
                        Else
                            miaData = Mid(txtCF.Text, 10, 2)
                        End If

                        Select Case Mid(txtCF.Text, 9, 1)
                            Case "A"
                                miaData = miaData & "/01"
                            Case "B"
                                miaData = miaData & "/02"
                            Case "C"
                                miaData = miaData & "/03"
                            Case "D"
                                miaData = miaData & "/04"
                            Case "E"
                                miaData = miaData & "/05"
                            Case "H"
                                miaData = miaData & "/06"
                            Case "L"
                                miaData = miaData & "/07"
                            Case "M"
                                miaData = miaData & "/08"
                            Case "P"
                                miaData = miaData & "/09"
                            Case "R"
                                miaData = miaData & "/10"
                            Case "S"
                                miaData = miaData & "/11"
                            Case "T"
                                miaData = miaData & "/12"
                        End Select

                        'If Mid(txtCF.Text, 7, 1) = "0" Then
                        '    miaData = miaData & "/200" & Mid(txtCF.Text, 8, 1)
                        'Else
                        miaData = miaData & "/19" & Mid(txtCF.Text, 7, 2)
                        'End If



                        If Mid(txtCF.Text, 12, 1) <> "Z" Then
                            CITTADINANZA = "ITALIA"
                        Else
                            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & UCase(Mid(txtCF.Text, 12, 4)) & "'"
                            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                CITTADINANZA = par.IfNull(myReaderA("NOME"), "")
                            End If
                            myReaderA.Close()
                        End If
                    Else
                        CITTADINANZA = "ITALIA"

                    End If

                    If id_anagrafica = 0 Then
                        'inserisco anagrafica
                        par.cmd.CommandText = "Insert into SISCOM_MI.ANAGRAFICA   (ID, COGNOME, NOME, RAGIONE_SOCIALE, COD_FISCALE, PARTITA_IVA, CITTADINANZA, RESIDENZA, DATA_NASCITA, " _
                        & "COD_COMUNE_NASCITA, SESSO, TELEFONO, ID_INDIRIZZO_RECAPITO,  " _
                        & " " _
                        & "TIPO_DOC, NUM_DOC, DATA_DOC, RILASCIO_DOC, COMUNE_RESIDENZA, PROVINCIA_RESIDENZA, INDIRIZZO_RESIDENZA, CIVICO_RESIDENZA, CAP_RESIDENZA, DOC_SOGGIORNO) Values " _
                        & "(SISCOM_MI.SEQ_ANAGRAFICA.NEXTVAL, '" & par.PulisciStrSql(UCase(LTrim(RTrim(txtCognome.Text)))) & "', '" & par.PulisciStrSql(LTrim(RTrim(UCase(txtNome.Text)))) _
                        & "','" & par.PulisciStrSql(UCase(LTrim(RTrim(txtRS.Text)))) & "', '" & UCase(txtCF.Text) & "','" & par.PulisciStrSql(UCase(LTrim(RTrim(txtpiva.Text)))) _
                        & "', '" & par.PulisciStrSql(UCase(CITTADINANZA)) & "', '" & par.PulisciStrSql(UCase(txtComuneResidenza.Text)) _
                        & " ', '" & par.AggiustaData(miaData) & "', '" & UCase(Mid(txtCF.Text, 12, 4)) & "','" & par.RicavaSesso(txtCF.Text) & "', NULL, NULL, " _
                        & " NULL, NULL, NULL, NULL, '" & par.PulisciStrSql(UCase(txtComuneResidenza.Text)) _
                        & "', '" & par.PulisciStrSql(UCase(txtPrResidenza.Text)) & "', '" & par.PulisciStrSql(UCase(LTrim(RTrim(txtIndirizzoResidenza.Text)))) & "', '" _
                        & par.PulisciStrSql(UCase(txtCivicoResidenza.Text)) & "', '" _
                        & par.PulisciStrSql(txtCapResidenza.Text) & "', NULL)"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select siscom_mi.seq_anagrafica.currval from dual"
                        Dim myReaderXX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderXX.Read Then
                            id_anagrafica = myReaderXX(0)
                        End If
                        myReaderXX.Close()

                    End If


                    Dim id_INDIRIZZO_UNITA As Long = 0
                    Dim id_INDIRIZZO_SPEDIZIONE As Long = 0


                    'INSERISCO indirizzo unita
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INDIRIZZI (ID,DESCRIZIONE,CIVICO,CAP,LOCALITA,COD_COMUNE) VALUES (SISCOM_MI.SEQ_INDIRIZZI.NEXTVAL,'" & par.PulisciStrSql(UCase(cmbTipoViaUnita.SelectedItem.Text & " " & txtIndirizzoUnita.Text)) _
                                      & "','" & par.PulisciStrSql(txtCivicoUnita.Text) & "','" _
                                      & par.PulisciStrSql(txtCapUnita.Text) & "','" & par.PulisciStrSql(UCase(txtComuneUnita.Text)) _
                                      & "','" & CODICE_COMUNE_UNITA & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "select siscom_mi.seq_INDIRIZZI.currval from dual"
                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        id_INDIRIZZO_UNITA = myReaderB(0)
                    End If
                    myReaderB.Close()

                    Dim INDICE_UNITA As Long = 0
                    Dim CODICE_UNITA As String = ""

                    Dim piano As Long = 0

                    par.cmd.CommandText = "select MAX(TO_NUMBER(cod_tipo_livello_piano)) as piano from SISCOM_MI.unita_immobiliari where id_edificio=1"
                    myReaderB = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        piano = myReaderB("piano")
                    End If
                    myReaderB.Close()

                    Dim interno As Long = 0

                    par.cmd.CommandText = "select MAX(TO_NUMBER(interno)) as interno from SISCOM_MI.unita_immobiliari where id_edificio=1 and cod_tipo_livello_piano='" & piano & "'"
                    myReaderB = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        interno = myReaderB("interno") + 1
                    End If
                    myReaderB.Close()

                    If interno > 999 Then
                        interno = 1
                        piano = piano + 1
                    End If

                    CODICE_UNITA = "000000101" & Format(piano, "00") & "00A" & Format(interno, "000")

                    'INSERISCO UNITA IMMOBILIARE
                    par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_IMMOBILIARI  (ID, COD_UNITA_IMMOBILIARE, COD_TIPOLOGIA, ID_EDIFICIO, " _
                                       & "INTERNO, ID_UNITA_PRINCIPALE, ID_PIANO, COD_TIPO_DISPONIBILITA, COD_STATO_CONS_LG_392_78, " _
                                       & "ID_CATASTALE, COD_STATO_CENSIMENTO, ID_OPERATORE_INSERIMENTO, ID_OPERATORE_AGGIORNAMENTO, ID_SCALA, " _
                                       & "COD_TIPO_LIVELLO_PIANO, ID_INDIRIZZO, ID_DESTINAZIONE_USO) Values " _
                                       & "(siscom_mi.seq_unita_immobiliari.nextval, '" & CODICE_UNITA & "', 'AL', 1, '" & Format(interno, "000") & "', NULL, NULL, 'OCCU', " _
                                       & "NULL, NULL, NULL, NULL, NULL, NULL, '" & piano & "', " & id_INDIRIZZO_UNITA & ", 1)"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "select siscom_mi.seq_UNITA_IMMOBILIARI.currval from dual"
                    myReaderB = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        INDICE_UNITA = myReaderB(0)
                    End If
                    myReaderB.Close()

                    Dim INDICE_CONTRATTO As Long = 0
                    Dim CODICE_NUOVO_CONTRATTO As String = ""


                    CODICE_NUOVO_CONTRATTO = CODICE_UNITA & "01"

                    Dim DESTINAZIONE As String = "R"

                    Select Case cmbTipoRapporto.SelectedItem.VALUE
                        Case "1", "2", "4", "7"
                            DESTINAZIONE = "R"
                        Case "6"
                            DESTINAZIONE = "0"
                        Case "3"
                            DESTINAZIONE = "N"
                        Case "5"
                            DESTINAZIONE = "0"

                    End Select
                    Dim TIPO_CONTRATTO As String = ""
                    Dim TIPO_CONTRATTO1 As String = "LEGIT"
                    Dim NOTITOLO As String = "LEGIT"

                    Select Case cmbTipoRapporto.SelectedItem.VALUE
                        Case "1", "2"
                            TIPO_CONTRATTO = "ERP"
                        Case "3"
                            TIPO_CONTRATTO = "USD"
                        Case "4"
                            TIPO_CONTRATTO = ""
                        Case "5"
                            TIPO_CONTRATTO = "EQC392"
                        Case "6"
                            TIPO_CONTRATTO = "L43198"

                        Case "7"
                            TIPO_CONTRATTO = "NONE"
                            TIPO_CONTRATTO1 = "ILLEG"
                            NOTITOLO = "NOTIT"
                    End Select


                    Dim SENZATITOLO As String = ""
                    If CHST.CHECKED = True Then
                        SENZATITOLO = "20091231"
                        NOTITOLO = "NOTIT"
                    End If

                    Dim NOME_RECAPITO As String = ""
                    If txtRS.Text = "" Then
                        NOME_RECAPITO = UCase(txtCognome.Text & " " & txtNome.Text)
                    Else
                        NOME_RECAPITO = UCase(txtRS.Text)
                    End If


                    'INSERISCO IL CONTRATTO

                    par.cmd.CommandText = "Insert into SISCOM_MI.RAPPORTI_UTENZA    (ID, COD_CONTRATTO_GIMI, COD_CONTRATTO, COD_TIPOLOGIA_RAPP_CONTR, COD_TIPOLOGIA_CONTR_LOC,     DURATA_ANNI, DURATA_MESI, DURATA_GIORNI, DATA_DECORRENZA, DATA_SCADENZA,     DATA_DISDETTA_LOCATARIO, NUM_REGISTRAZIONE, SERIE_REGISTRAZIONE, IMP_CANONE_INIZIALE, IMP_DEPOSITO_CAUZ,     COD_FASCIA_REDDITO, MESSA_IN_MORA, PRATICA_AL_LEGALE, ISCRIZIONE_RUOLO, RATEIZZAZIONI_IN_CORSO,     DECADENZA, SFRATTO, NOTE, DATA_REG, COD_UFFICIO_REG,     DATA_STIPULA, DATA_CONSEGNA, DATA_SCADENZA_RINNOVO, DURATA_RINNOVO, MESI_DISDETTA,     DATA_RICONSEGNA, DELIBERA, DATA_DELIBERA, NRO_RATE, FREQ_VAR_ISTAT,     VERSAMENTO_TR, PER_BANDO, TIPO_COR, LUOGO_COR, VIA_COR,     NOTE_COR, CIVICO_COR, SIGLA_COR, CAP_COR, PRESSO_COR,     BOZZA, INTERESSI_CAUZIONE, NRO_REPERTORIO, DATA_REPERTORIO, NRO_ASSEGNAZIONE_PG,     DATA_ASSEGNAZIONE_PG, INIZIO_PERIODO, LIBRETTO_DEPOSITO, ID_DEST_RATE, INVIO_BOLLETTA,     FL_CONGUAGLIO, PERC_RINNOVO_CONTRATTO, PERC_ISTAT, IMP_CANONE_ATTUALE, PROVENIENZA_ASS,     INTERESSI_RIT_PAG, IMPORTO_ANTICIPO, ID_DOMANDA, ID_AU, ID_ISEE,     ID_COMMISSARIATO, REG_TELEMATICA, DEST_USO, DESCR_DEST_USO, DATA_INVIO_RIC_DISDETTA,     MOTIVO_REC_FORZOSO, FL_STAMPATO, BOLLO, N_OFFERTA, DATA_NOTIFICA_DISDETTA,     MITTENTE_DISDETTA, DATA_CONVALIDA_SFRATTO, DATA_ESECUZIONE_SFRATTO, DATA_RINVIO_SFRATTO, DATA_CONFERMA_FP) Values   (SISCOM_MI.SEQ_RAPPORTI_UTENZA.NEXTVAL, '', '" & CODICE_NUOVO_CONTRATTO & "', '" & TIPO_CONTRATTO1 & "', '" & TIPO_CONTRATTO & "', 4, 48, NULL, '20090101', '20130101',  '" & SENZATITOLO & "' , NULL, NULL, NULL, 0,  NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL,  '20090101', '20090101', '20170101', 4, 6,     NULL, 'XXX', 19000101, 12, NULL,     NULL, NULL, '" & cmbTipoViaCor.SelectedItem.Text & "', '" & par.PulisciStrSql(UCase(txtComuneSpedizione.Text)) & "', '" & par.PulisciStrSql(UCase(txtIndirizzoSpedizione.Text)) & "', NULL, '" & par.PulisciStrSql(txtCivicoSpedizione.Text) & "', '" & par.PulisciStrSql(txtPrSpedizione.Text) & "', '" & par.PulisciStrSql(txtCapSpedizione.Text) & "', '" & par.PulisciStrSql(NOME_RECAPITO) & "',     0, NULL, NULL, NULL, NULL,     NULL, '" & Prossimo_Periodo & "', NULL, 1, 1,     '1', 0, 0, NULL, " & cmbTipoRapporto.SelectedItem.Value & " ,     '0', 0, NULL, NULL, NULL,     1, 'IMPORTATO', '" & DESTINAZIONE & "', NULL, NULL,     4, 1, NULL, NULL, NULL,     -1, NULL, NULL, NULL, NULL)"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "select siscom_mi.seq_RAPPORTI_UTENZA.currval from dual"
                    myReaderB = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        INDICE_CONTRATTO = myReaderB(0)
                    End If
                    myReaderB.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL (ID_CONTRATTO,PROSSIMA_BOLLETTA) VALUES (" & INDICE_CONTRATTO & ",'" & Prossimo_Periodo & "')"
                    par.cmd.ExecuteNonQuery()

                    Dim Titolo As String = "CAP"
                    If txtRS.Text <> "" Then
                        Titolo = "NA"
                    End If
                    'INSERISCO IN SOGGETTI CONTRATTUALI
                    If TIPO_CONTRATTO <> "USD" Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" & id_anagrafica & "," & INDICE_CONTRATTO & ",'" & Titolo & "','INTE','" & NOTITOLO & "')"
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" & id_anagrafica & "," & INDICE_CONTRATTO & ",'NA','INTE','" & NOTITOLO & "')"
                    End If


                    par.cmd.ExecuteNonQuery()

                    'INSERISCO UNITA CONTRATTUALE
                    par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_CONTRATTUALE (ID_CONTRATTO, ID_UNITA, COD_UNITA_IMMOBILIARE, TIPOLOGIA, ID_EDIFICIO, SCALA, COD_TIPO_LIVELLO_PIANO, INTERNO, ID_UNITA_PRINCIPALE, SEZIONE, FOGLIO, NUMERO, SUB, COD_TIPOLOGIA_CATASTO, RENDITA, COD_CATEGORIA_CATASTALE, COD_CLASSE_CATASTALE, COD_QUALITA_CATASTALE, SUPERFICIE_MQ, CUBATURA, NUM_VANI, SUPERFICIE_CATASTALE, RENDITA_STORICA, IMMOBILE_STORICO, REDDITO_DOMINICALE, VALORE_IMPONIBILE, REDDITO_AGRARIO, VALORE_BILANCIO, DATA_ACQUISIZIONE, DATA_FINE_VALIDITA, DITTA, NUM_PARTITA, ESENTE_ICI, PERC_POSSESSO, INAGIBILE, MICROZONA_CENSUARIA, ZONA_CENSUARIA, COD_STATO_CATASTALE, INDIRIZZO, CIVICO, CAP, LOCALITA, COD_COMUNE, SUP_CONVENZIONALE, VAL_LOCATIVO_UNITA) Values (" _
                                          & INDICE_CONTRATTO & "," & INDICE_UNITA & ", '" & CODICE_UNITA & "', 'AL', 1, NULL, '02', '', NULL, NULL,    NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, '" _
                                          & par.PulisciStrSql(UCase(cmbTipoViaUnita.SelectedItem.Text & " " & txtIndirizzoUnita.Text)) & "', '" & par.PulisciStrSql(txtCivicoUnita.Text) _
                                          & "', '" & par.PulisciStrSql(txtCapUnita.Text) & "', NULL, '" & CODICE_COMUNE_UNITA & "', NULL, NULL)"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTESTATARI_RAPPORTO (ID_CONTRATTO,ID_ANAGRAFICA,DATA_INIZIO,DATA_FINE) VALUES (" & INDICE_CONTRATTO & "," & id_anagrafica & ",'19000101','29991231')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & INDICE_CONTRATTO & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F01','')"
                    par.cmd.ExecuteNonQuery()

                    Dim ESERCIZIO_F As Long = 0
                    ESERCIZIO_F = par.RicavaEsercizioCorrente

                    If cmbVoce1.SelectedItem.Text <> "--" And txtImporto1.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " & ESERCIZIO_F & ", " & cmbVoce1.SelectedItem.Value & ", " & par.VirgoleInPunti((txtImporto1.Text) * 12) & ", 1, 12, " & par.VirgoleInPunti(txtImporto1.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce2.SelectedItem.Text <> "--" And txtImporto2.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce2.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto2.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto2.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce3.SelectedItem.Text <> "--" And txtImporto3.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce3.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto3.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto3.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce4.SelectedItem.Text <> "--" And txtImporto4.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce4.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto4.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto4.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce5.SelectedItem.Text <> "--" And txtImporto5.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce5.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto5.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto5.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce6.SelectedItem.Text <> "--" And txtImporto6.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce6.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto6.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto6.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce7.SelectedItem.Text <> "--" And txtImporto7.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce7.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto7.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto7.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce8.SelectedItem.Text <> "--" And txtImporto8.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce8.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto8.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto8.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce9.SelectedItem.Text <> "--" And txtImporto9.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce9.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto9.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto9.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce10.SelectedItem.Text <> "--" And txtImporto10.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce10.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto10.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto10.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If



                    If cmbVoce11.SelectedItem.Text <> "--" And txtImporto11.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce11.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto11.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto11.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If


                    If cmbVoce12.SelectedItem.Text <> "--" And txtImporto12.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce12.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto12.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto12.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce13.SelectedItem.Text <> "--" And txtImporto13.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce13.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto13.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto13.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce14.SelectedItem.Text <> "--" And txtImporto14.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce14.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto14.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto14.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce15.SelectedItem.Text <> "--" And txtImporto15.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce15.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto15.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto15.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce16.SelectedItem.Text <> "--" And txtImporto16.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce16.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto16.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto16.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce17.SelectedItem.Text <> "--" And txtImporto17.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce17.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto17.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto17.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce18.SelectedItem.Text <> "--" And txtImporto18.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce18.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto18.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto18.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce19.SelectedItem.Text <> "--" And txtImporto19.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce19.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto19.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto19.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If cmbVoce20.SelectedItem.Text <> "--" And txtImporto20.Text <> "" Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                            & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values " _
                                           & "(SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & INDICE_CONTRATTO & ", " & INDICE_UNITA & ", " _
                                           & ESERCIZIO_F & ", " & cmbVoce20.SelectedItem.Value & ", " _
                                           & par.VirgoleInPunti((txtImporto20.Text) * 12) & ", 1, 12, " _
                                           & par.VirgoleInPunti(txtImporto20.Text) & ", " & Year(Now) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If


                    Dim I As Integer
                    Dim RIFERIMENTO_AL As String = ""
                    Dim NOMINATIVO As String = ""
                    Dim ID_BOLLETTA As Long = 0

                    Dim TOTALE_BOLLETTA As Double = 0

                    For I = 0 To ListaArretrati.Items.Count - 1
                        If ListaArretrati.Items(I).Selected Then

                            Select Case CInt(Mid(ListaArretrati.Items(I).Value, 5, 2))
                                Case 1, 3, 5, 7, 8, 10, 12
                                    RIFERIMENTO_AL = ListaArretrati.Items(I).Value & "31"
                                Case 2
                                    RIFERIMENTO_AL = ListaArretrati.Items(I).Value & "28"
                                Case Else
                                    RIFERIMENTO_AL = ListaArretrati.Items(I).Value & "30"
                            End Select

                            If txtRS.Text <> "" Then
                                NOMINATIVO = UCase(txtRS.Text)
                            Else
                                NOMINATIVO = UCase(txtCognome.Text & " " & txtNome.Text)
                            End If

                            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE (ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                                & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F,ID_UNITA, FL_ANNULLATA, " _
                                                & "PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, " _
                                                & "RIFERIMENTO_A, FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG, RIF_FILE, " _
                                                & "RIF_BOLLETTINO, RIF_FILE_TXT, DATA_VALUTA, DATA_VALUTA_CREDITORE, RIF_CONTABILE, RIF_FILE_RENDICONTO,ID_TIPO) " _
                                                & "Values " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL," & CInt(Mid(ListaArretrati.Items(I).Value, 5, 2)) _
                                                & ", '" & Format(Now, "yyyyMMdd") & "', '" & par.AggiustaData(DateAdd("d", 10, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) & "', NULL, " _
                                                & "NULL, NULL, 'RATA n." & CInt(Mid(ListaArretrati.Items(I).Value, 5, 2)) & "', " & INDICE_CONTRATTO & ", " & ESERCIZIO_F & ", " _
                                                & INDICE_UNITA & ", '0', NULL, " & id_anagrafica & ", '" & par.PulisciStrSql(NOMINATIVO) & "'," _
                                                & "'" & par.PulisciStrSql(UCase(cmbTipoViaCor.SelectedItem.Text & " " & txtIndirizzoSpedizione.Text) _
                                                & ", " & txtCivicoSpedizione.Text) _
                                                & "', '" & par.PulisciStrSql(UCase(txtCapSpedizione.Text _
                                                & " " & txtComuneSpedizione.Text & " (" & txtPrSpedizione.Text & ")")) & "', NULL, '" _
                                                & ListaArretrati.Items(I).Value & "01', '" & RIFERIMENTO_AL & "', " _
                                                & "'1', 1, NULL, NULL, NULL, " & CInt(Mid(ListaArretrati.Items(I).Value, 1, 4)) & ", NULL, 1, NULL, NULL, " _
                                                & "'REC', NULL, '', NULL, NULL, NULL, NULL,1)"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select siscom_mi.seq_BOL_BOLLETTE.currval from dual"
                            myReaderB = par.cmd.ExecuteReader()
                            If myReaderB.Read Then
                                ID_BOLLETTA = myReaderB(0)
                            End If
                            myReaderB.Close()

                            TOTALE_BOLLETTA = 0

                            If cmbVoce1.SelectedItem.Text <> "--" And txtImporto1.Text <> "" Then

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce1.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto1.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto1.Text

                            End If

                            If cmbVoce2.SelectedItem.Text <> "--" And txtImporto2.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce2.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto2.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto2.Text
                            End If

                            If cmbVoce3.SelectedItem.Text <> "--" And txtImporto3.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce3.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto3.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto3.Text
                            End If

                            If cmbVoce4.SelectedItem.Text <> "--" And txtImporto4.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce4.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto4.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto4.Text
                            End If

                            If cmbVoce5.SelectedItem.Text <> "--" And txtImporto5.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce5.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto5.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto5.Text

                            End If

                            If cmbVoce6.SelectedItem.Text <> "--" And txtImporto6.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce6.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto6.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto6.Text
                            End If

                            If cmbVoce7.SelectedItem.Text <> "--" And txtImporto7.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce7.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto7.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto7.Text
                            End If

                            If cmbVoce8.SelectedItem.Text <> "--" And txtImporto8.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce8.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto8.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto8.Text
                            End If

                            If cmbVoce9.SelectedItem.Text <> "--" And txtImporto9.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce9.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto9.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto9.Text
                            End If

                            If cmbVoce10.SelectedItem.Text <> "--" And txtImporto10.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce10.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto10.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto10.Text
                            End If

                            If cmbVoce11.SelectedItem.Text <> "--" And txtImporto11.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce11.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto11.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto11.Text
                            End If

                            If cmbVoce12.SelectedItem.Text <> "--" And txtImporto12.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce12.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto12.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto12.Text
                            End If

                            If cmbVoce13.SelectedItem.Text <> "--" And txtImporto13.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce13.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto13.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto13.Text
                            End If

                            If cmbVoce14.SelectedItem.Text <> "--" And txtImporto14.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce14.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto14.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto14.Text
                            End If

                            If cmbVoce15.SelectedItem.Text <> "--" And txtImporto15.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce15.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto15.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto15.Text
                            End If

                            If cmbVoce16.SelectedItem.Text <> "--" And txtImporto16.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce16.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto16.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto16.Text
                            End If

                            If cmbVoce17.SelectedItem.Text <> "--" And txtImporto17.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce17.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto17.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto17.Text
                            End If

                            If cmbVoce18.SelectedItem.Text <> "--" And txtImporto18.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce18.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto18.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto18.Text
                            End If

                            If cmbVoce19.SelectedItem.Text <> "--" And txtImporto19.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce19.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto19.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto19.Text
                            End If

                            If cmbVoce20.SelectedItem.Text <> "--" And txtImporto20.Text <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & cmbVoce20.SelectedItem.Value _
                                                    & "," & par.VirgoleInPunti(txtImporto20.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + txtImporto20.Text
                            End If

                            If SPESEmav > 0 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()

                                TOTALE_BOLLETTA = TOTALE_BOLLETTA + SPESEmav
                            End If


                            If TOTALE_BOLLETTA > APPLICABOLLO Then

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                    & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()

                            End If

                        End If
                    Next


                    Response.Write("<script>alert('Operazione Effettuata! Ora sarai reindirizzato al contratto appena creato.\nSi ricorda che le bollette create dovranno essere stampate tramite il modulo BANCA Intesa e quindi non saranno comprese nella bollettazione massiva!');window.open('Contratto.aspx?ID=" & INDICE_CONTRATTO & "&COD=" & CODICE_NUOVO_CONTRATTO & "', '" & CODICE_NUOVO_CONTRATTO & "', 'height=780,width=1160');document.location.href='pagina_home.aspx';</script>")
                Else
                    Response.Write("<script>alert('Comune di residenza e/o unità e/o spedizione non riconosciuto!');</script>")
                End If


                par.myTrans.Commit()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception

                lblErrore.Text = ex.Message
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End Try
        End If
    End Sub

    Protected Sub imgCopia1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCopia1.Click
        txtComuneUnita.Text = txtComuneResidenza.Text
        txtPrUnita.Text = txtPrResidenza.Text
        txtCapUnita.Text = txtCapResidenza.Text
        txtCivicoUnita.Text = txtCivicoResidenza.Text
        txtIndirizzoUnita.Text = txtIndirizzoResidenza.Text
        cmbTipoViaUnita.Text = cmbTipoViaResidenza.Text
    End Sub

    Protected Sub imgCopia2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCopia2.Click
        txtComuneSpedizione.Text = txtComuneUnita.Text
        txtPrSpedizione.Text = txtPrUnita.Text
        txtCapSpedizione.Text = txtCapUnita.Text
        txtCivicoSpedizione.Text = txtCivicoUnita.Text
        txtIndirizzoSpedizione.Text = txtIndirizzoUnita.Text
        cmbTipoViaCor.Text = cmbTipoViaUnita.Text
    End Sub

    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
        If TrovaCF(txtcf.text) = True Then

        End If
    End Sub

    Function TrovaCF(ByVal cf As String) As Boolean
        TrovaCF = False
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            id_anagrafica = 0

            par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA WHERE COD_FISCALE='" & par.PulisciStrSql(UCase(cf)) & "'"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                id_anagrafica = myReaderB(0)

                txtComuneResidenza.Text = par.IfNull(myReaderB("COMUNE_RESIDENZA"), "")
                txtPrResidenza.Text = par.IfNull(myReaderB("PROVINCIA_RESIDENZA"), "")
                txtCapResidenza.Text = par.IfNull(myReaderB("CAP_RESIDENZA"), "")
                txtCivicoResidenza.Text = par.IfNull(myReaderB("CIVICO_RESIDENZA"), "")
                txtIndirizzoResidenza.Text = par.IfNull(myReaderB("INDIRIZZO_RESIDENZA"), "")
                'cmbTipoViaResidenza.Text()

            End If
            myReaderB.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try

    End Function
End Class
