Imports System.Collections


Partial Class IMPIANTI_Imp_Idrico
    Inherits PageSetIdMode

    Dim par As New CM.Global
    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""

    Public TabberHide As String = ""

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sOrdinamento As String
    Public sVerifiche As String

    Public ID_TIPOLOGIA_USO As Integer

    Public sProvenienza As String

    Dim lstEdificiScale As System.Collections.Generic.List(Of Epifani.EdificiScale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        lstEdificiScale = CType(HttpContext.Current.Session.Item("LSTEDIFICISCALE"), System.Collections.Generic.List(Of Epifani.EdificiScale))


        If Not IsPostBack Then

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            sProvenienza = Request.QueryString("SL")

            vIdImpianto = 0
            vIdImpianto = Session.Item("ID")

            lstEdificiScale.Clear()


            ' CONNESSIONE DB
            lIdConnessione = Format(Now, "yyyyMMddHHmmss")

            'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
            Me.txtConnessione.Text = CStr(lIdConnessione)
            Me.txtIdImpianto.Text = "-1"

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                HttpContext.Current.Session.Add("SESSION_IMPIANTI", par.OracleConn)
            End If

            CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

            SettaggioCampi()

            If vIdImpianto <> 0 Then
                TabberHide = "tabbertab"
                VisualizzaDati()
                txtindietro.Text = 0

                'Visibile = "Visible =" & Chr(34) & "true" & Chr(34)
                'Visibile = "style=" & Chr(34) & "visibility:hidden" & Chr(34)
                Tabber1 = "tabbertabdefault"
            Else
                TabberHide = "tabbertabhide"
                Me.txtIdImpianto.Text = -1

                Me.btnElimina.Visible = False

                'Visibile = "style=" & Chr(34) & "visibility:false" & Chr(34) NOTA: non si riesce a rendere non visibile un tabber tab
                Tabber1 = "tabbertabdefault"
                txtindietro.Text = 1
            End If

            'Controllo modifica campi nel form
            Dim CTRL As Control

            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            For Each CTRL In Me.Tab_Idrico_Generale.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            For Each CTRL In Me.Tab_Idrico_Certificazioni.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next
            '*****************

            '*** CONTROLLO DATA
            txtAnnoRealizzazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'CType(Tab_Idrico_Generale.FindControl("txtDataAccensione"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Idrico.png';</script>")
            End If

            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 2, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If

        End If

            'CType(Tab_Idrico_Generale.FindControl("lblDataRiposo"), Label).Visible = False
            'CType(Tab_Idrico_Generale.FindControl("txtDataAccensione"), TextBox).Visible = False


    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
            Case "4"
                Tabber4 = "tabbertabdefault"
            Case "5"
                Tabber5 = "tabbertabdefault"
        End Select

        If vIdImpianto <> 0 Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
        End If


    End Sub

    Private Sub SettaggioCampi()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim i As Integer

        'CARICO COMBO COMPLESSI
        Dim gest As Integer
        gest = 0

        Try
            If gest > 0 Then
                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where substr(ID,1,1)= " & gest & " and ID<>1 order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 order by DENOMINAZIONE asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"



            '*** TIPOLOGIA_USO_TERMICI
            'par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_USO_IDRICI order by ID"
            'myReader1 = par.cmd.ExecuteReader
            ''cmbTipoUso.Items.Add(New ListItem(" ", -1))

            'While myReader1.Read
            '    cmbTipoUso.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'cmbTipoUso.Text = cmbTipoUso.Items(0).Text
            'myReader1.Close()

            dlist = CheckBoxTipologia

            da = New Oracle.DataAccess.Client.OracleDataAdapter("select * from SISCOM_MI.TIPOLOGIA_USO_IDRICI where ID<3 order by ID", par.OracleConn)
            da.Fill(ds)

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"
            dlist.DataBind()

            da.Dispose()
            da = Nothing

            dlist.DataSource = Nothing
            dlist = Nothing

            ds.Clear()
            ds.Dispose()
            ds = Nothing

            For i = 0 To CheckBoxTipologia.Items.Count - 1
                If CheckBoxTipologia.Items(i).Text = "IDRICO-SANITARIO" Then
                    CheckBoxTipologia.Items(i).Selected = True
                    Exit For
                End If
            Next
            '*********************+++

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        CaricaEdifici()
    End Sub

    Private Sub CaricaEdifici()
        Dim gest As Integer = 0

        Try

            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))

            If gest <> 0 Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI order by DENOMINAZIONE asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            DrLEdificio.SelectedValue = "-1"

            myReader1.Close()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbComplesso.SelectedValue <> "-1" Then

            FiltraEdifici()
        Else
            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))
        End If

    End Sub


    Protected Sub cmbComplesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Private Sub FiltraEdifici()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.cmbComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim gest As Integer = 0
                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))

                lstEdificiScale.Clear()

                '*** RIEMPIMENTO COMBO EDIFICI
                If gest <> 0 Then

                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE," _
                                                    & " (select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID ) " _
                                                    & " from SISCOM_MI.EDIFICI " _
                                                    & " where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"

                Else
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE," _
                                                    & " (select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID ) " _
                                                    & " from SISCOM_MI.EDIFICI " _
                                                    & " where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"

                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                    'Dim gen As Epifani.Edifici
                    'gen = New Epifani.Edifici(par.IfNull(myReader1("ID"), -1), par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1(2), 0), 0)

                    'lstEdifici.Add(gen)
                    'gen = Nothing
                End While
                myReader1.Close()
                '*******************************

                '*** RIEMPIMENTO LISTA EDIFICI-SCALE-U.I.

                par.cmd.CommandText = " select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE," _
                                            & "SISCOM_MI.SCALE_EDIFICI.ID as ID_SCALE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as SCALE, " _
                                            & " (select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_SCALA=SCALE_EDIFICI.ID ) " _
                                    & " from SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI  " _
                                    & " where  SISCOM_MI.EDIFICI.ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString & " and " _
                                            & "SISCOM_MI.EDIFICI.ID=SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO (+) " _
                                    & " order by DENOMINAZIONE,SCALE asc "

                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read
                    Dim gen As Epifani.EdificiScale
                    gen = New Epifani.EdificiScale(par.IfNull(myReader1("ID"), -1), par.IfNull(myReader1("ID_SCALE"), -1), par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("SCALE"), " "), par.IfNull(myReader1(4), 0), 0)

                    lstEdificiScale.Add(gen)
                    gen = Nothing
                End While
                myReader1.Close()



                ' LISTA EDIFICI CHECK BOX
                dlist = CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList)
                dlist.DataSource = lstEdificiScale

                dlist.DataTextField = "DescrizioneScalaNO_MQ"
                dlist.DataValueField = "ID_SCALA"
                dlist.DataBind()


                If FlagConnessione = True Then
                    par.OracleConn.Close()
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
        Dim SommaUI As Integer
        Dim i, j As Integer
        Dim ID_TIPOLOGIA As Integer


        '*** FORM PRINCIPALE
        Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

        Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
        FiltraEdifici()

        Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

        Me.txtDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
        Me.txtAnnoRealizzazione.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


        '*** I_IDRICI
        par.cmd.CommandText = "select * from SISCOM_MI.I_IDRICI where SISCOM_MI.I_IDRICI.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** FORM PRINCIPALE
            'Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")
            ID_TIPOLOGIA = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), 1)


            For i = 0 To CheckBoxTipologia.Items.Count - 1
                If CheckBoxTipologia.Items(i).Value = ID_TIPOLOGIA Or ID_TIPOLOGIA = 3 Then
                    CheckBoxTipologia.Items(i).Selected = True
                Else
                    CheckBoxTipologia.Items(i).Selected = False
                End If
            Next


            '*** TAB GENERALE
            CType(Tab_Idrico_Generale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(Tab_Idrico_Generale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")
            'CType(Tab_Idrico_Generale.FindControl("txtDataAccensione"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRIMA_ATTIVAZIONE"), ""))

            CType(Tab_Idrico_Generale.FindControl("cmbCompressore"), DropDownList).Items.FindByValue(par.IfNull(myReader2("COMPRESSORE"), "")).Selected = True

            CType(Tab_Idrico_Generale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

            '*** TAB CERTIFICAZIONI
            CType(Tab_Idrico_Certificazioni.FindControl("cmbLibretto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LIBRETTO"), "")).Selected = True
            CType(Tab_Idrico_Certificazioni.FindControl("cmbConformita"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF"), "")).Selected = True
            CType(Tab_Idrico_Certificazioni.FindControl("cmbISPESL"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_ISPESL"), "")).Selected = True
            CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRATICA_ISPESL"), ""))

            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

        End If
        myReader2.Close()


        '*** TAB GENERALE (EDIFICI)
        Dim lstEdificiScale As System.Collections.Generic.List(Of Epifani.EdificiScale)
        lstEdificiScale = CType(HttpContext.Current.Session.Item("LSTEDIFICISCALE"), System.Collections.Generic.List(Of Epifani.EdificiScale))

        par.cmd.CommandText = "select ID_SCALA from SISCOM_MI.IMPIANTI_SCALE where ID_IMPIANTO = " & vIdImpianto

        myReader2 = par.cmd.ExecuteReader()
        SommaUI = 0

        While myReader2.Read
            For i = 0 To CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
                If CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value = par.IfNull(myReader2("ID_SCALA"), "-1") Then
                    CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True

                    '************
                    For j = 0 To lstEdificiScale.Count - 1

                        If lstEdificiScale(j).ID_SCALA = par.IfNull(myReader2("ID_SCALA"), "-1") Then
                            SommaUI = SommaUI + lstEdificiScale(j).TOT_UNITA
                        End If

                    Next j
                End If
            Next
        End While
        myReader2.Close()

        CType(Tab_Idrico_Generale.FindControl("txtTotUI"), TextBox).Text = SommaUI
        '**************************


    End Sub


    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader      
        Dim ds As New Data.DataSet()
     
        Try
            'Apro la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdImpianto <> 0 Then
                ' LEGGO IMPIANTI (idrico)

                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Impianto aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If

                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

            Else
                'par.myTrans.Rollback()
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdImpianto = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If

    End Sub


    Public Function ControlloCampi() As Boolean
        Dim i As Integer

        ControlloCampi = True

        If Me.cmbComplesso.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare un Complesso Immobiliare!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If par.IfEmpty(Me.txtDenominazione.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Denominazione Impianto!');</script>")
            ControlloCampi = False
            txtDenominazione.Focus()
            Exit Function
        End If

        'If par.IfEmpty(Me.cmbTipoUso.Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Selezionare la tipologia d\'uso dell\'impianto!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        ID_TIPOLOGIA_USO = 0
        For i = 0 To CheckBoxTipologia.Items.Count - 1
            If CheckBoxTipologia.Items(i).Selected = True Then
                ID_TIPOLOGIA_USO = ID_TIPOLOGIA_USO + CheckBoxTipologia.Items(i).Value
            End If
        Next

        If ID_TIPOLOGIA_USO = 0 Then
            Response.Write("<script>alert('Selezionare la tipologia d\'uso dell\'impianto!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If Me.txtAnnoRealizzazione.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazione.Text = ""

        End If

        If CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = "dd/mm/YYYY" Then
            CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = ""
        End If

    End Function


    Private Sub Salva()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            ' '' Ricavo vIdImpianto
            par.cmd.CommandText = " select SISCOM_MI.SEQ_IMPIANTI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdImpianto = myReader1(0)
            End If

            myReader1.Close()
            par.cmd.CommandText = ""

            Me.txtIdImpianto.Text = vIdImpianto

            ' IMPIANTI
            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI (ID, ID_COMPLESSO, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE,ANNO_COSTRUZIONE,DITTA_COSTRUTTRICE,ANNOTAZIONI) " _
                                & "values (:id,:id_complesso,:id_edificio,:cod_tipologia,:descrizione,:anno,:ditta,:annotazioni)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "ID"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Idrico_Generale.FindControl("txtNote"), TextBox).Text, 4000)))


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")


            ' I_IDRICI
            par.cmd.CommandText = "insert into SISCOM_MI.I_IDRICI (ID,ID_TIPOLOGIA_USO, LIBRETTO, DICH_CONF,PRATICA_ISPESL,DATA_PRATICA_ISPESL," _
                                                                & "DITTA_GESTIONE,TELEFONO_DITTA,COMPRESSORE) " _
                                & "values (:id,:id_tipologia_uso,:libretto,:dich_conf,:ispesl,:data_ispesl,:ditta_gestione,:num_tel,:compressore)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia_uso", Convert.ToInt32(Me.cmbTipoUso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia_uso", ID_TIPOLOGIA_USO))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Idrico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dich_conf", CType(Tab_Idrico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ispesl", CType(Tab_Idrico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ispesl", par.AggiustaData(CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Idrico_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_attivazione", par.AggiustaData(CType(Tab_Idrico_Generale.FindControl("txtDataAccensione"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Idrico_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("compressore", CType(Tab_Idrico_Generale.FindControl("cmbCompressore"), DropDownList).SelectedValue.ToString()))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*******************************

            ' IMPIANTI_EDIFICI
            AggiornaImpiantiEdifici()



            '*** INSERIMENTO PRE-SERBATOI IDRICI
            Dim lstPreSerbatoi As System.Collections.Generic.List(Of Epifani.Serbatoi)

            lstPreSerbatoi = CType(HttpContext.Current.Session.Item("LSTPRESERBATOI"), System.Collections.Generic.List(Of Epifani.Serbatoi))

            For Each gen As Epifani.Serbatoi In lstPreSerbatoi

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " insert into SISCOM_MI.SERBATOI_IDRICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE,TIPO_SERBATOIO) " _
                                    & " values (SISCOM_MI.SEQ_SERBATOI_IDRICI.NEXTVAL,:id_impianto,:modello,:matricola,:anno,:volume,:pressione_b,:pressione_e,:note,:tipo_serbatoio)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", Strings.Left(gen.MODELLO, 200)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", Strings.Left(gen.MATRICOLA, 30)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", gen.ANNO_COSTRUZIONE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("volume", strToNumber(gen.VOLUME)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_b", strToNumber(gen.PRESSIONE_BOLLA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_e", strToNumber(gen.PRESSIONE_ESERCIZIO)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 300)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_serbatoio", "PRE-AUTOCLAVE"))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Serbatoio Pre-Autoclave")

            Next
            '********************************

            '*** INSERIMENTO SERBATOI IDRICI
            Dim lstSerbatoi As System.Collections.Generic.List(Of Epifani.Serbatoi)

            lstSerbatoi = CType(HttpContext.Current.Session.Item("LSTSERBATOI"), System.Collections.Generic.List(Of Epifani.Serbatoi))

            For Each gen As Epifani.Serbatoi In lstSerbatoi

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " insert into SISCOM_MI.SERBATOI_IDRICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE,TIPO_SERBATOIO) " _
                                    & " values (SISCOM_MI.SEQ_SERBATOI_IDRICI.NEXTVAL,:id_impianto,:modello,:matricola,:anno,:volume,:pressione_b,:pressione_e,:note,:tipo_serbatoio )"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", Strings.Left(gen.MODELLO, 200)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", Strings.Left(gen.MATRICOLA, 30)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", gen.ANNO_COSTRUZIONE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("volume", strToNumber(gen.VOLUME)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_b", strToNumber(gen.PRESSIONE_BOLLA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_e", strToNumber(gen.PRESSIONE_ESERCIZIO)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 300)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_serbatoio", "AUTOCLAVE"))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Serbatoio Autoclave")

            Next
            '********************************


            '*** INSERIMENTO POMPE SOLLEVAMENTO
            Dim lstPompeS As System.Collections.Generic.List(Of Epifani.PompeS)

            lstPompeS = CType(HttpContext.Current.Session.Item("LSTPOMPES"), System.Collections.Generic.List(Of Epifani.PompeS))

            For Each gen As Epifani.PompeS In lstPompeS

                par.cmd.CommandText = "insert into SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA,DISCONNETTORE) " _
                                    & "values (SISCOM_MI.SEQ_POMPE_CIRCOLAZIONE_IDRICI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & "," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.PORTATA, "Null")) & "," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.PREVALENZA, "Null")) & ",'" _
                                        & gen.DISCONNETTORE & "')"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

            Next
            '********************************


            ' COMMIT
            par.myTrans.Commit()


            '*** AGGIORNO il COD. IMPIANTO
            par.cmd.CommandText = "select COD_IMPIANTO from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                Me.txtCodImpianto.Text = myReader2(0)
            End If

            myReader2.Close()
            par.cmd.CommandText = ""


            '**** AGGIORNO I COMPONENTI

            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato

            '*** PRE-SERBATOI
            par.cmd.CommandText = " select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE " _
                                & " from SISCOM_MI.SERBATOI_IDRICI " _
                                & " where   SISCOM_MI.SERBATOI_IDRICI.TIPO_SERBATOIO='PRE-AUTOCLAVE' and " _
                                        & " SISCOM_MI.SERBATOI_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                                & " order by SISCOM_MI.SERBATOI_IDRICI.MODELLO "


            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds1 As New Data.DataSet()

            da1.Fill(ds1, "SERBATOI_IDRICI")

            CType(TabDettagliIDRICO.FindControl("DataGrid1"), DataGrid).DataSource = ds1
            CType(TabDettagliIDRICO.FindControl("DataGrid1"), DataGrid).DataBind()
            ds1.Dispose()


            '*** SERBATOI
            par.cmd.CommandText = " select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE " _
                                & " from SISCOM_MI.SERBATOI_IDRICI " _
                                & " where  SISCOM_MI.SERBATOI_IDRICI.TIPO_SERBATOIO='AUTOCLAVE' and " _
                                    & "    SISCOM_MI.SERBATOI_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                                & " order by SISCOM_MI.SERBATOI_IDRICI.MODELLO "


            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds2 As New Data.DataSet()

            da2.Fill(ds2, "SERBATOI_IDRICI")

            CType(TabDettagliIDRICO.FindControl("DataGrid2"), DataGrid).DataSource = ds2
            CType(TabDettagliIDRICO.FindControl("DataGrid2"), DataGrid).DataBind()
            ds2.Dispose()


            '*** POMPE DI SOLLEVAMENTO
            par.cmd.CommandText = " select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA,DISCONNETTORE " _
                                & " from SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI " _
                                & " where SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                                & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI.MODELLO "

            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds3 As New Data.DataSet()

            da3.Fill(ds3, "POMPE_CIRCOLAZIONE_TERMICI")

            CType(TabDettagliIDRICO.FindControl("DataGrid3"), DataGrid).DataSource = ds3
            CType(TabDettagliIDRICO.FindControl("DataGrid3"), DataGrid).DataBind()
            ds3.Dispose()
            '*******************************


            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            'VisualizzaDati()

            TabberHide = "tabbertab"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"



        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub


    Private Sub Update()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            '* IMPIANTI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update SISCOM_MI.IMPIANTI set ID_COMPLESSO=:id_complesso, ID_EDIFICIO=:id_edificio,COD_TIPOLOGIA=:cod_tipologia,DESCRIZIONE=:descrizione,ANNO_COSTRUZIONE=:anno,DITTA_COSTRUTTRICE=:ditta,ANNOTAZIONI=:annotazioni where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "ID"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Idrico_Generale.FindControl("txtNote"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")

            '*** I_IDRICI
            par.cmd.CommandText = " update SISCOM_MI.I_IDRICI " _
                                & " set ID_TIPOLOGIA_USO=:id_tipologia_uso,LIBRETTO=:libretto,DICH_CONF=:dich_conf,PRATICA_ISPESL=:ispesl,DATA_PRATICA_ISPESL=:data_ispesl," _
                                     & "DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel,COMPRESSORE=:compressore " _
                                & " where ID=:id_impianto"

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia_uso", Convert.ToInt32(Me.cmbTipoUso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia_uso", ID_TIPOLOGIA_USO))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Idrico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dich_conf", CType(Tab_Idrico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ispesl", CType(Tab_Idrico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ispesl", par.AggiustaData(CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Idrico_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_attivazione", par.AggiustaData(CType(Tab_Idrico_Generale.FindControl("txtDataAccensione"), TextBox).Text))) DATA_PRIMA_ATTIVAZIONE=:data_attivazione
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Idrico_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("compressore", CType(Tab_Idrico_Generale.FindControl("cmbCompressore"), DropDownList).SelectedValue.ToString()))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            ' IMPIANTI_EDIFICI
            AggiornaImpiantiEdifici()

            par.myTrans.Commit() 'COMMIT

            '*** AGGIORNO il COD. IMPIANTO
            par.cmd.CommandText = "select COD_IMPIANTO from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                Me.txtCodImpianto.Text = myReader2(0)
            End If

            myReader2.Close()
            par.cmd.CommandText = ""


            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub





    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            sProvenienza = Request.QueryString("SL")
            If par.IfNull(sProvenienza, 0) = 1 Then
                Response.Write("<script>self.close();</script>")
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub





    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = par.IfEmpty(par.IfNull(Request.QueryString("VER"), ""), "")


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            ElseIf Strings.Len(sVerifiche) > 0 Then
                Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
            Else
                Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "');</script>")
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
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



    Public Property vIdImpianto() As Long
        Get
            If Not (ViewState("par_idImpianto") Is Nothing) Then
                Return CLng(ViewState("par_idImpianto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idImpianto") = value
        End Set

    End Property


    Private Sub AggiornaImpiantiEdifici()
        Dim i As Integer

        Try

        Catch ex As Exception
        End Try

        par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_SCALE where ID_IMPIANTO = " & vIdImpianto
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = ""

        For i = 0 To CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
            If CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True Then
                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                           & "(" & vIdImpianto & "," & CType(Tab_Idrico_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value & ")"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
            End If
        Next


    End Sub


    Private Function RitornaNumDaSiNo(ByVal valoredapassare As String) As String
        Dim a As String = "Null"

        Try
            If valoredapassare = "SI" Then
                a = 1
            ElseIf valoredapassare = "NO" Then
                a = 0
            Else
                a = "Null"
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

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try
            Me.btnSalva.Visible = False
            Me.btnElimina.Visible = False

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Dim sNote As String

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtElimina.Text = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI where ID = " & vIdImpianto
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
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE IMPIANTO"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO IDRICO"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto Idrico del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", sNote))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************


                ' COMMIT
                par.myTrans.Commit()

                Response.Write("<SCRIPT>alert('Eliminazione completata correttamente!');</SCRIPT>")

                Session.Add("LAVORAZIONE", "0")

                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreImpianto = Request.QueryString("IM")

                sOrdinamento = Request.QueryString("ORD")
                sVerifiche = par.IfEmpty(par.IfNull(Request.QueryString("VER"), ""), "")

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()

                Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                If txtindietro.Text = 1 Then
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                ElseIf Strings.Len(sVerifiche) > 0 Then
                    Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
                Else
                    Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "');</script>")
                End If

            Else
                CType(Me.Page.FindControl("txtElimina"), TextBox).Text = "0"
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

End Class
