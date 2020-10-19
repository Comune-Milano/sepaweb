Imports System.Collections

Partial Class IMPIANTI_Imp_RiscaldamentoA
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""

    Public TabberHide As String = ""

    Public Visibile As String = ""

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sOrdinamento As String
    Public sVerifiche As String

    Public sProvenienza As String

    Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))


        If Not IsPostBack Then

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            sProvenienza = Request.QueryString("SL")

            vIdImpianto = 0
            vIdImpianto = Session.Item("ID")

            lstEdifici.Clear()


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
            CType(Me.Page.FindControl("txtTIPO_IMPIANTO"), TextBox).Text = "TA"

            SettaggioCampi()
            AbilitazioneOggetti(True)

            If vIdImpianto <> 0 Then
                'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)
                TabberHide = "tabbertab"
                VisualizzaDati()
                txtindietro.Text = 0
                Tabber1 = "tabbertabdefault"
            Else
                '*** DEVO DISABILITARE IL TAB COMPONENTI
                'NuovoImpianto()
                TabberHide = "tabbertabhide"
                Me.txtIdImpianto.Text = -1

                Me.btnElimina.Visible = False

                'Visibile = "style=" & Chr(34) & "visibility:hidden" & Chr(34)

                Tabber1 = "tabbertabdefault"
                txtindietro.Text = 1
                'Me.LblRiepilogo.Visible = False
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


            '*** FORM GENERALE
            For Each CTRL In Me.TabGeneraleA.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** TAB CERTIFICAZIONI
            For Each CTRL In Me.Tab_Termico_Certificazioni.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** TAB RENDIMENTO
            'CType(Tab_Termico_Rendimento.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            '*** CONTROLLO DATA
            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            DrLEdificio.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(TabGeneraleA.FindControl("txtAnnoRealizzazione"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            ' per impianto termico AUTONOMO
            CType(Tab_Termico_Certificazioni.FindControl("LblLibretto"), Label).Text = "Libretto Impianto"

            'DATA DISMISSIONI CT non SERVE
            CType(Tab_Termico_Certificazioni.FindControl("lblDataCT"), Label).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Visible = False

            'TRATTAMENTO ACQUA non SERVE
            CType(Tab_Termico_Certificazioni.FindControl("LblTrattamentoAcqua"), Label).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).Visible = False

            'LICENZA UTF non SERVE
            CType(Tab_Termico_Certificazioni.FindControl("lblLicenzaUTF"), Label).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).Visible = False

            'DECRETO PREFETTIZIO  non SERVE
            CType(Tab_Termico_Certificazioni.FindControl("lblDecreto"), Label).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Visible = False

            ' Pratica VVF non SERVE
            CType(Tab_Termico_Certificazioni.FindControl("lblCPI"), Label).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("lblDataRilascio"), Label).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("lblDataScadenza"), Label).Visible = False

            CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Visible = False
            CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Visible = False
            '********************

            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_TermicoAutonomo35.png';</script>")
            End If


            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 4, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If
        End If


            If CType(TabGeneraleA.FindControl("cmbVentilazione"), DropDownList).SelectedItem.ToString = "SI" Then
                CType(TabGeneraleA.FindControl("txtVentilazione"), TextBox).Enabled = True

                CType(TabGeneraleA.FindControl("lblVentilazione"), Label).Enabled = True
                CType(TabGeneraleA.FindControl("lblVentilazioneCM2"), Label).Enabled = True
            Else
                CType(TabGeneraleA.FindControl("txtVentilazione"), TextBox).Text = ""
                CType(TabGeneraleA.FindControl("txtVentilazione"), TextBox).Enabled = False

                CType(TabGeneraleA.FindControl("lblVentilazione"), Label).Enabled = False
                CType(TabGeneraleA.FindControl("lblVentilazioneCM2"), Label).Enabled = False
            End If


            If CType(TabGeneraleA.FindControl("cmbAreazione"), DropDownList).SelectedItem.ToString = "SI" Then
                CType(TabGeneraleA.FindControl("txtAreazione"), TextBox).Enabled = True

                CType(TabGeneraleA.FindControl("lblAreazione"), Label).Enabled = True
                CType(TabGeneraleA.FindControl("lblAreazioneCM2"), Label).Enabled = True
            Else
                CType(TabGeneraleA.FindControl("txtAreazione"), TextBox).Text = ""
                CType(TabGeneraleA.FindControl("txtAreazione"), TextBox).Enabled = False

                CType(TabGeneraleA.FindControl("lblAreazione"), Label).Enabled = False
                CType(TabGeneraleA.FindControl("lblAreazioneCM2"), Label).Enabled = False
            End If


            'CType(TabGeneraleA.FindControl("txtCapacita"), TextBox).Enabled = True
            'CType(TabGeneraleA.FindControl("lblCapacita"), Label).Enabled = True


            'If CType(TabGeneraleA.FindControl("cmbCombustibile"), DropDownList).Text = "1" Then 'METANO
            '    CType(TabGeneraleA.FindControl("cmbTipoSerbatoio"), DropDownList).Text = ""
            '    CType(TabGeneraleA.FindControl("cmbTipoSerbatoio"), DropDownList).Enabled = False
            '    CType(TabGeneraleA.FindControl("lblSerbatoio"), Label).Enabled = False

            '    CType(TabGeneraleA.FindControl("txtCapacita"), TextBox).Text = ""
            '    CType(TabGeneraleA.FindControl("txtCapacita"), TextBox).Enabled = False
            '    CType(TabGeneraleA.FindControl("lblCapacita"), Label).Enabled = False
            'End If

            'If CType(TabGeneraleA.FindControl("cmbCombustibile"), DropDownList).Text = "3" Then  'GASOLIO
            '    CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Enabled = True
            '    CType(Tab_Termico_Certificazioni.FindControl("lblDecreto"), Label).Enabled = True
            'Else
            '    CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Text = ""
            '    CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Enabled = False
            '    CType(Tab_Termico_Certificazioni.FindControl("lblDecreto"), Label).Enabled = False
            'End If


            'txtindietro.Text = txtindietro.Text - 1

            ' ''TxtDataInizio.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            ' ''txtDataOrdine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            ' ''txtDatFine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            ' ''TxtDataInizio.Attributes.Add("onfocus", "javascript:selectText(this);")
            ' ''txtDataOrdine.Attributes.Add("onfocus", "javascript:selectText(this);")
            ' ''txtDatFine.Attributes.Add("onfocus", "javascript:selectText(this);")
            'txtindietro.Text = txtindietro.Text - 1



    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""
        Tabber6 = ""

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
        End Select

        If vIdImpianto <> 0 Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
        End If


    End Sub

    Private Sub SettaggioCampi()

        '' CONNESSIONE DB
        'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        'par.SettaCommand(par)


        'CARICO COMBO COMPLESSI
        Dim gest As Integer
        gest = 0

        Try

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    If par.OracleConn.State = Data.ConnectionState.Closed Then
            '        par.OracleConn.Open()
            '        par.SettaCommand(par)
            '        'HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
            '    End If

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
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_USO_TERMICI order by ID"
            myReader1 = par.cmd.ExecuteReader
            'cmbTipoUso.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                If par.IfNull(myReader1("DESCRIZIONE"), "") = "RISCALDAMENTO" Or par.IfNull(myReader1("DESCRIZIONE"), "") = "COMBINATA" Then
                    cmbTipoUso.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End If
            End While
            cmbTipoUso.Text = cmbTipoUso.Items(0).Text
            myReader1.Close()
            '*********************


            '***  TIPOLOGIA_COMBUSTIBILI (Tab Generale)
            'par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_COMBUSTIBILI order by ID"
            'myReader1 = par.cmd.ExecuteReader
            'CType(TabGeneraleA.FindControl("cmbCombustibile"), DropDownList).Items.Add(New ListItem(" ", -1))

            'While myReader1.Read
            '    CType(TabGeneraleA.FindControl("cmbCombustibile"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'CType(TabGeneraleA.FindControl("cmbCombustibile"), DropDownList).Text = "-1"
            'myReader1.Close()

            'par.OracleConn.Close()


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
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            'par.OracleConn.Open()
            'par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
            'End If

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
            'par.OracleConn.Close()

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


        ' ''If Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "UNI_IMMOB" Then
        ' ''    FiltraEdifici()
        ' ''    Me.cmbUnitaImmob.Items.Add(New ListItem(" SELEZIONARE UN EDIFICIO ", -1))
        ' ''    FiltraUnitaComuni()
        ' ''ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "UNI_COM" Then
        ' ''    FiltraEdifici()
        ' ''    FiltraUnitaComuni()
        ' ''ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "EDIF" Then
        ' ''    FiltraEdifici()
        ' ''Else

        ' ''    Me.cmbUnitaComune.Items.Clear()
        ' ''    CaricaEdifici()
        ' ''End If

    End Sub

    Protected Sub cmbComplesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Private Sub FiltraEdifici()
        Dim StringaSql As String
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
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)


                'HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                'End If


                Dim gest As Integer = 0
                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))


                If gest <> 0 Then
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"
                Else
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"
                End If
                StringaSql = par.cmd.CommandText

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


    Private Sub FiltraUnitaImmob()

        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.DrLEdificio.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.cmbUnitaImmob.Items.Clear()
                Me.cmbUnitaImmob.Items.Add(New ListItem(" ", -1))

                par.cmd.CommandText = "  select ID, 'COD.'||COD_UNITA_IMMOBILIARE||' - SCALA '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala)||' - INTERNO '||INTERNO as DESCRIZIONE from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToCharArray
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbUnitaImmob.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
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


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        '*** FORM PRINCIPALE
        Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

        Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
        FiltraEdifici()

        Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")
        FiltraUnitaImmob()

        Me.cmbUnitaImmob.SelectedValue = par.IfNull(myReader1("ID_UNITA_IMMOBILIARE"), "-1")

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

        CType(TabGeneraleA.FindControl("txtDitta"), TextBox).Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
        CType(TabGeneraleA.FindControl("txtAnnoRealizzazione"), TextBox).Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


        par.cmd.CommandText = "select * from SISCOM_MI.I_TERMICI_AUTONOMI where SISCOM_MI.I_TERMICI_AUTONOMI.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** FORM PRINCIPALE
            Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")

            '*** TAB GENERALE
            CType(TabGeneraleA.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(TabGeneraleA.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

            CType(TabGeneraleA.FindControl("cmbApparecchio"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_APPARECCHIO"), "")
            CType(TabGeneraleA.FindControl("cmbTipoUbicazione"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_UBICAZIONE"), "")
            CType(TabGeneraleA.FindControl("cmbPosizionamento"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_POSIZIONAMENTO"), "")
            CType(TabGeneraleA.FindControl("cmbScaricoFumi"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_SCARICO_FUMI"), "")

            CType(TabGeneraleA.FindControl("cmbAreazione"), DropDownList).Items.FindByValue(par.IfNull(myReader2("FORO_AREAZIONE"), "")).Selected = True
            CType(TabGeneraleA.FindControl("txtAreazione"), TextBox).Text = par.IfNull(myReader2("DIMENSIONE_FORO_AREAZIONE"), "")

            CType(TabGeneraleA.FindControl("cmbVentilazione"), DropDownList).Items.FindByValue(par.IfNull(myReader2("FORO_VENTILAZIONE"), "")).Selected = True
            CType(TabGeneraleA.FindControl("txtVentilazione"), TextBox).Text = par.IfNull(myReader2("DIMENSIONE_FORO_VENTILAZIONE"), "")

            CType(TabGeneraleA.FindControl("cmbCappa"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_CAPPA_PIANO_COTTURA"), "")

            CType(TabGeneraleA.FindControl("txtPotenza"), TextBox).Text = par.IfNull(myReader2("POTENZA"), "")
            CType(TabGeneraleA.FindControl("txtConsumo"), TextBox).Text = par.IfNull(myReader2("CONSUMO_MEDIO"), "")

            CType(TabGeneraleA.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


            '*** TAB CERTIFICAZIONI
            CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LIBRETTO"), "")).Selected = True
            'CType(TabGeneraleA.FindControl("cmbExUNI"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CERT_EX_UNI_8364"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF_LG_46_90"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DECR_PREFETTIZIO_SERBATOI"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LICENZA_UTF"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_ISPESL"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).Items.FindByValue(par.IfNull(myReader2("TRATTAMENTO_ACQUA"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CONT_ENERGIA"), "")).Selected = True
            'CType(TabGeneraleA.FindControl("cmbDenuncia"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DENUNCIA_IMPIANTO"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_PRATICA_ISPESL"), ""))
            'CType(Tab_Termico_Certificazioni.FindControl("txtDataCT"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_DISMISSIONE_CT"), ""))

            CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_VVF"), "")).Selected = True
            CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_RILASCIO_VVF"), ""))
            CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_SCADENZA_VVF"), ""))

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
                ' LEGGO IMP_TERMICI


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

        If par.IfEmpty(Me.cmbTipoUso.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Selezionare la tipologia d\'uso dell\'impianto!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If CType(TabGeneraleA.FindControl("txtAnnoRealizzazione"), TextBox).Text = "dd/mm/YYYY" Then
            CType(TabGeneraleA.FindControl("txtAnnoRealizzazione"), TextBox).Text = ""
        End If

        If CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = "dd/mm/YYYY" Then
            CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text = ""
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

            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI (ID, ID_COMPLESSO, ID_EDIFICIO,ID_UNITA_IMMOBILIARE,COD_TIPOLOGIA,DESCRIZIONE,ANNO_COSTRUZIONE,DITTA_COSTRUTTRICE,ANNOTAZIONI) " _
                                & "values (:id,:id_complesso,:id_edificio,:id_unita,:cod_tipologia,:descrizione,:anno,:ditta,:annotazioni)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbUnitaImmob.SelectedValue, -1)))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "TA"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(CType(TabGeneraleA.FindControl("txtAnnoRealizzazione"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(CType(TabGeneraleA.FindControl("txtDitta"), TextBox).Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(TabGeneraleA.FindControl("txtNote"), TextBox).Text, 4000)))


            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = ""

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")
            '************************


            par.cmd.CommandText = "insert into SISCOM_MI.I_TERMICI_AUTONOMI (ID,ID_TIPOLOGIA_USO, DITTA_GESTIONE,TELEFONO_DITTA," _
                                            & " POTENZA,CONSUMO_MEDIO,PRATICA_ISPESL,DATA_PRATICA_ISPESL,LIBRETTO,CONT_ENERGIA," _
                                            & " TRATTAMENTO_ACQUA,LICENZA_UTF,DICH_CONF_LG_46_90,DECR_PREFETTIZIO_SERBATOI," _
                                            & " PRATICA_VVF,DATA_RILASCIO_VVF,DATA_SCADENZA_VVF," _
                                            & " TIPO_APPARECCHIO, TIPO_UBICAZIONE,TIPO_POSIZIONAMENTO, " _
                                            & " FORO_AREAZIONE,DIMENSIONE_FORO_AREAZIONE,FORO_VENTILAZIONE," _
                                            & " DIMENSIONE_FORO_VENTILAZIONE,TIPO_CAPPA_PIANO_COTTURA, TIPO_SCARICO_FUMI) " _
                                 & "values (:id,:id_tipologia,:ditta,:telefono," _
                                        & ":potenza,:consumo,:ispesl,:data_ispesl,:libretto,:energia," _
                                        & ":trattamento,:utf,:conformita,:decreto,:vvf,:data_vvf,:scadenza_vvf," _
                                        & ":apparecchio,:ubicazione,:posizionamento,:foro,:dimensione_foro,:ventilazione," _
                                        & ":dimensione_ventilazione,:cappa,:scarico_fumi)"


            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbTipoUso.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(CType(TabGeneraleA.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("telefono", Strings.Left(CType(TabGeneraleA.FindControl("txtNumTelefonico"), TextBox).Text, 50)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("potenza", strToNumber(CType(TabGeneraleA.FindControl("txtPotenza"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("consumo", strToNumber(CType(TabGeneraleA.FindControl("txtConsumo"), TextBox).Text)))

            ' CERTIFICAZIONI
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ispesl", CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ispesl", par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("energia", CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("trattamento", CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("utf", CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conformita", CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("decreto", CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("vvf", CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_vvf", par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scadenza_vvf", par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Text)))
            '*************

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("apparecchio", CType(TabGeneraleA.FindControl("cmbApparecchio"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", CType(TabGeneraleA.FindControl("cmbTipoUbicazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("posizionamento", CType(TabGeneraleA.FindControl("cmbPosizionamento"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("foro", CType(TabGeneraleA.FindControl("cmbAreazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dimensione_foro", "1")) 'strToNumber(CType(TabGeneraleA.FindControl("txtAreazione"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ventilazione", CType(TabGeneraleA.FindControl("cmbVentilazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dimensione_ventilazione", "1")) 'strToNumber(CType(TabGeneraleA.FindControl("txtVentilazione"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cappa", CType(TabGeneraleA.FindControl("cmbCappa"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scarico_fumi", CType(TabGeneraleA.FindControl("cmbScaricoFumi"), DropDownList).SelectedValue.ToString()))


            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = ""


            '*** INSERIMENTO GENERATORI
            Dim lstGeneratori As System.Collections.Generic.List(Of Epifani.Generatori)

            lstGeneratori = CType(HttpContext.Current.Session.Item("LSTGENERATORI"), System.Collections.Generic.List(Of Epifani.Generatori))

            For Each gen As Epifani.Generatori In lstGeneratori

                par.cmd.CommandText = "insert into SISCOM_MI.GENERATORI_TERMICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE,FLUIDO_TERMOVETTORE,MARC_EFF_ENERGETICA) " _
                                    & "values (SISCOM_MI.SEQ_GENERATORI_TERMICI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                                        & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "','" _
                                        & par.PulisciStrSql(gen.FLUIDO_TERMOVETTORE) & "','" & gen.MARC_EFF_ENERGETICA & "')"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Generatori")

            Next
            '********************************


            '*** INSERIMENTO BRUCIATORI
            Dim lstBruciatori As System.Collections.Generic.List(Of Epifani.Bruciatori)

            lstBruciatori = CType(HttpContext.Current.Session.Item("LSTBRUCIATORI"), System.Collections.Generic.List(Of Epifani.Bruciatori))

            For Each gen As Epifani.Bruciatori In lstBruciatori

                par.cmd.CommandText = "insert into SISCOM_MI.BRUCIATORI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,CAMPO_FUNZIONAMENTO,CAMPO_FUNZIONAMENTO_MAX,NOTE) " _
                    & "values (SISCOM_MI.SEQ_BRUCIATORI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(gen.MODELLO, 200)) & "',' " _
                        & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
                        & par.VirgoleInPunti(par.IfEmpty(gen.CAMPO_FUNZIONAMENTO, "Null")) & "," & par.VirgoleInPunti(par.IfEmpty(gen.CAMPO_FUNZIONAMENTO_MAX, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "')"


                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Bruciatori")

            Next
            '********************************


            '*** INSERIMENTO POMPE DI CIRCOLAZIONE
            'Dim lstPompe As System.Collections.Generic.List(Of Epifani.Pompe)

            'lstPompe = CType(HttpContext.Current.Session.Item("LSTPOMPE"), System.Collections.Generic.List(Of Epifani.Pompe))

            'For Each gen As Epifani.Pompe In lstPompe

            '    par.cmd.CommandText = "insert into SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE) " _
            '                        & "values (SISCOM_MI.SEQ_POMPE_CIRCOLAZIONE_TERMICI.NEXTVAL," & vIdImpianto & ",'" & par.PulisciStrSql(gen.MODELLO) & "',' " _
            '                            & par.PulisciStrSql(gen.MATRICOLA) & "','" & gen.ANNO_COSTRUZIONE & "'," _
            '                            & par.VirgoleInPunti(par.IfEmpty(gen.POTENZA, "Null")) & ",'" & Strings.Left(par.PulisciStrSql(gen.NOTE), 300) & "')"

            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""

            '    '*** EVENTI_IMPIANTI
            '    par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Circolazione")

            'Next
            '********************************

            '*** INSERIMENTO CONTROLLO DEL RENDIMENTO DU COMBUSTIONE
            'Dim lstControlloRendimento As System.Collections.Generic.List(Of Epifani.ControlloRendimento)

            'lstControlloRendimento = CType(HttpContext.Current.Session.Item("LSTCONTROLLORENDIMENTO"), System.Collections.Generic.List(Of Epifani.ControlloRendimento))

            'For Each gen As Epifani.ControlloRendimento In lstControlloRendimento
            '    par.cmd.Parameters.Clear()

            '    par.cmd.CommandText = "insert into SISCOM_MI.RENDIMENTO_TERMICI (ID,ID_IMPIANTO,TEMP_FUMI,TEMP_AMB,O2,CO2," _
            '                                                        & "BACHARACH,CO,RENDIMENTO,TIRAGGIO,DATA_ESAME,ESECUTORE) " _
            '                    & " values (SISCOM_MI.SEQ_RENDIMENTO_TERMICI.NEXTVAL,:id_impianto,:tempi_fumi,:tempi_amb,:o2,:co2,:bacharach,:co," _
            '                             & ":rendimento,:tiraggio,:data_esame,:esecutore)"

            '    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdREND)) ' "SISCOM_MI.SEQ_RENDIMENTO_I_TERMICI.NEXTVAL"))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tempi_fumi", strToNumber(gen.TEMP_FUMI)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tempi_amb", strToNumber(gen.TEMP_AMB)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("o2", strToNumber(gen.O2)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("co2", strToNumber(gen.CO2)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("bacharach", strToNumber(gen.BACHARACH)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("co", strToNumber(gen.CO)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rendimento", strToNumber(gen.RENDIMENTO)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tiraggio", strToNumber(gen.TIRAGGIO)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_esame", par.AggiustaData(gen.DATA_ESAME)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esecutore", Strings.Left(gen.ESECUTORE, 100)))


            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            '    par.cmd.Parameters.Clear()

            '    '*** EVENTI_IMPIANTI
            '    par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Controlli del rendimento di combustione")

            'Next
            '**********************


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

            '*** BRUCIATORI
            par.cmd.CommandText = " select SISCOM_MI.BRUCIATORI.ID,SISCOM_MI.BRUCIATORI.MODELLO," _
                                        & "SISCOM_MI.BRUCIATORI.MATRICOLA,SISCOM_MI.BRUCIATORI.NOTE," _
                                           & "SISCOM_MI.BRUCIATORI.ANNO_COSTRUZIONE,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO_MAX " _
                                  & " from SISCOM_MI.BRUCIATORI " _
                                  & " where SISCOM_MI.BRUCIATORI.ID_IMPIANTO = " & vIdImpianto _
                                  & " order by SISCOM_MI.BRUCIATORI.MODELLO "

            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds1 As New Data.DataSet()

            da1.Fill(ds1, "BRUCIATORI")

            CType(TabDettagli1.FindControl("DataGrid1"), DataGrid).DataSource = ds1
            CType(TabDettagli1.FindControl("DataGrid1"), DataGrid).DataBind()
            ds1.Dispose()


            '*** GENERATORI
            par.cmd.CommandText = "select SISCOM_MI.GENERATORI_TERMICI.ID,SISCOM_MI.GENERATORI_TERMICI.MODELLO," _
                        & "SISCOM_MI.GENERATORI_TERMICI.MATRICOLA,SISCOM_MI.GENERATORI_TERMICI.NOTE," _
                           & "SISCOM_MI.GENERATORI_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.GENERATORI_TERMICI.POTENZA,SISCOM_MI.GENERATORI_TERMICI.MARC_EFF_ENERGETICA,SISCOM_MI.GENERATORI_TERMICI.FLUIDO_TERMOVETTORE " _
                  & " from SISCOM_MI.GENERATORI_TERMICI " _
                  & " where SISCOM_MI.GENERATORI_TERMICI.ID_IMPIANTO = " & vIdImpianto _
                  & " order by SISCOM_MI.GENERATORI_TERMICI.MODELLO "

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds2 As New Data.DataSet()

            da2.Fill(ds2, "GENERATORI_TERMICI")

            CType(TabDettagli1.FindControl("DataGrid2"), DataGrid).DataSource = ds2
            CType(TabDettagli1.FindControl("DataGrid2"), DataGrid).DataBind()
            ds2.Dispose()


            '*** POMPE
            'par.cmd.CommandText = "select SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO," _
            '            & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MATRICOLA,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.NOTE," _
            '               & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.POTENZA " _
            '      & " from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI " _
            '      & " where SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID_IMPIANTO = " & vIdImpianto _
            '      & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO "

            'Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim ds3 As New Data.DataSet()

            'da3.Fill(ds3, "POMPE_CIRCOLAZIONE_TERMICI")

            'CType(Tab_Termico_PompeA.FindControl("DataGrid3"), DataGrid).DataSource = ds3
            'CType(Tab_Termico_PompeA.FindControl("DataGrid3"), DataGrid).DataBind()
            'ds3.Dispose()
            '*******************************



            '*** CONTROLLO DEL RENDIMENTO DU COMBUSTIONE
            'par.cmd.CommandText = "select ID,DATA_ESAME," _
            '            & "ESECUTORE,TEMP_FUMI," _
            '               & "TEMP_AMB,O2," _
            '               & "CO2,BACHARACH," _
            '               & "CO,RENDIMENTO," _
            '               & "TIRAGGIO " _
            '      & " from SISCOM_MI.RENDIMENTO_TERMICI " _
            '      & " where SISCOM_MI.RENDIMENTO_TERMICI.ID_IMPIANTO = " & vIdImpianto _
            '      & " order by SISCOM_MI.RENDIMENTO_TERMICI.DATA_ESAME "

            'Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim ds4 As New Data.DataSet()

            'da4.Fill(ds4, "RENDIMENTO_TERMICI")

            'CType(Tab_Termico_Rendimento.FindControl("DataGrid1"), DataGrid).DataSource = ds4
            'CType(Tab_Termico_Rendimento.FindControl("DataGrid1"), DataGrid).DataBind()
            'ds4.Dispose()
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

            par.cmd.CommandText = "update SISCOM_MI.IMPIANTI  set " _
                                         & " ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & ", ID_EDIFICIO=" & RitornaNullSeMenoUno(Me.DrLEdificio.SelectedValue.ToString) & "," _
                                         & " ID_UNITA_IMMOBILIARE=" & RitornaNullSeMenoUno(Me.cmbUnitaImmob.SelectedValue.ToString) & "," _
                                         & " DESCRIZIONE='" & par.PulisciStrSql(Me.txtDenominazione.Text) & "'," _
                                         & " COD_TIPOLOGIA='TA'," _
                                         & " DITTA_COSTRUTTRICE='" & par.PulisciStrSql(CType(TabGeneraleA.FindControl("txtDitta"), TextBox).Text) & "'," _
                                         & " ANNO_COSTRUZIONE='" & par.AggiustaData(CType(TabGeneraleA.FindControl("txtAnnoRealizzazione"), TextBox).Text) & "'," _
                                         & " ANNOTAZIONI='" & par.PulisciStrSql(CType(TabGeneraleA.FindControl("txtNote"), TextBox).Text) & "'" _
                                         & " where ID = " & vIdImpianto

            par.cmd.ExecuteNonQuery()


            '****************************************+                
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = ""

            par.cmd.CommandText = "update SISCOM_MI.I_TERMICI_AUTONOMI set " _
                                    & " ID_TIPOLOGIA_USO=:id_tipologia, DITTA_GESTIONE=:ditta,TELEFONO_DITTA=:telefono," _
                                    & " POTENZA=:potenza,CONSUMO_MEDIO=:consumo,PRATICA_ISPESL=:ispesl,DATA_PRATICA_ISPESL=:data_ispesl," _
                                    & " LIBRETTO=:libretto,CONT_ENERGIA=:energia,TRATTAMENTO_ACQUA=:trattamento,LICENZA_UTF=:utf," _
                                    & " DICH_CONF_LG_46_90=:conformita,DECR_PREFETTIZIO_SERBATOI=:decreto,PRATICA_VVF=:vvf,DATA_RILASCIO_VVF=:data_vvf," _
                                    & " DATA_SCADENZA_VVF=:scadenza_vvf,TIPO_APPARECCHIO=:apparecchio,TIPO_UBICAZIONE=:ubicazione,TIPO_POSIZIONAMENTO=:posizionamento," _
                                    & " FORO_AREAZIONE=:foro,DIMENSIONE_FORO_AREAZIONE=:dimensione_foro,FORO_VENTILAZIONE=:ventilazione,DIMENSIONE_FORO_VENTILAZIONE=:dimensione_ventilazione," _
                                    & " TIPO_CAPPA_PIANO_COTTURA=:cappa,TIPO_SCARICO_FUMI=:scarico_fumi " _
                              & " where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipologia", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbTipoUso.SelectedValue)))) 'RitornaNullSeMenoUno(Me.cmbTipoUso.SelectedValue.ToString)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(CType(TabGeneraleA.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("telefono", Strings.Left(CType(TabGeneraleA.FindControl("txtNumTelefonico"), TextBox).Text, 50)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("potenza", strToNumber(CType(TabGeneraleA.FindControl("txtPotenza"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("consumo", strToNumber(CType(TabGeneraleA.FindControl("txtConsumo"), TextBox).Text)))

            ' CERTIFICAZIONI
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ispesl", CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ispesl", par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("energia", CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("trattamento", CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("utf", CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conformita", CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("decreto", CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("vvf", CType(Tab_Termico_Certificazioni.FindControl("cmbCPI"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_vvf", par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataRilascio"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scadenza_vvf", par.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataScadenza"), TextBox).Text)))
            '*************

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("apparecchio", CType(TabGeneraleA.FindControl("cmbApparecchio"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", CType(TabGeneraleA.FindControl("cmbTipoUbicazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("posizionamento", CType(TabGeneraleA.FindControl("cmbPosizionamento"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("foro", CType(TabGeneraleA.FindControl("cmbAreazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dimensione_foro", strToNumber(CType(TabGeneraleA.FindControl("txtAreazione"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ventilazione", CType(TabGeneraleA.FindControl("cmbVentilazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dimensione_ventilazione", strToNumber(CType(TabGeneraleA.FindControl("txtVentilazione"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cappa", CType(TabGeneraleA.FindControl("cmbCappa"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scarico_fumi", CType(TabGeneraleA.FindControl("cmbScaricoFumi"), DropDownList).SelectedValue.ToString()))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = ""
            '*******************************************


            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")


            'AggiornaImpiantiEdifici()

            par.myTrans.Commit() '

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

            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans
            'par.myTrans.Rollback()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            'HttpContext.Current.Session.Remove("LSTGENERATORI")
            'HttpContext.Current.Session.Remove("LSTBRUCIATORI")


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

            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans
            'par.myTrans.Rollback()


            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            'HttpContext.Current.Session.Remove("LSTGENERATORI")
            'HttpContext.Current.Session.Remove("LSTBRUCIATORI")


            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            ElseIf Strings.Len(sVerifiche) > 0 Then
                Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
            Else
                Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "');</script>")
                'Response.Write("<script>document.location.href=""RisultatiImpianti.aspx""</script>")
                'Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")
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



    Private Property TipoImmobile() As String
        Get
            If Not (ViewState("pa_tipoimmob") Is Nothing) Then
                Return CStr(ViewState("pa_tipoimmob"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("pa_tipoimmob") = value
        End Set

    End Property



    Private Sub AbilitazioneOggetti(ByVal ValorePass As Boolean)
        Try

            cmbComplesso.Enabled = ValorePass
            DrLEdificio.Enabled = ValorePass

            txtDenominazione.ReadOnly = Not (ValorePass)
            txtCodImpianto.ReadOnly = True

            cmbTipoUso.Enabled = ValorePass

        Catch ex As Exception

        End Try

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

    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.DrLEdificio.SelectedValue <> "-1" Then
            FiltraUnitaImmob()
        Else
            Me.cmbUnitaImmob.Items.Clear()
            'If Me.cmbComplesso.SelectedValue = "-1" Then
            '    Me.cmbUnitaComune.Items.Clear()
            'Else
            '    FiltraUnitaComuni()
            'End If
        End If

    End Sub

    Protected Sub cmbUnitaImmob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnitaImmob.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


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

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO TERMICO AUTONOMO"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto Termico Autonomo del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
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
