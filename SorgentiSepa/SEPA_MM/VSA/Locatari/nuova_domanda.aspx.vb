
Partial Class VSA_Locatari_nuova_domanda
    Inherits PageSetIdMode
    Dim par As New CM.Global
    'Dim annoDaPresent As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            DAFARE = True
            AUpresente = True
            dataErrata = False

            LBLid.Value = Request.QueryString("ID")
            LBLcodUI.Value = Request.QueryString("COD")
            LBLintest.Value = Request.QueryString("INTEST")

            ControllaProroga()
            ControllaPassaggioERP()
            'ContrattoL43198()
            'ContrattoTemporaneo()
            'CercaContratto392()
            If Request.QueryString("GLoc") <> "1" Then
                caricaComponenti()
            Else
                lblDom.Visible = False
                cmbComponenti.Visible = False
            End If

            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim FFOO As Boolean = False
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & LBLcodUI.Value & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If par.IfNull(myReader("PROVENIENZA_ASS"), "") = "10" Then
                        FFOO = True
                    End If
                    dataStipula = par.FormattaData(par.IfNull(myReader("DATA_STIPULA"), ""))
                End If
                myReader.Close()

                Dim abusAmmvo As Boolean = False
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO =" & LBLid.Value
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    abusAmmvo = True
                End If
                myReader.Close()


                If abusAmmvo = False Then
                    If FFOO = True Then
                        par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 AND ID <> 1 and ID <> 0 and ID <> 9 and ID <> 10 "
                    Else
                        If PassaggioERP = True Then
                            'Dim TipoGL As String = "4"
                            'par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=132"
                            'myReader = par.cmd.ExecuteReader()
                            'If myReader.Read Then
                            '    TipoGL = par.IfNull(myReader("valore"), "4")
                            'End If
                            'myReader.Close()
                            'par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE ID IN (" & TipoGL & ") order by descrizione"
                            par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE ID in (10,12) "
                        Else
                            Select Case TipoContratto
                                Case "EQC392"
                                    Dim TipoGL As String = "4"
                                    par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=132"
                                    myReader = par.cmd.ExecuteReader()
                                    If myReader.Read Then
                                        TipoGL = par.IfNull(myReader("valore"), "4")
                                    End If
                                    myReader.Close()
                                    par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 and ID IN (" & TipoGL & ",12) "

                                Case Else
                                    If contrProroga = True Then
                                        par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 AND ID in (3, 10, 12) and ID <> 9 "
                                    Else
                                        par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 AND ID <> 10 and ID <> 6 and ID <> 9 "
                                        If TipoContratto = "L43198" Then
                                            par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE ID in (12)"
                                        End If
                                    End If
                            End Select
                        End If
                    End If
                Else
                    par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE ID = 9"
                End If

                par.cmd.CommandText &= " minus SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE ID = 11 order by 2"
                myReader = par.cmd.ExecuteReader()
                cmbTipo.Items.Add(New ListItem(" - seleziona - ", -1))
                While myReader.Read
                    cmbTipo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("ID")))
                End While
                myReader.Close()



                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                'Response.Write(ex.Message)
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

            End Try

        End If
    End Sub

    Public Property annoRedd() As String
        Get
            If Not (ViewState("par_anni") Is Nothing) Then
                Return CStr(ViewState("par_anni"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_anni") = value
        End Set

    End Property

    Public Property dataStipula() As String
        Get
            If Not (ViewState("par_dataStipula") Is Nothing) Then
                Return CStr(ViewState("par_dataStipula"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataStipula") = value
        End Set

    End Property

    Public Property data1scadenza() As String
        Get
            If Not (ViewState("par_data1scadenza") Is Nothing) Then
                Return CStr(ViewState("par_data1scadenza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_data1scadenza") = value
        End Set

    End Property

    Public Property data2scadenza() As String
        Get
            If Not (ViewState("par_data2scadenza") Is Nothing) Then
                Return CStr(ViewState("par_data2scadenza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_data2scadenza") = value
        End Set

    End Property

    Public Property contrProroga() As Boolean
        Get
            If Not (ViewState("par_contrProroga") Is Nothing) Then
                Return CLng(ViewState("par_contrProroga"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_contrProroga") = value
        End Set

    End Property

    Public Property PassaggioERP() As Boolean
        Get
            If Not (ViewState("par_PassaggioERP") Is Nothing) Then
                Return CLng(ViewState("par_PassaggioERP"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_PassaggioERP") = value
        End Set

    End Property

    Public Property TipoContratto() As String
        Get
            If Not (ViewState("par_TipoContratto") Is Nothing) Then
                Return CStr(ViewState("par_TipoContratto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TipoContratto") = value
        End Set

    End Property


    'Public Property contratto392() As Boolean
    '    Get
    '        If Not (ViewState("par_contratto392") Is Nothing) Then
    '            Return CLng(ViewState("par_contratto392"))
    '        Else
    '            Return False
    '        End If
    '    End Get

    '    Set(ByVal value As Boolean)
    '        ViewState("par_contratto392") = value
    '    End Set

    'End Property
    Public Property dataErrata() As Boolean
        Get
            If Not (ViewState("par_dataErrata") Is Nothing) Then
                Return CLng(ViewState("par_dataErrata"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_dataErrata") = value
        End Set

    End Property

    Public Property DAFARE() As Boolean
        Get
            If Not (ViewState("par_DAFARE") Is Nothing) Then
                Return CLng(ViewState("par_DAFARE"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_DAFARE") = value
        End Set

    End Property

    Public Property AUpresente() As Boolean
        Get
            If Not (ViewState("par_AUpresente") Is Nothing) Then
                Return CLng(ViewState("par_AUpresente"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_AUpresente") = value
        End Set

    End Property

    Public Property idBandoUltimo() As Long
        Get
            If Not (ViewState("par_idBandoUltimo") Is Nothing) Then
                Return CLng(ViewState("par_idBandoUltimo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBandoUltimo") = value
        End Set

    End Property

    Public Property annoReddAU() As Integer
        Get
            If Not (ViewState("par_annoReddAU") Is Nothing) Then
                Return CInt(ViewState("par_annoReddAU"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_annoReddAU") = value
        End Set

    End Property

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim err As Boolean = False

        If Request.QueryString("GLoc") = "1" Then
            Response.Redirect("RicercaContratti.aspx")
        Else
            'If cmbMotivo.SelectedValue = "-1" And (cmbTipo.SelectedValue <> "3") Then
            '    lblMess.Visible = True
            '    lblMess.Text = "Attenzione! Inserire il motivo di presentazione della richiesta!"
            '    btnProcedi.Visible = False
            'Response.Write("alert('Attenzione! Inserire il motivo di presentazione della richiesta!')")

            'If IsNothing(cmbAnniRedd.SelectedItem) Then
            '    lblMess.Visible = True
            '    lblMess.Text = "Attenzione! Non è possibile procedere!"
            '    err = True
            'End If

            If dataErrata = True Then
                err = True
            End If

            If cmbMotivo.Visible = True And cmbMotivo.SelectedValue = "-1" Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire il motivo di presentazione della richiesta!"
                'btnProcedi.Visible = False
                err = True
            End If
            If txtDataEvento.Visible = True And txtDataEvento.Text <> "" And Len(txtDataEvento.Text) = 10 Then
                If Not par.ControllaData(txtDataEvento) Or Right(txtDataEvento.Text, 4) < 2000 Or Right(txtDataEvento.Text, 4) > 2100 Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire una data valida!"
                    'btnProcedi.Visible = False
                    err = True
                End If
            ElseIf txtDataEvento.Visible = True And txtDataEvento.Text = "" Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Campo data evento non valorizzato!"
                'btnProcedi.Visible = False
                err = True
            ElseIf txtDataEvento.Visible = True And Len(txtDataEvento.Text) < 10 Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire una data valida!"
                'btnProcedi.Visible = False
                err = True
            End If
            If txtDataPr.Visible = True And txtDataPr.Text <> "" And Len(txtDataPr.Text) = 10 Then
                If Not par.ControllaData(txtDataPr) Or Right(txtDataPr.Text, 4) < 2000 Or Right(txtDataPr.Text, 4) > 2100 Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire una data valida!"
                    'btnProcedi.Visible = False
                    err = True
                End If
            ElseIf txtDataPr.Visible = True And txtDataPr.Text = "" Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Campo data presentazione non valorizzato!"
                'btnProcedi.Visible = False
                err = True
            ElseIf txtDataPr.Visible = True And Len(txtDataPr.Text) < 10 Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire una data valida!"
                'btnProcedi.Visible = False
                err = True
            End If
            If cmbMotivo.SelectedValue = 23 Then 'Peggioramento definitivo a canone provvisorio
                If Right(txtDataPr.Text, 4) <> Right(txtDataEvento.Text, 4) Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! L'anno di presentazione e l'anno dell'evento devono essere uguali all'anno di reddito!"
                    'btnProcedi.Visible = False
                    err = True
                Else
                    lblMess.Visible = False
                    ' btnProcedi.Visible = True
                End If
            End If

            If cmbTipo.SelectedValue = "-1" Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Selezionare il tipo di domanda che si intende presentare!"
                'btnProcedi.Visible = False
                err = True
            End If
            'If err = False And cmbMotivo.SelectedValue >= 17 Then
            '    Response.Redirect("dichiarazione1.aspx?ID=" & LBLid.Value & "&COD=" & LBLcodUI.Value & "&INTEST=" & cmbComponenti.SelectedItem.Value & "&ModR=" & cmbModRichiesta.SelectedItem.Value & "&COMP=" & cmbComponenti.SelectedItem.Value & "&T=" & cmbTipo.SelectedItem.Value & "&CAUS=" & cmbMotivo.SelectedValue & "&DATA=" & par.AggiustaData(par.IfNull(txtDataPr.Text, "")) & "&ANNI=" & annoRedd & "&DATAEVE=" & par.AggiustaData(txtDataEvento.Text) & "&INIZ=" & par.AggiustaData(txtDataIn.Text) & "&FINE=" & par.AggiustaData(txtDataFine.Text))
            'ElseIf err = False Then
            '    Response.Redirect("dichiarazione1.aspx?ID=" & LBLid.Value & "&COD=" & LBLcodUI.Value & "&INTEST=" & cmbComponenti.SelectedItem.Value & "&ModR=" & cmbModRichiesta.SelectedItem.Value & "&COMP=" & cmbComponenti.SelectedItem.Value & "&T=" & cmbTipo.SelectedItem.Value & "&CAUS=" & cmbMotivo.SelectedValue & "")
            'End If
            If txtAnnoReddito.Visible = True And txtAnnoReddito.Text = "" Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Anno reddituale non inserito!"
                err = True
            End If

            'If cmbTipo.SelectedValue = 3 Then
            '    If IsNothing(cmbAnniRedd.SelectedItem) = False Then
            '        If cmbAnniRedd.Visible = True And txtAnnoReddito.Text <> cmbAnniRedd.SelectedItem.Text Then
            '            lblMess.Visible = True
            '            lblMess.Text = "Attenzione! L'anno reddituale specificato non coincide con l'anno reddituale da inserire!"
            '            err = True
            '        End If
            '    End If
            'End If

            If cmbMotivo.SelectedItem.Value = 23 Then
                If txtAnnoReddito.Text = Mid(txtDataPr.Text, 7, 4) And txtAnnoReddito.Text = Mid(txtDataEvento.Text, 7, 4) Then

                Else
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! L'anno reddituale specificato deve coincidere con l'anno dell'evento e della presentazione!"
                    err = True
                End If

            End If

            If cmbTipo.SelectedValue = 5 Then
                If txtUIscambio.Text = "" Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire il codice dell'alloggio con cui si intende effettuare lo scambio!"
                    err = True
                Else
                    If ControlloCodUI(txtUIscambio.Text) = False Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Codice dell'alloggio non corretto!"
                        err = True
                    End If
                End If
            End If

            If txtSaldo2.Visible = True Then
                txtSaldo2.Text = HiddenLBLsaldo2.Value
            End If


            If cmbTipo.SelectedValue = 0 Or cmbTipo.SelectedValue = 5 Or cmbTipo.SelectedValue = 10 Then
                If ControlliCampiDebito() = True Then
                    err = True
                Else
                    lblMess.Visible = False
                End If
            End If

            If cmbTipo.SelectedValue = 5 Then
                If ControlloCodUI(txtUIscambio.Text) = True Then
                    If ControlliCampiDebito2() = True Then
                        err = True
                    Else
                        lblMess.Visible = False
                    End If
                End If
                If LBLcodContr.Value = LBLcodContr2.Value Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Codice alloggio non corretto!"
                    err = True
                End If
            End If

            If DAFARE = False Then
                lblMess0.Visible = True
                err = True
            End If

            If AUpresente = False Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Impossibile procedere, nessuna AU è stata trovata!"
                err = True
            End If

            If err = False Then
                lblMess.Visible = False
                lblMess.Text = ""
            End If

            If lblMess.Visible = True Then
                err = True
            End If

            'If cmbTipo.SelectedItem.Value = "6" Then
            '    If ControllaConiuge() = False Then
            '        lblMess.Visible = True
            '        lblMess.Text = "Impossibile procedere! Il subentrante selezionato non è il CONIUGE del titolare dell'alloggio."
            '        err = True
            '    End If
            'End If




            If err = False Then
                Dim dataevento As String = ""
                Dim sindacato As String = ""
                Dim altro As String = ""

                Select Case cmbTipo.SelectedItem.Value
                    Case "5"
                        dataevento = par.AggiustaData(txtDataPr.Text)
                    Case Else
                        dataevento = par.AggiustaData(txtDataEvento.Text)
                End Select
                If cmbMotivo.SelectedValue = 28 Then
                    dataevento = "20100101"
                End If
                If cmbMotivo.SelectedValue = 29 Then
                    Select Case cmbAU.SelectedValue
                        Case 1
                            dataevento = "20080101"
                        Case 2
                            dataevento = "20100101"
                        Case 3
                            dataevento = "20120101"
                        Case 5
                            dataevento = "20140101"
                    End Select
                End If
                If Not IsNothing(cmbSindacato.SelectedValue) Then
                    sindacato = cmbSindacato.SelectedValue
                Else
                    sindacato = ""
                End If
                If Not IsNothing(txtAltro) Then
                    altro = par.PulisciStrSql(txtAltro.Text)
                End If
                Dim compExtra As String = ""
                If cmbTipo.SelectedValue = 0 Or cmbTipo.SelectedValue = 5 Or cmbTipo.SelectedValue = 10 Then
                    LBLintest2.Value = cmbIntestRU.SelectedValue
                    Response.Redirect("dichiarazione1.aspx?ID=" & LBLid.Value & "&ID2=" & LBLid2.Value & "&COD=" & LBLcodUI.Value & "&INTEST=" & cmbComponenti.SelectedItem.Value & "&ModR=" & cmbModRichiesta.SelectedItem.Value & "&T=" & cmbTipo.SelectedItem.Value & "&CAUS=" & cmbMotivo.SelectedValue & "&DATA=" & par.AggiustaData(par.IfNull(txtDataPr.Text, "")) & "&ANNI=" & txtAnnoReddito.Text & "&DATAEVE=" & dataevento & "&INIZ=" & par.AggiustaData(txtDataIn.Text) & "&FINE=" & par.AggiustaData(txtDataFine.Text) & "&IDSIND=" & sindacato & "&ALTRO=" & altro & "&DEB=" & txtSaldo.Text & "&IMP=" & txtImporto.Text & "&NRA=" & txtNumRate.Text & "&DEB2=" & txtSaldo2.Text & "&IMP2=" & txtImporto2.Text & "&NRA2=" & txtNumRate2.Text & "&CODCONTR2=" & LBLcodContr2.Value & "&INTEST2=" & LBLintest2.Value)
                Else
                    If cmbComponenti.SelectedValue = LBLcompEXTRA.Value Then
                        compExtra = "1"
                    Else
                        compExtra = ""
                    End If
                    Response.Redirect("dichiarazione1.aspx?ID=" & LBLid.Value & "&COD=" & LBLcodUI.Value & "&INTEST=" & cmbComponenti.SelectedItem.Value & "&COMPEX=" & compExtra & "&ModR=" & cmbModRichiesta.SelectedItem.Value & "&T=" & cmbTipo.SelectedItem.Value & "&CAUS=" & cmbMotivo.SelectedValue & "&DATA=" & par.AggiustaData(par.IfNull(txtDataPr.Text, "")) & "&ANNI=" & txtAnnoReddito.Text & "&DATAEVE=" & dataevento & "&INIZ=" & par.AggiustaData(txtDataIn.Text) & "&FINE=" & par.AggiustaData(txtDataFine.Text) & "&IDSIND=" & sindacato & "&ALTRO=" & altro)
                End If
            End If
        End If
    End Sub

    Private Function ControllaSeAmmissibile(ByVal idContr As Long, ByRef motivoEsclusione As String, ByVal tipoContr As String) As Boolean
        Dim ammissibile As Boolean = False

        Try
            Dim idBandoAU As Integer = 0
            Dim idDichAU As Integer = 0
            If idContr <> 0 Then
                par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.id,UTENZA_BANDI.id as id_bando_au FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE /*UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL<='" & par.AggiustaData(txtDataEvento.Text) & "' and*/" _
                    & " NVL(FL_GENERAZ_AUTO,0)=0 And (UTENZA_DICHIARAZIONI.NOTE_WEB Is NULL Or UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                    & " AND RAPPORTO=(select cod_contratto from siscom_mi.rapporti_utenza where id=" & idContr & ") ORDER BY ID_BANDO DESC"
                Dim myReader002 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader002.Read Then
                    idBandoAU = myReader002("id_bando_au")
                    idDichAU = myReader002("id")
                End If
                myReader002.Close()

                If motivoEsclusione = "" Then
                    par.cmd.CommandText = "select * from siscom_mi.canoni_ec where id_dichiarazione=" & idDichAU & " and id_bando_au=" & idBandoAU & " order by inizio_validita_can desc,id desc"
                    Dim myReader02 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader02.Read Then
                        If myReader02("id_area_economica") = 4 Then
                            motivoEsclusione = "utente in Area di Decadenza"
                        End If

                        ammissibile = False
                    End If
                    myReader02.Close()
                End If
                If motivoEsclusione = "" Then
                    par.cmd.CommandText = "select * from siscom_mi.canoni_ec where id_dichiarazione=" & idDichAU & " and id_bando_au=" & idBandoAU & ""
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If Not myReader.Read Then
                        motivoEsclusione = "Anagrafe Utenza mancante"
                        ammissibile = False
                        par.cmd.CommandText = "select * from siscom_mi.canoni_ec where id_contratto=" & idContr & " /*and inizio_validita_can<='20191231' */AND fine_validita_can>='20180101' and tipo_provenienza=1"
                        Dim myReader01 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If Not myReader01.Read Then
                            motivoEsclusione = "Anagrafe Utenza e domanda di Revisione Canone con validità 2018 mancanti"
                            ammissibile = False
                        Else
                            motivoEsclusione = ""
                            ammissibile = True
                        End If
                        myReader01.Close()
                    End If
                    myReader.Close()
                End If

                If tipoContr <> "ERP" Then
                    par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI " _
                        & "WHERE UTENZA_DICHIARAZIONI.DATA_FINE_VAL>='20171231' AND NVL(FL_GENERAZ_AUTO,0)=0 " _
                        & " And (UTENZA_DICHIARAZIONI.NOTE_WEB Is NULL Or UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                        & "AND RAPPORTO=(select cod_contratto from siscom_mi.rapporti_utenza where id=" & idContr & ") ORDER BY ID_BANDO DESC"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If Not myReader2.Read Then
                        motivoEsclusione = "Anagrafe Utenza 2017 con validità 2018 mancante"
                        ammissibile = False
                    Else
                        motivoEsclusione = ""
                    End If
                    myReader2.Close()
                End If

                If motivoEsclusione = "" Then
                    ammissibile = True
                End If

            End If
            'If ammissibile = True Then
            '    par.cmd.CommandText = " select nvl(SUM (tot_emesso - tot_incassato),0) AS saldo_contab_al_31122016  from (  " _
            '        & " select rapporti_utenza.COD_CONTRATTO,  " _
            '        & " SUM (IMPORTO_TOTALE -NVL(IMPORTO_RIC_B, 0) - NVL(QUOTA_SIND_B, 0)) AS tot_emesso,   " _
            '        & " NVL ( SUM ( IMPORTO_PAGATO - nvl((SELECT SUM (imp_pagato) FROM siscom_mi.bol_bollette_voci WHERE bol_bollette_voci.id_bolletta = bol_bollette.id   " _
            '        & " AND (id_voce IN (150, 151, 677, 676, 7, 126, 182) OR id_voce IN (SELECT id FROM siscom_mi.t_voci_bolletta WHERE gruppo = 5))),0) /*- NVL (IMPORTO_RIC_PAGATO_B, 0)*/) - NVL ( (SELECT SUM (importo_pagato)   " _
            '        & " FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE data_pagamento  > '" & par.AggiustaData(txtDataPr.Text) & "'   " _
            '        & " AND id_gruppo_voce_bolletta <> 5 AND id_t_voce_bolletta NOT IN (150, 151, 677, 676, 7, 126, 182)  " _
            '        & " AND id_bolletta = BOL_BOLLETTE.ID AND bol_bollette.id_contratto = rapporti_utenza.id), 0), 0) AS tot_incassato   " _
            '        & " FROM siscom_mi.rapporti_utenza,siscom_mi.bol_bollette WHERE      " _
            '        & " (bol_bollette.DATA_SCADENZA<= '20161231' or BOL_BOLLETTE.id_tipo=26 or BOL_BOLLETTE.id_tipo=27)  " _
            '        & " and BOL_BOLLETTE.id_tipo NOT IN (22,25)  " _
            '        & " and id_Bolletta_storno is null " _
            '        & " and nvl(importo_ruolo,0)=0 " _
            '        & " and nvl(importo_ingiunzione,0)=0  " _
            '        & " and rapporti_utenza.id=" & idContr & "  " _
            '        & " And BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID  " _
            '        & " And (FL_ANNULLATA = 0 Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL))  " _
            '        & " And bol_bollette.id_bolletta_ric Is null " _
            '        & " And bol_bollette.id_rateizzazione Is null " _
            '        & " GROUP BY rapporti_utenza.COD_CONTRATTO,BOL_BOLLETTE.ID,BOL_BOLLETTE.ID_contratto,rapporti_utenza.id)   "
            '    Dim saldo As Integer = par.cmd.ExecuteScalar
            '    If saldo > 0 Then
            '        ammissibile = True
            '    Else
            '        motivoEsclusione = "saldo al 31/12/2016 pari a 0"
            '        ammissibile = False
            '    End If
            'End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try


        Return ammissibile

    End Function

    Private Function ControllaSeEsisteAltroRU(ByRef RUattivo As Boolean, ByRef RUchiuso As Boolean) As Boolean

        Try
            RUattivo = False
            RUchiuso = False
            If LBLid.Value <> "" Then

                par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where id=" & LBLid.Value & " and data_riconsegna is not null"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    RUchiuso = True
                    par.cmd.CommandText = "select (select cod_tipologia_contr_loc from siscom_mi.rapporti_utenza where soggetti_contrattuali.id_contratto=rapporti_utenza.id) as tipoRU , siscom_mi.getstatocontratto(id_contratto) as stato_ru,id_contratto from siscom_mi.soggetti_contrattuali where cod_tipologia_occupante='INTE' and id_contratto<>" & LBLid.Value & "" _
                      & "  And id_anagrafica IN (SELECT id FROM siscom_mi.anagrafica WHERE cod_fiscale IN (SELECT cod_fiscale " _
                        & " From utenza_comp_nucleo Where progr = 0 And id_dichiarazione = " _
                        & "(Select ID From utenza_dichiarazioni Where rapporto = (Select cod_contratto From siscom_mi.rapporti_utenza Where ID =" & LBLid.Value & ") " _
                        & "And id = (select max(id) from utenza_dichiarazioni udd where utenza_dichiarazioni.rapporto=udd.rapporto)))) " _
                        & "And id_contratto IN (SELECT id FROM siscom_mi.rapporti_utenza WHERE soggetti_contrattuali.id_contratto =rapporti_utenza.id And cod_tipologia_contr_loc Not IN ('USD','NONE')) "
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If myReader("stato_ru") = "IN CORSO" Then
                            If ControllaSeAmmissibile(myReader("id_contratto"), "", par.IfNull(myReader("tipoRU"), "")) = True Then
                                RUattivo = True
                            End If
                        End If
                    End If
                    myReader.Close()

                End If
                myReader0.Close()

            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try


        Return RUattivo

    End Function

    Private Function ControlloCodUI(ByVal codUI As String) As Boolean
        Dim esiste As Boolean = False

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            If LBLcodContr2.Value = "" Then
                par.cmd.CommandText = "SELECT DISTINCT (UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE), ANAGRAFICA.*,RAPPORTI_UTENZA.*,RAPPORTI_UTENZA.ID AS IDCONTR FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA " _
                & "WHERE UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE = '" & codUI & "' AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    LBLcodContr2.Value = par.IfNull(myReader("COD_CONTRATTO"), "")
                    LBLid2.Value = par.IfNull(myReader("IDCONTR"), "")
                    LBLintest2.Value = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")
                    txtSaldo2.Text = Format(par.CalcolaSaldoAttuale(LBLid2.Value), "##,##0.00")
                    HiddenLBLsaldo2.Value = Format(par.CalcolaSaldoAttuale(LBLid2.Value), "##,##0.00")
                    esiste = True
                End If
                myReader.Close()
            Else
                esiste = True
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

        Return esiste

    End Function

    Private Function ControlliCampiDebito() As Boolean

        Dim errore As Boolean = False

        If par.IfEmpty(txtSaldo.Text, 0) <> 0 Then
            If Not IsNumeric(txtSaldo.Text) Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire valore numerico!"
                errore = True
            Else
                If txtImporto.Text <> "" Then
                    If Not IsNumeric(txtImporto.Text) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Inserire valore numerico!"
                        errore = True
                    Else
                        If CDec(par.IfEmpty(txtImporto.Text, 0)) > Format(Math.Abs(CDec(txtSaldo.Text))) Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! L'importo accertato non può essere superiore al saldo!"
                            errore = True
                        Else
                            If par.IfEmpty(txtImporto.Text, 0) = 0 Then
                                If par.IfEmpty(txtNumRate.Text, 0) > 0 Then
                                    lblMess.Visible = True
                                    lblMess.Text = "Attenzione! Numero di rate non valido!"
                                    errore = True
                                End If
                            Else
                                If txtNumRate.Text = "" Then
                                    lblMess.Visible = True
                                    lblMess.Text = "Attenzione! Inserire il numero di rate!"
                                    errore = True
                                Else
                                    If Not IsNumeric(txtNumRate.Text) Then
                                        lblMess.Visible = True
                                        lblMess.Text = "Attenzione! Inserire valore numerico!"
                                        errore = True
                                    Else
                                        If Not (par.IfEmpty(txtNumRate.Text, 0) >= 0 And par.IfEmpty(txtNumRate.Text, 0) <= 72) Then
                                            lblMess.Visible = True
                                            lblMess.Text = "Attenzione! Inserire un numero di rate compreso tra 0 e 72!"
                                            errore = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    If txtNumRate.Text = "" Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Inserire il numero di rate!"
                        errore = True
                    Else
                        If Not IsNumeric(txtNumRate.Text) Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! Inserire valore numerico!"
                            errore = True
                        Else
                            If Not (par.IfEmpty(txtNumRate.Text, 0) >= 0 And par.IfEmpty(txtNumRate.Text, 0) <= 72) Then
                                lblMess.Visible = True
                                lblMess.Text = "Attenzione! Inserire un numero di rate compreso tra 0 e 72!"
                                errore = True
                            End If
                        End If
                    End If
                End If
            End If
        Else
            If txtImporto.Text <> "" Then
                If Not IsNumeric(txtImporto.Text) Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire valore numerico!"
                    errore = True
                Else
                    If par.IfEmpty(txtImporto.Text, 0) > 0 Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Importo non corretto!"
                        errore = True
                    End If
                End If
            Else
                txtImporto.Text = txtSaldo.Text
            End If
            If txtNumRate.Text <> "" Then
                If Not IsNumeric(txtNumRate.Text) Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire valore numerico!"
                    errore = True
                Else
                    If par.IfEmpty(txtNumRate.Text, 0) > 0 Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Numero di rate non corretto!"
                        errore = True
                    End If
                End If
            Else
                txtNumRate.Text = 0
            End If
        End If

        Return errore

    End Function

    Private Function ControlliCampiDebito2() As Boolean

        Dim errore As Boolean = False

        If par.IfEmpty(txtSaldo2.Text, 0) <> 0 Then
            If Not IsNumeric(txtSaldo2.Text) Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire valore numerico!"
                errore = True
            Else
                If txtImporto2.Text <> "" Then
                    If Not IsNumeric(txtImporto2.Text) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Inserire valore numerico!"
                        errore = True
                    Else
                        If CDec(par.IfEmpty(txtImporto2.Text, 0)) > Format(Math.Abs(CDec(txtSaldo2.Text))) Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! L'importo accertato non può essere superiore al saldo!"
                            errore = True
                        Else
                            If txtImporto2.Text = 0 Then
                                If par.IfEmpty(txtNumRate2.Text, 0) > 0 Then
                                    lblMess.Visible = True
                                    lblMess.Text = "Attenzione! Numero di rate non valido!"
                                    errore = True
                                End If
                            Else
                                If txtNumRate2.Text = "" Then
                                    lblMess.Visible = True
                                    lblMess.Text = "Attenzione! Inserire il numero di rate!"
                                    errore = True
                                Else
                                    If Not IsNumeric(txtNumRate2.Text) Then
                                        lblMess.Visible = True
                                        lblMess.Text = "Attenzione! Inserire valore numerico!"
                                        errore = True
                                    Else
                                        If Not (par.IfEmpty(txtNumRate2.Text, 0) >= 0 And par.IfEmpty(txtNumRate2.Text, 0) <= 72) Then
                                            lblMess.Visible = True
                                            lblMess.Text = "Attenzione! Inserire un numero di rate compreso tra 0 e 72!"
                                            errore = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    If txtNumRate2.Text = "" Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Inserire il numero di rate!"
                        errore = True
                    Else
                        If Not IsNumeric(txtNumRate2.Text) Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! Inserire valore numerico!"
                            errore = True
                        Else
                            If Not (par.IfEmpty(txtNumRate2.Text, 0) >= 0 And par.IfEmpty(txtNumRate2.Text, 0) <= 72) Then
                                lblMess.Visible = True
                                lblMess.Text = "Attenzione! Inserire un numero di rate compreso tra 0 e 72!"
                                errore = True
                            End If
                        End If
                    End If
                End If
            End If
        Else
            If txtImporto2.Text <> "" Then
                If txtNumRate2.Text <> "" Then
                    If Not IsNumeric(txtNumRate2.Text) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Inserire valore numerico!"
                        errore = True
                    Else
                        If par.IfEmpty(txtNumRate2.Text, 0) > 0 Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! Numero di rate non corretto!"
                            errore = True
                        End If
                    End If
                Else
                    txtNumRate2.Text = 0
                End If

                If Not IsNumeric(txtImporto2.Text) Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire valore numerico!"
                    errore = True
                Else
                    If par.IfEmpty(txtImporto2.Text, 0) > 0 Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! Importo non corretto!"
                        errore = True
                    End If
                End If
            Else
                txtImporto2.Text = txtSaldo2.Text
            End If
        End If

        Return errore


    End Function

    Private Function RicavaInizioValidita(ByVal causaleDom As String, ByVal annoReddito As Integer, ByVal dataEvento As String) As String
        Dim dataInizioValidita As String = ""
        Dim meseSuccessDataEvento As String = ""
        Dim dataEventoPIU1 As String = ""
        Dim annoDataPIU1 As String = ""

        Dim meseSuccessDataPres As String = ""
        Dim dataPresPIU1 As String = ""
        Dim annoDataPresPIU1 As String = ""
        'If Month(Now) = 12 Then
        '    annoDataOggi = Year(Now) + 1
        'Else
        '    annoDataOggi = Year(Now)
        'End If

        If dataEvento <> "" Then
            If dataEvento.Substring(4, 2) = 12 Then
                annoDataPIU1 = Left(dataEvento, 4) + 1
            Else
                annoDataPIU1 = Left(dataEvento, 4)
            End If

            If dataEvento.Substring(4, 2) = 12 Then
                annoDataPresPIU1 = Left(dataEvento, 4) + 1
            Else
                annoDataPresPIU1 = Left(dataEvento, 4)
            End If
        End If

        Try
            If dataEvento <> "" Then
                dataEventoPIU1 = par.FormattaData(Date.Parse(par.FormattaData(dataEvento), New System.Globalization.CultureInfo("it-IT", False)).AddMonths(1).ToString("dd/MM/yyyy"))
                meseSuccessDataEvento = Month(CDate(par.FormattaData(dataEventoPIU1)))
                If meseSuccessDataEvento < 10 Then
                    meseSuccessDataEvento = "0" & meseSuccessDataEvento
                End If

                dataPresPIU1 = par.FormattaData(Date.Parse(par.FormattaData(dataEvento), New System.Globalization.CultureInfo("it-IT", False)).AddMonths(1).ToString("dd/MM/yyyy"))
                meseSuccessDataPres = Month(CDate(par.FormattaData(dataPresPIU1)))
                If meseSuccessDataPres < 10 Then
                    meseSuccessDataPres = "0" & meseSuccessDataPres
                End If
            End If

            Select Case causaleDom
                Case 17
                    dataInizioValidita = annoReddito + 1 & "0101"
                Case 18, 23
                    If annoReddito = "2007" Then
                        dataInizioValidita = annoReddito + 1 & "0101"
                    Else
                        If annoReddito Mod 2 = 0 Then
                            dataInizioValidita = annoDataPIU1 & meseSuccessDataEvento & "01"
                        Else
                            'If par.AggiustaData(txtDataPr.Text) <= annoReddito & "1231" Then
                            dataInizioValidita = annoDataPIU1 & meseSuccessDataEvento & "01"
                            'ElseIf par.AggiustaData(txtDataPr.Text) >= annoReddito + 1 & "0101" Then
                            '    dataInizioValidita = annoReddito + 1 & "0101"
                            'End If
                        End If
                    End If
                Case 19, 27
                    'dataEventoPIU1 = par.FormattaData(Date.Parse(par.FormattaData(Format(Now, "yyyyMMdd")), New System.Globalization.CultureInfo("it-IT", False)).AddMonths(1).ToString("dd/MM/yyyy"))
                    'meseSuccessDataEvento = Month(CDate(par.FormattaData(dataEventoPIU1)))
                    'If meseSuccessDataEvento < 10 Then
                    '    meseSuccessDataEvento = "0" & meseSuccessDataEvento
                    'End If
                    'dataInizioValidita = annoDataOggi & meseSuccessDataEvento & "01"
                    dataInizioValidita = annoDataPIU1 & meseSuccessDataEvento & "01"
                Case 20
                    If annoReddito Mod 2 = 0 Then 'ANNO PARI
                        dataInizioValidita = annoReddito + 2 & "0101"
                    Else
                        dataInizioValidita = annoReddito + 1 & "0101"
                    End If
                Case 21
                    If annoReddito = "2007" Then
                        dataInizioValidita = annoReddito + 1 & "0101"
                    Else
                        'If annoReddito Mod 2 = 0 Then 'ANNO PARI
                        '    dataInizioValidita = annoReddito + 2 & "0101"
                        'Else
                        dataInizioValidita = annoDataPIU1 & meseSuccessDataEvento & "01"
                        'End If
                    End If
                Case 22
                    If annoReddito = "2006" Then
                        dataInizioValidita = annoReddito + 2 & "0101"
                    ElseIf annoReddito = "2007" Then
                        dataInizioValidita = annoReddito + 1 & "0101"
                    Else
                        dataInizioValidita = annoDataPIU1 & meseSuccessDataEvento & "01"
                    End If
                Case 28
                    dataInizioValidita = annoDataPresPIU1 & meseSuccessDataPres & "01"

                    'If annoRedd Mod 2 = 0 Then
                    '    dataInizioValidita = annoReddito + 1 & meseSuccessDataPres & "01"
                    'Else
                    '    dataInizioValidita = annoReddito + 2 & meseSuccessDataPres & "01"
                    'End If
                Case 29
                    dataInizioValidita = annoReddito + 2 & "0101"
            End Select

            '****** DATA INIZIO VALIDITA SUCCESSIVA ALLA DATA DI STIPULA DEL CONTRATTO (PER QUELLI NUOVI)
            If dataInizioValidita < par.AggiustaData(dataStipula) Then
                dataInizioValidita = par.AggiustaData(dataStipula)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return dataInizioValidita

    End Function

    Private Function RicavaFineValidita(ByVal causaleDom As String, ByVal annoReddito As Integer) As String
        Try
            Dim dataFineValidita As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            If annoReddito Mod 2 = 0 And causaleDom <> 29 Then

                par.cmd.CommandText = "SELECT dichiarazioni_vsa.*,domande_bando_vsa.pg as pgd FROM dichiarazioni_vsa,domande_bando_vsa WHERE DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=3 and domande_bando_vsa.CONTRATTO_num='" & LBLcodUI.Value & "' and DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID_STATO <> 2 AND DICHIARAZIONI_VSA.DATA_INIZIO_VAL>'" & par.AggiustaData(txtDataIn.Text) & "' ORDER BY DATA_INIZIO_VAL ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    par.cmd.CommandText = "SELECT UTENZA_dichiarazioni.*,utenza_bandi.anno_isee,utenza_bandi.descrizione as nome_bando FROM UTENZA_dichiarazioni,utenza_bandi WHERE utenza_bandi.id=utenza_dichiarazioni.id_bando and RAPPORTO='" & LBLcodUI.Value & "' and UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL>='" & par.AggiustaData(txtDataIn.Text) & "' AND UTENZA_DICHIARAZIONI.ID IN (SELECT ID_DICHIARAZIONE FROM SISCOM_MI.CANONI_EC) ORDER BY DATA_INIZIO_VAL ASC"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        If par.IfNull(myReader1("DATA_INIZIO_VAL"), "") = par.AggiustaData(txtDataIn.Text) Then
                            dataFineValidita = par.FormattaData(par.IfNull(myReader1("DATA_FINE_VAL"), ""))
                            If cmbTipo.SelectedValue = 3 Then
                                lblMess0.Visible = True
                                lblMess0.Text = "La data di fine validità è stata impostata a " & dataFineValidita & " perchè è stata trovata una domanda " & myReader1("NOME_BANDO") & " (redditi " & myReader1("ANNO_ISEE") & ") con pari inizio validità (" & par.FormattaData(myReader1("data_inizio_val")) & ")"
                            End If
                        Else
                            dataFineValidita = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")

                            If cmbTipo.SelectedValue = 3 Then
                                lblMess0.Visible = True
                                lblMess0.Text = "La data di fine validità è stata impostata a " & dataFineValidita & " perchè è stata trovata una domanda " & myReader1("NOME_BANDO") & " (redditi " & myReader1("ANNO_ISEE") & ") con validità successiva (" & par.FormattaData(myReader1("data_inizio_val")) & " - " & par.FormattaData(myReader1("data_fine_val")) & ")"
                            End If
                        End If
                    Else
                        If par.IfNull(myReader("DATA_INIZIO_VAL"), "") = par.AggiustaData(txtDataIn.Text) Then
                            dataFineValidita = par.FormattaData(par.IfNull(myReader("DATA_FINE_VAL"), ""))
                            If cmbTipo.SelectedValue = 3 Then
                                lblMess0.Visible = True
                                lblMess0.Text = "La data di fine validità è stata impostata a " & dataFineValidita & " perchè è stata trovata una domanda " & myReader1("NOME_BANDO") & " (redditi " & myReader1("ANNO_ISEE") & ") con pari inizio validità (" & par.FormattaData(myReader1("data_inizio_val")) & ")"
                            End If
                        Else
                            dataFineValidita = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")

                            If cmbTipo.SelectedValue = 3 Then
                                lblMess0.Visible = True
                                lblMess0.Text = "La data di fine validità è stata impostata a " & par.FormattaData(dataFineValidita) & " perchè è stata trovata una domanda di revisione canone (PG " & myReader("pgd") & ") con validità successiva (" & par.FormattaData(myReader("data_inizio_val")) & " - " & par.FormattaData(myReader("data_fine_val")) & ")"
                            End If
                        End If
                    End If
                    myReader1.Close()
                Else
                    par.cmd.CommandText = "SELECT UTENZA_dichiarazioni.*,utenza_bandi.anno_isee,utenza_bandi.descrizione as nome_bando FROM UTENZA_dichiarazioni,utenza_bandi WHERE utenza_bandi.id=utenza_dichiarazioni.id_bando and RAPPORTO='" & LBLcodUI.Value & "' and UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL>='" & par.AggiustaData(txtDataIn.Text) & "' AND UTENZA_DICHIARAZIONI.ID IN (SELECT ID_DICHIARAZIONE FROM SISCOM_MI.CANONI_EC) ORDER BY DATA_INIZIO_VAL ASC"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        If par.IfNull(myReader1("DATA_INIZIO_VAL"), "") = par.AggiustaData(txtDataIn.Text) Then
                            dataFineValidita = par.FormattaData(par.IfNull(myReader1("DATA_FINE_VAL"), ""))
                            If cmbTipo.SelectedValue = 3 Then
                                lblMess0.Visible = True
                                lblMess0.Text = "La data di fine validità è stata impostata a " & dataFineValidita & " perchè è stata trovata una domanda " & myReader1("NOME_BANDO") & " (redditi " & myReader1("ANNO_ISEE") & ") con pari inizio validità (" & par.FormattaData(myReader1("data_inizio_val")) & ")"
                            End If
                        Else
                            dataFineValidita = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")

                            If cmbTipo.SelectedValue = 3 Then
                                lblMess0.Visible = True
                                lblMess0.Text = "La data di fine validità è stata impostata a " & dataFineValidita & " perchè è stata trovata una domanda " & myReader1("NOME_BANDO") & " (redditi " & myReader1("ANNO_ISEE") & ") con validità successiva (" & par.FormattaData(myReader1("data_inizio_val")) & " - " & par.FormattaData(myReader1("data_fine_val")) & ")"
                            End If
                        End If
                    Else
                        lblMess0.Visible = False
                        lblMess0.Text = ""
                        '******** 19/03/2013 NUOVA IMPLEMENTAZIONE PER DATE FINE VALIDITA ********
                        Select Case causaleDom
                            Case "29"
                                dataFineValidita = annoReddito + 3 & "1231"
                            Case "28"
                                If annoReddito Mod 2 = 0 Then
                                    dataFineValidita = annoReddito + 3 & "1231"
                                Else
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                            Case "17"
                                If annoReddito Mod 2 = 0 Then
                                    dataFineValidita = annoReddito + 1 & "1231"
                                Else
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                            Case "18", "23"
                                If annoReddito = "2007" Then
                                    dataFineValidita = annoReddito + 2 & "1231"
                                Else
                                    If annoReddito Mod 2 = 0 Then
                                        dataFineValidita = annoReddito + 1 & "1231"
                                        If par.AggiustaData(txtDataIn.Text) >= annoReddito + 1 & "1231" Then
                                            dataFineValidita = annoReddito + 2 & "1231"
                                        End If
                                    Else
                                        If par.AggiustaData(txtDataIn.Text) >= annoReddito & "0201" And par.AggiustaData(txtDataIn.Text) <= annoReddito & "1201" Then
                                            dataFineValidita = annoReddito & "1231"
                                        ElseIf par.AggiustaData(txtDataIn.Text) >= annoReddito + 1 & "0101" Then
                                            dataFineValidita = annoReddito + 2 & "1231"
                                        End If
                                    End If
                                End If
                            Case "19"
                                If annoReddito Mod 2 = 0 Then
                                    dataFineValidita = annoReddito + 1 & "1231"
                                Else
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                            Case "20"
                                If annoReddito Mod 2 = 0 Then
                                    dataFineValidita = annoReddito + 3 & "1231"
                                Else
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                            Case "21"
                                If annoReddito Mod 2 = 0 Then
                                    dataFineValidita = Mid(txtDataIn.Text, 7) & "1231"
                                Else
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                            Case "22"
                                If annoReddito Mod 2 = 0 Then
                                    dataFineValidita = annoReddito + 3 & "1231"
                                Else
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                        End Select
                        '******** 19/03/2013 NUOVA IMPLEMENTAZIONE PER DATE FINE VALIDITA ********



                        ''If causaleDom = 29 Then
                        ''    Select Case cmbAU.SelectedValue
                        ''        Case 1
                        ''            dataFineValidita = "20091231"
                        ''        Case 2
                        ''            dataFineValidita = "20121231"
                        ''        Case 3
                        ''            dataFineValidita = "20131231"
                        ''        Case Else
                        ''            dataFineValidita = "20121231"
                        ''    End Select
                        ''Else
                        ''    'dataFineValidita = Year(Now) & "1231"
                        ''    dataFineValidita = "20091231"
                        ''End If
                    End If
                    myReader1.Close()
                End If
                myReader.Close()

                'CONTROLLO SE CI SONO DOMANDE DI ANAGR. UTENZA SOSPESE 
                If cmbTipo.SelectedValue = 3 Then
                    If causaleDom <> 28 And causaleDom <> 29 Then
                        par.cmd.CommandText = "SELECT UTENZA_dichiarazioni.* FROM UTENZA_dichiarazioni WHERE RAPPORTO='" & LBLcodUI.Value & "' and UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL<='" & par.AggiustaData(txtDataIn.Text) & "' AND DATA_FINE_VAL >= '" & par.AggiustaData(txtDataIn.Text) & "' AND UTENZA_DICHIARAZIONI.ID not IN (SELECT ID_DICHIARAZIONE FROM SISCOM_MI.CANONI_EC) ORDER BY DATA_INIZIO_VAL ASC"
                        Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader11.Read Then
                            If par.IfNull(myReader11("ID_STATO"), "0") = "0" Then
                                If par.IfNull(myReader11("fl_sosp_7"), "0") = "1" And par.IfNull(myReader11("fl_sosp_1"), "0") = "0" And par.IfNull(myReader11("fl_sosp_2"), "0") = "0" And par.IfNull(myReader11("fl_sosp_3"), "0") = "0" And par.IfNull(myReader11("fl_sosp_4"), "0") = "0" And par.IfNull(myReader11("fl_sosp_5"), "0") = "0" And par.IfNull(myReader11("fl_sosp_6"), "0") = "0" Then
                                    DAFARE = True
                                Else
                                    If par.IfNull(myReader11("fl_sosp_7"), "0") = "0" And par.IfNull(myReader11("fl_sosp_1"), "0") = "0" And par.IfNull(myReader11("fl_sosp_2"), "0") = "0" And par.IfNull(myReader11("fl_sosp_3"), "0") = "0" And par.IfNull(myReader11("fl_sosp_4"), "0") = "0" And par.IfNull(myReader11("fl_sosp_5"), "0") = "0" And par.IfNull(myReader11("fl_sosp_6"), "0") = "0" Then
                                        DAFARE = True
                                    Else
                                        DAFARE = False
                                    End If
                                End If
                            End If
                        End If
                        myReader11.Close()

                        If DAFARE = False Then
                            lblMess0.Visible = True
                            lblMess0.Text = "E' stata trovata una dichiarazione AU sospesa. E' necessario applicare il canone prima di effettuare una revisione canone!"
                        End If
                    End If
                End If
            Else
                lblMess0.Visible = False
                lblMess0.Text = ""

                '******** 19/03/2013 NUOVA IMPLEMENTAZIONE PER DATE FINE VALIDITA ********
                Select Case causaleDom
                    Case "29"
                        dataFineValidita = annoReddito + 3 & "1231"
                    Case "28"
                        If annoReddito Mod 2 = 0 Then
                            dataFineValidita = annoReddito + 3 & "1231"
                        Else
                            dataFineValidita = annoReddito + 2 & "1231"
                        End If
                    Case "17"
                        If annoReddito Mod 2 = 0 Then
                            dataFineValidita = annoReddito + 1 & "1231"
                        Else
                            dataFineValidita = annoReddito + 2 & "1231"
                        End If
                    Case "18", "23"
                        If annoReddito = "2007" Then
                            dataFineValidita = annoReddito + 2 & "1231"
                        Else
                            If annoReddito Mod 2 = 0 Then
                                dataFineValidita = annoReddito + 1 & "1231"
                            Else
                                If par.AggiustaData(txtDataIn.Text) >= annoReddito & "0201" And par.AggiustaData(txtDataIn.Text) <= annoReddito & "1201" Then
                                    dataFineValidita = annoReddito & "1231"
                                ElseIf par.AggiustaData(txtDataIn.Text) >= annoReddito + 1 & "0101" Then
                                    dataFineValidita = annoReddito + 2 & "1231"
                                End If
                            End If
                        End If
                    Case "19"
                        If annoReddito Mod 2 = 0 Then
                            dataFineValidita = annoReddito + 3 & "1231"
                        Else
                            dataFineValidita = annoReddito + 2 & "1231"
                        End If
                    Case "20"
                        If annoReddito Mod 2 = 0 Then
                            dataFineValidita = annoReddito + 3 & "1231"
                        Else
                            dataFineValidita = annoReddito + 2 & "1231"
                        End If
                    Case "21"
                        If annoReddito Mod 2 = 0 Then
                            dataFineValidita = annoReddito + 3 & "1231"
                        Else
                            dataFineValidita = annoReddito + 2 & "1231"
                        End If
                    Case "22"
                        If annoReddito Mod 2 = 0 Then
                            dataFineValidita = annoReddito + 3 & "1231"
                        Else
                            dataFineValidita = annoReddito + 2 & "1231"
                        End If
                End Select

                '******** 19/03/2013 fine NUOVA IMPLEMENTAZIONE PER DATE FINE VALIDITA ********


                ''dataFineValidita = Year(Now) & "1231"
                ''If causaleDom = 29 Then
                ''    Select Case cmbAU.SelectedValue
                ''        Case 1
                ''            dataFineValidita = "20091231"
                ''        Case 2
                ''            dataFineValidita = "20121231"
                ''        Case 3
                ''            dataFineValidita = "20131231"
                ''        Case Else
                ''            dataFineValidita = "20121231"
                ''    End Select
                ''End If
                If cmbTipo.SelectedValue = 3 Then
                    If causaleDom <> 28 And causaleDom <> 29 Then
                        par.cmd.CommandText = "SELECT UTENZA_dichiarazioni.* FROM UTENZA_dichiarazioni WHERE RAPPORTO='" & LBLcodUI.Value & "' and UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL<='" & par.AggiustaData(txtDataIn.Text) & "' AND DATA_FINE_VAL >= '" & par.AggiustaData(txtDataIn.Text) & "' AND UTENZA_DICHIARAZIONI.ID not IN (SELECT ID_DICHIARAZIONE FROM SISCOM_MI.CANONI_EC) ORDER BY DATA_INIZIO_VAL ASC"
                        Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader11.Read Then
                            If par.IfNull(myReader11("ID_STATO"), "0") = "0" Then
                                If par.IfNull(myReader11("fl_sosp_7"), "0") = "1" And par.IfNull(myReader11("fl_sosp_1"), "0") = "0" And par.IfNull(myReader11("fl_sosp_2"), "0") = "0" And par.IfNull(myReader11("fl_sosp_3"), "0") = "0" And par.IfNull(myReader11("fl_sosp_4"), "0") = "0" And par.IfNull(myReader11("fl_sosp_5"), "0") = "0" And par.IfNull(myReader11("fl_sosp_6"), "0") = "0" Then
                                    DAFARE = True
                                Else
                                    If par.IfNull(myReader11("fl_sosp_7"), "0") = "0" And par.IfNull(myReader11("fl_sosp_1"), "0") = "0" And par.IfNull(myReader11("fl_sosp_2"), "0") = "0" And par.IfNull(myReader11("fl_sosp_3"), "0") = "0" And par.IfNull(myReader11("fl_sosp_4"), "0") = "0" And par.IfNull(myReader11("fl_sosp_5"), "0") = "0" And par.IfNull(myReader11("fl_sosp_6"), "0") = "0" Then
                                        DAFARE = True
                                    Else
                                        DAFARE = False
                                    End If
                                End If
                            End If
                        End If
                        myReader11.Close()

                        If DAFARE = False Then
                            lblMess0.Visible = True
                            lblMess0.Text = "E' stata trovata una dichiarazione AU sospesa. E' necessario applicare il canone prima di effettuare una revisione canone!"
                        End If
                    End If
                End If
            End If

            Dim dataFineCanone As String = ""
            par.cmd.CommandText = "select fine_canone from utenza_bandi where id in (select max(id) from utenza_bandi where stato=2)"
            dataFineCanone = par.IfNull(par.cmd.ExecuteScalar, "")

            If dataFineValidita > dataFineCanone And dataFineCanone > par.AggiustaData(txtDataIn.Text) Then
                dataFineValidita = dataFineCanone
            End If

            'par.cmd.CommandText = "SELECT dichiarazioni_vsa.*,domande_bando_vsa.pg as pgd FROM dichiarazioni_vsa,domande_bando_vsa WHERE DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=3 and domande_bando_vsa.CONTRATTO_num='" & LBLcodUI.Value & "' and DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.DATA_INIZIO_VAL>'" & par.AggiustaData(txtDataIn.Text) & "' ORDER BY DATA_INIZIO_VAL ASC"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.HasRows = False Then
            '    par.cmd.CommandText = "SELECT UTENZA_dichiarazioni.*,utenza_bandi.anno_isee,utenza_bandi.descrizione as nome_bando FROM UTENZA_dichiarazioni,utenza_bandi WHERE utenza_bandi.id=utenza_dichiarazioni.id_bando and RAPPORTO='" & LBLcodUI.Value & "' and UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL>'" & par.AggiustaData(txtDataIn.Text) & "' ORDER BY DATA_INIZIO_VAL ASC"
            '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader1.HasRows = False Then
            '        dataFineValidita = Year(Now) & "1231"
            '    Else
            '        If myReader1.Read Then
            '            dataFineValidita = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")
            '            If cmbTipo.SelectedValue = 3 Then
            '                lblMess0.Visible = True
            '                lblMess0.Text = "La data di fine validità è stata impostata a " & dataFineValidita & " perchè è stata trovata una domanda " & myReader1("NOME_BANDO") & " (redditi " & myReader1("ANNO_ISEE") & ") con validità successiva (" & par.FormattaData(myReader1("data_inizio_val")) & " - " & par.FormattaData(myReader1("data_fine_val")) & ")"
            '            End If
            '        End If

            '    End If
            '    myReader1.Close()
            'Else
            '    If myReader.Read Then
            '        dataFineValidita = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")
            '        If cmbTipo.SelectedValue = 3 Then
            '            lblMess0.Visible = True
            '            lblMess0.Text = "La data di fine validità è stata impostata a " & par.FormattaData(dataFineValidita) & " perchè è stata trovata una domanda di revisione canone (PG " & myReader("pgd") & ") con validità successiva (" & par.FormattaData(myReader("data_inizio_val")) & " - " & par.FormattaData(myReader("data_fine_val")) & ")"
            '        End If
            '    End If
            'End If
            'myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Return dataFineValidita

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        'Dim dataFineValidita As String = ""
        'Try
        '    Select Case causaleDom
        '        Case 17
        '            If annoReddito Mod 2 = 0 Then 'ANNO PARI
        '                dataFineValidita = annoReddito + 1 & "1231"
        '            Else
        '                dataFineValidita = annoReddito + 2 & "1231"
        '            End If
        '        Case 18, 23
        '            If annoReddito = "2007" Then
        '                dataFineValidita = annoReddito + 2 & "1231"
        '            Else
        '                If annoReddito Mod 2 = 0 Then
        '                    dataFineValidita = annoReddito + 1 & "1231"

        '                Else
        '                    If par.AggiustaData(txtDataPr.Text) <= annoReddito & "1231" Then
        '                        dataFineValidita = annoReddito & "1231"
        '                    ElseIf par.AggiustaData(txtDataPr.Text) >= annoReddito + 1 & "0101" Then
        '                        dataFineValidita = annoReddito + 2 & "1231"
        '                    End If
        '                End If
        '            End If
        '        Case 19, 20
        '            If annoReddito Mod 2 = 0 Then
        '                dataFineValidita = annoReddito + 3 & "1231"
        '            Else
        '                dataFineValidita = annoReddito + 2 & "1231"
        '            End If
        '        Case 21
        '            If annoReddito = "2007" Then
        '                dataFineValidita = annoReddito + 2 & "1231"
        '            Else
        '                If annoReddito Mod 2 = 0 Then
        '                    dataFineValidita = annoReddito + 1 & "1231"
        '                Else
        '                    dataFineValidita = annoReddito + 2 & "1231"
        '                End If
        '            End If
        '        Case 22
        '            If annoReddito = "2006" Then
        '                dataFineValidita = annoReddito + 3 & "1231"
        '            ElseIf annoReddito = "2007" Then
        '                dataFineValidita = annoReddito + 2 & "1231"
        '            Else
        '                If annoReddito Mod 2 = 0 Then
        '                    dataFineValidita = annoReddito + 3 & "1231"
        '                Else
        '                    dataFineValidita = annoReddito + 2 & "1231"
        '                End If
        '            End If
        '    End Select

        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try

    End Function

    Protected Sub AggiornaComponenti()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            cmbComponenti.Items.Clear()

            If cmbIntestRU.Visible = True Then
                cmbIntestRU.Items.Clear()
            End If
            If cmbTipo.SelectedValue > 1 Then
                par.cmd.CommandText = "SELECT ANAGRAFICA.ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,COD_CONTRATTO,DATA_INIZIO,DATA_FINE," _
                & "TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE FROM SISCOM_MI.ANAGRAFICA," _
                & "SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA " _
                & "WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & LBLid.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND TIPOLOGIA_PARENTELA.COD = COD_TIPOLOGIA_PARENTELA"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    'If cmbTipo.SelectedValue = "1" Then
                    'cmbComponenti.Items.Add(New ListItem(myReader("NOMINATIVO"), myReader("ID")).Enabled = False)
                    'Else
                    cmbComponenti.Items.Add(New ListItem(par.IfNull(myReader("NOMINATIVO"), "") & " - " & par.IfNull(myReader("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_FINE"), "")), myReader("ID")))
                    'End If
                    LBLcodContr.Value = myReader("COD_CONTRATTO")
                    If cmbIntestRU.Visible = True Then
                        cmbIntestRU.Items.Add(New ListItem(par.IfNull(myReader("NOMINATIVO"), "") & " - " & par.IfNull(myReader("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_FINE"), "")), myReader("ID")))
                    End If
                End If
                myReader.Close()
            End If

            par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA,DATA_INIZIO,DATA_FINE,TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.TIPOLOGIA_PARENTELA WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & LBLid.Value & " AND TIPOLOGIA_PARENTELA.COD = COD_TIPOLOGIA_PARENTELA AND COD_TIPOLOGIA_OCCUPANTE<>'INTE' AND NVL(SOGGETTI_CONTRATTUALI.DATA_INIZIO,'29991231')<'" & par.AggiustaData(txtDataPr.Text) & "' AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' ORDER BY NOMINATIVO ASC"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read
                If par.RicavaEta(par.IfNull(myReader2("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) >= 18 Then
                    cmbComponenti.Items.Add(New ListItem(par.IfNull(myReader2("NOMINATIVO"), "") & " - " & par.IfNull(myReader2("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader2("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader2("DATA_FINE"), "")), myReader2("ID")))
                    If cmbIntestRU.Visible = True Then
                        cmbIntestRU.Items.Add(New ListItem(par.IfNull(myReader2("NOMINATIVO"), "") & " - " & par.IfNull(myReader2("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader2("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader2("DATA_FINE"), "")), myReader2("ID")))
                    End If
                End If
            End While
            myReader2.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub caricaComponenti()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT ANAGRAFICA.ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,COD_CONTRATTO,DATA_INIZIO,DATA_FINE," _
                & "TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE FROM SISCOM_MI.ANAGRAFICA," _
                & "SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_PARENTELA " _
                & "WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND " _
                & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & LBLid.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND TIPOLOGIA_PARENTELA.COD = COD_TIPOLOGIA_PARENTELA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                'If cmbTipo.SelectedValue = "1" Then
                'cmbComponenti.Items.Add(New ListItem(myReader("NOMINATIVO"), myReader("ID")).Enabled = False)
                'Else
                cmbComponenti.Items.Add(New ListItem(par.IfNull(myReader("NOMINATIVO"), "") & " - " & par.IfNull(myReader("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_FINE"), "")), myReader("ID")))
                'End If
                If cmbIntestRU.Visible = True Then
                    cmbIntestRU.Items.Add(New ListItem(par.IfNull(myReader("NOMINATIVO"), "") & " - " & par.IfNull(myReader("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_FINE"), "")), myReader("ID")))
                End If
                LBLcodContr.Value = myReader("COD_CONTRATTO")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA,DATA_INIZIO,DATA_FINE,TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.TIPOLOGIA_PARENTELA WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & LBLid.Value & " AND TIPOLOGIA_PARENTELA.COD = COD_TIPOLOGIA_PARENTELA AND COD_TIPOLOGIA_OCCUPANTE<>'INTE' AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "' ORDER BY NOMINATIVO ASC"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                If par.RicavaEta(par.IfNull(myReader("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) >= 18 Then
                    cmbComponenti.Items.Add(New ListItem(par.IfNull(myReader("NOMINATIVO"), "") & " - " & par.IfNull(myReader("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_FINE"), "")), myReader("ID")))
                    If cmbIntestRU.Visible = True Then
                        cmbIntestRU.Items.Add(New ListItem(par.IfNull(myReader("NOMINATIVO"), "") & " - " & par.IfNull(myReader("PARENTE"), "") & " - " & par.FormattaData(par.IfNull(myReader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_FINE"), "")), myReader("ID")))
                    End If
                End If
            End While
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged

        Try
            Dim item As ListItem
            Dim stringaSQL As String = ""
            Dim motivoEscl As String = ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            item = cmbTipo.SelectedItem

            lblAltro.Visible = False
            txtAltro.Visible = False
            cmbSindacato.Visible = False
            lblSindac.Visible = False
            lblDom.Visible = False
            cmbComponenti.Visible = False
            txtDataIn.Text = ""
            txtDataFine.Text = ""
            txtDataFine.Visible = False
            txtDataIn.Visible = False
            lblInizio.Visible = False
            lblFine.Visible = False
            lblMess0.Text = ""
            lblMess.Text = ""
            AUpresente = True

            If contrProroga = True Then
                stringaSQL = "select * from t_causali_domanda_vsa where id_motivo=" & item.Value & " and cod<>4 and cod = 27 order by descrizione asc"
            Else
                stringaSQL = "select * from t_causali_domanda_vsa where id_motivo=" & item.Value & " and cod<>4 and cod <> 27 order by descrizione asc"
            End If


            par.OracleConn.Open()
            par.SettaCommand(par)

            btnProcedi.Visible = True
            Select Case item.Value
                Case "-1"
                    cmbMotivo.Items.Clear()
                    cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                    cmbMotivo.Visible = False
                    lblMotivo.Visible = False
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblDataPr.Text = "Data di presentazione"
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False
                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(2)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(2))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(3)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(3))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(4)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(4))
                    ''End If
                    ''cmbModRichiesta.Visible = False
                    ''lblModR.Visible = False
                    ''If cmbModRichiesta.Items.Count = 0 Then
                    ''    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                    ''    cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                    ''End If


                    'btnProcedi.Visible = True
                    cmbComponenti.Items.Item(0).Enabled = True
                    lblDom.Text = "Intestatario domanda"
                    lblDescrizione.Visible = False
                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lbl1.Visible = False
                    lblSaldo.Visible = False
                    txtSaldo.Visible = False
                    lblImporto.Visible = False
                    txtImporto.Visible = False
                    lblNumRate.Visible = False
                    txtNumRate.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False


                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    txtAnnoReddito.Text = ""
                Case "0" 'Voltura
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(2)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(2))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(3)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(3))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(4)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(4))
                    ''End If
                    'cmbModRichiesta.Visible = True
                    'lblModR.Visible = True
                    'If cmbModRichiesta.Items.Count = 0 Then
                    '    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                    '    cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                    'End If
                    'cmbMotivo.Visible = True
                    'lblMotivo.Visible = True
                    'lblDataPr.Visible = False
                    'txtDataPr.Visible = False
                    'lblDataEvento.Visible = False
                    'txtDataEvento.Visible = False
                    'lblanno.Visible = False
                    'cmbAnniRedd.Visible = False
                    'lblVerificaAnno.Visible = False
                    'txtAnnoReddito.Visible = False
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    cmbMotivo.Visible = True
                    lblMotivo.Visible = True
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False
                    cmbMotivo.Items.Clear()
                    'par.RiempiDList(Me.Page, par.OracleConn, "cmbMotivo", "select * from t_causali_domanda_vsa where id_motivo=" & item.Value & " order by descrizione asc", "DESCRIZIONE", "COD")
                    par.cmd.CommandText = stringaSQL
                    myReader = par.cmd.ExecuteReader()
                    cmbMotivo.Items.Add(New ListItem(" - seleziona - ", -1))
                    While myReader.Read
                        cmbMotivo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("COD")))
                    End While
                    myReader.Close()
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'btnProcedi.Visible = True
                    cmbComponenti.Items.Item(0).Enabled = False
                    lblDom.Text = "Richiedente"
                    If cmbComponenti.Items.Count = 1 Then
                        lblMess.Visible = True
                        lblMess.Text = "Impossibile procedere! Nucleo familiare composto da un solo membro."
                        cmbMotivo.Visible = False
                        lblMotivo.Visible = False
                        lblDataPr.Visible = False
                        txtDataPr.Visible = False
                        lblDataEvento.Visible = False
                        txtDataEvento.Visible = False
                        lblanno.Visible = False
                        cmbAnniRedd.Visible = False

                        lblVerificaAnno.Visible = False
                        txtAnnoReddito.Visible = False

                    End If
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>CAMBIO INTESTAZIONE</b>: regola il Cambio Intestazione (voltura) del contratto quando l'avente titolo al subentro non era presente al momento della stipula del contratto (assegnazione e stipula)."

                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    lbl1.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False


                Case "1" 'Subentro
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(2)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(2))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(3)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(3))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(4)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(4))
                    ''End If
                    ''cmbModRichiesta.Visible = True
                    ''lblModR.Visible = True
                    ''If cmbModRichiesta.Items.Count = 0 Then
                    ''    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                    ''    cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                    ''End If
                    'cmbMotivo.Visible = True
                    'lblMotivo.Visible = True
                    'lblDataPr.Visible = False
                    'txtDataPr.Visible = False
                    'lblDataEvento.Visible = False
                    'txtDataEvento.Visible = False

                    'lblanno.Visible = False
                    'cmbAnniRedd.Visible = False
                    'lblVerificaAnno.Visible = False
                    'txtAnnoReddito.Visible = False

                    'cmbMotivo.Items.Clear()
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)
                    cmbMotivo.Visible = True
                    lblMotivo.Visible = True
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False
                    cmbMotivo.Items.Clear()

                    'par.RiempiDList(Me.Page, par.OracleConn, "cmbMotivo", "select * from t_causali_domanda_vsa where id_motivo=" & item.Value & " order by descrizione asc", "DESCRIZIONE", "COD")
                    par.cmd.CommandText = stringaSQL
                    myReader = par.cmd.ExecuteReader()
                    cmbMotivo.Items.Add(New ListItem(" - seleziona - ", -1))
                    While myReader.Read
                        cmbMotivo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("COD")))
                    End While
                    myReader.Close()
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'btnProcedi.Visible = True
                    cmbComponenti.Items.Item(0).Enabled = False
                    lblDom.Text = "Intestatario domanda/contratto"
                    If cmbComponenti.Items.Count = 1 Then
                        lblMess.Visible = True
                        lblMess.Text = "Impossibile procedere! Nucleo familiare composto da un solo membro."
                        cmbMotivo.Visible = False
                        lblMotivo.Visible = False
                        lblDataPr.Visible = False
                        txtDataPr.Visible = False
                        lblDataEvento.Visible = False
                        txtDataEvento.Visible = False
                        lblanno.Visible = False
                        cmbAnniRedd.Visible = False
                        lblVerificaAnno.Visible = False
                        txtAnnoReddito.Visible = False
                        cmbModRichiesta.Visible = False
                        lblModR.Visible = False
                        ' btnProcedi.Visible = False
                    End If
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>VARIAZIONE INTESTAZIONE</b>: regola il subentro nell'intestazione del contratto di locazione da parte di persona o persone già facenti parte del nucleo familiare originario - compresi coniuge e figli (matrimonio o nascita)."

                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lbl1.Visible = False
                    lblSaldo.Visible = False
                    txtSaldo.Visible = False
                    lblImporto.Visible = False
                    txtImporto.Visible = False
                    lblNumRate.Visible = False
                    txtNumRate.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False

                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                Case "2" 'Ampliamento
                    cmbMotivo.Visible = True
                    lblMotivo.Visible = True
                    cmbModRichiesta.Visible = False
                    lblModR.Visible = False
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblanno.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    cmbAnniRedd.Visible = False
                    cmbMotivo.Items.Clear()
                    'par.RiempiDList(Me.Page, par.OracleConn, "cmbMotivo", "select * from t_causali_domanda_vsa where id_motivo=" & item.Value & " order by descrizione asc", "DESCRIZIONE", "COD")
                    par.cmd.CommandText = stringaSQL
                    myReader = par.cmd.ExecuteReader()
                    cmbMotivo.Items.Add(New ListItem(" - seleziona - ", -1))
                    While myReader.Read
                        cmbMotivo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("COD")))
                    End While
                    myReader.Close()
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'btnProcedi.Visible = True
                    cmbComponenti.Items.Item(0).Enabled = True
                    lblDom.Text = "Intestatario domanda"
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>AMPLIAMENTO</b>: regola l'ampliamento del nucleo familiare per persone legate da vincoli di affinità o meno."

                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lbl1.Visible = False
                    lblSaldo.Visible = False
                    txtSaldo.Visible = False
                    lblImporto.Visible = False
                    txtImporto.Visible = False
                    lblNumRate.Visible = False
                    txtNumRate.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False


                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                Case "3" 'Riduzione Canone
                    cmbMotivo.Visible = True
                    lblMotivo.Visible = True
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False

                    cmbMotivo.Items.Clear()
                    par.cmd.CommandText = stringaSQL
                    myReader = par.cmd.ExecuteReader()
                    cmbMotivo.Items.Add(New ListItem(" - seleziona - ", -1))
                    While myReader.Read
                        cmbMotivo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("COD")))
                    End While
                    myReader.Close()
                    ''cmbModRichiesta.Visible = True
                    ''lblModR.Visible = True
                    ''If cmbModRichiesta.Items.Count = 0 Then
                    ''    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                    ''    cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(2)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(2))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(3)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(3))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(4)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(4))
                    ''End If
                    ''cmbModRichiesta.Items.Add(New ListItem("verifica d'ufficio", 2))
                    ''cmbModRichiesta.Items.Add(New ListItem("sindacati", 3))
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    ' btnProcedi.Visible = True
                    cmbComponenti.Items.Item(0).Enabled = True
                    lblDom.Text = "Intestatario domanda"
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>RIDUZIONE CANONE</b>: regola la trattazione delle Richieste di Revisione Canoni degli Utenti avverso l'attribuzione di un'area e una classe di canone per intervenute variazioni anagrafiche e/o reddituali nel nucleo familiare"

                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lbl1.Visible = False
                    lblSaldo.Visible = False
                    txtSaldo.Visible = False
                    lblImporto.Visible = False
                    txtImporto.Visible = False
                    lblNumRate.Visible = False
                    txtNumRate.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False


                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    If contrProroga = True Then
                        cmbAnniRedd.Visible = False
                    End If

                Case "5" 'Cambio Diretto
                    cmbMotivo.Items.Clear()
                    cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                    cmbMotivo.SelectedValue = -1
                    cmbMotivo.Visible = False
                    lblMotivo.Visible = False
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblModR.Visible = True
                    cmbModRichiesta.Visible = True
                    lblInizio.Visible = True
                    txtDataIn.Visible = True
                    lblFine.Visible = True
                    txtDataFine.Visible = True

                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False

                    txtDataPr.Text = ""
                    lblDataPr.Text = "Data di presentazione"
                    lblDataPr.Visible = True
                    txtDataPr.Visible = True
                    txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                    txtDataPr.Focus()

                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(2)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(2))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(3)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(3))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(4)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(4))
                    ''End If
                    ' ''cmbModRichiesta.Visible = True
                    ' ''lblModR.Visible = True
                    ''If cmbModRichiesta.Items.Count = 0 Then
                    ''    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                    ''    cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                    ''End If
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    ' btnProcedi.Visible = True

                    cmbComponenti.Items.Item(0).Enabled = True

                    'If cmbComponenti.Items.Count > 1 Then
                    '    For i As Integer = 1 To cmbComponenti.Items.Count
                    '        cmbComponenti.Items.Remove(cmbComponenti.Items.Item(i))
                    '    Next
                    'End If

                    lblDom.Text = "Intestatario domanda/contratto"
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>CAMBIO CONSENSUALE</b>: regola il Cambio Consensuale di alloggio tra Assegnatari di ERP del patrimonio in gestione."

                    lblAltro.Visible = False
                    txtAltro.Visible = False

                Case "6"
                    cmbMotivo.Visible = True
                    lblMotivo.Visible = True
                    'cmbModRichiesta.Visible = False
                    'lblModR.Visible = False
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False

                    cmbMotivo.Items.Clear()
                    par.cmd.CommandText = stringaSQL
                    myReader = par.cmd.ExecuteReader()
                    cmbMotivo.Items.Add(New ListItem(" - seleziona - ", -1))
                    While myReader.Read
                        cmbMotivo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("COD")))
                    End While
                    myReader.Close()
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'cmbComponenti.Items.Item(0).Enabled = False
                    lblDom.Text = "Intestatario domanda/contratto"
                    'If cmbComponenti.Items.Count = 1 Then
                    '    lblMess.Visible = True
                    '    lblMess.Text = "Impossibile procedere! Nucleo familiare composto da un solo membro."
                    '    cmbMotivo.Visible = False
                    '    lblMotivo.Visible = False
                    '    lblDataPr.Visible = False
                    '    txtDataPr.Visible = False
                    '    lblDataEvento.Visible = False
                    '    txtDataEvento.Visible = False
                    '    lblanno.Visible = False
                    '    cmbAnniRedd.Visible = False

                    '    lblVerificaAnno.Visible = False
                    '    txtAnnoReddito.Visible = False
                    'End If
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>SUBENTRO PER FORZE DELL'ORDINE</b>: regola il subentro nell'intestazione del contratto di locazione, appartenente alle Forze dell'Ordine, da parte di persona o persone già facenti parte del nucleo familiare originario - compresi coniuge e figli (matrimonio o nascita)."

                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lbl1.Visible = False
                    lblSaldo.Visible = False
                    txtSaldo.Visible = False
                    lblImporto.Visible = False
                    txtImporto.Visible = False
                    lblNumRate.Visible = False
                    txtNumRate.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False

                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                Case "7"
                    cmbMotivo.Visible = True
                    lblMotivo.Visible = True
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False
                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False

                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(2)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(2))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(3)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(3))
                    ''End If
                    ''If Not IsNothing(cmbModRichiesta.Items.FindByValue(4)) Then
                    ''    cmbModRichiesta.Items.Remove(cmbModRichiesta.Items.FindByValue(4))
                    ''End If
                    ''If cmbModRichiesta.Items.Count = 0 Then
                    ''    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                    ''    cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                    ''End If
                    'cmbModRichiesta.Visible = True
                    'lblModR.Visible = True
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)
                    cmbMotivo.Items.Clear()
                    'par.RiempiDList(Me.Page, par.OracleConn, "cmbMotivo", "select * from t_causali_domanda_vsa where id_motivo=" & item.Value & " order by descrizione asc", "DESCRIZIONE", "COD")
                    par.cmd.CommandText = stringaSQL
                    myReader = par.cmd.ExecuteReader()
                    cmbMotivo.Items.Add(New ListItem(" - seleziona - ", -1))
                    While myReader.Read
                        cmbMotivo.Items.Add(New ListItem(myReader("DESCRIZIONE"), myReader("COD")))
                    End While
                    myReader.Close()
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    '   btnProcedi.Visible = True
                    cmbComponenti.Items.Item(0).Enabled = True
                    lblDom.Text = "Intestatario domanda"
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>OSPITALITA'</b>: regola la concessione dell'autorizzazione all'ospitalità temporanea di persone non appartenenti al nucleo familiare originario."

                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lbl1.Visible = False
                    lblSaldo.Visible = False
                    txtSaldo.Visible = False
                    lblImporto.Visible = False
                    txtImporto.Visible = False
                    lblNumRate.Visible = False
                    txtNumRate.Visible = False

                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False


                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                Case "4" 'CAMBI IN EMERGENZA
                    lblModR.Visible = True
                    cmbModRichiesta.Visible = True

                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    cmbMotivo.Items.Clear()
                    cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                    cmbMotivo.SelectedValue = -1
                    cmbMotivo.Visible = False
                    lblMotivo.Visible = False
                    lblDataPr.Text = "Data di presentazione"
                    lblDataPr.Visible = True
                    txtDataPr.Visible = True
                    txtDataPr.Text = ""

                    txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                    txtDataPr.Focus()
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = True
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'cmbComponenti.Items.Item(0).Enabled = False
                    lblDom.Text = "Richiedente"
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                    lblDescrizione.Visible = True
                    If TipoContratto <> "EQC392" Then
                        lblDescrizione.Text = "<b>CAMBIO IN EMERGENZA</b>: regola il Cambio in Emergenza (ex art.22) di alloggio tra Assegnatari di ERP del patrimonio in gestione."
                    Else
                        lblDescrizione.Text = "<b>CAMBIO IN EMERGENZA</b>: Art.22 c.10 è applicabile ai contratti NON ERP SOLO PER CAMBI DOVUTI A SPECIFICHE ESIGENZE AZIENDALI"
                    End If
                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    lbl1.Visible = False
                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False


                Case "8" 'CREAZIONE POSIZIONE ABUSIVA
                    lblModR.Visible = True
                    cmbModRichiesta.Visible = True

                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    cmbMotivo.Items.Clear()
                    cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                    cmbMotivo.SelectedValue = -1
                    cmbMotivo.Visible = False
                    lblMotivo.Visible = False
                    lblDataPr.Visible = True
                    lblDataPr.Text = "Data di sopralluogo"
                    txtDataPr.Visible = True
                    txtDataPr.Text = ""
                    lblDataEvento.Visible = True
                    txtDataEvento.Visible = True
                    txtDataEvento.Text = ""

                    txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                    txtDataEvento.Focus()
                    '  lblDataEvento.Visible = False
                    ' txtDataEvento.Visible = False
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = True
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'cmbComponenti.Items.Item(0).Enabled = False
                    lblDom.Text = "Richiedente"
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID>5 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>CREAZIONE POSIZIONE ABUSIVA</b>:"
                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    'imgAggComp.Visible = True
                    lbl1.Visible = False
                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False



                Case "9" 'ABUSIVISMO AMM.VO ART.15 C.2 RR 1/2004
                    lblModR.Visible = True
                    cmbModRichiesta.Visible = True

                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    cmbMotivo.Items.Clear()
                    cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                    cmbMotivo.SelectedValue = -1
                    cmbMotivo.Visible = False
                    lblMotivo.Visible = False
                    lblDataPr.Text = "Data di presentazione"
                    lblDataPr.Visible = True
                    txtDataPr.Visible = True
                    txtDataPr.Text = ""
                    lblDataEvento.Visible = True
                    txtDataEvento.Visible = True
                    txtDataEvento.Text = ""

                    txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                    txtDataEvento.Focus()
                    '  lblDataEvento.Visible = False
                    ' txtDataEvento.Visible = False
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = True
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    'cmbComponenti.Items.Item(0).Enabled = False
                    lblDom.Text = "Richiedente"
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>ABUSIVISMO AMM.VO ART.15 C.2 RR 1/2004</b>:"
                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    lbl1.Visible = False
                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False

                Case "10" 'CAMBIO TIPOLOGIA CONTRATTUALE
                    lblModR.Visible = True
                    cmbModRichiesta.Visible = True

                    lblNotifica.Visible = False
                    txtDataNotifica.Visible = False
                    lblAU.Visible = False
                    cmbAU.Visible = False

                    cmbMotivo.Items.Clear()
                    cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                    cmbMotivo.SelectedValue = -1
                    cmbMotivo.Visible = False
                    lblMotivo.Visible = False
                    lblDataPr.Text = "Data di presentazione"
                    lblDataPr.Visible = True
                    txtDataPr.Visible = True
                    txtDataPr.Text = ""
                    lblDataEvento.Visible = True
                    txtDataEvento.Visible = True
                    txtDataEvento.Text = ""

                    txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                    txtDataEvento.Focus()
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = True
                    lblMess.Visible = False
                    lblMess0.Visible = False
                    lblDom.Text = "Richiedente"
                    par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=14 OR ID=15 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)
                    cmbComponenti.Items.Item(0).Enabled = True
                    lblDescrizione.Visible = True
                    lblDescrizione.Text = "<b>Cambio Tipologia Contrattuale</b>:"
                    lblAltro.Visible = False
                    txtAltro.Visible = False
                    lblUIscambio.Visible = False
                    txtUIscambio.Visible = False
                    imgRicercaUI.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    lbl1.Visible = False
                    lbl2.Visible = False
                    lblSaldo2.Visible = False
                    txtSaldo2.Visible = False
                    lblImporto2.Visible = False
                    txtImporto2.Visible = False
                    lblNumRate2.Visible = False
                    txtNumRate2.Visible = False
                Case "12"

                    '12/03/2018 Controllo se richiedente è ammissibile per la rateizz. straordinaria
                    Dim ruAttivo As Boolean = False
                    Dim ruChiuso As Boolean = False

                    ControllaSeEsisteAltroRU(ruAttivo, ruChiuso)
                    If ruAttivo = False And ruChiuso = True Then
                        'lblMess.Visible = True
                        'lblMess.Text = "Attenzione! Impossibile procedere, il richiedente non risulta ammissibile alla sottoscrizione di un piano di rientro."

                        Dim strScript As String = "<script language='javascript'>var conf = window.confirm('Attenzione...non è stata trovata correlazione con altri contratti! Cliccare su OK per proseguire con la creazione dell\'istanza.');" _
                        & "if (conf){ document.getElementById('btnConfermaRAT').click();}" _
                                & "else{}</script>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "conf", strScript, False)

                        If confermaRat.Value = "1" Then
                            lblMess.Visible = False
                        Else
                            lblMess.Visible = True
                        End If

                    ElseIf ruAttivo = False And ruChiuso = False Then
                        'If TipoContratto = "ERP" Then
                        If ControllaSeAmmissibile(LBLid.Value, motivoEscl, TipoContratto) = False Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! Impossibile procedere, " & motivoEscl & "."
                        End If
                        'End If
                    End If

                    If lblMess.Visible = False Then


                        lblModR.Visible = True
                        cmbModRichiesta.Visible = True

                        lblNotifica.Visible = False
                        txtDataNotifica.Visible = False
                        lblAU.Visible = False
                        cmbAU.Visible = False

                        cmbMotivo.Items.Clear()
                        cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
                        cmbMotivo.SelectedValue = -1
                        cmbMotivo.Visible = False
                        lblMotivo.Visible = False
                        lblDataPr.Visible = True
                        txtDataPr.Visible = True

                        lblDataEvento.Visible = True
                        txtDataEvento.Visible = True
                        txtDataEvento.Text = ""

                        txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                        txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                        txtDataEvento.Focus()
                        '  lblDataEvento.Visible = False
                        ' txtDataEvento.Visible = False
                        lblanno.Visible = True
                        cmbAnniRedd.Visible = False

                        lblVerificaAnno.Visible = False
                        txtAnnoReddito.Visible = True
                        'txtAnnoReddito.Text = "2016"
                        'txtAnnoReddito.ReadOnly = True
                        lblMess.Visible = False
                        lblMess0.Visible = False
                        'cmbComponenti.Items.Item(0).Enabled = False
                        lblDom.Text = "Richiedente"
                        par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                        lblDescrizione.Visible = True
                        lblDescrizione.Text = "<b>RATEIZZAZIONE STRAORDINARIA</b>: permette di rateizzare un debito ai sensi della delibera di giunta comunale 48/2018."

                        lblAltro.Visible = False
                        txtAltro.Visible = False
                        lblUIscambio.Visible = False
                        txtUIscambio.Visible = False
                        imgRicercaUI.Visible = False
                        lbl1.Visible = False
                        lbl2.Visible = False
                        lblSaldo2.Visible = False
                        txtSaldo2.Visible = False
                        lblImporto2.Visible = False
                        txtImporto2.Visible = False
                        lblNumRate2.Visible = False
                        txtNumRate2.Visible = False

                        'come richiesto a verbale del 11/02/2016
                        Me.lblDataEvento.Text = "Data Protocollo"
                    Else
                        btnProcedi.Visible = False
                    End If

            End Select

            If item.Value <> "3" Then
                lblInizio.Visible = False
                txtDataIn.Visible = False
                lblFine.Visible = False
                txtDataFine.Visible = False
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Function ControllaConiuge() As Boolean

        Dim coniuge As Boolean = True

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                 & "COD_TIPOLOGIA_PARENTELA ='CON' AND ANAGRAFICA.ID=" & par.IfNull(cmbComponenti.SelectedValue, 0)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read = False Then
                coniuge = False
            End If
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return coniuge

    End Function

    Protected Sub ElencoAU()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            cmbAU.Items.Clear()

            'par.cmd.CommandText = "SELECT ID_BANDO_AU,CASE WHEN ID_BANDO_AU = 1 THEN 'AU 2007' WHEN ID_BANDO_AU = 2 THEN 'AU 2009' WHEN ID_BANDO_AU = 3 THEN 'AU 2011' END AS NOMEBANDO FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO = " & LBLid.Value & " AND ID_BANDO_AU IS NOT NULL AND ID_DICHIARAZIONE IN (SELECT ID FROM UTENZA_DICHIARAZIONI WHERE RAPPORTO='" & LBLcodContr.Value & "') ORDER BY ID_BANDO_AU DESC"

            Dim presenzaBandoUltimo As Boolean = False
            Dim contaBandi As Integer = 0
            par.cmd.CommandText = "SELECT ID as IDBANDO,ANNO_ISEE,DESCRIZIONE FROM UTENZA_BANDI where id<>4 order by id desc"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable()
            da1.Fill(dt1)
            da1.Dispose()
            If dt1.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt1.Rows
                    idBandoUltimo = row.Item("IDBANDO")
                    annoReddAU = row.Item("ANNO_ISEE")
                    contaBandi = contaBandi + 1
                Next
            End If

            'par.cmd.CommandText = "SELECT ID_BANDO,CASE WHEN ID_BANDO = 1 THEN 'AU 2007' WHEN ID_BANDO = 2 THEN 'AU 2009' WHEN ID_BANDO = 3 THEN 'AU 2011' END AS NOMEBANDO FROM UTENZA_DICHIARAZIONI WHERE RAPPORTO='" & LBLcodContr.Value & "'"
            par.cmd.CommandText = "SELECT UTENZA_BANDI.* FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.ID_BANDO=UTENZA_BANDI.ID AND RAPPORTO='" & LBLcodContr.Value & "' ORDER BY UTENZA_BANDI.ID DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows Then

                AUpresente = True
                While myReader.Read
                    cmbAU.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), ""), myReader("ID")))
                End While
                myReader.Close()

                Dim NONRispondente As Boolean = True

                If cmbAU.Items.Count <> contaBandi Then
                    For Each row As Data.DataRow In dt1.Rows
                        For Each ch As ListItem In cmbAU.Items
                            If ch.Value = row.Item("IDBANDO") Then
                                NONRispondente = False
                                'Else
                                '    NONRispondente = False
                                Exit For
                            Else
                                NONRispondente = True
                            End If
                        Next
                        If NONRispondente = True Then
                            cmbAU.Items.Add(New ListItem("NON RISP. (" & row.Item("DESCRIZIONE") & ")", row.Item("IDBANDO")))
                        End If
                    Next
                End If

                'For Each ch As ListItem In cmbAU.Items
                '    If ch.Value = idBandoUltimo Then
                '        presenzaBandoUltimo = True
                '    End If
                'Next
                'If presenzaBandoUltimo = False Then
                '    cmbAU.Items.Add(New ListItem("NON RISPONDENTE", -1))
                'End If
            Else
                cmbAU.Items.Add(New ListItem("---", -1))
                'AUpresente = False
                'lblMess.Visible = True
                'lblMess.Text = "Attenzione! Impossibile procedere, nessuna AU è stata trovata!"
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ControllaProroga()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID =" & LBLid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                TipoContratto = par.IfNull(myReader("cod_tipologia_contr_loc"), "")
                If (par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And par.IfNull(myReader("DEST_USO"), "") = "D") Or par.IfNull(myReader("FL_PROROGA"), "") = "1" Or par.IfNull(myReader("FL_ASSEGN_DEF"), "") = "1" Then
                    contrProroga = True
                    data1scadenza = par.IfNull(myReader("DATA_SCADENZA"), "")
                    data2scadenza = par.IfNull(myReader("DATA_SCADENZA_RINNOVO"), "")
                Else
                    contrProroga = False
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub ContrattoL43198()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID =" & LBLid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And par.IfNull(myReader("DEST_USO"), "") = "D" Then
                    contrProroga = True
                    data1scadenza = par.IfNull(myReader("DATA_SCADENZA"), "")
                    data2scadenza = par.IfNull(myReader("DATA_SCADENZA_RINNOVO"), "")
                Else
                    contrProroga = False
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Private Sub ControllaPassaggioERP()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT rapporti_utenza.id FROM SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC=TIPOLOGIA_CONTRATTO_LOCAZIONE.cod and FL_PASSAGGIO_ERP=1 and nvl(dest_uso,'-1')<>'P' and rapporti_utenza.ID =" & LBLid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                PassaggioERP = True
            Else
                PassaggioERP = False
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    'Private Sub CercaContratto392()
    '    Try
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)


    '        par.cmd.CommandText = "SELECT rapporti_utenza.id FROM SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC=TIPOLOGIA_CONTRATTO_LOCAZIONE.cod and FL_PASSAGGIO_ERP=1 and nvl(dest_uso,'-1')<>'P' and rapporti_utenza.ID =" & LBLid.Value
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader.Read Then
    '            contratto392 = True
    '        Else
    '            contratto392 = False
    '        End If
    '        myReader.Close()

    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try

    'End Sub
    Private Sub ContrattoTemporaneo()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID =" & LBLid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("FL_PROROGA"), "") = "1" Or par.IfNull(myReader("FL_ASSEGN_DEF"), "") = "1" Then
                    contrProroga = True
                    data1scadenza = par.IfNull(myReader("DATA_SCADENZA"), "")
                    data2scadenza = par.IfNull(myReader("DATA_SCADENZA_RINNOVO"), "")
                Else
                    contrProroga = False
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    'Protected Sub convExArt20()
    '    Try
    '        Dim condizioneNO As Integer = 0
    '        Dim condizioneSI As Integer = 0
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)

    '        par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & LBLcodContr.Value & "' ORDER BY DATA_PG DESC"
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader1.Read Then

    '            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & myReader1("ID_DICHIARAZIONE")
    '            Dim myReaderEta As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReaderEta.Read
    '                If par.RicavaEta(par.IfNull(myReaderEta("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) <= 75 And par.IfNull(myReaderEta("PERC_INVAL"), "0") < 66 Then
    '                    condizioneNO = 1
    '                Else
    '                    condizioneSI = 1
    '                End If
    '            End While
    '            myReaderEta.Close()

    '        Else
    '            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_DICHIARAZIONI.ID AS ID_DICH FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
    '            & "AND RAPPORTO='" & LBLcodContr.Value & "' AND STATO = 1 ORDER BY ID_BANDO DESC"
    '            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader2.Read Then
    '                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & myReader2("ID_DICH")
    '                Dim myReaderEta As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                While myReaderEta.Read
    '                    If par.RicavaEta(par.IfNull(myReaderEta("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) <= 75 And par.IfNull(myReaderEta("PERC_INVAL"), "0") < 66 Then
    '                        condizioneNO = 1
    '                    Else
    '                        condizioneSI = 1
    '                    End If
    '                End While
    '                myReaderEta.Close()

    '            Else
    '                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE CONTRATTO_NUM='" & LBLcodContr.Value & "'"
    '                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReader3.Read Then
    '                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & myReader3("ID_DICHIARAZIONE")
    '                    Dim myReaderEta As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                    While myReaderEta.Read
    '                        If par.RicavaEta(par.IfNull(myReaderEta("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) <= 75 And par.IfNull(myReaderEta("PERC_INVAL"), "0") < 66 Then
    '                            condizioneNO = 1
    '                        Else
    '                            condizioneSI = 1
    '                        End If
    '                    End While
    '                    myReaderEta.Close()

    '                Else
    '                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & LBLid.Value
    '                    Dim myReaderEta As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                    While myReaderEta.Read
    '                        If par.RicavaEta(par.IfNull(myReaderEta("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) <= 75 Then
    '                            condizioneNO = 1
    '                        Else
    '                            lblMess.Visible = True
    '                            lblMess.Text = "Non si dispongono di tutte le informazioni che consentono o meno l'avvio del procedimento. <br/>Si desidera proseguire ugualmente?"
    '                        End If
    '                    End While
    '                    myReaderEta.Close()

    '                End If
    '                myReader3.Close()

    '            End If
    '            myReader2.Close()

    '        End If
    '        myReader1.Close()

    '        If condizioneNO = 1 And condizioneSI = 0 Then
    '            lblMess.Visible = True
    '            lblMess.Text = "Impossibile procedere! Per questa tipologia di ampliamento è necessaria la presenza di un " _
    '                & "componente con età maggiore di 75 o con percentuale di invalidità pari o superiore al 66%."
    '            'btnProcedi.Visible = False
    '        End If

    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Protected Sub cmbMotivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMotivo.SelectedIndexChanged
        Try
            Dim item As ListItem
            item = cmbMotivo.SelectedItem

            lblAU.Visible = False
            cmbAU.Visible = False
            lblNotifica.Visible = False
            txtDataNotifica.Visible = False
            lblAltro.Visible = False
            txtAltro.Visible = False
            cmbSindacato.Visible = False
            lblSindac.Visible = False
            lblDom.Visible = False
            cmbComponenti.Visible = False
            txtDataIn.Text = ""
            txtDataFine.Text = ""
            lblMess0.Text = ""
            lblMess.Text = ""
            txtAnnoReddito.Text = ""

            If item.Value = 5 Then
                'convExArt20()
            Else
                lblMess.Visible = False
                lblMess0.Visible = False
                ' btnProcedi.Visible = True
            End If

            If item.Value = 17 Or item.Value = 18 Or item.Value = 23 Or item.Value = 27 Or item.Value = 28 Or cmbTipo.SelectedItem.Value = 2 Then
                cmbAnniRedd.Items.Clear()
                cmbModRichiesta.Items.Clear()
                par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE (ID<>2 AND ID<>5) ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                'cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                'cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                'cmbModRichiesta.Items.Add(New ListItem("sindacati", 3))
                'cmbModRichiesta.Items.Add(New ListItem("altro", 4))
            End If

            If item.Value = 29 Then
                cmbModRichiesta.Items.Clear()
                par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                'cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
                'cmbModRichiesta.Items.Add(New ListItem("a mezzo posta", 1))
                'cmbModRichiesta.Items.Add(New ListItem("sindacati", 3))
                'cmbModRichiesta.Items.Add(New ListItem("altro", 4))
                'cmbModRichiesta.Items.Add(New ListItem("verifica d'ufficio", 2))
            End If

            If item.Value >= 19 And item.Value <= 22 Then
                cmbModRichiesta.Items.Clear()
                par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID= 2 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

                'cmbModRichiesta.Items.Add(New ListItem("verifica d'ufficio", 2))
            End If

            'If item.Value = 23 Then
            '    cmbModRichiesta.Items.Clear()
            '    cmbModRichiesta.Items.Add(New ListItem("di persona", 0))
            'End If


            If item.Value >= 17 Or cmbTipo.SelectedItem.Value = 2 Or cmbTipo.SelectedItem.Value = 0 Or cmbTipo.SelectedItem.Value = 1 Or cmbTipo.SelectedItem.Value = 7 Or cmbTipo.SelectedItem.Value = 6 Then
                lblInizio.Visible = True
                txtDataIn.Visible = True
                lblFine.Visible = True
                txtDataFine.Visible = True

                lblanno.Visible = False
                cmbAnniRedd.Visible = False

                lblVerificaAnno.Visible = False
                txtAnnoReddito.Visible = False

                txtDataEvento.Text = ""
                lblDataEvento.Visible = True
                txtDataEvento.Visible = True
                txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                txtDataPr.Text = ""
                lblDataPr.Visible = True
                txtDataPr.Visible = True
                txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                If contrProroga = True Then
                    txtDataPr.Focus()
                Else
                    txtDataEvento.Focus()
                End If
            End If
            If item.Value = 20 Then
                lblDataEvento.Visible = False
                txtDataEvento.Visible = False
                lblanno.Visible = True
                txtAnnoReddito.Visible = True
            End If

            If item.Value = 28 Or item.Value = 29 Then
                lblNotifica.Visible = True
                txtDataNotifica.Visible = True
                txtDataNotifica.Text = ""
                txtDataNotifica.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                lblDataEvento.Visible = False
                txtDataEvento.Visible = False
                lblanno.Visible = True
                txtAnnoReddito.Visible = True


                'rdbAU.SelectedValue = VerificaAU2009()
                'For Each i As ListItem In rdbAU.Items
                '    If i.Selected = False Then
                '        i.Enabled = False
                '    End If
                'Next
            End If

            If item.Value = 22 Or item.Value = 21 Then
                lblanno.Visible = True
                txtAnnoReddito.Visible = True
            End If

            If item.Value = 27 Then
                txtDataEvento.Text = par.FormattaData(data1scadenza)
                txtDataEvento.ReadOnly = True
                txtDataEvento.ToolTip = "Data Scadenza Contratto"
            End If

            lblModR.Visible = True
            cmbModRichiesta.Visible = True

            If cmbTipo.SelectedValue <> "3" Then
                lblInizio.Visible = False
                txtDataIn.Visible = False
                lblFine.Visible = False
                txtDataFine.Visible = False
            End If

            If item.Value = 29 Then
                lblAU.Visible = True
                cmbAU.Visible = True
                ElencoAU()
                If cmbAU.SelectedValue <> -1 Then
                    cmbAnniRedd.Visible = True
                    lblVerificaAnno.Visible = True
                    cmbAnniRedd.Items.Clear()

                    Select Case cmbAU.SelectedValue
                        Case 1
                            cmbAnniRedd.Items.Add(New ListItem(2006))
                        Case 2
                            cmbAnniRedd.Items.Add(New ListItem(2008))
                        Case 3
                            cmbAnniRedd.Items.Add(New ListItem(2010))
                        Case 5
                            cmbAnniRedd.Items.Add(New ListItem(2012))
                    End Select
                End If
            End If

            '*** VERIFICO CHE SE LA MOTIVAZIONE E' PER DECESSO O SEPARAZIONE DEVE ESSERCI ALMENO UN ALTRO COMPONENTE NEL NUCLEO
            If item.Value = 12 Or item.Value = 13 Then
                If cmbComponenti.Items.Count = 1 Then
                    lblMess.Visible = True
                    lblMess.Text = "Impossibile procedere! Nucleo familiare composto da un solo membro."
                    cmbModRichiesta.Visible = False
                    lblModR.Visible = False
                    lblDataPr.Visible = False
                    txtDataPr.Visible = False
                    lblDataEvento.Visible = False
                    txtDataEvento.Visible = False
                    lblanno.Visible = False
                    cmbAnniRedd.Visible = False

                    lblVerificaAnno.Visible = False
                    txtAnnoReddito.Visible = False
                End If
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    'lblMess.Text = "Attenzione! Per questa tipologia di domanda l'intestatario della domanda deve coincidere con il titolare del contratto!"

    Protected Sub txtDataPr_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataPr.TextChanged
        'Dim dataErrata As Boolean = False
        dataErrata = False
        If txtDataPr.Text <> "" And Len(txtDataPr.Text) = 10 Then
            If Not par.ControllaData(txtDataPr) Or Right(txtDataPr.Text, 4) < 2000 Or Right(txtDataPr.Text, 4) > 2100 Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire una data valida!"
                'btnProcedi.Visible = False
                dataErrata = True
                lblDom.Visible = False
                cmbComponenti.Visible = False
                imgAggComp.Visible = False
                imgRicercaComp.Visible = False
                Exit Sub
            Else
                If par.AggiustaData(txtDataEvento.Text) > par.AggiustaData(txtDataPr.Text) Then
                    If cmbTipo.SelectedValue <> 9 Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! La data di presentazione non può essere precedente alla data dell'evento!"
                        'btnProcedi.Visible = False
                        dataErrata = True
                        lblDom.Visible = False
                        cmbComponenti.Visible = False
                        imgAggComp.Visible = False
                        imgRicercaComp.Visible = False
                        Exit Sub
                    End If
                Else
                    dataErrata = False
                    lblMess.Visible = False
                    ' btnProcedi.Visible = True
                End If
            End If
        ElseIf Len(txtDataPr.Text) < 10 Then
            lblMess.Visible = True
            lblMess.Text = "Attenzione! Inserire una data valida!"
            'btnProcedi.Visible = False
            dataErrata = True
            lblDom.Visible = False
            cmbComponenti.Visible = False
            imgAggComp.Visible = False
            imgRicercaComp.Visible = False
            Exit Sub
        End If

        If cmbMotivo.SelectedValue = 28 Or cmbMotivo.SelectedValue = 29 Then
            If txtDataNotifica.Text <> "" And Len(txtDataNotifica.Text) = 10 Then
                If Not par.ControllaData(txtDataNotifica) Or Right(txtDataNotifica.Text, 4) < 2000 Or Right(txtDataNotifica.Text, 4) > 2100 Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Inserire una data valida!"
                    'btnProcedi.Visible = False
                    dataErrata = True
                    lblDom.Visible = False
                    cmbComponenti.Visible = False
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                    Exit Sub
                Else
                    If par.AggiustaData(txtDataNotifica.Text) > par.AggiustaData(txtDataPr.Text) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! La data di presentazione non può essere precedente alla data di notifica!"
                        'btnProcedi.Visible = False
                        dataErrata = True
                        lblDom.Visible = False
                        cmbComponenti.Visible = False
                        imgAggComp.Visible = False
                        imgRicercaComp.Visible = False
                        Exit Sub
                    Else
                        If cmbMotivo.SelectedValue = 28 Then
                            If par.AggiustaData(txtDataPr.Text) <= par.AggiustaData(Date.Parse(par.FormattaData(txtDataNotifica.Text), New System.Globalization.CultureInfo("it-IT", False)).AddDays(120).ToString("dd/MM/yyyy")) Then
                                lblMess.Visible = True
                                lblMess.Text = "Attenzione! La data di presentazione deve essere oltre i 120 giorni dalla data di notifica!"
                                dataErrata = True
                            End If
                        Else
                            If cmbModRichiesta.SelectedValue <> 2 Then
                                If par.AggiustaData(txtDataPr.Text) >= par.AggiustaData(Date.Parse(par.FormattaData(txtDataNotifica.Text), New System.Globalization.CultureInfo("it-IT", False)).AddDays(120).ToString("dd/MM/yyyy")) Then
                                    lblMess.Visible = True
                                    lblMess.Text = "Attenzione! La data di presentazione deve essere entro i 120 giorni dalla data di notifica!"
                                    dataErrata = True
                                End If
                            End If
                        End If
                    End If
                End If
            ElseIf Len(txtDataNotifica.Text) < 10 Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Inserire una data valida!"
                'btnProcedi.Visible = False
                dataErrata = True
                lblDom.Visible = False
                cmbComponenti.Visible = False
                imgAggComp.Visible = False
                imgRicercaComp.Visible = False
                Exit Sub
            End If
        End If

        If dataErrata = False Then
            If cmbMotivo.SelectedValue = 20 Then
                annoRedd = ""
            End If

            If cmbTipo.SelectedItem.Value = "5" Then
                lblUIscambio.Visible = True
                txtUIscambio.Visible = True
                imgRicercaUI.Visible = True
                lbl1.Visible = True
                lblSaldo.Visible = True
                txtSaldo.Visible = True
                txtSaldo.Text = Format(par.CalcolaSaldoAttuale(LBLid.Value), "##,##0.00")
                lblImporto.Visible = True
                txtImporto.Visible = True
                lblNumRate.Visible = True
                txtNumRate.Visible = True
                txtImporto.Attributes.Add("onclick", "javascript:if (document.getElementById('txtImporto').value == '') {document.getElementById('txtImporto').value=document.getElementById('txtSaldo').value};")
                txtImporto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                txtImporto.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

                '22/05/2012 Prospetto saldo dell'alloggio con cui si vuole effettuare il cambio
                lbl2.Visible = True
                lblSaldo2.Visible = True
                txtSaldo2.Visible = True
                'txtSaldo2.Text = Format(par.CalcolaSaldoAttuale(LBLid2.Value), "##,##0.00")
                lblImporto2.Visible = True
                txtImporto2.Visible = True
                lblNumRate2.Visible = True
                txtNumRate2.Visible = True
                txtImporto2.Attributes.Add("onclick", "javascript:if (document.getElementById('txtImporto2').value == '') {document.getElementById('txtImporto2').value=document.getElementById('txtSaldo2').value};")
                txtImporto2.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                txtImporto2.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            End If

            If cmbTipo.SelectedItem.Value = "10" Then
                cmbAnniRedd.Visible = False
                lblVerificaAnno.Visible = False
                lblIntestRU.Visible = True
                cmbIntestRU.Visible = True
            End If
            If cmbTipo.SelectedItem.Value <> "2" And cmbTipo.SelectedItem.Value <> "3" Then
                AggiornaComponenti()
                If PassaggioERP = False Then
                    cmbComponenti.Visible = True
                    lblDom.Visible = True
                Else
                    If cmbTipo.SelectedItem.Value <> 4 Then
                        cmbComponenti.Visible = True
                        lblDom.Visible = True
                    End If
                End If
            End If
            If cmbTipo.SelectedItem.Value = "8" Then
                imgAggComp.Visible = True
                imgRicercaComp.Visible = True
            End If

            Select Case cmbMotivo.SelectedValue
                Case "4", "5", "6"
                    txtDataIn.Text = txtDataPr.Text
                    txtDataFine.Text = "31/12/2999"
                    VerificaDataFine()
                    annoRedd = Right(txtDataIn.Text, 4) - 1
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = True

                    lblVerificaAnno.Visible = True
                    txtAnnoReddito.Visible = True

                    cmbAnniRedd.Items.Clear()
                    cmbAnniRedd.Items.Add(New ListItem(annoRedd))
                    txtAnnoReddito.Focus()
                    Exit Sub
                Case "30"
                    txtDataIn.Text = txtDataPr.Text
                    txtDataFine.Text = "31/12/2999"
                Case "-1"
                    txtDataIn.Text = txtDataPr.Text
                    txtDataFine.Text = "31/12/2999"
                    If cmbTipo.SelectedItem.Value <> "12" Then
                        annoRedd = Right(txtDataIn.Text, 4) - 1
                        lblanno.Visible = True
                        cmbAnniRedd.Visible = True

                        lblVerificaAnno.Visible = True
                        txtAnnoReddito.Visible = True

                        cmbAnniRedd.Items.Clear()
                        cmbAnniRedd.Items.Add(New ListItem(annoRedd))
                        txtAnnoReddito.Focus()
                    End If
                Case "23", "18"
                    txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue, par.AggiustaData(txtDataEvento.Text)))
                    txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue))
                Case "27"
                    lblanno.Visible = True
                    txtAnnoReddito.Visible = True
                Case "28"
                    If txtAnnoReddito.Text <> "" Then
                        txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text, par.AggiustaData(txtDataPr.Text)))
                        txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text))
                    End If
                Case "29"
                    If cmbAnniRedd.SelectedValue <> "" Then
                        'txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue, par.AggiustaData(txtDataPr.Text)))
                        'txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue))
                    Else
                        txtAnnoReddito.Focus()
                    End If
            End Select
        End If
        txtAnnoReddito.Focus()
    End Sub


    Private Function VerificaDataFine()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.DATA_INIZIO_VAL>='" & par.AggiustaData(txtDataIn.Text) & "' AND DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID_STATO <> 2 AND CONTRATTO_NUM='" & LBLcodUI.Value & "' ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then

                'txtDataFine.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_VAL"), "29991231"))
                txtDataFine.Text = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")

                lblMess0.Visible = True
                lblMess0.Text = "La data di fine validità è stata impostata a " & txtDataFine.Text & " perchè è stata trovata una domanda di " & myReader1("DESCRIZIONE") & " con validità successiva"

            Else

                par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL>='" & par.AggiustaData(txtDataIn.Text) & "' AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                & "AND RAPPORTO='" & LBLcodUI.Value & "' ORDER BY ID_BANDO DESC"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then

                    'txtDataFine.Text = par.FormattaData(par.IfNull(myReader2("DATA_INIZIO_VAL"), "29991231"))

                    txtDataFine.Text = Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(par.IfNull(myReader2("DATA_INIZIO_VAL"), "29991231")))), "dd/MM/yyyy")
                    txtDataIn.Text = "01/01/" & Mid(txtDataFine.Text, 7, 4)
                    lblMess0.Visible = True
                    lblMess0.Text = "La data di fine validità è stata impostata a " & txtDataFine.Text & " perchè è stata trovata una domanda di bando " & myReader2("NOME_BANDO") & " (redditi " & myReader2("ANNO_ISEE") & ") con validità successiva"

                End If
                myReader2.Close()

            End If
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function VerificaAU(ByVal idbando As Integer) As Integer
        Dim presentata As Integer = 0
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC WHERE ID_BANDO_AU = " & idbando & " AND ID_CONTRATTO=" & LBLid.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                presentata = 1
            End If
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

        Return presentata

    End Function

    Private Function CalcoloDecorrenze(ByVal causaleDom As Integer, ByVal dataPr As String) As String

        Dim annoReddito As String = ""
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM CALCOLO_DECORRENZE_REV_C WHERE CAUSALE_DOMANDA_VSA=" & causaleDom
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable()
            da1.Fill(dt1)
            da1.Dispose()

            If dt1.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt1.Rows
                    If causaleDom = 18 And row.Item("ANNO_REDDITO") = 2007 Then
                        If dataPr > row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                            annoReddito &= row.Item("ANNO_REDDITO")
                        End If
                    End If
                    If causaleDom = 17 Then
                        If row.Item("ANNO_REDDITO") Mod 2 = 0 Then 'ANNO PARI
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        Else
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        End If
                    ElseIf causaleDom = 18 And row.Item("ANNO_REDDITO") <> 2007 Then
                        If row.Item("ANNO_REDDITO") Mod 2 = 0 Then 'ANNO PARI
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        Else
                            If dataPr >= row.Item("DATA_INIZIO") And dataPr < row.Item("DATA_FINE") Then
                                annoReddito &= row.Item("ANNO_REDDITO")
                            End If
                        End If
                    End If
                Next
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

        Return annoReddito

    End Function

    Protected Sub txtDataEvento_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataEvento.TextChanged
        Dim dataErrata As Boolean = False

        If txtDataPr.Visible = True Then
            txtDataPr.Text = ""
        End If

        'If txtAnnoReddito.Visible = True Then
        '    txtAnnoReddito.Text = ""
        'End If

        txtDataIn.Text = ""
        txtDataFine.Text = ""

        If txtDataEvento.Visible = True And txtDataEvento.Text <> "" And Len(txtDataEvento.Text) = 10 Then
            If Not par.ControllaData(txtDataEvento) Or Right(txtDataEvento.Text, 4) < 2000 Or Right(txtDataEvento.Text, 4) > 2100 Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! Data evento non valida!"
                'btnProcedi.Visible = False
                dataErrata = True
                lblDom.Visible = False
                cmbComponenti.Visible = False
                Exit Sub
            End If
        ElseIf txtDataEvento.Visible = True And Len(txtDataEvento.Text) < 10 Then
            lblMess.Visible = True
            lblMess.Text = "Attenzione! Inserire una data valida!"
            ' btnProcedi.Visible = False
            dataErrata = True
            lblDom.Visible = False
            cmbComponenti.Visible = False
            Exit Sub
        End If

        '***** 27/06/2012 Controllo che la data dell'evento sia precedente alla data di stipula di massimo 2 anni
        If cmbTipo.SelectedValue <> "8" Then
            If dataStipula <> "" Then
                If DateDiff(DateInterval.Year, CDate(txtDataEvento.Text), CDate(dataStipula)) > 2 Then
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Data evento non valida rispetto alla data di stipula del contratto!"
                    dataErrata = True
                    lblDom.Visible = False
                    cmbComponenti.Visible = False
                    Exit Sub
                End If
            End If
        End If

        If dataErrata = False Then
            lblMess.Visible = False
            annoRedd = Right(txtDataEvento.Text, 4) - 1
            lblanno.Visible = True
            cmbAnniRedd.Visible = True
            lblVerificaAnno.Visible = True
            txtAnnoReddito.Visible = True

            cmbAnniRedd.Items.Clear()
            cmbAnniRedd.Items.Add(New ListItem(annoRedd))

            If cmbMotivo.SelectedValue = 21 Or cmbMotivo.SelectedValue = 22 Then
                cmbAnniRedd.Visible = False
                lblVerificaAnno.Visible = False
            End If

            If cmbTipo.SelectedValue = 2 Then
                annoRedd = ""
                cmbAnniRedd.Items.Clear()
                lblanno.Visible = False
                cmbAnniRedd.Visible = False

                lblVerificaAnno.Visible = False
                txtAnnoReddito.Visible = False

                txtDataPr.Focus()
            End If



            If cmbMotivo.SelectedValue = 19 Then
                annoRedd = Right(txtDataEvento.Text, 4) - 1
                lblanno.Visible = True
                cmbAnniRedd.Visible = True

                lblVerificaAnno.Visible = True
                txtAnnoReddito.Visible = True

                cmbAnniRedd.Items.Clear()
                cmbAnniRedd.Items.Add(New ListItem(annoRedd))
                txtDataPr.Focus()
            End If
            If cmbMotivo.SelectedValue = 23 Then
                annoRedd = Right(txtDataEvento.Text, 4)
                lblanno.Visible = True
                cmbAnniRedd.Visible = True

                lblVerificaAnno.Visible = True
                txtAnnoReddito.Visible = True

                cmbAnniRedd.Items.Clear()
                cmbAnniRedd.Items.Add(New ListItem(annoRedd))
                txtDataPr.Focus()
            End If

            Select Case cmbMotivo.SelectedValue
                Case "3"
                    txtDataIn.Text = txtDataEvento.Text
                    txtDataFine.Text = "31/12/2999"
                    VerificaDataFine()

                    annoRedd = Right(txtDataIn.Text, 4) - 1
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = True

                    lblVerificaAnno.Visible = True
                    txtAnnoReddito.Visible = True

                    cmbAnniRedd.Items.Clear()
                    cmbAnniRedd.Items.Add(New ListItem(annoRedd))
                    txtDataPr.Focus()

                Case "2", "0", "7", "8", "9", "10", "11", "12", "13", "14", "1", "16", "15", "24", "25", "26"
                    txtDataIn.Text = txtDataEvento.Text
                    txtDataFine.Text = "31/12/2999"

                    annoRedd = Right(txtDataIn.Text, 4) - 1
                    lblanno.Visible = True
                    cmbAnniRedd.Visible = True

                    lblVerificaAnno.Visible = True
                    txtAnnoReddito.Visible = True

                    cmbAnniRedd.Items.Clear()
                    cmbAnniRedd.Items.Add(New ListItem(annoRedd))
                    txtDataPr.Focus()

                Case "6", "4", "5", "30"

                Case "22", "21"
                    txtDataPr.Focus()
                    'Case "30"
                    '    txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, Right(txtDataEvento.Text, 4), par.AggiustaData(txtDataEvento.Text)))
                    '    txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, Right(txtDataEvento.Text, 4)))
                Case "23", "18"
                    If cmbAnniRedd.SelectedValue Mod 2 = 0 Then
                        txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue, par.AggiustaData(txtDataEvento.Text)))
                        txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue))
                        txtDataPr.Focus()
                    Else
                        txtDataPr.Focus()
                    End If

                Case Else
                    txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue, par.AggiustaData(txtDataEvento.Text)))
                    txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue))
                    txtDataPr.Focus()
            End Select


            If cmbTipo.SelectedItem.Value <> "2" And cmbTipo.SelectedItem.Value <> "3" Then
                If PassaggioERP = False Then
                    cmbComponenti.Visible = True
                    lblDom.Visible = True
                Else
                    If cmbTipo.SelectedItem.Value <> 4 Then
                        cmbComponenti.Visible = True
                        lblDom.Visible = True
                    End If
                End If


                If cmbTipo.SelectedValue = "8" Then
                    imgAggComp.Visible = True
                    imgRicercaComp.Visible = True
                    txtAnnoReddito.Text = cmbAnniRedd.SelectedValue
                Else
                    imgAggComp.Visible = False
                    imgRicercaComp.Visible = False
                End If
            End If

            If cmbTipo.SelectedItem.Value = "10" Or cmbTipo.SelectedItem.Value = "12" Then
                cmbAnniRedd.Visible = False
                lblVerificaAnno.Visible = False
            End If
        End If

    End Sub

    Protected Sub cmbModRichiesta_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbModRichiesta.SelectedIndexChanged
        Dim item As ListItem
        item = cmbModRichiesta.SelectedItem
        If item.Value = 3 Then
            par.RiempiDList(Me.Page, par.OracleConn, "cmbSindacato", "select * from sindacati_vsa order by descrizione asc", "DESCRIZIONE", "ID")
            lblSindac.Visible = True
            cmbSindacato.Visible = True
            cmbSindacato.Focus()
        Else
            lblSindac.Visible = False
            cmbSindacato.Visible = False
        End If
        If item.Value = 4 Then
            lblAltro.Visible = True
            txtAltro.Visible = True
            txtAltro.Focus()
        Else
            lblAltro.Visible = False
            txtAltro.Visible = False
        End If
        If item.Value <> 3 And item.Value <> 4 Then
            txtDataEvento.Focus()
        End If
    End Sub

    Protected Sub txtAnnoReddito_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAnnoReddito.TextChanged
        Dim annoEvento As String = ""
        If cmbTipo.SelectedValue = "0" Or cmbTipo.SelectedValue = "10" Then
            lblSaldo.Visible = True
            txtSaldo.Visible = True
            txtSaldo.Text = Format(par.CalcolaSaldoAttuale(LBLid.Value), "##,##0.00")
            lblImporto.Visible = True
            txtImporto.Visible = True
            lblNumRate.Visible = True
            txtNumRate.Visible = True
            txtImporto.Attributes.Add("onclick", "javascript:if (document.getElementById('txtImporto').value == '') {document.getElementById('txtImporto').value=document.getElementById('txtSaldo').value};")
            txtImporto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtImporto.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
        End If
        Select Case cmbMotivo.SelectedValue
            Case "20"
                If Len(txtAnnoReddito.Text) = 4 And IsNumeric(txtAnnoReddito.Text) = True Then
                    If CInt(txtAnnoReddito.Text) Mod 2 = 0 Then
                        txtDataIn.Text = "01/01/" & CInt(txtAnnoReddito.Text) + 2
                        txtDataFine.Text = "31/12/" & CInt(txtAnnoReddito.Text) + 3
                    Else
                        txtDataIn.Text = "01/01/" & CInt(txtAnnoReddito.Text) + 1
                        txtDataFine.Text = "31/12/" & CInt(txtAnnoReddito.Text) + 2
                    End If
                    annoRedd = txtAnnoReddito.Text
                    txtDataEvento.Text = txtDataIn.Text
                Else
                    lblMess.Visible = True
                    lblMess.Text = "Attenzione! Anno REDDITUALE non valido!"
                    lblDom.Visible = False
                    cmbComponenti.Visible = False
                    Exit Sub
                End If
            Case "21", "22"
                annoRedd = txtAnnoReddito.Text

                txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text, par.AggiustaData(txtDataEvento.Text)))
                txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text))
            Case "27"
                txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text, data1scadenza))
                txtDataFine.Text = par.FormattaData(data2scadenza)
            Case "28"
                '20/03/2013 CONTROLLO CHE LA DATA DI PRESENTAZIONE NON SIA SUPERIORE AL 31-12-annoreddito+1/+2 (anno pari/disp.)
                annoRedd = txtAnnoReddito.Text
                If annoRedd Mod 2 = 0 Then
                    If CDate(txtDataPr.Text) > CDate("31/12/" & annoRedd + 1) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! La data di presentazione non deve essere oltre il 31/12/" & annoRedd + 1 & "!"
                        Exit Sub
                    Else
                        lblMess.Visible = False
                        lblMess.Text = ""
                    End If
                Else
                    If CDate(txtDataPr.Text) > CDate("31/12/" & annoRedd + 2) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! La data di presentazione non deve essere oltre il 31/12/" & annoRedd + 2 & "!"
                        Exit Sub
                    Else
                        lblMess.Visible = False
                        lblMess.Text = ""
                    End If
                End If

                txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text, par.AggiustaData(txtDataPr.Text)))
                txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text))
            Case "29"
                txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text, par.AggiustaData(txtDataPr.Text)))
                txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text))
            Case "18"
                If txtAnnoReddito.Text Mod 2 = 0 Then
                    txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text, par.AggiustaData(txtDataEvento.Text)))
                    txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, txtAnnoReddito.Text))
                    txtDataPr.Focus()
                Else
                    txtDataPr.Focus()
                End If
        End Select
        If txtDataEvento.Text <> "" Then
            annoEvento = Right(txtDataEvento.Text, 4)
        Else
            annoEvento = Right(txtDataPr.Text, 4)
        End If
        If cmbTipo.SelectedValue = 3 Then
            If IsNothing(cmbAnniRedd.SelectedItem) = False Then
                If cmbAnniRedd.Visible = True And txtAnnoReddito.Text = cmbAnniRedd.SelectedItem.Text Then
                    lblMess.Visible = False
                    lblMess.Text = ""
                End If
            End If
        End If


        btnProcedi.Focus()
    End Sub

    Protected Sub txtNumRate_TextChanged(sender As Object, e As System.EventArgs) Handles txtNumRate.TextChanged
        If txtImporto.Text = "" Then
            txtImporto.Text = txtSaldo.Text
        End If
    End Sub


    Protected Sub txtUIscambio_TextChanged(sender As Object, e As System.EventArgs) Handles txtUIscambio.TextChanged
        ControlloCodUI(txtUIscambio.Text)
    End Sub

    Protected Sub txtNumRate2_TextChanged(sender As Object, e As System.EventArgs) Handles txtNumRate2.TextChanged
        If txtImporto2.Text = "" Then
            txtImporto2.Text = txtSaldo2.Text
        End If
    End Sub

    Protected Sub txtDataNotifica_TextChanged(sender As Object, e As System.EventArgs) Handles txtDataNotifica.TextChanged
        If txtDataPr.Text <> "" Then
            If par.AggiustaData(txtDataNotifica.Text) > par.AggiustaData(txtDataPr.Text) Then
                lblMess.Visible = True
                lblMess.Text = "Attenzione! La data di presentazione non può essere precedente alla data di notifica!"
                'btnProcedi.Visible = False
                dataErrata = True
                lblDom.Visible = False
                cmbComponenti.Visible = False
                Exit Sub
            Else
                If cmbMotivo.SelectedValue = 28 Then
                    If par.AggiustaData(txtDataPr.Text) <= par.AggiustaData(Date.Parse(par.FormattaData(txtDataNotifica.Text), New System.Globalization.CultureInfo("it-IT", False)).AddDays(30).ToString("dd/MM/yyyy")) Then
                        lblMess.Visible = True
                        lblMess.Text = "Attenzione! La data di presentazione deve essere oltre i 30 giorni dalla data di notifica!"
                        dataErrata = True
                    End If
                Else
                    If cmbModRichiesta.SelectedValue <> 2 Then
                        If par.AggiustaData(txtDataPr.Text) >= par.AggiustaData(Date.Parse(par.FormattaData(txtDataNotifica.Text), New System.Globalization.CultureInfo("it-IT", False)).AddDays(30).ToString("dd/MM/yyyy")) Then
                            lblMess.Visible = True
                            lblMess.Text = "Attenzione! La data di presentazione deve essere entro i 30 giorni dalla data di notifica!"
                            dataErrata = True
                        End If
                    End If
                End If
            End If
        End If
        txtDataPr.Focus()
    End Sub

    Protected Sub cmbAU_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAU.SelectedIndexChanged
        cmbAnniRedd.Items.Clear()
        Select Case cmbAU.SelectedValue
            Case -1
                cmbAnniRedd.Items.Add(New ListItem(annoReddAU))
            Case 1
                cmbAnniRedd.Items.Add(New ListItem(2006))
            Case 2
                cmbAnniRedd.Items.Add(New ListItem(2008))
            Case 3
                cmbAnniRedd.Items.Add(New ListItem(2010))
            Case 5
                cmbAnniRedd.Items.Add(New ListItem(2012))

        End Select

        'If cmbAU.SelectedValue <> "-1" Then
        '    txtDataIn.Text = par.FormattaData(RicavaInizioValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue, par.AggiustaData(txtDataPr.Text)))
        '    txtDataFine.Text = par.FormattaData(RicavaFineValidita(cmbMotivo.SelectedValue, cmbAnniRedd.SelectedValue))
        'End If

    End Sub

    Protected Sub btnAggComp_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAggComp.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA FROM SISCOM_MI.ANAGRAFICA WHERE " _
                & "ANAGRAFICA.ID=" & LBLintest.Value & " ORDER BY NOMINATIVO ASC"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                If par.RicavaEta(par.IfNull(myReader2("DATA_NASCITA"), Format(Now, "yyyyMMdd"))) >= 18 Then
                    cmbComponenti.Items.Add(New ListItem(par.IfNull(myReader2("NOMINATIVO"), ""), myReader2("ID")))
                    cmbComponenti.SelectedValue = myReader2("ID")
                    LBLcompEXTRA.Value = myReader2("ID")
                End If
            End If
            myReader2.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnConfermaRAT_Click(sender As Object, e As ImageClickEventArgs) Handles btnConfermaRAT.Click
        confermaRat.Value = "1"

        lblModR.Visible = True
        cmbModRichiesta.Visible = True

        lblNotifica.Visible = False
        txtDataNotifica.Visible = False
        lblAU.Visible = False
        cmbAU.Visible = False

        cmbMotivo.Items.Clear()
        cmbMotivo.Items.Add(New ListItem("- seleziona -", -1))
        cmbMotivo.SelectedValue = -1
        cmbMotivo.Visible = False
        lblMotivo.Visible = False
        lblDataPr.Visible = True
        txtDataPr.Visible = True

        lblDataEvento.Visible = True
        txtDataEvento.Visible = True
        txtDataEvento.Text = ""

        txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


        txtDataEvento.Focus()
        '  lblDataEvento.Visible = False
        ' txtDataEvento.Visible = False
        lblanno.Visible = True
        cmbAnniRedd.Visible = False

        lblVerificaAnno.Visible = False
        txtAnnoReddito.Visible = True
        txtAnnoReddito.Text = "2016"
        txtAnnoReddito.ReadOnly = True
        lblMess.Visible = False
        lblMess0.Visible = False
        'cmbComponenti.Items.Item(0).Enabled = False
        lblDom.Text = "Richiedente"
        par.caricaComboBox("SELECT * FROM T_MOTIVO_PRESENTAZ_VSA WHERE ID=0 OR ID=1 ORDER BY ID ASC", cmbModRichiesta, "ID", "DESCRIZIONE", False)

        lblDescrizione.Visible = True
        lblDescrizione.Text = "<b>RATEIZZAZIONE STRAORDINARIA</b>: permette di rateizzare un debito ai sensi della delibera di giunta comunale 48/2018."

        lblAltro.Visible = False
        txtAltro.Visible = False
        lblUIscambio.Visible = False
        txtUIscambio.Visible = False
        imgRicercaUI.Visible = False
        lbl1.Visible = False
        lbl2.Visible = False
        lblSaldo2.Visible = False
        txtSaldo2.Visible = False
        lblImporto2.Visible = False
        txtImporto2.Visible = False
        lblNumRate2.Visible = False
        txtNumRate2.Visible = False
        btnProcedi.Visible = True
        'come richiesto a verbale del 11/02/2016
        Me.lblDataEvento.Text = "Data Protocollo"
    End Sub
End Class
