
Partial Class CONS_domanda
    Inherits PageSetIdMode
    Dim lValoreCorrente As Long
    Dim sAnnoIsee As String
    Dim sAnnoCanone As String

    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False And bEseguito = False Then
            txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            bMemorizzato = False
            lIdDomanda = Request.QueryString("ID")
            lIdDichiarazione = Request.QueryString("ID1")
            lProgr = Request.QueryString("PROGR")

            Fl_Indici = Request.QueryString("APP")
            If Fl_Indici = "0" Then
                txtI.Value = "0"
            Else
                txtI.Value = "1"
            End If
            txtTab.Text = "1"
            lNuovaDomanda = 0
            imgAttendi.Visible = True
            VisualizzaDomanda()
            'If Request.QueryString("LE") = 1 Then
            '    Label5.Visible = False
            '    txtL.Text = "1"
            'Else
            '    txtL.Text = "0"
            'End If
            'AggiornaLog()

            imgAttendi.Visible = False
        End If
        bEseguito = True
    End Sub

    'Private Function AggiornaLog()
    '    Try
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)

    '        par.cmd.CommandText = "INSERT INTO CONSULTAZIONI_WEB (ID,ID_VISIONATO,TIPO_VISIONATO,DATA_VISIONATO,ORA_VISIONATO,ID_OPERATORE) VALUES (SEQ_CONSULTAZIONI_WEB.NEXTVAL," & lIdDomanda & ",'DO','" & Format(Now, "yyyyMMdd") & "','" & Format(Now, "HHmm") & "'," & Session.Item("ID_OPERATORE") & ")"
    '        par.cmd.ExecuteNonQuery()

    '    Catch ex As Exception

    '        par.OracleConn.Close()
    '        par.OracleConn.Dispose()
    '    End Try
    'End Function

    Private Function VisualizzaDomanda()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long
        Dim LB1 As Label
        Dim VALORE_R As Double

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            Dom_Richiedente1.DisattivaTutto()
            Dom_Abitative_1_1.DisattivaTutto()
            Dom_Abitative_2_1.DisattivaTutto()
            Dom_Dichiara1.DisattivaTutto()
            Dom_Familiari1.DisattivaTutto()
            Dom_Requisiti1.DisattivaTutto()



            par.cmd.CommandText = "SELECT DOMANDE_BANDO.N_DISTINTA,DICHIARAZIONI.ID_CAF,CAF_WEB.COD_CAF FROM DOMANDE_BANDO,DICHIARAZIONI,CAF_WEB WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DICHIARAZIONI.ID_CAF=CAF_WEB.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read() Then

                LBLENTE.Visible = True
                LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT POSIZIONE FROM BANDI_GRADUATORIA_DEF WHERE ID_DOMANDA=" & lIdDomanda
            myReader = par.cmd.ExecuteReader()

            If myReader.Read() Then

                LBLENTE.Visible = True
                LBLENTE.Text = LBLENTE.Text & " - Pos Grad.: " & par.IfNull(myReader("posizione"), "")

            End If
            myReader.Close()


            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbProvRec", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbTipoIRec", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbResidenza", "SELECT DESCRIZIONE,ID FROM T_RESIDENZA_LOMBARDIA ORDER BY ID ASC", "DESCRIZIONE", "ID")

            par.RiempiDList(Dom_Dichiara1, par.OracleConn, "cmbPresentaD", "SELECT DESCRIZIONE,COD FROM T_CAUSALI_DOMANDA ORDER BY COD ASC", "DESCRIZIONE", "COD")

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                lblStato.Text = myReader("ID_STATO")
                lIndice_Bando = myReader("ID_BANDO")
                lIdDichiarazione = myReader("ID_DICHIARAZIONE")
                lProgr = myReader("PROGR_COMPONENTE")

                lblISBAR.Text = par.Tronca(par.IfNull(myReader("isbarc_r"), 0))
                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                lblPG.ToolTip = lIdDomanda

                cT = Dom_Richiedente1.FindControl("txtPresso")
                cT.Text = par.IfNull(myReader("PRESSO_REC_DNTE"), "")

                cT = Dom_Richiedente1.FindControl("txtIndirizzoRec")
                cT.Text = par.IfNull(myReader("IND_REC_DNTE"), "")

                cT = Dom_Richiedente1.FindControl("txtCivicoRec")
                cT.Text = par.IfNull(myReader("CIVICO_REC_DNTE"), "")

                cT = Dom_Richiedente1.FindControl("txtTelRec")
                cT.Text = par.IfNull(myReader("TELEFONO_REC_DNTE"), "")

                cT = Dom_Richiedente1.FindControl("txtCAPRec")
                cT.Text = par.IfNull(myReader("CAP_REC_DNTE"), "")

                CT1 = CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("PERIODO_RES"), 0)).Selected = True


                CT1 = CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_CAUSALE_DOMANDA"), 0)).Selected = True

                If par.IfNull(myReader("FL_FAM_1"), "0") = "0" Then
                    CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = False
                Else
                    CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_FAM_2"), "0") = "0" Then
                    CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked = False
                Else
                    CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_FAM_3"), "0") = "0" Then
                    CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked = False
                Else
                    CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_FAM_4"), "0") = "0" Then
                    CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked = False
                Else
                    CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_PROFUGO"), "0") = "0" Then
                    CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked = False
                Else
                    CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked = True
                End If


                If par.IfNull(myReader("FL_MOROSITA_G"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_AFF_ONEROSO"), "0") = "0" Then
                    CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked = True
                End If

                cT = Dom_Abitative_2_1.FindControl("txtAnnoCanone")
                cT.Text = par.IfNull(myReader("ANNO_RIF_CANONE"), "")

                cT = Dom_Abitative_2_1.FindControl("txtSpese")
                cT.Text = par.IfNull(myReader("IMPORTO_CANONE"), "0")

                cT = Dom_Abitative_2_1.FindControl("txtAnnoAcc")
                cT.Text = par.IfNull(myReader("ANNO_RIF_SPESE_ACC"), "")

                cT = Dom_Abitative_2_1.FindControl("txtSpeseAcc")
                cT.Text = par.IfNull(myReader("IMPORTO_SPESE_ACC"), "0")

                cT = Note1.FindControl("txtNote")
                cT.Text = par.IfNull(myReader("NOTE_WEB"), "")

                If par.IfNull(myReader("REQUISITO1"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO1"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO2"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO3"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO4"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO5"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO7"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO8"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO9"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_MOROSITA"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked = True
                End If


                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=0 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=1 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=2 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=3 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=4 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=5 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=6 order by id asc", "DESCRIZIONE", "ID")


                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=7 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=8 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=9 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=10 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=11 order by id asc", "DESCRIZIONE", "ID")

                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=12 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=13 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA8", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=14 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA9", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=15 order by id asc", "DESCRIZIONE", "ID")

                CT1 = Dom_Familiari1.FindControl("cmbF1")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_0"), 0)).Selected = True

                CT1 = Dom_Familiari1.FindControl("cmbF2")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_1"), 0)).Selected = True

                CT1 = Dom_Familiari1.FindControl("cmbF3")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_2"), 0)).Selected = True

                CT1 = Dom_Familiari1.FindControl("cmbF4")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_3"), 0)).Selected = True

                CT1 = Dom_Familiari1.FindControl("cmbF5")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_4"), 0)).Selected = True

                CT1 = Dom_Familiari1.FindControl("cmbF6")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_5"), 0)).Selected = True

                CT1 = Dom_Familiari1.FindControl("cmbF7")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_6"), 0)).Selected = True

                CT1 = Dom_Abitative_1_1.FindControl("cmbA1")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_7"), 0)).Selected = True

                CT1 = Dom_Abitative_1_1.FindControl("cmbA2")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_8"), 0)).Selected = True

                CT1 = Dom_Abitative_1_1.FindControl("cmbA3")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_9"), 0)).Selected = True

                CT1 = Dom_Abitative_1_1.FindControl("cmbA4")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_10"), 0)).Selected = True

                CT1 = Dom_Abitative_1_1.FindControl("cmbA5")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_11"), 0)).Selected = True

                CT1 = Dom_Abitative_2_1.FindControl("cmbA6")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_12"), 0)).Selected = True

                CT1 = Dom_Abitative_2_1.FindControl("cmbA7")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_13"), 0)).Selected = True

                CT1 = Dom_Abitative_2_1.FindControl("cmbA8")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_14"), 0)).Selected = True

                CT1 = Dom_Abitative_2_1.FindControl("cmbA9")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_15"), 0)).Selected = True

                Dim item As ListItem

                par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(myReader("ID_LUOGO_REC_DNTE"), 0)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbProvRec", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")

                    CT1 = Dom_Richiedente1.FindControl("cmbProvRec")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader1("SIGLA"), 0)).Selected = True
                    item = CT1.SelectedItem

                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRec", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

                    CT1 = Dom_Richiedente1.FindControl("cmbComuneRec")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(par.IfNull(myReader1("NOME"), "")).Selected = True
                    item = CT1.SelectedItem

                End If
                myReader1.Close()

                CT1 = Dom_Richiedente1.FindControl("cmbTipoIRec")
                CT1.Items.FindByValue(par.IfNull(myReader("ID_TIPO_IND_REC_DNTE"), "")).Selected = True

                Select Case par.IfNull(myReader("PERIODO_RES"), 0)
                    Case 0
                        VALORE_R = 0
                    Case 1
                        VALORE_R = 0
                    Case 2
                        VALORE_R = 40
                    Case 3
                        VALORE_R = 85
                    Case 4
                        VALORE_R = 0
                End Select
                VALORE_R = (VALORE_R / 100) * 0.3

                txtIndici.Text = "V1=" & (par.IfNull(myReader("isbar"), 0)) _
                           & "&V2=" & (par.IfNull(myReader("isbarc"), 0)) _
                           & "&V3=" & (par.IfNull(myReader("isbarc_r"), 0)) _
                           & "&V4=" & Format(par.IfNull(myReader("disagio_a"), 0), "##,##0.000000000000000") _
                           & "&V5=" & Format(par.IfNull(myReader("disagio_e"), 0), "##,##0.000000000000000") _
                           & "&V6=" & Format(par.IfNull(myReader("disagio_f"), 0), "##,##0.000000000000000") _
                           & "&V7=" & par.Converti(par.IfNull(myReader("reddito_isee"), 0)) _
                           & "&V8=" & par.Converti(par.IfNull(myReader("ISR_ERP"), 0)) _
                           & "&V9=" & par.Converti(par.IfNull(myReader("ISP_ERP"), 0)) _
                           & "&V10=" & par.Converti(par.IfNull(myReader("ISe_ERP"), 0)) _
                           & "&V11=" & Format(VALORE_R, "##,##0.000000000000000") _
                           & "&V12=" & par.Converti(par.IfNull(myReader("PSE"), 0)) _
                           & "&V13=" & par.Converti(par.IfNull(myReader("VSE"), 0)) _
                           & "&PG=" & lblPG.Text & "&UJ=3"

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT TIPO_BANDO,DATA_INIZIO FROM BANDI WHERE ID=" & lIndice_Bando
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                Select Case lBando
                    Case 0
                        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                    Case 1
                        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                    Case 2
                        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                End Select
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROGR=" & lProgr
            myReader = par.cmd.ExecuteReader()
            Dim sPresso As String
            sPresso = ""

            If myReader.Read() Then
                cT = Dom_Richiedente1.FindControl("txtCognome")
                cT.Text = par.IfNull(myReader("COGNOME"), "")

                cT = Dom_Richiedente1.FindControl("txtNome")
                cT.Text = par.IfNull(myReader("NOME"), "")

                cT = Dom_Richiedente1.FindControl("txtDataNascita")
                cT.Text = Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 1, 4)

                cT = Dom_Richiedente1.FindControl("txtCF")
                cT.Text = par.IfNull(myReader("COD_FISCALE"), "")

                lblCF.Text = par.IfNull(myReader("COD_FISCALE"), "")

                CT1 = Dom_Richiedente1.FindControl("cmbSesso")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(par.IfNull(myReader("SESSO"), "M")).Selected = True

                lIndice_Componente = myReader("ID")

                If lProgr <> 0 Then
                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(cT.Text, 12, 4) & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                        If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True
                            LB1 = Dom_Richiedente1.FindControl("Label6")
                            LB1.Visible = False
                            LB1 = Dom_Richiedente1.FindControl("Label7")
                            LB1.Visible = False
                            CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                            CT1.Visible = False
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                            CT1.Visible = False
                        Else
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        End If
                    End If
                    myReader1.Close()
                End If

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lblPGDic.Text = lblSPG.Text & "-" & myReader("PG") & "-F205"
                iNumComponenti = myReader("N_COMP_NUCLEO") - 1
                LBLaNNOsIT.Text = "ANNO SIT. ECON. " & par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                If lProgr = 0 Then
                    cT = Dom_Richiedente1.FindControl("txtIndRes")
                    cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                    cT = Dom_Richiedente1.FindControl("txtCivicoRes")
                    cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                    cT = Dom_Richiedente1.FindControl("txtTelRes")
                    cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                    cT = Dom_Richiedente1.FindControl("txtCAPRes")
                    cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                    lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                    lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                    lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")

                Else
                    par.cmd.CommandText = "SELECT * FROM COMP_NAS_RES WHERE ID_COMPONENTE=" & lIndice_Componente
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then


                        lIndiceAppoggio_1 = myReader2("ID_LUOGO_RES_DNTE")
                        lIndiceAppoggio_2 = myReader2("ID_TIPO_IND_RES_DNTE")

                        CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text = par.IfNull(myReader2("IND_RES_DNTE"), "")
                        CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text = par.IfNull(myReader2("CIVICO_RES_DNTE"), "")

                        CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCAPRes")
                        cT.Text = par.IfNull(myReader2("CAP_RES"), "")

                        CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader2("ID_TIPO_IND_RES_DNTE"), 6)).Selected = True

                    End If
                    myReader2.Close()
                End If
            End If
            myReader.Close()
            If lProgr = 0 Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                    CT1.SelectedIndex = -1
                    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader("NOME")).Selected = True
                        LB1 = Dom_Richiedente1.FindControl("Label6")
                        LB1.Visible = False
                        LB1 = Dom_Richiedente1.FindControl("Label7")
                        LB1.Visible = False
                        CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                        CT1.Visible = False
                        CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                        CT1.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                        par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("NOME")).Selected = True
                    End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2

                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
                End If
                myReader.Close()
            End If

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = Dom_Richiedente1.FindControl("cmbNazioneRes")
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True
                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = Dom_Richiedente1.FindControl("cmbPrRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                    CT1 = Dom_Richiedente1.FindControl("cmbComuneRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT F_RESIDENZA FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & lIndice_Componente
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Fl_Residenza = myReader("F_RESIDENZA")
            End If
            myReader.Close()
            If Fl_Residenza = "1" Then
                CType(Dom_Abitative_2_1.FindControl("alert2"), Image).Visible = True
                CType(Dom_Abitative_2_1.FindControl("alert3"), Image).Visible = True
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_NASCITA"",PERC_INVAL,INDENNITA_ACC FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY PROGR ASC", par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            da.Dispose()
            ds.Dispose()

            Dom_Richiedente1.DisattivaRichiedente()
            If lProgr = 0 Then
                Dom_Richiedente1.DisattivaIndirizzo()
            End If


            If Request.QueryString("LE") = 1 Then
                'Label5.Visible = False
                txtL.Text = "1"
            Else
                txtL.Text = "0"
                par.cmd.CommandText = "INSERT INTO CONSULTAZIONI_WEB (ID,ID_VISIONATO,TIPO_VISIONATO,DATA_VISIONATO,ORA_VISIONATO,ID_OPERATORE) VALUES (SEQ_CONSULTAZIONI_WEB.NEXTVAL," & lIdDomanda & ",'DO','" & Format(Now, "yyyyMMdd") & "','" & Format(Now, "HHmm") & "'," & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()
            End If

            lblNominativo.Text = par.VaroleDaPassare(par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text) & " " & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtNome"), TextBox).Text))
            par.cmd.Dispose()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans
            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Session.Add("LAVORAZIONE", "1")

        Catch ex As Exception
            Label10.Text = ex.ToString
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Function

    Public Property Fl_Indici() As String
        Get
            If Not (ViewState("par_Fl_Indici") Is Nothing) Then
                Return CStr(ViewState("par_Fl_Indici"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Fl_Indici") = value
        End Set

    End Property

    Public Property Fl_Residenza() As String
        Get
            If Not (ViewState("par_Residenza") Is Nothing) Then
                Return CStr(ViewState("par_Residenza"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Residenza") = value
        End Set

    End Property


    Public Property DeviSalvare() As Boolean
        Get
            If Not (ViewState("par_DeviSalvare") Is Nothing) Then
                Return CLng(ViewState("par_DeviSalvare"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_DeviSalvare") = value
        End Set

    End Property


    Public Property bMemorizzato() As Boolean
        Get
            If Not (ViewState("par_bMemorizzato") Is Nothing) Then
                Return CLng(ViewState("par_bMemorizzato"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_bMemorizzato") = value
        End Set

    End Property


    Public Property lBando() As Long
        Get
            If Not (ViewState("par_lBando") Is Nothing) Then
                Return CLng(ViewState("par_lBando"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lBando") = value
        End Set

    End Property


    Public Property iNumComponenti() As Integer
        Get
            If Not (ViewState("par_componenti") Is Nothing) Then
                Return CInt(ViewState("par_componenti"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_componenti") = value
        End Set

    End Property


    Public Property lProgr() As Integer
        Get
            If Not (ViewState("par_lProgr") Is Nothing) Then
                Return CInt(ViewState("par_lProgr"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_lProgr") = value
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

    Public Property lNuovaDomanda() As Long
        Get
            If Not (ViewState("par_lNuovaDomanda") Is Nothing) Then
                Return CLng(ViewState("par_lNuovaDomanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lNuovaDomanda") = value
        End Set

    End Property


    Public Property lIndice_Componente() As Long
        Get
            If Not (ViewState("par_lIndice_Componente") Is Nothing) Then
                Return CLng(ViewState("par_lIndice_Componente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIndice_Componente") = value
        End Set

    End Property

    Public Property iTab() As Integer
        Get
            If Not (ViewState("par_itab") Is Nothing) Then
                Return CInt(ViewState("par_itab"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_itab") = value
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

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                End If
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘‘par.cmd.Transaction = par.myTrans
                'par.myTrans.Rollback()

                'par.OracleConn.Close()
                'par.OracleConn.Dispose()
                'HttpContext.Current.Session.Remove("TRANSAZIONE")
                'HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                If txtL.Text = "0" Then
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                Else
                    Response.Write("<script>window.close();</script>")
                End If
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                If txtL.Text = "0" Then
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
End Class
