
Partial Class CENSIMENTO_GestSloggio
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim vId As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If



        'misura_txt.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

        'misura_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
        'misura_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


        'perc_txt.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimalPerc(this);")

        'perc_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
        'perc_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")



        lastraF_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
        lastraPF_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
        serr_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

        datapreSL_txt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        dataSL_txt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        btn_verbale.Visible = False

       


        If Not IsPostBack Then

            'Ddl_mano.Enabled = False
            'Ddl_sopL.Enabled = False
            'lastraF_txt.Enabled = False
            'lastraPF_txt.Enabled = False
            'serr_txt.Enabled = False




            provenienza.Value = Request.QueryString("PROVENIENZA")

            If Session.Item("CONT_DISDETTE") = "1" Then
                lettura.Value = "2"
            End If


            If provenienza.Value = 0 Then


                idunita.Value = Request.QueryString("ID")
                EVENTO.Value = Request.QueryString("T")
                idcontratto.Value = Request.QueryString("C")
                chiamante.Value = Request.QueryString("A")
                CaricaNuovoSloggio()

            Else

                idunita.Value = Request.QueryString("ID")
                id_stato.Value = Request.QueryString("IDSTATO")
                id_sloggio.Value = Request.QueryString("IDSLOGGIO")

               

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





            End If



            'If ControllaStato() = True Then
            CaricaDati()

            'Else
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Impossibile procedere! Una procedura di sloggio risulta in sospeso'); window.close();", True)


            '  ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Close_Window", "window.close();", True)



            '  End If

            Session.Add("INDIRIZZOUNITA", Label21.Text)
            Session.Add("CODICEUI", CODICE.Value)
            Session.Add("QUARTIERE", quartiere.Value)
            Session.Add("VIA", via_civico.Value)
            Session.Add("SCALA", scala.Value)
            Session.Add("PIANO", piano.Value)
            Session.Add("SUP_MQ", sup_mq.Value)
            Session.Add("INTERNO", interno.Value)



            'Dim CTRL As Control
            'For Each CTRL In Me.form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
            '    ElseIf TypeOf CTRL Is CheckBox Then
            '        DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")
            '    End If
            'Next
        End If

        If id_stato.Value = 1 Or id_stato.Value = 2 Then

            btn_verbale.Visible = True

        End If

    End Sub



    Private Sub CaricaNuovoSloggio()


        Try
            'Richiamo la connessione
           

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)

            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "insert into SISCOM_MI.SL_SLOGGIO " _
                                                  & "(ID, ID_UNITA_IMMOBILIARE, ID_STATO)" _
                                                  & " values (SISCOM_MI.SEQ_SL_SLOGGIO.NEXTVAL, '" & idunita.Value & "','0')"


            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            AssegnaID()

           






        Catch ex As Exception
            If chiamante.Value = "1" Then

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try




    End Sub


    Private Sub AssegnaID()


        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)

            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT SL_SLOGGIO.ID FROM SISCOM_MI.SL_SLOGGIO WHERE sl_sloggio.ID_STATO=0 AND SL_SLOGGIO.ID_UNITA_IMMOBILIARE= " & idunita.Value

            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '
                id_sloggio.Value = par.IfNull(myReader3("ID"), "")
                id_stato.Value = 0



            End If

            myReader3.Close()




        Catch ex As Exception
            If chiamante.Value = "1" Then

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try




    End Sub



   
    Private Function CaricaDati()
        Try



            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Request.QueryString("L") = 2 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
            End If







            par.cmd.CommandText = "select unita_immobiliari.id_unita_principale, tipologia_unita_immobiliari.descrizione as tipounita,unita_immobiliari.cod_tipologia,unita_immobiliari.cod_tipo_disponibilita,complessi_immobiliari.id as idq, identificativi_catastali.superficie_mq as SUP_MQ ,COMPLESSI_IMMOBILIARI.ID_QUARTIERE AS ID_QUART, TAB_QUARTIERI.NOME AS NOME_QUART, edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.id as idunita,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipologia_unita_immobiliari, siscom_mi.identificativi_catastali, siscom_mi.tipo_livello_piano,siscom_mi.tab_quartieri,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and COMPLESSI_IMMOBILIARI.ID_QUARTIERE = tab_quartieri.id and unita_immobiliari.id_catastale=identificativi_catastali.id (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.ID=" & idunita.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '
                CODICE.Value = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")


                Label21.Text = "Codice: " & CODICE.Value & " (" & par.IfNull(myReader3("tipounita"), "") & ") Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") _
                 & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & " Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")

                via_civico.Value = par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "")
                scala.Value = par.IfNull(myReader3("scala"), "")
                piano.Value = par.IfNull(myReader3("miopiano"), "")
                quartiere.Value = par.IfNull(myReader3("nome_quart"), "")
                sup_mq.Value = par.IfNull(myReader3("sup_mq"), "")
                interno.Value = par.IfNull(myReader3("interno"), "")





                If par.IfNull(myReader3("fl_piano_vendita"), "0") = "1" Then
                    lblPianoVendita.Visible = True
                    lblPianoVendita.Text = "<b>Unità inserita nel piano vendita!!</b>"
                Else
                    lblPianoVendita.Visible = False
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



                End If


              


               



            End If
            myReader3.Close()









            If id_stato.Value = 0 Then



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


            



                End If











                If id_stato.Value = 1 Then


                    'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "select SISCOM_MI.SL_SLOGGIO.PORTA_BLINDATA, SISCOM_MI.SL_SLOGGIO.APERTURA, SISCOM_MI.SL_SLOGGIO.SOPRALUCE, SISCOM_MI.SL_SLOGGIO.SOST_SERRATURA_PORTA, " _
                                                             & "SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_FINESTRA, SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_PORTA_FINESTRA, SISCOM_MI.SL_SLOGGIO.SOST_SERRANDA_BOX, " _
                                                             & "SISCOM_MI.SL_SLOGGIO.NUM_SOST_SERRATURA_NEGOZIO, SISCOM_MI.SL_SLOGGIO.DATA_PRE_SLOGGIO, SISCOM_MI.SL_SLOGGIO.DATA_SLOGGIO, SISCOM_MI.SL_SLOGGIO.STATO_VERBALE " _
                                                         & " from SISCOM_MI.SL_SLOGGIO " _
                                                         & " where SL_SLOGGIO.ID = '" & id_sloggio.Value & "' AND " _
                                                         & "SL_SLOGGIO.ID_UNITA_IMMOBILIARE = '" & idunita.Value & "' AND " _
                                                         & "SL_SLOGGIO.ID_STATO = 1 "




                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader11.Read Then


                        stato_verb.Value = par.IfNull(myReader11("STATO_VERBALE"), "")

                        Dim supporto = 0

                        supporto = par.IfNull(myReader11("PORTA_BLINDATA"), "")

                        If supporto = 1 Then

                            Chk_PB.Checked = True
                            supporto = 0
                            supporto = par.IfNull(myReader11("APERTURA"), "")
                            Ddl_mano.SelectedValue = supporto

                            supporto = 0
                            supporto = par.IfNull(myReader11("SOPRALUCE"), "")
                            Ddl_sopL.SelectedValue = supporto


                        Else
                            Chk_PB.Checked = False

                        End If


                        supporto = 0
                        supporto = par.IfNull(myReader11("SOST_SERRATURA_PORTA"), "")
                        If supporto = 1 Then

                            ChPB3.Checked = True
                        Else

                            ChPB3.Checked = False

                        End If


                        supporto = 0
                        supporto = par.IfNull(myReader11("NUM_LASTRA_PROT_FINESTRA"), "")
                        If supporto <> 0 Then

                            Chk_nLF.Checked = True
                            lastraF_txt.Text = supporto
                        Else

                            Chk_nLF.Checked = False

                        End If


                        supporto = 0
                        supporto = par.IfNull(myReader11("NUM_LASTRA_PROT_PORTA_FINESTRA"), "")
                        If supporto <> 0 Then

                            Chk_nLPF.Checked = True
                            lastraPF_txt.Text = supporto
                        Else

                            Chk_nLPF.Checked = False

                        End If




                        supporto = 0
                        supporto = par.IfNull(myReader11("SOST_SERRANDA_BOX"), "")
                        If supporto = 1 Then

                            CheckBox4.Checked = True
                        Else

                            CheckBox4.Checked = False

                        End If




                        supporto = 0
                        supporto = par.IfNull(myReader11("NUM_SOST_SERRATURA_NEGOZIO"), "")
                        If supporto <> 0 Then

                            Chk_nSerr.Checked = True
                            serr_txt.Text = supporto
                        Else

                            Chk_nSerr.Checked = False

                        End If



                        datapreSL_txt.Text = par.FormattaData(par.IfNull(myReader11("DATA_PRE_SLOGGIO"), ""))
                        dataSL_txt.Text = par.FormattaData(par.IfNull(myReader11("DATA_SLOGGIO"), ""))







                    End If


                    Chk_preSL.Checked = True
                    Chk_nLF.Enabled = False
                    Chk_preSL.Enabled = False
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
                    datapreSL_txt.Enabled = False
                    dataSL_txt.Enabled = False
                    btn_verbale.Visible = True

                    myReader11.Close()






                End If














                If id_stato.Value = 2 Then


                    'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "select SISCOM_MI.SL_SLOGGIO.PORTA_BLINDATA, SISCOM_MI.SL_SLOGGIO.APERTURA, SISCOM_MI.SL_SLOGGIO.SOPRALUCE, SISCOM_MI.SL_SLOGGIO.SOST_SERRATURA_PORTA, " _
                                                             & "SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_FINESTRA, SISCOM_MI.SL_SLOGGIO.NUM_LASTRA_PROT_PORTA_FINESTRA, SISCOM_MI.SL_SLOGGIO.SOST_SERRANDA_BOX, " _
                                                             & "SISCOM_MI.SL_SLOGGIO.NUM_SOST_SERRATURA_NEGOZIO, SISCOM_MI.SL_SLOGGIO.DATA_PRE_SLOGGIO, SISCOM_MI.SL_SLOGGIO.DATA_SLOGGIO " _
                                                         & " from SISCOM_MI.SL_SLOGGIO " _
                                                         & " where SL_SLOGGIO.ID = '" & id_sloggio.Value & "' AND " _
                                                         & "SL_SLOGGIO.ID_UNITA_IMMOBILIARE = '" & idunita.Value & "' AND " _
                                                         & "SL_SLOGGIO.ID_STATO = 2 "




                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader11.Read Then

                        Dim supporto = 0

                        supporto = par.IfNull(myReader11("PORTA_BLINDATA"), "")

                        If supporto = 1 Then

                            Chk_PB.Checked = True
                            supporto = 0
                            supporto = par.IfNull(myReader11("APERTURA"), "")
                            Ddl_mano.SelectedValue = supporto

                            supporto = 0
                            supporto = par.IfNull(myReader11("SOPRALUCE"), "")
                            Ddl_sopL.SelectedValue = supporto


                        Else
                            Chk_PB.Checked = False

                        End If


                        supporto = 0
                        supporto = par.IfNull(myReader11("SOST_SERRATURA_PORTA"), "")
                        If supporto = 1 Then

                            ChPB3.Checked = True
                        Else

                            ChPB3.Checked = False

                        End If


                        supporto = 0
                        supporto = par.IfNull(myReader11("NUM_LASTRA_PROT_FINESTRA"), "")
                        If supporto <> 0 Then

                            Chk_nLF.Checked = True
                            lastraF_txt.Text = supporto
                        Else

                            Chk_nLF.Checked = False

                        End If


                        supporto = 0
                        supporto = par.IfNull(myReader11("NUM_LASTRA_PROT_PORTA_FINESTRA"), "")
                        If supporto <> 0 Then

                            Chk_nLPF.Checked = True
                            lastraPF_txt.Text = supporto
                        Else

                            Chk_nLPF.Checked = False

                        End If




                        supporto = 0
                        supporto = par.IfNull(myReader11("SOST_SERRANDA_BOX"), "")
                        If supporto = 1 Then

                            CheckBox4.Checked = True
                        Else

                            CheckBox4.Checked = False

                        End If




                        supporto = 0
                        supporto = par.IfNull(myReader11("NUM_SOST_SERRATURA_NEGOZIO"), "")
                        If supporto <> 0 Then

                            Chk_nSerr.Checked = True
                            serr_txt.Text = supporto
                        Else

                            Chk_nSerr.Checked = False

                        End If



                        datapreSL_txt.Text = par.FormattaData(par.IfNull(myReader11("DATA_PRE_SLOGGIO"), ""))
                        dataSL_txt.Text = par.FormattaData(par.IfNull(myReader11("DATA_SLOGGIO"), ""))







                    End If


                    Chk_preSL.Checked = True
                    Chk_nLF.Enabled = False
                    Chk_preSL.Enabled = False
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
                    datapreSL_txt.Enabled = False
                    dataSL_txt.Enabled = False
                    btn_verbale.Visible = True
                    Chk_SL.Checked = True
                    Chk_SL.Enabled = False
                    myReader11.Close()
                    btn_salva.Visible = False






                End If







        Catch ex As Exception
            If chiamante.Value = "1" Then

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Function


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




    


    Protected Sub btn_salva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_salva.Click

        Dim PB_val = 0
        Dim porta_val = 0
        Dim box_val = 0
        Dim lastraFin = 0
        Dim lastraPFin = 0
        Dim serr_val = 0


        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)

            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            If id_stato.Value = 1 Then



                If (Chk_SL.Checked = True) Then

                    par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                                            & " set ID_STATO = 2" _
                                                            & " where ID = '" & id_sloggio.Value & "'"


                    par.cmd.ExecuteNonQuery()




                    par.myTrans.Commit()
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


                    id_stato.Value = 2
                    stato_verb.Value = 1
                    btn_verbale.Visible = True
                    btn_salva.Visible = False
                    Chk_SL.Enabled = False

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Procedura di sloggio completata!');", True)

                End If

            End If









            If id_stato.Value = 0 Then


                If (Chk_SL.Checked = True) Then

                    If (Chk_preSL.Checked = False) Then

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! I dati relativi al pre-sloggio non sono stati immessi!');", True)

                    End If
                End If





                If (Chk_preSL.Checked = True) Then

                    If ControllaDatiPre() = True Then


                        If Chk_PB.Checked = True Then

                            PB_val = 1

                        End If

                        If ChPB3.Checked = True Then

                            box_val = 1

                        End If

                        If CheckBox4.Checked = True Then

                            porta_val = 1

                        End If

                        If lastraF_txt.Text <> "" Then


                            lastraFin = lastraF_txt.Text

                        End If


                        If lastraPF_txt.Text <> "" Then


                            lastraPFin = lastraPF_txt.Text

                        End If


                        If serr_txt.Text <> "" Then

                            serr_val = serr_txt.Text

                        End If







                        par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                                           & " set PORTA_BLINDATA = '" & PB_val & "', APERTURA = '" & Me.Ddl_mano.SelectedValue & "', SOPRALUCE = '" & Me.Ddl_sopL.SelectedValue & "', " _
                                                           & "SOST_SERRATURA_PORTA = '" & porta_val & "', NUM_LASTRA_PROT_FINESTRA = '" & lastraFin & "', " _
                                                           & " NUM_LASTRA_PROT_PORTA_FINESTRA = '" & lastraPFin & "', SOST_SERRANDA_BOX = '" & box_val & "', " _
                                                           & " NUM_SOST_SERRATURA_NEGOZIO = " & serr_val & ", ID_STATO = 1 , DATA_PRE_SLOGGIO = '" & par.AggiustaData(datapreSL_txt.Text) & "', DATA_SLOGGIO = '" & par.AggiustaData(dataSL_txt.Text) & "'" _
                                                           & " where ID = '" & id_sloggio.Value & "'"


                        par.cmd.ExecuteNonQuery()




                        par.myTrans.Commit()
                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


                        id_stato.Value = 1
                        stato_verb.Value = 0
                        btn_verbale.Visible = True

                        Chk_preSL.Checked = True
                        Chk_nLF.Enabled = False
                        Chk_preSL.Enabled = False
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
                        datapreSL_txt.Enabled = False
                        dataSL_txt.Enabled = False

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Procedura di pre-sloggio completata!');", True)





                    End If
                Else

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione! Selezionare la voce PRE-SLOGGIO COMPLETO per salvare!');", True)

                End If





            End If


        Catch ex As Exception
            If chiamante.Value = "1" Then

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Private Function ControllaDatiPre() As Boolean


        ControllaDatiPre = False

        Try

            If Chk_PB.Checked = True Then

                If Ddl_mano.SelectedValue = -1 Or Ddl_sopL.SelectedValue = -1 Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Specificare le caratteristiche della porta blindata!');", True)
                    ControllaDatiPre = False
                    Exit Function

                End If

            End If




            If Chk_nLF.Checked = True Then

                If lastraF_txt.Text = "" Then


                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Specificare il numero lastre di protezione finestra!');", True)
                    ControllaDatiPre = False

                    Exit Function

                End If


            End If


            If Chk_nLPF.Checked = True Then

                If lastraPF_txt.Text = "" Then


                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Specificare il numero lastre di protezione porta finestra!');", True)
                    ControllaDatiPre = False
                    Exit Function


                End If


            End If


            If Chk_nSerr.Checked = True Then

                If serr_txt.Text = "" Then


                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Specificare il numero di sostituzioni serratura serranda negozio!');", True)
                    ControllaDatiPre = False
                    Exit Function


                End If


            End If


            If datapreSL_txt.Text = "" Then


                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Inserire la data di PRE-SLOGGIO!');", True)
                ControllaDatiPre = False
                Exit Function

            End If


            If dataSL_txt.Text = "" Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Inserire la data di SLOGGIO!');", True)
                ControllaDatiPre = False
                Exit Function

            End If






            If IsDate(datapreSL_txt.Text) = False Then


                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('La data di PRE-SLOGGIO inserita non è corretta!');", True)
                ControllaDatiPre = False
                Exit Function

            End If



            If IsDate(dataSL_txt.Text) = False Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('La data di SLOGGIO inserita non è corretta!');", True)
                ControllaDatiPre = False
                Exit Function

            End If



            If datapreSL_txt.Text <> "" And dataSL_txt.Text <> "" Then

                If (dataSL_txt.Text.Length) < 10 Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('La data di SLOGGIO inserita non è corretta!');", True)
                    ControllaDatiPre = False
                    Exit Function

                End If


                If (datapreSL_txt.Text.Length) < 10 Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('La data di PRE-SLOGGIO inserita non è corretta!');", True)
                    ControllaDatiPre = False
                    Exit Function

                End If



                If par.AggiustaData(dataSL_txt.Text) < par.AggiustaData(datapreSL_txt.Text) Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('La data di SLOGGIO deve essere uguale o successiva alla data di PRE-SLOGGIO!');", True)
                    ControllaDatiPre = False
                    Exit Function
                End If
            End If




            ControllaDatiPre = True





        Catch ex As Exception
            If chiamante.Value = "1" Then

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try




    End Function



    Protected Sub TStampe_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles TStampe.MenuItemClick


           Select TStampe.SelectedValue

            Case 1

                Response.Write("<script>window.open('ModuloPresloggio.aspx?', 'Modulo', '');</script>")



            Case 2


                Response.Write("<script>window.open('ModuloRitChiavi.aspx?', 'Modulo', '');</script>")

            Case 3

                Response.Write("<script>window.open('ModuloRappSloggio.aspx?', 'Modulo', '');</script>")

        End Select

    End Sub




End Class
