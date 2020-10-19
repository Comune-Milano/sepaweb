Imports System.Collections

Partial Class IMPIANTI_Imp_Antincendio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""
    Public Tabber7 As String = ""
    Public Tabber8 As String = ""
    Public Tabber9 As String = ""

    Public TabberHide As String = ""

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String
    Public sOrdinamento As String
    Public sVerifiche As String

    Public sProvenienza As String

    Dim lstScale As System.Collections.Generic.List(Of Epifani.Scale)
    Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)
    Dim lstPiani As System.Collections.Generic.List(Of Epifani.Scale)
    Dim lstPianiAutoPompa As System.Collections.Generic.List(Of Epifani.Scale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        Response.Expires = 0

        lstScale = CType(HttpContext.Current.Session.Item("LSTSCALE"), System.Collections.Generic.List(Of Epifani.Scale))
        lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))
        lstPiani = CType(HttpContext.Current.Session.Item("LSTPIANIIDRANTI"), System.Collections.Generic.List(Of Epifani.Scale))
        lstPianiAutoPompa = CType(HttpContext.Current.Session.Item("LSTPIANIAUTOPOMPA"), System.Collections.Generic.List(Of Epifani.Scale))


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
            lstEdifici.Clear()
            lstPiani.clear()
            lstPianiAutoPompa.Clear()


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
            For Each CTRL In Me.Tab_Antincendio_Generale.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** FORM DETTAGLI - SERBATOI ACCUMULO
            For Each CTRL In Me.Tab_Antincendio_Dettagli.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM SPRINKLER 
            For Each CTRL In Me.Tab_Antincendio_Sprinkler.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM RILEVATORE FUMI 
            For Each CTRL In Me.Tab_Antincendio_Fumi.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM IDRANTI/NASPI + VERIFICHE
            For Each CTRL In Me.Tab_Antincendio_Idranti.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** FORM ESTINTORI + VERIFICHE
            For Each CTRL In Me.Tab_Antincendio_Estintori.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM ATTACCO AUTOPOMPA
            For Each CTRL In Me.Tab_Antincendio_Autopompa.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            '*** FORM VERIFICHE 
            For Each CTRL In Me.Tab_Antincendio_Verifiche.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next


            '*** CONTROLLO DATA
            txtAnnoRealizzazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CType(Tab_Antincendio_Verifiche.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Verifiche.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Verifiche.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Verifiche.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")


            CType(Tab_Antincendio_Idranti.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Idranti.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Idranti.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Idranti.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            CType(Tab_Antincendio_Estintori.FindControl("txtData"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Estintori.FindControl("txtDataScadenza"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Estintori.FindControl("cmbPreAllarme"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Estintori.FindControl("txtValidita"), TextBox).Attributes.Add("onkeypress", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")


            If vIdImpianto = 0 Then
                CType(Tab_Antincendio_Verifiche.FindControl("btnApri"), ImageButton).Visible = False
                CType(Tab_Antincendio_Verifiche.FindControl("btnVisualizza"), ImageButton).Visible = False
                CType(Tab_Antincendio_Verifiche.FindControl("btnElimina"), ImageButton).Visible = False
            End If

            cmbComplesso.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            DrLEdificio.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
            CType(Tab_Antincendio_Generale.FindControl("cmbBox"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

            If par.IfNull(sProvenienza, 0) = 1 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
                btnINDIETRO.Visible = False
            Else
                Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Antincendio.png';</script>")
            End If

            If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 8, 1) = 0 Then
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            End If

        End If

            'If CType(Tab_Antincendio_Generale.FindControl("cmbBox"), DropDownList).Text = "S" Then
            '    CType(Tab_Antincendio_Generale.FindControl("txtQuantitaEstintori"), TextBox).Enabled = True
            'Else
            '    CType(Tab_Antincendio_Generale.FindControl("txtQuantitaEstintori"), TextBox).Text = ""
            '    CType(Tab_Antincendio_Generale.FindControl("txtQuantitaEstintori"), TextBox).Enabled = False
            'End If

    End Sub



    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""
        Tabber6 = ""
        Tabber7 = ""
        Tabber8 = ""
        Tabber9 = ""

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
            Case "8"
                Tabber8 = "tabbertabdefault"
            Case "9"
                Tabber9 = "tabbertabdefault"
        End Select

        If vIdImpianto <> 0 Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
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

            '*** TIPOLOGIA_SPRINKLER
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_SPRINKLER order by DESCRIZIONE"
            myReader1 = par.cmd.ExecuteReader

            CType(Tab_Antincendio_Sprinkler.FindControl("cmbSprinkler"), DropDownList).Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                CType(Tab_Antincendio_Sprinkler.FindControl("cmbSprinkler"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            CType(Tab_Antincendio_Sprinkler.FindControl("cmbSprinkler"), DropDownList).Text = CType(Tab_Antincendio_Sprinkler.FindControl("cmbSprinkler"), DropDownList).Items(0).Text
            myReader1.Close()


            '*** TIPOLOGIA_RILEVAZIONE_FUMI
            par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI order by DESCRIZIONE"
            myReader1 = par.cmd.ExecuteReader

            CType(Tab_Antincendio_Fumi.FindControl("cmbFumi"), DropDownList).Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                CType(Tab_Antincendio_Fumi.FindControl("cmbFumi"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            CType(Tab_Antincendio_Fumi.FindControl("cmbFumi"), DropDownList).Text = CType(Tab_Antincendio_Fumi.FindControl("cmbFumi"), DropDownList).Items(0).Text
            myReader1.Close()



        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
        CaricaEdifici()

    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        FiltraEdifici()
        FiltraScale()

    End Sub

    Protected Sub cmbComplesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
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

    Private Sub FiltraEdifici()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim FlagConnessione As Boolean
        Dim bTrovato As Boolean

        Try

            FlagConnessione = False
            bTrovato = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.DrLEdificio.Items.Clear()
            CType(Tab_Antincendio_Dettagli.FindControl("cmbEdificioSerbatoio"), DropDownList).Items.Clear()
            CType(Tab_Antincendio_Dettagli.FindControl("cmbEdificioMotopompa"), DropDownList).Items.Clear()

            DrLEdificio.Items.Add(New ListItem(" ", -1))
            CType(Tab_Antincendio_Dettagli.FindControl("cmbEdificioSerbatoio"), DropDownList).Items.Add(New ListItem(" ", -1))
            CType(Tab_Antincendio_Dettagli.FindControl("cmbEdificioMotopompa"), DropDownList).Items.Add(New ListItem(" ", -1))

            lstScale.Clear()
            lstEdifici.Clear()


            par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE," _
                                            & " (select count(*) from SISCOM_MI.UNITA_IMMOBILIARI where ID_EDIFICIO=EDIFICI.ID) " _
                                            & " from SISCOM_MI.EDIFICI " _
                                            & " where ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by DENOMINAZIONE asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                CType(Tab_Antincendio_Dettagli.FindControl("cmbEdificioSerbatoio"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                CType(Tab_Antincendio_Dettagli.FindControl("cmbEdificioMotopompa"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                Dim gen As Epifani.Edifici
                gen = New Epifani.Edifici(par.IfNull(myReader1("ID"), -1), par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1(2), 0), 0)

                lstEdifici.Add(gen)
                gen = Nothing
                bTrovato = True
            End While
            myReader1.Close()

            ' LISTA EDIFICI CHECK BOX
            dlist = CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList)
            dlist.DataSource = lstEdifici

            dlist.DataTextField = "DescrizioneNO_MQ"
            dlist.DataValueField = "ID"
            dlist.DataBind()


            If bTrovato = True Then
                CType(Tab_Antincendio_Dettagli.FindControl("lblATTENZIONE"), Label).Visible = False
            Else
                CType(Tab_Antincendio_Dettagli.FindControl("lblATTENZIONE"), Label).Visible = True
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

    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        FiltraScale()

    End Sub

    Protected Sub DrLEdificio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

    End Sub

    Private Sub FiltraScale()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim dlistMotopompe As CheckBoxList
        Dim dlistPIANI As CheckBoxList
        Dim FlagConnessione As Boolean
        Dim bTrovato As Boolean

        Dim Entro As Integer
        Dim Fuori As Integer
        Dim mezzanini As Integer
        Dim Attico As Integer
        Dim SuperAttico As Integer
        Dim Sottotetto As Integer
        Dim Seminter As Integer
        Dim PTerra As Integer

        Dim sStrSQL As String
        Dim TROVATO As Boolean

        Try

            FlagConnessione = False
            bTrovato = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            lstScale.Clear()
            lstPiani.Clear()
            lstPianiAutoPompa.Clear()


            ' SCALE ***************************************************************
            par.cmd.CommandText = "select  ID, DESCRIZIONE AS SCALE from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString & " order by DESCRIZIONE asc"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read

                Dim gen As Epifani.Scale
                gen = New Epifani.Scale(par.IfNull(myReader2("ID"), -1), Me.DrLEdificio.SelectedItem.ToString, par.IfNull(myReader2("SCALE"), " "))
                lstScale.Add(gen)
                gen = Nothing
                bTrovato = True
            End While
            myReader2.Close()


            ' SCALE SERVITE DALL'ELETTROPOMPA
            dlistMotopompe = CType(Tab_Antincendio_Dettagli.FindControl("CheckBoxScale"), CheckBoxList)
            dlistMotopompe.DataSource = lstScale

            dlistMotopompe.DataTextField = "SCALE"
            dlistMotopompe.DataValueField = "ID"
            dlistMotopompe.DataBind()

            If bTrovato = True Then
                CType(Tab_Antincendio_Dettagli.FindControl("CheckBoxScale"), CheckBoxList).Visible = True
                CType(Tab_Antincendio_Dettagli.FindControl("lblATTENZIONE_M"), Label).Visible = False
            Else
                CType(Tab_Antincendio_Dettagli.FindControl("CheckBoxScale"), CheckBoxList).Visible = False
                CType(Tab_Antincendio_Dettagli.FindControl("lblATTENZIONE_M"), Label).Visible = True
            End If
            '########################################



            '*** Presenza di Centrale TERMICA nell'Edificio
            par.cmd.CommandText = "select  ID from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='TE' and ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read = True Then

                Dim gen As Epifani.Scale
                gen = New Epifani.Scale(-1, "C.T.", "C.T.")
                lstScale.Add(gen)
                gen = Nothing
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
                gen = Nothing
            End If
            myReader2.Close()
            '**********************************

            ' SCALE INTESTAZIONE
            dlist = CheckBoxScale
            dlist.DataSource = lstScale

            dlist.DataTextField = "SCALE_NO_TITLE"
            dlist.DataValueField = "ID"
            dlist.DataBind()
            '*********************************************************************************************


            bTrovato = False

            '' PIANO DEL TAB IDRANTI e ATTACCO AUTOPOMPA

            par.cmd.CommandText = "select  NUM_PIANI_ENTRO,NUM_PIANI_FUORI,PIANO_TERRA,SEMINTERRATO,SOTTOTETTO,ATTICO,SUPERATTICO,NUM_MEZZANINI " _
                               & " from SISCOM_MI.EDIFICI where ID = " & Me.DrLEdificio.SelectedValue.ToString

            myReader2 = par.cmd.ExecuteReader()

            If myReader2.Read Then
                Entro = par.IfNull(myReader2("NUM_PIANI_ENTRO"), 0)
                Fuori = par.IfNull(myReader2("NUM_PIANI_FUORI"), 0)
                mezzanini = par.IfNull(myReader2("NUM_MEZZANINI"), 0)
                Attico = par.IfNull(myReader2("ATTICO"), 0)
                SuperAttico = par.IfNull(myReader2("SUPERATTICO"), 0)
                Sottotetto = par.IfNull(myReader2("SOTTOTETTO"), 0)
                Seminter = par.IfNull(myReader2("SEMINTERRATO"), 0)
                PTerra = par.IfNull(myReader2("PIANO_TERRA"), 0)
            End If
            myReader2.Close()

            par.cmd.CommandText = ""
            sStrSQL = "select COD, DESCRIZIONE from SISCOM_MI.TIPO_LIVELLO_PIANO"

            If Fuori <> 0 Then
                sStrSQL = sStrSQL & " where (LIVELLO <= " & Fuori
                TROVATO = True
            Else
                sStrSQL = sStrSQL & " where (LIVELLO <= " & Fuori
                TROVATO = True
            End If

            If TROVATO = True Then
                sStrSQL = sStrSQL & " and "
            Else
                sStrSQL = sStrSQL & " where( "
                TROVATO = True
            End If
            sStrSQL = sStrSQL & " LIVELLO >=-" & Entro

            If TROVATO = True Then
                sStrSQL = sStrSQL & " and (ROUND(LIVELLO,0)=LIVELLO) "
            Else
                sStrSQL = sStrSQL & " where (ROUND(LIVELLO,0)=LIVELLO) "
                TROVATO = True
            End If

            If PTerra = 1 Then
                sStrSQL = sStrSQL & " ) "
            Else
                sStrSQL = sStrSQL & " and LIVELLO<>0) "
            End If

            If mezzanini <> 0 Then
                If TROVATO = True Then
                    sStrSQL = sStrSQL & " or (LIVELLO<" & Fuori & " and (ROUND(LIVELLO,0)<>LIVELLO)) "
                End If
            End If

            If Attico <> 0 Then
                sStrSQL = sStrSQL & " or COD = 74 "
            End If
            If SuperAttico <> 0 Then
                sStrSQL = sStrSQL & " or COD = 75 "
            End If
            If Sottotetto <> 0 Then
                sStrSQL = sStrSQL & " or COD = 73 "
            End If
            If Seminter <> 0 Then
                sStrSQL = sStrSQL & " or COD = 72 "
            End If

            'sStrSQL = sStrSQL & " ) ORDER BY COD ASC"

            par.cmd.CommandText = sStrSQL

            myReader2 = par.cmd.ExecuteReader

            While myReader2.Read
                Dim genP As Epifani.Scale
                genP = New Epifani.Scale(par.IfNull(myReader2("COD"), -1), "", par.IfNull(myReader2("DESCRIZIONE"), " "))
                lstPiani.Add(genP)
                lstPianiAutoPompa.Add(genP)
                genP = Nothing
                bTrovato = True
            End While

            par.cmd.CommandText = ""
            myReader2.Close()


            ' PIANI IDRANTE 
            dlistPIANI = CType(Tab_Antincendio_Idranti.FindControl("CheckBoxPiano"), CheckBoxList)
            dlistPIANI.DataSource = lstPiani

            dlistPIANI.DataTextField = "SCALE_NO_TITLE"
            dlistPIANI.DataValueField = "ID"
            dlistPIANI.DataBind()


            ' PAINI ATTACCO AUTOPOMPA
            dlistPIANI = CType(Tab_Antincendio_Autopompa.FindControl("CheckBoxPiano"), CheckBoxList)
            dlistPIANI.DataSource = lstPianiAutoPompa

            dlistPIANI.DataTextField = "SCALE_NO_TITLE"
            dlistPIANI.DataValueField = "ID"
            dlistPIANI.DataBind()


            If bTrovato = True Then
                '*** PIANI IDRANTI
                CType(Tab_Antincendio_Idranti.FindControl("CheckBoxPiano"), CheckBoxList).Visible = True
                CType(Tab_Antincendio_Idranti.FindControl("lblATTENZIONE_M"), Label).Visible = False

                '*** PIANI ATTACCO AUTOPOMPA
                CType(Tab_Antincendio_Autopompa.FindControl("CheckBoxPiano"), CheckBoxList).Visible = True
                CType(Tab_Antincendio_Autopompa.FindControl("lblATTENZIONE_M"), Label).Visible = False

            Else
                '*** PIANI IDRANTI
                CType(Tab_Antincendio_Idranti.FindControl("CheckBoxPiano"), CheckBoxList).Visible = False
                CType(Tab_Antincendio_Idranti.FindControl("lblATTENZIONE_M"), Label).Visible = True

                '*** PIANI ATTACCO AUTOPOMPA
                CType(Tab_Antincendio_Autopompa.FindControl("CheckBoxPiano"), CheckBoxList).Visible = False
                CType(Tab_Antincendio_Autopompa.FindControl("lblATTENZIONE_M"), Label).Visible = True
            End If
            '########################################


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
        Dim i, j As Integer
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
        Dim SommaUI As Integer


        '*** FORM PRINCIPALE
        Me.txtIdImpianto.Text = par.IfNull(myReader1("ID"), "-1")

        Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
        FiltraEdifici()

        Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")
        FiltraScale()

        Me.txtDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtCodImpianto.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")
        'CodImpianto = par.IfNull(myReader1("COD_IMPIANTO"), "")

        Me.txtDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
        Me.txtAnnoRealizzazione.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))

        ' I_ANTINCENDIO
        par.cmd.CommandText = "select * from SISCOM_MI.I_ANTINCENDIO where SISCOM_MI.I_ANTINCENDIO.ID = " & vIdImpianto '& " FOR UPDATE NOWAIT"
        myReader2 = par.cmd.ExecuteReader()

        If myReader2.Read Then

            '*** TAB GENERALE
            'CType(Tab_Antincendio_Generale.FindControl("cmbTipo"), DropDownList).SelectedValue = par.IfNull(myReader2("ID_TIPOLOGIA_USO"), "")

            CType(Tab_Antincendio_Generale.FindControl("txtDittaGestione"), TextBox).Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
            CType(Tab_Antincendio_Generale.FindControl("txtNumTelefonico"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

            CType(Tab_Antincendio_Generale.FindControl("cmbGruppo"), DropDownList).Items.FindByValue(par.IfNull(myReader2("GRUPPO_ELETTROGENO"), "")).Selected = True

            CType(Tab_Antincendio_Generale.FindControl("cmbBox"), DropDownList).Items.FindByValue(par.IfNull(myReader2("PRESENZA_BOX"), "")).Selected = True
            'CType(Tab_Antincendio_Generale.FindControl("txtQuantitaEstintori"), TextBox).Text = par.IfNull(myReader2("NUM_ESTINTORI"), "")

            CType(Tab_Antincendio_Generale.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

        End If
        myReader2.Close()

        '*** TAB GENERALE (EDIFICI)
        Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)
        lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))

        par.cmd.CommandText = "select ID_EDIFICIO from SISCOM_MI.IMPIANTI_EDIFICI where  ID_IMPIANTO = " & vIdImpianto

        myReader2 = par.cmd.ExecuteReader()
        SommaUI = 0

        While myReader2.Read
            For i = 0 To CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
                If CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
                    CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True

                    '************
                    For j = 0 To lstEdifici.Count - 1

                        If lstEdifici(j).ID = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
                            SommaUI = SommaUI + lstEdifici(j).TOT_UNITA
                        End If

                    Next j
                End If
            Next
        End While
        myReader2.Close()

        CType(Tab_Antincendio_Generale.FindControl("txtTotUI"), TextBox).Text = SommaUI
        '**************************



        '*** SCALE
        par.cmd.CommandText = "select ID_SCALA from SISCOM_MI.I_ANT_SCALE where  SISCOM_MI.I_ANT_SCALE.ID_IMPIANTO = " & vIdImpianto
        myReader2 = par.cmd.ExecuteReader()

        While myReader2.Read

            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Value = par.IfNull(myReader2("ID_SCALA"), "-1") Then
                    CheckBoxScale.Items(i).Selected = True
                End If
            Next
        End While
        myReader2.Close()

        'DISABILITO COMBO COMPLESSO ED EDIFICIO
        Me.cmbComplesso.Enabled = False

        If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
            Me.DrLEdificio.Enabled = False
        End If

        'myReader2 = par.cmd.ExecuteReader()

        ''                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'While myReader2.Read
        '    For i = 0 To CType(TabGenerale.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
        '        If CType(TabGenerale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
        '            CType(TabGenerale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True

        '            '************
        '            For j = 0 To lstEdifici.Count - 1

        '                If lstEdifici(j).ID = par.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
        '                    SommaMQ = SommaMQ + lstEdifici(j).DIMENSIONE
        '                    SommaUI = SommaUI + lstEdifici(j).TOT_UNITA
        '                End If

        '            Next j
        '            '********************************
        '        End If
        '    Next
        'End While
        'myReader2.Close()

        '**IDINTERVENTO = par.IfNull(myReader1("ID_TIPO_INTERVENTO"), "-1")

        'CType(TabGenerale.FindControl("txtTotUI"), TextBox).Text = SommaUI
        'CType(TabGenerale.FindControl("txtTotMq"), TextBox).Text = SommaMQ

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

            If vIdImpianto <> 0 Then
                ' LEGGO IMPIANTI

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

        'If CType(Tab_Antincendio_Generale.FindControl("cmbTipo"), DropDownList).SelectedValue = -1 Then
        '    Response.Write("<script>alert('Selezionare la tipologia dell\'impianto!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        If Me.txtAnnoRealizzazione.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazione.Text = ""
        End If


    End Function

    Private Sub Salva()
        Dim i As Integer
        Dim vIdMotopompa As Integer
        Dim vIdIdrante As Integer
        Dim vIdEstintore As Integer
        Dim vIdAutoPompa As Integer

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
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
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "AN"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Antincendio_Generale.FindControl("txtNote"), TextBox).Text, 4000)))


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DATI_IMPIANTO, "")


            'SCALE
            par.cmd.CommandText = ""

            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                    par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                               & "(" & vIdImpianto & "," & CheckBoxScale.Items(i).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    '*** EVENTI_IMPIANTI
                    par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scale Servite")
                Else
                    ' -1 = C.T.
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) = -1 Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                                   & "(" & vIdImpianto & ",-1)"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "C.T. Servita")

                    End If

                    '-2 = BOX
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) = -2 Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                                   & "(" & vIdImpianto & ",-2)"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "BOX Serviti")
                    End If
                End If
            Next


            ' I_ANTINCENDIO 
            ' note ID_TIPOLOGIA_USO e NUM_ESTINTORI non più usati
            par.cmd.CommandText = "insert into SISCOM_MI.I_ANTINCENDIO (ID,DITTA_GESTIONE,TELEFONO_DITTA," _
                                                                    & "GRUPPO_ELETTROGENO,PRESENZA_BOX) " _
                                 & "values (:id,:ditta_gestione,:num_tel,:gruppo,:box)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdImpianto))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("uso", CType(Tab_Antincendio_Generale.FindControl("cmbTipo"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Antincendio_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Antincendio_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("gruppo", CType(Tab_Antincendio_Generale.FindControl("cmbGruppo"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("box", CType(Tab_Antincendio_Generale.FindControl("cmbBox"), DropDownList).SelectedValue.ToString()))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("estintori", CType(Tab_Antincendio_Generale.FindControl("txtQuantitaEstintori"), TextBox).Text))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*******************************

            ' IMPIANTI_EDIFICI
            AggiornaImpiantiEdifici()


            '*** INSERIMENTO SERBATOI ACCUMULO
            Dim lstSerbatoiAccumulo As System.Collections.Generic.List(Of Epifani.SerbatoiAccumulo)

            lstSerbatoiAccumulo = CType(HttpContext.Current.Session.Item("LSTSERBATOIACCUMULO"), System.Collections.Generic.List(Of Epifani.SerbatoiAccumulo))

            For Each gen As Epifani.SerbatoiAccumulo In lstSerbatoiAccumulo

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SERBATOI  " _
                                            & " (ID,ID_IMPIANTO,ID_UBICAZIONE_EDIFICIO,ID_UBICAZIONE_SCALA,CAPACITA) " _
                                    & "values (SISCOM_MI.SEQ_I_ANT_SERBATOI.NEXTVAL,:id_impianto,:id_edificio,:id_scala,:capacita) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", gen.ID_UBICAZIONE_EDIFICIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", gen.ID_UBICAZIONE_SCALA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("capacita", strToNumber(gen.CAPACITA)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Serbatoio Accumulo")

            Next
            '********************************

            '*** INSERIMENTO MOTOPOMPE UNI 70
            Dim lstMotopompe As System.Collections.Generic.List(Of Epifani.Motopompe)

            lstMotopompe = CType(HttpContext.Current.Session.Item("LSTMOTOPOMPE"), System.Collections.Generic.List(Of Epifani.Motopompe))

            For Each gen As Epifani.Motopompe In lstMotopompe

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_MOTOPOMPE  " _
                                            & " (ID,ID_IMPIANTO,ID_UBICAZIONE_EDIFICIO,ID_UBICAZIONE_SCALA)" _
                                    & "values (SISCOM_MI.SEQ_I_ANT_MOTOPOMPE.NEXTVAL,:id_impianto,:id_edificio,:id_scala) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", gen.ID_UBICAZIONE_EDIFICIO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", gen.ID_UBICAZIONE_SCALA))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Motopompa UNI 70")


                '**** Ricavo ID della Motopompa
                par.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_MOTOPOMPE.CURRVAL FROM dual "
                Dim myReaderScale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderScale.Read Then
                    vIdMotopompa = myReaderScale(0)
                End If
                myReaderScale.Close()
                par.cmd.CommandText = ""

                '***********
                Dim lstScaleSel As System.Collections.Generic.List(Of Epifani.Scale)
                lstScaleSel = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

                For Each genScale As Epifani.Scale In lstScaleSel

                    If genScale.DENOMINAZIONE_EDIFICIO = gen.ID Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_MOTOPOMPE_SCALE  (ID_I_ANT_MOTOPOMPE,ID_SCALA) values " _
                                   & "(" & vIdMotopompa & "," & genScale.DENOMINAZIONE_SCALA & ")"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next

                '********************************
            Next
            '********************************


            '*** INSERIMENTO SPRINKLER
            Dim lstSprinkler As System.Collections.Generic.List(Of Epifani.Sprinkler)

            lstSprinkler = CType(HttpContext.Current.Session.Item("LSTSPRINKLER"), System.Collections.Generic.List(Of Epifani.Sprinkler))

            For Each gen As Epifani.Sprinkler In lstSprinkler

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SPRINKLER (ID, ID_IMPIANTO,ALLACCIAMENTO,ID_TIPOLOGIA_SPRINKLER,CERTIFICAZIONI) " _
                                    & "values (SISCOM_MI.SEQ_I_ANT_SPRINKLER.NEXTVAL,:id_impianto,:allacciamento,:sprinkler,:cert) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("allacciamento", gen.ALLACCIAMENTO))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("sprinkler", RitornaNullSeIntegerMenoUno(Convert.ToInt32(gen.ID_TIPOLOGIA_SPRINKLER))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cert", gen.CERTIFICAZIONI))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Sprinkler")

            Next
            '********************************


            '*** INSERIMENTO RILEVATORE FUMI
            Dim lstFumi As System.Collections.Generic.List(Of Epifani.RilevatoreFumi)

            lstFumi = CType(HttpContext.Current.Session.Item("LSTFUMI"), System.Collections.Generic.List(Of Epifani.RilevatoreFumi))

            For Each gen As Epifani.RilevatoreFumi In lstFumi

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_RILEVAZIONE_FUMI (ID, ID_IMPIANTO,UBICAZIONE_CENTRALINA,ID_TIPOLOGIA_RILEVAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_ANT_RILEVAZIONE_FUMI.NEXTVAL,:id_impianto,:ubicazione,:fumi) "


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", gen.UBICAZIONE_CENTRALINA))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fumi", RitornaNullSeIntegerMenoUno(Convert.ToInt32(gen.ID_TIPOLOGIA_RILEVAZIONE))))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Rilevatore Fumi")

            Next
            '********************************

            '*** INSERIMENTO IDRANTI + VERIFICHE
            Dim lstIdranti As System.Collections.Generic.List(Of Epifani.Idranti)

            lstIdranti = CType(HttpContext.Current.Session.Item("LSTIDRANTI"), System.Collections.Generic.List(Of Epifani.Idranti))

            For Each gen As Epifani.Idranti In lstIdranti


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_IDRANTI (ID,ID_IMPIANTO, LOCALIZZAZIONE, NUM_IDRANTI,DIAMETRO) " _
                                & " values (SISCOM_MI.SEQ_I_ANT_IDRANTI.NEXTVAL,:id_impianto,:localizzazione,:num_idranti,:diametro)"


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("localizzazione", par.PulisciStringaInvio(gen.LOCALIZZAZIONE, 200)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_idranti", strToNumber(gen.NUM_IDRANTI)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("diametro", strToNumber(gen.DIAMETRO)))


                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""


                '**** Ricavo ID dell'IDRANTE
                par.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_IDRANTI.CURRVAL FROM dual "
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderI.Read Then
                    vIdIdrante = myReaderI(0)
                End If
                myReaderI.Close()
                par.cmd.CommandText = ""
                '**********


                '***********
                Dim lstPianiSel As System.Collections.Generic.List(Of Epifani.Scale)
                lstPianiSel = CType(HttpContext.Current.Session.Item("LSTPIANIIDRANTI_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

                For Each genPiani As Epifani.Scale In lstPianiSel

                    If genPiani.DENOMINAZIONE_EDIFICIO = gen.ID Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_IDRANTI_PIANI  (ID_ANT_IDRANTI,COD_LIVELLO_PIANO) values " _
                                   & "(" & vIdIdrante & ",'" & genPiani.DENOMINAZIONE_SCALA & "')"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Piani dell'idrante")

                    End If
                Next
                '********************************

                If Strings.Len(Strings.Trim(gen.DATA)) > 0 Then
                    'INSERISCO LA VERIFICA NUOVA
                    par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO,ID_COMPONENTE) " _
                                    & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico,:id_componente )"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ID"))
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

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_componente", vIdIdrante))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                End If

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Inserimento IDRANTI/NASPI")

            Next
            '********************************

            '*** INSERIMENTO estintori + VERIFICHE
            Dim lstEstintori As System.Collections.Generic.List(Of Epifani.Estintori)

            lstEstintori = CType(HttpContext.Current.Session.Item("LSTESTINTORI"), System.Collections.Generic.List(Of Epifani.Estintori))

            For Each gen As Epifani.Estintori In lstEstintori


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_ESTINTORI (ID,ID_IMPIANTO, ESTINTORI) " _
                                & " values (SISCOM_MI.SEQ_I_ANT_ESTINTORI.NEXTVAL,:id_impianto,:estintori)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("estintori", strToNumber(gen.ESTINTORI)))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""


                '**** Ricavo ID dell'IDRANTE
                par.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_ESTINTORI.CURRVAL FROM dual "
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderI.Read Then
                    vIdEstintore = myReaderI(0)
                End If
                myReaderI.Close()
                par.cmd.CommandText = ""
                '**********


                If Strings.Len(Strings.Trim(gen.DATA)) > 0 Then
                    'INSERISCO LA VERIFICA NUOVA
                    par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO,ID_COMPONENTE) " _
                                    & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico,:id_componente )"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ES"))
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

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_componente", vIdEstintore))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                End If

                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Inserimento ESTINTORI")

            Next
            '********************************



            '*** INSERIMENTO ATTACCO AUTOPOMPA
            Dim lstAutoPompa As System.Collections.Generic.List(Of Epifani.AutoPompa)

            lstAutoPompa = CType(HttpContext.Current.Session.Item("LSTAUTOPOMPA"), System.Collections.Generic.List(Of Epifani.AutoPompa))

            For Each gen As Epifani.AutoPompa In lstAutoPompa

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_AUTOPOMPA (ID,ID_IMPIANTO, BOCCA_COLLEGAMENTO) " _
                                & " values (SISCOM_MI.SEQ_I_ANT_AUTOPOMPA.NEXTVAL,:id_impianto,:bocca)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("bocca", gen.BOCCA_COLLEGAMENTO))


                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = ""


                '**** Ricavo ID dell'ATTACCO AUTOPOMA
                par.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_AUTOPOMPA.CURRVAL FROM dual "
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderI.Read Then
                    vIdAutoPompa = myReaderI(0)
                End If
                myReaderI.Close()
                par.cmd.CommandText = ""
                '**********


                '***********
                Dim lstPianiAutoPompaSel As System.Collections.Generic.List(Of Epifani.Scale)
                lstPianiAutoPompaSel = CType(HttpContext.Current.Session.Item("LSTPIANIAUTOPOMPA_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

                For Each genPianiA As Epifani.Scale In lstPianiAutoPompaSel

                    If genPianiA.DENOMINAZIONE_EDIFICIO = gen.ID Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_AUTOPOMPA_PIANI  (ID_ANT_AUTOPOMPA,COD_LIVELLO_PIANO) values " _
                                   & "(" & vIdAutoPompa & ",'" & genPianiA.DENOMINAZIONE_SCALA & "')"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Piani dell'Attacco Autopompa")

                    End If
                Next
                '********************************


                '*** EVENTI_IMPIANTI
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Inserimento ATTACCO AUTOPOMPA")

            Next
            '********************************





            'INSERISCO LA VERIFICA NUOVA
            Dim lstVerifiche As System.Collections.Generic.List(Of Epifani.VerificheImpianti)

            lstVerifiche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE"), System.Collections.Generic.List(Of Epifani.VerificheImpianti))

            For Each gen As Epifani.VerificheImpianti In lstVerifiche

                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO) " _
                                & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico )"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "PR"))
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
                par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_VERIFICA_IMPIANTO, "Verifica Dichiarazione Pressione Residua")

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

            '*** SERBATOI ACCUMULO
            par.cmd.CommandText = "select SISCOM_MI.I_ANT_SERBATOI.ID,SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_SCALA," _
                            & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                            & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA"",SISCOM_MI.I_ANT_SERBATOI.CAPACITA" _
              & " from  SISCOM_MI.I_ANT_SERBATOI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
              & " where SISCOM_MI.I_ANT_SERBATOI.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
              & " and   SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
              & " order by SISCOM_MI.I_ANT_SERBATOI.ID "

            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds1 As New Data.DataSet()

            da1.Fill(ds1, "I_ANT_SERBATOI")

            CType(Tab_Antincendio_Dettagli.FindControl("DataGridSerbatoio"), DataGrid).DataSource = ds1
            CType(Tab_Antincendio_Dettagli.FindControl("DataGridSerbatoio"), DataGrid).DataBind()
            ds1.Dispose()
            '*******************************


            '*** MOTOPOMPA UNI 70
            par.cmd.CommandText = "select SISCOM_MI.I_ANT_MOTOPOMPE.ID,SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_SCALA," _
                            & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                            & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA""," _
                            & " (select count(*) from SISCOM_MI.I_ANT_MOTOPOMPE_SCALE where  SISCOM_MI.I_ANT_MOTOPOMPE_SCALE.ID_I_ANT_MOTOPOMPE=SISCOM_MI.I_ANT_MOTOPOMPE.ID) AS ""SCALE_SERVITE"" " _
              & " from  SISCOM_MI.I_ANT_MOTOPOMPE,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
              & " where SISCOM_MI.I_ANT_MOTOPOMPE.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
              & " and   SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
              & " order by SISCOM_MI.I_ANT_MOTOPOMPE.ID "

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds2 As New Data.DataSet()

            da2.Fill(ds2, "I_ANT_MOTOPOMPE ")

            CType(Tab_Antincendio_Dettagli.FindControl("DataGridMotopompa"), DataGrid).DataSource = ds2
            CType(Tab_Antincendio_Dettagli.FindControl("DataGridMotopompa"), DataGrid).DataBind()
            ds2.Dispose()
            '*******************************


            '*** SPRINKLER
            par.cmd.CommandText = "select SISCOM_MI.I_ANT_SPRINKLER.ID,SISCOM_MI.I_ANT_SPRINKLER.ALLACCIAMENTO," _
                            & " SISCOM_MI.TIPOLOGIA_SPRINKLER.DESCRIZIONE AS ""SPRINKLER"",SISCOM_MI.I_ANT_SPRINKLER.CERTIFICAZIONI,SISCOM_MI.I_ANT_SPRINKLER.ID_TIPOLOGIA_SPRINKLER " _
                  & " from SISCOM_MI.I_ANT_SPRINKLER,SISCOM_MI.TIPOLOGIA_SPRINKLER " _
                  & " where SISCOM_MI.I_ANT_SPRINKLER.ID_IMPIANTO = " & vIdImpianto _
                  & " and SISCOM_MI.I_ANT_SPRINKLER.ID_TIPOLOGIA_SPRINKLER=SISCOM_MI.TIPOLOGIA_SPRINKLER.ID (+) " _
                  & " order by SISCOM_MI.I_ANT_SPRINKLER.ALLACCIAMENTO "

            Dim daS As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsS As New Data.DataSet()

            daS.Fill(dsS, "I_ANT_SPRINKLER ")

            CType(Tab_Antincendio_Sprinkler.FindControl("DataGridS"), DataGrid).DataSource = dsS
            CType(Tab_Antincendio_Sprinkler.FindControl("DataGridS"), DataGrid).DataBind()
            dsS.Dispose()
            '*******************************


            '*** RILEVATORE FUMI
            par.cmd.CommandText = "select SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID," _
                                            & " SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI.DESCRIZIONE AS ""TIPOLOGIA_FUMI"",SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.UBICAZIONE_CENTRALINA,SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID_TIPOLOGIA_RILEVAZIONE " _
                                  & " from SISCOM_MI.I_ANT_RILEVAZIONE_FUMI,SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI " _
                                  & " where SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID_IMPIANTO = " & vIdImpianto _
                                  & " and SISCOM_MI.I_ANT_RILEVAZIONE_FUMI.ID_TIPOLOGIA_RILEVAZIONE=SISCOM_MI.TIPOLOGIA_RILEVAZIONE_FUMI.ID (+) " _
                                  & " order by TIPOLOGIA_FUMI "

            Dim daF As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsF As New Data.DataSet()

            daF.Fill(dsF, "I_ANT_RILEVAZIONE_FUMI ")

            CType(Tab_Antincendio_Fumi.FindControl("DataGridF"), DataGrid).DataSource = dsF
            CType(Tab_Antincendio_Fumi.FindControl("DataGridF"), DataGrid).DataBind()
            dsF.Dispose()
            '*******************************


            '*** IDRANTI/NASPI + VERIFICHE
            par.cmd.CommandText = "  select SISCOM_MI.I_ANT_IDRANTI.ID,(select count(*) from SISCOM_MI.I_ANT_IDRANTI_PIANI where  SISCOM_MI.I_ANT_IDRANTI_PIANI.ID_ANT_IDRANTI=SISCOM_MI.I_ANT_IDRANTI.ID) AS ""PIANI""," _
                                    & " SISCOM_MI.I_ANT_IDRANTI.DIAMETRO, SISCOM_MI.I_ANT_IDRANTI.NUM_IDRANTI, SISCOM_MI.I_ANT_IDRANTI.LOCALIZZAZIONE, " _
                                    & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                    & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.I_ANT_IDRANTI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.I_ANT_IDRANTI.ID_IMPIANTO=" & vIdImpianto _
                                    & " and SISCOM_MI.I_ANT_IDRANTI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ID' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) " _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


            Dim daI As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsI As New Data.DataSet()

            daI.Fill(dsI, "I_ANT_IDRANTI ")

            CType(Tab_Antincendio_Idranti.FindControl("DataGridI"), DataGrid).DataSource = dsI
            CType(Tab_Antincendio_Idranti.FindControl("DataGridI"), DataGrid).DataBind()
            dsI.Dispose()
            '*******************************


            '*** ESTINTORI + VERIFICHE
            par.cmd.CommandText = "  select SISCOM_MI.I_ANT_ESTINTORI.ID,SISCOM_MI.I_ANT_ESTINTORI.ESTINTORI, " _
                                    & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                    & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                    & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.I_ANT_ESTINTORI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.I_ANT_ESTINTORI.ID_IMPIANTO=" & vIdImpianto _
                                    & " and SISCOM_MI.I_ANT_ESTINTORI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ES' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                    & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) " _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


            Dim daE As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsE As New Data.DataSet()

            daE.Fill(dsE, "I_ANT_ESTINTORI ")

            CType(Tab_Antincendio_Estintori.FindControl("DataGridE"), DataGrid).DataSource = dsE
            CType(Tab_Antincendio_Estintori.FindControl("DataGridE"), DataGrid).DataBind()
            dsE.Dispose()
            '*******************************


            '*** ATTACCO AUTOPOMPA
            par.cmd.CommandText = "  select SISCOM_MI.I_ANT_AUTOPOMPA.ID," _
                                    & " (select count(*) from SISCOM_MI.I_ANT_AUTOPOMPA_PIANI where  SISCOM_MI.I_ANT_AUTOPOMPA_PIANI.ID_ANT_AUTOPOMPA=SISCOM_MI.I_ANT_AUTOPOMPA.ID) AS ""PIANI""," _
                                    & " SISCOM_MI.I_ANT_AUTOPOMPA.BOCCA_COLLEGAMENTO " _
                                & " from SISCOM_MI.I_ANT_AUTOPOMPA " _
                                & " where SISCOM_MI.I_ANT_AUTOPOMPA.ID_IMPIANTO=" & vIdImpianto _
                                & " order by PIANI "

            Dim daA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsA As New Data.DataSet()

            daA.Fill(dsA, "I_ANT_AUTOPOMPA ")

            CType(Tab_Antincendio_Autopompa.FindControl("DataGridAUTOP"), DataGrid).DataSource = dsA
            CType(Tab_Antincendio_Autopompa.FindControl("DataGridAUTOP"), DataGrid).DataBind()
            dsA.Dispose()
            '*******************************


            '*** VERIFICHE
            par.cmd.CommandText = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                        & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                        & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                                & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                                & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PR'" _
                                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                                & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "

            Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dsV As New Data.DataSet()

            daV.Fill(dsV, "IMPIANTI_VERIFICHE ")

            CType(Tab_Antincendio_Verifiche.FindControl("DataGrid1"), DataGrid).DataSource = dsV
            CType(Tab_Antincendio_Verifiche.FindControl("DataGrid1"), DataGrid).DataBind()
            dsV.Dispose()
            '*******************************


            'DISABILITO COMBO COMPLESSO ED EDIFICIO
            Me.cmbComplesso.Enabled = False

            If Strings.Len(Strings.Trim(Me.DrLEdificio.SelectedItem.Text)) > 0 Then
                Me.DrLEdificio.Enabled = False
            End If


            'ABILITO I BOTTONI DELLE VERIFICHE
            CType(Tab_Antincendio_Verifiche.FindControl("btnAgg"), ImageButton).Visible = True
            CType(Tab_Antincendio_Verifiche.FindControl("btnApri"), ImageButton).Visible = True
            CType(Tab_Antincendio_Verifiche.FindControl("btnVisualizza"), ImageButton).Visible = True
            CType(Tab_Antincendio_Verifiche.FindControl("btnElimina"), ImageButton).Visible = True

            '**************************


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID = " & vIdImpianto & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

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
        Dim i As Integer

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            ' IMPIANTI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "update SISCOM_MI.IMPIANTI set ID_COMPLESSO=:id_complesso, ID_EDIFICIO=:id_edificio,COD_TIPOLOGIA=:cod_tipologia,DESCRIZIONE=:descrizione,ANNO_COSTRUZIONE=:anno,DITTA_COSTRUTTRICE=:ditta,ANNOTAZIONI=:annotazioni where ID=:id_impianto"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", Convert.ToInt32(Me.cmbComplesso.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.DrLEdificio.SelectedValue))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_tipologia", "AN"))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", par.AggiustaData(txtAnnoRealizzazione.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(txtDitta.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("annotazioni", Strings.Left(CType(Tab_Antincendio_Generale.FindControl("txtNote"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DATI_IMPIANTO, "")

            '************************************

            'SCALE
            par.cmd.CommandText = "delete from SISCOM_MI.I_ANT_SCALE where ID_IMPIANTO = " & vIdImpianto
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""


            For i = 0 To CheckBoxScale.Items.Count - 1
                If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                    par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                               & "(" & vIdImpianto & "," & CheckBoxScale.Items(i).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    '*** EVENTI_IMPIANTI
                    'par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Scale Servite")
                Else
                    ' -1 = C.T.
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) = -1 Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                                   & "(" & vIdImpianto & ",-1)"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                    End If

                    '-2 = BOX
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) = -2 Then
                        par.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SCALE (ID_IMPIANTO,ID_SCALA) values " _
                                   & "(" & vIdImpianto & ",-2)"

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                    End If

                End If
            Next


            'I_ANTINCENDIO
            par.cmd.CommandText = ""
            par.cmd.CommandText = " update SISCOM_MI.I_ANTINCENDIO " _
                                & " set DITTA_GESTIONE=:ditta_gestione,TELEFONO_DITTA=:num_tel, " _
                                     & "GRUPPO_ELETTROGENO=:gruppo,PRESENZA_BOX=:box " _
                                & " where ID=:id_impianto"

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("uso", CType(Tab_Antincendio_Generale.FindControl("cmbTipo"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta_gestione", Strings.Left(CType(Tab_Antincendio_Generale.FindControl("txtDittaGestione"), TextBox).Text, 300)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_tel", Strings.Left(CType(Tab_Antincendio_Generale.FindControl("txtNumTelefonico"), TextBox).Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("gruppo", CType(Tab_Antincendio_Generale.FindControl("cmbGruppo"), DropDownList).SelectedValue.ToString()))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("box", CType(Tab_Antincendio_Generale.FindControl("cmbBox"), DropDownList).SelectedValue.ToString()))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("estintori", CType(Tab_Antincendio_Generale.FindControl("txtQuantitaEstintori"), TextBox).Text))
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

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO ANTINCENDIO"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                sNote = "Cancellazione Impianto Antincendio del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
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


    Private Sub AbilitazioneOggetti(ByVal ValorePass As Boolean)
        Try

            cmbComplesso.Enabled = ValorePass
            DrLEdificio.Enabled = ValorePass

            txtDenominazione.ReadOnly = Not (ValorePass)
            txtCodImpianto.ReadOnly = True


        Catch ex As Exception

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
        End Try
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


    Private Sub AggiornaImpiantiEdifici()
        Dim i As Integer

        Try

        Catch ex As Exception
        End Try

        par.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_EDIFICI where ID_IMPIANTO = " & vIdImpianto
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = ""

        For i = 0 To CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items.Count - 1
            If CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Selected = True Then
                par.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_EDIFICI (ID_IMPIANTO,ID_EDIFICIO) values " _
                           & "(" & vIdImpianto & "," & CType(Tab_Antincendio_Generale.FindControl("CheckBoxEdifici"), CheckBoxList).Items(i).Value & ")"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                '*** EVENTI_IMPIANTI
                'par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Edifici Alimentati")

            End If
        Next

    End Sub


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function



End Class
