'SCHEDA SAL (PAGAMENTI + ELENCO MANUTENZIONI)
Imports System.Math
Imports System.Collections
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports Telerik.Web.UI


Partial Class MANUTENZIONI_SAL
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""

    Public TabberHide As String = ""

    Public TabRiepilogo As String = ""
    Public TabSitProgressiva As String

    Public sValoreEsercizioFinanziarioR As String
    Public sValoreStruttura As String

    Public sValoreServizio As String

    Public sValoreFornitore As String
    Public sValoreAppalto As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String
    Public sValoreAdp As String
    Public sValoreStato As String

    Public sOrdinamento As String
    Public sValoreProvenienza As String

    Public importo, penale, importoT, penaleT, oneriT, astaT, ivaT, ritenutaT, ritenutaNoIvaT, rimborsoT, risultato1T, risultato2T, risultato3T, risultato3Tparziale, risultato4T, risultatoImponibileT, ivaLiquidazioneT As Decimal
    Public importoP, penaleP, oneriP, astaP, ivaP, ritenutaP, rimborsoP, risultato1P, risultato2P, risultato3P, risultato4P, risultatoImponibileP As Decimal
    Public solaLetturaImportoMinoreZero As Boolean = False

    Public lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
        If par.IfNull(Session.Item("BP_MS_RIELABORA_CDP"), "0") = "0" Then
            btnRielaboraPagamento.Visible = False
            btnRielbSal.Visible = False
        Else
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                btnRielaboraPagamento.Visible = False
            Else
                btnRielaboraPagamento.Visible = True
            End If

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                btnRielbSal.Visible = False
            Else
                btnRielbSal.Visible = True
            End If
        End If


        If Not IsPostBack Then

            Try
                HFGriglia.Value = CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).ClientID
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("SAL")
                'Panel1.Visible = False
                HFTAB.Value = "tab1,tab2,tab3"
                HFAltezzaTab.Value = 400
                HFAltezzaFGriglie.Value = "480"
                txtDataDel.SelectedDate = Format(Now, "dd/MM/yyyy")
                'PARAMENTRI x LA RICERCA

                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreStruttura = Request.QueryString("STR")

                sValoreFornitore = Request.QueryString("FO")
                sValoreAppalto = Request.QueryString("AP")
                sValoreServizio = Request.QueryString("SV")

                sValoreData_Dal = Request.QueryString("DAL")
                sValoreData_Al = Request.QueryString("AL")

                sValoreStato = Request.QueryString("ST")

                sOrdinamento = Request.QueryString("ORD")
                sValoreProvenienza = Request.QueryString("PROVENIENZA")


                '***************************************************


                'SETTAGGIO VARIABILI
                Me.HLink_Fornitore.Text = ""
                Me.HLink_Appalto.Text = ""


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)


                If par.OracleConn.State = Data.ConnectionState.Open Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "alert('Impossibile visualizzare');", True)
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                Setta_StatoPagamento()

                'Dim idEsercizioFinanziario As String = sValoreEsercizioFinanziarioR
                If IsNumeric(sValoreEsercizioFinanziarioR) AndAlso sValoreEsercizioFinanziarioR > 0 Then
                    par.cmd.CommandText = "select substr(inizio,1,4) as anno from siscom_mi.t_Esercizio_finanziario " _
                        & " where t_esercizio_finanziario.id=(select id_Esercizio_finanziario from siscom_mi.pf_main where id=" & sValoreEsercizioFinanziarioR & ")"
                    lblEsercizioFinanziario.Text = "Anno B.P. " & par.cmd.ExecuteScalar
                End If


                vIdPagamento = 0

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    vIdPagamento = Request.QueryString("ID")
                Else
                    vIdPagamento = Session.Item("ID")
                End If
                If IsNothing(sValoreAppalto) Then
                    par.cmd.CommandText = "SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID=" & vIdPagamento
                    sValoreAppalto = par.IfNull(par.cmd.ExecuteScalar, DBNull.Value)
                End If
                caricaImportoResiduoDaTrattenere()




                If vIdPagamento <> 0 Then
                    'VISUALIZZAZIONE MANUTENZIONE

                    'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)
                    VisualizzaDati()

                    TabberHide = "tabbertabhide"

                    Tabber1 = "tabbertabdefault"
                    txtindietro.Text = 0
                Else

                    'Nell'inserimento, prendo la struttura dell'Operatore
                    Me.txtID_STRUTTURA.Value = Session.Item("ID_STRUTTURA")

                    SettaValRicerca()


                    TabberHide = "tabbertabhide"

                    Tabber1 = "tabbertabdefault"
                    txtindietro.Text = 1
                End If
                If par.IfNull(Session.Item("BP_MS_RIELABORA_CDP"), "0") = "0" Then
                    btnRielaboraPagamento.Visible = False
                End If
                If par.IfNull(Session.Item("BP_MS_RIELABORA_SAL"), "0") = "0" Then
                    btnRielbSal.Visible = False
                End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                '*** FORM RIEPILOGO
                For Each CTRL In Me.Tab_SAL_Riepilogo.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                ''*** FORM DETTAGLI
                For Each CTRL In Me.Tab_SAL_Dettagli.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                '*** FORM RIEPILOGO PROGRESSIVO
                For Each CTRL In Me.Tab_SAL_RiepilogoProg.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next


                Me.txtDataSAL.Attributes.Add("onkeypress", "javascript:CompletaDataTelerik(event,this);")
                Me.txtDataDel.Attributes.Add("onkeypress", "javascript:CompletaDataTelerik(event,this);")

                Me.HLink_ElencoMandati.Attributes.Add("onClick", "javascript:window.open('../PAGAMENTI/Tab_ElencoMandati.aspx?ID_PAGAMENTO=" & vIdPagamento & "','Elenco','height=600,width=800');")

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")
                '  AbilitaDisabilita()

                'Or Session.Item("BP_GENERALE") = "1"
                If Session.Item("BP_MS_L") = "1" Or IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                End If


                ' SE l'operatore è BP_GENERALE=1 (può vedere tutte le strutture) perrò la sua struttura + diversa da quella selezionata allora la maschera è in SOLO LETTURA
                If Session.Item("BP_GENERALE") = "1" And par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) <> Session.Item("ID_STRUTTURA") Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                End If

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                    Me.btnINDIETRO.Visible = False
                    Me.btnStampaSAL.Visible = False
                    Me.btnStampa.Visible = False
                    btnRielbSal.Visible = False
                    btnRielaboraPagamento.Visible = False

                    lstListaRapporti.Clear()

                    'CHIUSURA DB
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                    End If

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                    Session.Item("LAVORAZIONE") = "0"

                End If

                If bloccato.Value <> "1" Then
                    'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)

                        par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                            & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                            & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO" ' AND PF_VOCI.FL_CC= 0
                        Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If Lett.Read Then
                            'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                            If par.IfNull(Lett(0), 0) >= 6 Then
                                btnAnnulla.Visible = False
                                If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                                    btnAnnulla.Visible = True
                                End If
                            End If
                        End If
                        Lett.Close()

                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Else

                        par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                           & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                           & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO " ''AND PF_VOCI.FL_CC=0
                        Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If Lett.Read Then
                            'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                            If par.IfNull(Lett(0), 0) >= 6 Then
                                btnAnnulla.Visible = False
                                If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                                    btnAnnulla.Visible = True
                                End If
                            End If
                        End If
                        Lett.Close()

                    End If

                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)

                        par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                            & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                            & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO"
                        Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If Lett.Read Then
                            If par.IfNull(Lett(0), 0) = 5 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                                btnAnnulla.Visible = True
                            End If
                        End If
                        Lett.Close()

                        'Verifica che il contratto legato al pagamento abbia o meno la spunta sull'anticipo contrattuale
                        par.cmd.CommandText = "select APPALTI.FL_ANTICIPO  from siscom_mi.appalti where id = " & sValoreAppalto
                        Dim anticipo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If anticipo = 0 Then
                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False

                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False

                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                        Else
                            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA")

                            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento
                            Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                            If Not String.IsNullOrEmpty(nome) Then
                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
                            Else
                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = True
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = True
                            End If


                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False

                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                        End If

                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Else

                        par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                           & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                           & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO"
                        Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If Lett.Read Then
                            If par.IfNull(Lett(0), 0) = 5 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                                btnAnnulla.Visible = True
                            End If
                        End If
                        Lett.Close()

                        'Verifica che il contratto legato al pagamento abbia o meno la spunta sull'anticipo contrattuale
                        par.cmd.CommandText = "select APPALTI.FL_ANTICIPO  from siscom_mi.appalti where id = " & sValoreAppalto
                        Dim anticipo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If anticipo = 0 Then
                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False

                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False

                            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                        Else

                            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA")
                            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento
                            Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                            If Not String.IsNullOrEmpty(nome) Then
                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False

                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = False

                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = False
                            Else
                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = True
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = True

                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = True
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Enabled = True

                                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = True
                                CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Enabled = True
                            End If
                        End If

                    End If
                End If



            Catch ex As Exception

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

            End Try
        End If

        txtid.Value = vIdPagamento

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
        End Select
        'VERIFICA ABILITAZIONE PER LA RIELABORAZIONE DEL CDP E DEL SAL


        idSAL.Value = vIdPagamento
        ' AbilitaDisabilita()
        If vIdPagamento = 0 Then
            Me.btnAnnulla.Visible = False
            Me.btnStampaSAL.Visible = False
            Me.btnStampa.Visible = False
            btnRielbSal.Visible = False
            btnRielaboraPagamento.Visible = False
        End If
        If par.IfNull(Session.Item("BP_MS_RIELABORA_CDP"), "0") = "0" Then
            btnRielaboraPagamento.Visible = False
            btnRielbSal.Visible = False
        End If
        Dim i As Integer = 0
        Dim esci As Integer = 0
        For Each item As RadTab In RadTabStrip.Tabs
            If item.Visible = True Then
                If esci = HiddenTabSelezionato.Value Then
                    Exit For
                End If
                esci += 1
            End If
            i += 1
        Next
        RadMultiPage1.SelectedIndex = i
        RadTabStrip.SelectedIndex = i
        If annullaSal = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Blocca_SbloccaMenu(1);", True)
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("MANU")) Then
            If Request.QueryString("MANU") = "1" Then
                btnAnnulla.Enabled = False
            End If
        End If
        If Not IsNothing(Request.QueryString("NASCONDIINDIETRO")) And Not String.IsNullOrEmpty(Request.QueryString("NASCONDIINDIETRO")) Then
            If Request.QueryString("NASCONDIINDIETRO") = "1" Then
                btnINDIETRO.Visible = False
            End If
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)

    End Sub

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property annullaSal() As String
        Get
            If Not (ViewState("par_annullaSal") Is Nothing) Then
                Return CStr(ViewState("par_annullaSal"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_annullaSal") = value
        End Set

    End Property

    Public Property vIdPagamento() As Long
        Get
            If Not (ViewState("par_idPagamento") Is Nothing) Then
                Return CLng(ViewState("par_idPagamento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPagamento") = value
        End Set

    End Property


    Private Sub Setta_StatoPagamento()


        Me.cmbStato.Items.Clear()

        Me.cmbStato.Items.Add(New RadComboBoxItem("NON FIRMATO", 0))
        Me.cmbStato.Items.Add(New RadComboBoxItem("FIRMATO CON RISERVA", 1))
        Me.cmbStato.Items.Add(New RadComboBoxItem("FIRMATO", 2))

        Me.cmbStato.SelectedValue = 0



    End Sub


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim sDescManutenzioni As String = ""

        Try

            'GRIGLIA
            BindGridManutenzioni("ID_PAGAMENTO=" & vIdPagamento, par.IfNull(myReader1("ID_APPALTO"), "-1"), par.IfNull(myReader1("PROGR_APPALTO"), " "))








            Me.lblProgAnnoPagamento.Text = par.IfNull(myReader1("PROGR_APPALTO"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
            If Not String.IsNullOrEmpty(par.FormattaData(par.IfNull(myReader1("DATA_SAL"), ""))) Then
                Me.txtDataSAL.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_SAL"), ""))
            End If
            If Not String.IsNullOrEmpty(par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), ""))) Then
                Me.txtDataDel.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), ""))
                txtDataDel.Enabled = False

            End If

            'Me.DataDelCaricata.Value = par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), ""))

            txtDescAttPagamento.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

            Me.txtPAGAMENTI_PROGR_APPALTO.Value = par.IfNull(myReader1("PROGR_APPALTO"), "")
            Me.txtPAGAMENTI_PROGR.Value = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")

            Me.cmbStato.SelectedValue = par.IfNull(myReader1("STATO_FIRMA"), "0")
            Me.txtSTATO.Value = Me.cmbStato.SelectedValue

            'Me.txtDataMandato.Text = par.FormattaData(par.IfNull(myReader1("DATA_MANDATO"), ""))
            'Me.txtNumMandato.Text = par.IfNull(myReader1("NUMERO_MANDATO"), "")

            'Me.cmbContoCorrente.SelectedValue = par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01")


            ''data emissione min 10/04/2012
            'par.cmd.CommandText = "SELECT MAX(DATA_CONSUNTIVAZIONE) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=" & vIdPagamento
            'Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If Lettore.Read Then
            '    DataEmissioneMin = par.IfNull(Lettore(0), "")
            'End If
            'Lettore.Close()


            '*****************************

            'FORNITORE
            sValoreFornitore = Request.QueryString("FO")

            par.cmd.CommandText = "select  ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(TIPO) as TIPO,trim(COD_FORNITORE) as COD_FORNITORE " _
                               & " from SISCOM_MI.FORNITORI where ID=" & par.IfNull(myReader1("ID_FORNITORE"), "-1")
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            myReader2 = par.cmd.ExecuteReader()

            If myReader2.Read Then
                If par.IfNull(myReader2("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader2("COD_FORNITORE"), "") = "" Then
                        Me.HLink_Fornitore.Text = par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
                    Else
                        Me.HLink_Fornitore.Text = par.IfNull(myReader2("COD_FORNITORE"), "") & " - " & par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader2("NOME"), "")
                    End If
                Else
                    If par.IfNull(myReader2("COD_FORNITORE"), "") = "" Then
                        Me.HLink_Fornitore.Text = par.IfNull(myReader2("RAGIONE_SOCIALE"), "")
                    Else
                        Me.HLink_Fornitore.Text = par.IfNull(myReader2("COD_FORNITORE"), "") & " - " & par.IfNull(myReader2("RAGIONE_SOCIALE"), "")
                    End If
                End If

                If par.IfNull(myReader2("TIPO"), "") = "F" Then
                    Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader2("ID"), "") & "','Fornitore','height=700,width=1300');")
                Else
                    Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader2("ID"), "") & "','Fornitore','height=700,width=1300');")
                End If

            End If
            myReader2.Close()


            'IBAN **************************************************
            par.cmd.CommandText = "select IBAN from SISCOM_MI.FORNITORI_IBAN " _
                               & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"

            myReader2 = par.cmd.ExecuteReader
            While myReader2.Read
                If Strings.Len(Strings.Trim(Me.txtIBAN.Text)) = 0 Then
                    Me.txtIBAN.Text = par.IfNull(myReader2("IBAN"), "")
                Else
                    Me.txtIBAN.Text = Me.txtIBAN.Text & vbCrLf & par.IfNull(myReader2("IBAN"), "")
                End If
            End While
            myReader2.Close()
            '******************************************************

            'CONTRATTO
            par.cmd.CommandText = "select  * from SISCOM_MI.APPALTI where ID=" & par.IfNull(myReader1("ID_APPALTO"), "-1")
            myReader2 = par.cmd.ExecuteReader()

            If myReader2.Read Then
                Me.HLink_Appalto.Text = par.IfNull(myReader2("NUM_REPERTORIO"), "")
                Session.Add("IDA", par.IfNull(myReader1("ID_APPALTO"), ""))
                Me.HLink_Appalto.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Appalti.aspx?ID=" & par.IfNull(myReader1("ID_APPALTO"), "") & "&IDL=-1','Appalto','height=700,width=1300');")
                'Me.HLink_Appalto.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Appalti.aspx?ID=" & par.IfNull(myReader2("ID"), "") & "&A=" & par.IfNull(myReader2("ID"), "") & "&IDL=-1','Appalto','height=550,width=800');")

                Me.lblDataAppalto.Text = par.FormattaData(par.IfNull(myReader2("DATA_REPERTORIO"), ""))
                Me.txtDescAppalto.Text = par.IfNull(myReader2("DESCRIZIONE"), "")
            End If
            myReader2.Close()



            '*****************************************************

            'SERVIZIO E DETTAGLIO SERVIZIO
            'par.cmd.CommandText = "select   PF_VOCI_IMPORTO.ID as ""ID_PF_VOCI_IMPORTO"", PF_VOCI_IMPORTO.DESCRIZIONE as ""DETT_VOCE"", " _
            '                            & " TAB_SERVIZI.ID as ""ID_TAB_SERVIZI"", TAB_SERVIZI.DESCRIZIONE as ""SERVIZIO""" _
            '                 & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI " _
            '                 & " where PF_VOCI_IMPORTO.ID in (select ID_VOCE_PF_IMPORTO from SISCOM_MI.PRENOTAZIONI " _
            '                                              & " where ID_PAGAMENTO=" & vIdPagamento & ")" _
            '                 & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=TAB_SERVIZI.ID (+) "


            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    Me.txtServizio.Text = par.IfNull(myReader2("SERVIZIO"), "")
            '    Me.txtServizioVoce.Text = par.IfNull(myReader2("DETT_VOCE"), "")

            'End If
            'myReader2.Close()



            'IMPORTI SAL
            importoT = 0
            penaleT = 0
            oneriT = 0
            astaT = 0
            ivaT = 0
            ritenutaT = 0
            rimborsoT = 0
            risultato1T = 0
            risultato2T = 0
            risultato3T = 0
            risultato4T = 0

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"",MANUTENZIONI.ID_STRUTTURA " _
                                      & ",(select anticipo_contrattuale from siscom_mi.prenotazioni where prenotazioni.id=manutenzioni.id_prenotazione_pagamento) as anticipo_contrattuale," _
                                      & "(select PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID= manutenzioni.id_appalto )) as perc_iva" _
                               & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID_PAGAMENTO=" & vIdPagamento _
                               & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "

            myReader2 = par.cmd.ExecuteReader()


            Dim perciva As Decimal = 0
            Dim anticipo As Decimal = 0

            While myReader2.Read
                Me.txtID_STRUTTURA.Value = par.IfNull(myReader2("ID_STRUTTURA"), "-1")

                '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                'If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If

                    anticipo += par.IfNull(myReader2("ANTICIPO_CONTRATTUALE"), 0)

                    CalcolaImporti2(par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0), par.IfNull(myReader2("IVA_CONSUMO"), 0), par.IfNull(myReader2("RIMBORSI"), 0), par.IfNull(myReader2("PENALE2"), 0), par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0))
                    If sDescManutenzioni = "" Then
                        sDescManutenzioni = par.IfNull(myReader2("DESCRIZIONE"), "")
                    Else
                        sDescManutenzioni = sDescManutenzioni & vbCrLf & par.IfNull(myReader2("DESCRIZIONE"), "")
                    End If

                    perciva = par.IfNull(myReader2("PERC_IVA"), 0)
                'Else
                '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                '    myReader2.Close()
                '    Exit Sub
                'End If
            End While
            myReader2.Close()

            Me.txtDescManutenzioni.Text = sDescManutenzioni


            CType(Tab_SAL_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(importoT, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriT, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1T, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaT, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2T, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaT, "", "##,##0.00") '6 campo

            '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
            CType(Tab_SAL_Riepilogo.FindControl("LabelRitenuta"), Label).Visible = False
            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta"), TextBox).Visible = False
            CType(Tab_SAL_Riepilogo.FindControl("LabelRitenutaEuro"), Label).Visible = False



            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3T, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaT, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(rimborsoT, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4T, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(anticipo, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(anticipo, "", "##,##0.00")


            Dim importo_iva As Decimal = IsNumFormat((anticipo * perciva) / 100, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + anticipo, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + anticipo, "", "##,##0.00")

            'IMPORTO PAGATO
            Dim SommaPagato As Decimal = 0
            par.cmd.CommandText = " select  sum(nvl(PAGAMENTI_LIQUIDATI.IMPORTO,0) ) as IMPORTO " _
                                           & " from  SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                           & " where ID_PAGAMENTO=" & vIdPagamento

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                SommaPagato = par.IfNull(myReader2(0), 0)
            End If
            myReader2.Close()

            cmb_Liquidazione.ClearSelection()
            If SommaPagato = 0 Then
                Me.cmb_Liquidazione.SelectedValue = 0
            ElseIf SommaPagato = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) Then
                Me.cmb_Liquidazione.SelectedValue = 3
            ElseIf CDec(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)) - SommaPagato - ivaLiquidazioneT < 1 Then
                Me.cmb_Liquidazione.SelectedValue = 3
            Else
                Me.cmb_Liquidazione.SelectedValue = 1
            End If

            'IMPORTI PROGRESSIVI
            Dim FiltraStoricoSAL As String = ""
            If vIdPagamento > 0 Then
                FiltraStoricoSAL = " and ID<=" & vIdPagamento
            End If

            importoP = 0
            penaleP = 0
            oneriP = 0
            astaP = 0
            ivaP = 0
            ritenutaP = 0
            rimborsoP = 0
            risultato1P = 0
            risultato2P = 0
            risultato3P = 0
            risultato4P = 0

            Dim Somma1 As Decimal = 0
            Dim sRisultato As String = ""

            'RIEPILOGO MANUTENZIONI (IMPORTI A CONSUMO)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                      & ",(select anticipo_contrattuale from siscom_mi.prenotazioni where prenotazioni.id=manutenzioni.id_prenotazione_pagamento) as anticipo_contrattuale," _
                                      & "(select PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID= manutenzioni.id_appalto )) as perc_iva" _
                               & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=3  and id_stato<>-3 " _
                                                                   & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & myReader1("ID_APPALTO") & ")) " & FiltraStoricoSAL & ")" _
                               & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


            myReader2 = par.cmd.ExecuteReader()
            anticipo = 0
            importo_iva = 0

            While myReader2.Read
                '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                ' If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If

                    anticipo += par.IfNull(myReader2("ANTICIPO_CONTRATTUALE"), 0)

                    sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgress2(Somma1, par.IfNull(myReader2("IVA_CONSUMO"), 0), par.IfNull(myReader2("RIMBORSI"), 0), par.IfNull(myReader2("PENALE2"), 0), par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0))
                'Else
                '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                '    myReader2.Close()
                '    Exit Sub
                'End If
            End While
            myReader2.Close()
            '****************************
            importo_iva += IsNumFormat((anticipo * perciva) / 100, "", "##,##0.00")

            'RIEPILOGO PRENOTAZIONI (IMPORTI A CANONE)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                      & " ID_VOCE_PF_IMPORTO,ID_APPALTO,PRENOTAZIONI.ID ,APPALTI.FL_RIT_LEGGE,anticipo_contrattuale  " _
                               & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI" _
                               & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=6  and id_stato<>-3 " _
                                                                   & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & myReader1("ID_APPALTO") & ")) " & FiltraStoricoSAL & ")" _
                               & "   and PRENOTAZIONI.ID_APPALTO=APPALTI.ID (+) "
            myReader2 = par.cmd.ExecuteReader

            While myReader2.Read

                anticipo += par.IfNull(myReader2("ANTICIPO_CONTRATTUALE"), 0)
                importo_iva += IsNumFormat((anticipo * perciva) / 100, "", "##,##0.00")

                sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgressCANONE(Somma1, par.IfNull(myReader2("FL_RIT_LEGGE"), 0), par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")

            End While
            myReader2.Close()
            '***************************



            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto"), TextBox).Text = IsNumFormat(importoP, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaP, "", "##,##0.00") '6 campo

            '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
            CType(Tab_SAL_RiepilogoProg.FindControl("LabelRitenuta"), Label).Visible = False
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Visible = False
            CType(Tab_SAL_RiepilogoProg.FindControl("LabelRitenutaEuro"), Label).Visible = False


            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaP, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(rimborsoP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(anticipo, "", "##,##0.00")


            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")


            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + anticipo, "", "##,##0.00")


            '***************************
            If par.IfNull(myReader1("DATA_TRASMISSIONE"), "") <> "" Then
                Trasmesso.Value = 1
            End If


            'ABILITO/DISABILITO CAMPI in base allo stato
            AbilitaDisabilita()


        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdPagamento <> 0 Then
                ' LEGGO PAGAMENTI
                If IsNothing(sValoreAppalto) Then
                    par.cmd.CommandText = "SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID=" & vIdPagamento
                    sValoreAppalto = par.IfNull(par.cmd.ExecuteScalar, 0)
                End If

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento
                Else
                    par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                End If
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                'par.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()

                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If

                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                bloccato.Value = 1
                FrmSolaLettura()

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    'Valori passati dalla maschera di ricerca PRE-INSERIMENTO
    Sub SettaValRicerca()
        Dim scriptblock As String
        Dim FlagConnessione As Boolean
        Dim sOrder As String = ""
        Dim sAnno As String = ""
        Dim sDescManutenzioni As String = ""

        Dim ElencoID_Rapporti As String = ""

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Dim gen As Epifani.ListaGenerale

            For Each gen In lstListaRapporti
                If ElencoID_Rapporti <> "" Then
                    ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                Else
                    ElencoID_Rapporti = gen.STR
                End If
            Next

            'CONTROLLO se nel frattempo qualcuno ha selezionato le stesse manutenzioni (se si va in EX1.Number = 54 )
            par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI where ID in (" & ElencoID_Rapporti & " ) FOR UPDATE NOWAIT"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
            myReaderB = par.cmd.ExecuteReader()
            myReaderB.Close()
            '*******************************

            'CONTROLLO se un istante prima la selezione delle manutenzioni, qualcuno ha creato il pagamento ed è uscito dalla maschera
            par.cmd.CommandText = "select count(*) from SISCOM_MI.MANUTENZIONI where ID_PAGAMENTO is not null and ID in (" & ElencoID_Rapporti & " )"
            myReaderB = par.cmd.ExecuteReader()

            If myReaderB.Read Then
                If par.IfNull(myReaderB(0), 0) > 0 Then
                    RadWindowManager1.RadAlert("Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!", 300, 150, "Attenzione", "", "null")

                    myReaderB.Close()

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        FlagConnessione = False
                    End If

                    AbilitaDisabilita()

                    Me.btnSalva.Visible = False
                    Exit Sub
                End If
            End If
            myReaderB.Close()
            '*************************************


            sValoreAppalto = Request.QueryString("AP")
            sValoreFornitore = Request.QueryString("FO")
            sValoreServizio = Request.QueryString("SV")


            Me.cmbStato.SelectedValue = 0 'par.IfNull(myReader1("STATO"), "0")
            Me.txtSTATO.Value = Me.cmbStato.SelectedValue

            'GRIGLIA
            BindGridManutenzioni("ID in (" & ElencoID_Rapporti & ")", sValoreAppalto, "")


            Me.lblProgAnnoPagamento.Text = ""
            ' Me.txtDataSAL.SelectedDate = ""
            'Me.txtDataDel.Text = ""

            'FORNITORE
            par.cmd.CommandText = "select  ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(TIPO) as TIPO,trim(COD_FORNITORE) as COD_FORNITORE " _
                               & " from SISCOM_MI.FORNITORI where ID=" & sValoreFornitore

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    Else
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    End If
                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                    Else
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                    End If
                End If

                If par.IfNull(myReader1("TIPO"), "") = "F" Then
                    Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader1("ID"), "") & "','Fornitore','height=700,width=1300');")
                Else
                    Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader1("ID"), "") & "','Fornitore','height=700,width=1300');")
                End If

            End If
            myReader1.Close()

            'IBAN **************************************************
            par.cmd.CommandText = "select IBAN from SISCOM_MI.FORNITORI_IBAN " _
                               & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & sValoreAppalto & ")"

            myReader1 = par.cmd.ExecuteReader
            While myReader1.Read
                If Strings.Len(Strings.Trim(Me.txtIBAN.Text)) = 0 Then
                    Me.txtIBAN.Text = par.IfNull(myReader1("IBAN"), "")
                Else
                    Me.txtIBAN.Text = Me.txtIBAN.Text & vbCrLf & par.IfNull(myReader1("IBAN"), "")
                End If
            End While
            myReader1.Close()
            '******************************************************


            'CONTRATTO
            par.cmd.CommandText = "select  * from SISCOM_MI.APPALTI where ID=" & sValoreAppalto
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                Me.HLink_Appalto.Text = par.IfNull(myReader1("NUM_REPERTORIO"), "")
                Me.HLink_Appalto.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Appalti.aspx?ID=" & par.IfNull(myReader1("ID"), "") & "&A=" & par.IfNull(myReader1("ID"), "") & "&IDL=-1','Appalto','height=700,width=1300');")

                Me.lblDataAppalto.Text = par.FormattaData(par.IfNull(myReader1("DATA_REPERTORIO"), ""))
                Me.txtDescAppalto.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
            End If
            myReader1.Close()


            'SERVIZIO E DETTAGLIO SERVIZIO
            'par.cmd.CommandText = "select   PF_VOCI_IMPORTO.ID as ""ID_PF_VOCI_IMPORTO"", PF_VOCI_IMPORTO.DESCRIZIONE as ""DETT_VOCE"", " _
            '                            & " TAB_SERVIZI.ID as ""ID_TAB_SERVIZI"", TAB_SERVIZI.DESCRIZIONE as ""SERVIZIO""" _
            '                 & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI " _
            '                 & " where PF_VOCI_IMPORTO.ID=" & sValoreServizio _
            '                 & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=TAB_SERVIZI.ID (+) "


            'myReader1 = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            '    Me.txtServizio.Text = par.IfNull(myReader1("SERVIZIO"), "")
            '    Me.txtServizioVoce.Text = par.IfNull(myReader1("DETT_VOCE"), "")

            'End If
            'myReader1.Close()


            'IMPORTI SAL
            importoT = 0
            penaleT = 0
            oneriT = 0
            astaT = 0
            ivaT = 0
            ritenutaT = 0
            rimborsoT = 0
            risultato1T = 0
            risultato2T = 0
            risultato3T = 0
            risultato4T = 0

            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                & ",(select anticipo_contrattuale from siscom_mi.prenotazioni where prenotazioni.id=manutenzioni.id_prenotazione_pagamento) as anticipo_contrattuale," _
                & " (select PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID= manutenzioni.id_appalto )) as perc_iva," _
                & " NVL((SELECT APPALTI_ANTICIPI_CONTRATTUALI.TIPO FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE APPALTI_ANTICIPI_CONTRATTUALI.ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID= manutenzioni.id_appalto ) AND APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO " _
                & " in  " _
               & "(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (MANUTENZIONI.ID_PF_VOCE_IMPORTO) " _
               & "      UNION " _
               & "      SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (MANUTENZIONI.ID_PF_VOCE_IMPORTO) " _
               & "     ) " _
                & "),0) AS TIPO " _
                & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                & " where MANUTENZIONI.ID In (" & ElencoID_Rapporti & ")" _
                & " And SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                & " ORDER BY TIPO DESC"

            myReader1 = par.cmd.ExecuteReader()

            Dim anticipo As Decimal = 0
            Dim perciva As Decimal = 0
            Dim risultato3TparzialeConAnticipo As Decimal = 0
            Dim risultato3Tparziale As Decimal = 0
            Dim anticipoCalcolato As Decimal = 0
            While myReader1.Read
                '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                ' If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If
                    anticipo += par.IfNull(myReader1("ANTICIPO_CONTRATTUALE"), 0)
                    CalcolaImporti(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), par.IfNull(myReader1("IVA_CONSUMO"), 0), par.IfNull(myReader1("RIMBORSI"), 0), par.IfNull(myReader1("PENALE2"), 0), par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), par.IfNull(myReader1("IMPORTO_ONERI_CONS"), 0))
                    If par.IfNull(myReader1("TIPO"), 0) = "1" Then
                        par.cmd.CommandText = "SELECT id FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID in " _
                                            & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce.Value & ") " _
                                            & " UNION " _
                                            & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce.Value & ")" _
                                            & ")"
                        Dim dtVociImporto As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                        For Each riga As Data.DataRow In dtVociImporto.Rows
                            If par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0) = riga.Item("ID") Then
                                anticipoCalcolato = risultato3T * 0.2D
                            End If
                        Next
                    End If
                    If sDescManutenzioni = "" Then
                        sDescManutenzioni = par.IfNull(myReader1("DESCRIZIONE"), "")
                    Else
                        sDescManutenzioni = sDescManutenzioni & vbCrLf & par.IfNull(myReader1("DESCRIZIONE"), "")
                    End If
                    perciva = par.IfNull(myReader1("PERC_IVA"), 0)

                'Else
                '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                '    myReader1.Close()

                '    If FlagConnessione = True Then
                '        par.cmd.Dispose()
                '        par.OracleConn.Close()
                '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '        FlagConnessione = False
                '    End If
                '    Exit Sub
                'End If
            End While
            myReader1.Close()


            Me.txtDescManutenzioni.Text = sDescManutenzioni

            CType(Tab_SAL_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(importoT, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriT, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1T, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaT, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2T, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaT, "", "##,##0.00") '6 campo

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3T, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaT, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(rimborsoT, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4T, "", "##,##0.00")
            Dim importo_iva As Decimal = 0
            If Me.txtSTATO.Value >= 1 Then
                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(anticipo, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                importo_iva = IsNumFormat((anticipo * perciva) / 100, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + anticipo, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")
            Else
                If tipoAnticipo.Value = "1" Then
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(importoDaProporre.Value, "", "##,##0.00")

                    importo_iva = IsNumFormat((CDec(importoDaProporre.Value) * perciva) / 100, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + CDec(importoDaProporre.Value), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                Else
                    par.cmd.CommandText = "SELECT NVL(VOCE_ANTICIPO,-1) FROM SISCOM_MI.APPALTI WHERE ID = " & sValoreAppalto
                    Dim voceAnticipo As String = par.IfNull(par.cmd.ExecuteScalar, "-1")
                    Dim risultatoAnticipo As Decimal = risultato3T * 0.2D
                    If voceAnticipo <> "-1" Then
                        If anticipoCalcolato <> 0 Then
                            risultatoAnticipo = anticipoCalcolato
                        Else
                            risultatoAnticipo = 0
                        End If
                    End If

                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")


                    importo_iva = IsNumFormat((Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)) * perciva) / 100, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoIvaTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("txtImportoTotaleTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                End If

            End If

            '**********************************************


            'IMPORTI PROGRESSIVI
            Dim FiltraStoricoSAL As String = ""
            If vIdPagamento > 0 Then
                FiltraStoricoSAL = " And ID<=" & vIdPagamento
            End If


            importoP = 0
            penaleP = 0
            oneriP = 0
            astaP = 0
            ivaP = 0
            ritenutaP = 0
            rimborsoP = 0
            risultato1P = 0
            risultato2P = 0
            risultato3P = 0
            risultato4P = 0

            Dim Somma1 As Decimal = 0
            Dim sRisultato As String = ""
            anticipo = 0
            importo_iva = 0

            'RIEPILOGO MANUTENZIONI (IMPORTI A CONSUMO)
            '& "   And ANNO=" & Year(Now)  tolto il 25/01/2012
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                      & ",(select anticipo_contrattuale from siscom_mi.prenotazioni where prenotazioni.id=manutenzioni.id_prenotazione_pagamento) as anticipo_contrattuale" _
                               & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=3  And id_stato<>-3 " _
                                                                   & "   And ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & sValoreAppalto & "))" & FiltraStoricoSAL & ")" _
                               & "   And SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                'If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If
                    anticipo += par.IfNull(myReader1("ANTICIPO_CONTRATTUALE"), 0)

                    sRisultato = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgress(Somma1, par.IfNull(myReader1("IVA_CONSUMO"), 0), par.IfNull(myReader1("RIMBORSI"), 0), par.IfNull(myReader1("PENALE2"), 0), par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), par.IfNull(myReader1("IMPORTO_ONERI_CONS"), 0))
                'Else
                '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                '    myReader1.Close()

                '    If FlagConnessione = True Then
                '        par.cmd.Dispose()
                '        par.OracleConn.Close()
                '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '        FlagConnessione = False
                '    End If
                '    Exit Sub
                'End If

            End While
            myReader1.Close()

            importo_iva += IsNumFormat((anticipo * perciva) / 100, "", "##,##0.00")


            'RIEPILOGO PRENOTAZIONI (IMPORTI A CANONE)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                      & " ID_VOCE_PF_IMPORTO,ID_APPALTO,PRENOTAZIONI.ID ,APPALTI.FL_RIT_LEGGE,anticipo_contrattuale  " _
                               & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI" _
                               & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=6  And id_stato<>-3 " _
                                                                   & "   And ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & sValoreAppalto & ")) " & FiltraStoricoSAL & ")" _
                               & "   And PRENOTAZIONI.ID_APPALTO=APPALTI.ID (+) "
            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                anticipo += par.IfNull(myReader1("ANTICIPO_CONTRATTUALE"), "0")
                importo_iva += IsNumFormat((anticipo * perciva) / 100, "", "##,##0.00")

                sRisultato = par.IfNull(myReader1("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgressCANONE(Somma1, par.IfNull(myReader1("FL_RIT_LEGGE"), 0), par.IfNull(myReader1("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), "PRENOTATO")

            End While
            myReader1.Close()
            '***************************




            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto"), TextBox).Text = IsNumFormat(importoP, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaP, "", "##,##0.00") '6 campo

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaP, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(rimborsoP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(anticipo, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")


            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + anticipo, "", "##,##0.00")
            '***************************


            'Me.txtDescAttPagamento.Text = "Pagamento ODL (Vedi Allegato)"
            'Me.txtDescAttPagamento.Text = ""

            AbilitaDisabilita()
            Session.Add("LAVORAZIONE", "1")


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()

                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Non è possibile effettuare il Sal perchè un ODL è aperto da un altro utente!');document.location.href='../../pagina_home_ncp.aspx'" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
                BindGridManutenzioni("ID in (" & ElencoID_Rapporti & ")", sValoreAppalto, "")
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

                AbilitaDisabilita()

                Me.btnSalva.Visible = False


            Else
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'Page.Dispose()

            Session.Add("LAVORAZIONE", "0")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdPagamento = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If
        idSAL.Value = vIdPagamento

    End Sub

    Public Function ControlloCampi() As Boolean

        ControlloCampi = True


        If IsNothing(Me.txtDataSAL.SelectedDate) And IsNothing(Me.txtDataDel.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la Data del SAL e la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")

            ControlloCampi = False
            Exit Function
        ElseIf IsNothing(Me.txtDataSAL.SelectedDate) And Not IsNothing(Me.txtDataDel.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la Data del SAL!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        ElseIf Not IsNothing(Me.txtDataSAL.SelectedDate) And IsNothing(Me.txtDataDel.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If txtDataDel.Enabled = True Then
            If Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4) <> DateTime.Now.Year Then
                RadWindowManager1.RadAlert("Attenzione...L\'anno della data di Emissione Pagamento è diverso dal: " & DateTime.Now.Year & "!", 300, 150, "Attenzione", "", "null")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If Me.cmbStato.SelectedValue = -1 Then
            RadWindowManager1.RadAlert("Selezionare lo stato!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

    End Function

    'PRIMO SALVATAGGIO (stato= NON FIRMATO)
    Private Sub Salva()
        Dim oDataGridItem As GridDataItem

        Dim sDescrizione As String = "Manutenzione dei seguenti ODL/ANNO:"

        Dim ID_Manutenzioni As New System.Collections.ArrayList()
        Dim ID_Prenotazioni As New System.Collections.ArrayList()

        Dim i As Integer
        Dim sStrManu As String = ""
        Dim sStrPre As String = ""

        Dim Importo_ConsuntivatoTotale As Decimal

        Dim sRisultato As String = ""

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Try



            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)



            ID_Manutenzioni.Clear()
            ID_Prenotazioni.Clear()

            For Each oDataGridItem In CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).Items

                ID_Prenotazioni.Add(oDataGridItem.Cells(16).Text) 'SISCOM_MI.MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO
                ID_Manutenzioni.Add(oDataGridItem.Cells(2).Text) 'SISCOM_MI.MANUTENZIONI.ID
                sDescrizione = sDescrizione & "  " & oDataGridItem.Cells(14).Text   'ODL_NUM
            Next

            sStrPre = ""
            For i = 0 To ID_Prenotazioni.Count - 1
                If sStrPre = "" Then
                    sStrPre = ID_Prenotazioni(i)
                Else
                    sStrPre = sStrPre & "," & ID_Prenotazioni(i)
                End If
            Next i

            sStrManu = ""
            For i = 0 To ID_Manutenzioni.Count - 1

                If sStrManu = "" Then
                    sStrManu = ID_Manutenzioni(i)
                Else
                    sStrManu = sStrManu & "," & ID_Manutenzioni(i)
                End If
            Next i


            'CONTROLLO se è stato premuto più volte SALVA ed il PAGAMENTO è stato già inserito
            par.cmd.CommandText = "select count(*) from SISCOM_MI.MANUTENZIONI where ID_PAGAMENTO is not null and ID in (" & sStrManu & " )"
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                If par.IfNull(myReader1(0), 0) > 0 Then

                    myReader1.Close()

                    par.cmd.CommandText = "select distinct(ID_PAGAMENTO) from SISCOM_MI.MANUTENZIONI where ID_PAGAMENTO is not null and ID in (" & sStrManu & " )"
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        vIdPagamento = par.IfNull(myReader1(0), 0)
                    End If
                    myReader1.Close()

                    GoTo STAMPA
                End If
            End If
            myReader1.Close()
            '*************************************



            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            '1) Per Ogni PRENOTAZIONE, Estraggo il Budget Per la VOCE e STRUTTURA
            '2) Somma dell'IMPORTO_CONSUNTIVATO di tutti i pagamenti per quella VOCE e struttura


            Dim Valore1 As Decimal = 0
            Dim SommaConsuntivato As Decimal = 0
            Dim SommaValoreLordo As Decimal = 0
            Dim SommaValoreAssestatoLordo As Decimal = 0
            Dim SommaValoreVariazioni As Decimal = 0
            Dim SommaAssesato As Decimal = 0


            '*********************************************************************************************************************************************************************
            '****************************************** CONTROLLO RESIDUO DISPONIBILITA' SUFFICIENTE A EMETTERE IL PAGAMENTO *****************************************************
            '*********************************************************************************************************************************************************************

            Dim vociControl() As String = sStrPre.Split(",")
            Dim flCc As Boolean = False
            For Each s As String In vociControl
                flCc = False


                par.cmd.CommandText = "select ID_VOCE_PF,IMPORTO_APPROVATO,ID_STRUTTURA,NVL(ANTICIPO_CONTRATTUALE_CON_IVA,0) AS ANTICIPO_CONTRATTUALE_CON_IVA from SISCOM_MI.PRENOTAZIONI where ID =" & s
                myReader1 = par.cmd.ExecuteReader()


                If myReader1.Read Then
                    '**********controllo se FL_CC

                    par.cmd.CommandText = "select FL_CC from SISCOM_MI.pf_voci where id = " & par.IfNull(myReader1("ID_VOCE_PF"), 0)
                    myReader2 = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        If par.IfNull(myReader2("fl_cc"), 0) = 1 Then
                            flCc = True
                        End If
                    End If
                    myReader2.Close()

                    If flCc = False Then

                        '******NON FL_CC***********NON FL_CC***********NON FL_CC***********NON FL_CC***********NON FL_CC***********NON FL_CC*****
                        'CONSUNTIVATO
                        par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO)-SUM(ANTICIPO_CONTRATTUALE_CON_IVA) " _
                        & " from   SISCOM_MI.PRENOTAZIONI " _
                        & " where  ID_VOCE_PF=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
                        & "   and  ID_STATO>1 " _
                        & "   and  ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            SommaConsuntivato = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()


                        'SOMMA_LORDO
                        par.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                           & " where ID_VOCE=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
                                           & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaValoreLordo = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        'SOMMA_LORDO_ASSESSTATO
                        par.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                           & " where ID_VOCE=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
                                           & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)


                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        'VARIAZIONI
                        par.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                           & " where ID_VOCE=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
                                           & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)


                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaValoreVariazioni = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        'DA_APPROVARE + CONSUNTIVATO < (SOMMA_LORDO +SOMMA_LORDO_ASSESSTATO)
                        Valore1 = par.IfNull(myReader1("IMPORTO_APPROVATO"), 0) - par.IfNull(myReader1("ANTICIPO_CONTRATTUALE_CON_IVA"), 0) + SommaConsuntivato
                        SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni

                    Else

                        '******FL_CC***********FL_CC***********FL_CC***********FL_CC*****
                        'CONSUNTIVATO
                        par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO)-SUM(ANTICIPO_CONTRATTUALE_CON_IVA) " _
                        & " from   SISCOM_MI.PRENOTAZIONI " _
                        & " where  ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI A WHERE fl_cc = 1 AND CODICE = (SELECT CODICE FROM SISCOM_MI.PF_VOCI B WHERE B.ID=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) & "))" _
                        & "   and  ID_STATO>1 " _
                        & "   and  ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            SommaConsuntivato = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()


                        'SOMMA_LORDO
                        par.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                           & " where ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI A WHERE fl_cc = 1 AND CODICE = (SELECT CODICE FROM SISCOM_MI.PF_VOCI B WHERE B.ID=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) & "))" _
                                           & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaValoreLordo = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        'SOMMA_LORDO_ASSESSTATO
                        par.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                           & " where ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI A WHERE fl_cc = 1 AND CODICE = (SELECT CODICE FROM SISCOM_MI.PF_VOCI B WHERE B.ID=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) & "))" _
                                           & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)


                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        'VARIAZIONI
                        par.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                           & " where ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI A WHERE fl_cc = 1 AND CODICE = (SELECT CODICE FROM SISCOM_MI.PF_VOCI B WHERE B.ID=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) & "))" _
                                           & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)


                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaValoreVariazioni = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        'DA_APPROVARE + CONSUNTIVATO < (SOMMA_LORDO +SOMMA_LORDO_ASSESSTATO)
                        Valore1 = par.IfNull(myReader1("IMPORTO_APPROVATO"), 0) - par.IfNull(myReader1("ANTICIPO_CONTRATTUALE_CON_IVA"), 0) + SommaConsuntivato
                        SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni


                    End If







                    If Valore1 > SommaAssesato Then
                        RadWindowManager1.RadAlert("L\'importo da prenotare supera il budget approvato per la voce di BP, impossibile emettere il SAL!", 300, 150, "Attenzione", "", "null")

                        myReader1.Close()
                        par.myTrans.Rollback()
                        Exit Sub

                    End If

                End If
                myReader1.Close()

            Next


            ' ''par.cmd.CommandText = "select ID_VOCE_PF,IMPORTO_APPROVATO,ID_STRUTTURA from SISCOM_MI.PRENOTAZIONI where ID in (" & sStrPre & ")"
            ' ''myReader1 = par.cmd.ExecuteReader()
            ' ''While myReader1.Read

            ' ''    'CONSUNTIVATO
            ' ''    'par.cmd.CommandText = "select sum(IMPORTO_CONSUNTIVATO) from SISCOM_MI.PAGAMENTI " _
            ' ''    '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
            ' ''    '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            ' ''    '                                            & " where  ID_VOCE_PF=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
            ' ''    '                                            & "   and  ID_STATO>1 " _
            ' ''    '                                            & "   and  ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1) & ")"

            ' ''    'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si può prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            ' ''    par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
            ' ''                        & " from   SISCOM_MI.PRENOTAZIONI " _
            ' ''                        & " where  ID_VOCE_PF=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
            ' ''                        & "   and  ID_STATO>1 " _
            ' ''                        & "   and  ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)

            ' ''    myReader2 = par.cmd.ExecuteReader()
            ' ''    If myReader2.Read Then
            ' ''        SommaConsuntivato = par.IfNull(myReader1(0), 0)
            ' ''    End If
            ' ''    myReader2.Close()



            ' ''    'SOMMA_LORDO
            ' ''    par.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
            ' ''                       & " where ID_VOCE=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
            ' ''                       & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)

            ' ''    myReader2 = par.cmd.ExecuteReader()
            ' ''    If myReader2.Read Then
            ' ''        sRisultato = par.IfNull(myReader2(0), "0")
            ' ''        SommaValoreLordo = Decimal.Parse(sRisultato)
            ' ''    End If
            ' ''    myReader2.Close()

            ' ''    'SOMMA_LORDO_ASSESSTATO
            ' ''    par.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
            ' ''                       & " where ID_VOCE=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
            ' ''                       & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)


            ' ''    myReader2 = par.cmd.ExecuteReader()
            ' ''    If myReader2.Read Then
            ' ''        sRisultato = par.IfNull(myReader2(0), "0")
            ' ''        SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
            ' ''    End If
            ' ''    myReader2.Close()

            ' ''    'VARIAZIONI
            ' ''    par.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
            ' ''                       & " where ID_VOCE=" & par.IfNull(myReader1("ID_VOCE_PF"), 0) _
            ' ''                       & "   and ID_STRUTTURA=" & par.IfNull(myReader1("ID_STRUTTURA"), -1)


            ' ''    myReader2 = par.cmd.ExecuteReader()
            ' ''    If myReader2.Read Then
            ' ''        sRisultato = par.IfNull(myReader2(0), "0")
            ' ''        SommaValoreVariazioni = Decimal.Parse(sRisultato)
            ' ''    End If
            ' ''    myReader2.Close()

            ' ''    'DA_APPROVARE + CONSUNTIVATO < (SOMMA_LORDO +SOMMA_LORDO_ASSESSTATO)
            ' ''    Valore1 = par.IfNull(myReader1("IMPORTO_APPROVATO"), 0) + SommaConsuntivato
            ' ''    SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni

            ' ''    If Valore1 > SommaAssesato Then
            ' ''        Response.Write("<script>alert('L\'importo da prenotare supera il budget approvato per la voce di BP, impossibile emettere il SAL!');</script>")
            ' ''        myReader1.Close()
            ' ''        par.myTrans.Rollback()
            ' ''        Exit Sub

            ' ''    End If

            ' ''End While
            ' ''myReader1.Close()
            '*********************************************************************************************************************************************************************
            '****************************************************** FINE CONTROLLO RESIDUO DISPONIBILE ***************************************************************************
            '*********************************************************************************************************************************************************************



            'SOMMMA IMPORTO_PRENOTATO di Tutte le PRENOTAZIONI meno la RITENUTA DI LEGGE IVATA delle MANUTENZIONI selezionate
            Importo_ConsuntivatoTotale = 0
            par.cmd.CommandText = "select IMPORTO_APPROVATO,RIT_LEGGE_IVATA from SISCOM_MI.PRENOTAZIONI where ID in (" & sStrPre & ")"
            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                Dim Val, Rit_Legge As Decimal

                Val = Decimal.Parse(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0))
                Rit_Legge = Decimal.Parse(par.IfNull(myReader1("RIT_LEGGE_IVATA"), 0))

                Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

            End While
            myReader1.Close()
            '****************************************


            '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
            If Importo_ConsuntivatoTotale = 0 Then
                RadWindowManager1.RadAlert("Nessun importo stanziato per i pagamenti selezionati!", 300, 150, "Attenzione", "", "null")

                par.myTrans.Rollback()
                Exit Sub
            End If


            'txtImportoTrattenuto + IVA PER POTER FARE IL CONFRONTO CON IL CONSUNTIVATO TOTALE CHE E' IVATO
            par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & ")"
            Dim percIva As Integer = CInt(par.cmd.ExecuteScalar)
            Dim importo_trattenuto As Decimal = CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text) + (CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text) * percIva / 100)
            If importo_trattenuto >= CDec(Importo_ConsuntivatoTotale) Then

                RadWindowManager1.RadAlert("Attenzione...Recupero anticipazione contrattuale maggiore dell\'importo da consuntivare!", 300, 150, "Attenzione", "", "null")
                par.myTrans.Rollback()
                SettaValRicerca()
                Exit Sub
            End If

            If CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text) > CDec(ImportoResiduoDaTrattenere.Value) Then
                RadWindowManager1.RadAlert("Attenzione...Recupero anticipazione contrattuale maggiore dell\'importo residuo da trattenere!", 300, 150, "Attenzione", "", "null")
                par.myTrans.Rollback()
                cmbStato.ClearSelection()
                cmbStato.SelectedValue = "0"
                SettaValRicerca()
                Exit Sub
            End If

            'GIANCARLO 16/02/2018
            'Se non viene inserito un SAL firmato non è possibile effettuare l'aggiornamento di stato da NON FIRMATO a FIRMATO
            If Not cmbStato.SelectedValue.Equals("0") Then
                Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
                Dim NOME As String = par.cmd.ExecuteScalar
                If String.IsNullOrEmpty(NOME) Then
                    RadWindowManager1.RadAlert("Attenzione...Non è possibile modificare lo stato del SAL in <strong>FIRMATO</strong><br />senza prima aver allegato un <strong>sal firmato</strong>!", 300, 150, "Attenzione", "", "null")
                    par.myTrans.Rollback()
                    SettaValRicerca()
                    Exit Sub
                End If
            End If


            '2) CREO PAGAMENTO
            par.cmd.CommandText = "select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                vIdPagamento = myReader1(0)
            End If
            myReader1.Close()


            par.cmd.Parameters.Clear()

            '   select APPALTI_ANTICIPI_CONTRATTUALI.TIPO,APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO 
            '       INTO tipoAnticipo,voce 
            'FROM APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO in (SELECT ID FROM APPALTI WHERE ID_GRUPPO IN (select id_gruppo from appalti where appalti.id=:new.id_appalto));
            'IF tipoAnticipo=0 THEN
            '   :NEW.IMPORTO_CONSUNTIVATO_ANT:=:NEW.IMPORTO_CONSUNTIVATO;
            'ELSE
            '   SELECT SUM(IMPORTO_aPPROVATO-NVL(RIT_LEGGE_IVATA,0)) INTO importoConsuntivatoAnt FROM PRENOTAZIONI WHERE ID_PAGAMENTO=:NEW.ID AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO
            '   in (SELECT ID FROM PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID=VOCE
            '           UNION
            '       SELECT ID FROM PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID=VOCE
            '   );
            '   :NEW.IMPORTO_CONSUNTIVATO_ANT:=importoConsuntivatoAnt;
            'END IF;    

            Dim voce As Integer = 0
            Dim tipo As Integer = 0
            par.cmd.CommandText = "SELECT APPALTI_ANTICIPI_CONTRATTUALI.TIPO AS TIPO, " _
                & " APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO AS VOCE" _
                & " FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI " _
                & " WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT MAX(ID_APPALTO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & sStrPre & "))))"
            Dim lettoreAnticipo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreAnticipo.Read Then
                tipo = par.IfNull(lettoreAnticipo("TIPO"), 0)
                voce = par.IfNull(lettoreAnticipo("VOCE"), 0)
            End If
            lettoreAnticipo.Close()
            Dim importo_consuntivato_ant As Decimal = 0
            If tipo = 0 Then
                importo_consuntivato_ant = Importo_ConsuntivatoTotale
            Else
                par.cmd.CommandText = "SELECT SUM(IMPORTO_aPPROVATO-NVL(RIT_LEGGE_IVATA,0)) " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO in " _
                    & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce & ") " _
                    & " UNION " _
                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce & ")" _
                    & ") AND PRENOTAZIONI.ID IN (" & sStrPre & ")"
                importo_consuntivato_ant = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If


            par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI  " _
                                        & " (ID, DATA_SAL,DATA_EMISSIONE,DATA_STAMPA,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,STATO_FIRMA,CONTO_CORRENTE,anticipo_contrattuale,importo_consuntivato_ant) " _
                                & "values (:id,:data_sal,:data,:data_stampa,:descrizione,:importo,:id_fornitore,:id_appalto,1,3,:stato_firma,:conto_corrente,:anticipo_contrattuale,:importo_consuntivato_ant) "

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamento))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtDataDel.SelectedDate)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_sal", par.AggiustaData(par.IfNull(Me.txtDataSAL.SelectedDate, ""))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_stampa", Format(Now, "yyyyMMdd")))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Me.txtDescAttPagamento.Text)) 'sDescrizione))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", Importo_ConsuntivatoTotale))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(sValoreFornitore)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", RitornaNullSeIntegerMenoUno(sValoreAppalto)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato_firma", Convert.ToInt32(Me.cmbStato.SelectedValue)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anticipo_contrattuale", CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_consuntivato_ant", importo_consuntivato_ant))


            par.cmd.ExecuteNonQuery()

            'DataDelCaricata.Value = Me.txtDataDel.Text

            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************


            'UPDATE PRENOTAZIONI
            par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=2, ID_PAGAMENTO=" & vIdPagamento & " where ID in (" & sStrPre & ")"
            par.cmd.ExecuteNonQuery()

            'UPDATE PRENOTAZIONI se DATA_PRENOTAZIONE è maggiore della DATA_EMISSIONE di PAGAMENTI
            par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_PRENOTAZIONE=" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                               & " where ID_PAGAMENTO=" & vIdPagamento _
                               & "   and DATA_PRENOTAZIONE>" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                               & "   and substr(DATA_PRENOTAZIONE,1,4)=" & Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4)
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            '**********************************

            'UPDATE MANUTENZIONI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=4, ID_PAGAMENTO=" & vIdPagamento & " where ID in (" & sStrManu & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE ID IN (" & sStrManu & ") AND ID_SEGNALAZIONI IS NOT NULL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim idSegnalazione As Integer = 0
            While lettore.Read
                idSegnalazione = par.IfNull(lettore("ID_SEGNALAZIONI"), 0)
                If idSegnalazione > 0 Then
                    'VERIFICARE LO STATO DELLA SEGNALAZIONE
                    par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idSegnalazione
                    Dim statoSegnalazione As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If statoSegnalazione = 7 Then
                        'SE TUTTE LE SEGNALAZIONI SONO IN STATO EMESSO PAGAMENTO CAMBIO STATO ALLA SEGNALAZIONE CON "CHIUSA"
                        par.cmd.CommandText = "SELECT DISTINCT STATO FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI=" & idSegnalazione & " AND STATO NOT IN (5,6) ORDER BY 1 ASC"
                        Dim lettoreStato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim primoStato As Integer = 0
                        If lettoreStato.Read Then
                            primoStato = par.IfNull(lettoreStato(0), 0)
                        End If
                        lettoreStato.Close()
                        If primoStato = 4 Then
                            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE (ID = " & idSegnalazione & " OR SEGNALAZIONI.ID IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione & ")) AND ID_STATO<>10"
                            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            While lettore2.Read
                                Dim idS As Integer = par.IfNull(lettore2(0), 0)
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO=10,DATA_CHIUSURA=TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') WHERE ID =" & idS
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & idS & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','EVASA','CHIUSA')"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                                    & " VALUES ( " & idS & ",'Emesso pagamento ultimo ODL collegato',TO_CHAR(SYSDATE,'YYYYMMDDHH24MI')," & Session.Item("ID_OPERATORE") & ",2) "
                                par.cmd.ExecuteNonQuery()
                            End While
                            lettore2.Close()
                        End If
                    End If
                End If
            End While
            lettore.Close()

            par.cmd.CommandText = ""
            '**********************************

            ''****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
            par.cmd.ExecuteNonQuery()
            '****************************************************

            ' COMMIT
            par.myTrans.Commit()
            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.PAGAMENTI WHERE ID=" & vIdPagamento
            txtDescAttPagamento.Text = par.IfNull(par.cmd.ExecuteScalar, "")


            par.cmd.CommandText = ""


STAMPA:
            '********
            '1)
            importoT = 0
            penaleT = 0
            oneriT = 0
            astaT = 0
            ivaT = 0
            ritenutaT = 0
            rimborsoT = 0
            risultato1T = 0
            risultato2T = 0
            risultato3T = 0
            risultato4T = 0


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                               & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID in (" & sStrManu & ")" _
                               & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                'If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If

                    CalcolaImporti(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), par.IfNull(myReader1("IVA_CONSUMO"), 0), par.IfNull(myReader1("RIMBORSI"), 0), par.IfNull(myReader1("PENALE2"), 0), par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), par.IfNull(myReader1("IMPORTO_ONERI_CONS"), 0))
                'Else
                '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                '    myReader1.Close()
                '    Exit Sub
                'End If

            End While
            myReader1.Close()



            'IMPORTI PROGRESSIVI
            Dim FiltraStoricoSAL As String = ""
            If vIdPagamento > 0 Then
                FiltraStoricoSAL = " and ID<=" & vIdPagamento
            End If

            importoP = 0
            penaleP = 0
            oneriP = 0
            astaP = 0
            ivaP = 0
            ritenutaP = 0
            rimborsoP = 0
            risultato1P = 0
            risultato2P = 0
            risultato3P = 0
            risultato4P = 0

            Dim Somma1 As Decimal = 0


            'RIEPILOGO MANUTENZIONI (IMPORTI A CONSUMO)
            ' & "   and ANNO=" & Year(Now)  ' tolto il 25/01/2012
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                      & ",(select anticipo_contrattuale from siscom_mi.prenotazioni where prenotazioni.id=manutenzioni.id_prenotazione_pagamento) as anticipo_contrattuale" _
                               & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=3  and id_stato<>-3 " _
                                                                   & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & sValoreAppalto & "))" & FiltraStoricoSAL & ") " _
                               & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "



            myReader1 = par.cmd.ExecuteReader()
            Dim anticipo As Decimal = 0

            While myReader1.Read
                '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                'If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If

                    anticipo += par.IfNull(myReader1("ANTICIPO_CONTRATTUALE"), 0)
                    sRisultato = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgress(Somma1, par.IfNull(myReader1("IVA_CONSUMO"), 0), par.IfNull(myReader1("RIMBORSI"), 0), par.IfNull(myReader1("PENALE2"), 0), par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), par.IfNull(myReader1("IMPORTO_ONERI_CONS"), 0))
                'Else
                '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                '    myReader1.Close()
                '    Exit Sub
                'End If

            End While
            myReader1.Close()

            'RIEPILOGO PRENOTAZIONI (IMPORTI A CANONE)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                      & " ID_VOCE_PF_IMPORTO,ID_APPALTO,PRENOTAZIONI.ID ,APPALTI.FL_RIT_LEGGE,ANTICIPO_CONTRATTUALE  " _
                                & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI" _
                                & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                & " where TIPO_PAGAMENTO=6  and id_stato<>-3 " _
                                & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & sValoreAppalto & "))" & FiltraStoricoSAL & ")" _
                                & "   and PRENOTAZIONI.ID_APPALTO=APPALTI.ID (+) "
            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                anticipo += par.IfNull(myReader1("ANTICIPO_CONTRATTUALE"), "0")

                sRisultato = par.IfNull(myReader1("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgressCANONE(Somma1, par.IfNull(myReader1("FL_RIT_LEGGE"), 0), par.IfNull(myReader1("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), "PRENOTATO")

            End While
            myReader1.Close()
            '***************************


            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto"), TextBox).Text = IsNumFormat(importoP, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaP, "", "##,##0.00") '6 campo

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaP, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(rimborsoP, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Text = IsNumFormat(anticipo, "", "##,##0.00")
            '***************************


            'SOMMMA PENALI delle MANUTENZIONI selezionate
            'par.cmd.CommandText = "select SUM(IMPORTO) from SISCOM_MI.APPALTI_PENALI where ID_MANUTENZIONE in (" & sStr1 & ")"
            'myReader1 = par.cmd.ExecuteReader()

            'If myReader1.Read Then
            '    penaleT = par.IfNull(myReader1(0), 0)
            'End If
            'myReader1.Close()
            '****************************************

            PdfSal(vIdPagamento)


            Me.txtSTATO.Value = Me.cmbStato.SelectedValue
            AbilitaDisabilita()



            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                RiempiCampi(myReader1)
            End If
            myReader1.Close()

            Session.Add("LAVORAZIONE", "1")


            'par.myTrans = par.OracleConn.BeginTransaction()
            'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            'Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

            TabberHide = "tabbertab"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"




        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    'Successivi SALVATAGGIO (Solo Combo FIRMA)
    Private Sub Update()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            '************************
            'GIANCARLO 16/02/2018
            'Se non viene inserito un SAL firmato non è possibile effettuare l'aggiornamento di stato da NON FIRMATO a FIRMATO
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
            Dim NOME As String = par.cmd.ExecuteScalar
            If Not cmbStato.SelectedValue.Equals("0") Then
                If String.IsNullOrEmpty(NOME) Then
                    RadWindowManager1.RadAlert("Attenzione...Non è possibile modificare lo stato del SAL in <strong>FIRMATO</strong><br />senza prima aver allegato un <strong>sal firmato</strong>!", 300, 150, "Attenzione", "", "null")
                    par.myTrans.Rollback()
                    cmbStato.ClearSelection()
                    cmbStato.SelectedValue = "0"
                    'SettaValRicerca()
                    Exit Sub
                End If
            Else
                If Not String.IsNullOrEmpty(NOME) Then
                    cmbStato.ClearSelection()
                    cmbStato.SelectedValue = "2"
                End If
            End If

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                                & " set STATO_FIRMA=:stato_firma, DATA_SAL=:data_sal, DATA_EMISSIONE=:data,CONTO_CORRENTE=:conto_corrente,DESCRIZIONE=:descrizione " _
                                & " where ID=" & vIdPagamento

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato_firma", Me.cmbStato.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtDataDel.SelectedDate)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_sal", par.AggiustaData(par.IfNull(Me.txtDataSAL.SelectedDate, ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Me.txtDescAttPagamento.Text)) 'sDescrizione))
            par.cmd.ExecuteNonQuery()

            'DataDelCaricata.Value = Me.txtDataDel.Text

            par.cmd.CommandText = ""
            '********************************************
            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
            CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Enabled = False

            ''****Scrittura evento MODIFICA DEL PAGAMENTO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Modificato lo stato della firma in: " & Me.cmbStato.SelectedItem.Text & "')"
            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()
            '****************************************************


            'UPDATE PRENOTAZIONI se DATA_PRENOTAZIONE è maggiore della DATA_EMISSIONE di PAGAMENTI
            par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_PRENOTAZIONE=" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                               & " where ID_PAGAMENTO=" & vIdPagamento _
                               & "   and DATA_PRENOTAZIONE>" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                               & "   and substr(DATA_PRENOTAZIONE,1,4)=" & Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4)

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            '**********************************


            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""

            Me.txtSTATO.Value = Me.cmbStato.SelectedValue
            AbilitaDisabilita()

            RadNotificationNote.Text = "Operazione completata correttamente!"
            RadNotificationNote.Show()



            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI) e non SONO in SOLO LETTURA
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text <> "1" Then
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader1.Close()

                Session.Add("LAVORAZIONE", "1")
                TabberHide = "tabbertab"

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtModificato.Text = "0"
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Dim sNote As String = ""
        Dim s1 As String = ""
        Try
            If txtElimina.Text = "1" Then

                s1 = ControllaSALsuccessivi()

                If s1 <> "" Then
                    RadWindowManager1.RadAlert("Attenzione: Impossibile annullare questo SAL perché ne sono stati emessi successivi (" & s1 & ")", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If

                If PagamentoStampato() = True Then
                    If Session.Item("FL_ANNULLA_SAL") = 1 Then
                        RadWindowManager1.RadAlert("Attenzione: è stato già stampato l\'attestato di pagamento! Ricordarsi di ristamparlo", 300, 150, "Attenzione", "", "null")
                    Else
                        RadWindowManager1.RadAlert("Attenzione: è stato già stampato l\'attestato di pagamento!\nRivolgersi ad un operatore abilitato a questo tipo di annullo", 300, 150, "Attenzione", "", "null")

                        Exit Sub
                    End If
                End If

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Dim controlloLiquidato As Boolean = False
                par.cmd.CommandText = "select count(*) from siscom_mi.pagamenti_liquidati where id_pagamento =" & vIdPagamento
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If lettore(0) > 0 Then
                        controlloLiquidato = True
                    End If
                End If
                lettore.Close()

                If controlloLiquidato Then
                    RadWindowManager1.RadAlert("Attenzione: il pagamento è già stato liquidato! Impossibile proseguire.", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=2, ID_PAGAMENTO=Null where ID_PAGAMENTO=" & vIdPagamento
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""


                'UPDATE PRENOTAZIONI
                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI ( " _
                                    & "    ID, ID_FORNITORE, ID_APPALTO, " _
                                    & "    ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO, " _
                                    & "    ID_PAGAMENTO, TIPO_PAGAMENTO, DESCRIZIONE, " _
                                    & "    DATA_PRENOTAZIONE, IMPORTO_PRENOTATO, IMPORTO_APPROVATO, " _
                                    & "    PROGR_FORNITORE, ANNO, DATA_SCADENZA, " _
                                    & "    DATA_STAMPA, ID_STRUTTURA, RIT_LEGGE_IVATA, " _
                                    & "    PERC_IVA, ID_PAGAMENTO_RIT_LEGGE, DATA_PRENOTAZIONE_TMP, " _
                                    & "    DATA_CONSUNTIVAZIONE, DATA_CERTIFICAZIONE, DATA_CERT_RIT_LEGGE, " _
                                    & "    IMPORTO_LIQUIDATO, DATA_ANNULLO, IMPORTO_RIT_LIQUIDATO, " _
                                    & "    FUORI_CAMPO_IVA, IMPONIBILE, IVA, " _
                                    & "    IMPONIBILE_LIQUIDATO, IVA_LIQUIDATA, ANTICIPO_CONTRATTUALE, " _
                                    & "    IMPORTO_PENALE, IMPONIBILE_RIMB, IVA_RIMB, " _
                                    & "    ANTICIPO_CONTRATTUALE_CON_IVA, FL_SCHEDA_IMPUTAZIONE) " _
                                    & " SELECT " _
                                    & "    SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, ID_FORNITORE, ID_APPALTO, " _
                                    & "    ID_VOCE_PF, ID_VOCE_PF_IMPORTO, -3 AS ID_STATO, " _
                                    & "    ID_PAGAMENTO, TIPO_PAGAMENTO, DESCRIZIONE, " _
                                    & "    DATA_PRENOTAZIONE, IMPORTO_PRENOTATO, IMPORTO_APPROVATO, " _
                                    & "    PROGR_FORNITORE, ANNO, DATA_SCADENZA, " _
                                    & "    DATA_STAMPA, ID_STRUTTURA, RIT_LEGGE_IVATA, " _
                                    & "    PERC_IVA, ID_PAGAMENTO_RIT_LEGGE, DATA_PRENOTAZIONE_TMP, " _
                                    & "    DATA_CONSUNTIVAZIONE, DATA_CERTIFICAZIONE, DATA_CERT_RIT_LEGGE, " _
                                    & "    IMPORTO_LIQUIDATO, '" & Format(Now, "YYYYMMDD") & "' DATA_ANNULLO, IMPORTO_RIT_LIQUIDATO, " _
                                    & "    FUORI_CAMPO_IVA, IMPONIBILE, IVA, " _
                                    & "    IMPONIBILE_LIQUIDATO, IVA_LIQUIDATA, ANTICIPO_CONTRATTUALE, " _
                                    & "    IMPORTO_PENALE, IMPONIBILE_RIMB, IVA_RIMB, " _
                                    & "    ANTICIPO_CONTRATTUALE_CON_IVA, FL_SCHEDA_IMPUTAZIONE " _
                                    & "    FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO = " & vIdPagamento
                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET ID_STATO=1, ID_PAGAMENTO=NULL, DATA_CERTIFICAZIONE = NULL " _
                    & " WHERE ID_PAGAMENTO = " & vIdPagamento _
                    & " AND ID_STATO <> -3"
                par.cmd.ExecuteNonQuery()

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamento
                par.cmd.CommandText = "update siscom_mi.pagamenti set id_stato = -3 where id = " & vIdPagamento
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""



                'LOG CANCELLAZIONE
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "ANNULLO SAL in PAGAMENTI MANUTENZIONE"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "PAGAMENTI di MANUTENZIONI"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", "SAL: " & Me.lblProgAnnoPagamento.Text & " del:" & Me.txtDataDel.SelectedDate))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left("Annullamento SAL: " & Me.lblProgAnnoPagamento.Text & " del:" & Me.txtDataDel.SelectedDate, 200)))

                sNote = "Annullamento SAL (Manutenzione) con SAL: " & Me.lblProgAnnoPagamento.Text & " del:" & Me.txtDataDel.SelectedDate _
                                                      & " , PAGAMENTO: " & Me.txtPAGAMENTI_PROGR.Value _
                                                      & " , Num. Rep.: " & Me.HLink_Appalto.Text

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", sNote))


                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************

                ' COMMIT
                par.myTrans.Commit()





                Session.Add("LAVORAZIONE", "0")


                'PARAMENTRI x LA RICERCA
                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))
                sValoreStruttura = Request.QueryString("STR")

                sValoreFornitore = Request.QueryString("FO")
                sValoreAppalto = Request.QueryString("AP")
                sValoreServizio = Request.QueryString("SV")

                sValoreData_Dal = Request.QueryString("DAL")
                sValoreData_Al = Request.QueryString("AL")


                sOrdinamento = Request.QueryString("ORD")
                sValoreProvenienza = Request.QueryString("PROVENIENZA")
                '***********************************************

                lstListaRapporti.Clear()

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Item("LAVORAZIONE") = "0"

                'Page.Dispose()
                '***************************


                Select Case sValoreProvenienza

                    Case "RISULTATI_SAL"
                        annullaSal = "1"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "alert('Operazione effettuata!');Blocca_SbloccaMenu(0);location.replace('RisultatiSAL.aspx?FO=" & sValoreFornitore _
                                                                                      & "&AP=" & sValoreAppalto _
                                                                                      & "&SV=" & sValoreServizio _
                                                                                      & "&DAL=" & sValoreData_Dal _
                                                                                      & "&AL=" & sValoreData_Al _
                                                                                      & "&ORD=" & sOrdinamento _
                                                                                      & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                      & "&PROVENIENZA=" & sValoreProvenienza & "');", True)

                    Case "RISULTATI_SAL_FIRMA"
                        annullaSal = "1"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "alert('Operazione effettuata!');Blocca_SbloccaMenu(0);location.replace('RisultatiSAL_FIRMA.aspx?FO=" & sValoreFornitore _
                                                                                      & "&AP=" & sValoreAppalto _
                                                                                      & "&SV=" & sValoreServizio _
                                                                                      & "&DAL=" & sValoreData_Dal _
                                                                                      & "&AL=" & sValoreData_Al _
                                                                                      & "&ST=" & sValoreStato _
                                                                                      & "&STR=" & sValoreStruttura _
                                                                                      & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                      & "&PROVENIENZA=" & sValoreProvenienza & "');", True)
                    Case Else
                        If txtindietro.Text = 1 Then
                            annullaSal = "1"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "Blocca_SbloccaMenu(0);document.location.href=""../../pagina_home_ncp.aspx", True)
                        End If
                End Select

            Else
                CType(Me.Page.FindControl("txtElimina"), TextBox).Text = "0"
            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)


            'Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click

        sValoreProvenienza = Request.QueryString("PROVENIENZA")

        If txtModificato.Text <> "111" Then

            lstListaRapporti.Clear()

            If sValoreProvenienza <> "CHIAMATA_DIRETTA" Then
                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                HttpContext.Current.Session.Remove("dtDettagli")
                Session.Item("LAVORAZIONE") = "0"
            End If

            'Page.Dispose()
            '**************************

            If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"
                Response.Write("<script>window.close();</script>")
            Else
                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
            End If


        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub
    Protected Sub btnINDIETRO_Click(sender As Object, e As System.EventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")


            'PARAMENTRI x LA RICERCA
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreStato = Request.QueryString("ST")

            sOrdinamento = Request.QueryString("ORD")
            sValoreProvenienza = Request.QueryString("PROVENIENZA")
            sValoreAdp = Request.QueryString("ADP")

            lstListaRapporti.Clear()

            'CHIUSURA DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("dtDettagli")
            Session.Item("LAVORAZIONE") = "0"

            ''Page.Dispose()
            '**************************

            Select Case sValoreProvenienza

                Case "RISULTATI_SAL"
                    Response.Write("<script>location.replace('RisultatiSAL.aspx?FO=" & sValoreFornitore _
                                                                                  & "&AP=" & sValoreAppalto _
                                                                                  & "&SV=" & sValoreServizio _
                                                                                  & "&DAL=" & sValoreData_Dal _
                                                                                  & "&AL=" & sValoreData_Al _
                                                                                  & "&ORD=" & sOrdinamento _
                                                                                  & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                  & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")


                Case "RISULTATI_SAL_FIRMA"
                    Response.Write("<script>location.replace('RisultatiSAL_FIRMA.aspx?FO=" & sValoreFornitore _
                                                                                  & "&AP=" & sValoreAppalto _
                                                                                  & "&SV=" & sValoreServizio _
                                                                                  & "&DAL=" & sValoreData_Dal _
                                                                                  & "&AL=" & sValoreData_Al _
                                                                                  & "&ST=" & sValoreStato _
                                                                                  & "&STR=" & sValoreStruttura _
                                                                                  & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                  & "&ADP=" & sValoreAdp _
                                                                                  & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")

                Case "CHIAMATA_DIRETTA"
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
                    Response.Write("<script>window.close();</script>")

                Case Else
                    If txtindietro.Text = 1 Then
                        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                    End If
            End Select


        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub


    Private Sub FrmSolaLettura(Optional ByVal NascondiAnnulla As Boolean = True)
        'Disabilita il form (SOLO LETTURA)

        Me.btnSalva.Visible = False
        If NascondiAnnulla = True Then
            Me.btnAnnulla.Visible = False
        End If

        Me.btnStampaSAL.Visible = True
        Me.btnStampa.Visible = False
        btnRielbSal.Visible = False
        btnRielaboraPagamento.Visible = False


        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = True
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
        Next

        '*** FORM RIEPILOGO
        For Each CTRL In Me.Tab_SAL_Riepilogo.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = True
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
        Next

        If bloccato.Value <> 1 Then
            Try
                'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                        & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                        & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.FL_CC=0"
                    Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lett.Read Then
                        'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                        If par.IfNull(Lett(0), 0) >= 6 Then
                            btnAnnulla.Visible = False
                            If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                                btnAnnulla.Visible = True
                            End If
                        End If
                        If par.IfNull(Lett(0), 0) = 5 Then
                            btnAnnulla.Visible = False
                            If Session.Item("FL_ANNULLA_SAL") = 1 Then
                                btnAnnulla.Visible = True
                            End If
                        End If

                    End If
                    Lett.Close()

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else

                    par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                       & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                       & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.FL_CC=0"
                    Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lett.Read Then
                        'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                        If par.IfNull(Lett(0), 0) >= 6 Then
                            btnAnnulla.Visible = False
                            If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                                btnAnnulla.Visible = True
                            End If
                        End If
                        If par.IfNull(Lett(0), 0) = 5 Then
                            btnAnnulla.Visible = False
                            If Session.Item("FL_ANNULLA_SAL") = 1 Then
                                btnAnnulla.Visible = True
                            End If
                        End If
                    End If
                    Lett.Close()

                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Private Sub caricaImportoResiduoDaTrattenere()
        Dim FlagConnessione As Boolean
        Try
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            Dim stringaPagamenti As String = ""
            For Each elemento In lstListaRapporti
                If stringaPagamenti = "" Then
                    stringaPagamenti = elemento.STR
                Else
                    stringaPagamenti &= "," & elemento.STR
                End If
            Next
            Dim importoTotale As Decimal = 0
            If IsNothing(sValoreAppalto) Then
                par.cmd.CommandText = "SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID=" & vIdPagamento
                sValoreAppalto = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            'par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & ")"
            Dim condizioneVoce As String = ""
            par.cmd.CommandText = "SELECT TIPO,ID_PF_VOCE_IMPORTO AS VOCE FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(Select ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & ")"
            Dim lettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreVoce.Read Then
                tipo.Value = par.IfNull(lettoreVoce("TIPO"), 0)
                voce.Value = par.IfNull(lettoreVoce("VOCE"), 0)
            End If
            lettoreVoce.Close()
            If tipo.Value = 1 Then
                condizioneVoce = " AND ID_PF_VOCE_IMPORTO in " _
                    & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce.Value & ") " _
                    & " UNION " _
                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce.Value & ")" _
                    & ")"
            End If
            par.cmd.CommandText = "Select SUM(IMPONIBILE) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(Select ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & ") " & condizioneVoce
            importoTotale = par.IfNull(par.cmd.ExecuteScalar, 0)
            Dim anticipoTrattenuto As Decimal = 0
            If stringaPagamenti <> "" Then
                par.cmd.CommandText = "Select SUM(ANTICIPO_CONTRATTUALE) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO In (Select ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO=(Select ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & "))"
                anticipoTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            Dim numeroRate As Integer = 0
            par.cmd.CommandText = "Select N_RATE_ANTICIPO,FL_ANTICIPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                numeroRate = par.IfNull(lettore("N_RATE_ANTICIPO"), 0)
                tipoAnticipo.Value = par.IfNull(lettore("FL_ANTICIPO"), 0)
            End If
            lettore.Close()
            ImportoResiduoDaTrattenere.Value = importoTotale - anticipoTrattenuto
            Dim importoRata As Decimal = 0
            If numeroRate <> 0 Then
                importoRata = Math.Round(importoTotale / numeroRate, 2)
            End If
            importoDaProporre.Value = Math.Min(CDec(ImportoResiduoDaTrattenere.Value), importoRata)
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Sub AbilitaDisabilita()

        Select Case txtSTATO.Value
            Case 0  'NON FIRMATO
                Me.cmbStato.Enabled = True
                Me.btnSalva.Visible = True
                Me.btnAnnulla.Visible = True
                Me.btnStampaSAL.Visible = True
                Me.btnStampa.Visible = True
                btnRielbSal.Visible = True
                btnRielaboraPagamento.Visible = True

            Case 1  'FIRMATO CON RISERVA
                Me.cmbStato.Enabled = True
                Me.btnSalva.Visible = True
                Me.btnAnnulla.Visible = True
                Me.btnStampaSAL.Visible = True
                Me.btnStampa.Visible = True
                btnRielbSal.Visible = True
                btnRielaboraPagamento.Visible = True

            Case 2  'FIRMATO
                Me.cmbStato.Enabled = True
                Me.btnSalva.Visible = True
                Me.btnAnnulla.Visible = True
                Me.btnStampaSAL.Visible = True
                Me.btnStampa.Visible = True
                btnRielbSal.Visible = True
                btnRielaboraPagamento.Visible = True

        End Select


        If vIdPagamento = 0 Then
            Me.btnAnnulla.Visible = False
            Me.btnStampaSAL.Visible = False
            Me.btnStampa.Visible = False
            btnRielbSal.Visible = False
            btnRielaboraPagamento.Visible = False
        End If


        'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.FL_CC=0"
            Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lett.Read Then
                'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                If par.IfNull(Lett(0), 0) >= 6 Then
                    btnAnnulla.Visible = False
                    If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                        btnAnnulla.Visible = True
                    End If
                End If
            End If
            Lett.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Else

            par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
               & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
               & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.FL_CC=0"
            Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lett.Read Then
                'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                If par.IfNull(Lett(0), 0) >= 6 Then
                    btnAnnulla.Visible = False
                    If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_ANNULLA_SAL") = 1 Then
                        btnAnnulla.Visible = True
                    End If
                End If
            End If
            Lett.Close()

        End If

        If Trasmesso.Value = 1 Then
            SOLO_LETTURA.Text = 1
            FrmSolaLettura(False)
            bloccato.Value = 1
        End If
        If solaLetturaImportoMinoreZero = True Then
            btnSalva.Enabled = False
            btnAnnulla.Enabled = False
        End If
    End Sub



    Sub CalcolaImporti(ByVal importo As Decimal, ByVal IVA_CONSUMO As Decimal, ByVal rimborso As Decimal, ByVal penale As Decimal, ByVal Id_Voce As Decimal, ByVal Id_Appalto As Decimal, ByVal Oneri_Inseriti As Decimal)
        Dim sStr1 As String
        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenuta, ritenutaIVATA, risultato5 As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            sStr1 = "select APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CON,APPALTI.FL_RIT_LEGGE " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & Id_Appalto _
                & "  and   APPALTI.ID=" & Id_Appalto

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader2 = par.cmd.ExecuteReader

            If myReader2.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                perc_oneri = par.IfNull(myReader2("PERC_ONERI_SIC_CON"), 0)

                perc_sconto = par.IfNull(myReader2("SCONTO_CONSUMO"), 0)
                perc_iva = IVA_CONSUMO 'par.IfNull(myReader2("IVA_CONSUMO"), 0) '************


                importoT = importoT + importo
                penaleT = penaleT + penale

                If Oneri_Inseriti = -1 Then
                    'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                    oneri = importo - ((importo * 100) / (100 + perc_oneri))
                Else
                    oneri = Oneri_Inseriti
                End If
                oneri = Round(oneri, 2)
                oneriT = oneriT + oneri


                'LORDO senza ONERI= IMPORTO-oneri
                risultato1 = importo - oneri
                risultato1T = risultato1T + risultato1

                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                asta = (risultato1 * perc_sconto) / 100
                asta = Round(asta, 2)
                astaT = astaT + asta

                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                risultato2 = risultato1 - asta '- penale
                risultato2T = risultato2T + risultato2


                'AGGIUNTO
                'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato3 = risultato2 + oneri

                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                    ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                    ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                Else
                    ritenuta = 0
                    ritenutaIVATA = 0
                End If
                ritenutaT = ritenutaT + ritenutaIVATA
                ritenutaNoIvaT = ritenutaNoIvaT + ritenuta

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                'risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri (prima del 13/07/2011 vedi mail)
                ' ora la ritenuta viene sottratta all'imponibile

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri
                risultato3T = risultato3T + risultato3

                risultatoImponibileT = risultatoImponibileT + risultato3 - ritenuta

                'IVA= (NETTO con oneri*perc_iva)/100
                iva = Math.Round((risultato5 * perc_iva) / 100.0, 2)
                ivaT = ivaT + iva

                'NETTO+ONERI+IVA
                risultato4 = risultato5 + iva + Round(rimborso, 2)
                risultato4T = risultato4T + risultato4

                rimborsoT = rimborsoT + Round(rimborso, 2)


            End If
            myReader2.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub CalcolaImporti2(ByVal importo As Decimal, ByVal IVA_CONSUMO As Decimal, ByVal rimborso As Decimal, ByVal penale As Decimal, ByVal Id_Voce As Decimal, ByVal Id_Appalto As Decimal, ByVal Oneri_Inseriti As Decimal)
        Dim sStr1 As String
        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenuta, ritenutaIVATA, risultato5, risultato5Liquidazione, ivaLiquidazione As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            sStr1 = "select APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CON,APPALTI.FL_RIT_LEGGE " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & Id_Appalto _
                & "  and   APPALTI.ID=" & Id_Appalto

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader2 = par.cmd.ExecuteReader

            If myReader2.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                perc_oneri = par.IfNull(myReader2("PERC_ONERI_SIC_CON"), 0)

                perc_sconto = par.IfNull(myReader2("SCONTO_CONSUMO"), 0)
                perc_iva = IVA_CONSUMO 'par.IfNull(myReader2("IVA_CONSUMO"), 0) '************


                importoT = importoT + importo
                penaleT = penaleT + penale

                If Oneri_Inseriti = -1 Then
                    'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                    oneri = importo - ((importo * 100) / (100 + perc_oneri))
                Else
                    oneri = Oneri_Inseriti
                End If
                oneri = Round(oneri, 2)
                oneriT = oneriT + oneri


                'LORDO senza ONERI= IMPORTO-oneri
                risultato1 = importo - oneri
                risultato1T = risultato1T + risultato1

                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                asta = (risultato1 * perc_sconto) / 100
                asta = Round(asta, 2)
                astaT = astaT + asta

                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                risultato2 = risultato1 - asta '- penale
                risultato2T = risultato2T + risultato2


                'AGGIUNTO
                'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato3 = risultato2 + oneri

                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                    ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                    ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                Else
                    ritenuta = 0
                    ritenutaIVATA = 0
                End If
                ritenutaT = ritenutaT + ritenutaIVATA


                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                'risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri (prima del 13/07/2011 vedi mail)
                ' ora la ritenuta viene sottratta all'imponibile

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato5 = risultato3 '- ritenuta 'risultato2 - ritenuta + oneri
                risultato3T = risultato3T + risultato3

                risultatoImponibileT = risultatoImponibileT + risultato3 - ritenuta

                'IVA= (NETTO con oneri*perc_iva)/100
                iva = Math.Round((risultato5 * perc_iva) / 100.0, 2)
                ivaT = ivaT + iva

                '***Controllo IVA liquidazione***'
                risultato5Liquidazione = risultato3 - ritenuta 'risultato2 - ritenuta + oneri
                ivaLiquidazione = Math.Round((risultato5Liquidazione * perc_iva) / 100.0, 2)
                ivaLiquidazioneT = ivaLiquidazioneT + ivaLiquidazione
                '********************************'
                'NETTO+ONERI+IVA
                risultato4 = risultato5 + iva + Round(rimborso, 2)
                risultato4T = risultato4T + risultato4

                rimborsoT = rimborsoT + Round(rimborso, 2)


            End If
            myReader2.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub CalcolaImportiProgress(ByVal importo As Decimal, ByVal IVA_CONSUMO As Decimal, ByVal rimborso As Decimal, ByVal penale As Decimal, ByVal Id_Voce As Decimal, ByVal Id_Appalto As Decimal, ByVal Oneri_Inseriti As Decimal)
        Dim sStr1 As String
        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenuta, ritenutaIVATA, risultato5 As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CON,APPALTI.FL_RIT_LEGGE " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & Id_Appalto _
                & "  and   APPALTI.ID=" & Id_Appalto

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader2 = par.cmd.ExecuteReader

            If myReader2.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                perc_oneri = par.IfNull(myReader2("PERC_ONERI_SIC_CON"), 0)

                perc_sconto = par.IfNull(myReader2("SCONTO_CONSUMO"), 0)
                perc_iva = IVA_CONSUMO 'par.IfNull(myReader2("IVA_CONSUMO"), 0) '************


                importoP = importoP + importo
                penaleP = penaleP + Round(penale, 2)

                If Oneri_Inseriti = -1 Then
                    'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                    oneri = importo - ((importo * 100) / (100 + perc_oneri))
                Else
                    oneri = Oneri_Inseriti
                End If
                oneri = Round(oneri, 2)
                oneriP = oneriP + oneri


                'LORDO senza ONERI= IMPORTO-oneri
                risultato1 = importo - oneri
                risultato1P = risultato1P + risultato1

                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                asta = (risultato1 * perc_sconto) / 100
                asta = Round(asta, 2)
                astaP = astaP + asta

                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                risultato2 = risultato1 - asta '- penale
                risultato2P = risultato2P + risultato2

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato3 = risultato2 + oneri

                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                    ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                    'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                Else
                    ritenuta = 0
                    ritenutaIVATA = 0
                End If
                ritenutaP = ritenutaP + ritenutaIVATA

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                'risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri (prima del 13/07/2011 vedi mail)
                ' ora la ritenuta viene sottratta all'imponibile
                risultato5 = risultato3 - ritenuta 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                risultato3P = risultato3P + risultato3

                risultatoImponibileP = risultatoImponibileP + risultato3 - ritenuta


                'IVA= (NETTO con oneri*perc_iva)/100
                iva = Math.Round((risultato5 * perc_iva) / 100, 2)
                ivaP = ivaP + iva

                'NETTO+ONERI+IVA
                risultato4 = risultato5 + iva + Round(rimborso, 2)
                risultato4P = risultato4P + risultato4

                rimborsoP = rimborsoP + Round(rimborso, 2)


            End If
            myReader2.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub CalcolaImportiProgress2(ByVal importo As Decimal, ByVal IVA_CONSUMO As Decimal, ByVal rimborso As Decimal, ByVal penale As Decimal, ByVal Id_Voce As Decimal, ByVal Id_Appalto As Decimal, ByVal Oneri_Inseriti As Decimal)
        Dim sStr1 As String
        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenuta, ritenutaIVATA, risultato5 As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CON,APPALTI.FL_RIT_LEGGE " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & Id_Appalto _
                & "  and   APPALTI.ID=" & Id_Appalto

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader2 = par.cmd.ExecuteReader

            If myReader2.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                perc_oneri = par.IfNull(myReader2("PERC_ONERI_SIC_CON"), 0)

                perc_sconto = par.IfNull(myReader2("SCONTO_CONSUMO"), 0)
                perc_iva = IVA_CONSUMO 'par.IfNull(myReader2("IVA_CONSUMO"), 0) '************


                importoP = importoP + importo
                penaleP = penaleP + Round(penale, 2)

                If Oneri_Inseriti = -1 Then
                    'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                    oneri = importo - ((importo * 100) / (100 + perc_oneri))
                Else
                    oneri = Oneri_Inseriti
                End If
                oneri = Round(oneri, 2)
                oneriP = oneriP + oneri


                'LORDO senza ONERI= IMPORTO-oneri
                risultato1 = importo - oneri
                risultato1P = risultato1P + risultato1

                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                asta = (risultato1 * perc_sconto) / 100
                asta = Round(asta, 2)
                astaP = astaP + asta

                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                risultato2 = risultato1 - asta '- penale
                risultato2P = risultato2P + risultato2

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                risultato3 = risultato2 + oneri

                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                    ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                    'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                Else
                    ritenuta = 0
                    ritenutaIVATA = 0
                End If
                ritenutaP = ritenutaP + ritenutaIVATA

                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                'risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri (prima del 13/07/2011 vedi mail)
                ' ora la ritenuta viene sottratta all'imponibile
                risultato5 = risultato3 '- ritenuta NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                risultato3P = risultato3P + risultato3

                risultatoImponibileP = risultatoImponibileP + risultato3 - ritenuta


                'IVA= (NETTO con oneri*perc_iva)/100
                iva = Math.Round((risultato5 * perc_iva) / 100, 2)
                ivaP = ivaP + iva

                'NETTO+ONERI+IVA
                risultato4 = risultato5 + iva + Round(rimborso, 2)
                risultato4P = risultato4P + risultato4

                rimborsoP = rimborsoP + Round(rimborso, 2)


            End If
            myReader2.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub CalcolaImportiProgressCANONE(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String)
        Dim sStr1 As String
        Dim perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, ritenuta_IVATA, risultato2, risultato3, risultato4 As Decimal
        Dim perc_oneri, importoDaPagare As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.* " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                & "  where ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   ID_APPALTO=" & Id_Appalto

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReaderT = par.cmd.ExecuteReader

            If myReaderT.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                'perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

                'D3= D1-(D1*D2/100)
                'D9= D4*100/D3
                Dim D3 As Decimal = 0
                D3 = par.IfNull(myReaderT("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderT("IMPORTO_CANONE"), 0) * par.IfNull(myReaderT("SCONTO_CANONE"), 0)) / 100)

                perc_oneri = (par.IfNull(myReaderT("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3

                perc_sconto = par.IfNull(myReaderT("SCONTO_CANONE"), 0)
                perc_iva = par.IfNull(myReaderT("IVA_CANONE"), 0)
            End If
            myReaderT.Close()


            risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
            risultato3 = Round(risultato3, 2)
            risultato3P = risultato3P + risultato3


            'ALIQUOTA 0,5% 
            If par.IfNull(fl_ritenuta, 0) = 1 Then

                ritenuta = (risultato3 * 0.5) / 100
                ritenuta = Round(ritenuta, 2)
                ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                ritenuta_IVATA = Round(ritenuta_IVATA, 2)
            Else
                ritenuta = 0
            End If
            ritenutaP = ritenutaP + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok

            'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
            risultatoImponibileP = risultatoImponibileP + risultato3 - ritenuta


            oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))    'Oneri di Sicurezza (txtOneri)
            oneri = Round(oneri, 2)
            oneriP = oneriP + oneri

            risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
            risultato2P = risultato2P + risultato2

            asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
            asta = Round(asta, 2)
            astaP = astaP + asta

            risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
            risultato1P = risultato1P + risultato1

            risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
            importoP = importoP + risultato4


            '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
            'importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
            importoDaPagare = risultato3 + (((risultato3) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
            importoDaPagare = Round(importoDaPagare, 2)

            risultato4P = risultato4P + importoDaPagare


            iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
            iva = Round(iva, 2)
            ivaP = ivaP + iva


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub BindGridManutenzioni(ByVal sCondizione As String, ByVal ID_APPALTO As Long, ByVal PROGR_APPALTO As String)
        Dim dt As New Data.DataTable
        Dim sStringaSql As String
        Dim sOrder As String = ""

        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4 As Decimal
        Dim risultato4Tot As Decimal

        Try

            sStringaSql = " select  MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                & " (MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) as ""ODL_ANNO""," _
                                & " to_char(to_date(substr(MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                & " to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_FINE_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_FINE_ORDINE," _
                                & " COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                & " TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                                & " MANUTENZIONI.PROGR,MANUTENZIONI.ANNO,MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO,MANUTENZIONI.ID_PF_VOCE_IMPORTO,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO,MANUTENZIONI.RIMBORSI,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE"" ,siscom_mi.GETOGGETTOMANUTENZIONE(MANUTENZIONI.ID) AS PATRIMONIO " _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.APPALTI_PENALI " _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO in (2,4) " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                        & "   and   SISCOM_MI.MANUTENZIONI." & sCondizione


            sStringaSql = sStringaSql & " union  select  SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO""," _
                                    & " to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                    & " to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_FINE_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_FINE_ORDINE," _
                                    & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                    & " TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                                    & "MANUTENZIONI.PROGR,MANUTENZIONI.ANNO,MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO,MANUTENZIONI.ID_PF_VOCE_IMPORTO,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO,MANUTENZIONI.RIMBORSI,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE"" ,siscom_mi.GETOGGETTOMANUTENZIONE(MANUTENZIONI.ID) AS PATRIMONIO " _
                          & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.APPALTI_PENALI " _
                          & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.STATO in (2,4) " _
                          & "   and   SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                          & "   and   SISCOM_MI.MANUTENZIONI." & sCondizione


            par.cmd.CommandText = sStringaSql & sOrder

            Dim ds1 As New Data.DataSet()

            dt.Columns.Add("ID_MANUTENZIONE")
            dt.Columns.Add("ODL_ANNO")
            dt.Columns.Add("PROGR_APPALTO")
            dt.Columns.Add("DATA_INIZIO_ORDINE")
            dt.Columns.Add("DATA_FINE_ORDINE")

            dt.Columns.Add("UBICAZIONE")

            dt.Columns.Add("IMP_NETTO_ONERI")
            dt.Columns.Add("IVA")
            dt.Columns.Add("RIMBORSI")

            dt.Columns.Add("IMPORTO_CONSUNTIVATO")
            dt.Columns.Add("IMP_NETTO_ONERI_IVA")
            dt.Columns.Add("PENALE")
            dt.Columns.Add("PATRIMONIO")

            dt.Columns.Add("STATO")
            dt.Columns.Add("PROGR")
            dt.Columns.Add("ANNO")
            dt.Columns.Add("ID_PRENOTAZIONE_PAGAMENTO")

            Dim RIGA As System.Data.DataRow

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            myReader2 = par.cmd.ExecuteReader()

            While myReader2.Read()

                'If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                    solaLetturaImportoMinoreZero = True
                End If

                    sStringaSql = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                                    & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                                    & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0) _
                                    & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & ID_APPALTO _
                                    & "  and   APPALTI.ID=" & ID_APPALTO

                    Dim myReaderT2 As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = sStringaSql
                    myReaderT2 = par.cmd.ExecuteReader

                    If myReaderT2.Read Then

                        perc_oneri = par.IfNull(myReaderT2("PERC_ONERI_SIC_CON"), 0)

                        perc_sconto = par.IfNull(myReaderT2("SCONTO_CONSUMO"), 0)
                        perc_iva = par.IfNull(myReader2("IVA_CONSUMO"), 0)



                        If par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0) = -1 Then

                            'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                            oneri = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) - ((par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) * 100) / (100 + perc_oneri))
                        Else
                            oneri = par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0)
                        End If

                        'LORDO senza ONERI= IMPORTO-oneri
                        risultato1 = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) - oneri

                        'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                        asta = (risultato1 * perc_sconto) / 100

                        'NETTO senza ONERI= (LORDO senza oneri-asta) 
                        risultato2 = risultato1 - asta '- penale

                        'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                        risultato3 = risultato2 + oneri

                        'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                        If par.IfNull(myReaderT2("FL_RIT_LEGGE"), 0) = 1 Then
                            ritenuta = (risultato3 * 0.5) / 100
                        Else
                            ritenuta = 0
                        End If


                        'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                        'risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri
                        risultato3 = risultato3 '- ritenuta NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%

                        'IVA= (NETTO con oneri*perc_iva)/100
                        iva = Math.Round((risultato3 * perc_iva) / 100, 2)

                        'NETTO+ONERI+IVA
                        risultato4 = risultato3 + iva + Round(par.IfNull(myReader2("RIMBORSI"), 0), 2)
                        risultato4Tot = risultato4Tot + risultato4


                        RIGA = dt.NewRow()
                        RIGA.Item("ID_MANUTENZIONE") = par.IfNull(myReader2("ID_MANUTENZIONE"), " ")
                        RIGA.Item("ODL_ANNO") = par.IfNull(myReader2("ODL_ANNO"), " ")
                        RIGA.Item("PROGR_APPALTO") = PROGR_APPALTO
                        RIGA.Item("DATA_INIZIO_ORDINE") = par.IfNull(myReader2("DATA_INIZIO_ORDINE"), " ")
                        RIGA.Item("DATA_FINE_ORDINE") = par.IfNull(myReader2("DATA_FINE_ORDINE"), " ")

                        RIGA.Item("PATRIMONIO") = par.IfNull(myReader2("PATRIMONIO"), " ")
                        RIGA.Item("IMPORTO_CONSUNTIVATO") = Format(par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")

                        RIGA.Item("UBICAZIONE") = par.IfNull(myReader2("UBICAZIONE"), " ")

                        RIGA.Item("IMP_NETTO_ONERI") = Format(par.IfNull(risultato3, 0), "##,##0.00")
                        RIGA.Item("IVA") = Format(par.IfNull(iva, 0), "##,##0.00")
                        RIGA.Item("RIMBORSI") = Format(par.IfNull(myReader2("RIMBORSI"), 0), "##,##0.00")
                        RIGA.Item("IMP_NETTO_ONERI_IVA") = Format(par.IfNull(risultato4, 0), "##,##0.00")
                        RIGA.Item("PENALE") = Format(par.IfNull(myReader2("PENALE"), 0), "##,##0.00")

                        RIGA.Item("STATO") = par.IfNull(myReader2("STATO"), " ")
                        RIGA.Item("PROGR") = par.IfNull(myReader2("PROGR"), " ")
                        RIGA.Item("ANNO") = par.IfNull(myReader2("ANNO"), " ")
                        RIGA.Item("ID_PRENOTAZIONE_PAGAMENTO") = par.IfNull(myReader2("ID_PRENOTAZIONE_PAGAMENTO"), " ")

                        dt.Rows.Add(RIGA)
                    End If
                    myReaderT2.Close()
                'End If

            End While
            myReader2.Close()


            'Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'da1.Fill(ds1, "MANUTENZIONI")

            CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).DataSource = dt
            Session.Add("dtDettagli", dt)
            CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).DataBind()
            'ds1.Dispose()


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            ''Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub PdfSal(ByVal ID_Pagamento As Double)
        Dim sStr1 As String
        Dim sDescrizione As String

        Dim perc_oneri, ritenuta, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenutaIVATA As Decimal
        Dim risultato4Tot As Decimal
        Dim FlagConnessione As Boolean

        Dim riga As Integer

        Try

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloSAL.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            '$SAL=                  PAGAMENTI.PROGR_APPALTO
            '$DATA_SAL=             PAGAMENTI.DATA_EMISSIONE
            '$data_emissione=      

            '$REPERTORIO=           APPALTI.NUM_REPERTORIO DEL APPALTI.DATA_REPERTORIO APPALTI.DESCRIZIONE
            '$DATA_REPERTORIO=      APPALTI.DATA_REPERTORIO APPALTI.DESCRIZIONE
            '$DESC_REPERTORIO=      APPALTI.DESCRIZIONE

            '$FORNITORE             FORNITORI.RAGIONE_SOCIALE,FORNITORI.COGNOME,FORNITORI.NOME " _
            '$FORNITORI_INDIRIZZI$  FORNITORI_INDIRIZZI.TIPO FORNITORI_INDIRIZZI.INDIRIZZ FORNITORI_INDIRIZZI.CIVICO FORNITORI_INDIRIZZI.CAP  ||' - '|| FORNITORI_INDIRIZZI.COMUNE


            '$dettagli= 
            '$imp_letterale=        contenuto = Replace(contenuto, "$imp_letterale$", NumeroInLettere(par.IfNull(myReader1("PAGAMENTI.IMPORTO_PRENOTATO"), 0)))

            '$data_stampa=          contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("PAGAMENTI.DATA_STAMPA")))
            '$chiamante=            contenuto = Replace(contenuto, "$chiamante$", "CONTRATTO")


            '$cod_capitolo=         PF_VOCI.CODICE
            '$voce_pf=              PF_VOCI.DESCRIZIONE
            '$finanziamento=        contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")
            '$dettaglio=            ???
            '$TOT=                  contenuto = Replace(contenuto, "$TOT$", par.IfNull(myReader1("PAGAMENTI.IMPORTO_PRENOTATO"), 0))

            '& " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"" " 

            sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR_APPALTO,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_SAL,PAGAMENTI.DATA_STAMPA,PAGAMENTI.IMPORTO_CONSUNTIVATO," _
                        & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.DATA_INIZIO,APPALTI.DATA_FINE,APPALTI.DURATA_MESI," _
                        & " FORNITORI.ID as ""ID_FORNITORE"",FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE" _
                 & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                 & " where   PAGAMENTI.ID=" & ID_Pagamento _
                     & " and PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                     & " and PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "
            '& " and PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO (+) "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then

                contenuto = Replace(contenuto, "$REPERTORIO$", myReader1("NUM_REPERTORIO"))
                contenuto = Replace(contenuto, "$DATA_REPERTORIO$", par.FormattaData(myReader1("DATA_REPERTORIO")))
                contenuto = Replace(contenuto, "$DESC_REPERTORIO$", myReader1("APPALTI_DESC"))


                'DATI FORNITORE
                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    End If
                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    End If
                End If


                'INDIRIZZO FORNITORE
                Dim sIndirizzoFornitore1 As String = ""

                par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReader1("ID_FORNITORE"), 0)

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                myReaderT = par.cmd.ExecuteReader
                While myReaderT.Read

                    sIndirizzoFornitore1 = par.IfNull(myReaderT("TIPO"), "") _
                                        & " " & par.IfNull(myReaderT("INDIRIZZO"), "") _
                                        & " " & par.IfNull(myReaderT("CIVICO"), "") _
                                        & " - " & par.IfNull(myReaderT("CAP"), "") _
                                        & " " & par.IfNull(myReaderT("COMUNE"), "")

                End While
                myReaderT.Close()
                contenuto = Replace(contenuto, "$fornitore_indirizzo$", sIndirizzoFornitore1)
                '**********************************************


                contenuto = Replace(contenuto, "$SAL$", myReader1("PROGR_APPALTO"))
                contenuto = Replace(contenuto, "$DATA_SAL$", par.FormattaData(par.IfNull(myReader1("DATA_SAL"), "")))
                contenuto = Replace(contenuto, "$DATA_DEL$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "")))

                If par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")) <> "" Then
                    contenuto = Replace(contenuto, "$data_stampa$", "Milano, li " & par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))
                Else
                    contenuto = Replace(contenuto, "$data_stampa$", "")
                End If


                '*****************Modifica Peppe 06/08/2010, $TOT$ deve essere uguale a riultato4, lo sposto dopo il calcolo dello stesso*******
                'contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "", "##,##0.00"))

                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO

                'sStr1 = "select * from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '    & " where ID_PF_VOCE_IMPORTO=" & par.IfNull(myReader1("ID_VOCI"), "Null") _
                '    & " and   ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), "Null")

                'Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                'par.cmd.CommandText = sStr1
                'myReaderT = par.cmd.ExecuteReader

                'If myReaderT.Read Then
                '    Dim importo, perc_oneri, perc_sconto, perc_iva As Double
                '    Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4 As Double

                'importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                'perc_oneri = par.IfNull(myReader1("PERC_ONERI_SIC_CON"), 0)

                'perc_sconto = par.IfNull(myReaderT("SCONTO_CONSUMO"), 0)
                'perc_iva = par.IfNull(myReaderT("IVA_CONSUMO"), 0)


                ''ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                'oneri = importo - ((importo * 100) / (100 + perc_oneri))

                ''LORDO senza ONERI= IMPORTO-oneri
                'risultato1 = importo - oneri

                ''RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                'asta = (risultato1 * perc_sconto) / 100

                ''NETTO senza ONERI= (LORDO senza oneri-asta)
                'risultato2 = risultato1 - asta

                ''NETTO con ONERI= (IMPORTO-asta)
                'risultato3 = importo - asta

                ''IVA= (NETTO con oneri*perc_iva)/100
                'iva = (risultato3 * perc_iva) / 100

                ''NETTO+ONERI+IVA
                'risultato4 = risultato3 + iva


                'IMPORTI PROGRESSIVI
                Dim FiltraStoricoSAL As String = ""
                If vIdPagamento > 0 Then
                    FiltraStoricoSAL = " and ID<=" & vIdPagamento
                End If

                importoP = 0
                penaleP = 0
                oneriP = 0
                astaP = 0
                ivaP = 0
                ritenutaP = 0
                rimborsoP = 0
                risultato1P = 0
                risultato2P = 0
                risultato3P = 0
                risultatoImponibileP = 0
                risultato4P = 0

                Dim Somma1 As Decimal = 0
                Dim sRisultato As String = ""

                'RIEPILOGO MANUTENZIONI (IMPORTI A CONSUMO)
                '& "   and ANNO=" & myReader1("ANNO") tolto il 25/12/2010
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select manutenzioni.id, MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                          & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                          & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                   & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                   & " where MANUTENZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                       & " where TIPO_PAGAMENTO=3 and id_stato<>-3" _
                                                                       & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & myReader1("ID_APPALTO") & ")) " & FiltraStoricoSAL & ")" _
                                   & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "

                myReaderT = par.cmd.ExecuteReader
                Dim imponibile_rimborso As Decimal = 0
                Dim iva_rimborso As Decimal = 0

                While myReaderT.Read
                    '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                    ' If par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                    If par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                        solaLetturaImportoMinoreZero = True
                    End If

                        par.cmd.CommandText = "SELECT IMPONIBILE_RIMBORSO, PERC_IVA_RIMBORSO " _
                                            & "FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & "WHERE     ID_MANUTENZIONI_INTERVENTI IN (SELECT ID " _
                                            & "FROM SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & "WHERE ID_MANUTENZIONE = " & par.IfNull(myReaderT("id"), "-1") & ") " _
                                            & "AND COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE' "
                        Dim myReaderRimb As Oracle.DataAccess.Client.OracleDataReader
                        myReaderRimb = par.cmd.ExecuteReader
                        While myReaderRimb.Read
                            imponibile_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0"))
                            iva_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0")) * CDec(par.IfNull(myReaderRimb("PERC_IVA_RIMBORSO"), "0")) / 100
                        End While
                        myReaderRimb.Close()

                        sRisultato = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), "0")
                        Somma1 = Decimal.Parse(sRisultato)

                        CalcolaImportiProgress(Somma1, par.IfNull(myReaderT("IVA_CONSUMO"), 0), par.IfNull(myReaderT("RIMBORSI"), 0), par.IfNull(myReaderT("PENALE2"), 0), par.IfNull(myReaderT("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReaderT("ID_APPALTO"), 0), par.IfNull(myReaderT("IMPORTO_ONERI_CONS"), 0))
                    'Else
                    '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                    '    myReaderT.Close()

                    '    If FlagConnessione = True Then
                    '        par.cmd.Dispose()
                    '        par.OracleConn.Close()
                    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    '        FlagConnessione = False
                    '    End If
                    '    Exit Sub
                    'End If

                End While
                myReaderT.Close()

                'RIEPILOGO PRENOTAZIONI (IMPORTI A CANONE)
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                          & " ID_VOCE_PF_IMPORTO,ID_APPALTO,PRENOTAZIONI.ID ,APPALTI.FL_RIT_LEGGE  " _
                                   & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI" _
                                   & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                       & " where TIPO_PAGAMENTO=6 and id_stato<>-3 " _
                                                                       & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & myReader1("ID_APPALTO") & ")) " & FiltraStoricoSAL & ")" _
                                   & "   and PRENOTAZIONI.ID_APPALTO=APPALTI.ID (+) "
                myReaderT = par.cmd.ExecuteReader

                While myReaderT.Read

                    sRisultato = par.IfNull(myReaderT("IMPORTO_APPROVATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)
                    CalcolaImportiProgressCANONE(Somma1, par.IfNull(myReaderT("FL_RIT_LEGGE"), 0), par.IfNull(myReaderT("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReaderT("ID_APPALTO"), 0), "PRENOTATO")

                End While
                myReaderT.Close()
                '***************************



                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(risultato4P, 0), "", "##,##0.00"))
                sDescrizione = "" 'Pagamento ODL (Vedi Allegato)" '& vbCrLf & vbCrLf

                Dim S2 As String = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & sDescrizione & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(importoP, 0), "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(oneriP, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato1P, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(astaP, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2P, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3P, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenutaP, "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile (al netto trattenute) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultatoImponibileP, "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                '************************************

                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaP, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                If rimborsoP > 0 Then
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(iva_rimborso, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Totale Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(rimborsoP, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                End If

                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4P, "", "##,##0.00") & "</td>"

                If penaleT > 0 Then
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(penaleP, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If
                If par.IfEmpty(CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Text, 0) > 0 Then
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(CType(Tab_SAL_RiepilogoProg.FindControl("txtImportoTrattenuto"), TextBox).Text, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If

                S2 = S2 & "</tr></table>"


                Dim T As String = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$imp_progressivo$", T)


                '************************************************


                'RIEPILOGO SAL
                importoT = 0
                penaleT = 0
                oneriT = 0
                astaT = 0
                ivaT = 0
                ritenutaT = 0
                rimborsoT = 0
                risultato1T = 0
                risultato2T = 0
                risultato3T = 0
                risultato4T = 0


                par.cmd.CommandText = "select manutenzioni.id, MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                          & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                          & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                   & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                   & " where MANUTENZIONI.ID_PAGAMENTO=" & vIdPagamento _
                                   & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


                myReaderT = par.cmd.ExecuteReader()
                imponibile_rimborso = 0
                iva_rimborso = 0

                While myReaderT.Read
                    '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                    'If par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                    If par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                        solaLetturaImportoMinoreZero = True
                    End If

                        par.cmd.CommandText = "SELECT IMPONIBILE_RIMBORSO, PERC_IVA_RIMBORSO " _
                                            & "FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & "WHERE     ID_MANUTENZIONI_INTERVENTI IN (SELECT ID " _
                                            & "FROM SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & "WHERE ID_MANUTENZIONE = " & par.IfNull(myReaderT("id"), "-1") & ") " _
                                            & "AND COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE' "
                        Dim myReaderRimb As Oracle.DataAccess.Client.OracleDataReader
                        myReaderRimb = par.cmd.ExecuteReader
                        While myReaderRimb.Read
                            imponibile_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0"))
                            iva_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0")) * CDec(par.IfNull(myReaderRimb("PERC_IVA_RIMBORSO"), "0")) / 100
                        End While
                        myReaderRimb.Close()

                        CalcolaImporti(par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0), par.IfNull(myReaderT("IVA_CONSUMO"), 0), par.IfNull(myReaderT("RIMBORSI"), 0), par.IfNull(myReaderT("PENALE2"), 0), par.IfNull(myReaderT("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReaderT("ID_APPALTO"), 0), par.IfNull(myReaderT("IMPORTO_ONERI_CONS"), 0))
                    'Else
                    '    RadWindowManager1.RadAlert("Nessun importo stanziato per questo tipo di pagamento!", 300, 150, "Attenzione", "", "null")

                    '    myReaderT.Close()

                    '    If FlagConnessione = True Then
                    '        par.cmd.Dispose()
                    '        par.OracleConn.Close()
                    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    '        FlagConnessione = False
                    '    End If
                    '    Exit Sub
                    'End If

                End While
                myReaderT.Close()


                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(risultato4T, 0), "", "##,##0.00"))
                sDescrizione = "" 'Pagamento ODL (Vedi Allegato)" '& vbCrLf & vbCrLf

                S2 = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & sDescrizione & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(importoT, 0), "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(oneriT, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato1T, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(astaT, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2T, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3T, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenutaT, "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile (al netto trattenute) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultatoImponibileT, "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                '*************************

                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaT, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                If rimborsoT > 0 Then
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(iva_rimborso, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Totale Rimborsi €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(rimborsoT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                End If
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T, "", "##,##0.00") & "</td>"

                If penaleT > 0 Then
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(penaleT, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If
                If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, 0) > 0 Then
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If

                S2 = S2 & "</tr></table>"


                T = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$imp_SAL$", T)
                '*********************************************************

                'DATE SAL

                S2 = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>DATA CONSEGNA LAVORI:</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.FormattaData(par.IfNull(myReader1("DATA_INIZIO"), "")) & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>DURATA CONTRATTUALE:</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DURATA_MESI"), "0") & " giorni</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>SCADENZA CONTRATTUALE:</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.FormattaData(par.IfNull(myReader1("DATA_FINE"), "")) & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "</tr></table>"


                T = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$Date_APPALTI$", T)
                '*********************************************************



                '*********** DETTAGLIO GRIGLIA MANUTENZIONI
                'TestoPagina = TestoPagina & "</table>"
                Dim TestoGrigliaM As String = "<p style='page-break-before: always'>&nbsp;</p>"

                TestoGrigliaM = TestoGrigliaM & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ODL</td>" _
                                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO</td>" _
                                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SAL</td>" _
                                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA ORDINE</td>" _
                                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA CONSUNTIVO</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI)</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IVA</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Tot. RIMBORSI</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI e IVA)</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. PENALE</td>" _
                                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                          & "</tr>"


                par.cmd.CommandText = "select MANUTENZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                              & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                              & " where ID_PAGAMENTO=" & ID_Pagamento _
                                              & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                                              & " order by MANUTENZIONI.PROGR "

                myReaderT = par.cmd.ExecuteReader

                riga = 1
                Dim RigheTotali As Integer
                Dim TotPagine As Integer
                Dim Pagine As Integer = 1

                'Dim j As Integer
                Dim ValTotale As Decimal = 0
                RigheTotali = CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).Items.Count
                TotPagine = Int(RigheTotali / 40)

                While myReaderT.Read

                    'ValTotale = ValTotale + par.IfNull(myReaderT("CONS_LORDO"), 0)
                    '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                    'If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                    If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                        solaLetturaImportoMinoreZero = True
                    End If

                        sStr1 = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                                        & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                                        & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReaderT("ID_PF_VOCE_IMPORTO"), 0) _
                                        & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & par.IfNull(myReaderT("ID_APPALTO"), 0) _
                                        & "  and   APPALTI.ID=" & par.IfNull(myReaderT("ID_APPALTO"), 0)

                        Dim myReaderT2 As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = sStr1
                        myReaderT2 = par.cmd.ExecuteReader

                        If myReaderT2.Read Then

                            perc_oneri = par.IfNull(myReaderT2("PERC_ONERI_SIC_CON"), 0)

                            perc_sconto = par.IfNull(myReaderT2("SCONTO_CONSUMO"), 0)

                            perc_iva = par.IfNull(myReaderT("IVA_CONSUMO"), 0) 'par.IfNull(myReaderT2("IVA_CONSUMO"), 0)


                            'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                            If par.IfNull(myReaderT("IMPORTO_ONERI_CONS"), 0) = -1 Then
                                oneri = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) - ((par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) * 100) / (100 + perc_oneri))
                            Else
                                oneri = par.IfNull(myReaderT("IMPORTO_ONERI_CONS"), 0)
                            End If
                            oneri = Round(oneri, 2)
                            'LORDO senza ONERI= IMPORTO-oneri
                            risultato1 = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) - oneri

                            'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                            asta = (risultato1 * perc_sconto) / 100
                            asta = Round(asta, 2)

                            'NETTO senza ONERI= (LORDO senza oneri-asta) 
                            risultato2 = risultato1 - asta '- penale

                            'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                            risultato3 = risultato2 + oneri

                            'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                            If par.IfNull(myReaderT2("FL_RIT_LEGGE"), 0) = 1 Then
                                ritenuta = (risultato3 * 0.5) / 100
                                ritenuta = Round(ritenuta, 2)
                                'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                                ritenutaIVATA = Math.Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                            Else
                                ritenuta = 0
                                ritenutaIVATA = 0
                            End If


                            'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                            risultato3 = risultato3 '- ritenuta NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%

                            'IVA= (NETTO con oneri*perc_iva)/100
                            iva = Math.Round((risultato3 * perc_iva) / 100, 2)

                            'NETTO+ONERI+IVA
                            risultato4 = risultato3 + iva + Round(CDec(par.IfNull(myReaderT("RIMBORSI"), 0)), 2)
                            risultato4Tot = risultato4Tot + risultato4


                            If riga >= 40 Then

                                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PROGR"), "") & "</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("ANNO"), "") & "</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("PROGR_APPALTO"), "") & "</td>" _
                                                      & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_INIZIO_ORDINE"), "")) & "</td>" _
                                                      & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_FINE_ORDINE"), "")) & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato3, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(iva, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("RIMBORSI"), 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato4, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("PENALE2"), 0), "##,##0.00") & "</td>" _
                                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                                      & "</tr>"

                                riga = 1
                            Else

                                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                    & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PROGR"), "") & "</td>" _
                                                    & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("ANNO"), "") & "</td>" _
                                                    & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("PROGR_APPALTO"), "") & "</td>" _
                                                    & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_INIZIO_ORDINE"), "")) & "</td>" _
                                                    & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_FINE_ORDINE"), "")) & "</td>" _
                                                    & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato3, 0), "##,##0.00") & "</td>" _
                                                    & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(iva, 0), "##,##0.00") & "</td>" _
                                                    & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("RIMBORSI"), 0), "##,##0.00") & "</td>" _
                                                    & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato4, 0), "##,##0.00") & "</td>" _
                                                    & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("PENALE2"), 0), "##,##0.00") & "</td>" _
                                                    & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                                    & "</tr>"
                                riga = riga + 1
                            End If

                            If riga = 1 Then
                                If Pagine < TotPagine Then

                                    TestoGrigliaM = TestoGrigliaM & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
                                    TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ODL</td>" _
                                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO</td>" _
                                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SAL</td>" _
                                                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA ORDINE</td>" _
                                                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA CONSUNTIVO</td>" _
                                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI)</td>" _
                                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IVA</td>" _
                                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Tot. RIMBORSI</td>" _
                                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI e IVA)</td>" _
                                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. PENALE</td>" _
                                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                                              & "</tr>"
                                    TestoGrigliaM = TestoGrigliaM & "<p style='page-break-before : always'>&nbsp;</p>"
                                    Pagine = Pagine + 1
                                End If

                            End If

                        End If
                        myReaderT2.Close()

                    'End If

                    'If riga > 90 Then

                    '    TestoGrigliaM = TestoGrigliaM & "<p style='page-break-before: always'>&nbsp;</p>"

                    '    TestoGrigliaM = TestoGrigliaM & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
                    '    TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                    '                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ODL</td>" _
                    '                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO</td>" _
                    '                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SAL</td>" _
                    '                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA ORDINE</td>" _
                    '                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA CONSUNTIVO</td>" _
                    '                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI)</td>" _
                    '                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IVA</td>" _
                    '                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Tot. RIMBORSI</td>" _
                    '                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI e IVA)</td>" _
                    '                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. PENALE</td>" _
                    '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                    '                              & "</tr>"


                    '    riga = 1
                    'End If

                End While
                myReaderT.Close()


                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & IsNumFormat(risultato4Tot, "", "##,##0.00") & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                          & "</tr>"

                contenuto = Replace(contenuto, "$grigliaM$", TestoGrigliaM)

                '********************************


                'End If
                'myReaderT.Close()

                '*****************FINE SCRITTURA DETTAGLI
                'contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))

                'contenuto = Replace(contenuto, "$dettaglio$", "MANUTENZIONE")

                'contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("COD_VOCE"), ""))
                'contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESC_VOCE"), ""))
                'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")


            End If
            myReader1.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            '************************

            ''****Scrittura evento STAMPA DEL PAGAMENTO
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F90','Stampa SAL')"
            par.cmd.ExecuteNonQuery()
            '****************************************************

            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""


            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""

            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True


            Dim nomefile As String = "AttSAL_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            nomefile = par.NomeFileManut("SAL", vIdPagamento) & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

            Dim i As Integer = 0
            For i = 0 To 10000
            Next
            'GIANCARLO 16-02-2017
            'inserimento della stampa cdp negli allegati
            Dim descrizione As String = "Stampa SAL"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA")
            par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                              & "SET STATO = 2 " _
                              & "WHERE " _
                              & "TIPO = " & idTipoOggetto _
                              & "AND ID_OGGETTO = " & vIdPagamento
            par.cmd.ExecuteNonQuery()
            If HiddenFieldRielabPagam.Value = "1" Then
                descrizione = "Stampa rielaborazione SAL"
                idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE SAL DI SISTEMA")
                par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                       & "SET STATO = 2 " _
                       & "WHERE " _
                       & "TIPO = " & idTipoOggetto _
                       & "AND ID_OGGETTO = " & vIdPagamento
                par.cmd.ExecuteNonQuery()
            End If
            par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
            Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
            'Imposto le vecchie rielaborazioni a 2...per barrare il nome

            par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, vIdPagamento, "../../../ALLEGATI/SAL/")


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();", True)


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI) e non SONO in SOLO LETTURA
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text <> "1" Then
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                myReader1.Close()

                Session.Add("LAVORAZIONE", "1")
                TabberHide = "tabbertab"

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtModificato.Text = "0"
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub btnStampaSAL_Click(sender As Object, e As System.EventArgs) Handles btnStampaSAL.Click
        Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE SAL DI SISTEMA")

        '*******************APERURA CONNESSIONE*********************
        ' RIPRENDO LA CONNESSIONE
        HiddenFieldRielabPagam.Value = "0"
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
        Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
        If String.IsNullOrEmpty(nome) Then

            PdfSal(vIdPagamento)
        Else

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../ALLEGATI/SAL/" & nome & "','SAL','');self.close();", True)
        End If

    End Sub


    Function ControllaSALsuccessivi() As String
        Dim FlagConnessione As Boolean

        ControllaSALsuccessivi = ""

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'CONTROLLO SE SONO STATI FATTI ALTRI SAL
            par.cmd.CommandText = "select PROGR_APPALTO from SISCOM_MI.PAGAMENTI " _
                            & " where TIPO_PAGAMENTO=3 " _
                            & "   and ID_APPALTO in ( (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =(select distinct(ID_APPALTO) from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamento & " ))))" _
                            & "    and PROGR_APPALTO>" & Val(Me.txtPAGAMENTI_PROGR_APPALTO.Value) _
                            & " and pagamenti.id_Stato<>-3 " _
                            & " order by PROGR_APPALTO "


            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            myReaderT = par.cmd.ExecuteReader
            While myReaderT.Read
                If ControllaSALsuccessivi <> "" Then
                    ControllaSALsuccessivi = ControllaSALsuccessivi & "," & par.IfNull(myReaderT("PROGR_APPALTO"), "")
                Else
                    ControllaSALsuccessivi = par.IfNull(myReaderT("PROGR_APPALTO"), "")
                End If
            End While
            myReaderT.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function


    Function PagamentoStampato() As Boolean
        Dim FlagConnessione As Boolean

        PagamentoStampato = False

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'CONTROLLO SE L'ATTESTATAO DI PAGAMENTO è STATO STAMPATO
            par.cmd.CommandText = "select count(*) from SISCOM_MI.EVENTI_PAGAMENTI where COD_EVENTO='F98' and ID_PAGAMENTO=" & vIdPagamento

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            myReaderT = par.cmd.ExecuteReader

            If myReaderT.Read Then
                If par.IfNull(myReaderT(0), 0) > 0 Then
                    PagamentoStampato = True
                End If
            End If
            myReaderT.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function
    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim flagconnessione As Boolean
        Try
            '*******************APERURA CONNESSIONE*********************
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
            Dim NOME As String = par.cmd.ExecuteScalar
            If Not String.IsNullOrEmpty(NOME) Then
                If Not cmbStato.SelectedValue.Equals("0") Then
                    idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
                    par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO in (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
                    NOME = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                    If String.IsNullOrEmpty(NOME) Then
                        HiddenFieldRielabPagam.Value = "0"
                        DataEmissione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                        txtDataScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                        Panel1.Visible = True
                        par.cmd.CommandText = "SELECT NVL(PAGAMENTI.DATA_EMISSIONE_PAGAMENTO,PAGAMENTI.DATA_EMISSIONE) AS DATA_EMISSIONE,DATA_SCADENZA,DESCRIZIONE_BREVE,PAGAMENTI.DESCRIZIONE,PAGAMENTI.PROGR,PAGAMENTI.ANNO, " _
                              & "(select descrizione from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as modalita," _
                              & "(select descrizione from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as condizione, " _
                              & "(select id from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_modalita," _
                              & "(select id from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_condizione " _
                              & ",'SAL n. '||pagamenti.progr_appalto||'/'||pagamenti.anno||' del '||siscom_mi.getdata (pagamenti.data_sal) as sal " _
                              & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                              & " WHERE   PAGAMENTI.ID=" & Me.txtid.Value _
                              & " AND PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                              & " AND PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "

                        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim sal As String = ""
                        If Lettore.Read Then
                            DataEmissione.Text = par.FormattaData(par.IfNull(Lettore("DATA_EMISSIONE"), ""))
                            ADP.Text = "Attestato di pagamento N." & par.IfNull(Lettore("PROGR"), "") & "/" & par.IfNull(Lettore("ANNO"), "")
                            txtModalitaPagamento.Text = par.IfNull(Lettore("modalita"), "")
                            txtCondizionePagamento.Text = par.IfNull(Lettore("condizione"), "")
                            idCondizione.Value = par.IfNull(Lettore("id_Condizione"), "NULL")
                            idModalita.Value = par.IfNull(Lettore("id_Modalita"), "NULL")
                            Me.txtDataScadenza.Text = par.FormattaData(CalcolaDataScadenza(idModalita.Value, idCondizione.Value, par.IfNull(Lettore("DATA_SCADENZA"), "")))
                            'Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione_breve"), "")
                            Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione"), "")
                            sal = par.IfNull(Lettore("sal"), "")
                        End If
                        Lettore.Close()
                        If txtDescrizioneBreve.Text = "" Then
                            If Len(sal) > 7 Then
                                'xtDescrizioneBreve.Text = txtDescManutenzioni.Text & " (" & sal & ")"
                                txtDescrizioneBreve.Text = sal
                            Else
                                txtDescrizioneBreve.Text = txtDescManutenzioni.Text
                            End If
                        End If
                        Dim Script As String = "function f(){$find(""" + RadWindowStampa.ClientID + """).show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        If Not String.IsNullOrWhiteSpace(Script) Then
                            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", Script, True)
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../ALLEGATI/SAL/" & NOME & "','AttPagamento','');self.close();", True)
                    End If
                Else
                    RadWindowManager1.RadAlert("Impossibile stampare il CDP!<br />Salvare prima il SAL", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Impossibile stampare il CDP!<br />Inserire prima un <strong>sal firmato</strong>", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            If flagconnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Panel1.Visible = False
        End Try
    End Sub

    Private Function CalcolaDataScadenza(ByVal TipoModalita As String, ByVal tipoPagamento As String, ByVal DataScadPagamento As String) As String
        CalcolaDataScadenza = ""
        TipoModalita = TipoModalita.ToUpper.Replace("NULL", "")
        tipoPagamento = tipoPagamento.ToUpper.Replace("NULL", "")

        If String.IsNullOrEmpty(DataScadPagamento) Then
            If Not String.IsNullOrEmpty(TipoModalita) Then
                Dim Table As String = ""
                Dim Column As String = ""
                Dim FlSomma As Integer = 0
                Dim DaySum As Integer = 0
                par.cmd.CommandText = "SELECT tab_rif,fld_rif,fl_somma_giorni FROM siscom_mi.TAB_DATE_MODALITA_PAG WHERE ID = (SELECT id_data_riferimento FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = " & idModalita.Value & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    Table = par.IfNull(lettore("tab_rif"), "")
                    Column = par.IfNull(lettore("fld_rif"), "")
                    FlSomma = par.IfNull(lettore("fl_somma_giorni"), "")
                End If
                lettore.Close()

                If Not String.IsNullOrEmpty(Table) And Not String.IsNullOrEmpty(Column) Then
                    par.cmd.CommandText = "select " & Column & " from siscom_Mi." & Table & " where id = " & Me.txtid.Value
                    CalcolaDataScadenza = par.IfNull(par.cmd.ExecuteScalar, "")
                End If

                If Not String.IsNullOrEmpty(CalcolaDataScadenza) Then
                    If FlSomma = 1 Then
                        par.cmd.CommandText = "select nvl(num_giorni,0) from siscom_mi.tipo_pagamento where id = " & tipoPagamento
                        DaySum = par.IfNull(par.cmd.ExecuteScalar, 0)

                        If DaySum > 0 Then
                            CalcolaDataScadenza = Date.Parse(par.FormattaData(CalcolaDataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(DaySum).ToString("dd/MM/yyyy")
                            CalcolaDataScadenza = par.AggiustaData(CalcolaDataScadenza)
                        End If
                    End If
                End If
            End If
        End If

        If String.IsNullOrEmpty(CalcolaDataScadenza) Then
            CalcolaDataScadenza = DataScadPagamento
        End If

    End Function
    Protected Sub ImgAnnulla_Click(sender As Object, e As System.EventArgs) Handles ImgAnnulla.Click
        Panel1.Visible = False
    End Sub
    Protected Sub ImgConferma_Click(sender As Object, e As System.EventArgs) Handles ImgConferma.Click
        If IsDate(DataEmissione.Text) Then
            Dim dataemissioneSal As String = ""
            Dim flagconnessione As Boolean
            Try
                '*******************APERURA CONNESSIONE*********************
                flagconnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    flagconnessione = True
                End If

                par.cmd.CommandText = "SELECT DATA_EMISSIONE from siscom_mi.PAGAMENTI WHERE PAGAMENTI.ID=" & Me.txtid.Value
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lettore.Read Then
                    dataemissioneSal = par.IfNull(Lettore("DATA_EMISSIONE"), "")
                End If
                Lettore.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                If flagconnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                If dataemissioneSal > par.AggiustaData(DataEmissione.Text) Then
                    RadWindowManager1.RadAlert("La data di emissione dell\'attestato di pagamento \ndeve essere successiva alla data di emissione del SAL!", 300, 150, "Attenzione", "", "null")
                Else
                    '  Panel1.Visible = False
                    stampaP(DataEmissione.Text)
                End If

            Catch ex As Exception
                If flagconnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                'Panel1.Visible = False
            End Try
        Else
            RadWindowManager1.RadAlert("Inserire correttamente data emissione e data scadanza!", 300, 150, "Attenzione", "", "null")

        End If
    End Sub



    Private Sub stampaP(Optional ByVal dataEmiss As String = Nothing)
        Dim sStr1 As String
        Dim sDescrizione As String

        Dim perc_oneri As Decimal = 0, perc_sconto As Decimal = 0, perc_iva As Decimal = 0
        Dim oneri As Decimal = 0, asta As Decimal = 0, iva As Decimal = 0, ritenuta As Decimal = 0, risultato1 As Decimal = 0, risultato2 As Decimal = 0, risultato3 As Decimal = 0, risultato4 As Decimal = 0, ritenutaIVATA As Decimal = 0
        Dim risultato4Tot As Decimal
        Dim FlagConnessione As Boolean

        Try

            If Me.txtid.Value = "" Then
                RadWindowManager1.RadAlert("Errore durante la stampa!", 300, 150, "Attenzione", "", "null")

            Else

                'RIEPILOGO SAL
                importoT = 0
                penaleT = 0
                oneriT = 0
                astaT = 0
                ivaT = 0
                ritenutaT = 0
                rimborsoT = 0
                risultato1T = 0
                risultato2T = 0
                risultato3T = 0
                risultato4T = 0
                risultatoImponibileT = 0

                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoMANU.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


                '****Scrittura evento EMISSIONE DEL PAGAMENTO
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Me.txtid.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','Stampa Attestato di Pagamento con data emissione del pagamento " & dataEmiss & "')"
                par.cmd.ExecuteNonQuery()

                Dim dataScadenza As String = "NULL"
                If IsDate(txtDataScadenza.Text) Then
                    dataScadenza = "'" & par.AggiustaData(txtDataScadenza.Text) & "'"
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.PAGAMENTI " _
                    & " SET DATA_EMISSIONE_PAGAMENTO='" & par.AggiustaData(dataEmiss) & "', " _
                    & " DATA_SCADENZA=" & dataScadenza & ", " _
                    & " DESCRIZIONE_BREVE='" & par.PulisciStrSql(txtDescrizioneBreve.Text) & "', " _
                    & " ID_TIPO_MODALITA_PAG=" & idModalita.Value & ", " _
                    & " ID_TIPO_PAGAMENTO=" & idCondizione.Value & " " _
                    & " WHERE ID=" & Me.txtid.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = ""

                contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(Me.txtid.Value))

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select manutenzioni.id, MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IVA_CONSUMO," _
                                          & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                          & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                   & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                   & " where MANUTENZIONI.ID_PAGAMENTO=" & Me.txtid.Value _
                                   & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


                myReader1 = par.cmd.ExecuteReader()
                Dim imponibile_rimborso As Decimal = 0
                Dim iva_rimborso As Decimal = 0

                While myReader1.Read
                    'MOD 24/02/2015
                    '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                    'If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                    If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                        solaLetturaImportoMinoreZero = True
                    End If

                    par.cmd.CommandText = "SELECT IMPONIBILE_RIMBORSO, PERC_IVA_RIMBORSO " _
                                            & "FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & "WHERE     ID_MANUTENZIONI_INTERVENTI IN (SELECT ID " _
                                            & "FROM SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & "WHERE ID_MANUTENZIONE = " & par.IfNull(myReader1("id"), "-1") & ") " _
                                            & "AND COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE' "
                    Dim myReaderRimb As Oracle.DataAccess.Client.OracleDataReader
                    myReaderRimb = par.cmd.ExecuteReader
                    While myReaderRimb.Read
                        imponibile_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0"))
                        iva_rimborso += CDec(par.IfNull(myReaderRimb("IMPONIBILE_RIMBORSO"), "0")) * CDec(par.IfNull(myReaderRimb("PERC_IVA_RIMBORSO"), "0")) / 100
                    End While
                    myReaderRimb.Close()

                    CalcolaImporti(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), par.IfNull(myReader1("IVA_CONSUMO"), 0), par.IfNull(myReader1("RIMBORSI"), 0), par.IfNull(myReader1("PENALE2"), 0), par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader1("ID_APPALTO"), 0), par.IfNull(myReader1("IMPORTO_ONERI_CONS"), 0))
                    'Else
                    'Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento!');</script>")
                    'myReader1.Close()

                    ''*********************CHIUSURA CONNESSIONE**********************
                    'If FlagConnessione = True Then
                    '    par.cmd.Dispose()
                    '    par.OracleConn.Close()
                    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    'End If

                    'Exit Sub
                    'End If
                End While
                myReader1.Close()

                '$anno=                 PAGAMENTI.ANNO
                '$progr=                PAGAMENTI.PROGR
                '$data_emissione=       PAGAMENTI.DATA_EMISSIONE
                '$dettagli_chiamante=   N. APPALTI.NUM_REPERTORIO DEL APPALTI.DATA_REPERTORIO APPALTI.DESCRIZIONE


                '$dettagli= 
                '$imp_letterale=        contenuto = Replace(contenuto, "$imp_letterale$", NumeroInLettere(par.IfNull(myReader1("PAGAMENTI.IMPORTO_PRENOTATO"), 0)))

                '$data_stampa=          contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("PAGAMENTI.DATA_STAMPA")))
                '$chiamante=            contenuto = Replace(contenuto, "$chiamante$", "CONTRATTO")


                '$cod_capitolo=         PF_VOCI.CODICE
                '$voce_pf=              PF_VOCI.DESCRIZIONE
                '$finanziamento=        contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")
                '$dettaglio=            ???
                '$TOT=                  contenuto = Replace(contenuto, "$TOT$", par.IfNull(myReader1("PAGAMENTI.IMPORTO_PRENOTATO"), 0))

                '                            & " PF_VOCI_IMPORTO.ID as ""ID_VOCI"",PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"" " _

                sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.PROGR_APPALTO,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_STAMPA,PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.CONTO_CORRENTE,PAGAMENTI.DESCRIZIONE as ""DESCRIZIONE_PAGAMENTI""," _
                            & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                            & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE, FORNITORI.ID as ID_FORNITORE, " _
                            & "(select descrizione from siscom_mi.tipo_modalita_pag where id=pagamenti.id_tipo_modalita_pag) as modalita," _
                            & " (select descrizione from siscom_mi.tipo_pagamento where id=pagamenti.id_tipo_pagamento) as condizione,to_char(to_date(pagamenti.data_scadenza,'yyyyMMdd'),'dd/MM/yyyy') as data_scadenza,pagamenti.descrizione_breve " _
                            & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                            & " where   PAGAMENTI.ID=" & Me.txtid.Value _
                            & " and PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                            & " and PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "

                '& " and PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO (+) " _
                '& " and PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID (+) " _
                '& " and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "

                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader

                If myReader1.Read Then




                    contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReader1("modalita"), "-"))
                    contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReader1("condizione"), "-"))
                    contenuto = Replace(contenuto, "$datascadenza$", par.IfNull(myReader1("data_scadenza"), "-"))
                    contenuto = Replace(contenuto, "$descrizionebreve$", par.IfNull(myReader1("descrizione_breve"), "-"))

                    'PAGAMENTI
                    contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progr$", myReader1("PROGR")) ' myReader1("PROGR_APPALTO"))

                    contenuto = Replace(contenuto, "$annoSAL$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progrSAL$", myReader1("PROGR_APPALTO"))

                    'contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "")))
                    contenuto = Replace(contenuto, "$data_emissione$", dataEmiss)
                    'contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))
                    contenuto = Replace(contenuto, "$data_stampa$", dataEmiss)

                    contenuto = Replace(contenuto, "$contratto$", "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(myReader1("DATA_REPERTORIO")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$CIG$", par.IfNull(myReader1("CIG"), ""))
                    contenuto = Replace(contenuto, "$conto_corrente$", par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01"))


                    'FORNITORI
                    Dim sFORNITORI As String = ""
                    If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            sFORNITORI = par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                        Else
                            sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                        End If
                    Else
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            sFORNITORI = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        Else
                            sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        End If
                    End If
                    contenuto = Replace(contenuto, "$fornitoreIntestazione$", sFORNITORI)
                    'INDIRIZZO FORNITORE
                    Dim sIndirizzoFornitore1 As String = ""
                    Dim sComuneFornitore1 As String = ""
                    par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReader1("ID_FORNITORE"), 0)

                    Dim myReaderTT As Oracle.DataAccess.Client.OracleDataReader
                    myReaderTT = par.cmd.ExecuteReader
                    While myReaderTT.Read

                        sIndirizzoFornitore1 = par.IfNull(myReaderTT("TIPO"), "") _
                                        & " " & par.IfNull(myReaderTT("INDIRIZZO"), "") _
                                        & " " & par.IfNull(myReaderTT("CIVICO"), "")

                        sComuneFornitore1 = par.IfNull(myReaderTT("CAP"), "") _
                                        & " " & par.IfNull(myReaderTT("COMUNE"), "")

                    End While
                    myReaderTT.Close()
                    contenuto = Replace(contenuto, "$fornitore_indirizzo$", sIndirizzoFornitore1)
                    contenuto = Replace(contenuto, "$comuneIntestazione$", sComuneFornitore1)


                    'IBAN **************************************************
                    par.cmd.CommandText = "select IBAN||' - '||(SELECT DISTINCT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI_IBAN.ID_FORNITORE=FORNITORI.ID) AS IBAN from SISCOM_MI.FORNITORI_IBAN " _
                                       & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"

                    Dim myReaderBP As Oracle.DataAccess.Client.OracleDataReader
                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        sFORNITORI = sFORNITORI & "<br/>" & par.IfNull(myReaderBP("IBAN"), "")
                    End While
                    myReaderBP.Close()
                    contenuto = Replace(contenuto, "$fornitori$", sFORNITORI)
                    '******************************************************


                    '*****************Modifica Peppe 06/08/2010, $TOT$ deve essere uguale a riultato4, lo sposto dopo il calcolo dello stesso*******
                    'contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "", "##,##0.00"))

                    '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO

                    'sStr1 = "select * from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '    & " where ID_PF_VOCE_IMPORTO=" & par.IfNull(myReader1("ID_VOCI"), "Null") _
                    '    & " and   ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), "Null")

                    'Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                    'par.cmd.CommandText = sStr1
                    'myReaderT = par.cmd.ExecuteReader

                    'If myReaderT.Read Then
                    '    Dim importo, perc_oneri, perc_sconto, perc_iva As Double
                    '    Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4 As Double

                    'importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                    'perc_oneri = par.IfNull(myReader1("PERC_ONERI_SIC_CON"), 0)

                    'perc_sconto = par.IfNull(myReaderT("SCONTO_CONSUMO"), 0)
                    'perc_iva = par.IfNull(myReaderT("IVA_CONSUMO"), 0)


                    ''ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                    'oneri = importo - ((importo * 100) / (100 + perc_oneri))

                    ''LORDO senza ONERI= IMPORTO-oneri
                    'risultato1 = importo - oneri

                    ''RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                    'asta = (risultato1 * perc_sconto) / 100

                    ''NETTO senza ONERI= (LORDO senza oneri-asta)
                    'risultato2 = risultato1 - asta

                    ''NETTO con ONERI= (IMPORTO-asta)
                    'risultato3 = importo - asta

                    ''IVA= (NETTO con oneri*perc_iva)/100
                    'iva = (risultato3 * perc_iva) / 100

                    ''NETTO+ONERI+IVA
                    'risultato4 = risultato3 + iva

                    'contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(risultato4T, 0), "", "##,##0.00"))


                    sDescrizione = par.IfNull(myReader1("DESCRIZIONE_PAGAMENTI"), "") '"Pagamento ODL (Vedi Allegato)" '& vbCrLf & vbCrLf

                    Dim S2 As String = "<table style='width:100%;'>"
                    S2 = S2 & "<tr>"
                    S2 = S2 & "<td colspan='2' style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & sDescrizione & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(importoT, 0), "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(oneriT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato1T, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(astaT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2T, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3T, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenutaT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Imponibile (al netto trattenute) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultatoImponibileT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaT, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    Dim percIva As String = "0"

                    If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, 0) > 0 Then

                        Dim totaleIVA As String = ""
                        par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) as PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"
                        Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore1.Read Then
                            percIva = par.IfNull(lettore1("PERC_IVA"), "0")
                        End If
                        lettore1.Close()
                        totaleIVA = (CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text) * CDec(percIva) / 100)
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Importo IVA recupero (" & IsNumFormat(percIva, "", "##,##0") & "%) €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(totaleIVA, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                    End If
                    If rimborsoT > 0 Then
                        'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile Rimborsi €</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                        'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA Rimborsi €</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(iva_rimborso, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                        'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Totale Rimborsi €</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(rimborsoT, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                    End If

                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T, "", "##,##0.00") & "</td>"

                    If penaleT > 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: right; width:40%;font-size:14pt;font-family :Arial ;'>Penale €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(-penaleT, "", "##,##0.00") & "</td>"
                    End If
                    If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, 0) > 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T - (CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text) + (CDec(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text) * CDec(percIva) / 100)) - penaleT, "", "##,##0.00") & "</td>"
                    ElseIf penaleT <> 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4T - penaleT, "", "##,##0.00") & "</td>"
                    End If

                    S2 = S2 & "</tr></table>"


                    Dim T As String = "<table style='width:100%;'>"
                    T = T & "<tr>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                    T = T & "</tr></table>"

                    contenuto = Replace(contenuto, "$dettagli$", T)

                    Dim TestoGrigliaINTESTAZIONE As String = "" ' "<p style='page-break-after: always'>&nbsp;</p>"

                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<table cellspacing='0' style='width:50%; border: 1px solid black;border-collapse: collapse;' >"
                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >A netto compresi oneri</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultato3T, "", "##,##0.00") & "</td>" _
                                              & "</tr>" _
                                              & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Ritenuta di legge 0,5 % (senza IVA)</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(ritenutaNoIvaT, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, 0) > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Recupero anticipazione contrattuale (SENZA IVA)</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(CType(Tab_SAL_Riepilogo.FindControl("txtImportoTrattenuto"), TextBox).Text, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If
                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile (al netto delle trattenute) </td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultatoImponibileT, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    If rimborsoT > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile rimborsi</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If
                    If penaleT > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Penale</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(-penaleT, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If

                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "</table>"
                    contenuto = Replace(contenuto, "$grigliaIntestazione$", TestoGrigliaINTESTAZIONE)




                    '*********** DETTAGLIO GRIGLIA VOCI BP
                    'TestoPagina = TestoPagina & "</table>"
                    Dim TestoGrigliaBP As String = "" '"<p style='page-break-before: always'>&nbsp;</p>"

                    TestoGrigliaBP = TestoGrigliaBP & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 10pt; font-weight: bold'>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>CODICE BP</td>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO BP</td>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 60%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE BP</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width: 20%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                              & "</tr>"


                    'ESTRAGGO TUTTE LE VOCI BP DIVERSE

                    'RIEPILOGO SAL

                    risultato4Tot = 0

                    par.cmd.CommandText = "select distinct(ID_VOCE), " _
                                        & "(SELECT DISTINCT RTRIM (LTRIM (SUBSTR (inizio, 1, 4))) AS annoBp FROM siscom_mi.T_ESERCIZIO_FINANZIARIO  WHERE id IN " _
                                        & "(select id_esercizio_finanziario from siscom_mi.pf_main where id in (SELECT DISTINCT id_piano_finanziario FROM siscom_mi.pf_voci WHERE id = PF_VOCI_IMPORTO.ID_VOCE))" _
                                        & ") AS ANNO " _
                                        & "from SISCOM_MI.PF_VOCI_IMPORTO " _
                                        & " where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.MANUTENZIONI where ID_PAGAMENTO=" & Me.txtid.Value & ")"

                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        'X OGNI TIPO DI VOCE
                        par.cmd.CommandText = "select sum(manutenzioni.importo_consuntivato) as importo_consuntivato,id_pf_voce_importo,manutenzioni.id_appalto,manutenzioni.iva_consumo,sum(manutenzioni.rimborsi) as rimborsi, " _
                                          & " APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                          & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                          & " where ID_PAGAMENTO=" & Me.txtid.Value _
                                          & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO where ID_VOCE=" & par.IfNull(myReaderBP("ID_VOCE"), 0) & ")" _
                                          & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                                          & " group by id_pf_voce_importo,manutenzioni.id_appalto,APPALTI_PENALI.IMPORTO, manutenzioni.iva_consumo,manutenzioni.rimborsi "


                        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                        myReaderB = par.cmd.ExecuteReader

                        While myReaderB.Read
                            '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                            ' If par.IfNull(myReaderB("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                            If par.IfNull(myReaderB("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                                solaLetturaImportoMinoreZero = True
                            End If

                                sStr1 = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                                    & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                                    & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReaderB("ID_PF_VOCE_IMPORTO"), 0) _
                                    & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & par.IfNull(myReaderB("ID_APPALTO"), 0) _
                                    & "  and   APPALTI.ID=" & par.IfNull(myReaderB("ID_APPALTO"), 0)


                                Dim myReaderB2 As Oracle.DataAccess.Client.OracleDataReader
                                par.cmd.CommandText = sStr1
                                myReaderB2 = par.cmd.ExecuteReader

                                If myReaderB2.Read Then

                                    perc_oneri = par.IfNull(myReaderB2("PERC_ONERI_SIC_CON"), 0)

                                    perc_sconto = par.IfNull(myReaderB2("SCONTO_CONSUMO"), 0)
                                    perc_iva = par.IfNull(myReaderB("IVA_CONSUMO"), 0) 'par.IfNull(myReaderT2("IVA_CONSUMO"), 0)


                                    'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                                    oneri = par.IfNull(myReaderB("IMPORTO_CONSUNTIVATO"), 0) - ((par.IfNull(myReaderB("IMPORTO_CONSUNTIVATO"), 0) * 100) / (100 + perc_oneri))
                                    oneri = Round(oneri, 2)
                                    'LORDO senza ONERI= IMPORTO-oneri
                                    risultato1 = par.IfNull(myReaderB("IMPORTO_CONSUNTIVATO"), 0) - oneri

                                    'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                                    asta = (risultato1 * perc_sconto) / 100
                                    asta = Round(asta, 2)

                                    'NETTO senza ONERI= (LORDO senza oneri-asta) 
                                    risultato2 = risultato1 - asta '- penale

                                    'AGGIUNTO
                                    'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                                    risultato3 = risultato2 + oneri

                                    'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                                    If par.IfNull(myReaderB2("FL_RIT_LEGGE"), 0) = 1 Then
                                        ritenuta = (risultato3 * 0.5) / 100
                                        ritenuta = Round(ritenuta, 2)
                                        ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                                        'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                                    Else
                                        ritenuta = 0
                                        ritenutaIVATA = 0
                                    End If


                                    'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                                    risultato3 = risultato3 - ritenuta

                                    'IVA= (NETTO con oneri*perc_iva)/100
                                    iva = Math.Round((risultato3 * perc_iva) / 100, 2)

                                    'NETTO+ONERI+IVA
                                    risultato4 = risultato3 + iva + Round(CDec(par.IfNull(myReaderB("RIMBORSI"), 0)), 2)
                                    risultato4Tot = risultato4Tot + risultato4

                                    Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                                    par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderBP("ID_VOCE"), 0)
                                    myReaderB3 = par.cmd.ExecuteReader

                                    If myReaderB3.Read Then
                                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("anno"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                                                                        & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & Format(par.IfNull(risultato4, 0), "##,##0.00") & "</td>" _
                                                                        & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                                                                        & "</tr>"

                                    End If
                                    myReaderB3.Close()

                                End If
                                myReaderB2.Close()
                            'End If
                        End While
                        myReaderB.Close()

                    End While
                    myReaderBP.Close()
                    par.cmd.CommandText = "select sum(anticipo_contrattuale) " _
                                   & " from   SISCOM_MI.PRENOTAZIONI " _
                                   & " where ID_PAGAMENTO=" & vIdPagamento

                    Dim anticipoContrattuale As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If anticipoContrattuale <> 0 Then


                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " RECUPERO ANTICIPAZIONE CONTRATTUALE   : " & IsNumFormat(anticipoContrattuale + (anticipoContrattuale * percIva / 100), "", "##,##0.00") & "</td>" _
                                  & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                  & "</tr>"
                    End If


                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " TOTALE   : " & IsNumFormat(risultato4Tot - (anticipoContrattuale + (anticipoContrattuale * percIva / 100)), "", "##,##0.00") & "</td>" _
                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                              & "</tr>"

                    contenuto = Replace(contenuto, "$grigliaBP$", TestoGrigliaBP)
                    '********************************



                    '*********** DETTAGLIO GRIGLIA MANUTENZIONI
                    'TestoPagina = TestoPagina & "</table>"
                    Dim TestoGrigliaM As String = "<p style='page-break-before: always'>&nbsp;</p>"

                    TestoGrigliaM = TestoGrigliaM & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                    TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ODL</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>SAL</td>" _
                                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA ORDINE</td>" _
                                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DATA CONSUNTIVO</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI)</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IVA</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Tot. RIMBORSI</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. NETTO (ONERI e IVA)</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. PENALE</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                              & "</tr>"


                    'RIEPILOGO SAL

                    risultato4Tot = 0

                    par.cmd.CommandText = "select MANUTENZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                       & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                                       & " where ID_PAGAMENTO=" & Me.txtid.Value _
                                       & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) " _
                                       & " order by MANUTENZIONI.PROGR "


                    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                    myReaderT = par.cmd.ExecuteReader

                    While myReaderT.Read
                        '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                        ' If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                        If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) < 0 Then
                            solaLetturaImportoMinoreZero = True
                        End If

                            sStr1 = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                                                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                                                & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReaderT("ID_PF_VOCE_IMPORTO"), 0) _
                                                & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & par.IfNull(myReaderT("ID_APPALTO"), 0) _
                                                & "  and   APPALTI.ID=" & par.IfNull(myReaderT("ID_APPALTO"), 0)

                            Dim myReaderT2 As Oracle.DataAccess.Client.OracleDataReader
                            par.cmd.CommandText = sStr1
                            myReaderT2 = par.cmd.ExecuteReader

                            If myReaderT2.Read Then

                                perc_oneri = par.IfNull(myReaderT2("PERC_ONERI_SIC_CON"), 0)

                                perc_sconto = par.IfNull(myReaderT2("SCONTO_CONSUMO"), 0)
                                perc_iva = par.IfNull(myReaderT("IVA_CONSUMO"), 0) 'par.IfNull(myReaderT2("IVA_CONSUMO"), 0)

                                Dim oneriC As Decimal = par.IfNull(myReaderT("IMPORTO_ONERI_CONS"), -1)
                                If oneriC = -1 Then


                                'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                                oneri = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) - ((par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) * 100) / (100 + perc_oneri))
                                Else
                                    oneri = oneriC
                                End If
                                oneri = Round(oneri, 2)
                                'LORDO senza ONERI= IMPORTO-oneri
                                risultato1 = par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0) - oneri

                                'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                                asta = (risultato1 * perc_sconto) / 100
                                asta = Round(asta, 2)
                                'NETTO senza ONERI= (LORDO senza oneri-asta) 
                                risultato2 = risultato1 - asta '- penale

                                'AGGIUNTO
                                'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                                risultato3 = risultato2 + oneri

                                'ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
                                If par.IfNull(myReaderT2("FL_RIT_LEGGE"), 0) = 1 Then
                                    ritenuta = (risultato3 * 0.5) / 100
                                    ritenuta = Round(ritenuta, 2)
                                    ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
                                    'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
                                Else
                                    ritenuta = 0
                                    ritenutaIVATA = 0
                                End If

                                'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                                risultato3 = risultato3 - ritenuta

                                'IVA= (NETTO con oneri*perc_iva)/100
                                iva = Math.Round((risultato3 * perc_iva) / 100, 2)

                                'NETTO+ONERI+IVA
                                risultato4 = risultato3 + iva + Round(CDec(par.IfNull(myReaderT("RIMBORSI"), 0)), 2)
                                risultato4Tot = risultato4Tot + risultato4

                                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PROGR"), "") & "</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("ANNO"), "") & "</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("PROGR"), "") & "</td>" _
                                                      & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_INIZIO_ORDINE"), "")) & "</td>" _
                                                      & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.FormattaData(par.IfNull(myReaderT("DATA_FINE_ORDINE"), "")) & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato3, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(iva, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("RIMBORSI"), 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(risultato4, 0), "##,##0.00") & "</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(par.IfNull(myReaderT("PENALE2"), 0), "##,##0.00") & "</td>" _
                                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                                      & "</tr>"


                            End If
                            myReaderT2.Close()

                        'End If
                    End While
                    myReaderT.Close()


                    TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & IsNumFormat(risultato4Tot, "", "##,##0.00") & "</td>" _
                              & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                              & "</tr>"

                    contenuto = Replace(contenuto, "$grigliaM$", TestoGrigliaM)
                    '********************************



                    'End If
                    'myReaderT.Close()

                    '*****************FINE SCRITTURA DETTAGLI
                    contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))
                    'contenuto = Replace(contenuto, "$dettaglio$", "MANUTENZIONE")

                    'contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("COD_VOCE"), ""))
                    'contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESC_VOCE"), ""))
                    'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")

                    par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim chiamante2 As String = ""
                    If lettore.Read Then
                        chiamante2 = par.IfNull(lettore(0), "")
                    End If
                    lettore.Close()
                    contenuto = Replace(contenuto, "$chiamante2$", chiamante2)
                    par.cmd.CommandText = "SELECT INITCAP(GESTORI_ORDINI.DESCRIZIONE) FROM SISCOM_MI.GESTORI_ORDINI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI " _
                        & " WHERE APPALTI.ID_gESTORE_ORDINI=GESTORI_ORDINI.ID AND APPALTI.ID=PAGAMENTI.ID_APPALTO AND PAGAMENTI.ID=" & Me.txtid.Value
                    lettore = par.cmd.ExecuteReader
                    Dim gestore As String = ""
                    If lettore.Read Then
                        gestore = par.IfNull(lettore(0), "")
                    End If
                    lettore.Close()
                    contenuto = Replace(contenuto, "$proponente$", gestore)




                End If
                myReader1.Close()


                par.myTrans.Commit() 'COMMIT


                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                myReader1.Close()

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)

                HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)



                Dim url As String = Server.MapPath("..\..\..\FileTemp\")
                Dim pdfConverter1 As PdfConverter = New PdfConverter

                Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter1.LicenseKey = Licenza
                End If

                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                pdfConverter1.PdfDocumentOptions.LeftMargin = 30
                pdfConverter1.PdfDocumentOptions.RightMargin = 30
                pdfConverter1.PdfDocumentOptions.TopMargin = 30
                pdfConverter1.PdfDocumentOptions.BottomMargin = 10
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfFooterOptions.FooterText = ("")
                pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = ""

                pdfConverter1.PdfDocumentOptions.ShowFooter = True
                'pdfConverter1.PdfFooterOptions.ShowPageNumber = True
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False

                Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                nomefile = par.NomeFileManut("CDP", vIdPagamento) & ".pdf"

                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

                Dim i As Integer = 0
                For i = 0 To 10000
                Next
                'GIANCARLO 16-02-2017
                'inserimento della stampa cdp negli allegati
                Dim descrizione As String = "Stampa pagamento"
                Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
                'Imposto le vecchie rielaborazioni a 2...per barrare il nome
                par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                    & "SET STATO = 2 " _
                                    & "WHERE " _
                                    & "TIPO = " & idTipoOggetto _
                                    & "AND ID_OGGETTO = " & vIdPagamento
                par.cmd.ExecuteNonQuery()
                If HiddenFieldRielabPagam.Value = "1" Then
                    descrizione = "Stampa rielaborazione pagamento"
                    idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
                    'Imposto le vecchie rielaborazioni a 2...per barrare il nome
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                        & "SET STATO = 2 " _
                                        & "WHERE " _
                                        & "TIPO = " & idTipoOggetto _
                                        & "AND ID_OGGETTO = " & vIdPagamento
                    par.cmd.ExecuteNonQuery()
                End If
                par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
                Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
                par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, vIdPagamento, "../../../ALLEGATI/SAL/")
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();", True)
            End If
            txtid.Value = ""

        Catch ex As Exception

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnRielaboraPagamento_Click(sender As Object, e As System.EventArgs) Handles btnRielaboraPagamento.Click
        Dim flagconnessione As Boolean
        Try
            '*******************APERURA CONNESSIONE*********************
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
            Dim NOME As String = par.cmd.ExecuteScalar
            If Not String.IsNullOrEmpty(NOME) Then
                If Not cmbStato.SelectedValue.Equals("0") Then
                    idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
                    par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento
                    NOME = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                    If Not String.IsNullOrEmpty(NOME) Then
                        HiddenFieldRielabPagam.Value = "1"
                        DataEmissione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                        txtDataScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                        par.cmd.CommandText = "SELECT NVL(PAGAMENTI.DATA_EMISSIONE_PAGAMENTO,PAGAMENTI.DATA_EMISSIONE) AS DATA_EMISSIONE,DATA_SCADENZA,DESCRIZIONE_BREVE,PAGAMENTI.DESCRIZIONE,PAGAMENTI.PROGR,PAGAMENTI.ANNO, " _
                              & "(select descrizione from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as modalita," _
                              & "(select descrizione from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as condizione, " _
                              & "(select id from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_modalita," _
                              & "(select id from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_condizione " _
                              & ",'SAL n. '||pagamenti.progr_appalto||'/'||pagamenti.anno||' del '||siscom_mi.getdata (pagamenti.data_sal) as sal " _
                              & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                              & " WHERE   PAGAMENTI.ID=" & Me.txtid.Value _
                              & " AND PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                              & " AND PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "

                        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim sal As String = ""
                        If Lettore.Read Then
                            DataEmissione.Text = par.FormattaData(par.IfNull(Lettore("DATA_EMISSIONE"), ""))
                            ADP.Text = "Attestato di pagamento N." & par.IfNull(Lettore("PROGR"), "") & "/" & par.IfNull(Lettore("ANNO"), "")
                            txtModalitaPagamento.Text = par.IfNull(Lettore("modalita"), "")
                            txtCondizionePagamento.Text = par.IfNull(Lettore("condizione"), "")
                            idCondizione.Value = par.IfNull(Lettore("id_Condizione"), "NULL")
                            idModalita.Value = par.IfNull(Lettore("id_Modalita"), "NULL")
                            Me.txtDataScadenza.Text = par.FormattaData(CalcolaDataScadenza(idModalita.Value, idCondizione.Value, par.IfNull(Lettore("DATA_SCADENZA"), "")))
                            'Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione_breve"), "")
                            Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione"), "")
                            sal = par.IfNull(Lettore("sal"), "")
                        End If
                        Lettore.Close()
                        Dim Script As String = "function f(){$find(""" + RadWindowStampa.ClientID + """).show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        If Not String.IsNullOrWhiteSpace(Script) Then
                            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", Script, True)
                        End If
                        If txtDescrizioneBreve.Text = "" Then
                            If Len(sal) > 7 Then
                                txtDescrizioneBreve.Text = txtDescManutenzioni.Text & " (" & sal & ")"
                                txtDescrizioneBreve.Text = txtDescManutenzioni.Text & " (" & sal & ")"
                            Else
                                txtDescrizioneBreve.Text = txtDescManutenzioni.Text
                            End If
                        End If
                    Else
                        RadWindowManager1.RadAlert("Impossibile rielaborare!\nStampare prima il CDP!", 300, 150, "Attenzione", "", "null")
                    End If
                Else
                    RadWindowManager1.RadAlert("Impossibile stampare il CDP!<br />Salvare prima il SAL", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Impossibile rielaborare il CDP!<br />Inserire prima un <strong>sal firmato</strong>", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            If flagconnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Panel1.Visible = False
        End Try
    End Sub

    Protected Sub btnRielbSal_Click(sender As Object, e As System.EventArgs) Handles btnRielbSal.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA")
        par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento
        Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
        If Not String.IsNullOrEmpty(nome) Then
            HiddenFieldRielabPagam.Value = "1"
            PdfSal(vIdPagamento)
        Else
            Response.Write("<script>alert('Impossibile rielaborare!\nStampare prima il SAL!');</script>")
        End If
    End Sub
End Class
