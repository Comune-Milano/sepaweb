Imports System.Collections

Partial Class IMPIANTI_Imp_Tutela
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

    Dim lstAlloggi As System.Collections.Generic.List(Of Epifani.Alloggi)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        lstAlloggi = CType(HttpContext.Current.Session.Item("LSTALLOGGI"), System.Collections.Generic.List(Of Epifani.Alloggi))


        If Not IsPostBack Then

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            sProvenienza = Request.QueryString("SL")

            vIdImpianto = 0
            vIdImpianto = Session.Item("ID")

            lstAlloggi.Clear()

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

            For Each CTRL In Me.Tab_Tutela_Generale.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            'CANCELLI
            For Each CTRL In Me.Tab_Tutela_Dettagli.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            'PASSI CARRABILI
            For Each CTRL In Me.Tab_Tutela_Carraio.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** FORM VERIFICHE PERIODICHE
            For Each CTRL In Me.Tab_Tutela_Verifiche1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM VERIFICHE ANNUALI SCALE
            For Each CTRL In Me.Tab_Tutela_Verifiche2.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*****************
            ' CONTROLLO DATA
            CType(Tab_Tutela_Alloggi.FindControl("txtDataAnt1"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Tutela_Alloggi.FindControl("txtDataAnt2"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Tutela_Alloggi.FindControl("txtDataBlind1"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Tutela_Alloggi.FindControl("txtDataLast1"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Tutela_Alloggi.FindControl("txtDataLast2"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CType(Tab_Tutela_Verifiche1.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Verifiche1.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Verifiche1.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Verifiche1.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            CType(Tab_Tutela_Verifiche2.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Verifiche2.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Verifiche2.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Verifiche2.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            CType(Tab_Tutela_Carraio.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")


            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            DrLEdificio.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            'CType(Tab_Tutela_Generale.FindControl("cmbCarrai"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Generale.FindControl("cmbCarrabile"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Tutela_Generale.FindControl("cmbAutomatizzato"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            CType(Tab_Tutela_Generale.FindControl("cmbVideoSorveglianza"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            If vIdImpianto = 0 Then
                CType(Tab_Tutela_Verifiche1.FindControl("btnApri"), ImageButton).Visible = False
                CType(Tab_Tutela_Verifiche1.FindControl("btnVisualizza"), ImageButton).Visible = False
                CType(Tab_Tutela_Verifiche1.FindControl("btnElimina"), ImageButton).Visible = False

                CType(Tab_Tutela_Verifiche2.FindControl("btnApri"), ImageButton).Visible = False
                CType(Tab_Tutela_Verifiche2.FindControl("btnVisualizza"), ImageButton).Visible = False
                CType(Tab_Tutela_Verifiche2.FindControl("btnElimina"), ImageButton).Visible = False
            End If

            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_TutelaImmobile.png';</script>")
            End If


            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 9, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If

        End If


            'If CType(Tab_Tutela_Generale.FindControl("cmbCarrai"), DropDownList).Text = "S" Then
            '    CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Enabled = True
            '    CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Focus()
            'Else
            '    CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Text = ""
            '    CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Enabled = False
            '    CType(Tab_Tutela_Generale.FindControl("cmbCarrai"), DropDownList).Focus()
            'End If

            If CType(Tab_Tutela_Generale.FindControl("cmbCarrabile"), DropDownList).Text = "S" Then
                CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Enabled = True
                CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Focus()
            Else
                CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Text = ""
                CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Enabled = False
                CType(Tab_Tutela_Generale.FindControl("cmbCarrabile"), DropDownList).Focus()
            End If

            If CType(Tab_Tutela_Generale.FindControl("cmbAutomatizzato"), DropDownList).Text = "S" Then
                CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Enabled = True
                CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Focus()
            Else
                CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Text = ""
                CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Enabled = False
                CType(Tab_Tutela_Generale.FindControl("cmbAutomatizzato"), DropDownList).Focus()
            End If

            If CType(Tab_Tutela_Generale.FindControl("cmbVideoSorveglianza"), DropDownList).Text = "S" Then
                CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Enabled = True
                CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Focus()
            Else
                CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Text = ""
                CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Enabled = False
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


            '*** TIPOLOGIA_RECINZIONE
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_RECINZIONE order by DESCRIZIONE"
            myReader1 = par.cmd.ExecuteReader

            CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).Text = CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).Items(0).Text
            myReader1.Close()
            '*********************

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
        'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbComplesso.SelectedValue <> "-1" Then
            ''*** CITOFONI
            'CType(Tab_Tutela_Dettagli.FindControl("CheckBoxScale"), CheckBoxList).Visible = False
            'CType(Tab_Tutela_Dettagli.FindControl("lblATTENZIONE"), Label).Visible = True

            FiltraEdifici()
            FiltraEdifici2()
        Else
            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))

            '*** CITOFONI
            CType(Tab_Tutela_Dettagli.FindControl("CheckBoxScale"), CheckBoxList).Visible = False
            CType(Tab_Tutela_Dettagli.FindControl("lblATTENZIONE"), Label).Visible = True
        End If
    End Sub

    Protected Sub cmbComplesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.TextChanged
        'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        FiltraEdifici2()

    End Sub

    Protected Sub DrLEdificio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Private Sub FiltraEdifici()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.cmbComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))

                'lstScale.Clear()

                '*** EDIFICI maschera principale
                par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE " _
                                    & " from SISCOM_MI.EDIFICI " _
                                    & " where SISCOM_MI.EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString _
                                    & " order by DENOMINAZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()


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

    '' FILTRA LE SCALE DEI CITOFONI e ALLOGGI
    Private Sub FiltraEdifici2()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean
        Dim dlistA As DataGrid

        Try

            FlagConnessione = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            'lstScale.Clear()

            '** select per elenco EDIFICI-SCALA (CITOFONO)
            'If Me.DrLEdificio.SelectedValue <> "-1" Then
            '    '** select per elenco EDIFICI-SCALA (CITOFONO)
            '    par.cmd.CommandText = " select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE," _
            '                                & "SISCOM_MI.SCALE_EDIFICI.ID as ID_SCALE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as SCALE " _
            '                        & " from SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI  " _
            '                        & " where  SISCOM_MI.EDIFICI.ID=" & Me.DrLEdificio.SelectedValue.ToString & " and " _
            '                                & "SISCOM_MI.EDIFICI.ID=SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO (+) " _
            '                        & " order by DENOMINAZIONE,SCALE asc "

            'Else
            '    '
            '    par.cmd.CommandText = " select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE," _
            '                                & "SISCOM_MI.SCALE_EDIFICI.ID as ID_SCALE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as SCALE " _
            '                        & " from SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI  " _
            '                        & " where  SISCOM_MI.EDIFICI.ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString & " and " _
            '                                & "SISCOM_MI.EDIFICI.ID=SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO (+) " _
            '                        & " order by DENOMINAZIONE,SCALE asc "

            'End If
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
            'CType(Tab_Tutela_Dettagli.FindControl("CheckBoxScale"), CheckBoxList).Visible = True
            'CType(Tab_Tutela_Dettagli.FindControl("lblATTENZIONE"), Label).Visible = False

            'dlist = CType(Tab_Tutela_Dettagli.FindControl("CheckBoxScale"), CheckBoxList)
            'dlist.DataSource = lstScale

            'dlist.DataTextField = "SCALE"
            'dlist.DataValueField = "ID"
            ''dlist.Attributes.Add()
            'dlist.DataBind()
            '*******************************

            '*** ALLOGGI
            par.cmd.CommandText = ""
            If Me.DrLEdificio.SelectedValue <> "-1" And vIdImpianto = 0 Then
                par.cmd.CommandText = "select SISCOM_MI.UNITA_IMMOBILIARI.ID as ID_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO," _
                                               & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA," _
                                               & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO," _
                                               & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO," _
                                               & "SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB " _
                                       & " from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                       & " where  SISCOM_MI.EDIFICI.ID=" & Me.DrLEdificio.SelectedValue.ToString & " and " _
                                       & "	      SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID and " _
                                       & "	      SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD and " _
                                       & "	      SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID and " _
                                       & "	      SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE "

            ElseIf vIdImpianto = 0 Then
                par.cmd.CommandText = "select SISCOM_MI.UNITA_IMMOBILIARI.ID as ID_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO," _
                                               & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA," _
                                               & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO," _
                                               & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO," _
                                               & "SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB " _
                                       & " from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                       & " where  SISCOM_MI.EDIFICI.ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString & " and " _
                                       & "	      SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID and " _
                                       & "	      SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD and " _
                                       & "	      SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID and " _
                                       & "	      SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE "

            End If

            If par.cmd.CommandText <> "" Then
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader2 = par.cmd.ExecuteReader()

                lstAlloggi.Clear()
                While myReader2.Read

                    Dim genA As Epifani.Alloggi
                    genA = New Epifani.Alloggi(lstAlloggi.Count, par.IfNull(myReader2("EDIFICIO"), " "), par.IfNull(myReader2("SCALA"), " "), par.IfNull(myReader2("PIANO"), " "), par.IfNull(myReader2("INTERNO"), " "), par.IfNull(myReader2("NOME_SUB"), " "), "", "", "", "", "", "", "", "", par.IfNull(myReader2("ID_UNITA_IMMOBILIARI"), -1))
                    lstAlloggi.Add(genA)
                    genA = Nothing

                End While
                myReader2.Close()

                dlistA = CType(Tab_Tutela_Alloggi.FindControl("DataGridA"), DataGrid)
                dlistA.DataSource = lstAlloggi

                'dlist.DataTextField = "SCALE"
                'dlist.DataValueField = "ID"
                ''dlist.Attributes.Add()
                dlistA.DataBind()
                dlistA.Dispose()


                'Dim daA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                'Dim dsA As New Data.DataSet()

                'daA.Fill(dsA, "UNITA_IMMOBILIARI")

                'CType(Tab_Tutela_Dettagli.FindControl("DataGridA"), DataGrid).DataSource = lstAlloggi
                'CType(Tab_Tutela_Dettagli.FindControl("DataGridA"), DataGrid).DataBind()
                'dsA.Dispose()

                '*******************************
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
        FiltraEdifici2()

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")


        '*** I_TUTELA
        par.cmd.CommandText = "select * from SISCOM_MI.I_TUTELA where SISCOM_MI.I_TUTELA.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** FORM PRINCIPALE
            'Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")

            '*** TAB GENERALE
            CType(Tab_Tutela_Generale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(Tab_Tutela_Generale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

            CType(Tab_Tutela_Generale.FindControl("cmbRecinzione"), DropDownList).Items.FindByValue(par.IfNull(myReader2("RECINZIONE"), "")).Selected = True
            'CType(Tab_Tutela_Generale.FindControl("cmbCarrai"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PASSI_CARRAI"), "")).Selected = True
            'CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Text = par.IfNull(myReader2("NUM_PASSI_CARRAI"), "")
            CType(Tab_Tutela_Generale.FindControl("cmbVideoSorveglianza"), DropDownList).Items.FindByValue(par.IfNull(myReader2("VIDEO_SORVEGLIANZA"), "")).Selected = True

            CType(Tab_Tutela_Generale.FindControl("cmbCarrabile"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CANCELLO_CARRABILE"), "")).Selected = True
            CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Text = par.IfNull(myReader2("NUM_CANCELLO_CARRABILE"), "")

            CType(Tab_Tutela_Generale.FindControl("cmbAutomatizzato"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CANCELLO_AUTO"), "")).Selected = True
            CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Text = par.IfNull(myReader2("NUM_CANCELLO_AUTO"), "")

            CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).SelectedValue = par.IfNull(myReader2("ID_RECINZIONE"), -1)
            CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Text = par.IfNull(myReader2("RESPONSABILE_TRATTAMENTO"), "")

            CType(Tab_Tutela_Generale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

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

            par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI (ID, ID_COMPLESSO, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE,ANNOTAZIONI) " _
                                & "values (:id,:id_complesso,:id_edificio,:cod_tipologia,:descrizione,:annotazioni)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "TU"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtNote"), TextBox).Text, 4000)))


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************


            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")


            ' I_TUTELA
            par.cmd.CommandText = "insert into SISCOM_MI.I_TUTELA (ID,DITTA_GESTIONE,TELEFONO_DITTA," _
                                            & "RECINZIONE,PASSI_CARRAI,NUM_PASSI_CARRAI,VIDEO_SORVEGLIANZA," _
                                            & "CANCELLO_CARRABILE,NUM_CANCELLO_CARRABILE,CANCELLO_AUTO,NUM_CANCELLO_AUTO,ID_RECINZIONE,RESPONSABILE_TRATTAMENTO) " _
                                & "values (:id,:ditta_gestione,:num_tel," _
                                        & ":recinzione,:passi_carrai,:num_passi,:video," _
                                        & ":cancello_carrabile,:num_carrabile,:cancello_auto,:num_auto,:tiporecinzione,:responsabile)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("recinzione", CType(Tab_Tutela_Generale.FindControl("cmbRecinzione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("passi_carrai", CType(Tab_Tutela_Generale.FindControl("cmbCarrai"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_passi", strToNumber(CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("video", CType(Tab_Tutela_Generale.FindControl("cmbVideoSorveglianza"), DropDownList).SelectedValue.ToString()))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cancello_carrabile", CType(Tab_Tutela_Generale.FindControl("cmbCarrabile"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_carrabile", strToNumber(CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Text)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cancello_auto", CType(Tab_Tutela_Generale.FindControl("cmbAutomatizzato"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_auto", strToNumber(CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Text)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tiporecinzione", RitornaNullSeIntegerMenoUno(CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).SelectedValue.ToString)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("responsabile", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Text, 200)))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*******************************



            '' INSERIMENTO CANCELLI
            Dim lstCancelli As System.Collections.Generic.List(Of Epifani.Cancelli)

            lstCancelli = CType(HttpContext.Current.Session.Item("LSTCANCELLI"), System.Collections.Generic.List(Of Epifani.Cancelli))

            For Each gen As Epifani.Cancelli In lstCancelli
                par.cmd.CommandText = "insert into SISCOM_MI.I_TUT_CANCELLI (ID, ID_IMPIANTO,MARCA,MODELLO,AUTOMATIZZATO,CARRABILE,DITTA_MANUTENZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_TUT_CANCELLI.NEXTVAL,:id_impianto," _
                                    & "       :marca,:modello,:auto,:carrabile,:ditta) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("marca", par.PulisciStringaInvio(gen.MARCA, 200)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", Strings.Left(gen.MODELLO, 200)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("auto", gen.AUTOMATIZZATO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("carrabile", gen.CARRABILE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(gen.DITTA_MANUTENZIONE, 200)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Cancelli")

            Next
            ''***************************************

            '' INSERIMENTO PASSI CARRABILI
            Dim lstCarrabile As System.Collections.Generic.List(Of Epifani.Carrabile)

            lstCarrabile = CType(HttpContext.Current.Session.Item("LSTCARRABILE"), System.Collections.Generic.List(Of Epifani.Carrabile))

            For Each gen As Epifani.Carrabile In lstCarrabile


                par.cmd.CommandText = "insert into SISCOM_MI.I_TUT_CARRABILE (ID, ID_IMPIANTO,NUM_LICENZA,DATA_RILASCIO) " _
                                    & "values (SISCOM_MI.SEQ_I_TUT_CARRABILE.NEXTVAL,:id_impianto,:licenza,:data) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("licenza", par.PulisciStringaInvio(gen.NUM_LICENZA, 50)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(gen.DATA_RILASCIO)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Passo Carrabile")

            Next
            ''***************************************


            '' INSERIMENTO ALLOGGI
            'Dim lstAlloggi As System.Collections.Generic.List(Of Epifani.Cancelli)

            'lstAlloggi = CType(HttpContext.Current.Session.Item("LSTALLOGGI"), System.Collections.Generic.List(Of Epifani.Alloggi))

            For Each gen As Epifani.Alloggi In lstAlloggi
                par.cmd.CommandText = "insert into SISCOM_MI.I_TUT_ALLOGGI (ID, ID_IMPIANTO,ID_UNITA_IMMOBILIARI," _
                                         & "ANTINTRUSIONE,DATA_INSTALLA_ANTINTR,DATA_RIMOZIONE_ANTINTR," _
                                         & "BLINDATA,DATA_INSTALLA_BLINDATA," _
                                         & "LASTRATURA,DATA_INSTALLA_LASTRATURA,DATA_RIMOZIONE_LASTRATURA) " _
                                    & "values (SISCOM_MI.SEQ_I_TUT_ALLOGGI.NEXTVAL,:id_impianto,:id_unita_immobiliare," _
                                           & ":anti,:data1_anti,:data2_anti,:blindatura,:data1_blindatura," _
                                           & ":lastratura,:data1_lastra,:data2_lastra) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita_immobiliare", RitornaNullSeIntegerMenoUno(Convert.ToInt32(gen.ID_UNITA_IMMOBILIARI))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anti", gen.ANTINTRUSIONE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data1_anti", par.AggiustaData(gen.DATA_INSTALLA_ANTINTR)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data2_anti", par.AggiustaData(gen.DATA_RIMOZIONE_ANTINTR)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("blindatura", gen.BLINDATA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data1_blindatura", par.AggiustaData(gen.DATA_INSTALLA_BLINDATA)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("lastratura", gen.LASTRATURA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data1_lastra", par.AggiustaData(gen.DATA_INSTALLA_LASTRATURA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data2_lastra", par.AggiustaData(gen.DATA_RIMOZIONE_LASTRATURA)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                ''*** EVENTI_IMPIANTI
                'par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Tutela Alloggi")

            Next
            ''***************************************

            'INSERISCO LA VERIFICA PERIODICHE
            Dim lstVerifiche As System.Collections.Generic.List(Of Epifani.VerificheImpianti)

            lstVerifiche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE"), System.Collections.Generic.List(Of Epifani.VerificheImpianti))

            For Each gen As Epifani.VerificheImpianti In lstVerifiche

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO) " _
                                & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico )"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "TP"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", par.PulisciStringaInvio(gen.DITTA, 100)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(gen.DATA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 4000)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(gen.ESITO)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", ""))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", gen.ESITO_DETTAGLIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(gen.MESI_VALIDITA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(gen.MESI_PREALLARME)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", par.AggiustaData(gen.DATA_SCADENZA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifica Periodica")

            Next
            '********************************


            'INSERISCO LA VERIFICA SCALE ANNUALI
            Dim lstVerifiche2 As System.Collections.Generic.List(Of Epifani.VerificheImpianti)

            lstVerifiche2 = CType(HttpContext.Current.Session.Item("LSTVERIFICHE2"), System.Collections.Generic.List(Of Epifani.VerificheImpianti))

            For Each gen As Epifani.VerificheImpianti In lstVerifiche2

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO) " _
                                & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico )"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "TS"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", par.PulisciStringaInvio(gen.DITTA, 100)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(gen.DATA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 4000)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(gen.ESITO)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", ""))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", gen.ESITO_DETTAGLIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(gen.MESI_VALIDITA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(gen.MESI_PREALLARME)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", par.AggiustaData(gen.DATA_SCADENZA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifica Annuale CHIUSURA/INGRESSO Scale")
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



            '**** AGGIORNO I CANCELLI
            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato


            par.cmd.CommandText = "select SISCOM_MI.I_TUT_CANCELLI.ID,SISCOM_MI.I_TUT_CANCELLI.CARRABILE,SISCOM_MI.I_TUT_CANCELLI.AUTOMATIZZATO," _
                                       & "SISCOM_MI.I_TUT_CANCELLI.MARCA,SISCOM_MI.I_TUT_CANCELLI.MODELLO,SISCOM_MI.I_TUT_CANCELLI.DITTA_MANUTENZIONE " _
                               & " from SISCOM_MI.I_TUT_CANCELLI " _
                               & " where SISCOM_MI.I_TUT_CANCELLI.ID_IMPIANTO = " & vIdImpianto _
                               & " order by SISCOM_MI.I_TUT_CANCELLI.ID "

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds2 As New Data.DataSet()

            da2.Fill(ds2, "I_TUT_CANCELLI")

            CType(Tab_Tutela_Dettagli.FindControl("DataGridCA"), DataGrid).DataSource = ds2
            CType(Tab_Tutela_Dettagli.FindControl("DataGridCA"), DataGrid).DataBind()
            ds2.Dispose()
            '************************


            '**** AGGIORNO I PASSI CARRABILI
            par.cmd.CommandText = "select SISCOM_MI.I_TUT_CARRABILE.ID,SISCOM_MI.I_TUT_CARRABILE.NUM_LICENZA," _
                                & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_CARRABILE.DATA_RILASCIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_RILASCIO"" " _
                      & " from SISCOM_MI.I_TUT_CARRABILE " _
                      & " where SISCOM_MI.I_TUT_CARRABILE.ID_IMPIANTO = " & vIdImpianto _
                      & " order by SISCOM_MI.I_TUT_CARRABILE.NUM_LICENZA "


            Dim daP As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsP As New Data.DataSet()

            daP.Fill(dsP, "I_TUT_CARRABILE")

            CType(Tab_Tutela_Carraio.FindControl("DataGridPasso"), DataGrid).DataSource = dsP
            CType(Tab_Tutela_Carraio.FindControl("DataGridPasso"), DataGrid).DataBind()
            dsP.Dispose()
            '************************

            '**** AGGIORNO ALLOGGI
            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato

            par.cmd.CommandText = "select SISCOM_MI.I_TUT_ALLOGGI.ID,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO, " _
                                    & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO, " _
                                    & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO,SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.ANTINTRUSIONE,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_ANTINTR,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_ANTINTR," _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_ANTINTR,'YYYYmmdd'),'DD/MM/YYYY') as DATA_RIMOZIONE_ANTINTR, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.BLINDATA,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_BLINDATA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_BLINDATA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.LASTRATURA,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_LASTRATURA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_LASTRATURA, " _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_LASTRATURA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_RIMOZIONE_LASTRATURA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI " _
                                & "from SISCOM_MI.I_TUT_ALLOGGI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                & "where SISCOM_MI.I_TUT_ALLOGGI.ID_IMPIANTO =" & vIdImpianto & " and " _
                                 & "      SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI=SISCOM_MI.UNITA_IMMOBILIARI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD and " _
                                 & "      SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE " _
                                & "order by SISCOM_MI.I_TUT_ALLOGGI.ID"

            Dim daA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsA As New Data.DataSet()

            daA.Fill(dsA, "SISCOM_MI.I_TUT_ALLOGGI")

            CType(Tab_Tutela_Alloggi.FindControl("DataGridA"), DataGrid).DataSource = dsA
            CType(Tab_Tutela_Alloggi.FindControl("DataGridA"), DataGrid).DataBind()
            dsA.Dispose()
            '************************


            '*** VERIFICHE PERIODICHE
            par.cmd.CommandText = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                        & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                        & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='TP'" _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "

            Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsV As New Data.DataSet()

            daV.Fill(dsV, "IMPIANTI_VERIFICHE ")

            CType(Tab_Tutela_Verifiche1.FindControl("DataGrid1"), DataGrid).DataSource = dsV
            CType(Tab_Tutela_Verifiche1.FindControl("DataGrid1"), DataGrid).DataBind()
            dsV.Dispose()
            '*******************************


            '*** VERIFICHE ANNUALI SCALE
            par.cmd.CommandText = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                        & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                        & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='TS'" _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "

            Dim daV2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsV2 As New Data.DataSet()

            daV.Fill(dsV2, "IMPIANTI_VERIFICHE ")

            CType(Tab_Tutela_Verifiche2.FindControl("DataGrid1"), DataGrid).DataSource = dsV2
            CType(Tab_Tutela_Verifiche2.FindControl("DataGrid1"), DataGrid).DataBind()
            dsV2.Dispose()
            '*******************************


            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If

            'ABILITO I BOTTONI DELLE VERIFICHE
            CType(Tab_Tutela_Verifiche1.FindControl("btnAgg"), ImageButton).Visible = True
            CType(Tab_Tutela_Verifiche1.FindControl("btnApri"), ImageButton).Visible = True
            CType(Tab_Tutela_Verifiche1.FindControl("btnVisualizza"), ImageButton).Visible = True
            CType(Tab_Tutela_Verifiche1.FindControl("btnElimina"), ImageButton).Visible = True

            CType(Tab_Tutela_Verifiche2.FindControl("btnAgg"), ImageButton).Visible = True
            CType(Tab_Tutela_Verifiche2.FindControl("btnApri"), ImageButton).Visible = True
            CType(Tab_Tutela_Verifiche2.FindControl("btnVisualizza"), ImageButton).Visible = True
            CType(Tab_Tutela_Verifiche2.FindControl("btnElimina"), ImageButton).Visible = True
            '**************************


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
            par.cmd.CommandText = "update SISCOM_MI.IMPIANTI set ID_COMPLESSO=:id_complesso, ID_EDIFICIO=:id_edificio,COD_TIPOLOGIA=:cod_tipologia,DESCRIZIONE=:descrizione,ANNOTAZIONI=:annotazioni where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "TU"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtNote"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")


            '*** I_TUTELA
            par.cmd.CommandText = " update SISCOM_MI.I_TUTELA " _
                                & " set DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel, " _
                                & "     RECINZIONE=:recinzione,PASSI_CARRAI=:passi_carrai,NUM_PASSI_CARRAI=:num_passi,VIDEO_SORVEGLIANZA=:video," _
                                & "     CANCELLO_CARRABILE=:cancello_carrabile,NUM_CANCELLO_CARRABILE=:num_carrabile,CANCELLO_AUTO=:cancello_auto,NUM_CANCELLO_AUTO=:num_auto," _
                                & "     ID_RECINZIONE=:tiporecinzione,RESPONSABILE_TRATTAMENTO=:responsabile " _
                                & " where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("recinzione", CType(Tab_Tutela_Generale.FindControl("cmbRecinzione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("passi_carrai", CType(Tab_Tutela_Generale.FindControl("cmbCarrai"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_passi", strToNumber(CType(Tab_Tutela_Generale.FindControl("txtNumCarrai"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("video", CType(Tab_Tutela_Generale.FindControl("cmbVideoSorveglianza"), DropDownList).SelectedValue.ToString()))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cancello_carrabile", CType(Tab_Tutela_Generale.FindControl("cmbCarrabile"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_carrabile", strToNumber(CType(Tab_Tutela_Generale.FindControl("txtNumCarrabile"), TextBox).Text)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cancello_auto", CType(Tab_Tutela_Generale.FindControl("cmbAutomatizzato"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_auto", strToNumber(CType(Tab_Tutela_Generale.FindControl("txtNumAutomatizzato"), TextBox).Text)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tiporecinzione", RitornaNullSeIntegerMenoUno(CType(Tab_Tutela_Generale.FindControl("cmbTipoRecinzione"), DropDownList).SelectedValue.ToString)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("responsabile", Strings.Left(CType(Tab_Tutela_Generale.FindControl("txtResponsabile"), TextBox).Text, 200)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************


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

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO TUTELA IMMOBILE"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Tutela Immobile del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
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
