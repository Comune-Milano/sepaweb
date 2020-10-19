Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class _Default
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim j As Integer

    Dim Str As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:400px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then


            'par.OracleConn.Open()

            'par.SettaCommand(par)
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans

            'par.OracleConn.Close()

            sValoreCF = par.DeCripta(Request.QueryString("A"))
            sValoreNM = Request.QueryString("B")
            sValoreC = par.DeCripta(UCase(Request.QueryString("C")))
            sValoreN = par.DeCripta(UCase(Request.QueryString("D")))

            txtCognome.Text = sValoreC
            txtNome.Text = sValoreN
            txtCF.Text = sValoreCF

            If par.RicavaSesso(sValoreCF) = "M" Then
                cmbSesso.SelectedValue = "M"
            Else
                cmbSesso.SelectedValue = "F"
            End If


            Wizard1.ActiveStepIndex = 0


            Select Case Val(sValoreNM) + 1
                Case 1
                    Image20.Visible = False
                    Image21.Visible = False
                    Image22.Visible = False
                    Image23.Visible = False
                    Image24.Visible = False
                    Image25.Visible = False
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 2
                    Image20.Visible = True
                    Image21.Visible = False
                    Image22.Visible = False
                    Image23.Visible = False
                    Image24.Visible = False
                    Image25.Visible = False
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 3
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = False
                    Image23.Visible = False
                    Image24.Visible = False
                    Image25.Visible = False
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 4
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = False
                    Image24.Visible = False
                    Image25.Visible = False
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 5
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = True
                    Image24.Visible = False
                    Image25.Visible = False
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 6
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = True
                    Image24.Visible = True
                    Image25.Visible = False
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 7
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = True
                    Image24.Visible = True
                    Image25.Visible = True
                    Image26.Visible = False
                    Image27.Visible = False
                    Image28.Visible = False
                Case 8
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = True
                    Image24.Visible = True
                    Image25.Visible = True
                    Image26.Visible = True
                    Image27.Visible = False
                    Image28.Visible = False
                Case 9
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = True
                    Image24.Visible = True
                    Image25.Visible = True
                    Image26.Visible = True
                    Image27.Visible = True
                    Image28.Visible = False
                Case 10
                    Image20.Visible = True
                    Image21.Visible = True
                    Image22.Visible = True
                    Image23.Visible = True
                    Image24.Visible = True
                    Image25.Visible = True
                    Image26.Visible = True
                    Image27.Visible = True
                    Image28.Visible = True
            End Select
            InserisciDati()




        End If

    End Sub


    Sub SkipStep(ByVal sender As Object, ByVal e As WizardNavigationEventArgs)





    End Sub


    Sub GetFavoriteNumerOnActiveStepIndex(ByVal sender As Object, ByVal e As EventArgs)

        If Wizard1.ActiveStepIndex = 0 Then
            ' Label1.Text = "RICHIEDENTE"
            Image18.ImageUrl = "Immagini/Richiedente_1.gif"
            Image19.ImageUrl = "Immagini/ReddRichiedente.gif"

        End If
        If Wizard1.ActiveStepIndex = 1 Then
            ' Label1.Text = "REDDITI DEL RICHIEDENTE"
            Image18.ImageUrl = "Immagini/Richiedente.gif"
            Image19.ImageUrl = "Immagini/ReddRichiedente_1.gif"
            Image20.ImageUrl = "Immagini/Componente2.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"
        End If

        If Wizard1.ActiveStepIndex = 2 Then
            Image19.ImageUrl = "Immagini/ReddRichiedente.gif"
            Image20.ImageUrl = "Immagini/Componente2_1.gif"
            Image21.ImageUrl = "Immagini/Componente3.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"
        End If

        If Wizard1.ActiveStepIndex = 3 Then
            Image20.ImageUrl = "Immagini/Componente2.gif"
            Image21.ImageUrl = "Immagini/Componente3_1.gif"
            Image22.ImageUrl = "Immagini/Componente4.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        If Wizard1.ActiveStepIndex = 4 Then
            Image21.ImageUrl = "Immagini/Componente3.gif"
            Image22.ImageUrl = "Immagini/Componente4_1.gif"
            Image23.ImageUrl = "Immagini/Componente5.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"
        End If

        If Wizard1.ActiveStepIndex = 5 Then
            Image22.ImageUrl = "Immagini/Componente4.gif"
            Image23.ImageUrl = "Immagini/Componente5_1.gif"
            Image24.ImageUrl = "Immagini/Componente6.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        If Wizard1.ActiveStepIndex = 6 Then
            Image23.ImageUrl = "Immagini/Componente5.gif"
            Image24.ImageUrl = "Immagini/Componente6_1.gif"
            Image25.ImageUrl = "Immagini/Componente7.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        If Wizard1.ActiveStepIndex = 7 Then
            Image24.ImageUrl = "Immagini/Componente6.gif"
            Image25.ImageUrl = "Immagini/Componente7_1.gif"
            Image26.ImageUrl = "Immagini/Componente8.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        If Wizard1.ActiveStepIndex = 8 Then
            Image25.ImageUrl = "Immagini/Componente7.gif"
            Image26.ImageUrl = "Immagini/Componente8_1.gif"
            Image27.ImageUrl = "Immagini/Componente9.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        If Wizard1.ActiveStepIndex = 9 Then
            Image26.ImageUrl = "Immagini/Componente8.gif"
            Image27.ImageUrl = "Immagini/Componente9_1.gif"
            Image28.ImageUrl = "Immagini/Componente10.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        If Wizard1.ActiveStepIndex = 10 Then
            Image27.ImageUrl = "Immagini/Componente9.gif"
            Image28.ImageUrl = "Immagini/Componente10_1.gif"
            Image29.ImageUrl = "Immagini/familiari.gif"

        End If

        ''If Wizard1.ActiveStepIndex >= 2 And Wizard1.ActiveStepIndex <= 10 Then
        ''    ' Label1.Text = "COMPONENTE " & Wizard1.ActiveStepIndex - 1
        ''End If

        If Wizard1.ActiveStepIndex = 11 Then
            Image19.ImageUrl = "Immagini/ReddRichiedente.gif"
            Image20.ImageUrl = "Immagini/Componente2.gif"
            Image21.ImageUrl = "Immagini/Componente3.gif"
            Image22.ImageUrl = "Immagini/Componente4.gif"
            Image23.ImageUrl = "Immagini/Componente5.gif"
            Image24.ImageUrl = "Immagini/Componente6.gif"
            Image25.ImageUrl = "Immagini/Componente7.gif"
            Image26.ImageUrl = "Immagini/Componente8.gif"
            Image27.ImageUrl = "Immagini/Componente9.gif"

            Image28.ImageUrl = "Immagini/Componente10.gif"
            Image29.ImageUrl = "Immagini/familiari_1.gif"
            Image30.ImageUrl = "Immagini/Abitative1.gif"
        End If
        If Wizard1.ActiveStepIndex = 12 Then
            Image29.ImageUrl = "Immagini/Familiari.gif"
            Image30.ImageUrl = "Immagini/Abitative1_1.gif"
            Image31.ImageUrl = "Immagini/Abitative2.gif"
        End If

        If Wizard1.ActiveStepIndex = 13 Then
            Image30.ImageUrl = "Immagini/Abitative1.gif"
            Image31.ImageUrl = "Immagini/Abitative2_1.gif"
            Image32.ImageUrl = "Immagini/Requisiti.gif"
        End If
        If Wizard1.ActiveStepIndex = 14 Then
            Image31.ImageUrl = "Immagini/Abitative2.gif"
            Image32.ImageUrl = "Immagini/Requisiti_1.gif"
            Image33.ImageUrl = "Immagini/Convalida.gif"
        End If
        If Wizard1.ActiveStepIndex = 15 Then
            Image32.ImageUrl = "Immagini/Requisiti.gif"
            Image33.ImageUrl = "Immagini/Convalida_1.gif"
            Image34.ImageUrl = "Immagini/Spedizione.gif"
            CalcolaStampa()
        End If
        If Wizard1.ActiveStepIndex = 16 Then
            Image33.ImageUrl = "Immagini/Convalida.gif"
            Image34.ImageUrl = "Immagini/Spedizione_1.gif"
        End If

        If Wizard1.ActiveStepIndex = 17 Then
            Image34.ImageUrl = "Immagini/Spedizione.gif"
            'LBLNUMERODOMANDA.Text = Format(lIdDomanda, "0000000000")
            LBLNUMERODOMANDA.Text = Format(Session.Item("FATTO"), "0000000000")
            lblNomeRicevuta.Text = txtCognome.Text & " " & txtNome.Text
            lblTelefonoRicevuta.Text = txtTelefono.Text

            'If RadioButton2.Checked = True Then
            '    lbldafare.Text = "Adesso devi:"
            '    lblchiamata.Visible = False
            'Else
            '    lbldafare.Text = "Adesso "
            '    lblchiamata.Visible = True
            '    lblspedire1.Visible = False
            '    lblSpedirea.Visible = False
            'End If
        End If

        'If Wizard1.ActiveStepIndex >= 3 Then
        '    Label3.Text = "The value selected on Step 3 is: " & DropDownList3.SelectedItem.Text
        'End If
    End Sub

    Public Property idDichiarazione() As Long
        Get
            If Not (ViewState("par_idDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_idDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idDichiarazione") = value
        End Set

    End Property

    Public Property N_INV_100_ACC() As Integer
        Get
            If Not (ViewState("par_N_INV_100_ACC") Is Nothing) Then
                Return CInt(ViewState("par_N_INV_100_ACC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_N_INV_100_ACC") = value
        End Set

    End Property



    Public Property N_INV_100_NO_ACC() As Integer
        Get
            If Not (ViewState("par_N_INV_100_NO_ACC") Is Nothing) Then
                Return CInt(ViewState("par_N_INV_100_NO_ACC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_N_INV_100_NO_ACC") = value
        End Set

    End Property

    Public Property N_INV_100_66() As Integer
        Get
            If Not (ViewState("par_N_INV_100_66") Is Nothing) Then
                Return CInt(ViewState("par_N_INV_100_66"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_N_INV_100_66") = value
        End Set

    End Property

    Function CalcolaStampa()


        Dim DATI_ANAGRAFICI As String
        Dim DATI_NUCLEO As String
        Dim SPESE_SOSTENUTE As String
        Dim PATRIMONIO_MOB As String
        Dim PATRIMONIO_IMMOB As String
        Dim REDDITO_NUCLEO As String
        Dim dichiarante As String
        Dim DATI_DICHIARANTE As String
        Dim REDDITO_IRPEF As String
        Dim REDDITO_DETRAZIONI As String
        Dim ANNO_SIT_ECONOMICA As String
        Dim CAT_CATASTALE As String
        Dim IMMAGINE_A As String
        Dim IMMAGINE_B As String
        Dim IMMAGINE_C As String
        Dim IMMAGINE_C1 As String
        Dim IMMAGINE_D As String
        Dim LUOGO As String
        Dim SDATA As String
        Dim LUOGO_REDDITO As String
        Dim DATA_REDDITO As String
        Dim numero As String

        Dim GIA_TITOLARI As String

        Dim STATO_NASCITA As String = ""
        Dim COMUNE_NASCITA As String = ""
        Dim PROVINCIA_NASCITA As String = ""





        Try
            par.OracleConn.Open()

            par.SettaCommand(par)
            par.cmd.CommandText = "select * from COMUNI_NAZIONI where ID=" & LBLNASCITA.Text
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myRec.Read Then
                If myRec("SIGLA") <> "E" And myRec("SIGLA") <> "C" Then
                    STATO_NASCITA = "ITALIA"
                    COMUNE_NASCITA = myRec("NOME")
                    PROVINCIA_NASCITA = myRec("SIGLA")
                Else
                    STATO_NASCITA = myRec("NOME")
                    COMUNE_NASCITA = ""
                    PROVINCIA_NASCITA = ""
                End If
            End If


            If STATO_NASCITA = "ITALIA" Then
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & UCase(txtCognome.Text) & "</I>   " _
                                & ", NOME:   <I>" & UCase(txtNome.Text) & "</I><BR>" _
                                & "NATO A:   <I>" & COMUNE_NASCITA & "</I>   , " _
                                & "PROVINCIA:   <I>" & PROVINCIA_NASCITA & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & txtDataNascita.Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & txtTelefono.Text & "</I><BR>"

            Else
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & UCase(txtCognome.Text) & "</I>   " _
                                & ", NOME:   <I>" & UCase(txtNome.Text) & "</I><BR>" _
                                & "STATO ESTERO:   <I>" & STATO_NASCITA & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & txtDataNascita.Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & txtTelefono.Text & "</I><BR>"


            End If

            If cmbNazioneRes.SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & cmbComuneRes.SelectedItem.Text & "</I>   , " _
                & "PROVINCIA:   <I>" & cmbPrRes.SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & cmbTipoIRes.SelectedItem.Text & " " & txtIndirizzo.Text & "</I>   ," _
                & "N. CIVICO:   <I>" & txtCivico.Text & "</I>   , CAP:   <I>" & txtCAP.Text & "</I>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & cmbComuneRes.SelectedItem.Text & "</I>   , " _
                & "STATO ESTERO:   <I>" & cmbNazioneRes.SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & cmbTipoIRes.SelectedItem.Text & " " & txtIndirizzo.Text & "</I>   ," _
                & "N. CIVICO:   <I>" & txtCivico.Text & "</I>   , CAP:   <I>" & txtCAP.Text & "</I>"
            End If


            If chTitolare.Checked = True Then
                GIA_TITOLARI = "ESISTONO"
            Else
                GIA_TITOLARI = "NON ESISTONO"
            End If
            N_INV_100_ACC = 0
            N_INV_100_66 = 0
            N_INV_100_NO_ACC = 0

            DATI_NUCLEO = ""


            SPESE_SOSTENUTE = ""
            PATRIMONIO_MOB = ""
            PATRIMONIO_IMMOB = ""
            CAT_CATASTALE = ""
            REDDITO_NUCLEO = ""
            REDDITO_IRPEF = ""
            REDDITO_DETRAZIONI = ""

            DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                        & "<td width=5%><small><small>    <center>0</center></small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & UCase(txtCF.Text) & "</I>   </small></small></td>" _
                        & "<td width=20%><small><small>   <I>" & UCase(txtCognome.Text) & "</I>   </small></small></td>" _
                        & "<td width=20%><small><small>   <I>" & UCase(txtNome.Text) & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & UCase(txtDataNascita.Text) & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & cmbParentela.SelectedItem.Text & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & cmbInvalidità.SelectedItem.Text & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & cmbAccompagnamento.SelectedItem.Text & "</I>   </small></small></td>" _
                        & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                        & "</tr>"

            If cmbInvalidità.SelectedItem.Text = "100" And cmbAccompagnamento.SelectedItem.Text = "SI" Then
                N_INV_100_ACC = N_INV_100_ACC + 1
            End If
            If cmbInvalidità.SelectedItem.Text = "100" And cmbAccompagnamento.SelectedItem.Text = "NO" Then
                N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
            End If
            If cmbInvalidità.SelectedItem.Text >= "66" And cmbInvalidità.SelectedItem.Text <= "99" And cmbAccompagnamento.SelectedItem.Text = "NO" Then
                N_INV_100_66 = N_INV_100_66 + 1
            End If

            If txtSpese.Text > 0 And cmbAccompagnamento.SelectedItem.Text = "SI" Then
                SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                    & "<td width=50%><small><small><CENTER>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</CENTER></small></small></td>" _
                    & "<td align=right width=50%><small><small>   <I>" & txtSpese.Text & ",00" & "</I></small></small></td>" _
                    & "</tr>"
            End If
            If txtImporto1.Text > 0 Then
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small>   <I>" & UCase(txtCod.Text) & "</I>   </small></small></td>" _
                    & "<td width=50%><small><small>   <I>" & UCase(txtIntermediario1.Text) & "</I>   </small></small></td>" _
                    & "<TD  align=right  width=50%><small><small>   <I>" & txtImporto1.Text & ",00</I></small></small></td>" _
                    & "</tr>"
            End If

            If txtImporto2.Text > 0 Then
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small>   <I>" & UCase(txtCodice2.Text) & "</I>   </small></small></td>" _
                    & "<td width=50%><small><small>   <I>" & UCase(txtIntermediario2.Text) & "</I>   </small></small></td>" _
                    & "<TD  align=right  width=50%><small><small>   <I>" & txtImporto2.Text & ",00</I></small></small></td>" _
                    & "</tr>"
            End If
            If txtValore1.Text > 0 Then
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                       & "<tr>" _
                       & "<td><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                       & "<td><small><small>   <I>" & txtImmob1_1.SelectedItem.Text & "</I>   </small></small></td>" _
                       & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                       & "<td><small><small><p align=right>   <I>" & txtValore1.Text & ",00</I>   </p></small></small></td>" _
                       & "<td><small><small><p align=right>   <I>" & txtMutuo1.Text & ",00</I>   </p></small></small></td>" _
                       & "<td><small><small>   <I></I><center>" & ValoreSI_NO(txtImmob1_5.Checked) & "</center><I></I>   </small></small></td>" _
                       & "</tr>"

            End If

            If txtAltri1.Text > 0 Then
                REDDITO_IRPEF = REDDITO_IRPEF _
                & "<tr>" _
                & "<td width=40%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                & "<TD  width=505%><small><small><p align=right>   <I>" & txtAltri1.Text & ",00</I>   </p></small></small></td>" _
                & "</tr>"
            End If

            If txtAltri2.Text > 0 Then
                REDDITO_IRPEF = REDDITO_IRPEF _
                & "<tr>" _
                & "<td width=40%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                & "<TD  width=505%><small><small><p align=right>   <I>" & txtAltri2.Text & ",00</I>   </p></small></small></td>" _
                & "</tr>"
            End If



            If txtValore2.Text > 0 Then
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                       & "<tr>" _
                       & "<td><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                       & "<td><small><small>   <I>" & txtImmob2_1.SelectedItem.Text & "</I>   </small></small></td>" _
                       & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                       & "<td><small><small><p align=right>   <I>" & txtValore2.Text & ",00</I>   </p></small></small></td>" _
                       & "<td><small><small><p align=right>   <I>" & txtMutuo2.Text & ",00</I>   </p></small></small></td>" _
                       & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox1.Checked) & "</center><I></I>   </small></small></td>" _
                       & "</tr>"
            End If

            If cmbImmobCat1_1.SelectedItem.Text <> " " Then
                CAT_CATASTALE = cmbImmobCat1_1.SelectedItem.Text
            End If
            If txtIrpef1.Text > 0 Or txtAgrari1.Text > 0 Then
                REDDITO_NUCLEO = REDDITO_NUCLEO _
                & "<tr>" _
                & "<td><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                & "<td><small><small><p align=right>   <I>" & txtIrpef1.Text & ",00</I>   </small></small></p></td>" _
                & "<td><small><small><p align=right>   <I>" & txtAgrari1.Text & ",00</I>   </small></small></p></td>" _
                & "</tr>"
            End If

            If txtIrpef2.Text > 0 Or txtAgrari2.Text > 0 Then
                REDDITO_NUCLEO = REDDITO_NUCLEO _
                & "<tr>" _
                & "<td><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                & "<td><small><small><p align=right>   <I>" & txtIrpef2.Text & ",00</I>   </small></small></p></td>" _
                & "<td><small><small><p align=right>   <I>" & txtAgrari2.Text & ",00</I>   </small></small></p></td>" _
                & "</tr>"
            End If

            If txtDetr1.Text > 0 Then
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<tr>" _
                & "<td width=25%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small>   <I>" & UCase(txtDet1_1.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small><p align=right>   <I>" & txtDetr1.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
            End If
            If txtDetr2.Text > 0 Then
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<tr>" _
                & "<td width=25%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small>   <I>" & UCase(txtDet2_1.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small><p align=right>   <I>" & txtDetr2.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
            End If
            If txtDetr3.Text > 0 Then
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<tr>" _
                & "<td width=25%><small><small><center>   <I>" & UCase(txtCognome.Text) & " " & UCase(txtNome.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small>   <I>" & UCase(txtDet3_1.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=25%><small><small><p align=right>   <I>" & txtDetr3.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
            End If

            If sValoreNM >= 1 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>1</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC1.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC1.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC1.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC1.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC1.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC1.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC1.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"

                If cmbInvaliditaC1.SelectedItem.Text = "100" And cmbAccompagnamentoC1.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC1.SelectedItem.Text = "100" And cmbAccompagnamentoC1.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC1.SelectedItem.Text >= "66" And cmbInvaliditaC1.SelectedItem.Text <= "99" And cmbAccompagnamentoC1.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If

                If txtSpeseC1.Text > 0 And cmbAccompagnamentoC1.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC1.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox8.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox6.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox7.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox8.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox11.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox9.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox10.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox11.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C1.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C1.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C1.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C1.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C1.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox3.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C1.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C1.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C1.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C1.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C1.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox2.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList28.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList28.SelectedItem.Text
                End If

                If TextBox60.Text > 0 Or TextBox61.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox60.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox61.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox62.Text > 0 Or TextBox63.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox62.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox63.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox61.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox61.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox63.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox63.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If


                If TextBox68.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList1.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox68.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox69.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList2.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox69.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox70.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC1.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList3.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox70.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If




            End If
            If sValoreNM >= 2 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>2</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC2.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC2.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC2.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC2.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC2.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC2.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC2.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"

                If cmbInvaliditaC2.SelectedItem.Text = "100" And cmbAccompagnamentoC2.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC2.SelectedItem.Text = "100" And cmbAccompagnamentoC2.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC2.SelectedItem.Text >= "66" And cmbInvaliditaC2.SelectedItem.Text <= "99" And cmbAccompagnamentoC2.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If

                If txtSpeseC2.Text > 0 And cmbAccompagnamentoC2.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC2.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox14.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox12.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox13.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox14.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox17.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox15.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox16.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox17.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C2.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C2.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C2.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C2.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C2.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox4.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C2.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C2.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C2.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C2.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C2.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox5.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList29.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList29.SelectedItem.Text
                End If

                If TextBox71.Text > 0 Or TextBox72.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox71.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox72.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox73.Text > 0 Or TextBox74.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox73.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox74.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox76.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox76.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox78.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox78.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox79.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList4.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox79.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox80.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList5.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox80.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox81.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC2.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList6.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox81.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If
            If sValoreNM >= 3 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>3</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC3.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC3.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC3.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC3.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC3.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC3.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC3.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC3.SelectedItem.Text = "100" And cmbAccompagnamentoC3.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC3.SelectedItem.Text = "100" And cmbAccompagnamentoC3.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC3.SelectedItem.Text >= "66" And cmbInvaliditaC3.SelectedItem.Text <= "99" And cmbAccompagnamentoC3.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If

                If txtSpeseC3.Text > 0 And cmbAccompagnamentoC3.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC3.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox20.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox18.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox19.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox20.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox23.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox21.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox22.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox23.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C3.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C3.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C3.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C3.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C3.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox6.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C3.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C3.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C3.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C3.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C3.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox7.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList30.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList30.SelectedItem.Text
                End If

                If TextBox82.Text > 0 Or TextBox83.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox82.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox83.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox84.Text > 0 Or TextBox85.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox84.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox85.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox87.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox87.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox89.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC2.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox89.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox90.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList7.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox90.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox91.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList8.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox91.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox92.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC3.Text) & " " & UCase(txtNomeC3.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList9.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox92.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If
            If sValoreNM >= 4 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>4</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC4.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC4.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC4.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC4.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC4.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC4.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC4.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC4.SelectedItem.Text = "100" And cmbAccompagnamentoC4.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC4.SelectedItem.Text = "100" And cmbAccompagnamentoC4.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC4.SelectedItem.Text >= "66" And cmbInvaliditaC4.SelectedItem.Text <= "99" And cmbAccompagnamentoC4.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If
                If txtSpeseC4.Text > 0 And cmbAccompagnamentoC4.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC4.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox26.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox24.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox25.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox26.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox29.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox27.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox28.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox29.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C4.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C4.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C4.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C4.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C4.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox8.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C4.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C4.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C4.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C4.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C4.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox9.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList31.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList31.SelectedItem.Text
                End If

                If TextBox93.Text > 0 Or TextBox94.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox93.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox94.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox95.Text > 0 Or TextBox96.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox95.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox96.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox98.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox98.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox100.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox100.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox101.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList10.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox101.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox102.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList11.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox102.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox103.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC4.Text) & " " & UCase(txtNomeC4.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList12.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox103.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If
            If sValoreNM >= 5 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>5</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC5.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC5.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC5.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC5.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC5.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC5.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC5.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC5.SelectedItem.Text = "100" And cmbAccompagnamentoC5.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC5.SelectedItem.Text = "100" And cmbAccompagnamentoC5.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC5.SelectedItem.Text >= "66" And cmbInvaliditaC5.SelectedItem.Text <= "99" And cmbAccompagnamentoC5.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If
                If txtSpeseC5.Text > 0 And cmbAccompagnamentoC5.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC5.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox32.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox30.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox31.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox32.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox35.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox33.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox34.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox35.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C5.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C5.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C5.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C5.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C5.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox10.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C5.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C5.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C5.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C5.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C5.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox11.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList32.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList32.SelectedItem.Text
                End If

                If TextBox104.Text > 0 Or TextBox105.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox104.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox105.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox106.Text > 0 Or TextBox107.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox106.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox107.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox109.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox109.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox111.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox111.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox112.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList13.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox112.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox113.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList14.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox113.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox114.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC5.Text) & " " & UCase(txtNomeC5.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList15.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox114.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If
            If sValoreNM >= 6 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>6</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC6.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC6.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC6.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC6.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC6.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC6.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC6.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC6.SelectedItem.Text = "100" And cmbAccompagnamentoC6.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC6.SelectedItem.Text = "100" And cmbAccompagnamentoC6.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC6.SelectedItem.Text >= "66" And cmbInvaliditaC6.SelectedItem.Text <= "99" And cmbAccompagnamentoC6.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If
                If txtSpeseC6.Text > 0 And cmbAccompagnamentoC6.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC6.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox38.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox36.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox37.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox38.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox41.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox39.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox40.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox41.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C6.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C6.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C6.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C6.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C6.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox12.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C6.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C6.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C6.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C6.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C6.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox13.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList33.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList33.SelectedItem.Text
                End If

                If TextBox115.Text > 0 Or TextBox116.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox115.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox116.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox117.Text > 0 Or TextBox118.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox117.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox118.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox120.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox120.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox122.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox122.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox123.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList16.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox123.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox124.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList17.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox124.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox125.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC6.Text) & " " & UCase(txtNomeC6.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList18.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox125.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If


            If sValoreNM >= 7 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>7</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC7.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC7.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC7.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC7.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC7.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC7.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC7.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC7.SelectedItem.Text = "100" And cmbAccompagnamentoC7.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC7.SelectedItem.Text = "100" And cmbAccompagnamentoC7.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC7.SelectedItem.Text >= "66" And cmbInvaliditaC7.SelectedItem.Text <= "99" And cmbAccompagnamentoC7.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If
                If txtSpeseC7.Text > 0 And cmbAccompagnamentoC7.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC1.Text) & " " & UCase(txtNomeC7.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC7.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox44.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox42.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox43.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox44.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox47.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox45.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox46.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox47.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C7.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C7.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C7.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C7.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C7.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox14.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C7.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C7.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C7.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C7.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C7.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox15.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList34.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList34.SelectedItem.Text
                End If

                If TextBox126.Text > 0 Or TextBox127.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox126.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox127.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox128.Text > 0 Or TextBox129.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox128.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox129.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox131.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox131.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox133.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox133.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox134.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList19.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox134.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox135.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList20.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox135.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox136.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC7.Text) & " " & UCase(txtNomeC7.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList21.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox136.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If
            If sValoreNM >= 8 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>8</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC8.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC8.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC8.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC8.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC8.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC8.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC8.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC8.SelectedItem.Text = "100" And cmbAccompagnamentoC8.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC8.SelectedItem.Text = "100" And cmbAccompagnamentoC8.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC8.SelectedItem.Text >= "66" And cmbInvaliditaC8.SelectedItem.Text <= "99" And cmbAccompagnamentoC8.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If
                If txtSpeseC8.Text > 0 And cmbAccompagnamentoC8.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC8.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox50.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox48.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox9.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox50.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox53.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox51.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox52.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox53.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C8.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C8.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C8.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C8.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C8.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox16.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C8.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C8.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C8.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C8.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C8.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox17.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If DropDownList35.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList35.SelectedItem.Text
                End If

                If TextBox137.Text > 0 Or TextBox138.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox137.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox138.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox139.Text > 0 Or TextBox140.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox139.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox140.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox142.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox142.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox144.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox144.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If


                If TextBox145.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList22.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox145.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox146.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList23.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox146.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox147.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC8.Text) & " " & UCase(txtNomeC8.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList24.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox147.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If
            If sValoreNM >= 9 Then
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>9</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtCFC9.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtCognomeC9.Text) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & UCase(txtNomeC9.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtDataNascitaC9.Text) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbParentelaC9.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbInvaliditaC9.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & cmbAccompagnamentoC9.SelectedItem.Text & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & UCase(txtASL.Text) & "</I>   </small></small></td>" _
                            & "</tr>"
                If cmbInvaliditaC9.SelectedItem.Text = "100" And cmbAccompagnamentoC9.SelectedItem.Text = "SI" Then
                    N_INV_100_ACC = N_INV_100_ACC + 1
                End If
                If cmbInvaliditaC9.SelectedItem.Text = "100" And cmbAccompagnamentoC9.SelectedItem.Text = "NO" Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                End If
                If cmbInvaliditaC9.SelectedItem.Text >= "66" And cmbInvaliditaC9.SelectedItem.Text <= "99" And cmbAccompagnamentoC9.SelectedItem.Text = "NO" Then
                    N_INV_100_66 = N_INV_100_66 + 1
                End If
                If txtSpeseC9.Text > 0 And cmbAccompagnamentoC9.SelectedItem.Text = "SI" Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                        & "<td width=50%><small><small><CENTER>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</CENTER></small></small></td>" _
                        & "<td align=right width=50%><small><small>   <I>" & txtSpeseC9.Text & ",00" & "</I></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox56.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox54.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox55.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox56.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If TextBox59.Text > 0 Then
                    PATRIMONIO_MOB = PATRIMONIO_MOB _
                        & "<tr>" _
                        & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(TextBox57.Text) & "</I>   </small></small></td>" _
                        & "<td width=50%><small><small>   <I>" & UCase(TextBox58.Text) & "</I>   </small></small></td>" _
                        & "<TD  align=right  width=50%><small><small>   <I>" & TextBox59.Text & ",00</I></small></small></td>" _
                        & "</tr>"
                End If

                If txtValore1C9.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob1C9.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta1C9.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore1C9.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo1C9.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox18.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If

                If txtValore2C9.Text > 0 Then
                    PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                           & "<tr>" _
                           & "<td><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                           & "<td><small><small>   <I>" & txtImmob2C9.SelectedItem.Text & "</I>   </small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & cmbPercProprieta2C9.SelectedItem.Text & "</I>   %   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtValore2C9.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small><p align=right>   <I>" & txtMutuo2C9.Text & ",00</I>   </p></small></small></td>" _
                           & "<td><small><small>   <I></I><center>" & ValoreSI_NO(CheckBox19.Checked) & "</center><I></I>   </small></small></td>" _
                           & "</tr>"
                End If
                If DropDownList36.SelectedItem.Text <> " " Then
                    CAT_CATASTALE = DropDownList36.SelectedItem.Text
                End If

                If TextBox148.Text > 0 Or TextBox149.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox148.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox149.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox150.Text > 0 Or TextBox151.Text > 0 Then
                    REDDITO_NUCLEO = REDDITO_NUCLEO _
                    & "<tr>" _
                    & "<td><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox150.Text & ",00</I>   </small></small></p></td>" _
                    & "<td><small><small><p align=right>   <I>" & TextBox151.Text & ",00</I>   </small></small></p></td>" _
                    & "</tr>"
                End If

                If TextBox153.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox153.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox155.Text > 0 Then
                    REDDITO_IRPEF = REDDITO_IRPEF _
                    & "<tr>" _
                    & "<td width=40%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                    & "<TD  width=505%><small><small><p align=right>   <I>" & TextBox155.Text & ",00</I>   </p></small></small></td>" _
                    & "</tr>"
                End If

                If TextBox156.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & UCase(DropDownList25.SelectedItem.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox156.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox157.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList26.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox157.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
                If TextBox158.Text > 0 Then
                    REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                    & "<tr>" _
                    & "<td width=25%><small><small><center>   <I>" & UCase(txtCognomeC9.Text) & " " & UCase(txtNomeC9.Text) & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small>   <I>" & DropDownList27.SelectedItem.Text & "</I>   </center></small></small></td>" _
                        & "<TD  width=25%><small><small><p align=right>   <I>" & TextBox158.Text & ",00</I>   </p></small></small></td>" _
                        & "</tr>"
                End If
            End If




            ANNO_SIT_ECONOMICA = "2017"



            IMMAGINE_A = "<img src=block_checked.gif width=10 height=10 border=1>"
            IMMAGINE_B = "<img src=block_checked.gif width=10 height=10 border=1>"

            If PATRIMONIO_MOB <> "" Then
                IMMAGINE_C = "<img src=block_checked.gif width=10 height=10 border=1>"
            Else
                IMMAGINE_C = "<img src=block.gif width=10 height=10 border=1>"
            End If

            If PATRIMONIO_IMMOB <> "" Then
                IMMAGINE_C1 = "<img src=block_checked.gif width=10 height=10 border=1>"
            Else
                IMMAGINE_C1 = "<img src=block.gif width=10 height=10 border=1>"
            End If

            If REDDITO_NUCLEO <> "" Then
                IMMAGINE_D = "<img src=block_checked.gif width=10 height=10 border=1>"
            Else
                IMMAGINE_D = "<img src=block.gif width=10 height=10 border=1>"
            End If

            LUOGO = "Milano"
            SDATA = Format(Now, "dd/MM/yyyy")


            dichiarante = " "
            DATI_DICHIARANTE = "<BR></BR>"
            LUOGO_REDDITO = "Milano"
            DATA_REDDITO = Format(Now, "dd/MM/yyyy")
            numero = "" 'lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text & " del " & Format(Now, "dd/MM/yyyy")
            sStringaSql = "<UL><UL>   <NOBR></NOBR><basefont SIZE=2></UL></UL>"
            sStringaSql = sStringaSql & "<p align='center'><b><font size='4'>COMUNE DI MILANO</font></b><P><CENTER>DICHIARAZIONE SOSTITUTIVA DELLE CONDIZIONI ECONOMICHE DEL NUCLEO FAMILIARE PER LA RICHIESTA DI PRESTAZIONI SOCIALI AGEVOLATE</CENTER>   <BR>"
            sStringaSql = sStringaSql & "<CENTER>"
            sStringaSql = sStringaSql & "</CENTER>   <NOBR></NOBR>   <CENTER>"
            sStringaSql = sStringaSql & ""
            sStringaSql = sStringaSql & "</CENTER><BR>"
            sStringaSql = sStringaSql & "<center><table border=1 cellspacing=0 width=95%><tr><td><small>   <B>QUADRO A: DATI ANAGRAFICI DEL RICHIEDENTE</B><BR></center>"
            sStringaSql = sStringaSql & DATI_ANAGRAFICI & "<br><br></small></td></tr></table>"
            sStringaSql = sStringaSql & "<BR><UL>   </UL><NOBR></NOBR><center>"
            sStringaSql = sStringaSql & "<table border=1 cellspacing=0 width=95%><tr><td><br><small>   QUADRO B: SOGGETTI COMPONENTI IL NUCLEO FAMILIARE: richiedente"
            sStringaSql = sStringaSql & " componenti la famiglia anagrafica e altri soggetti considerati a carico ai fini IRPEF"
            sStringaSql = sStringaSql & "<BR>"
            sStringaSql = sStringaSql & ""
            sStringaSql = sStringaSql & ""
            sStringaSql = sStringaSql & "<center>"
            sStringaSql = sStringaSql & "</small>"
            sStringaSql = sStringaSql & "<table border=1 cellspacing=0 width=90%><tr><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>A</small></p>"
            sStringaSql = sStringaSql & "</td><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>B</small></p>"
            sStringaSql = sStringaSql & "</td><td colspan=2>"
            sStringaSql = sStringaSql & "<p align='center'><small>C</small></p>"
            sStringaSql = sStringaSql & "</td><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>D</small></p>"
            sStringaSql = sStringaSql & "</td><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>E</small></td><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>F</small></td><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>G</small></td><td>"
            sStringaSql = sStringaSql & "<p align='center'><small>H</small></p>"
            sStringaSql = sStringaSql & "</td></tr>   <small>   <tr><td bgcolor=#C0C0C0><center><small><small>N.Progr.</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE FISCALE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>COGNOME</small></small></center></td><td   bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>DATA DI NASCITA</small></small></center></td>"
            sStringaSql = sStringaSql & "</small>"
            sStringaSql = sStringaSql & "<td bgcolor=#C0C0C0>"
            sStringaSql = sStringaSql & "<p align='center'><small><small><small>GR. PARENTELA</small></small></small></td><td bgcolor=#C0C0C0>"
            sStringaSql = sStringaSql & "<p align='center'><small><small><small>&nbsp;% INVALIDITA'</small></small></small></td><td bgcolor=#C0C0C0>"
            sStringaSql = sStringaSql & "<p align='center'><small><small><small>INDENNITA' ACC.</small></small></small></td>   <td bgcolor=#C0C0C0><small><small><small>ASL&nbsp;</small></small></small></td></tr><UL><UL>   <NOBR></NOBR>"
            sStringaSql = sStringaSql & DATI_NUCLEO
            sStringaSql = sStringaSql & "</ul>"
            sStringaSql = sStringaSql & "</ul>"
            sStringaSql = sStringaSql & "</table></center>"
            sStringaSql = sStringaSql & "<BR><UL>   <NOBR></NOBR><small>   <B>Altre informazioni sul nucleo familiare</B><BR></small>"
            sStringaSql = sStringaSql & "<p><small>Nel nucleo famigliare del richiedente <b>" & GIA_TITOLARI & "</b>"
            sStringaSql = sStringaSql & " titolari di un contratto di assegnazione di alloggio di edilizia residenziale pubblica<BR>"
            sStringaSql = sStringaSql & "</p>"
            sStringaSql = sStringaSql & "<table cellspacing=0 border=0 width=90%><tr><td height=18 width=35% ><small>   - nel nucleo familiare sono presenti n.   </small></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right><small>   <I>" & N_INV_100_ACC & "</I>   </p></td></tr></table></td><td width=50% ><small>   componenti con invalidit&agrave; al 100% (con indennit&agrave; di accompagnamento)   </small></td></tr><tr><td><small><CENTER>Spese effettivamente sostenute distinte per componente</small><table border=1 cellpadding=0 cellspacing=0 width=50%>   <tr><td width=50%><small><CENTER><b>A</b></CENTER></small></td><td align=right width=50%><small><CENTER><b>B</b></CENTER></small></td></tr>   <tr><td bgcolor=#C0C0C0 width=50%><CENTER><small><small>Nome</small></small></small></CENTER></td><small>   <td bgcolor=#C0C0C0 align=right width=50%><small><small><CENTER>SPESA</CENTER></small></small></td></tr><UL><UL>   <NOBR></NOBR>" & SPESE_SOSTENUTE & "</UL></UL>   <NOBR></NOBR></table></CENTER></td><td>&nbsp;</td><td>&nbsp;<BR>"
            sStringaSql = sStringaSql & "</small></td></tr><tr><td height=18 width=30% ><small>   - nel nucleo sono presenti n.   </small></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><small>   <small>   " & N_INV_100_NO_ACC & "</small></small></I>   </p></td></tr></table></td><td width=55% ><small>   componenti con invalidit&agrave; al 100% senza indennit&agrave; di accompagnamento<BR>"
            sStringaSql = sStringaSql & "</small></td></tr><tr><td height=18 width=30% ><small>   - nel nucleo sono presenti n.   </small></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><small>   <small>" & N_INV_100_66 & "</small></small></I>   </p></td></tr></table></td><td width=55% ><small>   componenti con invalidit&agrave; inferiore al 100% e superiore al 66%<BR>"
            sStringaSql = sStringaSql & "</small></td></tr></table>"
            sStringaSql = sStringaSql & "</ul>"
            sStringaSql = sStringaSql & "</td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
            sStringaSql = sStringaSql & "<center><table cellspacing=0 border=1 width=95%><tr><td><small><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </small><br><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><small><BR>"
            sStringaSql = sStringaSql & "<B>QUADRO C: SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   <B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>posseduto alla data del 31 dicembre " & ANNO_SIT_ECONOMICA & "&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td></tr>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE INTERMEDIARIO O GESTORE</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>INTERMEDIARIO O GESTORE (indicare se &egrave;  Banca"
            sStringaSql = sStringaSql & " Posta"
            sStringaSql = sStringaSql & " SIM"
            sStringaSql = sStringaSql & " SGR"
            sStringaSql = sStringaSql & " Impresa di investimento comunitaria o extracomunitaria"
            sStringaSql = sStringaSql & " Agente di cambio"
            sStringaSql = sStringaSql & " ecc.)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>VALORE INVESTIMENTO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
            sStringaSql = sStringaSql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>posseduto alla data del 31"
            sStringaSql = sStringaSql & " Dicembre " & ANNO_SIT_ECONOMICA
            sStringaSql = sStringaSql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TIPO DI PATRIMONIO  IMMOBILIARE</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>QUOTA POSSEDUTA (percentuale)</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
            sStringaSql = sStringaSql & " come definita ai fini ICI)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>AD USO ABITATIVO DEL NUCLEO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"
            sStringaSql = sStringaSql & "</table></center><br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=90%><tr><td width=80%><p align=right><small>   Categoria catastale dell'immobile ad uso abitativo del nucleo   </small></p></td><td width=10% style='border: thin solid rgb(0"
            sStringaSql = sStringaSql & " 0"
            sStringaSql = sStringaSql & " 0)'><small><p align=center>   <I>" & CAT_CATASTALE & "</I>   </p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>QUADRO D: REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
            sStringaSql = sStringaSql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>REDDITO COMPLESSIVO DICHIARATO AI FINI IRPEF (1)</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>PROVENTI AGRARI DA DICHIARAZIONE IRAP (per i soli impreditori agricolil)</small></small><center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
            sStringaSql = sStringaSql & "</table></center><br>(1) al netto dei redditi agrari dell'imprenditore agricolo; compresi i redditi da lavoro prestato nelle zone di frontiera"
            sStringaSql = sStringaSql & "<BR><UL>"
            sStringaSql = sStringaSql & "<BR></UL><HR><BR><BR>"
            sStringaSql = sStringaSql & ""
            'sStringaSql = sStringaSql & " ai sensi dell'articolo 76 del DPR 28 dicembre 2000"
            'sStringaSql = sStringaSql & " n. 445"
            'sStringaSql = sStringaSql & " per falsit&agrave; in atti e dichiarazioni mendaci"
            'sStringaSql = sStringaSql & " dichiaro di aver compilato i Quadri:"
            sStringaSql = sStringaSql & "<BR><I>"
            sStringaSql = sStringaSql & "</I>   <I>"
            sStringaSql = sStringaSql & "</I>   <I>"
            sStringaSql = sStringaSql & "</I>   <I>"
            sStringaSql = sStringaSql & "</I>   <I>"
            sStringaSql = sStringaSql & "</I><I></I>"
            sStringaSql = sStringaSql & ""
            sStringaSql = sStringaSql & "<BR><BR>"
            'sStringaSql = sStringaSql & " altres&igrave;"
            'sStringaSql = sStringaSql & " di essere a conoscenza che"
            'sStringaSql = sStringaSql & " nel caso di erogazione di una prestazione sociale agevolata"
            'sStringaSql = sStringaSql & " potranno essere eseguiti controlli"
            'sStringaSql = sStringaSql & " diretti ad accertare la veridicit&agrave; delle informazioni fornite ed effettuati"
            'sStringaSql = sStringaSql & " da parte della Guardia di finanza"
            'sStringaSql = sStringaSql & " presso gli istituti di credito e gli altri intermediari finanziari che gestiscono il patrimonio mobiliare"
            'sStringaSql = sStringaSql & " ai sensi degli articoli 4"
            'sStringaSql = sStringaSql & " comma 2"
            'sStringaSql = sStringaSql & " del decreto legislativo 31 marzo 1998"
            'sStringaSql = sStringaSql & " n. 109"
            'sStringaSql = sStringaSql & " e 6"
            'sStringaSql = sStringaSql & " comma 3"
            'sStringaSql = sStringaSql & " del decreto del Presidente del Consiglio dei   Ministri 7 maggio 1999"
            'sStringaSql = sStringaSql & " n. 221"
            sStringaSql = sStringaSql & "<BR><BR>"
            'sStringaSql = sStringaSql & "<CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I>" & LUOGO & "</I>   </center></small></td><td width=33%><small><center>"
            'sStringaSql = sStringaSql & " li   <I>" & Format(Now, "dd/MM/yyyy") & "</I>   </center></small></td><td width=34%><center>   _______________________________   </center></td></tr><tr><td width=33% height=15><small><center>(luogo)</center></small></td><td width=33%><small><center>(data)</center></small></td><td width=34%><small><center>(firma)</center></small></td></tr></table></CENTER>"
            'sStringaSql = sStringaSql & "<p>&nbsp;</p>"
            'sStringaSql = sStringaSql & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
            'sStringaSql = sStringaSql & "<tr>"
            'sStringaSql = sStringaSql & "<td width='100%'><font face='Arial' size='1'>DICHIARAZIONE RESA E SOTTOSCRITTA IN NOME E PER CONTO DEL RICHIEDENTE DA<BR>"
            'sStringaSql = sStringaSql & "(COGNOME)___________________________________(NOME)___________________________________<BR>"
            'sStringaSql = sStringaSql & "(DOC. DIRICONOSCIMENTO, N°.)________________________<BR>"
            'sStringaSql = sStringaSql & "IN QUALITA' DI (GRADO PARENTELA)_________________________, COMPONENENTE MAGGIORENNE IL NUCLEO FAMILIARE<br>"
            'sStringaSql = sStringaSql & "RICHIEDENTE L'ALLOGGIO, MUNITO DI DELEGA ALLEGATA AGLIA ATTI.<br>"
            'sStringaSql = sStringaSql & "<br>"
            'sStringaSql = sStringaSql & "L'OPERATORE______________</font></td>"
            'sStringaSql = sStringaSql & "</tr>"
            'sStringaSql = sStringaSql & "</table>"
            sStringaSql = sStringaSql & "<UL><UL><BR>"
            sStringaSql = sStringaSql & "</B>" & dichiarante & "<BR>"
            sStringaSql = sStringaSql & DATI_DICHIARANTE
            sStringaSql = sStringaSql & "<br><br>"
            sStringaSql = sStringaSql & "</ul>"
            sStringaSql = sStringaSql & "</ul>"
            sStringaSql = sStringaSql & "</small></td></tr></table></center><BR>"
            sStringaSql = sStringaSql & "</center>"
            sStringaSql = sStringaSql & "<p><p style='page-break-before: always'>&nbsp;</p>"
            sStringaSql = sStringaSql & "<BR><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ERP</B><BR>"
            sStringaSql = sStringaSql & "<B><BR><UL>ALTRI REDDITI</ul></B>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>IMPORTO REDDITO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_IRPEF & "</UL></UL><BR>"
            sStringaSql = sStringaSql & "</table></center>   <B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>TIPO DETRAZIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>IMPORTO DETRAZIONE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
            sStringaSql = sStringaSql & "</table></center><BR>"
            sStringaSql = sStringaSql & "<BR><BR><CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I>" & LUOGO_REDDITO & "</I>   </center></small></td><td width=33%><small><center>"
            sStringaSql = sStringaSql & " li   <I>" & Format(Now, "dd/MM/yyyy") & "</I>   </center></small></td><td width=34%><center></center></td></tr><tr><td width=33% height=15><small><center></center></small></td><td width=33%><small><center></center></small></td><td width=34%><small><center></center></small></td></tr></table></CENTER><BR>"
            sStringaSql = sStringaSql & "</small>"
            sStringaSql = sStringaSql & "</table>"
            sStringaSql = sStringaSql & "<p align='left'>"
            sStringaSql = sStringaSql & " &nbsp;"
            sStringaSql = sStringaSql & "<p align='left'>"
            sStringaSql = sStringaSql & "</font></b></p>"
            sStringaSql = sStringaSql & "<BR><BR>"
            sStringaSql = sStringaSql & "</center>"
            sStringaSql = sStringaSql & "<p align='center'><font face='Arial'></font></p>"
            sStringaSql = sStringaSql & "<BR><BR>"


            Dim DETRAZIONI As Long


            'Dim INV_100_CON As Integer
            'Dim INV_100_NO As Integer
            'Dim INV_66_99 As Integer
            Dim TOT_COMPONENTI As Integer
            Dim REDDITO_COMPLESSIVO As Double
            Dim TOT_SPESE As Long
            Dim DETRAZIONI_FRAGILE As Long
            Dim DETRAZIONI_FR As Long

            Dim MOBILI As Double
            Dim TASSO_RENDIMENTO As Double
            Dim FIGURATIVO_MOBILI As Double
            Dim TOTALE_ISEE_ERP As Double
            Dim IMMOBILI As Long
            Dim MUTUI As Long
            Dim IMMOBILI_RESIDENZA As Long
            Dim MUTUI_RESIDENZA As Long
            Dim TOTALE_PATRIMONIO_ISEE_ERP As Double
            Dim TOTALE_IMMOBILI As Long
            Dim LIMITE_PATRIMONIO As Double

            Dim ISR_ERP As Double
            Dim ISP_ERP As Double
            Dim ISE_ERP As Double
            Dim VSE As Double
            Dim ISEE_ERP As Double
            Dim ESCLUSIONE As String




            Dim MINORI As Integer
            Dim adulti As Integer

            Dim STRINGA_STAMPA As String
            Dim STRINGA_STAMPA_1 As String
            Dim STRINGA_STAMPA_2 As String
            Dim STRINGA_STAMPA_3 As String
            Dim STRINGA_STAMPA_4 As String
            Dim STRINGA_STAMPA_5 As String
            Dim STRINGA_STAMPA_6 As String
            Dim STRINGA_STAMPA_66 As String
            Dim STRINGA_STAMPA_7 As String
            Dim TIPO_ALLOGGIO As Integer

            'Dim Testo_Da_Scrivere As String
            'Dim glIndice_Bando_Origine As Long
            'Dim DescrizioneBandoAggiornamento As String
            Dim limite_isee As Integer = 0



            MINORI = 0
            adulti = 0

            ISR_ERP = 0
            ISP_ERP = 0
            ISE_ERP = 0

            VSE = 0

            TOT_COMPONENTI = 0

            DETRAZIONI = 0
            REDDITO_COMPLESSIVO = 0
            TOT_SPESE = 0
            DETRAZIONI_FRAGILE = 0
            DETRAZIONI_FR = 0
            ISEE_ERP = 0
            MOBILI = 0
            FIGURATIVO_MOBILI = 0

            IMMOBILI = 0
            MUTUI = 0
            IMMOBILI_RESIDENZA = 0
            MUTUI_RESIDENZA = 0
            TOTALE_IMMOBILI = 0

            TOTALE_ISEE_ERP = 0
            TOTALE_PATRIMONIO_ISEE_ERP = 0
            LIMITE_PATRIMONIO = 0

            STRINGA_STAMPA = ""
            STRINGA_STAMPA_1 = ""
            STRINGA_STAMPA_2 = ""
            STRINGA_STAMPA_3 = ""
            STRINGA_STAMPA_4 = ""
            STRINGA_STAMPA_5 = ""
            STRINGA_STAMPA_6 = ""
            STRINGA_STAMPA_66 = ""
            STRINGA_STAMPA_7 = ""
            TIPO_ALLOGGIO = -1

            ESCLUSIONE = ""



            TASSO_RENDIMENTO = 0 'par.IfNull(myReader("TASSO_RENDIMENTO"), 0)

            TOT_COMPONENTI = sValoreNM



            Dim req1 As String
            Dim req2 As String
            Dim req3 As String
            Dim req4 As String
            Dim req5 As String
            Dim req6 As String
            Dim req7 As String
            Dim req20 As String

            If chR1.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Mancanza della cittadinanza o del permesso di soggiorno</li>"
                req1 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req1 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If
            If chR2.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Mancanza della residenza anagrafica o attività lavorativa nel comune</li>"
            End If
            If chR3.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Predecente Assegnazione in proprietà</li>"
                req2 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req2 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If

            If CheckBox20.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Predecente Assegnazione in proprietà</li>"
                req20 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req20 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If

            'If Valore01(CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked) = "0" Then
            ''ESCLUSIONE = ESCLUSIONE & "<li>Decadenza</li>"
            ''req3 = "<img src=block.gif width=10 height=10 border=1>"
            'Else
            req3 = "<img src=block_checked.gif width=10 height=10 border=1>"
            'End If
            If chR5.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Cessione</li>"
                req4 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req4 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If
            If chR6.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Proprietà o Godimento di alloggio adeguato</li>"
                req5 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req5 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If
            If chR7.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Morosità da alloggio ERP negli ultimi 5 anni</li>"
                req6 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req6 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If
            If chR8.Checked = False Then
                ''ESCLUSIONE = ESCLUSIONE & "<li>Occupazione abusiva negli ultimi 5 anni</li>"
                req7 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                req7 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If



            '***INIZIO STAMPA


            Dim sa1 As String
            Dim sA2 As String
            Dim sA3 As String
            Dim sA4 As String
            Dim k1 As String
            Dim i1 As String
            Dim i2 As String
            Dim m1 As String
            Dim m2 As String
            Dim sc1 As String = ""
            Dim sc2 As String = ""
            Dim sc3 As String = ""
            Dim sc4 As String = ""
            Dim sc5 As String = ""
            Dim sc6 As String = ""
            Dim i1a As String = ""
            Dim i1b As String = ""
            Dim i1c As String = ""
            Dim i2a As String = ""
            Dim i2b As String = ""
            Dim i2c As String = ""
            Dim i2d As String = ""
            Dim i3a As String = ""
            Dim i3b As String = ""
            Dim i3c As String = ""
            Dim i3d As String = ""
            Dim i3e As String = ""
            Dim i4a As String = ""
            Dim i4b As String = ""
            Dim i4c As String = ""
            Dim i5a As String = ""
            Dim i5b As String = ""
            Dim i5c As String = ""
            Dim i5d As String = ""
            Dim i6a As String = ""
            Dim i6b As String = ""
            Dim i6c As String = ""
            Dim i7a As String = ""
            Dim i7b As String = ""
            Dim i7c As String = ""
            Dim i8a1 As String = ""
            Dim i8a2 As String = ""
            Dim i8b As String = ""
            Dim i8c As String = ""
            Dim i8d As String = ""

            Dim LOCAZIONE As String
            Dim ACCESSORIE As String

            Dim i8e As String = ""
            Dim i9a As String = ""
            Dim i9b As String = ""
            Dim i9c As String = ""
            Dim i9d As String = ""
            Dim i10a As String = ""
            Dim i10b As String = ""
            Dim i10c As String = ""
            Dim i11a As String = ""
            Dim i11b As String = ""
            Dim i11c As String = ""
            Dim i12a As String = ""
            Dim i12b As String = ""
            Dim i12c As String = ""
            Dim i13a As String = ""
            Dim i13b As String = ""
            Dim i14a As String = ""
            Dim i14b As String = ""
            Dim i15a As String = ""
            Dim i15b As String = ""
            Dim i16a As String = ""
            Dim i16b As String = ""

            Dim protocollo As String


            Dim pg_dichiarazione As String

            Dim DATA_PRESENTA_DICH As String = ""
            Dim LUOGO_PRESENTA_DICH As String = ""
            Dim DATA_STAMPA As String = ""
            Dim ID_DOMANDA As String
            Dim DATA_STAMPA_DOMANDA As String = ""
            Dim PUNTEGGI_INTERMEDI As String = ""
            'Dim sISBAR As String




            protocollo = "" 'lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text
            pg_dichiarazione = "" 'lblPGDic.Text

            LUOGO_PRESENTA_DICH = "Milano" 'par.IfNull(myReader("LUOGO"), "")
            DATA_PRESENTA_DICH = Format(Now, "dd/MM/yyyy") 'par.FormattaData(par.IfNull(myReader("DATA"), ""))

            DATA_STAMPA = Format(Now, "dd/MM/yyyy") 'par.FormattaData(Mid(par.IfNull(myReader("DATA_ORA"), ""), 1, 8))


            Dim DATI_BANDO As String = ""
            par.cmd.CommandText = "SELECT * FROM BANDI order by id desc"
            myRec = par.cmd.ExecuteReader()
            If myRec.Read() Then
                DATI_BANDO = par.IfNull(myRec("DESCRIZIONE"), "")
            End If
            myRec.Close()

            ID_DOMANDA = "" 'protocollo & " del " & txtDataPG.Text
            DATA_STAMPA_DOMANDA = Format(Now, "dd/MM/yyyy")

            ''If cmbF1.Checked = False Then
            sa1 = "<img src=block.gif width=10 height=10 border=1>"
            ''Else
            ''sa1 = "<img src=block_checked.gif width=10 height=10 border=1>"
            ''End If

            ''If Valore01(CType(Dom_Dichiara1.FindControl("CF2"), CheckBox).Checked) = "0" Then
            sA2 = "<img src=block.gif width=10 height=10 border=1>"
            ''Else
            ''sA2 = "<img src=block_checked.gif width=10 height=10 border=1>"
            ''End If

            ''If Valore01(CType(Dom_Dichiara1.FindControl("CF3"), CheckBox).Checked) = "0" Then
            sA3 = "<img src=block.gif width=10 height=10 border=1>"
            ''Else
            ''sA3 = "<img src=block_checked.gif width=10 height=10 border=1>"
            ''End If

            ''If Valore01(CType(Dom_Dichiara1.FindControl("CF4"), CheckBox).Checked) = "0" Then
            sA4 = "<img src=block.gif width=10 height=10 border=1>"
            ''Else
            ''sA4 = "<img src=block_checked.gif width=10 height=10 border=1>"
            ''End If

            Select Case cmbPresentaD.SelectedIndex

                Case 0
                    sc1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    sc2 = "<img src=block.gif width=10 height=10 border=1>"
                    sc3 = "<img src=block.gif width=10 height=10 border=1>"
                    sc4 = "<img src=block.gif width=10 height=10 border=1>"
                    sc5 = "<img src=block.gif width=10 height=10 border=1>"
                    sc6 = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    sc2 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    sc1 = "<img src=block.gif width=10 height=10 border=1>"
                    sc3 = "<img src=block.gif width=10 height=10 border=1>"
                    sc4 = "<img src=block.gif width=10 height=10 border=1>"
                    sc5 = "<img src=block.gif width=10 height=10 border=1>"
                    sc6 = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    sc3 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    sc2 = "<img src=block.gif width=10 height=10 border=1>"
                    sc1 = "<img src=block.gif width=10 height=10 border=1>"
                    sc4 = "<img src=block.gif width=10 height=10 border=1>"
                    sc5 = "<img src=block.gif width=10 height=10 border=1>"
                    sc6 = "<img src=block.gif width=10 height=10 border=1>"
                Case 3
                    sc4 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    sc2 = "<img src=block.gif width=10 height=10 border=1>"
                    sc3 = "<img src=block.gif width=10 height=10 border=1>"
                    sc1 = "<img src=block.gif width=10 height=10 border=1>"
                    sc5 = "<img src=block.gif width=10 height=10 border=1>"
                    sc6 = "<img src=block.gif width=10 height=10 border=1>"
                Case 4
                    sc5 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    sc2 = "<img src=block.gif width=10 height=10 border=1>"
                    sc3 = "<img src=block.gif width=10 height=10 border=1>"
                    sc4 = "<img src=block.gif width=10 height=10 border=1>"
                    sc1 = "<img src=block.gif width=10 height=10 border=1>"
                    sc6 = "<img src=block.gif width=10 height=10 border=1>"
                Case 5
                    sc6 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    sc2 = "<img src=block.gif width=10 height=10 border=1>"
                    sc3 = "<img src=block.gif width=10 height=10 border=1>"
                    sc4 = "<img src=block.gif width=10 height=10 border=1>"
                    sc5 = "<img src=block.gif width=10 height=10 border=1>"
                    sc1 = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            If cfProfugo.Checked = False Then
                k1 = "<img src=block.gif width=10 height=10 border=1>"
            Else
                k1 = "<img src=block_checked.gif width=10 height=10 border=1>"
            End If


            i1 = txtAnnoCanone.Text
            i2 = txtSpeseLoc.Text


            m1 = txtAnnoAcc.Text

            m2 = txtSpeseAcc.Text



            'Dim Ia As String

            Select Case cmbF1.SelectedIndex
                Case 0
                    i1a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i1b = "<img src=block.gif width=10 height=10 border=1>"
                    i1c = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i1b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i1a = "<img src=block.gif width=10 height=10 border=1>"
                    i1c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i1c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i1b = "<img src=block.gif width=10 height=10 border=1>"
                    i1a = "<img src=block.gif width=10 height=10 border=1>"
            End Select


            Select Case cmbF2.SelectedIndex
                Case 0
                    i2a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i2b = "<img src=block.gif width=10 height=10 border=1>"
                    i2c = "<img src=block.gif width=10 height=10 border=1>"
                    i2d = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i2b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i2a = "<img src=block.gif width=10 height=10 border=1>"
                    i2c = "<img src=block.gif width=10 height=10 border=1>"
                    i2d = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i2c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i2b = "<img src=block.gif width=10 height=10 border=1>"
                    i2a = "<img src=block.gif width=10 height=10 border=1>"
                    i2d = "<img src=block.gif width=10 height=10 border=1>"
                Case 3
                    i2d = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i2b = "<img src=block.gif width=10 height=10 border=1>"
                    i2c = "<img src=block.gif width=10 height=10 border=1>"
                    i2a = "<img src=block.gif width=10 height=10 border=1>"
            End Select


            Select Case cmbF3.SelectedIndex
                Case 0
                    i3a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i3b = "<img src=block.gif width=10 height=10 border=1>"
                    i3c = "<img src=block.gif width=10 height=10 border=1>"
                    i3d = "<img src=block.gif width=10 height=10 border=1>"
                    i3e = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i3b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i3a = "<img src=block.gif width=10 height=10 border=1>"
                    i3c = "<img src=block.gif width=10 height=10 border=1>"
                    i3d = "<img src=block.gif width=10 height=10 border=1>"
                    i3e = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i3c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i3b = "<img src=block.gif width=10 height=10 border=1>"
                    i3a = "<img src=block.gif width=10 height=10 border=1>"
                    i3d = "<img src=block.gif width=10 height=10 border=1>"
                    i3e = "<img src=block.gif width=10 height=10 border=1>"
                Case 3
                    i3d = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i3b = "<img src=block.gif width=10 height=10 border=1>"
                    i3c = "<img src=block.gif width=10 height=10 border=1>"
                    i3a = "<img src=block.gif width=10 height=10 border=1>"
                    i3e = "<img src=block.gif width=10 height=10 border=1>"
                Case 4
                    i3e = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i3b = "<img src=block.gif width=10 height=10 border=1>"
                    i3c = "<img src=block.gif width=10 height=10 border=1>"
                    i3d = "<img src=block.gif width=10 height=10 border=1>"
                    i3a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbF4.SelectedIndex
                Case 0
                    i4a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i4b = "<img src=block.gif width=10 height=10 border=1>"
                    i4c = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i4b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i4a = "<img src=block.gif width=10 height=10 border=1>"
                    i4c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i4c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i4b = "<img src=block.gif width=10 height=10 border=1>"
                    i4a = "<img src=block.gif width=10 height=10 border=1>"
            End Select


            Select Case cmbF5.SelectedIndex
                Case 0
                    i5a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i5b = "<img src=block.gif width=10 height=10 border=1>"
                    i5c = "<img src=block.gif width=10 height=10 border=1>"
                    i5d = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i5b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i5a = "<img src=block.gif width=10 height=10 border=1>"
                    i5c = "<img src=block.gif width=10 height=10 border=1>"
                    i5d = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i5c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i5b = "<img src=block.gif width=10 height=10 border=1>"
                    i5a = "<img src=block.gif width=10 height=10 border=1>"
                    i5d = "<img src=block.gif width=10 height=10 border=1>"
                Case 3
                    i5d = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i5b = "<img src=block.gif width=10 height=10 border=1>"
                    i5c = "<img src=block.gif width=10 height=10 border=1>"
                    i5a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbF6.SelectedIndex
                Case 0
                    i6a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i6b = "<img src=block.gif width=10 height=10 border=1>"
                    i6c = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i6b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i6a = "<img src=block.gif width=10 height=10 border=1>"
                    i6c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i6c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i6b = "<img src=block.gif width=10 height=10 border=1>"
                    i6a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbF7.SelectedIndex
                Case 0
                    i7a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i7b = "<img src=block.gif width=10 height=10 border=1>"
                    i7c = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i7b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i7a = "<img src=block.gif width=10 height=10 border=1>"
                    i7c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i7c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i7b = "<img src=block.gif width=10 height=10 border=1>"
                    i7a = "<img src=block.gif width=10 height=10 border=1>"
            End Select



            Select Case cmbA1.SelectedIndex
                Case 0
                    i8a1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                    i8b = "<img src=block.gif width=10 height=10 border=1>"
                    i8c = "<img src=block.gif width=10 height=10 border=1>"
                    i8e = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i8a2 = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                    i8b = "<img src=block.gif width=10 height=10 border=1>"
                    i8c = "<img src=block.gif width=10 height=10 border=1>"
                    i8e = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i8b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                    i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                    i8c = "<img src=block.gif width=10 height=10 border=1>"
                    i8e = "<img src=block.gif width=10 height=10 border=1>"
                Case 3
                    i8c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                    i8b = "<img src=block.gif width=10 height=10 border=1>"
                    i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                    i8e = "<img src=block.gif width=10 height=10 border=1>"
                Case 4
                    'i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                    'i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                    'i8b = "<img src=block.gif width=10 height=10 border=1>"
                    'i8c = "<img src=block.gif width=10 height=10 border=1>"
                    'i8e = "<img src=block.gif width=10 height=10 border=1>"
                    i8e = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                    i8b = "<img src=block.gif width=10 height=10 border=1>"
                    i8c = "<img src=block.gif width=10 height=10 border=1>"
                    i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                Case 5
                    i8e = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                    i8b = "<img src=block.gif width=10 height=10 border=1>"
                    i8c = "<img src=block.gif width=10 height=10 border=1>"
                    i8a1 = "<img src=block.gif width=10 height=10 border=1>"
            End Select
            Dim TAB_T3 As String = ""

            If ChMorosita.Checked = True Then
                i8d = "<img src=block.gif width=10 height=10 border=1>"

                LOCAZIONE = ""
                ACCESSORIE = ""



            Else

                i8d = "<img src=block_checked.gif width=10 height=10 border=1>"

                LOCAZIONE = txtSpeseLoc.Text & ",00"
                ACCESSORIE = txtSpeseAcc.Text & ",00"

                TAB_T3 = "<span style='font-size: 10pt; color: black; font-family: Arial'><strong>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 14pt; font-family: Arial'>TABELLA T3<br /></span><br />"
                TAB_T3 = TAB_T3 & "ANNO &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </strong><span style='font-size: 10pt; color: black; font-family: Arial'><em>(Indicare l' anno in cui si e' verificata la <b>morosita'</b>)<br />"
                TAB_T3 = TAB_T3 & "</em></span></span><br />"

                TAB_T3 = TAB_T3 & "<table border='0' cellpadding='1' cellspacing='1' style='border-right: black 1px solid;border-top: black 1px solid; font-size: 10pt; border-left: black 1px solid; border-bottom: black 1px solid;font-family: ARIAL; text-align: left' width='95%'><tr><td width=30% style='height: 16px; border-bottom: black 1px solid;'><span style='font-size: 10pt; color: black; font-family: Arial'><span style='font-size: 12pt'>"
                TAB_T3 = TAB_T3 & "Totale componenti il nucleo richiedente al 31 dicembre</span></span></td><td  width=20% style='height: 16px; border-bottom: black 1px solid;'><span style='font-size: 10pt; font-family: Arial'><span style='font-size: 12pt'>N. </span>"
                TAB_T3 = TAB_T3 & "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; </span></td><td  width=30% style='height: 16px; border-bottom: black 1px solid;'><p><span style='font-size: 10pt; color: black; font-family: Arial'><span style='font-size: 12pt'>"
                TAB_T3 = TAB_T3 & "Maggiorenni alla data del 31 dicembre</span> </span></p><p><span style='font-size: 10pt; color: black; font-family: Arial'></span>&nbsp;</p></td><td  width=20% style='height: 16px; border-bottom: black 1px solid;'> <span style='font-size: 10pt; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>N. &nbsp;</span> &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; </span></td></tr><tr><td style='border-bottom: black 1px solid; height: 16px'><span style='font-size: 12pt; color: black; font-family: Arial'>Minorenni al di sotto dei 15 anni alla data del 31 dicembre</span></td><td style='border-bottom: black 1px solid; height: 16px'><span style='font-size: 12pt; font-family: Arial'>N.</span></td><td style='border-bottom: black 1px solid; height: 16px'>"
                TAB_T3 = TAB_T3 & "&nbsp;</td><td style='border-bottom: black 1px solid; height: 16px'>"
                TAB_T3 = TAB_T3 & "&nbsp;</td></tr><tr><td style='border-bottom: black 1px solid; height: 16px'><p><span style='font-size: 10pt; color: black; font-family: Arial'>I<span style='font-size: 12pt'>nvalidi"
                TAB_T3 = TAB_T3 & "al 100% con indennita' di accompagnamento</span></span></p>            </td>            <td style='border-bottom: black 1px solid; height: 16px'>                <span style='font-size: 12pt; font-family: Arial'>N.</span></td>            <td style='border-bottom: black 1px solid; height: 16px'>                <p class='MsoNormal' style='margin-bottom: 0pt; line-height: normal; mso-layout-grid-align: none'>                    <span style='font-size: 12pt; color: black; font-family: Arial'>Spese                        sostenute per invalidi al 100% con indennita' di accompagnamento </span>                </p>                </td>            <td style='border-bottom: black 1px solid; height: 16px'>                <span style='font-size: 10pt; color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp;&nbsp; ,00&nbsp; &nbsp;</span>&nbsp;</span>            </td>        </tr>        <tr>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 10pt; color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Invalidi                    al 100% senza indennita' di accompagnamento</span></span></td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 12pt; font-family: Arial'>N.</span></td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <p class='MsoNormal' style='margin-bottom: 0pt; line-height: normal; mso-layout-grid-align: none'>                    <span style='font-size: 12pt; color: black; font-family: Arial'>Invalidi                        tra il 66 ed il 99% </span>                </p>                <p>                    <span style='font-size: 10pt; color: black; font-family: Arial'>                        <o:p></o:p>                    </span>&nbsp;</p>            </td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 12pt; font-family: Arial'>N.</span></td>        </tr>        <tr>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 12pt; color: black; font-family: Arial'>Reddito                    lordo complessivo</span></td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp;&nbsp; ,00</span>&nbsp; &nbsp;&nbsp;</span>            </td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <p class='MsoNormal' style='margin-bottom: 0pt; line-height: normal; mso-layout-grid-align: none'>                    <span style='color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Detrazioni                        al reddito per Irpef, spese mediche e rette per case di riposo</span> </span>                <o:p></o:p>                    </p>                </td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp;&nbsp; ,00</span>&nbsp; &nbsp;&nbsp;</span>            </td>        </tr>        <tr style='font-size: 10pt'>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 12pt; color: black; font-family: Arial'>Canone                    affitto complessivo annuo</span></td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 10pt; color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp; &nbsp; ,00&nbsp; </span>&nbsp;&nbsp;</span>            </td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <p class='MsoNormal' style='margin-bottom: 0pt; line-height: normal; mso-layout-grid-align: none'>                    <span style='font-size: 12pt; color: black; font-family: Arial'>Spese                        accessorie per l'abitazione (condominio e riscaldamento) </span>                <o:p></o:p>                    </p>                </td>            <td style='height: 16px; border-bottom: black 1px solid;'>                <span style='font-size: 10pt; color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp; &nbsp; ,00&nbsp;</span> &nbsp;&nbsp;</span>            </td>        </tr>        <tr>            <td style='border-bottom: black 1px solid; height: 16px'>                <span style='font-size: 10pt; color: black'><span style='font-family: Arial'><span style='font-size: 12pt'>Patrimonio                    immobiliare (valore ai fini ICI)                    </span>"
                TAB_T3 = TAB_T3 & "<br />                    <o:p></o:p>                </span></span>            </td>            <td style='border-bottom: black 1px solid; height: 16px'>                <span style='font-size: 10pt; color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp;&nbsp; ,00</span>&nbsp; &nbsp;&nbsp;</span>            </td>            <td style='border-bottom: black 1px solid; height: 16px'>                <p class='MsoNormal' style='margin: 6pt 0cm 0pt; line-height: normal; mso-layout-grid-align: none'>                    <span style='font-size: 12pt; color: black; font-family: Arial'>Detrazione                        per mutui (mutuo residuo) </span>                <o:p></o:p>                    </p>                </td>            <td style='border-bottom: black 1px solid; height: 16px'>                "
                TAB_T3 = TAB_T3 & "<span style='font-size: 12pt'><span style='color: black; font-family: Arial'>Euro &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp; &nbsp; &nbsp;                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; ,00&nbsp; &nbsp;&nbsp;</span>            </span>"
                TAB_T3 = TAB_T3 & "</td>        </tr>        <tr>            <td style='border-bottom: black 1px solid; height: 16px'>                <span style='font-size: 12pt; color: black; font-family: Arial'>Patrimonio                    mobiliare alla data del 31 dicembre<br />"
                TAB_T3 = TAB_T3 & "</span>            </td>            <td style='border-bottom: black 1px solid; height: 16px'>                <span style='font-size: 10pt; color: black; font-family: Arial'>"
                TAB_T3 = TAB_T3 & " <span style='font-size: 12pt'>Euro &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;"
                TAB_T3 = TAB_T3 & "&nbsp;&nbsp; ,00&nbsp;</span> &nbsp;&nbsp;</span></td>            <td style='border-bottom: black 1px solid; height: 16px'>            &nbsp;</td>            <td style='border-bottom: black 1px solid; height: 16px'>            &nbsp;</td>        </tr>    </table>"
                'End If
            End If

            Select Case cmbA2.SelectedIndex
                Case 0
                    i9a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i9b = "<img src=block.gif width=10 height=10 border=1>"
                    i9c = "<img src=block.gif width=10 height=10 border=1>"
                    i9d = "<img src=block.gif width=10 height=10 border=1>"

                Case 1
                    i9b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i9a = "<img src=block.gif width=10 height=10 border=1>"
                    i9c = "<img src=block.gif width=10 height=10 border=1>"
                    i9d = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i9c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i9b = "<img src=block.gif width=10 height=10 border=1>"
                    i9a = "<img src=block.gif width=10 height=10 border=1>"
                    i9d = "<img src=block.gif width=10 height=10 border=1>"
                Case 3
                    i9d = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i9b = "<img src=block.gif width=10 height=10 border=1>"
                    i9c = "<img src=block.gif width=10 height=10 border=1>"
                    i9a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbA3.SelectedIndex
                Case 0
                    i10a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i10b = "<img src=block.gif width=10 height=10 border=1>"
                    i10c = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i10b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i10a = "<img src=block.gif width=10 height=10 border=1>"
                    i10c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i10c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i10b = "<img src=block.gif width=10 height=10 border=1>"
                    i10a = "<img src=block.gif width=10 height=10 border=1>"
            End Select


            Select Case cmbA4.SelectedIndex
                Case 0
                    i11a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i11b = "<img src=block.gif width=10 height=10 border=1>"
                    i11c = "<img src=block.gif width=10 height=10 border=1>"

                Case 1
                    i11b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i11a = "<img src=block.gif width=10 height=10 border=1>"
                    i11c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i11c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i11b = "<img src=block.gif width=10 height=10 border=1>"
                    i11a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbA5.SelectedIndex
                Case 0
                    i12a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i12b = "<img src=block.gif width=10 height=10 border=1>"
                    i12c = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i12b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i12a = "<img src=block.gif width=10 height=10 border=1>"
                    i12c = "<img src=block.gif width=10 height=10 border=1>"
                Case 2
                    i12c = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i12b = "<img src=block.gif width=10 height=10 border=1>"
                    i12a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbA6.SelectedIndex
                Case 0
                    i13a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i13b = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i13b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i13a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbA7.SelectedIndex
                Case 0
                    i14a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i14b = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i14b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i14a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbA8.SelectedIndex
                Case 0
                    i15a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i15b = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i15b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i15a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Select Case cmbA9.SelectedIndex
                Case 0
                    i16a = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i16b = "<img src=block.gif width=10 height=10 border=1>"
                Case 1
                    i16b = "<img src=block_checked.gif width=10 height=10 border=1>"
                    i16a = "<img src=block.gif width=10 height=10 border=1>"
            End Select

            Dim CANONE_LOCAZIONE As String

            If Val(i2) > 0 Then
                CANONE_LOCAZIONE = " <tr><td><font face='Arial' size='2'>3)</font></td>" _
                                 & "<TD colspan='3'><font face='Arial' size='2'>che per l'abitazione occupata in locazione" _
                                 & " come residenza principale al momento di presentazione" _
                                 & " della domanda il canone di locazione per" _
                                 & " l'anno &lt;" & i1 & "&gt; e' di euro " & i2 & ";</font></td>" _
                                 & "</tr>" _
                                 & "<tr>" _
                                 & "<td><font face='Arial' size='2'>4)</font></td>" _
                                 & "<TD colspan='3'><font face='Arial' size='2'>che per l'abitazione di cui al comma precedente" _
                                 & " le spese accessorie di competenza per l'anno" _
                                 & "&lt;" & m1 & "&gt; sono di euro " & m2 & ";</font></td>" _
                                 & "</tr>"
            Else
                CANONE_LOCAZIONE = ""
            End If



            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='50%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='50%'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='center'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='50%' valign='top'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='50%'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='center'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<div align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top' width='100%'>" & vbCrLf
            sStringaSql = sStringaSql & "<p class='titolo' align='center'>&nbsp;</p>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>MILANO" & vbCrLf
            sStringaSql = sStringaSql & "li " & Format(Now, "dd/MM/yyyy") & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p class='titolo' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<b><font face='Arial' size='3'>" & vbCrLf
            sStringaSql = sStringaSql & "DOMANDA DI ASSEGNAZIONE ALLOGGI ERP<BR>" & vbCrLf
            sStringaSql = sStringaSql & "(RR 10 febbraio 2004 n 1)</font></b></p>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "&nbsp;" & vbCrLf
            sStringaSql = sStringaSql & "<p>" & DATI_BANDO & "&nbsp;</P><P></p>" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;<font face='Arial' size='2'>" & DATI_ANAGRAFICI & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</p>" & vbCrLf
            sStringaSql = sStringaSql & "<p class='titolo' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<b><font face='Arial' size='3'>" & vbCrLf
            sStringaSql = sStringaSql & "CHIEDE</font></b>" & vbCrLf
            sStringaSql = sStringaSql & "</p>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & vbCrLf
            sStringaSql = sStringaSql & "l'assegnazione di un alloggio di edilizia" & vbCrLf
            sStringaSql = sStringaSql & "residenziale pubblica a" & vbCrLf
            sStringaSql = sStringaSql & "canone sociale" & vbCrLf
            sStringaSql = sStringaSql & "ai sensi" & vbCrLf
            sStringaSql = sStringaSql & "del RR 10 febbraio 2004 n.1" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "A tal fine" & vbCrLf
            sStringaSql = sStringaSql & "ai sensi del DPR 28 dicembre" & vbCrLf
            sStringaSql = sStringaSql & "2000" & vbCrLf
            sStringaSql = sStringaSql & "n. 445" & vbCrLf
            sStringaSql = sStringaSql & "sotto la propria responsabilita'" & vbCrLf
            sStringaSql = sStringaSql & "e nella consapevolezza delle conseguenze" & vbCrLf
            sStringaSql = sStringaSql & "penali in caso di dichiarazione mendace" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p class='titolo' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<b><font face='Arial' size='3'>" & vbCrLf
            sStringaSql = sStringaSql & "DICHIARA</font></b>" & vbCrLf
            sStringaSql = sStringaSql & "</p>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><font face='Arial' size='2'>1)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='2'>che il proprio nucleo familiare e' composto" & vbCrLf
            sStringaSql = sStringaSql & "così come indicato nella dichiarazione sostituiva" & vbCrLf
            sStringaSql = sStringaSql & "allegata</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'></font>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'></font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'></font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1005'><font face='Arial' size='2'>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'></font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1005'><font face='Arial' size='2'>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'><font face='Arial' size='2'>2)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='1117'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & k1 & " di essere nella condizione di profugo rimpatriato" & vbCrLf
            sStringaSql = sStringaSql & "da non oltre un quinquennio;</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & CANONE_LOCAZIONE & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='2'>&nbsp;&nbsp;&nbsp;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='1117'>" & vbCrLf
            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='center'><b><font face='Arial' size='3'>REQUISITI" & vbCrLf
            sStringaSql = sStringaSql & "AMMISSIBILITA'</font></b></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='2'>&nbsp;&nbsp;&nbsp;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'><font face='Arial' size='2'>a)</font></td>            <td width='55'><font face='Arial' size='2'>" & req1 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>che tutti i componenti" & vbCrLf
            sStringaSql = sStringaSql & "il nucleo familiare sono in possesso della cittadinanza di uno" & vbCrLf
            sStringaSql = sStringaSql & "Stato dell'unione europea oppure sono in possesso della carta di" & vbCrLf
            sStringaSql = sStringaSql & "soggiorno o permesso di soggiorno validi in corso di rinnovo;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='2'>di presentare domanda in quanto:</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'><font face='Arial' size='2'>" & sc1 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>residente nel comune;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'><font face='Arial' size='2'>" & sc2 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>presta la propria attivita' lavorativa nel" & vbCrLf
            sStringaSql = sStringaSql & "comune;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'><font face='Arial' size='2'>" & sc3 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>il comune di residenza e quello in cui presta" & vbCrLf
            sStringaSql = sStringaSql & "la propria attivita' lavorativa" & vbCrLf
            sStringaSql = sStringaSql & "non hanno" & vbCrLf
            sStringaSql = sStringaSql & "indetto un bando per l'assegnazione" & vbCrLf
            sStringaSql = sStringaSql & "degli" & vbCrLf
            sStringaSql = sStringaSql & "alloggi di erp per due semestri" & vbCrLf
            sStringaSql = sStringaSql & "consecutivi;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'><font face='Arial' size='2'>" & sc4 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>svolgera' una nuova attivita' lavorativa nel" & vbCrLf
            sStringaSql = sStringaSql & "comune" & vbCrLf
            sStringaSql = sStringaSql & "a seguito della perdita" & vbCrLf
            sStringaSql = sStringaSql & "della precedente" & vbCrLf
            sStringaSql = sStringaSql & "attivita' lavorativa esclusiva" & vbCrLf
            sStringaSql = sStringaSql & "o principale" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "a causa di ristrutturazione industriale" & vbCrLf
            sStringaSql = sStringaSql & "o" & vbCrLf
            sStringaSql = sStringaSql & "di eventi non a lui imputabili" & vbCrLf
            sStringaSql = sStringaSql & "ovvero svolgera'" & vbCrLf
            sStringaSql = sStringaSql & "la propria attivita' lavorativa" & vbCrLf
            sStringaSql = sStringaSql & "nel comune" & vbCrLf
            sStringaSql = sStringaSql & "presso nuovi insediamenti o attivita'" & vbCrLf
            sStringaSql = sStringaSql & "produttive;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'><font face='Arial' size='2'>" & sc5 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>sara' assunto nel comune in base ad accordi" & vbCrLf
            sStringaSql = sStringaSql & "con le organizzazioni sindacali" & vbCrLf
            sStringaSql = sStringaSql & "di settore" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "a seguito di piani di sviluppo" & vbCrLf
            sStringaSql = sStringaSql & "occupazionale" & vbCrLf
            sStringaSql = sStringaSql & "nel comune medesimo;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'><font face='Arial' size='2'>" & sc6 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='2'>emigrato italiano all'estero." & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='16'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55'>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='2' width='1056'>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "</div>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<div align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top'>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='middle'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req2 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1061'><font face='Arial' size='2'>che nessun componente del nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "indicato nella dichiarazione" & vbCrLf
            sStringaSql = sStringaSql & "sostitutiva" & vbCrLf
            sStringaSql = sStringaSql & "allegata alla data di presentazione" & vbCrLf
            sStringaSql = sStringaSql & "della" & vbCrLf
            sStringaSql = sStringaSql & "domanda ha ottenuto l'assegnazione" & vbCrLf
            sStringaSql = sStringaSql & "in proprieta'" & vbCrLf
            sStringaSql = sStringaSql & "immediata o futura di alloggio" & vbCrLf
            sStringaSql = sStringaSql & "realizzato" & vbCrLf
            sStringaSql = sStringaSql & "con contributi pubblici o ha" & vbCrLf
            sStringaSql = sStringaSql & "usufruito di" & vbCrLf
            sStringaSql = sStringaSql & "finanziamenti agevolati in qualunque" & vbCrLf
            sStringaSql = sStringaSql & "forma" & vbCrLf
            sStringaSql = sStringaSql & "concessi dallo Stato e da enti" & vbCrLf
            sStringaSql = sStringaSql & "pubblici (Art.8" & vbCrLf
            sStringaSql = sStringaSql & "comma 1" & vbCrLf
            sStringaSql = sStringaSql & "lett. c RR 1/2004);" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf

            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='middle'><font face='Arial' size='2'>d)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req20 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123' valign='middle'><font face='Arial' size='2'>assenza di precedente assegnazione in locazione di un alloggio ERP, qualora il rilascio sia dovuto a provvedimento amministrativo di decadenza per aver destinato l'alloggio o le relative pertinenze ad attivita' illecite che risultino da provvedimenti giudiziari e/o della pubblica sicurezza;" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf

            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='middle'><font face='Arial' size='2'>e)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req3 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123' valign='middle'><font face='Arial' size='2'>che nessun componente del nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "e' risultato in precedenza assegnatario" & vbCrLf
            sStringaSql = sStringaSql & "di" & vbCrLf
            sStringaSql = sStringaSql & "alloggio ERP revocato con provvedimento" & vbCrLf
            sStringaSql = sStringaSql & "amministrativo" & vbCrLf
            sStringaSql = sStringaSql & "di decadenza per aver destinato" & vbCrLf
            sStringaSql = sStringaSql & "l'alloggio" & vbCrLf
            sStringaSql = sStringaSql & "o le relative pertinenze ad attivita'" & vbCrLf
            sStringaSql = sStringaSql & "illecite" & vbCrLf
            sStringaSql = sStringaSql & "(Art.8 comma 1" & vbCrLf
            sStringaSql = sStringaSql & "lett. d RR 1/2004);" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='middle'><font face='Arial' size='2'>f)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req4 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123'><font face='Arial' size='2'>che nessun componente del nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "e' risultato in precedenza assegnatario" & vbCrLf
            sStringaSql = sStringaSql & "in" & vbCrLf
            sStringaSql = sStringaSql & "locazione semplice di alloggio" & vbCrLf
            sStringaSql = sStringaSql & "ERP ceduto" & vbCrLf
            sStringaSql = sStringaSql & "in tutto o in parte al di fuori" & vbCrLf
            sStringaSql = sStringaSql & "dei casi" & vbCrLf
            sStringaSql = sStringaSql & "previsti dalla legge (Art.8 comma 1" & vbCrLf
            sStringaSql = sStringaSql & "lett. e RR 1/2004);" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='middle'><font face='Arial' size='2'>g)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req5 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123'><font face='Arial' size='2'>che nessun componente del nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "indicato nella dichiarazione" & vbCrLf
            sStringaSql = sStringaSql & "sostitutiva" & vbCrLf
            sStringaSql = sStringaSql & "allegata alla data di presentazione" & vbCrLf
            sStringaSql = sStringaSql & "della" & vbCrLf
            sStringaSql = sStringaSql & "domanda e' titolare del diritto" & vbCrLf
            sStringaSql = sStringaSql & "di proprieta'" & vbCrLf
            sStringaSql = sStringaSql & "o altri diritti reali di godimento" & vbCrLf
            sStringaSql = sStringaSql & "su alloggio" & vbCrLf
            sStringaSql = sStringaSql & "adeguato alle esigenze del nucleo" & vbCrLf
            sStringaSql = sStringaSql & "familiare" & vbCrLf
            sStringaSql = sStringaSql & "nell'ambito del territorio nazionale e all'estero (Art.8" & vbCrLf
            sStringaSql = sStringaSql & "comma 1" & vbCrLf
            sStringaSql = sStringaSql & "lett. g RR 1/2004);" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='middle'><font face='Arial' size='2'>h)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req6 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123'><font face='Arial' size='2'>che nessun componente del nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "indicato nella dichiarazione" & vbCrLf
            sStringaSql = sStringaSql & "sostitutiva" & vbCrLf
            sStringaSql = sStringaSql & "allegata e' stato sfrattato per" & vbCrLf
            sStringaSql = sStringaSql & "morosita' da" & vbCrLf
            sStringaSql = sStringaSql & "alloggi ERP negli ultimi 5 anni" & vbCrLf
            sStringaSql = sStringaSql & "e abbia pagato" & vbCrLf
            sStringaSql = sStringaSql & "le somme dovute all'ente gestore;" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='middle'><font face='Arial' size='2'>i)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='56'><font face='Arial' size='2'><BR>" & req7 & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123'><font face='Arial' size='2'>che nessun componente del nucleo familiare indicato nella dichiarazione sostitutiva allegata ha occupato senza titolo alloggi ERP negli ultimi 5 anni;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='top'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123' colspan='2'><font face='Arial' size='2'><b>Periodo di residenza/lavoro in Lombardia</b></font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='top'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123' colspan='2'><font face='Arial' size='2'>" & cmbResidenza.SelectedItem.Text & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='12' valign='top'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1123' colspan='2'>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'><b>Dichiara inoltre le condizioni familiari" & vbCrLf
            sStringaSql = sStringaSql & "e abitative di seguito indicate</b>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</p>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "</div>" & vbCrLf
            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<div align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='4' class='titolo'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='center'><U><b><font face='Arial' size='3'>CONDIZIONI" & vbCrLf
            sStringaSql = sStringaSql & "FAMILIARI</font></b></U><font face='Arial' size='2'><BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>1)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>ANZIANI</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Nuclei familiari di non più di due componenti" & vbCrLf
            sStringaSql = sStringaSql & "o persone singole che" & vbCrLf
            sStringaSql = sStringaSql & "alla data di presentazione" & vbCrLf
            sStringaSql = sStringaSql & "della domanda" & vbCrLf
            sStringaSql = sStringaSql & "abbiano superato 65 anni" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "ovvero quando uno dei due componenti" & vbCrLf
            sStringaSql = sStringaSql & "pur" & vbCrLf
            sStringaSql = sStringaSql & "non avendo tale eta'" & vbCrLf
            sStringaSql = sStringaSql & "sia totalmente inabile" & vbCrLf
            sStringaSql = sStringaSql & "al lavoro" & vbCrLf
            sStringaSql = sStringaSql & "ai sensi delle lett. a) e b) del" & vbCrLf
            sStringaSql = sStringaSql & "punto 6.2 del Bando" & vbCrLf
            sStringaSql = sStringaSql & "o abbia un'eta' superiore" & vbCrLf
            sStringaSql = sStringaSql & "a 75 anni; tali persone singole o nuclei" & vbCrLf
            sStringaSql = sStringaSql & "familiari possono avere minori a carico.</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i1a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>un componente con eta' maggiore di 65 anni" & vbCrLf
            sStringaSql = sStringaSql & "e l'altro totalmente inabile al lavoro o" & vbCrLf
            sStringaSql = sStringaSql & "con eta' maggiore di 75 anni</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i1b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>tutti con eta' maggiore di 65 anni</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i1c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>2)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>DISABILI</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Nuclei familiari nei quali uno o più componenti" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "anche se anagraficamente non conviventi" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "ma presenti nella domanda" & vbCrLf
            sStringaSql = sStringaSql & "siano affetti" & vbCrLf
            sStringaSql = sStringaSql & "da minorazioni o malattie invalidanti che" & vbCrLf
            sStringaSql = sStringaSql & "comportino un handicap grave (art. 3" & vbCrLf
            sStringaSql = sStringaSql & "comma" & vbCrLf
            sStringaSql = sStringaSql & "3" & vbCrLf
            sStringaSql = sStringaSql & "legge 5 febbraio 1992 n. 104)" & vbCrLf
            sStringaSql = sStringaSql & "ovvero" & vbCrLf
            sStringaSql = sStringaSql & "una percentuale di invalidita' certificata" & vbCrLf
            sStringaSql = sStringaSql & "ai sensi della legislazione vigente o dai" & vbCrLf
            sStringaSql = sStringaSql & "competenti organi sanitari regionali. Il" & vbCrLf
            sStringaSql = sStringaSql & "disabile non anagraficamente convivente e'" & vbCrLf
            sStringaSql = sStringaSql & "riconosciuto come componente del nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "solo in presenza di una richiesta di ricongiungimento" & vbCrLf
            sStringaSql = sStringaSql & "al nucleo familiare del richiedente stesso" & vbCrLf
            sStringaSql = sStringaSql & "che comprenda lo stesso disabile nel nucleo" & vbCrLf
            sStringaSql = sStringaSql & "assegnatario.</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i2a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>disabilita' al 100% o handicap grave con accompagnamento</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i2b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>disabilita' al 100% o handicap grave</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i2c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>disabilita' dal 66% al 99%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i2d & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='2'>d)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>3)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>FAMIGLIA DI NUOVA FORMAZIONE</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Nuclei familiari" & vbCrLf
            sStringaSql = sStringaSql & "come definiti" & vbCrLf
            sStringaSql = sStringaSql & "al punto 4.1 lett. b del Bando" & vbCrLf
            sStringaSql = sStringaSql & "da costituirsi prima della consegna" & vbCrLf
            sStringaSql = sStringaSql & "dell'alloggio" & vbCrLf
            sStringaSql = sStringaSql & "ovvero costituitisi entro" & vbCrLf
            sStringaSql = sStringaSql & "i due anni precedenti alla data della domanda;" & vbCrLf
            sStringaSql = sStringaSql & "in tali nuclei familiari possono essere presenti" & vbCrLf
            sStringaSql = sStringaSql & "figli minorenni o minori affidati.</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i3a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>giovane coppia con almeno un componente di" & vbCrLf
            sStringaSql = sStringaSql & "eta' non superiore al trentesimo anno alla" & vbCrLf
            sStringaSql = sStringaSql & "data della domanda e con minori</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i3b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>famiglia di nuova formazione con minori</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i3c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>giovane coppia con almeno un componente di" & vbCrLf
            sStringaSql = sStringaSql & "eta' non superiore al trentesimo anno alla" & vbCrLf
            sStringaSql = sStringaSql & "data della domanda" & vbCrLf
            sStringaSql = sStringaSql & "senza minori</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i3d & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>d)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>famiglia di nuova formazione senza minori</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i3e & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>e)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>4)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>PERSONE SOLE" & vbCrLf
            sStringaSql = sStringaSql & "CON EVENTUALI MINORI A CARICO</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Nuclei di un componente" & vbCrLf
            sStringaSql = sStringaSql & "con un eventuale" & vbCrLf
            sStringaSql = sStringaSql & "minore o più a carico.</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i4a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>persone sole con uno o più o minori" & vbCrLf
            sStringaSql = sStringaSql & "tutti" & vbCrLf
            sStringaSql = sStringaSql & "a carico</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i4b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>persona sola</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i4c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "</div>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<div align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top'>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font size='3' face='Arial'>5)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font size='3' face='Arial'>STATO DI DISOCCUPAZIONE</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Stato di disoccupazione" & vbCrLf
            sStringaSql = sStringaSql & "sopravvenuto successivamente" & vbCrLf
            sStringaSql = sStringaSql & "all'anno di riferimento del reddito e che" & vbCrLf
            sStringaSql = sStringaSql & "perduri all'atto di presentazione della domanda" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "determinando una caduta del reddito complessivo" & vbCrLf
            sStringaSql = sStringaSql & "del nucleo familiare superiore al 50%:</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i5a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>richiedente e altro componente</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i5b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>richiedente o altro componente con eta' maggiore" & vbCrLf
            sStringaSql = sStringaSql & "di 45 anni</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i5c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>richiedente o altro componente con eta' minore" & vbCrLf
            sStringaSql = sStringaSql & "di 45 anni</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i5d & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>d)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>6)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>RICONGIUNZIONE</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Nucleo familiare che necessiti di alloggio" & vbCrLf
            sStringaSql = sStringaSql & "idoneo per accogliervi parente disabile</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i6a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>ricongiunzione del concorrente disabile(*)" & vbCrLf
            sStringaSql = sStringaSql & "(dal 74% al 100%) con ascendenti o discendenti" & vbCrLf
            sStringaSql = sStringaSql & "diretti o collaterali di primo grado presenti" & vbCrLf
            sStringaSql = sStringaSql & "nella domanda;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i6b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>ricongiunzione del concorrente ascendente" & vbCrLf
            sStringaSql = sStringaSql & "o discendente diretto o collaterale di primo" & vbCrLf
            sStringaSql = sStringaSql & "grado con disabile(*) (dal 74% al 100%)" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "residente nel Comune in cui e' stata presentata" & vbCrLf
            sStringaSql = sStringaSql & "la domanda;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i6c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='100%' colspan='4' class='piccolo'><font face='Arial' size='2'>(*) Per disabile si considera una persona" & vbCrLf
            sStringaSql = sStringaSql & "con una grave patologia medica" & vbCrLf
            sStringaSql = sStringaSql & "(psico-fisica)" & vbCrLf
            sStringaSql = sStringaSql & "o con grave handicap" & vbCrLf
            sStringaSql = sStringaSql & "attestati" & vbCrLf
            sStringaSql = sStringaSql & "dagli organi" & vbCrLf
            sStringaSql = sStringaSql & "sanitari regionali" & vbCrLf
            sStringaSql = sStringaSql & "continuativi" & vbCrLf
            sStringaSql = sStringaSql & "nel tempo" & vbCrLf
            sStringaSql = sStringaSql & "o con prognosi infausta.<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>7)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%' colspan='3'><B><font face='Arial' size='3'>CASI PARTICOLARI</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i7a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>nucleo di un componente" & vbCrLf
            sStringaSql = sStringaSql & "con eventualmente" & vbCrLf
            sStringaSql = sStringaSql & "un minore o più a carico" & vbCrLf
            sStringaSql = sStringaSql & "domiciliato o proveniente" & vbCrLf
            sStringaSql = sStringaSql & "da luoghi di detenzione o comunita' terapeutiche</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i7b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>nucleo familiare di emigrato che necessiti" & vbCrLf
            sStringaSql = sStringaSql & "rientrare in Italia</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i7c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='4' class='piccolo' width='100%'><font face='Arial' size='2'>(*) E' volontariato il servizio reso in modo" & vbCrLf
            sStringaSql = sStringaSql & "continuativo" & vbCrLf
            sStringaSql = sStringaSql & "senza fini di lucro" & vbCrLf
            sStringaSql = sStringaSql & "attraverso" & vbCrLf
            sStringaSql = sStringaSql & "prestazioni personali" & vbCrLf
            sStringaSql = sStringaSql & "volontarie e gratuite" & vbCrLf
            sStringaSql = sStringaSql & "(LR 24.07.1993" & vbCrLf
            sStringaSql = sStringaSql & "n. 22) anche presso cooperativa" & vbCrLf
            sStringaSql = sStringaSql & "sociale (LR 1.06.1993" & vbCrLf
            sStringaSql = sStringaSql & "n. 16) almeno da tre" & vbCrLf
            sStringaSql = sStringaSql & "anni precedenti alla data di apertura del" & vbCrLf
            sStringaSql = sStringaSql & "bando.<BR>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</tbody>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<div align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "<table width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<tbody>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='4' class='titolo' width='100%'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='center'><U><B><font face='Arial' size='3'>CONDIZIONI ABITATIVE</font></B></U><font face='Arial' size='2'><BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>8)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='95%'><B><font face='Arial' size='3'>RILASCIO ALLOGGIO</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3' width='95%'><font face='Arial' size='2'>Concorrenti che debbano rilasciare l'alloggio" & vbCrLf
            sStringaSql = sStringaSql & "a seguito di ordinanza" & vbCrLf
            sStringaSql = sStringaSql & "sentenza esecutiva" & vbCrLf
            sStringaSql = sStringaSql & "o verbale di conciliazione" & vbCrLf
            sStringaSql = sStringaSql & "ovvero a seguito" & vbCrLf
            sStringaSql = sStringaSql & "di altro provvedimento giudiziario o amministrativo:</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top' align='center' width='51'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i8a1 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top' align='center' width='5%'><font face='Arial' size='2'>a1)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>e' stato eseguito il provvedimento di rilascio" & vbCrLf
            sStringaSql = sStringaSql & "da meno di un anno dalla presentazione della" & vbCrLf
            sStringaSql = sStringaSql & "domanda e il nucleo familiare si trova nelle" & vbCrLf
            sStringaSql = sStringaSql & "condizioni di cui al punto 6.9 del Bando per il quale" & vbCrLf
            sStringaSql = sStringaSql & "non si deve considerare il periodo temporale" & vbCrLf
            sStringaSql = sStringaSql & "previsto;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top' align='center' width='5%'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i8a2 & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD valign='top' align='center' width='5%'><font face='Arial' size='2'>a2)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1001'><font face='Arial' size='2'>e' stato eseguito il provvedimento di rilascio" & vbCrLf
            sStringaSql = sStringaSql & "da meno di un anno dalla presentazione della" & vbCrLf
            sStringaSql = sStringaSql & "domanda e il concorrente ha stipulato un" & vbCrLf
            sStringaSql = sStringaSql & "nuovo contratto di locazione per un alloggio" & vbCrLf
            sStringaSql = sStringaSql & "non avente i requisiti minimi per l'assegnazione" & vbCrLf
            sStringaSql = sStringaSql & "di un alloggio ERP nella Regione" & vbCrLf
            sStringaSql = sStringaSql & "di cui" & vbCrLf
            sStringaSql = sStringaSql & "all'Art.13" & vbCrLf
            sStringaSql = sStringaSql & "comma 9 del RR n. 1/2004;</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i8b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1001'><font face='Arial' size='2'>e' decorso" & vbCrLf
            sStringaSql = sStringaSql & "al momento della presentazione" & vbCrLf
            sStringaSql = sStringaSql & "della domanda" & vbCrLf
            sStringaSql = sStringaSql & "il termine fissato" & vbCrLf
            sStringaSql = sStringaSql & "per il" & vbCrLf
            sStringaSql = sStringaSql & "rilascio" & vbCrLf
            sStringaSql = sStringaSql & "ovvero e' gia' stato" & vbCrLf
            sStringaSql = sStringaSql & "notificato l'atto" & vbCrLf
            sStringaSql = sStringaSql & "di precetto ai fini dell'esecuzione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i8c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1001'><font face='Arial' size='2'>e' in possesso di titolo esecutivo di sfratto" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "ma non e' decorso al momento di" & vbCrLf
            sStringaSql = sStringaSql & "presentazione" & vbCrLf
            sStringaSql = sStringaSql & "della domanda il termine fissato" & vbCrLf
            sStringaSql = sStringaSql & "per il rilascio</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i8d & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>d)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1001'><font face='Arial' size='2'>il provvedimento di rilascio e' stato motivato" & vbCrLf
            sStringaSql = sStringaSql & "da morosita'." & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i8e & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>e)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>non sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='110%' colspan='4'><font face='Arial' size='2'>(In" & vbCrLf
            sStringaSql = sStringaSql & "caso di rilascio per morosita' il punteggio e' attribuito solo" & vbCrLf
            sStringaSql = sStringaSql & "quando il canone di locazione da corrispondere" & vbCrLf
            sStringaSql = sStringaSql & "integrato delle" & vbCrLf
            sStringaSql = sStringaSql & "spese accessorie" & vbCrLf
            sStringaSql = sStringaSql & "sia stato superiore" & vbCrLf
            sStringaSql = sStringaSql & "di oltre il 5%" & vbCrLf
            sStringaSql = sStringaSql & "all'importo" & vbCrLf
            sStringaSql = sStringaSql & "del canone sopportabile come definito al punto 8 del Bando.&nbsp;" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>9)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<td width='105%' valign='top' align='center' colspan='3'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='left'><B><font face='Arial' size='3'>CONDIZIONE ABITATIVA IMPROPRIA</font></B>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i9a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>richiedenti che dimorino da almeno tre anni" & vbCrLf
            sStringaSql = sStringaSql & "presso strutture di assistenza" & vbCrLf
            sStringaSql = sStringaSql & "o beneficenza" & vbCrLf
            sStringaSql = sStringaSql & "legalmente riconosciute</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i9b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>richiedenti che dimorino in strutture di" & vbCrLf
            sStringaSql = sStringaSql & "tipo alberghiero a carico di" & vbCrLf
            sStringaSql = sStringaSql & "amministrazioni" & vbCrLf
            sStringaSql = sStringaSql & "pubbliche</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i9c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>richiedenti che dimorino da almeno tre anni" & vbCrLf
            sStringaSql = sStringaSql & "in locali non originariamente" & vbCrLf
            sStringaSql = sStringaSql & "destinati alla" & vbCrLf
            sStringaSql = sStringaSql & "residenza abitativa" & vbCrLf
            sStringaSql = sStringaSql & "anche di" & vbCrLf
            sStringaSql = sStringaSql & "tipo rurale" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "ovvero in locali inabitabili" & vbCrLf
            sStringaSql = sStringaSql & "ai sensi del" & vbCrLf
            sStringaSql = sStringaSql & "regolamento d'igiene del comune" & vbCrLf
            sStringaSql = sStringaSql & "o in altro" & vbCrLf
            sStringaSql = sStringaSql & "ricovero procurato a titolo precario</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i9d & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>d)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>non sussiste la condizione" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>10)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<td width='105%' valign='top' align='center' colspan='3'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='left'><B><font face='Arial' size='3'>COABITAZIONE</font></B>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<td width='105%' valign='top' align='center' colspan='3'>" & vbCrLf
            sStringaSql = sStringaSql & "<p align='left'><font face='Arial' size='2'>Richiedenti che abitino da almeno tre anni" & vbCrLf
            sStringaSql = sStringaSql & "con il proprio nucleo familiare in uno stesso" & vbCrLf
            sStringaSql = sStringaSql & "alloggio con altro o più nuclei familiari:</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i10a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>nuclei non legati da vincoli di parentela" & vbCrLf
            sStringaSql = sStringaSql & "o di affinita'</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i10b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>nuclei legati da vincoli di parentela o di" & vbCrLf
            sStringaSql = sStringaSql & "affinita' entro il quarto grado</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i10c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='95%'><font face='Arial' size='2'>non sussiste la condizione" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</div>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<div align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<center>" & vbCrLf
            sStringaSql = sStringaSql & "<table width='1147' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<tbody>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'><B><font face='Arial' size='3'>11)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1086' colspan='5'><B><font face='Arial' size='3'>SOVRAFFOLLAMENTO</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='1086' colspan='5'><font face='Arial' size='2'>Richiedenti che abitino da almeno tre anni" & vbCrLf
            sStringaSql = sStringaSql & "con il proprio nucleo familiare:</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i11a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='974' colspan='3'><font face='Arial' size='2'>in alloggio che presenta forte sovraffollamento" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "vale a dire</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 3 o più persone in 1 vano abitabile</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 14 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 4 o 5 persone in 2 vani abitabili</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 28 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 6 persone in 3 o meno vani abitabili</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 42 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 7 o più persone in 4 o meno vani abitabili</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 56 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i11b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='974' colspan='3'><font face='Arial' size='2'>in alloggio che presenta sovraffollamento" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "vale a dire:</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 1 o 2 persone in 1 vano abitabile</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 14 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 3 persone in 2 vani abitabili</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 28 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 4 o 5 persone in 3 vani abitabili</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 42 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 6 persone in 4 vani abitabili</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 56 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='36'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='304'><font face='Arial' size='2'>- 7 o più persone in 5 vani abitabili</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='622'><font face='Arial' size='2'>= 70 mq + 20%</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='49'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='55' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i11c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='45' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='974' colspan='3'><font face='Arial' size='2'>non sussiste la condizione</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;</p>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</div>" & vbCrLf
            sStringaSql = sStringaSql & "</center>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<TR valign='top'>" & vbCrLf
            sStringaSql = sStringaSql & "<td>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>12)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>CONDIZIONI DELL'ALLOGGIO</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Richiedenti che abitino da almeno tre anni" & vbCrLf
            sStringaSql = sStringaSql & "con il proprio nucleo familiare:</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i12a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>in alloggio privo di servizi igienici interni" & vbCrLf
            sStringaSql = sStringaSql & "o con servizi igienici interni non regolamentari" & vbCrLf
            sStringaSql = sStringaSql & "(vale a dire: lavello" & vbCrLf
            sStringaSql = sStringaSql & "tazza e doccia o vasca)" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "ovvero privi di servizi a rete (acqua o elettricita'" & vbCrLf
            sStringaSql = sStringaSql & "o gas)" & vbCrLf
            sStringaSql = sStringaSql & "ovvero in alloggi per i quali sia" & vbCrLf
            sStringaSql = sStringaSql & "stata accertata dall'ASL la condizione di" & vbCrLf
            sStringaSql = sStringaSql & "antigienicita' ineliminabile con normali interventi" & vbCrLf
            sStringaSql = sStringaSql & "manutentivi</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i12b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>in alloggio privo di impianto di riscaldamento" & vbCrLf
            sStringaSql = sStringaSql & "(centralizzato o con caldaia autonoma)" & vbCrLf
            sStringaSql = sStringaSql & "ovvero" & vbCrLf
            sStringaSql = sStringaSql & "con servizi igienici interni privi di areazione" & vbCrLf
            sStringaSql = sStringaSql & "naturale o meccanica" & vbCrLf
            sStringaSql = sStringaSql & "ovvero in alloggi per" & vbCrLf
            sStringaSql = sStringaSql & "i quali sia stata accertata dall'ASL la condizione" & vbCrLf
            sStringaSql = sStringaSql & "di antigienicita' eliminabile con normali" & vbCrLf
            sStringaSql = sStringaSql & "interventi manutentivi</font> </td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i12c & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>c)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>13)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>BARRIERE ARCHITETTONICHE</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Richiedenti" & vbCrLf
            sStringaSql = sStringaSql & "di cui al precedente punto 2)" & vbCrLf
            sStringaSql = sStringaSql & "che abitino con il proprio nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "in alloggio che" & vbCrLf
            sStringaSql = sStringaSql & "per accessibilita' o per" & vbCrLf
            sStringaSql = sStringaSql & "tipologia" & vbCrLf
            sStringaSql = sStringaSql & "non consenta una normale condizione" & vbCrLf
            sStringaSql = sStringaSql & "abitativa (barriere architettoniche" & vbCrLf
            sStringaSql = sStringaSql & "mancanza" & vbCrLf
            sStringaSql = sStringaSql & "di servizi igienici adeguati o di un locale" & vbCrLf
            sStringaSql = sStringaSql & "separato per la patologia presente)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i13a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i13b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>14)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>CONDIZIONI DI ACCESSIBILITA'</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Richiedenti" & vbCrLf
            sStringaSql = sStringaSql & "di cui ai precedenti punti 1)" & vbCrLf
            sStringaSql = sStringaSql & "e 2)" & vbCrLf
            sStringaSql = sStringaSql & "che abitino con il proprio nucleo familiare" & vbCrLf
            sStringaSql = sStringaSql & "in alloggio che non e' servito da ascensore" & vbCrLf
            sStringaSql = sStringaSql & "ed e' situato superiormente al primo piano</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i14a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i14b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>non sussiste la condizione<BR>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>" & vbCrLf
            sStringaSql = sStringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & vbCrLf
            sStringaSql = sStringaSql & "<table>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>15)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>LONTANANZA DALLA SEDE DI LAVORO</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><font face='Arial' size='2'>Richiedente che risieda da almeno tre anni" & vbCrLf
            sStringaSql = sStringaSql & "in un alloggio situato in localita' diversa" & vbCrLf
            sStringaSql = sStringaSql & "dal Comune in cui presta la propria attivita'" & vbCrLf
            sStringaSql = sStringaSql & "lavorativa esclusiva o principale" & vbCrLf
            sStringaSql = sStringaSql & "ovvero" & vbCrLf
            sStringaSql = sStringaSql & "sia destinato all'atto del bando a prestare" & vbCrLf
            sStringaSql = sStringaSql & "servizio presso nuovi insediamenti o attivita'" & vbCrLf
            sStringaSql = sStringaSql & "produttive in Comune diverso da quello di" & vbCrLf
            sStringaSql = sStringaSql & "residenza; la distanza del luogo di residenza" & vbCrLf
            sStringaSql = sStringaSql & "dal Comune sede di lavoro deve essere superiore" & vbCrLf
            sStringaSql = sStringaSql & "a 90 minuti di percorrenza con gli ordinari" & vbCrLf
            sStringaSql = sStringaSql & "mezzi di trasporto pubblico</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i15a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i15b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%' valign='top'><font face='Arial' size='2'>non sussiste la condizione<BR><BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;</p>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'><B><font face='Arial' size='3'>16)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='3'>AFFITTO ONEROSO (situazione gestita automaticamente dal sistema)</font></B></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td></td>" & vbCrLf
            sStringaSql = sStringaSql & "<TD colspan='3'><font face='Arial' size='2'>Richiedenti titolari da almeno tre anni di" & vbCrLf
            sStringaSql = sStringaSql & "un contratto di locazione relativo all'abitazione" & vbCrLf
            sStringaSql = sStringaSql & "principale il cui &quot;canone integrato&quot;" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "all'atto del bando" & vbCrLf
            sStringaSql = sStringaSql & "sia superiore di oltre" & vbCrLf
            sStringaSql = sStringaSql & "il 5% al &quot;canone sopportabile&quot;.</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i16a & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>a)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%'><font face='Arial' size='2'>sussiste la condizione</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%'></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
            sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & i16b & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='2'>b)</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='85%' valign='top'><font face='Arial' size='2'>non sussiste la condizione" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>&nbsp;" & vbCrLf
            sStringaSql = sStringaSql & "<p>&nbsp;" & vbCrLf
            sStringaSql = sStringaSql & "</p>" & vbCrLf
            sStringaSql = sStringaSql & "<p><font face='Arial' size='2'><BR>" & vbCrLf
            'sStringaSql = sStringaSql & "Il sottoscritto dichiara infine di aver preso" & vbCrLf
            'sStringaSql = sStringaSql & "conoscenza di tutte le norme contenute nel" & vbCrLf
            'sStringaSql = sStringaSql & "bando di assegnazione degli immobili ERP" & vbCrLf
            'sStringaSql = sStringaSql & "e di possedere tutti i requisiti di partecipazione" & vbCrLf
            'sStringaSql = sStringaSql & "in esso indicati e di autorizzare" & vbCrLf
            'sStringaSql = sStringaSql & "ai sensi" & vbCrLf
            'sStringaSql = sStringaSql & "dell'art. 10 della legge 31.12.1996" & vbCrLf
            'sStringaSql = sStringaSql & "n. 675" & vbCrLf
            'sStringaSql = sStringaSql & "" & vbCrLf
            'sStringaSql = sStringaSql & "il trattamento dei dati da parte della Regione" & vbCrLf
            'sStringaSql = sStringaSql & "Lombardia" & vbCrLf
            'sStringaSql = sStringaSql & "per l'esclusiva finalita' prevista" & vbCrLf
            'sStringaSql = sStringaSql & "dal bando ed in quanto obbligatori per la" & vbCrLf
            'sStringaSql = sStringaSql & "stessa" & vbCrLf
            'sStringaSql = sStringaSql & "nonche' l'elaborazione in forma anonima" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            'sStringaSql = sStringaSql & "Per ogni ulteriore trattamento dei" & vbCrLf
            'sStringaSql = sStringaSql & "dati verra'" & vbCrLf
            'sStringaSql = sStringaSql & "richiesta esplicita autorizzazione" & vbCrLf
            'sStringaSql = sStringaSql & "e sono" & vbCrLf
            'sStringaSql = sStringaSql & "fatte salve le facolta' del sottoscritto" & vbCrLf
            'sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font>" & vbCrLf
            sStringaSql = sStringaSql & "</p>" & vbCrLf
            sStringaSql = sStringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
            sStringaSql = sStringaSql & "<TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "<tr>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='50%' valign='top'><font face='Arial' size='2'>Data " & Format(Now, "dd/MM/yyyy") & "</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "<td width='50%' align='center'><font face='Arial' size='2'><BR>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "<BR>" & vbCrLf
            sStringaSql = sStringaSql & "</font></td>" & vbCrLf
            sStringaSql = sStringaSql & "</tr>" & vbCrLf
            sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            sStringaSql = sStringaSql & "</table>"
            sStringaSql = sStringaSql & "<p>&nbsp;</p>"

            If TAB_T3 <> "" Then
                sStringaSql = sStringaSql & vbCrLf & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" & TAB_T3
            End If
            ''sStringaSql = sStringaSql & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
            ''sStringaSql = sStringaSql & "<tr>"
            ''sStringaSql = sStringaSql & "<td width='100%'><font face='Arial' size='1'>DICHIARAZIONE RESA E SOTTOSCRITTA IN NOME E PER CONTO DEL RICHIEDENTE DA<BR>"
            ''sStringaSql = sStringaSql & "(COGNOME)___________________________________(NOME)___________________________________<BR>"
            ''sStringaSql = sStringaSql & "(DOC. DIRICONOSCIMENTO, N°.)________________________<BR>"
            ''sStringaSql = sStringaSql & "IN QUALITA' DI (GRADO PARENTELA)_________________________, COMPONENENTE MAGGIORENNE IL NUCLEO FAMILIARE<br>"
            ''sStringaSql = sStringaSql & "RICHIEDENTE L'ALLOGGIO, MUNITO DI DELEGA ALLEGATA AGLIA ATTI.<br>"
            ''sStringaSql = sStringaSql & "<br>"
            ''sStringaSql = sStringaSql & "L'OPERATORE______________</font></td>"
            ''sStringaSql = sStringaSql & "</tr>"
            ''sStringaSql = sStringaSql & "</table>"
            ''sStringaSql = sStringaSql & "<font face='Arial' size='2'><BR>" & vbCrLf
            ''sStringaSql = sStringaSql & "( ) Annotazione estremi documento di identità" & vbCrLf
            ''sStringaSql = sStringaSql & "__________________________ <BR>" & vbCrLf
            ''sStringaSql = sStringaSql & "<BR>" & vbCrLf
            ''sStringaSql = sStringaSql & "Firma apposta dal dichiarante in presenza" & vbCrLf
            ''sStringaSql = sStringaSql & "di _________________________ <BR>" & vbCrLf
            ''sStringaSql = sStringaSql & "<BR>" & vbCrLf
            ''sStringaSql = sStringaSql & "(*) ai sensi dell'art. 5 comma 3 della legge" & vbCrLf
            ''sStringaSql = sStringaSql & "15.5.1997 n. 127 la firma" & vbCrLf
            ''sStringaSql = sStringaSql & "apposta in calce" & vbCrLf
            ''sStringaSql = sStringaSql & "" & vbCrLf
            ''sStringaSql = sStringaSql & "non deve essere autenticata<BR>" & vbCrLf
            ''sStringaSql = sStringaSql & "</font>" & vbCrLf
            ''sStringaSql = sStringaSql & "</td>" & vbCrLf
            ''sStringaSql = sStringaSql & "</tr>" & vbCrLf
            ''sStringaSql = sStringaSql & "</TBODY>" & vbCrLf
            ''sStringaSql = sStringaSql & "</table>" & vbCrLf
            ''sStringaSql = sStringaSql & "<p align='left'>" & vbCrLf
            ''sStringaSql = sStringaSql & "<font face='Arial' size='2'>" & vbCrLf
            ''sStringaSql = sStringaSql & "<BR><BR><BR>" & protocollo & "</font>" & vbCrLf
            ''sStringaSql = sStringaSql & "" & vbCrLf








            par.OracleConn.Close()


        Catch ex As Exception
            par.OracleConn.Close()
            Label4.Visible = True
            Label4.Text = ex.Message
        Finally

        End Try
    End Function

    Private Sub InserisciDati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            UtenteMail = ""
            PWUtenteMail = ""
            smtp = ""
            MittenteMail = ""
            SSL = "0"

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=57"
            Dim myReader111 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                UtenteMail = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=58"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                PWUtenteMail = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=59"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                smtp = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=60"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                MittenteMail = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=115"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                SSL = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=116"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                PORTA_SSL = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()


            par.cmd.CommandText = "SELECT ID FROM BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lIndice_Bando = myReader(0)
            End If
            myReader.Close()

            Dim lsiFrutto As New ListItem(" ", "-1")

            RiempiDList(par.OracleConn, cmbNazioneRes, "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            RiempiDList(par.OracleConn, cmbPrRes, "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            RiempiDList(par.OracleConn, cmbTipoIRes, "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            RiempiDList(par.OracleConn, cmbProvRec, "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            cmbProvRec.Items.Add(lsiFrutto)
            cmbProvRec.SelectedIndex = -1
            cmbProvRec.Items.FindByValue(-1).Selected = True

            RiempiDList(par.OracleConn, cmbTipoIRec, "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            RiempiDListParenti(par.OracleConn, cmbParentela, "SELECT DESCRIZIONE,COD FROM T_TIPO_PARENTELA ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            cmbParentela.SelectedIndex = -1
            cmbParentela.Items.FindByText("CAPOFAMIGLIA").Selected = True




            RiempiDListImmobile(par.OracleConn, cmbImmobCat1_1, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            cmbImmobCat1_1.Items.Add(lsiFrutto)
            cmbImmobCat1_1.SelectedIndex = -1
            cmbImmobCat1_1.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList28, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList28.Items.Add(lsiFrutto)
            DropDownList28.SelectedIndex = -1
            DropDownList28.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList29, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList29.Items.Add(lsiFrutto)
            DropDownList29.SelectedIndex = -1
            DropDownList29.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList30, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList30.Items.Add(lsiFrutto)
            DropDownList30.SelectedIndex = -1
            DropDownList30.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList31, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList31.Items.Add(lsiFrutto)
            DropDownList31.SelectedIndex = -1
            DropDownList31.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList32, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList32.Items.Add(lsiFrutto)
            DropDownList32.SelectedIndex = -1
            DropDownList32.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList33, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList33.Items.Add(lsiFrutto)
            DropDownList33.SelectedIndex = -1
            DropDownList33.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList34, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList34.Items.Add(lsiFrutto)
            DropDownList34.SelectedIndex = -1
            DropDownList34.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList35, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList35.Items.Add(lsiFrutto)
            DropDownList35.SelectedIndex = -1
            DropDownList35.Items.FindByValue(-1).Selected = True
            'RiempiDList(par.OracleConn, DropDownList36, "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")
            DropDownList36.Items.Add(lsiFrutto)
            DropDownList36.SelectedIndex = -1
            DropDownList36.Items.FindByValue(-1).Selected = True


            RiempiDList(par.OracleConn, cmbResidenza, "SELECT DESCRIZIONE,ID FROM T_RESIDENZA_LOMBARDIA ORDER BY ID ASC", "DESCRIZIONE", "ID")
            cmbResidenza.Items.Add(lsiFrutto)
            cmbResidenza.SelectedIndex = -1
            cmbResidenza.Items.FindByValue(-1).Selected = True

            RiempiDList(par.OracleConn, cmbPresentaD, "SELECT DESCRIZIONE,COD FROM T_CAUSALI_DOMANDA ORDER BY COD ASC", "DESCRIZIONE", "COD")
            cmbPresentaD.Items.Add(lsiFrutto)
            cmbPresentaD.SelectedIndex = -1
            cmbPresentaD.Items.FindByValue(-1).Selected = True

            RiempiDList(par.OracleConn, cmbF1, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=0 order by id asc", "DESCRIZIONE", "ID")
            lsiFrutto = New ListItem("Non sussiste la condizione", "-1")
            cmbF1.Items.Add(lsiFrutto)
            cmbF1.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbF2, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=1 order by id asc", "DESCRIZIONE", "ID")
            cmbF2.Items.Add(lsiFrutto)
            cmbF2.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbF3, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=2 order by id asc", "DESCRIZIONE", "ID")
            cmbF3.Items.Add(lsiFrutto)
            cmbF3.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbF4, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=3 order by id asc", "DESCRIZIONE", "ID")
            cmbF4.Items.Add(lsiFrutto)
            cmbF4.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbF5, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=4 order by id asc", "DESCRIZIONE", "ID")
            cmbF5.Items.Add(lsiFrutto)
            cmbF5.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbF6, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=5 order by id asc", "DESCRIZIONE", "ID")
            cmbF6.Items.Add(lsiFrutto)
            cmbF6.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbF7, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=6 order by id asc", "DESCRIZIONE", "ID")
            cmbF7.Items.Add(lsiFrutto)
            cmbF7.Items.FindByText("Non sussiste la condizione").Selected = True


            RiempiDList(par.OracleConn, cmbA1, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=7 order by id asc", "DESCRIZIONE", "ID")
            cmbA1.Items.Add(lsiFrutto)
            cmbA1.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA2, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=8 order by id asc", "DESCRIZIONE", "ID")
            cmbA2.Items.Add(lsiFrutto)
            cmbA2.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA3, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=9 order by id asc", "DESCRIZIONE", "ID")
            cmbA3.Items.Add(lsiFrutto)
            cmbA3.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA4, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=10 order by id asc", "DESCRIZIONE", "ID")
            cmbA4.Items.Add(lsiFrutto)
            cmbA4.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA5, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=11 order by id asc", "DESCRIZIONE", "ID")
            cmbA5.Items.Add(lsiFrutto)
            cmbA5.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA6, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=12 order by id asc", "DESCRIZIONE", "ID")
            cmbA6.Items.Add(lsiFrutto)
            cmbA6.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA7, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=13 order by id asc", "DESCRIZIONE", "ID")
            cmbA7.Items.Add(lsiFrutto)
            cmbA7.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA8, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=14 order by id asc", "DESCRIZIONE", "ID")
            cmbA8.Items.Add(lsiFrutto)
            cmbA8.Items.FindByText("Non sussiste la condizione").Selected = True

            RiempiDList(par.OracleConn, cmbA9, "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=15 order by id asc", "DESCRIZIONE", "ID")
            cmbA9.Items.Add(lsiFrutto)
            cmbA9.Items.FindByText("Non sussiste la condizione").Selected = True



            cmbNazioneRes.SelectedIndex = -1
            cmbNazioneRes.Items.FindByText("ITALIA").Selected = True

            cmbPrRes.SelectedIndex = -1
            cmbPrRes.Items.FindByText("MI").Selected = True



            'cmbProvRec.SelectedIndex = -1
            'cmbProvRec.Items.FindByText("MI").Selected = True

            cmbComuneRes.SelectedIndex = -1
            RiempiDList(par.OracleConn, cmbComuneRes, "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI' ORDER BY NOME ASC", "NOME", "ID")

            cmbComuneRes.SelectedIndex = -1
            cmbComuneRes.Items.FindByText("MILANO").Selected = True

            cmbComuneRec.SelectedIndex = -1
            'RiempiDList(par.OracleConn, cmbComuneRec, "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI' ORDER BY NOME ASC", "NOME", "ID")

            cmbTipoIRes.SelectedIndex = -1
            cmbTipoIRes.Items.FindByText("VIA").Selected = True

            cmbTipoIRec.SelectedIndex = -1
            cmbTipoIRec.Items.FindByText("VIA").Selected = True

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(txtCF.Text, 12, 4) & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read() Then
                LBLNASCITA.Text = myReader1("ID")
                If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                    txtNato.Text = par.IfNull(myReader1("NOME"), "")
                Else
                    txtNato.Text = par.IfNull(myReader1("NOME"), "") & " (" & par.IfNull(myReader1("SIGLA"), "") & ")"
                End If
                Dim MIADATA As String

                If Val(Mid(txtCF.Text, 10, 2)) > 40 Then
                    MIADATA = Format(Val(Mid(txtCF.Text, 10, 2)) - 40, "00")
                Else
                    MIADATA = Mid(txtCF.Text, 10, 2)
                End If
                Select Case Mid(txtCF.Text, 9, 1)
                    Case "A"
                        MIADATA = MIADATA & "/01"
                    Case "B"
                        MIADATA = MIADATA & "/02"
                    Case "C"
                        MIADATA = MIADATA & "/03"
                    Case "D"
                        MIADATA = MIADATA & "/04"
                    Case "E"
                        MIADATA = MIADATA & "/05"
                    Case "H"
                        MIADATA = MIADATA & "/06"
                    Case "L"
                        MIADATA = MIADATA & "/07"
                    Case "M"
                        MIADATA = MIADATA & "/08"
                    Case "P"
                        MIADATA = MIADATA & "/09"
                    Case "R"
                        MIADATA = MIADATA & "/10"
                    Case "S"
                        MIADATA = MIADATA & "/11"
                    Case "T"
                        MIADATA = MIADATA & "/12"
                End Select
                If Mid(txtCF.Text, 7, 1) = "0" Then
                    MIADATA = MIADATA & "/200" & Mid(txtCF.Text, 8, 1)
                Else
                    MIADATA = MIADATA & "/19" & Mid(txtCF.Text, 7, 2)
                End If
                txtDataNascita.Text = MIADATA
            End If

            lsiFrutto = New ListItem("NO", "0")
            cmbAccompagnamento.Items.Add(lsiFrutto)
            cmbAccompagnamentoC1.Items.Add(lsiFrutto)
            cmbAccompagnamentoC2.Items.Add(lsiFrutto)
            cmbAccompagnamentoC3.Items.Add(lsiFrutto)
            cmbAccompagnamentoC4.Items.Add(lsiFrutto)
            cmbAccompagnamentoC5.Items.Add(lsiFrutto)
            cmbAccompagnamentoC6.Items.Add(lsiFrutto)
            cmbAccompagnamentoC7.Items.Add(lsiFrutto)
            cmbAccompagnamentoC8.Items.Add(lsiFrutto)
            cmbAccompagnamentoC9.Items.Add(lsiFrutto)



            'cmbC1_Ind.Items.Add(lsiFrutto)
            'cmbC2_Ind.Items.Add(lsiFrutto)
            'cmbC3_Ind.Items.Add(lsiFrutto)
            'cmbC4_Ind.Items.Add(lsiFrutto)
            'cmbC5_Ind.Items.Add(lsiFrutto)
            'cmbC6_Ind.Items.Add(lsiFrutto)
            'cmbC7_Ind.Items.Add(lsiFrutto)
            'cmbC8_Ind.Items.Add(lsiFrutto)
            'cmbC9_Ind.Items.Add(lsiFrutto)

            lsiFrutto = New ListItem("SI", "1")

            cmbAccompagnamento.Items.Add(lsiFrutto)
            cmbAccompagnamento.SelectedIndex = -1
            cmbAccompagnamento.SelectedItem.Text = "NO"

            cmbAccompagnamentoC1.Items.Add(lsiFrutto)
            cmbAccompagnamentoC1.SelectedIndex = -1
            cmbAccompagnamentoC1.SelectedItem.Text = "NO"

            cmbAccompagnamentoC2.Items.Add(lsiFrutto)
            cmbAccompagnamentoC2.SelectedIndex = -1
            cmbAccompagnamentoC2.SelectedItem.Text = "NO"

            cmbAccompagnamentoC3.Items.Add(lsiFrutto)
            cmbAccompagnamentoC3.SelectedIndex = -1
            cmbAccompagnamentoC3.SelectedItem.Text = "NO"


            cmbAccompagnamentoC4.Items.Add(lsiFrutto)
            cmbAccompagnamentoC4.SelectedIndex = -1
            cmbAccompagnamentoC4.SelectedItem.Text = "NO"

            cmbAccompagnamentoC5.Items.Add(lsiFrutto)
            cmbAccompagnamentoC5.SelectedIndex = -1
            cmbAccompagnamentoC5.SelectedItem.Text = "NO"

            cmbAccompagnamentoC6.Items.Add(lsiFrutto)
            cmbAccompagnamentoC6.SelectedIndex = -1
            cmbAccompagnamentoC6.SelectedItem.Text = "NO"

            cmbAccompagnamentoC7.Items.Add(lsiFrutto)
            cmbAccompagnamentoC7.SelectedIndex = -1
            cmbAccompagnamentoC7.SelectedItem.Text = "NO"

            cmbAccompagnamentoC8.Items.Add(lsiFrutto)
            cmbAccompagnamentoC8.SelectedIndex = -1
            cmbAccompagnamentoC8.SelectedItem.Text = "NO"

            cmbAccompagnamentoC9.Items.Add(lsiFrutto)
            cmbAccompagnamentoC9.SelectedIndex = -1
            cmbAccompagnamentoC9.SelectedItem.Text = "NO"


            'cmbC1_Ind.Items.Add(lsiFrutto)
            'cmbC1_Ind.SelectedIndex = -1
            'cmbC1_Ind.SelectedItem.Text = "NO"

            'cmbC2_Ind.Items.Add(lsiFrutto)
            'cmbC2_Ind.SelectedIndex = -1
            'cmbC2_Ind.SelectedItem.Text = "NO"

            'cmbC3_Ind.Items.Add(lsiFrutto)
            'cmbC3_Ind.SelectedIndex = -1
            'cmbC3_Ind.SelectedItem.Text = "NO"

            'cmbC4_Ind.Items.Add(lsiFrutto)
            'cmbC4_Ind.SelectedIndex = -1
            'cmbC4_Ind.SelectedItem.Text = "NO"

            'cmbC5_Ind.Items.Add(lsiFrutto)
            'cmbC5_Ind.SelectedIndex = -1
            'cmbC5_Ind.SelectedItem.Text = "NO"

            'cmbC6_Ind.Items.Add(lsiFrutto)
            'cmbC6_Ind.SelectedIndex = -1
            'cmbC6_Ind.SelectedItem.Text = "NO"

            'cmbC7_Ind.Items.Add(lsiFrutto)
            'cmbC7_Ind.SelectedIndex = -1
            'cmbC7_Ind.SelectedItem.Text = "NO"

            'cmbC8_Ind.Items.Add(lsiFrutto)
            'cmbC8_Ind.SelectedIndex = -1
            'cmbC8_Ind.SelectedItem.Text = "NO"

            'cmbC9_Ind.Items.Add(lsiFrutto)
            'cmbC9_Ind.SelectedIndex = -1
            'cmbC9_Ind.SelectedItem.Text = "NO"

            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Label4.Text = ex.Message
        End Try
    End Sub


    Private Function ValoreSI_NO(ByVal v As Boolean) As String
        If v = True Then
            ValoreSI_NO = "SI"
        Else
            ValoreSI_NO = "NO"
        End If
    End Function
    Private Function RiempiDList(ByVal OracleConn As Oracle.DataAccess.Client.OracleConnection, ByVal sNomeDlist As Object, ByVal sQuery As String, ByVal sCampoTesto As String, ByVal sCampoChiave As String) As DropDownList
        Dim dlist As DropDownList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim ds As New Data.DataSet()



        dlist = sNomeDlist

        da = New Oracle.DataAccess.Client.OracleDataAdapter(sQuery, OracleConn)
        da.Fill(ds)

        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        RiempiDList = dlist

        da.Dispose()
        da = Nothing

        dlist.DataSource = Nothing
        dlist = Nothing

        ds.Clear()
        ds.Dispose()
        ds = Nothing

    End Function

    Private Function RiempiDListParenti(ByVal OracleConn As Oracle.DataAccess.Client.OracleConnection, ByVal sNomeDlist As Object, ByVal sQuery As String, ByVal sCampoTesto As String, ByVal sCampoChiave As String) As DropDownList
        Dim dlist As DropDownList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim ds As New Data.DataSet()



        dlist = sNomeDlist

        da = New Oracle.DataAccess.Client.OracleDataAdapter(sQuery, OracleConn)
        da.Fill(ds)

        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        RiempiDListParenti = dlist


        dlist = cmbParentelaC1
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC2
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC3
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC4
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC5
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC6
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC7
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC8
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = cmbParentelaC9
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()


        da.Dispose()
        da = Nothing

        dlist.DataSource = Nothing
        dlist = Nothing

        ds.Clear()
        ds.Dispose()
        ds = Nothing

    End Function


    Private Function RiempiDListImmobile(ByVal OracleConn As Oracle.DataAccess.Client.OracleConnection, ByVal sNomeDlist As Object, ByVal sQuery As String, ByVal sCampoTesto As String, ByVal sCampoChiave As String) As DropDownList
        Dim dlist As DropDownList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim ds As New Data.DataSet()





        da = New Oracle.DataAccess.Client.OracleDataAdapter(sQuery, OracleConn)
        da.Fill(ds)

        dlist = sNomeDlist
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        RiempiDListImmobile = dlist

        dlist = DropDownList28
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList29
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList30
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList31
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList32
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList33
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList34
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList35
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()

        dlist = DropDownList36
        dlist.Items.Clear()
        dlist.DataSource = ds
        dlist.DataTextField = sCampoTesto
        dlist.DataValueField = sCampoChiave
        dlist.DataBind()



        da.Dispose()
        da = Nothing

        dlist.DataSource = Nothing
        dlist = Nothing

        ds.Clear()
        ds.Dispose()
        ds = Nothing

    End Function

    Public Property smtp() As String
        Get
            If Not (ViewState("par_smtp") Is Nothing) Then
                Return CStr(ViewState("par_smtp"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_smtp") = value
        End Set

    End Property

    Public Property PWUtenteMail() As String
        Get
            If Not (ViewState("par_pwUtenteMail") Is Nothing) Then
                Return CStr(ViewState("par_pwUtenteMail"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_pwUtenteMail") = value
        End Set

    End Property

    Public Property MittenteMail() As String
        Get
            If Not (ViewState("par_MittenteMail") Is Nothing) Then
                Return CStr(ViewState("par_MittenteMail"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_MittenteMail") = value
        End Set

    End Property

    Public Property SSL() As String
        Get
            If Not (ViewState("par_SSL") Is Nothing) Then
                Return CStr(ViewState("par_SSL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_SSL") = value
        End Set

    End Property

    Public Property PORTA_SSL() As String
        Get
            If Not (ViewState("par_PORTA_SSL") Is Nothing) Then
                Return CStr(ViewState("par_PORTA_SSL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_PORTA_SSL") = value
        End Set

    End Property

    Public Property UtenteMail() As String
        Get
            If Not (ViewState("par_UtenteMail") Is Nothing) Then
                Return CStr(ViewState("par_UtenteMail"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_UtenteMail") = value
        End Set

    End Property


    Public Property lIndice_Bando() As Long
        Get
            If Not (ViewState("par_lIndice_Bando") Is Nothing) Then
                Return CLng(ViewState("par_lIndice_Bando"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIndice_Bando") = value
        End Set

    End Property

    Public Property sValoreNM() As String
        Get
            If Not (ViewState("par_sValoreNM") Is Nothing) Then
                Return CStr(ViewState("par_sValoreNM"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sValoreNM") = value
        End Set
    End Property

    Public Property sValoreN() As String
        Get
            If Not (ViewState("par_sValoreN") Is Nothing) Then
                Return CStr(ViewState("par_sValoreN"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sValoreN") = value
        End Set
    End Property

    Public Property sValoreC() As String
        Get
            If Not (ViewState("par_sValoreC") Is Nothing) Then
                Return CStr(ViewState("par_sValoreC"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sValoreC") = value
        End Set
    End Property

    Public Property sValoreCF() As String
        Get
            If Not (ViewState("par_sValoreCF") Is Nothing) Then
                Return CStr(ViewState("par_sValoreCF"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sValoreCF") = value
        End Set
    End Property

    Public Property sStringaSql() As String
        Get
            If Not (ViewState("par_sStringaSql") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSql"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sStringaSql") = value
        End Set
    End Property

    Protected Sub cmbNazioneRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazioneRes.SelectedIndexChanged
        If cmbNazioneRes.Items.FindByText("ITALIA").Selected = False Then
            cmbPrRes.Visible = False
            cmbComuneRes.Enabled = False
        Else
            cmbPrRes.Visible = True
            cmbComuneRes.Enabled = True
        End If

    End Sub

    Protected Sub cmbPrRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrRes.SelectedIndexChanged
        Dim item As ListItem

        item = cmbPrRes.SelectedItem
        RiempiDList(par.OracleConn, cmbComuneRes, "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

    End Sub

    Protected Sub cmbTipoIRec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoIRec.SelectedIndexChanged

    End Sub

    Protected Sub cmbProvRec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvRec.SelectedIndexChanged
        Dim item As ListItem

        item = cmbProvRec.SelectedItem
        RiempiDList(par.OracleConn, cmbComuneRec, "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

    End Sub

    Protected Sub Wizard1_ActiveStepChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Wizard1.ActiveStepChanged
        'If TextBox1.Text <> "" Then
        '    '    If C1 = 1 Then
        '    Wizard1.ActiveStepIndex = 2
        'End If
        '    If Label2C2.Visible = True Or Label3C2.Visible = True Then
        '        Wizard1.ActiveStepIndex = 3
        '    End If
        '    If Label2C3.Visible = True Or Label3C3.Visible = True Then
        '        Wizard1.ActiveStepIndex = 4
        '    End If
        '    If Label2C4.Visible = True Or Label3C4.Visible = True Then
        '        Wizard1.ActiveStepIndex = 5
        '    End If
        '    If Label2C5.Visible = True Or Label3C5.Visible = True Then
        '        Wizard1.ActiveStepIndex = 6
        '    End If
        '    If Label2C6.Visible = True Or Label3C6.Visible = True Then
        '        Wizard1.ActiveStepIndex = 7
        '    End If
        '    If Label2C7.Visible = True Or Label3C7.Visible = True Then
        '        Wizard1.ActiveStepIndex = 8
        '    End If
        '    If Label2C8.Visible = True Or Label3C8.Visible = True Then
        '        Wizard1.ActiveStepIndex = 9
        '    End If
        '    If Label2C9.Visible = True Or Label3C9.Visible = True Then
        '        Wizard1.ActiveStepIndex = 10
        '    End If
    End Sub


    Protected Sub Wizard1_FinishButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles Wizard1.FinishButtonClick


        If Wizard1.ActiveStepIndex = 16 Then
            Try
                If Session.Item("FATTO") <> "0" Then
                    Exit Sub
                End If

                Label110.Visible = False
                Label1111.Text = "Errore/Mancante"
                If txtmail.Text = "" Then
                    Label110.Visible = True
                    e.Cancel = True
                    Exit Sub
                End If
                If txtConfermaMail.Text = "" Then
                    Label1111.Visible = True
                    e.Cancel = True
                    Exit Sub
                End If
                If UCase(txtConfermaMail.Text) <> UCase(txtmail.Text) Then
                    Label1111.Visible = True
                    Label1111.Text = "Diverso da indirizzo e-mail"
                    e.Cancel = True
                    Exit Sub
                End If

                Dim StringaSQL As String = ""

                par.OracleConn.Open()

                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                Dim NomeFile1 As String = UCase(txtCF.Text) & "_" & Format(Now, "yyyyMMddHHmmss")
                Dim NomeFile As String = Server.MapPath("..\ALLEGATI\DOMANDE\") & NomeFile1


                Dim LUOGORES As Long = 0
                Dim ID_TIPO_CAT_AB As String = "NULL"

                Dim idComponente As Long
                Dim RESIDENZA As String = "0"

                If DropDownList36.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList36.SelectedValue
                End If

                If DropDownList35.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList35.SelectedValue
                End If

                If DropDownList34.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList34.SelectedValue
                End If

                If DropDownList33.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList33.SelectedValue
                End If

                If DropDownList32.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList32.SelectedValue
                End If

                If DropDownList31.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList31.SelectedValue
                End If

                If DropDownList30.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList30.SelectedValue
                End If

                If DropDownList29.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList29.SelectedValue
                End If

                If DropDownList28.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = DropDownList28.SelectedValue
                End If

                If cmbImmobCat1_1.SelectedValue <> -1 Then
                    ID_TIPO_CAT_AB = cmbImmobCat1_1.SelectedValue
                End If

                If cmbNazioneRes.SelectedItem.Text <> "ITALIA" Then
                    LUOGORES = cmbNazioneRes.SelectedValue
                Else
                    LUOGORES = cmbComuneRes.SelectedValue
                End If


                StringaSQL = "Insert into DICHIARAZIONI_WEB (ID, ID_BANDO, PG, ANNO_PG, DATA_PG, LUOGO, DATA, NOTE, ID_STATO," _
                & "ID_LUOGO_NAS_DNTE, TELEFONO_DNTE, ID_LUOGO_RES_DNTE, ID_TIPO_IND_RES_DNTE, IND_RES_DNTE, CIVICO_RES_DNTE, " _
                & "N_COMP_NUCLEO, N_INV_100_CON, N_INV_100_SENZA, N_INV_100_66, ID_TIPO_CAT_AB, ANNO_SIT_ECONOMICA, LUOGO_INT_ERP, " _
                & "DATA_INT_ERP, LUOGO_S, DATA_S, ID_CAF, PROGR_DNTE, FL_GIA_TITOLARE, CAP_RES_DNTE)" _
                & "Values " _
                & "(SEQ_DICHIARAZIONI_WEB.NEXTVAL, " & lIndice_Bando & " , '0000000000', " & Year(Now) & ", '" & Format(Now, "yyyyMMdd") & "','Milano', '" & Format(Now, "yyyyMMdd") _
                & "', NULL, 1, " & LBLNASCITA.Text & ", " _
                & "'" & par.PulisciStrSql(txtTelefono.Text) & "', " & LUOGORES & ", " & cmbTipoIRes.SelectedValue & ", '" _
                & par.PulisciStrSql(txtIndirizzo.Text) & "', '" & par.PulisciStrSql(txtCivico.Text) & "'," _
                & Val(sValoreNM) + 1 & ", " & N_INV_100_ACC & "," & N_INV_100_NO_ACC & "," & N_INV_100_66 & ", " & ID_TIPO_CAT_AB & ", " _
                & "2017, 'Milano', '" & Format(Now, "yyyyMMdd") & "', 'Milano', '" & Format(Now, "yyyyMMdd") & "', 6, 0, '" & Valore01(chTitolare.Checked) & "', '" & txtCAP.Text & "')"
                par.cmd.CommandText = StringaSQL
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_WEB.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idDichiarazione = myReader(0)
                End If
                myReader.Close()


                ''salva componenti
                StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                            & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                            & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",0,'" _
                            & par.PulisciStrSql(UCase(txtCognome.Text)) & "','" _
                            & par.PulisciStrSql(UCase(txtNome.Text)) & "'," _
                            & cmbParentela.SelectedValue & ",'" _
                            & par.PulisciStrSql(UCase(txtCF.Text)) & "','" _
                            & par.PulisciStrSql(cmbInvalidità.SelectedValue) & "','" _
                            & par.PulisciStrSql(par.AggiustaData(txtDataNascita.Text)) & "','" _
                            & par.PulisciStrSql(txtASL.Text) & "','" _
                            & cmbAccompagnamento.SelectedValue & "','" & par.RicavaSesso(txtCF.Text) & "')"
                par.cmd.CommandText = StringaSQL
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idComponente = myReader(0)
                End If
                myReader.Close()

                If cmbInvalidità.SelectedValue = 100 And cmbAccompagnamento.SelectedValue = 1 And txtSpese.Text > 10000 Then
                    StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                        & idComponente & "," _
                        & txtSpese.Text & ",'" _
                        & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If


                If txtImporto1.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                               & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                               & "'" & par.PulisciStrSql(txtCod.Text) & "','" & par.PulisciStrSql(txtIntermediario1.Text) & "'," & txtImporto1.Text & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If
                If txtImporto2.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                               & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                               & "'" & par.PulisciStrSql(txtCodice2.Text) & "','" & par.PulisciStrSql(txtIntermediario2.Text) & "'," & txtImporto2.Text & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If
                If txtValore1.Text > 0 Then
                    If txtImmob1_5.Checked = True Then
                        RESIDENZA = "1"
                    Else
                        RESIDENZA = "0"
                    End If
                    StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1_1.SelectedValue _
                                & "," & cmbPercProprieta1.SelectedValue _
                                & "," & txtValore1.Text _
                                & "," & txtMutuo1.Text _
                                & ",'" & RESIDENZA & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If
                If txtValore2.Text > 0 Then
                    If CheckBox1.Checked = True Then
                        RESIDENZA = "1"
                    Else
                        RESIDENZA = "0"
                    End If
                    StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2_1.SelectedValue _
                                & "," & cmbPercProprieta2.SelectedValue _
                                & "," & txtValore2.Text _
                                & "," & txtMutuo2.Text _
                                & ",'" & RESIDENZA & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                If txtIrpef1.Text > 0 Or txtAgrari1.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                   & idComponente & "," _
                                   & par.IfEmpty(txtIrpef1.Text, 0) _
                                   & "," & par.IfEmpty(txtAgrari1.Text, 0) & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If
                If txtIrpef2.Text > 0 Or txtAgrari2.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                   & idComponente & "," _
                                   & par.IfEmpty(txtIrpef2.Text, 0) _
                                   & "," & par.IfEmpty(txtAgrari2.Text, 0) & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                If txtAltri1.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                & txtAltri1.Text _
                                & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                If txtAltri2.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                & txtAltri2.Text _
                                & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                If txtDetr1.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                    & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & txtDet1_1.SelectedValue _
                                    & "," & txtDetr1.Text _
                                    & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                If txtDetr2.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                    & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & txtDet2_1.SelectedValue _
                                    & "," & txtDetr2.Text _
                                    & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                If txtDetr3.Text > 0 Then
                    StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                    & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & txtDet3_1.SelectedValue _
                                    & "," & txtDetr3.Text _
                                    & ")"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()
                End If

                'COMPONENTE 1
                If sValoreNM >= 1 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",1,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC1.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC1.Text)) & "'," _
                                & cmbParentelaC1.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC1.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC1.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC1.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC1.SelectedValue & "','" & par.RicavaSesso(txtCFC1.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC1.SelectedValue = 100 And cmbAccompagnamentoC1.SelectedValue = 1 And txtSpeseC1.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC1.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If


                    If TextBox8.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox6.Text) & "','" & par.PulisciStrSql(TextBox7.Text) & "'," & TextBox8.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox11.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox9.Text) & "','" & par.PulisciStrSql(TextBox10.Text) & "'," & TextBox11.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C1.Text > 0 Then
                        If CheckBox3.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C1.SelectedValue _
                                    & "," & cmbPercProprieta1C1.SelectedValue _
                                    & "," & txtValore1C1.Text _
                                    & "," & txtMutuo1C1.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C2.Text > 0 Then
                        If CheckBox2.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C1.SelectedValue _
                                    & "," & cmbPercProprieta2C1.SelectedValue _
                                    & "," & txtValore2C1.Text _
                                    & "," & txtMutuo2C1.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox60.Text > 0 Or TextBox61.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox60.Text, 0) _
                                       & "," & par.IfEmpty(TextBox61.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox62.Text > 0 Or TextBox63.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox62.Text, 0) _
                                       & "," & par.IfEmpty(TextBox63.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox65.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox65.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox67.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox67.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox68.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList1.SelectedValue _
                                        & "," & TextBox68.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox69.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList2.SelectedValue _
                                        & "," & TextBox69.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox70.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList3.SelectedValue _
                                        & "," & TextBox70.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

                If sValoreNM >= 2 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",2,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC2.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC2.Text)) & "'," _
                                & cmbParentelaC2.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC2.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC2.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC2.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC2.SelectedValue & "','" & par.RicavaSesso(txtCFC2.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC2.SelectedValue = 100 And cmbAccompagnamentoC2.SelectedValue = 1 And txtSpeseC2.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC2.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox14.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox12.Text) & "','" & par.PulisciStrSql(TextBox13.Text) & "'," & TextBox14.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox17.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox15.Text) & "','" & par.PulisciStrSql(TextBox16.Text) & "'," & TextBox17.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C2.Text > 0 Then
                        If CheckBox4.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C2.SelectedValue _
                                    & "," & cmbPercProprieta1C2.SelectedValue _
                                    & "," & txtValore1C2.Text _
                                    & "," & txtMutuo1C2.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C2.Text > 0 Then
                        If CheckBox5.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C2.SelectedValue _
                                    & "," & cmbPercProprieta2C2.SelectedValue _
                                    & "," & txtValore2C2.Text _
                                    & "," & txtMutuo2C2.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox71.Text > 0 Or TextBox72.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox71.Text, 0) _
                                       & "," & par.IfEmpty(TextBox72.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox73.Text > 0 Or TextBox74.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox73.Text, 0) _
                                       & "," & par.IfEmpty(TextBox74.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox76.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox76.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox78.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox78.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox79.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList4.SelectedValue _
                                        & "," & TextBox79.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox80.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList5.SelectedValue _
                                        & "," & TextBox80.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox81.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList6.SelectedValue _
                                        & "," & TextBox81.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If


                End If

                If sValoreNM >= 3 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",3,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC3.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC3.Text)) & "'," _
                                & cmbParentelaC3.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC3.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC3.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC3.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC3.SelectedValue & "','" & par.RicavaSesso(txtCFC3.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC3.SelectedValue = 100 And cmbAccompagnamentoC3.SelectedValue = 1 And txtSpeseC3.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC3.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If


                    If TextBox20.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox18.Text) & "','" & par.PulisciStrSql(TextBox19.Text) & "'," & TextBox20.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox23.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox21.Text) & "','" & par.PulisciStrSql(TextBox22.Text) & "'," & TextBox23.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C3.Text > 0 Then
                        If CheckBox6.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C3.SelectedValue _
                                    & "," & cmbPercProprieta1C3.SelectedValue _
                                    & "," & txtValore1C3.Text _
                                    & "," & txtMutuo1C3.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C3.Text > 0 Then
                        If CheckBox7.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C3.SelectedValue _
                                    & "," & cmbPercProprieta2C3.SelectedValue _
                                    & "," & txtValore2C3.Text _
                                    & "," & txtMutuo2C3.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox82.Text > 0 Or TextBox83.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox82.Text, 0) _
                                       & "," & par.IfEmpty(TextBox83.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox84.Text > 0 Or TextBox85.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox84.Text, 0) _
                                       & "," & par.IfEmpty(TextBox85.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox87.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox87.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox89.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox89.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox90.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList7.SelectedValue _
                                        & "," & TextBox90.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox91.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList8.SelectedValue _
                                        & "," & TextBox91.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox92.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList9.SelectedValue _
                                        & "," & TextBox92.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If




                End If

                If sValoreNM >= 4 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",4,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC4.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC4.Text)) & "'," _
                                & cmbParentelaC4.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC4.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC4.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC4.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC4.SelectedValue & "','" & par.RicavaSesso(txtCFC4.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC4.SelectedValue = 100 And cmbAccompagnamentoC4.SelectedValue = 1 And txtSpeseC4.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC4.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If


                    If TextBox26.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox24.Text) & "','" & par.PulisciStrSql(TextBox25.Text) & "'," & TextBox26.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox29.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox27.Text) & "','" & par.PulisciStrSql(TextBox28.Text) & "'," & TextBox29.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C4.Text > 0 Then
                        If CheckBox8.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C4.SelectedValue _
                                    & "," & cmbPercProprieta1C4.SelectedValue _
                                    & "," & txtValore1C4.Text _
                                    & "," & txtMutuo1C4.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C4.Text > 0 Then
                        If CheckBox9.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C4.SelectedValue _
                                    & "," & cmbPercProprieta2C4.SelectedValue _
                                    & "," & txtValore2C4.Text _
                                    & "," & txtMutuo2C4.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox93.Text > 0 Or TextBox94.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox93.Text, 0) _
                                       & "," & par.IfEmpty(TextBox94.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox95.Text > 0 Or TextBox96.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox95.Text, 0) _
                                       & "," & par.IfEmpty(TextBox96.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox98.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox98.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox100.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox100.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox101.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList10.SelectedValue _
                                        & "," & TextBox101.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox102.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList11.SelectedValue _
                                        & "," & TextBox102.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox103.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList12.SelectedValue _
                                        & "," & TextBox103.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                End If

                If sValoreNM >= 5 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",5,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC5.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC5.Text)) & "'," _
                                & cmbParentelaC5.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC5.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC5.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC5.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC5.SelectedValue & "','" & par.RicavaSesso(txtCFC5.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC5.SelectedValue = 100 And cmbAccompagnamentoC5.SelectedValue = 1 And txtSpeseC5.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC5.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                    If TextBox32.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox30.Text) & "','" & par.PulisciStrSql(TextBox31.Text) & "'," & TextBox32.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox35.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox33.Text) & "','" & par.PulisciStrSql(TextBox34.Text) & "'," & TextBox35.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C5.Text > 0 Then
                        If CheckBox10.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C5.SelectedValue _
                                    & "," & cmbPercProprieta1C5.SelectedValue _
                                    & "," & txtValore1C5.Text _
                                    & "," & txtMutuo1C5.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C5.Text > 0 Then
                        If CheckBox11.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C5.SelectedValue _
                                    & "," & cmbPercProprieta2C5.SelectedValue _
                                    & "," & txtValore2C5.Text _
                                    & "," & txtMutuo2C5.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox104.Text > 0 Or TextBox105.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox104.Text, 0) _
                                       & "," & par.IfEmpty(TextBox105.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox106.Text > 0 Or TextBox107.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox106.Text, 0) _
                                       & "," & par.IfEmpty(TextBox107.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox109.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox109.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox111.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox111.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox112.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList13.SelectedValue _
                                        & "," & TextBox112.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox113.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList14.SelectedValue _
                                        & "," & TextBox113.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox114.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList15.SelectedValue _
                                        & "," & TextBox114.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                End If

                If sValoreNM >= 6 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",6,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC6.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC6.Text)) & "'," _
                                & cmbParentelaC6.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC6.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC6.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC6.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC6.SelectedValue & "','" & par.RicavaSesso(txtCFC6.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC6.SelectedValue = 100 And cmbAccompagnamentoC6.SelectedValue = 1 And txtSpeseC6.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC6.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                    If TextBox38.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox36.Text) & "','" & par.PulisciStrSql(TextBox37.Text) & "'," & TextBox38.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox41.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox39.Text) & "','" & par.PulisciStrSql(TextBox40.Text) & "'," & TextBox41.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C6.Text > 0 Then
                        If CheckBox12.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C6.SelectedValue _
                                    & "," & cmbPercProprieta1C6.SelectedValue _
                                    & "," & txtValore1C6.Text _
                                    & "," & txtMutuo1C6.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C6.Text > 0 Then
                        If CheckBox13.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C6.SelectedValue _
                                    & "," & cmbPercProprieta2C6.SelectedValue _
                                    & "," & txtValore2C6.Text _
                                    & "," & txtMutuo2C6.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox115.Text > 0 Or TextBox116.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox115.Text, 0) _
                                       & "," & par.IfEmpty(TextBox116.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox117.Text > 0 Or TextBox118.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox117.Text, 0) _
                                       & "," & par.IfEmpty(TextBox118.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox120.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox120.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox122.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox122.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox123.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList16.SelectedValue _
                                        & "," & TextBox123.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox124.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList17.SelectedValue _
                                        & "," & TextBox124.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox125.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList18.SelectedValue _
                                        & "," & TextBox125.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                End If

                If sValoreNM >= 7 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",7,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC7.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC7.Text)) & "'," _
                                & cmbParentelaC7.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC7.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC7.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC7.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC7.SelectedValue & "','" & par.RicavaSesso(txtCFC7.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC7.SelectedValue = 100 And cmbAccompagnamentoC7.SelectedValue = 1 And txtSpeseC7.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC7.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                    If TextBox44.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox42.Text) & "','" & par.PulisciStrSql(TextBox43.Text) & "'," & TextBox44.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox47.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox45.Text) & "','" & par.PulisciStrSql(TextBox46.Text) & "'," & TextBox47.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C7.Text > 0 Then
                        If CheckBox14.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C7.SelectedValue _
                                    & "," & cmbPercProprieta1C7.SelectedValue _
                                    & "," & txtValore1C7.Text _
                                    & "," & txtMutuo1C7.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C7.Text > 0 Then
                        If CheckBox15.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C7.SelectedValue _
                                    & "," & cmbPercProprieta2C7.SelectedValue _
                                    & "," & txtValore2C7.Text _
                                    & "," & txtMutuo2C7.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox126.Text > 0 Or TextBox127.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox126.Text, 0) _
                                       & "," & par.IfEmpty(TextBox127.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox128.Text > 0 Or TextBox129.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox128.Text, 0) _
                                       & "," & par.IfEmpty(TextBox129.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox131.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox131.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox133.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox133.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox134.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList19.SelectedValue _
                                        & "," & TextBox134.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox135.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList20.SelectedValue _
                                        & "," & TextBox135.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox136.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList21.SelectedValue _
                                        & "," & TextBox136.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                End If

                If sValoreNM >= 8 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",8,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC8.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC8.Text)) & "'," _
                                & cmbParentelaC8.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC8.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC8.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC8.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC8.SelectedValue & "','" & par.RicavaSesso(txtCFC8.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC8.SelectedValue = 100 And cmbAccompagnamentoC8.SelectedValue = 1 And txtSpeseC8.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC8.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If



                    If TextBox50.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox48.Text) & "','" & par.PulisciStrSql(TextBox49.Text) & "'," & TextBox50.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox53.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox51.Text) & "','" & par.PulisciStrSql(TextBox52.Text) & "'," & TextBox53.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C8.Text > 0 Then
                        If CheckBox16.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C8.SelectedValue _
                                    & "," & cmbPercProprieta1C8.SelectedValue _
                                    & "," & txtValore1C8.Text _
                                    & "," & txtMutuo1C8.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C8.Text > 0 Then
                        If CheckBox17.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C8.SelectedValue _
                                    & "," & cmbPercProprieta2C8.SelectedValue _
                                    & "," & txtValore2C8.Text _
                                    & "," & txtMutuo2C8.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox137.Text > 0 Or TextBox138.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox137.Text, 0) _
                                       & "," & par.IfEmpty(TextBox138.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox139.Text > 0 Or TextBox140.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox139.Text, 0) _
                                       & "," & par.IfEmpty(TextBox140.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox142.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox142.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox144.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox144.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox145.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList22.SelectedValue _
                                        & "," & TextBox145.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox146.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList23.SelectedValue _
                                        & "," & TextBox146.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox147.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList24.SelectedValue _
                                        & "," & TextBox147.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If




                End If

                If sValoreNM >= 9 Then
                    StringaSQL = "INSERT INTO COMP_NUCLEO_WEB (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL," _
                                & "DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_WEB.NEXTVAL," & idDichiarazione & ",9,'" _
                                & par.PulisciStrSql(UCase(txtCognomeC9.Text)) & "','" _
                                & par.PulisciStrSql(UCase(txtNomeC9.Text)) & "'," _
                                & cmbParentelaC9.SelectedValue & ",'" _
                                & par.PulisciStrSql(UCase(txtCFC9.Text)) & "','" _
                                & par.PulisciStrSql(cmbInvaliditaC9.SelectedValue) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascitaC9.Text)) & "','" _
                                & par.PulisciStrSql(txtASL.Text) & "','" _
                                & cmbAccompagnamentoC9.SelectedValue & "','" & par.RicavaSesso(txtCFC9.Text) & "')"
                    par.cmd.CommandText = StringaSQL
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_WEB.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponente = myReader(0)
                    End If
                    myReader.Close()

                    If cmbInvaliditaC9.SelectedValue = 100 And cmbAccompagnamentoC9.SelectedValue = 1 And txtSpeseC9.Text > 10000 Then
                        StringaSQL = "INSERT INTO COMP_ELENCO_SPESE_WEB (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_WEB.NEXTVAL," _
                            & idComponente & "," _
                            & txtSpeseC9.Text & ",'" _
                            & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If


                    If TextBox56.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox54.Text) & "','" & par.PulisciStrSql(TextBox55.Text) & "'," & TextBox56.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox59.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_PATR_MOB_WEB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES " _
                                   & "(SEQ_COMP_PATR_MOB_WEB.NEXTVAL," & idComponente & "," _
                                   & "'" & par.PulisciStrSql(TextBox57.Text) & "','" & par.PulisciStrSql(TextBox58.Text) & "'," & TextBox59.Text & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore1C9.Text > 0 Then
                        If CheckBox18.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob1C9.SelectedValue _
                                    & "," & cmbPercProprieta1C9.SelectedValue _
                                    & "," & txtValore1C9.Text _
                                    & "," & txtMutuo1C9.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If txtValore2C9.Text > 0 Then
                        If CheckBox19.Checked = True Then
                            RESIDENZA = "1"
                        Else
                            RESIDENZA = "0"
                        End If
                        StringaSQL = "INSERT INTO COMP_PATR_IMMOB_WEB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                                    & " (SEQ_COMP_PATR_IMMOB_WEB.NEXTVAL," & idComponente & "," & txtImmob2C9.SelectedValue _
                                    & "," & cmbPercProprieta2C9.SelectedValue _
                                    & "," & txtValore2C9.Text _
                                    & "," & txtMutuo2C9.Text _
                                    & ",'" & RESIDENZA & "')"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox148.Text > 0 Or TextBox149.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox148.Text, 0) _
                                       & "," & par.IfEmpty(TextBox149.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If
                    If TextBox150.Text > 0 Or TextBox151.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_REDDITO_WEB (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_WEB.NEXTVAL," _
                                       & idComponente & "," _
                                       & par.IfEmpty(TextBox150.Text, 0) _
                                       & "," & par.IfEmpty(TextBox151.Text, 0) & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox153.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox153.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox155.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_ALTRI_REDDITI_WEB (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                                    & " (SEQ_COMP_ALTRI_REDDITI_WEB.NEXTVAL," & idComponente & "," _
                                    & TextBox155.Text _
                                    & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox156.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList25.SelectedValue _
                                        & "," & TextBox156.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox157.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList26.SelectedValue _
                                        & "," & TextBox157.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TextBox158.Text > 0 Then
                        StringaSQL = "INSERT INTO COMP_DETRAZIONI_WEB(ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                                        & " (SEQ_COMP_DETRAZIONI_WEB.NEXTVAL," & idComponente & "," & DropDownList27.SelectedValue _
                                        & "," & TextBox158.Text _
                                        & ")"
                        par.cmd.CommandText = StringaSQL
                        par.cmd.ExecuteNonQuery()
                    End If

                End If





                StringaSQL = "Insert into DOMANDE_BANDO_WEB (ID, ID_BANDO, TIPO_PRATICA, ID_DICHIARAZIONE, PROGR_COMPONENTE, " _
                           & "PG, ANNO_PG, DATA_PG, ID_STATO, ID_VECCHIO_STATO,ID_CAUSALE_DOMANDA, ID_TIPO_CONTENZIOSO, " _
                           & "TIPO_ALLOGGIO, ANNI_RESIDENZA_DNTE, DATA_RESIDENZA_DNTE, DURATA_RESIDENZA_DNTE, FL_FAM_1, " _
                           & "FL_FAM_2, FL_FAM_3, FL_FAM_4, FL_MOROSITA, FL_PROFUGO, ANNO_RIF_CANONE, IMPORTO_CANONE, " _
                           & "ANNO_RIF_SPESE_ACC, IMPORTO_SPESE_ACC, ID_PARA_0, ID_PARA_1, ID_PARA_2, ID_PARA_3, " _
                           & "ID_PARA_4, ID_PARA_5, ID_PARA_6, ID_PARA_7, ID_PARA_8, ID_PARA_9, ID_PARA_10, ID_PARA_11, " _
                           & "ID_PARA_12, ID_PARA_13, ID_PARA_14, ID_PARA_15, NOTE_SOCIALI, NOTE, PRESSO_REC_DNTE, " _
                           & "IND_REC_DNTE, ID_LUOGO_REC_DNTE, TELEFONO_REC_DNTE, ID_TIPO_IND_REC_DNTE, CIVICO_REC_DNTE, " _
                           & "REQUISITO1, REQUISITO2, REQUISITO3, REQUISITO4, REQUISITO5, REQUISITO6, REQUISITO7, REQUISITO8, " _
                           & "REQUISITO9, ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E, REDDITO_ISEE, ISR_ERP, " _
                           & "ISP_ERP, ISE_ERP, FL_INIZIO_REQ, FL_PRATICA_CHIUSA, FL_QUERELA, FL_SOSPENSIVA, " _
                           & "FL_ISTRUTTORIA_COMPLETA, FL_COMPLETA, FL_ESAMINATA, FL_SUPPLEMENTO, FL_PROPOSTA, " _
                           & "FL_CONTROLLA_REQUISITI, FL_INVITO, DATA_INIZIO_REQ, UNIFICATO, DATA_UNIFICATO, " _
                           & "CONTRATTO_NUM, CONTRATTO_DATA, NUM_ALLOGGIO, CODICE_ALLOGGIO, DATA_FP, FL_RINNOVO, PERIODO_RES, " _
                           & "FL_MOROSITA_G, FL_AFF_ONEROSO, CAP_REC_DNTE, FL_SCARICO, FL_CONFERMA_SCARICO, " _
                           & "N_DISTINTA, CONTRATTO_DATA_DEC, MINORI_CARICO, FL_ASS_ESTERNA, DATA_INVIO_ASS_EST, " _
                           & "DATA_RIPRESA_ASS_EST, PSE, VSE, LUOGO_NAS_EXTRA, CARTA_I, CARTA_I_DATA, ASS_TEMPORANEA, " _
                           & "INV_CARROZZINA, PERMESSO_SOGG_N, PERMESSO_SOGG_DATA, PERMESSO_SOGG_SCADE, PERMESSO_SOGG_RINNOVO, " _
                           & "CONTATTI_PARTICOLARI, COINTESTATARI, CARTA_SOGG_N, CARTA_SOGG_DATA, PERMESSO_SOGG_DATA_RINNOVO, " _
                           & "TIPO_LAVORO, FL_NATO_ESTERO, NOTE_WEB, PERMESSO_SOGG_CONT,INDIRIZZO_MAIL,nome_file) " _
                           & "Values " _
                           & "(SEQ_DOMANDE_BANDO_WEB.NEXTVAL, " & lIndice_Bando & " , 150, " & idDichiarazione & " , 0, " _
                           & "'0000000000', NULL, '" & Format(Now, "yyyyMMdd") & "','1', NULL, 0, NULL, 0, NULL, NULL, " _
                           & "NULL, '0', '0', '0', '0', '" & Valore01(ChMorosita.Checked) & "', '" & Valore01(cfProfugo.Checked) & "', '" & par.IfEmpty(txtAnnoCanone.Text, "2017") & "'," _
                           & par.IfEmpty(txtSpeseLoc.Text, "0") & ", '" & par.IfEmpty(txtAnnoAcc.Text, "2017") & "', " _
                           & par.IfEmpty(txtSpeseAcc.Text, "0") & ", " & cmbF1.SelectedValue & " , " & cmbF2.SelectedValue _
                           & "," & cmbF3.SelectedValue & "," & cmbF4.SelectedValue & "," & cmbF5.SelectedValue & "," _
                           & cmbF6.SelectedValue & "," & cmbF7.SelectedValue & "," & cmbA1.SelectedValue & "," & cmbA2.SelectedValue & ", " _
                           & cmbA3.SelectedValue & "," & cmbA4.SelectedValue & "," & cmbA5.SelectedValue & "," & cmbA6.SelectedValue _
                           & "," & cmbA7.SelectedValue & "," & cmbA8.SelectedValue & "," & cmbA9.SelectedValue _
                           & ",'','', '" & par.PulisciStrSql(TextBox5.Text) & "', " _
                           & "'" & par.PulisciStrSql(TextBox2.Text) & "', " & par.IfEmpty(cmbComuneRec.SelectedValue, "NULL") & ", '" _
                           & par.PulisciStrSql(TextBox4.Text) & "'," & cmbTipoIRec.SelectedValue & ", '" & par.PulisciStrSql(TextBox3.Text) _
                           & "', '" & Valore01(chR1.Checked) & "', '" & Valore01(chR2.Checked) & "', '" & Valore01(chR3.Checked) _
                           & "', '1', '" & Valore01(chR5.Checked) & "', " _
                           & "'1', '" & Valore01(chR6.Checked) & "', '" & Valore01(chR7.Checked) & "', '" & Valore01(chR8.Checked) _
                           & "', 0, 0, 0, 0, 0, 0, " _
                           & "0, 0, 0, 0, NULL, '0', NULL, NULL, '0', '0', '0', NULL, NULL, NULL, NULL, " _
                           & "NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '0', " & cmbResidenza.SelectedValue _
                           & ", '0', '0', '" & txtCapRec.Text & "', '0', '0', " _
                           & "NULL, NULL, NULL, '0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, " _
                           & "NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'0','" & par.PulisciStrSql(txtmail.Text) & "','" & NomeFile1 & ".htm')"
                par.cmd.CommandText = StringaSQL
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_WEB.CURRVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lIdDomanda = myReader(0)
                End If
                myReader.Close()
                Session.Item("FATTO") = lIdDomanda



                sStringaSql = "<table style='border-right: black 1px dashed; border-top: black 1px dashed; font-size: 12pt;border-left: black 1px dashed; border-bottom: black 1px dashed' width='100%'><tr><td style='width: 351px'>" _
                        & "<span tyle='font-family: Arial; text-decoration: underline'>Da Allegare alla Domanda</span></td><td></td></tr><tr><td style='width: 351px'></td><td></td> </tr><tr><td style='width: 351px'>" _
                        & "<span style='font-size: 16pt; font-family: Arial'><strong>DOMANDA N.</strong></span></td><td><span style='font-size: 24pt; font-family: Arial'><strong>" & Format(lIdDomanda, "0000000000") & "</strong></span></td></tr>" _
                        & "<tr style='font-size: 12pt'><td style='width: 351px'>&nbsp;</td><td>&nbsp;</td></tr><tr style='font-size: 12pt'><td style='width: 351px'><span style='font-family: Arial'>Intestata a</span></td>" _
                        & "<td><span style='font-family: Arial'>" & UCase(txtCognome.Text & " " & txtNome.Text) & "</span></td></tr><tr style='font-size: 12pt'><td style='width: 351px'><span style='font-family: Arial'>Data Inserimento</span></td>" _
                        & "<td><span style='font-family: Arial'>" & Format(Now, "dd/MM/yyyy") & "</span></td></tr><tr style='font-size: 12pt'><td style='width: 351px'><span style='font-family: Arial'>Indirizzo E-Mail</span></td>" _
                        & "<td><span style='font-family: Arial'>" & txtmail.Text & "</span></td></tr><tr style='font-size: 12pt'><td style='width: 351px'>&nbsp;</td><td>&nbsp;</td></tr><tr style='font-size: 12pt'><td style='width: 351px'>" _
                        & "&nbsp;</td><td>&nbsp;</td></tr><tr style='font-size: 12pt'><td style='width: 351px'>&nbsp;</td><td>&nbsp;</td></tr></table>" _
                        & "<p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font></p>" _
                        & sStringaSql

                IO.File.WriteAllText(NomeFile & ".htm", sStringaSql)
                'HyperLink1.NavigateUrl = "stampa.aspx?FILE=" & NomeFile1

                'Dim url As String = NomeFile & ".htm"
                'Dim pdfConverter As PdfConverter = New PdfConverter
                ''pdfConverter.LicenseKey = "P38cBx6AWW7b9c81TjEGxnrazP+J7rOjs+9omJ3TUycauK+cL WdrITM5T59hdW5r"
                'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                'pdfConverter.PdfDocumentOptions.ShowHeader = False
                'pdfConverter.PdfDocumentOptions.ShowFooter = False
                'pdfConverter.PdfDocumentOptions.LeftMargin = 5
                'pdfConverter.PdfDocumentOptions.RightMargin = 5
                'pdfConverter.PdfDocumentOptions.TopMargin = 5
                'pdfConverter.PdfDocumentOptions.BottomMargin = 5
                'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                'pdfConverter.PdfDocumentOptions.ShowHeader = False
                'pdfConverter.PdfFooterOptions.FooterText = ("")
                'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                'pdfConverter.PdfFooterOptions.DrawFooterLine = False
                'pdfConverter.PdfFooterOptions.PageNumberText = ""
                'pdfConverter.PdfFooterOptions.ShowPageNumber = False

                'pdfConverter.SavePdfFromUrlToFile(url, NomeFile & ".pdf")

                If UtenteMail <> "" Then

                    Dim mail As New System.Net.Mail.MailMessage


                    mail.IsBodyHtml = True
                    mail.From = New System.Net.Mail.MailAddress(MittenteMail)
                    mail.To.Add(New System.Net.Mail.MailAddress(txtmail.Text))
                    mail.Subject = "Autocompilazione Domanda di Bando Erp"
                    mail.Body = "Si conferma l'avvenuta registrazione in data " & Format(Now, "dd/MM/yyyy") & " dei dati inseriti mediante la procedura per la presentazione on-line della domanda di assegnazione di alloggio E.R.P. effettuata a nome di:<br><br>Richiedente:" & txtCognome.Text & " " & txtNome.Text & "<br>Codice Fiscale:" & txtCF.Text & "<br>e-mail:" & txtmail.Text & "<br>N.Registrazione:" & Format(lIdDomanda, "0000000000") & "<br>Telefono:" & txtTelefono.Text & "<br><br>PER CONFERMARE LA REGISTRAZIONE E PER PRESENTARE UFFICIALMENTE LA DOMANDA DI ASSEGNAZIONE DI ALLOGGIO E.R.P. OCCORRE ORA ESPRIMERE IL PROPRIO CONSENSO AL FINE DI POTER ESSERE CONTATTATO DA UN OPERATORE DEL COMUNE DI MILANO,PREMENDO IL SEGUENTE LINK:<br><BR><a href=" & Chr(34) & System.Configuration.ConfigurationManager.AppSettings("SitoConferma") & "AutoCompilazione/Valida.aspx?ID=" & par.CriptaMolto("SistemieSoluzionisrl-ValidazioneDomandaNumero" & Format(lIdDomanda, "0000000000")) & Chr(34) & ">CONFERMO LA VOLONTA' DI PRESENTARE DOMANDA DI ASSEGNAZIONE DI ALLOGGIO ERP N. " & Format(lIdDomanda, "0000000000") & "</a><br><br>CONFERMATA LA REGISTRAZIONE UN OPERATORE DEL COMUNE DI MILANO LA CONTATTERA' AL PIU' PRESTO.<br><br>QUALORA PER RAGIONI NON PREVENTIVABILI QUESTO NON AVVENGA ENTRO POCHI GIORNI, POTRA' CONTATTARE PERSONALMENTE GLI UFFICI COMUNALI DI VIA PIRELLI 39 AL SEGUENTE NUMERO: 02/88464407.<br><br>IN ALTERNATIVA POTRA' UTILIZZARE LA PRESENTE CASELLA DI POSTA ELETTRONICA, CITANDO OBBLIGATORIAMENTE, IN ENTRAMBI I CASI, IL NUMERO DI REGISTRAZIONE DELLA DOMANDA.<br><br><br>CORDIALI SALUTI.<BR><br><br>COMUNE DI MILANO<BR>DIREZIONE CENTRALE CASA<BR>SETTORE ASSEGNAZIONE ALLOGGI DI E.R.P.<BR>SEZIONE(BANDI)<BR>Via Pirelli, 39 - MILANO"

                    If SSL = "1" Then
                        Dim smtpClient = New System.Net.Mail.SmtpClient(smtp, PORTA_SSL)
                        smtpClient.Timeout = 300000
                        smtpClient.EnableSsl = True
                        smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                        smtpClient.UseDefaultCredentials = False
                        smtpClient.Credentials = New System.Net.NetworkCredential(UtenteMail, PWUtenteMail)
                        smtpClient.Send(mail)
                        mail.Dispose()
                    Else
                        Dim smtpClient = New System.Net.Mail.SmtpClient(smtp)
                        smtpClient.Timeout = 300000
                        smtpClient.EnableSsl = False
                        smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                        smtpClient.UseDefaultCredentials = False
                        smtpClient.Credentials = New System.Net.NetworkCredential(UtenteMail, PWUtenteMail)
                        smtpClient.Send(mail)
                        mail.Dispose()
                    End If


                End If

                par.myTrans.Commit()
                par.OracleConn.Close()



            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()

                Session.Add("ERRORE", ex.Message)
                Response.Write("<script>top.location.href='Errore.aspx';</script>")
                'Label4.Visible = True
                'Label4.Text = "ATTENZIONE...SI E' VERIFICATO UN ERRORE, LA TUA DOMANDA NON E' STATA MEMORIZZATA!" & vbCrLf & ex.Message
            End Try
        End If

    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Public Property lIdDomanda() As Long
        Get
            If Not (ViewState("par_lIdDomanda") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomanda") = value
        End Set

    End Property

    Public Property C1() As Long
        Get
            If Not (ViewState("par_c1") Is Nothing) Then
                Return CLng(ViewState("par_c1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_1") = value
        End Set

    End Property

    Protected Sub Wizard1_NextButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles Wizard1.NextButtonClick
        If e.CurrentStepIndex = 0 Then
            lblPressoErrore.Visible = False
            lblAvvIndennita.Visible = False

            If txtCAP.Text = "" Then
                txtCAP.Text = "-----"
            End If

            If TextBox5.Text <> "" Then
                If TextBox2.Text = "" Or TextBox3.Text = "" Then
                    lblPressoErrore.Visible = True
                    e.Cancel = True
                End If
            End If

            If cmbAccompagnamento.SelectedItem.Text = "SI" And cmbInvalidità.SelectedItem.Text <> "100" Then
                lblAvvIndennita.Visible = True
                e.Cancel = True
            End If
        End If

        If e.CurrentStepIndex = sValoreNM + 1 Then
            Wizard1.ActiveStepIndex = 11
        End If

        If e.CurrentStepIndex = 1 Then


            If txtMutuo1.Text > txtValore1.Text Then
                lblMutuo.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2.Text > txtValore2.Text Then
                lblMutuo.Visible = True
                e.Cancel = True
            End If

            lblImmob.Visible = False

            If txtImmob1_5.Checked = True And CheckBox1.Checked = True Then
                lblImmob.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If txtImmob1_5.Checked = True And txtImmob1_1.SelectedValue <> 0 Then
                    lblImmob.Visible = True
                    lblImmob.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox1.Checked = True And txtImmob2_1.SelectedValue <> 0 Then
                        lblImmob.Visible = True
                        lblImmob.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (txtImmob1_5.Checked = True Or CheckBox1.Checked = True) And cmbImmobCat1_1.SelectedValue = -1 Then
                            lblImmob.Visible = True
                            lblImmob.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        End If
                    End If
                End If
            End If

        End If

        If e.CurrentStepIndex = 2 Then
            Label2.Visible = False
            Label3.Visible = False
            lblMutuo1C1.Visible = False
            lblCFC1.Visible = False

            lblAvvIndennitac1.Visible = False
            If cmbAccompagnamentoC1.SelectedItem.Text = "SI" And cmbInvaliditaC1.SelectedItem.Text <> "100" Then
                lblAvvIndennitac1.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC1.Text = "" Then
                Label2.Visible = True
                Label2.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC1.Text = "" Then
                Label3.Visible = True
                Label3.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC1.Text) < 16 Then
                lblCFC1.Visible = True
                lblCFC1.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC1.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC1.Text), UCase(txtCognomeC1.Text), UCase(txtNomeC1.Text)) = True And UCase(txtCognomeC1.Text) <> "" And UCase(txtNomeC1.Text) <> "" Then

                Else
                    lblCFC1.Visible = True
                    lblCFC1.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC1.Visible = True
                lblCFC1.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C1.Text > txtValore1C1.Text Then
                lblMutuo1C1.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C1.Text > txtValore2C1.Text Then
                lblMutuo2C1.Visible = True
                e.Cancel = True
            End If

            lblImmobC1.Visible = False
            If CheckBox3.Checked = True And CheckBox2.Checked = True Then
                lblImmobC1.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox3.Checked = True And txtImmob1C1.SelectedValue <> 0 Then
                    lblImmobC1.Visible = True
                    lblImmobC1.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox2.Checked = True And txtImmob2C1.SelectedValue <> 0 Then
                        lblImmobC1.Visible = True
                        lblImmobC1.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox3.Checked = True Or CheckBox2.Checked = True) And DropDownList28.SelectedValue = -1 Then
                            lblImmobC1.Visible = True
                            lblImmobC1.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox3.Checked = True Or CheckBox2.Checked = True) And (txtImmob1_5.Checked = True Or CheckBox1.Checked = True) Then
                                lblImmobC1.Visible = True
                                lblImmobC1.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If

        End If

        If e.CurrentStepIndex = 3 Then
            Label2C2.Visible = False
            Label3C2.Visible = False
            lblCFC2.Visible = False
            lblMutuo1C2.Visible = False

            lblAvvIndennitac2.Visible = False
            If cmbAccompagnamentoC2.SelectedItem.Text = "SI" And cmbInvaliditaC2.SelectedItem.Text <> "100" Then
                lblAvvIndennitac2.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC2.Text = "" Then
                Label2C2.Visible = True
                Label2C2.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC2.Text = "" Then
                Label3C2.Visible = True
                Label3C2.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC2.Text) < 16 Then
                lblCFC2.Visible = True
                lblCFC2.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC2.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC2.Text), UCase(txtCognomeC2.Text), UCase(txtNomeC2.Text)) = True Then

                Else
                    lblCFC2.Visible = True
                    lblCFC2.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC2.Visible = True
                lblCFC2.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C2.Text > txtValore1C2.Text Then
                lblMutuo1C2.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C2.Text > txtValore2C2.Text Then
                lblMutuo2C2.Visible = True
                e.Cancel = True
            End If

            lblImmobC2.Visible = False
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then
                lblImmobC2.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox4.Checked = True And txtImmob1C2.SelectedValue <> 0 Then
                    lblImmobC2.Visible = True
                    lblImmobC2.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox5.Checked = True And txtImmob2C2.SelectedValue <> 0 Then
                        lblImmobC2.Visible = True
                        lblImmobC2.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox4.Checked = True Or CheckBox5.Checked = True) And DropDownList29.SelectedValue = -1 Then
                            lblImmobC2.Visible = True
                            lblImmobC2.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox4.Checked = True Or CheckBox5.Checked = True) And (CheckBox3.Checked = True Or CheckBox2.Checked = True) Then
                                lblImmobC2.Visible = True
                                lblImmobC2.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If

        End If

        If e.CurrentStepIndex = 4 Then
            Label2C3.Visible = False
            Label3C3.Visible = False
            lblCFC3.Visible = False
            lblMutuo1C3.Visible = False

            lblAvvIndennitac3.Visible = False
            If cmbAccompagnamentoC3.SelectedItem.Text = "SI" And cmbInvaliditaC3.SelectedItem.Text <> "100" Then
                lblAvvIndennitac3.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC3.Text = "" Then
                Label2C3.Visible = True
                Label2C3.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC3.Text = "" Then
                Label3C3.Visible = True
                Label3C3.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC3.Text) < 16 Then
                lblCFC3.Visible = True
                lblCFC3.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC3.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC3.Text), UCase(txtCognomeC3.Text), UCase(txtNomeC3.Text)) = True Then

                Else
                    lblCFC3.Visible = True
                    lblCFC3.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC3.Visible = True
                lblCFC3.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C3.Text > txtValore1C3.Text Then
                lblMutuo1C3.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C3.Text > txtValore2C3.Text Then
                lblMutuo2C3.Visible = True
                e.Cancel = True
            End If

            lblImmobC3.Visible = False
            If CheckBox6.Checked = True And CheckBox7.Checked = True Then
                lblImmobC3.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox6.Checked = True And txtImmob1C3.SelectedValue <> 0 Then
                    lblImmobC3.Visible = True
                    lblImmobC3.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox7.Checked = True And txtImmob2C3.SelectedValue <> 0 Then
                        lblImmobC3.Visible = True
                        lblImmobC3.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox6.Checked = True Or CheckBox7.Checked = True) And DropDownList30.SelectedValue = -1 Then
                            lblImmobC3.Visible = True
                            lblImmobC3.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox6.Checked = True Or CheckBox7.Checked = True) And (CheckBox4.Checked = True Or CheckBox5.Checked = True) Then
                                lblImmobC3.Visible = True
                                lblImmobC3.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If e.CurrentStepIndex = 5 Then
            Label2C4.Visible = False
            Label3C4.Visible = False
            lblCFC4.Visible = False
            lblMutuo1C4.Visible = False

            lblAvvIndennitac4.Visible = False
            If cmbAccompagnamentoC4.SelectedItem.Text = "SI" And cmbInvaliditaC4.SelectedItem.Text <> "100" Then
                lblAvvIndennitac4.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC4.Text = "" Then
                Label2C4.Visible = True
                Label2C4.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC4.Text = "" Then
                Label3C4.Visible = True
                Label3C4.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC4.Text) < 16 Then
                lblCFC4.Visible = True
                lblCFC4.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC4.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC4.Text), UCase(txtCognomeC4.Text), UCase(txtNomeC4.Text)) = True Then

                Else
                    lblCFC4.Visible = True
                    lblCFC4.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC4.Visible = True
                lblCFC4.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C4.Text > txtValore1C4.Text Then
                lblMutuo1C4.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C4.Text > txtValore2C4.Text Then
                lblMutuo2C4.Visible = True
                e.Cancel = True
            End If

            lblImmobC4.Visible = False
            If CheckBox8.Checked = True And CheckBox9.Checked = True Then
                lblImmobC4.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox8.Checked = True And txtImmob1C4.SelectedValue <> 0 Then
                    lblImmobC4.Visible = True
                    lblImmobC4.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox9.Checked = True And txtImmob2C4.SelectedValue <> 0 Then
                        lblImmobC4.Visible = True
                        lblImmobC4.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox8.Checked = True Or CheckBox9.Checked = True) And DropDownList31.SelectedValue = -1 Then
                            lblImmobC4.Visible = True
                            lblImmobC4.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox8.Checked = True Or CheckBox9.Checked = True) And (CheckBox6.Checked = True Or CheckBox7.Checked = True) Then
                                lblImmobC4.Visible = True
                                lblImmobC4.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If

                        End If
                    End If
                End If
            End If

        End If

        If e.CurrentStepIndex = 6 Then
            Label2C5.Visible = False
            Label3C5.Visible = False
            lblCFC5.Visible = False
            lblMutuo1C5.Visible = False

            lblAvvIndennitac5.Visible = False
            If cmbAccompagnamentoC5.SelectedItem.Text = "SI" And cmbInvaliditaC5.SelectedItem.Text <> "100" Then
                lblAvvIndennitac5.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC5.Text = "" Then
                Label2C5.Visible = True
                Label2C5.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC5.Text = "" Then
                Label3C5.Visible = True
                Label3C5.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC5.Text) < 16 Then
                lblCFC5.Visible = True
                lblCFC5.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC5.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC5.Text), UCase(txtCognomeC5.Text), UCase(txtNomeC5.Text)) = True Then

                Else
                    lblCFC5.Visible = True
                    lblCFC5.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC5.Visible = True
                lblCFC5.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C5.Text > txtValore1C5.Text Then
                lblMutuo1C5.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C5.Text > txtValore2C5.Text Then
                lblMutuo2C5.Visible = True
                e.Cancel = True
            End If

            lblImmobC5.Visible = False
            If CheckBox10.Checked = True And CheckBox11.Checked = True Then
                lblImmobC5.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox10.Checked = True And txtImmob1C5.SelectedValue <> 0 Then
                    lblImmobC5.Visible = True
                    lblImmobC5.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox11.Checked = True And txtImmob2C5.SelectedValue <> 0 Then
                        lblImmobC5.Visible = True
                        lblImmobC5.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox10.Checked = True Or CheckBox11.Checked = True) And DropDownList32.SelectedValue = -1 Then
                            lblImmobC5.Visible = True
                            lblImmobC5.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox10.Checked = True Or CheckBox11.Checked = True) And (CheckBox8.Checked = True Or CheckBox9.Checked = True) Then
                                lblImmobC5.Visible = True
                                lblImmobC5.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If

        End If

        If e.CurrentStepIndex = 7 Then
            Label2C6.Visible = False
            Label3C6.Visible = False
            lblCFC6.Visible = False
            lblMutuo1C6.Visible = False

            lblAvvIndennitac6.Visible = False
            If cmbAccompagnamentoC6.SelectedItem.Text = "SI" And cmbInvaliditaC6.SelectedItem.Text <> "100" Then
                lblAvvIndennitac6.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC2.Text = "" Then
                Label2C6.Visible = True
                Label2C6.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC6.Text = "" Then
                Label3C6.Visible = True
                Label3C6.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC6.Text) < 16 Then
                lblCFC6.Visible = True
                lblCFC6.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC6.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC6.Text), UCase(txtCognomeC6.Text), UCase(txtNomeC6.Text)) = True Then

                Else
                    lblCFC6.Visible = True
                    lblCFC6.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC6.Visible = True
                lblCFC6.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C6.Text > txtValore1C6.Text Then
                lblMutuo1C6.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C6.Text > txtValore2C6.Text Then
                lblMutuo2C6.Visible = True
                e.Cancel = True
            End If

            lblImmobC6.Visible = False
            If CheckBox12.Checked = True And CheckBox13.Checked = True Then
                lblImmobC6.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox12.Checked = True And txtImmob1C6.SelectedValue <> 0 Then
                    lblImmobC6.Visible = True
                    lblImmobC6.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox13.Checked = True And txtImmob2C6.SelectedValue <> 0 Then
                        lblImmobC6.Visible = True
                        lblImmobC6.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox12.Checked = True Or CheckBox13.Checked = True) And DropDownList33.SelectedValue = -1 Then
                            lblImmobC6.Visible = True
                            lblImmobC6.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox12.Checked = True Or CheckBox13.Checked = True) And (CheckBox10.Checked = True Or CheckBox11.Checked = True) Then
                                lblImmobC6.Visible = True
                                lblImmobC6.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If e.CurrentStepIndex = 8 Then
            Label2C7.Visible = False
            Label3C7.Visible = False
            lblCFC7.Visible = False
            lblMutuo1C7.Visible = False

            lblAvvIndennitac7.Visible = False
            If cmbAccompagnamentoC7.SelectedItem.Text = "SI" And cmbInvaliditaC7.SelectedItem.Text <> "100" Then
                lblAvvIndennitac7.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC7.Text = "" Then
                Label2C7.Visible = True
                Label2C7.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC7.Text = "" Then
                Label3C7.Visible = True
                Label3C7.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC7.Text) < 16 Then
                lblCFC7.Visible = True
                lblCFC7.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC7.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC7.Text), UCase(txtCognomeC7.Text), UCase(txtNomeC7.Text)) = True Then

                Else
                    lblCFC7.Visible = True
                    lblCFC7.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC7.Visible = True
                lblCFC7.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C7.Text > txtValore1C7.Text Then
                lblMutuo1C7.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C7.Text > txtValore2C7.Text Then
                lblMutuo2C7.Visible = True
                e.Cancel = True
            End If

            lblImmobC7.Visible = False
            If CheckBox14.Checked = True And CheckBox15.Checked = True Then
                lblImmobC7.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox14.Checked = True And txtImmob1C7.SelectedValue <> 0 Then
                    lblImmobC7.Visible = True
                    lblImmobC7.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox15.Checked = True And txtImmob2C7.SelectedValue <> 0 Then
                        lblImmobC7.Visible = True
                        lblImmobC7.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox14.Checked = True Or CheckBox15.Checked = True) And DropDownList34.SelectedValue = -1 Then
                            lblImmobC7.Visible = True
                            lblImmobC7.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox14.Checked = True Or CheckBox15.Checked = True) And (CheckBox12.Checked = True Or CheckBox13.Checked = True) Then
                                lblImmobC7.Visible = True
                                lblImmobC7.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If e.CurrentStepIndex = 9 Then
            Label2C8.Visible = False
            Label3C8.Visible = False
            lblCFC8.Visible = False
            lblMutuo1C8.Visible = False

            lblAvvIndennitac8.Visible = False
            If cmbAccompagnamentoC8.SelectedItem.Text = "SI" And cmbInvaliditaC8.SelectedItem.Text <> "100" Then
                lblAvvIndennitac8.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC8.Text = "" Then
                Label2C8.Visible = True
                Label2C8.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC8.Text = "" Then
                Label3C8.Visible = True
                Label3C8.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC8.Text) < 16 Then
                lblCFC8.Visible = True
                lblCFC8.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC8.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC8.Text), UCase(txtCognomeC8.Text), UCase(txtNomeC8.Text)) = True Then

                Else
                    lblCFC8.Visible = True
                    lblCFC8.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC8.Visible = True
                lblCFC8.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C8.Text > txtValore1C8.Text Then
                lblMutuo1C8.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C8.Text > txtValore2C8.Text Then
                lblMutuo2C8.Visible = True
                e.Cancel = True
            End If

            lblImmobC8.Visible = False
            If CheckBox16.Checked = True And CheckBox17.Checked = True Then
                lblImmobC8.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox16.Checked = True And txtImmob1C8.SelectedValue <> 0 Then
                    lblImmobC8.Visible = True
                    lblImmobC8.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox17.Checked = True And txtImmob2C8.SelectedValue <> 0 Then
                        lblImmobC8.Visible = True
                        lblImmobC8.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox16.Checked = True Or CheckBox17.Checked = True) And DropDownList35.SelectedValue = -1 Then
                            lblImmobC8.Visible = True
                            lblImmobC8.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox16.Checked = True Or CheckBox17.Checked = True) And (CheckBox14.Checked = True Or CheckBox15.Checked = True) Then
                                lblImmobC8.Visible = True
                                lblImmobC8.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If
        End If



        If e.CurrentStepIndex = 10 Then
            Label2C9.Visible = False
            Label3C9.Visible = False
            lblCFC9.Visible = False
            lblMutuo1C9.Visible = False

            lblAvvIndennitac9.Visible = False
            If cmbAccompagnamentoC9.SelectedItem.Text = "SI" And cmbInvaliditaC9.SelectedItem.Text <> "100" Then
                lblAvvIndennitac9.Visible = True
                e.Cancel = True
            End If

            If txtCognomeC9.Text = "" Then
                Label2C9.Visible = True
                Label2C9.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If
            If txtNomeC9.Text = "" Then
                Label3C9.Visible = True
                Label3C9.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If Len(txtCFC9.Text) < 16 Then
                lblCFC9.Visible = True
                lblCFC9.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If par.ControllaCF(UCase(txtCFC9.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCFC9.Text), UCase(txtCognomeC9.Text), UCase(txtNomeC9.Text)) = True Then

                Else
                    lblCFC9.Visible = True
                    lblCFC9.Text = "Mancante/Non Corretto"
                    e.Cancel = True
                End If
            Else
                lblCFC9.Visible = True
                lblCFC9.Text = "Mancante/Non Corretto"
                e.Cancel = True
            End If

            If txtMutuo1C9.Text > txtValore1C9.Text Then
                lblMutuo1C9.Visible = True
                e.Cancel = True
            End If

            If txtMutuo2C9.Text > txtValore2C9.Text Then
                lblMutuo2C9.Visible = True
                e.Cancel = True
            End If

            lblImmobC9.Visible = False
            If CheckBox18.Checked = True And CheckBox19.Checked = True Then
                lblImmobC9.Text = "Solo 1 immobile a uso abitativo!"
                e.Cancel = True
            Else
                If CheckBox18.Checked = True And txtImmob1C9.SelectedValue <> 0 Then
                    lblImmobC9.Visible = True
                    lblImmobC9.Text = "Uso abitativo solo per Fabbricati!"
                    e.Cancel = True
                Else
                    If CheckBox19.Checked = True And txtImmob2C9.SelectedValue <> 0 Then
                        lblImmobC9.Visible = True
                        lblImmobC9.Text = "Uso abitativo solo per Fabbricati!"
                        e.Cancel = True
                    Else
                        If (CheckBox18.Checked = True Or CheckBox19.Checked = True) And DropDownList36.SelectedValue = -1 Then
                            lblImmobC9.Visible = True
                            lblImmobC9.Text = "Specificare Cat. Catastale"
                            e.Cancel = True
                        Else
                            If (CheckBox18.Checked = True Or CheckBox19.Checked = True) And (CheckBox16.Checked = True Or CheckBox17.Checked = True) Then
                                lblImmobC9.Visible = True
                                lblImmobC9.Text = "Atri Imm. ad uso Abitativo!"
                                e.Cancel = True
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function RicavaDataDaCF(ByVal cf As String) As String
        Dim MIADATA As String

        If Val(Mid(cf, 10, 2)) > 40 Then
            MIADATA = Format(Val(Mid(cf, 10, 2)) - 40, "00")
        Else
            MIADATA = Mid(cf, 10, 2)
        End If
        Select Case Mid(cf, 9, 1)
            Case "A"
                MIADATA = MIADATA & "/01"
            Case "B"
                MIADATA = MIADATA & "/02"
            Case "C"
                MIADATA = MIADATA & "/03"
            Case "D"
                MIADATA = MIADATA & "/04"
            Case "E"
                MIADATA = MIADATA & "/05"
            Case "H"
                MIADATA = MIADATA & "/06"
            Case "L"
                MIADATA = MIADATA & "/07"
            Case "M"
                MIADATA = MIADATA & "/08"
            Case "P"
                MIADATA = MIADATA & "/09"
            Case "R"
                MIADATA = MIADATA & "/10"
            Case "S"
                MIADATA = MIADATA & "/11"
            Case "T"
                MIADATA = MIADATA & "/12"
        End Select
        If Mid(cf, 7, 1) = "0" Then
            MIADATA = MIADATA & "/200" & Mid(cf, 8, 1)
        Else
            MIADATA = MIADATA & "/19" & Mid(cf, 7, 2)
        End If
        RicavaDataDaCF = MIADATA

    End Function

    Protected Sub btnStampaRic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaRic.Click
        Response.Write("<script>window.open('Ricevuta.aspx?DO=" & par.CriptaMolto(Format(Session.Item("FATTO"), "0000000000")) & "&NO=" & par.CriptaMolto(txtCognome.Text & " " & txtNome.Text) & "&TE=" & par.CriptaMolto(txtTelefono.Text) & "','Ricevuta','');</script>")
    End Sub


End Class
