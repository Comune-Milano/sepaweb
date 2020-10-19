Imports System.Collections
Imports Telerik.Web.UI
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Segnalazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreStato As String
    Public sOrdinamento As String
    'Public TabellaNote As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0


        If Not IsPostBack Then

            txtDataS.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CaricaTipoSegnalazione()
            CaricaDatiSopralluogo()

            txtDataCInt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtOraCInt.Text = Format(Now, "HH:mm")
            Me.txtDataCInt.Text = Format(Now, "dd/MM/yyyy")

            sValoreStato = Request.QueryString("ST")
            sOrdinamento = Request.QueryString("ORD")
            'sOrdinamento = Request.QueryString("ORD")
            cmbNoteChiusura.Attributes.Add("onChange", "getDropDownListvalue();")
            'cmbTipoIntervento.Enabled = False
            vIdSegnalazione = -1
            vIdSegnalazione = Request.QueryString("IDS")
            idSegnalazione.Value = vIdSegnalazione
            Me.txtSTATO.Value = -1
            Me.txtSTATO_PF.Value = -1

            ' CONNESSIONE DB
            lIdConnessione = Format(Now, "yyyyMMddHHmmss")

            'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
            Me.txtConnessione.Text = CStr(lIdConnessione)
            Me.txtIdSegnalazioni.Text = "-1"

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
            End If

            CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"


            If vIdSegnalazione <> -1 Then
                VisualizzaDati()
                txtindietro.Text = 0
            End If

            CaricaEsercizio()

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

            'cmbStato.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

            'If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 7, 1) = 0 Then
            '    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
            '    FrmSolaLettura()
            'End If

        End If
        Select Case Request.QueryString("ST")
            Case "-1"
                FrmSolaLettura()
            Case "0"
            Case "1"
            Case "2"
                FrmSolaLettura()
            Case "3"
            Case "4"
            Case "5"
                FrmSolaLettura()
            Case "10"
                FrmSolaLettura()
        End Select
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

    Public Property vIdSegnalazione() As Long
        Get
            If Not (ViewState("par_idSegnalazione") Is Nothing) Then
                Return CLng(ViewState("par_idSegnalazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idSegnalazione") = value
        End Set

    End Property



    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)

        Try

            Dim sStr1 As String


            '*** FORM PRINCIPALE
            Me.txtIdSegnalazioni.Text = par.IfNull(myReader1("ID"), "-1")
            Me.cmbTipoSegn.SelectedValue = par.IfNull(myReader1("ID_TIPO_SEGNALAZIONE"), "-1")
            'If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
            CaricaTipologieLivello1()
            'End If
            Me.cmbTipoSegnalazioneLivello1.SelectedValue = par.IfNull(myReader1("ID_TIPO_SEGN_LIVELLO_1"), "-1")
            If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                CaricaTipologieLivello2()
                Me.cmbTipoSegnalazioneLivello2.SelectedValue = par.IfNull(myReader1("ID_TIPO_SEGN_LIVELLO_2"), "-1")
                If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
                    CaricaTipologieLivello3()
                    Me.cmbTipoSegnalazioneLivello3.SelectedValue = par.IfNull(myReader1("ID_TIPO_SEGN_LIVELLO_3"), "-1")
                    If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" Then
                        CaricaTipologieLivello4()
                        Me.cmbTipoSegnalazioneLivello4.SelectedValue = par.IfNull(myReader1("ID_TIPO_SEGN_LIVELLO_4"), "-1")
                    End If
                End If
            End If



            cmbUrgenza.ClearSelection()
            Me.cmbUrgenza.SelectedIndex = par.IfNull(myReader1("ID_PERICOLO_SEGNALAZIONE"), "0")

            sStr1 = ""

            'CONTROLLO UNITA
            If par.IfNull(myReader1("ID_UNITA"), "-1") <> "-1" Then

                sStr1 = "select ID,COD_UNITA_IMMOBILIARE as CODICE,'Unità Cod.'||COD_UNITA_IMMOBILIARE as TITOLO " _
                    & " from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & par.IfNull(myReader1("ID_UNITA"), "-1")

                tipo.Value = "U"
                identificativo.Value = par.IfNull(myReader1("ID_UNITA"), "-1")

                'CONTROLLO EDIFICIO
            ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "-1") <> "-1" Then
                sStr1 = "select 'Edificio '||COD_EDIFICIO||' - '||DENOMINAZIONE as TITOLO " _
                    & " from SISCOM_MI.EDIFICI where ID=" & par.IfNull(myReader1("ID_EDIFICIO"), "-1")

                tipo.Value = "E"
                identificativo.Value = par.IfNull(myReader1("ID_EDIFICIO"), "-1")
            Else

                'CONTROLLO COMPLESSO
                sStr1 = "select 'Complesso '||COD_COMPLESSO||' - '||DENOMINAZIONE as TITOLO " _
                                    & " from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                    & " where ID=" & par.IfNull(myReader1("ID_COMPLESSO"), "-1")
                tipo.Value = "C"
                identificativo.Value = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
            End If


            par.cmd.CommandText = sStr1

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                If par.IfNull(myReader1("ID_UNITA"), "-1") <> "-1" Then
                    lblTitolo.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(myReader2("CODICE"), "") & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & par.IfNull(myReader2("TITOLO"), "") & "</a>"
                    lblTitolo.Visible = True
                    'lblTitolo.Enabled = True
                    Label29.Visible = True
                    'lblRiporta.Visible = True
                    'lblRiporta.Attributes.Add("onclick", "javascript:document.getElementById('txtRichiedente').value=document.getElementById('intestatario').value;document.getElementById('txtNome').value=document.getElementById('intestatario1').value;")


                    par.cmd.CommandText = "select RAPPORTI_UTENZA.ID as IDC,ANAGRAFICA.* " _
                                       & " from SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA " _
                                       & " where SISCOM_MI.RAPPORTI_UTENZA.ID=SISCOM_MI.UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                       & " and   SISCOM_MI.getstatocontratto(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
                                       & " and   SISCOM_MI.ANAGRAFICA.ID=SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                                       & " and   SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                                       & " and   SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO=SISCOM_MI.UNITA_CONTRATTUALE.ID_CONTRATTO" _
                                       & " and   SISCOM_MI.UNITA_CONTRATTUALE.ID_UNITA=" & par.IfNull(myReader2("ID"), "0")

                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader3.Read
                        If par.IfNull(myReader3("RAGIONE_SOCIALE"), "") <> "" Then
                            lblIntestatario.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../../Contratti/Contratto.aspx?LT=1&ID=" & myReader3("IDC") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & par.IfNull(myReader3("RAGIONE_SOCIALE"), "") & "</a>"
                            lblIntestatario.Visible = True

                            txtIntestatario.Value = UCase(par.IfNull(myReader3("RAGIONE_SOCIALE"), ""))
                        Else
                            lblIntestatario.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../../Contratti/Contratto.aspx?LT=1&ID=" & myReader3("IDC") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & par.IfNull(myReader3("COGNOME"), "") & " " & par.IfNull(myReader3("NOME"), "") & "</a>"
                            txtIntestatario.Value = UCase(par.IfNull(myReader3("COGNOME"), ""))
                            txtIntestatario1.Value = UCase(par.IfNull(myReader3("NOME"), ""))
                            Me.lblIntestatario.Visible = True
                        End If
                    Loop
                    myReader3.Close()

                Else
                    lblTitolo.Text = par.IfNull(myReader2("TITOLO"), "")
                End If
            End If
            myReader2.Close()

            txtSTATO.Value = par.IfNull(myReader1("ID_STATO"), "")


            Me.lblDataIns.Text = par.FormattaData(Mid(par.IfNull(myReader1("DATA_ORA_RICHIESTA"), "                 "), 1, 8)) & " " & Mid(par.IfNull(myReader1("DATA_ORA_RICHIESTA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader1("DATA_ORA_RICHIESTA"), "          "), 11, 2)
            txtRichiedente.Text = par.IfNull(myReader1("COGNOME_RS"), "")
            txtNome.Text = par.IfNull(myReader1("nome"), "")
            txtTel1.Text = par.IfNull(myReader1("TELEFONO1"), "")
            txtTel2.Text = par.IfNull(myReader1("TELEFONO2"), "")
            txtemail.Text = par.IfNull(myReader1("MAIL"), "")
            txtDescrizione.Text = par.IfNull(myReader1("DESCRIZIONE_RIC"), "")
            Me.lblNum.Text = par.IfNull(myReader1("ID"), "")

            cmbFiliali.Items.Add(New ListItem("--", "-1"))
            par.cmd.CommandText = "SELECT ID,tab_filiali.nome FROM SISCOM_MI.tab_filiali "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While lettore.Read
                cmbFiliali.Items.Add(New ListItem(par.IfNull(lettore("nome"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()

            par.cmd.CommandText = "SELECT ID_OPERATORE,(OPERATORI.COGNOME ||' '||OPERATORI.NOME)AS OPERATORE,data_ora FROM SISCOM_MI.EVENTI_SEGNALAZIONI,OPERATORI where OPERATORI.ID = ID_OPERATORE AND cod_evento = 'F181'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                Me.lblInoltro.Text = "Inoltrata da: " & par.IfNull(lettore("operatore"), "") & " il " & par.FormattaData(lettore("data_ora"))
            End If
            lettore.Close()
            Me.cmbFiliali.SelectedValue = par.IfNull(myReader1("id_struttura"), -1)


            'CaricaTabellaNote(Me.txtIdSegnalazioni.Text)
            'txtNote.Text = par.IfNull(myReader1("NOTE"), "")
            'cmbTipo.Items.FindByValue(par.IfNull(myReader1("TIPO_RICHIESTA"), "0")).Selected = True


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub

    Private Sub VisualizzaDati()

        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        'Dim dlist As CheckBoxList
        Dim ds As New Data.DataSet()

        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdSegnalazione <> -1 Then
                ' LEGGO IMPIANTI

                par.cmd.CommandText = "select nvl(id_segnalazione_padre,0) from siscom_mi.segnalazioni where segnalazioni.id=" & vIdSegnalazione
                idSegnalazionePadre.Value = par.IfNull(par.cmd.ExecuteScalar, 0)

                par.cmd.CommandText = "select * from SISCOM_MI.SEGNALAZIONI where SISCOM_MI.SEGNALAZIONI.ID = " & vIdSegnalazione & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                    RiempiNoteChiusura(par.IfNull(myReader1("id_tipologie"), "-1"))
                    urgenzaOld.Value = par.IfNull(myReader1("id_pericolo_Segnalazione"), "0")
                    tipoOld.Value = par.IfNull(myReader1("id_tipo_segnalazione"), "")



                    tipoOld1.Value = par.IfNull(myReader1("id_tipo_Segn_livello_1"), "")
                    tipoOld2.Value = par.IfNull(myReader1("id_tipo_Segn_livello_2"), "")
                    tipoOld3.Value = par.IfNull(myReader1("id_tipo_Segn_livello_3"), "")
                    tipoOld4.Value = par.IfNull(myReader1("id_tipo_Segn_livello_4"), "")
                End If
                myReader1.Close()

                CaricaTabellaNote(vIdSegnalazione)




                'If txtSTATO.Value = 0 Then
                '    'Salva(1)
                'End If

                'FrmSolaLettura()
                SettaStato()
                SettaBottoni()

                Session.Add("LAVORAZIONE", "1")

            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
               
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    RadWindowManager1.RadAlert("Scheda Segnalazione aperta da un altro utente. Non è possibile effettuare modifiche!", 300, 150, "Attenzione", "", "null")
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.SEGNALAZIONI where SISCOM_MI.SEGNALAZIONI.ID = " & vIdSegnalazione
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If

                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                Me.txtDescNoteChiusura.Enabled = True
                SettaStato()

            Else
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)


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


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If txtannullo.Value = "1" Then
            txtannullo.Value = "0"
            If Salva(6) = True Then
                RadWindowManager1.RadAlert("Operazione completata correttamente!", 300, 150, "Attenzione", "", "null")

                'FrmSolaLettura()
                SettaStato()
                SettaBottoni()
            End If
        End If
    End Sub

    Protected Sub btnManutenzione_Click(sender As Object, e As System.EventArgs) Handles btnManutenzione.Click
        Try
            If txtannullo.Value = "1" Then

                txtannullo.Value = "0"

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Add("LAVORAZIONE", "0")


                '**************************


                Session.Add("ID", 0)
                sValoreStato = Request.QueryString("ST")
                Response.Write("<script>location.replace('Manutenzioni.aspx?CO=" & vIdSegnalazione _
                                                                        & "&ED=" & sValoreStato _
                                                                        & "&SE=" & "-" _
                                                                        & "&TIPOR=0" _
                                                                        & "&PROVENIENZA=SEGNALAZIONI&NASCONDIINDIETRO=1" & "');</script>")

                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")


            Else
                Dim Script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                If Not String.IsNullOrWhiteSpace(Script) Then
                    RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", Script, True)
                End If
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
  Protected Sub btnManutenzioneVis_Click(sender As Object, e As System.EventArgs) Handles btnManutenzioneVis.Click
        Try
            If txtannullo.Value = "1" Then

                txtannullo.Value = "0"

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Add("LAVORAZIONE", "0")


                '**************************

                Dim IDM As String = ""
                If Not IsNothing(Session.Item("IDMAN")) Then
                    IDM = Session.Item("IDMAN").ToString()
                    Session.Remove("IDMAN")
                End If


                Session.Add("ID", 0)
                sValoreStato = Request.QueryString("ST")

                If IDM <> "" Then
                    Response.Write("<script>location.replace('Manutenzioni.aspx?CO=" & vIdSegnalazione _
                                                                            & "&ED=" & sValoreStato _
                                                                            & "&SE=" & "-" _
                                                                            & "&TIPOR=0" _
                                                                           & "&IDM=" & IDM _
                                                                            & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                            & "&PROVENIENZA=SEGNALAZIONI&NASCONDIINDIETRO=1" & "');</script>")
                End If

                'Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")


            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        Try
            If txtModificato.Text <> "111" Then

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    'par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Add("LAVORAZIONE", "0")


                '**************************


                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")
                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
            Else
                txtModificato.Text = "1"

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

Protected Sub btnINDIETRO_Click(sender As Object, e As System.EventArgs) Handles btnINDIETRO.Click
        Try
            If txtModificato.Text <> "111" Then

                sValoreStato = Request.QueryString("ST")
                sOrdinamento = Request.QueryString("ORD")

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    'par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                Session.Add("LAVORAZIONE", "0")

                '**************************
                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")
                If txtindietro.Text = 1 Then
                    Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                Else

                    Dim tipo As String = Request.QueryString("TIPO")
                    If tipo = "NE" Then
                        Response.Write("<script>location.replace('RisultatiSegnNOEmesso.aspx?ST=" & sValoreStato & "&ORD=" & sOrdinamento & "&" & Session.Item("SEGNALAZIONIINDIETRO") & "');</script>")
                    End If
                    Response.Write("<script>location.replace('RisultatiSegn.aspx?ST=" & sValoreStato & "&ORD=" & sOrdinamento & "&" & Session.Item("SEGNALAZIONIINDIETRO") & "');</script>")
                End If

            Else
                txtModificato.Text = "1"
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)

        Me.btnSalva.Enabled = False
        btnSalvaSegnalazione.Enabled = False
        Me.btnManutenzione.Visible = False
        Me.btnManutenzioneVis.Enabled = False
        Me.btnChiudiSegnalazione.Enabled = False
        Me.imgInCarSoprall.Enabled = False

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

        Me.txtDescNoteChiusura.ReadOnly = False
        'Me.btnInoltra.Visible = False
    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
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



    Function Salva(ByVal Stato As Integer) As Boolean

        Try

            Salva = False

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Session.Add("LAVORAZIONE", "1")

            If Stato = 1 Then
                par.cmd.CommandText = "select * from siscom_mi.segnalazioni_sopralluogo where id_segnalazione = " & vIdSegnalazione
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    Stato = Stato
                Else
                    Stato = 0
                End If
                lettore.Close()
            End If

            Dim dataInCarico As String = ""
            If Stato = 6 Then
                dataInCarico = " , data_in_carico = '" & Format(Now, "yyyyMMdd") & "'"
            End If
            ' SEGNALAZIONI
            par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=" & Stato & dataInCarico & " where ID=" & vIdSegnalazione


            par.cmd.ExecuteNonQuery()
            WriteEvent("F02", "AGGIORNAMENTO SEGNALAZIONE")
            WriteEventSegnalazione("F233", "Modifica stato segnalazione", "APERTA", "IN CORSO", vIdSegnalazione)

            par.cmd.CommandText = ""
            If txtNote.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
                                    & "VALUES (" & vIdSegnalazione & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()
                txtNote.Text = ""
                WriteEvent("F02", "NOTA SEGNALAZIONE")

            End If

            '************************************

            par.myTrans.Commit() 'COMMIT

            txtSTATO.Value = Stato

            par.cmd.CommandText = "select * from SISCOM_MI.SEGNALAZIONI where SISCOM_MI.SEGNALAZIONI.ID = " & vIdSegnalazione & " FOR UPDATE NOWAIT"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                RiempiCampi(myReader1)
            End If
            myReader1.Close()

            SettaStato()
            Session.Add("LAVORAZIONE", "1")

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Me.txtModificato.Text = "0"

            Salva = True


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

    End Function

    Protected Sub WriteEventSegnalazione(ByVal cod As String, ByVal motivo As String, Optional ByVal valoreVecchio As String = "", Optional ByVal valoreNuovo As String = "", Optional idSegn As Integer = 0)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                connOpNow = True
            End If
            If idSegn = 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegn & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegn & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
                par.cmd.ExecuteNonQuery()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Sub SettaStato()

        Select Case txtSTATO.Value
            Case 0
                Me.lblSTATO.Text = "SEGNALAZIONE APERTA"
                Me.btnManutenzioneVis.Enabled = False

            Case 1
                Me.lblSTATO.Text = "SEGNALAZIONE IN SOPRALLUOGO"
                Me.btnSalva.Enabled = False
                'Me.imgInCarSoprall.Visible = False
                Me.btnManutenzioneVis.Enabled = False
                Me.btnSalva.Enabled = False
            Case 2
                Me.lblSTATO.Text = "SEGNALAZIONE ANNULLATA"
                Me.btnSalva.Enabled = False
                Me.imgInCarSoprall.Enabled = False
                Me.btnManutenzioneVis.Enabled = False
                Me.btnSalva.Enabled = False
            Case 3
                Me.lblSTATO.Text = "SEGNALAZIONE IN CARICO"
                Me.btnSalva.Enabled = False
                Me.imgInCarSoprall.Enabled = False
                Me.btnManutenzioneVis.Enabled = False
            Case 4
                Me.lblSTATO.Text = "SEGNALAZIONE con ORDINE EMESSO"
                Me.btnSalva.Enabled = False
                Me.imgInCarSoprall.Enabled = False
                Me.btnManutenzione.Enabled = True
                Me.btnSalva.Enabled = False
                Me.btnChiudiSegnalazione.Enabled = False

            Case 5
                Me.lblSTATO.Text = "SEGNALAZIONE con RICHIESTA RESPINTA"
                Me.btnSalva.Enabled = False
                Me.imgInCarSoprall.Enabled = False
                Me.btnManutenzioneVis.Enabled = False
                Me.btnSalva.Enabled = False
            Case 6
                Me.lblSTATO.Text = "SEGNALAZIONE IN CORSO"
                Me.btnSalva.Enabled = False
                'Me.imgInCarSoprall.Enabled = False
                Me.imgInCarSoprall.Enabled = True
                Me.btnManutenzione.Enabled = True
                Me.btnManutenzioneVis.Enabled = True
                Me.btnSalva.Enabled = False
                Me.btnChiudiSegnalazione.Enabled = True

            Case 7
                Me.lblSTATO.Text = "SEGNALAZIONE EVASA"
                Me.btnSalva.Enabled = False
                'Me.imgInCarSoprall.Enabled = False
                Me.imgInCarSoprall.Enabled = True
                Me.btnManutenzione.Enabled = False
                Me.btnManutenzioneVis.Enabled = True
                Me.btnSalva.Enabled = False
                Me.btnChiudiSegnalazione.Enabled = False

            Case 10
                Me.lblSTATO.Text = "SEGNALAZIONE CHIUSA"
                Me.btnSalva.Enabled = False
                Me.imgInCarSoprall.Enabled = False
                Me.btnManutenzioneVis.Enabled = False
                Me.btnChiudiSegnalazione.Enabled = False
                Me.btnSalva.Enabled = False

        End Select
        If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
            Me.btnSalva.Enabled = False
        End If
    End Sub

    Sub SettaBottoni()
        Try
            Select Case txtSTATO.Value
                Case 0
                    'Me.lblSTATO.Text = "SEGNALAZIONE APERTA"
                    Me.btnSalva.Enabled = True
                    Me.btnManutenzione.Enabled = True
                    Me.btnManutenzioneVis.Enabled = False
                    Me.btnChiudiSegnalazione.Enabled = True
                Case 1
                    'Me.lblSTATO.Text = "SEGNALAZIONE PRESA IN CARICO"
                    Me.btnSalva.Enabled = False
                    Me.btnManutenzione.Enabled = True
                    Me.btnManutenzioneVis.Enabled = False
                    Me.btnChiudiSegnalazione.Enabled = True
                    'Me.btnInoltra.Visible = False

                Case 2
                    'Me.lblSTATO.Text = "SEGNALAZIONE ANNULLATA"
                    Me.btnSalva.Enabled = False
                    Me.btnManutenzione.Enabled = False
                    Me.btnManutenzioneVis.Enabled = False
                    Me.btnChiudiSegnalazione.Enabled = False
                    'Me.btnInoltra.Visible = False

                Case 3
                    'Me.lblSTATO.Text = "SEGNALAZIONE IN VERIFICA"
                    Me.btnSalva.Enabled = False
                    imgInCarSoprall.Enabled = True
                    Me.btnManutenzione.Enabled = True
                    Me.btnManutenzioneVis.Enabled = False
                    Me.btnChiudiSegnalazione.Enabled = True
                    'Me.btnInoltra.Visible = False
                Case 4
                    'Me.lblSTATO.Text = "SEGNALAZIONE con ORDINE EMESSO"
                    Me.btnSalva.Enabled = False
                    'Me.btnManutenzione.Visible = False
                    Me.btnManutenzione.Enabled = True
                    Me.btnManutenzioneVis.Enabled = True
                    Me.btnChiudiSegnalazione.Enabled = False
                    'Me.btnInoltra.Visible = False

                Case 5
                    'Me.lblSTATO.Text = "SEGNALAZIONE con RICHIESTA RESPINTA"
                    Me.btnSalva.Enabled = False
                    Me.btnManutenzione.Enabled = False
                    Me.btnManutenzioneVis.Enabled = False
                    Me.btnChiudiSegnalazione.Enabled = False
                    'Me.btnInoltra.Visible = False

                Case 10
                    'Me.lblSTATO.Text = "SEGNALAZIONE CHIUSA"
                    Me.btnSalva.Enabled = False
                    Me.btnManutenzione.Enabled = False
                    Me.btnManutenzioneVis.Enabled = True
                    Me.btnChiudiSegnalazione.Enabled = False
                    'Me.btnInoltra.Visible = False

            End Select
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                Me.btnSalva.Enabled = False
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Sub CaricaTabellaNote(ByVal IdNota As String)

        Dim S1 As String
        Dim FlagConnessione As Boolean

        TabellaNote.Text = "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='100px'>DATA-ORA</td><td width='150px'>OPERATORE</td><td>NOTE</td></tr>"

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            par.cmd.CommandText = "select SEGNALAZIONI_NOTE.*,operatori.operatore " _
                               & " from SEPA.operatori,siscom_mi.SEGNALAZIONI_NOTE " _
                               & " where SEGNALAZIONI_NOTE.ID_segnalazione=" & IdNota _
                               & "   and SEGNALAZIONI_NOTE.id_operatore=operatori.id (+) " _
                               & " order by data_ora desc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                If Strings.Left(par.IfNull(myReader1("note"), ""), 25) = "<a href='../CICLO_PASSIVO" Then
                    S1 = "<a href='StampaOrdine.aspx?" & Strings.Mid(par.IfNull(myReader1("note"), ""), 71)

                Else
                    S1 = par.IfNull(myReader1("note"), "")
                End If
                TabellaNote.Text = TabellaNote.Text & "<tr style='height: 20px;font-family: ARIAL; font-size: 8pt;'>" _
                                                & " <td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & par.FormattaData(Mid(par.IfNull(myReader1("data_ora"), ""), 1, 8)) & " " & Mid(par.IfNull(myReader1("data_ora"), ""), 9, 2) & ":" & Mid(par.IfNull(myReader1("data_ora"), ""), 11, 2) & "</td>" _
                                                & " <td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='150px'>" & par.IfNull(myReader1("operatore"), "") & "</td>" _
                                                & " <td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;'>" & Replace(S1, vbCrLf, "</br>") & "</td></tr>"
            End While
            myReader1.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try

        TabellaNote.Text = TabellaNote.Text & "</table>"
    End Sub

    Protected Sub btn_Chiudi1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi1.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub
 Protected Sub btn_Inserisci1_Click(sender As Object, e As System.EventArgs) Handles btn_Inserisci1.Click

        Try

            If par.IfEmpty(Me.txtDescNoteChiusura.Text, "Null") = "Null" Then
                RadWindowManager1.RadAlert("Inserire la motivazione!", 300, 150, "Attenzione", "", "null")
                Me.txtAppare1.Value = "1"
                Exit Sub
            End If
            If par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") >= par.AggiustaData(Me.lblDataIns.Text.Substring(0, 10)) & Me.lblDataIns.Text.Substring(11, 5).Replace(":", "").Replace(".", "") Then


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")

                ' SEGNALAZIONI
                'par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=10, DATA_CHIUSURA = '" & Format(Now, "yyyyMMddHHmm") & "' where ID=" & Me.txtIdSegnalazioni.Text
                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10, DATA_CHIUSURA = '" & par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") & "' where id=" & txtIdSegnalazioni.Text

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '************************************
                WriteEvent("F02", "SEGNALAZIONE CHIUSA")


                par.cmd.CommandText = "insert into SISCOM_MI.SEGNALAZIONI_NOTE " _
                            & " (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE, ID_TIPO_SEGNALAZIONE_note) values " _
                            & " (" & Me.txtIdSegnalazioni.Text & ", '" & par.PulisciStrSql(txtDescNoteChiusura.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""


                par.myTrans.Commit() 'COMMIT
                Me.txtModificato.Text = 0
                txtSTATO.Value = 10

                'FrmSolaLettura()
                SettaStato()
                SettaBottoni()

                Me.btnManutenzioneVis.Enabled = False

                CaricaTabellaNote(Me.txtIdSegnalazioni.Text)

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                RadNotificationNote.Text = "Segnalazione chiusa"
                RadNotificationNote.Show()
            Else
                RadWindowManager1.RadAlert("Attenzione...\nLa data e l\'ora di chiusura deve essere successiva a quella di apertura!\nImpossibile Procedere", 300, 150, "Attenzione", "", "null")
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
 Protected Sub btnChiudiEF_Click(sender As Object, e As System.EventArgs) Handles btnChiudiEF.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub



    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaEsercizio()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE , SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                               & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                               & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                               & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID "


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                Me.cmbEsercizio.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))

            End While
            myReader1.Close()
            'par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO")

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            Me.cmbEsercizio.Enabled = True
            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                'Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                '  Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)


                Me.cmbServizio.Enabled = True
                Me.cmbServizioVoce.Enabled = True
                Me.cmbAppalto.Enabled = True
                CaricaServizi()
            End If



        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        CaricaServizi()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub


    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaServizi()
        Dim FlagConnessione As Boolean

        Try

            Me.cmbServizio.Items.Clear()
            ' Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            '    Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbEsercizio.SelectedValue <> "-1" Then

                ' APRO CONNESSIONE
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If



                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                End If

                Me.cmbServizio.Items.Clear()
                '  Me.cmbServizio.Items.Add(New ListItem(" ", -1))


                Select Case Me.tipo.Value
                    Case "C"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "


                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                               & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                               & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                     & " where ID_COMPLESSO=" & Me.identificativo.Value & ") ) ) " _
                                          & " order by DESCRIZIONE asc"

                    Case "E"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                               & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                               & " where ID_EDIFICIO=" & Me.identificativo.Value & ") ) " _
                                          & " order by DESCRIZIONE asc"

                    Case "U"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select
                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                                   & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                         & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & Me.identificativo.Value & ")))) " _
                                          & " order by DESCRIZIONE asc"
                    Case Else

                        'par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                        '                   & " from SISCOM_MI.TAB_SERVIZI " _
                        '                   & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                        '                                & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                        '                                & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                        '                                                                   & " from SISCOM_MI.LOTTI " _
                        '                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                        '                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        'Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                        '    Case 6
                        '        If Session.Item("FL_COMI") <> 1 Then
                        '            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        '        End If
                        '    Case 7
                        '        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        'End Select
                        'par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                        '                                          & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                        '                                                                           & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                                           & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                        '                                                                                                 & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & Me.identificativo.Value & ")))) " _
                        '                  & " order by DESCRIZIONE asc"

                End Select
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)

                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                'End While
                'myReader1.Close()

                Me.cmbServizio.SelectedValue = "-1"
                '**************************

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub cmbServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged
        FiltraDettaglio()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub FiltraDettaglio()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean


        Try


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbServizioVoce.Items.Clear()
            '  Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            '  Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizio.SelectedValue <> "-1" Then


                ' APRO CONNESSIONE
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If



                par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                    & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                     & "where  PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                  & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                  & " where  ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )" _
                                                                  & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO<>15" _
                                     & "  and PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                       & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")"

                Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & "  and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                     & " order by DESCRIZIONE asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                '    i = i + 1
                'End While
                'myReader1.Close()
                '**************************
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizioVoce, "ID", "DESCRIZIONE", True)
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                If i = 2 Then
                    cmbServizioVoce.Items(1).Selected = True
                    FiltraAppalti()
                End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraAppalti()
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                par.cmd.CommandText = " select ID,TRIM(NUM_REPERTORIO) AS NUM_REPERTORIO,TRIM(NUM_REPERTORIO) || ' - ' || (select ragione_sociale from siscom_mi.fornitori where fornitori.id=appalti.id_fornitore) as fornitore " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                 & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                 & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
                                    & "  and ID_STATO=1" _
                                    & " order by NUM_REPERTORIO "

            Else

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Exit Sub
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "FORNITORE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " ") & "-" & par.IfNull(myReader1("FORNITORE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub cmbServizioVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizioVoce.SelectedIndexChanged
        FiltraAppalti()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub


    'RICAVA LO STATO DELL'ESERCIZIO SELEZIONATO (5,6,7)
    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try

            Me.txtSTATO_PF.Value = -1

            If ID_ESERCIZIO < 0 Then Exit Sub

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                Me.txtSTATO_PF.Value = par.IfNull(myReaderF("ID_STATO"), -1)
            End If
            myReaderF.Close()

            par.cmd.Parameters.Clear()

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            ' Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub imgInCarSoprall_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgInCarSoprall.Click
        Dim connOpNow As Boolean = False
        Dim inSopral As Boolean = False

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                connOpNow = True
            End If

            par.cmd.CommandText = "select * from siscom_mi.SEGNALAZIONI_SOPRALLUOGO where id_segnalazione = " & vIdSegnalazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.HasRows Then
                inSopral = True
            End If

            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
        If inSopral = True Then
            Salva(6)
        End If


    End Sub

    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                connOpNow = True
            End If


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES ( " & vIdSegnalazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
            & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"

            par.cmd.ExecuteNonQuery()

            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


        End Try
    End Sub


    Protected Sub btnSalvaInoltra_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaInoltra.Click
        Dim connOpNow As Boolean = False

        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            ''CREO LA TRANSAZIONE
            'par.myTrans = par.OracleConn.BeginTransaction()
            '''par.cmd.Transaction = par.myTrans
            'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            par.cmd.CommandText = "update siscom_mi.segnalazioni set id_struttura = " & Me.cmbFiliali.SelectedValue & " where id = " & vIdSegnalazione
            par.cmd.ExecuteNonQuery()
            WriteEvent("F181", "")
            RadWindowManager1.RadAlert("Operazione eseguita correttamente!", 300, 150, "Attenzione", "", "null")


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


        End Try

    End Sub
    Private Sub RiempiNoteChiusura(ByVal idTipoIntervento As String)
        Dim ConnOpenNow As Boolean = False

        Try

            If idTipoIntervento <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    ConnOpenNow = True
                End If

                cmbNoteChiusura.Items.Add(New ListItem("--", "-1"))

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_NOTE_CHIUSURA"
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While Lettore.Read
                    cmbNoteChiusura.Items.Add(New ListItem(par.IfNull(Lettore("descrizione"), "--"), par.IfNull(Lettore("descrizione"), "-1")))

                End While
                Lettore.Close()

                If ConnOpenNow = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If


            End If
        Catch ex As Exception

            If ConnOpenNow = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


        End Try
    End Sub


    'Protected Sub btnInoltra_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnInoltra.Click
    '    If confRespinta.Value = 1 Then

    '        Try
    '            ' RIPRENDO LA CONNESSIONE
    '            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)

    '            par.cmd.CommandText = "update siscom_mi.segnalazioni set id_stato = 5 where id = " & vIdSegnalazione
    '            par.cmd.ExecuteNonQuery()
    '            WriteEvent("F02", "SEGNALAZIONE RESPINTA")


    '            If txtNote.Text <> "" Then
    '                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
    '                                    & "VALUES (" & vIdSegnalazione & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
    '                par.cmd.ExecuteNonQuery()
    '                txtNote.Text = ""
    '                WriteEvent("F02", "NOTA SEGNALAZIONE")

    '            End If

    '            Response.Write("<script>alert('Operazione eseguita correttamente')</script>")

    '            VisualizzaDati()
    '        Catch ex As Exception
    '            If par.OracleConn.State = Data.ConnectionState.Open Then
    '                '*********************CHIUSURA CONNESSIONE**********************
    '                par.cmd.Dispose()
    '                par.OracleConn.Close()
    '                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            End If

    '            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '            Response.Write("<script>top.location.href='../Errore.aspx';</script>")


    '        End Try

    '    End If
    'End Sub



    

    

   

    

  

Protected Sub btnProcediEF_Click(sender As Object, e As System.EventArgs) Handles btnProcediEF.Click
        Try
            If Me.cmbEsercizio.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare  l\'esercizio finanaziario!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbServizio.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare il Servizio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbServizioVoce.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare la voce DGR!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbAppalto.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare il numero di repertorio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If


            If txtannullo.Value = "1" Then

                txtannullo.Value = "0"

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Add("LAVORAZIONE", "0")


                '**************************

                Session.Add("ID", 0)
                sValoreStato = Request.QueryString("ST")
                Response.Write("<script>location.replace('Manutenzioni.aspx?SE=" & Me.cmbServizio.SelectedValue.ToString _
                                                                        & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                        & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                        & "&CO=" & vIdSegnalazione _
                                                                        & "&TIPOR=0" _
                                                                        & "&ED=" & sValoreStato _
                                                                        & "&NUOVA=1 " _
                                                                        & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                        & "&PROVENIENZA=SEGNALAZIONI&NASCONDIINDIETRO=1" & "');</script>")

                'Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")


            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnSalvaSegnalazione_Click(sender As Object, e As System.EventArgs) Handles btnSalvaSegnalazione.Click
        If cmbTipoSegn.SelectedValue <> "-1" Then
            Try
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()

                Dim tipoSegnalazione1 As String = ""
                If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> -1 Then
                    tipoSegnalazione1 = ",id_tipo_segn_livello_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                End If
                Dim tipoSegnalazione2 As String = ""
                If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> -1 Then
                    tipoSegnalazione2 = ",id_tipo_segn_livello_2=" & cmbTipoSegnalazioneLivello2.SelectedValue
                Else
                    tipoSegnalazione2 = ",id_tipo_segn_livello_2=NULL"
                End If
                Dim tipoSegnalazione3 As String = ""
                If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> -1 Then
                    tipoSegnalazione3 = ",id_tipo_segn_livello_3=" & cmbTipoSegnalazioneLivello3.SelectedValue
                Else
                    tipoSegnalazione3 = ",id_tipo_segn_livello_3=NULL"
                End If
                Dim tipoSegnalazione4 As String = ""
                If IsNumeric(cmbTipoSegnalazioneLivello4.SelectedValue) AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> -1 Then
                    tipoSegnalazione4 = ",id_tipo_segn_livello_4=" & cmbTipoSegnalazioneLivello4.SelectedValue
                Else
                    tipoSegnalazione4 = ",id_tipo_segn_livello_4=NULL"
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET " _
                    & " ID_PERICOLO_SEGNALAZIONE=" & cmbUrgenza.SelectedIndex _
                    & tipoSegnalazione1 _
                    & tipoSegnalazione2 _
                    & tipoSegnalazione3 _
                    & tipoSegnalazione4 _
                    & " WHERE ID=" & vIdSegnalazione
                par.cmd.ExecuteNonQuery()
                Dim testo As String = ""
                If cmbUrgenza.SelectedIndex <> urgenzaOld.Value _
                    Or cmbTipoSegn.SelectedValue <> tipoOld.Value _
                    Or cmbTipoSegnalazioneLivello1.SelectedValue <> tipoOld1.Value _
                    Or cmbTipoSegnalazioneLivello2.SelectedValue <> tipoOld2.Value _
                    Or cmbTipoSegnalazioneLivello3.SelectedValue <> tipoOld3.Value _
                    Or cmbTipoSegnalazioneLivello4.SelectedValue <> tipoOld4.Value Then
                    If cmbUrgenza.SelectedIndex <> urgenzaOld.Value Then
                        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.PERICOLO_sEGNALAZIONI WHERE ID=" & urgenzaOld.Value
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            testo &= "-Vecchio valore criticità:" & par.IfNull(lettore("descrizione"), "")
                        End If
                        lettore.Close()
                    End If
                    If cmbTipoSegn.SelectedValue <> tipoOld.Value Then
                        par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE ID=" & tipoOld.Value
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            testo &= "-Vecchio valore categoria segnalazione:" & par.IfNull(lettore("descrizione"), "")
                        End If
                        lettore.Close()
                    End If

                    If cmbTipoSegnalazioneLivello1.SelectedValue <> tipoOld1.Value AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                        If IsNumeric(tipoOld1.Value) Then
                            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID=" & tipoOld1.Value
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                testo &= "-Vecchio valore categoria 1 segnalazione:" & par.IfNull(lettore("descrizione"), "")
                            End If
                            lettore.Close()
                        Else
                            testo &= "-Vecchio valore categoria 1 segnalazione: / "
                        End If
                    End If

                    If cmbTipoSegnalazioneLivello2.SelectedValue <> tipoOld2.Value AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
                        If IsNumeric(tipoOld2.Value) Then
                            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID=" & tipoOld2.Value
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                testo &= "-Vecchio valore categoria 2 segnalazione:" & par.IfNull(lettore("descrizione"), "")
                            End If
                            lettore.Close()
                        Else
                            testo &= "-Vecchio valore categoria 2 segnalazione: / "
                        End If
                    End If

                    If cmbTipoSegnalazioneLivello3.SelectedValue <> tipoOld3.Value AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> -1 Then
                        If IsNumeric(tipoOld3.Value) Then
                            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID=" & tipoOld3.Value
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                testo &= "-Vecchio valore categoria 3 segnalazione:" & par.IfNull(lettore("descrizione"), "")
                            End If
                            lettore.Close()
                        Else
                            testo &= "-Vecchio valore categoria 3 segnalazione: / "
                        End If
                    End If

                    If cmbTipoSegnalazioneLivello4.SelectedValue <> tipoOld4.Value AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" Then
                        If IsNumeric(tipoOld4.Value) Then
                            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID=" & tipoOld4.Value
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                testo &= "-Vecchio valore categoria 4 segnalazione:" & par.IfNull(lettore("descrizione"), "")
                            End If
                            lettore.Close()
                        Else
                            testo &= "-Vecchio valore categoria 4 segnalazione: / "
                        End If
                    End If
                End If
                If Trim(testo) <> "" Then
                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE ( " _
                            & " ID_SEGNALAZIONE, NOTE, DATA_ORA,  " _
                            & " ID_OPERATORE, SOLLECITO)  " _
                            & " VALUES (" & vIdSegnalazione & ", " _
                            & " '" & par.PulisciStrSql("Modifica segnalazione " & testo) & "', " _
                            & " '" & Format(Now, "yyyyMMddHHmm") & "', " _
                            & Session.Item("ID_OPERATORE") & ", " _
                            & "0)"
                    par.cmd.ExecuteNonQuery()
                End If
                If Trim(txtNote.Text) <> "" Then
                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE ( " _
                    & " ID_SEGNALAZIONE, NOTE, DATA_ORA,  " _
                    & " ID_OPERATORE, SOLLECITO)  " _
                    & " VALUES (" & vIdSegnalazione & ", " _
                    & " '" & par.PulisciStrSql(txtNote.Text) & "', " _
                    & " '" & Format(Now, "yyyyMMddHHmm") & "', " _
                    & Session.Item("ID_OPERATORE") & ", " _
                    & "0)"
                    par.cmd.ExecuteNonQuery()
                End If
                par.myTrans.Commit()
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                ' RadWindowManager1.RadAlert("Operazione effettuata correttamente!", 300, 150, "Attenzione", "(location.href='Segnalazioni.aspx" & Request.Url.Query & "';)", "null")
                Response.Write("<script>alert('Operazione effettuata correttamente!');location.href='Segnalazioni.aspx" & Request.Url.Query & "';</script>")
            Catch ex As Exception
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        Else
            RadWindowManager1.RadAlert("Il tipo intervento è obbligatorio", 300, 150, "Attenzione", "", "null")

        End If
    End Sub

Private Sub CaricaTipologieLivello1()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegn.SelectedValue & " ORDER BY DESCRIZIONE"
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
            If cmbTipoSegnalazioneLivello1.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello1.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello1.Items.Remove(cmbTipoSegnalazioneLivello1.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello2()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello2, "ID", "DESCRIZIONE", True)
            If cmbTipoSegnalazioneLivello2.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello2.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello2.Items.Remove(cmbTipoSegnalazioneLivello2.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello3()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            If cmbTipoSegnalazioneLivello3.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello3.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello3.Items.Remove(cmbTipoSegnalazioneLivello3.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello4()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            If cmbTipoSegnalazioneLivello4.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello4.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello4.Items.Remove(cmbTipoSegnalazioneLivello4.Items.FindItemByValue("-1"))
                End If
            End If
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        CaricaTipologieLivello2()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        CaricaTipologieLivello3()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        CaricaTipologieLivello4()
    End Sub
    Private Sub CaricaTipoSegnalazione()
        Try
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE ORDER BY ID", cmbTipoSegn, "ID", "DESCRIZIONE")
            cmbTipoSegnalazioneLivello1.Items.Clear()
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub







    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Not IsNothing(Request.QueryString("NASCONDIINDIETRO")) And Not String.IsNullOrEmpty(Request.QueryString("NASCONDIINDIETRO")) Then
            If Request.QueryString("NASCONDIINDIETRO") = "1" Then
                btnINDIETRO.Visible = False
            End If
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Blocca_SbloccaMenu(1);", True)
    End Sub



#Region "Sopralluogo"
    Private Sub CaricaDatiSopralluogo()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select * from siscom_mi.segnalazioni_sopralluogo where id_segnalazione = " & idSegnalazione.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Me.txtTecnico.Text = par.IfNull(lettore("TECNICO"), "")
                Me.txtRapporto.Text = par.IfNull(lettore("RAPPORTO"), "")
                Me.rdbPericolo.SelectedValue = par.IfNull(lettore("FL_PERICOLO"), 0)
            Else
                Me.rdbPericolo.SelectedValue = 0
            End If
            lettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Protected Sub btnSalvaSopralluogo_Click(sender As Object, e As System.EventArgs) Handles btnSalvaSopralluogo.Click
        Try


            If Not String.IsNullOrEmpty(Me.txtTecnico.Text) Or Not String.IsNullOrEmpty(Me.txtRapporto.Text) Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Dim dataS As String = ""
                If IsDate(Me.txtDataS.Text) AndAlso Len(Me.txtDataS.Text) = 10 Then
                    dataS = par.AggiustaData(Me.txtDataS.Text)
                End If
                par.cmd.CommandText = "select id from siscom_mi.segnalazioni_sopralluogo where id_segnalazione = " & idSegnalazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO SET " _
                                        & "TECNICO = '" & par.PulisciStrSql(Me.txtTecnico.Text.ToUpper) & "', " _
                                        & "RAPPORTO = '" & par.PulisciStrSql(Me.txtRapporto.Text.ToUpper) & "', " _
                                        & "FL_PERICOLO = " & Me.rdbPericolo.SelectedValue _
                                        & ",DATA_SOPRALLUOGO = '" & dataS & "' WHERE ID = " & par.IfNull(lettore("ID"), -1)
                    par.cmd.ExecuteNonQuery()

                    WriteEvent("F02", "AGGIORNAMENTO DATI SOPRALLUOGO")

                Else
                    par.cmd.CommandText = "insert into siscom_mi.SEGNALAZIONI_SOPRALLUOGO(id,id_segnalazione,tecnico,rapporto,fl_pericolo,data_sopralluogo) values " _
                                        & "(siscom_mi.seq_segnalazioni_sopralluogo.nextval," & idSegnalazione.Value & ",'" & par.PulisciStrSql(Me.txtTecnico.Text.ToUpper) _
                                        & "','" & par.PulisciStrSql(Me.txtRapporto.Text.ToUpper) & "'," & Me.rdbPericolo.SelectedValue & ",'" & par.AggiustaData(Me.txtDataS.Text) & "')"

                    par.cmd.ExecuteNonQuery()

                    WriteEvent("F55", "DATI SOPRALLUOGO")

                End If
                lettore.Close()



                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                Dim script As String = "function f(){$find(""" + RadWindowSopralluogo.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Else
                Response.Write("<script>alert('Definire almeno il nome del tecnico o il rapporto del sopralluogo!')</script>")

            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnStampaSopralluogo_Click(sender As Object, e As System.EventArgs) Handles btnStampaSopralluogo.Click
        'Response.Write("<script>alert('Funzione non disponibile!')</script>")
        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\SopralluogoCallC.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim richiesta As String = ""
            Dim note As String = ""
            Dim condominio As String = ""
            Dim gestAuto As String = ""
            Dim sfratto As String = ""
            Dim morosità As String = ""
            Dim sloggio As String = ""
            Dim idContratto As String = ""
            par.cmd.CommandText = "select * from siscom_mi.segnalazioni where id = " & idSegnalazione.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then

                contenuto = Replace(contenuto, "$nrichiesta$", par.IfNull(lettore("ID"), ""))
                contenuto = Replace(contenuto, "$datarichiesta$", par.FormattaData(Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "                 "), 1, 8)))
                contenuto = Replace(contenuto, "$descrizione$", par.IfNull(lettore("descrizione_ric"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(lettore("COGNOME_RS"), "") & " " & par.IfNull(lettore("NOME"), ""))


                contenuto = Replace(contenuto, "$numerotelefono1$", par.IfNull(lettore("TELEFONO1"), ""))
                contenuto = Replace(contenuto, "$numerotelefono2$", par.IfNull(lettore("TELEFONO2"), ""))


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO WHERE ID_SEGNALAZIONE = " & idSegnalazione.Value
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader.Read Then

                    contenuto = Replace(contenuto, "$rapporto$", par.IfNull(myreader("rapporto"), ""))
                    contenuto = Replace(contenuto, "$tecnico$", par.IfNull(myreader("tecnico"), ""))

                    If myreader("fl_pericolo") = 1 Then
                        contenuto = Replace(contenuto, "$pericolo$", "SI")
                    ElseIf myreader("fl_pericolo") = 0 Then
                        contenuto = Replace(contenuto, "$pericolo$", "NO")
                    Else
                        contenuto = Replace(contenuto, "$pericolo$", "")

                    End If
                Else
                    contenuto = Replace(contenuto, "$pericolo$", "")
                    contenuto = Replace(contenuto, "$rapporto$", "&nbsp; ")
                    contenuto = Replace(contenuto, "$tecnico$", "")

                End If
                myreader.Close()

                If par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "1" Then
                    richiesta = "SEGNALAZIONE GUASTI"
                    par.cmd.CommandText = "select descrizione from siscom_mi.tipologie_guasti where id = " & par.IfNull(lettore("id_tipologie"), "0")
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        richiesta = richiesta & " - " & par.IfNull(myreader("descrizione"), "")
                    End If
                    myreader.Close()
                ElseIf par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "0" Then
                    richiesta = "RICHIESTA INFORMAZIONI"

                End If
                contenuto = Replace(contenuto, "$tiporichiesta$", richiesta)

                par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = " & idSegnalazione.Value
                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    note = note & par.IfNull(myreader("note"), "") & "<br/>"
                End While
                myreader.Close()
                contenuto = Replace(contenuto, "$note$", note)

                Dim indirizzo As String = ""
                par.cmd.CommandText = "SELECT COD_EDIFICIO,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = " & par.IfNull(lettore("id_edificio"), "0")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = par.IfNull(myreader("denominazione"), "")
                    contenuto = Replace(contenuto, "$edificio$", "EDIFICIO COD." & par.IfNull(myreader("cod_edificio"), ""))
                End If
                myreader.Close()

                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.cod_unita_immobiliare, UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS piano,SCALE_EDIFICI.descrizione AS SCALA, " _
                                    & "siscom_mi.Getintestatari(id_contratto) AS intestatario " _
                                    & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO, siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita(+)" _
                                    & "AND UNITA_IMMOBILIARI.ID = " & par.IfNull(lettore("id_unita"), 0)
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = indirizzo & " " & "SCALA: " & par.IfNull(myreader("SCALA"), "--") & " PIANO: " & par.IfNull(myreader("PIANO"), "--") & " INTERNO:" & par.IfNull(myreader("interno"), "--")
                    contenuto = Replace(contenuto, "$unita$", "U.I. cod." & par.IfNull(myreader("COD_UNITA_IMMOBILIARE"), ""))
                Else
                    contenuto = Replace(contenuto, "$unita$", "")

                End If
                myreader.Close()
                contenuto = Replace(contenuto, "$indirizzo$", indirizzo)

                par.cmd.CommandText = "Select nome from siscom_mi.tab_filiali Where id = " & par.IfNull(lettore("id_struttura"), "")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    contenuto = Replace(contenuto, "$struttura$", "STRUTTURA: " & par.IfNull(myreader("nome"), ""))
                End If
                myreader.Close()


                par.cmd.CommandText = "SELECT ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO, rapporti_utenza.data_riconsegna, siscom_mi.Getintestatari (id_contratto) AS intestatario, " _
                                    & "SISCOM_MI.Getstatocontratto(ID_CONTRATTO) AS STATO,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPO_CONTRATTO " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & "AND NVL(DATA_RICONSEGNA,'50000000')=(" _
                                    & "SELECT MAX(NVL(DATA_RICONSEGNA,'50000000')) " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & ")"

                myreader = par.cmd.ExecuteReader
                Dim datiCont As String = ""
                If myreader.Read Then
                    idContratto = par.IfNull(myreader("ID_CONTRATTO"), "")
                    datiCont = "CONTRATTO: " & par.IfNull(myreader("tipo_contratto"), "") & " " & par.IfNull(myreader("cod_contratto"), "") & " Stato Contratto: " & par.IfNull(myreader("stato"), "") & " Saldo: " & Format(par.CalcolaSaldoAttuale(par.IfNull(myreader("ID_CONTRATTO"), "0")), "##,##0.00") & " Euro "
                    contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myreader("intestatario"), ""))
                    If par.IfNull(myreader("data_riconsegna"), "") <> "" Then
                        sloggio = "SLOGGIO: " & par.FormattaData(par.IfNull(myreader("data_riconsegna"), ""))
                    End If
                Else
                    contenuto = Replace(contenuto, "$intestatario$", "")

                End If
                contenuto = Replace(contenuto, "$contratto$", datiCont)





                par.cmd.CommandText = "SELECT condomini.denominazione, (cond_amministratori.cognome || ' ' ||cond_amministratori.nome) AS amministratore " _
                                & "FROM siscom_mi.condomini,siscom_mi.cond_amministratori,siscom_mi.cond_amministrazione " _
                                & "WHERE condomini.ID =cond_amministrazione.id_condominio AND cond_amministratori.ID = id_amministratore AND cond_amministrazione.data_fine IS NULL " _
                                & "AND condomini.ID IN (SELECT id_condominio FROM siscom_mi.cond_edifici WHERE id_edificio = " & par.IfNull(lettore("id_edificio"), 0) & ")"

                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    condominio = condominio & "CONDOMINIO: " & par.IfNull(myreader("denominazione"), "") & " AMMINISTRATORE: " & par.IfNull(myreader("amministratore"), "")
                End While
                myreader.Close()

                contenuto = Replace(contenuto, "$condomino$", condominio)
                contenuto = Replace(contenuto, "$gestauto$", gestAuto)
                contenuto = Replace(contenuto, "$morosità$", morosità)
                contenuto = Replace(contenuto, "$sfratto$", sfratto)
                contenuto = Replace(contenuto, "$sloggio$", sloggio)

                If idContratto <> "" Then
                    par.cmd.CommandText = "SELECT ID_MOROSITA ,(CASE WHEN COD_STATO = 'M20' THEN 'SI' ELSE 'NO' END)AS PRATICA_LEGALE FROM SISCOM_MI.MOROSITA_LETTERE where  cod_stato not in ('M94','M98','M100') and id_contratto =" & idContratto
                    myreader = par.cmd.ExecuteReader

                    If myreader.Read Then

                        morosità = "MESSA IN MORA "
                        If par.IfNull(myreader("pratica_legale"), "NO") = "SI" Then
                            morosità = morosità & "- AVVIATA PRATICA LEGALE"
                        End If
                    End If
                    myreader.Close()
                End If


            End If
            lettore.Close()


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
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = idSegnalazione.Value & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\..\..\IMG\"))

            Response.Write("<script>window.open('../../../FileTemp/" & nomefile & ".pdf','','');</script>")

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim script As String = "function f(){$find(""" + RadWindowSopralluogo.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnChiudiSopralluogo_Click(sender As Object, e As System.EventArgs) Handles btnChiudiSopralluogo.Click
        Dim connOpNow As Boolean = False
        Dim inSopral As Boolean = False

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                connOpNow = True
            End If

            par.cmd.CommandText = "select * from siscom_mi.SEGNALAZIONI_SOPRALLUOGO where id_segnalazione = " & vIdSegnalazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.HasRows Then
                inSopral = True
            End If

            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
        If inSopral = True Then
            Salva(6)
        End If
    End Sub

    Private Sub btnManutenzioneVis1_Click(sender As Object, e As EventArgs) Handles btnManutenzioneVis1.Click
        Try


            txtannullo.Value = "0"

                'CHIUSURA DB
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Add("LAVORAZIONE", "0")


                '**************************

                Dim IDM As String = ""
                If Not IsNothing(Session.Item("IDMAN")) Then
                    IDM = Session.Item("IDMAN").ToString()
                    Session.Remove("IDMAN")
                End If


                Session.Add("ID", 0)
                sValoreStato = Request.QueryString("ST")

                If IDM <> "" Then
                Response.Write("<script>location.replace('Manutenzioni.aspx?CO=" & vIdSegnalazione _
                                                                        & "&ED=" & sValoreStato _
                                                                        & "&SE=" & "-" _
                                                                        & "&TIPOR=0" _
                                                                       & "&IDM=" & IDM _
                                                                        & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                        & "&PROVENIENZA=SEGNALAZIONI&NASCONDIINDIETRO=1" & "');</script>")
                End If

            'Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")



        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    'Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
    '    Dim connOpNow As Boolean = False

    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '            connOpNow = True
    '        End If


    '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
    '        & "VALUES ( " & idSegnalazione.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
    '        & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"

    '        par.cmd.ExecuteNonQuery()

    '        If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If


    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")


    '    End Try
    'End Sub
#End Region

   
    
    
  
End Class
