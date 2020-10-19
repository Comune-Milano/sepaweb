
Partial Class CENSIMENTO_VerbaleSloggio
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public classetab As String = ""
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public tabdefault3 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            idunita.Value = Request.QueryString("ID")
            id_sloggio.Value = Request.QueryString("IDSLOGGIO")
            id_stato.Value = Request.QueryString("IDSTATO")
            chiamante.Value = Request.QueryString("A")
            If id_stato.Value = 2 Then
                salva_btn.Visible = False
            End If
            CaricaDati()
            If Request.QueryString("T") = 1 Then
                Me.lblTitolo.Text = "VERBALE DI CONSISTENZA"
            End If

            totAdd_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            totAdd_txt.Attributes.Add("onblur", "javascript:AutoDecimal2(this);")
            totAdd_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")

            stimaCosti_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            stimaCosti_txt.Attributes.Add("onblur", "javascript:AutoDecimal2(this);")
            stimaCosti_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")

            adNormativo_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
            adNormativo_txt.Attributes.Add("onblur", "javascript:AutoDecimal2(this);")
            adNormativo_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")

            stimaCosti_txt.Attributes.Add("onfocusin", "javascript:SettaTestoOncosto(this);")
            stimaCosti_txt.Attributes.Add("onfocusout", "javascript:SettaTestoOutcosto(this);")

            adNormativo_txt.Attributes.Add("onfocusin", "javascript:SettaTestoOncosto(this);")
            adNormativo_txt.Attributes.Add("onfocusout", "javascript:SettaTestoOutcosto(this);")




            If totAdd_txt.Text = "" Or totAdd_txt.Text = 0 Then
                totAdd_txt.Text = "0,00"
            Else
                totAdd_txt.Text = FormatNumber(totAdd_txt.Text, 2)
            End If

            If stimaCosti_txt.Text = "" Or stimaCosti_txt.Text = 0 Then
                stimaCosti_txt.Text = "0,00"
            Else
                stimaCosti_txt.Text = FormatNumber(stimaCosti_txt.Text, 2)
            End If
            If adNormativo_txt.Text = "" Or adNormativo_txt.Text = 0 Then
                adNormativo_txt.Text = "0,00"
            Else
                adNormativo_txt.Text = FormatNumber(adNormativo_txt.Text, 2)
            End If

            If id_stato.Value = 2 Then

                If InStr(totAdd_txt.Text, ",") = 0 Then
                    totAdd_txt.Text = totAdd_txt.Text & ",00"
                End If
                totAdd_txt.Enabled = False

                If InStr(stimaCosti_txt.Text, ",") = 0 Then
                    stimaCosti_txt.Text = stimaCosti_txt.Text & ",00"
                End If
                stimaCosti_txt.Enabled = False
                If InStr(adNormativo_txt.Text, ",") = 0 Then
                    adNormativo_txt.Text = adNormativo_txt.Text & ",00"
                End If
                adNormativo_txt.Enabled = False
            End If
            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")
                End If
            Next
        End If


    End Sub


    Private Sub CaricaDati()
        Try
            Label10.Visible = False

            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select unita_immobiliari.id_unita_principale, tipologia_unita_immobiliari.descrizione as tipounita,unita_immobiliari.cod_tipologia,unita_immobiliari.cod_tipo_disponibilita,complessi_immobiliari.id as idq, identificativi_catastali.superficie_mq as SUP_MQ ,COMPLESSI_IMMOBILIARI.ID_QUARTIERE AS ID_QUART, TAB_QUARTIERI.NOME AS NOME_QUART, edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.id as idunita,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipologia_unita_immobiliari, siscom_mi.identificativi_catastali, siscom_mi.tipo_livello_piano,siscom_mi.tab_quartieri,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and COMPLESSI_IMMOBILIARI.ID_QUARTIERE = tab_quartieri.id and unita_immobiliari.id_catastale=identificativi_catastali.id (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.ID=" & idunita.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '
                CODICE.Value = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")
                Label21.Text = "Codice Unità: " & CODICE.Value & "<br/>Tipologia:" & par.IfNull(myReader3("tipounita"), "") & "<br/>Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") & " " _
                 & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & "<br/>Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")
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

            par.cmd.CommandText = "SELECT SL_SLOGGIO.TOT_RAPPORTO_SLOGGIO AS TOT, SL_SLOGGIO.STIMA_COSTI AS STIMA, SL_SLOGGIO.AD_NORMATIVO AS NORM FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & id_sloggio.Value & ""

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                '
                totAdd_txt.Text = par.IfNull(lettore("TOT"), "")
                stimaCosti_txt.Text = par.IfNull(lettore("STIMA"), "")
                adNormativo_txt.Text = par.IfNull(lettore("NORM"), "")

            End If
            lettore.Close()









            par.cmd.CommandText = "SELECT SL_SLOGGIO.STATO_VERBALE AS ST_VERB FROM SISCOM_MI.SL_SLOGGIO WHERE SL_SLOGGIO.ID = " & id_sloggio.Value

            Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore1.Read Then
                stato_verb.Value = par.IfNull(lettore1("ST_VERB"), "")
            End If
            lettore1.Close()


 


            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

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


    Protected Sub salva_btn_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles salva_btn.Click

        Try
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'par.cmd.CommandText = "SELECT SL_SLOGGIO.STATO_VERBALE AS ST_VERB FROM SISCOM_MI.SL_SLOGGIO WHERE SL_SLOGGIO.ID = " & id_sloggio.Value

            'Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader3.Read Then
            '    stato_verb.Value = par.IfNull(myReader3("ST_VERB"), "")
            'End If
            'myReader3.Close()

            'caso 2

            If id_stato.Value >= 0 And stato_verb.Value = 1 Then



                ' salvataggio per tab1'

                For K As Integer = 0 To CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""



                    par.cmd.CommandText = "update SISCOM_MI.SL_RAPPORTO " _
                                                    & "set QUANTITA='" & DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                    & " TOTALE=" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                    & " CHK_1='" & st_1.Value & "',  CHK_2='" & st_2.Value & "',  CHK_3='" & st_3.Value & "', CHK_4='" & st_4.Value & "' " _
                                                    & "WHERE ID_SLOGGIO = " & id_sloggio.Value & "AND ID_DESC_ST_MANUT = " & CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & ""



                    par.cmd.ExecuteNonQuery()



                Next




                ' salvataggio per tab2'




                For K As Integer = 0 To CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "update SISCOM_MI.SL_RAPPORTO " _
                                                    & "set QUANTITA='" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text)) & "', " _
                                                    & " TOTALE=" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                    & " CHK_1='" & st_1.Value & "',  CHK_2='" & st_2.Value & "',  CHK_3='" & st_3.Value & "', CHK_4='" & st_4.Value & "' " _
                                                    & "WHERE ID_SLOGGIO = " & id_sloggio.Value & "AND ID_DESC_ST_MANUT = " & CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & ""


                    par.cmd.ExecuteNonQuery()



                Next






                ' salvataggio per tab3'




                For K As Integer = 0 To CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "update SISCOM_MI.SL_RAPPORTO " _
                                                  & "set QUANTITA='" & DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                  & " TOTALE=" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                  & " CHK_1='" & st_1.Value & "',  CHK_2='" & st_2.Value & "',  CHK_3='" & st_3.Value & "', CHK_4='" & st_4.Value & "' " _
                                                  & "WHERE ID_SLOGGIO = " & id_sloggio.Value & "AND ID_DESC_ST_MANUT = " & CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & ""


                    par.cmd.ExecuteNonQuery()



                Next









                ' salvataggio per tab4'




                For K As Integer = 0 To CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "update SISCOM_MI.SL_RAPPORTO " _
                                                  & "set QUANTITA='" & DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                  & " TOTALE=" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                  & " CHK_1='" & st_1.Value & "',  CHK_2='" & st_2.Value & "',  CHK_3='" & st_3.Value & "', CHK_4='" & st_4.Value & "' " _
                                                  & "WHERE ID_SLOGGIO = " & id_sloggio.Value & "AND ID_DESC_ST_MANUT = " & CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & ""


                    par.cmd.ExecuteNonQuery()



                Next









                ' salvataggio per tab5'




                If CType(Tab_Note1.FindControl("txtNote"), TextBox).Text <> "" Then


                    par.cmd.CommandText = ""

                    par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                                  & "set NOTE='" & par.PulisciStrSql(CType(Tab_Note1.FindControl("txtNote"), TextBox).Text) & "'" _
                                                  & "WHERE ID = " & id_sloggio.Value & ""


                    par.cmd.ExecuteNonQuery()


                End If



                par.cmd.CommandText = ""

                Dim condizioneLivello As String = ""
                If ReturnDdlLivello(CType(Tab_GeneraleUI1.FindControl("ddl_livello"), DropDownList)) <> "null" Then
                    condizioneLivello = " LIVELLO= '" & ReturnDdlLivello(CType(Tab_GeneraleUI1.FindControl("ddl_livello"), DropDownList)) & "', "

                Else
                    condizioneLivello = " LIVELLO= null, "

                End If


                Dim condizioneStCons As String = ""
                If ReturnDdlStConserv(CType(Tab_GeneraleUI1.FindControl("ddl_statocons"), DropDownList)) <> "null" Then
                    condizioneStCons = " cod_stato_conserv= '" & ReturnDdlStConserv(CType(Tab_GeneraleUI1.FindControl("ddl_statocons"), DropDownList)) & "', "
                Else
                    condizioneStCons = " cod_stato_conserv= null, "

                End If



                par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                              & "set tot_rapporto_sloggio=" & par.VirgoleInPunti(CDec(Me.totAdd_txt.Text)) & ", " _
                                              & " STIMA_COSTI= " & par.VirgoleInPunti(CDec(Me.stimaCosti_txt.Text)) & ", " _
                                              & " AD_NORMATIVO= " & par.VirgoleInPunti(CDec(Me.adNormativo_txt.Text)) & ", " _
                                              & " ASCENSORE= " & ReturnNullSeMenoUno(CType(Tab_GeneraleUI1.FindControl("ddl_ascensore"), DropDownList).SelectedValue) & ", " _
                                              & " SCIVOLI= " & ChkZerUno(CType(Tab_GeneraleUI1.FindControl("chk_scivoli"), CheckBox)) & ", " _
                                              & " MONTASCALE= " & ChkZerUno(CType(Tab_GeneraleUI1.FindControl("chk_montascale"), CheckBox)) & ", " _
                                              & " FORO_AREAZIONE= " & ChkZerUno(CType(Tab_GeneraleUI1.FindControl("chk_esistente"), CheckBox)) & ", " _
                                              & " LOCALE_FORO_AREAZ= '" & par.PulisciStrSql(CType(Tab_GeneraleUI1.FindControl("txt_locale"), TextBox).Text) & "', " _
                                              & condizioneStCons & condizioneLivello _
                                              & " RECUPERABILE= " & ReturnNullSeMenoUno(CType(Tab_GeneraleUI1.FindControl("ddl_UIRecuperabile"), DropDownList).SelectedValue) & " " _
                                              & " WHERE ID = " & id_sloggio.Value & ""


                par.cmd.ExecuteNonQuery()





                ''*********************CHIUSURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If


                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione completata!');", True)

                '  ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "opener.location.href('VerificaSManutentivo.aspx?A=" & chiamante.Value & "&ID= " & idunita.Value & "'); window.focus();", True)








            End If


            'caso 3

            If id_stato.Value >= 0 And stato_verb.Value = 0 Then



                ' salvataggio per tab1'

                For K As Integer = 0 To CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "insert into SISCOM_MI.SL_RAPPORTO " _
                                                     & "(ID, ID_SLOGGIO, ID_DESC_ST_MANUT, QUANTITA, TOTALE, CHK_1, CHK_2, CHK_3, CHK_4 )" _
                                                     & " values (SISCOM_MI.SEQ_SL_RAPPORTO.NEXTVAL, " & RitornaNullseZero(id_sloggio.Value) & ", '" & CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & "', " _
                                                     & "'" & DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                     & "" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                     & "'" & st_1.Value & "', '" & st_2.Value & "', '" & st_3.Value & "', '" & st_4.Value & "')"


                    par.cmd.ExecuteNonQuery()



                Next




                ' salvataggio per tab2'




                For K As Integer = 0 To CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "insert into SISCOM_MI.SL_RAPPORTO " _
                                                     & "(ID, ID_SLOGGIO, ID_DESC_ST_MANUT, QUANTITA, TOTALE, CHK_1, CHK_2, CHK_3, CHK_4 )" _
                                                     & " values (SISCOM_MI.SEQ_SL_RAPPORTO.NEXTVAL, " & RitornaNullseZero(id_sloggio.Value) & ", '" & CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & "', " _
                                                     & "'" & DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                     & "" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                     & "'" & st_1.Value & "', '" & st_2.Value & "', '" & st_3.Value & "', '" & st_4.Value & "')"


                    par.cmd.ExecuteNonQuery()



                Next






                ' salvataggio per tab3'




                For K As Integer = 0 To CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "insert into SISCOM_MI.SL_RAPPORTO " _
                                                     & "(ID, ID_SLOGGIO, ID_DESC_ST_MANUT, QUANTITA, TOTALE, CHK_1, CHK_2, CHK_3, CHK_4 )" _
                                                     & " values (SISCOM_MI.SEQ_SL_RAPPORTO.NEXTVAL, " & RitornaNullseZero(id_sloggio.Value) & ", '" & CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & "', " _
                                                     & "'" & DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                     & "" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                     & "'" & st_1.Value & "', '" & st_2.Value & "', '" & st_3.Value & "', '" & st_4.Value & "')"


                    par.cmd.ExecuteNonQuery()



                Next









                ' salvataggio per tab4'




                For K As Integer = 0 To CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1


                    st_1.Value = 0
                    st_2.Value = 0
                    st_3.Value = 0
                    st_4.Value = 0




                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True Then


                        st_1.Value = 1


                    End If


                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True Then


                        st_2.Value = 1


                    End If

                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True Then


                        st_3.Value = 1


                    End If

                    If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True Then


                        st_4.Value = 1


                    End If

                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "insert into SISCOM_MI.SL_RAPPORTO " _
                                                     & "(ID, ID_SLOGGIO, ID_DESC_ST_MANUT, QUANTITA, TOTALE, CHK_1, CHK_2, CHK_3, CHK_4 )" _
                                                     & " values (SISCOM_MI.SEQ_SL_RAPPORTO.NEXTVAL, " & RitornaNullseZero(id_sloggio.Value) & ", '" & CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(1).Text & "', " _
                                                     & "'" & DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text & "', " _
                                                     & "" & par.VirgoleInPunti(CDec(DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)) & ", " _
                                                     & "'" & st_1.Value & "', '" & st_2.Value & "', '" & st_3.Value & "', '" & st_4.Value & "')"


                    par.cmd.ExecuteNonQuery()



                Next





                'salvataggio per tab 5


                If CType(Tab_Note1.FindControl("txtNote"), TextBox).Text <> "" Then


                    par.cmd.CommandText = ""

                    par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                                  & "set NOTE='" & par.PulisciStrSql(CType(Tab_Note1.FindControl("txtNote"), TextBox).Text) & "'" _
                                                  & "WHERE ID = " & RitornaNullseZero(id_sloggio.Value) & ""


                    par.cmd.ExecuteNonQuery()


                End If












                par.cmd.CommandText = ""

                'par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                '                                             & " set STATO_VERBALE =1, tot_rapporto_sloggio=" & par.VirgoleInPunti(CDec(Me.totAdd_txt.Text)) & ", " _
                '                                             & " STIMA_COSTI= " & par.VirgoleInPunti(CDec(Me.stimaCosti_txt.Text)) & ", " _
                '                                             & " AD_NORMATIVO= " & par.VirgoleInPunti(CDec(Me.adNormativo_txt.Text)) & " " _
                '                                             & "WHERE ID = " & id_sloggio.Value & ""






                Dim condizioneLivello As String = ""
                If ReturnDdlLivello(CType(Tab_GeneraleUI1.FindControl("ddl_livello"), DropDownList)) <> "null" Then
                    condizioneLivello = " LIVELLO= '" & ReturnDdlLivello(CType(Tab_GeneraleUI1.FindControl("ddl_livello"), DropDownList)) & "', "

                Else

                    condizioneLivello = " LIVELLO= null, "

                End If


                Dim condizioneStCons As String = ""
                If ReturnDdlStConserv(CType(Tab_GeneraleUI1.FindControl("ddl_statocons"), DropDownList)) <> "null" Then
                    condizioneStCons = " cod_stato_conserv= '" & ReturnDdlStConserv(CType(Tab_GeneraleUI1.FindControl("ddl_statocons"), DropDownList)) & "', "
                Else

                    condizioneStCons = " cod_stato_conserv= null, "

                End If




                par.cmd.CommandText = "update SISCOM_MI.SL_SLOGGIO " _
                                           & "set STATO_VERBALE =1, tot_rapporto_sloggio=" & par.VirgoleInPunti(CDec(Me.totAdd_txt.Text)) & ", " _
                                           & " STIMA_COSTI= " & par.VirgoleInPunti(CDec(Me.stimaCosti_txt.Text)) & ", " _
                                           & " AD_NORMATIVO= " & par.VirgoleInPunti(CDec(Me.adNormativo_txt.Text)) & ", " _
                                           & " ASCENSORE= " & ReturnNullSeMenoUno(CType(Tab_GeneraleUI1.FindControl("ddl_ascensore"), DropDownList).SelectedValue) & ", " _
                                           & " SCIVOLI= " & ChkZerUno(CType(Tab_GeneraleUI1.FindControl("chk_scivoli"), CheckBox)) & ", " _
                                           & " MONTASCALE= " & ChkZerUno(CType(Tab_GeneraleUI1.FindControl("chk_montascale"), CheckBox)) & ", " _
                                           & " FORO_AREAZIONE= " & ChkZerUno(CType(Tab_GeneraleUI1.FindControl("chk_esistente"), CheckBox)) & ", " _
                                           & " LOCALE_FORO_AREAZ= '" & par.PulisciStrSql(CType(Tab_GeneraleUI1.FindControl("txt_locale"), TextBox).Text) & "', " _
                                           & condizioneStCons & condizioneLivello _
                                           & " RECUPERABILE= " & ReturnNullSeMenoUno(CType(Tab_GeneraleUI1.FindControl("ddl_UIRecuperabile"), DropDownList).SelectedValue) & " " _
                                           & " WHERE ID = " & id_sloggio.Value & ""


                stato_verb.Value = 1


                par.cmd.ExecuteNonQuery()








                'Response.Write("<script>opener.location.href('VerificaSManutentivo.aspx?A=" & chiamante.Value & "&ID= " & idunita.Value & "');</script>")

                'Response.Write("<script>alert('opener');</script>")















                ''*********************CHIUSURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione completata!');", True)

                ' ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "opener.location.href('VerificaSManutentivo.aspx?A=" & chiamante.Value & "&ID= " & idunita.Value & "');window.focus();", True)
            End If








        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try










    End Sub

    Protected Sub calcolaTot_btn_Click(sender As Object, e As System.EventArgs) Handles calcolaTot_btn.Click

        Dim var As Decimal = 0
        Try
            For K As Integer = 0 To CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1




                If DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text <> 0 Then

                    If var = 0 Then

                        var = DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text


                    Else

                        var = var + DirectCast(CType(Tab_DatiUI1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text



                    End If




                End If


            Next






            For K As Integer = 0 To CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1




                If DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text <> 0 Then

                    If var = 0 Then

                        var = CDec(DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)


                    Else

                        var = var + CDec(DirectCast(CType(Tab_PavimRivest1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text)



                    End If




                End If


            Next






            For K As Integer = 0 To CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1




                If DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text <> 0 Then

                    If var = 0 Then

                        var = DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text


                    Else

                        var = var + DirectCast(CType(Tab_SanitRubinet1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text



                    End If




                End If


            Next









            For K As Integer = 0 To CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items.Count - 1




                If DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text <> 0 Then

                    If var = 0 Then

                        var = DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text


                    Else

                        var = var + DirectCast(CType(Tab_Serramenti1.FindControl("dgDatiUI"), DataGrid).Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text



                    End If




                End If


            Next


            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Operazione completata!" & FormatNumber(var, 2) & " ');", True)

            totAdd_txt.Text = FormatNumber(var, 2)


        Catch ex As Exception
	   ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
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

    Private Function ReturnDdlStConserv(ByVal ddl As DropDownList) As String

        ReturnDdlStConserv = "null"

        If ddl.SelectedValue = -1 Then
            ReturnDdlStConserv = "null"
        End If

        If ddl.SelectedValue = 0 Then
            ReturnDdlStConserv = "NORMA"
        End If

        If ddl.SelectedValue = 1 Then
            ReturnDdlStConserv = "MEDIO"
        End If


        If ddl.SelectedValue = 2 Then
            ReturnDdlStConserv = "SCADE"
        End If


        Return ReturnDdlStConserv
    End Function






    Private Function ReturnDdlLivello(ByVal ddl As DropDownList) As String

        ReturnDdlLivello = "null"

        If ddl.SelectedValue = -1 Then
            ReturnDdlLivello = "null"
        End If

        If ddl.SelectedValue = 0 Then
            ReturnDdlLivello = "BASSO"
        End If

        If ddl.SelectedValue = 1 Then
            ReturnDdlLivello = "MEDIO"
        End If


        If ddl.SelectedValue = 2 Then
            ReturnDdlLivello = "ALTO"
        End If


        Return ReturnDdlLivello
    End Function

    Private Function RitornaNullseZero(ByVal v As String) As String
        If par.IfEmpty(v, 0) = 0 Then
            RitornaNullseZero = "null"
        Else
            RitornaNullseZero = v
        End If

    End Function


End Class
