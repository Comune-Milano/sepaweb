Imports System.Collections

Partial Class IMPIANTI_Imp_Sollevamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""

    Public TabberHide As String = ""
    Public TabberHideI As String = ""    'TABBER dell'INQUILINO solo per MONTASCALE

    Public Visibile As String = ""


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

        If Not IsPostBack Then

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            sProvenienza = Request.QueryString("SL")

            vIdImpianto = 0
            'MODIFICA MARCO 11/05/2012
            If Not IsNothing(Request.QueryString("ID")) Then
                vIdImpianto = Request.QueryString("ID")
                lstScale = New System.Collections.Generic.List(Of Epifani.Scale)
            Else
                vIdImpianto = Session.Item("ID")
            End If
            '-------------------------


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
            AbilitazioneOggetti(True)

            If vIdImpianto <> 0 Then
                'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)
                TabberHide = "tabbertab"
                TabberHideI = "tabbertabhide"

                VisualizzaDati()
                txtindietro.Text = 0
                Tabber1 = "tabbertabdefault"
            Else
                '*** DEVO DISABILITARE IL TAB COMPONENTI
                TabberHide = "tabbertabhide"
                TabberHideI = "tabbertabhide"

                Me.txtIdImpianto.Text = -1
                Me.btnElimina.Visible = False

                Tabber1 = "tabbertabdefault"
                txtindietro.Text = 1
                'Me.LblRiepilogo.Visible = False
            End If

            Dim CTRL As Control

            '*** FORM PRINCIPALE
            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            DrLEdificio.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            cmbTipoUso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")

            txtDenominazione.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtDitta.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            txtAnnoRealizzazione.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            txtAnnoRealizzazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            ''*** FORM GENERALE
            For Each CTRL In Me.Tab_Sollevamento_Generale.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** TAB. CERTIFICAZIONI
            ''*** FORM GENERALE
            For Each CTRL In Me.Tab_Sollevamento_Certificazioni.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next
            '*********************+

            CType(Tab_Sollevamento_Generale.FindControl("txtNote"), TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

            '*** FORM VERIFICHE PERIODICHE BIENNALI
            For Each CTRL In Me.Tab_Sollevamento_Verifiche1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM VERIFICHE PERIODICHE STRAORDINARIE
            For Each CTRL In Me.Tab_Sollevamento_Verifiche2.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            ' CONTROLLO DATA
            CType(Tab_Sollevamento_Verifiche1.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Verifiche1.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Verifiche1.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Verifiche1.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            CType(Tab_Sollevamento_Verifiche2.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Verifiche2.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Verifiche2.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Verifiche2.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")


            If vIdImpianto = 0 Then
                CType(Tab_Sollevamento_Verifiche1.FindControl("btnApri"), ImageButton).Visible = False
                CType(Tab_Sollevamento_Verifiche1.FindControl("btnVisualizza"), ImageButton).Visible = False
                CType(Tab_Sollevamento_Verifiche1.FindControl("btnElimina"), ImageButton).Visible = False

                CType(Tab_Sollevamento_Verifiche2.FindControl("btnApri"), ImageButton).Visible = False
                CType(Tab_Sollevamento_Verifiche2.FindControl("btnVisualizza"), ImageButton).Visible = False
                CType(Tab_Sollevamento_Verifiche2.FindControl("btnElimina"), ImageButton).Visible = False
            End If


            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                If IsNothing(Request.QueryString("ID")) Then
                    Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Sollevamento.png';</script>")

                End If
            End If


            CType(Tab_Sollevamento_Generale.FindControl("cmbTeleallarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 6, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If

        End If

        If CType(Tab_Sollevamento_Generale.FindControl("cmbTeleallarme"), DropDownList).Text = "S" Then
            CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Enabled = True
        Else
            CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Text = ""
            CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Enabled = False
        End If


        If Me.cmbTipoUso.SelectedItem.Text = "MONTASCALE" Then
            TabberHideI = "tabbertab"
        Else
            TabberHideI = "tabbertabhide"
        End If

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
            'par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_USO_TERMICI order by ID"
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

        'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

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

                lstScale.Clear()


                Dim gest As Integer = 0
                Me.DrLEdificio.Items.Clear()
                Me.DrLEdificio.Items.Add(New ListItem(" ", -1))


                If gest <> 0 Then
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(ID,1,1)= " & gest & " order by DENOMINAZIONE asc"
                Else
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()
                '**************************


                If FlagConnessione = True Then
                    'par.cmd.Dispose()
                    par.OracleConn.Close()
                    'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim i As Integer
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList()

        Dim bTrovato As Boolean = False

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

        CType(Tab_Sollevamento_Generale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")


        'I_SOLLEVAMENTO
        par.cmd.CommandText = "select * from SISCOM_MI.I_SOLLEVAMENTO where SISCOM_MI.I_SOLLEVAMENTO.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            'Me.cmbScala.SelectedValue = par.IfNull(myReader2("ID_SCALA"), "-1")

            CType(Tab_Sollevamento_Generale.FindControl("txtMarcaModello"), TextBox).Text = par.IfNull(myReader2("MARCA_MODELLO"), "")

            CType(Tab_Sollevamento_Generale.FindControl("txtNumImpianto"), TextBox).Text = par.IfNull(myReader2("NUM_IMPIANTO"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Text = par.IfNull(myReader2("MATRICOLA"), "")


            Me.cmbTipoUso.SelectedValue = par.IfNull(myReader2("TIPOLOGIA"), "")
            CType(Tab_Sollevamento_Generale.FindControl("cmbTipoAzionamento"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_AZIONAMENTO"), "")

            CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text = par.IfNull(myReader2("NUM_FERMATE"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtVelocita"), TextBox).Text = par.IfNull(myReader2("VELOCITA"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtPortata"), TextBox).Text = par.IfNull(myReader2("PORTATA_KG"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumPersone"), TextBox).Text = par.IfNull(myReader2("PORTATA_PERSONE"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtCorsa"), TextBox).Text = par.IfNull(myReader2("CORSA_MT"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumVisite"), TextBox).Text = par.IfNull(myReader2("N_VISITE_MENSILI"), "")

            CType(Tab_Sollevamento_Generale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")


            CType(Tab_Sollevamento_Generale.FindControl("cmbTipoManovra"), DropDownList).SelectedValue = par.IfNull(myReader2("TIPO_MANOVRA"), "")
            CType(Tab_Sollevamento_Generale.FindControl("cmbTeleallarme"), DropDownList).Items.FindByValue(par.IfNull(myReader2("TELEALLARME"), "")).Selected = True
            CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Text = par.IfNull(myReader2("TELEFONO_TELEALLARME"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumero"), TextBox).Text = par.IfNull(myReader2("NUMERICO"), "")
            CType(Tab_Sollevamento_Generale.FindControl("txtNumLotto"), TextBox).Text = par.IfNull(myReader2("NUM_LOTTO"), "")

            CType(Tab_Sollevamento_Generale.FindControl("cmbModello"), DropDownList).SelectedValue = par.IfNull(myReader2("MODELLO"), "")
            CType(Tab_Sollevamento_Generale.FindControl("cmbUbicazione"), DropDownList).SelectedValue = par.IfNull(myReader2("UBICAZIONE"), "")


            '*** TAB. CERTIFICAZIONI
            CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformitaCE"), DropDownList).Items.FindByValue(par.IfNull(myReader2("CONF_CE"), "")).Selected = True
            CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita37_08"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF_LG_37_08"), "")).Selected = True
            'CType(Tab_Sollevamento_Certificazioni.FindControl("Conformita162_99"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF_LG_162_99"), "")).Selected = True
            CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita459_96"), DropDownList).Items.FindByValue(par.IfNull(myReader2("DICH_CONF_LG_459_96"), "")).Selected = True

            CType(Tab_Sollevamento_Certificazioni.FindControl("cmbSchema"), DropDownList).Items.FindByValue(par.IfNull(myReader2("SCHEMA_IMPIANTO"), "")).Selected = True
            'CType(Tab_Sollevamento_Certificazioni.FindControl("cmbISPESL"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRATICA_ISPELS"), "")).Selected = True

            CType(Tab_Sollevamento_Certificazioni.FindControl("cmbLibretto"), DropDownList).Items.FindByValue(par.IfNull(myReader2("LIBRETTO"), "")).Selected = True
            CType(Tab_Sollevamento_Certificazioni.FindControl("cmbNum_Matricola"), DropDownList).Items.FindByValue(par.IfNull(myReader2("NUM_MATRICOLA"), "")).Selected = True

            '*******************

            'DISABILITO COMBO COMPLESSO ED EDIFICIO (NO per questo impianto)
            Me.cmbComplesso.Enabled = False

        End If
        myReader2.Close()

        '*** SCALE
        par.cmd.CommandText = "select ID_SCALA from SISCOM_MI.IMPIANTI_SCALE where ID_IMPIANTO = " & vIdImpianto
        myReader2 = par.cmd.ExecuteReader()

        While myReader2.Read

            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Value = par.IfNull(myReader2("ID_SCALA"), "-1") Then
                    CheckBoxScale.Items(i).Selected = True
                    bTrovato = True
                End If
            Next
        End While
        myReader2.Close()

        If bTrovato = False And par.IfEmpty(Me.cmbScala.SelectedValue, -1) <> -1 Then
            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Value = par.IfNull(Me.cmbScala.SelectedValue, "-1") Then
                    CheckBoxScale.Items(i).Selected = True
                    bTrovato = True
                    Exit For
                End If
            Next
        End If
        '**********************************************


        '*** INQUILINI che usano il MANTASCALE
        par.cmd.CommandText = "select ID_CONTRATTO from SISCOM_MI.IMPIANTI_INTESTATARI where  SISCOM_MI.IMPIANTI_INTESTATARI.ID_IMPIANTO = " & vIdImpianto
        myReader2 = par.cmd.ExecuteReader()

        While myReader2.Read

            For Each oDataGridItem In CType(Tab_Sollevamento_Inquilino.FindControl("DataGrid1"), DataGrid).Items ' dgMain.Items

                If oDataGridItem.Cells(0).Text = par.IfNull(myReader2("ID_CONTRATTO"), "-1") Then
                    chkExport = oDataGridItem.FindControl("CheckBox1")
                    chkExport.Checked = True
                    oDataGridItem.Cells(1).Font.Bold = True
                    oDataGridItem.Cells(2).Font.Bold = True
                    oDataGridItem.Cells(3).Font.Bold = True
                    oDataGridItem.Cells(4).Font.Bold = True

                    Exit For
                End If
            Next
        End While
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
                ' LEGGO IMPIANTO SOLLEVAMENTO

                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()


                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                'par.OracleConn.Close()

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

        '*** NOME COMPLESSO
        If Me.cmbComplesso.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare un Complesso Immobiliare!');</script>")
            Me.cmbComplesso.Focus()
            ControlloCampi = False
            Exit Function
        End If

        '*** NOME IMPIANTO
        If par.IfEmpty(Me.txtDenominazione.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Denominazione Impianto!');</script>")
            '****par.SetFocusControl(Page, "TabGenerale_txtNumImpianto")

            txtDenominazione.Focus()
            ControlloCampi = False

            Exit Function
        End If

        '*** TIPOLOGIA D'USO
        If par.IfEmpty(Me.cmbTipoUso.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Selezionare la tipologia d\'uso dell\'impianto!');</script>")
            Me.cmbTipoUso.Focus()
            ControlloCampi = False
            Exit Function
        End If

        '*** TIPO AZIONAMENTO
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("cmbTipoAzionamento"), DropDownList).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Selezionare il tipo di azionamento dell\'impianto!');</script>")
            CType(Tab_Sollevamento_Generale.FindControl("cmbTipoAzionamento"), DropDownList).Focus()
            ControlloCampi = False
            Exit Function
        End If

        '*** NUM. IMPIANTO o '*** NUM. MATRICOLA
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumImpianto"), TextBox).Text, "Null") = "Null" And par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il numero dell\'impianto o la matricola!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtNumImpianto"), TextBox).Focus()
            Exit Function
        End If

        ''*** NUM. MATRICOLA
        'If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Inserire la matricola dell\'impianto!');</script>")
        '    ControlloCampi = False
        '    CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Focus()
        '    Exit Function
        'End If


        '*** VELOCITA'
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtVelocita"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la velocià!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtVelocita"), TextBox).Focus()
            Exit Function
        End If

        '*** PORTATA
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtPortata"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la portata!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtPortata"), TextBox).Focus()
            Exit Function
        End If

        '*** CORSA
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtCorsa"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la corsa!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtCorsa"), TextBox).Focus()
            Exit Function
        End If

        '*** N_VISITE_MENSILI
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumVisite"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire le visite mensili!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtNumVisite"), TextBox).Focus()
            Exit Function
        End If

        '*** MAX PERSONE
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumPersone"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il numero massimo di persone!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtNumPersone"), TextBox).Focus()
            Exit Function
        End If

        '*** NUM. FERMATE
        If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il numero di fermate!');</script>")
            ControlloCampi = False
            CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazione.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazione.Text = ""

        End If

    End Function

    Private Sub Salva()
        Dim i As Integer

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


            '*** IMPIANTI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI (ID, ID_COMPLESSO, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE,ANNO_COSTRUZIONE,DITTA_COSTRUTTRICE,ANNOTAZIONI) " _
                                & "values (:id,:id_complesso,:id_edificio,:cod_tipologia,:descrizione,:anno,:ditta,:annotazioni)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "SO"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNote"), TextBox).Text, 4000)))

            'RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString)
            ' ID_SCALA                    NUMBER, (UNITA_IMMOBILIARI)


            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")


            'SCALE
            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Selected = True Then
                    par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                               & "(" & vIdImpianto & "," & CheckBoxScale.Items(i).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    '*** EVENTI_IMPIANTI
                    par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scale Servite")
                End If
            Next


            '*** I_SOLLEVAMENTO
            par.cmd.CommandText = "insert into SISCOM_MI.I_SOLLEVAMENTO " _
                                        & " (ID,NUM_IMPIANTO, MATRICOLA, TIPOLOGIA,TIPO_AZIONAMENTO,NUM_FERMATE," _
                                        & "VELOCITA,PORTATA_KG,PORTATA_PERSONE,CORSA_MT,CONF_CE,DICH_CONF_LG_37_08," _
                                        & "DICH_CONF_LG_459_96,SCHEMA_IMPIANTO,MARCA_MODELLO,DITTA_GESTIONE,TELEFONO_DITTA," _
                                        & "TIPO_MANOVRA,TELEALLARME,TELEFONO_TELEALLARME,NUMERICO,NUM_LOTTO,MODELLO,UBICAZIONE,LIBRETTO,NUM_MATRICOLA, N_VISITE_MENSILI) " _
                                & "values (:id,:num_impianto,:matricola,:tipologia,:tipo_azionamento,:num_fermate,:velocita,:portata," _
                                         & ":persone,:corsa,:conf_ce,:conf37_08,:conf459,:schema,:marca,:ditta_gestione,:num_tel," _
                                         & ":tipo_manovra,:teleallarme,:tel_allarme,:numerico,:num_lotto,:modello,:ubicazione,:libretto,:num_matricola,:n_visite_mensili) "

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_impianto", CType(Tab_Sollevamento_Generale.FindControl("txtNumImpianto"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", Me.cmbTipoUso.SelectedValue.ToString))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_azionamento", CType(Tab_Sollevamento_Generale.FindControl("cmbTipoAzionamento"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_fermate", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("velocita", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtVelocita"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("portata", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtPortata"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("persone", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumPersone"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("corsa", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtCorsa"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf_ce", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformitaCE"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf37_08", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita37_08"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf459", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita459_96"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("schema", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbSchema"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("marca", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtMarcaModello"), TextBox).Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_visite_mensili", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumVisite"), TextBox).Text)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbScala.SelectedValue, -1)))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_manovra", CType(Tab_Sollevamento_Generale.FindControl("cmbTipoManovra"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("teleallarme", CType(Tab_Sollevamento_Generale.FindControl("cmbTeleallarme"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel_allarme", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("numerico", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumero"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lotto", CType(Tab_Sollevamento_Generale.FindControl("txtNumLotto"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", CType(Tab_Sollevamento_Generale.FindControl("cmbModello"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", CType(Tab_Sollevamento_Generale.FindControl("cmbUbicazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_matricola", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbNum_Matricola"), DropDownList).SelectedValue.ToString()))


            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()


            '*** INSERIMENTO VERIFICHE BIENNALI
            Dim lstVerifiche As System.Collections.Generic.List(Of Epifani.VerificheImpianti)

            lstVerifiche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE"), System.Collections.Generic.List(Of Epifani.VerificheImpianti))

            For Each gen As Epifani.VerificheImpianti In lstVerifiche


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO) " _
                                & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico )"


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "PB"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", par.PulisciStringaInvio(gen.DITTA, 100)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(gen.DATA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 4000)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(gen.ESITO)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", gen.ES_PRESCRIZIONE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", gen.ESITO_DETTAGLIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(gen.MESI_VALIDITA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(gen.MESI_PREALLARME)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", par.AggiustaData(gen.DATA_SCADENZA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))


                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifiche Periodiche Biennali")

            Next
            '********************************


            '*** INSERIMENTO VERIFICHE STRAORDINARIE
            Dim lstVerifiche2 As System.Collections.Generic.List(Of Epifani.VerificheImpianti)

            lstVerifiche2 = CType(HttpContext.Current.Session.Item("LSTVERIFICHE2"), System.Collections.Generic.List(Of Epifani.VerificheImpianti))

            For Each gen As Epifani.VerificheImpianti In lstVerifiche2


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO) " _
                                & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico )"


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ST"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", par.PulisciStringaInvio(gen.DITTA, 100)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(gen.DATA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(gen.NOTE, 4000)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(gen.ESITO)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", gen.ES_PRESCRIZIONE))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", gen.ESITO_DETTAGLIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(gen.MESI_VALIDITA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(gen.MESI_PREALLARME)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", par.AggiustaData(gen.DATA_SCADENZA)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))


                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifiche Straordinarie")

            Next
            '********************************

            'AGGIORNA IMPIANTI_INTESTATARI  (Inquilini che usano il MANTASCALE)
            AggiornaImpiantiInquilini()

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


            '**** AGGIORNO LE VERIFICHE PB
            par.cmd.CommandText = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                        & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                        & "ESITO,MESI_PREALLARME,TIPO, " _
                                        & " DECODE(ES_PRESCRIZIONE,'S','SI','N','NO','P','PARZIALE') AS ""ES_PRESCRIZIONE"" " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PB'" _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds1 As New Data.DataSet()
            da1.Fill(ds1, "IMPIANTI_VERIFICHE")

            CType(Tab_Sollevamento_Verifiche1.FindControl("DataGrid1"), DataGrid).DataSource = ds1
            CType(Tab_Sollevamento_Verifiche1.FindControl("DataGrid1"), DataGrid).DataBind()
            ds1.Dispose()
            '*******

            '**** AGGIORNO LE VERIFICHE ST
            par.cmd.CommandText = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                        & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                        & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE" _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ST'" _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds2 As New Data.DataSet()
            da2.Fill(ds2, "IMPIANTI_VERIFICHE")

            CType(Tab_Sollevamento_Verifiche2.FindControl("DataGrid1"), DataGrid).DataSource = ds2
            CType(Tab_Sollevamento_Verifiche2.FindControl("DataGrid1"), DataGrid).DataBind()
            ds2.Dispose()
            '*******


            'ABILITO I BOTTONI DELLE VERIFICHE
            CType(Tab_Sollevamento_Verifiche1.FindControl("btnAgg"), ImageButton).Visible = True
            CType(Tab_Sollevamento_Verifiche1.FindControl("btnApri"), ImageButton).Visible = True
            CType(Tab_Sollevamento_Verifiche1.FindControl("btnVisualizza"), ImageButton).Visible = True
            CType(Tab_Sollevamento_Verifiche1.FindControl("btnElimina"), ImageButton).Visible = True

            CType(Tab_Sollevamento_Verifiche2.FindControl("btnAgg"), ImageButton).Visible = True
            CType(Tab_Sollevamento_Verifiche2.FindControl("btnApri"), ImageButton).Visible = True
            CType(Tab_Sollevamento_Verifiche2.FindControl("btnVisualizza"), ImageButton).Visible = True
            CType(Tab_Sollevamento_Verifiche2.FindControl("btnElimina"), ImageButton).Visible = True
            '**************************


            'DISABILITO COMBO COMPLESSO ED EDIFICIO (NO per questo impianto)
            Me.cmbComplesso.Enabled = False

            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()


            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            ''VisualizzaDati()

            TabberHide = "tabbertab"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


            '' ''    Me.LBLINSCOMP.Enabled = False
            '' ''    Me.LBLINSEDIFICIO.Enabled = False
            '' ''    Me.LblDenominazione.Enabled = False
            '' ''    Me.LBLINSUNIIMMOB.Enabled = False
            '' ''    Me.cmbComplesso.Enabled = False
            '' ''    Me.DrLEdificio.Enabled = False
            '' ''    Me.cmbUnitaImmob.Enabled = False
            '' ''    Me.cmbUnitaComune.Enabled = False
            '' ''Else
            '' ''    Response.Write("<SCRIPT>alert('Il campo COSTO deve essere avvalorato!');</SCRIPT>")

            '' ''End If



        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub Update()
        Dim i As Integer

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
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "SO"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNote"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()


            '*** DETTAGLI I_SOLLEVAMENTO
            par.cmd.CommandText = " update SISCOM_MI.I_SOLLEVAMENTO " _
                                & " set NUM_IMPIANTO=:num_impianto,MATRICOLA=:matricola,TIPOLOGIA=:tipologia,TIPO_AZIONAMENTO=:tipo_azionamento,NUM_FERMATE=:num_fermate," _
                                     & "VELOCITA=:velocita,PORTATA_KG=:portata,PORTATA_PERSONE=:persone,CORSA_MT=:corsa,CONF_CE=:conf_ce,DICH_CONF_LG_37_08=:conf37_08," _
                                     & "DICH_CONF_LG_459_96=:conf459,SCHEMA_IMPIANTO=:schema,MARCA_MODELLO=:marca,DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel, " _
                                        & "TIPO_MANOVRA=:tipo_manovra,TELEALLARME=:teleallarme,TELEFONO_TELEALLARME=:tel_allarme,NUMERICO=:numerico,NUM_LOTTO=:num_lotto,MODELLO=:modello,UBICAZIONE=:ubicazione,LIBRETTO=:libretto,NUM_MATRICOLA=:num_matricola, " _
                                        & " n_visite_mensili = :n_visite_mensili " _
                                & " where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_impianto", CType(Tab_Sollevamento_Generale.FindControl("txtNumImpianto"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", Me.cmbTipoUso.SelectedValue.ToString))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_azionamento", CType(Tab_Sollevamento_Generale.FindControl("cmbTipoAzionamento"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_fermate", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("velocita", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtVelocita"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("portata", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtPortata"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("persone", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumPersone"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("corsa", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtCorsa"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf_ce", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformitaCE"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf37_08", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita37_08"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf459", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita459_96"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("schema", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbSchema"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("marca", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtMarcaModello"), TextBox).Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_visite_mensili", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumVisite"), TextBox).Text)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbScala.SelectedValue, -1)))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_manovra", CType(Tab_Sollevamento_Generale.FindControl("cmbTipoManovra"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("teleallarme", CType(Tab_Sollevamento_Generale.FindControl("cmbTeleallarme"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel_allarme", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("numerico", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumero"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lotto", CType(Tab_Sollevamento_Generale.FindControl("txtNumLotto"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", CType(Tab_Sollevamento_Generale.FindControl("cmbModello"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", CType(Tab_Sollevamento_Generale.FindControl("cmbUbicazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_matricola", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbNum_Matricola"), DropDownList).SelectedValue.ToString()))

            'par.VirgoleInPunti(par.IfEmpty(CType(TabGenerale.FindControl("txtCapacita"), TextBox).Text, "Null"))

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            '*** DETTAGLI I_SOLLEVAMENTO
            par.cmd.CommandText = " update SISCOM_MI.I_SOLLEVAMENTO_TMP " _
                                & " set NUM_IMPIANTO=:num_impianto,MATRICOLA=:matricola,TIPOLOGIA=:tipologia,TIPO_AZIONAMENTO=:tipo_azionamento,NUM_FERMATE=:num_fermate," _
                                     & "VELOCITA=:velocita,PORTATA_KG=:portata,PORTATA_PERSONE=:persone,CORSA_MT=:corsa,CONF_CE=:conf_ce,DICH_CONF_LG_37_08=:conf37_08," _
                                     & "DICH_CONF_LG_459_96=:conf459,SCHEMA_IMPIANTO=:schema,MARCA_MODELLO=:marca,DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel, " _
                                        & "TIPO_MANOVRA=:tipo_manovra,TELEALLARME=:teleallarme,TELEFONO_TELEALLARME=:tel_allarme,NUMERICO=:numerico,NUM_LOTTO=:num_lotto,MODELLO=:modello,UBICAZIONE=:ubicazione,LIBRETTO=:libretto,NUM_MATRICOLA=:num_matricola, " _
                                        & " n_visite_mensili = :n_visite_mensili " _
                                & " where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_impianto", CType(Tab_Sollevamento_Generale.FindControl("txtNumImpianto"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", CType(Tab_Sollevamento_Generale.FindControl("txtNumMatricola"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", Me.cmbTipoUso.SelectedValue.ToString))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_azionamento", CType(Tab_Sollevamento_Generale.FindControl("cmbTipoAzionamento"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_fermate", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("velocita", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtVelocita"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("portata", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtPortata"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("persone", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumPersone"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("corsa", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtCorsa"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf_ce", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformitaCE"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf37_08", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita37_08"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conf459", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbConformita459_96"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("schema", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbSchema"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("marca", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtMarcaModello"), TextBox).Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_visite_mensili", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumVisite"), TextBox).Text)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbScala.SelectedValue, -1)))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_manovra", CType(Tab_Sollevamento_Generale.FindControl("cmbTipoManovra"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("teleallarme", CType(Tab_Sollevamento_Generale.FindControl("cmbTeleallarme"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel_allarme", Strings.Left(CType(Tab_Sollevamento_Generale.FindControl("txtNumTelAllarme"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("numerico", strToNumber(CType(Tab_Sollevamento_Generale.FindControl("txtNumero"), TextBox).Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lotto", CType(Tab_Sollevamento_Generale.FindControl("txtNumLotto"), TextBox).Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", CType(Tab_Sollevamento_Generale.FindControl("cmbModello"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", CType(Tab_Sollevamento_Generale.FindControl("cmbUbicazione"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("libretto", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_matricola", CType(Tab_Sollevamento_Certificazioni.FindControl("cmbNum_Matricola"), DropDownList).SelectedValue.ToString()))

            'par.VirgoleInPunti(par.IfEmpty(CType(TabGenerale.FindControl("txtCapacita"), TextBox).Text, "Null"))

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            'AGGIORNA IMPIANTI_INTESTATARI  (Inquilini che usano il MANTASCALE)
            AggiornaImpiantiInquilini()


            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")


            'SCALE
            par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_SCALE where ID_IMPIANTO = " & vIdImpianto
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""


            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                    par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                               & "(" & vIdImpianto & "," & CheckBoxScale.Items(i).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                End If
            Next


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
            If valorepass <> "-1" Then
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
        Dim dlist As CheckBoxList

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.DrLEdificio.SelectedValue <> "-1" Then
            FiltraScale()
        Else
            lstScale.Clear()

            ' SCALE INTESTAZIONE
            dlist = CheckBoxScale
            dlist.DataSource = lstScale

            dlist.DataTextField = "SCALE_NO_TITLE"
            dlist.DataValueField = "ID"
            dlist.DataBind()

            Me.cmbScala.Items.Clear()
            cmbScala.Items.Add(New ListItem(" ", -1))
        End If
    End Sub



    Private Sub FiltraScale()
        Dim StringaSql As String
        Dim ds As New Data.DataSet()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dlist As CheckBoxList

        Dim FlagConnessione As Boolean


        Try

            FlagConnessione = False
            If Me.DrLEdificio.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                lstScale.Clear()

                Me.cmbScala.Items.Clear()
                Me.cmbScala.Items.Add(New ListItem(" ", -1))

                par.cmd.CommandText = "select  ID, DESCRIZIONE from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString & " order by DESCRIZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbScala.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                    Dim gen As Epifani.Scale
                    gen = New Epifani.Scale(par.IfNull(myReader1("ID"), -1), Me.DrLEdificio.SelectedItem.ToString, par.IfNull(myReader1("DESCRIZIONE"), " "))
                    lstScale.Add(gen)
                    gen = Nothing

                End While
                myReader1.Close()
                '********************


                ' SCALE INTESTAZIONE
                dlist = CheckBoxScale
                dlist.DataSource = lstScale

                dlist.DataTextField = "SCALE_NO_TITLE"
                dlist.DataValueField = "ID"
                dlist.DataBind()
                '*********************************************************************************************


                ' LISTA CHECK BOX INQUILINI del MONTASCALE 
                StringaSql = "select ID_CONTRATTO AS ID,INTESTATARIO,SCALA,PIANO,INTERNO " _
                          & " from SISCOM_MI.INTESTATARI_UI " _
                          & " where SISCOM_MI.INTESTATARI_UI.ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString _
                          & " order by INTESTATARIO"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, par.OracleConn)
                da.Fill(ds, "INTESTATARI_UI")

                CType(Tab_Sollevamento_Inquilino.FindControl("DataGrid1"), DataGrid).DataSource = ds
                CType(Tab_Sollevamento_Inquilino.FindControl("DataGrid1"), DataGrid).DataBind()


                da.Dispose()
                da = Nothing

                ds.Clear()
                ds.Dispose()
                ds = Nothing
                '************************


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

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO SOLLEVAMENTO"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto Sollevamento del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
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




    Protected Sub cmbTipoUso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoUso.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Protected Sub cmbTipoUso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoUso.TextChanged
        Me.CalcolaCanone()
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Private Sub AggiornaImpiantiInquilini()
        Dim i As Integer
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList()
        Dim sID As String

        Try

            par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_INTESTATARI where ID_IMPIANTO = " & vIdImpianto
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            '**************
            If Me.cmbTipoUso.SelectedItem.Text <> "MONTASCALE" Then
                Exit Sub
            End If

            For Each oDataGridItem In CType(Tab_Sollevamento_Inquilino.FindControl("DataGrid1"), DataGrid).Items ' dgMain.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")
                If chkExport.Checked Then
                    sID = oDataGridItem.Cells(0).Text
                    'sID =CType(oDataGridItem.FindControl("ID"), Label).Text

                    oDataGridItem.Cells(1).Font.Bold = True
                    oDataGridItem.Cells(2).Font.Bold = True
                    oDataGridItem.Cells(3).Font.Bold = True
                    oDataGridItem.Cells(4).Font.Bold = True

                    oExArgs.Add(sID)
                Else
                    oDataGridItem.Cells(1).Font.Bold = False
                    oDataGridItem.Cells(2).Font.Bold = False
                    oDataGridItem.Cells(3).Font.Bold = False
                    oDataGridItem.Cells(4).Font.Bold = False

                End If
            Next
            '**************

            For i = 0 To oExArgs.Count - 1
                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_INTESTATARI (ID_IMPIANTO,ID_CONTRATTO) values " _
                                   & "(" & vIdImpianto & "," & oExArgs(i) & ")"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
            Next i

            'Dim grid As DataGrid = CType(Tab_Sollevamento_Inquilino.FindControl("DataGrid1"), DataGrid)
            'Dim str As String = ""
            'For i As Integer = 0 To grid.Items.Count - 1
            '    Dim chk As CheckBox
            '    Dim nome As String
            '    Dim numero As String

            '    numero = (i).ToString()
            '    If numero.Length = 1 Then numero = "0" & numero
            '    nome = "Tab_Sollevamento_Inquilino$DataGrid1$ctl" & numero & "$CheckBox1"

            '    chk = Me.FindControl(nome)
            '    If Not IsNothing(chk) Then
            '        str &= numero & ")" & grid.Items(i).Cells(1).Text & chk.Checked & vbCrLf
            '    End If

            '    'System.Diagnostics.Debug.Print(i.ToString() & " " & chk.Checked)
            '    'chk = Me.FindControl("Tab_Sollevamento_Inquilino$DataGrid1$ctl03$CheckBox1")
            'Next
            'MsgBox(str)


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Public Sub CalcolaCanone()
        Dim FlagConnessione As Boolean
        Dim TipoSollevamento As String
        Dim NumFermate As Integer

        Try
            FlagConnessione = False

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            CType(Tab_Sollevamento_Generale.FindControl("txtNumero"), TextBox).Text = ""

            If par.IfEmpty(CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text, "Null") = "Null" Then
                CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text = ""
                Exit Sub
            End If


            TipoSollevamento = Me.cmbTipoUso.SelectedItem.ToString
            NumFermate = CType(Tab_Sollevamento_Generale.FindControl("txtNumFermate"), TextBox).Text
            If NumFermate = 1 Then NumFermate = 2
            If NumFermate > 20 Then NumFermate = 20

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            'PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)

            'If vIdImpianto > 0 Then
            '    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '    ‘‘par.cmd.Transaction = par.myTrans
            'End If

            par.cmd.CommandText = "  select SISCOM_MI.TAB_CANONE_SOLLEVAMENTO.NUMERO " _
                                & " from  SISCOM_MI.TAB_CANONE_SOLLEVAMENTO " _
                                & " where SISCOM_MI.TAB_CANONE_SOLLEVAMENTO.FERMATE = " & NumFermate _
                                  & " and SISCOM_MI.TAB_CANONE_SOLLEVAMENTO.TIPO='" & TipoSollevamento & "'"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                CType(Tab_Sollevamento_Generale.FindControl("txtNumero"), TextBox).Text = par.IfNull(myReader1("NUMERO"), "")
            End If
            myReader1.Close()

            If FlagConnessione = True Then
                par.OracleConn.Close()
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub



End Class
