Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class ANAUT_max
    Inherits PageSetIdMode
    Dim lValoreCorrente As Long
    Dim sAnnoIsee As String
    Dim sAnnoCanone As String
    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Public Property annoRedd() As String
        Get
            If Not (ViewState("par_annoRedd") Is Nothing) Then
                Return CStr(ViewState("par_annoRedd"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_annoRedd") = value
        End Set

    End Property


    Function VisualizzaDichiarazione()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim CTT As Label
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long

        Dim RESIDENZA As String
        Dim SOMMA As Long
        Dim DESCRIZIONE As String = ""
        Dim i As Integer
        Dim MIOPROGR As Integer
        Dim scriptblock As String
        Dim CodiceUnita As String = ""


        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)


            Dim canoneApplicato As Boolean = False
            Dim idContratto As Long = 0

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ART_15,UTENZA_DICHIARAZIONI.RAPPORTO,UTENZA_BANDI.DATA_INIZIO,UTENZA_BANDI.STATO,UTENZA_DICHIARAZIONI.ID_BANDO,utenza_dichiarazioni.DATA_PG,UTENZA_DICHIARAZIONI.FL_SOSPENSIONE FROM UTENZA_BANDI,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=" & lIdDichiarazione & " AND UTENZA_DICHIARAZIONI.ID_BANDO=UTENZA_BANDI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If Request.QueryString("CR") = "1" And (myReader("art_15") = "1" Or Request.QueryString("ASST") = "1") Then

                Else
                    If myReader("STATO") <> 1 Then
                        btnSalva.Visible = False
                        imgStampa.Visible = False
                        Dic_Utenza1.DisattivaTutto()
                        Dic_Integrazione1.DisattivaTutto()
                        Dic_Note1.DisattivaTutto()
                        Dic_Nucleo1.DisattivaTutto()
                        Dic_Patrimonio1.DisattivaTutto()
                        Dic_Reddito1.DisattivaTutto()
                        Dic_Reddito_Conv1.DisattivaTutto()
                        cmbStato.Enabled = False
                        cmbAnnoReddituale.Enabled = False
                        Dic_Documenti1.DisattivaTutto()
                        nonstampare.Value = "1"
                        Response.Write("<script>alert('Non è possibile apportare modifiche!');</script>")
                    End If

                    '15/05/2013 AGGIUNTO CONTROLLO PER BLOCCARE LE DICHIARAZIONI PER CUI IL CANONE NON RISULTA APPLICATO (non presente in canoni_ec)
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & par.IfNull(myReader("RAPPORTO"), "") & "'"
                    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderID.Read Then
                        idContratto = par.IfNull(myReaderID("ID"), "")
                    End If
                    myReaderID.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE ID_BANDO_AU=" & par.IfNull(myReader("ID_BANDO"), 0) & " AND ID_CONTRATTO=" & idContratto
                    Dim myReaderAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAU.Read Then
                        canoneApplicato = True
                    Else
                        canoneApplicato = False
                    End If
                    myReaderAU.Close()

                    If myReader("STATO") = "1" Or myReader("STATO") = "2" Then
                        If canoneApplicato = True Then
                            'If par.IfNull(myReader("FL_SOSPENSIONE"), "0") = "0" Then
                            btnSalva.Visible = False
                            imgStampa.Visible = False
                            Dic_Utenza1.DisattivaTutto()
                            Dic_Integrazione1.DisattivaTutto()
                            Dic_Note1.DisattivaTutto()
                            Dic_Nucleo1.DisattivaTutto()
                            Dic_Patrimonio1.DisattivaTutto()
                            Dic_Reddito1.DisattivaTutto()
                            Dic_Reddito_Conv1.DisattivaTutto()
                            cmbStato.Enabled = False
                            cmbAnnoReddituale.Enabled = False
                            Dic_Documenti1.DisattivaTutto()
                            nonstampare.Value = "1"
                            Response.Write("<script>alert('Non è possibile apportare modifiche! Utilizzare la funzione di INSERIMENTO DOMANDA DI VERIFICA AU.');</script>")
                            'End If
                        End If
                    End If

                End If
            End If
            myReader.Close()

            '02/07/2012 per i contratti presenti in RAPPORTI_UTENZA_AU_ABUSIVI
            If Request.QueryString("CR") = "1" Then
                cmbAnnoReddituale.Enabled = False
            End If
            If SoloLettura = "1" Then
                btnSalva.Visible = False
                'Label5.Visible = False
                imgStampa.Visible = False
                'Label6.Visible = False

                Dic_Utenza1.DisattivaTutto()
                Dic_Integrazione1.DisattivaTutto()
                Dic_Note1.DisattivaTutto()
                Dic_Nucleo1.DisattivaTutto()
                Dic_Patrimonio1.DisattivaTutto()
                Dic_Reddito1.DisattivaTutto()
                Dic_Reddito_Conv1.DisattivaTutto()
                cmbStato.Enabled = False
                cmbAnnoReddituale.Enabled = False
                Dic_Documenti1.DisattivaTutto()
                nonstampare.Value = "1"
            End If
            'par.cmd.CommandText = "SELECT DICHIARAZIONI.ID_CAF,DOMANDE_BANDO.N_DISTINTA,CAF_WEB.COD_CAF FROM DOMANDE_BANDO,DICHIARAZIONI,CAF_WEB WHERE DICHIARAZIONI.ID_CAF=CAF_WEB.ID AND DICHIARAZIONI.ID=" & lIdDichiarazione & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    If Len(Session.Item("OPERATORE")) > 6 Then
            '        If UCase(Mid(Session.Item("OPERATORE"), 1, 6)) = "COMUNE" Then
            '            LBLENTE.Visible = True
            '            LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")

            '            If myReader("N_DISTINTA") = 0 And myReader("ID_CAF") <> Session.Item("ID_CAF") Then
            '                btnSalva.Visible = False
            '                Label5.Visible = False
            '                imgStampa.Visible = False
            '                Label6.Visible = False

            '                Dic_Utenza1.DisattivaTutto()
            '                Dic_Integrazione1.DisattivaTutto()
            '                Dic_Note1.DisattivaTutto()
            '                Dic_Nucleo1.DisattivaTutto()
            '                Dic_Patrimonio1.DisattivaTutto()
            '                Dic_Reddito1.DisattivaTutto()
            '                cmbStato.Enabled = False

            '                Response.Write("<script>alert('Non è possibile modificare. La domanda è ancora in carico presso un ente esterno!');</script>")
            '            End If
            '        Else
            '            If myReader("N_DISTINTA") > 0 Then
            '                btnSalva.Visible = False
            '                Label5.Visible = False
            '                imgStampa.Visible = False
            '                Label6.Visible = False

            '                Dic_Utenza1.DisattivaTutto()
            '                Dic_Integrazione1.DisattivaTutto()
            '                Dic_Note1.DisattivaTutto()
            '                Dic_Nucleo1.DisattivaTutto()
            '                Dic_Patrimonio1.DisattivaTutto()
            '                Dic_Reddito1.DisattivaTutto()
            '                cmbStato.Enabled = False

            '                Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " !');</script>")
            '            End If
            '        End If
            '    Else
            '        If myReader("N_DISTINTA") > 0 Then
            '            btnSalva.Visible = False
            '            Label5.Visible = False
            '            imgStampa.Visible = False
            '            Label6.Visible = False

            '            Dic_Utenza1.DisattivaTutto()
            '            Dic_Integrazione1.DisattivaTutto()
            '            Dic_Note1.DisattivaTutto()
            '            Dic_Nucleo1.DisattivaTutto()
            '            Dic_Patrimonio1.DisattivaTutto()
            '            Dic_Reddito1.DisattivaTutto()
            '            cmbStato.Enabled = False

            '            Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " !');</script>")
            '        End If
            '    End If
            'Else
            '    If Len(Session.Item("OPERATORE")) > 6 Then
            '        If UCase(Mid(Session.Item("OPERATORE"), 1, 6)) = "COMUNE" Then

            '            btnSalva.Visible = False
            '            Label5.Visible = False
            '            imgStampa.Visible = False
            '            Label6.Visible = False

            '            Dic_Utenza1.DisattivaTutto()
            '            Dic_Integrazione1.DisattivaTutto()
            '            Dic_Note1.DisattivaTutto()
            '            Dic_Nucleo1.DisattivaTutto()
            '            Dic_Patrimonio1.DisattivaTutto()
            '            Dic_Reddito1.DisattivaTutto()
            '            cmbStato.Enabled = False

            '            Response.Write("<script>alert('Non è possibile modificare. La domanda NON è stata ancora creata!');</script>")

            '        End If
            '    End If
            'End If
            'myReader.Close()

            txtDataPG.Text = Format(Now, "dd/MM/yyyy")
            Dim lsiFrutto As New ListItem("DA COMPLETARE", "0")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("COMPLETA", "1")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("DA CANCELLARE", "2")
            cmbStato.Items.Add(lsiFrutto)

            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            'Modifiche Anagrafe Utenza
            'par.RiempiDList(Dic_Patrimonio1, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")



            par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM UTENZA_EVENTI_DICHIARAZIONI WHERE COD_EVENTO='F132' AND ID_PRATICA=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader(0) > 0 Then
                    Session.Item("STAMPATO") = "1"
                Else
                    Session.Item("STAMPATO") = "0"
                End If
            Else
                Session.Item("STAMPATO") = "0"
            End If

            myReader.Close()



            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then

                par.cmd.CommandText = "select UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza where UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND cod_contratto='" & par.IfNull(myReader("RAPPORTO"), "0") & "'"
                Dim myReaderUN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderUN.Read Then
                    CodiceUnita = par.IfNull(myReaderUN("COD_UNITA_IMMOBILIARE"), "-1")
                End If
                myReaderUN.Close()


                If par.IfNull(myReader("ART_15"), "0") = "1" Then
                    lblArt15.Visible = True
                End If
                lIndice_Bando = myReader("ID_BANDO")
                Session.Add("idBandoAU", lIndice_Bando)
                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                CType(Dic_Utenza1.FindControl("txtId"), HiddenField).Value = lIdDichiarazione
                CType(Dic_Utenza1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))

                CType(Dic_Utenza1.FindControl("txtDataCessazione"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_cessazione"), ""))
                CType(Dic_Utenza1.FindControl("txtDataDec"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_decorrenza"), ""))

                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                lotto45.Value = "0"
                If par.IfNull(myReader("FL_4_5_LOTTO"), 0) = 1 Then
                    CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = True
                    CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Text = par.IfNull(myReader("COD_CONVOCAZIONE"), "")
                    CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Visible = True
                    lotto45.Value = "1"
                Else
                    CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Enabled = False
                    CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Enabled = False
                End If

                If CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = "2006" Then
                    imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;return confirm('ATTENZIONE..Stai elaborando utilizzando il 2006 come anno fiscale, SENZA TENERE CONTO di quanto previsto dalla LG 36/2008. Proseguire?');")
                    cmbAnnoReddituale.Items.FindByValue("2006").Selected = True
                    txt36.Value = "0"
                    CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA 2006"
                Else
                    cmbAnnoReddituale.SelectedIndex = -1
                    cmbAnnoReddituale.Items.FindByValue(CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text).Selected = True
                    cmbAnnoReddituale.Enabled = False

                    Select Case CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text
                        Case "2007"
                            rdApplica.Visible = True
                            rdNoApplica.Visible = True
                            lblApplica36.Visible = True
                            CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA 2007"
                            imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;if (document.getElementById('txt36').value==0) {return confirm('ATTENZIONE..Stai elaborando utilizzando il 2007 come anno fiscale, SENZA TENERE CONTO di quanto previsto dalla LG 36/2008. Proseguire?');} else {return confirm('ATTENZIONE..Stai elaborando utilizzando il 2007 come anno fiscale, TENENDO CONTO di quanto previsto dalla LG 36/2008. Proseguire?');}")
                            If par.IfNull(myReader("FL_APPLICA_36"), "0") = "0" Then
                                rdNoApplica.Checked = True
                                rdApplica.Checked = False
                            Else
                                rdApplica.Checked = True
                                rdNoApplica.Checked = False
                            End If
                        Case Else
                            rdApplica.Visible = True
                            rdNoApplica.Visible = True
                            lblApplica36.Visible = True
                            CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text
                            rdApplica.Checked = True
                            rdNoApplica.Checked = False
                            rdNoApplica.Enabled = False
                            lblApplica36.Enabled = False
                    End Select

                End If

                If CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = "2010" Then
                    cmbAnnoReddituale.Enabled = False
                End If

                annoRedd = cmbAnnoReddituale.SelectedItem.Value

                CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE_WEB"), "")

                CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).SelectedIndex = -1
                CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).Items.FindByValue(par.IfNull(myReader("TIPO_DOCUMENTO"), 0)).Selected = True
                CType(Dic_Note1.FindControl("txtCINum"), TextBox).Text = par.IfNull(myReader("CARTA_I"), "")
                CType(Dic_Note1.FindControl("txtCIData"), TextBox).Text = par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), ""))
                CType(Dic_Note1.FindControl("txtCSData"), TextBox).Text = par.FormattaData(par.IfNull(myReader("CARTA_SOGG_DATA"), ""))
                CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text = par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_SCADE"), ""))
                CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text = par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_RINNOVO"), ""))
                CType(Dic_Note1.FindControl("txtPSData"), TextBox).Text = par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_DATA"), ""))
                CType(Dic_Note1.FindControl("txtCIRilascio"), TextBox).Text = par.IfNull(myReader("CARTA_I_RILASCIATA"), "")
                CType(Dic_Note1.FindControl("txtPSNum"), TextBox).Text = par.IfNull(myReader("PERMESSO_SOGG_N"), "")
                CType(Dic_Note1.FindControl("txtCSNum"), TextBox).Text = par.IfNull(myReader("CARTA_SOGG_N"), "")
                If par.IfNull(myReader("FL_NATO_ESTERO"), "0") = "1" Then
                    CType(Dic_Note1.FindControl("ChNatoEstero"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_CITTADINO_IT"), "0") = "1" Then
                    CType(Dic_Note1.FindControl("ChCittadinanza"), CheckBox).Checked = True
                End If

                CType(Dic_Note1.FindControl("cmbLavorativa"), DropDownList).SelectedIndex = -1
                CType(Dic_Note1.FindControl("cmbLavorativa"), DropDownList).Items.FindByValue(par.IfNull(myReader("fl_attivita_lav"), 0)).Selected = True





                CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))

                'Modifiche Anagrafe Utenza
                'CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedIndex = -1
                'CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Items.FindByValue(par.IfNull(myReader("ID_TIPO_CAT_AB"), "0")).Selected = True
                'CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedIndex = -1
                'CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Items.FindByValue(par.IfNull(myReader("POSSESSO_UI"), "-1")).Selected = True

                'If par.IfNull(myReader("FL_UBICAZIONE"), "0") = "0" Then
                '    CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = False
                'Else
                '    CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = True
                'End If

                'If par.IfNull(myReader("POSSESSO_UI"), "-1") <> "-1" Then
                '    CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = True
                '    CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Enabled = True
                'Else
                '    CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = False
                '    CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Enabled = False
                'End If
                'If par.IfNull(myReader("ID_TIPO_CAT_AB"), "-1") <> "-1" Then
                '    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True
                'Else
                '    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = False
                'End If

                cT = Dic_Utenza1.FindControl("txtCAPRes")
                cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                cT = Dic_Utenza1.FindControl("txtIndRes")
                cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                cT = Dic_Utenza1.FindControl("txtCivicoRes")
                cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                cT = Dic_Utenza1.FindControl("txtTelRes")
                cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")

                'If par.IfNull(myReader("FL_GIA_TITOLARE"), "0") = "0" Then
                '    CType(Dic_Utenza1.FindControl("chTitolare"), CheckBox).Checked = False
                'Else
                '    CType(Dic_Utenza1.FindControl("chTitolare"), CheckBox).Checked = True
                'End If

                CType(Dic_Reddito_Conv1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader("minori_carico"), "0")
                CType(Dic_Utenza1.FindControl("txtFoglio"), TextBox).Text = par.IfNull(myReader("FOGLIO"), "")
                CType(Dic_Utenza1.FindControl("txtMappale"), TextBox).Text = par.IfNull(myReader("MAPPALE"), "")
                CType(Dic_Utenza1.FindControl("txtSub"), TextBox).Text = par.IfNull(myReader("SUB"), "")
                CType(Dic_Utenza1.FindControl("txtCodAlloggio"), TextBox).Text = par.IfNull(myReader("COD_ALLOGGIO"), "")



                CType(Dic_Utenza1.FindControl("txtScala"), TextBox).Text = par.IfNull(myReader("scala"), "")
                CType(Dic_Utenza1.FindControl("txtPiano"), TextBox).Text = par.IfNull(myReader("piano"), "")
                CType(Dic_Utenza1.FindControl("txtAlloggio"), TextBox).Text = par.IfNull(myReader("alloggio"), "")
                'CType(Dic_Utenza1.FindControl("txtCodAlloggio"), TextBox).Text = par.IfNull(myReader("COD_alloggio"), "")

                CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text = par.IfNull(myReader("RAPPORTO"), "")
                CType(Dic_Utenza1.FindControl("txtRapportoReale"), TextBox).Text = par.IfNull(myReader("RAPPORTO_REALE"), "")
                CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Text = par.IfNull(myReader("POSIZIONE"), "")

                CType(Dic_Utenza1.FindControl("imgPatrimonio"), System.Web.UI.WebControls.Image).Attributes.Add("onclick", "if (document.getElementById('Dic_Utenza1_TxtRapporto').value != '') {window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & CodiceUnita & "','Dettagli','height=580,top=0,left=0,width=780');} else {alert('Codice unità non valido!');}")

                If par.IfNull(myReader("TIPO_ASS"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("R2"), RadioButton).Checked = True
                Else
                    CType(Dic_Utenza1.FindControl("R1"), RadioButton).Checked = True
                End If


                If par.IfNull(myReader("int_c"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("int_v"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("int_a"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("ch3"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("ch3"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("int_M"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_TUTORE"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("ch5"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("ch5"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_DELEGATO"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("ch6"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("ch6"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_RIC_POSTA"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("chPosta"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("chPosta"), CheckBox).Checked = True
                End If



                '**** FLAG FF.OO. IN SERVIZIO ****
                Dim item As MenuItem
                If par.IfNull(myReader("FL_IN_SERVIZIO"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("chInServizio"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("chInServizio"), CheckBox).Checked = True

                    item = New MenuItem("Autocert.stato di servizio", "AutocertStServ", "", "javascript:AutocertStServ();")
                    MenuStampe.Items(0).ChildItems.AddAt(MenuStampe.Items(0).ChildItems.Count - 1, item)
                End If

                If par.IfNull(myReader("FL_SOSP_1"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp1"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp1"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_2"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp2"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp2"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_3"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp3"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp3"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_4"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp4"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp4"), CheckBox).Checked = True
                End If


                If par.IfNull(myReader("FL_SOSP_5"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp5"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp5"), CheckBox).Checked = True
                End If


                If par.IfNull(myReader("FL_SOSP_6"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp6"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp6"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_7"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("Sosp7"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("Sosp7"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_DA_VERIFICARE"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSPENSIONE"), "0") = "0" Then
                    CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = False
                Else
                    CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = True
                End If


                lblISEE.Text = par.IfNull(myReader("isee"), "0,00")
                ISEE_DICHIARAZIONE = lblISEE.Text


                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Utenza1.FindControl("cmbNazioneNas")
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        CT1 = Dic_Utenza1.FindControl("cmbPrNas")
                        CT1.Visible = False
                        CT1 = Dic_Utenza1.FindControl("cmbComuneNas")
                        CT1.Visible = False
                        CTT = Dic_Utenza1.FindControl("label6")
                        CTT.Visible = False
                        CTT = Dic_Utenza1.FindControl("label7")
                        CTT.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Utenza1.FindControl("cmbPrNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Utenza1.FindControl("cmbComuneNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    End If
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Utenza1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader1("DESCRIZIONE")).Selected = True
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Utenza1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Utenza1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Utenza1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True

                        'cT = Dic_Utenza1.FindControl("txtCAPRes")
                        'cT.Text = myReader1("CAP")
                    End If
                End If
                myReader1.Close()
            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    lblDomAssociata.Text = par.IfNull(myReader("PG"), "")
            'End If
            'myReader.Close()
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
                CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
                CType(Dic_Utenza1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader("COD_FISCALE"), "")
                CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then

            '    CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

            '    CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
            '    CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
            '    CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NAS"), ""))

            '    cT = Dic_Sottoscrittore1.FindControl("txtIndRes")
            '    cT.Text = par.IfNull(myReader("IND"), "")

            '    cT = Dic_Sottoscrittore1.FindControl("txtCivicoRes")
            '    cT.Text = par.IfNull(myReader("CIVICO"), "")

            '    cT = Dic_Sottoscrittore1.FindControl("txtTelRes")
            '    cT.Text = par.IfNull(myReader("TELEFONO"), "")

            '    cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
            '    cT.Text = par.IfNull(myReader("CAP_RES"), "")

            '    lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS")
            '    lIndiceAppoggio_1 = myReader("ID_LUOGO_RES")
            '    lIndiceAppoggio_2 = myReader("ID_TIPO_IND")
            'End If
            'myReader.Close()

            'par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
            '    CT1.SelectedIndex = -1
            '    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
            '        CT1.Items.FindByText(myReader("NOME")).Selected = True

            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
            '        CT1.Visible = False
            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
            '        CT1.Visible = False
            '        CTT = Dic_Sottoscrittore1.FindControl("label6")
            '        CTT.Visible = False
            '        CTT = Dic_Sottoscrittore1.FindControl("label7")
            '        CTT.Visible = False
            '    Else
            '        CT1.Items.FindByText("ITALIA").Selected = True
            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
            '        CT1.SelectedIndex = -1
            '        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
            '        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
            '        CT1.SelectedIndex = -1
            '        CT1.Items.FindByText(myReader("NOME")).Selected = True
            '    End If
            'End If
            'myReader.Close()

            'par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    CT1 = Dic_Sottoscrittore1.FindControl("cmbTipoIRes")
            '    CT1.SelectedIndex = -1
            '    CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
            'End If
            'myReader.Close()

            'par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
            '    CT1.SelectedIndex = -1
            '    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
            '        CT1.Items.FindByText(myReader("NOME")).Selected = True

            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
            '        CT1.Visible = False
            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
            '        CT1.Visible = False
            '        CTT = Dic_Sottoscrittore1.FindControl("label10")
            '        CTT.Visible = False
            '        CTT = Dic_Sottoscrittore1.FindControl("label11")
            '        CTT.Visible = False

            '    Else
            '        CT1.Items.FindByText("ITALIA").Selected = True
            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
            '        CT1.SelectedIndex = -1
            '        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
            '        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
            '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
            '        CT1.SelectedIndex = -1
            '        CT1.Items.FindByText(myReader("NOME")).Selected = True

            '        'cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
            '        'cT.Text = myReader("CAP")
            '    End If
            'End If
            'myReader.Close()

            Dim MIAS As String
            Dim INDENNITA As String

            txtbinserito.Value = "1"
            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE FROM UTENZA_COMP_NUCLEO,T_TIPO_PARENTELA where UTENZA_COMP_NUCLEO.id_DICHIARAZIONE=" & lIdDichiarazione & " AND UTENZA_COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
                    INDENNITA = "SI"
                Else
                    INDENNITA = "NO"
                End If
                MIAS = ""
                MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
                CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
                If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                    CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
                End If
                CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader("PROGR") + 1

                SOMMA = 0
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    SOMMA = SOMMA + Val(myReader2("IMPORTO"))
                    DESCRIZIONE = par.IfNull(myReader2("DESCRIZIONE"), "")
                    MIOPROGR = myReader("PROGR")
                End While
                myReader2.Close()
                If SOMMA <> 0 Then
                    For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                        If MIOPROGR = CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Value Then
                            CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 17)
                            'CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(myReader2("IMPORTO"), 6) & ",00   " & par.MiaFormat(myReader2("DESCRIZIONE"), 17), myReader("PROGR")))
                        End If
                    Next
                End If

                par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    'CType(Dic_Reddito_Conv1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 35) & " " & par.MiaFormat(myReader2("CONDIZIONE"), 5) & " " & par.MiaFormat(myReader2("PROFESSIONE"), 5) & Chr(160) & Chr(160) & " " & par.MiaFormat(myReader2("DIPENDENTE"), 7) & ",00 " & par.MiaFormat(myReader2("PENSIONE"), 7) & ",00 " & par.MiaFormat(myReader2("AUTONOMO"), 7) & ",00 " & par.MiaFormat(myReader2("NON_IMPONIBILI"), 7) & ",00 " & par.MiaFormat(myReader2("occasionali"), 7) & ",00 " & par.MiaFormat(myReader2("DOM_AG_FAB"), 7) & ",00 " & par.MiaFormat(myReader2("ONERI"), 7) & ",00 ", myReader("PROGR")))
                    CType(Dic_Reddito_Conv1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 35) & " " & par.MiaFormat("", 5) & " " & par.MiaFormat("", 5) & Chr(160) & Chr(160) & " " & par.MiaFormat(par.IfNull(myReader2("DIPENDENTE"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("PENSIONE"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("AUTONOMO"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("NON_IMPONIBILI"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("occasionali"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("DOM_AG_FAB"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("ONERI"), ""), 7) & "    ", myReader("PROGR")))
                End While
                myReader2.Close()





                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(myReader2("COD_INTERMEDIARIO"), 27) & " " & par.MiaFormat(myReader2("INTERMEDIARIO"), 16) & " " & par.MiaFormat(myReader2("IMPORTO"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()


                Dim PienaP As String = ""

                par.cmd.CommandText = "SELECT UTENZA_COMP_PATR_IMMOB.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,UTENZA_COMP_PATR_IMMOB WHERE UTENZA_COMP_PATR_IMMOB.ID_COMPONENTE=" & myReader("ID") & " and UTENZA_COMP_PATR_IMMOB.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY UTENZA_COMP_PATR_IMMOB.ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    PienaP = ""
                    If par.IfNull(myReader2("PIENA_PROPRIETA"), "0") = "1" Then
                        PienaP = Chr(160) & Chr(160) & Chr(160) & Chr(160) & "SI"
                    Else
                        PienaP = Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160)
                    End If
                    CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & par.MiaFormat(RESIDENZA, 2) & " " & par.MiaFormat(par.IfNull(myReader2("cat_catastale"), "A01"), 3) & " " & par.MiaFormat(par.IfNull(myReader2("comune"), "MILANO"), 30) & " " & par.MiaFormat(par.IfNull(myReader2("N_VANI"), "0"), 2) & " " & par.MiaFormat(par.IfNull(myReader2("SUP_UTILE"), "0"), 7) & " " & PienaP & " " & par.MiaFormat(par.IfNull(myReader2("INDIRIZZO"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader2("CIVICO"), ""), 5) & " " & par.MiaFormat(par.IfNull(myReader2("REND_CATAST_DOMINICALE"), ""), 8) & ",00", myReader("PROGR")))
                    'Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbTipo.SelectedItem.Text, 20) & " " & par.MiaFormat(txtPerc.Text, 6) & " " & par.MiaFormat(Val(txtValore.Text), 8) & ",00 " & par.MiaFormat(Val(TxtMutuo.Text), 8) & ",00 " & cmbResidenza.SelectedItem.Text & " " & par.MiaFormat(cmbTipoImm.SelectedItem.Text, 3) & " " & par.MiaFormat(cmbComune.SelectedItem.Text, 30) & " " & par.MiaFormat(txtNumVani.Text, 2) & " " & par.MiaFormat(txtSupUtile.Text, 7)

                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 35) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 15) & ",00 " & par.MiaFormat(myReader2("PROV_AGRARI"), 15) & ",00", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 50) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT UTENZA_COMP_DETRAZIONI.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,UTENZA_COMP_DETRAZIONI WHERE UTENZA_COMP_DETRAZIONI.ID_COMPONENTE=" & myReader("id") & " and UTENZA_COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by UTENZA_comp_detrazioni.id_componente asc"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 30) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(myReader2("importo"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

            End While
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM UTENZA_DOC_MANCANTE WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY DESCRIZIONE ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                CType(Dic_Documenti1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 250), myReader("ID_DOC")))
            End While
            myReader.Close()

            '***** Controllo se il contratto appartiene o meno alle FFOO *****
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza where cod_contratto='" & par.IfNull(CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text, "") & "' AND PROVENIENZA_ASS=10"
            Dim myReaderFFOO As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderFFOO.Read Then
                FFOO = True
            End If
            myReaderFFOO.Close()

            If FFOO = True Then
                CType(Dic_Utenza1.FindControl("chInServizio"), CheckBox).Visible = True
            Else
                CType(Dic_Utenza1.FindControl("chInServizio"), CheckBox).Visible = False
            End If
            '***** FINE Controllo se il contratto appartiene o meno alle FFOO *****
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            '‘‘par.cmd.Transaction = par.myTrans



            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Session.Add("LAVORAZIONE", "1")

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader3.Read() Then
                    'lIndice_Bando = myReader3("ID_BANDO")
                    lblPG.Text = par.IfNull(myReader3("pg"), "")
                    txtDataPG.Text = par.FormattaData(par.IfNull(myReader3("data_pg"), ""))
                    CType(Dic_Utenza1.FindControl("txtId"), HiddenField).Value = lIdDichiarazione
                    CType(Dic_Utenza1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data"), ""))

                    CType(Dic_Utenza1.FindControl("txtDataCessazione"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data_Cessazione"), ""))
                    CType(Dic_Utenza1.FindControl("txtDataDec"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data_Decorrenza"), ""))

                    CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")
                    CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), ""))
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")

                    CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader3("NOTE_WEB"), "")
                    CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).SelectedIndex = -1
                    CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).Items.FindByValue(par.IfNull(myReader3("TIPO_DOCUMENTO"), 0)).Selected = True
                    CType(Dic_Note1.FindControl("txtCINum"), TextBox).Text = par.IfNull(myReader3("CARTA_I"), "")
                    CType(Dic_Note1.FindControl("txtCIData"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("CARTA_I_DATA"), ""))
                    CType(Dic_Note1.FindControl("txtCSData"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("CARTA_SOGG_DATA"), ""))
                    CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("PERMESSO_SOGG_SCADE"), ""))
                    CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("PERMESSO_SOGG_RINNOVO"), ""))
                    CType(Dic_Note1.FindControl("txtPSData"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("PERMESSO_SOGG_DATA"), ""))
                    CType(Dic_Note1.FindControl("txtCIRilascio"), TextBox).Text = par.IfNull(myReader3("CARTA_I_RILASCIATA"), "")
                    CType(Dic_Note1.FindControl("txtPSNum"), TextBox).Text = par.IfNull(myReader3("PERMESSO_SOGG_N"), "")
                    CType(Dic_Note1.FindControl("txtCSNum"), TextBox).Text = par.IfNull(myReader3("CARTA_SOGG_N"), "")

                    If par.IfNull(myReader3("FL_NATO_ESTERO"), "0") = "1" Then
                        CType(Dic_Note1.FindControl("ChNatoEstero"), CheckBox).Checked = True
                    Else
                        CType(Dic_Note1.FindControl("ChNatoEstero"), CheckBox).Checked = False
                    End If

                    If par.IfNull(myReader3("FL_CITTADINO_IT"), "0") = "1" Then
                        CType(Dic_Note1.FindControl("ChCittadinanza"), CheckBox).Checked = True
                    Else
                        CType(Dic_Note1.FindControl("ChCittadinanza"), CheckBox).Checked = False
                    End If


                    CType(Dic_Note1.FindControl("cmbLavorativa"), DropDownList).SelectedIndex = -1
                    CType(Dic_Note1.FindControl("cmbLavorativa"), DropDownList).Items.FindByValue(par.IfNull(myReader3("fl_attivita_lav"), 0)).Selected = True


                    CType(Dic_Reddito_Conv1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader3("minori_carico"), "0")
                    CType(Dic_Utenza1.FindControl("txtFoglio"), TextBox).Text = par.IfNull(myReader3("FOGLIO"), "")
                    CType(Dic_Utenza1.FindControl("txtMappale"), TextBox).Text = par.IfNull(myReader3("MAPPALE"), "")
                    CType(Dic_Utenza1.FindControl("txtSub"), TextBox).Text = par.IfNull(myReader3("SUB"), "")
                    CType(Dic_Utenza1.FindControl("txtCodAlloggio"), TextBox).Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")

                    CType(Dic_Utenza1.FindControl("txtDataCessazione"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data_cessazione"), ""))
                    CType(Dic_Utenza1.FindControl("txtDataDec"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data_decorrenza"), ""))
                    CType(Dic_Utenza1.FindControl("txtFoglio"), TextBox).Text = par.IfNull(myReader3("FOGLIO"), "")
                    CType(Dic_Utenza1.FindControl("txtMappale"), TextBox).Text = par.IfNull(myReader3("MAPPALE"), "")
                    CType(Dic_Utenza1.FindControl("txtSub"), TextBox).Text = par.IfNull(myReader3("SUB"), "")
                    CType(Dic_Utenza1.FindControl("txtCodAlloggio"), TextBox).Text = par.IfNull(myReader3("COD_ALLOGGIO"), "")



                    CType(Dic_Utenza1.FindControl("txtScala"), TextBox).Text = par.IfNull(myReader3("scala"), "")
                    CType(Dic_Utenza1.FindControl("txtPiano"), TextBox).Text = par.IfNull(myReader3("piano"), "")
                    CType(Dic_Utenza1.FindControl("txtAlloggio"), TextBox).Text = par.IfNull(myReader3("alloggio"), "")
                    'CType(Dic_Utenza1.FindControl("txtCodAlloggio"), TextBox).Text = par.IfNull(myReader("COD_alloggio"), "")

                    CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text = par.IfNull(myReader3("RAPPORTO"), "")
                    CType(Dic_Utenza1.FindControl("txtRapportoReale"), TextBox).Text = par.IfNull(myReader3("RAPPORTO_REALE"), "")
                    CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Text = par.IfNull(myReader3("POSIZIONE"), "")


                    CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_INT_ERP"), ""))


                    If par.IfNull(myReader3("TIPO_ASS"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("R2"), RadioButton).Checked = True
                    Else
                        CType(Dic_Utenza1.FindControl("R1"), RadioButton).Checked = True
                    End If


                    If par.IfNull(myReader3("int_c"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("int_v"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("int_a"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("ch3"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("ch3"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("int_M"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_TUTORE"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("ch5"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("ch5"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_DELEGATO"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("ch6"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("ch6"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_RIC_POSTA"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("chPosta"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("chPosta"), CheckBox).Checked = True
                    End If


                    If par.IfNull(myReader3("FL_SOSP_1"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp1"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp1"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_SOSP_2"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp2"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp2"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_SOSP_3"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp3"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp3"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_SOSP_4"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp4"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp4"), CheckBox).Checked = True
                    End If


                    If par.IfNull(myReader3("FL_SOSP_5"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp5"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp5"), CheckBox).Checked = True
                    End If


                    If par.IfNull(myReader3("FL_SOSP_6"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp6"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp6"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_SOSP_7"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("Sosp7"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("Sosp7"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_DA_VERIFICARE"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader3("FL_SOSPENSIONE"), "0") = "0" Then
                        CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = False
                    Else
                        CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = True
                    End If

                    'Modifiche Anagrafe Utenza
                    'CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedIndex = -1
                    'CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Items.FindByValue(par.IfNull(myReader3("ID_TIPO_CAT_AB"), "0")).Selected = True
                    'If par.IfNull(myReader3("ID_TIPO_CAT_AB"), "-1") <> "-1" Then
                    '    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True
                    '    CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = True
                    '    CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Enabled = True
                    'Else
                    '    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = False
                    '    CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = False
                    '    CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Enabled = False
                    'End If

                    cT = Dic_Utenza1.FindControl("txtCAPRes")
                    cT.Text = par.IfNull(myReader3("CAP_RES_DNTE"), "")

                    cT = Dic_Utenza1.FindControl("txtIndRes")
                    cT.Text = par.IfNull(myReader3("IND_RES_DNTE"), "")

                    cT = Dic_Utenza1.FindControl("txtCivicoRes")
                    cT.Text = par.IfNull(myReader3("CIVICO_RES_DNTE"), "")

                    cT = Dic_Utenza1.FindControl("txtTelRes")
                    cT.Text = par.IfNull(myReader3("TELEFONO_DNTE"), "")

                    lIndiceAppoggio_0 = myReader3("ID_LUOGO_NAS_DNTE")
                    lIndiceAppoggio_1 = myReader3("ID_LUOGO_RES_DNTE")
                    lIndiceAppoggio_2 = myReader3("ID_TIPO_IND_RES_DNTE")

                    'If par.IfNull(myReader3("FL_GIA_TITOLARE"), "0") = "0" Then
                    '    CType(Dic_Utenza1.FindControl("chTitolare"), CheckBox).Checked = False
                    'Else
                    '    CType(Dic_Utenza1.FindControl("chTitolare"), CheckBox).Checked = True
                    'End If

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dic_Utenza1.FindControl("cmbNazioneNas")
                        CT1.SelectedIndex = -1
                        If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True

                            CT1 = Dic_Utenza1.FindControl("cmbPrNas")
                            CT1.Visible = False
                            CT1 = Dic_Utenza1.FindControl("cmbComuneNas")
                            CT1.Visible = False
                            CTT = Dic_Utenza1.FindControl("label6")
                            CTT.Visible = False
                            CTT = Dic_Utenza1.FindControl("label7")
                            CTT.Visible = False

                        Else
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dic_Utenza1.FindControl("cmbPrNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dic_Utenza1.FindControl("cmbComuneNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        End If
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dic_Utenza1.FindControl("cmbTipoIRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("DESCRIZIONE")).Selected = True
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dic_Utenza1.FindControl("cmbNazioneRes")
                        CT1.SelectedIndex = -1
                        If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        Else
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dic_Utenza1.FindControl("cmbPrRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dic_Utenza1.FindControl("cmbComuneRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True

                            'cT = Dic_Utenza1.FindControl("txtCAPRes")
                            'cT.Text = myReader1("CAP")
                        End If
                    End If
                    myReader1.Close()
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then
                    CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader3("COGNOME"), "")
                    CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader3("NOME"), "")
                    CType(Dic_Utenza1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader3("COD_FISCALE"), "")
                    CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_NASCITA"), ""))
                End If
                myReader3.Close()

                'par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                'myReader3 = par.cmd.ExecuteReader()
                'If myReader3.Read() Then

                '    CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

                '    CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader3("COGNOME"), "")
                '    CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader3("NOME"), "")
                '    CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_NAS"), ""))

                '    cT = Dic_Sottoscrittore1.FindControl("txtIndRes")
                '    cT.Text = par.IfNull(myReader3("IND"), "")

                '    cT = Dic_Sottoscrittore1.FindControl("txtCivicoRes")
                '    cT.Text = par.IfNull(myReader3("CIVICO"), "")

                '    cT = Dic_Sottoscrittore1.FindControl("txtTelRes")
                '    cT.Text = par.IfNull(myReader3("TELEFONO"), "")

                '    lIndiceAppoggio_0 = myReader3("ID_LUOGO_NAS")
                '    lIndiceAppoggio_1 = myReader3("ID_LUOGO_RES")
                '    lIndiceAppoggio_2 = myReader3("ID_TIPO_IND")
                'End If
                'myReader3.Close()

                'par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                'myReader3 = par.cmd.ExecuteReader()
                'If myReader3.Read() Then
                '    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
                '    CT1.SelectedIndex = -1
                '    If myReader3("SIGLA") = "E" Or myReader3("SIGLA") = "C" Then
                '        CT1.Items.FindByText(myReader3("NOME")).Selected = True
                '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                '        CT1.Visible = False
                '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                '        CT1.Visible = False
                '        CTT = Dic_Sottoscrittore1.FindControl("label6")
                '        CTT.Visible = False
                '        CTT = Dic_Sottoscrittore1.FindControl("label7")
                '        CTT.Visible = False
                '    Else
                '        CT1.Items.FindByText("ITALIA").Selected = True
                '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                '        CT1.SelectedIndex = -1
                '        CT1.Items.FindByText(myReader3("SIGLA")).Selected = True
                '        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader3("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                '        CT1.SelectedIndex = -1
                '        CT1.Items.FindByText(myReader3("NOME")).Selected = True
                '    End If
                'End If
                'myReader3.Close()

                'par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                'myReader3 = par.cmd.ExecuteReader()
                'If myReader3.Read() Then
                '    CT1 = Dic_Sottoscrittore1.FindControl("cmbTipoIRes")
                '    CT1.SelectedIndex = -1
                '    CT1.Items.FindByText(myReader3("DESCRIZIONE")).Selected = True
                'End If
                'myReader3.Close()

                'par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                'myReader3 = par.cmd.ExecuteReader()
                'If myReader3.Read() Then
                '    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
                '    CT1.SelectedIndex = -1
                '    If myReader3("SIGLA") = "E" Or myReader3("SIGLA") = "C" Then
                '        CT1.Items.FindByText(myReader3("NOME")).Selected = True
                '    Else
                '        CT1.Items.FindByText("ITALIA").Selected = True
                '        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                '        CT1.SelectedIndex = -1
                '        CT1.Items.FindByText(myReader3("SIGLA")).Selected = True
                '        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader3("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                '        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                '        CT1.SelectedIndex = -1
                '        CT1.Items.FindByText(myReader3("NOME")).Selected = True

                '        cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                '        cT.Text = myReader3("CAP")
                '    End If
                'End If
                'myReader3.Close()

                Dim MIAS As String
                Dim INDENNITA As String

                txtbinserito.Value = "1"
                par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE FROM UTENZA_COMP_NUCLEO,T_TIPO_PARENTELA where UTENZA_COMP_NUCLEO.id_DICHIARAZIONE=" & lIdDichiarazione & " AND UTENZA_COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
                myReader3 = par.cmd.ExecuteReader()
                While myReader3.Read
                    If par.IfNull(myReader3("INDENNITA_ACC"), "0") = "1" Then
                        INDENNITA = "SI"
                    Else
                        INDENNITA = "NO"
                    End If
                    MIAS = ""
                    MIAS = par.MiaFormat(par.IfNull(myReader3("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader3("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader3("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader3("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader3("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
                    CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader3("PROGR")))
                    If par.IfNull(myReader3("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                        CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 52) & " ", myReader3("PROGR")))
                    End If
                    CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader3("PROGR") + 1

                    SOMMA = 0
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        SOMMA = SOMMA + Val(myReader2("IMPORTO"))
                        DESCRIZIONE = par.IfNull(myReader2("DESCRIZIONE"), "")
                        MIOPROGR = myReader3("PROGR")
                    End While
                    myReader2.Close()
                    If SOMMA <> 0 Then
                        For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                            If MIOPROGR = CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Value Then
                                CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 17)
                                'CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(myReader2("IMPORTO"), 6) & ",00   " & par.MiaFormat(myReader2("DESCRIZIONE"), 17), myReader("PROGR")))
                            End If
                        Next
                    End If


                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader3("COGNOME"), "") & "," & par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader2("COD_INTERMEDIARIO"), ""), 13) & " " & par.MiaFormat(par.IfNull(myReader2("INTERMEDIARIO"), ""), 30) & " " & par.MiaFormat(par.IfNull(myReader2("IMPORTO"), "0"), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT UTENZA_COMP_PATR_IMMOB.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,UTENZA_COMP_PATR_IMMOB WHERE UTENZA_COMP_PATR_IMMOB.ID_COMPONENTE=" & myReader3("ID") & " and UTENZA_COMP_PATR_IMMOB.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY UTENZA_COMP_PATR_IMMOB.ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        RESIDENZA = " "
                        If myReader2("F_RESIDENZA") = "0" Then
                            RESIDENZA = "NO"
                        Else
                            RESIDENZA = "SI"
                        End If
                        CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(par.IfNull(myReader2("VALORE"), "0")), 8) & ",00 " & par.MiaFormat(Val(par.IfNull(myReader2("MUTUO"), "0")), 8) & ",00 " & RESIDENZA, myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Reddito_Conv1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader3("COGNOME"), "") & "," & par.IfNull(myReader3("NOME"), ""), 35) & " " & par.MiaFormat("", 5) & " " & par.MiaFormat("", 5) & Chr(160) & Chr(160) & " " & par.MiaFormat(par.IfNull(myReader2("DIPENDENTE"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("PENSIONE"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("AUTONOMO"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("NON_IMPONIBILI"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("DOM_AG_FAB"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("OCCASIONALI"), "0"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("ONERI"), ""), 7) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 35) & " " & par.MiaFormat(par.IfNull(myReader2("REDDITO_IRPEF"), "0"), 15) & ",00 " & par.MiaFormat(par.IfNull(myReader2("PROV_AGRARI"), "0"), 15) & ",00", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 50) & " " & par.MiaFormat(Val(par.IfNull(myReader2("IMPORTO"), "0")), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT UTENZA_COMP_DETRAZIONI.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,UTENZA_COMP_DETRAZIONI WHERE UTENZA_COMP_DETRAZIONI.ID_COMPONENTE=" & myReader3("id") & " and UTENZA_COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by UTENZA_comp_detrazioni.id_componente asc"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 30) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(par.IfNull(myReader2("importo"), "0"), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                End While
                myReader3.Close()


                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Else
                Label4.Text = EX1.Message

            End If
            imgStampa.Enabled = False
            imgStampa.Visible = False
            btnSalva.Enabled = False
            btnSalva.Visible = False

            btnSalva.Visible = False
            Label5.Visible = False
            imgStampa.Visible = False
            Label6.Visible = False

            Dic_Utenza1.DisattivaTutto()
            Dic_Integrazione1.DisattivaTutto()
            Dic_Note1.DisattivaTutto()
            Dic_Nucleo1.DisattivaTutto()
            Dic_Patrimonio1.DisattivaTutto()
            Dic_Reddito1.DisattivaTutto()
            Dic_Reddito_Conv1.DisattivaTutto()
            cmbStato.Enabled = False
            cmbAnnoReddituale.Visible = False
            imgAnagrafe.Visible = False
            solalettura.Value = "1"
            MenuStampe.Visible = False
            Dic_Documenti1.DisattivaTutto()
            '                cmbStato.Enabled = False
            'Label5.Visible = False
            'Label6.Visible = False
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            imgStampa.Enabled = False
            btnSalva.Enabled = False
            Label4.Visible = True
            Label4.Text = ex.Message
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:Anagrafe Utenza - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Function


    Public Property ISEE_DICHIARAZIONE() As String
        Get
            If Not (ViewState("par_ISEE_DICHIARAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_ISEE_DICHIARAZIONE"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ISEE_DICHIARAZIONE") = value
        End Set

    End Property

    Public Property PatrSuperato() As String
        Get
            If Not (ViewState("par_PatrSuperato") Is Nothing) Then
                Return CStr(ViewState("par_PatrSuperato"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_PatrSuperato") = value
        End Set

    End Property


    Public Property FFOO() As Boolean
        Get
            If Not (ViewState("par_FFOO") Is Nothing) Then
                Return CLng(ViewState("par_FFOO"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_FFOO") = value
        End Set

    End Property
    Public Property bMemorizzato() As Boolean
        Get
            If Not (ViewState("par_bMemorizzato1") Is Nothing) Then
                Return CLng(ViewState("par_bMemorizzato1"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_bMemorizzato1") = value
        End Set

    End Property

    Public Property lBando() As Long
        Get
            If Not (ViewState("par_lBando1") Is Nothing) Then
                Return CLng(ViewState("par_lBando1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lBando1") = value
        End Set

    End Property

    Public Property lIndice_Bando() As Long
        Get
            If Not (ViewState("par_lIndice_Bando1") Is Nothing) Then
                Return CLng(ViewState("par_lIndice_Bando1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIndice_Bando1") = value
        End Set

    End Property

    Public Property lNuovaDichiarazione() As Long
        Get
            If Not (ViewState("par_lNuovaDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lNuovaDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lNuovaDichiarazione") = value
        End Set

    End Property

    Public Property iTab() As Integer
        Get
            If Not (ViewState("par_itab1") Is Nothing) Then
                Return CInt(ViewState("par_itab1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_itab1") = value
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

    Public Property MINORI_15() As Integer
        Get
            If Not (ViewState("par_MINORI_15") Is Nothing) Then
                Return CInt(ViewState("par_MINORI_15"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_MINORI_15") = value
        End Set

    End Property

    Public Property MAGGIORI_65() As Integer
        Get
            If Not (ViewState("par_MAGGIORI_65") Is Nothing) Then
                Return CInt(ViewState("par_MAGGIORI_65"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_MAGGIORI_65") = value
        End Set

    End Property


    Public Property PREVALENTE_DIP() As Integer
        Get
            If Not (ViewState("par_PREVALENTE_DIP") Is Nothing) Then
                Return CInt(ViewState("par_PREVALENTE_DIP"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_PREVALENTE_DIP") = value
        End Set

    End Property

    Public Property SoloLettura() As Long
        Get
            If Not (ViewState("par_SoloLettura") Is Nothing) Then
                Return CLng(ViewState("par_SoloLettura"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_SoloLettura") = value
        End Set

    End Property

    Public Property TORNA() As Long
        Get
            If Not (ViewState("par_TORNA") Is Nothing) Then
                Return CLng(ViewState("par_TORNA"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_TORNA") = value
        End Set

    End Property

    Public Property CHIUDI() As Long
        Get
            If Not (ViewState("par_CHIUDI") Is Nothing) Then
                Return CLng(ViewState("par_CHIUDI"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_CHIUDI") = value
        End Set

    End Property

    Public Property lIdDichiarazione() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, La Dichiarazione deve essere stampata!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript31")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript31", scriptblock)
                    End If

                    'H1.Text = "0"
                    Exit Sub
                End If
                'H1.Text = "1"
                'If par.OracleConn.State = Data.ConnectionState.Open Then
                '    par.OracleConn.Close()
                'End If
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()
                'par.myTrans.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                Session.Remove("STAMPATO")
                Session.Remove("idBandoAU")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()

                If TORNA = "1" Then
                    Response.Write("<script>location.replace('RisultatoRicercaD.aspx?XX=1&ENTE=ALTRI ENTI&CO=" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "');</script>")
                End If

                If CHIUDI <> "1" Then
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                Else
                    Response.Write("<script>window.close();</script>")
                End If
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()

                If TORNA = "1" Then
                    Response.Write("<script>location.replace('RisultatoRicercaD.aspx?XX=1&ENTE=ALTRI ENTI&CO=" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "');</script>")
                End If

                If CHIUDI <> "1" Then
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                Else
                    Response.Write("<script>window.close();</script>")
                End If
            End If

        Catch EX As Exception
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""ErrorPage.aspx""</script>")
        Finally

        End Try

    End Sub

    Private Function VerificaDati(ByRef S As String) As Boolean
        VerificaDati = True
        If CType(Dic_Utenza1.FindControl("txtData1"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <MILANO, Lì> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If
        If CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <COGNOME> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If
        If CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <NOME> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If
        If CType(Dic_Utenza1.FindControl("txtCF"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <CODICE FISCALE> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If

        If CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <DATA DI NASCITA> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If

        If par.ControllaValiditaCF(CType(Dic_Utenza1.FindControl("txtCF"), TextBox).Text, CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text, CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text, CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text) = False Then
            Response.Write("<script>alert('Attenzione...Il codice fiscale non è corretto!')</script>")
            VerificaDati = False
            Exit Function
        End If



    End Function


    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Function NuovaDichiarazione()
        Dim CT1 As DropDownList

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE" & lIdDichiarazione, par.OracleConn)

            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader.HasRows = False Then
                    If lIdDichiarazione = -1 Then
                        Response.Write("<script>alert('Non è possibile inserire nuove dichiarazioni!');history.go(-1);</script>")
                        btnSalva.Visible = False
                        imgStampa.Visible = False
                        'Label6.Visible = False
                        'Label5.Visible = False
                    Else
                        par.OracleConn.Close()
                        Session.Item("LAVORAZIONE") = "0"
                        Response.Write("<script>alert('Non è possibile apportare modifiche!');</script>")
                        btnSalva.Visible = False
                    End If
                    Exit Function
                Else
                    sAnnoIsee = myReader(0)

                    CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(sAnnoIsee)
                    CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = sAnnoIsee
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = sAnnoIsee
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = sAnnoIsee

                    lIndice_Bando = myReader(1)



                    cmbAnnoReddituale.SelectedIndex = -1
                    cmbAnnoReddituale.Items.FindByText(sAnnoIsee).Selected = True


                End If
            Else
                If lIdDichiarazione = -1 Then
                    par.OracleConn.Close()
                    Session.Item("LAVORAZIONE") = "0"
                    Response.Write("<script>alert('Non è possibile inserire nuove dichiarazioni!');history.go(-1);</script>")
                    btnSalva.Visible = False
                    'Label6.Visible = False
                    'Label5.Visible = False
                    imgStampa.Visible = False
                    Exit Function
                Else
                    Response.Write("<script>alert('Non è possibile apportare modifiche!');</script>")
                End If
                Exit Function
            End If
            myReader.Close()

            txtDataPG.Text = Format(Now, "dd/MM/yyyy")
            Dim lsiFrutto As New ListItem("DA COMPLETARE", "0")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("COMPLETA", "1")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("DA CANCELLARE", "2")
            cmbStato.Items.Add(lsiFrutto)

            cmbStato.SelectedItem.Text = "DA COMPLETARE"



            CType(Dic_Utenza1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")
            'CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")
            CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")

            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")


            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=4415"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                CT1 = Dic_Utenza1.FindControl("cmbNazioneNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = Dic_Utenza1.FindControl("cmbPrNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                CT1 = Dic_Utenza1.FindControl("cmbComuneNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True

                CT1 = Dic_Utenza1.FindControl("cmbNazioneRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = Dic_Utenza1.FindControl("cmbPrRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.RiempiDList(Dic_Utenza1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                CT1 = Dic_Utenza1.FindControl("cmbComuneRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True
                CType(Dic_Utenza1.FindControl("txtCAPRes"), TextBox).Text = par.IfNull(myReader("CAP"), "")


                'CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
                'CT1.SelectedIndex = -1
                'CT1.Items.FindByText("ITALIA").Selected = True
                'CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                'CT1.SelectedIndex = -1
                'CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                'par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                'CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                'CT1.SelectedIndex = -1
                'CT1.Items.FindByText(myReader("NOME")).Selected = True

                'CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
                'CT1.SelectedIndex = -1
                'CT1.Items.FindByText("ITALIA").Selected = True
                'CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                'CT1.SelectedIndex = -1
                'CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                'par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                'CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                'CT1.SelectedIndex = -1
                'CT1.Items.FindByText(myReader("NOME")).Selected = True
                'CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text = par.IfNull(myReader("CAP"), "")
            End If
            myReader.Close()

            CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text = "VIA"
            CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value = "6"
            CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text = "VIA"
            CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value = "6"

            par.cmd.CommandText = "SELECT MAX(ID) FROM UTENZA_NUM_PROTOCOLLI"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = myReader(0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO UTENZA_NUM_PROTOCOLLI VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            lblPG.Text = Format(lValoreCorrente, "0000000000")


            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


            par.cmd.CommandText = "INSERT INTO UTENZA_Dichiarazioni (ID,ID_BANDO,ID_CAF,CHIAVE_ENTE_ESTERNO,FL_4_5_LOTTO) VALUES (SEQ_UTENZA_DICHIARAZIONI.NEXTVAL," & lIndice_Bando & "," & Session.Item("ID_CAF") & ",-1,1)"
            par.cmd.ExecuteNonQuery()

            Session.Add("idBandoAU", lIndice_Bando)

            par.cmd.CommandText = "SELECT SEQ_UTENZA_DICHIARAZIONI.CURRVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lIdDichiarazione = myReader(0)
                'par.cmd.CommandText = "INSERT INTO COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR) VALUES (SEQ_COMP_NUCLEO.NEXTVAL," & lIdDichiarazione & ",0)"
                'par.cmd.ExecuteNonQuery()
                CType(Dic_Utenza1.FindControl("txtId"), HiddenField).Value = lIdDichiarazione
            End If
            myReader.Close()
            lblPG.ToolTip = lIdDichiarazione
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            Session.Add("LAVORAZIONE", "1")
            Session.Add("STAMPATO", "0")

            If CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = "2008" Then
                cmbAnnoReddituale.Enabled = False
            End If

        Catch ex1 As Oracle.DataAccess.Client.OracleException
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Errore : " & ex1.Message & "');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            Label4.Text = ex1.Message
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
        Catch ex As Exception
            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Errore : " & ex.Message & "');" _
                        & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            Label4.Text = ex.Message
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
        Finally

        End Try
    End Function

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
        Dim LUOGO As String = ""
        Dim SDATA As String = ""
        Dim LUOGO_REDDITO As String = ""
        Dim DATA_REDDITO As String = ""
        Dim numero As String
        Dim sStringasql As String
        Dim GIA_TITOLARI As String = ""


        Dim i As Integer

        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            Dim CODICEANAGRAFICO As String = ""
            par.cmd.CommandText = "SELECT operatori.*,caf_web.cod_caf as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
            Dim myReaderENTE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderENTE.Read() Then
                CODICEANAGRAFICO = par.IfNull(myReaderENTE("ENTE"), "") & " - " & par.IfNull(myReaderENTE("COD_ANA"), "")
            End If
            myReaderENTE.Close()

            If CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text & "</I>   " _
                                & ", NOME:   <I>" & CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text & "</I><BR>" _
                                & "NATO A:   <I>" & CType(Dic_Utenza1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Text & "</I>   , " _
                                & "PROVINCIA:   <I>" & CType(Dic_Utenza1.FindControl("cmbPrNas"), DropDownList).SelectedItem.Text & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & CType(Dic_Utenza1.FindControl("txtTelRes"), TextBox).Text & "</I><BR>"

            Else
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text & "</I>   " _
                                & ", NOME:   <I>" & CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text & "</I><BR>" _
                                & "STATO ESTERO:   <I>" & CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & CType(Dic_Utenza1.FindControl("txtTelRes"), TextBox).Text & "</I><BR>"


            End If


            Dim INDIRIZZOSTAMPA As String = ""

            If CType(Dic_Utenza1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & CType(Dic_Utenza1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</I>   , " _
                & "PROVINCIA:   <I>" & CType(Dic_Utenza1.FindControl("cmbPrRes"), DropDownList).SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Utenza1.FindControl("txtIndRes"), TextBox).Text & "</I>   ," _
                & "N. CIVICO:   <I>" & CType(Dic_Utenza1.FindControl("txtCivicoRes"), TextBox).Text & "</I>   , CAP:   <I>" & CType(Dic_Utenza1.FindControl("txtCAPRes"), TextBox).Text & "</I>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & CType(Dic_Utenza1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</I>   , " _
                & "STATO ESTERO:   <I>" & CType(Dic_Utenza1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Utenza1.FindControl("txtIndRes"), TextBox).Text & "</I>   ," _
                & "N. CIVICO:   <I>" & CType(Dic_Utenza1.FindControl("txtCivicoRes"), TextBox).Text & "</I>   , CAP:   <I>" & CType(Dic_Utenza1.FindControl("txtCAPRes"), TextBox).Text & "</I>"
            End If
            INDIRIZZOSTAMPA = "INDIRIZZO:" & CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Utenza1.FindControl("txtIndRes"), TextBox).Text & ", " & CType(Dic_Utenza1.FindControl("txtCivicoRes"), TextBox).Text & " CAP:" & CType(Dic_Utenza1.FindControl("txtCAPRes"), TextBox).Text

            If CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text <> "" Then

                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/>COD RAPPORTO DI UTENZA: <I>" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "</I>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/>COD CONVOCAZIONE: <I>" & CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Text & "</I>"
            End If

            If CType(Dic_Utenza1.FindControl("R1"), RadioButton).Checked = True Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>TIPOLOGIA: <I>ERP</I><BR>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>TIPOLOGIA: <i>EC</i><BR><br/>"
            End If

            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR><b>DATI RELATIVI ALL'ALLOGGIO:</b><BR>"
            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>FOGLIO: " & CType(Dic_Utenza1.FindControl("txtFoglio"), TextBox).Text & "&nbsp; MAPPALE: " & CType(Dic_Utenza1.FindControl("txtMappale"), TextBox).Text & "&nbsp; SUB: " & CType(Dic_Utenza1.FindControl("txtsub"), TextBox).Text
            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>SCALA: " & CType(Dic_Utenza1.FindControl("txtScala"), TextBox).Text & "&nbsp; PIANO: " & CType(Dic_Utenza1.FindControl("txtPiano"), TextBox).Text & "&nbsp; N.ALLOGGIO/INTERNO: " & CType(Dic_Utenza1.FindControl("txtAlloggio"), TextBox).Text

            If Valore01(CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR><img src=block_checked.gif width=10 height=10 border=1>Intestatario di Contratto"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR><img src=block.gif width=10 height=10 border=1>Intestatario di Contratto"
            End If

            If Valore01(CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Utente in corso di Voltura"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Utente in corso di Voltura"
            End If

            If Valore01(CType(Dic_Utenza1.FindControl("ch3"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Occupante Abusivo"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Occupante Abusivo"
            End If

            If Valore01(CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Altro Componente Maggiorenne"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Altro Componente Maggiorenne"
            End If

            If Valore01(CType(Dic_Utenza1.FindControl("ch5"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Tutore"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Tutore"
            End If

            If Valore01(CType(Dic_Utenza1.FindControl("ch6"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Presentata da Delegato"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Presentata da Delegato"
            End If

            If Valore01(CType(Dic_Utenza1.FindControl("chPosta"), CheckBox).Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Presentata tramite posta"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Presentata tramite posta"
            End If


            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/><br/><b>ESTREMI DOCUMENTO DI RICONOSCIMENTO</b><br/> "

            If CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text <> "ITALIA" And vcomunitario.Value = "1" And CType(Dic_Note1.FindControl("ChCittadinanza"), CheckBox).Checked = False Then
                If CType(Dic_Note1.FindControl("txtPSNum"), TextBox).Text <> "" Then
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "PERMESSO DI SOGGIORNO N.: <i>" & CType(Dic_Note1.FindControl("txtPSNum"), TextBox).Text & "</i> Data Rilascio: <i>" & CType(Dic_Note1.FindControl("txtPSData"), TextBox).Text & "</i><br/>"
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "Data Scadenza: <i>" & CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text & "</i> Data Rinnovo : <i>" & CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text & "</i><br/>"
                Else
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "CARTA DI SOGGIORNO N.: <i>" & CType(Dic_Note1.FindControl("txtCSNum"), TextBox).Text & "</i> Data Rilascio: <i>" & CType(Dic_Note1.FindControl("txtCSData"), TextBox).Text & "</i><br/>"
                End If
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).SelectedItem.Text & " N.: <i>" & CType(Dic_Note1.FindControl("txtCINum"), TextBox).Text & "</i> Data Rilascio: <i>" & CType(Dic_Note1.FindControl("txtCIData"), TextBox).Text & "</i> Rilasciato da: <i>" & CType(Dic_Note1.FindControl("txtCIRilascio"), TextBox).Text & "</i><br/>"

            End If





            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>COD. CONTRATTO: <I>" & CType(Dic_Utenza1.FindControl("txtcodALLOGGIO"), TextBox).Text & "</I>"
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>POSIZIONE: <I>" & CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Text & "</I>"
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>RAPPORTO: <I>" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "</I>"
            '            If CType(Dic_Utenza1.FindControl("R1"), RadioButton).Checked = True Then
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>TIPOLOGIA: <I>ERP</I><BR>"
            'Else
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>TIPOLOGIA: <i>EC</i><BR>"
            'End If
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>RAPPORTO REALE: <I>" & CType(Dic_Utenza1.FindControl("txtRapportoReale"), TextBox).Text & "</I>"
            DATI_NUCLEO = ""

            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                DATI_NUCLEO = DATI_NUCLEO & "<TR>" _
                            & "<TD width=5%><small><small>    <center>" & i & "</center></small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16) & "</I>   </small></small></TD>" _
                            & "<TD width=20%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25) & "</I>   </small></small></TD>" _
                            & "<TD width=20%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25) & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10) & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 25) & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6) & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 120, 2) & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5) & "</I>   </small></small></TD>" _
                            & "</TR>"
            Next

            SPESE_SOSTENUTE = ""
            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                                    & "<td width=50%><small><small><CENTER>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 52) & "</CENTER></small></small></td>" _
                                    & "<td align=right width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",00" & "</I></small></small></td>" _
                                    & "</tr>"
                End If
            Next i

            ANNO_SIT_ECONOMICA = CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text

            PATRIMONIO_MOB = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items.Count - 1
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                               & "<TR>" _
                               & "<TD width=25%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></small></small></TD>" _
                               & "<TD  width=25%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 27) & "</I>   </small></small></TD>" _
                               & "<TD width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 55, 16) & "</I>   </small></small></TD>" _
                               & "<TD  align=right  width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8) & ",00</I></small></small></TD>" _
                               & "</TR>"
            Next i

            PATRIMONIO_IMMOB = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                                   & "<TR>" _
                                   & "<TD><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20) & "</I>   </small></small></TD>" _
                                   & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6) & "</I>   %   </p></small></small></TD>" _
                                   & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 8) & ",00</I>   </p></small></small></TD>" _
                                   & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 67, 8) & ",00</I>   </p></small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 82, 3) & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 86, 30) & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 117, 2) & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 120, 7) & "</center><I></I>   </small></small></TD>" _
                                   & "</TR>"
            Next i
            If PATRIMONIO_IMMOB <> "" Then
                'Modifiche Anagrafe Utenza
                'If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True Then
                '    CAT_CATASTALE = CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Text
                'Else
                '    CAT_CATASTALE = ""
                'End If
            Else
                CAT_CATASTALE = ""
            End If

            REDDITO_NUCLEO = ""
            For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
                REDDITO_NUCLEO = REDDITO_NUCLEO _
                & "<TR>" _
                & "<TD><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 35) & "</I>   </center></small></small></TD>" _
                & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) & ",00</I>   </small></small></p></TD>" _
                & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ",00</I>   </small></small></p></TD>" _
                & "</TR>"
            Next i

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

            DATI_DICHIARANTE = "<BR></BR>"
            'End If

            REDDITO_IRPEF = ""
            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
                REDDITO_IRPEF = REDDITO_IRPEF _
                & "<TR>" _
                & "<TD width=40%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 50) & "</I>   </center></small></small></TD>" _
                & "<TD  width=505%><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 52, 8) & ",00</I>   </p></small></small></TD>" _
                & "</TR>"
            Next i

            REDDITO_DETRAZIONI = ""
            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items.Count - 1
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<TR>" _
                & "<TD width=25%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 30) & "</I>   </center></small></small></TD>" _
                & "<TD  width=25%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 32, 35) & "</I>   </center></small></small></TD>" _
                & "<TD  width=25%><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 68, 8) & ",00</I>   </p></small></small></TD>" _
                & "</TR>"
            Next i

            LUOGO_REDDITO = "Milano"

            DATA_REDDITO = CType(Dic_Integrazione1.FindControl("txtdata1"), TextBox).Text

            numero = lblPG.Text & " del " & Format(Now, "dd/MM/yyyy")


            sStringasql = "<HTML><HEAD><TITLE>Finestra di Stampa</TITLE></HEAD><BODY><UL><UL>   <NOBR></NOBR><basefont SIZE=1></UL></UL>"
            sStringasql = sStringasql & "<img border='0' src='..\IMG\logo.gif' width='166' height='104'><br><CENTER><B><p style='font-family: arial; font-size: 16pt; font-weight: bold'>DICHIARAZIONE SOSTITUTIVA </p><br/><p style='font-family: arial; font-size: 14pt;'>ai sensi degli artt. 46 e 47 D.P.R. 445 del 28/12/2000<br/>Il sottoscritto, consapevole delle responsabilità e delle sanzioni penali previste dagli artt. 75 e 76 del D.P.R. n. 445/2000 per chi rende false attestazioni e dichiarazioni mendaci, sotto la propria personale responsabilità</p><br/><p style='font-family: arial; font-size: 16pt; font-weight: bold'>DICHIARA</p></b></CENTER>   <BR>"
            sStringasql = sStringasql & "<CENTER>"
            sStringasql = sStringasql & "</CENTER>   <NOBR></NOBR>   <CENTER>"
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & " </CENTER>"
            sStringasql = sStringasql & "<center><table border=1 cellspacing=0 width=95%><tr><td><medium>   <B>DATI ANAGRAFICI DEL RICHIEDENTE</B><BR>"
            sStringasql = sStringasql & DATI_ANAGRAFICI & "<br></medium></td></tr></table></center>"
            sStringasql = sStringasql & "<BR><UL>   </UL><NOBR></NOBR><center>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=95%><tr><td><br><medium>   SOGGETTI COMPONENTI IL NUCLEO FAMILIARE: richiedente"
            sStringasql = sStringasql & " componenti la famiglia anagrafica e altri soggetti considerati a carico ai fini IRPEF"
            sStringasql = sStringasql & "<BR>"
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & "<center>"
            sStringasql = sStringasql & "</medium>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=90%><tr><td>"
            sStringasql = sStringasql & "<p align='center'><small>A</small></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><small>B</small></p>"
            sStringasql = sStringasql & "</td><td colspan=2>"
            sStringasql = sStringasql & "<p align='center'><small>C</small></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><small>D</small></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><small>E</small></td><td>"
            sStringasql = sStringasql & "<p align='center'><small>F</small></td><td>"
            sStringasql = sStringasql & "<p align='center'><small>G</small></td><td>"
            sStringasql = sStringasql & "<p align='center'><small>H</small></p>"
            sStringasql = sStringasql & "</td></tr>   <small>   <tr><td bgcolor=#C0C0C0><center><small><small>N.Progr.</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE FISCALE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>COGNOME</small></small></center></td><td   bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>DATA DI NASCITA</small></small></center></td>"
            sStringasql = sStringasql & "</small>"
            sStringasql = sStringasql & "<td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><small><small><small>GR. PARENTELA</small></small></small></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><small><small><small>&nbsp;% INVALIDITA'</small></small></small></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><small><small><small>INDENNITA' ACC.</small></small></small></td>   <td bgcolor=#C0C0C0><small><small><small>ASL&nbsp;</small></small></small></td></tr><UL><UL>   <NOBR></NOBR>"
            sStringasql = sStringasql & DATI_NUCLEO
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</table></center>"
            sStringasql = sStringasql & "<BR><UL>   <NOBR></NOBR><p style='font-family: ARIAL; font-size: 14pt;'>   <B>Altre informazioni sul nucleo familiare</B><BR></p>"
            sStringasql = sStringasql & "<p><p style='font-family: ARIAL; font-size: 14pt;'>Nel nucleo famigliare del richiedente <b>" & GIA_TITOLARI & "</b>"
            sStringasql = sStringasql & " titolari di un contratto di assegnazione di alloggio di edilizia residenziale pubblica<BR>"
            sStringasql = sStringasql & "</p></p>"
            sStringasql = sStringasql & "<table cellspacing=0 border=0 width=90%><tr><td height=18 width=35% ><p style='font-family: ARIAL; font-size: 14pt;'>   - nel nucleo familiare sono presenti n.   </p></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right><p style='font-family: ARIAL; font-size: 14pt;'>   <I>" & N_INV_100_ACC & "</I>   </p></td></tr></table></td><td width=50% ></p><p style='font-family: ARIAL; font-size: 14pt;'>   componenti con invalidit&agrave; al 100% (con indennit&agrave; di accompagnamento)   </p></td></tr><tr><td><p style='font-family: ARIAL; font-size: 14pt;'><CENTER><p style='font-family: ARIAL; font-size: 14pt;'>Spese effettivamente sostenute distinte per componente</p><table border=1 cellpadding=0 cellspacing=0 width=50%>   <tr><td width=50%><small><CENTER><b>A</b></CENTER></small></td><td align=right width=50%><small><CENTER><b>B</b></CENTER></small></td></tr>   <tr><td bgcolor=#C0C0C0 width=50%><CENTER><small><small>Nome</small></small></small></CENTER></td><small>   <td bgcolor=#C0C0C0 align=right width=50%><small><small><CENTER>SPESA</CENTER></small></small></td></tr><UL><UL>   <NOBR></NOBR>" & SPESE_SOSTENUTE & "</UL></UL>   <NOBR></NOBR></table></CENTER></td><td>&nbsp;</td><td>&nbsp;<BR>"
            sStringasql = sStringasql & "</p></td></tr><tr><td height=18 width=30% ><p style='font-family: ARIAL; font-size: 14pt;'>   - nel nucleo sono presenti n.   </p></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I> <p style='font-family: ARIAL; font-size: 14pt;'>   " & N_INV_100_NO_ACC & "</p></I>   </p></td></tr></table></td><td width=55% ><p style='font-family: ARIAL; font-size: 14pt;'>   componenti con invalidit&agrave; al 100% senza indennit&agrave; di accompagnamento<BR>"
            sStringasql = sStringasql & "</p></td></tr><tr><td height=18 width=30% ><p style='font-family: ARIAL; font-size: 14pt;'>   - nel nucleo sono presenti n.   </p></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><p style='font-family: ARIAL; font-size: 14pt;'>" & N_INV_100_66 & "</p></I>   </p></td></tr></table></td><td width=55% ><p style='font-family: ARIAL; font-size: 14pt;'>   componenti con invalidit&agrave; inferiore al 100% e superiore al 66%<BR>"
            sStringasql = sStringasql & "</p></td></tr></table>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=95%><tr><td><medium><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </medium><br><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><medium><BR>"
            sStringasql = sStringasql & "<B>SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   <B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>posseduto alla data del 31 dicembre " & ANNO_SIT_ECONOMICA & "&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td></tr>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE INTERMEDIARIO O GESTORE</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>INTERMEDIARIO O GESTORE (indicare se &egrave;  Banca"
            sStringasql = sStringasql & " Posta"
            sStringasql = sStringasql & " SIM"
            sStringasql = sStringasql & " SGR"
            sStringasql = sStringasql & " Impresa di investimento comunitaria o extracomunitaria"
            sStringasql = sStringasql & " Agente di cambio"
            sStringasql = sStringasql & " ecc.)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>VALORE INVESTIMENTO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>posseduto alla data del 31"
            sStringasql = sStringasql & " Dicembre " & ANNO_SIT_ECONOMICA
            sStringasql = sStringasql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td><td><center>G</center></td><td><center>H</center></td><td><center>I</center></td><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TIPO DI PATRIMONIO  IMMOBILIARE</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>QUOTA POSSEDUTA (percentuale)</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
            sStringasql = sStringasql & " come definita ai fini ICI)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>CAT.CATASTALE</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>COMUNE</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>VANI</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>SUP.UTILE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"
            sStringasql = sStringasql & "</table></center><br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=90%><tr><td width=80%><p align=right><small>      </small></p></td><td width=10% style='border: thin solid rgb(0"
            sStringasql = sStringasql & " 0"
            sStringasql = sStringasql & " 0)'><small><p align=center>   <I>" & CAT_CATASTALE & "</I>   </p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><medium>   <B>REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>REDDITO COMPLESSIVO DICHIARATO AI FINI IRPEF (1)</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>PROVENTI AGRARI DA DICHIARAZIONE IRAP (per i soli impreditori agricolil)</small></small><center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
            sStringasql = sStringasql & "</table></center><br>(1) al netto dei redditi agrari dell'imprenditore agricolo; compresi i redditi da lavoro prestato nelle zone di frontiera"
            sStringasql = sStringasql & "</B>" & dichiarante & "<BR>"
            sStringasql = sStringasql & DATI_DICHIARANTE
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</small></td></tr></table></center><BR>"
            sStringasql = sStringasql & "</center>"
            sStringasql = sStringasql & "<BR><center><table  cellspacing=0 border=1 width=95%><tr><td><medium>   <B>INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ERP</B><BR>"
            sStringasql = sStringasql & "<B><BR><UL>ALTRI REDDITI</ul></B>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>IMPORTO REDDITO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_IRPEF & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>TIPO DETRAZIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>IMPORTO DETRAZIONE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center><BR>"
            sStringasql = sStringasql & "<BR><BR><CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I>" & LUOGO_REDDITO & "</I>   </center></small></td><td width=33%><small><center>"
            sStringasql = sStringasql & " li   <I>" & DATA_REDDITO & "</I>   </center></small></td><td width=34%><center></center></td></tr><tr><td width=33% height=15><small><center>(luogo)</center></small></td><td width=33%><small><center>(data)</center></small></td><td width=34%><small><center></center></small></td></tr></table></CENTER><BR>"
            sStringasql = sStringasql & "</small>"
            sStringasql = sStringasql & "</table>"
            sStringasql = sStringasql & "</br>"
            sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<p align='left'><img border='0' src='..\IMG\logo.gif' width='166' height='104'></p>"

            If ANNO_SIT_ECONOMICA <> "" Then
                sStringasql = sStringasql & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ")</p></font></p>"
            Else
                sStringasql = sStringasql & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA 2007 (Redditi 2006)</p></font></p>"
            End If

            If CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text <> "" Then
                sStringasql = sStringasql & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text & "   " _
                    & " " & CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text & "</I> - COD. RAPPORTO UTENZA: " & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & " " & INDIRIZZOSTAMPA & "</p>"

            Else
                sStringasql = sStringasql & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text & "   " _
                & " " & CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text & "</I> - COD. CONVOCAZIONE: " & CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Text & " " & INDIRIZZOSTAMPA & "</p>"

            End If
            '


            'sStringasql = sStringasql & CalcolaRedditoDatabase(lIdDichiarazione)
            sStringasql = sStringasql & "</BR>"
            'sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
            Dim sCalcoloISEE As String = ""

            sCalcoloISEE = CalcolaIsee(lIdDichiarazione)
            sStringasql = sStringasql & sCalcoloISEE

            Dim ss1 As String = ""
            If ANNO_SIT_ECONOMICA <> "" Then
                ss1 = ss1 & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ")</p></font></p>"
            Else
                ss1 = ss1 & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA 2007 (Redditi 2006)</p></font></p>"
            End If

            If CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text <> "" Then
                ss1 = ss1 & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text & "   " _
                    & " " & CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text & "</I> - COD. RAPPORTO UTENZA: " & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & " " & INDIRIZZOSTAMPA & "</p>"
            Else
                ss1 = ss1 & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & CType(Dic_Utenza1.FindControl("txtCognome"), TextBox).Text & "   " _
                    & " " & CType(Dic_Utenza1.FindControl("txtNome"), TextBox).Text & "</I> - COD. CONVOCAZIONE: " & CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Text & " " & INDIRIZZOSTAMPA & "</p>"
            End If

            ss1 = ss1 & "</BR>"

            sCalcoloISEE = ss1 & sCalcoloISEE

            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & " &nbsp;"
            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & "<b><p style='font-family: ARIAL; font-size: 14pt;'>Il sottoscritto attesta di essere a conoscenza che sui dati dichiarati potranno essere eseguiti controlli sulla veridicità degli stessi ai sensi dell’articolo 71 del Dpr 445/2000 e da parte della Guardia di Finanza presso gli istituti di credito o altri intermediari finanziari, ai sensi dell’art.4, comma 10 D.Lgs. 109/1998 e dell’art.6 D.P.C.M. 221/1999</b><br/><br/>"
            sStringasql = sStringasql & " Dic.N. " & numero & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Firma del Dichiarante"
            sStringasql = sStringasql & "<br>Elaborata da: " & CODICEANAGRAFICO & "</p>"

            sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<font face='Arial'><table style='style='font-family: ARIAL; font-size: 12pt;width: 90%;'>" _
                                    & "<tr>" _
                                    & "<td>" _
                                    & "<B><p style='font-family: ARIAL; font-size: 16pt;'>INFORMATIVA AI SENSI DELL'ARTICOLO 13 DEL DECRETO LEGISLATIVO 30 GIUGNO 2003, N. 196 -Codice in materia di protezione dei dati personali-" _
                                    & "<br />" _
                                    & "</p></B>" _
                                    & "<br />" _
                                    & "<p style='font-family: ARIAL; font-size: 12pt;'>Gentile Signore/a," _
                                    & "<br />" _
                                    & "Il D. Lgs. 30 giugno 2003, n.196 ""Codice in materia di protezione dei dati personali"" prevede la tutela delle persone e di " _
                                    & "altri soggetti rispetto al trattamento dei dati personali. Il trattamento in base al citato ""Codice"" è " _
                                    & "disciplinato assicurando un elevato livello di tutela dei diritti e delle " _
                                    & "libertà fondamentali nonché della dignità dell’interessato secondo principi di " _
                                    & "correttezza, liceità, trasparenza e di tutela della riservatezza. A tal fine il " _
                                    & "Comune di Milano in qualità di Titolare del trattamento dei dati personali, ai " _
                                    & "sensi dell' art. 13 del Codice, Le fornisce le seguenti informazioni." _
                                    & "<br />" _
                                    & "<br />" _
                                    & "1. Oggetto e finalità del trattamento I dati personali sono raccolti e trattati per l'esclusivo assolvimento degli obblighi istituzionali dell'Amministrazione comunale, riguardanti in particolare l'assegnazione in locazione di alloggi di edilizia residenziale pubblica e per finalità amministrative strettamente connesse e strumentali alla gestione delle procedure di assegnazione degli alloggi stessi, nonchè alle disposizioni definite dalle normative nazionale e regionali in tema di edilizia residenziale pubblica." _
                                    & "<br /><br />" _
                                    & "2. Modalità del trattamento In relazione alle finalità indicate, il trattamento dei dati sarà effettuato attraverso modalità cartacee e/o informatizzate. I trattamenti saranno effettuati solo da soggetti autorizzati con l’attenzione e la cautela previste dalle norme in materia garantendo la massima sicurezza e riservatezza dei dati personali, sensibili e giudiziari qualora raccolti per gli adempimenti necessari." _
                                    & "<br /><br />" _
                                    & "3. Natura del trattamento Il conferimento dei dati è obbligatorio per la realizzazione delle finalità descritte e l'eventuale rifiuto determinerà l'impossibilità di dar corso alla Sua istanza e di porre in essere gli adempimenti conseguenti e inerenti la procedura per l'assegnazione degli alloggi." _
                                    & "<br /><br />" _
                                    & "4. Ambito di comunicazione e diffusione dei dati I dati personali, con esclusione di quelli idonei a rivelare lo stato di salute, potranno essere oggetto di diffusione. La graduatoria approvata dagli organi competenti in esito alla procedura di assegnazione verrà diffusa mediante pubblicazione nelle forme previste dalle norme in materia e anche attraverso il sito internet del Comune di Milano nel rispetto dei principi di pertinenza e non eccedenza. I dati personali verranno comunicati a soggetti pubblici o privati se previsto da disposizioni di legge o di regolamento." _
                                    & "<br /><br />" _
                                    & "5. Responsabili del trattamento dei dati I Responsabili del trattamento sono:<BR/>- il Comune di Milano nella persona del Direttore del Settore Gestione Patrimonio Abitativo Pubblico, via Larga 12, 20123 Milano, in qualità di titolare del trattamento ai sensi dell’art.29 del D.Lgs.196/2003<BR/>- MM S.P.A., Via del Vecchio Politecnico, 8 - 20121 Milano, nella persona del Direttore Generale o del rappresentante pro tempore, in qualità di responsabile per il trattamento dei dati personali, sensibili e /o giudiziari, per le finalità di gestione del patrimonio abitativo pubblico  e degli annessi usi diversi" _
                                    & "<BR/><BR/>" _
                                    & "6. Consenso Il Comune di Milano, in quanto soggetto pubblico, non deve richiedere il consenso degli interessati per poter trattare i loro dati." _
                                    & "<br /><br />" _
                                    & "7.Diritti dell'interessato L’interessato potrà esercitare i diritti previsti dall'art. 7 del D. Lgs.196/03 ed in particolare ottenere la conferma dell'esistenza o meno di dati personali che lo riguardano, dell’origine dei dati personali, delle modalità del trattamento, della logica applicata in caso di trattamento effettuato con l'ausilio di strumenti elettronici, nonché l'aggiornamento, la rettificazione ovvero quando vi ha interesse, l'integrazione dei dati." _
                                    & "<br /><BR/>" _
                                    & "L'interessato ha in oltre diritto:<br />- di ottenere la cancellazione, la trasformazione in forma anonima o il blocco dei dati trattati in violazione di legge;<br />- di opporsi, in tutto o in parte, per motivi legittimi al trattamento dei dati personali che lo riguardano, ancorché pertinenti allo scopo della raccolta.<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FIRMA DEL DICHIARANTE_______________________________</td></tr></table>"


            sStringasql = sStringasql & "</font></BODY></HTML>"

            HttpContext.Current.Session.Remove("DICHIARAZIONE")



            Dim url As String = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\")

            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("Dichiarazione N. " & numero & " / " & CODICEANAGRAFICO)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = ""
            If CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = False Then
                nomefile = "00_" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Else
                nomefile = "00_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sStringasql, url & nomefile, Server.MapPath("..\IMG\"))
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            If CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = False Then
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sCalcoloISEE, url & "02_" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "_" & lIdDichiarazione & ".pdf", Server.MapPath("..\IMG\"))
            Else
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sCalcoloISEE, url & "02_" & lIdDichiarazione & ".pdf", Server.MapPath("..\IMG\"))
            End If
            Dim ix As Integer = 0
            For ix = 0 To 1000

            Next

            If rdApplica.Checked = True And rdApplica.Visible = True Then
                sStringasql = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                 & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                 & "'F132','APPLICAZIONE LG 36/2008','I')"
            Else
                sStringasql = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F132','','I')"
            End If
            par.cmd.CommandText = sStringasql
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET ISEE='" & ISEE_DICHIARAZIONE & "' WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader.Close()

            Response.Write("<script>window.open('../ALLEGATI/ANAGRAFE_UTENZA/" & nomefile & "','Dichiarazione','');</script>")

        Catch ex As Exception
            Label4.Text = "err " & ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        Finally

        End Try
    End Function

    Private Function CalcolaIsee(ByVal Pratica_Id As Long) As String

        Dim DETRAZIONI As Long


        Dim INV_100_CON As Integer
        Dim INV_100_NO As Integer
        Dim INV_66_99 As Integer
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


        Dim PARAMETRO_MINORI As Double

        Dim MINORI As Integer
        Dim adulti As Integer

        Dim STRINGA_STAMPA As String


        Dim TIPO_ALLOGGIO As Integer
        Dim Calcola_36 As Boolean
        Dim Da_Calcolare36 As Boolean
        Dim limite_isee As Long

        Da_Calcolare36 = False
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

        TIPO_ALLOGGIO = -1

        ESCLUSIONE = ""
        Dim CONTRATTO_30_ANNI As Boolean


        If CType(Dic_Utenza1.FindControl("txtDataDec"), TextBox).Text <> "" Then
            If par.RicavaEta(par.IfEmpty(par.AggiustaData(CType(Dic_Utenza1.FindControl("txtDataDec"), TextBox).Text), Format(Now, "yyyMMdd"))) > 30 Then
                CONTRATTO_30_ANNI = True
            Else
                CONTRATTO_30_ANNI = False
            End If
        Else
            CONTRATTO_30_ANNI = False
        End If

        Calcola_36 = False
        If rdApplica.Checked = True And rdApplica.Visible = True Then
            Calcola_36 = True
        End If

        par.cmd.CommandText = "SELECT TASSO_RENDIMENTO FROM UTENZA_BANDI WHERE ID=" & lIndice_Bando
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        If myReader.Read() Then

            TASSO_RENDIMENTO = par.IfNull(myReader("TASSO_RENDIMENTO"), 0)
            limite_isee = 35000


        End If
        myReader.Close()



        If Calcola_36 = True Then
            TASSO_RENDIMENTO = par.RicavaTasso(CInt(cmbAnnoReddituale.SelectedItem.Text))
            If CInt(cmbAnnoReddituale.SelectedItem.Text) > 2006 Then
                limite_isee = 35000
            End If
            'If cmbAnnoReddituale.SelectedItem.Text = "2007" Then
            '    TASSO_RENDIMENTO = 4.4100000000000001
            '    limite_isee = 35000
            'End If
            'If cmbAnnoReddituale.SelectedItem.Text = "2008" Then
            '    TASSO_RENDIMENTO = 4.75
            '    limite_isee = 35000
            'End If
            'If cmbAnnoReddituale.SelectedItem.Text = "2009" Then
            '    TASSO_RENDIMENTO = 4.3200000000000003
            '    limite_isee = 35000
            'End If
        End If

        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
        myReader = par.cmd.ExecuteReader()



        TOT_COMPONENTI = 0
        Dim ETA_RICHIEDENTE As Integer
        Dim VECCHI As Integer = 0

        Dim Entro70km As Boolean = False

        Do While myReader.Read()
            ETA_RICHIEDENTE = par.RicavaEta(myReader("DATA_NASCITA"))
            If ETA_RICHIEDENTE >= 15 Then
                If ETA_RICHIEDENTE >= 18 Then
                    adulti = adulti + 1
                    If ETA_RICHIEDENTE < 65 Then
                        VECCHI = VECCHI + 1
                    End If
                End If
            Else
                MINORI = MINORI + 1
            End If

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DETRAZIONI = DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()



            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
            End While
            myReader1.Close()



            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()



            DETRAZIONI_FRAGILE = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.HasRows Then
                While myReader1.Read
                    DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReader1("IMPORTO"), 0)
                    TOT_SPESE = TOT_SPESE + par.IfNull(myReader1("IMPORTO"), 0)

                    If DETRAZIONI_FRAGILE > 10000 Then
                        DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                    Else
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    End If

                End While
                myReader1.Close()
            Else
                If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    TOT_SPESE = TOT_SPESE + 10000
                End If
                myReader1.Close()
            End If



            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()



            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                    IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + par.IfNull(myReader1("VALORE"), 0)
                    MUTUI_RESIDENZA = MUTUI_RESIDENZA + par.IfNull(myReader1("MUTUO"), 0)
                Else
                    IMMOBILI = IMMOBILI + par.IfNull(myReader1("VALORE"), 0)
                    MUTUI = MUTUI + par.IfNull(myReader1("MUTUO"), 0)
                End If

                If par.IfNull(myReader1("fl_70km"), "0") = "1" And Mid(par.IfNull(myReader1("cat_catastale"), "0"), 1, 1) = "A" Then
                    Entro70km = True
                End If
            End While
            myReader1.Close()
            TOT_COMPONENTI = TOT_COMPONENTI + 1
        Loop
        'End If
        myReader.Close()



        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
            INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
            INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
        End If
        myReader.Close()

        DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

        'STRINGA_STAMPA = "< center > "
        STRINGA_STAMPA = STRINGA_STAMPA & "<table border='1' cellpadding='0' cellspacing='0' width='95%'>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><font face='Arial' size='3'><b>CALCOLO ISEE  ai fini dell’art. 18 c. 1 lett. e  LR 1/2004</b></font>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><table border='0' cellpadding='0' cellspacing='0' width='81%' style='font-family: Arial; font-size: 12 pt'>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO COMPLESSIVO DEL NUCLEO FAMILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >FIGURATIVO REDDITO MOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI DAL REDDITO LORDO</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% CON INDENNITA'</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_CON & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >SPESE SOSTENUTE PER ASSISTENZA</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOT_SPESE, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% SENZA INDENNITA'</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_NO & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 66%-99%</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_66_99 & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Detrazioni per nucleo familiare affetto da fragilità (" & INV_100_CON + INV_100_NO + INV_66_99 & " invalidi)</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI_FR, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
        If ISEE_ERP < 0 Then
            ISEE_ERP = 0
        End If

        ISR_ERP = ISEE_ERP

        ISEE_ERP = 0

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE DEL REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO MOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(FIGURATIVO_MOBILI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI PER MUTUI CONTRATTI</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI - MUTUI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        If IMMOBILI_RESIDENZA > 0 Then
            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE DELLA RESIDENZA DI PROPRIETA'</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA, "##,##0.00")) & "</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >MUTUO RESIDUO</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE NETTO</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA - MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
        End If

        TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE COMPLESSIVO DEL PATRIMONIO DA CONSIDERARE AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_IMMOBILI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Coefficiente della valutazione del patrimonio immobiliare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;0,20</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

        ISP_ERP = TOTALE_ISEE_ERP

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_ISEE_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>PATRIMONIO COMPLESSIVO AI FINI ERP</B></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(TOTALE_PATRIMONIO_ISEE_ERP, "##,##0.00")) & "</B></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        Dim PARAMETRO As Double

        Select Case TOT_COMPONENTI
            Case 1
                PARAMETRO = 1
            Case 2
                PARAMETRO = (138 / 100)
            Case 3
                PARAMETRO = (167 / 100)
            Case 4
                PARAMETRO = (190 / 100)
            Case 5
                PARAMETRO = (211 / 100)
            Case Else
                PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
        End Select

        PARAMETRO_MINORI = 0
        VSE = PARAMETRO
        If adulti >= 2 Then
            VSE = VSE - (MINORI * (1 / 10))
            PARAMETRO_MINORI = (MINORI * (1 / 10))
        End If

        LIMITE_PATRIMONIO = 16000 + (6000 * VSE)

        'If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO Then

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>Limite Patrimonio Familiare</b></font></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</font></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(LIMITE_PATRIMONIO * 3, "##,##0.00")) & "</b></font></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        'End If


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di componenti del nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & TOT_COMPONENTI & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro corrispondente alla composizione del nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di minori di 15 anni nel nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & MINORI & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro per minori</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >-</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO_MINORI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di adulti nel nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & adulti & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISP_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        ISE_ERP = ISR_ERP + ISP_ERP

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >ISE-erp: indicatore della situazione economica (erp)</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISE_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VSE: Valore della scala di equivalenza</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(VSE, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        ISEE_ERP = ISE_ERP / VSE

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%'><font face='Arial' size='3'><b>ISEE-erp: Indicatore della Situazione Economica Equivalente (erp) (*)<b></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='2'>Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(ISEE_ERP, "##,##0.00")) & "</b></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        ISEE_DICHIARAZIONE = par.Converti(Format(ISEE_ERP, "##,##0.00"))


        If ISEE_ERP > limite_isee Then
            If limite_isee = 35000 Then
                If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
                    Calcola_36 = False
                Else
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
                End If
            Else
                If (VECCHI = TOT_COMPONENTI) And CONTRATTO_30_ANNI = True Then
                    Calcola_36 = False
                Else
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
                End If
            End If


        End If

        If ISEE_ERP > 35000 And Calcola_36 = True Then
            If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
                Calcola_36 = False
            Else
                Da_Calcolare36 = True
            End If
        End If

        PatrSuperato = "0"
        If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO * 3 Then
            If limite_isee = 35000 Then
                If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
                    Calcola_36 = False
                Else
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
                    PatrSuperato = "1"
                End If
            Else
                If (VECCHI = TOT_COMPONENTI) And CONTRATTO_30_ANNI = True Then
                    Calcola_36 = False
                Else
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                    STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
                    PatrSuperato = "1"
                End If

            End If
        End If

        If PatrSuperato = "1" Then
            Da_Calcolare36 = True
        End If

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='2'>* L'indice ISEE ERP è stato elaborato sulla base della dichiarazione sostitutiva sottoscritta e potrà subire variazioni a seguito di successive verifiche e controlli da parte degli uffici competenti.</font>" & "</b></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "</table>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</table>"

        par.cmd.CommandText = "update utenza_dichiarazioni set " _
                       & "" _
                       & "" _
                       & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                       & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
                       & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
                       & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "',PATR_SUPERATO='" & PatrSuperato & "' WHERE ID=" & Pratica_Id
        par.cmd.ExecuteNonQuery()

        'Modifiche Anagrafe Utenza
        'If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Value = 1 Or CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Checked = True Then
        '    Da_Calcolare36 = True
        'End If
        If Entro70km = True Then
            Da_Calcolare36 = True
        End If

        If Da_Calcolare36 = True And Calcola_36 = True Then

            'STRINGA_STAMPA = STRINGA_STAMPA & "<p style='page-break-before: always'>&nbsp;</p>"
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



            TIPO_ALLOGGIO = -1

            ESCLUSIONE = ""
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()



            TOT_COMPONENTI = 0

            Do While myReader.Read()

                If par.RicavaEta(myReader("DATA_NASCITA")) >= 15 Then
                    If par.RicavaEta(myReader("DATA_NASCITA")) >= 18 Then
                        adulti = adulti + 1
                    End If
                Else
                    MINORI = MINORI + 1
                End If

                'par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                DETRAZIONI = 0 'DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
                'End While
                'myReader1.Close()



                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
                End While
                myReader1.Close()



                'par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                'myReader1 = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
                'End While
                'myReader1.Close()



                DETRAZIONI_FRAGILE = 0
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.HasRows Then
                    While myReader1.Read
                        DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReader1("IMPORTO"), 0)
                        TOT_SPESE = TOT_SPESE + par.IfNull(myReader1("IMPORTO"), 0)

                        If DETRAZIONI_FRAGILE > 10000 Then
                            DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                        Else
                            DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        End If

                    End While
                    myReader1.Close()
                Else
                    If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        TOT_SPESE = TOT_SPESE + 10000
                    End If
                    myReader1.Close()
                End If



                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
                End While
                myReader1.Close()



                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    If par.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                        IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + par.IfNull(myReader1("VALORE"), 0)
                        MUTUI_RESIDENZA = MUTUI_RESIDENZA + par.IfNull(myReader1("MUTUO"), 0)
                    Else
                        IMMOBILI = IMMOBILI + par.IfNull(myReader1("VALORE"), 0)
                        MUTUI = MUTUI + par.IfNull(myReader1("MUTUO"), 0)
                    End If
                End While
                myReader1.Close()
                TOT_COMPONENTI = TOT_COMPONENTI + 1
            Loop
            'End If
            myReader.Close()

            MOBILI = MOBILI - 25000
            If MOBILI < 0 Then MOBILI = 0

            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
                INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
                INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
            End If
            myReader.Close()

            DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)


            'STRINGA_STAMPA = STRINGA_STAMPA & "<BR><BR><table border='1' cellpadding='0' cellspacing='0' width='95%'>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><font face='Arial' size='2'><b>CALCOLO ISEE per Decadenza</b></font>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><table border='0' cellpadding='0' cellspacing='0' width='81%' style='font-family: Arial; font-size: 8 pt'>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO COMPLESSIVO DEL NUCLEO FAMILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >FIGURATIVO REDDITO MOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI DAL REDDITO LORDO</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% CON INDENNITA'</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_CON & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >SPESE SOSTENUTE PER ASSISTENZA</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOT_SPESE, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% SENZA INDENNITA'</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_NO & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 66%-99%</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_66_99 & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Detrazioni per nucleo familiare affetto da fragilità (" & INV_100_CON + INV_100_NO + INV_66_99 & " invalidi)</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI_FR, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP

            ISEE_ERP = 0

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE DEL REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO MOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(FIGURATIVO_MOBILI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI PER MUTUI CONTRATTI</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format((IMMOBILI - MUTUI), "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE DELLA RESIDENZA DI PROPRIETA'</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >MUTUO RESIDUO PER LA RESIDENZA</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE NETTO DELLA CASA DI RESIDENZA</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA - MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA) - 25000
            If TOTALE_IMMOBILI < 0 Then TOTALE_IMMOBILI = 0

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE COMPLESSIVO DEL PATRIMONIO DA CONSIDERARE AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_IMMOBILI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Coefficiente della valutazione del patrimonio immobiliare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;0,20</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

            ISP_ERP = TOTALE_ISEE_ERP

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_ISEE_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>PATRIMONIO COMPLESSIVO AI FINI ERP</B></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(TOTALE_PATRIMONIO_ISEE_ERP, "##,##0.00")) & "</B></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"




            Select Case TOT_COMPONENTI
                Case 1
                    PARAMETRO = 1
                Case 2
                    PARAMETRO = (138 / 100)
                Case 3
                    PARAMETRO = (167 / 100)
                Case 4
                    PARAMETRO = (190 / 100)
                Case 5
                    PARAMETRO = (211 / 100)
                Case Else
                    PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
            End Select

            PARAMETRO_MINORI = 0
            VSE = PARAMETRO
            If adulti >= 2 Then
                VSE = VSE - (MINORI * (1 / 10))
                PARAMETRO_MINORI = (MINORI * (1 / 10))
            End If

            LIMITE_PATRIMONIO = 16000 + (6000 * VSE)


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>Limite Patrimonio Familiare</b></font></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</font></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(LIMITE_PATRIMONIO * 3, "##,##0.00")) & "</b></font></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"





            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di componenti del nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & TOT_COMPONENTI & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro corrispondente alla composizione del nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di minori di 15 anni nel nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & MINORI & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro per minori</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >-</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO_MINORI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di adulti nel nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & adulti & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISP_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            ISE_ERP = ISR_ERP + ISP_ERP

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >ISE-erp: indicatore della situazione economica (erp)</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISE_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VSE: Valore della scala di equivalenza</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(VSE, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            ISEE_ERP = ISE_ERP / VSE

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%'><font face='Arial' size='3'><b>ISEE-erp: Indicatore della Situazione Economica Equivalente (erp)<b></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(ISEE_ERP, "##,##0.00")) & "</b></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            ' ISEE_DICHIARAZIONE = par.Converti(Format(ISEE_ERP, "##,##0.00"))


            If ISEE_ERP > limite_isee Then

                'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            End If


            If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO * 3 Then
                'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            End If


            'STRINGA_STAMPA = STRINGA_STAMPA & "</table>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</table>"

        End If


        STRINGA_STAMPA = STRINGA_STAMPA & "</center>"



        CalcolaIsee = STRINGA_STAMPA

        'par.cmd.CommandText = "update utenza_dichiarazioni set " _
        '                       & "" _
        '                       & "" _
        '                       & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
        '                       & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
        '                       & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
        '                       & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "',PATR_SUPERATO='" & PatrSuperato & "' WHERE ID=" & Pratica_Id
        'par.cmd.ExecuteNonQuery()

    End Function




    Function AggiustaOggetti()
        'Response.Write("<script></script>")
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False And bEseguito = False Then
            'Response.Expires = 0
            txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            imgUscita.Attributes.Add("onclick", "javascript:Uscita=1;")
            cmbAnnoReddituale.Attributes.Add("onclick", "javascript:Uscita=1;")
            rdApplica.Attributes.Add("onclick", "javascript:document.getElementById('txt36').value=1;")
            rdNoApplica.Attributes.Add("onclick", "javascript:document.getElementById('txt36').value=0;")

            bMemorizzato = False
            lIdDichiarazione = Request.QueryString("ID")
            SoloLettura = Request.QueryString("LE")

            CHIUDI = Request.QueryString("CHIUDI")
            TORNA = Request.QueryString("TORNA")
            'If Session.Item("ID_CAF") = "11" Or Session.Item("ID_CAF") = "12" Then
            '    SoloLettura = "1"
            'End If
            If Session.Item("ID_CAF") = "6" Then
                If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                    SoloLettura = "1"
                    nonstampare.Value = "1"
                End If
            End If

            If Session.Item("LIVELLO") = "1" Then
                SoloLettura = "0"
            End If

            If Session.Item("PROP_DEC") = "1" Then
                propdec.Value = "1"
            Else
                propdec.Value = "0"
            End If
            If SoloLettura = "1" Then
                Label3.Visible = False
                cmbStato.Visible = False

                rdApplica.Enabled = False
                rdNoApplica.Enabled = False
                cmbAnnoReddituale.Enabled = False
            End If
            txtTab.Value = "1"

            'Modifiche Anagrafe Utenza
            'CType(Dic_Patrimonio1.FindControl("Label15"), Label).Visible = True
            'CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Visible = True
            'CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Visible = True

            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If

            'VISUALIZZO IMG_CANONE SOLO PER GLI ABUSIVI
            If par.IfNull(Request.QueryString("CR"), "") = "1" Then
                IMGCanone.Visible = True
            End If

            assTemp.Value = Request.QueryString("ASST")
            If lIdDichiarazione = -1 Then
                lNuovaDichiarazione = 1
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                CType(Dic_Utenza1.FindControl("txtbinserito"), HiddenField).Value = "0"
                NuovaDichiarazione()
                CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add("")

                CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = True
                CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Visible = True
                lotto45.Value = "1"
            Else
                lNuovaDichiarazione = 0
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                VisualizzaDichiarazione()
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=1','Anagrafe','top=0,left=0,width=600,height=400');")
                End If

            End If

            bEseguito = True
            AggiustaOggetti()
            CType(Dic_Reddito1.FindControl("label3"), Label).Text = "REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE AI FINI ISEE"

        End If


        CType(Dic_Reddito_Conv1.FindControl("Label1"), Label).Visible = False
        CType(Dic_Reddito_Conv1.FindControl("Label2"), Label).Visible = False


        scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "function CalcolaReddito() {window.open('RedditoConv.aspx?ID=" & lIdDichiarazione & "',null,'');}" _
            & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript3000")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript3000", scriptblock)
        End If
        If CHIUDI <> "1" Then
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "function Eventi() {window.open('Eventi.aspx?ID=" & lIdDichiarazione & "','Eventi','');}function Indici() {window.open('Indici.aspx?ID=" & lIdDichiarazione & "','Indici','top=0,left=0,width=490,height=650');}function PropDec() {if (document.getElementById('propdec').value == '1') {window.open('ProposteDec.aspx?ID=" & lIdDichiarazione & "&PG=" & lblPG.Text & "&I=0','Proposte','top=0,left=0,width=498,height=650');} else {alert('Non disponibile!');}}" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30006")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30006", scriptblock)
            End If
        Else
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "function Eventi() {window.open('Eventi.aspx?ID=" & lIdDichiarazione & "','Eventi','');}function Indici() {window.open('Indici.aspx?ID=" & lIdDichiarazione & "','Indici','top=0,left=0,width=490,height=650');}function PropDec() {if (document.getElementById('propdec').value == '1') {window.open('ProposteDec.aspx?ID=" & lIdDichiarazione & "&PG=" & lblPG.Text & "&I=0','Proposte','top=0,left=0,width=498,height=650');} else {alert('Non disponibile!');}}" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30006")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30006", scriptblock)
            End If
        End If


        CType(Dic_Utenza1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Utenza1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"


        CType(Dic_Nucleo1.FindControl("HyperLink1111"), HyperLink).Target = "_blank"
        CType(Dic_Nucleo1.FindControl("HyperLink1111"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"

        CType(Dic_Utenza1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Patrimonio1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"

        CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"

        CType(Dic_Reddito_Conv1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Reddito_Conv1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"

        CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"

        CType(Dic_Note1.FindControl("HyperLinkNote"), HyperLink).Target = "_blank"
        CType(Dic_Note1.FindControl("HyperLinkNote"), HyperLink).NavigateUrl = "ANAUT/Help_rel2.pdf"

        If SoloLettura = "1" Then
            'Dim TN As MenuItem

            'TN = MenuStampe.FindItem(" /10")
            'If Not IsNothing(TN) Then
            '    MenuStampe.Items.Remove(TN)
            'End If
            MenuStampe.Visible = False
            nonstampare.Value = "1"
        End If

        'MenuStampe.Visible = False

        'CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        'CType(Dic_Sottoscrittore1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        'CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"


        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next

    End Sub

    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        Call btnSalva_Click(sender, e)
        If bMemorizzato = True Then
            If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                Response.Write("<script>alert('Attenzione...Lo stato della dichiarazione deve essere COMPLETA!')</script>")
                Exit Sub
            End If
            CalcolaStampa()
            Session.Item("STAMPATO") = "1"
            lblISEE.Text = ISEE_DICHIARAZIONE
        Else
            imgStampa.Enabled = False
            imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
        End If
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Dim idComponenti(15) As Long
        Dim S As String
        Dim i As Integer
        Dim j As Integer
        'Dim progr As Integer
        Dim NUM_PARENTI As Integer
        Dim totconv As Double
        Dim TotISEE As Double
        Dim Applica_36 As String


        Try

            bMemorizzato = False

            NUM_PARENTI = 0

            N_INV_100_ACC = 0
            N_INV_100_NO_ACC = 0
            N_INV_100_66 = 0

            MAGGIORI_65 = 0
            MINORI_15 = 0
            PREVALENTE_DIP = 0


            S = ""
            If VerificaDati(S) = False Then
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                Exit Try
            End If



            If DateDiff("m", DateSerial(Mid(CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text, 7, 4), Mid(CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text, 4, 2), Mid(CType(Dic_Utenza1.FindControl("txtDataNascita"), TextBox).Text, 1, 2)), Now) / 12 < 18 Then
                Response.Write("<script>alert('Attenzione...Il richiedente deve essere maggiorenne! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                Exit Try
            End If

            If Len(CType(Dic_Utenza1.FindControl("txtCapRes"), TextBox).Text) < 5 Then
                Response.Write("<script>alert('Attenzione...Il CAP di Residenza deve essere 5 caratteri! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                Exit Try
            End If

            'Modifiche Anagrafe Utenza
            'If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Text <> "SI" And CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = True Then
            '    If cmbStato.SelectedItem.Text = "COMPLETA" Then
            '        Response.Write("<script>alert('ATTENZIONE...è possibile specificare la dislocazione dell\'immobile solo se la U.i. posseduta è adeguata al nucleo o di valore')</script>")
            '        imgStampa.Enabled = False
            '        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
            '        Exit Try
            '    Else
            '        imgStampa.Enabled = False
            '        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
            '    End If
            'End If


            If CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = True Then
                If CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Text = "" Then
                    Response.Write("<script>alert('ATTENZIONE...Specificare il codice di convocazione!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If
            End If

            If Len(CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text) = 19 Then
                CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Text = Mid(CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text, 1, 17)
            End If


            If CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked = False Then
                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                    Response.Write("<script>alert('ATTENZIONE...Specificare se intestatario di contratto o altro componente!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    Exit Try
                Else
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                End If
            End If

            If CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked = True And CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked = True Then
                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                    Response.Write("<script>alert('ATTENZIONE...Specificare se intestatario di contratto o altro componente!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    Exit Try
                Else
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                End If
            End If


            Dim COMUNITARIO As Boolean = True
            vcomunitario.Value = "0"

            If CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text <> "ITALIA" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text) & "'"
                Dim myReadernaz As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReadernaz.Read() Then
                    If par.IfNull(myReadernaz("SIGLA"), "") = "C" Or par.IfNull(myReadernaz("SIGLA"), "") = "I" Then
                        COMUNITARIO = True
                        vcomunitario.Value = "0"
                    Else
                        COMUNITARIO = False
                        vcomunitario.Value = "1"
                    End If

                Else
                    COMUNITARIO = False
                    vcomunitario.Value = "1"
                End If
                myReadernaz.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            If CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text <> "ITALIA" And CType(Dic_Note1.FindControl("ChNatoEstero"), CheckBox).Checked = False And COMUNITARIO = False And CType(Dic_Note1.FindControl("ChCittadinanza"), CheckBox).Checked = False Then


                If CType(Dic_Note1.FindControl("txtPSNum"), TextBox).Text = "" And CType(Dic_Note1.FindControl("txtCSNum"), TextBox).Text = "" Then
                    If cmbStato.SelectedItem.Text = "COMPLETA" Then
                        Response.Write("<script>alert('Attenzione! Intestatario extra comunitario. Inserire gli estremi del permesso o carta di soggiorno! Memorizzazione non effettuata.')</script>")
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                        Exit Try
                    Else
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    End If
                End If

                If par.IfEmpty(CType(Dic_Note1.FindControl("txtCSNum"), TextBox).Text, "") = "" Then
                    If Len(par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text))) = 8 Then
                        If par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text)) < Format(Now, "yyyyMMdd") Then
                            If par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) = "" Or par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) < par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text)) Then
                                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                                    Response.Write("<script>alert('Attenzione! Intestatario extra comunitario. Il permesso di soggiorno è scaduto! Memorizzazione non effettuata.')</script>")
                                    imgStampa.Enabled = False
                                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                                    Exit Try
                                Else
                                    imgStampa.Enabled = False
                                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                                End If
                            End If
                        End If
                    Else
                        If par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text)) < Format(Now, "yyyyMMdd") Then
                            If par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) = "" Or par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) < par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text)) Then
                                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                                    Response.Write("<script>alert('Attenzione! Intestatario extra comunitario. Il permesso di soggiorno è scaduto! Memorizzazione non effettuata.')</script>")
                                    imgStampa.Enabled = False
                                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                                    Exit Try
                                Else
                                    imgStampa.Enabled = False
                                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                                End If
                            End If
                        End If
                    End If
                End If

                If CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text = "//0" Then CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text = ""

                If CType(Dic_Note1.FindControl("cmbLavorativa"), DropDownList).SelectedItem.Value = 1 Then
                    If par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) <> "" Then
                        If DateDiff("d", CDate(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text), CDate(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) > 60 Then 'And frmBandiERP.casContinuativo.Text = "0" Then
                            Response.Write("<script>alert('Attenzione: il permesso di soggiorno risulta rinnovato oltre i termini di legge oppure non risulta una disoccupazione. La dichiarazione verrà inserita automaticamente nelle dichiarazioni -Da Verificare-')</script>")
                            CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = True

                        End If
                    End If
                Else
                    Response.Write("<script>alert('Attenzione: il permesso di soggiorno risulta rinnovato oltre i termini di legge oppure non risulta una disoccupazione. La dichiarazione verrà inserita automaticamente nelle dichiarazioni -Da Verificare-')</script>")
                    CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = True
                End If
            Else
                If CType(Dic_Note1.FindControl("txtCINum"), TextBox).Text = "" Or CType(Dic_Note1.FindControl("txtCIData"), TextBox).Text = "" Or CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).SelectedItem.Text = "--" Then
                    If cmbStato.SelectedItem.Text = "COMPLETA" Then
                        Response.Write("<script>alert('Attenzione!Inserire la tipologia, il numero e la data del documento di riconoscimento! Memorizzazione non effettuata.')</script>")
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                        Exit Try
                    Else
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    End If
                End If
            End If





            If CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = True And CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked = True Then
                Response.Write("<script>alert('Attenzione!La dichiarazione può essere sospesa o da verificare! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                Exit Try
            End If

            If CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = True Then
                If CType(Dic_Utenza1.FindControl("Sosp1"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("Sosp2"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("Sosp3"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("Sosp4"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("Sosp5"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("Sosp6"), CheckBox).Checked = False And CType(Dic_Utenza1.FindControl("Sosp7"), CheckBox).Checked = False Then
                    Response.Write("<script>alert('Attenzione!Indicare il motivo della sospensione! Memorizzazione non effettuata.')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If
            End If


            If CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked = True And cmbStato.SelectedItem.Text = "COMPLETA" Then
                Response.Write("<script>alert('ATTENZIONE...La dichiarazione non può essere completa se è in fase di sospensione! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                Exit Try
            End If

            If CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = False Then
                If CType(Dic_Utenza1.FindControl("txtPiano"), TextBox).Text = "" Then
                    If cmbStato.SelectedItem.Text = "COMPLETA" Then
                        Response.Write("<script>alert('ATTENZIONE...Inserire il piano! Memorizzazione non effettuata.')</script>")
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                        Exit Try
                    Else
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                    End If
                End If
            End If


            S = ""

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans



            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            'End If

            Dim sStringaSql As String



            Dim COD_PARENTE As Long
            Dim INDENNITA As String

            COD_PARENTE = 1
            INDENNITA = ""

            par.cmd.CommandText = "SELECT COD FROM T_TIPO_PARENTELA WHERE DESCRIZIONE='" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 81, 25) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                COD_PARENTE = myReader("COD")
            End If
            myReader.Close()
            'If Dic_Nucleo1.ProgrDaCancellare <> "" Then
            'sStringaSql = "DELETE FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & Dic_Nucleo1.ProgrDaCancellare & ")"
            'par.cmd.CommandText = sStringaSql
            'par.cmd.ExecuteNonQuery()
            'Else
            sStringaSql = "DELETE FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()


            'End If
            Dic_Nucleo1.ProgrDaCancellare = ""

            If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 120, 2) = "SI" Then
                INDENNITA = 1
            Else
                INDENNITA = 0
            End If

            If INDENNITA = 1 Then
                N_INV_100_ACC = N_INV_100_ACC + 1
            Else
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 107, 6)) = 100 Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                Else
                    If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 107, 6)) > 66 And Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 107, 6)) < 100 Then
                        N_INV_100_66 = N_INV_100_66 + 1
                    End If
                End If
            End If

            For i = 0 To 14
                idComponenti(i) = -1
            Next

            NUM_PARENTI = 0
            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                par.cmd.CommandText = "SELECT COD FROM T_TIPO_PARENTELA WHERE DESCRIZIONE='" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 25) & "'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    COD_PARENTE = myReader("COD")
                End If
                myReader.Close()
                If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 120, 2) = "SI" Then
                    INDENNITA = 1
                Else
                    INDENNITA = 0
                End If

                If CInt(Mid(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10)), 1, 4)) < 15 Then
                    MINORI_15 = 1
                End If

                If CInt(Mid(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10)), 1, 4)) > 65 Then
                    MAGGIORI_65 = 1
                End If

                par.cmd.CommandText = "SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'"
                Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader4.Read = False Then
                    sStringaSql = "INSERT INTO UTENZA_COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL,DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_UTENZA_COMP_NUCLEO.NEXTVAL," & lIdDichiarazione & "," & i & ",'" _
                                & Trim(par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25))) & "','" _
                                & Trim(par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25))) & "'," _
                                & COD_PARENTE & ",'" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10))) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "','" _
                                & INDENNITA & "','" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "')"
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_UTENZA_COMP_NUCLEO.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponenti(i) = myReader(0)
                    End If
                    myReader.Close()
                Else
                    idComponenti(i) = myReader4(0)
                    sStringaSql = "UPDATE UTENZA_COMP_NUCLEO set PROGR=" & i & ",COGNOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25)) & "'," _
                                & "NOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25)) & "'," _
                                & "GRADO_PARENTELA=" & COD_PARENTE & "," _
                                & "COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'," _
                                & "PERC_INVAL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) & "'," _
                                & "DATA_NASCITA='" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10))) & "'," _
                                & "USL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "'," _
                                & "INDENNITA_ACC='" & INDENNITA & "'," _
                                & "SESSO='" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "' " _
                                & "WHERE ID=" & idComponenti(i)
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                End If

                myReader4.Close()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_REDDITI WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()





                'If Dic_Patrimonio1.idCOMPMOB <> "" Then
                '    sStringaSql = "DELETE FROM COMP_PATR_MOB WHERE id_componente=" & idComponenti(i) & " and " & Dic_Patrimonio1.idCOMPMOB & ")"
                '    par.cmd.CommandText = sStringaSql
                '    par.cmd.ExecuteNonQuery()
                'End If
                'Dic_Patrimonio1.idCOMPMOB = ""


                NUM_PARENTI = NUM_PARENTI + 1
                If INDENNITA = 1 Then
                    If i <> 0 Then
                        N_INV_100_ACC = N_INV_100_ACC + 1
                    End If
                Else
                    If i <> 0 Then
                        If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) = 100 Then
                            N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                        Else
                            If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) > 66 And Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) < 100 Then
                                N_INV_100_66 = N_INV_100_66 + 1
                            End If
                        End If
                    End If
                End If
            Next i


            par.cmd.CommandText = "DELETE FROM UTENZA_DOC_MANCANTE WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()

            For i = 0 To CType(Dic_Documenti1.FindControl("listbox1"), ListBox).Items.Count - 1
                par.cmd.CommandText = "INSERT INTO UTENZA_DOC_MANCANTE (ID_DICHIARAZIONE,ID_DOC,DESCRIZIONE) VALUES (" & lIdDichiarazione & "," & CType(Dic_Documenti1.FindControl("listbox1"), ListBox).Items(i).Value & ",'" & par.PulisciStrSql(CType(Dic_Documenti1.FindControl("listbox1"), ListBox).Items(i).Text) & "')"
                par.cmd.ExecuteNonQuery()
            Next


            Dim cat_Imm As String
            Dim AU As String

            Dim Tipo_Ass As String


            If CType(Dic_Utenza1.FindControl("R1"), RadioButton).Checked = True Then
                Tipo_Ass = "1"
            Else
                Tipo_Ass = "0"
            End If
            'Modifiche Anagrafe Utenza
            cat_Imm = ""
            AU = ",POSSESSO_UI=NULL"
            'If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = False Then
            '    cat_Imm = ""
            'Else
            '    cat_Imm = ",ID_TIPO_CAT_AB=" & CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Value
            'End If

            'If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = False Then
            '    AU = ",POSSESSO_UI=NULL"
            'Else
            '    AU = ",POSSESSO_UI=" & CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Value
            'End If

            Applica_36 = "0"
            If cmbAnnoReddituale.Items.FindByValue("2007").Selected = True Then
                If rdApplica.Checked = True Then
                    Applica_36 = "1"
                Else
                    Applica_36 = "0"
                End If
            End If


            Dim cod_convocazione As String = ""

            If CType(Dic_Utenza1.FindControl("lbl45_Lotto"), Label).Visible = True Then
                cod_convocazione = CType(Dic_Utenza1.FindControl("txtCodConvocazione"), TextBox).Text
            End If


            Dim item As MenuItem
            If CType(Dic_Utenza1.FindControl("chInServizio"), CheckBox).Checked = True Then
                item = New MenuItem("Autocert.stato di servizio", "AutocertStServ", "", "javascript:AutocertStServ();")
                MenuStampe.Items(0).ChildItems.AddAt(MenuStampe.Items(0).ChildItems.Count - 1, item)
            End If
            sStringaSql = ""
            sStringaSql = "UPDATE UTENZA_DICHIARAZIONI SET PRESENZA_MIN_15=" & MINORI_15 & ",PRESENZA_MAG_65=" & MAGGIORI_65 & ", COD_CONVOCAZIONE='" & cod_convocazione & "',fl_applica_36='1',FL_UBICAZIONE='0', data_decorrenza='" & par.AggiustaData(CType(Dic_Utenza1.FindControl("txtDataDec"), TextBox).Text) & "',data_cessazione='" & par.AggiustaData(CType(Dic_Utenza1.FindControl("txtDataCessazione"), TextBox).Text) _
                          & "',PG='" & lblPG.Text & "', DATA_PG='" _
                          & par.AggiustaData(txtDataPG.Text) & "',LUOGO='Milano',DATA='" _
                          & par.AggiustaData(CType(Dic_Utenza1.FindControl("txtData1"), TextBox).Text) _
                          & "',NOTE_WEB='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtNote"), TextBox).Text) _
                          & "',ID_STATO=" & cmbStato.SelectedItem.Value _
                          & ",N_COMP_NUCLEO=" & NUM_PARENTI & ",N_INV_100_CON=" & N_INV_100_ACC _
                          & ",N_INV_100_SENZA=" & N_INV_100_NO_ACC _
                          & ",N_INV_100_66=" & N_INV_100_66 _
                          & AU _
                          & cat_Imm & ",ANNO_SIT_ECONOMICA=" & Val(CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text) _
                          & ",LUOGO_S='Milano',DATA_S='" _
                          & "',LUOGO_INT_ERP='Milano',DATA_INT_ERP='" & par.AggiustaData(CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text) _
                          & "',MINORI_CARICO='" & Val(CType(Dic_Reddito_Conv1.FindControl("txtMinori"), TextBox).Text) & "',TIPO_ASS='" & Tipo_Ass & "',ALLOGGIO='" _
                          & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtAlloggio"), TextBox).Text) _
                          & "',PIANO='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtPiano"), TextBox).Text) _
                          & "',POSIZIONE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Text) _
                          & "',RAPPORTO='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text) _
                          & "',RAPPORTO_REALE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtRapportoReale"), TextBox).Text) _
                          & "',SCALA='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtScala"), TextBox).Text) _
                          & "',INT_A='" & Valore01(CType(Dic_Utenza1.FindControl("ch3"), CheckBox).Checked) _
                          & "',INT_V='" & Valore01(CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked) _
                          & "',INT_C='" & Valore01(CType(Dic_Utenza1.FindControl("ch1"), CheckBox).Checked) _
                          & "',INT_M='" & Valore01(CType(Dic_Utenza1.FindControl("ch4"), CheckBox).Checked) _
                          & "',FL_TUTORE='" & Valore01(CType(Dic_Utenza1.FindControl("ch5"), CheckBox).Checked) _
                          & "',FL_DELEGATO='" & Valore01(CType(Dic_Utenza1.FindControl("ch6"), CheckBox).Checked) _
                          & "',FL_RIC_POSTA='" & Valore01(CType(Dic_Utenza1.FindControl("chPosta"), CheckBox).Checked) _
                          & "',FL_DA_VERIFICARE='" & Valore01(CType(Dic_Utenza1.FindControl("X1"), CheckBox).Checked) _
                          & "',FL_SOSPENSIONE='" & Valore01(CType(Dic_Utenza1.FindControl("X2"), CheckBox).Checked) _
                          & "',FL_SOSP_1='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp1"), CheckBox).Checked) _
                          & "',FL_SOSP_2='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp2"), CheckBox).Checked) _
                          & "',FL_SOSP_3='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp3"), CheckBox).Checked) _
                          & "',FL_SOSP_4='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp4"), CheckBox).Checked) _
                          & "',FL_SOSP_5='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp5"), CheckBox).Checked) _
                          & "',FL_SOSP_6='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp6"), CheckBox).Checked) _
                          & "',FL_SOSP_7='" & Valore01(CType(Dic_Utenza1.FindControl("Sosp7"), CheckBox).Checked) _
                          & "',COD_ALLOGGIO='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtCodAlloggio"), TextBox).Text) _
                          & "',FOGLIO='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtFoglio"), TextBox).Text) _
                          & "',MAPPALE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtMappale"), TextBox).Text) _
                          & "',SUB='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtSub"), TextBox).Text) _
                          & "', TIPO_DOCUMENTO=" & CType(Dic_Note1.FindControl("cmbTipoDocumento"), DropDownList).SelectedItem.Value & "," _
                          & "CARTA_I='" & par.PulisciStrSql(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtCINum"), TextBox).Text)) & "'," _
                          & "CARTA_I_DATA='" & par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtCIData"), TextBox).Text)) & "'," _
                          & "CARTA_I_RILASCIATA='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtCIRilascio"), TextBox).Text) & "'," _
                          & "PERMESSO_SOGG_N='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSNum"), TextBox).Text) & "'," _
                          & "PERMESSO_SOGG_DATA='" & par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSData"), TextBox).Text)) & "'," _
                          & "PERMESSO_SOGG_SCADE='" & par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSScade"), TextBox).Text)) & "'," _
                          & "PERMESSO_SOGG_RINNOVO='" & par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtPSRinnovo"), TextBox).Text)) & "'," _
                          & "CARTA_SOGG_N='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtCSNum"), TextBox).Text) & "'," _
                          & "FL_NATO_ESTERO='" & Valore01(CType(Dic_Note1.FindControl("ChNatoEstero"), CheckBox).Checked) & "'," _
                          & "FL_CITTADINO_IT='" & Valore01(CType(Dic_Note1.FindControl("ChCittadinanza"), CheckBox).Checked) & "'," _
                          & "CARTA_SOGG_DATA='" & par.AggiustaData(par.PulisciStrSql(CType(Dic_Note1.FindControl("txtCSData"), TextBox).Text)) & "', " _
                          & "FL_ATTIVITA_LAV='" & CType(Dic_Note1.FindControl("cmbLavorativa"), DropDownList).SelectedItem.Value & "' "






            If CType(Dic_Utenza1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                sStringaSql = sStringaSql & ",ID_LUOGO_RES_DNTE=" & CType(Dic_Utenza1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Value _
                              & ",ID_TIPO_IND_RES_DNTE=" & CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
                              & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtIndRes"), TextBox).Text) _
                              & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtCivicoRes"), TextBox).Text) _
                              & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtTelRes"), TextBox).Text) & "' "
            Else
                sStringaSql = sStringaSql & ",ID_LUOGO_RES_DNTE=" & CType(Dic_Utenza1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Value _
                              & ",ID_TIPO_IND_RES_DNTE=" & CType(Dic_Utenza1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
                              & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtIndRes"), TextBox).Text) _
                              & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtCivicoRes"), TextBox).Text) _
                              & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtTelRes"), TextBox).Text) & "' "
            End If

            If CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_NAS_DNTE=" & CType(Dic_Utenza1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Value
            Else
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Utenza1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_NAS_DNTE=" & CType(Dic_Utenza1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Value
            End If


            If FFOO = True Then
                sStringaSql = sStringaSql & ",FL_IN_SERVIZIO=" & Valore01(CType(Dic_Utenza1.FindControl("chInServizio"), CheckBox).Checked)
            End If
            par.cmd.CommandText = sStringaSql & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()

            Dim INDICE As Integer



            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

                    For j = 0 To cmbComp.Items.Count - 1
                        If cmbComp.Items(j).Value = CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Value Then
                            INDICE = j
                            Exit For
                        End If
                    Next

                    sStringaSql = "INSERT INTO UTENZA_COMP_ELENCO_SPESE (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_UTENZA_COMP_ELENCO_SPESE.NEXTVAL," & idComponenti(INDICE) & "," _
                               & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",'" _
                               & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 66, 17)) & "')"
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()

                End If
            Next i





            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                ' par.cmd.CommandText = "SELECT ID FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & idComponenti(INDICE)
                ' Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'If myReader5.Read = False Then
                sStringaSql = "INSERT INTO UTENZA_COMP_PATR_MOB (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES (SEQ_UTENZA_COMP_PATR_MOB.NEXTVAL," & idComponenti(INDICE) & "," _
                           & "'" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 27)) & "','" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 55, 16)) & "'," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8)) & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                'Else
                'sStringaSql = "UPDATE COMP_PATR_MOB SET COD_INTERMEDIARIO='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 13)) & "'," _
                '            & "INTERMEDIARIO='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 41, 30)) & "',IMPORTO=" & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8)) _
                '            & " WHERE ID=" & myReader5(0)
                'par.cmd.CommandText = sStringaSql
                'par.cmd.ExecuteNonQuery()
                'End If
            Next i

            Dim ID_TIPO As Integer
            Dim RESIDENZA As String
            Dim FL_70KM As String = "0"
            Dim PienaP As Integer = 0

            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20)
                    Case "FABBRICATI"
                        ID_TIPO = 0
                    Case "TERRENI AGRICOLI"
                        ID_TIPO = 1
                    Case "TERRENI EDIFICABILI"
                        ID_TIPO = 2
                End Select

                If par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 132, 2) = "SI" Then
                    PienaP = 1
                Else
                    PienaP = 0
                End If


                RESIDENZA = "0"

                '    Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2)
                '        Case "SI"
                '            RESIDENZA = "1"
                '        Case Else
                '            RESIDENZA = "0"
                'End Select

                FL_70KM = "0"
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 86, 30)) & "'"
                Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCC.Read() Then
                    If myReaderCC("DISTANZA_KM") <= 70 Then
                        FL_70KM = "1"
                    End If
                    If myReaderCC("DISTANZA_KM") = "0" Then
                        Response.Write("<script>alert('Attenzione...per il comune selezionato  non è stata specificata la distanza in km. Il calcolo del canone potrebbe ERRATO. Contattare il responsabile');</script>")
                    End If
                End If
                myReaderCC.Close()

                'par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 86, 30))

                sStringaSql = "INSERT INTO UTENZA_COMP_PATR_IMMOB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,INDIRIZZO,CIVICO,REND_CATAST_DOMINICALE) VALUES " _
                                & " (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                                & "," & par.VirgoleInPunti(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6)) _
                                & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 8)) _
                                & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 67, 8)) _
                                & ",'" & RESIDENZA _
                                & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 82, 3) _
                                & "','" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 86, 30)) _
                                & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 117, 2) _
                                & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 120, 7) _
                                & "','" & FL_70KM & "'," & PienaP & ",'" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 135, 25) & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 161, 5) & "'," & par.IfEmpty(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 167, 8), "0") & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i

            For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next
                sStringaSql = "INSERT INTO UTENZA_COMP_REDDITO (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_UTENZA_COMP_REDDITO.NEXTVAL," _
                           & idComponenti(INDICE) & "," _
                           & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) _
                           & "," & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

            Next i

            'sStringaSql = "DELETE FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            'par.cmd.CommandText = sStringaSql
            'par.cmd.ExecuteNonQuery()



            'If CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1" Then
            '    sStringaSql = "INSERT INTO SOTTOSCRITTORI (ID_DICHIARAZIONE) VALUES (" & lIdDichiarazione & ")"
            '    par.cmd.CommandText = sStringaSql
            '    par.cmd.ExecuteNonQuery()

            '    If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
            '        sStringaSql = "UPDATE SOTTOSCRITTORI SET " _
            '        & "ID_LUOGO_RES=" & CType(Dic_Sottoscrittore1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Value _
            '        & ",ID_TIPO_IND=" & CType(Dic_Sottoscrittore1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
            '        & ",IND='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtIndRes"), TextBox).Text) _
            '        & "',CIVICO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCivicoRes"), TextBox).Text) _
            '        & "',TELEFONO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtTelRes"), TextBox).Text) _
            '        & "',COGNOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text) _
            '        & "',NOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text) _
            '        & "',DATA_NAS='" & par.AggiustaData(CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text) & "' " _
            '        & ",CAP_RES='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text) & "' "
            '    Else
            '        sStringaSql = "UPDATE SOTTOSCRITTORI SET " _
            '        & "ID_LUOGO_RES=" & CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Value _
            '        & ",ID_TIPO_IND=" & CType(Dic_Sottoscrittore1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
            '        & ",IND='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtIndRes"), TextBox).Text) _
            '        & "',CIVICO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCivicoRes"), TextBox).Text) _
            '        & "',TELEFONO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtTelRes"), TextBox).Text) _
            '        & "',COGNOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text) _
            '        & "',NOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text) _
            '        & "',DATA_NAS='" & par.AggiustaData(CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text) & "' " _
            '        & ",CAP_RES='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text) & "' "
            '    End If

            '    If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
            '        sStringaSql = sStringaSql & ",ID_LUOGO_NAS=" & CType(Dic_Sottoscrittore1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Value
            '    Else
            '        sStringaSql = sStringaSql & ",ID_LUOGO_NAS=" & CType(Dic_Sottoscrittore1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Value
            '    End If

            '    par.cmd.CommandText = sStringaSql & " WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            '    par.cmd.ExecuteNonQuery()

            'End If


            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                sStringaSql = "INSERT INTO UTENZA_COMP_ALTRI_REDDITI (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                            & " (SEQ_UTENZA_COMP_ALTRI_REDDITI.NEXTVAL," & idComponenti(INDICE) & "," _
                            & "" & Val(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 52, 8)) _
                            & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i


            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                Select Case par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 32, 35)
                    Case "IRPEF"
                        ID_TIPO = 0
                    Case "Spese Sanitarie"
                        ID_TIPO = 1
                    Case "Ricovero in strut. sociosanitarie"
                        ID_TIPO = 2
                End Select

                sStringaSql = "INSERT INTO UTENZA_COMP_DETRAZIONI (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                            & " (SEQ_UTENZA_COMP_DETRAZIONI.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                            & "," & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 68, 8) _
                            & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i


            For i = 0 To CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items.Count - 1

                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                sStringaSql = "INSERT INTO UTENZA_REDDITI (ID,ID_UTENZA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,occasionali,dom_ag_fab,ONERI) VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & idComponenti(INDICE) & ",'" _
                           & Val(par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 5)) & "','" _
                           & Val(par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 44, 5)) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 51, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 62, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 73, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 84, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 95, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 106, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 117, 7) & "')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i

            If lNuovaDichiarazione = 1 Then
                sStringaSql = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F130','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                Session.Add("ID_NUOVA_DIC", lblPG.Text)
                lNuovaDichiarazione = 0
            Else
                sStringaSql = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F131','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            End If

            'par.cmd.CommandText = "SELECT ID,PG FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    sStringaSql = "UPDATE DOMANDE_BANDO SET FL_RINNOVO='1',ISBAR=0,ISBARC=0,ISBARC_R=0,DISAGIO_F=0,DISAGIO_E=0,DISAGIO_A=0,REDDITO_ISEE=0,ID_STATO='2',fl_completa='1',FL_ESAMINATA='0',FL_ISTRUTTORIA_COMPLETA='0' WHERE ID=" & myReader("ID")
            '    par.cmd.CommandText = sStringaSql
            '    par.cmd.ExecuteNonQuery()
            '    If cmbStato.Text <> "2" Then
            '        Label9.Text = "LA DOMANDA DEVE ESSERE RIELABORATA!!"
            '        scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                    & "alert('ATTENZIONE, La domanda " & myReader("PG") & " deve essere nuovamente elaborata e stampata!');" _
            '                    & "</script>"
            '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript29")) Then
            '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript29", scriptblock)
            '        End If
            '    Else
            '        Label9.Text = ""
            '        scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                    & "alert('ATTENZIONE, Questa dichiarazione  e la domanda " & myReader("PG") & " saranno cancellate!');" _
            '                    & "</script>"
            '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript300")) Then
            '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript300", scriptblock)
            '        End If

            '    End If

            'End If
            totconv = 0

            Dim DIP As Double = 0
            Dim ALT As Double = 0

            par.cmd.CommandText = "SELECT * FROM utenza_redditi WHERE ID_utenza=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                totconv = totconv + CDbl(par.IfNull(myReader("DIPENDENTE"), 0)) + CDbl(par.IfNull(myReader("PENSIONE"), 0)) + CDbl(par.IfNull(myReader("OCCASIONALI"), 0)) + CDbl(par.IfNull(myReader("AUTONOMO"), 0)) + CDbl(par.IfNull(myReader("NON_IMPONIBILI"), 0)) + CDbl(par.IfNull(myReader("DOM_AG_FAB"), 0))

                DIP = DIP + CDbl(par.IfNull(myReader("DIPENDENTE"), 0)) + CDbl(par.IfNull(myReader("PENSIONE"), 0)) + CDbl(par.IfNull(myReader("NON_IMPONIBILI"), 0))
                ALT = ALT + CDbl(par.IfNull(myReader("OCCASIONALI"), 0)) + CDbl(par.IfNull(myReader("AUTONOMO"), 0)) + CDbl(par.IfNull(myReader("DOM_AG_FAB"), 0))
            End While
            myReader.Close()



            par.cmd.CommandText = "SELECT * FROM utenza_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & myReader1("ID")
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    TotISEE = TotISEE + CDbl(par.IfNull(myReader("REDDITO_IRPEF"), 0)) + CDbl(par.IfNull(myReader("PROV_AGRARI"), 0))
                End While
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader1("ID")
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    TotISEE = TotISEE + CDbl(par.IfNull(myReader("IMPORTO"), 0))
                End While
                myReader.Close()

            End While
            myReader1.Close()

            Dim daS As Boolean = True

            If (TotISEE < 9230 Or TotISEE > 35000) And TotISEE > 0 Then
                'If (TotISEE < 9230) And TotISEE > 0 Then
                If totconv <> TotISEE And totconv > 0 Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                        & "alert('ATTENZIONE, ci sono delle incongruenze nei redditi effettivi ed Isee. In questi casi il calcolo dell ISEE e del canone di locazione non sono attendibili!');" _
                                        & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                    End If
                    daS = False
                End If


                If totconv = 0 Then
                    daS = False
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE, Impossibile Stampare. Compilare i campi relativi ai redditi effettivi!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                    End If
                End If

            Else
                If totconv > 0 Then
                    'daS = False
                    'scriptblock = "<script language='javascript' type='text/javascript'>" _
                    '& "alert('ATTENZIONE, Impossibile Stampare. Dichiarazione NON in area di protezione, i dati relativi ai redditi effettivi devono essere cancellati e assicurarsi di aver inserito i dati reddituali nei campi corretti!');" _
                    '& "</script>"
                    'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                    '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                    'End If
                    If totconv <> TotISEE Then
                        daS = False
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                                            & "alert('ATTENZIONE, ci sono delle incongruenze nei redditi effettivi ed Isee. In questi casi il calcolo dell ISEE e del canone di locazione non sono attendibili!');" _
                                            & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                        End If
                    End If
                End If
            End If

            If DIP > ((ALT + DIP) * 80) / 100 Then
                PREVALENTE_DIP = 1
            Else
                PREVALENTE_DIP = 0
            End If

            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET PREVALENTE_DIP = " & PREVALENTE_DIP & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            myReader.Close()

            bMemorizzato = True
            If daS = True Then
                imgStampa.Enabled = True
                imgStampa.ImageUrl = "..\NuoveImm\Img_Stampa.png"
            Else
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                bMemorizzato = False
            End If

            txtModificato.Value = "0"

            If Session.Item("ANAGRAFE") = "1" Then
                'imgAnagrafe.Attributes.Clear()
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=1','Anagrafe','top=0,left=0,width=600,height=400');")
            End If

        Catch EX As Exception
            Label4.Text = EX.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        Finally
        End Try
    End Sub

    Private Function CalcolaRedditoDatabase(ByVal Pratica_Id As String) As String

        Dim RA As Decimal
        Dim RD As Decimal
        Dim RF As Decimal
        Dim OD As Decimal

        Dim RA1 As Decimal
        Dim RD1 As Decimal
        Dim RF1 As Decimal
        Dim OD1 As Decimal

        Dim REDDITO_ERP As Decimal
        Dim REDDITO_EQUO As Decimal
        Dim REDDITO_INVAL1 As Integer
        Dim REDDITO_INVAL2 As Integer
        Dim REDDITO_FIGLIO As Decimal
        Dim REDDITO_MINORE As Decimal
        Dim REDDITO_CONV As Decimal
        Dim LIMITE_REDDITO1 As Decimal
        Dim LIMITE_REDDITO2 As Decimal
        Dim LIMITE_REDDITO3 As Decimal
        Dim LIMITE_REDDITO4 As Decimal
        Dim LIMITE_REDDITO_FIGLIO As Decimal
        Dim LIMITE_REDDITO_MINORE As Decimal
        Dim MINORI As Integer
        Dim FIGLI As Integer

        Dim I As Integer

        Dim Invalidi As Integer
        Dim Maggiorazione As Decimal
        Dim percentuale_app As Integer
        Dim COMPONENTI As Integer
        Dim PERC_LAVORO_DIP As Integer
        Dim ComponentiNucleo()
        Dim RedditoNucleo()
        Dim TotaleLordo As Decimal
        Dim MiaStringa As String


        TotaleLordo = 0

        MiaStringa = ""

        'par.OracleConn.Open()
        'par.SettaCommand(par)

        par.cmd.CommandText = "SELECT PARAMETER.* FROM PARAMETER"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read
            Select Case myReader("ID")
                Case 0
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        REDDITO_ERP = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Limite Reddito ERP non valido!"
                        Exit Function
                    End If
                Case 1
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        REDDITO_EQUO = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Limite Reddito Ex Equo Canone non valido!"
                        Exit Function
                    End If
                Case 4
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        REDDITO_INVAL1 = PuntiInVirgole(Val(par.IfNull(myReader("VALORE"), 0)))
                    Else
                        CalcolaRedditoDatabase = "Maggiorazione per un invalido non valida!"
                        Exit Function
                    End If
                Case 5
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        REDDITO_INVAL2 = PuntiInVirgole(Val(par.IfNull(myReader("VALORE"), 0)))
                    Else
                        CalcolaRedditoDatabase = "Maggiorazione per uno o più invalidi non valida!"
                        Exit Function
                    End If
                Case 2
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        REDDITO_FIGLIO = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Detrazioni per figlio a carico non valide!"
                        Exit Function
                    End If
                Case 3
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        REDDITO_MINORE = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Detrazioni per minore a carico non valide!"
                        Exit Function
                    End If
                Case 9 '1/2 PERSONE
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        LIMITE_REDDITO1 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Detrazioni per minore a carico non valide!"
                        Exit Function
                    End If
                Case 10 '3/4 PERSONE
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        LIMITE_REDDITO2 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Detrazioni per minore a carico non valide!"
                        Exit Function
                    End If
                Case 11 '5/6 PERSONE
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        LIMITE_REDDITO3 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Detrazioni per minore a carico non valide!"
                        Exit Function
                    End If
                Case 12 '7 O + PERSONE
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        LIMITE_REDDITO4 = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Detrazioni per minore a carico non valide!"
                        Exit Function
                    End If
                Case 13 'REDDITO PER FIGLIO A CARICO
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        LIMITE_REDDITO_FIGLIO = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Limite Reddito per figlio a carico non valido!"
                        Exit Function
                    End If
                Case 15 'REDDITO PER MINROE A CARICO
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        LIMITE_REDDITO_MINORE = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Limite Reddito per Minore a carico non valido!"
                        Exit Function
                    End If
                Case 17 'PERCENTUALE DI DETRAZIONE PER REDDITO DA DIPENDENTE/PENSIONE E FIGLI
                    If IsNumeric(par.IfNull(myReader("VALORE"), 0)) Then
                        PERC_LAVORO_DIP = PuntiInVirgole(par.IfNull(myReader("VALORE"), 0))
                    Else
                        CalcolaRedditoDatabase = "Percentuale di detrazione per lavoro dipendente o pensione non valido!"
                        Exit Function
                    End If

            End Select

        End While
        myReader.Close()


        RA = 0
        RD = 0
        RF = 0
        OD = 0

        RA1 = 0
        RD1 = 0
        RF1 = 0
        OD1 = 0

        MINORI = 0
        FIGLI = 0


        par.cmd.CommandText = "select * from utenza_DICHIARAZIONI where id=" & Pratica_Id
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            MINORI = Val(myReader("MINORI_CARICO"))
        Else
            MINORI = 0
        End If

        myReader.Close()


        par.cmd.CommandText = "select COUNT(ID) from utenza_comp_nucleo where id_dichiarazione=" & Pratica_Id & " order by progr asc"
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            COMPONENTI = myReader(0)
        Else
            COMPONENTI = 0
        End If
        ReDim ComponentiNucleo(COMPONENTI)
        ReDim RedditoNucleo(COMPONENTI)
        myReader.Close()

        par.cmd.CommandText = "select * from utenza_comp_nucleo where id_dichiarazione=" & Pratica_Id & " order by progr asc"
        myReader = par.cmd.ExecuteReader()



        'If myReader.Read Then
        I = 1
        While myReader.Read
            ComponentiNucleo(I) = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")

            If par.IfNull(myReader("PERC_INVAL"), 0) > 66 Then
                Invalidi = Invalidi + 1
            End If
            I = I + 1
        End While

        'MYREC.MoveFirst()



        Maggiorazione = 0
        Select Case Invalidi
            Case 1
                Maggiorazione = ((REDDITO_ERP * REDDITO_INVAL1) / 100)
                percentuale_app = REDDITO_INVAL1
            Case Is > 1
                Maggiorazione = ((REDDITO_ERP * REDDITO_INVAL2) / 100)
                percentuale_app = REDDITO_INVAL2
            Case 0
                Maggiorazione = 0
        End Select

        myReader.Close()

        par.cmd.CommandText = "select * from utenza_redditi where id_utenza=" & Pratica_Id
        myReader = par.cmd.ExecuteReader()


        I = 1

        While myReader.Read




            RA = RA + par.IfNull(myReader("AUTONOMO"), 0) + par.IfNull(myReader("OCCASIONALI"), 0)
            RA1 = RA1 + par.IfNull(myReader("AUTONOMO"), 0) + par.IfNull(myReader("OCCASIONALI"), 0)


            RD = RD + par.IfNull(myReader("dipendente"), 0) + par.IfNull(myReader("pensione"), 0)
            RD1 = RD1 + par.IfNull(myReader("dipendente"), 0) + par.IfNull(myReader("pensione"), 0)


            RF = RF + par.IfNull(myReader("dom_ag_fab"), 0)
            RF1 = RF1 + par.IfNull(myReader("dom_ag_fab"), 0)


            OD = OD + par.IfNull(myReader("oneri"), 0)
            OD1 = OD1 + par.IfNull(myReader("oneri"), 0)

            par.cmd.CommandText = "select PROGR from utenza_COMP_NUCLEO where id=" & myReader("ID_COMPONENTE")
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                RedditoNucleo(myReader1("PROGR") + 1) = Format(RA1 + RD1 + RF1 - OD1, "##,##0.00")
            End If
            myReader1.Close()
            TotaleLordo = TotaleLordo + Format(RA1 + RD1 + RF1 - OD1, "##,##0.00")
            RA1 = 0
            RD1 = 0
            RF1 = 0
            OD1 = 0

            I = I + 1
        End While
        'End If

        Select Case COMPONENTI
            Case 1, 2
                If RF > LIMITE_REDDITO1 Then
                    MiaStringa = "Limite Reddito da beni Immobili (1/2 persone) superato!" & vbCrLf
                    MiaStringa = "</BR>"
                End If
            Case 3, 4
                If RF > LIMITE_REDDITO2 Then
                    MiaStringa = "Limite Reddito da beni Immobili (3/4 persone) superato!" & vbCrLf
                    MiaStringa = "</BR>"
                End If
            Case 5, 6
                If RF > LIMITE_REDDITO3 Then
                    MiaStringa = "Limite Reddito da beni Immobili (5/6 persone) superato!"
                    MiaStringa = "</BR>"
                End If
            Case Else
                If RF > LIMITE_REDDITO4 Then
                    MiaStringa = "Limite Reddito da beni Immobili (7 o + persone) superato!"
                    MiaStringa = "</BR>"
                End If
        End Select

        'REDDITO_CONV = RA + (RD - ((RD * PERC_LAVORO_DIP) / 100)) - ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO)) + RF - OD
        Dim MM As Decimal

        If RD > 0 And RA = 0 And RF = 0 Then
            MM = (RD - ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO)) - OD)
            REDDITO_CONV = RA + (MM - ((MM * PERC_LAVORO_DIP) / 100))
        Else
            If RD = 0 And (RA > 0 Or RF > 0) Then
                MM = ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO))
                REDDITO_CONV = RA + RF - OD - MM
            Else
                MM = (RD - ((MINORI * REDDITO_MINORE) + (FIGLI * REDDITO_FIGLIO)))
                REDDITO_CONV = RA + (MM - ((MM * PERC_LAVORO_DIP) / 100)) + RF - OD
            End If
        End If
        MiaStringa = MiaStringa & vbCrLf & "<BR>"



        MiaStringa = MiaStringa & "<center>"
        MiaStringa = MiaStringa & "<table border='1' cellpadding='0' cellspacing='0' width='95%'>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='95%'><table border='0' cellpadding='0' cellspacing='0' width='95%'>"
        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='95%' ><p align='left'><b><font face='Arial' size='2'>CALCOLO REDDITO CONVENZIONALE</font></b></p></td>"
        MiaStringa = MiaStringa & "</tr>"
        MiaStringa = MiaStringa & "</table>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "</tr>"

        'MiaStringa = MiaStringa & "<center>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='95%'><table border='0' cellpadding='0' cellspacing='0' width='71%' style='font-family: Arial; font-size: 8 pt'>"
        'MiaStringa = MiaStringa & "<tr>"
        'MiaStringa = MiaStringa & "<td width='21%' >Limite Reddito Convenzionale</td>"
        'MiaStringa = MiaStringa & "<td width='8%' ></td>"
        'MiaStringa = MiaStringa & "<td width='2%' >Euro</td>"
        'MiaStringa = MiaStringa & "<td width='40%' >" & Format(REDDITO_ERP, "##,##0.00") & "</td>"
        'MiaStringa = MiaStringa & "</tr>"


        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30%' >N° Invalidi</td>"
        MiaStringa = MiaStringa & "<td width='25%' >&nbsp;" & Invalidi & "</td>"
        MiaStringa = MiaStringa & "<td width='10%' ></td>"
        MiaStringa = MiaStringa & "<td width='20%'></td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30%' >Maggiorazione (" & percentuale_app & "%)</td>"
        MiaStringa = MiaStringa & "<td width='25%' ></td>"
        MiaStringa = MiaStringa & "<td width='10%' >Euro</td>"
        MiaStringa = MiaStringa & "<td width='20%' >&nbsp;" & Format(Maggiorazione, "##,##0.00") & "</td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30%' >&nbsp;&nbsp; </td>"
        MiaStringa = MiaStringa & "<td width='25%' ></td>"
        MiaStringa = MiaStringa & "<td width='10%'></td>"
        MiaStringa = MiaStringa & "<td width='20%' ></td>"
        MiaStringa = MiaStringa & "</tr>"

        'MiaStringa = MiaStringa & "<tr>"
        'MiaStringa = MiaStringa & "<td width='21%' >Limite Reddito Convenzionale effettivo</td>"
        'MiaStringa = MiaStringa & "<td width='8%'></td>"
        'MiaStringa = MiaStringa & "<td width='2%' >Euro</td>"
        'MiaStringa = MiaStringa & "<td width='40%' >" & Format(Maggiorazione + REDDITO_ERP, "##,##0.00") & "</td>"
        'MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30" ' >&nbsp; </td>"
        MiaStringa = MiaStringa & "<td width='25%' ></td>"
        MiaStringa = MiaStringa & "<td width='10%'></td>"
        MiaStringa = MiaStringa & "<td width='20%' ></td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30%' >Componenti a Carico (" & MINORI + FIGLI & ")</td>"
        MiaStringa = MiaStringa & "<td width='25%' >Totale Detrazioni</td>"
        MiaStringa = MiaStringa & "<td width='10%' >Euro</td>"
        MiaStringa = MiaStringa & "<td width='20%' >&nbsp;" & Format((FIGLI + MINORI) * REDDITO_FIGLIO, "##,##0.00") & "</td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30%' >&nbsp; </td>"
        MiaStringa = MiaStringa & "<td width='25%' ></td>"
        MiaStringa = MiaStringa & "<td width='10%' ></td>"
        MiaStringa = MiaStringa & "<td width='20%' ></td>"
        MiaStringa = MiaStringa & "</tr>"

        'MiaStringa = MiaStringa & "<tr>"
        'MiaStringa = MiaStringa & "<td width='21%' >Reddito Convenzionale Calcolato</td>"
        'MiaStringa = MiaStringa & "<td width='8%' ></td>"
        'MiaStringa = MiaStringa & "<td width='2%' >Euro</td>"
        'MiaStringa = MiaStringa & "<td width='40%' >" & Format(REDDITO_CONV, "##,##0.00") & "</td>"
        'MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='30%'></td>"
        MiaStringa = MiaStringa & "<td width='25%'></td>"
        MiaStringa = MiaStringa & "<td width='10%'></td>"
        MiaStringa = MiaStringa & "<td width='20%'></td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "</table>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "</tr>"

        'MiaStringa = MiaStringa & "</center>"
        'MiaStringa = MiaStringa & "</BR>"

        ''MiaStringa = MiaStringa & "<table border='1' cellpadding='0' cellspacing='0' width='95%'>"
        ''MiaStringa = MiaStringa & "<tr>"
        ''MiaStringa = MiaStringa & "<td width='95%'>"

        ''If REDDITO_CONV <= Maggiorazione + REDDITO_ERP Then
        ''    MiaStringa = MiaStringa & "<p align='center'><font face='Arial' size='4'><b>&nbsp;REDDITO INFERIORE AL LIMITE</b></font></p>"
        ''Else
        ''    MiaStringa = MiaStringa & "<p align='center'><font face='Arial' size='4'><b>&nbsp;REDDITO SUPERIORE AL LIMITE</b></font></p>"
        ''End If

        ''MiaStringa = MiaStringa & "</td>"
        ''MiaStringa = MiaStringa & "</tr>"
        ''MiaStringa = MiaStringa & "</table>"

        'MiaStringa = MiaStringa & "</BR>"

        'MiaStringa = MiaStringa & "<center>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='95%'><b><font face='Arial' size='2'>REDDITO DEI SINGOLI COMPONENTI</font></b><table border='0' cellpadding='0' cellspacing='0' width='71%' style='font-family: Arial; font-size: 10 pt'>"


        For I = 1 To UBound(ComponentiNucleo)
            MiaStringa = MiaStringa & "<tr>"
            MiaStringa = MiaStringa & "<td width='55%'><font face='Arial' size='1'>" & ComponentiNucleo(I) & "</font></td>"
            MiaStringa = MiaStringa & "<td width='5%'><font face='Arial' size='1'>Euro</FONT></td>"
            MiaStringa = MiaStringa & "<td width='35%'><font face='Arial' size='1'>&nbsp;" & IfVuoto(RedditoNucleo(I)) & "</font></td>"
            MiaStringa = MiaStringa & "</tr>"

        Next I

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='55%'><font face='Arial' size='1'>TOTALE lordo nucleo</font></td>"
        MiaStringa = MiaStringa & "<td width='5%'><font face='Arial' size='1'>Euro</FONT></td>"
        MiaStringa = MiaStringa & "<td width='35%'><font face='Arial' size='1'>&nbsp;" & Format(TotaleLordo, "##,##0.00") & "</font></td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "<tr>"
        MiaStringa = MiaStringa & "<td width='55%' ><font face='Arial' size='3'><b>Reddito Convenzionale</b></font></td>"
        MiaStringa = MiaStringa & "<td width='5%' ><font face='Arial' size='1'>Euro</font></td>"
        MiaStringa = MiaStringa & "<td width='35%' ><font face='Arial' size='3'><b>&nbsp;" & Format(REDDITO_CONV, "##,##0.00") & "</b></font></td>"
        MiaStringa = MiaStringa & "</tr>"

        MiaStringa = MiaStringa & "</table>"
        MiaStringa = MiaStringa & "</td>"
        MiaStringa = MiaStringa & "</tr>"
        'MiaStringa = MiaStringa & "</CENTER>"
        MiaStringa = MiaStringa & "</table>"
        MiaStringa = MiaStringa & "</center>"

        CalcolaRedditoDatabase = MiaStringa


        'If lblSecondario.Visible = False Then
        '    MyExecuteSql("UPDATE PRATICHE SET REDDITO_CONV=" & VirgoleInPunti(REDDITO_CONV) & ",FL_REDDITO_PENSIONATO=" & TIPO3 & ",FL_REDDITO_AUTONOMO=" & TIPO1 & ",FL_REDDITO_DIPENDENTE=" & TIPO2 & " WHERE ID=" & Pratica_Id)
        '    MyDb.CommitTrans()
        '    MyDb.BeginTrans()
        'End If
        ''Else
        ''If lblSecondario.Visible = False Then
        '' MyExecuteSql("UPDATE PRATICHE SET REDDITO_CONV=" & VirgoleInPunti(REDDITO_CONV) & ",FL_REDDITO_PENSIONATO=" & TIPO3 & ",FL_REDDITO_AUTONOMO=" & TIPO1 & ",FL_REDDITO_DIPENDENTE=" & TIPO2 & " WHERE ID=" & Pratica_Id)
        '' End If
        ''End If
    End Function

    Function IfVuoto(ByVal s As Object) As String
        If s = "" Or s = " " Or s = "  " Then
            IfVuoto = "0,00"
        Else
            IfVuoto = s
        End If
    End Function
    Function PuntiInVirgole(ByVal N As Object) As String
        Dim S As String
        Dim pos As Integer
        If IsDBNull(N) Then
            PuntiInVirgole = "NULL"
        Else
            S = N
            pos = InStr(1, S, ".")

            If pos > 1 Then
                Mid(S, pos, 1) = ","
            End If
            PuntiInVirgole = S
        End If
    End Function

    Protected Sub cmbAnnoReddituale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnnoReddituale.SelectedIndexChanged
        Response.Write("<script>Uscita=1;</script>")

        Select Case cmbAnnoReddituale.SelectedItem.Value
            Case "2006"
                rdApplica.Visible = False
                rdNoApplica.Visible = False
                lblApplica36.Visible = False

                CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA 2006"
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = "2006"
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = "2006"
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = "2006"
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = "2006"
                'imgStampa.Attributes.Clear()
                imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;return confirm('ATTENZIONE..Stai elaborando utilizzando il 2006 come anno fiscale, SENZA TENERE CONTO di quanto previsto dalla LG 36/2008. Proseguire?');")
            Case "2007"
                rdApplica.Visible = True
                rdNoApplica.Visible = True
                lblApplica36.Visible = True
                rdApplica.Enabled = True
                rdNoApplica.Enabled = True
                imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;if (document.getElementById('txt36').value==0) {return confirm('ATTENZIONE..Stai elaborando utilizzando il 2007 come anno fiscale, SENZA TENERE CONTO di quanto previsto dalla LG 36/2008. Proseguire?');} else {return confirm('ATTENZIONE..Stai elaborando utilizzando il 2007 come anno fiscale, TENENDO CONTO di quanto previsto dalla LG 36/2008. Proseguire?');}")
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = "2007"
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = "2007"
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = "2007"
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = "2007"
                CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA 2007"
            Case Else
                rdApplica.Visible = True
                rdNoApplica.Visible = True
                lblApplica36.Visible = True
                rdNoApplica.Checked = False
                rdApplica.Checked = True
                rdApplica.Enabled = False
                rdNoApplica.Enabled = False
                imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;")
                CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
        End Select


    End Sub
End Class
