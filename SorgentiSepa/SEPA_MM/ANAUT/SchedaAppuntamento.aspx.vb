Imports System.Drawing

Partial Class ANAUT_SchedaAppuntamento
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            chiamante.Value = Request.QueryString("T")
            CaricaDati(par.DeCriptaMolto(Request.QueryString("ID")))
            PrendiUltima()
        End If
        VerificaOperatore()
        pagine.Value = pagine.Value + 1
    End Sub

    Private Sub VerificaOperatore()
        'mod_GESTIONE_CONTATTI
        Try
            If (LBL392.Visible = True Or LBL431.Visible = True) And (InStr(UCase(lblFiliale.Text), "TERRITORIALE A") > 0 Or InStr(UCase(lblFiliale.Text), "TERRITORIALE B") > 0 Or InStr(UCase(lblFiliale.Text), "TERRITORIALE C") > 0 Or InStr(UCase(lblFiliale.Text), "TERRITORIALE D") > 0) Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("LIVELLO_WEB"), "0") <> "1" Then
                            If par.IfNull(myReader("ID_CAF"), "0") = "63" Then
                                Label11.Visible = False
                                imgCreaAU.Visible = False
                                Label14.Visible = False
                                imgSposta.Visible = False
                                Label13.Visible = False
                                imgAnnulla.Visible = False
                                Label15.Visible = False
                                imgReimposta.Visible = False
                                imgCreaAU.Visible = False
                                Label11.Visible = False
                            End If
                        End If
                    End If
                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try

            End If

        Catch ex As Exception

        End Try
    End Sub

    Function CaricaDati(ByVal Indice As String)
        Try
            Dim caricoAUSI As String = ""

            IDA.Value = ""
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT CONVOCAZIONI_AU_STATI.RAPPRESENTAZIONE,CONVOCAZIONI_AU_STATI.ID AS IDSTATOC,CONVOCAZIONI_AU_STATI.DESCRIZIONE AS STATOC,RAPPORTI_UTENZA.COD_CONTRATTO,tab_filiali.nome as filiale,CONVOCAZIONI_AU.*,CONVOCAZIONI_AU_GRUPPI.ID_AU,UTENZA_SPORTELLI.DESCRIZIONE AS DSP,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC FROM UTENZA_SPORTELLI,SISCOM_MI.CONVOCAZIONI_AU_STATI,SISCOM_MI.CONVOCAZIONI_AU_GRUPPI, SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TAB_FILIALI,SISCOM_MI.CONVOCAZIONI_AU WHERE UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND CONVOCAZIONI_AU_STATI.ID=CONVOCAZIONI_AU.ID_STATO AND CONVOCAZIONI_AU_GRUPPI.ID=CONVOCAZIONI_AU.ID_GRUPPO AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE AND CONVOCAZIONI_AU.ID=" & Indice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblCodContratto.Text = par.IfNull(myReader("cod_contratto"), "")
                Session.Add("COLIGHT", lblCodContratto.Text)
                lblCognome.Text = par.IfNull(myReader("cognome"), "")
                lblNome.Text = par.IfNull(myReader("nome"), "")
                caricoAUSI = par.IfNull(myReader("carico_ausi"), "0")
                Label17.Text = Format(par.IfNull(myReader("id"), ""), "000000000")
                IDC.Value = par.IfNull(myReader("id"), "")
                lblAppuntamento.Text = par.FormattaData(par.IfNull(myReader("DATA_APP"), ""))
                GIO.Value = par.IfNull(myReader("DATA_APP"), "")
                lblOraApp.Text = par.IfNull(myReader("ORE_APP"), "")
                ORA.Value = Replace(par.IfNull(myReader("ORE_APP"), ""), ",", "")
                lblStato.Text = par.IfNull(myReader("statoC"), "--")
                lblStato.BackColor = Drawing.ColorTranslator.FromHtml(par.IfNull(myReader("RAPPRESENTAZIONE"), "#33CC33"))
                STATOC.Value = par.IfNull(myReader("IDstatoC"), "0")
                If STATOC.Value = "1" Then
                    lblStato.ForeColor = Color.Black ' Drawing.ColorTranslator.FromHtml(par.IfNull(myReader("RAPPRESENTAZIONE"), "#33CC33"))
                End If
                txtIdContratto.Value = par.IfNull(myReader("ID_CONTRATTO"), "0")
                lblSportello.Text = par.IfNull(myReader("DSP"), "0")
                SPORTELLO.Value = par.IfNull(myReader("ID_SPORTELLO"), "")
                lblFiliale.Text = par.IfNull(myReader("filiale"), "0")
                FILIALE.Value = par.IfNull(myReader("ID_FILIALE"), "0")
                GRUPPO.Value = par.IfNull(myReader("ID_GRUPPO"), "0")

                If Session.Item("ANAGRAFE_UTENZA_LIGHT") = "1" Then
                    Label16.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('VisualizzaContratto.aspx','RU','height=780,top=0,left=0,width=1160');" & Chr(34) & ">Visualizza Contratto</a>"
                Else
                    Label16.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">Visualizza Contratto</a>"
                End If

                par.cmd.CommandText = "SELECT AGENDA_APPUNTAMENTI.* FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_CONVOCAZIONE=" & Indice
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    IDA.Value = par.IfNull(myReader1("ID"), "")
                Else
                    IDA.Value = ""
                End If
                myReader1.Close()


                Select Case STATOC.Value
                    Case "2"
                        imgCreaAU.Visible = True
                        Label11.Visible = False

                        imgReimposta.Visible = True
                        Label15.Visible = True

                        imgSposta.Visible = False
                        Label14.Visible = False

                        imgAnnulla.Visible = False
                        Label13.Visible = False

                        'Case "1"
                        '    Label13.Visible = True
                        '    Label13.Text = "RIPRISTINA APPUNTAMENTO"
                        '    imgAnnulla.Visible = Visible

                        '    imgSposta.Visible = False

                        '    imgCreaAU.Visible = False
                        '    Label11.Visible = False

                        '    Label14.Visible = False

                        '    Label15.Visible = True
                        '    imgReimposta.Visible = True

                        '    imgAnnulla.ToolTip = "Ripristina Appuntamento"

                        '    If par.IfNull(myReader("ID_MOTIVO_ANNULLO"), "0") = "1" Then
                        '        imgCreaAU.Visible = True
                        '        Label11.Visible = True
                        '    End If

                        '    If par.IfNull(myReader("carico_ausi"), "0") = "1" And par.IfNull(myReader("ID_MOTIVO_ANNULLO"), "0") = "2" And Request.QueryString("X") = "0" Then
                        '        Label13.Visible = False
                        '        Label13.Text = "RIPRISTINA APPUNTAMENTO"
                        '        imgAnnulla.Visible = False

                        '        imgReimposta.Visible = False
                        '        Label15.Visible = False

                        '        Response.Write("<script>alert('Attenzione...Convocazione presa in carico da AUCM, non è possibile effettuare operazioni!');</script>")

                        '    End If
                    Case "1"
                        imgCreaAU.Visible = True
                        Label11.Visible = True

                        imgReimposta.Visible = True
                        Label15.Visible = True

                        imgSposta.Visible = True
                        Label14.Visible = True

                        imgAnnulla.Visible = True
                        Label13.Visible = True
                        Label13.Text = "RIPRISTINA APPUNTAMENTO"
                        imgAnnulla.ToolTip = "Ripristina Appuntamento"
                    Case "0"
                        imgCreaAU.Visible = True
                        Label11.Visible = True

                        imgReimposta.Visible = True
                        Label15.Visible = True

                        imgSposta.Visible = True
                        Label14.Visible = True

                        imgAnnulla.Visible = True
                        Label13.Visible = True
                        Label13.Text = "ANNULLA APPUNTAMENTO"
                        imgAnnulla.ToolTip = "Annulla Appuntamento"
                End Select
                presa.Value = "0"
                If par.IfNull(myReader("ID_MOTIVO_ANNULLO"), "0") = "2" And par.IfNull(myReader("carico_ausi"), "0") = "0" And Session.Item("MOD_AU_CONV_SINDACATI") = "1" And Request.QueryString("X") = "1" Then
                    Label13.Visible = True
                    Label13.Text = "PRESA IN CARICO AUCM"
                    imgAnnulla.Visible = True
                    imgAnnulla.ImageUrl = "~/NuoveImm/page-down-icon.png"
                    imgSposta.Visible = False
                    imgCreaAU.Visible = False
                    Label11.Visible = False
                    Label14.Visible = False
                    Label15.Visible = False
                    imgReimposta.Visible = False
                    presa.Value = "1"
                    imgAnnulla.ToolTip = "Presa in carico AUCM"
                End If
                If par.IfNull(myReader("ID_MOTIVO_ANNULLO"), -1) <> -1 Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_MOTIVO_ANNULLO_APP WHERE ID=" & par.IfNull(myReader("ID_MOTIVO_ANNULLO"), "0")
                    Dim myReaderANN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderANN.Read Then
                        lblSospesa.Visible = True
                        lblSospesa.Text = "Convocazione sospesa per"
                        lblSospensione.Visible = True
                        lblSospensione.Text = par.IfNull(myReaderANN("DESCRIZIONE"), "")
                    End If
                    myReaderANN.Close()
                End If

                DicAUSI.Value = "0"
                If par.IfNull(myReader("carico_ausi"), "0") = "1" And par.IfNull(myReader("ID_MOTIVO_ANNULLO"), "0") = "2" Then
                    lblStato.Text = lblStato.Text & " - PRESA DA AUCM"
                    DicAUSI.Value = "1"
                End If

                par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,T_STATI_DICHIARAZIONE.DESCRIZIONE AS STATO FROM T_STATI_DICHIARAZIONE,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID_STATO=T_STATI_DICHIARAZIONE.COD (+) and ID_BANDO=" & myReader("ID_AU") & " AND RAPPORTO='" & par.IfNull(myReader("cod_contratto"), "") & "'"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.HasRows = True Then
                    If myReader1.Read Then
                        imgCreaAU.Visible = True
                        Label11.Visible = True
                        Label11.Text = "VIS. SCHEDA AU"
                        Label11.ToolTip = "Visualizza scheda AU"
                        imgCreaAU.ToolTip = "Visualizza scheda AU"
                        iddich.Value = myReader1("ID")
                        lblConvocazione.Text = myReader1("pg") & " - " & myReader1("stato")

                        If par.IfNull(myReader1("fl_sospensione"), "0") = "1" Then
                            lblSospensione.Visible = True
                            lblSospesa.Visible = True
                            If par.IfNull(myReader1("fl_sosp_1"), "0") = "1" Then
                                lblSospensione.Text = "Titolare Deceduto;" & vbCrLf
                            End If

                            If par.IfNull(myReader1("fl_sosp_2"), "0") = "1" Then
                                lblSospensione.Text = lblSospensione.Text & "Titolare Separato;" & vbCrLf
                            End If

                            If par.IfNull(myReader1("fl_sosp_3"), "0") = "1" Then
                                lblSospensione.Text = lblSospensione.Text & "Titolare Trasferito;" & vbCrLf
                            End If

                            If par.IfNull(myReader1("fl_sosp_4"), "0") = "1" Then
                                lblSospensione.Text = lblSospensione.Text & "Ricongiungimento;" & vbCrLf
                            End If

                            If par.IfNull(myReader1("fl_sosp_5"), "0") = "1" Then
                                lblSospensione.Text = lblSospensione.Text & "Ampliamento;" & vbCrLf
                            End If

                            If par.IfNull(myReader1("fl_sosp_6"), "0") = "1" Then
                                lblSospensione.Text = lblSospensione.Text & "Diminuzione;" & vbCrLf
                            End If

                            If par.IfNull(myReader1("fl_sosp_7"), "0") = "1" Then
                                lblSospensione.Text = lblSospensione.Text & "Documentazione Macante;" & vbCrLf
                            End If

                        End If

                        If par.IfNull(myReader1("fl_da_verificare"), "0") = "1" Then
                            lblSospensione.Visible = True
                            lblSospesa.Visible = False
                            lblSospensione.Text = "DA VERIFICARE"
                        End If
                    End If
                Else
                    iddich.Value = ""
                End If
                myReader1.Close()

                If lblNome.Text = "" Then
                    lblNome.Visible = False
                    Label2.Visible = False
                    Label6.Text = "Nominativo"
                End If

                If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "EQC392" Then
                    LBL392.Visible = True
                    S392.Value = "1"

                    If Mid(UCase(lblCodContratto.Text), 1, 6) = "000000" Or Mid(UCase(lblCodContratto.Text), 1, 2) = "41" Or Mid(UCase(lblCodContratto.Text), 1, 2) = "42" Or Mid(UCase(lblCodContratto.Text), 1, 2) = "43" Then
                        LBL392.Text = LBL392.Text & " - V"
                    End If
                    par.cmd.CommandText = "Select rapporti_utenza.id " _
                                        & "FROM " _
                                        & "siscom_mi.rapporti_utenza," _
                                        & "SISCOM_MI.BOL_BOLLETTE " _
                                        & "WHERE " _
                                        & "BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                        & "AND BOL_BOLLETTE.RIFERIMENTO_DA>='20120101' " _
                                        & "And (ID_BOLLETTA_RIC Is Not NULL Or ID_RATEIZZAZIONE Is Not NULL) " _
                                        & "AND RAPPORTI_UTENZA.COD_CONTRATTO='" & UCase(lblCodContratto.Text) & "'"
                    Dim myReaderRATMOR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRATMOR.HasRows = True Then
                        LBL392.Text = LBL392.Text & " - RATMOR"
                    End If
                    myReaderRATMOR.Close()
                Else
                    LBL392.Visible = False
                    S392.Value = "0"
                End If

                If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" Then
                    LBL431.Visible = True
                    L431.Value = "1"

                    If Mid(UCase(lblCodContratto.Text), 1, 6) = "000000" Or Mid(UCase(lblCodContratto.Text), 1, 2) = "41" Or Mid(UCase(lblCodContratto.Text), 1, 2) = "42" Or Mid(UCase(lblCodContratto.Text), 1, 2) = "43" Then
                        LBL431.Text = LBL431.Text & " - V"
                    End If
                    par.cmd.CommandText = "Select rapporti_utenza.id " _
                                        & "FROM " _
                                        & "siscom_mi.rapporti_utenza," _
                                        & "SISCOM_MI.BOL_BOLLETTE " _
                                        & "WHERE " _
                                        & "BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                        & "AND BOL_BOLLETTE.RIFERIMENTO_DA>='20120101' " _
                                        & "And (ID_BOLLETTA_RIC Is Not NULL Or ID_RATEIZZAZIONE Is Not NULL) " _
                                        & "AND RAPPORTI_UTENZA.COD_CONTRATTO='" & UCase(lblCodContratto.Text) & "'"
                    Dim myReaderRATMOR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderRATMOR.HasRows = True Then
                        LBL431.Text = LBL431.Text & " - RATMOR"
                    End If
                    myReaderRATMOR.Close()
                Else
                    LBL431.Visible = False
                    L431.Value = "0"
                End If
            End If
            myReader.Close()



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYmmdd'),'DD/MM/YYYY')||'-'||SUBSTR(DATA_ORA,9,2)||'.'||SUBSTR(DATA_ORA,11,2) AS DATA_APP,DESCRIZIONE AS NOTE,OPERATORI.OPERATORE FROM OPERATORI,SISCOM_MI.CONVOCAZIONI_AU_EVENTI WHERE OPERATORI.ID=CONVOCAZIONI_AU_EVENTI.ID_OPERATORE AND ID_CONVOCAZIONE=" & Indice & " ORDER BY DATA_ORA DESC", par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "AGENDA_APPUNTAMENTI_EVENTI,AGENDA_APPUNTAMENTI_EVENTI")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If Session.Item("MOD_AU_CONV_REI") = "0" Then
                Label15.Visible = False
                imgReimposta.Visible = False
            End If

            If Session.Item("MOD_AU_CONV_RIP") = "0" Then
                Label13.Visible = False
                imgAnnulla.Visible = False
            End If

            If Session.Item("MOD_AU_CONV_ANN") = "0" Then
                Label13.Visible = False
                imgAnnulla.Visible = False
            Else
                If STATOC.Value <> "2" And Request.QueryString("X") = "0" And caricoAUSI = "0" Then
                    Label13.Visible = True
                    imgAnnulla.Visible = True
                End If
            End If

            If Session.Item("MOD_AU_CONV_SPOSTA") = "0" Then
                Label14.Visible = False
                imgSposta.Visible = False
            End If

            If Session.Item("MOD_AU_CONV_N") = "0" Then
                Label11.Visible = False
                imgCreaAU.Visible = False
            End If

            If Request.QueryString("X") = "1" And caricoAUSI = "1" Then
                Label11.Visible = True
                imgCreaAU.Visible = True
                Label14.Visible = False
                imgSposta.Visible = False
                Label13.Visible = False
                imgAnnulla.Visible = False
                Label15.Visible = False
                imgReimposta.Visible = False
            End If



        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:SchedaAppuntamento - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If chiamante.Value <> "1" Then
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        Else
            Response.Write("<script>self.close();</script>")
        End If
    End Sub

    Protected Sub imgCreaAU_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCreaAU.Click
            CaricaDati(par.DeCriptaMolto(Request.QueryString("ID")))
    End Sub

    Protected Sub imgAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAnnulla.Click
            CaricaDati(par.DeCriptaMolto(Request.QueryString("ID")))
    End Sub

    Protected Sub imgSposta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSposta.Click
            CaricaDati(par.DeCriptaMolto(Request.QueryString("ID")))
    End Sub

    Protected Sub imgReimposta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgReimposta.Click
            CaricaDati(par.DeCriptaMolto(Request.QueryString("ID")))
    End Sub

    Protected Sub S392_ValueChanged(sender As Object, e As System.EventArgs) Handles S392.ValueChanged

    End Sub

    Private Sub PrendiUltima()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim id_dichia As Long = 0
            Dim id_dichia1 As Long = 0
            Dim data_Fine As String = ""
            Dim data_Fine1 As String = ""

            Dim MESSAGGIO As String = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/VisualizzazioneCompNucleo.aspx?ID=" & txtIdContratto.Value & "&T=4','Componenti','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">La situazione anagrafica e reddituale più recente corrisponde a quella contrattuale.<br/>Premendo questo link saranno visualizzati i componenti</a>"
            Dim MESSAGGIO1 As String = ""
            Dim MESSAGGIO2 As String = ""


            par.cmd.CommandText = "SELECT rapporti_utenza.id as idRU,DOMANDE_BANDO_VSA.*,T_MOTIVO_DOMANDA_VSA.*,DICHIARAZIONI_VSA.*,RAPPORTI_UTENZA.* FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=" & txtIdContratto.Value & " AND DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM=RAPPORTI_UTENZA.COD_CONTRATTO ORDER BY DICHIARAZIONI_VSA.DATA_FINE_VAL DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("fl_autorizzazione"), 0) = 1 Then
                    id_dichia = myReader1("ID_DICHIARAZIONE")
                    data_Fine = par.IfNull(myReader1("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                    MESSAGGIO = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/VisualizzazioneCompNucleo.aspx?ID=" & id_dichia & "&IDRU=" & par.IfNull(myReader1("idRU"), 0) & "&T=1','Componenti','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">La situazione anagrafica e reddituale più recente corrisponde alla domanda di <b>""" & myReader1("DESCRIZIONE") & """</b> presentata in data " & par.FormattaData(myReader1("DATA_presentazione")) & ".<br/>Premendo questo link saranno visualizzati i componenti</a>"
                Else
                    id_dichia = myReader1("ID_DICHIARAZIONE")
                    MESSAGGIO2 = "Attenzione, esistono DATI ANAGRAFICI da verificare relativi alla domanda di <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & id_dichia & "&CH=2&LE=1','DichVSA','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">" & myReader1("DESCRIZIONE") & "</a>" & " del " & par.FormattaData(par.IfNull(myReader1("DATA_presentazione"), "")) & ".<br />Si consiglia di porre maggiore accuratezza nella compilazione della scheda."
                End If
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE,rapporti_utenza.id as idRU FROM siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE rapporti_utenza.id=" & txtIdContratto.Value & " and NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
            & "AND RAPPORTO=rapporti_utenza.cod_contratto ORDER BY UTENZA_DICHIARAZIONI.DATA_FINE_VAL DESC"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                id_dichia1 = myReader2("ID")
                data_Fine1 = par.IfNull(myReader2("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                MESSAGGIO1 = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/VisualizzazioneCompNucleo.aspx?ID=" & id_dichia1 & "&IDRU=" & par.IfNull(myReader2("idRU"), 0) & "&T=2','Componenti','height=400,top=200,left=410,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">La situazione anagrafica e reddituale più recente corrisponde a: <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/>Premendo questo link saranno visualizzati i componenti</a>"
            End If
            myReader2.Close()

            If data_Fine = "" And data_Fine1 = "" Then
                lblDatiUltimoNucleo.Text = MESSAGGIO
                '    Intestatari(idcont.Value, t.Value)
                '    tipo.Value = 0
            Else
                If data_Fine >= data_Fine1 Then
                    lblDatiUltimoNucleo.Text = MESSAGGIO
                    '        iddich.Value = id_dichia
                    '        Intestatari1(id_dichia)
                    '        tipo.Value = 1
                Else
                    '        iddich.Value = id_dichia1
                    lblDatiUltimoNucleo.Text = MESSAGGIO1
                    '        Intestatari2(id_dichia1)
                    '        lblMsgVSA.Text = MESSAGGIO2
                    '        tipo.Value = 2
                End If
            End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
