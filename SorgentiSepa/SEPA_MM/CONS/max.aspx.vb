
Partial Class CONS_max
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
            Response.Expires = 0
            LBLENTE.Visible = False
            txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            bMemorizzato = False
            lIdDichiarazione = Request.QueryString("ID")
            txtTab.Text = "1"

            lNuovaDichiarazione = 0

            VisualizzaDichiarazione()

            bEseguito = True

        End If
    End Sub

    Private Function AggiornaLog()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "INSERT INTO CONSULTAZIONI_WEB (ID,ID_VISIONATO,TIPO_VISIONATO,DATA_VISIONATO,ORA_VISIONATO,ID_OPERATORE) VALUES (SEQ_CONSULTAZIONI_WEB.NEXTVAL," & lIdDichiarazione & ",'DI','" & Format(Now, "yyyyMMdd") & "','" & Format(Now, "HHmm") & "'," & Session.Item("ID_OPERATORE") & ")"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception

            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Function

    Function VisualizzaDichiarazione()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim CTT As Label
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long

        Dim RESIDENZA As String
        Dim SOMMA As Long
        Dim DESCRIZIONE As String
        Dim i As Integer
        Dim MIOPROGR As Integer




        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            Dic_Dichiarazione1.DisattivaTutto()
            Dic_Integrazione1.DisattivaTutto()
            Dic_Note1.DisattivaTutto()
            Dic_Nucleo1.DisattivaTutto()
            Dic_Patrimonio1.DisattivaTutto()
            Dic_Reddito1.DisattivaTutto()
            cmbStato.Enabled = False

            par.cmd.CommandText = "SELECT BANDI.TIPO_BANDO,BANDI.DATA_INIZIO,BANDI.STATO FROM BANDI,DICHIARAZIONI WHERE DICHIARAZIONI.ID=" & lIdDichiarazione & " AND DICHIARAZIONI.ID_BANDO=BANDI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
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

            par.cmd.CommandText = "SELECT DICHIARAZIONI.ID_CAF,DOMANDE_BANDO.N_DISTINTA,CAF_WEB.COD_CAF FROM DOMANDE_BANDO,DICHIARAZIONI,CAF_WEB WHERE DICHIARAZIONI.ID_CAF=CAF_WEB.ID AND DICHIARAZIONI.ID=" & lIdDichiarazione & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                
                'LBLENTE.Visible = True
                LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")


            End If
            myReader.Close()

            txtDataPG.Text = Format(Now, "dd/MM/yyyy")

            Dim lsiFrutto As New ListItem("DA COMPLETARE", "0")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("COMPLETA", "1")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("DA CANCELLARE", "2")
            cmbStato.Items.Add(lsiFrutto)

            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Dic_Patrimonio1, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")


            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then
                lIndice_Bando = myReader("ID_BANDO")

                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))

                cmbStato.SelectedValue = par.IfNull(myReader("id_stato"), 0)

                CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")

                CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))
                CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))

                CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedIndex = -1
                CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Items.FindByValue(par.IfNull(myReader("ID_TIPO_CAT_AB"), "0")).Selected = True

                If par.IfNull(myReader("ID_TIPO_CAT_AB"), "-1") <> "-1" Then
                    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True
                End If

                cT = Dic_Dichiarazione1.FindControl("txtCAPRes")
                cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                cT = Dic_Dichiarazione1.FindControl("txtIndRes")
                cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                cT = Dic_Dichiarazione1.FindControl("txtCivicoRes")
                cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                cT = Dic_Dichiarazione1.FindControl("txtTelRes")
                cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")

                If par.IfNull(myReader("FL_GIA_TITOLARE"), "0") = "0" Then
                    CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = False
                Else
                    CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = True
                End If


                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneNas")
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                        CT1.Visible = False
                        CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                        CT1.Visible = False
                        CTT = Dic_Dichiarazione1.FindControl("label6")
                        CTT.Visible = False
                        CTT = Dic_Dichiarazione1.FindControl("label7")
                        CTT.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    End If
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Dichiarazione1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader1("DESCRIZIONE")).Selected = True
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Dichiarazione1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Dichiarazione1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True

                        'cT = Dic_Dichiarazione1.FindControl("txtCAPRes")
                        'cT.Text = myReader1("CAP")
                    End If
                End If
                myReader1.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDomAssociata.Text = par.IfNull(myReader("PG"), "")
                lIdDomanda = par.IfNull(myReader("id"), -1)

            End If
            myReader.Close()
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
                CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
                CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader("COD_FISCALE"), "")
                CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

                CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
                CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
                CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NAS"), ""))

                cT = Dic_Sottoscrittore1.FindControl("txtIndRes")
                cT.Text = par.IfNull(myReader("IND"), "")

                cT = Dic_Sottoscrittore1.FindControl("txtCivicoRes")
                cT.Text = par.IfNull(myReader("CIVICO"), "")

                cT = Dic_Sottoscrittore1.FindControl("txtTelRes")
                cT.Text = par.IfNull(myReader("TELEFONO"), "")

                cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                cT.Text = par.IfNull(myReader("CAP_RES"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                    CT1.Visible = False
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                    CT1.Visible = False
                    CTT = Dic_Sottoscrittore1.FindControl("label6")
                    CTT.Visible = False
                    CTT = Dic_Sottoscrittore1.FindControl("label7")
                    CTT.Visible = False
                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = Dic_Sottoscrittore1.FindControl("cmbTipoIRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                    CT1.Visible = False
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                    CT1.Visible = False
                    CTT = Dic_Sottoscrittore1.FindControl("label10")
                    CTT.Visible = False
                    CTT = Dic_Sottoscrittore1.FindControl("label11")
                    CTT.Visible = False

                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    'cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                    'cT.Text = myReader("CAP")
                End If
            End If
            myReader.Close()

            Dim MIAS As String
            Dim INDENNITA As String
            Dim INVALIDITA As String = "NO"

            Dim TIPO As String = ""
            Dim SOTTOTIPO As String = ""

            Dim DOCIMPORTO As String = ""
            Dim DOCMUTUO As String = ""

            Dim TIPOREDDITO As String = "--"
            Dim TIPOAGRARI As String = "--"


            par.cmd.CommandText = "SELECT COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO,T_TIPO_PARENTELA where COMP_NUCLEO.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
                    INDENNITA = "SI"
                Else
                    INDENNITA = "NO"
                End If
                INVALIDITA = "NO"
                If Val(par.IfNull(myReader("PERC_INVAL"), "0")) > 0 Then
                    INVALIDITA = "SI"
                End If


                MIAS = ""
                MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INVALIDITA, 4) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader("DATA_CERTIFICATO"), "")), 10)
                CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
                If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                    CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
                End If
                CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader("PROGR") + 1

                SOMMA = 0
                DESCRIZIONE = ""
                par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
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
                            CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 100)

                        End If
                    Next
                End If



                par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    TIPO = ""
                    SOTTOTIPO = ""

                    Select Case par.IfNull(myReader2("TIPO"), 0)
                        Case 0
                            TIPO = "SALDO AL 31/12"
                        Case 1
                            TIPO = "TITOLI AL 31/12-ASS. VITA"
                        Case 2
                            TIPO = "PARTECIPAZIONI AL 31/12"
                    End Select

                    Select Case par.IfNull(myReader2("SOTTOTIPO"), 0)
                        Case 0
                            SOTTOTIPO = "AUTOCERTIFICATO"
                        Case 1
                            SOTTOTIPO = "SALDO CONTO"
                        Case 2
                            SOTTOTIPO = "SALDO TITOLI"
                        Case 3
                            SOTTOTIPO = "SALDO PREMI VERSATI"
                        Case 4
                            SOTTOTIPO = "DOCUMENTAZIONE CCIAA"
                        Case 5
                            SOTTOTIPO = "730"
                        Case 6
                            SOTTOTIPO = "UNICO"
                        Case 7
                            SOTTOTIPO = "BILANCIO"
                    End Select

                    CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(TIPO, 27) & " " & par.MiaFormat(SOTTOTIPO, 25) & " " & par.MiaFormat(myReader2("IMPORTO"), 10) & ",00" & " " & par.MiaFormat(par.IfNull(myReader2("IBAN"), ""), 27) & " " & par.MiaFormat(par.IfNull(myReader2("INTERMEDIARIO"), " "), 50), myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT COMP_PATR_IMMOB.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB WHERE COMP_PATR_IMMOB.ID_COMPONENTE=" & myReader("ID") & " and COMP_PATR_IMMOB.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB.ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    RESIDENZA = " "
                    If myReader2("F_RESIDENZA") = "0" Then
                        RESIDENZA = "NO"
                    Else
                        RESIDENZA = "SI"
                    End If

                    Select Case myReader2("TIPO_DOC_VALORE")
                        Case -1
                            DOCIMPORTO = "--"
                        Case 0
                            DOCIMPORTO = "AUTOCERTIFICATO"
                        Case 1
                            DOCIMPORTO = "730"
                        Case 2
                            DOCIMPORTO = "UNICO"
                        Case 3
                            DOCIMPORTO = "ATTO DI ACQUISIZIONE"
                        Case 4
                            DOCIMPORTO = "DICHIARAZIONE ICI"
                        Case 5
                            DOCIMPORTO = "VISURA CATASTO"
                    End Select

                    Select Case myReader2("TIPO_DOC_MUTUO")
                        Case -1
                            DOCMUTUO = "--"
                        Case 0
                            DOCMUTUO = "AUTOCERTIFICATO"
                        Case 1
                            DOCMUTUO = "DOC. ISTITUTO DI CREDITO"

                    End Select


                    CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(DOCIMPORTO, 25) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(DOCMUTUO, 25) & " " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    Select Case par.IfNull(myReader2("TIPO_REDDITO"), "-1")
                        Case "-1"
                            TIPOREDDITO = "--"
                        Case "0"
                            TIPOREDDITO = "CUD"
                        Case "1"
                            TIPOREDDITO = "730"
                        Case "2"
                            TIPOREDDITO = "UNICO"
                        Case "3"
                            TIPOREDDITO = "AUTOCERTIFICATO"
                    End Select


                    Select Case par.IfNull(myReader2("TIPO_AGRARI"), "-1")
                        Case "-1"
                            TIPOAGRARI = "--"
                        Case "0"
                            TIPOAGRARI = "AUTOCERTIFICATO"
                        Case "1"
                            TIPOAGRARI = "UNICO"
                    End Select

                    CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 35) & " " & par.MiaFormat(TIPOREDDITO, 25) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 8) & ",00" & " " & par.MiaFormat(TIPOAGRARI, 25) & " " & par.MiaFormat(myReader2("PROV_AGRARI"), 8) & ",00", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    TIPO = "--"
                    Select Case par.IfNull(myReader2("TIPO_REDDITO"), 0)
                        Case 1
                            TIPO = "AUTOCERTIFICATO"
                        Case 2
                            TIPO = "EROGAZIONE FSA"
                        Case 3
                            TIPO = "EROGAZIONE SUSSIDI"
                        Case 4
                            TIPO = "PROVVIDENZE INV. CIVILI"
                        Case 5
                            TIPO = "PENS./ASS. SOCIALE"
                        Case 6
                            TIPO = "ALTRO"
                    End Select

                    CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 45) & " " & par.MiaFormat(TIPO, 24) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

                Dim TIPO_RILEVAZIONE As String = ""

                par.cmd.CommandText = "SELECT COMP_DETRAZIONI.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI WHERE COMP_DETRAZIONI.ID_COMPONENTE=" & myReader("id") & " and COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by comp_detrazioni.id_componente asc"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    Select Case myReader2("TIPO_RILEVAZIONE")
                        Case "-1"
                            TIPO_RILEVAZIONE = "--"
                        Case "0"
                            TIPO_RILEVAZIONE = "CUD"
                        Case "1"
                            TIPO_RILEVAZIONE = "730"
                        Case "2"
                            TIPO_RILEVAZIONE = "UNICO"
                        Case "3"
                            TIPO_RILEVAZIONE = "AUTOCERTIFICATO"
                    End Select
                    CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 30) & " " & par.MiaFormat(TIPO_RILEVAZIONE, 15) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(myReader2("importo"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

            End While
            myReader.Close()

            'txtbinserito.Text = "1"
            'par.cmd.CommandText = "SELECT COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO,T_TIPO_PARENTELA where COMP_NUCLEO.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
            'myReader = par.cmd.ExecuteReader()
            'While myReader.Read
            '    If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
            '        INDENNITA = "SI"
            '    Else
            '        INDENNITA = "NO"
            '    End If
            '    MIAS = ""
            '    MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
            '    CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
            '    If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
            '        CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
            '    End If
            '    CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader("PROGR") + 1

            '    SOMMA = 0
            '    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
            '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        SOMMA = SOMMA + Val(myReader2("IMPORTO"))
            '        DESCRIZIONE = par.IfNull(myReader2("DESCRIZIONE"), "")
            '        MIOPROGR = myReader("PROGR")
            '    End While
            '    myReader2.Close()
            '    If SOMMA <> 0 Then
            '        For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
            '            If MIOPROGR = CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Value Then
            '                CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 17)
            '                'CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(myReader2("IMPORTO"), 6) & ",00   " & par.MiaFormat(myReader2("DESCRIZIONE"), 17), myReader("PROGR")))
            '            End If
            '        Next
            '    End If


            '    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
            '    myReader2 = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(myReader2("COD_INTERMEDIARIO"), 13) & " " & par.MiaFormat(myReader2("INTERMEDIARIO"), 30) & " " & par.MiaFormat(myReader2("IMPORTO"), 8) & ",00 ", myReader("PROGR")))
            '    End While
            '    myReader2.Close()

            '    par.cmd.CommandText = "SELECT COMP_PATR_IMMOB.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB WHERE COMP_PATR_IMMOB.ID_COMPONENTE=" & myReader("ID") & " and COMP_PATR_IMMOB.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB.ID_COMPONENTE ASC"
            '    myReader2 = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        RESIDENZA = " "
            '        If myReader2("F_RESIDENZA") = "0" Then
            '            RESIDENZA = "NO"
            '        Else
            '            RESIDENZA = "SI"
            '        End If
            '        CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader("PROGR")))
            '    End While
            '    myReader2.Close()

            '    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
            '    myReader2 = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 35) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 15) & ",00 " & par.MiaFormat(myReader2("PROV_AGRARI"), 15) & ",00", myReader("PROGR")))
            '    End While
            '    myReader2.Close()

            '    par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
            '    myReader2 = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 50) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader("PROGR")))
            '    End While
            '    myReader2.Close()

            '    par.cmd.CommandText = "SELECT COMP_DETRAZIONI.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI WHERE COMP_DETRAZIONI.ID_COMPONENTE=" & myReader("id") & " and COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by comp_detrazioni.id_componente asc"
            '    myReader2 = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 30) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(myReader2("importo"), 8) & ",00 ", myReader("PROGR")))
            '    End While
            '    myReader2.Close()

            'End While
            'myReader.Close()





            Session.Add("LAVORAZIONE", "1")

            If Request.QueryString("LE") = 1 Then
                'Label5.Visible = False
                txtL.Text = "1"
            Else
                txtL.Text = "0"
                par.cmd.CommandText = "INSERT INTO CONSULTAZIONI_WEB (ID,ID_VISIONATO,TIPO_VISIONATO,DATA_VISIONATO,ORA_VISIONATO,ID_OPERATORE) VALUES (SEQ_CONSULTAZIONI_WEB.NEXTVAL," & lIdDichiarazione & ",'DI','" & Format(Now, "yyyyMMdd") & "','" & Format(Now, "HHmm") & "'," & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()

        Catch ex As Exception

            Label4.Text = ex.Message
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function


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

    Public Property lIdDomanda() As Long
        Get
            If Not (ViewState("par_lIdDomanda1") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomanda1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomanda1") = value
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

                'If par.OracleConn.State = Data.ConnectionState.Open Then
                '    par.OracleConn.Close()
                'End If
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘‘par.cmd.Transaction = par.myTrans
                'par.myTrans.Rollback()
                ''par.myTrans.Dispose()
                'par.OracleConn.Close()
                'par.OracleConn.Dispose()
                'HttpContext.Current.Session.Remove("TRANSAZIONE")
                'HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                'Session.Remove("STAMPATO")
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
