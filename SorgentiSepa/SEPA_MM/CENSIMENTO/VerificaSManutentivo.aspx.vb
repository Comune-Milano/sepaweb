
Partial Class CENSIMENTO_VerificaSManutentivo
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Str As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()

            Me.Chk_PB.Attributes.Add("onclick", "javascript:AbilitaDrop(this.checked);")
            lastraF_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            lastraPF_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            serr_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

            datapreSL_txt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            dataSL_txt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            idunita.Value = Request.QueryString("ID")
            EVENTO.Value = Request.QueryString("T")
            idcontratto.Value = Request.QueryString("C")

            chiamante.Value = Request.QueryString("A")

            If Session.Item("CONT_DISDETTE") = "1" Then
                lettura.Value = "2"
            End If

            If Request.QueryString("L") = "2" Then
                Dim CTRL1 As Control
                For Each CTRL1 In Me.form1.Controls
                    If TypeOf CTRL1 Is TextBox Then
                        DirectCast(CTRL1, TextBox).Enabled = False
                    ElseIf TypeOf CTRL1 Is DropDownList Then
                        DirectCast(CTRL1, DropDownList).Enabled = False
                    ElseIf TypeOf CTRL1 Is CheckBox Then
                        DirectCast(CTRL1, CheckBox).Enabled = False
                    End If
                Next
                chLavori.Enabled = False
                ImgSalva.Visible = False
            End If


            cmbRiassegnabile.SelectedIndex = -1
            cmbRiassegnabile.Items.FindByValue("2").Selected = True

            txtDataSopralluogo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            datapreSL_txt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataConsegnaChiavi.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataRipresaChiavi.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            dataSL_txt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDisponibilita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataQuantificazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataSTAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataSTDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CaricaDati()
            Session.Add("INDIRIZZOUNITA", Label21.Text)

            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
                End If
            Next

            If idcontratto.Value = -1 Then
                Me.btn_verbale.ImageUrl = "../NuoveImm/imgVerbCons.png"
                t.Value = 1
            End If
        End If
        txtImpDanni.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
        txtImpDanni.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
        txtImpTrasporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
        txtImpTrasporto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
    End Sub

    Private Sub CaricaDati()
        Try
            Label10.Visible = False




            If chiamante.Value = "1" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'par.RiempiDListConVuoto(Me, par.OracleConn, "cmbTipo", "select * from T_TIPO_ALL_ERP ORDER BY COD ASC", "DESCRIZIONE", "COD")
            'par.RiempiDListConVuoto(Me, par.OracleConn, "cmbQuartiere", "select * from siscom_mi.TAB_QUARTIERI ORDER BY NOME ASC", "NOME", "ID")


            par.cmd.CommandText = "select * from T_TIPO_ALL_ERP ORDER BY COD ASC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            ' cmbTipo.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            myReader1.Close()


            par.cmd.CommandText = "select * from SISCOM_MI.TAB_STRUTTURE ORDER BY DESCRIZIONE ASC"
            myReader1 = par.cmd.ExecuteReader()
            cmbStruttura.Items.Add(New ListItem("--", "NULL"))
            While myReader1.Read
                cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()



            par.cmd.CommandText = "select * from siscom_mi.TAB_QUARTIERI ORDER BY NOME ASC"
            myReader1 = par.cmd.ExecuteReader()
            cmbQuartiere.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbQuartiere.Items.Add(New ListItem(par.IfNull(myReader1("nome"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()


            par.cmd.CommandText = "select unita_immobiliari.id_unita_principale,tipologia_unita_immobiliari.descrizione as tipounita,unita_immobiliari.cod_tipologia,unita_immobiliari.cod_tipo_disponibilita,complessi_immobiliari.id as idq,COMPLESSI_IMMOBILIARI.ID_QUARTIERE,edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.id as idunita,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipologia_unita_immobiliari,siscom_mi.tipo_livello_piano,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.ID=" & idunita.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '
                CODICE.Value = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")
                Label21.Text = "Codice " & CODICE.Value & " (" & par.IfNull(myReader3("tipounita"), "") & ")</br>Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") _
                & "</br>" & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & "</br>Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")

                If par.IfNull(myReader3("fl_piano_vendita"), "0") = "1" Then
                    lblPianoVendita.Visible = True
                    lblPianoVendita.Text = "<b>Unità inserita nel piano vendita!!</b>"
                Else
                    lblPianoVendita.Visible = False
                End If

                lblsuggerito.Text = ""
                If par.IfNull(myReader3("cod_tipologia"), "") = "AL" Then
                    par.cmd.CommandText = "select valore from siscom_mi.dimensioni where id_unita_immobiliare=" & par.IfNull(myReader3("idunita"), "-1") & " and cod_tipologia='SUP_NETTA'"
                    Dim myReader33 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader33.Read Then
                        lblsuggerito.Text = "Val.Suggerito:" & Format(par.IfNull(myReader33("valore"), 0) / 14, "0") & " - Sup.Netta (" & par.IfNull(myReader33("valore"), 0) & ") / 14"
                    End If
                    myReader33.Close()

                End If


                If par.IfNull(myReader3("GEST_RISC_DIR"), "0") = "1" Then
                    cmbDirettaRisc.SelectedIndex = -1
                    cmbDirettaRisc.Items.FindByText("SI").Selected = True
                Else
                    cmbDirettaRisc.SelectedIndex = -1
                    cmbDirettaRisc.Items.FindByText("NO").Selected = True
                End If

                If par.IfNull(myReader3("condominio"), "0") = "1" Then
                    cmbCondominio.SelectedIndex = -1
                    cmbCondominio.Items.FindByText("SI").Selected = True
                    cmbDirettaRisc.Enabled = True
                Else
                    cmbCondominio.SelectedIndex = -1
                    cmbCondominio.Items.FindByText("NO").Selected = True

                    cmbDirettaRisc.SelectedIndex = -1
                    cmbDirettaRisc.Items.FindByText("--").Selected = True
                    cmbDirettaRisc.Enabled = False
                End If


                If Mid(CODICE.Value, 1, 6) = "000000" Then
                    Response.Write("<script>alert('Attenzione...Non è possibile effettuare la verifica dello stato manutentivo su una unità VIRTUALE! I campi saranno disabilitati!\nSi ricorda che la data di sloggio per i rapporti virtuali va inserita direttamente nella maschera del rapporto.');</script>")
                    Dim CTRL1 As Control
                    For Each CTRL1 In Me.form1.Controls
                        If TypeOf CTRL1 Is TextBox Then
                            DirectCast(CTRL1, TextBox).Enabled = False
                        ElseIf TypeOf CTRL1 Is DropDownList Then
                            DirectCast(CTRL1, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL1 Is CheckBox Then
                            DirectCast(CTRL1, CheckBox).Enabled = False
                        End If
                    Next

                    chLavori.Enabled = False
                    ImgSalva.Visible = False
                End If


                If par.IfNull(myReader3("cod_tipo_disponibilita"), "INDEF") = "INDEF" Then
                    Response.Write("<script>alert('Attenzione...Non è possibile effettuare la verifica dello stato manutentivo su una unità con stato NON DEFINIBILE.\nI campi saranno disabilitati!\nSe si vogliono gestire i dati di questa unità, contattare gli uffici preposti del Comune di Milano.');</script>")
                    Dim CTRL1 As Control
                    For Each CTRL1 In Me.form1.Controls
                        If TypeOf CTRL1 Is TextBox Then
                            DirectCast(CTRL1, TextBox).Enabled = False
                        ElseIf TypeOf CTRL1 Is DropDownList Then
                            DirectCast(CTRL1, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL1 Is CheckBox Then
                            DirectCast(CTRL1, CheckBox).Enabled = False
                        End If
                    Next

                    chLavori.Enabled = False
                    ImgSalva.Visible = False
                End If

                If par.IfNull(myReader3("id_destinazione_uso"), -1) = -1 Then
                    Response.Write("<script>alert('Attenzione...destinazione d\'uso non specificata. I campi saranno disabilitati!');</script>")
                    Dim CTRL1 As Control
                    For Each CTRL1 In Me.form1.Controls
                        If TypeOf CTRL1 Is TextBox Then
                            DirectCast(CTRL1, TextBox).Enabled = False
                        ElseIf TypeOf CTRL1 Is DropDownList Then
                            DirectCast(CTRL1, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL1 Is CheckBox Then
                            DirectCast(CTRL1, CheckBox).Enabled = False
                        End If
                    Next

                    chLavori.Enabled = False
                    ImgSalva.Visible = False
                End If



                If par.IfNull(myReader3("id_unita_principale"), -1) <> -1 Then
                    Response.Write("<script>alert('Attenzione...Non è possibile effettuare la verifica dello stato menutentivo su una pertinenza. I campi saranno disabilitati!');</script>")
                    Dim CTRL1 As Control
                    For Each CTRL1 In Me.form1.Controls
                        If TypeOf CTRL1 Is TextBox Then
                            DirectCast(CTRL1, TextBox).Enabled = False
                        ElseIf TypeOf CTRL1 Is DropDownList Then
                            DirectCast(CTRL1, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL1 Is CheckBox Then
                            DirectCast(CTRL1, CheckBox).Enabled = False
                        End If
                    Next

                    chLavori.Enabled = False
                    ImgSalva.Visible = False
                End If

                Destinazione = par.IfNull(myReader3("id_destinazione_uso"), 1)
                Select Case Destinazione
                    Case 1, 6
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    Case 2
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "1"
                    Case 3
                        POR = "1"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    Case 4
                        POR = "0"
                        EQ = "1"
                        OA = "0"
                        MO = "0"
                    Case 5
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                End Select

                cmbQuartiere.SelectedIndex = -1
                cmbQuartiere.Items.FindByValue(par.IfNull(myReader3("ID_QUARTIERE"), "-1")).Selected = True

                IndiceComplesso = par.IfNull(myReader3("IDQ"), "-1")
                IndiceEdificio = par.IfNull(myReader3("IDF"), "-1")

                tipounita.Value = par.IfNull(myReader3("cod_tipologia"), "")

                If par.IfNull(myReader3("cod_tipologia"), "") <> "AL" Then
                    cmbTipo.SelectedIndex = -1
                    cmbTipo.Items.FindByValue("10").Selected = True
                    cmbTipo.Enabled = False
                    txtVani.Text = "0"
                    txtServizi.Text = "0"
                End If
            End If
            myReader3.Close()

            txtDataDisponibilita.Text = ""
            par.cmd.CommandText = "select * from alloggi where cod_alloggio='" & CODICE.Value & "'"
            myReader3 = par.cmd.ExecuteReader()
            If myReader3.Read Then
                txtDataDisponibilita.Text = par.FormattaData(par.IfNull(myReader3("data_disponibilita"), ""))

                'cmbDecentramento.SelectedIndex = -1
                'cmbDecentramento.Items.FindByText(par.IfNull(myReader3("ZONA"), "01")).Selected = True

                If par.IfNull(myReader3("tipo_alloggio"), "10") = "-1" Then
                    cmbTipo.SelectedIndex = -1
                    cmbTipo.Items.FindByValue("10").Selected = True
                Else
                    cmbTipo.SelectedIndex = -1
                    cmbTipo.Items.FindByValue(par.IfNull(myReader3("TIPO_ALLOGGIO"), "10")).Selected = True
                End If


                txtVani.Text = par.IfNull(myReader3("NUM_LOCALI"), "0")
                txtServizi.Text = par.IfNull(myReader3("NUM_SERVIZI"), "0")

            Else
                par.cmd.CommandText = "select * from siscom_mi.ui_usi_diversi where cod_alloggio='" & CODICE.Value & "'"
                Dim myReader31 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader31.Read Then
                    txtDataDisponibilita.Text = par.FormattaData(par.IfNull(myReader31("data_disponibilita"), ""))

                    'cmbDecentramento.SelectedIndex = -1
                    'cmbDecentramento.Items.FindByText(par.IfNull(myReader31("ZONA"), "01")).Selected = True

                    If par.IfNull(myReader31("tipo_alloggio"), "10") = "-1" Then
                        cmbTipo.SelectedIndex = -1
                        cmbTipo.Items.FindByValue("10").Selected = True
                    Else
                        cmbTipo.SelectedIndex = -1
                        cmbTipo.Items.FindByValue(par.IfNull(myReader31("TIPO_ALLOGGIO"), "10")).Selected = True
                    End If


                    txtVani.Text = par.IfNull(myReader31("NUM_LOCALI"), "0")
                    txtServizi.Text = par.IfNull(myReader31("NUM_SERVIZI"), "0")

                End If
                myReader31.Close()
            End If
            myReader3.Close()

            If txtDataDisponibilita.Text = "" Then
                par.cmd.CommandText = "select * from siscom_mi.ui_usi_diversi where cod_alloggio='" & CODICE.Value & "'"
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    txtDataDisponibilita.Text = par.FormattaData(par.IfNull(myReader3("data_disponibilita"), ""))
                End If
                myReader3.Close()
            End If

            idcontratto.Value = "-1"
            par.cmd.CommandText = "SELECT rapporti_utenza.data_decorrenza,siscom_mi.getstatocontratto(rapporti_utenza.id) as statoc,rapporti_utenza.id,RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_RICONSEGNA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_UNITA=" & idunita.Value & " AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' order by statoc desc"
            myReader3 = par.cmd.ExecuteReader()
            If myReader3.Read Then
                idcontratto.Value = par.IfNull(myReader3("ID"), "")
                statoc.Value = par.IfNull(myReader3("statoc"), "")
                RUdataDecorrenza.Value = par.IfNull(myReader3("data_decorrenza"), "19000101")
                lblDataDisdetta.Text = par.FormattaData(par.IfNull(myReader3("DATA_DISDETTA_LOCATARIO"), ""))
                If lblDataDisdetta.Text = "" Then
                    dataSL_txt.Enabled = False
                    lblDataDisdetta.Text = "---"
                    msgDataDisd.Value = 1
                Else
                    datapreSL_txt.Text = lblDataDisdetta.Text
                End If
                lblUltimoRapporto.Text = par.IfNull(myReader3("COD_CONTRATTO"), "")
            End If
            myReader3.Close()

            If idcontratto.Value = "" Or idcontratto.Value = "-1" Then
                par.cmd.CommandText = "SELECT siscom_mi.getstatocontratto(rapporti_utenza.id) as statoc,rapporti_utenza.id,RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_RICONSEGNA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_UNITA=" & idunita.Value & " AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL order by rapporti_utenza.data_riconsegna desc"
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    idcontratto.Value = "-1"
                    'max 09/06/2016 ma perchè la riga sotto è commentata?????
                    idContChiuso.Value = par.IfNull(myReader3("id"), "")
                    '--------------
                    statoc.Value = par.IfNull(myReader3("statoc"), "")
                    lblDataDisdetta.Text = par.FormattaData(par.IfNull(myReader3("DATA_DISDETTA_LOCATARIO"), ""))
                    lblUltimoRapporto.Text = par.IfNull(myReader3("COD_CONTRATTO"), "")
                    dataSL_txt.Text = par.FormattaData(par.IfNull(myReader3("DATA_RICONSEGNA"), ""))
                    datasloggio.Value = par.IfNull(myReader3("DATA_RICONSEGNA"), "")
                    'dataSL_txt.Enabled = False
                    'txtDataSopralluogo.Enabled = False
                    'chGRTP.Enabled = False
                    'txtNoteGRTP.Enabled = False
                    'datapreSL_txt.Enabled = False
                    DisattivaSloggio()
                End If
                myReader3.Close()
            End If

            If idcontratto.Value = "-1" And lblDataDisdetta.Text = "" Then
                statoc.Value = "CHIUSO"
                dataSL_txt.Enabled = False
                txtDataSopralluogo.Enabled = False
                chGRTP.Enabled = False
                txtNoteGRTP.Enabled = False
                datapreSL_txt.Enabled = False
                DisattivaSloggio()
            End If



            Inserimento = -1
            par.cmd.CommandText = "select siscom_mi.unita_STATO_MANUTENTIVO.* from siscom_mi.unita_STATO_MANUTENTIVO WHERE ID_UNITA=" & idunita.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                cmbStruttura.ClearSelection()
                ' If par.IfNull(myReader("ID_STRUTTURA_COMP"), "NULL") = "NULL" Then

                'End If
                cmbStruttura.Items.FindByValue(par.IfNull(myReader("ID_STRUTTURA_COMP"), "NULL")).Selected = True

                txtDataSTDal.Text = par.FormattaData(par.IfNull(myReader("DATA_CONSEGNA_STR"), ""))
                txtDataSTAl.Text = par.FormattaData(par.IfNull(myReader("DATA_RIPRESA_STR"), ""))


                Select Case par.IfNull(myReader("ST_DEP_CHIAVI"), "")
                    Case "0" 'VIA SAPONARO
                        Rd4.Checked = False
                        Rd3.Checked = False
                        Rd2.Checked = False
                        Rd5.Checked = False
                        Rd1.Checked = True
                    Case "1" 'VIA NEWTON
                        Rd4.Checked = False
                        Rd3.Checked = False
                        Rd1.Checked = False
                        Rd5.Checked = False
                        Rd2.Checked = True
                    Case "2" 'VIA COSTA
                        Rd4.Checked = False
                        Rd2.Checked = False
                        Rd1.Checked = False
                        Rd5.Checked = False
                        Rd3.Checked = True
                    Case "3" 'VIA SALEMI
                        Rd3.Checked = False
                        Rd2.Checked = False
                        Rd1.Checked = False
                        Rd5.Checked = False
                        Rd4.Checked = True
                    Case Else
                        Rd3.Checked = False
                        Rd2.Checked = False
                        Rd1.Checked = False
                        Rd4.Checked = False
                        Rd5.Checked = True
                End Select




                Inserimento = par.IfNull(myReader("id_unita"), -1)
                If par.IfNull(myReader("DATA_RILEVAZIONE"), "") <> "" Then
                    lblUltimaRilevazione.Text = "Ultima Rilevazione effettuata il : " & par.FormattaData(par.IfNull(myReader("DATA_RILEVAZIONE"), "")) & "</br>"
                Else
                    lblUltimaRilevazione.Text = "Ultima Rilevazione effettuata il : Nessuna rilevazione effettuata!</br>"
                End If


                txtMotivazioni.Text = par.IfNull(myReader("MOTIVAZIONI"), "")
                If lblDataDisdetta.Text <> "---" Then
                    txtDataSopralluogo.Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))
                    datapreSL_txt.Text = par.FormattaData(par.IfNull(myReader("DATA_PRE_SLOGGIO"), ""))
                End If

                If lblDataDisdetta.Text <> "---" And datapreSL_txt.Text = "" Then
                    datapreSL_txt.Text = lblDataDisdetta.Text
                End If

                cmbStatoAlloggio.SelectedIndex = -1
                cmbStatoAlloggio.Items.FindByValue(par.IfNull(myReader("riassegnabile"), 0)).Selected = True
                cmbRiassegnabile.SelectedIndex = -1
                cmbRiassegnabile.Items.FindByValue(par.IfNull(myReader("Tipo_Riassegnabile"), 0)).Selected = True



                'cmbPBlindata.SelectedIndex = -1
                'cmbPBlindata.Items.FindByValue(par.IfNull(myReader("p_blindata"), 0)).Selected = True

                cmbHandicap.SelectedIndex = -1
                cmbHandicap.Items.FindByValue(par.IfNull(myReader("handicap"), 0)).Selected = True

                txtNote.Text = par.IfNull(myReader("note"), "")

                txtDataFine.Text = par.FormattaData(par.IfNull(myReader("DATA_pre_S"), ""))

                txtNoteSicurezza.Text = par.IfNull(myReader("note_sicurezza"), "")
                txtNoteTipoPorta.Text = par.IfNull(myReader("note_tipo_porta"), "")


                txtDataQuantificazione.Text = par.FormattaData(par.IfNull(myReader("DATA_Q"), ""))
                txtNoteDanni.Text = par.IfNull(myReader("NOTE_DANNI"), "")
                txtImpDanni.Text = Format(par.IfNull(myReader("IMPORTO_DANNI"), "0"), "0.00")
                txtImpTrasporto.Text = Format(par.IfNull(myReader("IMPORTO_TRASPORTO"), "0"), "0.00")

                If par.IfNull(myReader("FL_AUTORIZZATI_IMP"), "0") = "0" Then
                    chAutorizzati.Checked = False
                Else
                    chAutorizzati.Checked = True
                End If



                If par.IfNull(myReader("fine_lavori"), "0") = "0" Then
                    chFineL.Checked = False
                Else
                    chFineL.Checked = True
                End If

                If par.IfNull(myReader("SOL_GP"), "0") = "0" Then
                    chGP.Checked = False
                Else
                    chGP.Checked = True
                End If

                If par.IfNull(myReader("SOL_GF"), "0") = "0" Then
                    ChGF.Checked = False
                Else
                    ChGF.Checked = True
                End If

                If par.IfNull(myReader("SOL_PB"), "0") = "0" Then
                    ChPB.Checked = False
                Else
                    ChPB.Checked = True
                End If

                If par.IfNull(myReader("SOL_PB1"), "0") = "0" Then
                    ChPB1.Checked = False
                Else
                    ChPB1.Checked = True
                End If

                If par.IfNull(myReader("SOL_PB2"), "0") = "0" Then
                    ChPB2.Checked = False
                Else
                    ChPB2.Checked = True
                End If

                If par.IfNull(myReader("SOL_PB3"), "0") = "0" Then
                    ChPB3.Checked = False
                Else
                    ChPB3.Checked = True
                End If
                If par.IfNull(myReader("SOL_LA"), "0") = "0" Then
                    ChLA.Checked = False
                Else
                    ChLA.Checked = True
                End If

                If par.IfNull(myReader("SOL_LA1"), "0") = "0" Then
                    ChLA1.Checked = False
                Else
                    ChLA1.Checked = True
                End If

                If par.IfNull(myReader("SOL_AL"), "0") = "0" Then
                    ChAL.Checked = False
                Else
                    ChAL.Checked = True
                End If

                If par.IfNull(myReader("ALLARME"), "0") = "0" Then
                    ChAllarme.Checked = False
                Else
                    ChAllarme.Checked = True
                End If

                txtDataConsegnaChiavi.Text = par.FormattaData(par.IfNull(myReader("DATA_consegna_chiavi"), ""))
                txtDataRipresaChiavi.Text = par.FormattaData(par.IfNull(myReader("DATA_ripresa_chiavi"), ""))
                txtConsegnatea.Text = par.IfNull(myReader("consegnate_a"), "")


                cmbAbusivo.SelectedIndex = -1
                cmbAbusivo.Items.FindByValue(par.IfNull(myReader("FL_ABUSIVO"), 0)).Selected = True

                If par.IfNull(myReader("FL_ABUSIVO"), "0") = "1" Then
                    POR = "0"
                    EQ = "0"
                    OA = "1"
                    MO = "0"
                End If

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_INTERVENTI_MANU_UI ORDER BY ID ASC"
            myReader1 = par.cmd.ExecuteReader()
            Do While myReader1.Read
                Dim lsiFrutto As New ListItem(myReader1("DESCRIZIONE"), myReader1("ID"))
                chLavori.Items.Add(lsiFrutto)
                lsiFrutto = Nothing
            Loop
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO_INT WHERE ID_UNITA=" & idunita.Value
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader2.Read
                chLavori.Items.FindByValue(myReader2("ID_INTERVENTO")).Selected = True
            Loop
            myReader2.Close()

            par.cmd.CommandText = "SELECT UNITA_STATO_MAN_S.*,rapporti_utenza.cod_contratto FROM siscom_mi.rapporti_utenza,SISCOM_MI.UNITA_STATO_MAN_S where rapporti_utenza.id=UNITA_STATO_MAN_S.id_contratto AND UNITA_STATO_MAN_S.ID_UNITA=" & idunita.Value & " ORDER BY data_memo desc"
            myReader1 = par.cmd.ExecuteReader()
            Do While myReader1.Read
                'Label12.Text = Label12.Text & "<a href=" & Chr(34) & "ReportStatoManutentivo.aspx?TIPO=1&ID=" & idunita.Value & "&DATA=" & myReader1("data_memo") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Stato Verifica al " & par.FormattaData(myReader1("data_memo")) & ", Assegnata al Contratto Codice " & myReader1("cod_contratto") & "</a></br>"
                Label12.Text = Label12.Text & "<a href=" & Chr(34) & "RepostStatoManutentivo1.aspx?A=0&TIPO=1&ID=" & idunita.Value & "&DATA=" & myReader1("data_memo") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Stato Verifica al " & par.FormattaData(myReader1("data_memo")) & ", Assegnata al Contratto Codice " & myReader1("cod_contratto") & "</a></br>"

            Loop
            myReader1.Close()

            CaricaZone()

            If cmbStatoAlloggio.SelectedItem.Value = 0 Then
                cmbRiassegnabile.SelectedIndex = -1
                cmbRiassegnabile.Items.FindByValue("2").Selected = True
                txtDataDisponibilita.Text = ""
                txtDataDisponibilita.Enabled = False
            Else
                txtDataDisponibilita.Enabled = True

            End If

            If lblDataDisdetta.Text = "---" Then
                'cmbStatoAlloggio.Enabled = False
                'cmbRiassegnabile.Enabled = False
                'chFineL.Enabled = False
                datapreSL_txt.Enabled = False
                dataSL_txt.Enabled = False
                txtDataSopralluogo.Enabled = False
                'txtDataDisponibilita.Enabled = False
                txtNoteGRTP.Enabled = False
                chGRTP.Enabled = False
                DisattivaSloggio()

            End If


            If dataSL_txt.Text <> "" And txtDataSopralluogo.Text = "" Then
                txtDataSopralluogo.Enabled = True
                datapreSL_txt.Enabled = True

            End If
            If idcontratto.Value <> "-1" Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.sl_sloggio where ID_UNITA_IMMOBILIARE = " & idunita.Value & " and id_contratto = " & idcontratto.Value & ""


            ElseIf idContChiuso.Value <> 0 Then

                par.cmd.CommandText = "select id,id_stato from siscom_mi.sl_sloggio where ID_UNITA_IMMOBILIARE = " & idunita.Value & " and id_contratto = " & idContChiuso.Value & ""
            Else
                par.cmd.CommandText = "select id,id_stato from siscom_mi.sl_sloggio where ID_UNITA_IMMOBILIARE = " & idunita.Value & " and id_contratto is null"

            End If

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idSloggio.Value = par.IfNull(lettore("id"), "0")
                idStatoSl.Value = par.IfNull(lettore("id_stato"), "0")
            End If
            lettore.Close()

            If idSloggio.Value <> "0" Then
                CaricaDatiSloggio()
            Else
                SettaStatoControl()

            End If

            Session.Add("Q", cmbQuartiere.SelectedItem.Text)
            Session.Add("P", cmbCondominio.SelectedItem.Text)
            Session.Add("R", cmbDirettaRisc.SelectedItem.Text)


            par.cmd.Dispose()

            If chiamante.Value = "1" Then

            Else
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If chiamante.Value = "1" Then

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Label10.Visible = True
            Label10.Text = ex.Message
        End Try
    End Sub


    Public Property Inserimento() As Long
        Get
            If Not (ViewState("par_Inserimento") Is Nothing) Then
                Return CLng(ViewState("par_Inserimento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Inserimento") = value
        End Set

    End Property

    Public Property IndiceComplesso() As String
        Get
            If Not (ViewState("par_IndiceComplesso") Is Nothing) Then
                Return CStr(ViewState("par_IndiceComplesso"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceComplesso") = value
        End Set

    End Property

    Public Property Destinazione() As String
        Get
            If Not (ViewState("par_Destinazione") Is Nothing) Then
                Return CStr(ViewState("par_Destinazione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Destinazione") = value
        End Set

    End Property

    Public Property IndiceEdificio() As String
        Get
            If Not (ViewState("par_IndiceEdificio") Is Nothing) Then
                Return CStr(ViewState("par_IndiceEdificio"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceEdificio") = value
        End Set

    End Property

    Public Property POR() As String
        Get
            If Not (ViewState("par_POR") Is Nothing) Then
                Return CStr(ViewState("par_POR"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_POR") = value
        End Set

    End Property

    Public Property EQ() As String
        Get
            If Not (ViewState("par_EQ") Is Nothing) Then
                Return CStr(ViewState("par_EQ"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_EQ") = value
        End Set

    End Property

    Public Property OA() As String
        Get
            If Not (ViewState("par_OA") Is Nothing) Then
                Return CStr(ViewState("par_OA"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_OA") = value
        End Set

    End Property

    Public Property MO() As String
        Get
            If Not (ViewState("par_MO") Is Nothing) Then
                Return CStr(ViewState("par_MO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_MO") = value
        End Set

    End Property




    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Salva()
    End Sub


    Private Function RicavaVia1(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String


        pos = InStr(1, indirizzo, " ")
        If pos > 0 Then
            via = Mid(indirizzo, 1, pos - 1)
            Select Case via
                Case "C.SO"
                    RicavaVia1 = "CORSO"
                Case "PIAZZA", "PZ.", "P.ZZA"
                    RicavaVia1 = "PIAZZA"
                Case "PIAZZALE", "P.LE"
                    RicavaVia1 = "PIAZZALE"
                Case "P.T"
                    RicavaVia1 = "PORTA"
                Case "S.T.R.", "STRADA"
                    RicavaVia1 = "STRADA"
                Case "V.", "VIA"
                    RicavaVia1 = "VIA"
                Case "VIALE", "V.LE"
                    RicavaVia1 = "VIALE"
                Case Else
                    RicavaVia1 = "VIA"
            End Select

        Else
            RicavaVia1 = ""
        End If

    End Function

    Private Function RicavaInd(ByVal indirizzo As String, ByVal TipoVia As String) As String
        RicavaInd = indirizzo

        RicavaInd = Trim(Replace(indirizzo, TipoVia, ""))

    End Function


    Private Function InserisciAlloggio()
        Dim sstringaIns As String = "INSERT INTO "
        Dim NUOVOALLOGGIO As Long = 0
        Dim TIPOALL As String = ""



        par.cmd.CommandText = "select SCALE_EDIFICI.DESCRIZIONE AS SCALE, EDIFICI.NUM_ASCENSORI,indirizzi.civico,indirizzi.descrizione as via,comuni_nazioni.nome as comune_di,identificativi_catastali.foglio,identificativi_catastali.sub,identificativi_catastali.numero,UNITA_IMMOBILIARI.*  from SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,siscom_mi.indirizzi,comuni_nazioni,siscom_mi.identificativi_catastali,siscom_mi.unita_immobiliari where UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND indirizzi.id=UNITA_IMMOBILIARI.id_indirizzo and comuni_nazioni.cod=EDIFICI.cod_comune and UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) and unita_immobiliari.id=" & idunita.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then

            TIPOALL = par.IfNull(myReader("cod_tipologia"), "")

            If cmbAbusivo.SelectedItem.Value = "1" Then
                POR = "0"
                EQ = "0"
                OA = "1"
                MO = "0"
            End If

            If par.IfNull(myReader("cod_tipologia"), "") = "AL" And Destinazione <> "5" Then
                sstringaIns = sstringaIns & " ALLOGGI (ID,PROPRIETA,STATO,PRENOTATO,ASSEGNATO,NOTE,GESTIONE,TIPOLOGIA_GESTORE,cod_alloggio,FL_POR,FL_OA,FL_MOD,eqcanone) VALUES (SEQ_ALLOGGI.NEXTVAL,0,5,0,0,'INSERITO DA WEB',9,'ERP','" & CODICE.Value & "', '" & POR & "','" & OA & "','" & MO & "','" & EQ & "')"
            Else

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UI_USI_DIVERSI WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.HasRows = False Then
                    sstringaIns = sstringaIns & " SISCOM_MI.UI_USI_DIVERSI (ID,PROPRIETA,STATO,PRENOTATO,ASSEGNATO,NOTE,GESTIONE,TIPOLOGIA_GESTORE,cod_alloggio,FL_POR,FL_OA,FL_MOD,eqcanone,DATA_DISPONIBILITA) VALUES (SEQ_ALLOGGI.NEXTVAL,0,5,0,0,'INSERITO DA WEB',9,'ERP','" & CODICE.Value & "', '" & POR & "','" & OA & "','" & MO & "','" & EQ & "','" & par.AggiustaData(txtDataDisponibilita.Text) & "')"
                Else
                    sstringaIns = ""
                    If myReader123.Read Then
                        NUOVOALLOGGIO = myReader123("id")
                    End If
                End If
                myReader123.Close()
            End If
            If sstringaIns <> "" Then
                par.cmd.CommandText = sstringaIns
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_ALLOGGI.CURRVAL FROM DUAL"
                'par.cmd.CommandText = sstringaIns
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    NUOVOALLOGGIO = myReader1(0)
                End If
                myReader1.Close()
            End If

            If TIPOALL = "AL" And Destinazione <> "5" Then
                sstringaIns = "UPDATE ALLOGGI SET "
            Else
                sstringaIns = "UPDATE SISCOM_MI.UI_USI_DIVERSI SET "
            End If
            sstringaIns = sstringaIns & "SCALA='" & par.IfNull(myReader("scale"), "") _
                        & "',NUM_ALLOGGIO='" & par.IfNull(myReader("interno"), "") _
                        & "',FOGLIO='" & par.IfNull(myReader("foglio"), "") _
                        & "',PARTICELLA='" & par.IfNull(myReader("NUMERO"), "") _
                        & "',SUB='" & par.IfNull(myReader("SUB"), "") & "'"



            par.cmd.CommandText = "select dimensioni.*  from SISCOM_MI.dimensioni where dimensioni.id_unita_immobiliare=" & idunita.Value
            Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader12.Read
                If myReader12("cod_tipologia") = "SUP_CONV" Then
                    sstringaIns = sstringaIns & ",SUP=" & par.VirgoleInPunti(par.IfNull(myReader12("VALORE"), "0"))
                End If


            Loop
            myReader12.Close()


            sstringaIns = sstringaIns & ",COMUNE='" & par.PulisciStrSql(UCase(par.IfNull(myReader("comune_di"), ""))) & "'"

            Dim TipoIndirizzo As String = "VIA"

            TipoIndirizzo = RicavaVia1(par.IfNull(myReader("via"), "VIA"))
            par.cmd.CommandText = "select * from T_TIPO_INDIRIZZO WHERE DESCRIZIONE='" & TipoIndirizzo & "'"

            Dim TIPOINDIRIZZO1 As String = "6"
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.Read Then
                TIPOINDIRIZZO1 = par.IfNull(myReader11("COD"), "6")
            End If
            myReader11.Close()
            sstringaIns = sstringaIns & ",TIPO_INDIRIZZO=" & TIPOINDIRIZZO1

            sstringaIns = sstringaIns & ",INDIRIZZO='" & par.PulisciStrSql(RicavaInd(par.IfNull(myReader("VIA"), ""), TipoIndirizzo)) & "'"
            sstringaIns = sstringaIns & ",NUM_CIVICO='" & par.PulisciStrSql(par.IfNull(myReader("civico"), "")) & "'"
            sstringaIns = sstringaIns & ",PIANO='" & par.IfNull(myReader("cod_tipo_livello_piano"), "78") & "'"

            If par.IfNull(myReader("NUM_ASCENSORI"), "0") <> "0" Then
                sstringaIns = sstringaIns & ",ASCENSORE='1',"
            Else
                sstringaIns = sstringaIns & ",ASCENSORE='0',"
            End If

            If cmbHandicap.SelectedItem.Value = "1" Then
                sstringaIns = sstringaIns & "h_motorio='1',barriere_arc='0'"
            Else
                sstringaIns = sstringaIns & "h_motorio='0',barriere_arc='1'"
            End If


            sstringaIns = sstringaIns & " WHERE ID=" & NUOVOALLOGGIO
            par.cmd.CommandText = sstringaIns
            par.cmd.ExecuteNonQuery()

        End If
        myReader.Close()


    End Function

    Private Function Salva()
        Try
            Dim mostrato As Boolean = False

            Dim Altro As String = ""

            Label10.Visible = False

            If chiamante.Value = "1" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If idcontratto.Value = "-1" And idcontratto.Value = "" Then
                idcontratto.Value = Request.QueryString("C")
            End If
            If tipounita.Value = "AL" Then
                If cmbHandicap.SelectedItem.Text = "NO" And txtMotivazioni.Text = "" And cmbAbusivo.SelectedItem.Text = "NO" Then
                    Response.Write("<script>alert('Inserire le motivazioni per cui l\'alloggio non è destinato a portatori di handicap. Salvataggio non Effettuato!');</script>")
                    If chiamante.Value <> "1" Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    End If
                    Exit Function

                End If
            End If

            If Trim(cmbQuartiere.SelectedItem.Text) = "" Then
                Response.Write("<script>alert('Inserire il quartiere in cui si trova l\'alloggio. Se il quartiere non è presente nella lista, contattare l\'amministratore che provvederà all\'inserimento. Salvataggio non Effettuato!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function
            End If

            If (txtVani.Text = "" Or txtServizi.Text = "") And cmbAbusivo.SelectedItem.Text = "NO" Then
                Response.Write("<script>alert('Inserire il numero dei vani e dei servizi! Salvataggio non Effettuato!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function
            End If

            '*** 12/08/2015 ELIMINATA OBBLIGATORIETA' DATA PRESUNTA DI FINE LAVORI COME DA RICHIESTA 1194/2015
            'If chFineL.Checked = True And String.IsNullOrEmpty(Me.txtDataFine.Text) Then
            '    Response.Write("<script>alert('Inserire la data presunta di fine lavori! Salvataggio non Effettuato!');</script>")
            '    If chiamante.Value <> "1" Then
            '        par.cmd.Dispose()
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            '    End If
            '    Exit Function
            'End If
            '*** 12/08/2015 FINE ELIMINATA OBBLIGATORIETA' DATA PRESUNTA DI FINE LAVORI COME DA RICHIESTA 1194/2015

            If datapreSL_txt.Text = "" And dataSL_txt.Text <> "" Then
                datapreSL_txt.Text = dataSL_txt.Text
            End If

            If dataSL_txt.Text <> "" And txtDataSopralluogo.Text = "" Then


                Response.Write("<script>alert('Inserire la data di sopralluogo di sloggio! Salvataggio non Effettuato!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function


            End If

            If chGRTP.Checked = True And dataSL_txt.Text = "" Then
                Response.Write("<script>alert('Inserire la data di sloggio! Salvataggio non Effettuato!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function
            End If

            If dataSL_txt.Text <> "" And lblDataDisdetta.Text <> "" Then
                If par.AggiustaData(dataSL_txt.Text) < par.AggiustaData(lblDataDisdetta.Text) Then
                    Response.Write("<script>alert('ATTENZIONE...La data di SLOGGIO dovrebbe essere uguale o successiva alla data di disdetta!!');</script>")
                    mostrato = True
                    'If chiamante.Value <> "1" Then
                    '    par.cmd.Dispose()
                    '    par.OracleConn.Close()
                    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    'End If
                    'Exit Function
                End If
            End If

            '*** 12/08/2015 ELIMINATA OBBLIGATORIETA' DATA PRESUNTA DISPONIBILITA' COME DA RICHIESTA 1194/2015
            'If cmbStatoAlloggio.SelectedItem.Value = 1 And txtDataDisponibilita.Text = "" And cmbRiassegnabile.SelectedItem.Value <> "2" Then
            '    Response.Write("<script>alert('La data di disponibilità deve essere inserita se la UI è agibile! Salvataggio non Effettuato!');</script>")
            '    If chiamante.Value <> "1" Then
            '        par.cmd.Dispose()
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            '    End If
            '    Exit Function
            'End If
            '*** 12/08/2015 ELIMINATA OBBLIGATORIETA' DATA PRESUNTA DISPONIBILITA' COME DA RICHIESTA 1194/2015

            If txtDataDisponibilita.Text <> "" Then
                If idcontratto.Value <> "-1" And dataSL_txt.Text = "" And lblDataDisdetta.Text <> "---" And datapreSL_txt.Text = "" Then
                    Response.Write("<script>alert('La data di SLOGGIO deve essere inserita! Salvataggio non Effettuato!');</script>")
                    If chiamante.Value <> "1" Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    End If
                    Exit Function
                End If
            End If

            'If txtDataDisponibilita.Text <> "" And dataSL_txt.Text <> "" Then
            '    If par.AggiustaData(dataSL_txt.Text) > par.AggiustaData(txtDataDisponibilita.Text) Then
            '        Response.Write("<script>alert('La data di DISPONIBILITA deve essere successiva alla data SLOGGIO! Salvataggio non Effettuato!');</script>")
            '        If chiamante.Value <> "1" Then
            '            par.cmd.Dispose()
            '            par.OracleConn.Close()
            '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            '        End If
            '        Exit Function
            '    End If
            'End If

            If txtImpDanni.Text = "" Or txtImpTrasporto.Text = "" Then
                Response.Write("<script>alert('Inserire l\'importo dei danni e del trasporto. Specificare 0,00 se non si conoscono! Salvataggio non Effettuato!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function
            End If


            If chFineL.Checked = True And par.AggiustaData(txtDataDisponibilita.Text) > Format(Now, "yyyyMMdd") Then
                Response.Write("<script>alert('Se è stata specificata la fine dei lavori, la data di disponibilità deve essere precedente o uguale alla data odierna! Salvataggio non Effettuato!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function
            End If

            If Me.Chk_SL.Checked = True And String.IsNullOrEmpty(Me.dataSL_txt.Text) Then
                Response.Write("<script>alert('Per completare lo sloggio avvalorare il campo DATA SLOGGIO!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function

            End If
            If par.AggiustaData(Me.dataSL_txt.Text) > Format(Now, "yyyyMMdd") Then
                Response.Write("<script>alert('La DATA SLOGGIO deve essere inferiore o uguale alla data odierna!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
                Exit Function
            End If


            '15/10/2015 Aggiunto controllo per rendere obbligatorio l'ammontare dei danni
            If Me.Chk_SL.Checked = True And chAutorizzati.Checked = False Then
                Response.Write("<script>alert('Per completare lo sloggio autorizzare l\'ammontare dei danni!');</script>")
                If chiamante.Value <> "1" Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Exit Function
            End If




            'controlli date ::::::::::::::::::::::::::::::::::::::::::::

            If datapreSL_txt.Text <> "" Or dataSL_txt.Text <> "" Then


                'If par.AggiustaData(datapreSL_txt.Text) <> "" And par.AggiustaData(lblDataDisdetta.Text) <> "" Then
                '    If par.AggiustaData(datapreSL_txt.Text) < par.AggiustaData(lblDataDisdetta.Text) Then
                '        Response.Write("<script>alert('La data di Pre-sloggio dovrebbe essere successiva o uguale alla data di disdetta!');</script>")
                '        ''''Exit Function
                '    End If
                'End If


                If par.AggiustaData(dataSL_txt.Text) <> "" And par.AggiustaData(lblDataDisdetta.Text) <> "" Then
                    If par.AggiustaData(dataSL_txt.Text) < par.AggiustaData(lblDataDisdetta.Text) Then
                        Response.Write("<script>alert('La data di Sloggio dovrebbe essere successiva o uguale alla data di disdetta!');</script>")
                        'Exit Function
                    End If
                End If



                If par.AggiustaData(dataSL_txt.Text) <> "" And par.AggiustaData(datapreSL_txt.Text) <> "" Then
                    If par.AggiustaData(dataSL_txt.Text) < par.AggiustaData(datapreSL_txt.Text) Then
                        Response.Write("<script>alert('La data di Sloggio deve essere successiva o uguale alla data di Pre-sloggio!\nOperazione non effettuata!');</script>")
                        Exit Function
                    End If
                End If


                If par.AggiustaData(dataSL_txt.Text) <> "" And par.AggiustaData(txtDataSopralluogo.Text) <> "" Then
                    If par.AggiustaData(txtDataSopralluogo.Text) < par.AggiustaData(dataSL_txt.Text) Then
                        If dataSL_txt.Text = datapreSL_txt.Text Then
                            txtDataSopralluogo.Text = dataSL_txt.Text
                        Else
                            Response.Write("<script>alert('La data di sopralluogo deve essere successiva o uguale alla data di Sloggio!\nOperazione non effettuata!');</script>")
                            Exit Function
                        End If
                    End If
                End If

                If par.AggiustaData(dataSL_txt.Text) <> "" Then
                    If RUdataDecorrenza.Value > par.AggiustaData(dataSL_txt.Text) Then
                        Response.Write("<script>alert('La data di SLOGGIO deve essere successiva o uguale alla data di decorrenza del contratto!\nOperazione non effettuata!');</script>")
                        Exit Function
                    End If
                End If

            End If




            '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::







            If Destinazione <> "5" Then
                par.cmd.CommandText = "SELECT * FROM alloggi WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                Dim myReader33 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader33.HasRows = False Then
                    InserisciAlloggio()
                End If
                myReader33.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UI_USI_DIVERSI WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                Dim myReader33 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader33.HasRows = False Then
                    InserisciAlloggio()
                End If
                myReader33.Close()
            End If



            If Inserimento = -1 Then
                par.cmd.CommandText = "insert into siscom_mi.unita_stato_manutentivo (id_unita) values (" & idunita.Value & ")"
                par.cmd.ExecuteNonQuery()
                Inserimento = idunita.Value
            End If

            If chGP.Checked = True Then
                Altro = Altro & "SOL_GP=1,"
            Else
                Altro = Altro & "SOL_GP=0,"
            End If

            If ChGF.Checked = True Then
                Altro = Altro & "SOL_GF=1,"
            Else
                Altro = Altro & "SOL_GF=0,"
            End If

            If ChPB.Checked = True Then
                Altro = Altro & "SOL_PB=1,"
            Else
                Altro = Altro & "SOL_PB=0,"
            End If

            If ChPB1.Checked = True Then
                Altro = Altro & "SOL_PB1=1,"
            Else
                Altro = Altro & "SOL_PB1=0,"
            End If

            If ChPB2.Checked = True Then
                Altro = Altro & "SOL_PB2=1,"
            Else
                Altro = Altro & "SOL_PB2=0,"
            End If

            If ChPB3.Checked = True Then
                Altro = Altro & "SOL_PB3=1,"
            Else
                Altro = Altro & "SOL_PB3=0,"
            End If


            If ChLA.Checked = True Then
                Altro = Altro & "SOL_LA=1,"
            Else
                Altro = Altro & "SOL_LA=0,"
            End If

            If ChLA1.Checked = True Then
                Altro = Altro & "SOL_LA1=1,"
            Else
                Altro = Altro & "SOL_LA1=0,"
            End If


            If ChAL.Checked = True Then
                Altro = Altro & "SOL_AL=1, "
            Else
                Altro = Altro & "SOL_AL=0, "
            End If

            If ChAllarme.Checked = True Then
                Altro = Altro & "ALLARME=1, "
            Else
                Altro = Altro & "ALLARME=0, "
            End If

            If chFineL.Checked = True Then
                Altro = Altro & "FINE_LAVORI=1, "
            Else
                Altro = Altro & "FINE_LAVORI=0, "
            End If

            If chGRTP.Checked = True Then
                Altro = Altro & "REC_GRTP=1, "
            Else
                Altro = Altro & "REC_GRTP=0, "
            End If

            If chAutorizzati.Checked = True Then
                Altro = Altro & "FL_AUTORIZZATI_IMP=1 "
            Else
                Altro = Altro & "FL_AUTORIZZATI_IMP=0 "
            End If

            If chAutorizzati.Checked = True And txtDataQuantificazione.Text = "" Then
                txtDataQuantificazione.Text = Format(Now, "dd/MM/yyyy")
            End If

            'ST_DEP_CHIAVI
            Dim ST_DEP As String = "NULL"

            If Rd1.Checked = True Then
                ST_DEP = "0"
            End If

            If Rd2.Checked = True Then
                ST_DEP = "1"
            End If

            If Rd3.Checked = True Then
                ST_DEP = "2"
            End If

            If Rd4.Checked = True Then
                ST_DEP = "3"
            End If

            par.cmd.CommandText = "update siscom_mi.unita_stato_manutentivo set DATA_CONSEGNA_STR='" & par.AggiustaData(txtDataSTDal.Text) & "',DATA_RIPRESA_STR='" & par.AggiustaData(txtDataSTAl.Text) & "'," _
                & "ID_STRUTTURA_COMP=" & cmbStruttura.SelectedItem.Value & ",ST_DEP_CHIAVI=" & ST_DEP & ", note_sicurezza='" & par.PulisciStrSql(txtNoteSicurezza.Text) & "'," _
                & " note_tipo_porta='" & par.PulisciStrSql(txtNoteTipoPorta.Text) & "', fl_abusivo='" & cmbAbusivo.SelectedItem.Value & "'," _
                & " IMPORTO_TRASPORTO=" & par.VirgoleInPunti(txtImpTrasporto.Text) & ", IMPORTO_DANNI=" & par.VirgoleInPunti(txtImpDanni.Text) & "," _
                & " NOTE_DANNI='" & par.PulisciStrSql(txtNoteDanni.Text) & "',DATA_Q='" & par.AggiustaData(txtDataQuantificazione.Text) & "'," _
                & " Tipo_Riassegnabile='" & cmbRiassegnabile.SelectedItem.Value & "',NoteGRTP='" & par.PulisciStrSql(txtNoteGRTP.Text) & "'," _
                & " data_rilevazione='" & Format(Now, "yyyyMMdd") & "',note='" & par.PulisciStrSql(txtNote.Text) & "', HANDICAP=" & cmbHandicap.SelectedItem.Value & "," _
                & " data_pre_sLOGGIO='" & par.AggiustaData(datapreSL_txt.Text) & "',data_s='" & par.AggiustaData(txtDataSopralluogo.Text) & "',riassegnabile=" & cmbStatoAlloggio.SelectedItem.Value & "," _
                & " p_blindata=0,data_PRE_S='" & par.AggiustaData(txtDataFine.Text) & "'," & Altro & ",MOTIVAZIONI='" & par.PulisciStrSql(txtMotivazioni.Text) & "',ZONA='" & cmbDecentramento.SelectedItem.Value & "'," _
                & " TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value & ",NUM_LOCALI='" & par.PulisciStrSql(txtVani.Text) & "',NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) & "'," _
                & " data_consegna_chiavi='" & par.AggiustaData(txtDataConsegnaChiavi.Text) & "',data_ripresa_chiavi='" & par.AggiustaData(txtDataRipresaChiavi.Text) & "'," _
                & " consegnate_a='" & par.PulisciStrSql(txtConsegnatea.Text) & "' where id_unita=" & idunita.Value
            par.cmd.ExecuteNonQuery()

            If idcontratto.Value <> "-1" Then
                '**** 04/09/2012 AGGIUNGO EVENTO DATA INSERIMENTO DATA FINE LAVORI *****
                If Not String.IsNullOrEmpty(txtDataFine.Text) And chFineL.Checked = True Then 'verifico che data fine sia pieno e anche chkfine sia checked, per scrivere l'evento sul contratto
                    ' che viene usato nel report delle ui disponibili amministrativamente
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES (" & idcontratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F187','" & txtDataFine.Text & "')"
                    par.cmd.ExecuteNonQuery()


                End If
                '**** 04/09/2012 (FINE) AGGIUNGO EVENTO DATA INSERIMENTO DATA FINE LAVORI *****

            End If


            par.cmd.CommandText = "update siscom_mi.complessi_immobiliari set id_quartiere=" & cmbQuartiere.SelectedItem.Value & " where id=" & IndiceComplesso
            par.cmd.ExecuteNonQuery()

            Dim ss As String

            If cmbCondominio.SelectedItem.Text = "NO" Then
                ss = "condominio=0,"
            Else
                ss = "condominio=1,"
            End If

            If cmbDirettaRisc.SelectedItem.Text = "NO" Then
                ss = ss & "gest_risc_dir=0"
            Else
                ss = ss & "gest_risc_dir=1"
            End If
            par.cmd.CommandText = "update siscom_mi.edifici set " & ss & " where id=" & IndiceEdificio
            par.cmd.ExecuteNonQuery()

            If cmbAbusivo.SelectedItem.Value = "1" Then
                POR = "0"
                EQ = "0"
                OA = "1"
                MO = "0"
                par.cmd.CommandText = "UPDATE ALLOGGI SET FL_OA='1',FL_MOD='0',FL_POR='0',EQCANONE='0', H_MOTORIO='" & cmbHandicap.SelectedItem.Value & "',ZONA='" & cmbDecentramento.SelectedItem.Value & "',NUM_LOCALI='" & par.PulisciStrSql(txtVani.Text) & "',NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) & "',TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value & " WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET FL_OA='1',FL_MOD='0',FL_POR='0',EQCANONE='0', H_MOTORIO='" & cmbHandicap.SelectedItem.Value & "',ZONA='" & cmbDecentramento.SelectedItem.Value & "',NUM_LOCALI='" & par.PulisciStrSql(txtVani.Text) & "',NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) & "',TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value & " WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                par.cmd.ExecuteNonQuery()

            Else
                Select Case Destinazione
                    Case 1, 6
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    Case 2
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "1"
                    Case 3
                        POR = "1"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                    Case 4
                        POR = "0"
                        EQ = "1"
                        OA = "0"
                        MO = "0"
                    Case 5
                        POR = "0"
                        EQ = "0"
                        OA = "0"
                        MO = "0"
                End Select

                par.cmd.CommandText = "UPDATE ALLOGGI SET FL_OA='" & OA & "',FL_MOD='" & MO & "',FL_POR='" & POR & "',EQCANONE='" & EQ & "',H_MOTORIO='" & cmbHandicap.SelectedItem.Value & "',ZONA='" & cmbDecentramento.SelectedItem.Value & "',NUM_LOCALI='" & par.PulisciStrSql(txtVani.Text) & "',NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) & "',TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value & " WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET FL_OA='" & OA & "',FL_MOD='" & MO & "',FL_POR='" & POR & "',EQCANONE='" & EQ & "',H_MOTORIO='" & cmbHandicap.SelectedItem.Value & "',ZONA='" & cmbDecentramento.SelectedItem.Value & "',NUM_LOCALI='" & par.PulisciStrSql(txtVani.Text) & "',NUM_SERVIZI='" & par.PulisciStrSql(txtServizi.Text) & "',TIPO_ALLOGGIO=" & cmbTipo.SelectedItem.Value & " WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                par.cmd.ExecuteNonQuery()


            End If

            'max 04/04/2017
            If chFineL.Checked = True Then
                par.cmd.CommandText = "select COD_STATO_CONS_LG_392_78 from siscom_mi.unita_immobiliari WHERE COD_UNITA_IMMOBILIARE='" & CODICE.Value & "'"
                Dim myReader333 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader333.Read Then
                    If par.IfNull(myReader333("COD_STATO_CONS_LG_392_78"), "") <> "NORMA" Then
                        par.cmd.CommandText = "UPDATE siscom_mi.unita_immobiliari SET COD_STATO_CONS_LG_392_78='NORMA' WHERE COD_UNITA_IMMOBILIARE='" & CODICE.Value & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idunita.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','STATO DI CONSERVAZIONE IMPOSTATO A NORMALE PER FINE LAVORI')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader333.Close()
                End If
            End If
            If dataSL_txt.Text <> "" And idcontratto.Value <> "-1" And Me.Chk_SL.Checked = True Then

                If dataSL_txt.Text <> "" And idcontratto.Value <> "-1" Then
                    If Request.QueryString("F") <> "" Then
                        Dim parRappUt As New CM.Global
                        parRappUt.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("F")), Oracle.DataAccess.Client.OracleConnection)
                        parRappUt.cmd = parRappUt.OracleConn.CreateCommand()
                        parRappUt.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("F")), Oracle.DataAccess.Client.OracleTransaction)
                        'parRappUt.cmd.Transaction = parRappUt.myTrans


                        parRappUt.cmd.CommandText = "update siscom_mi.rapporti_utenza set data_riconsegna='" & parRappUt.AggiustaData(dataSL_txt.Text) & "' where id=" & idcontratto.Value
                        parRappUt.cmd.ExecuteNonQuery()

                        'nuovo evento cond_avvisi per l'avviso dello sloggio al settore condomini
                        parRappUt.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & idunita.Value & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = parRappUt.cmd.ExecuteReader
                        Do While lettore.Read
                            parRappUt.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (7," & idunita.Value & ",'" & Format(Now, "yyyyMMdd") & "',0," & lettore("ID_CONDOMINIO") & "," & idcontratto.Value & ")"
                            parRappUt.cmd.ExecuteNonQuery()
                        Loop
                        lettore.Close()

                        parRappUt.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idcontratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F180','" & dataSL_txt.Text & "')"
                        parRappUt.cmd.ExecuteNonQuery()


                        parRappUt.myTrans.Commit()
                        parRappUt.myTrans = parRappUt.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & Request.QueryString("F"), parRappUt.myTrans)
                        parRappUt.Dispose()

                        Session.Add("dataRic", dataSL_txt.Text)
                        Response.Write("<script>if (typeof opener.opener != 'undefined'){opener.opener.document.getElementById('txtModificato').value = '1';opener.opener.document.getElementById('imgSalva').click();}</script>")

                    Else
                        par.cmd.CommandText = "update siscom_mi.rapporti_utenza set data_riconsegna='" & par.AggiustaData(dataSL_txt.Text) & "' where id=" & idcontratto.Value
                        par.cmd.ExecuteNonQuery()
                        'nuovo evento cond_avvisi per l'avviso dello sloggio al settore condomini
                        par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & idunita.Value & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Do While lettore.Read
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (7," & idunita.Value & ",'" & Format(Now, "yyyyMMdd") & "',0," & lettore("ID_CONDOMINIO") & "," & idcontratto.Value & ")"
                            par.cmd.ExecuteNonQuery()
                        Loop
                        lettore.Close()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idcontratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F180','" & dataSL_txt.Text & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

            End If
            'If Me.Chk_SL.Checked = True Then

            If cmbStatoAlloggio.SelectedItem.Value = "0" Then
                    par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='NAGI' where id=" & idunita.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE ALLOGGI set STATO='12' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi set STATO='12' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>window.opener.document.getElementById('form1').DrLDisponib.value = 'NAGI';</script>")
                    Response.Write("<script>window.opener.document.getElementById('form1').statodisp.value = 'NAGI';</script>")
                Else

                par.cmd.CommandText = "SELECT siscom_mi.getstatocontratto(rapporti_utenza.id) as statoc,rapporti_utenza.id,RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_RICONSEGNA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_UNITA=" & idunita.Value & " AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL order by rapporti_utenza.data_riconsegna desc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        statoc.Value = par.IfNull(myReader3("statoc"), "")
                    End If
                    myReader3.Close()

                    If statoc.Value = "CHIUSO" Or statoc.Value = "" Or statoc.Value = "IN CORSO (S.T.)" Then

                        par.cmd.CommandText = "SELECT stato from alloggi where cod_alloggio='" & CODICE.Value & "'"
                        Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader4.Read Then
                            If par.IfNull(myReader4("stato"), "") = "7" Then
                                par.cmd.CommandText = "UPDATE ALLOGGI set data_reso='" & par.AggiustaData(txtDataDisponibilita.Text) & "',data_disponibilita='" & par.AggiustaData(txtDataDisponibilita.Text) & "' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "UPDATE ALLOGGI set data_reso='" & par.AggiustaData(txtDataDisponibilita.Text) & "',data_disponibilita='" & par.AggiustaData(txtDataDisponibilita.Text) & "',PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null,stato='5' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If
                        myReader4.Close()

                        par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET data_disponibilita='" & par.AggiustaData(txtDataDisponibilita.Text) & "',PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null,stato='5' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                        par.cmd.ExecuteNonQuery()

                        'libero unità immobiliare
                        par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='LIBE' where id=" & idunita.Value
                        par.cmd.ExecuteNonQuery()

                        'libero pertinenze
                        par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='LIBE' where id_unita_principale=" & idunita.Value
                        par.cmd.ExecuteNonQuery()


                        Response.Write("<script>window.opener.document.getElementById('form1').DrLDisponib.value = 'LIBE';</script>")
                        Response.Write("<script>window.opener.document.getElementById('form1').statodisp.value = 'LIBE';</script>")
                    End If

                    If cmbRiassegnabile.SelectedItem.Value = "2" Then
                        par.cmd.CommandText = "UPDATE ALLOGGI set STATO='12' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi set STATO='12' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If statoc.Value = "BOZZA" Then
                        par.cmd.CommandText = "UPDATE ALLOGGI set data_reso='" & par.AggiustaData(txtDataDisponibilita.Text) & "',data_disponibilita='" & par.AggiustaData(txtDataDisponibilita.Text) & "' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi set data_reso='" & par.AggiustaData(txtDataDisponibilita.Text) & "',data_disponibilita='" & par.AggiustaData(txtDataDisponibilita.Text) & "' WHERE COD_ALLOGGIO='" & CODICE.Value & "'"
                        par.cmd.ExecuteNonQuery()
                    End If
                End If
            'End If


            Dim I As Integer = 0

            par.cmd.CommandText = "delete siscom_mi.unita_stato_manutentivo_INT  where ID_UNITA=" & idunita.Value
            par.cmd.ExecuteNonQuery()

            For I = 0 To chLavori.Items.Count - 1
                If chLavori.Items(I).Selected = True Then
                    par.cmd.CommandText = "INSERT INTO siscom_mi.unita_stato_manutentivo_INT (ID_UNITA,ID_INTERVENTO) VALUES (" & idunita.Value & "," & chLavori.Items(I).Value & ")"
                    par.cmd.ExecuteNonQuery()
                Else

                End If
            Next

            If EVENTO.Value = "1" And idcontratto.Value <> "" And idcontratto.Value <> "-1" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & idcontratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F24','')"
                par.cmd.ExecuteNonQuery()
                EVENTO.Value = "0"
            End If

            If resetta.Value = "1" And txtDataDisponibilita.Text <> "" Then
                'reset PROGRAMMA INTERVENTI
                par.cmd.CommandText = "SELECT nvl(ID_PRG_EVENTI,-1) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & idunita.Value
                Dim ID_PRG_EVENTI As String = par.IfNull(par.cmd.ExecuteScalar, "-1")
                If ID_PRG_EVENTI <> "-1" Then
                    Dim descrProgrammaIntervento As String = ""
                    par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.PROGRAMMAZIONE_INTERVENTI WHERE ID = " & ID_PRG_EVENTI
                    descrProgrammaIntervento = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idunita.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','PROGRAMMA INTERVENTI PRIMA DELLA FINE LAVORI: " & par.PulisciStrSql(descrProgrammaIntervento) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                par.cmd.CommandText = "update siscom_mi.unita_immobiliari set ID_PRG_EVENTI=null where id=" & idunita.Value
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idunita.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','RESET PROGRAMMA INTERVENTI PER FINE LAVORI')"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>window.opener.document.getElementById('form1').DrLStatoCens.value = 'NULL';</script>")
                End If

                'If chiamante.Value = "1" Then
                '    par.myTrans.Commit()
                'End If

                Modificato.Value = "0"

            'If Not String.IsNullOrEmpty(Me.lblDataDisdetta.Text) Then
            SalvaSloggio()
            'End If


            If chiamante.Value = "1" Then

                par.myTrans.Commit()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                'Response.Write("<script>window.opener.document.getElementById('form1').txtModificato.value = '1';</script>")
            Else
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            datasloggio.Value = par.AggiustaData(dataSL_txt.Text)

            Response.Write("<script>alert('Operazione Effettuata!');</script>")


        Catch ex As Exception
            If chiamante.Value = "1" Then


            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Label10.Visible = True
            Label10.Text = ex.Message
        End Try
    End Function


    Function RicavaContratto() As String

    End Function


    Protected Sub cmbStatoAlloggio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatoAlloggio.SelectedIndexChanged
        If cmbStatoAlloggio.SelectedItem.Value = 0 Then
            cmbRiassegnabile.SelectedIndex = -1
            cmbRiassegnabile.Items.FindByValue("2").Selected = True
            txtDataDisponibilita.Text = ""
            txtDataDisponibilita.Enabled = False
        Else
            txtDataDisponibilita.Enabled = True
            cmbRiassegnabile.SelectedIndex = -1
            cmbRiassegnabile.Items.FindByValue("0").Selected = True
        End If
    End Sub

    Protected Sub chFineL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chFineL.CheckedChanged
        If chFineL.Checked = True Then
            cmbRiassegnabile.SelectedIndex = -1
            cmbRiassegnabile.Items.FindByValue("1").Selected = True
            txtDataDisponibilita.Enabled = True
            txtDataDisponibilita.Text = Format(Now, "dd/MM/yyyy")
            cmbStatoAlloggio.SelectedIndex = -1
            cmbStatoAlloggio.Items.FindByValue("1").Selected = True
            Label30.Text = "Data Disponibilità"
        Else
            Label30.Text = "Data Presunta Disponibilità"
        End If
    End Sub

    Protected Sub cmbRiassegnabile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRiassegnabile.SelectedIndexChanged
        If cmbRiassegnabile.SelectedItem.Text = "--" Then
            'cmbStatoAlloggio.SelectedIndex = -1
            'cmbStatoAlloggio.Items.FindByValue("0").Selected = True
        Else
            cmbStatoAlloggio.SelectedIndex = -1
            cmbStatoAlloggio.Items.FindByValue("1").Selected = True
        End If
    End Sub

    Protected Sub cmbCondominio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCondominio.SelectedIndexChanged
        If cmbCondominio.SelectedItem.Value = "1" Then
            cmbDirettaRisc.Enabled = True
        Else
            cmbDirettaRisc.SelectedIndex = -1
            cmbDirettaRisc.Items.FindByValue("NULL").Selected = True
            cmbDirettaRisc.Enabled = False
        End If
    End Sub

    Protected Sub cmbAbusivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAbusivo.SelectedIndexChanged
        If cmbAbusivo.SelectedItem.Value = "1" Then
            cmbRiassegnabile.SelectedIndex = -1
            cmbRiassegnabile.Items.FindByValue("1").Selected = True
            txtDataDisponibilita.Enabled = True
            txtDataDisponibilita.Text = Format(Now, "dd/MM/yyyy")
            cmbStatoAlloggio.SelectedIndex = -1
            cmbStatoAlloggio.Items.FindByValue("1").Selected = True
            Label30.Text = "Data Disponibilità"
        End If
    End Sub

    Private Sub CaricaZone()
        cmbDecentramento.Items.Clear()
        cmbDecentramento.Enabled = False
        par.cmd.CommandText = "SELECT ZONA_ALER.* FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,ZONA_ALER WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND ZONA_ALER.COD = EDIFICI.ID_ZONA AND UNITA_IMMOBILIARI.ID=" & idunita.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            'cmbDecentramento.Items.FindByText(par.IfNull(myReader("ZONA"), "01")).Selected = True
            cmbDecentramento.Items.Add(New ListItem(par.IfNull(myReader("ZONA"), "01"), par.IfNull(myReader("COD"), "")))
        Else
            cmbDecentramento.Items.Add(New ListItem("", ""))
        End If
        myReader.Close()
    End Sub


    Private Sub CaricaDatiSloggio()
        Try
            par.cmd.CommandText = "select SISCOM_MI.SL_SLOGGIO.PORTA_BLINDATA, SISCOM_MI.SL_SLOGGIO.APERTURA, SISCOM_MI.SL_SLOGGIO.SOPRALUCE, SISCOM_MI.SL_SLOGGIO.SOST_SERRATURA_PORTA, " _
                                     & "SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_FINESTRA, SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_PORTA_FINESTRA, SISCOM_MI.SL_SLOGGIO.SOST_SERRANDA_BOX, " _
                                     & "SISCOM_MI.SL_SLOGGIO.NUM_SOST_SERRATURA_NEGOZIO, SISCOM_MI.SL_SLOGGIO.DATA_PRE_SLOGGIO, SISCOM_MI.SL_SLOGGIO.DATA_SLOGGIO, SISCOM_MI.SL_SLOGGIO.STATO_VERBALE,SL_SLOGGIO.TOT_RAPPORTO_SLOGGIO " _
                                     & " from SISCOM_MI.SL_SLOGGIO " _
                                     & " where SL_SLOGGIO.ID = '" & idSloggio.Value & "' AND " _
                                     & "SL_SLOGGIO.ID_UNITA_IMMOBILIARE = " & idunita.Value

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If lettore.Read Then

                Dim supporto As Integer = 0

                supporto = par.IfNull(lettore("PORTA_BLINDATA"), 0)

                If supporto = 1 Then

                    Chk_PB.Checked = True
                    supporto = 0
                    supporto = par.IfNull(lettore("APERTURA"), 0)
                    Ddl_mano.SelectedValue = supporto

                    supporto = 0
                    supporto = par.IfNull(lettore("SOPRALUCE"), 0)
                    Ddl_sopL.SelectedValue = supporto


                Else
                    Chk_PB.Checked = False

                End If

                supporto = 0
                supporto = par.IfNull(lettore("SOST_SERRATURA_PORTA"), 0)
                If supporto = 1 Then

                    CheckBox4.Checked = True
                Else

                    CheckBox4.Checked = False

                End If


                supporto = 0
                supporto = par.IfNull(lettore("NUM_LASTRA_PROT_FINESTRA"), 0)
                If supporto <> 0 Then

                    Chk_nLF.Checked = True
                    lastraF_txt.Text = supporto
                Else

                    Chk_nLF.Checked = False

                End If


                supporto = 0
                supporto = par.IfNull(lettore("NUM_LASTRA_PROT_PORTA_FINESTRA"), 0)
                If supporto <> 0 Then

                    Chk_nLPF.Checked = True
                    lastraPF_txt.Text = supporto
                Else

                    Chk_nLPF.Checked = False

                End If




                supporto = 0
                supporto = par.IfNull(lettore("SOST_SERRANDA_BOX"), 0)
                If supporto = 1 Then

                    ChPB4.Checked = True
                Else

                    ChPB4.Checked = False

                End If




                supporto = 0
                supporto = par.IfNull(lettore("NUM_SOST_SERRATURA_NEGOZIO"), 0)
                If supporto <> 0 Then

                    Chk_nSerr.Checked = True
                    serr_txt.Text = supporto
                Else

                    Chk_nSerr.Checked = False

                End If

                '  Me.txtDanniVerbSl.Text = Format(par.IfNull(lettore("TOT_RAPPORTO_SLOGGIO"), 0), "##,##0.00")

                If datapreSL_txt.Text = "" Then
                    datapreSL_txt.Text = par.FormattaData(par.IfNull(lettore("DATA_PRE_SLOGGIO"), ""))
                End If
                dataSL_txt.Text = par.FormattaData(par.IfNull(lettore("DATA_SLOGGIO"), ""))

            End If
            ' lettore.Close()




            If idStatoSl.Value = 2 Then
                Chk_nLF.Enabled = False
                Chk_PB.Enabled = False
                Chk_nLPF.Enabled = False
                CheckBox4.Enabled = False
                Ddl_mano.Enabled = False
                Ddl_sopL.Enabled = False
                ChPB3.Enabled = False
                lastraF_txt.Enabled = False
                lastraPF_txt.Enabled = False
                Chk_nSerr.Enabled = False
                serr_txt.Enabled = False
                ' datapreSL_txt.Enabled = False
                ' dataSL_txt.Enabled = False
                btn_verbale.Visible = True
                Chk_SL.Checked = True
                Chk_SL.Enabled = False

                Me.txtDanniVerbSl.Text = Format(par.IfNull(lettore("TOT_RAPPORTO_SLOGGIO"), 0), "##,##0.00")

            End If

            lettore.Close()



        Catch ex As Exception
            If chiamante.Value = "1" Then


            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Label10.Visible = True
            Label10.Text = ex.Message
        End Try
    End Sub
    Private Sub SalvaSloggio()
        Try

            If idSloggio.Value = "0" Then
                par.cmd.CommandText = "insert into SISCOM_MI.SL_SLOGGIO " _
                                      & "(ID, ID_UNITA_IMMOBILIARE,ID_CONTRATTO, ID_STATO)" _
                                      & " values (SISCOM_MI.SEQ_SL_SLOGGIO.NEXTVAL, " & idunita.Value & "," & RitornaNullseMenoUno(idcontratto.Value) & ",'0')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SISCOM_MI.SEQ_SL_SLOGGIO.currval from dual"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idSloggio.Value = par.IfNull(lettore(0), "")
                End If
                lettore.Close()
            End If

            Dim stato As String = ""
            If Me.Chk_SL.Checked = True Then
                stato = 2
                idStatoSl.Value = stato

            Else
                stato = 0
                idStatoSl.Value = stato

            End If




            par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                   & " set PORTA_BLINDATA = " & ChkZerUno(Chk_PB) & ", APERTURA = " & ReturnNullSeMenoUno(Me.Ddl_mano.SelectedValue) & ", SOPRALUCE = " & ReturnNullSeMenoUno(Me.Ddl_sopL.SelectedValue) & ", " _
                                   & "SOST_SERRATURA_PORTA = '" & ChkZerUno(Me.CheckBox4) & "', NUM_LASTRA_PROT_FINESTRA = " & par.IfEmpty(Me.lastraF_txt.Text, "null") & ", " _
                                   & " NUM_LASTRA_PROT_PORTA_FINESTRA = " & par.IfEmpty(Me.lastraPF_txt.Text, "NULL") & ", SOST_SERRANDA_BOX = " & ChkZerUno(Me.ChPB4) & ", " _
                                   & " NUM_SOST_SERRATURA_NEGOZIO = " & par.IfEmpty(Me.serr_txt.Text, "null") & ", ID_STATO = " & stato & " , DATA_PRE_SLOGGIO = '" & par.AggiustaData(datapreSL_txt.Text) & "', DATA_SLOGGIO = '" & par.AggiustaData(dataSL_txt.Text) & "'" _
                                   & " where ID = " & idSloggio.Value


            par.cmd.ExecuteNonQuery()



            '::::::::::::::::::::::::::::::::::::::::::::: modifiche nuove


            'par.cmd.CommandText = "select DATA_APP_PRE_SLOGGIO, DATA_APP_RAPPORTO_SLOGGIO FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & idSloggio.Value

            'Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettore1.Read Then

            '    txtOraAppPresloggio.Value = Mid(par.IfNull(lettore1("DATA_APP_PRE_SLOGGIO"), ""), 9, 2) & ":" & Mid(par.IfNull(lettore1("DATA_APP_PRE_SLOGGIO"), ""), 11, 2)
            '    txtOraAppSloggio.Value = Mid(par.IfNull(lettore1("DATA_APP_RAPPORTO_SLOGGIO"), ""), 9, 2) & ":" & Mid(par.IfNull(lettore1("DATA_APP_RAPPORTO_SLOGGIO"), ""), 11, 2)


            'End If
            'lettore1.Close()


            'par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
            '                      & " SET DATA_APP_PRE_SLOGGIO = '" & par.AggiustaData(datapreSL_txt.Text) & txtOraAppPresloggio.Value.Replace(":", "").Replace(".", "") & "' , " _
            '                      & " DATA_APP_RAPPORTO_SLOGGIO = '" & par.AggiustaData(dataSL_txt.Text) & txtOraAppSloggio.Value.Replace(":", "").Replace(".", "") & "' " _
            '                      & "WHERE ID = " & idSloggio.Value


            'par.cmd.ExecuteNonQuery()

            ' ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::




            If stato = 0 Then
                SettaStatoControl()
            ElseIf stato = 2 Then
                DisattivaSloggio()
                Me.btn_verbale.Visible = True
            End If

        Catch ex As Exception
            If chiamante.Value = "1" Then


            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Label10.Visible = True
            Label10.Text = ex.Message

        End Try
    End Sub
    Private Function ChkZerUno(ByVal chk As CheckBox) As Integer
        ChkZerUno = 0

        If chk.Checked = True Then
            ChkZerUno = 1
        End If

        Return ChkZerUno
    End Function
    Private Function ReturnNullSeMenoUno(ByVal n As Integer) As String

        ReturnNullSeMenoUno = "null"
        If n <> -1 Then
            ReturnNullSeMenoUno = n
        End If

        Return ReturnNullSeMenoUno
    End Function
    Private Sub DisattivaSloggio()
        'Me.btn_verbale.Visible = False
        'Me.Chk_SL.Enabled = False
        Me.Chk_PB.Enabled = False
        Me.Ddl_mano.Enabled = False
        Me.Ddl_sopL.Enabled = False
        Me.CheckBox4.Enabled = False
        Me.Chk_nLF.Enabled = False
        Me.lastraF_txt.Enabled = False
        Me.Chk_nLPF.Enabled = False
        Me.lastraPF_txt.Enabled = False
        Me.ChPB4.Enabled = False
        Me.Chk_nSerr.Enabled = False
        Me.serr_txt.Enabled = False
        '  Me.datapreSL_txt.Enabled = False
        '  Me.dataSL_txt.Enabled = False
        Me.chGRTP.Enabled = False
        Me.txtNoteGRTP.Enabled = False

        '   Me.txtDataSopralluogo.Enabled = False


        par.cmd.CommandText = "select SISCOM_MI.SL_SLOGGIO.PORTA_BLINDATA, SISCOM_MI.SL_SLOGGIO.APERTURA, SISCOM_MI.SL_SLOGGIO.SOPRALUCE, SISCOM_MI.SL_SLOGGIO.SOST_SERRATURA_PORTA, " _
                                    & "SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_FINESTRA, SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_PORTA_FINESTRA, SISCOM_MI.SL_SLOGGIO.SOST_SERRANDA_BOX, " _
                                    & "SISCOM_MI.SL_SLOGGIO.NUM_SOST_SERRATURA_NEGOZIO, SISCOM_MI.SL_SLOGGIO.DATA_PRE_SLOGGIO, SISCOM_MI.SL_SLOGGIO.DATA_SLOGGIO, SISCOM_MI.SL_SLOGGIO.STATO_VERBALE,SL_SLOGGIO.TOT_RAPPORTO_SLOGGIO " _
                                    & " from SISCOM_MI.SL_SLOGGIO " _
                                    & " where SL_SLOGGIO.ID = '" & idSloggio.Value & "' AND " _
                                    & "SL_SLOGGIO.ID_UNITA_IMMOBILIARE = " & idunita.Value

        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        If lettore.Read Then
            Me.txtDanniVerbSl.Text = Format(par.IfNull(lettore("TOT_RAPPORTO_SLOGGIO"), 0), "##,##0.00")

        End If




    End Sub
    Private Sub SettaStatoControl()

        If Chk_PB.Checked = True Then

            Ddl_mano.Enabled = True
            Ddl_sopL.Enabled = True
        Else
            Ddl_mano.Enabled = False
            Ddl_sopL.Enabled = False

        End If

        If Chk_nLF.Checked = True Then

            lastraF_txt.Enabled = True
        Else
            lastraF_txt.Enabled = False
        End If

        If Chk_nLPF.Checked = True Then

            lastraPF_txt.Enabled = True
        Else

            lastraPF_txt.Enabled = False
        End If


        If Chk_nSerr.Checked = True Then

            serr_txt.Enabled = True
        Else
            serr_txt.Enabled = False

        End If
    End Sub


    Protected Sub TStampe_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles TStampe.MenuItemClick

        Select Case TStampe.SelectedValue

            Case 1
                Response.Write("<script>window.open('ModuloPresloggio.aspx?', 'PreSlogMod', '');</script>")
            Case 2
                Response.Write("<script>window.open('ModuloRitChiavi.aspx?', 'ModuloRitChiavi', '');</script>")
            Case 3
                Response.Write("<script>window.open('ModuloRappSloggio.aspx?ID= " & idunita.Value & "' , 'ModuloRappSloggio', '');</script>")
            Case 4
                Response.Write("<script>window.open('ModuloPromUtente.aspx?PROV=0&IDSLOGGIO= " & idSloggio.Value & "' , 'ModuloRappSloggio', '');</script>")
        End Select

    End Sub
    Private Function RitornaNullseMenoUno(ByVal v As String) As String
        If par.IfEmpty(v, 0) = -1 Then
            RitornaNullseMenoUno = "null"
        ElseIf par.IfEmpty(v, 0) = 0 Then
            RitornaNullseMenoUno = "null"
        Else
            RitornaNullseMenoUno = v
        End If

    End Function


End Class
