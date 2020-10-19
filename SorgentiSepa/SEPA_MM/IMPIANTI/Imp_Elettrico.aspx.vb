﻿Imports System.Collections

Partial Class IMPIANTI_Imp_Elettrico
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""
    Public Tabber7 As String = ""

    Public TabberHide As String = ""

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sOrdinamento As String
    Public sVerifiche As String

    Public sProvenienza As String

    Dim lstScale As System.Collections.Generic.List(Of Epifani.Scale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        lstScale = CType(HttpContext.Current.Session.Item("LSTSCALE"), System.Collections.Generic.List(Of Epifani.Scale))
        'lstEdificiTV = CType(HttpContext.Current.Session.Item("LSTTV_EDIFICI"), System.Collections.Generic.List(Of Epifani.Scale))


        If Not IsPostBack Then

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")
            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            sProvenienza = Request.QueryString("SL")

            vIdImpianto = 0
            vIdImpianto = Session.Item("ID")

            lstScale.Clear()


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

            For Each CTRL In Me.Tab_Elettrico_Generale.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            For Each CTRL In Me.TabElettricoDettagli.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            For Each CTRL In Me.TabElettricoPortineria.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            For Each CTRL In Me.TabElettricoBox.Controls
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
            'CType(TabElettricoCitofonico.FindControl("txtDataC"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            'CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Elettrico.png';</script>")
            End If

            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 1, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If

        End If



    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""
        Tabber6 = ""
        Tabber7 = ""


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
            Case "6"
                Tabber6 = "tabbertabdefault"
            Case "7"
                Tabber7 = "tabbertabdefault"
        End Select

        If vIdImpianto <> 0 Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
        End If


    End Sub

    Private Sub SettaggioCampi()

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
            'CType(TabElettricoTV.FindControl("cmbEdificioTV"), DropDownList).Items.Clear()

            DrLEdificio.Items.Add(New ListItem(" ", -1))
            'CType(TabElettricoTV.FindControl("cmbEdificioTV"), DropDownList).Items.Add(New ListItem(" ", -1))

            If gest <> 0 Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI order by DENOMINAZIONE asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                'CType(TabElettricoTV.FindControl("cmbEdificioTV"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            DrLEdificio.SelectedValue = "-1"
            'CType(TabElettricoTV.FindControl("cmbEdificioTV"), DropDownList).SelectedValue = "-1"

            myReader1.Close()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        FiltraEdifici()
        FiltraScale()

    End Sub

    Protected Sub cmbComplesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        FiltraScale()

    End Sub

    Protected Sub DrLEdificio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Private Sub FiltraEdifici()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean


        Try

            FlagConnessione = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))

            lstScale.Clear()

            '*** select per elenco EDIFICI (TV) e EDIFICI maschera principale
            par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE " _
                                & " from SISCOM_MI.EDIFICI " _
                                & " where SISCOM_MI.EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString _
                                & " order by DENOMINAZIONE asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            '** select per elenco EDIFICI-SCALA (CITOFONO)
            'par.cmd.CommandText = " select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE," _
            '                            & "SISCOM_MI.SCALE_EDIFICI.ID as ID_SCALE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as SCALE " _
            '                    & " from SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI  " _
            '                    & " where  SISCOM_MI.EDIFICI.ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString & " and " _
            '                            & "SISCOM_MI.EDIFICI.ID=SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO (+) " _
            '                    & " order by DENOMINAZIONE,SCALE asc "

            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader2.Read

            '    Dim gen As Epifani.Scale
            '    gen = New Epifani.Scale(par.IfNull(myReader2("ID_SCALE"), -1), par.IfNull(myReader2("DENOMINAZIONE"), " "), par.IfNull(myReader2("SCALE"), " "))
            '    lstScale.Add(gen)
            '    gen = Nothing

            'End While
            'myReader2.Close()
            '************

            ''*** CITOFONO
            'CType(TabElettricoCitofonico.FindControl("CheckBoxEdifici"), CheckBoxList).Visible = True
            'CType(TabElettricoCitofonico.FindControl("lblATTENZIONE"), Label).Visible = False

            'dlist = CType(TabElettricoCitofonico.FindControl("CheckBoxEdifici"), CheckBoxList)
            'dlist.DataSource = lstScale

            'dlist.DataTextField = "SCALE"
            'dlist.DataValueField = "ID"
            ''dlist.Attributes.Add()
            'dlist.DataBind()


            If FlagConnessione = True Then
                par.OracleConn.Close()
            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraScale()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim FlagConnessione As Boolean
        Dim bTrovato As Boolean

        Try

            FlagConnessione = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            lstScale.Clear()
            bTrovato = False

            ' SCALE ***************************************************************
            par.cmd.CommandText = "select  ID, DESCRIZIONE AS SCALE from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString & " order by DESCRIZIONE asc"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read

                Dim gen As Epifani.Scale
                gen = New Epifani.Scale(par.IfNull(myReader2("ID"), -1), Me.DrLEdificio.SelectedItem.ToString, "SCALA= " & par.IfNull(myReader2("SCALE"), " "))
                lstScale.Add(gen)
                bTrovato = True
                gen = Nothing
            End While
            myReader2.Close()



            '*** Presenza di Centrale TERMICA nell'Edificio
            par.cmd.CommandText = "select  ID from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='TE' and ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read = True Then

                Dim gen As Epifani.Scale
                gen = New Epifani.Scale(-1, "C.T.", "C.T. Centrale Termica")
                lstScale.Add(gen)
                bTrovato = True
                gen = Nothing
            End If
            myReader2.Close()
            '**********************************

            '*** Presenza di SOLLEVAMENTO nell'Edificio
            par.cmd.CommandText = "select  ID from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='TE' and ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString

            par.cmd.CommandText = "select SISCOM_MI.I_SOLLEVAMENTO.TIPOLOGIA as ""TIPOLOGIA"" " _
                                & "from SISCOM_MI.I_SOLLEVAMENTO " _
                                & "where SISCOM_MI.I_SOLLEVAMENTO.ID in  (" _
                                    & " select SISCOM_MI.IMPIANTI.ID from SISCOM_MI.IMPIANTI " _
                                    & " where SISCOM_MI.IMPIANTI.COD_TIPOLOGIA='SO' " _
                                    & " and SISCOM_MI.IMPIANTI.ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString & ")"

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read = True Then

                Dim gen As Epifani.Scale

                Select Case par.IfNull(myReader2("TIPOLOGIA"), "")
                    Case 1  'ASCENSORE
                        gen = New Epifani.Scale(-3, "ASCENSORE", "ASCENSORE")
                        lstScale.Add(gen)
                        bTrovato = True
                        gen = Nothing
                    Case 2  'MONTACARICHI
                        gen = New Epifani.Scale(-4, "MONTACARICHI", "MONTACARICHI")
                        lstScale.Add(gen)
                        bTrovato = True
                        gen = Nothing
                    Case 3  'MONTASCALE
                        gen = New Epifani.Scale(-5, "MONTASCALE", "MONTASCALE")
                        lstScale.Add(gen)
                        bTrovato = True
                        gen = Nothing
                    Case 4  'PIATTAFORMA ELEVATRICE
                        gen = New Epifani.Scale(-6, "PIATTAFORMA ELEVATRICE", "PIATTAFORMA ELEVATRICE")
                        lstScale.Add(gen)
                        bTrovato = True
                        gen = Nothing
                End Select
                
            End If
            myReader2.Close()
            '**********************************

            '*** Presenza di BOX nell'Edificio
            par.cmd.CommandText = "select ID from SISCOM_MI.UNITA_IMMOBILIARI where COD_TIPOLOGIA='B' and ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then

                Dim gen As Epifani.Scale
                gen = New Epifani.Scale(-2, "BOX", "BOX")
                lstScale.Add(gen)
                bTrovato = True
                gen = Nothing
            End If
            myReader2.Close()
            '**********************************

            ' SCALE QUADRO SCALA
            dlist = CType(TabElettricoDettagli.FindControl("CheckBoxScaleSC"), CheckBoxList)
            dlist.DataSource = lstScale

            dlist.DataTextField = "SCALE_NO_TITLE"
            dlist.DataValueField = "ID"
            dlist.DataBind()


            dlist = CType(TabElettricoDettagli.FindControl("CheckBoxScaleSE"), CheckBoxList)
            dlist.DataSource = lstScale

            dlist.DataTextField = "SCALE_NO_TITLE"
            dlist.DataValueField = "ID"
            dlist.DataBind()
            '*********************************************************************************************

            If bTrovato = True Then
                CType(TabElettricoDettagli.FindControl("CheckBoxScaleSC"), CheckBoxList).Visible = True
                CType(TabElettricoDettagli.FindControl("lblATTENZIONE_SC"), Label).Visible = False

                CType(TabElettricoDettagli.FindControl("CheckBoxScaleSE"), CheckBoxList).Visible = True
                CType(TabElettricoDettagli.FindControl("lblATTENZIONE_SE"), Label).Visible = False
            Else
                CType(TabElettricoDettagli.FindControl("CheckBoxScaleSC"), CheckBoxList).Visible = False
                CType(TabElettricoDettagli.FindControl("lblATTENZIONE_SC"), Label).Visible = True

                CType(TabElettricoDettagli.FindControl("CheckBoxScaleSE"), CheckBoxList).Visible = False
                CType(TabElettricoDettagli.FindControl("lblATTENZIONE_SE"), Label).Visible = True
            End If


            If FlagConnessione = True Then
                par.OracleConn.Close()
            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader



        '*** FORM PRINCIPALE
        Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

        Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
        FiltraEdifici()

        Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")
        FiltraScale()

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

        Me.txtDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
        Me.txtAnnoRealizzazione.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


        '*** I_ELETTRICI
        par.cmd.CommandText = "select * from SISCOM_MI.I_ELETTRICI where SISCOM_MI.I_ELETTRICI.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** FORM PRINCIPALE
            'Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")

            '*** TAB GENERALE
            CType(Tab_Elettrico_Generale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(Tab_Elettrico_Generale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

            CType(Tab_Elettrico_Generale.FindControl("cmbAvanquadro"), DropDownList).Items.FindByValue(par.IfNull(myReader2("AVANQUADRO"), "")).Selected = True
            CType(Tab_Elettrico_Generale.FindControl("cmbDifferenziale"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DIFFERENZIALE"), "")).Selected = True
            CType(Tab_Elettrico_Generale.FindControl("cmbNorma"), DropDownList).Items.FindByValue(par.IfNull(myReader2("NORMA"), "")).Selected = True

            CType(Tab_Elettrico_Generale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

            '*** TAB CERTIFICAZIONI
            'CType(Tab_Idrico_Certificazioni.FindControl("cmbLibretto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LIBRETTO"), "")).Selected = True
            'CType(Tab_Idrico_Certificazioni.FindControl("cmbConformita"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF"), "")).Selected = True
            'CType(Tab_Idrico_Certificazioni.FindControl("cmbISPESL"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_ISPESL"), "")).Selected = True
            'CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRATICA_ISPESL"), ""))

        End If
        myReader2.Close()

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
                ' LEGGO IMPIANTI (Elettrico)

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

        If Me.txtAnnoRealizzazione.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazione.Text = ""

        End If

        'If CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = "dd/mm/YYYY" Then
        '    CType(Tab_Idrico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = ""
        'End If

    End Function


    Private Sub Salva()
        Dim vIdQuadroSE As Integer
        Dim vIdQuadroSC As Integer

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
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "EL"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Elettrico_Generale.FindControl("txtNote"), TextBox).Text, 4000)))


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")


            ' I_ELETTRICI
            par.cmd.CommandText = "insert into SISCOM_MI.I_ELETTRICI (ID,AVANQUADRO,DIFFERENZIALE,NORMA,DITTA_GESTIONE,TELEFONO_DITTA) " _
                                & "values (:id,:avanquadro,:differenziale,:norma,:ditta_gestione,:num_tel)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia_uso", Convert.ToInt32(Me.cmbTipoUso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("avanquadro", CType(Tab_Elettrico_Generale.FindControl("cmbAvanquadro"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", CType(Tab_Elettrico_Generale.FindControl("cmbDifferenziale"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", CType(Tab_Elettrico_Generale.FindControl("cmbNorma"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Elettrico_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Elettrico_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*******************************

            ' IMPIANTI_EDIFICI
            'AggiornaImpiantiEdifici()



            ' INSERIMENTO QUADRO SERVIZI
            Dim lstQuadroSE As System.Collections.Generic.List(Of Epifani.Quadro)

            lstQuadroSE = CType(HttpContext.Current.Session.Item("LSTQUADRO_SE"), System.Collections.Generic.List(Of Epifani.Quadro))

            For Each gen As Epifani.Quadro In lstQuadroSE
                par.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SERVIZI (ID, ID_IMPIANTO,QUANTITA,DIFFERENZIALE,NORMA,UBICAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_QUADRO_SERVIZI.NEXTVAL,:id_impianto,:quantita,:differenziale,:norma,:ubicazione) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", gen.QUANTITA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", gen.DIFFERENZIALE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", gen.NORMA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Strings.Left(gen.UBICAZIONE, 300)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Quadro Servizi")

                '**** Ricavo ID della Quadro Servizi
                par.cmd.CommandText = " select SISCOM_MI.SEQ_I_ELE_QUADRO_SERVIZI.CURRVAL FROM dual "
                Dim myReaderScale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderScale.Read Then
                    vIdQuadroSE = myReaderScale(0)
                End If
                myReaderScale.Close()
                par.cmd.CommandText = ""

                '***********
                Dim lstScaleSel As System.Collections.Generic.List(Of Epifani.Scale)
                lstScaleSel = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

                For Each genScale As Epifani.Scale In lstScaleSel

                    If genScale.DENOMINAZIONE_EDIFICIO = gen.ID Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI  (ID_QUADRO_SERVIZI,ID_ELEMENTO) values " _
                                   & "(" & vIdQuadroSE & "," & genScale.DENOMINAZIONE_SCALA & ")"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Elementi Servite del Quadro Servizi")

                    End If
                Next
            Next
            '***************************************


            ' INSERIMENTO QUADRO SCALA
            Dim lstQuadroSC As System.Collections.Generic.List(Of Epifani.Quadro)

            lstQuadroSC = CType(HttpContext.Current.Session.Item("LSTQUADRO_SC"), System.Collections.Generic.List(Of Epifani.Quadro))

            For Each gen As Epifani.Quadro In lstQuadroSC
                par.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SCALA (ID, ID_IMPIANTO,QUANTITA,DIFFERENZIALE,NORMA,UBICAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_QUADRO_SCALA.NEXTVAL,:id_impianto,:quantita,:differenziale,:norma,:ubicazione) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", gen.QUANTITA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", gen.DIFFERENZIALE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", gen.NORMA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Strings.Left(gen.UBICAZIONE, 300)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Quadro Scala")

                '**** Ricavo ID della Quadro Scala
                par.cmd.CommandText = " select SISCOM_MI.SEQ_I_ELE_QUADRO_SCALA.CURRVAL FROM dual "
                Dim myReaderScale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderScale.Read Then
                    vIdQuadroSC = myReaderScale(0)
                End If
                myReaderScale.Close()
                par.cmd.CommandText = ""

                '***********
                Dim lstScaleSel2 As System.Collections.Generic.List(Of Epifani.Scale)
                lstScaleSel2 = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL2"), System.Collections.Generic.List(Of Epifani.Scale))

                For Each genScale As Epifani.Scale In lstScaleSel2

                    If genScale.DENOMINAZIONE_EDIFICIO = gen.ID Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI  (ID_QUADRO_SCALA,ID_ELEMENTO) values " _
                                   & "(" & vIdQuadroSC & "," & genScale.DENOMINAZIONE_SCALA & ")"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Elementi Servite del Quadro Scala")

                    End If
                Next

            Next
            '***************************************

            '*** INSERIMENTO PORTINERIE
            Dim lstPortineria As System.Collections.Generic.List(Of Epifani.Portineria)

            lstPortineria = CType(HttpContext.Current.Session.Item("LSTPORTINERIA"), System.Collections.Generic.List(Of Epifani.Portineria))

            For Each gen As Epifani.Portineria In lstPortineria

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.I_ELE_PORTINERIA (ID, ID_IMPIANTO,QUADRO,DIFFERENZIALE,NORMA,ID_TIPO_DISTRIBUZIONE,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_PORTINERIA.NEXTVAL,:id_impianto,:quadro,:differenziale,:norma,:id_distribuzione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quadro", gen.QUADRO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", gen.DIFFERENZIALE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", gen.NORMA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(gen.ID_TIPO_DISTRIBUZIONE))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 300)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Portineria")

            Next
            '********************************


            '*** INSERIMENTO BOX
            Dim lstBox As System.Collections.Generic.List(Of Epifani.Box)

            lstBox = CType(HttpContext.Current.Session.Item("LSTBOX"), System.Collections.Generic.List(Of Epifani.Box))

            For Each gen As Epifani.Box In lstBox

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.I_ELE_BOX (ID, ID_IMPIANTO,SUP_9_AUTO,QUADRO,DIFFERENZIALE," _
                                                            & "ID_TIPO_DISTRIBUZIONE,PULSANTE_SGANCIO,PRATICA_VVF," _
                                                            & "VERIFICA,MESSA_TERRA,SCARICHE_ATMOSFERICHE,SCARICATORI,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_BOX.NEXTVAL,:id_impianto,:auto,:quadro,:differenziale," _
                                        & ":id_distribuzione,:sgancio,:pratica,:verifiche,:messaterra,:scariche,:scaricatori,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("auto", gen.SUP_9_AUTO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quadro", gen.QUADRO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", gen.DIFFERENZIALE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(gen.ID_TIPO_DISTRIBUZIONE))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("sgancio", gen.PULSANTE_SGANCIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pratica", gen.PRATICA_VVF))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("verifiche", gen.VERIFICA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("messaterra", gen.MESSA_TERRA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scariche", gen.SCARICHE_ATMOSFERICHE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scaricatori", gen.SCARICATORI))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 300)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Impianti Elettrici Box")

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


            '**** AGGIORNO QUADRO SERVIZI
            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato

            par.cmd.CommandText = "select SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID,SISCOM_MI.I_ELE_QUADRO_SERVIZI.QUANTITA,SISCOM_MI.I_ELE_QUADRO_SERVIZI.DIFFERENZIALE,SISCOM_MI.I_ELE_QUADRO_SERVIZI.NORMA,SISCOM_MI.I_ELE_QUADRO_SERVIZI.UBICAZIONE," _
                              & " (select count(*) from SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI where  SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI.ID_QUADRO_SERVIZI=SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID) AS ""SCALE_SERVITE"" " _
                              & " from SISCOM_MI.I_ELE_QUADRO_SERVIZI " _
                              & " where SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID "


            Dim daSE As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsSE As New Data.DataSet()

            daSE.Fill(dsSE, "I_ELE_QUADRO_SERVIZI")

            CType(TabElettricoDettagli.FindControl("DataGridServizio"), DataGrid).DataSource = dsSE
            CType(TabElettricoDettagli.FindControl("DataGridServizio"), DataGrid).DataBind()
            dsSE.Dispose()


            '**** AGGIORNO QUADRO SCALA
            par.cmd.CommandText = "select SISCOM_MI.I_ELE_QUADRO_SCALA.ID,SISCOM_MI.I_ELE_QUADRO_SCALA.QUANTITA,SISCOM_MI.I_ELE_QUADRO_SCALA.DIFFERENZIALE,SISCOM_MI.I_ELE_QUADRO_SCALA.NORMA,SISCOM_MI.I_ELE_QUADRO_SCALA.UBICAZIONE," _
                                    & " (select count(*) from SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI where  SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI.ID_QUADRO_SCALA=SISCOM_MI.I_ELE_QUADRO_SCALA.ID) AS ""SCALE_SERVITE"" " _
                              & " from SISCOM_MI.I_ELE_QUADRO_SCALA " _
                              & " where SISCOM_MI.I_ELE_QUADRO_SCALA.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.I_ELE_QUADRO_SCALA.ID "


            Dim daSC As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsSC As New Data.DataSet()

            daSC.Fill(dsSC, "I_ELE_QUADRO_SCALA")

            CType(TabElettricoDettagli.FindControl("DataGridScala"), DataGrid).DataSource = dsSC
            CType(TabElettricoDettagli.FindControl("DataGridScala"), DataGrid).DataBind()
            dsSC.Dispose()



            '**** AGGIORNO LE PORTINERIA
            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato

            par.cmd.CommandText = "select SISCOM_MI.I_ELE_PORTINERIA.ID,SISCOM_MI.I_ELE_PORTINERIA.QUADRO," _
                    & "SISCOM_MI.I_ELE_PORTINERIA.DIFFERENZIALE,SISCOM_MI.I_ELE_PORTINERIA.NORMA," _
                       & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                       & "SISCOM_MI.I_ELE_PORTINERIA.ID_TIPO_DISTRIBUZIONE,SISCOM_MI.I_ELE_PORTINERIA.NOTE " _
              & " from SISCOM_MI.I_ELE_PORTINERIA,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
              & " where SISCOM_MI.I_ELE_PORTINERIA.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ELE_PORTINERIA.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
              & " order by SISCOM_MI.I_ELE_PORTINERIA.ID "

            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds1 As New Data.DataSet()

            da1.Fill(ds1, "I_ELE_PORTINERIA")

            CType(TabElettricoPortineria.FindControl("DataGrid1"), DataGrid).DataSource = ds1
            CType(TabElettricoPortineria.FindControl("DataGrid1"), DataGrid).DataBind()
            ds1.Dispose()


            '*** BOX
            par.cmd.CommandText = "  select SISCOM_MI.I_ELE_BOX.ID,SUP_9_AUTO,QUADRO,DIFFERENZIALE," _
                                           & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                                           & "ID_TIPO_DISTRIBUZIONE,PULSANTE_SGANCIO,PRATICA_VVF,VERIFICA," _
                                           & "MESSA_TERRA, SCARICHE_ATMOSFERICHE,SCARICATORI,NOTE " _
                                  & " from SISCOM_MI.I_ELE_BOX,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
                                  & " where SISCOM_MI.I_ELE_BOX.ID_IMPIANTO = " & vIdImpianto _
                                  & " and   SISCOM_MI.I_ELE_BOX.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
                                  & " order by SISCOM_MI.I_ELE_BOX.ID "

            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds3 As New Data.DataSet()

            da3.Fill(ds3, "I_ELE_BOX")

            CType(TabElettricoBox.FindControl("DataGridBox"), DataGrid).DataSource = ds3
            CType(TabElettricoBox.FindControl("DataGridBox"), DataGrid).DataBind()
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
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "EL"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Elettrico_Generale.FindControl("txtNote"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")


            '*** I_ELETTRICI
            par.cmd.CommandText = " update SISCOM_MI.I_ELETTRICI " _
                                & " set AVANQUADRO=:avanquadro,DIFFERENZIALE=:differenziale,NORMA=:norma," _
                                     & "DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel " _
                                & " where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("avanquadro", CType(Tab_Elettrico_Generale.FindControl("cmbAvanquadro"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", CType(Tab_Elettrico_Generale.FindControl("cmbDifferenziale"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", CType(Tab_Elettrico_Generale.FindControl("cmbNorma"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Elettrico_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Elettrico_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            ' IMPIANTI_EDIFICI
            'AggiornaImpiantiEdifici()

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

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO ELETTRICO"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto Elettrico del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
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


 
End Class