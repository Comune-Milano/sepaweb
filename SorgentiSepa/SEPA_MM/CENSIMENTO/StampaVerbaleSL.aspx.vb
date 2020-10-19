Imports ExpertPdf.HtmlToPdf

Partial Class CENSIMENTO_StampaVerbaleSL
    Inherits PageSetIdMode

    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If




        If Not IsPostBack Then





            idunita.Value = Request.QueryString("ID")
            id_sloggio.Value = Request.QueryString("IDSLOGGIO")
            id_stato.Value = Request.QueryString("IDSTATO")
            stato_verb.Value = Request.QueryString("ST_VERB")









            ControllaStato()

            CaricaDati()











            totAdd_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
            totAdd_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")

            stimaCosti_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
            stimaCosti_txt.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")


            adNormativo_txt.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);")
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
                stimaCosti_txt.Text = FormatNumber(adNormativo_txt.Text, 2)

            End If






            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.dgDatiUI.Items.Count - 1
                di = Me.dgDatiUI.Items(i)

                If CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "" Or CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = 0 Then

                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"

                Else
                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = FormatNumber(CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text, 2)


                End If


            Next


            For i = 0 To Me.dgSanit.Items.Count - 1
                di = Me.dgSanit.Items(i)

                If CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "" Or CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = 0 Then

                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"

                Else
                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = FormatNumber(CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text, 2)


                End If


            Next





            For i = 0 To Me.dgSerram.Items.Count - 1
                di = Me.dgSerram.Items(i)

                If CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "" Or CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = 0 Then

                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"

                Else
                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = FormatNumber(CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text, 2)


                End If


            Next



            For i = 0 To Me.dgPavim.Items.Count - 1
                di = Me.dgPavim.Items(i)

                If CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "" Or CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = 0 Then

                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"

                Else
                    CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = FormatNumber(CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text, 2)


                End If


            Next





            If id_stato.Value = 2 Then

                If InStr(totAdd_txt.Text, ",") = 0 Then

                    totAdd_txt.Text = totAdd_txt.Text & ",00"

                End If






                If InStr(stimaCosti_txt.Text, ",") = 0 Then

                    stimaCosti_txt.Text = stimaCosti_txt.Text & ",00"

                End If





                If InStr(adNormativo_txt.Text, ",") = 0 Then

                    adNormativo_txt.Text = adNormativo_txt.Text & ",00"

                End If







            End If






            stampaVerbale1()













        End If

    End Sub




    Private Sub stampaVerbale1()


        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)
        Dim frm As New HtmlForm()
        Dim lbl As New Label
        'lbl.Text = "<br/><br/><br/><br/>"

        dgDatiUI.Parent.Controls.Add(frm)
        dgSanit.Parent.Controls.Add(frm)
        dgSerram.Parent.Controls.Add(frm)
        dgPavim.Parent.Controls.Add(frm)

        frm.Attributes("runat") = "server"

        frm.Controls.Add(datiIntest1)
        frm.Controls.Add(dgDatiUI)
        frm.Controls.Add(datiIntest2)
        frm.Controls.Add(dgSanit)
        frm.Controls.Add(datiIntest3)
        frm.Controls.Add(dgSerram)
        frm.Controls.Add(datiIntest4)
        frm.Controls.Add(dgPavim)
        frm.Controls.Add(note)
        frm.Controls.Add(tbl_totale)
        frm.Controls.Add(tbl_generalitaUI)
        frm.Controls.Add(tbl_stima)
        frm.Controls.Add(tbl_adeguamento)



        frm.RenderControl(sourcecode)
        sourcecode.Flush()
        Html = Html & stringWriter.ToString



       




        Dim url As String = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\")
        Dim pdfConverter1 As PdfConverter = New PdfConverter
        Dim Licenza As String = System.Web.HttpContext.Current.Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdfConverter1.LicenseKey = Licenza
        End If
        pdfConverter1.PageWidth = 1200

        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait

        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter1.PdfDocumentOptions.ShowHeader = True
        pdfConverter1.PdfDocumentOptions.ShowFooter = True
        pdfConverter1.PdfDocumentOptions.LeftMargin = 10
        pdfConverter1.PdfDocumentOptions.RightMargin = 15
        pdfConverter1.PdfDocumentOptions.TopMargin = 10
        pdfConverter1.PdfDocumentOptions.BottomMargin = 10
        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
        pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
        pdfConverter1.PdfHeaderOptions.HeaderText = UCase("")
        pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
        pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 12
        pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10
        pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
        pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
        pdfConverter1.PdfFooterOptions.FooterText = UCase("")
        pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
        pdfConverter1.PdfFooterOptions.DrawFooterLine = False


        pdfConverter1.PdfFooterOptions.PageNumberText = "PAG."
        pdfConverter1.PdfFooterOptions.ShowPageNumber = True




        Dim nomefile As String = "Stampa" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)




        Response.Redirect("..\/FileTemp\/" & nomefile)












    End Sub




    Private Sub CaricaDati()
        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            par.cmd.CommandText = "select unita_immobiliari.id_unita_principale, tipologia_unita_immobiliari.descrizione as tipounita,unita_immobiliari.cod_tipologia,unita_immobiliari.cod_tipo_disponibilita,complessi_immobiliari.id as idq, identificativi_catastali.superficie_mq as SUP_MQ ,COMPLESSI_IMMOBILIARI.ID_QUARTIERE AS ID_QUART, TAB_QUARTIERI.NOME AS NOME_QUART, edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.id as idunita,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipologia_unita_immobiliari, siscom_mi.identificativi_catastali, siscom_mi.tipo_livello_piano,siscom_mi.tab_quartieri,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and COMPLESSI_IMMOBILIARI.ID_QUARTIERE = tab_quartieri.id and unita_immobiliari.id_catastale=identificativi_catastali.id (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.ID=" & idunita.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '
                CODICE.Value = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")


                'Label21.Text = "Codice: " & CODICE.Value & " (" & par.IfNull(myReader3("tipounita"), "") & ") Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") _
                ' & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & " Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")

                txt_codiceUI1.Text = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")
                txt_codiceUI2.Text = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")
                txt_codiceUI3.Text = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")
                txt_codiceUI4.Text = par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "")


                txt_quartiere1.Text = par.IfNull(myReader3("NOME_QUART"), "")
                txt_quartiere2.Text = par.IfNull(myReader3("NOME_QUART"), "")
                txt_quartiere3.Text = par.IfNull(myReader3("NOME_QUART"), "")
                txt_quartiere4.Text = par.IfNull(myReader3("NOME_QUART"), "")


                txt_via1.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " " & par.IfNull(myReader3("CIVICO"), "")
                txt_via2.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " " & par.IfNull(myReader3("CIVICO"), "")
                txt_via3.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " " & par.IfNull(myReader3("CIVICO"), "")
                txt_via4.Text = par.IfNull(myReader3("DESCRIZIONE"), "") & " " & par.IfNull(myReader3("CIVICO"), "")



                txt_scala1.Text = par.IfNull(myReader3("SCALA"), "")
                txt_scala2.Text = par.IfNull(myReader3("SCALA"), "")
                txt_scala3.Text = par.IfNull(myReader3("SCALA"), "")
                txt_scala4.Text = par.IfNull(myReader3("SCALA"), "")



                txt_piano1.Text = par.IfNull(myReader3("MIOPIANO"), "")
                txt_piano2.Text = par.IfNull(myReader3("MIOPIANO"), "")
                txt_piano3.Text = par.IfNull(myReader3("MIOPIANO"), "")
                txt_piano4.Text = par.IfNull(myReader3("MIOPIANO"), "")



                txt_nUI1.Text = par.IfNull(myReader3("INTERNO"), "")
                txt_nUI2.Text = par.IfNull(myReader3("INTERNO"), "")
                txt_nUI3.Text = par.IfNull(myReader3("INTERNO"), "")
                txt_nUI4.Text = par.IfNull(myReader3("INTERNO"), "")



                txt_supmq1.Text = par.IfNull(myReader3("SUP_MQ"), "")
                txt_supmq2.Text = par.IfNull(myReader3("SUP_MQ"), "")
                txt_supmq3.Text = par.IfNull(myReader3("SUP_MQ"), "")
                txt_supmq4.Text = par.IfNull(myReader3("SUP_MQ"), "")




            End If
            myReader3.Close()







            par.cmd.CommandText = "select  SISCOM_MI.SL_SLOGGIO.DATA_PRE_SLOGGIO, SISCOM_MI.SL_SLOGGIO.DATA_SLOGGIO" _
                                                   & " from SISCOM_MI.SL_SLOGGIO " _
                                                   & " where SL_SLOGGIO.ID = '" & id_sloggio.Value & "' AND " _
                                                   & "SL_SLOGGIO.ID_UNITA_IMMOBILIARE = " & idunita.Value & ""




            Dim lettData As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettData.Read Then

                txt_dataPreSL1.Text = par.FormattaData(par.IfNull(lettData("DATA_PRE_SLOGGIO"), ""))
              
                txt_dataSL1.Text = par.FormattaData(par.IfNull(lettData("DATA_SLOGGIO"), ""))



            End If


            lettData.Close()











            par.cmd.CommandText = "SELECT siscom_mi.getstatocontratto(rapporti_utenza.id) as statoc,rapporti_utenza.id,RAPPORTI_UTENZA.DATA_DISDETTA_LOCATARIO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.DATA_RICONSEGNA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_UNITA=" & idunita.Value & " AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL"




            Dim lettDataDisd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettDataDisd.Read Then

                txt_dataDisd1.Text = par.FormattaData(par.IfNull(lettDataDisd("DATA_DISDETTA_LOCATARIO"), ""))





            End If


            lettDataDisd.Close()
















            par.cmd.CommandText = ""

            par.cmd.CommandText = "SELECT SL_SLOGGIO.TOT_RAPPORTO_SLOGGIO AS TOT, SL_SLOGGIO.STIMA_COSTI AS STIMA, SL_SLOGGIO.AD_NORMATIVO AS NORM FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & id_sloggio.Value & ""

            Dim myReader333 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader333.Read Then
                '
                totAdd_txt.Text = par.IfNull(myReader333("TOT"), "")
                stimaCosti_txt.Text = par.IfNull(myReader333("STIMA"), "")
                adNormativo_txt.Text = par.IfNull(myReader333("NORM"), "")


            End If

            myReader333.Close()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


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


    Private Sub ControllaStato()


        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT SL_SLOGGIO.STATO_VERBALE AS ST_VERB FROM SISCOM_MI.SL_SLOGGIO WHERE SL_SLOGGIO.ID = " & id_sloggio.Value

        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader3.Read Then
            '
            stato_verb.Value = par.IfNull(myReader3("ST_VERB"), "")




        End If

        myReader3.Close()


        If id_stato.Value >= 0 And stato_verb.Value = 0 Then




            generaTable()
            impostaValori()




        Else


            caricaTable()




        End If

        SettaStatoControl()


    End Sub


    Private Sub caricaTable()


        'tab1
        Dim dt As New Data.DataTable()

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
       & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
       & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
       & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
       & " WHERE sl_tipo_st_manut.tab_appartenenza = 1 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        dgDatiUI.DataSource = dt
        dgDatiUI.DataBind()
        For K As Integer = 0 To dgDatiUI.Items.Count - 1
            If dgDatiUI.Items(K).Cells(2).Text = 0 Then
                DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False
            End If
            If dgDatiUI.Items(K).Cells(3).Text = 0 Then
                DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False
            End If
            If dgDatiUI.Items(K).Cells(4).Text = 0 Then
                DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False
            End If
            If dgDatiUI.Items(K).Cells(5).Text = 0 Then
                DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False
            End If
        Next

        For K As Integer = 0 To dgDatiUI.Items.Count - 1
            par.cmd.CommandText = ""
            par.cmd.CommandText = "SELECT SL_RAPPORTO.ID_SLOGGIO, SL_RAPPORTO.ID_DESC_ST_MANUT, SL_RAPPORTO.QUANTITA, SL_RAPPORTO.TOTALE, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_1 IS NULL THEN '0' ELSE TO_CHAR(CHK_1) END) as CHK_1, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_2 IS NULL THEN '0' ELSE TO_CHAR(CHK_2) END) as CHK_2, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_3 IS NULL THEN '0' ELSE TO_CHAR(CHK_3) END) as CHK_3, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_4 IS NULL THEN '0' ELSE TO_CHAR(CHK_4) END) as CHK_4 " _
                                  & " FROM SISCOM_MI.SL_RAPPORTO " _
                                  & " WHERE SL_RAPPORTO.ID_SLOGGIO = " & id_sloggio.Value & " AND  SL_RAPPORTO.ID_DESC_ST_MANUT = " & dgDatiUI.Items(K).Cells(1).Text & ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                '
                If par.IfNull(myReader1("CHK_1"), "") <> "0" Then
                    DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_2"), "") <> "0" Then
                    DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_3"), "") <> "0" Then
                    DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_4"), "") <> "0" Then
                    DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True
                End If
                DirectCast(dgDatiUI.Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text = par.IfNull(myReader1("QUANTITA"), "")
                DirectCast(dgDatiUI.Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text = par.IfNull(myReader1("TOTALE"), "")
            End If
        Next






        'tab2


        dt = New Data.DataTable()
        da.Dispose()
        par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
       & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
       & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
       & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
       & " WHERE sl_tipo_st_manut.tab_appartenenza = 2 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        dgSanit.DataSource = dt
        dgSanit.DataBind()

        For K As Integer = 0 To dgSanit.Items.Count - 1
            If dgSanit.Items(K).Cells(2).Text = 0 Then
                DirectCast(dgSanit.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False
            End If
            If dgSanit.Items(K).Cells(3).Text = 0 Then
                DirectCast(dgSanit.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False
            End If
            If dgSanit.Items(K).Cells(4).Text = 0 Then
                DirectCast(dgSanit.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False
            End If
            If dgSanit.Items(K).Cells(5).Text = 0 Then
                DirectCast(dgSanit.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False
            End If
        Next

        For K As Integer = 0 To dgSanit.Items.Count - 1
            par.cmd.CommandText = ""
            par.cmd.CommandText = "SELECT SL_RAPPORTO.ID_SLOGGIO, SL_RAPPORTO.ID_DESC_ST_MANUT, SL_RAPPORTO.QUANTITA, SL_RAPPORTO.TOTALE, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_1 IS NULL THEN '0' ELSE TO_CHAR(CHK_1) END) as CHK_1, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_2 IS NULL THEN '0' ELSE TO_CHAR(CHK_2) END) as CHK_2, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_3 IS NULL THEN '0' ELSE TO_CHAR(CHK_3) END) as CHK_3, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_4 IS NULL THEN '0' ELSE TO_CHAR(CHK_4) END) as CHK_4 " _
                                  & " FROM SISCOM_MI.SL_RAPPORTO " _
                                  & " WHERE SL_RAPPORTO.ID_SLOGGIO = " & id_sloggio.Value & " AND  SL_RAPPORTO.ID_DESC_ST_MANUT = " & dgSanit.Items(K).Cells(1).Text & ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                '
                If par.IfNull(myReader1("CHK_1"), "") <> "0" Then
                    DirectCast(dgSanit.Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_2"), "") <> "0" Then
                    DirectCast(dgSanit.Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_3"), "") <> "0" Then
                    DirectCast(dgSanit.Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_4"), "") <> "0" Then
                    DirectCast(dgSanit.Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True
                End If
                DirectCast(dgSanit.Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text = par.IfNull(myReader1("QUANTITA"), "")
                DirectCast(dgSanit.Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text = par.IfNull(myReader1("TOTALE"), "")
            End If
        Next












        dt = New Data.DataTable()
        da.Dispose()




        par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
       & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
       & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
       & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
       & " WHERE sl_tipo_st_manut.tab_appartenenza = 3 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)



        dgSerram.DataSource = dt
        dgSerram.DataBind()









        For K As Integer = 0 To dgSerram.Items.Count - 1




            If dgSerram.Items(K).Cells(2).Text = 0 Then


                DirectCast(dgSerram.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


            End If




            If dgSerram.Items(K).Cells(3).Text = 0 Then


                DirectCast(dgSerram.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


            End If



            If dgSerram.Items(K).Cells(4).Text = 0 Then


                DirectCast(dgSerram.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


            End If



            If dgSerram.Items(K).Cells(5).Text = 0 Then


                DirectCast(dgSerram.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


            End If



        Next




        For K As Integer = 0 To dgSerram.Items.Count - 1

            par.cmd.CommandText = ""

            par.cmd.CommandText = "SELECT SL_RAPPORTO.ID_SLOGGIO, SL_RAPPORTO.ID_DESC_ST_MANUT, SL_RAPPORTO.QUANTITA, SL_RAPPORTO.TOTALE, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_1 IS NULL THEN '0' ELSE TO_CHAR(CHK_1) END) as CHK_1, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_2 IS NULL THEN '0' ELSE TO_CHAR(CHK_2) END) as CHK_2, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_3 IS NULL THEN '0' ELSE TO_CHAR(CHK_3) END) as CHK_3, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_4 IS NULL THEN '0' ELSE TO_CHAR(CHK_4) END) as CHK_4 " _
                                  & " FROM SISCOM_MI.SL_RAPPORTO " _
                                  & " WHERE SL_RAPPORTO.ID_SLOGGIO = " & id_sloggio.Value & " AND  SL_RAPPORTO.ID_DESC_ST_MANUT = " & dgSerram.Items(K).Cells(1).Text & ""


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                '
                If par.IfNull(myReader1("CHK_1"), "") <> "0" Then


                    DirectCast(dgSerram.Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True

                End If


                If par.IfNull(myReader1("CHK_2"), "") <> "0" Then


                    DirectCast(dgSerram.Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True

                End If


                If par.IfNull(myReader1("CHK_3"), "") <> "0" Then


                    DirectCast(dgSerram.Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True

                End If


                If par.IfNull(myReader1("CHK_4"), "") <> "0" Then


                    DirectCast(dgSerram.Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True

                End If



                DirectCast(dgSerram.Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text = par.IfNull(myReader1("QUANTITA"), "")
                DirectCast(dgSerram.Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text = par.IfNull(myReader1("TOTALE"), "")





            End If



        Next








        'tab4

        dt = New Data.DataTable()
        da.Dispose()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
       & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
       & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
       & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
       & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
       & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
       & " WHERE sl_tipo_st_manut.tab_appartenenza = 4 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        dgPavim.DataSource = dt
        dgPavim.DataBind()

        For K As Integer = 0 To dgPavim.Items.Count - 1
            If dgPavim.Items(K).Cells(2).Text = 0 Then
                DirectCast(dgPavim.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False
            End If
            If dgPavim.Items(K).Cells(3).Text = 0 Then
                DirectCast(dgPavim.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False
            End If
            If dgPavim.Items(K).Cells(4).Text = 0 Then
                DirectCast(dgPavim.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False
            End If
            If dgPavim.Items(K).Cells(5).Text = 0 Then
                DirectCast(dgPavim.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False
            End If
        Next

        For K As Integer = 0 To dgPavim.Items.Count - 1
            par.cmd.CommandText = ""
            par.cmd.CommandText = "SELECT SL_RAPPORTO.ID_SLOGGIO, SL_RAPPORTO.ID_DESC_ST_MANUT, SL_RAPPORTO.QUANTITA, SL_RAPPORTO.TOTALE, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_1 IS NULL THEN '0' ELSE TO_CHAR(CHK_1) END) as CHK_1, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_2 IS NULL THEN '0' ELSE TO_CHAR(CHK_2) END) as CHK_2, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_3 IS NULL THEN '0' ELSE TO_CHAR(CHK_3) END) as CHK_3, " _
                                  & " (CASE WHEN SL_RAPPORTO.CHK_4 IS NULL THEN '0' ELSE TO_CHAR(CHK_4) END) as CHK_4 " _
                                  & " FROM SISCOM_MI.SL_RAPPORTO " _
                                  & " WHERE SL_RAPPORTO.ID_SLOGGIO = " & id_sloggio.Value & " AND  SL_RAPPORTO.ID_DESC_ST_MANUT = " & dgPavim.Items(K).Cells(1).Text & ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                '
                If par.IfNull(myReader1("CHK_1"), "") <> "0" Then
                    DirectCast(dgPavim.Items(K).Cells(8).FindControl("stato1"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_2"), "") <> "0" Then
                    DirectCast(dgPavim.Items(K).Cells(9).FindControl("stato2"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_3"), "") <> "0" Then
                    DirectCast(dgPavim.Items(K).Cells(10).FindControl("stato3"), CheckBox).Checked = True
                End If
                If par.IfNull(myReader1("CHK_4"), "") <> "0" Then
                    DirectCast(dgPavim.Items(K).Cells(11).FindControl("stato4"), CheckBox).Checked = True

                End If
                DirectCast(dgPavim.Items(K).Cells(13).FindControl("quantita_txt"), TextBox).Text = par.IfNull(myReader1("QUANTITA"), "")
                DirectCast(dgPavim.Items(K).Cells(15).FindControl("addebito_txt"), TextBox).Text = par.IfNull(myReader1("TOTALE"), "")
            End If
        Next





        'tab 5



        dt = New Data.DataTable()

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If


        par.cmd.CommandText = "SELECT SL_SLOGGIO.NOTE FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & id_sloggio.Value & ""

        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader3.Read Then
            '
            txtNote.Text = par.IfNull(myReader3("NOTE"), "")

        End If

        myReader3.Close()


      


        'somme



        par.cmd.CommandText = ""

        par.cmd.CommandText = "SELECT SL_SLOGGIO.TOT_RAPPORTO_SLOGGIO AS TOT, SL_SLOGGIO.STIMA_COSTI AS STIMA, SL_SLOGGIO.AD_NORMATIVO AS NORM FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & id_sloggio.Value & ""

        Dim myReader333 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader333.Read Then
            '
            totAdd_txt.Text = par.IfNull(myReader333("TOT"), "")
            stimaCosti_txt.Text = par.IfNull(myReader333("STIMA"), "")
            adNormativo_txt.Text = par.IfNull(myReader333("NORM"), "")


        End If

        myReader333.Close()


        ' generalita UI




        dt = New Data.DataTable()
        '*****************APERTURA CONNESSIONE***************


        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT ASCENSORE, SCIVOLI, MONTASCALE, FORO_AREAZIONE, LOCALE_FORO_AREAZ, COD_STATO_CONSERV, LIVELLO, RECUPERABILE " _
                              & " FROM siscom_mi.SL_SLOGGIO " _
                              & " WHERE SL_SLOGGIO.ID = " & DirectCast(Me.Page.FindControl("id_sloggio"), HiddenField).Value & ""

        myReader3 = par.cmd.ExecuteReader()
        If myReader3.Read Then



            If par.IfNull(myReader3("ASCENSORE"), "") > 0 Then

                ddl_ascensore.SelectedValue = 1
            ElseIf par.IfNull(myReader3("ASCENSORE"), "") = 0 Then

                ddl_ascensore.SelectedValue = 0

            Else

                ddl_ascensore.SelectedValue = -1

            End If




            If par.IfNull(myReader3("SCIVOLI"), "") > 0 Then

                chk_scivoli.Checked = True
            Else

                chk_scivoli.Checked = False

            End If





            If par.IfNull(myReader3("MONTASCALE"), "") > 0 Then

                chk_montascale.Checked = True
            Else
                chk_montascale.Checked = False

            End If




            If par.IfNull(myReader3("FORO_AREAZIONE"), "") > 0 Then

                chk_esistente.Checked = True
            Else
                chk_esistente.Checked = False

            End If




            If par.IfNull(myReader3("LOCALE_FORO_AREAZ"), "") <> "" Then

                chk_locale.Checked = True
                txt_locale.Text = par.IfNull(myReader3("LOCALE_FORO_AREAZ"), "")

            Else
                chk_locale.Checked = False

            End If



            If par.IfNull(myReader3("COD_STATO_CONSERV"), "") = "SCADE" Then

                ddl_statocons.SelectedValue = 2

            ElseIf par.IfNull(myReader3("COD_STATO_CONSERV"), "") = "MEDIO" Then

                ddl_statocons.SelectedValue = 1


            ElseIf par.IfNull(myReader3("COD_STATO_CONSERV"), "") = "NORMA" Then

                ddl_statocons.SelectedValue = 0

            Else

                ddl_statocons.SelectedValue = -1

            End If


            If par.IfNull(myReader3("LIVELLO"), "") = "BASSO" Then

                ddl_livello.SelectedValue = 0

            ElseIf par.IfNull(myReader3("LIVELLO"), "") = "MEDIO" Then

                ddl_livello.SelectedValue = 1


            ElseIf par.IfNull(myReader3("LIVELLO"), "") = "ALTO" Then

                ddl_livello.SelectedValue = 2

            Else

                ddl_livello.SelectedValue = -1

            End If


            If par.IfNull(myReader3("RECUPERABILE"), "") = 0 Then

                ddl_UIRecuperabile.SelectedValue = 0
            ElseIf par.IfNull(myReader3("RECUPERABILE"), "") = 1 Then

                ddl_UIRecuperabile.SelectedValue = 1

            Else

                ddl_UIRecuperabile.SelectedValue = -1

            End If



        End If
        myReader3.Close()







    End Sub








    Private Sub generaTable()
        Try

            Dim dt As New Data.DataTable()
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
           & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
           & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
           & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
           & " WHERE sl_tipo_st_manut.tab_appartenenza = 1 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgDatiUI.DataSource = dt
            dgDatiUI.DataBind()





            For K As Integer = 0 To dgDatiUI.Items.Count - 1




                If dgDatiUI.Items(K).Cells(2).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


                End If




                If dgDatiUI.Items(K).Cells(3).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


                End If



                If dgDatiUI.Items(K).Cells(4).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


                End If



                If dgDatiUI.Items(K).Cells(5).Text = 0 Then


                    DirectCast(dgDatiUI.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


                End If



            Next
















            dt = New Data.DataTable()
            par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
          & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
          & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
          & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
          & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
          & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
          & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
          & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
          & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
          & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
          & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
          & " WHERE sl_tipo_st_manut.tab_appartenenza = 2 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgSanit.DataSource = dt
            dgSanit.DataBind()









            For K As Integer = 0 To dgSanit.Items.Count - 1




                If dgSanit.Items(K).Cells(2).Text = 0 Then


                    DirectCast(dgSanit.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


                End If




                If dgSanit.Items(K).Cells(3).Text = 0 Then


                    DirectCast(dgSanit.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


                End If



                If dgSanit.Items(K).Cells(4).Text = 0 Then


                    DirectCast(dgSanit.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


                End If



                If dgSanit.Items(K).Cells(5).Text = 0 Then


                    DirectCast(dgSanit.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


                End If



            Next














            dt = New Data.DataTable()

          


            par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
           & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
           & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
           & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
           & " WHERE sl_tipo_st_manut.tab_appartenenza = 3 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgSerram.DataSource = dt
            dgSerram.DataBind()









            For K As Integer = 0 To dgSerram.Items.Count - 1




                If dgSerram.Items(K).Cells(2).Text = 0 Then


                    DirectCast(dgSerram.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


                End If




                If dgSerram.Items(K).Cells(3).Text = 0 Then


                    DirectCast(dgSerram.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


                End If



                If dgSerram.Items(K).Cells(4).Text = 0 Then


                    DirectCast(dgSerram.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


                End If



                If dgSerram.Items(K).Cells(5).Text = 0 Then


                    DirectCast(dgSerram.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


                End If



            Next

















            dt = New Data.DataTable()



            par.cmd.CommandText = "SELECT   sl_tipo_st_manut.ID AS id_tipo, sl_tipo_st_manut.descrizione AS tipologia, sl_desc_stato_manut.ID AS id_stato_man, " _
           & "sl_desc_stato_manut.descrizione AS stato, CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) END AS id_manut1, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) END AS id_manut2, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) END AS id_manut3, " _
           & "CASE WHEN (SELECT ID FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) IS NULL THEN '0' else (SELECT to_char(ID) FROM(siscom_mi.sl_stati_manut) WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) END AS id_manut4, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_1) AS manut1, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_2) AS manut2, " _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_3) AS manut3," _
           & "(SELECT descrizione FROM siscom_mi.sl_stati_manut WHERE sl_stati_manut.ID = sl_desc_stato_manut.stato_manut_4) AS manut4, " _
           & "sl_desc_stato_manut.unita_misura AS um, sl_desc_stato_manut.costo_unitario || ' €' AS costo " _
           & "FROM siscom_mi.sl_tipo_st_manut, siscom_mi.sl_desc_stato_manut " _
           & " WHERE sl_tipo_st_manut.tab_appartenenza = 4 AND sl_desc_stato_manut.id_tipo_st_manut = sl_tipo_st_manut.ID ORDER BY id_tipo, id_stato_MAN"



            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)



            dgPavim.DataSource = dt
            dgPavim.DataBind()









            For K As Integer = 0 To dgPavim.Items.Count - 1




                If dgPavim.Items(K).Cells(2).Text = 0 Then


                    DirectCast(dgPavim.Items(K).Cells(8).FindControl("stato1"), CheckBox).Visible = False


                End If




                If dgPavim.Items(K).Cells(3).Text = 0 Then


                    DirectCast(dgPavim.Items(K).Cells(9).FindControl("stato2"), CheckBox).Visible = False


                End If



                If dgPavim.Items(K).Cells(4).Text = 0 Then


                    DirectCast(dgPavim.Items(K).Cells(10).FindControl("stato3"), CheckBox).Visible = False


                End If



                If dgPavim.Items(K).Cells(5).Text = 0 Then


                    DirectCast(dgPavim.Items(K).Cells(11).FindControl("stato4"), CheckBox).Visible = False


                End If



            Next



            'tab generalita



            par.cmd.CommandText = "SELECT unita_immobiliari.cod_unita_immobiliare, unita_immobiliari.COD_STATO_CONS_LG_392_78, edifici.num_ascensori " _
                                & " FROM siscom_mi.unita_immobiliari, siscom_mi.edifici" _
                                & "  WHERE edifici.ID = unita_immobiliari.id_edificio " _
                                & "And unita_immobiliari.ID = " & DirectCast(Me.Page.FindControl("idunita"), HiddenField).Value & ""

            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then



                If par.IfNull(myReader3("NUM_ASCENSORI"), "-1") > 0 Then

                    ddl_ascensore.SelectedValue = 1
                Else

                    ddl_ascensore.SelectedValue = 0

                End If

                If par.IfNull(myReader3("COD_STATO_CONS_LG_392_78"), "") = "SCADE" Then

                    ddl_statocons.SelectedValue = 2

                ElseIf par.IfNull(myReader3("COD_STATO_CONS_LG_392_78"), "") = "MEDIO" Then

                    ddl_statocons.SelectedValue = 1


                ElseIf par.IfNull(myReader3("COD_STATO_CONS_LG_392_78"), "") = "NORMA" Then

                    ddl_statocons.SelectedValue = 0

                Else

                    ddl_statocons.SelectedValue = -1

                End If


            End If
            myReader3.Close()





            If sola_lettura.Value = 1 Then
                'txtNote.Enabled = False
            End If























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






    Private Sub impostaValori()

        Try


            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.dgDatiUI.Items.Count - 1
                di = Me.dgDatiUI.Items(i)





                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = 0








            Next










            For i = 0 To Me.dgSanit.Items.Count - 1
                di = Me.dgSanit.Items(i)





                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = 0












            Next





            For i = 0 To Me.dgSerram.Items.Count - 1
                di = Me.dgSerram.Items(i)





                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = 0











            Next







            For i = 0 To Me.dgPavim.Items.Count - 1
                di = Me.dgPavim.Items(i)





                CType(di.Cells(15).FindControl("addebito_txt"), TextBox).Text = "0,00"



                CType(di.Cells(13).FindControl("quantita_txt"), TextBox).Text = 0












            Next


















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



    Private Sub SettaStatoControl()



        If chk_locale.Checked = True Then

            txt_locale.Enabled = True
        Else
            txt_locale.Enabled = False

        End If
    End Sub











End Class
