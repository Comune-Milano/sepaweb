
Partial Class Contratti_Tab_Azioni_Legali
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Public indiceconnessione As String
    Public indicecontratto As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Tab_Azioni_Legali1_modify').value='1';document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Tab_Azioni_Legali1_modify').value='1';document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            End If
        Next
        SettaFunzioniJS()
        If Not IsPostBack Then
            par.OracleConn = CType(HttpContext.Current.Session.Item(indiceconnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & indiceconnessione), Oracle.DataAccess.Client.OracleTransaction)
            CaricaDati()
        End If
        CmbContAnagEsito.Visible = False

    End Sub

    Private Sub SettaFunzioniJS()
        TxtDataContAnag.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataContGiudiziari.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataDecorrContAnag.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataDecorrContGiudiz.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataDecorrMesseInMora.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataDecorrMorosita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataDecorrReqGen.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataDecorrStatoAbit.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataMorosita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataReqGenerali.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataStatoAbitativo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtRinnovoData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtRinnovoDataDecorr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Sub CaricaDati()
        If indicecontratto <> "" Then

            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO WHERE ID_CONTRATTO = " & indicecontratto

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read Then
                Try
                    Me.cmbReqGenerali.SelectedValue = par.IfNull(myReader("REQ_GENERALI"), "-1")
                    Me.cmbReqGenEsito.SelectedValue = par.IfNull(myReader("REQ_GENERALI_ESITO"), -1)
                    Me.TxtDataReqGenerali.Text = par.FormattaData(par.IfNull(myReader("REQ_GENERALI_DATA"), ""))
                    Me.txtNoteReqGenerali.Text = par.IfNull(myReader("REQ_GENERALI_NOTE"), "")

                    Me.CmbMorosita.SelectedValue = par.IfNull(myReader("MOROSITA_PREGR"), -1)
                    Me.TxtDataMorosita.Text = par.FormattaData(par.IfNull(myReader("MOROSITA_PREGR_DATA"), ""))
                    Me.TxtNoteMorosita.Text = par.IfNull(myReader("MOROSITA_PREGR_NOTE"), "")

                    Me.CmbStatoAbitativo.SelectedValue = par.IfNull(myReader("REG_STATO_ABITATIVO"), -1)
                    Me.TxtDataStatoAbitativo.Text = par.FormattaData(par.IfNull(myReader("REG_STATO_ABITATIVO_DATA"), ""))
                    Me.TxtNoteStatoAbitativo.Text = par.IfNull(myReader("REG_STATO_ABITATIVO_NOTE"), "")

                    Me.CmbContrAnagr.SelectedValue = par.IfNull(myReader("CONTROLLO_ANAGRA"), -1)
                    Me.CmbContAnagEsito.SelectedValue = par.IfNull(myReader("CONTROLLO_ANAGRA_ESITO"), -1)
                    Me.TxtDataContAnag.Text = par.FormattaData(par.IfNull(myReader("CONTROLLO_ANAGRA_DATA"), ""))
                    Me.TxtNoteContAnag.Text = par.IfNull(myReader("CONTROLLO_ANAGRA_NOTE"), "")

                    'Me.CmbFormMesseInMora.SelectedValue = par.IfNull(myReader("MOROSITA_ANNI_PREC"), -1)
                    Me.TxtNoteMesseInMora.Text = par.IfNull(myReader("MOROSITA_ANNI_NOTE"), "")
                    'Me.TxtDataMesseInMora.Text = par.FormattaData(par.IfNull(myReader("MOROSITA_ANNI_PREC_DATA"), ""))

                    Me.CmbContGiudiziari.SelectedValue = par.IfNull(myReader("CONTENZIOSI"), -1)
                    Me.TxtDataContGiudiziari.Text = par.FormattaData(par.IfNull(myReader("CONTENZIOSI_DATA"), ""))
                    Me.TxtNoteContGiudiz.Text = par.IfNull(myReader("CONTENZIOSI_NOTE"), "")

                    Me.CmbRinnovoAmmissibile.SelectedValue = par.IfNull(myReader("RINNOVO_OK"), -1)
                    Me.TxtRinnovoData.Text = par.FormattaData(par.IfNull(myReader("RINNOVO_OK_DATA"), ""))

                    Me.TxtNumIdentReqGenerali.Text = par.IfNull(myReader("REG_GENERALI_NUM_ID"), "")
                    Me.TxtNumIdentMorosita.Text = par.IfNull(myReader("MOROSITA_PREGR_NUM_ID"), "")
                    Me.TxtNumIdentStatoAbitativo.Text = par.IfNull(myReader("REG_STATO_ABITATIVO_NUM_ID"), "")
                    Me.TxtNumIdentInMora.Text = par.IfNull(myReader("MOROSITA_ANNI_PREC_NUM_ID"), "")
                    Me.TxtNumIdentContAnag.Text = par.IfNull(myReader("CONTROLLO_ANAGRA_NUM_ID"), "")
                    Me.TxtNumIdentContGiudiz.Text = par.IfNull(myReader("CONTENZIOSI_NUM_ID"), "")
                    Me.TxtNumIdentRinnovo.Text = par.IfNull(myReader("RINNOVO_OK_NUM_ID"), "")

                    Me.TxtDataDecorrReqGen.Text = par.FormattaData(par.IfNull(myReader("REQ_GENERALI_DATA_DECORR"), ""))
                    Me.TxtDataDecorrMorosita.Text = par.FormattaData(par.IfNull(myReader("MOROSITA_PREGR_DATA_DECORR"), ""))
                    Me.TxtDataDecorrStatoAbit.Text = par.FormattaData(par.IfNull(myReader("REG_STATO_ABIT_DATA_DECORR"), ""))
                    Me.TxtDataDecorrContAnag.Text = par.FormattaData(par.IfNull(myReader("CONTROLLO_ANAGRA_DATA_DECORR"), ""))
                    Me.TxtDataDecorrMesseInMora.Text = par.FormattaData(par.IfNull(myReader("MOROSITA_ANNI_PREC_DATA_DECORR"), ""))
                    Me.TxtDataDecorrContGiudiz.Text = par.FormattaData(par.IfNull(myReader("CONTENZIOSI_DATA_DECORR"), ""))
                    Me.TxtRinnovoDataDecorr.Text = par.FormattaData(par.IfNull(myReader("RINNOVO_OK_DATA_DECORR"), ""))

                    If par.IfNull(myReader("REQ_GENERALI"), "-1") <> "-1" Then
                        If Me.cmbReqGenerali.SelectedValue = 1 Or cmbReqGenerali.SelectedValue = 2 Then
                            Me.cmbReqGenEsito.Enabled = True
                        Else
                            Me.cmbReqGenEsito.Enabled = False
                        End If
                    End If
                Catch ex As Exception
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                    Session.Item("LAVORAZIONE") = "0"
                    par.myTrans.Rollback()
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End Try
            End If
            myReader.Close()

            CtrInterventoVerificaSEC()
            CaricaAttributi()

        End If
    End Sub

    Private Sub CtrInterventoVerificaSEC()
        Try
            '********* Per il blocco Regolarità Stato Abitativo si riporta l'esito dell'ultimo Intervento Verifica dal modulo SEC (Regolare, Non Regolare) *********

            par.cmd.CommandText = "select id_unita from siscom_mi.unita_contrattuale where id_Contratto=" & indicecontratto & " and id_unita_principale is null"
            Dim id_unita As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)

            par.cmd.CommandText = "select id_stato_alloggio_arrivo from siscom_mi.interventi_sicurezza where id_unita=" & id_unita & " and id_tipo_intervento=4 and id_stato_alloggio_arrivo is not null"
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                If par.IfNull(lettore0("id_stato_alloggio_arrivo"), 0) = 3 Then
                    CmbStatoAbitativo.SelectedValue = 1
                Else
                    CmbStatoAbitativo.SelectedValue = 0
                End If
            End If
            lettore0.Close()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub MemorizzaAttributi()
        'PROPOSTA DECADENZA
        If cmbReqGenerali.SelectedValue <> "" Then
            If cmbReqGenerali.Attributes("Val. Corrente").ToString.ToUpper <> cmbReqGenerali.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(cmbReqGenerali.Attributes("NOME").ToUpper.ToString, cmbReqGenerali.Attributes("Val. Corrente").ToUpper.ToString, cmbReqGenerali.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If cmbReqGenEsito.SelectedValue <> "" Then
            If cmbReqGenEsito.Attributes("Val. Corrente").ToString.ToUpper <> cmbReqGenEsito.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(cmbReqGenEsito.Attributes("NOME").ToUpper.ToString, cmbReqGenEsito.Attributes("Val. Corrente").ToUpper.ToString, cmbReqGenEsito.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If TxtDataReqGenerali.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataReqGenerali.Text.ToUpper Then
            If ScriviLogOp(TxtDataReqGenerali.Attributes("NOME").ToUpper.ToString, TxtDataReqGenerali.Attributes("Val. Corrente").ToUpper.ToString, TxtDataReqGenerali.Text.ToUpper) = False Then

            End If
        End If

        If TxtDataDecorrReqGen.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataDecorrReqGen.Text.ToUpper Then
            If ScriviLogOp(TxtDataDecorrReqGen.Attributes("NOME").ToUpper.ToString, TxtDataDecorrReqGen.Attributes("Val. Corrente").ToUpper.ToString, TxtDataDecorrReqGen.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentReqGenerali.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentReqGenerali.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentReqGenerali.Attributes("NOME").ToUpper.ToString, TxtNumIdentReqGenerali.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentReqGenerali.Text.ToUpper) = False Then

            End If
        End If

        If txtNoteReqGenerali.Attributes("Val. Corrente").ToString.ToUpper <> txtNoteReqGenerali.Text.ToUpper Then
            If ScriviLogOp(txtNoteReqGenerali.Attributes("NOME").ToUpper.ToString, txtNoteReqGenerali.Attributes("Val. Corrente").ToUpper.ToString, txtNoteReqGenerali.Text.ToUpper) = False Then

            End If
        End If

        'EMESSO DECRETO DECADENZA
        If CmbMorosita.SelectedValue <> "" Then
            If CmbMorosita.Attributes("Val. Corrente").ToString.ToUpper <> CmbMorosita.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(CmbMorosita.Attributes("NOME").ToUpper.ToString, CmbMorosita.Attributes("Val. Corrente").ToUpper.ToString, CmbMorosita.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If TxtDataMorosita.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataMorosita.Text.ToUpper Then
            If ScriviLogOp(TxtDataMorosita.Attributes("NOME").ToUpper.ToString, TxtDataMorosita.Attributes("Val. Corrente").ToUpper.ToString, TxtDataMorosita.Text.ToUpper) = False Then

            End If
        End If

        If TxtDataDecorrMorosita.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataDecorrMorosita.Text.ToUpper Then
            If ScriviLogOp(TxtDataDecorrMorosita.Attributes("NOME").ToUpper.ToString, TxtDataDecorrMorosita.Attributes("Val. Corrente").ToUpper.ToString, TxtDataDecorrMorosita.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentMorosita.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentMorosita.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentMorosita.Attributes("NOME").ToUpper.ToString, TxtNumIdentMorosita.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentMorosita.Text.ToUpper) = False Then

            End If
        End If

        If TxtNoteMorosita.Attributes("Val. Corrente").ToString.ToUpper <> TxtNoteMorosita.Text.ToUpper Then
            If ScriviLogOp(TxtNoteMorosita.Attributes("NOME").ToUpper.ToString, TxtNoteMorosita.Attributes("Val. Corrente").ToUpper.ToString, TxtNoteMorosita.Text.ToUpper) = False Then

            End If
        End If

        'REGOLARITA STATO ABITATIVO
        If CmbStatoAbitativo.SelectedValue <> "" Then
            If CmbStatoAbitativo.Attributes("Val. Corrente").ToString.ToUpper <> CmbStatoAbitativo.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(CmbStatoAbitativo.Attributes("NOME").ToUpper.ToString, CmbStatoAbitativo.Attributes("Val. Corrente").ToUpper.ToString, CmbStatoAbitativo.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If TxtDataStatoAbitativo.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataStatoAbitativo.Text.ToUpper Then
            If ScriviLogOp(TxtDataStatoAbitativo.Attributes("NOME").ToUpper.ToString, TxtDataStatoAbitativo.Attributes("Val. Corrente").ToUpper.ToString, TxtDataStatoAbitativo.Text.ToUpper) = False Then

            End If
        End If

        If TxtDataDecorrStatoAbit.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataDecorrStatoAbit.Text.ToUpper Then
            If ScriviLogOp(TxtDataDecorrStatoAbit.Attributes("NOME").ToUpper.ToString, TxtDataDecorrStatoAbit.Attributes("Val. Corrente").ToUpper.ToString, TxtDataDecorrStatoAbit.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentStatoAbitativo.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentStatoAbitativo.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentStatoAbitativo.Attributes("NOME").ToUpper.ToString, TxtNumIdentStatoAbitativo.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentStatoAbitativo.Text.ToUpper) = False Then

            End If
        End If

        If TxtNoteStatoAbitativo.Attributes("Val. Corrente").ToString.ToUpper <> TxtNoteStatoAbitativo.Text.ToUpper Then
            If ScriviLogOp(TxtNoteStatoAbitativo.Attributes("NOME").ToUpper.ToString, TxtNoteStatoAbitativo.Attributes("Val. Corrente").ToUpper.ToString, TxtNoteStatoAbitativo.Text.ToUpper) = False Then

            End If
        End If


        'CONTROLLO ANAGRAFICO

        If CmbContrAnagr.SelectedValue <> "" Then
            If CmbContrAnagr.Attributes("Val. Corrente").ToString.ToUpper <> CmbContrAnagr.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(CmbContrAnagr.Attributes("NOME").ToUpper.ToString, CmbContrAnagr.Attributes("Val. Corrente").ToUpper.ToString, CmbContrAnagr.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If CmbContAnagEsito.SelectedValue <> "" Then
            If CmbContAnagEsito.Attributes("Val. Corrente").ToString.ToUpper <> CmbContAnagEsito.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(CmbContAnagEsito.Attributes("NOME").ToUpper.ToString, CmbContAnagEsito.Attributes("Val. Corrente").ToUpper.ToString, CmbContAnagEsito.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If TxtDataContAnag.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataContAnag.Text.ToUpper Then
            If ScriviLogOp(TxtDataContAnag.Attributes("NOME").ToUpper.ToString, TxtDataContAnag.Attributes("Val. Corrente").ToUpper.ToString, TxtDataContAnag.Text.ToUpper) = False Then

            End If
        End If

        If TxtDataDecorrContAnag.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataDecorrContAnag.Text.ToUpper Then
            If ScriviLogOp(TxtDataDecorrContAnag.Attributes("NOME").ToUpper.ToString, TxtDataDecorrContAnag.Attributes("Val. Corrente").ToUpper.ToString, TxtDataDecorrContAnag.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentContAnag.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentContAnag.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentContAnag.Attributes("NOME").ToUpper.ToString, TxtNumIdentContAnag.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentContAnag.Text.ToUpper) = False Then

            End If
        End If

        If txtNoteReqGenerali.Attributes("Val. Corrente").ToString.ToUpper <> txtNoteReqGenerali.Text.ToUpper Then
            If ScriviLogOp(txtNoteReqGenerali.Attributes("NOME").ToUpper.ToString, txtNoteReqGenerali.Attributes("Val. Corrente").ToUpper.ToString, txtNoteReqGenerali.Text.ToUpper) = False Then

            End If
        End If


        'ACCESSO UFFICIALE GIUDIZIARIO
        TxtDataDecorrMesseInMora.Attributes.Add("NOME", "ACCESSO UFFICIALE GIUDIZIARIO - DATA DECORR./ESEC.:")
        TxtNumIdentInMora.Attributes.Add("NOME", "ACCESSO UFFICIALE GIUDIZIARIO - N.IDENT.:")
        TxtNoteMesseInMora.Attributes.Add("NOME", "ACCESSO UFFICIALE GIUDIZIARIO - NOTE:")

        If TxtDataDecorrMesseInMora.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataDecorrMesseInMora.Text.ToUpper Then
            If ScriviLogOp(TxtDataDecorrMesseInMora.Attributes("NOME").ToUpper.ToString, TxtDataDecorrMesseInMora.Attributes("Val. Corrente").ToUpper.ToString, TxtDataDecorrMesseInMora.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentInMora.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentInMora.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentInMora.Attributes("NOME").ToUpper.ToString, TxtNumIdentInMora.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentInMora.Text.ToUpper) = False Then

            End If
        End If

        If TxtNoteMesseInMora.Attributes("Val. Corrente").ToString.ToUpper <> TxtNoteMesseInMora.Text.ToUpper Then
            If ScriviLogOp(TxtNoteMesseInMora.Attributes("NOME").ToUpper.ToString, TxtNoteMesseInMora.Attributes("Val. Corrente").ToUpper.ToString, TxtNoteMesseInMora.Text.ToUpper) = False Then

            End If
        End If

        'PRESENZA DI CONTENZIOSI GIUDIZIARI PER INADEMPIMENTI CONTRATTUALI/PROCEDURE ESECUTIVE
        If CmbContGiudiziari.SelectedValue <> "" Then
            If CmbContGiudiziari.Attributes("Val. Corrente").ToString.ToUpper <> CmbContGiudiziari.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(CmbContGiudiziari.Attributes("NOME").ToUpper.ToString, CmbContGiudiziari.Attributes("Val. Corrente").ToUpper.ToString, CmbContGiudiziari.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If TxtDataContGiudiziari.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataContGiudiziari.Text.ToUpper Then
            If ScriviLogOp(TxtDataContGiudiziari.Attributes("NOME").ToUpper.ToString, TxtDataContGiudiziari.Attributes("Val. Corrente").ToUpper.ToString, TxtDataContGiudiziari.Text.ToUpper) = False Then

            End If
        End If

        If TxtDataDecorrContGiudiz.Attributes("Val. Corrente").ToString.ToUpper <> TxtDataDecorrContGiudiz.Text.ToUpper Then
            If ScriviLogOp(TxtDataDecorrContGiudiz.Attributes("NOME").ToUpper.ToString, TxtDataDecorrContGiudiz.Attributes("Val. Corrente").ToUpper.ToString, TxtDataDecorrContGiudiz.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentContGiudiz.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentContGiudiz.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentContGiudiz.Attributes("NOME").ToUpper.ToString, TxtNumIdentContGiudiz.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentContGiudiz.Text.ToUpper) = False Then

            End If
        End If

        If TxtNoteContGiudiz.Attributes("Val. Corrente").ToString.ToUpper <> TxtNoteContGiudiz.Text.ToUpper Then
            If ScriviLogOp(TxtNoteContGiudiz.Attributes("NOME").ToUpper.ToString, TxtNoteContGiudiz.Attributes("Val. Corrente").ToUpper.ToString, TxtNoteContGiudiz.Text.ToUpper) = False Then

            End If
        End If


        'RINNOVO AMMISSIBILE

        CmbRinnovoAmmissibile.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - STATO:")
        TxtRinnovoData.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - DATA EMISS.:")
        TxtRinnovoDataDecorr.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - DATA DECORR./ESEC.:")
        TxtNumIdentRinnovo.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - N.IDENT.:")

        If CmbRinnovoAmmissibile.SelectedValue <> "" Then
            If CmbRinnovoAmmissibile.Attributes("Val. Corrente").ToString.ToUpper <> CmbRinnovoAmmissibile.SelectedItem.Text.ToUpper Then
                If ScriviLogOp(CmbRinnovoAmmissibile.Attributes("NOME").ToUpper.ToString, CmbRinnovoAmmissibile.Attributes("Val. Corrente").ToUpper.ToString, CmbRinnovoAmmissibile.SelectedItem.Text.ToUpper) = False Then

                End If
            End If
        End If
        If TxtRinnovoData.Attributes("Val. Corrente").ToString.ToUpper <> TxtRinnovoData.Text.ToUpper Then
            If ScriviLogOp(TxtRinnovoData.Attributes("NOME").ToUpper.ToString, TxtRinnovoData.Attributes("Val. Corrente").ToUpper.ToString, TxtRinnovoData.Text.ToUpper) = False Then

            End If
        End If

        If TxtRinnovoDataDecorr.Attributes("Val. Corrente").ToString.ToUpper <> TxtRinnovoDataDecorr.Text.ToUpper Then
            If ScriviLogOp(TxtRinnovoDataDecorr.Attributes("NOME").ToUpper.ToString, TxtRinnovoDataDecorr.Attributes("Val. Corrente").ToUpper.ToString, TxtRinnovoDataDecorr.Text.ToUpper) = False Then

            End If
        End If

        If TxtNumIdentRinnovo.Attributes("Val. Corrente").ToString.ToUpper <> TxtNumIdentRinnovo.Text.ToUpper Then
            If ScriviLogOp(TxtNumIdentRinnovo.Attributes("NOME").ToUpper.ToString, TxtNumIdentRinnovo.Attributes("Val. Corrente").ToUpper.ToString, TxtNumIdentRinnovo.Text.ToUpper) = False Then

            End If
        End If

    End Sub
    Private Function ScriviLogOp(ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String) As Boolean
        Try
            Dim aperto As Boolean = False

            'If par.cmd.Connection.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.cmd = par.OracleConn.CreateCommand()
            '    aperto = True
            'End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & indicecontratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F23','" & par.PulisciStrSql(CAMPO) & " Val. Precedente: " & VAL_PRECEDENTE & " , Val. Corrente: " & VAL_IMPOSTATO & "')"
            par.cmd.ExecuteNonQuery()

            'If aperto = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            'End If
            ScriviLogOp = True
        Catch ex As Exception
            par.OracleConn.Close()
            ScriviLogOp = False
        End Try
    End Function


    Private Sub CaricaAttributi()

        'PROPOSTA DECADENZA
        cmbReqGenerali.Attributes.Add("NOME", "PROPOSTA DECADENZA - STATO:")
        cmbReqGenEsito.Attributes.Add("NOME", "PROPOSTA DECADENZA - ESITO:")
        TxtDataReqGenerali.Attributes.Add("NOME", "PROPOSTA DECADENZA - DATA EMISS.:")
        TxtDataDecorrReqGen.Attributes.Add("NOME", "PROPOSTA DECADENZA - DATA DECORR./ESEC.:")
        TxtNumIdentReqGenerali.Attributes.Add("NOME", "PROPOSTA DECADENZA - N.IDENT.:")
        txtNoteReqGenerali.Attributes.Add("NOME", "PROPOSTA DECADENZA - NOTE:")
        cmbReqGenerali.Attributes.Add("Val. Corrente", cmbReqGenerali.SelectedItem.Text)
        cmbReqGenEsito.Attributes.Add("Val. Corrente", cmbReqGenEsito.SelectedItem.Text)
        TxtDataReqGenerali.Attributes.Add("Val. Corrente", TxtDataReqGenerali.Text)
        TxtDataDecorrReqGen.Attributes.Add("Val. Corrente", TxtDataDecorrReqGen.Text)
        TxtNumIdentReqGenerali.Attributes.Add("Val. Corrente", TxtNumIdentReqGenerali.Text)
        txtNoteReqGenerali.Attributes.Add("Val. Corrente", txtNoteReqGenerali.Text)


        'EMESSO DECRETO DECADENZA
        CmbMorosita.Attributes.Add("NOME", "EMESSO DECRETO DECADENZA - STATO:")
        TxtDataMorosita.Attributes.Add("NOME", "EMESSO DECRETO DECADENZA - DATA EMISS.:")
        TxtDataDecorrMorosita.Attributes.Add("NOME", "EMESSO DECRETO DECADENZA - DATA DECORR./ESEC.:")
        TxtNumIdentMorosita.Attributes.Add("NOME", "EMESSO DECRETO DECADENZA - N.IDENT.:")
        TxtNoteMorosita.Attributes.Add("NOME", "EMESSO DECRETO DECADENZA - NOTE:")

        CmbMorosita.Attributes.Add("Val. Corrente", CmbMorosita.SelectedItem.Text)
        TxtDataMorosita.Attributes.Add("Val. Corrente", TxtDataMorosita.Text)
        TxtDataDecorrMorosita.Attributes.Add("Val. Corrente", TxtDataDecorrMorosita.Text)
        TxtNumIdentMorosita.Attributes.Add("Val. Corrente", TxtNumIdentMorosita.Text)
        TxtNoteMorosita.Attributes.Add("Val. Corrente", TxtNoteMorosita.Text)


        'REGOLARITA STATO ABITATIVO
        CmbStatoAbitativo.Attributes.Add("NOME", "REGOLARITA STATO ABITATIVO - STATO:")
        TxtDataStatoAbitativo.Attributes.Add("NOME", "REGOLARITA STATO ABITATIVO - DATA EMISS.:")
        TxtDataDecorrStatoAbit.Attributes.Add("NOME", "REGOLARITA STATO ABITATIVO - DATA DECORR./ESEC.:")
        TxtNumIdentStatoAbitativo.Attributes.Add("NOME", "REGOLARITA STATO ABITATIVO - N.IDENT.:")
        TxtNoteStatoAbitativo.Attributes.Add("NOME", "REGOLARITA STATO ABITATIVO - NOTE:")

        CmbStatoAbitativo.Attributes.Add("Val. Corrente", CmbStatoAbitativo.SelectedItem.Text)
        TxtDataStatoAbitativo.Attributes.Add("Val. Corrente", TxtDataStatoAbitativo.Text)
        TxtDataDecorrStatoAbit.Attributes.Add("Val. Corrente", TxtDataDecorrStatoAbit.Text)
        TxtNumIdentStatoAbitativo.Attributes.Add("Val. Corrente", TxtNumIdentStatoAbitativo.Text)
        TxtNoteStatoAbitativo.Attributes.Add("Val. Corrente", TxtNoteStatoAbitativo.Text)

        'CONTROLLO ANAGRAFICO
        CmbContrAnagr.Attributes.Add("NOME", "CONTROLLO ANAGRAFICO - STATO:")
        CmbContAnagEsito.Attributes.Add("NOME", "CONTROLLO ANAGRAFICO - ESITO:")
        TxtDataContAnag.Attributes.Add("NOME", "CONTROLLO ANAGRAFICO - DATA EMISS.:")
        TxtDataDecorrContAnag.Attributes.Add("NOME", "CONTROLLO ANAGRAFICO - DATA DECORR./ESEC.:")
        TxtNumIdentContAnag.Attributes.Add("NOME", "CONTROLLO ANAGRAFICO - N.IDENT.:")
        txtNoteReqGenerali.Attributes.Add("NOME", "CONTROLLO ANAGRAFICO - NOTE:")

        CmbContrAnagr.Attributes.Add("Val. Corrente", CmbContrAnagr.SelectedItem.Text)
        CmbContAnagEsito.Attributes.Add("Val. Corrente", CmbContAnagEsito.SelectedItem.Text)
        TxtDataContAnag.Attributes.Add("Val. Corrente", TxtDataContAnag.Text)
        TxtDataDecorrContAnag.Attributes.Add("Val. Corrente", TxtDataDecorrContAnag.Text)
        TxtNumIdentContAnag.Attributes.Add("Val. Corrente", TxtNumIdentContAnag.Text)
        TxtNoteContAnag.Attributes.Add("Val. Corrente", TxtNoteContAnag.Text)

        'ACCESSO UFFICIALE GIUDIZIARIO
        TxtDataDecorrMesseInMora.Attributes.Add("NOME", "ACCESSO UFFICIALE GIUDIZIARIO - DATA DECORR./ESEC.:")
        TxtNumIdentInMora.Attributes.Add("NOME", "ACCESSO UFFICIALE GIUDIZIARIO - N.IDENT.:")
        TxtNoteMesseInMora.Attributes.Add("NOME", "ACCESSO UFFICIALE GIUDIZIARIO - NOTE:")

        TxtDataDecorrMesseInMora.Attributes.Add("Val. Corrente", TxtDataDecorrMesseInMora.Text)
        TxtNumIdentInMora.Attributes.Add("Val. Corrente", TxtNumIdentInMora.Text)
        TxtNoteMesseInMora.Attributes.Add("Val. Corrente", TxtNoteMesseInMora.Text)

        'PRESENZA DI CONTENZIOSI GIUDIZIARI PER INADEMPIMENTI CONTRATTUALI/PROCEDURE ESECUTIVE
        CmbContGiudiziari.Attributes.Add("NOME", "CONTENZ. GIUDIZIARI PER INADEMP. CONTRATT./PROC. ESECUTIVE - STATO:")
        TxtDataContGiudiziari.Attributes.Add("NOME", "CONTENZ. GIUDIZIARI PER INADEMP. CONTRATT./PROC. ESECUTIVE - DATA EMISS.:")
        TxtDataDecorrContGiudiz.Attributes.Add("NOME", "CONTENZ. GIUDIZIARI PER INADEMP. CONTRATT./PROC. ESECUTIVE - DATA DECORR./ESEC.:")
        TxtNumIdentContGiudiz.Attributes.Add("NOME", "CONTENZ. GIUDIZIARI PER INADEMP. CONTRATT./PROC. ESECUTIVE - N.IDENT.:")
        TxtNoteContGiudiz.Attributes.Add("NOME", "CONTENZ. GIUDIZIARI PER INADEMP. CONTRATT./PROC. ESECUTIVE - NOTE:")

        CmbContGiudiziari.Attributes.Add("Val. Corrente", CmbContGiudiziari.SelectedItem.Text)
        TxtDataContGiudiziari.Attributes.Add("Val. Corrente", TxtDataContGiudiziari.Text)
        TxtDataDecorrContGiudiz.Attributes.Add("Val. Corrente", TxtDataDecorrContGiudiz.Text)
        TxtNumIdentContGiudiz.Attributes.Add("Val. Corrente", TxtNumIdentContGiudiz.Text)
        TxtNoteContGiudiz.Attributes.Add("Val. Corrente", TxtNoteContGiudiz.Text)

        'RINNOVO AMMISSIBILE
        CmbRinnovoAmmissibile.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - STATO:")
        TxtRinnovoData.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - DATA EMISS.:")
        TxtRinnovoDataDecorr.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - DATA DECORR./ESEC.:")
        TxtNumIdentRinnovo.Attributes.Add("NOME", "RINNOVO AMMISSIBILE - N.IDENT.:")

        CmbRinnovoAmmissibile.Attributes.Add("Val. Corrente", CmbRinnovoAmmissibile.SelectedItem.Text)
        TxtRinnovoData.Attributes.Add("Val. Corrente", TxtRinnovoData.Text)
        TxtRinnovoDataDecorr.Attributes.Add("Val. Corrente", TxtRinnovoDataDecorr.Text)
        TxtNumIdentRinnovo.Attributes.Add("Val. Corrente", TxtNumIdentRinnovo.Text)
    End Sub

    Private Sub Salva()
        Try


            If indicecontratto <> "0" Then

                '*********************APERTURA CONNESSIONE**********************
                'If par.OracleConn.State = Data.ConnectionState.Closed Then
                '    par.OracleConn.Open()
                '    par.SettaCommand(par)
                'End If
                If Me.CmbRinnovoAmmissibile.SelectedValue <> "0" And Me.CmbRinnovoAmmissibile.SelectedValue <> "" Then
                    ' If Me.cmbReqGenEsito.SelectedValue = "1" AndAlso Me.CmbMorosita.SelectedValue = "0" AndAlso Me.CmbStatoAbitativo.SelectedValue = "1" AndAlso Me.CmbContAnagEsito.SelectedValue = "1" AndAlso Me.CmbContGiudiziari.SelectedValue = "0" Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO (ID_CONTRATTO, REQ_GENERALI, REQ_GENERALI_ESITO, REQ_GENERALI_DATA, " _
                                            & "REQ_GENERALI_NOTE, MOROSITA_PREGR, MOROSITA_PREGR_DATA, MOROSITA_PREGR_NOTE, REG_STATO_ABITATIVO, REG_STATO_ABITATIVO_DATA, " _
                                            & "REG_STATO_ABITATIVO_NOTE, CONTROLLO_ANAGRA, CONTROLLO_ANAGRA_ESITO, CONTROLLO_ANAGRA_DATA, CONTROLLO_ANAGRA_NOTE, " _
                                            & "MOROSITA_ANNI_NOTE, CONTENZIOSI, CONTENZIOSI_DATA, CONTENZIOSI_NOTE, RINNOVO_OK, RINNOVO_OK_DATA, " _
                                            & "REG_GENERALI_NUM_ID, MOROSITA_PREGR_NUM_ID, REG_STATO_ABITATIVO_NUM_ID, CONTROLLO_ANAGRA_NUM_ID, " _
                                            & " CONTENZIOSI_NUM_ID, RINNOVO_OK_NUM_ID, MOROSITA_ANNI_PREC_NUM_ID, " _
                                            & "REQ_GENERALI_DATA_DECORR," _
                                            & "MOROSITA_PREGR_DATA_DECORR," _
                                            & "REG_STATO_ABIT_DATA_DECORR," _
                                            & "CONTROLLO_ANAGRA_DATA_DECORR," _
                                            & "MOROSITA_ANNI_PREC_DATA_DECORR," _
                                            & "CONTENZIOSI_DATA_DECORR," _
                                            & "RINNOVO_OK_DATA_DECORR) " _
                                            & "VALUES (" & (indicecontratto) & ", " _
                                            & "" & Me.cmbReqGenerali.SelectedValue & ", " & Me.cmbReqGenEsito.SelectedValue & ", '" & par.AggiustaData(Me.TxtDataReqGenerali.Text) & "' ,'" & par.PulisciStrSql(Me.txtNoteReqGenerali.Text) & "', " _
                                            & "" & Me.CmbMorosita.SelectedValue & ", '" & par.AggiustaData(Me.TxtDataMorosita.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteMorosita.Text) & "', " & Me.CmbStatoAbitativo.SelectedValue & ", " _
                                            & "'" & par.AggiustaData(Me.TxtDataStatoAbitativo.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteStatoAbitativo.Text) & "', " & Me.CmbContrAnagr.SelectedValue & "," & Me.CmbContAnagEsito.SelectedValue & ", " _
                                            & "'" & par.AggiustaData(Me.TxtDataContAnag.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteContAnag.Text) & "', " _
                                            & "'" & par.PulisciStrSql(Me.TxtNoteMesseInMora.Text) & "', " & Me.CmbContGiudiziari.SelectedValue & ", '" & par.AggiustaData(Me.TxtDataContGiudiziari.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteContGiudiz.Text) & "', " _
                                            & "" & Me.CmbRinnovoAmmissibile.SelectedValue & ", '" & par.AggiustaData(Me.TxtRinnovoData.Text) & "' ," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentReqGenerali.Text) & "'," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentMorosita.Text) & "'," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentStatoAbitativo.Text) & "'," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentContAnag.Text) & "'," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentContGiudiz.Text) & "'," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentRinnovo.Text) & "'," _
                                            & "'" & par.PulisciStrSql(Me.TxtNumIdentInMora.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrReqGen.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrMorosita.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrStatoAbit.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrContAnag.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrMesseInMora.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrContGiudiz.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtRinnovoDataDecorr.Text) & "')"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    'Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '& "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '& "'F23','')"
                    'par.cmd.ExecuteNonQuery()
                    'Else
                    'Response.Write("<script>alert('Verificare la congruenza dei dati inseriti!');</script>")

                    'End If


                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO (ID_CONTRATTO, REQ_GENERALI, REQ_GENERALI_ESITO, REQ_GENERALI_DATA, " _
                                        & "REQ_GENERALI_NOTE, MOROSITA_PREGR, MOROSITA_PREGR_DATA, MOROSITA_PREGR_NOTE, REG_STATO_ABITATIVO, REG_STATO_ABITATIVO_DATA, " _
                                        & "REG_STATO_ABITATIVO_NOTE, CONTROLLO_ANAGRA, CONTROLLO_ANAGRA_ESITO, CONTROLLO_ANAGRA_DATA,CONTROLLO_ANAGRA_NOTE, " _
                                        & "MOROSITA_ANNI_NOTE,  CONTENZIOSI, CONTENZIOSI_DATA, CONTENZIOSI_NOTE, RINNOVO_OK, RINNOVO_OK_DATA, " _
                                        & "REG_GENERALI_NUM_ID, MOROSITA_PREGR_NUM_ID, REG_STATO_ABITATIVO_NUM_ID, CONTROLLO_ANAGRA_NUM_ID, " _
                                        & "CONTENZIOSI_NUM_ID, RINNOVO_OK_NUM_ID, MOROSITA_ANNI_PREC_NUM_ID, " _
                                        & "REQ_GENERALI_DATA_DECORR," _
                                            & "MOROSITA_PREGR_DATA_DECORR," _
                                            & "REG_STATO_ABIT_DATA_DECORR," _
                                            & "CONTROLLO_ANAGRA_DATA_DECORR," _
                                            & "MOROSITA_ANNI_PREC_DATA_DECORR," _
                                            & "CONTENZIOSI_DATA_DECORR," _
                                            & "RINNOVO_OK_DATA_DECORR" _
                                        & ") VALUES (" & (indicecontratto) & "," _
                                        & "" & Me.cmbReqGenerali.SelectedValue & ", " & Me.cmbReqGenEsito.SelectedValue & ", '" & par.AggiustaData(Me.TxtDataReqGenerali.Text) & "' ,'" & par.PulisciStrSql(Me.txtNoteReqGenerali.Text) & "', " _
                                        & "" & Me.CmbMorosita.SelectedValue & ", '" & par.AggiustaData(Me.TxtDataMorosita.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteMorosita.Text) & "', " & Me.CmbStatoAbitativo.SelectedValue & ", " _
                                        & "'" & par.AggiustaData(Me.TxtDataStatoAbitativo.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteStatoAbitativo.Text) & "', " & Me.CmbContrAnagr.SelectedValue & "," & Me.CmbContAnagEsito.SelectedValue & ", " _
                                        & "'" & par.AggiustaData(Me.TxtDataContAnag.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteContAnag.Text) & "', " _
                                        & "'" & par.PulisciStrSql(Me.TxtNoteMesseInMora.Text) & "', " & Me.CmbContGiudiziari.SelectedValue & ", '" & par.AggiustaData(Me.TxtDataContGiudiziari.Text) & "', '" & par.PulisciStrSql(Me.TxtNoteContGiudiz.Text) & "', " _
                                        & "" & Me.CmbRinnovoAmmissibile.SelectedValue & ", '" & par.AggiustaData(Me.TxtRinnovoData.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentReqGenerali.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentMorosita.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentStatoAbitativo.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentContAnag.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentContGiudiz.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentRinnovo.Text) & "'," _
                                        & "'" & par.PulisciStrSql(Me.TxtNumIdentInMora.Text) & "'," _
                                        & "'" & par.AggiustaData(Me.TxtDataDecorrReqGen.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrMorosita.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrStatoAbit.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrContAnag.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrMesseInMora.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtDataDecorrContGiudiz.Text) & "'," _
                                            & "'" & par.AggiustaData(Me.TxtRinnovoDataDecorr.Text) & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    ' Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '& "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '& "'F23','')"
                    'par.cmd.ExecuteNonQuery()
                End If

            Else
                ' Response.Write("<script>alert('ERRORE!');</script>")
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub Update()
        Try

            '*********************APERTURA CONNESSIONE**********************
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If

            If Me.CmbRinnovoAmmissibile.SelectedValue <> "0" And Me.CmbRinnovoAmmissibile.SelectedValue <> "" Then
                'If Me.cmbReqGenEsito.SelectedValue = "1" AndAlso Me.CmbMorosita.SelectedValue = "0" AndAlso Me.CmbStatoAbitativo.SelectedValue = "1" AndAlso Me.CmbContAnagEsito.SelectedValue = "1" AndAlso Me.CmbContGiudiziari.SelectedValue = "0" Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO SET REQ_GENERALI =" & Me.cmbReqGenerali.SelectedValue & ", REQ_GENERALI_ESITO = " & Me.cmbReqGenEsito.SelectedValue & ", REQ_GENERALI_DATA='" & par.AggiustaData(Me.TxtDataReqGenerali.Text) & "', " _
                                        & "REQ_GENERALI_NOTE='" & par.PulisciStrSql(Me.txtNoteReqGenerali.Text) & "', MOROSITA_PREGR=" & Me.CmbMorosita.SelectedValue & ", MOROSITA_PREGR_DATA='" & par.AggiustaData(Me.TxtDataMorosita.Text) & "', MOROSITA_PREGR_NOTE='" & par.PulisciStrSql(Me.TxtNoteMorosita.Text) & "', REG_STATO_ABITATIVO=" & Me.CmbStatoAbitativo.SelectedValue & ", REG_STATO_ABITATIVO_DATA='" & par.AggiustaData(Me.TxtDataStatoAbitativo.Text) & "', " _
                                        & "REG_STATO_ABITATIVO_NOTE='" & par.PulisciStrSql(Me.TxtNoteStatoAbitativo.Text) & "', CONTROLLO_ANAGRA=" & Me.CmbContrAnagr.SelectedValue & ", CONTROLLO_ANAGRA_ESITO=" & Me.CmbContAnagEsito.SelectedValue & ", CONTROLLO_ANAGRA_DATA='" & par.AggiustaData(Me.TxtDataContAnag.Text) & "',CONTROLLO_ANAGRA_NOTE='" & par.PulisciStrSql(Me.TxtNoteContAnag.Text) & "', " _
                                        & "MOROSITA_ANNI_NOTE='" & par.PulisciStrSql(Me.TxtNoteMesseInMora.Text) & "',  CONTENZIOSI=" & Me.CmbContGiudiziari.SelectedValue & ", CONTENZIOSI_DATA='" & par.AggiustaData(Me.TxtDataContGiudiziari.Text) & "', CONTENZIOSI_NOTE='" & par.PulisciStrSql(Me.TxtNoteContGiudiz.Text) & "', RINNOVO_OK=" & Me.CmbRinnovoAmmissibile.SelectedValue & ", RINNOVO_OK_DATA='" & par.AggiustaData(Me.TxtRinnovoData.Text) & "', " _
                                        & "REG_GENERALI_NUM_ID        ='" & par.PulisciStrSql(Me.TxtNumIdentReqGenerali.Text) & "'," _
                                        & "MOROSITA_PREGR_NUM_ID      ='" & par.PulisciStrSql(Me.TxtNumIdentMorosita.Text) & "'," _
                                        & "REG_STATO_ABITATIVO_NUM_ID ='" & par.PulisciStrSql(Me.TxtNumIdentStatoAbitativo.Text) & "'," _
                                        & "CONTROLLO_ANAGRA_NUM_ID    ='" & par.PulisciStrSql(Me.TxtNumIdentContAnag.Text) & "'," _
                                        & "CONTENZIOSI_NUM_ID         ='" & par.PulisciStrSql(Me.TxtNumIdentContGiudiz.Text) & "'," _
                                        & "RINNOVO_OK_NUM_ID          ='" & par.PulisciStrSql(Me.TxtNumIdentRinnovo.Text) & "'," _
                                        & "MOROSITA_ANNI_PREC_NUM_ID  ='" & par.PulisciStrSql(Me.TxtNumIdentInMora.Text) & "'," _
                                        & "REQ_GENERALI_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrReqGen.Text) & "'," _
                                        & "MOROSITA_PREGR_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrMorosita.Text) & "'," _
                                        & "REG_STATO_ABIT_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrStatoAbit.Text) & "'," _
                                        & "CONTROLLO_ANAGRA_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrContAnag.Text) & "'," _
                                        & "MOROSITA_ANNI_PREC_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrMesseInMora.Text) & "'," _
                                        & "CONTENZIOSI_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrContGiudiz.Text) & "'," _
                                        & "RINNOVO_OK_DATA_DECORR ='" & par.AggiustaData(Me.TxtRinnovoDataDecorr.Text) & "' " _
                                        & " WHERE ID_CONTRATTO= " & indicecontratto

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                'Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            Else
                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO SET REQ_GENERALI =" & Me.cmbReqGenerali.SelectedValue & ", REQ_GENERALI_ESITO = " & Me.cmbReqGenEsito.SelectedValue & ", REQ_GENERALI_DATA='" & par.AggiustaData(Me.TxtDataReqGenerali.Text) & "', " _
                                    & "REQ_GENERALI_NOTE='" & par.PulisciStrSql(Me.txtNoteReqGenerali.Text) & "', MOROSITA_PREGR=" & Me.CmbMorosita.SelectedValue & ", MOROSITA_PREGR_DATA='" & par.AggiustaData(Me.TxtDataMorosita.Text) & "', MOROSITA_PREGR_NOTE='" & par.PulisciStrSql(Me.TxtNoteMorosita.Text) & "', REG_STATO_ABITATIVO=" & Me.CmbStatoAbitativo.SelectedValue & ", REG_STATO_ABITATIVO_DATA='" & par.AggiustaData(Me.TxtDataStatoAbitativo.Text) & "', " _
                                    & "REG_STATO_ABITATIVO_NOTE='" & par.PulisciStrSql(Me.TxtNoteStatoAbitativo.Text) & "', CONTROLLO_ANAGRA='" & Me.CmbContrAnagr.SelectedValue & "', CONTROLLO_ANAGRA_ESITO=" & Me.CmbContAnagEsito.SelectedValue & ", CONTROLLO_ANAGRA_DATA='" & par.AggiustaData(Me.TxtDataContAnag.Text) & "',CONTROLLO_ANAGRA_NOTE='" & par.PulisciStrSql(Me.TxtNoteContAnag.Text) & "', " _
                                    & "MOROSITA_ANNI_NOTE='" & par.PulisciStrSql(Me.TxtNoteMesseInMora.Text) & "',  CONTENZIOSI=" & Me.CmbContGiudiziari.SelectedValue & ", CONTENZIOSI_DATA='" & par.AggiustaData(Me.TxtDataContGiudiziari.Text) & "', CONTENZIOSI_NOTE='" & par.PulisciStrSql(Me.TxtNoteContGiudiz.Text) & "', RINNOVO_OK=" & Me.CmbRinnovoAmmissibile.SelectedValue & ", RINNOVO_OK_DATA='" & par.AggiustaData(Me.TxtRinnovoData.Text) & "'," _
                                    & "REG_GENERALI_NUM_ID        ='" & par.PulisciStrSql(Me.TxtNumIdentReqGenerali.Text) & "'," _
                                    & "MOROSITA_PREGR_NUM_ID      ='" & par.PulisciStrSql(Me.TxtNumIdentMorosita.Text) & "'," _
                                    & "REG_STATO_ABITATIVO_NUM_ID ='" & par.PulisciStrSql(Me.TxtNumIdentStatoAbitativo.Text) & "'," _
                                    & "CONTROLLO_ANAGRA_NUM_ID    ='" & par.PulisciStrSql(Me.TxtNumIdentContAnag.Text) & "'," _
                                    & "CONTENZIOSI_NUM_ID         ='" & par.PulisciStrSql(Me.TxtNumIdentContGiudiz.Text) & "'," _
                                    & "RINNOVO_OK_NUM_ID          ='" & par.PulisciStrSql(Me.TxtNumIdentRinnovo.Text) & "'," _
                                    & "MOROSITA_ANNI_PREC_NUM_ID  ='" & par.PulisciStrSql(Me.TxtNumIdentInMora.Text) & "'," _
                                     & "REQ_GENERALI_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrReqGen.Text) & "'," _
                                        & "MOROSITA_PREGR_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrMorosita.Text) & "'," _
                                        & "REG_STATO_ABIT_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrStatoAbit.Text) & "'," _
                                        & "CONTROLLO_ANAGRA_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrContAnag.Text) & "'," _
                                        & "MOROSITA_ANNI_PREC_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrMesseInMora.Text) & "'," _
                                        & "CONTENZIOSI_DATA_DECORR ='" & par.AggiustaData(Me.TxtDataDecorrContGiudiz.Text) & "'," _
                                        & "RINNOVO_OK_DATA_DECORR ='" & par.AggiustaData(Me.TxtRinnovoDataDecorr.Text) & "'" _
                                    & " WHERE ID_CONTRATTO= " & indicecontratto
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                'Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Public Sub ImgButSave()
        If modify.Value = "1" Then
            par.OracleConn = CType(HttpContext.Current.Session.Item(indiceconnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & indiceconnessione), Oracle.DataAccess.Client.OracleTransaction)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO WHERE ID_CONTRATTO = " & indicecontratto

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read Then
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'myReader.Close()
                Update()
                MemorizzaAttributi()
            Else
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                myReader.Close()

                Salva()
            End If

            CaricaAttributi()
            ControllaAzLegali()

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('Tab_Azioni_Legali1_modify').value='0';", True)
        End If
    End Sub

    Protected Sub cmbReqGenerali_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbReqGenerali.SelectedIndexChanged
        If CStr(par.IfEmpty(cmbReqGenerali.SelectedValue, 0)) <> "NULL" Then
            Me.cmbReqGenEsito.Enabled = True
            cmbReqGenEsito.Items.Clear()
            cmbReqGenEsito.Items.Add(New ListItem("IN CORSO", "0"))
            cmbReqGenEsito.Items.Add(New ListItem("ARCHIVIATO", "1"))
            cmbReqGenEsito.Items.Add(New ListItem("EMESSO DECRETO", "2"))
        Else
            Me.cmbReqGenEsito.Enabled = False
            cmbReqGenEsito.Items.Clear()
            cmbReqGenEsito.Items.Add(New ListItem("- - -", "NULL"))
            cmbReqGenEsito.Items.Add(New ListItem("IN CORSO", "0"))
            cmbReqGenEsito.Items.Add(New ListItem("ARCHIVIATO", "1"))
            cmbReqGenEsito.Items.Add(New ListItem("EMESSO DECRETO", "2"))
        End If
    End Sub

    Protected Sub CmbContrAnagr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbContrAnagr.SelectedIndexChanged
        If Me.CmbContrAnagr.SelectedValue = "1" Then
            Me.CmbContAnagEsito.Enabled = True
        Else
            Me.CmbContAnagEsito.Enabled = False
        End If
    End Sub


    Private Sub ControllaAzLegali()
        Dim ConnAperta As Boolean = False
        Dim decretoDecadenza As Integer = 0
        Dim intestMononucleoDeceduto As Integer = 0

        Try
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    ConnAperta = True
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_CONTROLLO WHERE ID_CONTRATTO =" & indicecontratto
            Dim myReaderAZLeg As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderAZLeg.Read Then
                If par.IfNull(myReaderAZLeg("MOROSITA_PREGR"), 0) = 1 Then
                    decretoDecadenza = 1
                End If
                If par.IfNull(myReaderAZLeg("CONTROLLO_ANAGRA_ESITO"), 0) = 1 Then
                    intestMononucleoDeceduto = 1
                End If
            End If
            myReaderAZLeg.Close()

            If decretoDecadenza = 1 Then
                par.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET MOTIVAZIONE='EMISSIONE DECRETO DECADENZA',ID_LISTA_CONV=NULL WHERE ID_CONTRATTO=" & indicecontratto & " AND ID_LISTA = (SELECT MAX(ID_LISTA) FROM UTENZA_LISTE_CDETT ULCD WHERE UTENZA_LISTE_CDETT.ID_CONTRATTO=ULCD.ID_CONTRATTO )"
                par.cmd.ExecuteNonQuery()
            End If
            If intestMononucleoDeceduto = 1 Then
                par.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET MOTIVAZIONE='TITOLARE MONONUCLEO DECEDUTO',ID_LISTA_CONV=NULL WHERE ID_CONTRATTO=" & indicecontratto & " AND ID_LISTA = (SELECT MAX(ID_LISTA) FROM UTENZA_LISTE_CDETT ULCD WHERE UTENZA_LISTE_CDETT.ID_CONTRATTO=ULCD.ID_CONTRATTO )"
                par.cmd.ExecuteNonQuery()
            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione ControllaProroga) - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

End Class
